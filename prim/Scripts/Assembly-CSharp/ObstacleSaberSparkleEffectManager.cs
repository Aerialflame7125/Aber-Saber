using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ObstacleSaberSparkleEffectManager : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(PlayerController))]
	private ObjectProvider _playerControllerProvider;

	[SerializeField]
	private ActiveObstaclesManager _activeObstaclesManager;

	[Space]
	[SerializeField]
	private ObstacleSaberSparkleEffect _obstacleSaberSparkleEffectPefab;

	[SerializeField]
	private HapticFeedbackController _hapticFeedbackController;

	private Saber[] _sabers;

	private ObstacleSaberSparkleEffect[] _effects;

	private Transform[] _effectsTransforms;

	private bool[] _isSystemActive;

	private bool[] _wasSystemActive;

	private Vector3[] _burnMarkPositions;

	public event Action<Saber.SaberType> sparkleEffectDidStartEvent;

	public event Action<Saber.SaberType> sparkleEffectDidEndEvent;

	private void Awake()
	{
		_effects = new ObstacleSaberSparkleEffect[2];
		_effectsTransforms = new Transform[2];
		for (int i = 0; i < 2; i++)
		{
			_effects[i] = UnityEngine.Object.Instantiate(_obstacleSaberSparkleEffectPefab);
			_effectsTransforms[i] = _effects[i].transform;
		}
		_sabers = new Saber[2];
		_burnMarkPositions = new Vector3[2];
		_isSystemActive = new bool[2];
		_wasSystemActive = new bool[2];
	}

	private void Start()
	{
		PlayerController providedObject = _playerControllerProvider.GetProvidedObject<PlayerController>();
		_sabers[0] = providedObject.leftSaber;
		_sabers[1] = providedObject.rightSaber;
	}

	private void OnDisable()
	{
		if (!(_hapticFeedbackController != null))
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			if (_isSystemActive[i])
			{
				_isSystemActive[i] = false;
			}
		}
	}

	private void Update()
	{
		_wasSystemActive[0] = _isSystemActive[0];
		_wasSystemActive[1] = _isSystemActive[1];
		_isSystemActive[0] = false;
		_isSystemActive[1] = false;
		List<ObstacleController> activeObstacleControllers = _activeObstaclesManager.activeObstacleControllers;
		foreach (ObstacleController item in activeObstacleControllers)
		{
			Bounds bounds = item.bounds;
			for (int i = 0; i < 2; i++)
			{
				if (!_sabers[i].isActiveAndEnabled || !GetBurnMarkPos(bounds, item.transform, _sabers[i].saberBladeBottomPos, _sabers[i].saberBladeTopPos, out var burnMarkPos))
				{
					continue;
				}
				_isSystemActive[i] = true;
				_burnMarkPositions[i] = burnMarkPos;
				_effects[i].SetPositionAndRotation(burnMarkPos, GetEffectRotation(burnMarkPos, item.transform, bounds));
				XRNode node = ((i != 0) ? XRNode.RightHand : XRNode.LeftHand);
				_hapticFeedbackController.ContinuousRumble(node);
				if (!_wasSystemActive[i])
				{
					_effects[i].StartEmission();
					if (this.sparkleEffectDidEndEvent != null)
					{
						this.sparkleEffectDidStartEvent(_sabers[i].saberType);
					}
				}
			}
		}
		for (int j = 0; j < 2; j++)
		{
			if (!_isSystemActive[j] && _wasSystemActive[j])
			{
				_effects[j].StopEmission();
				if (this.sparkleEffectDidEndEvent != null)
				{
					this.sparkleEffectDidEndEvent(_sabers[j].saberType);
				}
			}
		}
	}

	private Quaternion GetEffectRotation(Vector3 pos, Transform transform, Bounds bounds)
	{
		pos = transform.InverseTransformPoint(pos);
		Vector3 direction = ((pos.x >= bounds.max.x - 0.01f) ? new Vector3(0f, 90f, 0f) : ((pos.x <= bounds.min.x + 0.01f) ? new Vector3(0f, -90f, 0f) : ((pos.y >= bounds.max.y - 0.01f) ? new Vector3(-90f, 0f, 0f) : ((!(pos.y <= bounds.min.y + 0.01f)) ? new Vector3(180f, 0f, 0f) : new Vector3(90f, 0f, 0f)))));
		return Quaternion.Euler(transform.TransformDirection(direction));
	}

	public Vector3 BurnMarkPosForSaberType(Saber.SaberType saberType)
	{
		if (saberType == _sabers[0].saberType)
		{
			return _burnMarkPositions[0];
		}
		return _burnMarkPositions[1];
	}

	private bool GetBurnMarkPos(Bounds bounds, Transform transform, Vector3 bladeBottomPos, Vector3 bladeTopPos, out Vector3 burnMarkPos)
	{
		bladeBottomPos = transform.InverseTransformPoint(bladeBottomPos);
		bladeTopPos = transform.InverseTransformPoint(bladeTopPos);
		float num = Vector3.Distance(bladeBottomPos, bladeTopPos);
		Vector3 vector = bladeTopPos - bladeBottomPos;
		vector.Normalize();
		if (bounds.IntersectRay(new Ray(bladeBottomPos, vector), out var distance) && distance <= num)
		{
			burnMarkPos = transform.TransformPoint(bladeBottomPos + vector * distance);
			return true;
		}
		burnMarkPos = Vector3.zero;
		return false;
	}
}
