using System;
using UnityEngine;

public class HitReticleUI : MonoBehaviour
{
    private Animator animator;
    private const string FLASH = "Flash";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        InteractionController.Instance.OnEnemyDamage += InteractionController_OnEnemyDamage;
    }

    private void InteractionController_OnEnemyDamage(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        animator.SetTrigger(FLASH);
    }
}
