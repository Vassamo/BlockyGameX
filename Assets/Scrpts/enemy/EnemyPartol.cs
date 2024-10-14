using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA; // First patrol point
    public Transform pointB; // Second patrol point
    public float speed = 2f; // Speed of the enemy

    private Vector3 target; // Current target position
    private bool movingToA = true; // Direction flag

    void Start()
    {
        target = pointA.position; // Start by moving to point A
    }

    void Update()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Check if the enemy has reached the target
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // Switch target
            if (movingToA)
            {
                target = pointB.position;
            }
            else
            {
                target = pointA.position;
            }
            movingToA = !movingToA; // Toggle direction
        }
    }
}
