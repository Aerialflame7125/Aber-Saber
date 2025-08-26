using System;
using UnityEngine;

public class RandomObjectPicker<T>
{
	private T[] _objects;

	private float _lastPickTime = -1000f;

	private float _minimumPickInterval;

	public RandomObjectPicker(T obj, float minimumPickInterval)
	{
		_objects = new T[1];
		_objects[0] = obj;
		_minimumPickInterval = minimumPickInterval;
	}

	public RandomObjectPicker(T[] objects, float minimumPickInterval)
	{
		_objects = new T[objects.Length];
		Array.Copy(objects, _objects, objects.Length);
		_minimumPickInterval = minimumPickInterval;
	}

	public T PickRandomObject()
	{
		float timeSinceLevelLoad = Time.timeSinceLevelLoad;
		if (timeSinceLevelLoad - _lastPickTime < _minimumPickInterval)
		{
			return default(T);
		}
		_lastPickTime = timeSinceLevelLoad;
		if (_objects.Length == 1)
		{
			return _objects[0];
		}
		int num = UnityEngine.Random.Range(0, _objects.Length - 1);
		T val = _objects[num];
		_objects[num] = _objects[_objects.Length - 1];
		_objects[_objects.Length - 1] = val;
		return val;
	}
}
