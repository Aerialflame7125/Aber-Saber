using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Created when a user control (.ascx file) is specified for output caching, using either the  page directive or the <see cref="T:System.Web.UI.PartialCachingAttribute" /> attribute, and the user control is inserted into a page's control hierarchy by dynamically loading the user control with the <see cref="M:System.Web.UI.TemplateControl.LoadControl(System.String)" /> method.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class PartialCachingControl : BasePartialCachingControl
{
	private Type type;

	private object[] parameters;

	private Control control;

	/// <summary>Gets a reference to the user control that is cached.</summary>
	/// <returns>The user control that is cached.</returns>
	public Control CachedControl => control;

	internal PartialCachingControl(Type type, object[] parameters)
	{
		this.type = type;
		this.parameters = parameters;
	}

	internal override Control CreateControl()
	{
		control = (Control)Activator.CreateInstance(type, parameters);
		if (control is UserControl)
		{
			((UserControl)control).InitializeAsUserControl(Page);
		}
		return control;
	}
}
