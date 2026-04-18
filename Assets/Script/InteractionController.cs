using System;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private WeaponController weaponController;
    public static InteractionController Instance {get; private set;}
    public bool IsAimingAtHealth { get; private set; }
    public Vector3 hitPoint { get; private set; }
    public event EventHandler OnGunfired;
    private IHealth health;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GameInput.Instance.OnAttackAction += GameInput_OnAttackAction;
    }

    private void GameInput_OnAttackAction(object sender, EventArgs e)
    {
        if (GameInput.Instance.IsGamePaused())  return;
        
        if (weaponController.IsWeaponEquipped())
        {
            Debug.Log("No weapons Equipped");
            return;    
        }
        
        OnGunfired?.Invoke(this, EventArgs.Empty);

        if (health != null)
        {
            health.TakeDamage(weaponController.GetWeaponDamage());
        }
    }
    
    public void HandleInteractions(RaycastHit hit)
    {
        hitPoint = hit.point;

        if (hit.transform.TryGetComponent(out IHealth healthObj))
        {
            health = healthObj;
            IsAimingAtHealth = true;
        }
        else
        {
            IsAimingAtHealth = false;
            ClearInteractions(Player.Instance.GetCameraTransform());
        }
    }

    public void ClearInteractions(Transform cameraTransform)
    {
        health = null;
        IsAimingAtHealth = false;
        hitPoint = cameraTransform.position + cameraTransform.forward * 1000f;
    }
}
