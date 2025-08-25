using Unity.Entities;
using Unity.Rendering;

namespace SurvivorsLike
{
    public partial struct ShaderAnimation : IComponentData
    {
        public int FramePerSecond;
        public float Time;
        public float Speed;
    }

    [MaterialProperty("_SpriteIndex")]
    public partial struct SpriteIndex : IComponentData
    {
        public float Value;
    }
}
