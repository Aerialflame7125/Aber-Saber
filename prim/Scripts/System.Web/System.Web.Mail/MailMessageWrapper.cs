using System.Collections;
using System.Text;

namespace System.Web.Mail;

internal class MailMessageWrapper
{
	private MailAddressCollection bcc = new MailAddressCollection();

	private MailAddressCollection cc = new MailAddressCollection();

	private MailAddress from;

	private MailAddressCollection to = new MailAddressCollection();

	private MailHeader header = new MailHeader();

	private MailMessage message;

	private string body;

	public IList Attachments => message.Attachments;

	public MailAddressCollection Bcc => bcc;

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

	public Encoding BodyEncoding
	{
		get
		{
			return message.BodyEncoding;
		}
		set
		{
			message.BodyEncoding = value;
		}
	}

	public MailFormat BodyFormat
	{
		get
		{
			return message.BodyFormat;
		}
		set
		{
			message.BodyFormat = value;
		}
	}

	public MailAddressCollection Cc => cc;

	public MailAddress From => from;

	public MailHeader Header => header;

	public MailPriority Priority
	{
		get
		{
			return message.Priority;
		}
		set
		{
			message.Priority = value;
		}
	}

	public string Subject
	{
		get
		{
			return message.Subject;
		}
		set
		{
			message.Subject = value;
		}
	}

	public MailAddressCollection To => to;

	public string UrlContentBase => message.UrlContentBase;

	public string UrlContentLocation => message.UrlContentLocation;

	public MailHeader Fields
	{
		get
		{
			MailHeader mailHeader = new MailHeader();
			foreach (string key in message.Fields.Keys)
			{
				mailHeader.Data[key] = message.Fields[key].ToString();
			}
			return mailHeader;
		}
	}

	public MailMessageWrapper(MailMessage message)
	{
		this.message = message;
		if (message.From != null)
		{
			from = MailAddress.Parse(message.From);
			header.From = from.ToString();
		}
		if (message.To != null)
		{
			to = MailAddressCollection.Parse(message.To);
			header.To = to.ToString();
		}
		if (message.Cc != null)
		{
			cc = MailAddressCollection.Parse(message.Cc);
			header.Cc = cc.ToString();
		}
		if (message.Bcc != null)
		{
			bcc = MailAddressCollection.Parse(message.Bcc);
			header.Bcc = bcc.ToString();
		}
		if (message.Subject != null)
		{
			if (MailUtil.NeedEncoding(message.Subject))
			{
				byte[] bytes = message.BodyEncoding.GetBytes(message.Subject);
				header.Subject = "=?" + message.BodyEncoding.BodyName + "?B?" + Convert.ToBase64String(bytes) + "?=";
			}
			else
			{
				header.Subject = message.Subject;
			}
		}
		if (message.Body != null)
		{
			body = message.Body.Replace("\n.\n", "\n..\n");
			body = body.Replace("\r\n.\r\n", "\r\n..\r\n");
		}
		if (message.UrlContentBase != null)
		{
			header.ContentBase = message.UrlContentBase;
		}
		if (message.UrlContentLocation != null)
		{
			header.ContentLocation = message.UrlContentLocation;
		}
		switch (message.BodyFormat)
		{
		case MailFormat.Html:
			header.ContentType = "text/html; charset=\"" + message.BodyEncoding.BodyName + "\"";
			break;
		case MailFormat.Text:
			header.ContentType = "text/plain; charset=\"" + message.BodyEncoding.BodyName + "\"";
			break;
		default:
			header.ContentType = "text/html; charset=\"" + message.BodyEncoding.BodyName + "\"";
			break;
		}
		switch (message.Priority)
		{
		case MailPriority.High:
			header.Importance = "high";
			break;
		case MailPriority.Low:
			header.Importance = "low";
			break;
		case MailPriority.Normal:
			header.Importance = "normal";
			break;
		default:
			header.Importance = "normal";
			break;
		}
		header.Priority = "normal";
		header.MimeVersion = "1.0";
		if (message.BodyEncoding is ASCIIEncoding)
		{
			header.ContentTransferEncoding = "7bit";
		}
		else
		{
			header.ContentTransferEncoding = "8bit";
		}
		foreach (string key in message.Headers.Keys)
		{
			header.Data[key] = (string)this.message.Headers[key];
		}
	}
}
