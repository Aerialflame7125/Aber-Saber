using System.Text;

namespace System.Web.Mail;

internal class MailUtil
{
	public static bool NeedEncoding(string str)
	{
		foreach (int num in str)
		{
			if ((num <= 61 || num >= 127) && (num <= 31 || num >= 61))
			{
				return true;
			}
		}
		return false;
	}

	public static string Base64Encode(string str)
	{
		return Convert.ToBase64String(Encoding.Default.GetBytes(str));
	}

	public static string GenerateBoundary()
	{
		StringBuilder stringBuilder = new StringBuilder("__MONO__Boundary");
		stringBuilder.Append("__");
		DateTime now = DateTime.Now;
		stringBuilder.Append(now.Year);
		stringBuilder.Append(now.Month);
		stringBuilder.Append(now.Day);
		stringBuilder.Append(now.Hour);
		stringBuilder.Append(now.Minute);
		stringBuilder.Append(now.Second);
		stringBuilder.Append(now.Millisecond);
		stringBuilder.Append("__");
		stringBuilder.Append(new Random().Next());
		stringBuilder.Append("__");
		return stringBuilder.ToString();
	}
}
