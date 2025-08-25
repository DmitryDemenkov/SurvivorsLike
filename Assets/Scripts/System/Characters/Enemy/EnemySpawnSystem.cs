using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SurvivorsLike
{
    public partial struct EnemySpawnSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            BeginInitializationEntityCommandBufferSystem.Singleton ecbSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            EntityCommandBuffer ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);

            Entity playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            float3 playerPosition = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position;

            foreach (var (spawnState, spawnData, prefabs) in SystemAPI.Query<RefRW<EnemySpawnState>, EnemySpawnData, DynamicBuffer<EnemySpawnPrefab>>())
            {
                spawnState.ValueRW.Timer -= deltaTime;
                if (spawnState.ValueRO.Timer <= 0)
                {
                    int randomIndex = spawnState.ValueRW.Random.NextInt(0, prefabs.Length);
                    Entity enemyPrefab = prefabs[randomIndex].Prefab;

                    Entity newEnemy = ecb.Instantiate(enemyPrefab);
                    float spawnAngle = spawnState.ValueRW.Random.NextFloat(0, 2 * math.PI);
                    float3 spawnPoint = new float3
                    {
                        x = math.sin(spawnAngle),
                        y = math.cos(spawnAngle),
                        z = 0f
                    };
                    spawnPoint *= spawnData.Distance;
                    spawnPoint += playerPosition;

                    ecb.SetComponent(newEnemy, LocalTransform.FromPosition(spawnPoint));

                    spawnState.ValueRW.Timer = spawnData.Interval;
                }
            }
        }
    }
}
