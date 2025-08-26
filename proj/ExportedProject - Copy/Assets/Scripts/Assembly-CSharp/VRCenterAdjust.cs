using UnityEngine;

public class VRCenterAdjust : MonoBehaviour
{
	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	private void Start()
	{
		if (_mainSettingsModel.roomCenter.magnitude > 5f)
		{
			ResetRoomTransform();
		}
		SetTransformFromSettings();
	}

	public void SetTransformFromSettings()
	{
		base.transform.position = _mainSettingsModel.roomCenter;
		Quaternion rotation = default(Quaternion);
		rotation.eulerAngles = new Vector3(0f, _mainSettingsModel.roomRotation, 0f);
		base.transform.rotation = rotation;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Delete))
		{
			ResetRoomTransform();
		}
	}

	public void Move(Vector3 move, bool updateSettings = true)
	{
		Vector3 position = base.transform.position;
		position += move;
		base.transform.position = position;
		if (updateSettings)
		{
			_mainSettingsModel.roomCenter = position;
		}
	}

	public void Rotate(float angle, bool updateSettings = true)
	{
		base.transform.Rotate(new Vector3(0f, angle, 0f));
		if (updateSettings)
		{
			_mainSettingsModel.roomRotation = base.transform.rotation.eulerAngles.y;
		}
	}

	public void ResetRoomTransform(bool updateSettings = true)
	{
		Vector3 vector = new Vector3(0f, 0f, 0f);
		base.transform.position = vector;
		base.transform.rotation = default(Quaternion);
		if (updateSettings)
		{
			_mainSettingsModel.roomCenter = vector;
			_mainSettingsModel.roomRotation = 0f;
		}
	}
}
