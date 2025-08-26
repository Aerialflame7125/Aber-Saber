namespace System.Web.Mail;

/// <summary>Provides enumerated values for e-mail encoding. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
[Obsolete("The recommended alternative is System.Net.Mime.TransferEncoding. http://go.microsoft.com/fwlink/?linkid=14202")]
public enum MailEncoding
{
	/// <summary>Specifies that the e-mail message uses UUEncode encoding.</summary>
	UUEncode,
	/// <summary>Specifies that the e-mail message uses Base64 encoding.</summary>
	Base64
}
