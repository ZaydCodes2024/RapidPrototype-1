using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform weaponPosition;
    private void Awake()
    {
        transform.SetParent(weaponPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        weaponPosition.GetComponent<WeaponController>().SetWeapon(this);
    }
}
