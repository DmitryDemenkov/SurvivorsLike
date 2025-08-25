using Unity.Entities;

namespace SurvivorsLike
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct TemporarySystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (temp, entity) in SystemAPI.Query<RefRW<Temporary>>().WithDisabled<DestroyEntityFlag>().WithEntityAccess())
            {
                temp.ValueRW.LifeTime -= SystemAPI.Time.DeltaTime;
                if (temp.ValueRO.LifeTime <= 0)
                {
                    SystemAPI.SetComponentEnabled<DestroyEntityFlag>(entity, true);
                }
            }
        }
    }
}
