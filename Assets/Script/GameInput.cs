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
    public event EventHandler OnGamePauseAction;
    public event EventHandler OnGameUnpauseAction;
    private InputSystem_Actions playerInputActions;
    private bool isGamePause;
    private bool isRunning;
    private void Awake()
    {
        Instance = this;
        playerInputActions = new InputSystem_Actions();
        playerInputActions.Enable();
        playerInputActions.Player.Attack.performed += Attack_Performed;
        playerInputActions.Player.Jump.performed += Jump_Performed;
        playerInputActions.Player.Pause.performed += Pause_Performed;
    }
    private void Pause_Performed(InputAction.CallbackContext context)
    {
        TogglePauseGame();
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Attack.performed -= Attack_Performed;
        playerInputActions.Player.Jump.performed -= Jump_Performed;
        playerInputActions.Player.Pause.performed -= Pause_Performed;
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
    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }
    public float GetMovementSpeed()
    {
        float movementSpeed;

        if (playerInputActions.Player.Sprint.IsPressed())
        {
            movementSpeed = PlayerMovement.Instance.GetRunSpeed();
            isRunning = true;
        }
        else
        {
            movementSpeed = PlayerMovement.Instance.GetWalkSpeed();
            isRunning = false;
        }
        
        return  movementSpeed;
    }
    public void TogglePauseGame()
    {
        isGamePause = !isGamePause;

        if (isGamePause)
        {
            Time.timeScale = 0f;
            OnGamePauseAction?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpauseAction?.Invoke(this, EventArgs.Empty);
        }
    }
    public bool IsRunning()
    {
        return isRunning;
    }

    public bool IsGamePaused()
    {
        return isGamePause;
    }
}