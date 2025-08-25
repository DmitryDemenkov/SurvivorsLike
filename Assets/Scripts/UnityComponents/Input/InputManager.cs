using UnityEngine;

public abstract class InputManager : MonoBehaviour
{
    public static InputManager Singleton { get; protected set; }

    public abstract Vector2 Move();

    public abstract Vector3 Scope();

    public abstract bool Shoot();

    public abstract int Selector();
}
