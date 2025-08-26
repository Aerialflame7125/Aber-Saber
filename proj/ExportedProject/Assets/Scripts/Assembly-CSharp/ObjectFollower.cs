using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("The transform of the object to follow")]
    public Transform target;

    [Header("Following Settings")]
    [Tooltip("How quickly the object moves toward the target")]
    public float moveSpeed = 5f;
    [Tooltip("Minimum distance to keep from target")]
    public float minDistance = 0.5f;
    [Tooltip("Whether to look at the target while following")]
    public bool lookAtTarget = true;
    [Tooltip("How smoothly to rotate toward the target (higher = smoother)")]
    public float rotationSmoothing = 5f;
    [Tooltip("Offset position from the target")]
    public Vector3 offset = Vector3.zero;

    private void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("No target assigned to ObjectFollower script!");
            return;
        }

        // Calculate target position with offset
        Vector3 targetPosition = target.position + offset;

        // Calculate distance to target
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        // Only move if we're farther than the minimum distance
        if (distanceToTarget > minDistance)
        {
            // Move toward target
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        // Rotate to face target if enabled
        if (lookAtTarget)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothing * Time.deltaTime);
        }
    }
}