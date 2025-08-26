using System.Collections.Generic;
using UnityEngine;

public class Saber : MonoBehaviour
{
	public enum SaberType
	{
		SaberA = 0,
		SaberB = 1
	}

	[SerializeField]
	private Transform _topPos;

	[SerializeField]
	private Transform _bottomPos;

	[SerializeField]
	private Transform _handlePos;

	[SerializeField]
	private VRController _vrController;

	[SerializeField]
	private SaberTypeObject _saberType;

	private bool _prevValuesAreValid;

	private float _bladeSpeed;

	private float kOutOfRangeBladeSpeed = 100f;

	private float kSmoothUpBladeSpeedCoef = 16f;

	private float kSmoothDownBladeSpeedCoef = 4f;

	private SaberHistoryData _historyData;

	private List<SaberAfterCutSwingRatingCounter> _afterCutSwingRatingCounters = new List<SaberAfterCutSwingRatingCounter>(20);

	private List<SaberAfterCutSwingRatingCounter> _unusedAfterCutSwingRatingCounters = new List<SaberAfterCutSwingRatingCounter>(20);

	private const int kNumberOfPrealocatedAfterCutSwingRatingCounters = 20;

	private Collider[] _colliders = new Collider[8];

	public SaberType saberType
	{
		get
		{
			return _saberType.saberType;
		}
	}

	public Vector3 saberBladeTopPos
	{
		get
		{
			return _topPos.position;
		}
	}

	public Vector3 saberBladeBottomPos
	{
		get
		{
			return _bottomPos.position;
		}
	}

	public Transform saberBladeTopPosTransform
	{
		get
		{
			return _topPos;
		}
	}

	public Transform saberBladeBottomPosTransform
	{
		get
		{
			return _bottomPos;
		}
	}

	public Vector3 handlePos
	{
		get
		{
			return _handlePos.position;
		}
	}

	public bool disableCutting { get; set; }

	public float bladeSpeed
	{
		get
		{
			return _bladeSpeed;
		}
	}

	private void Start()
	{
		_historyData = new SaberHistoryData();
		for (int i = 0; i < 20; i++)
		{
			_unusedAfterCutSwingRatingCounters.Add(new SaberAfterCutSwingRatingCounter());
		}
	}

	private void Update()
	{
		if (!_vrController.active)
		{
			_prevValuesAreValid = false;
			return;
		}
		Vector3 position = _topPos.position;
		Vector3 position2 = _bottomPos.position;
		float timeSinceLevelLoad = Time.timeSinceLevelLoad;
		SaberHistoryData.TimeAndPos lastAddedTimeAndPos = _historyData.lastAddedTimeAndPos;
		int num = 0;
		while (num < _afterCutSwingRatingCounters.Count)
		{
			SaberAfterCutSwingRatingCounter saberAfterCutSwingRatingCounter = _afterCutSwingRatingCounters[num];
			if (saberAfterCutSwingRatingCounter.didFinish)
			{
				_afterCutSwingRatingCounters.RemoveAt(num);
				_unusedAfterCutSwingRatingCounters.Add(saberAfterCutSwingRatingCounter);
			}
			else
			{
				num++;
			}
		}
		int count = _afterCutSwingRatingCounters.Count;
		for (int i = 0; i < count; i++)
		{
			_afterCutSwingRatingCounters[i].ProcessNewSaberData(position, position2, timeSinceLevelLoad);
		}
		_historyData.AddNewData(position, position2, timeSinceLevelLoad);
		if (_prevValuesAreValid && !disableCutting)
		{
			if ((double)Time.deltaTime > 0.001)
			{
				float num2 = ((position - lastAddedTimeAndPos.topPos) / Time.deltaTime).magnitude;
				if (num2 > kOutOfRangeBladeSpeed)
				{
					num2 = 0f;
				}
				_bladeSpeed = Mathf.Lerp(_bladeSpeed, num2, (!(num2 < _bladeSpeed)) ? kSmoothDownBladeSpeedCoef : (Time.deltaTime * kSmoothUpBladeSpeedCoef));
			}
			Vector3 center;
			Vector3 halfSize;
			Quaternion orientation;
			if (GeometryTools.ThreePointsToBox(position, position2, (lastAddedTimeAndPos.bottomPos + lastAddedTimeAndPos.topPos) * 0.5f, out center, out halfSize, out orientation))
			{
				int num3 = Physics.OverlapBoxNonAlloc(center, halfSize, _colliders, orientation, LayerMasks.noteLayerMask);
				for (int j = 0; j < num3; j++)
				{
					CuttableBySaber component = _colliders[j].gameObject.GetComponent<CuttableBySaber>();
					component.Cut(this, center, orientation, position - lastAddedTimeAndPos.topPos);
				}
			}
		}
		_prevValuesAreValid = true;
	}

	public float ComputeSwingRating()
	{
		return _historyData.ComputeSwingRating();
	}

	public SaberAfterCutSwingRatingCounter CreateAfterCutSwingRatingCounter()
	{
		SaberAfterCutSwingRatingCounter saberAfterCutSwingRatingCounter;
		if (_unusedAfterCutSwingRatingCounters.Count > 0)
		{
			saberAfterCutSwingRatingCounter = _unusedAfterCutSwingRatingCounters[0];
			_unusedAfterCutSwingRatingCounters.RemoveAt(0);
		}
		else
		{
			saberAfterCutSwingRatingCounter = new SaberAfterCutSwingRatingCounter();
		}
		SaberHistoryData.TimeAndPos lastAddedTimeAndPos = _historyData.lastAddedTimeAndPos;
		saberAfterCutSwingRatingCounter.Init(_historyData.ComputeCutPlaneNormal(), lastAddedTimeAndPos.topPos, lastAddedTimeAndPos.bottomPos, lastAddedTimeAndPos.time);
		_afterCutSwingRatingCounters.Add(saberAfterCutSwingRatingCounter);
		return saberAfterCutSwingRatingCounter;
	}
}
