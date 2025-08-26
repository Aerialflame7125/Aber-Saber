namespace System.Web.UI.WebControls.WebParts;

/// <summary>Indicates whether the designated property of a Web Parts control is displayed in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> object.</summary>
/// <exception cref="T:System.Web.AspNetHostingPermission">for operating in a hosted environment. Demand value: <see cref="F:System.Security.Permissions.SecurityAction.LinkDemand" />; Permission value: <see cref="F:System.Web.AspNetHostingPermissionLevel.Minimal" />.</exception>
[AttributeUsage(AttributeTargets.Property)]
public sealed class WebBrowsableAttribute : Attribute
{
	/// <summary>Represents an instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute" /> class with the <see cref="P:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute.Browsable" /> property set to <see langword="true" />.</summary>
	public static readonly WebBrowsableAttribute Yes = new WebBrowsableAttribute(browsable: true);

	/// <summary>Represents an instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute" /> class with the <see cref="P:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute.Browsable" /> property set to <see langword="false" />.</summary>
	public static readonly WebBrowsableAttribute No = new WebBrowsableAttribute(browsable: false);

	/// <summary>Represents an instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute" /> class with the <see cref="P:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute.Browsable" /> property set to the default value, which is <see langword="false" />.</summary>
	public static readonly WebBrowsableAttribute Default = No;

	private bool _browsable;

	/// <summary>Gets a value indicating whether a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> control should display a specific property of a Web Parts control.</summary>
	/// <returns>
	///     <see langword="true" /> if <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> will display the property; otherwise, <see langword="false" />.</returns>
	public bool Browsable => _browsable;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute" /> class with the <see cref="P:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute.Browsable" /> property set to <see langword="true" />.</summary>
	public WebBrowsableAttribute()
		: this(browsable: true)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute" /> class with the specified value for the <see cref="P:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute.Browsable" /> property.</summary>
	/// <param name="browsable">A Boolean value indicating whether the property should be displayed in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />. </param>
	public WebBrowsableAttribute(bool browsable)
	{
		_browsable = browsable;
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
		if (obj is WebBrowsableAttribute webBrowsableAttribute)
		{
			return webBrowsableAttribute.Browsable == Browsable;
		}
		return false;
	}

	/// <summary>Returns the hash code for the display name value.</summary>
	/// <returns>A 32-bit signed integer hash code.</returns>
	public override int GetHashCode()
	{
		return _browsable.GetHashCode();
	}

	/// <summary>Determines whether the current instance is set to the default value.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute" /> equals <see cref="F:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute.Default" />; otherwise, <see langword="false" />.</returns>
	public override bool IsDefaultAttribute()
	{
		return Equals(Default);
	}
}
