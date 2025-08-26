using UnityEngine;
public class EnableDemoTextSettingsController : SwitchSettingsController
{
    [SerializeField]
    private MainSettingsModel _mainSettingsModel;

    [SerializeField]
    private GameObject firstObject;  // Reference to the first GameObject

    [SerializeField]
    private GameObject secondObject;  // Reference to the second GameObject

    [SerializeField]
    private GameObject thirdObject;  // Reference to the third GameObject

    protected override bool GetInitValue()
    {
        bool initialValue = _mainSettingsModel.enableDemoText;
        // Set the initial state of both objects based on the setting
        SetObjectsActive(initialValue);
        return initialValue;
    }

    protected override void ApplyValue(bool value)
    {
        _mainSettingsModel.enableDemoText = value;

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

        if (secondObject != null)
        {
            secondObject.SetActive(active);
        }

        if (thirdObject != null)
        {
            thirdObject.SetActive(active);
        }
    }
}