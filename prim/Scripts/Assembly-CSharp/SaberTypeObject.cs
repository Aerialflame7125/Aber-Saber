using UnityEngine;

public class SaberTypeObject : MonoBehaviour
{
	[SerializeField]
	private Saber.SaberType _saberType;

	public Saber.SaberType saberType => _saberType;
}
