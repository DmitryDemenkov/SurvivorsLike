using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

namespace SurvivorsLike
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    [UpdateBefore(typeof(EndSimulationEntityCommandBufferSystem))]
    public partial struct DestroySystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var endEcbSystem = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var endEcb = endEcbSystem.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (_, entity) in SystemAPI.Query<DestroyEntityFlag>().WithEntityAccess())
            {
                if (SystemAPI.HasComponent<PlayerTag>(entity))
                {
                    Object.Destroy(SystemAPI.GetComponent<Animated>(entity).Animator.Value.gameObject);
                    if (UIController.Singleton != null)
                    {
                        UIController.Singleton.EnableGameOverPanel(true);
                    }
                }

                if (SystemAPI.HasBuffer<Child>(entity))
                {
                    var children = SystemAPI.GetBuffer<Child>(entity);
                    foreach (var child in children)
                    {
                        endEcb.DestroyEntity(child.Value);
                    }
                }
                endEcb.DestroyEntity(entity);
            }
        }
    }
}