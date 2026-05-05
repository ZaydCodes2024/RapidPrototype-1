using System;
using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform weaponTip;
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private float bulletSpeed = 500f;
    [SerializeField] private CameraShake cameraShake;
    private Weapon weapon;
    private float weaponDamage = 20f;
    
    private void Start()
    {
        InteractionController.Instance.OnGunfired += InteractionController_OnGunfired;
        GameInput.Instance.OnReloadAction += GameInput_OnReloadAction;
    }

    private void GameInput_OnReloadAction(object sender, EventArgs e)
    {
        Debug.Log("Reloading");
    }

    private void InteractionController_OnGunfired(object sender, EventArgs e)
    {
        if (GameInput.Instance.IsGamePaused())  return;
        
        FireBullet();
        cameraShake.ShakeCamera();
        SoundManager.Instance.PlayWeaponShootSound(transform.position, 0.5f);
    }
    private void FireBullet()
    {
        Transform bulletTransform = Instantiate(bulletPrefab, weaponTip.position, weaponTip.rotation);
        Transform cameraTransform = Player.Instance.GetCameraTransform();
        Rigidbody bulletRb = bulletTransform.GetComponent<Rigidbody>();
        TrailRenderer trailRenderer = bulletTransform.GetComponent<TrailRenderer>();
        Vector3 targetPoint = cameraTransform.position + cameraTransform.forward * 1000f;
        Vector3 direction = (targetPoint - weaponTip.position).normalized;

        float travelTime = 0.04f;

        if (trailRenderer != null)
        {
            trailRenderer.Clear();
        }

        bulletTransform.gameObject.SetActive(true);

        bulletRb.AddForce(direction * bulletSpeed, ForceMode.Impulse);

        Destroy(bulletTransform.gameObject, travelTime);
        
    }
    public bool IsWeaponEquipped() => weapon == null;
    public float GetWeaponDamage() => weaponDamage;
    public void SetWeapon(Weapon weapon) => this.weapon = weapon;
}
