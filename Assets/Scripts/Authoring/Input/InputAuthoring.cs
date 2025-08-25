using UnityEngine;
using Unity.Entities;

namespace SurvivorsLike
{
    public class InputAuthoring : MonoBehaviour
    {
        public InputManager inputManager;

        private class Baker : Baker<InputAuthoring>
        {
            public override void Bake(InputAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);
                AddComponent<InputInitializationFlag>(entity);
                SetComponentEnabled<InputInitializationFlag>(entity, true);
            }
        }
    }
}
