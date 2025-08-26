using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>An <see cref="T:System.Web.UI.ITemplate" /> implementation that is called from the generated page class code. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class CompiledTemplateBuilder : ITemplate
{
	private BuildTemplateMethod templateMethod;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.CompiledTemplateBuilder" /> class.</summary>
	/// <param name="buildTemplateMethod">A delegate used to handle the <see cref="M:System.Web.UI.CompiledTemplateBuilder.InstantiateIn(System.Web.UI.Control)" /> method call.</param>
	public CompiledTemplateBuilder(BuildTemplateMethod buildTemplateMethod)
	{
		templateMethod = buildTemplateMethod;
	}

	/// <summary>Populates the <see cref="T:System.Web.UI.Control" /> object with the child controls contained in the template.</summary>
	/// <param name="container">A <see cref="T:System.Web.UI.Control" /> that represents the container used to store the child controls in the template.</param>
	public void InstantiateIn(Control container)
	{
		templateMethod(container);
	}
}
