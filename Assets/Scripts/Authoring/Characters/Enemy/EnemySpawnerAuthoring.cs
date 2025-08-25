using UnityEngine;
using Unity.Entities;

namespace SurvivorsLike
{
    public class EnemySpawnerAuthoring : MonoBehaviour
    {
        public EnemyAuthoring[] prefabs;
        public float SpawnInterval;
        public float SpawnDistance;

        private class Baker : Baker<EnemySpawnerAuthoring>
        {
            public override void Bake(EnemySpawnerAuthoring authoring)
            {
                Entity spawnEntity = GetEntity(TransformUsageFlags.None);

                DynamicBuffer<EnemySpawnPrefab> prefabBuffer = AddBuffer<EnemySpawnPrefab>(spawnEntity);
                foreach (var prefab in authoring.prefabs)
                {
                    prefabBuffer.Add(new EnemySpawnPrefab
                    {
                        Prefab = GetEntity(prefab, TransformUsageFlags.Dynamic)
                    });
                }

                AddComponent(spawnEntity, new EnemySpawnData
                {
                    Interval = authoring.SpawnInterval,
                    Distance = authoring.SpawnDistance
                });

                AddComponent(spawnEntity, new EnemySpawnState
                {
                    Timer = 0f,
                    Random = Unity.Mathematics.Random.CreateFromIndex((uint)System.DateTime.UtcNow.Ticks)
                });
            }
        }
    }
}
