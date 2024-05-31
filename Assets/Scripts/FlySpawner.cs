using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpawner : MonoBehaviour
{
    public GameObject flyPrefab; // Prefab of the fly object
    public float spawnDelay = 1f; // Delay between fly spawns

    private Camera mainCamera; // Reference to the main camera

    private void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera != null)
        {
            InvokeRepeating("SpawnFly", 0f, spawnDelay);
        }
        else
        {
            Debug.LogError("Main camera not found!");
        }
    }

    private void SpawnFly()
    {
        if (mainCamera == null)
        {
            Debug.LogWarning("Main camera reference is null. Fly spawning is canceled.");
            return;
        }

        // Generate random spawn position on the edges of the screen
        float randomX;
        float randomY;
        Vector3 spawnPosition;

        // Determine which edge to spawn the fly on
        int randomEdge = Random.Range(0, 4);

        switch (randomEdge)
        {
            case 0: // Top edge
                randomX = Random.Range(0f, 1f);
                randomY = 1f;
                spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(randomX, randomY, mainCamera.nearClipPlane));
                break;
            case 1: // Bottom edge
                randomX = Random.Range(0f, 1f);
                randomY = 0f;
                spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(randomX, randomY, mainCamera.nearClipPlane));
                break;
            case 2: // Left edge
                randomX = 0f;
                randomY = Random.Range(0f, 1f);
                spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(randomX, randomY, mainCamera.nearClipPlane));
                break;
            case 3: // Right edge
                randomX = 1f;
                randomY = Random.Range(0f, 1f);
                spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(randomX, randomY, mainCamera.nearClipPlane));
                break;
            default:
                spawnPosition = Vector3.zero;
                break;
        }

        spawnPosition.z = 0f;

        // Instantiate the fly at the spawn position
        GameObject fly = Instantiate(flyPrefab, spawnPosition, Quaternion.identity);
        fly.GetComponent<FlyMovement>().SetTargetPosition(GetRandomEdgePosition());
    }

    private Vector3 GetRandomEdgePosition()
    {
        // Randomly select one of the four edges
        int randomEdge = Random.Range(0, 4);

        // Calculate the random position on the selected edge
        float randomX;
        float randomY;
        switch (randomEdge)
        {
            case 0: // Top edge
                randomX = Random.Range(0f, 1f);
                randomY = 1f;
                break;
            case 1: // Bottom edge
                randomX = Random.Range(0f, 1f);
                randomY = 0f;
                break;
            case 2: // Left edge
                randomX = 0f;
                randomY = Random.Range(0f, 1f);
                break;
            case 3: // Right edge
                randomX = 1f;
                randomY = Random.Range(0f, 1f);
                break;
            default:
                randomX = 0f;
                randomY = 0f;
                break;
        }

        // Convert the random position to world coordinates
        Vector3 edgePosition = mainCamera.ViewportToWorldPoint(new Vector3(randomX, randomY, mainCamera.nearClipPlane));
        edgePosition.z = 0f;
        return edgePosition;
    }
}
