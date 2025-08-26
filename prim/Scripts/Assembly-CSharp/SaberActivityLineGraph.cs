using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberActivityLineGraph : MonoBehaviour
{
	[SerializeField]
	private SaberActivityCounter _saberActivityCounter;

	[SerializeField]
	private LineRenderer _lineRenderer;

	[SerializeField]
	private LineRenderer _lineRenderer2;

	[SerializeField]
	private int _pointCount = 100;

	[SerializeField]
	private float _pointDistance = 0.02f;

	[SerializeField]
	private float _scale = 0.01f;

	[SerializeField]
	private float _updateFps = 10f;

	private Vector3[] _pointPositions;

	private Queue<float> _pointsValues;

	private Queue<float> _pointsValues2;

	private void Awake()
	{
		_pointsValues = new Queue<float>(_pointCount);
		_pointsValues2 = new Queue<float>(_pointCount);
		_pointPositions = new Vector3[_pointCount];
		for (int i = 0; i < _pointPositions.Length; i++)
		{
			ref Vector3 reference = ref _pointPositions[i];
			reference = new Vector3((float)i * _pointDistance, 0f, 0f);
			_pointsValues.Enqueue(0f);
			_pointsValues2.Enqueue(0f);
		}
		_lineRenderer.positionCount = _pointCount;
		_lineRenderer.SetPositions(_pointPositions);
		_lineRenderer2.positionCount = _pointCount;
		_lineRenderer2.SetPositions(_pointPositions);
	}

	private void Start()
	{
		StartCoroutine(UpdateGraphCoroutine());
	}

	private IEnumerator UpdateGraphCoroutine()
	{
		YieldInstruction yieldInstruction = new WaitForSeconds(1f / _updateFps);
		while (true)
		{
			if (_pointsValues.Count == _pointPositions.Length)
			{
				_pointsValues.Dequeue();
			}
			float newValue = _saberActivityCounter.saberMovementAveragingValueRecorder.GetAverageValue();
			_pointsValues.Enqueue(newValue);
			int pointIndex = 0;
			foreach (float pointsValue in _pointsValues)
			{
				float num = pointsValue;
				ref Vector3 reference = ref _pointPositions[pointIndex];
				reference = new Vector3((float)pointIndex * _pointDistance, num * _scale);
				pointIndex++;
			}
			_lineRenderer.SetPositions(_pointPositions);
			if (_pointsValues2.Count == _pointPositions.Length)
			{
				_pointsValues2.Dequeue();
			}
			newValue = _saberActivityCounter.saberMovementAveragingValueRecorder.GetLastValue();
			_pointsValues2.Enqueue(newValue);
			pointIndex = 0;
			foreach (float item in _pointsValues2)
			{
				float num2 = item;
				ref Vector3 reference2 = ref _pointPositions[pointIndex];
				reference2 = new Vector3((float)pointIndex * _pointDistance, num2 * _scale);
				pointIndex++;
			}
			_lineRenderer2.SetPositions(_pointPositions);
			yield return yieldInstruction;
		}
	}
}
