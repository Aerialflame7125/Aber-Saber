using System.Collections.Specialized;
using System.Security.Permissions;
using System.Text;
using System.Web.Configuration;

namespace System.Web;

/// <summary>Provides a type-safe way to create and manipulate individual HTTP cookies.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpCookie
{
	[Serializable]
	private sealed class CookieNVC : NameValueCollection
	{
		public CookieNVC()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("");
			bool flag = true;
			foreach (string key in Keys)
			{
				if (!flag)
				{
					stringBuilder.Append("&");
				}
				string[] array = GetValues(key);
				if (array == null)
				{
					array = new string[1] { string.Empty };
				}
				bool flag2 = true;
				string[] array2 = array;
				foreach (string text2 in array2)
				{
					if (!flag2)
					{
						stringBuilder.Append("&");
					}
					if (key != null && key.Length > 0)
					{
						stringBuilder.Append(HttpUtility.UrlEncode(key));
						stringBuilder.Append("=");
					}
					if (text2 != null && text2.Length > 0)
					{
						stringBuilder.Append(HttpUtility.UrlEncode(text2));
					}
					flag2 = false;
				}
				flag = false;
			}
			return stringBuilder.ToString();
		}

		public override void Set(string name, string value)
		{
			if (base.IsReadOnly)
			{
				throw new NotSupportedException("Collection is read-only");
			}
			if (name == null)
			{
				Clear();
				name = string.Empty;
			}
			base.Set(name, value);
		}
	}

	private string path = "/";

	private string domain;

	private DateTime expires = DateTime.MinValue;

	private string name;

	private CookieFlags flags;

	private NameValueCollection values;

	/// <summary>Gets or sets the domain to associate the cookie with.</summary>
	/// <returns>The name of the domain to associate the cookie with. The default value is the current domain.</returns>
	public string Domain
	{
		get
		{
			return domain;
		}
		set
		{
			domain = value;
		}
	}

	/// <summary>Gets or sets the expiration date and time for the cookie.</summary>
	/// <returns>The time of day (on the client) at which the cookie expires.</returns>
	public DateTime Expires
	{
		get
		{
			return expires;
		}
		set
		{
			expires = value;
		}
	}

	/// <summary>Gets a value indicating whether a cookie has subkeys.</summary>
	/// <returns>
	///     <see langword="true" /> if the cookie has subkeys, otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	public bool HasKeys => values.HasKeys();

	/// <summary>Gets a shortcut to the <see cref="P:System.Web.HttpCookie.Values" /> property. This property is provided for compatibility with previous versions of Active Server Pages (ASP).</summary>
	/// <param name="key">The key (index) of the cookie value. </param>
	/// <returns>The cookie value.</returns>
	public string this[string key]
	{
		get
		{
			return values[key];
		}
		set
		{
			values[key] = value;
		}
	}

	/// <summary>Gets or sets the name of a cookie.</summary>
	/// <returns>The default value is a null reference (<see langword="Nothing" /> in Visual Basic) unless the constructor specifies otherwise.</returns>
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

	/// <summary>Gets or sets the virtual path to transmit with the current cookie.</summary>
	/// <returns>The virtual path to transmit with the cookie. The default is <see langword="/" />, which is the server root.</returns>
	public string Path
	{
		get
		{
			return path;
		}
		set
		{
			path = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether to transmit the cookie using Secure Sockets Layer (SSL)--that is, over HTTPS only.</summary>
	/// <returns>
	///     <see langword="true" /> to transmit the cookie over an SSL connection (HTTPS); otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	public bool Secure
	{
		get
		{
			return (flags & CookieFlags.Secure) == CookieFlags.Secure;
		}
		set
		{
			if (value)
			{
				flags |= CookieFlags.Secure;
			}
			else
			{
				flags &= ~CookieFlags.Secure;
			}
		}
	}

	/// <summary>Gets or sets an individual cookie value.</summary>
	/// <returns>The value of the cookie. The default value is a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
	public string Value
	{
		get
		{
			return HttpUtility.UrlDecode(values.ToString());
		}
		set
		{
			values.Clear();
			if (value == null || !(value != ""))
			{
				return;
			}
			string[] array = value.Split('&');
			foreach (string text in array)
			{
				int num = text.IndexOf('=');
				if (num == -1)
				{
					values.Add(null, text);
					continue;
				}
				string text2 = text.Substring(0, num);
				string value2 = text.Substring(num + 1);
				values.Add(text2, value2);
			}
		}
	}

	/// <summary>Gets a collection of key/value pairs that are contained within a single cookie object.</summary>
	/// <returns>A collection of cookie values.</returns>
	public NameValueCollection Values => values;

	/// <summary>Gets or sets a value that specifies whether a cookie is accessible by client-side script.</summary>
	/// <returns>
	///     <see langword="true" /> if the cookie has the <see langword="HttpOnly" /> attribute and cannot be accessed through a client-side script; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool HttpOnly
	{
		get
		{
			return (flags & CookieFlags.HttpOnly) == CookieFlags.HttpOnly;
		}
		set
		{
			if (value)
			{
				flags |= CookieFlags.HttpOnly;
			}
			else
			{
				flags &= ~CookieFlags.HttpOnly;
			}
		}
	}

	[Obsolete]
	internal HttpCookie(string name, string value, string path, DateTime expires)
	{
		this.name = name;
		values = new CookieNVC();
		Value = value;
		this.path = path;
		this.expires = expires;
	}

	/// <summary>Creates and names a new cookie.</summary>
	/// <param name="name">The name of the new cookie. </param>
	public HttpCookie(string name)
	{
		this.name = name;
		values = new CookieNVC();
		Value = "";
		HttpCookiesSection httpCookiesSection = (HttpCookiesSection)WebConfigurationManager.GetSection("system.web/httpCookies");
		if (!string.IsNullOrWhiteSpace(httpCookiesSection.Domain))
		{
			domain = httpCookiesSection.Domain;
		}
		if (httpCookiesSection.HttpOnlyCookies)
		{
			flags |= CookieFlags.HttpOnly;
		}
		if (httpCookiesSection.RequireSSL)
		{
			flags |= CookieFlags.Secure;
		}
	}

	/// <summary>Creates, names, and assigns a value to a new cookie.</summary>
	/// <param name="name">The name of the new cookie. </param>
	/// <param name="value">The value of the new cookie. </param>
	public HttpCookie(string name, string value)
		: this(name)
	{
		Value = value;
	}

	internal string GetCookieHeaderValue()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(name);
		stringBuilder.Append("=");
		stringBuilder.Append(Value);
		if (domain != null)
		{
			stringBuilder.Append("; domain=");
			stringBuilder.Append(domain);
		}
		if (path != null)
		{
			stringBuilder.Append("; path=");
			stringBuilder.Append(path);
		}
		if (expires != DateTime.MinValue)
		{
			stringBuilder.Append("; expires=");
			stringBuilder.Append(expires.ToUniversalTime().ToString("r"));
		}
		if ((flags & CookieFlags.Secure) != 0)
		{
			stringBuilder.Append("; secure");
		}
		if ((flags & CookieFlags.HttpOnly) != 0)
		{
			stringBuilder.Append("; HttpOnly");
		}
		return stringBuilder.ToString();
	}
}
