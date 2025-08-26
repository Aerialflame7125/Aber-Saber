using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Contains basic functionality for creating a user in a step that can be templated. This class cannot be inherited.</summary>
[Browsable(false)]
public sealed class CreateUserWizardStep : TemplatedWizardStep
{
	/// <summary>Gets or sets a value indicating whether the user is allowed to return to the current step from a subsequent step in a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control.</summary>
	/// <returns>
	///     <see langword="true" /> if the user is allowed to return to the <see cref="T:System.Web.UI.WebControls.CreateUserWizardStep" /> step; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the property.</exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public override bool AllowReturn
	{
		get
		{
			return ViewState.GetBool("AllowReturn", def: false);
		}
		set
		{
			ViewState["AllowReturn"] = value;
		}
	}

	/// <summary>Gets or sets the title to use for the user-account-creation step of the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control. </summary>
	/// <returns>The title to use for the user-account-creation step of the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control. The default value is "Sign up for your new account." The default text for the control is localized based on the server's current locale.</returns>
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
			return Locale.GetText("Sign Up for Your New Account");
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

	/// <summary>Gets or sets the type of user interface (UI) to display for the <see cref="T:System.Web.UI.WebControls.CreateUserWizardStep" /> step of a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control.</summary>
	/// <returns>The <see cref="F:System.Web.UI.WebControls.WizardStepType.Auto" /> enumeration value of the <see cref="T:System.Web.UI.WebControls.WizardStepType" /> enumeration.</returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Web.UI.WebControls.CreateUserWizardStep.StepType" /> property to a value other than the <see langword="WizardStepType.Auto" /> enumeration value.</exception>
	[Filterable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[Themeable(false)]
	public override WizardStepType StepType
	{
		get
		{
			return WizardStepType.Auto;
		}
		set
		{
			throw new InvalidOperationException();
		}
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.WebControls.CreateUserWizardStep" /> control.</summary>
	public CreateUserWizardStep()
	{
	}
}
