using UnityEngine;
using Unity.Entities;

namespace SurvivorsLike
{
    public class WeaponRangeAuthoring : MonoBehaviour
    {
        public WeaponData weaponData;

        private class Baker : Baker<WeaponRangeAuthoring>
        {
            public override void Bake(WeaponRangeAuthoring authoring)
            {
                Entity weaponEntity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(weaponEntity, new WeaponRange
                {
                    BulletCount = authoring.weaponData.bulletCount,
                    Angle = authoring.weaponData.angle
                });
            }
        }
    }
}
