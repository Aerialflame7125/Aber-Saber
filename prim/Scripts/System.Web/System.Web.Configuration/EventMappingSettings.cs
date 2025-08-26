using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Defines the ASP.NET event mapping settings for event providers. This class cannot be inherited.</summary>
public sealed class EventMappingSettings : ConfigurationElement
{
	private static ConfigurationProperty endEventCodeProp;

	private static ConfigurationProperty nameProp;

	private static ConfigurationProperty startEventCodeProp;

	private static ConfigurationProperty typeProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets the ending event code of the range.</summary>
	/// <returns>The ending event code of the range. The default is <see cref="F:System.Int32.MaxValue" />.</returns>
	[IntegerValidator(MinValue = 0, MaxValue = int.MaxValue)]
	[ConfigurationProperty("endEventCode", DefaultValue = "2147483647")]
	public int EndEventCode
	{
		get
		{
			return (int)base[endEventCodeProp];
		}
		set
		{
			base[endEventCodeProp] = value;
		}
	}

	/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object.</summary>
	/// <returns>The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object. The default is an empty string ("").</returns>
	[ConfigurationProperty("name", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
	public string Name
	{
		get
		{
			return (string)base[nameProp];
		}
		set
		{
			base[nameProp] = value;
		}
	}

	/// <summary>Gets or sets the starting event code of the range.</summary>
	/// <returns>The starting event code of the range. The default is 0.</returns>
	[IntegerValidator(MinValue = 0, MaxValue = int.MaxValue)]
	[ConfigurationProperty("startEventCode", DefaultValue = "0")]
	public int StartEventCode
	{
		get
		{
			return (int)base[startEventCodeProp];
		}
		set
		{
			base[startEventCodeProp] = value;
		}
	}

	/// <summary>Gets or sets a custom event type.</summary>
	/// <returns>A valid type reference or an empty string (""). The default is an empty string.</returns>
	[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
	public string Type
	{
		get
		{
			return (string)base[typeProp];
		}
		set
		{
			base[typeProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static EventMappingSettings()
	{
		endEventCodeProp = new ConfigurationProperty("endEventCode", typeof(int), int.MaxValue, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);
		nameProp = new ConfigurationProperty("name", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		startEventCodeProp = new ConfigurationProperty("startEventCode", typeof(int), 0, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);
		typeProp = new ConfigurationProperty("type", typeof(string), "", ConfigurationPropertyOptions.IsRequired);
		properties = new ConfigurationPropertyCollection();
		properties.Add(endEventCodeProp);
		properties.Add(nameProp);
		properties.Add(startEventCodeProp);
		properties.Add(typeProp);
	}

	internal EventMappingSettings()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> class using the specified name and type.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object being created.</param>
	/// <param name="type">The fully qualified type of the event class to use.</param>
	public EventMappingSettings(string name, string type)
	{
		Name = name;
		Type = type;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> class using the specified values.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object being created.</param>
	/// <param name="type">The fully qualified type of the event class to use.</param>
	/// <param name="startEventCode">The starting event code range.</param>
	/// <param name="endEventCode">The ending event code range.</param>
	public EventMappingSettings(string name, string type, int startEventCode, int endEventCode)
	{
		Name = name;
		Type = type;
		StartEventCode = startEventCode;
		EndEventCode = endEventCode;
	}
}
