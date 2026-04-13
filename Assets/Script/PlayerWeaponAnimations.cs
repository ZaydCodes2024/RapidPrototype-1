using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAnimations : MonoBehaviour
{
    [SerializeField] List<AnimationClip> locomotionAnimationClips;
    [SerializeField] private AnimationClip jumpClip;
    [SerializeField] private AnimationClip landClip;
    [SerializeField] private PlayerMovement playerMovement;
    private Animator animator;
    private AnimationSystem animationSystem;
    private bool wasGrounded;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        animationSystem = new AnimationSystem(animator, locomotionAnimationClips);
    }
    private void Update()
    {
        animationSystem.UpdateLocomotion(); 
        bool isGrounded = playerMovement.IsGrounded();

        // Jump (left ground)
        if (wasGrounded && !isGrounded)
        {
            animationSystem.PlayOneShot(jumpClip);
        }

        // Land (hit ground)
        if (!wasGrounded && isGrounded)
        {
            animationSystem.PlayOneShot(landClip);
        }

        wasGrounded = isGrounded;
    }

    private void OnDestroy()
    {
        if (animationSystem != null)
        {
            animationSystem.Destroy();
        }
    }
}
