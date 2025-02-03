using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{ 
    public GameObject bulletObject;
    public Transform firePoint; 
    public float bulletSpeed = 20f;
    void Update()
    {
        RotateTowardsMouse();
        if (Input.GetButtonDown("Fire1")) // Left Click
        {
            Shoot();
        }
    }

    void RotateTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Convert mouse position to a ray
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // Flat plane at Y = 0
        float hitDistance;

        if (groundPlane.Raycast(ray, out hitDistance)) // If the ray hits the plane
        {
            Vector3 targetPoint = ray.GetPoint(hitDistance); // Get world position
            Vector3 direction = (targetPoint - transform.position).normalized; 
            direction.y = 0; // Keep player upright

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation;
        }
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletObject, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;
        Destroy(bullet, 3f); // Destroy bullet after 3 seconds
    }
}
