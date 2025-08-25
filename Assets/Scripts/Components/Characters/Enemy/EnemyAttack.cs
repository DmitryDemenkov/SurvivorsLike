using Unity.Entities;

namespace SurvivorsLike
{
    public struct EnemyAttackData : IComponentData
    {
        public int HitPoints;
        public float CooldownTime;
    }

    public struct EnemyCooldownExpirationTimestamp : IComponentData, IEnableableComponent
    {
        public double Value;
    }
}
