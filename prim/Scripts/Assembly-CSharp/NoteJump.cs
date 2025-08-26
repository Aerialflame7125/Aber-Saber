using System;
using UnityEngine;

public class NoteJump : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(PlayerController))]
	private ObjectProvider _playerControllerProvider;

	[SerializeField]
	private FloatVariable _songTime;

	[Space]
	[SerializeField]
	private Transform _rotatedObject;

	[Space]
	[SerializeField]
	private float _yAvoidanceUp = 0.45f;

	[SerializeField]
	private float _yAvoidanceDown = 0.15f;

	private Vector3 _startPos;

	private Vector3 _endPos;

	private float _jumpDuration;

	private float _startTime;

	private float _startVerticalVelocity;

	private Quaternion _startRotation;

	private Quaternion _middleRotation;

	private Quaternion _endRotation;

	private float _gravity;

	private float _yAvoidance;

	private PlayerController _playerController;

	private float _missedTime;

	private bool _missedMarkReported;

	private bool _threeQuartersMarkReported;

	public const float kMissedTimeOffset = 0.15f;

	public float startTime => _startTime;

	public float jumpDuration => _jumpDuration;

	public float velocityZ => (_endPos.z - _startPos.z) / _jumpDuration;

	public event Action noteJumpDidFinishEvent;

	public event Action noteJumpDidPassMissedMarkerEvent;

	public event Action<NoteJump> noteJumpDidPassThreeQuartersEvent;

	public void Init(Vector3 startPos, Vector3 endPos, float jumpDuration, float startTime, float gravity, float flipYSide, NoteCutDirection cutDirection)
	{
		_startPos = startPos;
		_endPos = endPos;
		_jumpDuration = jumpDuration;
		_startTime = startTime;
		_gravity = gravity;
		if (flipYSide > 0f)
		{
			_yAvoidance = flipYSide * _yAvoidanceUp;
		}
		else
		{
			_yAvoidance = flipYSide * _yAvoidanceDown;
		}
		_missedMarkReported = false;
		_threeQuartersMarkReported = false;
		_startVerticalVelocity = _gravity * _jumpDuration * 0.5f;
		_endRotation = default(Quaternion);
		switch (cutDirection)
		{
		case NoteCutDirection.Left:
			_endRotation.eulerAngles = new Vector3(0f, 0f, -90f);
			break;
		case NoteCutDirection.Right:
			_endRotation.eulerAngles = new Vector3(0f, 0f, 90f);
			break;
		case NoteCutDirection.Up:
			_endRotation.eulerAngles = new Vector3(0f, 0f, 180f);
			break;
		case NoteCutDirection.Down:
			_endRotation = Quaternion.identity;
			break;
		case NoteCutDirection.UpLeft:
			_endRotation.eulerAngles = new Vector3(0f, 0f, -135f);
			break;
		case NoteCutDirection.UpRight:
			_endRotation.eulerAngles = new Vector3(0f, 0f, 135f);
			break;
		case NoteCutDirection.DownLeft:
			_endRotation.eulerAngles = new Vector3(0f, 0f, -45f);
			break;
		case NoteCutDirection.DownRight:
			_endRotation.eulerAngles = new Vector3(0f, 0f, 45f);
			break;
		default:
			_endRotation = Quaternion.identity;
			break;
		}
		_missedTime = startTime + jumpDuration * 0.5f + 0.15f;
		Vector3 eulerAngles = _endRotation.eulerAngles;
		eulerAngles += UnityEngine.Random.insideUnitSphere * 30f;
		_middleRotation = default(Quaternion);
		_middleRotation.eulerAngles = eulerAngles;
	}

	private void Start()
	{
		_playerController = _playerControllerProvider.GetProvidedObject<PlayerController>();
		_startRotation = _rotatedObject.transform.rotation;
	}

	private float EaseInOutQuad(float t)
	{
		return (!(t < 0.5f)) ? (-1f + (4f - 2f * t) * t) : (2f * t * t);
	}

	public void ManualUpdate()
	{
		float num = _songTime.value - _startTime;
		float num2 = num / _jumpDuration;
		Vector3 vector = default(Vector3);
		if (_startPos.x == _endPos.x)
		{
			vector.x = _startPos.x;
		}
		else if (num2 < 0.25f)
		{
			vector.x = _startPos.x + (_endPos.x - _startPos.x) * EaseInOutQuad(num2 * 4f);
		}
		else
		{
			vector.x = _endPos.x;
		}
		vector.z = _playerController.MoveTowardsHead(_startPos.z, _endPos.z, num2);
		vector.y = _startPos.y + _startVerticalVelocity * num - _gravity * num * num * 0.5f;
		if (_yAvoidance != 0f && num2 < 0.25f)
		{
			float num3 = 0.5f - Mathf.Cos(num2 * 8f * (float)Math.PI) * 0.5f;
			vector.y += num3 * _yAvoidance;
		}
		base.transform.position = vector;
		if (num2 < 0.5f)
		{
			Quaternion a = ((!(num2 < 0.125f)) ? Quaternion.Lerp(_middleRotation, _endRotation, Mathf.Sin((num2 - 0.125f) * (float)Math.PI * 2f)) : Quaternion.Lerp(_startRotation, _middleRotation, Mathf.Sin(num2 * (float)Math.PI * 4f)));
			Vector3 headPos = _playerController.headPos;
			headPos.y = Mathf.Lerp(headPos.y, vector.y, 0.8f);
			Vector3 normalized = (vector - headPos).normalized;
			Quaternion b = default(Quaternion);
			b.SetLookRotation(normalized, _rotatedObject.up);
			_rotatedObject.localRotation = Quaternion.Lerp(a, b, num2 * 2f);
		}
		if (num2 >= 0.75f && !_threeQuartersMarkReported)
		{
			_threeQuartersMarkReported = true;
			if (this.noteJumpDidPassThreeQuartersEvent != null)
			{
				this.noteJumpDidPassThreeQuartersEvent(this);
			}
		}
		if (_songTime.value >= _missedTime && !_missedMarkReported)
		{
			_missedMarkReported = true;
			if (this.noteJumpDidPassMissedMarkerEvent != null)
			{
				this.noteJumpDidPassMissedMarkerEvent();
			}
		}
		if (num2 >= 1f && this.noteJumpDidFinishEvent != null)
		{
			this.noteJumpDidFinishEvent();
		}
	}
}
