using System.ComponentModel;

namespace System.Web;

[AttributeUsage(AttributeTargets.All)]
internal sealed class WebSysDefaultValueAttribute : DefaultValueAttribute
{
	private Type _type;

	private bool _localized;

	public override object TypeId => typeof(DefaultValueAttribute);

	public override object Value
	{
		get
		{
			if (!_localized)
			{
				_localized = true;
				string name = (string)base.Value;
				if (!string.IsNullOrEmpty(name))
				{
					object obj = global::SR.GetString(name);
					if (_type != null)
					{
						try
						{
							obj = TypeDescriptor.GetConverter(_type).ConvertFromInvariantString((string)obj);
						}
						catch (NotSupportedException)
						{
							obj = null;
						}
					}
					SetValue(obj);
				}
			}
			return base.Value;
		}
	}

	internal WebSysDefaultValueAttribute(Type type, string value)
		: base(value)
	{
		_type = type;
	}

	internal WebSysDefaultValueAttribute(string value)
		: base(value)
	{
	}
}
