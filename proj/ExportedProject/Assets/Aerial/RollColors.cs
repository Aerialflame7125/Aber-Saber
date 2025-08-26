using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class RollColors : MonoBehaviour
{
    public float colorChangeSpeed = 1f;   // Speed of color cycling

    private float hue = 0f;
    private List<object> colorTargets = new List<object>();
    private List<PropertyInfo> colorProperties = new List<PropertyInfo>();

    void Start()
    {
        // Find all components with a .color property on this GameObject and its children
        foreach (var comp in GetComponentsInChildren<Component>())
        {
            var prop = comp.GetType().GetProperty("color", BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite && prop.PropertyType == typeof(Color))
            {
                colorTargets.Add(comp);
                colorProperties.Add(prop);
            }
        }
    }

    void Update()
    {
        // Increment hue over time, looping back to 0 after 1
        hue += Time.deltaTime * colorChangeSpeed;
        if (hue > 1f) hue -= 1f;

        // Convert HSV to RGB, full saturation and value for vivid colors
        Color color = Color.HSVToRGB(hue, 1f, 1f);

        // Set color on all found targets
        for (int i = 0; i < colorTargets.Count; i++)
        {
            colorProperties[i].SetValue(colorTargets[i], color, null);
        }
    }
}
