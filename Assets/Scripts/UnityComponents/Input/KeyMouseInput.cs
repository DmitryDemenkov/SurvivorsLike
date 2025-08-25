using UnityEngine;

public class KeyMouseInput : InputManager
{
    private PlayerInput _input;

    public void Awake()
    {
        _input = new PlayerInput();
        _input.Enable();

        Singleton = this;
    }

    public override Vector2 Move() 
    {
        return _input.Player.Move.ReadValue<Vector2>();
    }

    public override Vector3 Scope()
    {
        Vector3 scope = Camera.main.ScreenToWorldPoint(_input.Player.Scope.ReadValue<Vector2>());
        scope.z = 0f;
        return scope;
    }

    public override int Selector()
    {
        return (int)_input.Player.Select.ReadValue<float>();
    }

    public override bool Shoot()
    {
        return _input.Player.Shoot.ReadValue<float>() > 0;
    }
}
