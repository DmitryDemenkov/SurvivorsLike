using Unity.Entities;
using Unity.Physics;
using Unity.Mathematics;

namespace SurvivorsLike
{
    public struct CharacterInitializationFlag : IComponentData, IEnableableComponent { }

    public struct AnimatedInitializationFlag : IComponentData, IEnableableComponent { }

    public struct InputInitializationFlag : IComponentData, IEnableableComponent { }

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct CharacterInitializationSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (mass, flag) in SystemAPI.Query<RefRW<PhysicsMass>, EnabledRefRW<CharacterInitializationFlag>>())
            {
                mass.ValueRW.InverseInertia = float3.zero;
                flag.ValueRW = false;
            }
        }
    }

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct PlayerInitializationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            if (PlayerAnimationUnity.Singleton == null || WeaponAnimationUnity.Sinlgeton == null) return;

            BeginInitializationEntityCommandBufferSystem.Singleton ecbSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            EntityCommandBuffer ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (flag, entity) in SystemAPI.Query<EnabledRefRW<AnimatedInitializationFlag>>().WithEntityAccess())
            {
                ecb.AddComponent(entity, new Animated
                {
                    Animator = PlayerAnimationUnity.Singleton,
                    WeaponAnimation = WeaponAnimationUnity.Sinlgeton
                });
                flag.ValueRW = false;
            }
        }
    }

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct InputInitializationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            if (InputManager.Singleton == null) return;

            BeginInitializationEntityCommandBufferSystem.Singleton ecbSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            EntityCommandBuffer ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (flag, entity) in SystemAPI.Query<EnabledRefRW<InputInitializationFlag>>().WithEntityAccess())
            {
                ecb.AddComponent(entity, new InputSingleton { Input = InputManager.Singleton });
                flag.ValueRW = false;
            }
        }
    }
}
