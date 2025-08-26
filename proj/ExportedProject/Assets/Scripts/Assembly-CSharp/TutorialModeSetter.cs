using UnityEngine;

public class TutorialModeSetter : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(SaberClashChecker))]
	private ObjectProvider _saberClashCheckerProvider;

	[SerializeField]
	private AudioSource _audioSource;

	[SerializeField]
	private float _fadeinSpeed = 0.1f;

	[SerializeField]
	private float _fadeoutSpeed = 1f;

	[SerializeField]
	private SongPreviewPlayer _songPreviewPlayer;

	[SerializeField]
	private IntroTutorialController _introTutorialController;

	[SerializeField]
	private TutorialSongController _tutorialSongController;

	[SerializeField]
	private TutorialNoteCutEffectSpawner _tutorialNoteCutEffectSpawner;

	[SerializeField]
	private NoteCutSoundEffectManager _noteCutSoundEffectManager;

	[SerializeField]
	private IntroTutorialRing _leftTutorialRing;

	[SerializeField]
	private IntroTutorialRing _rightTutorialRing;

	[SerializeField]
	[EventSender]
	private GameEvent noteCuttingInAnyDirectionDidStartEvent;

	[SerializeField]
	private TutorialModeCheckerSO _tutorialModeCheckerSO;

	private SaberClashChecker _saberClashChecker;

	private float _songPreviewPlayerInitialVolume;

	private Saber.SaberType _leftSaberType;

	private Saber.SaberType _rightSaberType;

	private bool _fadeingOut;

	private void Start()
	{
		_saberClashChecker = _saberClashCheckerProvider.GetProvidedObject<SaberClashChecker>();
		_songPreviewPlayerInitialVolume = _songPreviewPlayer.volume;
		_leftSaberType = _leftTutorialRing.saberType;
		_rightSaberType = _rightTutorialRing.saberType;
		_introTutorialController.introTutorialWillFinishEvent += HandleIntroTutorialControllerWillFinish;
	}

	private void Update()
	{
		if (_fadeingOut)
		{
			_audioSource.volume -= Time.deltaTime * _fadeoutSpeed;
			if (_audioSource.volume <= 0f)
			{
				_audioSource.Stop();
				base.enabled = false;
			}
			return;
		}
		float volume = _audioSource.volume;
		if (_saberClashChecker.sabersAreClashing)
		{
			if (!_audioSource.isPlaying)
			{
				_audioSource.Play();
			}
			volume += Time.deltaTime * _fadeinSpeed;
			if (volume > 1f)
			{
				volume = 1f;
			}
		}
		else
		{
			volume -= Time.deltaTime * _fadeoutSpeed;
			if (volume <= 0f)
			{
				_audioSource.Stop();
				volume = 0f;
			}
		}
		_audioSource.volume = volume;
		_songPreviewPlayer.volume = (1f - volume) * _songPreviewPlayerInitialVolume;
		if (_saberClashChecker.sabersAreClashing)
		{
			_leftTutorialRing.saberType = _rightSaberType;
			_rightTutorialRing.saberType = _leftSaberType;
		}
		else
		{
			_leftTutorialRing.saberType = _leftSaberType;
			_rightTutorialRing.saberType = _rightSaberType;
		}
	}

	private void HandleTutorialSongControllerSongDidFinish()
	{
		_tutorialModeCheckerSO.tutorialModeEnabled = true;
	}

	private void HandleIntroTutorialControllerWillFinish()
	{
		_fadeingOut = true;
		if (_leftTutorialRing.saberType != _leftSaberType)
		{
			_tutorialSongController.songDidFinishEvent += HandleTutorialSongControllerSongDidFinish;
			_tutorialSongController.specialTutorialMode = true;
			_tutorialNoteCutEffectSpawner.handleWrongSaberTypeAsGoodAndDontWarnOnBadCuts = true;
			_noteCutSoundEffectManager.handleWrongSaberTypeAsGood = true;
		}
	}
}
