using UnityEngine;
public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float time = 3f;
    void Start()
    {
        Destroy(gameObject, time); // Destroy bullet after a few seconds
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Check if it hits an enemy
        {
            Destroy(other.gameObject); // Destroy enemy (modify as needed)
            Destroy(gameObject); // Destroy bullet on impact
        }
    }
}
