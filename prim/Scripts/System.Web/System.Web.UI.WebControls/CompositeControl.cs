using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Implements the basic functionality required by Web controls that contain child controls.</summary>
[Designer("System.Web.UI.Design.WebControls.CompositeControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class CompositeControl : WebControl, INamingContainer, ICompositeControlDesignerAccessor
{
	/// <summary>Gets a value that indicates whether the control should set the <see langword="disabled" /> attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is <see langword="false" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, <see langword="false" />.</returns>
	public override bool SupportsDisabledAttribute => base.RenderingCompatibilityLessThan40;

	/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> object that represents the child controls in a <see cref="T:System.Web.UI.WebControls.CompositeControl" />.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that represents the child controls in the <see cref="T:System.Web.UI.WebControls.CompositeControl" />.</returns>
	public override ControlCollection Controls
	{
		get
		{
			EnsureChildControls();
			return base.Controls;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> class. </summary>
	protected CompositeControl()
	{
	}

	/// <summary>Binds a data source to the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> and all its child controls.</summary>
	public override void DataBind()
	{
		EnsureChildControls();
		base.DataBind();
	}

	/// <summary>Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		EnsureChildControls();
		base.Render(writer);
	}

	/// <summary>Enables a designer to recreate the composite control's collection of child controls in the design-time environment.</summary>
	void ICompositeControlDesignerAccessor.RecreateChildControls()
	{
		RecreateChildControls();
	}

	/// <summary>Recreates the child controls in a control derived from <see cref="T:System.Web.UI.WebControls.CompositeControl" />. </summary>
	[MonoTODO("not sure exactly what this one does..")]
	protected virtual void RecreateChildControls()
	{
		CreateChildControls();
	}
}
