using UnityEngine;
public class EnableAerialLogTextController : SwitchSettingsController
{
    [SerializeField]
    private MainSettingsModel _mainSettingsModel;

    [SerializeField]
    private GameObject firstObject;  // Reference to the first GameObject

    protected override bool GetInitValue()
    {
        bool initialValue = _mainSettingsModel.enableAerialLogText;
        // Set the initial state of both objects based on the setting
        SetObjectsActive(initialValue);
        return initialValue;
    }

    protected override void ApplyValue(bool value)
    {
        _mainSettingsModel.enableAerialLogText = value;

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
        if (firstObject != null)
        {
            firstObject.SetActive(active);
        }
    }
}