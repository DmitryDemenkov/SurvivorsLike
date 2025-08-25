using Unity.Entities;

namespace SurvivorsLike
{
    public partial struct Health : IComponentData
    {
        public int MaxHealth;
        public int Value;
    }

    public struct DamageThisFrame : IBufferElementData
    {
        public int Value;
    }
}
