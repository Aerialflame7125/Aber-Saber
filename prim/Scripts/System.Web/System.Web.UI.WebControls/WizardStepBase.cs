using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Implements the basic functionality required by a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
[ControlBuilder(typeof(WizardStepControlBuilder))]
[Bindable(false)]
[ToolboxItem("")]
public abstract class WizardStepBase : View
{
	private Wizard wizard;

	/// <summary>Gets or sets a value indicating whether the user is allowed to return to the current step from a subsequent step in a <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection. </summary>
	/// <returns>
	///     <see langword="true" /> if the user is allowed to return to the current step; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[Themeable(false)]
	[Filterable(false)]
	public virtual bool AllowReturn
	{
		get
		{
			object obj = ViewState["AllowReturn"];
			if (obj == null)
			{
				return true;
			}
			return (bool)obj;
		}
		set
		{
			ViewState["AllowReturn"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
	/// <returns>
	///     <see langword="true" /> to use themes; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[Browsable(true)]
	public override bool EnableTheming
	{
		get
		{
			return base.EnableTheming;
		}
		set
		{
			base.EnableTheming = value;
		}
	}

	/// <summary>Gets or sets the programmatic identifier assigned to the server control.</summary>
	/// <returns>The programmatic identifier assigned to the control.</returns>
	/// <exception cref="T:System.ArgumentException">The property was set to an invalid identifier string at design time.-or-The property was set to the same identifier as the containing <see cref="P:System.Web.UI.WebControls.WizardStepBase.Wizard" /> control at design time.-or- The property was set to the same identifier as another step in the containing <see cref="P:System.Web.UI.WebControls.WizardStepBase.Wizard" /> control at design time.</exception>
	public override string ID
	{
		get
		{
			return base.ID;
		}
		set
		{
			base.ID = value;
		}
	}

	/// <summary>Gets the name associated with a step in a control that acts as a wizard.</summary>
	/// <returns>The name associated with a step in a control that acts as a wizard.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public virtual string Name
	{
		get
		{
			if (Title != null && Title.Length > 0)
			{
				return Title;
			}
			if (ID != null && ID.Length > 0)
			{
				return ID;
			}
			return null;
		}
	}

	/// <summary>Gets or sets the type of navigation user interface (UI) to display for a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WizardStepType" /> enumeration values. The default value is <see langword="WizardStepType.Auto" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.WizardStepType" /> enumeration values.</exception>
	[DefaultValue(WizardStepType.Auto)]
	public virtual WizardStepType StepType
	{
		get
		{
			object obj = ViewState["StepType"];
			if (obj == null)
			{
				return WizardStepType.Auto;
			}
			return (WizardStepType)obj;
		}
		set
		{
			ViewState["StepType"] = value;
		}
	}

	/// <summary>Gets or sets the title to use for a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control when the sidebar feature is enabled.</summary>
	/// <returns>The title to use for a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control when the sidebar feature is enabled. The default value is an empty string ("").</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public virtual string Title
	{
		get
		{
			object obj = ViewState["Title"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			ViewState["Title"] = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.Wizard" /> control that is the parent of the object derived from <see cref="T:System.Web.UI.WebControls.WizardStepBase" />.</summary>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.Wizard" /> control that is the parent of the object derived from <see cref="T:System.Web.UI.WebControls.WizardStepBase" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public Wizard Wizard => wizard;

	/// <summary>Restores view-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.WebControls.WebControl.SaveViewState" /> method.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the that represents the control state to be restored.</param>
	protected override void LoadViewState(object savedState)
	{
		base.LoadViewState(savedState);
	}

	/// <summary>Raises the <see cref="M:System.Web.UI.Control.OnLoad(System.EventArgs)" /> event.</summary>
	/// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	protected internal override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
	}

	/// <summary>Outputs the content of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> control's child controls to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	protected internal override void RenderChildren(HtmlTextWriter writer)
	{
		base.RenderChildren(writer);
	}

	internal void SetWizard(Wizard w)
	{
		wizard = w;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> class. </summary>
	protected WizardStepBase()
	{
	}
}
