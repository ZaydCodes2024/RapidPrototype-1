using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] Texture2D crosshairTexture;
    [SerializeField] float crosshairScale = 1f;
    private GUIStyle crosshairStyle = new GUIStyle();
    private void OnGUI()
    {
        crosshairStyle.normal.background = crosshairTexture;
        Vector2 pivotPoint = new Vector2(crosshairStyle.normal.background.width / 2, crosshairStyle.normal.background.height / 2);
        Vector2 position = new Vector2(Screen.width / 2 - pivotPoint.x * crosshairScale, Screen.height / 2 - pivotPoint.y * crosshairScale);

        GUI.DrawTexture(new Rect(position.x, position.y, crosshairStyle.normal.background.width * crosshairScale, crosshairStyle.normal.background.height * crosshairScale), crosshairStyle.normal.background);
    }
    private void Update()
    {
        PerformRayCast();
    }

    void PerformRayCast()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            InteractionController.Instance.HandleInteractions(hit);
        }
        else
        {
            InteractionController.Instance.ClearInteractions();
        }
    }
}
