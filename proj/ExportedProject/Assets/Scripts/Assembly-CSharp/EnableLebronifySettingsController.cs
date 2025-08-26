using UnityEngine;

public class EnableLebronifySettingsController : SwitchSettingsController
{
    [SerializeField]
    private MainSettingsModel _mainSettingsModel;

    [SerializeField]
    private GameObject lebronGameObject; // Reference to your "Lebron" GameObject

    // Cache for sprite controllers
    private LebronSpriteController[] _spriteControllers;

    private void Start()
    {
        // Find all sprite controllers in the scene
        _spriteControllers = FindObjectsOfType<LebronSpriteController>();
    }

    protected override bool GetInitValue()
    {
        bool initialValue = _mainSettingsModel.enableLebronify;
        // Set the initial state of Lebron object and sprites based on the setting
        SetActiveStates(initialValue);
        return initialValue;
    }

    protected override void ApplyValue(bool value)
    {
        _mainSettingsModel.enableLebronify = value;
        // Enable or disable Lebron object and sprites based on the value
        SetActiveStates(value);
    }

    protected override string TextForValue(bool value)
    {
        return (!value) ? "OFF" : "ON";
    }

    // Helper method to set active states
    private void SetActiveStates(bool active)
    {
        // Set Lebron GameObject active state
        if (lebronGameObject != null)
        {
            lebronGameObject.SetActive(active);
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
            _spriteControllers = FindObjectsOfType<LebronSpriteController>();
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