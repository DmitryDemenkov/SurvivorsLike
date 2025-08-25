using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Burst;

namespace SurvivorsLike
{
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    public partial struct ShaderAnimationSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (animation, spriteIndex, transform, parent) in 
                SystemAPI.Query<RefRW<ShaderAnimation>, RefRW<SpriteIndex>, RefRW<LocalTransform>, Parent>())
            {
                animation.ValueRW.Time = (float)SystemAPI.Time.ElapsedTime;

                float nextIndex = animation.ValueRO.Time * animation.ValueRO.Speed * animation.ValueRO.FramePerSecond % animation.ValueRO.FramePerSecond;
                nextIndex = math.floor(nextIndex);

                spriteIndex.ValueRW.Value = nextIndex;

                if (SystemAPI.HasComponent<Movement>(parent.Value))
                {
                    Movement movement = SystemAPI.GetComponent<Movement>(parent.Value);
                    transform.ValueRW.Rotation.value.y = movement.Direction.x >= 0 ? 0 : 1;
                }
            }
        }
    }
}