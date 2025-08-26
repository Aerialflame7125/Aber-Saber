using UnityEngine;

public class AnchorIntoParent : MonoBehaviour
{
	[SerializeField]
	private Transform _parentTransform;

	[SerializeField]
	private Vector3 _positionOffset;

	private void Start()
	{
		base.transform.parent = _parentTransform;
		base.transform.localPosition = _positionOffset;
		base.transform.localRotation = Quaternion.identity;
		base.transform.localScale = Vector3.one;
	}
}
