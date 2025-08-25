using Unity.Entities;

namespace SurvivorsLike
{
    public struct WeaponArsenal : IComponentData
    {
        public int CurrentWeapon;
    }

    public struct WeaponDataElement : IBufferElementData
    {
        public float BulletSpeed;
        public int BulletDamage;
        public float Cooldown;
        public int BulletCount;
        public float Angle;
    }
}