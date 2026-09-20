using UnityEngine;

public class BalloonPop : MonoBehaviour
{
    public GameObject popEffectPrefab; // Assign your pop effect prefab (particle system + audio source)
    public float popEffectDuration = 1f; // How long the pop effect lasts before being destroyed

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collider object has a Projectile.cs component
        if (collision.gameObject.GetComponent<Projectile>() != null)
        {
            PopBalloon();
        }
    }

    private void PopBalloon()
    {
        // Instantiate the pop effect prefab at the balloon's position
        if (popEffectPrefab != null)
        {
            GameObject popEffect = Instantiate(popEffectPrefab, transform.position, Quaternion.identity);

            // Automatically destroy the pop effect after its duration
            Destroy(popEffect, popEffectDuration);
        }

        // Destroy the balloon object
        Destroy(gameObject);
    }
}
