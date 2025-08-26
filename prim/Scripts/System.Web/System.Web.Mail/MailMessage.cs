using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace System.Web.Mail;

/// <summary>Provides properties and methods for constructing an e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
[Obsolete("The recommended alternative is System.Net.Mail.MailMessage. http://go.microsoft.com/fwlink/?linkid=14202")]
public class MailMessage
{
	private ArrayList attachments;

	private string bcc;

	private string body = string.Empty;

	private Encoding bodyEncoding;

	private MailFormat bodyFormat;

	private string cc;

	private string from;

	private ListDictionary headers;

	private MailPriority priority;

	private string subject = string.Empty;

	private string to;

	private string urlContentBase;

	private string urlContentLocation;

	private Hashtable fields;

	/// <summary>Specifies the collection of attachments that are transmitted with the message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <returns>An <see cref="T:System.Collections.IList" /> collection of <see cref="T:System.Web.Mail.MailAttachment" /> objects.</returns>
	public IList Attachments => attachments;

	/// <summary>Gets or sets a semicolon-delimited list of email addresses that receive a blind carbon copy (BCC) of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <returns>A semicolon-delimited list of e-mail addresses that receive a blind carbon copy (BCC) of the e-mail message.</returns>
	public string Bcc
	{
		get
		{
			return bcc;
		}
		set
		{
			bcc = value;
		}
	}

	/// <summary>Gets or sets the body of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <returns>The body of the e-mail message.</returns>
	public string Body
	{
		get
		{
			return body;
		}
		set
		{
			body = value;
		}
	}

	/// <summary>Gets or sets the encoding type of the body of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <returns>One of the <see cref="T:System.Text.Encoding" /> values that indicates the encoding type of the body of the e-mail message.</returns>
	public Encoding BodyEncoding
	{
		get
		{
			return bodyEncoding;
		}
		set
		{
			bodyEncoding = value;
		}
	}

	/// <summary>Gets or sets the content type of the body of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <returns>One of the <see cref="T:System.Web.Mail.MailFormat" /> values.</returns>
	public MailFormat BodyFormat
	{
		get
		{
			return bodyFormat;
		}
		set
		{
			bodyFormat = value;
		}
	}

	/// <summary>Gets or sets a semicolon-delimited list of e-mail addresses that receive a carbon copy (CC) of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <returns>A semicolon-delimited list of e-mail addresses that receive a carbon copy (CC) of the e-mail message.</returns>
	public string Cc
	{
		get
		{
			return cc;
		}
		set
		{
			cc = value;
		}
	}

	/// <summary>Gets or sets the e-mail address of the sender. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <returns>The sender's e-mail address.</returns>
	public string From
	{
		get
		{
			return from;
		}
		set
		{
			from = value;
		}
	}

	/// <summary>Specifies the custom headers that are transmitted with the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> collection of custom headers.</returns>
	public IDictionary Headers => headers;

	/// <summary>Gets or sets the priority of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <returns>One of the <see cref="T:System.Web.Mail.MailPriority" /> values.</returns>
	public MailPriority Priority
	{
		get
		{
			return priority;
		}
		set
		{
			priority = value;
		}
	}

	/// <summary>Gets or sets the subject line of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <returns>The subject line of the e-mail message.</returns>
	public string Subject
	{
		get
		{
			return subject;
		}
		set
		{
			subject = value;
		}
	}

	/// <summary>Gets or sets a semicolon-delimited list of recipient e-mail addresses. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <returns>A semicolon-delimited list of e-mail addresses.</returns>
	public string To
	{
		get
		{
			return to;
		}
		set
		{
			to = value;
		}
	}

	/// <summary>Gets or sets the <see langword="Content-Base" /> HTTP header, the URL base of all relative URLs used within the HTML-encoded body of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <returns>The URL base.</returns>
	public string UrlContentBase
	{
		get
		{
			return urlContentBase;
		}
		set
		{
			urlContentBase = value;
		}
	}

	/// <summary>Gets or sets the <see langword="Content-Location" /> HTTP header for the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <returns>The <see langword="content-base" /> header.</returns>
	public string UrlContentLocation
	{
		get
		{
			return urlContentLocation;
		}
		set
		{
			urlContentLocation = value;
		}
	}

	/// <summary>Gets a collection of objects that map to Microsoft Collaboration Data Objects (CDO) fields. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> collection of objects that map to Collaboration Data Objects (CDO) fields.</returns>
	public IDictionary Fields => fields;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Mail.MailMessage" /> class. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	public MailMessage()
	{
		attachments = new ArrayList(8);
		headers = new ListDictionary();
		bodyEncoding = Encoding.Default;
		fields = new Hashtable();
	}
}
