using System.Configuration;
using System.Configuration.Provider;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Security;

namespace System.Web.Profile;

/// <summary>Provides untyped access to profile property values and information.</summary>
public class ProfileBase : SettingsBase
{
	private bool _propertiyValuesLoaded;

	private bool _dirty;

	private DateTime _lastActivityDate = DateTime.MinValue;

	private DateTime _lastUpdatedDate = DateTime.MinValue;

	private SettingsContext _settingsContext;

	private SettingsPropertyValueCollection _propertiyValues;

	private const string Profiles_SettingsPropertyCollection = "Profiles.SettingsPropertyCollection";

	private static SettingsPropertyCollection _properties;

	/// <summary>Gets or sets a profile property value indexed by the property name.</summary>
	/// <param name="propertyName">The name of the profile property.</param>
	/// <returns>The value of the specified profile property, typed as <see langword="object" />.</returns>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">An attempt was made to set a property value on an anonymous profile where the property's <see langword="allowAnonymous" /> attribute is <see langword="false" />.</exception>
	/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">There are no properties defined for the current profile.-or-The specified profile property name does not exist in the current profile.-or-The provider for the specified profile property did not recognize the specified property.</exception>
	/// <exception cref="T:System.Configuration.SettingsPropertyIsReadOnlyException">An attempt was made to set a property value that was marked as read-only.</exception>
	/// <exception cref="T:System.Configuration.SettingsPropertyWrongTypeException">An attempt was made to assign a value to a property using an incompatible type.</exception>
	public override object this[string propertyName]
	{
		get
		{
			return GetPropertyValue(propertyName);
		}
		set
		{
			SetPropertyValue(propertyName, value);
		}
	}

	/// <summary>Gets a value indicating whether the user profile is for an anonymous user.</summary>
	/// <returns>
	///     <see langword="true" /> if the user profile is for an anonymous user; otherwise, <see langword="false" />.</returns>
	public bool IsAnonymous => !(bool)_settingsContext["IsAuthenticated"];

	/// <summary>Gets a value indicating whether any of the profile properties have been modified.</summary>
	/// <returns>
	///     <see langword="true" /> if any of the profile properties have been modified; otherwise, <see langword="false" />.</returns>
	public bool IsDirty => _dirty;

	/// <summary>Gets the most recent date and time that the profile was read or modified.</summary>
	/// <returns>The most recent date and time that the profile was read or modified by the default provider.</returns>
	public DateTime LastActivityDate => _lastActivityDate;

	/// <summary>Gets the most recent date and time that the profile was modified.</summary>
	/// <returns>The most recent date and time that the profile was modified by the default provider.</returns>
	public DateTime LastUpdatedDate => _lastUpdatedDate;

	/// <summary>Gets a collection of <see cref="T:System.Configuration.SettingsProperty" /> objects for each property in the profile.</summary>
	/// <returns>A <see cref="T:System.Configuration.SettingsPropertyCollection" /> of <see cref="T:System.Configuration.SettingsProperty" /> objects for each property in the profile for the application.</returns>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A property type specified in the  section of the Web.config file could not be created.-or-The <see langword="allowAnonymous" /> attribute for a property in the  section of the Web.config file is set to <see langword="true" /> and the <see langword="enabled" /> attribute of the  element is set to <see langword="false" />.-or-The <see langword="serializeAs" /> attribute for a property in the  section of the Web.config file is set to <see cref="F:System.Configuration.SettingsSerializeAs.Binary" /> and the <see cref="P:System.Type.IsSerializable" /> property of the specified <see langword="type" /> returns <see langword="false" />.-or-The name of a provider specified using the <see langword="provider" /> attribute of a profile property could not be found in the <see cref="P:System.Web.Profile.ProfileManager.Providers" /> collection.-or-The <see langword="type" /> specified for a profile property could not be found.-or-A profile property was specified with a name that matches a property name on the base class specified in the <see langword="inherits" /> attribute of the  section.</exception>
	public new static SettingsPropertyCollection Properties
	{
		get
		{
			if (_properties == null)
			{
				InitProperties();
			}
			return _properties;
		}
	}

	/// <summary>Gets the user name for the profile.</summary>
	/// <returns>The user name for the profile or the anonymous-user identifier assigned to the profile.</returns>
	public string UserName => (string)_settingsContext["UserName"];

