using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField]
    private float linearSpeed = 5;

    private Vector3[] WayPoints = new Vector3[]
    {
        new(0.0f, 0.0f),
        new(8.0f, 0.0f),
        new(8.0f, 6.5f),
        new(0.5f, 6.5f),
        new(0.5f, 3.0f),
        new(4.5f, 3.0f),
    };

    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        index++;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if there are waypoints and index is valid
        if (WayPoints.Length == 0 || index >= WayPoints.Length)
            return;

        // Get current position and target waypoint, adjusted for parent position
        Vector3 parentPosition = transform.parent != null ? transform.parent.position : Vector3.zero;
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = WayPoints[index] + parentPosition; // Adjust waypoint by parent position

        // Calculate direction to target
        Vector3 direction = (targetPosition - currentPosition).normalized;

        // Move towards target waypoint using Translate
        float step = linearSpeed * Time.deltaTime;
        transform.Translate(direction * step, Space.World);

        // Check if reached the current waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            // Move to next waypoint
            index++;
            // Optional: Loop back to first waypoint when reaching the end
            if (index >= WayPoints.Length)
            {
                Destroy(gameObject);
            }
            else
            {
                Vector2 position = transform.position;
                var lookDirection = WayPoints[index] + parentPosition - transform.position;
                if (lookDirection.magnitude < 0.01f) return;
                var angle = Vector2.SignedAngle(Vector3.right, lookDirection);
                transform.rotation = Quaternion.Euler(0, 0, angle);

            }
        }
    }
}
