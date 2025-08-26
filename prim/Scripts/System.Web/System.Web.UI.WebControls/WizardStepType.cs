namespace System.Web.UI.WebControls;

/// <summary>Specifies the types of navigation UI that can be displayed for a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
public enum WizardStepType
{
	/// <summary>The navigation UI that is rendered for the step is determined automatically by the order in which the step is declared.</summary>
	Auto,
	/// <summary>The step is the last one to appear. No navigation buttons are rendered.</summary>
	Complete,
	/// <summary>The step is the final data collection step. Finish and Previous buttons are rendered for navigation.</summary>
	Finish,
	/// <summary>The step is the first one to appear. A Next button is rendered but a Previous button is not rendered for this step.</summary>
	Start,
	/// <summary>The step is any step between the Start and the Finish steps. Previous and Next buttons are rendered for navigation. This step type is useful for overriding the <see cref="F:System.Web.UI.WebControls.WizardStepType.Auto" /> step type.</summary>
	Step
}
