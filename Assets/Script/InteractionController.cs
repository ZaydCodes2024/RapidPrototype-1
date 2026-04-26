using System;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private WeaponController weaponController;
    public static InteractionController Instance {get; private set;}
    public event EventHandler OnGunfired;
    public event EventHandler OnEnemyDamage;
    private IHealth health;
    private bool isAimingAtEnemy;
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
            OnEnemyDamage?.Invoke(this, EventArgs.Empty);
        }
    }
    
    public void HandleInteractions(RaycastHit hit)
    {

        if (hit.transform.TryGetComponent(out IHealth healthObj))
        {
            health = healthObj;
            isAimingAtEnemy = true;
        }
        else
        {
            ClearInteractions(Player.Instance.GetCameraTransform());
            isAimingAtEnemy = false;
        }
    }

    public void ClearInteractions(Transform cameraTransform)
    {
        health = null;
        isAimingAtEnemy = false;
    }

    public bool IsAimingAtEnemy()
    {
        return isAimingAtEnemy;
    }
}
