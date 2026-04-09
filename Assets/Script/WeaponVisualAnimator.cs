using UnityEngine;

public class WeaponVisualAnimator : MonoBehaviour
{
    private Animator animator;
    private const string IS_WALKING = "IsWalking";
    private const string IS_SPRINTING = "IsSprinting";
    [SerializeField] private PlayerMovement player;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player == null) return;

        animator.SetBool(IS_WALKING, player.IsWalking());
        animator.SetBool(IS_SPRINTING, GameInput.Instance.IsSprinting());
    }
}
