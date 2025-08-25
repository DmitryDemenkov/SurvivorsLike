using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace SurvivorsLike
{
    public partial struct MovementSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (velocity, movement) in SystemAPI.Query<RefRW<PhysicsVelocity>, Movement>())
            {
                float2 moveVector = movement.Direction * movement.Speed;
                velocity.ValueRW.Linear = new float3(moveVector, 0f);
            }
        }
    }
}
