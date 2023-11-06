using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target; // Reference to the player character
    public float moveSpeed = 2.0f;
    public float smoothTime = 0.1f; // Adjust this value to control smoothing

    private Vector3 currentVelocity = Vector3.zero;

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Vector3 targetPosition = target.position;

            // Calculate the new position towards the target with smoothing
            float step = moveSpeed * Time.deltaTime;
            Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime, step);

            // Update the enemy's position
            transform.position = newPosition;
        }
    }
}