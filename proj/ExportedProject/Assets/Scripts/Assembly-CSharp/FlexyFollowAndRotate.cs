using UnityEngine;

public class FlexyFollowAndRotate : MonoBehaviour
{
	[SerializeField]
	private Transform _target;

	[SerializeField]
	private float _smooth = 4f;

	private void Update()
	{
		Vector3 position = base.transform.position;
		Quaternion rotation = base.transform.rotation;
		base.transform.position = Vector3.Slerp(position, _target.position, Time.deltaTime * _smooth);
		base.transform.rotation = Quaternion.Slerp(rotation, _target.rotation, Time.deltaTime * _smooth);
	}
}
