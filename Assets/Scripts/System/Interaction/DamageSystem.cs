using Unity.Entities;

namespace SurvivorsLike
{
    public partial struct DamageSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (health, damageThisFrame, entity) in SystemAPI.Query<RefRW<Health>, DynamicBuffer<DamageThisFrame>>().WithDisabled<DestroyEntityFlag>().WithEntityAccess())
            {
                if (damageThisFrame.IsEmpty) continue;
                foreach (var damage in damageThisFrame)
                {
                    health.ValueRW.Value -= damage.Value;
                }

                damageThisFrame.Clear();

                if (health.ValueRO.Value <= 0)
                {
                    SystemAPI.SetComponentEnabled<DestroyEntityFlag>(entity, true);
                }

                if (SystemAPI.HasComponent<PlayerTag>(entity) && UIController.Singleton != null)
                {
                    UIController.Singleton.SetHealthRatio(health.ValueRO.Value / (float)health.ValueRO.MaxHealth);
                }
            }
        }
    }
}