using System.Configuration;

namespace System.Web.Services.Configuration;

/// <summary>Represents the <see langword="WsiProfiles" /> element in the configuration file.</summary>
public sealed class WsiProfilesElement : ConfigurationElement
{
	private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

	private readonly ConfigurationProperty name = new ConfigurationProperty("name", typeof(WsiProfiles), WsiProfiles.None, ConfigurationPropertyOptions.IsKey);

	/// <summary>Gets or sets whether the Web service conforms to the WSI Basic Profile version 1.1.</summary>
	/// <returns>A <see cref="T:System.Web.Services.WsiProfiles" /> object that specifies whether the Web service conforms to the WSI Basic Profile version 1.1.</returns>
	[ConfigurationProperty("name", IsKey = true, DefaultValue = WsiProfiles.None)]
	public WsiProfiles Name
	{
		get
		{
			return (WsiProfiles)base[name];
		}
		set
		{
			if (!IsValidWsiProfilesValue(value))
			{
				throw new ArgumentOutOfRangeException("value");
			}
			base[name] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties => properties;

	/// <summary>Initializes and instance of the <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> class.</summary>
	public WsiProfilesElement()
	{
		properties.Add(name);
	}

	/// <summary>Initializes and instance of the <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> class, using the specified <see cref="T:System.Web.Services.WsiProfiles" /> enumeration value.</summary>
	/// <param name="name">A <see cref="T:System.Web.Services.WsiProfiles" /> object that specifies whether the Web service conforms to the WSI Basic Profile version 1.1.</param>
	public WsiProfilesElement(WsiProfiles name)
		: this()
	{
		Name = name;
	}

	private bool IsValidWsiProfilesValue(WsiProfiles value)
	{
		return Enum.IsDefined(typeof(WsiProfiles), value);
	}
}
