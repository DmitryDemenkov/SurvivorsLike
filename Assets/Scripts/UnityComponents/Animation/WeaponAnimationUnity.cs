using UnityEngine;

public class WeaponAnimationUnity : MonoBehaviour
{
    public static WeaponAnimationUnity Sinlgeton;

    public Sprite[] sprites;

    private void Awake()
    {
        Sinlgeton = this;
    }

    public void Scope(float mouseX, float mouseY)
    {
        Vector3 mousePosition = new Vector3(mouseX, mouseY, 0f);
        float angle = Vector3.Angle(Vector3.down, (mousePosition - transform.position).normalized);

        var angles = transform.rotation.eulerAngles;
        angles.z = angle - 90f;

        transform.rotation = Quaternion.Euler(angles);
    }

    public void SetSprite(int index)
    {
        GetComponent<SpriteRenderer>().sprite = sprites[index];
    }
}
