using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Provides programmatic access to the <see langword="webParts" /> configuration file section. This class cannot be inherited.</summary>
public sealed class WebPartsSection : ConfigurationSection
{
	private static ConfigurationProperty enableExportProp;

	private static ConfigurationProperty personalizationProp;

	private static ConfigurationProperty transformersProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets a value indicating whether to enable the export of control data to an XML description file.</summary>
	/// <returns>
	///     <see langword="true" /> to enable the export of control data to an XML description file; otherwise, <see langword="false" />.</returns>
	[ConfigurationProperty("enableExport", DefaultValue = "False")]
	public bool EnableExport
	{
		get
		{
			return (bool)base[enableExportProp];
		}
		set
		{
			base[enableExportProp] = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.Configuration.WebPartsPersonalization" /> object that allows you to specify the Web Parts personalization provider and set Web Parts personalization authorizations.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.WebPartsPersonalization" /> object that allows you to specify the personalization provider and set personalization authorizations.</returns>
	[ConfigurationProperty("personalization")]
	public WebPartsPersonalization Personalization => (WebPartsPersonalization)base[personalizationProp];

	/// <summary>Gets a collection of <see cref="T:System.Web.Configuration.TransformerInfo" /> objects.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.TransformerInfoCollection" /> collection of <see cref="T:System.Web.Configuration.TransformerInfo" /> objects.</returns>
	[ConfigurationProperty("transformers")]
	public TransformerInfoCollection Transformers => (TransformerInfoCollection)base[transformersProp];

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static WebPartsSection()
	{
		enableExportProp = new ConfigurationProperty("enableExport", typeof(bool), false);
		personalizationProp = new ConfigurationProperty("personalization", typeof(WebPartsPersonalization), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		transformersProp = new ConfigurationProperty("transformers", typeof(TransformerInfoCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		properties = new ConfigurationPropertyCollection();
		properties.Add(enableExportProp);
		properties.Add(personalizationProp);
		properties.Add(transformersProp);
	}

	[MonoTODO("why override this?")]
	protected internal override object GetRuntimeObject()
	{
		return this;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.WebPartsSection" /> class using default settings.</summary>
	public WebPartsSection()
	{
	}
}
