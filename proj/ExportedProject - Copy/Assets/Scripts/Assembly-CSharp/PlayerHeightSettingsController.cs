using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class PlayerHeightSettingsController : SimpleSettingsController
{
	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	[SerializeField]
	private TextMeshProUGUI _text;

	private float _playerHeight;

	private float playerHeight
	{
		get
		{
			return _playerHeight;
		}
		set
		{
			_playerHeight = value;
			_text.text = string.Format("{0:N1}m", _playerHeight);
		}
	}

	public override void Init()
	{
		playerHeight = _mainSettingsModel.playerHeight;
	}

	public override void ApplySettings()
	{
		_mainSettingsModel.playerHeight = playerHeight;
	}

	public override void CancelSettings()
	{
	}

	public void AutoSetHeight()
	{
		playerHeight = Mathf.Clamp(InputTracking.GetLocalPosition(XRNode.Head).y + 0.1f, 1.4f, 2f);
	}

	public void ResetHeight()
	{
		playerHeight = 1.8f;
	}
}