	private static void InitProperties()
	{
		SettingsPropertyCollection settingsPropertyCollection = new SettingsPropertyCollection();
		ProfileSection profileSection = (ProfileSection)WebConfigurationManager.GetSection("system.web/profile");
		RootProfilePropertySettingsCollection propertySettings = profileSection.PropertySettings;
		for (int i = 0; i < propertySettings.GroupSettings.Count; i++)
		{
			ProfileGroupSettings profileGroupSettings = propertySettings.GroupSettings[i];
			ProfilePropertySettingsCollection propertySettings2 = profileGroupSettings.PropertySettings;
			for (int j = 0; j < propertySettings2.Count; j++)
			{
				SettingsProperty settingsProperty = CreateSettingsProperty(profileGroupSettings, propertySettings2[j]);
				ValidateProperty(settingsProperty, propertySettings2[j].ElementInformation);
				settingsPropertyCollection.Add(settingsProperty);
			}
		}
		for (int k = 0; k < propertySettings.Count; k++)
		{
			SettingsProperty settingsProperty2 = CreateSettingsProperty(null, propertySettings[k]);
			ValidateProperty(settingsProperty2, propertySettings[k].ElementInformation);
			settingsPropertyCollection.Add(settingsProperty2);
		}
		if (profileSection.Inherits.Length > 0)
		{
			Type profileCommonType = ProfileParser.GetProfileCommonType(HttpContext.Current);
			if (profileCommonType != null)
			{
				Type baseType = profileCommonType.BaseType;
				while (true)
				{
					PropertyInfo[] array = baseType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
					if (array.Length != 0)
					{
						for (int l = 0; l < array.Length; l++)
						{
							settingsPropertyCollection.Add(CreateSettingsProperty(array[l]));
						}
					}
					if (baseType.BaseType == null || baseType.BaseType == typeof(ProfileBase))
					{
						break;
					}
					baseType = baseType.BaseType;
				}
			}
		}
		settingsPropertyCollection.SetReadOnly();
		lock ("Profiles.SettingsPropertyCollection")
		{
			if (_properties == null)
			{
				_properties = settingsPropertyCollection;
			}
		}
	}

	/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileBase" /> class.</summary>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">The <see langword="enabled" /> attribute of the  section of the Web.config file is <see langword="false" />.</exception>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A property type specified in the  section of the Web.config file could not be created.-or-The <see langword="allowAnonymous" /> attribute for a property in the  section of the Web.config file is set to <see langword="true" /> and the <see langword="enabled" /> attribute of the  element is set to <see langword="false" />.-or-The <see langword="serializeAs" /> attribute for a property in the  section of the Web.config file is set to <see cref="F:System.Configuration.SettingsSerializeAs.Binary" /> and the <see cref="P:System.Type.IsSerializable" /> property of the specified <see langword="type" /> returns <see langword="false" />.-or-The name of a provider specified using the <see langword="provider" /> attribute of a profile property could not be found in the <see cref="P:System.Web.Profile.ProfileManager.Providers" /> collection.-or-The <see langword="type" /> specified for a profile property could not be found.-or-A profile property was specified with a name that matches a property name on the base class specified in the <see langword="inherits" /> attribute of the  section.</exception>
	public ProfileBase()
	{
	}

	/// <summary>Used by ASP.NET to create an instance of a profile for the specified user name.</summary>
	/// <param name="username">The name of the user to create a profile for.</param>
	/// <returns>An <see cref="T:System.Web.Profile.ProfileBase" /> that represents the profile for the specified user.</returns>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">The <see langword="enabled" /> attribute of the  section of the Web.config file is <see langword="false" />.</exception>
	/// <exception cref="T:System.Web.HttpException">The current hosting permission level is less than <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" />.</exception>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A property type specified in the  section of the Web.config file could not be created.-or-The <see langword="allowAnonymous" /> attribute for a property in the  section of the Web.config file is set to <see langword="true" /> and the <see langword="enabled" /> attribute of the  element is set to <see langword="false" />.-or-The <see langword="serializeAs" /> attribute for a property in the  section of the Web.config file is set to <see cref="F:System.Configuration.SettingsSerializeAs.Binary" /> and the <see cref="P:System.Type.IsSerializable" /> property of the specified <see langword="type" /> returns <see langword="false" />.-or-The name of a provider specified using the <see langword="provider" /> attribute of a profile property could not be found in the <see cref="P:System.Web.Profile.ProfileManager.Providers" /> collection.-or-The <see langword="type" /> specified for a profile property could not be found.-or-A profile property was specified with a name that matches a property name on the base class specified in the <see langword="inherits" /> attribute of the  section.</exception>
	public static ProfileBase Create(string username)
	{
		return Create(username, isAuthenticated: true);
	}

