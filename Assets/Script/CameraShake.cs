using UnityEngine;

public class CameraShake : MonoBehaviour
{
   [SerializeField] private float shakeDuration = 0.2f;
   [SerializeField] private float shakeMagnitude= 0.1f;
   [SerializeField] private float shakeIntensity = 0.3f;
   private float currentDuration;
   private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        if (currentDuration > 0)
        {
            Vector3 randomOffset = Random.insideUnitCircle * shakeMagnitude;
            transform.localPosition = initialPosition + randomOffset * shakeIntensity;

            currentDuration -= Time.deltaTime;
        }
        else
        {
            transform.localPosition = initialPosition;
        }
    }

    public void ShakeCamera()
    {
        currentDuration = shakeDuration;
    }
}
