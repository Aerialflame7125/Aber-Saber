using System.Collections;
using UnityEngine;

public class TutorialController : MonoBehaviour, IPauseTriggerSetable
{
	[SerializeField]
	private TutorialSongController _tutorialSongController;

	[SerializeField]
	private IntroTutorialController _introTutorialController;

	[SerializeField]
	private AudioFading _audioFading;

	[SerializeField]
	private TutorialPauseManager _pauseManager;

	[SerializeField]
	private TutorialSceneSetupData _tutorialSceneSetupData;

	[SerializeField]
	private GameEvent _pauseMenuDidFinishWithContinueEvent;

	[Space]
	[SerializeField]
	[EventSender]
	private GameEvent _tutorialIntroStartedEvent;

	[SerializeField]
	[EventSender]
	private GameEvent _tutorialFinishedEvent;

	private bool _doingOutroTransition;

	private VRPlatformHelper _vrPlatformHelper;

	private IPauseTrigger _pauseTrigger;

	private void Awake()
	{
		_introTutorialController.introTutorialDidFinishEvent += HandleIntroTutorialDidFinishEvent;
		_tutorialSongController.songDidFinishEvent += HandleTutorialSongControllerSongDidFinishEvent;
		_vrPlatformHelper = PersistentSingleton<VRPlatformHelper>.instance;
		_vrPlatformHelper.inputFocusWasCapturedEvent += HandleInputFocusWasCaptured;
		_pauseMenuDidFinishWithContinueEvent.Subscribe(HandlePauseMenuDidFinishWithContinue);
	}

	private void Start()
	{
		_tutorialIntroStartedEvent.Raise();
	}

	private void OnDestroy()
	{
		_introTutorialController.introTutorialDidFinishEvent -= HandleIntroTutorialDidFinishEvent;
		_tutorialSongController.songDidFinishEvent -= HandleTutorialSongControllerSongDidFinishEvent;
		if (_vrPlatformHelper != null)
		{
			_vrPlatformHelper.inputFocusWasCapturedEvent -= HandleInputFocusWasCaptured;
		}
		_pauseMenuDidFinishWithContinueEvent.Unsubscribe(HandlePauseMenuDidFinishWithContinue);
	}

	public void SetPauseTrigger(IPauseTrigger pauseTrigger)
	{
		if (_pauseTrigger != null)
		{
			_pauseTrigger.SetCallback(null);
		}
		_pauseTrigger = pauseTrigger;
		_pauseTrigger.SetCallback(delegate
		{
			if (!_doingOutroTransition && !_pauseManager.pause)
			{
				Pause();
			}
		});
	}

	private void HandleIntroTutorialDidFinishEvent()
	{
		_tutorialSongController.StartSong();
	}

	private void HandleTutorialSongControllerSongDidFinishEvent()
	{
		_doingOutroTransition = true;
		_audioFading.FadeOut();
		StartCoroutine(OutroCoroutine());
	}

	private void HandleInputFocusWasCaptured()
	{
		if (!_doingOutroTransition && !_pauseManager.pause)
		{
			Pause();
		}
	}

	private void HandleDashboardWasActivated()
	{
		if (!_doingOutroTransition && !_pauseManager.pause)
		{
			Pause();
		}
	}

	private void HandlePauseMenuDidFinishWithContinue()
	{
		if (_pauseManager.pause)
		{
			_pauseManager.pause = false;
		}
	}

	private IEnumerator OutroCoroutine()
	{
		yield return new WaitForSeconds(0.5f);
		_tutorialFinishedEvent.Raise();
		yield return new WaitForSeconds(2.7f);
		_tutorialSceneSetupData.Finish(true);
	}

	private void Pause()
	{
		if (!_pauseManager.pause)
		{
			_pauseManager.pause = true;
		}
	}
}
