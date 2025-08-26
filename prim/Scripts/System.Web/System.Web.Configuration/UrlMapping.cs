using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Maps a URL that is displayed to users to the URL of a page in your Web application. This class cannot be inherited.</summary>
public sealed class UrlMapping : ConfigurationElement
{
	private static ConfigurationProperty mappedUrlProp;

	private static ConfigurationProperty urlProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>A URL in your Web application.</summary>
	/// <returns>The URL in your Web application that has been mapped to the value specified by the <see cref="P:System.Web.Configuration.UrlMapping.Url" /> property. </returns>
	[ConfigurationProperty("mappedUrl", Options = ConfigurationPropertyOptions.IsRequired)]
	public string MappedUrl
	{
		get
		{
			return (string)base[mappedUrlProp];
		}
		internal set
		{
			base[mappedUrlProp] = value;
		}
	}

	/// <summary>Gets the URL that is displayed to the user.</summary>
	/// <returns>The URL that is displayed to the user.</returns>
	[ConfigurationProperty("url", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
	public string Url
	{
		get
		{
			return (string)base[urlProp];
		}
		internal set
		{
			base[urlProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	private static void ValidateUrl(object value)
	{
		string text = value as string;
		if (string.IsNullOrEmpty(text) || VirtualPathUtility.IsAppRelative(text))
		{
			return;
		}
		throw new ConfigurationException("Only app-relative (~/) URLs are allowed");
	}

	static UrlMapping()
	{
		mappedUrlProp = new ConfigurationProperty("mappedUrl", typeof(string), null, PropertyHelper.WhiteSpaceTrimStringConverter, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);
		urlProp = new ConfigurationProperty("url", typeof(string), null, PropertyHelper.WhiteSpaceTrimStringConverter, new CallbackValidator(typeof(string), ValidateUrl), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		properties = new ConfigurationPropertyCollection();
		properties.Add(mappedUrlProp);
		properties.Add(urlProp);
	}

	internal UrlMapping()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.UrlMapping" /> class.</summary>
	/// <param name="url">The URL to be displayed to the user.</param>
	/// <param name="mappedUrl">A URL that exists in your Web application.</param>
	public UrlMapping(string url, string mappedUrl)
	{
		Url = url;
		MappedUrl = mappedUrl;
	}
}
