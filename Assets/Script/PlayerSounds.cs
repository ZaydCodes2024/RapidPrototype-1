using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private float footstepTimer;
    private float footstepTimerMax;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;

        footstepTimerMax = GameInput.Instance.IsRunning() ? 0.5f : 0.7f;

        if (footstepTimer < 0)
        {
            footstepTimer = footstepTimerMax;

            if (playerMovement.IsMoving() && playerMovement.IsGrounded())
            {
                SoundManager.Instance.PlayFootstepSound(transform.position, 0.5f);
            }    
        }
    }
}