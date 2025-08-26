using UnityEngine;

public class SaberTypeObject : MonoBehaviour
{
	[SerializeField]
	private Saber.SaberType _saberType;

	public Saber.SaberType saberType
	{
		get
		{
			return _saberType;
		}
	}
}
