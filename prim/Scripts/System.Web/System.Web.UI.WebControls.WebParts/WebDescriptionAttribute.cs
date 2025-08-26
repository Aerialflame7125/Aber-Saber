namespace System.Web.UI.WebControls.WebParts;

/// <summary>Defines the string value to use as a ToolTip for a property of a Web Parts control.</summary>
[AttributeUsage(AttributeTargets.Property)]
public class WebDescriptionAttribute : Attribute
{
	/// <summary>Represents an instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebDescriptionAttribute" /> class with the <see cref="P:System.Web.UI.WebControls.WebParts.WebDescriptionAttribute.Description" /> property set to an empty string ("").</summary>
	public static readonly WebDescriptionAttribute Default = new WebDescriptionAttribute();

	private string _description;

	/// <summary>Gets the ToolTip for a property to display in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> control.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the value to display in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />.</returns>
	public virtual string Description => DescriptionValue;

	/// <summary>Gets or sets the ToolTip to display in the <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> control.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the value to display in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />.</returns>
	protected string DescriptionValue
	{
		get
		{
			return _description;
		}
		set
		{
			_description = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebDescriptionAttribute" /> class. </summary>
	public WebDescriptionAttribute()
		: this(string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebDescriptionAttribute" /> class with the specified description.</summary>
	/// <param name="description">The ToolTip to use in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />. </param>
	public WebDescriptionAttribute(string description)
	{
		_description = description;
	}

	/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
	/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance, or <see langword="null" />. </param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj == this)
		{
			return true;
		}
		if (obj is WebDescriptionAttribute webDescriptionAttribute)
		{
			return webDescriptionAttribute.Description == Description;
		}
		return false;
	}

	/// <summary>Returns the hash code for the display name value.</summary>
	/// <returns>A 32-bit signed integer hash code.</returns>
	public override int GetHashCode()
	{
		return Description.GetHashCode();
	}

	/// <summary>Determines whether the current instance is set to the default value.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.WebParts.WebDescriptionAttribute" /> equals <see cref="F:System.Web.UI.WebControls.WebParts.WebDescriptionAttribute.Default" />; otherwise, <see langword="false" />.</returns>
	public override bool IsDefaultAttribute()
	{
		return Equals(Default);
	}
}
