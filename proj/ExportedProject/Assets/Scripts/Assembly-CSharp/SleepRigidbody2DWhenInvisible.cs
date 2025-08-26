using UnityEngine;

public class SleepRigidbody2DWhenInvisible : MonoBehaviour
{
	public Rigidbody2D _rigidbody2D;

	private void Awake()
	{
	}

	private void OnBecameInvisible()
	{
		_rigidbody2D.Sleep();
	}
}
