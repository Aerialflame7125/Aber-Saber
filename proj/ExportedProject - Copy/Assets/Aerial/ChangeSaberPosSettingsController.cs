using UnityEngine;

public class ChangeSaberPosSettingsController : SwitchSettingsController
{
    [SerializeField]
    private MainSettingsModel _mainSettingsModel;

    // Optional references to update visuals directly from this controller
    [SerializeField, Tooltip("Optional: Object to enable when in Reverse mode")]
    private GameObject objectToEnableWhenReverse;

    [SerializeField, Tooltip("Optional: Object to enable when in Default mode")]
    private GameObject objectToEnableWhenDefault;

    protected override bool GetInitValue()
    {
        bool initialValue = _mainSettingsModel.enableSaberPos;

        // Optional: Update visuals if objects are assigned
        UpdateVisuals(initialValue);

        return initialValue;
    }

    protected override void ApplyValue(bool value)
    {
        _mainSettingsModel.enableSaberPos = value;

        // Optional: Update visuals if objects are assigned
        UpdateVisuals(value);
    }

    protected override string TextForValue(bool value)
    {
        return (!value) ? "Default" : "Reverse";
    }

    // Optional method to update visuals directly if needed
    private void UpdateVisuals(bool isReverse)
    {
        if (objectToEnableWhenReverse != null && objectToEnableWhenDefault != null)
        {
            objectToEnableWhenReverse.SetActive(isReverse);
            objectToEnableWhenDefault.SetActive(!isReverse);
        }
    }
}