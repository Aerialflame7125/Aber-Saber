using System.Net.Mail;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for e-mail–related events.</summary>
public class MailMessageEventArgs : LoginCancelEventArgs
{
	private MailMessage _message;

	/// <summary>Gets the e-mail message contents.</summary>
	/// <returns>A <see cref="T:System.Web.Mail.MailMessage" /> containing the message contents.</returns>
	public MailMessage Message => _message;

	/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.WebControls.MailMessageEventArgs" /> class.</summary>
	/// <param name="message">The <see cref="T:System.Net.Mail.MailMessage" /> containing the message.</param>
	public MailMessageEventArgs(MailMessage message)
	{
		_message = message;
	}
}
