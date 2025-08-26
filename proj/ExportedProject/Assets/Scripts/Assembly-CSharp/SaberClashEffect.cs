// In SaberClashEffect.cs

using UnityEngine;
using UnityEngine.XR;
using System.Collections; // Required for Coroutines

public class SaberClashEffect : MonoBehaviour
{
    // No longer need ObjectProvider here directly, as we're using FindObjectOfType
    // [SerializeField]
    // [Provider(typeof(SaberClashChecker))]
    // private ObjectProvider _saberClashCheckerProvider;

    [SerializeField]
    private HapticFeedbackController _hapticFeedbackController;

    [SerializeField]
    private ParticleSystem _sparkleParticleSystem;

    [SerializeField]
    private ParticleSystem _glowParticleSystem;

    private ParticleSystem.EmissionModule _sparkleParticleSystemEmmisionModule;
    private ParticleSystem.EmissionModule _glowParticleSystemEmmisionModule;

    private bool _sabersAreClashing;
    private SaberClashChecker _saberClashChecker;

    // Removed the ineffective 'wait' coroutine as it wasn't being started.

    private void Awake()
    {
        _sparkleParticleSystemEmmisionModule = _sparkleParticleSystem.emission;
        _sparkleParticleSystemEmmisionModule.enabled = false;
        _glowParticleSystemEmmisionModule = _glowParticleSystem.emission;
        _glowParticleSystemEmmisionModule.enabled = false;
    }

    private void Start()
    {
        // Start a coroutine to wait for SaberClashChecker to be ready
        StartCoroutine(WaitForSaberClashCheckerReady());
    }

    private IEnumerator WaitForSaberClashCheckerReady()
    {
        // First, get the SaberClashChecker instance.
        // Loop until it's found (in case it's instantiated later than SaberClashEffect)
        while (_saberClashChecker == null)
        {
            _saberClashChecker = FindObjectOfType<SaberClashChecker>();
            if (_saberClashChecker == null)
            {
                //Debug.LogWarning("SaberClashEffect: SaberClashChecker not found yet. Waiting...");
                yield return null; // Wait one frame before trying again
            }
        }

        // Now that _saberClashChecker is found, wait until its sabers are assigned by SaberLoader
        while (!_saberClashChecker.IsReady)
        {
            //Debug.LogWarning("SaberClashEffect: SaberClashChecker found, but sabers are not yet assigned. Waiting...");
            yield return null; // Wait one frame before checking again
        }

        Debug.Log("SaberClashEffect: SaberClashChecker is now fully ready with sabers assigned!");
        // Now you can safely use _saberClashChecker in Update and other methods.
        // (No further changes to Update are strictly necessary if _saberClashChecker is guaranteed non-null here)
    }

    private void OnDisable()
    {
        if (_sabersAreClashing)
        {
            _sabersAreClashing = false;
            _sparkleParticleSystemEmmisionModule.enabled = false;
            _glowParticleSystemEmmisionModule.enabled = false;
            _glowParticleSystem.Clear();
        }
    }

    private void Update()
    {
        // Now, this null check is primarily for robustness against unexpected unassignment,
        // as the coroutine should guarantee it's not null before Update logic runs.
        if (_saberClashChecker == null || !_saberClashChecker.IsReady)
        {
            return; // Skip Update logic if _saberClashChecker or its sabers are not ready
        }

        // Your original Update logic
        if (_saberClashChecker.sabersAreClashing)
        {
            base.transform.position = _saberClashChecker.clashingPoint;
            _hapticFeedbackController.ContinuousRumble(XRNode.LeftHand);
            _hapticFeedbackController.ContinuousRumble(XRNode.RightHand);
            if (!_sabersAreClashing)
            {
                _sabersAreClashing = true;
                _sparkleParticleSystemEmmisionModule.enabled = true;
                _glowParticleSystemEmmisionModule.enabled = true;
            }
        }
        else if (_sabersAreClashing)
        {
            _sabersAreClashing = false;
            _sparkleParticleSystemEmmisionModule.enabled = false;
            _glowParticleSystemEmmisionModule.enabled = false;
            _glowParticleSystem.Clear();
        }
    }
}