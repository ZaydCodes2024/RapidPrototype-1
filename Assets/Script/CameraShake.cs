using UnityEngine;

public class CameraShake : MonoBehaviour
{
   [SerializeField] private float shakeDuration = 0.2f;
   [SerializeField] private float shakeIntensity= 0.1f;
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
            Vector3 randomOffset = Random.insideUnitCircle * shakeIntensity;
            transform.localPosition = initialPosition + randomOffset;

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
