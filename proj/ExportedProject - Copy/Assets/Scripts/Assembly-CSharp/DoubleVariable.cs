using UnityEngine;

public abstract class DoubleVariable : ScriptableObject
{
	[SerializeField]
	private double _defaultValue;

	protected double _value;

	public double value
	{
		get
		{
			return _value;
		}
	}

	private void OnEnable()
	{
		_value = _defaultValue;
	}
}
