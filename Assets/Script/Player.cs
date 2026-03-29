using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    private PlayerMovement playerMovement;
    private PlayerLook playerLook;
    public static Player Instance {get; private set;}
    private void Awake()
    {
        Instance = this;
        playerMovement = GetComponent<PlayerMovement>();
        playerLook = GetComponent<PlayerLook>();
    }

    private void Update()
    {
        playerLook.HandleMouseLook();
        playerMovement.HandleMovement();
    }
    public Transform GetCameraTransform()
    {
        return cameraTransform;
    }
}
