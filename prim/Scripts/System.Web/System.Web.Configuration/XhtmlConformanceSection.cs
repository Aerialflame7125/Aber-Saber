using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the <see langword="xhtmlConformance" /> section. This class cannot be inherited. </summary>
public sealed class XhtmlConformanceSection : ConfigurationSection
{
	private static ConfigurationProperty modeProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets the <see cref="P:System.Web.Configuration.XhtmlConformanceSection.Mode" /> property. </summary>
	/// <returns>One of the <see cref="T:System.Web.Configuration.XhtmlConformanceMode" /> values. The default is <see cref="F:System.Web.Configuration.XhtmlConformanceMode.Transitional" />.</returns>
	[ConfigurationProperty("mode", DefaultValue = "Transitional")]
	public XhtmlConformanceMode Mode
	{
		get
		{
			return (XhtmlConformanceMode)base[modeProp];
		}
		set
		{
			base[modeProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static XhtmlConformanceSection()
	{
		modeProp = new ConfigurationProperty("mode", typeof(XhtmlConformanceMode), XhtmlConformanceMode.Transitional, new GenericEnumConverter(typeof(XhtmlConformanceMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		properties = new ConfigurationPropertyCollection();
		properties.Add(modeProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.XhtmlConformanceSection" /> class using default parameters.</summary>
	public XhtmlConformanceSection()
	{
	}
}
