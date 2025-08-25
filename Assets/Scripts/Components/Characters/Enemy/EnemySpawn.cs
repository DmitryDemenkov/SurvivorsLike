using Unity.Entities;

namespace SurvivorsLike
{
    public partial struct EnemySpawnPrefab : IBufferElementData
    {
        public Entity Prefab;
    }

    public partial struct EnemySpawnData : IComponentData
    {
        public float Interval;
        public float Distance;
    }

    public partial struct EnemySpawnState : IComponentData
    {
        public float Timer;
        public Unity.Mathematics.Random Random;
    }
}