	/// <summary>Used by ASP.NET to create an instance of a profile for the specified user name. Takes a parameter indicating whether the user is authenticated or anonymous.</summary>
	/// <param name="username">The name of the user to create a profile for.</param>
	/// <param name="isAuthenticated">
	///       <see langword="true" /> to indicate the user is authenticated; <see langword="false" /> to indicate the user is anonymous.</param>
	/// <returns>A <see cref="T:System.Web.Profile.ProfileBase" /> object that represents the profile for the specified user.</returns>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">The <see langword="enabled" /> attribute of the  section of the Web.config file is <see langword="false" />.</exception>
	/// <exception cref="T:System.Web.HttpException">The current hosting permission level is less than <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" />.</exception>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A property type specified in the  section of the Web.config file could not be created.-or-The <see langword="allowAnonymous" /> attribute for a property in the  section of the Web.config file is set to <see langword="true" /> and the <see langword="enabled" /> attribute of the  element is set to <see langword="false" />.-or-The <see langword="serializeAs" /> attribute for a property in the  section of the Web.config file is set to <see cref="F:System.Configuration.SettingsSerializeAs.Binary" /> and the <see cref="P:System.Type.IsSerializable" /> property of the specified <see langword="type" /> returns <see langword="false" />.-or-The name of a provider specified using the <see langword="provider" /> attribute of a profile property could not be found in the <see cref="P:System.Web.Profile.ProfileManager.Providers" /> collection.-or-The <see langword="type" /> specified for a profile property could not be found.-or-A profile property was specified with a name that matches a property name on the base class specified in the <see langword="inherits" /> attribute of the  section.</exception>
	public static ProfileBase Create(string username, bool isAuthenticated)
	{
		ProfileBase profileBase = null;
		Type profileCommonType = ProfileParser.GetProfileCommonType(HttpContext.Current);
		profileBase = ((!(profileCommonType != null)) ? new DefaultProfile() : ((ProfileBase)Activator.CreateInstance(profileCommonType)));
		profileBase.Initialize(username, isAuthenticated);
		return profileBase;
	}

	/// <summary>Gets a group of properties identified by a group name.</summary>
	/// <param name="groupName">The name of the group of properties.</param>
	/// <returns>A <see cref="T:System.Web.Profile.ProfileGroupBase" /> object for a group of properties configured with the specified group name.</returns>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">The specified profile property group name was not found in the  configuration section.</exception>
	public ProfileGroupBase GetProfileGroup(string groupName)
	{
		ProfileGroupBase profileGroupBase = null;
		Type profileGroupType = ProfileParser.GetProfileGroupType(HttpContext.Current, groupName);
		if (profileGroupType != null)
		{
			profileGroupBase = (ProfileGroupBase)Activator.CreateInstance(profileGroupType);
			profileGroupBase.Init(this, groupName);
			return profileGroupBase;
		}
		throw new ProviderException("Group '" + groupName + "' not found");
	}

	/// <summary>Gets the value of a profile property.</summary>
	/// <param name="propertyName">The name of the profile property.</param>
	/// <returns>The value of the specified profile property, typed as <see langword="object" />.</returns>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">An attempt was made to set a property value on an anonymous profile where the property's <see langword="allowAnonymous" /> attribute is <see langword="false" />.</exception>
	/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">There are no properties defined for the current profile.-or-The specified profile property name does not exist in the current profile.-or-The provider for the specified profile property did not recognize the specified property.</exception>
	public object GetPropertyValue(string propertyName)
	{
		if (!_propertiyValuesLoaded)
		{
			InitPropertiesValues();
		}
		_lastActivityDate = DateTime.UtcNow;
		return _propertiyValues[propertyName].PropertyValue;
	}

