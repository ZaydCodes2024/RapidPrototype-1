using System;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private WeaponController weaponController;
    public static InteractionController Instance {get; private set;}
    public event EventHandler OnGunfired;
    public event EventHandler OnEnemyDamage;
    private Enemy enemyHealth;
    private bool isAimingAtEnemy;
    private void Awake() => Instance = this;
    private void Start() => GameInput.Instance.OnAttackAction += GameInput_OnAttackAction;

    private void GameInput_OnAttackAction(object sender, EventArgs e)
    {
        if (GameInput.Instance.IsGamePaused())  return;
        
        if (weaponController.IsWeaponEquipped())
        {
            Debug.Log("No weapons Equipped");
            return;    
        }
        
        OnGunfired?.Invoke(this, EventArgs.Empty);

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(weaponController.GetWeaponDamage());
            OnEnemyDamage?.Invoke(this, EventArgs.Empty);
        }
    }
    
    public void HandleInteractions(RaycastHit hit)
    {

        if (hit.transform.TryGetComponent(out Enemy enemy))
        {
            enemyHealth = enemy;
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
        enemyHealth = null;
        isAimingAtEnemy = false;
    }

    public bool IsAimingAtEnemy() => isAimingAtEnemy;
}
