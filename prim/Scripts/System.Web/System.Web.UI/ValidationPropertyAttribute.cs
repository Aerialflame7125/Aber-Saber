using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Defines the metadata attribute that ASP.NET server controls use to identify a validation property. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class ValidationPropertyAttribute : Attribute
{
	private string name;

	/// <summary>Gets the name of the ASP.NET server control's validation property.</summary>
	/// <returns>The name of the validation property.</returns>
	public string Name => name;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ValidationPropertyAttribute" /> class.</summary>
	/// <param name="name">The name of the validation property. </param>
	public ValidationPropertyAttribute(string name)
	{
		this.name = name;
	}
}
