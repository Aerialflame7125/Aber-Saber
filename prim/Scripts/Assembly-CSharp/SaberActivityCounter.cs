using UnityEngine;

public class SaberActivityCounter : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(PlayerController))]
	private ObjectProvider _playerControllerProvider;

	[SerializeField]
	private float _averageWindowDuration = 0.5f;

	[SerializeField]
	private float _valuesPerSecond = 2f;

	[SerializeField]
	private float _increaseSpeed = 100f;

	[SerializeField]
	private float _deceraseSpeed = 20f;

	private Saber _leftSaber;

	private Saber _rightSaber;

	private Vector3 _prevLeftSaberTipPos;

	private Vector3 _prevRightSaberTipPos;

	private Vector3 _prevLeftHandPos;

	private Vector3 _prevRightHandPos;

	private bool _havePrevPos;

	private float _leftSaberMovementDistance;

	private float _rightSaberMovementDistance;

	private float _leftHandMovementDistance;

	private float _rightHandMovementDistance;

	private MovementHistoryRecorder _saberMovementHistoryRecorder;

	private MovementHistoryRecorder _handMovementHistoryRecorder;

	public float leftSaberMovementDistance => _leftSaberMovementDistance;

	public float rightSaberMovementDistance => _rightSaberMovementDistance;

	public float leftHandMovementDistance => _leftHandMovementDistance;

	public float rightHandMovementDistance => _rightHandMovementDistance;

	public AveragingValueRecorder saberMovementAveragingValueRecorder => _saberMovementHistoryRecorder.averagingValueRecorer;

	public AveragingValueRecorder handMovementAveragingValueRecorder => _handMovementHistoryRecorder.averagingValueRecorer;

	private void Awake()
	{
		_saberMovementHistoryRecorder = new MovementHistoryRecorder(_averageWindowDuration, _valuesPerSecond, _increaseSpeed, _deceraseSpeed);
		_handMovementHistoryRecorder = new MovementHistoryRecorder(_averageWindowDuration, _valuesPerSecond, _increaseSpeed, _deceraseSpeed);
	}

	private void Start()
	{
		PlayerController providedObject = _playerControllerProvider.GetProvidedObject<PlayerController>();
		_leftSaber = providedObject.leftSaber;
		_rightSaber = providedObject.rightSaber;
	}

	private void Update()
	{
		if (!(Time.timeSinceLevelLoad < 1f))
		{
			if (!_havePrevPos)
			{
				_prevLeftSaberTipPos = _leftSaber.saberBladeTopPos;
				_prevRightSaberTipPos = _rightSaber.saberBladeTopPos;
				_prevLeftHandPos = _leftSaber.handlePos;
				_prevRightHandPos = _rightSaber.handlePos;
				_havePrevPos = true;
			}
			Vector3 saberBladeTopPos = _leftSaber.saberBladeTopPos;
			float num = Vector3.Distance(saberBladeTopPos, _prevLeftSaberTipPos);
			_leftSaberMovementDistance += num;
			_prevLeftSaberTipPos = saberBladeTopPos;
			_saberMovementHistoryRecorder.AddMovement(num);
			saberBladeTopPos = _rightSaber.saberBladeTopPos;
			num = Vector3.Distance(saberBladeTopPos, _prevRightSaberTipPos);
			_rightSaberMovementDistance += num;
			_prevRightSaberTipPos = saberBladeTopPos;
			_saberMovementHistoryRecorder.AddMovement(num);
			_saberMovementHistoryRecorder.Update(Time.deltaTime);
			saberBladeTopPos = _leftSaber.handlePos;
			num = Vector3.Distance(saberBladeTopPos, _prevLeftHandPos);
			_leftHandMovementDistance += num;
			_prevLeftHandPos = saberBladeTopPos;
			_handMovementHistoryRecorder.AddMovement(num);
			saberBladeTopPos = _rightSaber.handlePos;
			num = Vector3.Distance(saberBladeTopPos, _prevRightHandPos);
			_rightHandMovementDistance += num;
			_prevRightHandPos = saberBladeTopPos;
			_handMovementHistoryRecorder.AddMovement(num);
			_handMovementHistoryRecorder.Update(Time.deltaTime);
		}
	}
}
