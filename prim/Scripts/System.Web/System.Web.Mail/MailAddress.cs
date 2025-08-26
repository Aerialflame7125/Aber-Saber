using System.Text;

namespace System.Web.Mail;

internal class MailAddress
{
	protected string user;

	protected string host;

	protected string name;

	public string User
	{
		get
		{
			return user;
		}
		set
		{
			user = value;
		}
	}

	public string Host
	{
		get
		{
			return host;
		}
		set
		{
			host = value;
		}
	}

	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
		}
	}

	public string Address
	{
		get
		{
			return user + "@" + host;
		}
		set
		{
			string[] array = value.Split('@');
			if (array.Length != 2)
			{
				throw new FormatException("Invalid e-mail address: '" + value + "'.");
			}
			user = array[0];
			host = array[1];
		}
	}

	public static MailAddress Parse(string str)
	{
		if (str == null || str.Trim() == "")
		{
			return null;
		}
		MailAddress mailAddress = new MailAddress();
		string text = null;
		string text2 = null;
		string[] array = str.Split(' ', '<');
		foreach (string text3 in array)
		{
			if (text3.IndexOf('@') > 0)
			{
				text = text3;
				break;
			}
			text2 = text2 + text3 + " ";
		}
		if (text == null)
		{
			throw new FormatException("Invalid e-mail address: '" + str + "'.");
		}
		text = text.Trim('<', '>', '(', ')');
		mailAddress.Address = text;
		if (text2 != null)
		{
			mailAddress.Name = text2.Trim(' ', '"');
			mailAddress.Name = ((mailAddress.Name.Length == 0) ? null : mailAddress.Name);
		}
		return mailAddress;
	}

	public override string ToString()
	{
		string text = "";
		if (name == null)
		{
			return "<" + Address + ">";
		}
		string text2 = Name;
		if (MailUtil.NeedEncoding(text2))
		{
			text2 = "=?" + Encoding.Default.BodyName + "?B?" + MailUtil.Base64Encode(text2) + "?=";
		}
		return "\"" + text2 + "\" <" + Address + ">";
	}
}
