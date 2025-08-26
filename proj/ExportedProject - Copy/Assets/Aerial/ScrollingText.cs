using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScrollingText : MonoBehaviour
{
    [Header("Text Settings")]
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private float scrollSpeed = 50f;
    [SerializeField] private GameObject targetObject; // The object text should appear over

    [Header("Text Content")]
    [SerializeField] private List<string> textLines = new List<string>();

    private RectTransform rectTransform;
    private float textHeight;
    private float resetPosition;
    private bool isOverTarget = false;

    private void Start()
    {
        if (textComponent == null)
            textComponent = GetComponent<TextMeshProUGUI>();

        rectTransform = textComponent.rectTransform;

        // Combine all lines into the text component
        string combinedText = string.Join("\n", textLines);
        textComponent.text = combinedText;

        // Calculate text height
        textHeight = textComponent.preferredHeight;

        // Set the reset position (when text should loop)
        resetPosition = -textHeight;

        // Initially hide the text
        textComponent.enabled = false;
    }

    private void Update()
    {
        // Check if text is over the target object
        CheckIfOverTarget();

        // Only scroll and display text if it's over the target
        if (isOverTarget)
        {
            textComponent.enabled = true;
            ScrollText();
        }
        else
        {
            textComponent.enabled = false;
        }
    }

    private void CheckIfOverTarget()
    {
        if (targetObject == null)
            return;

        // Cast a ray from the UI element position to detect if it's over the target
        Ray ray = Camera.main.ScreenPointToRay(RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            isOverTarget = hit.collider.gameObject == targetObject;
        }
        else
        {
            isOverTarget = false;
        }
    }

    private void ScrollText()
    {
        // Move text upward
        Vector3 position = rectTransform.localPosition;
        position.y -= scrollSpeed * Time.deltaTime;
        rectTransform.localPosition = position;

        // If text has scrolled off screen, reset its position to create infinite loop
        if (rectTransform.localPosition.y < resetPosition)
        {
            position.y = 0;
            rectTransform.localPosition = position;
        }
    }

    // Method to set the text lines (can be called from another script)
    public void SetTextLines(List<string> lines)
    {
        textLines = lines;
        string combinedText = string.Join("\n", textLines);
        textComponent.text = combinedText;
        textHeight = textComponent.preferredHeight;
        resetPosition = -textHeight;
    }
}