using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DTTowerManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    public float fireForce = 50f;
    private readonly string _balloonTag = "Balloon";

    public float shootingInterval = 1f; // Delay between shots in seconds.

    private GameObject currentTarget; // Track the current target inside the collider.
    private List<GameObject> balloonsInRange = new List<GameObject>();
    private bool isShooting = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_balloonTag))
        {
            balloonsInRange.Add(other.gameObject);

            if (!isShooting)
            {
                // Start shooting if not already shooting.
                isShooting = true;
                ShootBalloon();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(_balloonTag))
        {
            balloonsInRange.Remove(other.gameObject);

            if (balloonsInRange.Count == 0)
            {
                // Stop shooting if no balloons left in range.
                isShooting = false;
            }
        }
    }

    private void ShootBalloon()
    {
        
        if (balloonsInRange.Count > 0)
        {
            GameObject targetBalloon = balloonsInRange[0]; // Target the first balloon in the list.
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            // Set up your bullet's behavior here and make it shoot towards the targetBalloon.

            // Calculate the direction to the target.
            Vector2 direction = (targetBalloon.transform.position - firePoint.position).normalized;

            // Apply a force to shoot the projectile.
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * fireForce;

            // Remove the targeted balloon from the list.
            balloonsInRange.RemoveAt(0);

            // Schedule the next shot after the shooting interval.
            Invoke("ShootBalloon", shootingInterval);
        }
        else
        {
            // No balloons in range, stop shooting.
            isShooting = false;
        }
    }
}