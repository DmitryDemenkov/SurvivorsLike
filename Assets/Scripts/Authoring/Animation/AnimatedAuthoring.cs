using UnityEngine;
using Unity.Entities;

namespace SurvivorsLike
{
    public class AnimatedAuthoring : MonoBehaviour
    {
        public PlayerAnimationUnity animatorUnity;

        private class Baker : Baker<AnimatedAuthoring>
        {
            public override void Bake(AnimatedAuthoring authoring)
            {
                Entity animatedEntity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<AnimatedInitializationFlag>(animatedEntity);
                SetComponentEnabled<AnimatedInitializationFlag>(animatedEntity, true);
            }
        }
    }
}