using UnityEngine;

public class EnableRealitySettingsController : SwitchSettingsController
{
    [SerializeField]
    private MainSettingsModel _mainSettingsModel;

    [SerializeField]
    private GameObject rcttsGameObject; // Reference to your "Rctts" GameObject

    // Cache for sprite controllers
    private RcttsSpriteController[] _spriteControllers;

    private void Start()
    {
        // Find all sprite controllers in the scene
        _spriteControllers = FindObjectsOfType<RcttsSpriteController>();
    }

    protected override bool GetInitValue()
    {
        bool initialValue = _mainSettingsModel.enableRctts;
        // Set the initial state of Rctts object and sprites based on the setting
        SetActiveStates(initialValue);
        return initialValue;
    }

    protected override void ApplyValue(bool value)
    {
        _mainSettingsModel.enableRctts = value;
        // Enable or disable Rctts object and sprites based on the value
        SetActiveStates(value);
    }

    protected override string TextForValue(bool value)
    {
        return (!value) ? "OFF" : "ON";
    }

    // Helper method to set active states
    private void SetActiveStates(bool active)
    {
        // Set Rctts GameObject active state
        if (rcttsGameObject != null)
        {
            rcttsGameObject.SetActive(active);
        }

        // Update all sprite controllers
        UpdateAllSpriteControllers(active);
    }

    // Helper method to update all sprite controllers
    private void UpdateAllSpriteControllers(bool active)
    {
        // If controllers haven't been found yet, find them
        if (_spriteControllers == null || _spriteControllers.Length == 0)
        {
            _spriteControllers = FindObjectsOfType<RcttsSpriteController>();
        }

        // Update each controller
        foreach (var controller in _spriteControllers)
        {
            if (controller != null)
            {
                controller.UpdateState(active);
            }
        }
    }
}