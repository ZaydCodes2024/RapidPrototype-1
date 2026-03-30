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
        
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerLook = GetComponent<PlayerLook>();
        LockCursorState();
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
    public void LockCursorState()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void UnlockCursorState()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
