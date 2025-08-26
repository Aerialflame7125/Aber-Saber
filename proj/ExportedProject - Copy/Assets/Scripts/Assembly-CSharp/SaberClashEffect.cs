using UnityEngine;
using UnityEngine.XR;

public class SaberClashEffect : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(SaberClashChecker))]
	private ObjectProvider _saberClashCheckerProvider;

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

	private void Awake()
	{
		_sparkleParticleSystemEmmisionModule = _sparkleParticleSystem.emission;
		_sparkleParticleSystemEmmisionModule.enabled = false;
		_glowParticleSystemEmmisionModule = _glowParticleSystem.emission;
		_glowParticleSystemEmmisionModule.enabled = false;
	}

	private void Start()
	{
		_saberClashChecker = _saberClashCheckerProvider.GetProvidedObject<SaberClashChecker>();
	}

	private void OnDisable()
	{
		if (_sabersAreClashing)
		{
			_sabersAreClashing = false;
		}
	}

	private void Update()
	{
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
