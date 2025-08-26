using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.Util;
using System.Xml;

namespace System.Web.Configuration;

/// <summary>Defines configuration settings that are used to support the globalization infrastructure of Web applications. This class cannot be inherited.</summary>
public sealed class GlobalizationSection : ConfigurationSection
{
	private static ConfigurationProperty cultureProp;

	private static ConfigurationProperty enableBestFitResponseEncodingProp;

	private static ConfigurationProperty enableClientBasedCultureProp;

	private static ConfigurationProperty fileEncodingProp;

	private static ConfigurationProperty requestEncodingProp;

	private static ConfigurationProperty resourceProviderFactoryTypeProp;

	private static ConfigurationProperty responseEncodingProp;

	private static ConfigurationProperty responseHeaderEncodingProp;

	private static ConfigurationProperty uiCultureProp;

	private static ConfigurationPropertyCollection properties;

	private string cached_fileencoding;

	private string cached_requestencoding;

	private string cached_responseencoding;

	private string cached_responseheaderencoding;

	private Hashtable encodingHash;

	private string cached_culture;

	private CultureInfo cached_cultureinfo;

	private string cached_uiculture;

	private CultureInfo cached_uicultureinfo;

	private static bool encoding_warning;

	private static bool culture_warning;

	private bool autoCulture;

	private bool autoUICulture;

