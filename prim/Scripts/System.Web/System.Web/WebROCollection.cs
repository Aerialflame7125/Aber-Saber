using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web.Util;

namespace System.Web;

internal class WebROCollection : NameValueCollection
{
	private bool got_id;

	private int id;

	public bool GotID => got_id;

	public int ID
	{
		get
		{
			return id;
		}
		set
		{
			got_id = true;
			id = value;
		}
	}

	public WebROCollection()
		: base(SecureHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant)
	{
	}

	public void Protect()
	{
		base.IsReadOnly = true;
	}

	public void Unprotect()
	{
		base.IsReadOnly = false;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		string[] allKeys = AllKeys;
		foreach (string text in allKeys)
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append('&');
			}
			if (text != null && text.Length > 0)
			{
				stringBuilder.Append(text);
				stringBuilder.Append('=');
			}
			stringBuilder.Append(Get(text));
		}
		return stringBuilder.ToString();
	}
}
