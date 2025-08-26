using System.Collections;
using UnityEngine;

public class GameplayManager : MonoBehaviour, IPauseTriggerSetable
{
	public enum GameState
	{
		Intro,
		Playing,
		Paused,
		Finished,
		Failed
	}

	[SerializeField]
	private GameSongController _gameSongController;

	[SerializeField]
	private GameEnergyCounter _gameEnergyCounter;

	[SerializeField]
	private GamePauseManager _gamePauseManager;

	[Space]
	[SerializeField]
	[EventSender]
	private GameEvent _levelFailedEvent;

	[SerializeField]
	[EventSender]
	private GameEvent _levelFinishedEvent;

	[SerializeField]
	private GameEvent _pauseMenuDidFinishWithContinueEvent;

	private GameState _gameState;

	private VRPlatformHelper _vrPlatformHelper;

	private IPauseTrigger _pauseTrigger;

	public GameState gameState
	{
		get
		{
			return _gameState;
		}
		private set
		{
			if (_gameState != value)
			{
				_gameState = value;
				switch (value)
				{
				case GameState.Finished:
					_levelFinishedEvent.Raise();
					break;
				case GameState.Failed:
					_levelFailedEvent.Raise();
					break;
				}
			}
		}
	}

	private void Awake()
	{
		_gameState = GameState.Intro;
		_gameSongController.songDidFinishEvent += HandleSongDidFinish;
		_gameEnergyCounter.gameEnergyDidReach0Event += HandleGameEnergyDidReach0;
		_vrPlatformHelper = PersistentSingleton<VRPlatformHelper>.instance;
		_vrPlatformHelper.inputFocusWasCapturedEvent += HandleInputFocusWasCaptured;
		_pauseMenuDidFinishWithContinueEvent.Subscribe(HandlePauseMenuDidFinishWithContinue);
	}

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		gameState = GameState.Playing;
		_gameSongController.StartSong();
	}

	private void OnDestroy()
	{
		if (_gameSongController != null)
		{
			_gameSongController.songDidFinishEvent -= HandleSongDidFinish;
		}
		if (_gameEnergyCounter != null)
		{
			_gameEnergyCounter.gameEnergyDidReach0Event -= HandleGameEnergyDidReach0;
		}
		if (_vrPlatformHelper != null)
		{
			_vrPlatformHelper.inputFocusWasCapturedEvent -= HandleInputFocusWasCaptured;
		}
		_pauseMenuDidFinishWithContinueEvent.Unsubscribe(HandlePauseMenuDidFinishWithContinue);
	}

	private void Update()
	{
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
			if (_gameState == GameState.Playing)
			{
				Pause();
			}
		});
	}

	private void HandleGameEnergyDidReach0()
	{
		if (_gameState != GameState.Failed && _gameState != GameState.Finished)
		{
			gameState = GameState.Failed;
		}
	}

	private void HandleSongDidFinish()
	{
		if (_gameState != GameState.Failed && _gameState != GameState.Finished)
		{
			gameState = GameState.Finished;
		}
	}

	private void HandleInputFocusWasCaptured()
	{
		if (_gameState == GameState.Playing)
		{
			Pause();
		}
	}

	private void HandlePauseMenuDidFinishWithContinue()
	{
		ResumeFromPause();
	}

	private void Pause()
	{
		if (_gameState == GameState.Playing)
		{
			_gameState = GameState.Paused;
			_gamePauseManager.pause = true;
		}
	}

	private void ResumeFromPause()
	{
		if (_gameState == GameState.Paused)
		{
			_gameState = GameState.Playing;
			_gamePauseManager.pause = false;
		}
	}
}