	/// <summary>Gets or sets a value specifying the default culture for processing incoming Web requests.</summary>
	/// <returns>The default culture for processing incoming Web requests.</returns>
	[ConfigurationProperty("culture", DefaultValue = "")]
	public string Culture
	{
		get
		{
			return (string)base[cultureProp];
		}
		set
		{
			base[cultureProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the best-fit character encoding for a response is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the best-fit character encoding for a response is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("enableBestFitResponseEncoding", DefaultValue = "False")]
	public bool EnableBestFitResponseEncoding
	{
		get
		{
			return (bool)base[enableBestFitResponseEncodingProp];
		}
		set
		{
			base[enableBestFitResponseEncodingProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Web.Configuration.GlobalizationSection.Culture" /> and <see cref="P:System.Web.Configuration.GlobalizationSection.UICulture" /> properties should be based on the <see cref="F:System.Net.HttpRequestHeader.AcceptLanguage" /> header field value that is sent by the client browser.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.Configuration.GlobalizationSection.Culture" /> and <see cref="P:System.Web.Configuration.GlobalizationSection.UICulture" /> should be based on the <see cref="F:System.Net.HttpRequestHeader.AcceptLanguage" /> header field value sent by the client browser; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("enableClientBasedCulture", DefaultValue = "False")]
	public bool EnableClientBasedCulture
	{
		get
		{
			return (bool)base[enableClientBasedCultureProp];
		}
		set
		{
			base[enableClientBasedCultureProp] = value;
		}
	}

	/// <summary>Gets or sets a value specifying the default encoding for .aspx, .asmx, and .asax file parsing.</summary>
	/// <returns>The default encoding value.</returns>
	[ConfigurationProperty("fileEncoding")]
	public Encoding FileEncoding
	{
		get
		{
			return GetEncoding(fileEncodingProp, ref cached_fileencoding);
		}
		set
		{
			base[fileEncodingProp] = value.WebName;
		}
	}

	/// <summary>Gets or sets a value specifying the content encoding of HTTP requests.</summary>
	/// <returns>The content encoding of HTTP requests. The default is UTF-8.</returns>
	[ConfigurationProperty("requestEncoding", DefaultValue = "utf-8")]
	public Encoding RequestEncoding
	{
		get
		{
			return GetEncoding(requestEncodingProp, ref cached_requestencoding);
		}
		set
		{
			base[requestEncodingProp] = value.WebName;
		}
	}

	/// <summary>Gets or sets the factory type of the resource provider.</summary>
	/// <returns>The factory type of the resource provider.</returns>
	[ConfigurationProperty("resourceProviderFactoryType", DefaultValue = "")]
	public string ResourceProviderFactoryType
	{
		get
		{
			return (string)base[resourceProviderFactoryTypeProp];
		}
		set
		{
			base[resourceProviderFactoryTypeProp] = value;
		}
	}

	/// <summary>Gets or sets a value specifying the content encoding of HTTP responses.</summary>
	/// <returns>The content encoding of HTTP responses. The default is UTF-8.</returns>
	[ConfigurationProperty("responseEncoding", DefaultValue = "utf-8")]
	public Encoding ResponseEncoding
	{
		get
		{
			return GetEncoding(responseEncodingProp, ref cached_responseencoding);
		}
		set
		{
			base[responseEncodingProp] = value.WebName;
		}
	}

	/// <summary>Gets or sets a value specifying the header encoding of HTTP responses.</summary>
	/// <returns>The header encoding of HTTP responses. The default is UTF-8.</returns>
	[ConfigurationProperty("responseHeaderEncoding", DefaultValue = "utf-8")]
	public Encoding ResponseHeaderEncoding
	{
		get
		{
			return GetEncoding(responseHeaderEncodingProp, ref cached_responseheaderencoding);
		}
		set
		{
			base[responseHeaderEncodingProp] = value.WebName;
		}
	}

	/// <summary>Gets or sets a value specifying the default culture for processing locale-dependent resource searches.</summary>
	/// <returns>The default culture for processing locale-dependent resource searches.</returns>
	[ConfigurationProperty("uiCulture", DefaultValue = "")]
	public string UICulture
	{
		get
		{
			return (string)base[uiCultureProp];
		}
		set
		{
			base[uiCultureProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	internal bool IsAutoCulture => autoCulture;

	internal bool IsAutoUICulture => autoUICulture;

	static GlobalizationSection()
	{
		cultureProp = new ConfigurationProperty("culture", typeof(string), "");
		enableBestFitResponseEncodingProp = new ConfigurationProperty("enableBestFitResponseEncoding", typeof(bool), false);
		enableClientBasedCultureProp = new ConfigurationProperty("enableClientBasedCulture", typeof(bool), false);
		fileEncodingProp = new ConfigurationProperty("fileEncoding", typeof(string));
		requestEncodingProp = new ConfigurationProperty("requestEncoding", typeof(string), "utf-8");
		resourceProviderFactoryTypeProp = new ConfigurationProperty("resourceProviderFactoryType", typeof(string), "");
		responseEncodingProp = new ConfigurationProperty("responseEncoding", typeof(string), "utf-8");
		responseHeaderEncodingProp = new ConfigurationProperty("responseHeaderEncoding", typeof(string), "utf-8");
		uiCultureProp = new ConfigurationProperty("uiCulture", typeof(string), "");
		properties = new ConfigurationPropertyCollection();
		properties.Add(cultureProp);
		properties.Add(enableBestFitResponseEncodingProp);
		properties.Add(enableClientBasedCultureProp);
		properties.Add(fileEncodingProp);
		properties.Add(requestEncodingProp);
		properties.Add(resourceProviderFactoryTypeProp);
		properties.Add(responseEncodingProp);
		properties.Add(responseHeaderEncodingProp);
		properties.Add(uiCultureProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.GlobalizationSection" /> class by using default settings.</summary>
	public GlobalizationSection()
	{
		encodingHash = new Hashtable();
	}

	private void VerifyData()
	{
		bool auto = false;
		try
		{
			GetSanitizedCulture(Culture, ref auto);
		}
		catch
		{
			throw new ConfigurationErrorsException("the <globalization> tag contains an invalid value for the 'culture' attribute");
		}
		try
		{
			GetSanitizedCulture(UICulture, ref auto);
		}
		catch
		{
			throw new ConfigurationErrorsException("the <globalization> tag contains an invalid value for the 'uiCulture' attribute");
		}
	}

	protected override void PostDeserialize()
	{
		base.PostDeserialize();
		VerifyData();
	}

	protected override void PreSerialize(XmlWriter writer)
	{
		base.PreSerialize(writer);
		VerifyData();
	}

	private CultureInfo GetSanitizedCulture(string culture, ref bool auto)
	{
		auto = false;
		if (culture == null)
		{
			throw new ArgumentNullException("culture");
		}
		if (culture.Length <= 3)
		{
			return new CultureInfo(culture);
		}
		if (culture.StartsWith("auto"))
		{
			auto = true;
			if (culture.Length > 5 && culture[4] == ':')
			{
				return new CultureInfo(culture.Substring(5));
			}
			return Helpers.InvariantCulture;
		}
		return new CultureInfo(culture);
	}

	internal CultureInfo GetUICulture()
	{
		string uICulture = UICulture;
		if (cached_uiculture != uICulture)
		{
			try
			{
				cached_uicultureinfo = GetSanitizedCulture(uICulture, ref autoUICulture);
				cached_uiculture = uICulture;
			}
			catch
			{
				CultureFailed("UICulture", uICulture);
				cached_uicultureinfo = new CultureInfo(127);
				cached_uiculture = null;
			}
		}
		return cached_uicultureinfo;
	}

	internal CultureInfo GetCulture()
	{
		string culture = Culture;
		if (cached_culture != culture)
		{
			try
			{
				cached_cultureinfo = GetSanitizedCulture(culture, ref autoCulture);
				cached_culture = culture;
			}
			catch
			{
				CultureFailed("Culture", culture);
				cached_cultureinfo = new CultureInfo(127);
				cached_culture = null;
			}
		}
		return cached_cultureinfo;
	}

	private Encoding GetEncoding(ConfigurationProperty prop, ref string cached_encoding_name)
	{
		string text = (string)base[prop];
		if (cached_encoding_name == null)
		{
			cached_encoding_name = ((text == null) ? "utf-8" : text);
		}
		Encoding encoding = (Encoding)encodingHash[prop];
		if (encoding == null || encoding.WebName != cached_encoding_name)
		{
			try
			{
				switch (cached_encoding_name.ToLower(Helpers.InvariantCulture))
				{
				case "utf-16le":
				case "utf-16":
				case "ucs-2":
				case "unicode":
				case "iso-10646-ucs-2":
					encoding = new UnicodeEncoding(bigEndian: false, byteOrderMark: true);
					break;
				case "utf-16be":
				case "unicodefffe":
					encoding = new UnicodeEncoding(bigEndian: true, byteOrderMark: true);
					break;
				case "utf-8":
				case "unicode-1-1-utf-8":
				case "unicode-2-0-utf-8":
				case "x-unicode-1-1-utf-8":
				case "x-unicode-2-0-utf-8":
					encoding = Encoding.UTF8;
					break;
				default:
					encoding = Encoding.GetEncoding(cached_encoding_name);
					break;
				}
			}
			catch
			{
				EncodingFailed(prop.Name, cached_encoding_name);
				encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: false);
			}
		}
		encodingHash[prop] = encoding;
		cached_encoding_name = encoding.WebName;
		return encoding;
	}

	private static void EncodingFailed(string att, string enc)
	{
		if (!encoding_warning)
		{
			encoding_warning = true;
			Console.WriteLine("Encoding {1} cannot be loaded.\n{0}=\"{1}\"\n", att, enc);
		}
	}

	private static void CultureFailed(string att, string cul)
	{
		if (!culture_warning)
		{
			culture_warning = true;
			Console.WriteLine("Culture {1} cannot be loaded. Perhaps your runtime \ndon't have ICU support?\n{0}=\"{1}\"\n", att, cul);
		}
	}
}
