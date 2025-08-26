using UnityEngine;

public class SmokeParams : PersistentScriptableObject
{
	[SerializeField]
	private bool _smokeEnabled;

	public bool smokeEnabled => _smokeEnabled;

	public void Init(bool smokeEnabled)
	{
		_smokeEnabled = smokeEnabled;
	}
}
