using Unity.Entities;
using UnityEngine;

namespace SurvivorsLike
{
    public class CharacterAuthoring : MonoBehaviour
    {
        public float speed;
        public int health;

        private class Baker : Baker<CharacterAuthoring>
        {
            public override void Bake(CharacterAuthoring authoring)
            {
                Entity characterEntity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<CharacterInitializationFlag>(characterEntity);
                AddComponent(characterEntity, new Movement 
                { 
                    Speed = authoring.speed
                });
                AddComponent(characterEntity, new Health
                {
                    MaxHealth = authoring.health,
                    Value = authoring.health
                });
                AddBuffer<DamageThisFrame>(characterEntity);
                AddComponent<DestroyEntityFlag>(characterEntity);
                SetComponentEnabled<DestroyEntityFlag>(characterEntity, false);
            }
        }
    }
}
