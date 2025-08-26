using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private Saber _leftSaber;

	[SerializeField]
	private Saber _rightSaber;

	[SerializeField]
	private GameObject _sabersContainer;

	[SerializeField]
	private Transform _headTransform;

	private bool _overrideHeadPos;

	private Vector3 _overriddenHeadPos;

	private bool _disableSabers;

	private Vector3 _headPos;

	public Saber leftSaber => _leftSaber;

	public Saber rightSaber => _rightSaber;

	public Vector3 headPos => _headPos;

	public bool disableSabers
	{
		get
		{
			return _disableSabers;
		}
		set
		{
			_disableSabers = value;
			_sabersContainer.SetActive(!value);
		}
	}

	public Saber SaberForType(Saber.SaberType saberType)
	{
		if (_leftSaber.saberType == saberType)
		{
			return _leftSaber;
		}
		if (_rightSaber.saberType == saberType)
		{
			return _rightSaber;
		}
		return null;
	}

	public void OverrideHeadPos(Vector3 pos)
	{
		_headPos = pos;
		_overrideHeadPos = true;
	}

	private void Update()
	{
		if (!_overrideHeadPos)
		{
			_headPos = _headTransform.position;
		}
	}

	public float MoveTowardsHead(float start, float end, float t)
	{
		return Mathf.LerpUnclamped(start + headPos.z * Mathf.Min(1f, t * 2f), end + headPos.z, t);
	}

	public void AllowOnlyOneSaber(Saber.SaberType saberType)
	{
		if (_leftSaber.saberType == saberType)
		{
			_rightSaber.gameObject.SetActive(value: false);
		}
		else
		{
			_leftSaber.gameObject.SetActive(value: false);
		}
	}
}
