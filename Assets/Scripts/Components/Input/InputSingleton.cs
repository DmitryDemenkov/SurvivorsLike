using Unity.Entities;

namespace SurvivorsLike
{
    public struct InputSingleton : IComponentData
    {
        public UnityObjectRef<InputManager> Input;
    }
}
