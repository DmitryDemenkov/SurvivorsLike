using Unity.Entities;

namespace SurvivorsLike
{
    public struct Animated : IComponentData
    {
        public UnityObjectRef<PlayerAnimationUnity> Animator;
        public UnityObjectRef<WeaponAnimationUnity> WeaponAnimation;
    }
}
