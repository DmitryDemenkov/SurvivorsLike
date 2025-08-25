using Unity.Entities;
using Unity.Mathematics;

namespace SurvivorsLike
{
    public struct Movement : IComponentData
    {
        public float Speed;
        public float2 Direction;
    }
}