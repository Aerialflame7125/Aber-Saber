using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Defines the template of the final step for creating a user account with the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control.</summary>
[Browsable(false)]
public sealed class CompleteWizardStep : TemplatedWizardStep
{
	/// <summary>Gets or sets the type of user interface (UI) to display for the <see cref="T:System.Web.UI.WebControls.CompleteWizardStep" /> page of a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control.</summary>
	/// <returns>The <see cref="F:System.Web.UI.WebControls.WizardStepType.Complete" /> enumeration value for the <see cref="T:System.Web.UI.WebControls.WizardStepType" /> enumeration.</returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Web.UI.WebControls.CompleteWizardStep.StepType" /> property.</exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Filterable(false)]
	[Browsable(false)]
	[Themeable(false)]
	public override WizardStepType StepType
	{
		get
		{
			return WizardStepType.Complete;
		}
		set
		{
			throw new InvalidOperationException();
		}
	}

	/// <summary>Gets or sets the title to display for the final user account creation step of the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control.</summary>
	/// <returns>The title to use for the final user account creation step of the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control. The default value is "Complete".</returns>
	[Localizable(true)]
	public override string Title
	{
		get
		{
			object obj = ViewState["TitleText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Complete");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("TitleText");
			}
			else
			{
				ViewState["TitleText"] = value;
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CompleteWizardStep" /> class.</summary>
	public CompleteWizardStep()
	{
	}
}
