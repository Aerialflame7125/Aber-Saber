using UnityEngine;

public class MovementHistoryRecorder
{
	private AveragingValueRecorder _averagingValueRecorer;

	private float _increaseSpeed;

	private float _decreaseSpeed;

	private float _distance;

	private float _accum;

	public AveragingValueRecorder averagingValueRecorer => _averagingValueRecorer;

	public float distance => _distance;

	public MovementHistoryRecorder(float averageWindowDuration, float historyValuesPerSecond, float increaseSpeed, float decreaseSpeed)
	{
		_averagingValueRecorer = new AveragingValueRecorder(averageWindowDuration, -1f, historyValuesPerSecond);
		_increaseSpeed = increaseSpeed;
		_decreaseSpeed = decreaseSpeed;
	}

	public void AddMovement(float distance)
	{
		_accum += distance * _increaseSpeed / Mathf.Max(1f, _accum);
		_distance += distance;
	}

	public void Update(float deltaTime)
	{
		_accum -= _decreaseSpeed * deltaTime;
		if (_accum < 0f)
		{
			_accum = 0f;
		}
		_averagingValueRecorer.Update(_accum, deltaTime);
	}
}
