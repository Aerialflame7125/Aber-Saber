public abstract class ListSettingsController : IncDecSettingsController
{
	private int _idx;

	private int _numberOfElements;

	protected abstract void GetInitValues(out int idx, out int numberOfElements);

	protected abstract void ApplyValue(int idx);

	protected abstract string TextForValue(int idx);

	public override void Init()
	{
		GetInitValues(out _idx, out _numberOfElements);
		RefreshUI();
	}

	public override void ApplySettings()
	{
		ApplyValue(_idx);
	}

	private void RefreshUI()
	{
		base.text = TextForValue(_idx);
		base.enableDec = _idx > 0;
		base.enableInc = _idx < _numberOfElements - 1;
	}

	public override void IncButtonPressed()
	{
		if (_idx < _numberOfElements - 1)
		{
			_idx++;
		}
		RefreshUI();
	}

	public override void DecButtonPressed()
	{
		if (_idx > 0)
		{
			_idx--;
		}
		RefreshUI();
	}
}
