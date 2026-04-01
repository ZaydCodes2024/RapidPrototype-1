using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Weapon weapon;
    private float weaponDamage = 20f;
    public bool IsWeaponEquipped()
    {
        return weapon == null;
    }
    public float GetWeaponDamage()
    {
        return weaponDamage;
    }
    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }
}
