using UnityEngine;

public class FollowLocalRotation : MonoBehaviour
{
	public Transform _target;

	private Transform _transform;

	private void Awake()
	{
		_transform = base.transform;
	}

	private void Update()
	{
		_transform.localRotation = _target.localRotation;
	}
}
