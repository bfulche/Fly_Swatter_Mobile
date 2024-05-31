using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBehavior : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float despawnDistance = 1f;

    public float flyEdgeOffset = 2f;

    private Vector3 targetPosition;
    private Renderer flyRenderer;
    private float flySize;

    private void Start()
    {
        // Get the fly's renderer and calculate its size
        flyRenderer = GetComponent<Renderer>();
        flySize = Mathf.Max(flyRenderer.bounds.size.x, flyRenderer.bounds.size.y);

        // Calculate a random start and end position for the fly's movement
        Vector3 startPosition = GetRandomEdgePosition(-flySize - flyEdgeOffset);
        targetPosition = GetRandomEdgePosition(flySize, startPosition);

        // Position the fly at the start position
        transform.position = startPosition;
    }

    private void Update()
    {
        // Move the fly towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the fly has reached the target position (plus a small despawn distance)
        if (Vector3.Distance(transform.position, targetPosition) < despawnDistance + (flySize + flyEdgeOffset))
        {
            Despawn();
        }
    }

    private Vector3 GetRandomEdgePosition(float offset, Vector3 excludePosition = default)
    {
        // Get the camera's viewport boundaries
        Vector3 viewportMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 viewportMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        // Calculate the edge positions with offset
        Vector3 leftEdge = new Vector3(viewportMin.x - offset, Random.Range(viewportMin.y, viewportMax.y), 0);
        Vector3 rightEdge = new Vector3(viewportMax.x + offset, Random.Range(viewportMin.y, viewportMax.y), 0);
        Vector3 topEdge = new Vector3(Random.Range(viewportMin.x, viewportMax.x), viewportMax.y + offset, 0);
        Vector3 bottomEdge = new Vector3(Random.Range(viewportMin.x, viewportMax.x), viewportMin.y - offset, 0);

        // Select a random edge position, excluding the provided position if any
        Vector3[] edgePositions = { leftEdge, rightEdge, topEdge, bottomEdge };
        Vector3 randomPosition = edgePositions[Random.Range(0, edgePositions.Length)];

        // If the random position is too close to the excluded position, try again
        if (excludePosition != default && Vector3.Distance(randomPosition, excludePosition) < despawnDistance)
        {
            return GetRandomEdgePosition(offset, excludePosition);
        }

        return randomPosition;
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }
}