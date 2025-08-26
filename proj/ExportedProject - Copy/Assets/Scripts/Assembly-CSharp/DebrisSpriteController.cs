using UnityEngine;

public class DebrisSpriteController : MonoBehaviour
{
    [SerializeField]
    private MainSettingsModel _mainSettingsModel;

    [SerializeField]
    private GameObject targetGameObject; // The specific GameObject inside the prefab to control

    [SerializeField]
    private bool invertBehavior = false; // Set to true to invert the behavior

    [SerializeField]
    private GameObject oppositeGameObject; // This GameObject will have the opposite active state

    // Track whether our script is initialized
    private bool _initialized = false;

    private void Awake()
    {
        // If no target is specified, default to a child object if possible, otherwise this GameObject
        if (targetGameObject == null)
        {
            // Try to find a child object first
            if (transform.childCount > 0)
            {
                targetGameObject = transform.GetChild(0).gameObject;
            }
            else
            {
                targetGameObject = gameObject;
            }
        }
    }

    private void Start()
    {
        // Initialize on start
        if (_mainSettingsModel != null)
        {
            // Apply initial state
            UpdateState(_mainSettingsModel.enableDebris);
        }
    }

    /// <summary>
    /// Public method that can be called from EnableDebrisSettingsController
    /// </summary>
    public void UpdateState(bool active)
    {
        Debug.Log($"DebrisSpriteController {name}: UpdateState called with {active}");

        // Apply inversion if needed
        bool targetActive = invertBehavior ? !active : active;

        // Set the target GameObject's active state (if assigned)
        if (targetGameObject != null && targetGameObject != gameObject)
        {
            targetGameObject.SetActive(targetActive);
            Debug.Log($"DebrisSpriteController {name}: Setting target {targetGameObject.name} to {targetActive}");
        }

        // Set the opposite GameObject's active state (if assigned)
        if (oppositeGameObject != null)
        {
            oppositeGameObject.SetActive(!targetActive);
            Debug.Log($"DebrisSpriteController {name}: Setting opposite {oppositeGameObject.name} to {!targetActive}");
        }

        _initialized = true;
    }
}