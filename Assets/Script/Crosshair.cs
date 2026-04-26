using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    private RaycastHit hit;
    private void Update()
    {
        PerformRayCast();
    }

    private void PerformRayCast()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out hit))
        {
            InteractionController.Instance.HandleInteractions(hit);
        }
        else
        {
            InteractionController.Instance.ClearInteractions(playerCamera.transform);
        }
    }
}
