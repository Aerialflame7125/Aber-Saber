public abstract class SwitchSettingsController : IncDecSettingsController
{
	private bool _on;

	protected abstract bool GetInitValue();

	protected abstract void ApplyValue(bool value);

	protected abstract string TextForValue(bool value);

	public override void Init()
	{
		_on = GetInitValue();
		RefreshUI();
	}

	public override void ApplySettings()
	{
		ApplyValue(_on);
	}

	private void RefreshUI()
	{
		base.text = TextForValue(_on);
		base.enableDec = _on;
		base.enableInc = !_on;
	}

	public override void IncButtonPressed()
	{
		_on = true;
		RefreshUI();
	}

	public override void DecButtonPressed()
	{
		_on = false;
		RefreshUI();
	}
}
