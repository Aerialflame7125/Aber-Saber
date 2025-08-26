using UnityEngine;

/// <summary>
/// This script creates a pulsating effect on the object's X-axis scale (width).
/// It allows for fine-tuning the pulsation speed, width range, and smoothness via an AnimationCurve.
/// </summary>
public class PulsingWidth : MonoBehaviour
{
    [Header("Pulsation Settings")]
    [Tooltip("The minimum width (local scale X) of the object.")]
    public float minWidth = 0.8f;

    [Tooltip("The maximum width (local scale X) of the object.")]
    public float maxWidth = 1.2f;

    [Tooltip("The speed of the pulsation. A value of 1 means one full cycle (min -> max -> min) takes 2 seconds.")]
    public float pulsationSpeed = 1f;

    [Header("Smoothness Control")]
    [Tooltip("A curve that defines the shape of the pulsation. The X-axis is time (0 to 1) and the Y-axis is the pulsation factor (0 to 1).")]
    public AnimationCurve pulsationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    // To store the original scale of the object, so we don't affect Y and Z.
    private Vector3 originalScale;

    void Awake()
    {
        // Store the initial scale when the script starts.
        originalScale = transform.localScale;
    }

    void Update()
    {
        // 1. Create a looping value between 0 and 1.
        // Mathf.PingPong creates a value that goes from 0 to the 'length' argument and back again.
        // We use 1.0 as the length to get a normalized 0 -> 1 -> 0 value.
        float pingPongTime = Mathf.PingPong(Time.time * pulsationSpeed, 1.0f);

        // 2. Evaluate the curve.
        // We use the ping-pong value as the "time" to sample our curve.
        // This applies the custom smoothness you define in the graph.
        float curveValue = pulsationCurve.Evaluate(pingPongTime);

        // 3. Map the curve value to our desired width range.
        // Mathf.Lerp interpolates between minWidth and maxWidth based on the curveValue (0 to 1).
        float targetWidth = Mathf.Lerp(minWidth, maxWidth, curveValue);

        // 4. Apply the new width while preserving the original Y and Z scale.
        transform.localScale = new Vector3(targetWidth, originalScale.y, originalScale.z);
    }
}