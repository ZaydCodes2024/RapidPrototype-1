using Unity.VisualScripting;
using UnityEngine;

public class DamageIndicatorUI : MonoBehaviour
{
    [SerializeField] private Transform damageIndicatorPivot;
    [SerializeField] private Transform playerPosition; 
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeStartTime, fadeTime;
    private float maxFadeTime;
    private Vector3 damageLocation;
    private void Start()
    {
        maxFadeTime = fadeTime;
    }
    private void Update()
    {
        if (fadeStartTime > 0)
        {
            fadeStartTime -= Time.deltaTime;
        }
        else
        {
            fadeTime -= Time.deltaTime;
            canvasGroup.alpha = fadeTime / maxFadeTime;

            if (fadeTime <= 0)
            {
                Destroy(gameObject);
            }
        }

        damageLocation.y = playerPosition.position.y;

        Vector3 direction = (damageLocation - playerPosition.position).normalized;
        float angle = Vector3.SignedAngle(direction, playerPosition.forward, Vector3.up);
        damageIndicatorPivot.transform.localEulerAngles = new Vector3(0, 0, angle);
    }

    public void SetDamageLocation(Vector3 position)
    {
        damageLocation = position;
    }
}
