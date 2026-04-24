using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    [SerializeField] private float cleanupTime;
    private void Start()
    {
        Destroy(gameObject, cleanupTime);
    }
}
