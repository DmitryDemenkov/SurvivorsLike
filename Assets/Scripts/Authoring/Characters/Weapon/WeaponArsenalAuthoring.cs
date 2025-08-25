using UnityEngine;
using Unity.Entities;

namespace SurvivorsLike
{
    public class WeaponArsenalAuthoring : MonoBehaviour
    {
        public WeaponData[] data;

        private class Baker : Baker<WeaponArsenalAuthoring>
        {
            public override void Bake(WeaponArsenalAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                DynamicBuffer<WeaponDataElement> dataBuffer = AddBuffer<WeaponDataElement>(entity);
                foreach (var dataElement in authoring.data)
                {
                    dataBuffer.Add(new WeaponDataElement 
                    { 
                        BulletDamage = dataElement.bulletDamage,
                        BulletSpeed = dataElement.bulletSpeed,
                        BulletCount = dataElement.bulletCount,
                        Angle = dataElement.angle,
                        Cooldown = dataElement.cooldown
                    });
                }
                AddComponent(entity, new WeaponArsenal { CurrentWeapon = 0 });
            }
        }
    }
}