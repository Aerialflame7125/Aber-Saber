namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see langword="SendMailError" /> event of controls such as the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control, the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control, and the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control. </summary>
public class SendMailErrorEventArgs : EventArgs
{
	private Exception exception;

	private bool exceptionHandled;

	/// <summary>Returns the exception thrown by an SMTP mail service when an e-mail message cannot be sent.</summary>
	/// <returns>An <see cref="T:System.Exception" /> object that contains the exception.</returns>
	public Exception Exception
	{
		get
		{
			return exception;
		}
		set
		{
			exception = value;
		}
	}

	/// <summary>Indicates if the SMTP exception that is contained in the <see cref="P:System.Web.UI.WebControls.SendMailErrorEventArgs.Exception" /> property has been handled.</summary>
	/// <returns>If <see langword="true" />, the exception is consumed and handled by the <see cref="T:System.Web.UI.WebControls.SendMailErrorEventHandler" /> delegate. If <see langword="false" />, the exception is rethrown, including the original call stack and error message.The default is <see langword="false" />.</returns>
	public bool Handled
	{
		get
		{
			return exceptionHandled;
		}
		set
		{
			exceptionHandled = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SendMailErrorEventArgs" /> class. </summary>
	/// <param name="e">An <see cref="T:System.Exception" /> object containing the exception.</param>
	public SendMailErrorEventArgs(Exception e)
	{
		exception = e;
		exceptionHandled = true;
	}
}
