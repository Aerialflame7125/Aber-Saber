namespace System.Web.UI;

/// <summary>Represents the definition of the control property and its value.</summary>
public class SimplePropertyEntry : PropertyEntry
{
	private bool useSetAttribute;

	private object val;

	/// <summary>Gets or sets a value indicating whether the <see cref="M:System.Web.UI.IAttributeAccessor.SetAttribute(System.String,System.String)" /> method should be called for the property during code creation.</summary>
	/// <returns>
	///     <see langword="true" /> if <see cref="M:System.Web.UI.IAttributeAccessor.SetAttribute(System.String,System.String)" /> should be called; otherwise, <see langword="false" />.</returns>
	public bool UseSetAttribute
	{
		get
		{
			return useSetAttribute;
		}
		set
		{
			useSetAttribute = value;
		}
	}

	/// <summary>Gets or sets the value of the property entry.</summary>
	/// <returns>An <see cref="T:System.Object" /> containing the value of the property entry.</returns>
	public object Value
	{
		get
		{
			return val;
		}
		set
		{
			val = value;
		}
	}

	internal SimplePropertyEntry()
	{
	}
}
