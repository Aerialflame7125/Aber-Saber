using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
	public GameObject _prefab;

	private void Awake()
	{
		GameObject gameObject = Object.Instantiate(_prefab);
		Transform transform = gameObject.transform;
		Transform transform2 = base.transform;
		transform.parent = transform2.parent;
		transform.localPosition = transform2.localPosition;
		transform.localRotation = transform2.localRotation;
		transform.localScale = transform2.localScale;
	}
}
