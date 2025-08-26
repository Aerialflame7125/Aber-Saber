using UnityEngine;

public class LebronSpriteController : MonoBehaviour
{
    [SerializeField]
    private MainSettingsModel _mainSettingsModel;

    [SerializeField]
    private GameObject lebron;

    private void Start()
    {
        if (_mainSettingsModel != null)
        {
            // Initialize with current setting
            UpdateState(_mainSettingsModel.enableLebronify);
        }
    }

    /// <summary>
    /// Public method that can be called to update the sprite's state
    /// </summary>
    public void UpdateState(bool active)
    {
        lebron.SetActive(active);
    }
}