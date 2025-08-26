using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

// The script now inherits from MonoBehaviour, not VRUINavigationController
public class HealthAndSafetyViewController : MonoBehaviour
{
    // This is the reference to the button in your scene.
    [SerializeField]
    private Button _continueButton;

    [SerializeField]
    private FadeOutOnGameEvent _fadeOutOnGameEvent;

    // A standard Unity Start method, called once when the script is first enabled.
    private void Start()
    {
        // We do our setup here, as this is the standard entry point for MonoBehaviour scripts.
        if (_continueButton != null)
        {
            // First, make sure the button is interactable.
            // You can also handle enabling it later by calling _continueButton.interactable = true; from another script.
            _continueButton.interactable = true;

            // We hook our method to the button's click event here.
            _continueButton.onClick.AddListener(ContinueButtonPressed);
        }
    }

    // A standard Unity OnDisable method, called when the GameObject or script is disabled.
    private void OnDisable()
    {
        // It's good practice to do cleanup when the script is disabled.
        if (_continueButton != null)
        {
            // We remove the listener to avoid it being called accidentally later.
            _continueButton.onClick.RemoveListener(ContinueButtonPressed);
        }
    }

    // --- Button Click Handler ---

    private void ContinueButtonPressed()
    {
        // Add a log message to confirm the button click is being registered.
        Debug.Log("Continue Button Pressed, attempting to load Menu scene...");

        StartCoroutine(FadeOutAndLoadScene());
    }

    IEnumerator FadeOutAndLoadScene()
    {
        yield return StartCoroutine(_fadeOutOnGameEvent.HandleGameEventCoroutine(0.5f));
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}