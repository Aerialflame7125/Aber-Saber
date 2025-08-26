namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.CustomValidator.ServerValidate" /> event of the <see cref="T:System.Web.UI.WebControls.CustomValidator" /> control. This class cannot be inherited.</summary>
public class ServerValidateEventArgs : EventArgs
{
	private bool isValid;

	private string value;

	/// <summary>Gets the value to validate in the custom event handler for the <see cref="E:System.Web.UI.WebControls.CustomValidator.ServerValidate" /> event.</summary>
	/// <returns>The value to validate in the custom event handler for the <see cref="E:System.Web.UI.WebControls.CustomValidator.ServerValidate" /> event.</returns>
	public string Value => value;

	/// <summary>Gets or sets whether the value specified by the <see cref="P:System.Web.UI.WebControls.ServerValidateEventArgs.Value" /> property passed validation.</summary>
	/// <returns>
	///     <see langword="true" /> to indicate that the value specified by the <see cref="P:System.Web.UI.WebControls.ServerValidateEventArgs.Value" /> property passed validation; otherwise, <see langword="false" />.</returns>
	public bool IsValid
	{
		get
		{
			return isValid;
		}
		set
		{
			isValid = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ServerValidateEventArgs" /> class.</summary>
	/// <param name="value">The value to validate. </param>
	/// <param name="isValid">
	///       <see langword="true" /> to indicate that the value passes validation; otherwise, <see langword="false" />. </param>
	public ServerValidateEventArgs(string value, bool isValid)
	{
		this.isValid = isValid;
		this.value = value;
	}
}
