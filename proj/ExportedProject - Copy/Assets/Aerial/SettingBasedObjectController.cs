using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

/// <summary>
/// Attaches to a MainSettingsModel game object and controls the activation of game objects
/// based on a specified setting variable value.
/// </summary>
public class SettingBasedObjectController : MonoBehaviour
{
    [Header("Setting Configuration")]
    [Tooltip("The name of the setting variable to check (without settingsModel prefix)")]
    [SerializeField] private string settingVariableName = "yourVariableName";

    [Header("Object Lists")]
    [Tooltip("Objects to enable when the setting is TRUE")]
    [SerializeField] private List<GameObject> objectsToEnableWhenTrue = new List<GameObject>();

    [Tooltip("Objects to enable when the setting is FALSE")]
    [SerializeField] private List<GameObject> objectsToEnableWhenFalse = new List<GameObject>();

    [Header("Behavior Settings")]
    [Tooltip("Check this setting on Start")]
    [SerializeField] private bool checkOnStart = true;

    [Tooltip("Debug mode - prints info about which setting is being checked")]
    [SerializeField] private bool debugMode = false;

    // Reference to the settings model
    [Space]
    [SerializeField]
    private MainSettingsModel settingsModel;

    private void Awake()
    {
        if (settingsModel == null)
        {
            Debug.LogError("SettingBasedObjectController: No MainSettingsModel component found on this GameObject!");
        }
    }

    private void Start()
    {
        if (checkOnStart)
        {
            ApplySettingState();
        }
    }

    /// <summary>
    /// Apply the current state of the setting to the objects in the lists.
    /// </summary>
    public void ApplySettingState()
    {
        if (settingsModel == null)
        {
            Debug.LogError("SettingBasedObjectController: Cannot apply setting state, MainSettingsModel is missing!");
            return;
        }

        bool settingValue = GetSettingValue();

        if (debugMode)
        {
            Debug.Log($"Setting '{settingVariableName}' = {settingValue}");
        }

        // Enable/disable objects based on setting value
        SetObjectsState(objectsToEnableWhenTrue, settingValue);
        SetObjectsState(objectsToEnableWhenFalse, !settingValue);
    }

    /// <summary>
    /// Get the current value of the specified setting variable using reflection.
    /// </summary>
    /// <returns>True if the setting is enabled, false otherwise.</returns>
    private bool GetSettingValue()
    {
        try
        {
            // Get the field info from the MainSettingsModel type
            FieldInfo fieldInfo = settingsModel.GetType().GetField(settingVariableName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (fieldInfo == null)
            {
                Debug.LogError($"SettingBasedObjectController: Variable '{settingVariableName}' not found in MainSettingsModel!");
                return false;
            }

            // Get the value of the field
            object value = fieldInfo.GetValue(settingsModel);

            // Try to convert to bool
            if (value is bool boolValue)
            {
                return boolValue;
            }
            else
            {
                Debug.LogError($"SettingBasedObjectController: Variable '{settingVariableName}' is not a boolean value!");
                return false;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"SettingBasedObjectController: Error accessing variable '{settingVariableName}': {e.Message}");
            return false;
        }
    }

    /// <summary>
    /// Set the active state of all objects in the given list.
    /// </summary>
    /// <param name="objects">List of GameObjects to modify</param>
    /// <param name="state">Active state to set</param>
    private void SetObjectsState(List<GameObject> objects, bool state)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(state);

                if (debugMode)
                {
                    Debug.Log($"Object '{obj.name}' set to {state}");
                }
            }
            else if (debugMode)
            {
                Debug.LogWarning("SettingBasedObjectController: A null GameObject reference found in list");
            }
        }
    }

    /// <summary>
    /// Public method to manually refresh the object states.
    /// </summary>
    public void RefreshObjectStates()
    {
        ApplySettingState();
    }
}