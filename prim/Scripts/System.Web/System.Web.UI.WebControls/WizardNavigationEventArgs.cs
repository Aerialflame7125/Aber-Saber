namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="P:System.Web.UI.WebControls.WizardNavigationEventArgs.CurrentStepIndex" /> property and the <see cref="P:System.Web.UI.WebControls.WizardNavigationEventArgs.NextStepIndex" /> property for navigation in wizard controls.</summary>
public class WizardNavigationEventArgs : EventArgs
{
	private int curStepIndex;

	private int nxtStepIndex;

	private bool cancel;

	/// <summary>Gets or sets a value indicating whether the navigation to the next step in the wizard should be canceled.</summary>
	/// <returns>
	///     <see langword="true" /> if the navigation to the next step should be canceled; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	public bool Cancel
	{
		get
		{
			return cancel;
		}
		set
		{
			cancel = value;
		}
	}

	/// <summary>Gets the index of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object currently displayed in the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
	/// <returns>A zero-based index value that represents the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object that is currently displayed in the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</returns>
	public int CurrentStepIndex => curStepIndex;

	/// <summary>Gets a value that represents the index of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object that the <see cref="T:System.Web.UI.WebControls.Wizard" /> control will display next.</summary>
	/// <returns>A zero-based index value that represents the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object that the <see cref="T:System.Web.UI.WebControls.Wizard" /> control will display next.</returns>
	public int NextStepIndex => nxtStepIndex;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WizardNavigationEventArgs" /> class.</summary>
	/// <param name="currentStepIndex">The index of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object that is currently displayed in the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</param>
	/// <param name="nextStepIndex">The index of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object that the <see cref="T:System.Web.UI.WebControls.Wizard" /> control will display next.</param>
	public WizardNavigationEventArgs(int currentStepIndex, int nextStepIndex)
	{
		curStepIndex = currentStepIndex;
		nxtStepIndex = nextStepIndex;
		cancel = false;
	}
}
