using UnityEngine;

public class CuttableBySaber : MonoBehaviour
{
	public delegate void WasCutBySaberDelegate(Saber saber, Vector3 cutPoint, Quaternion orientation, Vector3 cutDirVec);

	[SerializeField]
	private Collider _collider;

	private bool _canBeCut;

	public bool canBeCut
	{
		get
		{
			return _canBeCut;
		}
		set
		{
			_collider.enabled = value;
			_canBeCut = value;
		}
	}

	public event WasCutBySaberDelegate wasCutBySaberEvent;

	private void Awake()
	{
		_canBeCut = _collider.enabled;
	}

	public void Cut(Saber saber, Vector3 cutPoint, Quaternion orientation, Vector3 cutDirVec)
	{
		if (this.wasCutBySaberEvent != null && _canBeCut)
		{
			this.wasCutBySaberEvent(saber, cutPoint, orientation, cutDirVec);
		}
	}
}
