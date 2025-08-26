using System;
using System.Collections;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(PlayerController))]
	private ObjectProvider _playerControllerProvider;

	[SerializeField]
	[Provider(typeof(SongController))]
	private ObjectProvider _songControlerProvider;

	[SerializeField]
	private FloatVariable _songTime;

	[SerializeField]
	private ActiveObstaclesManager _activeObstaclesManager;

	[SerializeField]
	private StretchableObstacle _stretchableObstacle;

	[SerializeField]
	private float _height = 1f;

	public const float kAvoidMarkTimeOffset = 0.15f;

	private PlayerController _playerController;

	private Vector3 _startPos;

	private Vector3 _midPos;

	private Vector3 _endPos;

	private float _move1Duration;

	private float _move2Duration;

	private float _startTimeOffset;

	private float _obstacleDuration;

	private bool _passedThreeQuartersOfMove2Reported;

	private bool _passedAvoidedMarkReported;

	private bool _initialized;

	private Bounds _bounds;

	private bool _dissolving;

	private ObstacleData _obstacleData;

	public Bounds bounds
	{
		get
		{
			return _bounds;
		}
	}

	public ObstacleData obstacleData
	{
		get
		{
			return _obstacleData;
		}
	}

	public bool hasPassedAvoidedMark
	{
		get
		{
			return _passedAvoidedMarkReported;
		}
	}

	public event Action<ObstacleController> didInitEvent;

	public event Action<ObstacleController> finishedMovementEvent;

	public event Action<ObstacleController> passedThreeQuartersOfMove2Event;

	public event Action<ObstacleController> passedAvoidedMarkEvent;

	public event Action<ObstacleController, float> didStartDissolvingEvent;

	public event Action<ObstacleController> didDissolveEvent;

	private void OnEnable()
	{
		_activeObstaclesManager.RegisterObstacle(this);
	}

	private void OnDisable()
	{
		_activeObstaclesManager.UnregisterObstacle(this);
	}

	private void Start()
	{
		_playerController = _playerControllerProvider.GetProvidedObject<PlayerController>();
	}

	private void Update()
	{
		if (!_initialized)
		{
			return;
		}
		float num = _songTime.value - _startTimeOffset;
		Vector3 posForTime = GetPosForTime(num);
		base.transform.position = posForTime;
		if (!_passedThreeQuartersOfMove2Reported && num > _move1Duration + _move2Duration * 0.75f)
		{
			_passedThreeQuartersOfMove2Reported = true;
			if (this.passedThreeQuartersOfMove2Event != null)
			{
				this.passedThreeQuartersOfMove2Event(this);
			}
		}
		if (!_passedAvoidedMarkReported && num > _move1Duration + _move2Duration * 0.5f + _obstacleDuration + 0.15f)
		{
			_passedAvoidedMarkReported = true;
			if (this.passedAvoidedMarkEvent != null)
			{
				this.passedAvoidedMarkEvent(this);
			}
		}
		if (num > _move1Duration + _move2Duration + _obstacleDuration && this.finishedMovementEvent != null)
		{
			this.finishedMovementEvent(this);
		}
	}

	public void Init(ObstacleData obstacleData, Vector3 startPos, Vector3 midPos, Vector3 endPos, float move1Duration, float move2Duration, float startTimeOffset, float singleLineWidth)
	{
		_initialized = true;
		_obstacleData = obstacleData;
		_obstacleDuration = obstacleData.duration;
		float num = (float)obstacleData.width * singleLineWidth;
		Vector3 vector = new Vector3((num - singleLineWidth) * 0.5f, 0f, 0f);
		_startPos = startPos + vector;
		_midPos = midPos + vector;
		_endPos = endPos + vector;
		_move1Duration = move1Duration;
		_move2Duration = move2Duration;
		_startTimeOffset = startTimeOffset;
		float num2 = (_endPos - _midPos).magnitude / move2Duration;
		float length = num2 * obstacleData.duration;
		_stretchableObstacle.SetSize(num * 0.98f, _height, length);
		_bounds = _stretchableObstacle.bounds;
		_passedThreeQuartersOfMove2Reported = false;
		_passedAvoidedMarkReported = false;
		if (this.didInitEvent != null)
		{
			this.didInitEvent(this);
		}
	}

	private Vector3 GetPosForTime(float time)
	{
		if (time < _move1Duration)
		{
			return Vector3.LerpUnclamped(_startPos, _midPos, time / _move1Duration);
		}
		float t = (time - _move1Duration) / _move2Duration;
		Vector3 result = default(Vector3);
		result.x = _startPos.x;
		result.y = _startPos.y;
		result.z = _playerController.MoveTowardsHead(_midPos.z, _endPos.z, t);
		return result;
	}

	private IEnumerator DissolveCoroutine(float duration)
	{
		if (this.didStartDissolvingEvent != null)
		{
			this.didStartDissolvingEvent(this, duration);
		}
		yield return new WaitForSeconds(duration);
		_dissolving = false;
		if (this.didDissolveEvent != null)
		{
			this.didDissolveEvent(this);
		}
	}

	public void Dissolve(float duration)
	{
		if (!_dissolving)
		{
			_dissolving = true;
			StartCoroutine(DissolveCoroutine(duration));
		}
	}
}
