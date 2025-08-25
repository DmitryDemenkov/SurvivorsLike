using UnityEngine;
using Unity.Entities;

namespace SurvivorsLike
{
    public class WeaponAuthoring : MonoBehaviour
    {
        public BulletAuthoring bulletPrefab;
        public WeaponData weaponData;

        private class Baker : Baker<WeaponAuthoring>
        {
            public override void Bake(WeaponAuthoring authoring)
            {
                Entity weaponEntity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(weaponEntity, new Weapon
                {
                    BulletPrefab = GetEntity(authoring.bulletPrefab, TransformUsageFlags.Dynamic),
                    BulletDamage = authoring.weaponData.bulletDamage,
                    BulletSpeed = authoring.weaponData.bulletSpeed,
                    Cooldown = authoring.weaponData.cooldown
                });
                AddComponent<WeaponCooldownTimestamp>(weaponEntity);
            }
        }
    }
}
