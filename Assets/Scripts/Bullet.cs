using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyDelay = 4f; // Time in seconds before the bullet is destroyed
    [SerializeField] private bool destroyAfterSomeTime;
    private bool isDestroyed = false; // Flag to track if the bullet has been destroyed

    private void Start()
    {
        // Schedule the bullet to be destroyed after the specified delay if it's still active and not already destroyed
        if (!isDestroyed)
        {
            if(destroyAfterSomeTime)
                Destroy(gameObject, destroyDelay);
        }
    }

    // Call this method to manually destroy the bullet
    public void DestroyBullet()
    {
        // Check if the bullet is still active and not destroyed before attempting to destroy it
        if (!isDestroyed)
        {
            isDestroyed = true;
        }
    }
}