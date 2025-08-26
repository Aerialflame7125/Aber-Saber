namespace System.Web.Mail;

/// <summary>Provides enumerated values for e-mail format. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
[Obsolete("The recommended alternative is System.Net.Mail.MailMessage.IsBodyHtml. http://go.microsoft.com/fwlink/?linkid=14202")]
public enum MailFormat
{
	/// <summary>Specifies that the e-mail format is plain text.</summary>
	Text,
	/// <summary>Specifies that the e-mail format is HTML.</summary>
	Html
}
