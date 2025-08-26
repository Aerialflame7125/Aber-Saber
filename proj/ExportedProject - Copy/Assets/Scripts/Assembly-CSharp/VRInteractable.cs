using UnityEngine;

public class VRInteractable : MonoBehaviour
{
	[SerializeField]
	private bool _interactable = true;

	public bool interactable
	{
		get
		{
			return _interactable;
		}
	}
}
