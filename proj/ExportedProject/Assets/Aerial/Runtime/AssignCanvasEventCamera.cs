using UnityEngine;

public class AssignCanvasEventCamera : MonoBehaviour
{
    void Start()
    {
        var canvas = GetComponent<Canvas>();
        if (canvas != null && canvas.renderMode == RenderMode.WorldSpace)
        {
            // Try to find the VR camera in any loaded scene
            Camera vrCamera = Camera.main;
            if (vrCamera == null)
            {
                // Fallback: search for a camera named "MainCamera" or "CenterEyeAnchor"
                foreach (var cam in GameObject.FindObjectsOfType<Camera>())
                {
                    if (cam.name == "MainCamera" || cam.name == "CenterEyeAnchor")
                    {
                        vrCamera = cam;
                        break;
                    }
                }
            }
            if (vrCamera != null)
            {
                canvas.worldCamera = vrCamera;
            }
            else
            {
                Debug.LogWarning("AssignCanvasEventCamera: Could not find VR camera to assign to Canvas.");
            }
        }
    }
}