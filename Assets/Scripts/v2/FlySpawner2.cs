using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpawner2 : MonoBehaviour
{
    public GameObject flyPrefab; // Reference to the fly prefab
    public float spawnInterval = 2f; // Time between spawns (in seconds)

    private float nextSpawnTime; // Time when the next fly will be spawned

    private void Start()
    {
        // Set the initial spawn time
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        // Check if it's time to spawn a new fly
        if (Time.time >= nextSpawnTime)
        {
            SpawnFly();
            // Update the next spawn time
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnFly()
    {
        // Calculate a random position on the screen
        Vector3 spawnPosition = GetRandomEdgePosition();

        // Spawn a new fly at the random position
        Instantiate(flyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomEdgePosition()
    {
        // Get the camera's viewport boundaries
        Vector3 viewportMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 viewportMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        // Calculate the edge positions
        Vector3 leftEdge = new Vector3(viewportMin.x, Random.Range(viewportMin.y, viewportMax.y), 0);
        Vector3 rightEdge = new Vector3(viewportMax.x, Random.Range(viewportMin.y, viewportMax.y), 0);
        Vector3 topEdge = new Vector3(Random.Range(viewportMin.x, viewportMax.x), viewportMax.y, 0);
        Vector3 bottomEdge = new Vector3(Random.Range(viewportMin.x, viewportMax.x), viewportMin.y, 0);

        // Select a random edge position
        Vector3[] edgePositions = { leftEdge, rightEdge, topEdge, bottomEdge };
        return edgePositions[Random.Range(0, edgePositions.Length)];
    }
}
