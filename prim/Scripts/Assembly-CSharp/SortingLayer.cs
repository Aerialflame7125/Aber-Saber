using UnityEngine;

public class SortingLayer : MonoBehaviour
{
	[SerializeField]
	private Renderer _renderer;

	public Renderer renderer => _renderer;

	private void Reset()
	{
		_renderer = GetComponent<Renderer>();
	}
}
