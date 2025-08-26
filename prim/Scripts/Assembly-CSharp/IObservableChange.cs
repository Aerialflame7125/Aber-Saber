using System;

public interface IObservableChange
{
	event Action didChangeEvent;
}
