using System.IO;
using System.Net.Sockets;
using System.Security.Permissions;

namespace System.Web.Mail;

/// <summary>Provides properties and methods for sending messages using the Collaboration Data Objects for Windows 2000 (CDOSYS) message component. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
[Obsolete("The recommended alternative is System.Net.Mail.SmtpClient. http://go.microsoft.com/fwlink/?linkid=14202")]
public class SmtpMail
{
	private static string smtpServer = "localhost";

	/// <summary>Gets or sets the name of the SMTP relay mail server to use to send e-mail messages. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <returns>The name of the e-mail relay server. </returns>
	public static string SmtpServer
	{
		get
		{
			return smtpServer;
		}
		set
		{
			smtpServer = value;
		}
	}

	private SmtpMail()
	{
	}

	/// <summary>Sends an e-mail message using arguments supplied in the properties of the <see cref="T:System.Web.Mail.MailMessage" /> class. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <param name="message">The <see cref="T:System.Web.Mail.MailMessage" /> to send. </param>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">The mail cannot be sent.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="M:System.Web.Mail.SmtpMail.Send(System.Web.Mail.MailMessage)" /> method requires the Microsoft Windows NT, Windows 2000, or Windows XP operating system.</exception>
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
	public static void Send(MailMessage message)
	{
		try
		{
			MailMessageWrapper msg = new MailMessageWrapper(message);
			SmtpClient smtpClient = new SmtpClient(smtpServer);
			smtpClient.Send(msg);
			smtpClient.Close();
		}
		catch (SmtpException ex)
		{
			throw new HttpException(ex.Message, ex);
		}
		catch (IOException ex2)
		{
			throw new HttpException(ex2.Message, ex2);
		}
		catch (FormatException ex3)
		{
			throw new HttpException(ex3.Message, ex3);
		}
		catch (SocketException ex4)
		{
			throw new HttpException(ex4.Message, ex4);
		}
	}

	/// <summary>Sends an e-mail message using the specified destination parameters. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <param name="from">The address of the e-mail sender. </param>
	/// <param name="to">The address of the e-mail recipient. </param>
	/// <param name="subject">The subject line of the e-mail message. </param>
	/// <param name="messageText">The body of the e-mail message. </param>
	/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="M:System.Web.Mail.SmtpMail.Send(System.String,System.String,System.String,System.String)" /> method requires the Microsoft Windows NT, Windows 2000, or Windows XP operating system.</exception>
	public static void Send(string from, string to, string subject, string messageText)
	{
		Send(new MailMessage
		{
			From = from,
			To = to,
			Subject = subject,
			Body = messageText
		});
	}
}
