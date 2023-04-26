using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; // The bullet prefab that will be spawned
    public Transform firePoint; // The point from which the bullet will be fired
    public float fireInterval = 2f; // The time interval between each bullet shot

    private float timeSinceLastShot; // The time elapsed since the last bullet shot

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= fireInterval)
        {
            Shoot();
            timeSinceLastShot = 0f;
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
