using UnityEngine;

public class PlayerBoundaryManager : MonoBehaviour
{
    private float minY = -10f; // Altura limite antes de cair
    private Vector3 spawnPosition;

    public void InitializeBoundary()
    {
        spawnPosition = transform.position;
        Debug.Log("SpawnPosition inicial registrado: " + spawnPosition);
    }

    void Update()
    {
        if (transform.position.y < minY)
        {
            Debug.LogWarning("Player caiu. Teleportando de volta ao spawn.");
            transform.position = spawnPosition + Vector3.up * 2f;
        }
    }
}