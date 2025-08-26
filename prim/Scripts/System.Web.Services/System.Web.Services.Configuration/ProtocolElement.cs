using System.Configuration;

namespace System.Web.Services.Configuration;

/// <summary>Represents a <see langword="protocol" /> element in the Web Services configuration file. The class cannot be inherited.</summary>
public sealed class ProtocolElement : ConfigurationElement
{
	private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

	private readonly ConfigurationProperty name = new ConfigurationProperty("name", typeof(WebServiceProtocols), WebServiceProtocols.Unknown, ConfigurationPropertyOptions.IsKey);

	/// <summary>Gets or sets the protocol name.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Configuration.WebServiceProtocols" /> object that represents the protocol name.</returns>
	[ConfigurationProperty("name", IsKey = true, DefaultValue = WebServiceProtocols.Unknown)]
	public WebServiceProtocols Name
	{
		get
		{
			return (WebServiceProtocols)base[name];
		}
		set
		{
			if (!IsValidProtocolsValue(value))
			{
				value = WebServiceProtocols.Unknown;
			}
			base[name] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties => properties;

	/// <summary>Creates an instance of this class.</summary>
	public ProtocolElement()
	{
		properties.Add(name);
	}

	/// <summary>Creates an instance of this class, and initializes the <see cref="P:System.Web.Services.Configuration.ProtocolElement.Name" /> property.</summary>
	/// <param name="protocol">The value to initialize <see cref="P:System.Web.Services.Configuration.ProtocolElement.Name" />.</param>
	public ProtocolElement(WebServiceProtocols protocol)
		: this()
	{
		Name = protocol;
	}

	private bool IsValidProtocolsValue(WebServiceProtocols value)
	{
		return Enum.IsDefined(typeof(WebServiceProtocols), value);
	}
}
