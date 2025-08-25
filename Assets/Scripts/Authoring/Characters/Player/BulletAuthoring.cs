using Unity.Entities;
using UnityEngine;

namespace SurvivorsLike
{
    public class BulletAuthoring : MonoBehaviour
    {
        public float lifeTime;

        private class Baker : Baker<BulletAuthoring>
        {
            public override void Bake(BulletAuthoring authoring)
            {
                Entity bulletEntity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<Movement>(bulletEntity);
                AddComponent<PlayerAttack>(bulletEntity);
                AddComponent(bulletEntity, new Temporary { LifeTime = authoring.lifeTime });
                AddComponent<DestroyEntityFlag>(bulletEntity);
                SetComponentEnabled<DestroyEntityFlag>(bulletEntity, false);
            }
        }
    }
}
