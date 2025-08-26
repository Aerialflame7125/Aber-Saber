using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Represents a step in a wizard control that can be customized through the use of templates.</summary>
[Themeable(true)]
[Bindable(false)]
[PersistChildren(false)]
[ParseChildren(true)]
[ToolboxItem(false)]
[ControlBuilder(typeof(WizardStepControlBuilder))]
public class TemplatedWizardStep : WizardStepBase
{
	private ITemplate _contentTemplate;

	private Control _contentTemplateContainer;

	private ITemplate _customNavigationTemplate;

	private Control _customNavigationTemplateContainer;

	/// <summary>Gets or sets the template for displaying the content of a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for displaying the content of a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	[TemplateContainer(typeof(Wizard))]
	public virtual ITemplate ContentTemplate
	{
		get
		{
			return _contentTemplate;
		}
		set
		{
			_contentTemplate = value;
		}
	}

	/// <summary>Gets the container that a <see cref="T:System.Web.UI.WebControls.Wizard" /> control uses to create a <see cref="P:System.Web.UI.WebControls.TemplatedWizardStep.ContentTemplate" /> template for a step.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Control" /> that contains the <see cref="P:System.Web.UI.WebControls.TemplatedWizardStep.ContentTemplate" /> template for a step.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Control ContentTemplateContainer
	{
		get
		{
			return _contentTemplateContainer;
		}
		internal set
		{
			_contentTemplateContainer = value;
		}
	}

	/// <summary>Gets or sets the template for displaying the navigation user interface (UI) of a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for displaying the navigation UI of a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	[TemplateContainer(typeof(Wizard))]
	public virtual ITemplate CustomNavigationTemplate
	{
		get
		{
			return _customNavigationTemplate;
		}
		set
		{
			_customNavigationTemplate = value;
		}
	}

	/// <summary>Gets the container that a <see cref="T:System.Web.UI.WebControls.Wizard" /> control uses to create a <see cref="P:System.Web.UI.WebControls.TemplatedWizardStep.CustomNavigationTemplate" /> template for a step.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Control" /> that contains the <see cref="P:System.Web.UI.WebControls.TemplatedWizardStep.CustomNavigationTemplate" /> template for a step.</returns>
	/// <exception cref="T:System.NullReferenceException">If <see cref="P:System.Web.UI.WebControls.TemplatedWizardStep.CustomNavigationTemplate" /> has no content.</exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Bindable(false)]
	public Control CustomNavigationTemplateContainer
	{
		get
		{
			return _customNavigationTemplateContainer;
		}
		internal set
		{
			_customNavigationTemplateContainer = value;
		}
	}

	/// <summary>Gets or sets the skin to apply to the control.</summary>
	/// <returns>The name of the skin to apply to the control. The default is <see cref="F:System.String.Empty" />.</returns>
	[Browsable(true)]
	[MonoTODO("Why override?")]
	public override string SkinID
	{
		get
		{
			return base.SkinID;
		}
		set
		{
			base.SkinID = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TemplatedWizardStep" /> class. </summary>
	public TemplatedWizardStep()
	{
	}
}
