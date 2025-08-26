using UnityEngine;

public class MotionBlurHandler : MonoBehaviour
{
    [SerializeField]
    private float positionSmoothing = 0.1f; // Adjust for desired smoothness
    [SerializeField]
    private float rotationSmoothing = 0.1f;

    private Vector3 smoothedPosition;
    private Quaternion smoothedRotation;

    public Vector3 SmoothedPosition => smoothedPosition;
    public Quaternion SmoothedRotation => smoothedRotation;

    private void Start()
    {
        // Initialize smoothed values
        smoothedPosition = transform.position;
        smoothedRotation = transform.rotation;
    }

    private void Update()
    {
        // Smooth position and rotation
        smoothedPosition = Vector3.Lerp(smoothedPosition, transform.position, positionSmoothing * Time.deltaTime);
        smoothedRotation = Quaternion.Lerp(smoothedRotation, transform.rotation, rotationSmoothing * Time.deltaTime);
    }
}