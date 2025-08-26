using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Represents a basic step that is displayed in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control. This class cannot be inherited.</summary>
[Bindable(false)]
[ControlBuilder(typeof(WizardStepControlBuilder))]
[ToolboxItem(false)]
public sealed class WizardStep : WizardStepBase
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> class. </summary>
	public WizardStep()
	{
	}
}
