using System;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private WeaponController weaponController;
    public static InteractionController Instance {get; private set;}
    public bool IsAimingAtHealth { get; private set; }
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
        if (weaponController.IsWeaponEquipped())
        {
            Debug.Log("No weapons Equipped");
            return;    
        }
        OnGunfired?.Invoke(this, EventArgs.Empty);
        health?.TakeDamage(weaponController.GetWeaponDamage());
    }
    
    public void HandleInteractions(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out IHealth healthObj))
        {
            health = healthObj;
            IsAimingAtHealth = true;
        }
        else
        {
            IsAimingAtHealth = false;
        }
        
    }

    public void ClearInteractions()
    {
        health = null;
        IsAimingAtHealth = false;
    }
}
