public class AVProLiveCameraSettingBase
{
	public enum DataType
	{
		Boolean = 0,
		Float = 1,
		Enum = 2
	}

	protected int _deviceIndex;

	protected int _settingIndex;

	protected bool _isAutomatic;

	public DataType DataTypeValue { get; protected set; }

	public int PropertyIndex { get; protected set; }

	public string Name { get; protected set; }

	public bool CanAutomatic { get; protected set; }

	public bool IsAutomatic
	{
		get
		{
			return _isAutomatic;
		}
		set
		{
			if (value != _isAutomatic)
			{
				IsDirty = true;
			}
			_isAutomatic = value;
		}
	}

	public bool IsDirty { get; protected set; }

	public virtual void SetDefault()
	{
	}

	public virtual void Update()
	{
	}
}
