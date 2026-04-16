using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance {get; private set;}
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float gravityScale = 1.5f;
    private bool isGrounded;
    private bool isMoving;
    private Rigidbody playerRb;
    // public event EventHandler OnCrouchDown;
    // public event EventHandler OnCrouchUp;
    // private bool isCrouching;
    // private float currentHeight;
    // private float lerpSpeed = 10f;
    // private float crouchHeight = 0.25f;
    // private float crouchSpeed = 2.5f;
    

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        Instance = this;
    }
    private void Start()
    {
        GameInput.Instance.OnJumpAction += GameInput_OnJumpaction;
    }

    private void GameInput_OnJumpaction(object sender, EventArgs e)
    {
        if (isGrounded)
        {
            playerRb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            SoundManager.Instance.PlayJumpSound(transform.position, 0.5f);
        }
    }

    public void HandleMovement()
    {

        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 forward = Player.Instance.GetCameraTransform().forward;
        Vector3 right = Player.Instance.GetCameraTransform().right;

        // Flatten them so looking up/down doesn't move the player vertically
        forward.y = 0f;
        right.y = 0f;

        Vector3 moveDir = forward * inputVector.y + right * inputVector.x;

        float moveSpeed;

        // if (isCrouching)
        // {
        //     moveSpeed = crouchSpeed;
        // }
        // else
        // {
            
        // }

        moveSpeed = GameInput.Instance.GetMovementSpeed();

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 1f;
        float groundCheckDistance = 1f;

        // currentHeight = isCrouching ? crouchHeight : playerHeight;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        Vector3 origin = transform.position + Vector3.down * 0.1f;
        isGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance, groundLayerMask);

        Debug.DrawRay(origin, Vector3.down * groundCheckDistance, Color.red);

        if (!canMove) // Cannot move towards moveDir
        {
            Vector3 moveDirX = new Vector3(moveDir.x,0,0).normalized; // Attempt only X movement
            canMove = ( moveDir.x < -0.5f || moveDir.x > 0.5f ) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX; // Can move only on the X
            }
            else // Cannot move only on the X
            {
                Vector3 moveDirZ = new Vector3(0,0,moveDir.z).normalized; // Attempt only Z movement
                canMove =  ( moveDir.z < -0.5f || moveDir.z > 0.5f ) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)    
                {
                    moveDir = moveDirZ; // Can move only on the Z
                }
                else // Cannot move in any direction
                {
                    
                }
            }
        }
        if (canMove)
        {
            playerRb.MovePosition(playerRb.position + moveDir * moveDistance);
            playerRb.AddForce(Physics.gravity * (gravityScale - 1) * Time.deltaTime, ForceMode.Acceleration);
        }

        isMoving = moveDir != Vector3.zero;

        // float targetHeight = isCrouching ? crouchHeight : playerHeight;
        // Player.Instance.GetCameraTransform().localPosition = Vector3.Slerp(Player.Instance.GetCameraTransform().localPosition, new Vector3(0, targetHeight, 0), Time.deltaTime * lerpSpeed);

    }

    public float GetWalkSpeed()
    {
        return walkSpeed;
    }

    public float GetRunSpeed()
    {
        return runSpeed;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
    public bool IsMoving()
    {
        return isMoving;
    }
}
