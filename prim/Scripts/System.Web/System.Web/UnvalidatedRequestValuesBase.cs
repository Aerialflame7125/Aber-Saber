using System.Collections.Specialized;

namespace System.Web;

/// <summary>Serves as the base class for classes that provide access to HTTP request values without triggering ASP.NET request validation.</summary>
public abstract class UnvalidatedRequestValuesBase
{
	/// <summary>When overridden in a derived class, gets the collection of form variables that the client submitted, without triggering ASP.NET request validation.</summary>
	/// <returns>The form variables from the HTTP request.</returns>
	/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
	public virtual NameValueCollection Form
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the collection of HTTP query string variables that the client submitted, without triggering ASP.NET request validation.</summary>
	/// <returns>The collection of query string variables sent by the client.</returns>
	/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
	public virtual NameValueCollection QueryString
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the collection of HTTP headers that the client sent, without triggering ASP.NET request validation.</summary>
	/// <returns>The headers from the HTTP request.</returns>
	/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
	public virtual NameValueCollection Headers
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the collection of cookies that the client sent, without triggering ASP.NET request validation.</summary>
	/// <returns>The cookies from the HTTP request.</returns>
	/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
	public virtual HttpCookieCollection Cookies
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the collection of files that the client uploaded, without triggering ASP.NET request validation.</summary>
	/// <returns>The files from the HTTP request.</returns>
	/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
	public virtual HttpFileCollectionBase Files
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the part of the requested URL that follows the website name, without triggering ASP.NET request validation.</summary>
	/// <returns>The part of the URL that follows the website name.</returns>
	/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
	public virtual string RawUrl
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the virtual path of the requested resource without triggering ASP.NET request validation.</summary>
	/// <returns>The virtual path.</returns>
	/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
	public virtual string Path
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets additional path information for a resource that has a URL extension, without triggering ASP.NET request validation.</summary>
	/// <returns>A string that contains additional path information for a resource.</returns>
	/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
	public virtual string PathInfo
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the specified object from the <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.Cookies" />, <see cref="P:System.Web.HttpRequest.QueryString" />, or <see cref="P:System.Web.HttpRequest.ServerVariables" /> collection, without triggering ASP.NET request validation.</summary>
	/// <param name="field">The key of the object to retrieve.</param>
	/// <returns>The object specified by the <paramref name="field" /> parameter.</returns>
	/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
	public virtual string this[string field]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the URL data for the request without triggering request validation.</summary>
	/// <returns>An object that contains the URL data for the request.</returns>
	/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
	public virtual Uri Url
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>
	///     Called from constructors in derived classes in order to initialize the <see cref="T:System.Web.UnvalidatedRequestValuesBase" /> class.</summary>
	protected UnvalidatedRequestValuesBase()
	{
	}
}
