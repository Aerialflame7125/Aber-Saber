using UnityEngine;

public class RcttsSpriteController : MonoBehaviour
{
    [SerializeField]
    private MainSettingsModel _mainSettingsModel;

    [SerializeField]
    private GameObject rctts;

    private void Start()
    {
        if (_mainSettingsModel != null)
        {
            // Initialize with current setting
            UpdateState(_mainSettingsModel.enableRctts);
        }
    }

    /// <summary>
    /// Public method that can be called to update the sprite's state
    /// </summary>
    public void UpdateState(bool active)
    {
        rctts.SetActive(active);
    }
}