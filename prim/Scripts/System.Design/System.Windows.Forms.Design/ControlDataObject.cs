namespace System.Windows.Forms.Design;

internal class ControlDataObject : IDataObject
{
	private object _data;

	private string _format;

	public ControlDataObject()
	{
		_data = null;
		_format = null;
	}

	public ControlDataObject(Control control)
	{
		SetData(control);
	}

	public ControlDataObject(Control[] controls)
	{
		SetData(controls);
	}

	public object GetData(Type format)
	{
		return GetData(format.ToString());
	}

	public object GetData(string format)
	{
		return GetData(format, autoConvert: true);
	}

	public object GetData(string format, bool autoConvert)
	{
		if (format == _format)
		{
			return _data;
		}
		return null;
	}

	public bool GetDataPresent(Type format)
	{
		return GetDataPresent(format.ToString());
	}

	public bool GetDataPresent(string format)
	{
		return GetDataPresent(format, autoConvert: true);
	}

	public bool GetDataPresent(string format, bool autoConvert)
	{
		if (format == _format)
		{
			return true;
		}
		return false;
	}

	public string[] GetFormats()
	{
		return GetFormats(autoConvert: true);
	}

	public string[] GetFormats(bool autoConvert)
	{
		return new string[2]
		{
			typeof(Control).ToString(),
			typeof(Control[]).ToString()
		};
	}

	public void SetData(object data)
	{
		if (data is Control)
		{
			SetData(typeof(Control), data);
		}
		else if (data is Control[])
		{
			SetData(typeof(Control[]), data);
		}
	}

	public void SetData(Type format, object data)
	{
		SetData(format.ToString(), data);
	}

	public void SetData(string format, object data)
	{
		SetData(format, autoConvert: true, data);
	}

	public void SetData(string format, bool autoConvert, object data)
	{
		if (ValidateFormat(format))
		{
			_data = data;
			_format = format;
		}
	}

	private bool ValidateFormat(string format)
	{
		bool result = false;
		string[] formats = GetFormats();
		for (int i = 0; i < formats.Length; i++)
		{
			if (formats[i] == format)
			{
				result = true;
				break;
			}
		}
		return result;
	}
}
