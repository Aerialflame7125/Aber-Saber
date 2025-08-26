using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class IncDecSettingsController : SimpleSettingsController
{
	[SerializeField]
	private TextMeshProUGUI _text;

	[SerializeField]
	private Button _decButton;

	[SerializeField]
	private Button _incButton;

	protected bool enableDec
	{
		set
		{
			_decButton.interactable = value;
		}
	}

	protected bool enableInc
	{
		set
		{
			_incButton.interactable = value;
		}
	}

	protected string text
	{
		set
		{
			_text.text = value;
		}
	}

	public override void CancelSettings()
	{
	}

	protected void OnEnable()
	{
		_incButton.onClick.AddListener(IncButtonPressed);
		_decButton.onClick.AddListener(DecButtonPressed);
	}

	private void OnDisable()
	{
		_incButton.onClick.RemoveListener(IncButtonPressed);
		_decButton.onClick.RemoveListener(DecButtonPressed);
	}

	public abstract void IncButtonPressed();

	public abstract void DecButtonPressed();
}
