using UnityEngine;

public class AVProLiveCameraSettingFloat : AVProLiveCameraSettingBase
{
	private float _currentValue;

	public float CurrentValue
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

	public float DefaultValue { get; private set; }

	public float MinValue { get; private set; }

	public float MaxValue { get; private set; }

	public float CurrentValueNormalised
	{
		get
		{
			return (_currentValue - MinValue) / (MaxValue - MinValue);
		}
		set
		{
			CurrentValue = Mathf.Lerp(MinValue, MaxValue, value);
		}
	}

	public AVProLiveCameraSettingFloat(int deviceIndex, int settingIndex, int propertyIndex, string name, bool canAutomatic, bool isAutomatic, float defaultValue, float currentValue, float minValue, float maxValue)
	{
		_deviceIndex = deviceIndex;
		_settingIndex = settingIndex;
		base.DataTypeValue = DataType.Float;
		base.PropertyIndex = propertyIndex;
		base.Name = name;
		base.CanAutomatic = canAutomatic;
		base.IsAutomatic = isAutomatic;
		MinValue = minValue;
		MaxValue = maxValue;
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
		if (base.IsDirty)
		{
			AVProLiveCameraPlugin.ApplyDeviceVideoSettingValue(_deviceIndex, _settingIndex, _currentValue, _isAutomatic);
			base.IsDirty = false;
		}
		else
		{
			AVProLiveCameraPlugin.UpdateDeviceVideoSettingValue(_deviceIndex, _settingIndex, out _currentValue, out _isAutomatic);
		}
	}
}
