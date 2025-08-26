using UnityEngine;

public class RecycleOnInvisible : MonoBehaviour
{
	public GameObject _gameObject;

	private void OnBecameInvisible()
	{
		_gameObject.Recycle();
	}
}
