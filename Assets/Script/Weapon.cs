using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private WeaponController weaponController;
    private void Awake()
    {
        SetWeaponPositionOnPlayer();
    }
    private void SetWeaponPositionOnPlayer()
    {
        transform.SetParent(weaponPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        weaponController.SetWeapon(this);
    }
    
}
