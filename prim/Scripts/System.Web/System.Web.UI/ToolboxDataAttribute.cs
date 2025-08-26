using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Specifies the default tag generated for a custom control when it is dragged from a toolbox in a tool such as Microsoft Visual Studio.</summary>
[AttributeUsage(AttributeTargets.Class)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class ToolboxDataAttribute : Attribute
{
	/// <summary>Represents the default <see cref="T:System.Web.UI.ToolboxDataAttribute" /> value for a custom control.</summary>
	public static readonly ToolboxDataAttribute Default = new ToolboxDataAttribute(string.Empty);

	private string data;

	/// <summary>Gets the string representing the initial values of the control's property, which is used in a visual designer for creating an instance of the control.</summary>
	/// <returns>A string representing the initial values for this attribute.</returns>
	public string Data => data;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ToolboxDataAttribute" /> class. </summary>
	/// <param name="data">The string to be set as the <see cref="P:System.Web.UI.ToolboxDataAttribute.Data" />.</param>
	public ToolboxDataAttribute(string data)
	{
		this.data = data;
	}

	/// <summary>Tests whether the <see cref="T:System.Web.UI.ToolboxDataAttribute" /> object is equal to the given object.</summary>
	/// <param name="obj">The object to compare to.</param>
	/// <returns>
	///     <see langword="true" />, if the <see cref="T:System.Web.UI.ToolboxDataAttribute" /> object is equal to the given object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (!(obj is ToolboxDataAttribute toolboxDataAttribute))
		{
			return false;
		}
		return toolboxDataAttribute.Data == data;
	}

	/// <summary>Returns the hash code of the custom control.</summary>
	/// <returns>A 32-bit signed <see langword="integer" /> representing the hash code.</returns>
	public override int GetHashCode()
	{
		if (data == null)
		{
			return -1;
		}
		return data.GetHashCode();
	}

	/// <summary>Tests whether the <see cref="T:System.Web.UI.ToolboxDataAttribute" /> object contains the default value for the <see cref="P:System.Web.UI.ToolboxDataAttribute.Data" /> property.</summary>
	/// <returns>
	///     <see langword="true" />, if the <see cref="T:System.Web.UI.ToolboxDataAttribute" /> contains the default value for the <see cref="P:System.Web.UI.ToolboxDataAttribute.Data" /> property; otherwise, <see langword="false" />.</returns>
	public override bool IsDefaultAttribute()
	{
		if (data != null)
		{
			return data.Length == 0;
		}
		return true;
	}
}
