using Unity.Entities;

namespace SurvivorsLike
{
    public struct Weapon : IComponentData
    {
        public Entity BulletPrefab;
        public float BulletSpeed;
        public int BulletDamage;
        public float Cooldown;
    }

    public struct WeaponCooldownTimestamp : IComponentData
    {
        public double Value;
    }

    public struct WeaponRange : IComponentData
    {
        public int BulletCount;
        public float Angle;
    }
}
