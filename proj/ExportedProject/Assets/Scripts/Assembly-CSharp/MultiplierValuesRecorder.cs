using System.Collections.Generic;
using UnityEngine;

public class MultiplierValuesRecorder : MonoBehaviour
{
	public struct MultiplierValue
	{
		public int multiplier { get; private set; }

		public float time { get; private set; }

		public MultiplierValue(int multiplier, float time)
		{
			this.multiplier = multiplier;
			this.time = time;
		}
	}

	[SerializeField]
	private ScoreController _scoreController;

	[SerializeField]
	private FloatVariable _songTime;

	private List<MultiplierValue> _multiplierValues = new List<MultiplierValue>(1000);

	public List<MultiplierValue> multiplierValues
	{
		get
		{
			return _multiplierValues;
		}
	}

	private void Start()
	{
		_scoreController.multiplierDidChangeEvent += HandleScoreControllerMultiplierDidChange;
	}

	private void OnDestroy()
	{
		_scoreController.multiplierDidChangeEvent -= HandleScoreControllerMultiplierDidChange;
	}

	private void HandleScoreControllerMultiplierDidChange(int multiplier, float multiplierProgress)
	{
		if (_multiplierValues.Count <= 0 || _multiplierValues[_multiplierValues.Count - 1].multiplier != multiplier)
		{
			MultiplierValue item = new MultiplierValue(multiplier, _songTime.value);
			_multiplierValues.Add(item);
		}
	}
}
