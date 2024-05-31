using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FlyMovement : MonoBehaviour
{
    public float speed = 3f; // Fly movement speed

    private Vector3 targetPosition; // Position the fly is moving towards

    public void SetTargetPosition(Vector3 target)
    {
        targetPosition = target;
    }

    private void Update()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the fly has reached the target position
        if (transform.position == targetPosition)
        {
            // Destroy the fly when it reaches the target
            Destroy(gameObject);
        }
    }
}

