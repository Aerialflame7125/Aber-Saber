using UnityEngine;

public class VariableSO<T> : ScriptableObject
{
	protected T _value;

	public virtual T value
	{
		get
		{
			return _value;
		}
		set
		{
			_value = value;
		}
	}
}
