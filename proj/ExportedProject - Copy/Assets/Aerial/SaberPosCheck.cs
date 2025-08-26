using UnityEngine;

public class SaberPosCheck : MonoBehaviour
{
    [SerializeField]
    private MainSettingsModel _mainSettingsModel;

    [SerializeField]
    private GameObject objectToEnableWhenReverse;

    [SerializeField]
    private GameObject objectToEnableWhenDefault;

    [SerializeField, Tooltip("How often to check settings value (in seconds)")]
    private float checkInterval = 0.5f;

    private float _timeSinceLastCheck;
    private bool _lastCheckedValue;

    private void Awake()
    {
        // Check immediately when scene starts, even before Start is called
        CheckAndApplySettings();
    }

    private void Start()
    {
        // Double check on Start to ensure settings are applied
        CheckAndApplySettings();
    }

    private void Update()
    {
        _timeSinceLastCheck += Time.deltaTime;

        if (_timeSinceLastCheck >= checkInterval)
        {
            CheckAndApplySettings();
            _timeSinceLastCheck = 0f;
        }
    }

    // This method can also be called directly from other scripts or events
    public void CheckAndApplySettings()
    {
        if (_mainSettingsModel == null)
        {
            Debug.LogWarning("MainSettingsModel not assigned in SaberPosCheck!");
            return;
        }

        // Changed from enableLebronify to enableSaberPos to match the controller
        bool currentValue = _mainSettingsModel.enableSaberPos;

        // Only update if the value has changed or it's the first check
        if (currentValue != _lastCheckedValue || Time.timeSinceLevelLoad < 1f)
        {
            _lastCheckedValue = currentValue;

            // Enable/disable objects based on setting value
            // Renamed variables for clarity - true = Reverse, false = Default
            if (objectToEnableWhenReverse != null)
                objectToEnableWhenReverse.SetActive(currentValue);

            if (objectToEnableWhenDefault != null)
                objectToEnableWhenDefault.SetActive(!currentValue);
        }
    }

    // Public method to force an immediate check
    public void ForceCheck()
    {
        CheckAndApplySettings();
    }
}