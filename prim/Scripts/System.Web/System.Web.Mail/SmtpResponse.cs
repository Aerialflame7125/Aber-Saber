namespace System.Web.Mail;

internal class SmtpResponse
{
	private string rawResponse;

	private int statusCode;

	private string[] parts;

	public int StatusCode
	{
		get
		{
			return statusCode;
		}
		set
		{
			statusCode = value;
		}
	}

	public string RawResponse
	{
		get
		{
			return rawResponse;
		}
		set
		{
			rawResponse = value;
		}
	}

	public string[] Parts
	{
		get
		{
			return parts;
		}
		set
		{
			parts = value;
		}
	}

	protected SmtpResponse()
	{
	}

	public static SmtpResponse Parse(string line)
	{
		SmtpResponse smtpResponse = new SmtpResponse();
		if (line.Length < 4)
		{
			throw new SmtpException("Response is to short " + line.Length + ".");
		}
		if (line[3] != ' ' && line[3] != '-')
		{
			throw new SmtpException("Response format is wrong.(" + line + ")");
		}
		smtpResponse.StatusCode = int.Parse(line.Substring(0, 3));
		smtpResponse.RawResponse = line;
		smtpResponse.Parts = line.Substring(0, 3).Split(';');
		return smtpResponse;
	}
}
