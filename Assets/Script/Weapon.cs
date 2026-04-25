using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform pivotPosition;
    [SerializeField] private WeaponController weaponController;

    private void Awake()
    {
        SetWeaponPositionOnPlayer();
    }
    private void SetWeaponPositionOnPlayer()
    {
        transform.SetParent(pivotPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        weaponController.SetWeapon(this);
    }
    
}
