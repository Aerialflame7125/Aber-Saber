using UnityEngine;
using System.Collections; // Required for Coroutines

public class CurveMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float duration = 2.0f; // Time in seconds for one full movement
    public AnimationCurve movementCurve; // This will appear in the Inspector

    [Header("Looping")]
    public bool loop = true;
    public bool pingPong = true; // If true, moves back and forth

    private Vector3 currentTargetPosition;
    private Coroutine movementCoroutine;

    void Start()
    {
        // Set initial position if not already set in Inspector
        if (startPosition == Vector3.zero && endPosition == Vector3.zero)
        {
            startPosition = transform.position;
            endPosition = transform.position + new Vector3(5, 0, 0); // Example: move 5 units to the right
        }

        // Start the movement
        StartMovement();
    }

    public void StartMovement()
    {
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }
        movementCoroutine = StartCoroutine(MoveObjectCoroutine());
    }

    IEnumerator MoveObjectCoroutine()
    {
        while (true) // Loop indefinitely if 'loop' is true
        {
            // Move from start to end
            yield return StartCoroutine(MoveSingleDirection(startPosition, endPosition));

            if (pingPong)
            {
                // Move from end back to start
                yield return StartCoroutine(MoveSingleDirection(endPosition, startPosition));
            }

            if (!loop)
            {
                break; // Stop if not looping
            }
        }
    }

    IEnumerator MoveSingleDirection(Vector3 start, Vector3 end)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float percentage = Mathf.Clamp01(elapsed / duration); // Ensures value is between 0 and 1

            // Evaluate the curve to get a smoothed 0-1 value
            float curveValue = movementCurve.Evaluate(percentage);

            // Lerp between start and end position using the curve's value
            transform.position = Vector3.Lerp(start, end, curveValue);

            yield return null; // Wait for the next frame
        }

        // Ensure the object ends exactly at the target position
        transform.position = end;
    }

    // Optional: If you want to trigger movement from other scripts or UI
    public void ToggleMovement()
    {
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
            movementCoroutine = null;
        }
        else
        {
            StartMovement();
        }
    }
}