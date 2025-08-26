using System.IO;
using System.Text;

namespace System.Web.Mail;

internal class SmtpStream
{
	protected Stream stream;

	protected Encoding encoding;

	protected SmtpResponse lastResponse;

	protected string command = "";

	public Stream Stream => stream;

	public SmtpResponse LastResponse => lastResponse;

	public SmtpStream(Stream stream)
	{
		this.stream = stream;
		encoding = new ASCIIEncoding();
	}

	public void WriteRset()
	{
		command = "RSET";
		WriteLine(command);
		ReadResponse();
		CheckForStatusCode(250);
	}

	public void WriteAuthLogin()
	{
		command = "AUTH LOGIN";
		WriteLine(command);
		ReadResponse();
	}

	public bool WriteStartTLS()
	{
		command = "STARTTLS";
		WriteLine(command);
		ReadResponse();
		return LastResponse.StatusCode == 220;
	}

	public void WriteEhlo(string hostName)
	{
		command = "EHLO " + hostName;
		WriteLine(command);
		ReadResponse();
		CheckForStatusCode(250);
	}

	public void WriteHelo(string hostName)
	{
		command = "HELO " + hostName;
		WriteLine(command);
		ReadResponse();
		CheckForStatusCode(250);
	}

	public void WriteMailFrom(string from)
	{
		command = "MAIL FROM: <" + from + ">";
		WriteLine(command);
		ReadResponse();
		CheckForStatusCode(250);
	}

	public void WriteRcptTo(string to)
	{
		command = "RCPT TO: <" + to + ">";
		WriteLine(command);
		ReadResponse();
		CheckForStatusCode(250);
	}

	public void WriteData()
	{
		command = "DATA";
		WriteLine(command);
		ReadResponse();
		CheckForStatusCode(354);
	}

	public void WriteQuit()
	{
		command = "QUIT";
		WriteLine(command);
		ReadResponse();
		CheckForStatusCode(221);
	}

	public void WriteBoundary(string boundary)
	{
		WriteLine("\r\n--{0}", boundary);
	}

	public void WriteFinalBoundary(string boundary)
	{
		WriteLine("\r\n--{0}--", boundary);
	}

	public void WriteDataEndTag()
	{
		command = "\r\n.";
		WriteLine(command);
		ReadResponse();
		CheckForStatusCode(250);
	}

	public void WriteHeader(MailHeader header)
	{
		string[] allKeys = header.Data.AllKeys;
		foreach (string text in allKeys)
		{
			WriteLine("{0}: {1}", text, header.Data[text]);
		}
		WriteLine("");
	}

	public void CheckForStatusCode(int statusCode)
	{
		if (LastResponse.StatusCode != statusCode)
		{
			throw new SmtpException("Server reponse: '" + lastResponse.RawResponse + "';Status code: '" + lastResponse.StatusCode + "';Expected status code: '" + statusCode + "';Last command: '" + command + "'");
		}
	}

	public void WriteBytes(byte[] buffer)
	{
		stream.Write(buffer, 0, buffer.Length);
	}

	public void WriteLine(string format, params object[] args)
	{
		WriteLine(string.Format(format, args));
	}

	public void WriteLine(string line)
	{
		byte[] bytes = encoding.GetBytes(line + "\r\n");
		stream.Write(bytes, 0, bytes.Length);
	}

	public void ReadResponse()
	{
		byte[] array = new byte[512];
		int num = 0;
		bool flag = false;
		do
		{
			int num2 = stream.Read(array, num, array.Length - num);
			if (num2 <= 0)
			{
				continue;
			}
			int num3 = num + num2 - 1;
			if (num3 > 4 && (array[num3] == 10 || array[num3] == 13))
			{
				int num4 = num3 - 3;
				while (num4 >= 0 && array[num4] != 10 && array[num4] != 13)
				{
					num4--;
				}
				flag = array[num4 + 4] == 32;
			}
			num += num2;
			if (num == array.Length)
			{
				byte[] array2 = new byte[array.Length * 2];
				Array.Copy(array, 0, array2, 0, array.Length);
				array = array2;
			}
		}
		while (!flag);
		string @string = encoding.GetString(array, 0, num - 1);
		lastResponse = SmtpResponse.Parse(@string);
	}
}
