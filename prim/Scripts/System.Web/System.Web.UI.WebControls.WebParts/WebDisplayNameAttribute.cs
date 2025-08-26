namespace System.Web.UI.WebControls.WebParts;

/// <summary>Defines the friendly name for a property of a Web Parts control.</summary>
[AttributeUsage(AttributeTargets.Property)]
public class WebDisplayNameAttribute : Attribute
{
	/// <summary>Represents an instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebDisplayNameAttribute" /> class with the <see cref="P:System.Web.UI.WebControls.WebParts.WebDisplayNameAttribute.DisplayName" /> property set to an empty string ("").</summary>
	public static readonly WebDisplayNameAttribute Default = new WebDisplayNameAttribute();

	private string _displayName;

	/// <summary>Gets the name of a property to display in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> control.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the value to display in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />.</returns>
	public virtual string DisplayName => DisplayNameValue;

	/// <summary>Gets or sets the name to display in the <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> control.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the value to display in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />.</returns>
	protected string DisplayNameValue
	{
		get
		{
			return _displayName;
		}
		set
		{
			_displayName = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebDisplayNameAttribute" /> class without a specified name. </summary>
	public WebDisplayNameAttribute()
		: this(string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebDisplayNameAttribute" /> class with a specified display name.</summary>
	/// <param name="displayName">The friendly name to use in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />.  </param>
	public WebDisplayNameAttribute(string displayName)
	{
		_displayName = displayName;
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
		if (obj is WebDisplayNameAttribute webDisplayNameAttribute)
		{
			return webDisplayNameAttribute.DisplayName == DisplayName;
		}
		return false;
	}

	/// <summary>Returns the hash code for the display name value.</summary>
	/// <returns>A 32-bit signed integer hash code.</returns>
	public override int GetHashCode()
	{
		return DisplayName.GetHashCode();
	}

	/// <summary>Determines whether the current instance is set to the default value.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.WebParts.WebDisplayNameAttribute" /> equals <see cref="F:System.Web.UI.WebControls.WebParts.WebDisplayNameAttribute.Default" />; otherwise, <see langword="false" />.</returns>
	public override bool IsDefaultAttribute()
	{
		return Equals(Default);
	}
}
