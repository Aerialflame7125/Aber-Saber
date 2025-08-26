using UnityEngine;
using System.Collections.Generic;

public class EnableDebrisSettingsController : SwitchSettingsController
{
    [SerializeField]
    private MainSettingsModel _mainSettingsModel;

    [Header("Debris Configuration")]
    [SerializeField]
    private GameObject[] debrisPrefabs; // Array of prefabs that contain DebrisSpriteControllers

    [SerializeField]
    private GameObject debrisParent; // Parent GameObject for instantiated debris (changed from Transform)

    // Cache for sprite controllers
    private List<DebrisSpriteController> _spriteControllers = new List<DebrisSpriteController>();
    private List<GameObject> _instantiatedDebris = new List<GameObject>();
    private bool _initialized = false;

    private void Awake()
    {
        InstantiateDebrisPrefabs();
    }

    private void Start()
    {
        // Find all sprite controllers after instantiating prefabs
        RefreshSpriteControllers();

        // Initialize value based on settings
        _initialized = true;
    }

    protected override bool GetInitValue()
    {
        bool initialValue = _mainSettingsModel.enableDebris;
        Debug.Log($"EnableDebrisSettingsController: Initial value is {initialValue}");

        // Set the initial state of sprites based on the setting
        if (_initialized)
        {
            UpdateAllSpriteControllers(initialValue);
        }

        return initialValue;
    }

    protected override void ApplyValue(bool value)
    {
        // Debug log to verify changes
        Debug.Log($"EnableDebrisSettingsController: Setting enableDebris to {value}");

        _mainSettingsModel.enableDebris = value;

        // Update sprite states without disabling parent objects
        UpdateAllSpriteControllers(value);

        // Save the settings to ensure changes persist
        _mainSettingsModel.Save();
    }

    protected override string TextForValue(bool value)
    {
        return (!value) ? "OFF" : "ON";
    }

    // Helper method to instantiate debris prefabs
    private void InstantiateDebrisPrefabs()
    {
        if (debrisPrefabs != null && debrisPrefabs.Length > 0)
        {
            foreach (GameObject prefab in debrisPrefabs)
            {
                if (prefab != null)
                {
                    // Determine parent transform
                    Transform parent = (debrisParent != null) ? debrisParent.transform : transform;

                    // Instantiate the prefab
                    GameObject instance = Instantiate(prefab, parent);

                    // Always keep the prefab instance active
                    instance.SetActive(true);

                    _instantiatedDebris.Add(instance);

                    // Add any sprite controllers from this prefab instance directly
                    DebrisSpriteController[] controllers = instance.GetComponentsInChildren<DebrisSpriteController>(true);
                    if (controllers.Length > 0)
                    {
                        _spriteControllers.AddRange(controllers);
                        Debug.Log($"Found {controllers.Length} controllers in prefab {instance.name}");
                    }

                    Debug.Log($"Instantiated debris prefab: {instance.name}");
                }
            }
        }
    }

    // Find all sprite controllers in the scene and in instantiated prefabs
    private void RefreshSpriteControllers()
    {
        // First get controllers from active objects in the scene
        DebrisSpriteController[] sceneControllers = FindObjectsOfType<DebrisSpriteController>();

        // Add scene controllers not already in our list
        foreach (var controller in sceneControllers)
        {
            if (!_spriteControllers.Contains(controller))
            {
                _spriteControllers.Add(controller);
            }
        }

        // Now manually search for any inactive controllers in our instantiated prefabs
        foreach (GameObject debris in _instantiatedDebris)
        {
            if (debris != null)
            {
                DebrisSpriteController[] inactiveControllers = debris.GetComponentsInChildren<DebrisSpriteController>(true);
                foreach (var controller in inactiveControllers)
                {
                    if (!_spriteControllers.Contains(controller))
                    {
                        _spriteControllers.Add(controller);
                    }
                }
            }
        }

        Debug.Log($"Found total of {_spriteControllers.Count} DebrisSpriteControllers");

        // Apply initial state to all controllers
        if (_spriteControllers.Count > 0 && _mainSettingsModel != null)
        {
            UpdateAllSpriteControllers(_mainSettingsModel.enableDebris);
        }
    }

    // Helper method to update all sprite controllers
    private void UpdateAllSpriteControllers(bool active)
    {
        if (_spriteControllers != null && _spriteControllers.Count > 0)
        {
            Debug.Log($"Updating {_spriteControllers.Count} sprite controllers to {active}");

            // Update each controller to handle specific objects within prefab
            foreach (var controller in _spriteControllers)
            {
                if (controller != null)
                {
                    controller.UpdateState(active);
                }
            }
        }
        else
        {
            Debug.LogWarning("No sprite controllers found to update!");
        }
    }

    // Clean up instantiated debris on destroy
    private void OnDestroy()
    {
        foreach (GameObject obj in _instantiatedDebris)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
    }
}