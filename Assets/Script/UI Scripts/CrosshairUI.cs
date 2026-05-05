using System;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairUI : MonoBehaviour
{
    private Image crosshair;
    private Color defaultColor;
    private void Awake()
    {
        crosshair = GetComponent<Image>();
        defaultColor = crosshair.color;
    }
    private void Update() => UpdateVisual();

    private void UpdateVisual()
    {
        if (InteractionController.Instance.IsAimingAtEnemy())
        {
            crosshair.color = Color.red;
        }
        else
        {
            crosshair.color = defaultColor;
        }
    }
}
