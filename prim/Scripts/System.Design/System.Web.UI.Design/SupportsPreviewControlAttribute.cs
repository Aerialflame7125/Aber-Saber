namespace System.Web.UI.Design;

/// <summary>Indicates whether a control designer requires a preview instance of the control at design time. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class SupportsPreviewControlAttribute : Attribute
{
	private bool is_default;

	private bool supports_preview;

	/// <summary>Gets an instance of the <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" /> class that is set to the default preview value. This field is read-only.</summary>
	public static readonly SupportsPreviewControlAttribute Default = new SupportsPreviewControlAttribute(supportsPreviewControl: false, isDefault: true);

	/// <summary>Gets a value indicating whether the control designer requires a temporary preview control at design time.</summary>
	/// <returns>
	///   <see langword="true" /> if the designer uses a temporary copy of the associated control for design-time preview; <see langword="false" /> if the designer uses an instance of the <see cref="P:System.ComponentModel.Design.ComponentDesigner.Component" /> control that is contained in the designer.</returns>
	public bool SupportsPreviewControl => supports_preview;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" /> class and sets the initial value of the <see cref="P:System.Web.UI.Design.SupportsPreviewControlAttribute.SupportsPreviewControl" /> property.</summary>
	/// <param name="supportsPreviewControl">The initial value to assign for <see cref="P:System.Web.UI.Design.SupportsPreviewControlAttribute.SupportsPreviewControl" />.</param>
	public SupportsPreviewControlAttribute(bool supportsPreviewControl)
		: this(supportsPreviewControl, isDefault: false)
	{
	}

	private SupportsPreviewControlAttribute(bool supportsPreviewControl, bool isDefault)
	{
		supports_preview = supportsPreviewControl;
		is_default = isDefault;
	}

	/// <summary>Determines whether the specified object represents the same preview attribute setting as the current instance of the <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" /> class.</summary>
	/// <param name="obj">The object to compare with the current instance of <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" />.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" /> attribute and its value is the same as this instance of <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" />; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj is SupportsPreviewControlAttribute supportsPreviewControlAttribute)
		{
			return supportsPreviewControlAttribute.supports_preview == supports_preview;
		}
		return false;
	}

	/// <summary>Returns the hash code for this instance of the <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" /> class.</summary>
	/// <returns>A 32-bit signed integer hash code for the current instance of <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" />.</returns>
	public override int GetHashCode()
	{
		if (!supports_preview)
		{
			return 0;
		}
		return 1;
	}

	/// <summary>Indicates whether the current instance of the <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" /> class is set to the default preview attribute value.</summary>
	/// <returns>
	///   <see langword="true" /> if the current instance of <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" /> is equal to the default preview attribute value; otherwise, <see langword="false" />.</returns>
	public override bool IsDefaultAttribute()
	{
		return is_default;
	}
}
