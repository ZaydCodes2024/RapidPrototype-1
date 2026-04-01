using System;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public static InteractionController Instance {get; private set;}
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
        if (IsHealthEmpty())     return;

        health.TakeDamage(10f);
    }
    
    public bool IsHealthEmpty()
    {
        return health == null;
    }
    public void HandleInteractions(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out IHealth healthObj))
        {
            health = healthObj;
        }
    }

    public void ClearInteractions()
    {
        health = null;
    }
}