	/// <summary>Sets the value of a profile property.</summary>
	/// <param name="propertyName">The name of the property to set.</param>
	/// <param name="propertyValue">The value to assign to the property.</param>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">An attempt was made to set a property value on an anonymous profile where the property's <see langword="allowAnonymous" /> attribute is <see langword="false" />.</exception>
	/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">There are no properties defined for the current profile.-or-The specified profile property name does not exist in the current profile.-or-The provider for the specified profile property did not recognize the specified property.</exception>
	/// <exception cref="T:System.Configuration.SettingsPropertyIsReadOnlyException">An attempt was made to set a value value on a property that was marked as read-only.</exception>
	/// <exception cref="T:System.Configuration.SettingsPropertyWrongTypeException">An attempt was made to assign a value to a property using an incompatible type.</exception>
	public void SetPropertyValue(string propertyName, object propertyValue)
	{
		if (!_propertiyValuesLoaded)
		{
			InitPropertiesValues();
		}
		if (_propertiyValues[propertyName] == null)
		{
			throw new SettingsPropertyNotFoundException("The settings property '" + propertyName + "' was not found.");
		}
		if (!(bool)_propertiyValues[propertyName].Property.Attributes["AllowAnonymous"] && IsAnonymous)
		{
			throw new ProviderException("This property cannot be set for anonymous users.");
		}
		_propertiyValues[propertyName].PropertyValue = propertyValue;
		_dirty = true;
		_lastActivityDate = DateTime.UtcNow;
		_lastUpdatedDate = _lastActivityDate;
	}

	private void InitPropertiesValues()
	{
		if (!_propertiyValuesLoaded)
		{
			_propertiyValues = ProfileManager.Provider.GetPropertyValues(_settingsContext, Properties);
			_propertiyValuesLoaded = true;
		}
	}

	private static Type GetPropertyType(ProfileGroupSettings pgs, ProfilePropertySettings pps)
	{
		Type type = HttpApplication.LoadType(pps.Type);
		if (type != null)
		{
			return type;
		}
		Type type2 = null;
		type2 = ((pgs != null) ? ProfileParser.GetProfileGroupType(HttpContext.Current, pgs.Name) : ProfileParser.GetProfileCommonType(HttpContext.Current));
		if (type2 == null)
		{
			return null;
		}
		PropertyInfo property = type2.GetProperty(pps.Name);
		if (property != null)
		{
			return property.PropertyType;
		}
		return null;
	}

	private static void ValidateProperty(SettingsProperty settingsProperty, ElementInformation elementInfo)
	{
		string text = string.Empty;
		if (!AnonymousIdentificationModule.Enabled && (bool)settingsProperty.Attributes["AllowAnonymous"])
		{
			text = "Profile property '{0}' allows anonymous users to store data. This requires that the AnonymousIdentification feature be enabled.";
		}
		if (settingsProperty.PropertyType == null)
		{
			text = "The type specified for a profile property '{0}' could not be found.";
		}
		if (settingsProperty.SerializeAs == SettingsSerializeAs.Binary && !settingsProperty.PropertyType.IsSerializable)
		{
			text = "The type for the property '{0}' cannot be serialized using the binary serializer, since the type is not marked as serializable.";
		}
		if (text.Length > 0)
		{
			throw new ConfigurationErrorsException(string.Format(text, settingsProperty.Name), elementInfo.Source, elementInfo.LineNumber);
		}
	}

