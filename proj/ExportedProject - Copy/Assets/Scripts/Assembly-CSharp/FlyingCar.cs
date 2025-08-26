using UnityEngine;

public class FlyingCar : MonoBehaviour
{
	[SerializeField]
	private float _startZ = -30f;

	[SerializeField]
	private float _endZ = 100f;

	[SerializeField]
	private float _speed = 1f;

	private float _progress;

	private Vector3 _pos;

	public static bool startflyingCars;

	private void Start()
	{
		_pos = base.transform.localPosition;
		_progress = (_pos.z - _startZ) / Mathf.Abs(_endZ - _startZ);
		UpdatePos();
		if (!startflyingCars)
		{
			base.gameObject.SetActive(false);
		}
	}

	private void Update()
	{
		_progress += Time.deltaTime * _speed / Mathf.Abs(_endZ - _startZ);
		if (_progress > 1f)
		{
			_progress = 0f - Random.value;
		}
		UpdatePos();
	}

	private void UpdatePos()
	{
		_pos.z = Mathf.LerpUnclamped(_startZ, _endZ, _progress);
		base.transform.localPosition = _pos;
	}
}
