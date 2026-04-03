using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set;}
    public event EventHandler OnAttackAction;
    public event EventHandler OnJumpAction;
    private InputSystem_Actions playerInputActions;

    private void Awake()
    {
        Instance = this;
        playerInputActions = new InputSystem_Actions();
        playerInputActions.Enable();
        playerInputActions.Player.Attack.performed += Attack_Performed;
        playerInputActions.Player.Jump.performed += Jump_Performed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Attack.performed -= Attack_Performed;
        playerInputActions.Player.Jump.performed -= Jump_Performed;
        playerInputActions.Dispose();
    }
    private void Jump_Performed(InputAction.CallbackContext context)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    private void Attack_Performed(InputAction.CallbackContext context)
    {
        OnAttackAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
        return inputVector;
    }
    public float GetMovementSpeed(float runspeed, float walkSpeed)
    {
        float movementSpeed = playerInputActions.Player.Sprint.IsPressed() ? runspeed : walkSpeed;
        return  movementSpeed;
    }
}