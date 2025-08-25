using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Burst;

namespace SurvivorsLike
{
    public partial struct EnemyMovementSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            Entity playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            float2 playerPosition = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position.xy;

            var movementJob = new EnemyMovementJob
            {
                PlayerPosition = playerPosition
            };

            state.Dependency = movementJob.ScheduleParallel(state.Dependency);
        }
    }

    [BurstCompile]
    [WithAll(typeof(EnemyTag))]
    public partial struct EnemyMovementJob : IJobEntity
    {
        public float2 PlayerPosition;

        public void Execute(ref Movement move, in LocalTransform transform)
        {
            float2 vectoreToPlayer = PlayerPosition - transform.Position.xy;
            move.Direction = math.normalize(vectoreToPlayer);
        }
    }
}
