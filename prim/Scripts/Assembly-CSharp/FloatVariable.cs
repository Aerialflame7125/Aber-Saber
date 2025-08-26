using UnityEngine;

public abstract class FloatVariable : ScriptableObject
{
	[SerializeField]
	private float _defaultValue;

	protected float _value;

	public float value => _value;

	private void OnEnable()
	{
		_value = _defaultValue;
	}
}
