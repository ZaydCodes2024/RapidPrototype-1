using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAnimations : MonoBehaviour
{
    [SerializeField] List<AnimationClip> animationClips;
    [SerializeField] private PlayerMovement playerMovement;
    private Animator animator;
    private AnimationSystem animationSystem;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        animationSystem = new AnimationSystem(animator, animationClips);
    }
    private void Update()
    {
        animationSystem.UpdateLocomotion(); 
    }

    private void OnDestroy()
    {
        if (animationSystem != null)
        {
            animationSystem.Destroy();
        }
    }
}
