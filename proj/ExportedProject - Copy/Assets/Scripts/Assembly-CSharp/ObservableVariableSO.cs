using System;

public class ObservableVariableSO<T> : VariableSO<T>, IObservableChange
{
	public override T value
	{
		get
		{
			return _value;
		}
		set
		{
			if (!_value.Equals(value))
			{
				_value = value;
				if (this.didChangeEvent != null)
				{
					this.didChangeEvent();
				}
			}
		}
	}

	public event Action didChangeEvent;

	public static implicit operator T(ObservableVariableSO<T> obj)
	{
		return obj.value;
	}
}
