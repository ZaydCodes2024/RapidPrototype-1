using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    [SerializeField] private float time;
    void Start()
    {
        Destroy(gameObject, time);
    }
}
