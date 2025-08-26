using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>The <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> class provides a way to programmatically access and modify the <see langword="profiles" /> section of a configuration file. This class cannot be inherited.</summary>
public sealed class ProfilePropertySettings : ConfigurationElement
{
	private static ConfigurationProperty allowAnonymousProp;

	private static ConfigurationProperty customProviderDataProp;

	private static ConfigurationProperty defaultValueProp;

	private static ConfigurationProperty nameProp;

	private static ConfigurationProperty providerProp;

	private static ConfigurationProperty readOnlyProp;

	private static ConfigurationProperty serializeAsProp;

	private static ConfigurationProperty typeProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets a value indicating whether the associated property in the dynamically generated <see langword="ProfileCommon" /> class can be set by anonymous users.</summary>
	/// <returns>
	///     <see langword="true" /> if the associated property in the <see langword="ProfileCommon" /> class can be set by anonymous users; otherwise, <see langword="false" />, indicating that anonymous users cannot change the property value. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("allowAnonymous", DefaultValue = false)]
	public bool AllowAnonymous
	{
		get
		{
			return (bool)base[allowAnonymousProp];
		}
		set
		{
			base[allowAnonymousProp] = value;
		}
	}

	/// <summary>Gets or sets a string of custom data for the profile property provider.</summary>
	/// <returns>A string of custom data for the profile property provider. The default is <see langword="null" />.</returns>
	[ConfigurationProperty("customProviderData", DefaultValue = "")]
	public string CustomProviderData
	{
		get
		{
			return (string)base[customProviderDataProp];
		}
		set
		{
			base[customProviderDataProp] = value;
		}
	}

	/// <summary>Gets or sets the default value used for the associated property in the dynamically generated <see langword="ProfileCommon" /> class. </summary>
	/// <returns>A string containing the default value used for the associated property in the dynamically generated <see langword="ProfileCommon" /> class. The default is an empty string ("").</returns>
	[ConfigurationProperty("defaultValue", DefaultValue = "")]
	public string DefaultValue
	{
		get
		{
			return (string)base[defaultValueProp];
		}
		set
		{
			base[defaultValueProp] = value;
		}
	}

	/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object and the associated property in the dynamically generated <see langword="ProfileCommon" /> class.</summary>
	/// <returns>A string containing the name of the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object. The default is <see langword="null" />.</returns>
	[ConfigurationProperty("name", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
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

	/// <summary>Gets or sets the name of a provider to use when serializing the named property.</summary>
	/// <returns>The name of a provider from the <see cref="P:System.Web.Configuration.ProfileSection.Providers" /> property, or an empty string (""). The default is an empty string.</returns>
	[ConfigurationProperty("provider", DefaultValue = "")]
	public string Provider
	{
		get
		{
			return (string)base[providerProp];
		}
		set
		{
			base[providerProp] = value;
		}
	}

	/// <summary>Gets or sets a value that determines whether the associated property in the dynamically generated <see langword="ProfileCommon" /> class is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> if the associated property in the <see langword="ProfileCommon" /> class is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("readOnly", DefaultValue = false)]
	public bool ReadOnly
	{
		get
		{
			return (bool)base[readOnlyProp];
		}
		set
		{
			base[readOnlyProp] = value;
		}
	}

	/// <summary>Gets or sets the serialization method used for the associated property in the dynamically generated <see langword="ProfileCommon" /> class.</summary>
	/// <returns>One of the <see cref="T:System.Web.Configuration.SerializationMode" /> values. The default is <see cref="F:System.Web.Configuration.SerializationMode.ProviderSpecific" />.</returns>
	[ConfigurationProperty("serializeAs", DefaultValue = "ProviderSpecific")]
	public SerializationMode SerializeAs
	{
		get
		{
			return (SerializationMode)base[serializeAsProp];
		}
		set
		{
			base[serializeAsProp] = value;
		}
	}

	/// <summary>Gets or sets the name of the type of the associated property in the dynamically generated <see langword="ProfileCommon" /> class.</summary>
	/// <returns>A valid, fully qualified type reference, or an empty string (""). The default is an empty string.</returns>
	[ConfigurationProperty("type", DefaultValue = "string")]
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

	static ProfilePropertySettings()
	{
		allowAnonymousProp = new ConfigurationProperty("allowAnonymous", typeof(bool), false);
		customProviderDataProp = new ConfigurationProperty("customProviderData", typeof(string), "");
		defaultValueProp = new ConfigurationProperty("defaultValue", typeof(string), "");
		nameProp = new ConfigurationProperty("name", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), new ProfilePropertyNameValidator(), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		providerProp = new ConfigurationProperty("provider", typeof(string), "");
		readOnlyProp = new ConfigurationProperty("readOnly", typeof(bool), false);
		serializeAsProp = new ConfigurationProperty("serializeAs", typeof(SerializationMode), SerializationMode.ProviderSpecific, new GenericEnumConverter(typeof(SerializationMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		typeProp = new ConfigurationProperty("type", typeof(string), "string");
		properties = new ConfigurationPropertyCollection();
		properties.Add(allowAnonymousProp);
		properties.Add(customProviderDataProp);
		properties.Add(defaultValueProp);
		properties.Add(nameProp);
		properties.Add(providerProp);
		properties.Add(readOnlyProp);
		properties.Add(serializeAsProp);
		properties.Add(typeProp);
	}

	internal ProfilePropertySettings()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> class with the specified name.</summary>
	/// <param name="name">A unique name for the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object.</param>
	public ProfilePropertySettings(string name)
	{
		Name = name;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> class with the specified name and settings.</summary>
	/// <param name="name">A unique name for the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object.</param>
	/// <param name="readOnly">
	///       <see langword="true" /> to indicate that the associated property in the dynamically generated <see langword="ProfileCommon" /> class should be read-only; otherwise, <see langword="false" />.</param>
	/// <param name="serializeAs">One of the <see cref="T:System.Web.Configuration.SerializationMode" /> values.</param>
	/// <param name="providerName">The name of a provider from the <see cref="P:System.Web.Configuration.ProfileSection.Providers" /> property, or an empty string ("").</param>
	/// <param name="defaultValue">A string containing the default value used for the named property in the generated page Profile class.</param>
	/// <param name="profileType">A valid type reference or an empty string.</param>
	/// <param name="allowAnonymous">
	///       <see langword="true" /> to indicate associated property in the dynamically generated <see langword="ProfileCommon" /> class should support anonymous users; otherwise, <see langword="false" />, to indicate that anonymous users cannot change the named property.</param>
	/// <param name="customProviderData">A string containing provider-specific information used by the provider associated with the property.</param>
	public ProfilePropertySettings(string name, bool readOnly, SerializationMode serializeAs, string providerName, string defaultValue, string profileType, bool allowAnonymous, string customProviderData)
	{
		Name = name;
		ReadOnly = readOnly;
		SerializeAs = serializeAs;
		Provider = providerName;
		DefaultValue = defaultValue;
		Type = profileType;
		AllowAnonymous = allowAnonymous;
		CustomProviderData = customProviderData;
	}
}
