public class AVProLiveCameraSettingBoolean : AVProLiveCameraSettingBase
{
	private bool _currentValue;

	public bool CurrentValue
	{
		get
		{
			return _currentValue;
		}
		set
		{
			if (!_isAutomatic)
			{
				if (value != _currentValue)
				{
					base.IsDirty = true;
				}
				_currentValue = value;
			}
		}
	}

	public bool DefaultValue { get; private set; }

	public AVProLiveCameraSettingBoolean(int deviceIndex, int settingIndex, int propertyIndex, string name, bool canAutomatic, bool isAutomatic, bool defaultValue, bool currentValue)
	{
		_deviceIndex = deviceIndex;
		_settingIndex = settingIndex;
		base.DataTypeValue = DataType.Boolean;
		base.PropertyIndex = propertyIndex;
		base.Name = name;
		base.CanAutomatic = canAutomatic;
		base.IsAutomatic = isAutomatic;
		DefaultValue = defaultValue;
		CurrentValue = currentValue;
		base.IsDirty = false;
	}

	public override void SetDefault()
	{
		CurrentValue = DefaultValue;
	}

	public override void Update()
	{
		float currentValue = ((!_currentValue) ? 0f : 1f);
		if (base.IsDirty)
		{
			AVProLiveCameraPlugin.ApplyDeviceVideoSettingValue(_deviceIndex, _settingIndex, currentValue, _isAutomatic);
			base.IsDirty = false;
		}
		else
		{
			AVProLiveCameraPlugin.UpdateDeviceVideoSettingValue(_deviceIndex, _settingIndex, out currentValue, out _isAutomatic);
			_currentValue = currentValue > 0f;
		}
	}
}
