using UnityEngine;

public class PlayerAnimationUnity : MonoBehaviour
{
    public static PlayerAnimationUnity Singleton;

    public SpriteRenderer Renderer;
    public Animator Animator;

    private void Awake()
    {
        Singleton = this;
    }

    public void SetRotation(int sign)
    {
        var angles = transform.rotation.eulerAngles;
        angles.y = sign >= 0 ? 0 : 180;

        transform.rotation = Quaternion.Euler(angles);
    }

    public void SetTransform(float x, float y)
    {
        transform.position = new Vector3(x, y, 0f);
    }

    public void SetSpeed(float speed)
    {
        if (Animator.isActiveAndEnabled)
        {
            Animator.SetBool("IsRuning", speed > 0);
        }
    }
}
