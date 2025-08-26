using System.Collections.Generic;

public class AveragingValueRecorder
{
	public struct AverageValueData
	{
		public float value { get; private set; }

		public float time { get; private set; }

		public AverageValueData(float value, float time)
		{
			this = default(AverageValueData);
			this.value = value;
			this.time = time;
		}
	}

	private float _averageWindowDuration = 1f;

	private float _historyValuesPerSecond = 1f;

	private int _historyValuesCount = 10;

	private Queue<AverageValueData> _averageWindowValues;

	private Queue<float> _historyValues;

	private float _time;

	private float _historyTime;

	private float _averageValue;

	private float _averageWindowValuesDuration;

	private float _lastValue;

	public AveragingValueRecorder(float averageWindowDuration, float historyWindowDuration, float historyValuesPerSecond)
	{
		_averageWindowDuration = averageWindowDuration;
		_historyValuesPerSecond = historyValuesPerSecond;
		_historyValuesCount = (int)(historyWindowDuration * historyValuesPerSecond);
		_averageWindowValues = new Queue<AverageValueData>(200);
		_historyValues = new Queue<float>((_historyValuesCount <= 0) ? ((int)(historyValuesPerSecond * 300f)) : _historyValuesCount);
	}

	public void Update(float value, float deltaTime)
	{
		_lastValue = value;
		_averageWindowValues.Enqueue(new AverageValueData(value, deltaTime));
		_averageWindowValuesDuration += deltaTime;
		while (_averageWindowValuesDuration > _averageWindowDuration)
		{
			_averageWindowValuesDuration -= _averageWindowValues.Peek().time;
			_averageWindowValues.Dequeue();
		}
		_averageValue = 0f;
		foreach (AverageValueData averageWindowValue in _averageWindowValues)
		{
			_averageValue += averageWindowValue.value * averageWindowValue.time / _averageWindowValuesDuration;
		}
		_time += deltaTime;
		_historyTime += deltaTime;
		if (_historyTime > 1f / _historyValuesPerSecond)
		{
			if (_historyValues.Count == _historyValuesCount && _historyValuesCount > 0)
			{
				_historyValues.Dequeue();
			}
			_historyValues.Enqueue(_averageValue);
			_historyTime = 0f;
		}
	}

	public float GetAverageValue()
	{
		return _averageValue;
	}

	public float GetLastValue()
	{
		return _lastValue;
	}

	public Queue<float> GetHistoryValues()
	{
		return _historyValues;
	}
}
