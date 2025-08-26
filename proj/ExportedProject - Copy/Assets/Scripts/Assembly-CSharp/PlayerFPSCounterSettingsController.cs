using UnityEngine;
public class PlayerFPSCounterSettingsController : SwitchSettingsController
{
    [SerializeField]
    private MainSettingsModel _mainSettingsModel;

    [SerializeField]
    private GameObject fpsCounterObject;  // Reference to the first FPS counter GameObject

    [SerializeField]
    private GameObject secondaryFpsObject;  // Reference to the second FPS counter GameObject

    protected override bool GetInitValue()
    {
        bool initialValue = _mainSettingsModel.enableFPSCounter;
        // Set the initial state of both objects based on the setting
        SetObjectsActive(initialValue);
        return initialValue;
    }

    protected override void ApplyValue(bool value)
    {
        _mainSettingsModel.enableFPSCounter = value;

        // Enable or disable both FPS counter objects based on the value
        SetObjectsActive(value);
    }

    protected override string TextForValue(bool value)
    {
        return (!value) ? "OFF" : "ON";
    }

    // Helper method to set both objects' active state
    private void SetObjectsActive(bool active)
    {
        if (fpsCounterObject != null)
        {
            fpsCounterObject.SetActive(active);
        }

        if (secondaryFpsObject != null)
        {
            secondaryFpsObject.SetActive(active);
        }
    }
}