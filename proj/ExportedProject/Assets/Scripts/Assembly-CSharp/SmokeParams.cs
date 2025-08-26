using UnityEngine;

public class SmokeParams : PersistentScriptableObject
{
	[SerializeField]
	private bool _smokeEnabled;

	public bool smokeEnabled
	{
		get
		{
			return _smokeEnabled;
		}
	}

	public void Init(bool smokeEnabled)
	{
		_smokeEnabled = smokeEnabled;
	}
}
