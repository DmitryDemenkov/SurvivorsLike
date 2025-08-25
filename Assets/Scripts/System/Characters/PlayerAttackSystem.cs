using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

namespace SurvivorsLike
{
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(AfterPhysicsSystemGroup))]
    public partial struct PlayerAttackSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var attackJob = new PlayerAttackJob
            {
                EnemyLookup = SystemAPI.GetComponentLookup<EnemyTag>(true),
                AttackLookup = SystemAPI.GetComponentLookup<PlayerAttack>(true),
                DestroyFlagLookup = SystemAPI.GetComponentLookup<DestroyEntityFlag>(),
                DamageBufferLookup = SystemAPI.GetBufferLookup<DamageThisFrame>(),
            };

            var simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
            state.Dependency = attackJob.Schedule(simulationSingleton, state.Dependency);
        }
    }

    [BurstCompile]
    public struct PlayerAttackJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentLookup<EnemyTag> EnemyLookup;
        [ReadOnly] public ComponentLookup<PlayerAttack> AttackLookup;
        public ComponentLookup<DestroyEntityFlag> DestroyFlagLookup;
        public BufferLookup<DamageThisFrame> DamageBufferLookup;

        public void Execute(TriggerEvent collisionEvent)
        {
            Entity enemyEntity;
            Entity playerAttack;

            if (EnemyLookup.HasComponent(collisionEvent.EntityA) && AttackLookup.HasComponent(collisionEvent.EntityB))
            {
                enemyEntity = collisionEvent.EntityA;
                playerAttack = collisionEvent.EntityB;
            }
            else if (EnemyLookup.HasComponent(collisionEvent.EntityB) && AttackLookup.HasComponent(collisionEvent.EntityA))
            {
                enemyEntity = collisionEvent.EntityB;
                playerAttack = collisionEvent.EntityA;
            }
            else
            {
                return;
            }

            PlayerAttack attack = AttackLookup[playerAttack];
            DynamicBuffer<DamageThisFrame> enemyDamageBuffer = DamageBufferLookup[enemyEntity];
            enemyDamageBuffer.Add(new DamageThisFrame
            {
                Value = attack.HitPoints
            });

            DestroyFlagLookup.SetComponentEnabled(playerAttack, true);
        }
    }
}