	private static SettingsProperty CreateSettingsProperty(PropertyInfo property)
	{
		SettingsProperty settingsProperty = new SettingsProperty(property.Name);
		Attribute[] array = (Attribute[])property.GetCustomAttributes(inherit: false);
		SettingsAttributeDictionary settingsAttributeDictionary = new SettingsAttributeDictionary();
		bool flag = false;
		settingsProperty.SerializeAs = SettingsSerializeAs.ProviderSpecific;
		settingsProperty.PropertyType = property.PropertyType;
		settingsProperty.IsReadOnly = false;
		settingsProperty.ThrowOnErrorDeserializing = false;
		settingsProperty.ThrowOnErrorSerializing = true;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] is DefaultSettingValueAttribute)
			{
				settingsProperty.DefaultValue = ((DefaultSettingValueAttribute)array[i]).Value;
				flag = true;
			}
			else if (array[i] is SettingsProviderAttribute)
			{
				Type type = HttpApplication.LoadType(((SettingsProviderAttribute)array[i]).ProviderTypeName);
				settingsProperty.Provider = (SettingsProvider)Activator.CreateInstance(type);
				settingsProperty.Provider.Initialize(null, null);
			}
			else if (array[i] is SettingsSerializeAsAttribute)
			{
				settingsProperty.SerializeAs = ((SettingsSerializeAsAttribute)array[i]).SerializeAs;
			}
			else if (array[i] is SettingsAllowAnonymousAttribute)
			{
				settingsProperty.Attributes["AllowAnonymous"] = ((SettingsAllowAnonymousAttribute)array[i]).Allow;
			}
			else if (array[i] is CustomProviderDataAttribute)
			{
				settingsProperty.Attributes["CustomProviderData"] = ((CustomProviderDataAttribute)array[i]).CustomProviderData;
			}
			else if (array[i] is ApplicationScopedSettingAttribute || array[i] is UserScopedSettingAttribute || array[i] is SettingsDescriptionAttribute || array[i] is SettingAttribute)
			{
				settingsAttributeDictionary.Add(array[i].GetType(), array[i]);
			}
		}
		if (settingsProperty.Provider == null)
		{
			settingsProperty.Provider = ProfileManager.Provider;
		}
		if (settingsProperty.Attributes["AllowAnonymous"] == null)
		{
			settingsProperty.Attributes["AllowAnonymous"] = false;
		}
		if (!flag && settingsProperty.PropertyType == typeof(string) && settingsProperty.DefaultValue == null)
		{
			settingsProperty.DefaultValue = string.Empty;
		}
		return settingsProperty;
	}

	private static SettingsProperty CreateSettingsProperty(ProfileGroupSettings pgs, ProfilePropertySettings pps)
	{
		SettingsProperty settingsProperty = new SettingsProperty(((pgs == null) ? string.Empty : (pgs.Name + ".")) + pps.Name);
		settingsProperty.Attributes.Add("AllowAnonymous", pps.AllowAnonymous);
		settingsProperty.DefaultValue = pps.DefaultValue;
		settingsProperty.IsReadOnly = pps.ReadOnly;
		settingsProperty.Provider = ProfileManager.Provider;
		settingsProperty.ThrowOnErrorDeserializing = false;
		settingsProperty.ThrowOnErrorSerializing = true;
		if (pps.Type.Length == 0 || pps.Type == "string")
		{
			settingsProperty.PropertyType = typeof(string);
		}
		else
		{
			settingsProperty.PropertyType = GetPropertyType(pgs, pps);
		}
		switch (pps.SerializeAs)
		{
		case SerializationMode.Binary:
			settingsProperty.SerializeAs = SettingsSerializeAs.Binary;
			break;
		case SerializationMode.ProviderSpecific:
			settingsProperty.SerializeAs = SettingsSerializeAs.ProviderSpecific;
			break;
		case SerializationMode.String:
			settingsProperty.SerializeAs = SettingsSerializeAs.String;
			break;
		case SerializationMode.Xml:
			settingsProperty.SerializeAs = SettingsSerializeAs.Xml;
			break;
		}
		return settingsProperty;
	}

	/// <summary>Initializes the profile property values and information for the current user.</summary>
	/// <param name="username">The name of the user to initialize the profile for.</param>
	/// <param name="isAuthenticated">
	///       <see langword="true" /> to indicate the user is authenticated; <see langword="false" /> to indicate the user is anonymous.</param>
	public void Initialize(string username, bool isAuthenticated)
	{
		_settingsContext = new SettingsContext();
		_settingsContext.Add("UserName", username);
		_settingsContext.Add("IsAuthenticated", isAuthenticated);
		SettingsProviderCollection settingsProviderCollection = new SettingsProviderCollection();
		settingsProviderCollection.Add(ProfileManager.Provider);
		Initialize(Context, Properties, settingsProviderCollection);
	}

	/// <summary>Updates the profile data source with changed profile property values.</summary>
	public override void Save()
	{
		if (IsDirty)
		{
			ProfileManager.Provider.SetPropertyValues(_settingsContext, _propertiyValues);
		}
	}
}
