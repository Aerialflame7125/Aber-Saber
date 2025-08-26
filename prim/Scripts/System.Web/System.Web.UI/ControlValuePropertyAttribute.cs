namespace System.Web.UI;

/// <summary>Specifies the default property of a control that a <see cref="T:System.Web.UI.WebControls.ControlParameter" /> object binds to at run time. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class ControlValuePropertyAttribute : Attribute
{
	private string propertyName;

	private object propertyValue;

	private Type propertyType;

	/// <summary>Gets the default property for a control.</summary>
	/// <returns>The default property for a control.</returns>
	public string Name => propertyName;

	/// <summary>Gets the default value for the default property of a control.</summary>
	/// <returns>The default value for the default property of a control.</returns>
	public object DefaultValue => propertyValue;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ControlValuePropertyAttribute" /> class using the specified property name.</summary>
	/// <param name="name">The default property for the control.</param>
	public ControlValuePropertyAttribute(string name)
	{
		propertyName = name;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ControlValuePropertyAttribute" /> class using the specified property name and default value.</summary>
	/// <param name="name">The default property for the control.</param>
	/// <param name="defaultValue">The default value for the default property.</param>
	public ControlValuePropertyAttribute(string name, object defaultValue)
	{
		propertyName = name;
		propertyValue = defaultValue;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ControlValuePropertyAttribute" /> class using the specified property name and default value. The default value is also converted to the specified data type.</summary>
	/// <param name="name">The default property for the control.</param>
	/// <param name="type">The <see cref="T:System.Type" /> to which the default value is converted.</param>
	/// <param name="defaultValue">The default value for the default property.</param>
	public ControlValuePropertyAttribute(string name, Type type, string defaultValue)
	{
		propertyName = name;
		propertyValue = defaultValue;
		propertyType = type;
	}

	/// <summary>Determines whether the current instance of the <see cref="T:System.Web.UI.ControlValuePropertyAttribute" /> object is equal to the specified object.</summary>
	/// <param name="obj">The <see cref="T:System.Object" /> to compare with this instance.</param>
	/// <returns>
	///     <see langword="true" /> if the object contained in the <paramref name="obj" /> parameter is equal to the current instance of <see cref="T:System.Web.UI.ControlValuePropertyAttribute" />; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj != null && obj is ControlValuePropertyAttribute)
		{
			ControlValuePropertyAttribute controlValuePropertyAttribute = (ControlValuePropertyAttribute)obj;
			if (propertyName == controlValuePropertyAttribute.propertyName && propertyValue == controlValuePropertyAttribute.propertyValue)
			{
				return propertyType == controlValuePropertyAttribute.propertyType;
			}
			return false;
		}
		return false;
	}

	/// <summary>Returns the hash code for this instance.</summary>
	/// <returns>A 32-bit signed integer hash code.</returns>
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
}
