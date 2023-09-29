using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TSTowerManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    public float fireForce = 50f;
    private readonly string _balloonTag = "Balloon";

    public float shootingInterval = 1f; // Delay between shots in seconds.
    
    private List<GameObject> balloonsInRange = new List<GameObject>();

    private void Start()
    {
        // Start the shooting coroutine
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            if (balloonsInRange.Count > 0)
            {
                Shoot();
            }

            // Wait for the shootingInterval seconds before shooting again
            yield return new WaitForSeconds(shootingInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_balloonTag))
        {
            balloonsInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(_balloonTag))
        {
            balloonsInRange.Remove(other.gameObject);
        }
    }

    private void Shoot()
    {
        if (balloonsInRange.Count > 0)
        {
            // Calculate angles for the four directions in radians
            float[] angles = { 45f, 135f, 225f, 315f };

            foreach (float angle in angles)
            {
                // Convert angle to radians
                float radians = angle * Mathf.Deg2Rad;

                // Calculate direction vector based on the angle
                Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

                // Create a bullet
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            
                // Apply a force to shoot the projectile.
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = direction * fireForce;
            }
        }
    }

}