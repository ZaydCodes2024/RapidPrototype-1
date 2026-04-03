using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform weaponTip;
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private float bulletSpeed = 500f;
    private Weapon weapon;
    private float weaponDamage = 20f;
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
        Vector3 targetPoint = InteractionController.Instance.hitPoint;
        Vector3 direction = (targetPoint - weaponTip.position).normalized;

        float travelTime = 0.1f;

        if (trailRenderer != null)
        {
            trailRenderer.Clear();
        }

        bulletTransform.gameObject.SetActive(true);

        bulletRb.AddForce(direction * bulletSpeed, ForceMode.Impulse);

        Destroy(bulletTransform.gameObject, travelTime);
        
    }
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
