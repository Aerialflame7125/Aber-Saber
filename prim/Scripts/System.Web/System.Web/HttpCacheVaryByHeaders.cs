using System.Collections;
using System.Security.Permissions;

namespace System.Web;

/// <summary>Provides a type-safe way to set the <see cref="P:System.Web.HttpCachePolicy.VaryByHeaders" /> property.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpCacheVaryByHeaders
{
	private bool vary_by_unspecified;

	private bool vary_by_accept;

	private bool vary_by_user_agent;

	private bool vary_by_user_charset;

	private bool vary_by_user_language;

	private Hashtable fields;

	/// <summary>Gets or sets a value indicating whether the ASP.NET output cache varies the cached responses by the <see langword="Accept" /> HTTP header, and appends it to the out-going <see langword="Vary" /> HTTP header.</summary>
	/// <returns>
	///     <see langword="true" /> when the ASP.NET output cache varies by the <see langword="Accept" /> header; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	public bool AcceptTypes
	{
		get
		{
			return vary_by_accept;
		}
		set
		{
			vary_by_unspecified = false;
			vary_by_accept = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the ASP.NET output cache varies the cached responses by the <see langword="User-Agent" /> header, and appends it to the out-going <see langword="Vary" /> HTTP header.</summary>
	/// <returns>
	///     <see langword="true" /> when the ASP.NET output cache varies by the <see langword="User-Agent" /> header, and adds it to the <see langword="Vary" /> HTTP header sent to the client; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	public bool UserAgent
	{
		get
		{
			return vary_by_user_agent;
		}
		set
		{
			vary_by_unspecified = false;
			vary_by_user_agent = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the ASP.NET output cache varies the cached responses by the <see langword="Accept-Charset" /> header, and appends it to the out-going <see langword="Vary" /> HTTP header.</summary>
	/// <returns>
	///     <see langword="true" /> when the ASP.NET output cache varies by the <see langword="Accept-Charset" /> header and adds it to the <see langword="Vary" /> HTTP header sent to the client; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	public bool UserCharSet
	{
		get
		{
			return vary_by_user_charset;
		}
		set
		{
			vary_by_unspecified = false;
			vary_by_user_charset = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the ASP.NET output cache varies the cached responses by the <see langword="Accept-Language" /> header, and appends it to the out-going <see langword="Vary" /> HTTP header.</summary>
	/// <returns>
	///     <see langword="true" /> when ASP.NET output cache varies by the <see langword="Accept-Language" /> header and adds it to the <see langword="Vary" /> HTTP header sent to the client; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	public bool UserLanguage
	{
		get
		{
			return vary_by_user_language;
		}
		set
		{
			vary_by_unspecified = false;
			vary_by_user_language = value;
		}
	}

	/// <summary>Gets or sets a custom header field that the ASP.NET output cache varies the cached responses by, and appends it to the out-going <see langword="Vary" /> HTTP header.</summary>
	/// <param name="header">The name of the custom header. </param>
	/// <returns>
	///     <see langword="true " />when the ASP.NET output cache varies by the specified custom field; otherwise, <see langword="false" />. The default value is <see langword="false" />. </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="header" /> is <see langword="null" />. </exception>
	public bool this[string header]
	{
		get
		{
			if (header == null)
			{
				throw new ArgumentNullException();
			}
			return fields.Contains(header);
		}
		set
		{
			if (header == null)
			{
				throw new ArgumentNullException();
			}
			vary_by_unspecified = false;
			if (value)
			{
				if (!fields.Contains(header))
				{
					fields.Add(header, true);
				}
				else
				{
					fields.Remove(header);
				}
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCacheVaryByHeaders" /> class.</summary>
	public HttpCacheVaryByHeaders()
	{
		fields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
	}

	internal string[] GetHeaderNames(bool omitVaryStar)
	{
		string[] array;
		if (vary_by_unspecified && !omitVaryStar)
		{
			array = new string[1] { "*" };
		}
		else
		{
			int num = (vary_by_accept ? 1 : 0) + (vary_by_user_agent ? 1 : 0) + (vary_by_user_charset ? 1 : 0) + (vary_by_user_language ? 1 : 0);
			array = new string[fields.Count + num];
			int num2 = 0;
			if (vary_by_accept)
			{
				array[num2++] = "Accept";
			}
			if (vary_by_user_agent)
			{
				array[num2++] = "User-Agent";
			}
			if (vary_by_user_charset)
			{
				array[num2++] = "Accept-Charset";
			}
			if (vary_by_user_language)
			{
				array[num2++] = "Accept-Language";
			}
			fields.Keys.CopyTo(array, num);
		}
		return array;
	}

	/// <summary>Causes ASP.NET to vary by all header values and sets the <see langword="Vary" /> HTTP header to the value * (an asterisk). All other <see langword="Vary" /> header information to be dropped.</summary>
	public void VaryByUnspecifiedParameters()
	{
		fields.Clear();
		vary_by_unspecified = (vary_by_accept = (vary_by_user_agent = (vary_by_user_charset = (vary_by_user_language = false))));
		vary_by_unspecified = true;
	}
}
