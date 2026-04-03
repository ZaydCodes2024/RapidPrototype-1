using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private Transform weaponTip;
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private float bulletSpeed = 500f;
    private void Awake()
    {
        transform.SetParent(weaponPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        weaponPosition.GetComponent<WeaponController>().SetWeapon(this);
    }
    private void Start()
    {
        InteractionController.Instance.OnGunfired += InteractionController_OnGunfired;
    }

    private void InteractionController_OnGunfired(object sender, EventArgs e)
    {
        FireBullet();
    }

    private void FireBullet()
    {
        Transform bulletTransform = Instantiate(bulletPrefab, weaponTip.position, weaponTip.rotation);
        Rigidbody bulletRb = bulletTransform.GetComponent<Rigidbody>();
        TrailRenderer trailRenderer = bulletTransform.GetComponent<TrailRenderer>();
        float travelTime = 0.1f;

        if (trailRenderer != null)
        {
            trailRenderer.Clear();
        }

        bulletTransform.gameObject.SetActive(true);

        bulletRb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);

        Destroy(bulletTransform.gameObject, travelTime);
        
    }
    
}
