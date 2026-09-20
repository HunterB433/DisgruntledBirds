using UnityEngine;

public class MusicSummoner : MonoBehaviour
{
    public GameObject prefab; // Drag your prefab here
    public Transform spawnPoint; // Optional: where to spawn the prefab

    void Start()
    {
            SpawnPrefab();
    }

    void SpawnPrefab()
    {
        if (prefab != null)
        {
            Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Prefab is not assigned.");
        }
    }
}
