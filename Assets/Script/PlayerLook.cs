using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    public void HandleMouseLook()
    {
        
        float mouseX = Mouse.current.delta.ReadValue().x * mouseSensitivity;
        float mouseY = Mouse.current.delta.ReadValue().y  * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);

        Vector3 currentRotation = Player.Instance.GetCameraTransform().rotation.eulerAngles;
        float desiredRotationX = currentRotation.x - mouseY;

        if (desiredRotationX > 180) desiredRotationX -= 360;

        desiredRotationX = Mathf.Clamp(desiredRotationX, -90f, 90f);
        Player.Instance.GetCameraTransform().rotation = Quaternion.Euler(desiredRotationX, currentRotation.y, currentRotation.z);
    }
}
