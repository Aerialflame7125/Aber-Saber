using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Converts a value that represents the location (target) in which to display the content resulting from a Web navigation to a string. This class also converts a string to a target value.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class TargetConverter : StringConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TargetConverter" /> class. </summary>
	public TargetConverter()
	{
	}

	/// <summary>Returns a collection of standard values from the default context for the data type this type converter is designed for.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> containing a standard set of valid values, or <see langword="null" /> if the data type does not support a standard set of values.</returns>
	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		return new StandardValuesCollection(new string[5] { "_blank", "_parent", "_search", "_self", "_top" });
	}

	/// <summary>Returns a value that indicates whether the collection of standard values returned from the <see cref="M:System.Web.UI.WebControls.TargetConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> method is an exclusive list.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.Web.UI.WebControls.TargetConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> is an exhaustive list of possible values; <see langword="false" /> if other values are possible.</returns>
	public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
	{
		return false;
	}

	/// <summary>Returns a value that indicates whether this object supports a standard set of values that can be picked from a list, using the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <returns>
	///     <see langword="true" /> if <see cref="M:System.Web.UI.WebControls.TargetConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> should be called to find a common set of values the object supports; otherwise, <see langword="false" />.</returns>
	public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
	{
		return true;
	}
}
