using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Permissions;
using System.Text;

namespace System.Web.Mail;

internal class SmtpClient
{
	private string server;

	private TcpClient tcpConnection;

	private SmtpStream smtp;

	private string username;

	private string password;

	private int port = 25;

	private bool usessl;

	private short authenticate = 1;

	public SmtpClient(string server)
	{
		this.server = server;
	}

	private void Connect()
	{
		tcpConnection = new TcpClient(server, port);
		NetworkStream stream = tcpConnection.GetStream();
		smtp = new SmtpStream(stream);
	}

	private void ChangeToSSLSocket()
	{
		Assembly assembly;
		try
		{
			assembly = Assembly.Load("Mono.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756");
		}
		catch (FileNotFoundException)
		{
			throw new SmtpException("Cannot load Mono.Security.dll");
		}
		Type type = assembly.GetType("Mono.Security.Protocol.Tls.SslClientStream");
		object[] array = new object[4] { smtp.Stream, server, true, null };
		Type type2 = assembly.GetType("Mono.Security.Protocol.Tls.SecurityProtocolType");
		int num = (int)Enum.Parse(type2, "Ssl3");
		int num2 = (int)Enum.Parse(type2, "Tls");
		array[3] = Enum.ToObject(type2, num | num2);
		object obj = Activator.CreateInstance(type, array);
		if (obj != null)
		{
			smtp = new SmtpStream((Stream)obj);
		}
	}

	private void ReadFields(MailMessageWrapper msg)
	{
		username = msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/sendusername"];
		password = msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/sendpassword"];
		string text = msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"];
		if (text != null)
		{
			authenticate = short.Parse(text);
		}
		text = msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/smtpusessl"];
		if (text != null)
		{
			usessl = bool.Parse(text);
		}
		text = msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/smtpserverport"];
		if (text != null)
		{
			port = int.Parse(text);
		}
	}

	private void StartSend(MailMessageWrapper msg)
	{
		ReadFields(msg);
		Connect();
		smtp.ReadResponse();
		smtp.CheckForStatusCode(220);
		if (usessl || (username != null && password != null && authenticate != 1))
		{
			smtp.WriteEhlo(Dns.GetHostName());
			if (usessl && smtp.WriteStartTLS())
			{
				ChangeToSSLSocket();
			}
			if (username != null && password != null && authenticate != 1)
			{
				smtp.WriteAuthLogin();
				if (smtp.LastResponse.StatusCode == 334)
				{
					smtp.WriteLine(Convert.ToBase64String(Encoding.ASCII.GetBytes(username)));
					smtp.ReadResponse();
					smtp.CheckForStatusCode(334);
					smtp.WriteLine(Convert.ToBase64String(Encoding.ASCII.GetBytes(password)));
					smtp.ReadResponse();
					smtp.CheckForStatusCode(235);
				}
			}
		}
		else
		{
			smtp.WriteHelo(Dns.GetHostName());
		}
	}

	public void Send(MailMessageWrapper msg)
	{
		if (msg.From == null)
		{
			throw new SmtpException("From property must be set.");
		}
		if (msg.To == null && msg.To.Count < 1)
		{
			throw new SmtpException("Atleast one recipient must be set.");
		}
		StartSend(msg);
		smtp.WriteRset();
		smtp.WriteMailFrom(msg.From.Address);
		foreach (MailAddress item in msg.To)
		{
			smtp.WriteRcptTo(item.Address);
		}
		foreach (MailAddress item2 in msg.Cc)
		{
			smtp.WriteRcptTo(item2.Address);
		}
		foreach (MailAddress item3 in msg.Bcc)
		{
			smtp.WriteRcptTo(item3.Address);
		}
		smtp.WriteData();
		if (msg.Attachments.Count == 0)
		{
			SendSinglepartMail(msg);
		}
		else
		{
			SendMultipartMail(msg);
		}
		smtp.WriteDataEndTag();
	}

	private void SendSinglepartMail(MailMessageWrapper msg)
	{
		smtp.WriteHeader(msg.Header);
		smtp.WriteBytes(msg.BodyEncoding.GetBytes(msg.Body));
	}

	[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
	private void SendMultipartMail(MailMessageWrapper msg)
	{
		string text = MailUtil.GenerateBoundary();
		string contentType = msg.Header.ContentType;
		msg.Header.ContentType = "multipart/mixed;\r\n   boundary=" + text;
		smtp.WriteHeader(msg.Header);
		smtp.WriteBoundary(text);
		MailHeader mailHeader = new MailHeader();
		mailHeader.ContentType = contentType;
		if (msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] != null)
		{
			msg.Fields.Data.Remove("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate");
		}
		if (msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/sendusername"] != null)
		{
			msg.Fields.Data.Remove("http://schemas.microsoft.com/cdo/configuration/sendusername");
		}
		if (msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/sendpassword"] != null)
		{
			msg.Fields.Data.Remove("http://schemas.microsoft.com/cdo/configuration/sendpassword");
		}
		mailHeader.Data.Add(msg.Fields.Data);
		smtp.WriteHeader(mailHeader);
		smtp.WriteBytes(msg.BodyEncoding.GetBytes(msg.Body));
		smtp.WriteBoundary(text);
		for (int i = 0; i < msg.Attachments.Count; i++)
		{
			MailAttachment mailAttachment = (MailAttachment)msg.Attachments[i];
			FileInfo fileInfo = new FileInfo(mailAttachment.Filename);
			MailHeader mailHeader2 = new MailHeader();
			mailHeader2.ContentType = MimeTypes.GetMimeType(fileInfo.Name) + "; name=\"" + fileInfo.Name + "\"";
			mailHeader2.ContentDisposition = "attachment; filename=\"" + fileInfo.Name + "\"";
			mailHeader2.ContentTransferEncoding = mailAttachment.Encoding.ToString();
			smtp.WriteHeader(mailHeader2);
			FileStream fileStream = fileInfo.OpenRead();
			IAttachmentEncoder attachmentEncoder = ((mailAttachment.Encoding != 0) ? ((IAttachmentEncoder)new Base64AttachmentEncoder()) : ((IAttachmentEncoder)new UUAttachmentEncoder(644, fileInfo.Name)));
			attachmentEncoder.EncodeStream(fileStream, smtp.Stream);
			fileStream.Close();
			smtp.WriteLine("");
			if (i < msg.Attachments.Count - 1)
			{
				smtp.WriteBoundary(text);
			}
			else
			{
				smtp.WriteFinalBoundary(text);
			}
		}
	}

	public void Close()
	{
		smtp.WriteQuit();
		tcpConnection.Close();
	}
}
