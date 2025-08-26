namespace System.Web.Mail;

/// <summary>Specifies the priority level for the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
[Obsolete("The recommended alternative is System.Net.Mail.MailPriority. http://go.microsoft.com/fwlink/?linkid=14202")]
public enum MailPriority
{
	/// <summary>Specifies that the e-mail message has normal priority.</summary>
	Normal,
	/// <summary>Specifies that the e-mail message has low priority.</summary>
	Low,
	/// <summary>Specifies that the e-mail message has high priority.</summary>
	High
}
