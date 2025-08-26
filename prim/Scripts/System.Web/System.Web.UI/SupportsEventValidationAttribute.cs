using System.Collections;

namespace System.Web.UI;

/// <summary>Defines the metadata attribute that Web server controls use to indicate support for event validation. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class SupportsEventValidationAttribute : Attribute
{
	private static Hashtable _typesSupportsEventValidation;

	static SupportsEventValidationAttribute()
	{
		_typesSupportsEventValidation = Hashtable.Synchronized(new Hashtable());
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.SupportsEventValidationAttribute" /> class.</summary>
	public SupportsEventValidationAttribute()
	{
	}

	internal static bool SupportsEventValidation(Type type)
	{
		object obj = _typesSupportsEventValidation[type];
		if (obj != null)
		{
			return (bool)obj;
		}
		object[] customAttributes = type.GetCustomAttributes(typeof(SupportsEventValidationAttribute), inherit: false);
		bool flag = customAttributes != null && customAttributes.Length != 0;
		_typesSupportsEventValidation[type] = flag;
		return flag;
	}
}
