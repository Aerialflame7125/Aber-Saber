using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures a <see cref="T:System.Web.Configuration.CustomError" /> section to map an ASP.NET error code to a custom page. This class cannot be inherited.</summary>
public sealed class CustomError : ConfigurationElement
{
	private static ConfigurationProperty redirectProp;

	private static ConfigurationProperty statusCodeProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets the redirection URL.</summary>
	/// <returns>The URL to which the application is redirected when an error occurs.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("redirect", Options = ConfigurationPropertyOptions.IsRequired)]
	public string Redirect
	{
		get
		{
			return (string)base[redirectProp];
		}
		set
		{
			base[redirectProp] = value;
		}
	}

	/// <summary>Gets or sets the HTTP error status code.</summary>
	/// <returns>The HTTP error status code that causes the redirection to the custom error page.</returns>
	[IntegerValidator(MinValue = 100, MaxValue = 999)]
	[ConfigurationProperty("statusCode", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
	public int StatusCode
	{
		get
		{
			return (int)base[statusCodeProp];
		}
		set
		{
			base[statusCodeProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static CustomError()
	{
		redirectProp = new ConfigurationProperty("redirect", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), new StringValidator(1), ConfigurationPropertyOptions.IsRequired);
		statusCodeProp = new ConfigurationProperty("statusCode", typeof(int), null, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(100, 999), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		properties = new ConfigurationPropertyCollection();
		properties.Add(redirectProp);
		properties.Add(statusCodeProp);
	}

	internal CustomError()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.CustomError" /> class. </summary>
	/// <param name="statusCode">The HTTP status code that will result in redirection to the error page.</param>
	/// <param name="redirect">The URL of the custom page mapped to the error code.</param>
	public CustomError(int statusCode, string redirect)
	{
		StatusCode = statusCode;
		Redirect = redirect;
	}

	/// <summary>Compares <see cref="T:System.Web.Configuration.CustomError" /> errors.</summary>
	/// <param name="customError">The error to compare against.</param>
	/// <returns>
	///     <see langword="true" /> if the errors  are equal; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object customError)
	{
		if (!(customError is CustomError customError2))
		{
			return false;
		}
		if (Redirect == customError2.Redirect)
		{
			return StatusCode == customError2.StatusCode;
		}
		return false;
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.CustomError" /> object hash code.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.CustomError" /> object hash code.</returns>
	public override int GetHashCode()
	{
		return Redirect.GetHashCode() + StatusCode;
	}
}
