using UnityEngine;
using Unity.Entities;

namespace SurvivorsLike
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public int AttackDamage;
        public float CooldownTime;

        private class Baker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                Entity enemyEntity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<EnemyTag>(enemyEntity);
                AddComponent(enemyEntity, new EnemyAttackData
                {
                    HitPoints = authoring.AttackDamage,
                    CooldownTime = authoring.CooldownTime
                });
                AddComponent<EnemyCooldownExpirationTimestamp>(enemyEntity);
                SetComponentEnabled<EnemyCooldownExpirationTimestamp>(enemyEntity, false);
            }
        }
    }
}