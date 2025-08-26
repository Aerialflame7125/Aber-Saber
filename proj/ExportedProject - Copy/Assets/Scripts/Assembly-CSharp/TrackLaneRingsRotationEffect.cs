using System.Collections.Generic;
using UnityEngine;

public class TrackLaneRingsRotationEffect : MonoBehaviour
{
	private class RingRotationEffect
	{
		public float _progressPos;

		public float _rotationAngle;

		public float _rotationStep;

		public float _rotationPropagationSpeed;

		public float _rotationFlexySpeed;
	}

	[SerializeField]
	private TrackLaneRingsManager _trackLaneRingsManager;

	[Header("Startup buildup")]
	[SerializeField]
	private float _startupRotationAngle;

	[SerializeField]
	private float _startupRotationStep = 10f;

	[SerializeField]
	private float _startupRotationPropagationSpeed = 10f;

	[SerializeField]
	private float _startupRotationFlexySpeed = 0.5f;

	private List<RingRotationEffect> _ringRotationEffects;

	private List<int> effectIndicesToDelete = new List<int>();

	private void Awake()
	{
		_ringRotationEffects = new List<RingRotationEffect>();
	}

	private void Start()
	{
		AddRingRotationEffect(_startupRotationAngle, _startupRotationStep, _startupRotationPropagationSpeed, _startupRotationFlexySpeed);
	}

	private void Update()
	{
		effectIndicesToDelete.Clear();
		for (int i = 0; i < _ringRotationEffects.Count; i++)
		{
			for (int j = i + 1; j < _ringRotationEffects.Count; j++)
			{
				if (_ringRotationEffects[i]._progressPos < _ringRotationEffects[j]._progressPos)
				{
					effectIndicesToDelete.Add(i);
					break;
				}
			}
		}
		for (int k = 0; k < effectIndicesToDelete.Count; k++)
		{
			_ringRotationEffects.RemoveAt(effectIndicesToDelete[k]);
		}
		TrackLaneRing[] rings = _trackLaneRingsManager.Rings;
		for (int num = _ringRotationEffects.Count - 1; num >= 0; num--)
		{
			RingRotationEffect ringRotationEffect = _ringRotationEffects[num];
			float progressPos = ringRotationEffect._progressPos;
			ringRotationEffect._progressPos += Time.deltaTime * ringRotationEffect._rotationPropagationSpeed;
			for (int l = 0; l < rings.Length; l++)
			{
				float num2 = (float)l / (float)rings.Length;
				if (num2 >= progressPos && num2 < ringRotationEffect._progressPos)
				{
					rings[l].SetRotation(ringRotationEffect._rotationAngle + (float)l * ringRotationEffect._rotationStep, ringRotationEffect._rotationFlexySpeed);
				}
			}
			if (ringRotationEffect._progressPos > 1f)
			{
				_ringRotationEffects.RemoveAt(num);
			}
		}
	}

	public void AddRingRotationEffect(float angle, float step, float propagationSpeed, float flexySpeed)
	{
		RingRotationEffect ringRotationEffect = new RingRotationEffect();
		ringRotationEffect._progressPos = 0f;
		ringRotationEffect._rotationAngle = angle;
		ringRotationEffect._rotationStep = step;
		ringRotationEffect._rotationPropagationSpeed = propagationSpeed;
		ringRotationEffect._rotationFlexySpeed = flexySpeed;
		_ringRotationEffects.Add(ringRotationEffect);
	}

	public float GetFirstRingRotationAngle()
	{
		TrackLaneRing[] rings = _trackLaneRingsManager.Rings;
		return rings[0].GetRotation();
	}

	public float GetFirstRingDestinationRotationAngle()
	{
		TrackLaneRing[] rings = _trackLaneRingsManager.Rings;
		return rings[0].GetDestinationRotation();
	}
}
