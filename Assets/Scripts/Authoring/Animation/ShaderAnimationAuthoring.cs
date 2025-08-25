using UnityEngine;
using Unity.Entities;

namespace SurvivorsLike
{
    public class ShaderAnimationAuthoring : MonoBehaviour
    {
        public int framePerSecond;
        public float animationSpeed;

        private class Baker : Baker<ShaderAnimationAuthoring>
        {
            public override void Bake(ShaderAnimationAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new ShaderAnimation
                {
                    Time = 0,
                    FramePerSecond = authoring.framePerSecond,
                    Speed = authoring.animationSpeed
                });
                AddComponent<SpriteIndex>(entity);
            }
        }
    }
}
