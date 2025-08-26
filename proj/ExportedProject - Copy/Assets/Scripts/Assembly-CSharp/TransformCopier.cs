using UnityEngine;

public class TransformCopier : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("The transform of the object to follow")]
    public Transform target;

    [Header("Following Settings")]
    [Tooltip("Copy the position of the target")]
    public bool copyPosition = true;
    [Tooltip("Copy the rotation of the target")]
    public bool copyRotation = true;
    [Tooltip("Position offset from the target")]
    public Vector3 positionOffset = Vector3.zero;
    [Tooltip("Rotation offset from the target (in Euler angles)")]
    public Vector3 rotationOffset = Vector3.zero;
    [Tooltip("Use local coordinates instead of world coordinates")]
    public bool useLocalTransform = false;

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("No target assigned to TransformCopier script!");
            return;
        }

        // Copy position if enabled
        if (copyPosition)
        {
            if (useLocalTransform)
            {
                transform.localPosition = target.localPosition + positionOffset;
            }
            else
            {
                transform.position = target.position + positionOffset;
            }
        }

        // Copy rotation if enabled
        if (copyRotation)
        {
            if (useLocalTransform)
            {
                transform.localRotation = target.localRotation * Quaternion.Euler(rotationOffset);
            }
            else
            {
                transform.rotation = target.rotation * Quaternion.Euler(rotationOffset);
            }
        }
    }
}