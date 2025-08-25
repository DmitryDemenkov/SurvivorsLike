using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace SurvivorsLike
{
    [UpdateInGroup(typeof(PresentationSystemGroup), OrderFirst = true)]
    public partial struct AnimatedSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Animated>();
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (anim, movement, transform) in SystemAPI.Query<RefRW<Animated>, Movement, LocalTransform>())
            {
                anim.ValueRW.Animator.Value.SetTransform(transform.Position.x, transform.Position.y);
                anim.ValueRW.Animator.Value.SetSpeed(math.length(movement.Direction));
            }
        }
    }
}
