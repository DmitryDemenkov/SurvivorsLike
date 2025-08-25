using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon Data", order = 51)]
public class WeaponData : ScriptableObject
{
    public Sprite sprite;

    public float bulletSpeed;
    public int bulletDamage;
    public float cooldown;

    [Min(1)]
    public int bulletCount;
    public float angle;
}
