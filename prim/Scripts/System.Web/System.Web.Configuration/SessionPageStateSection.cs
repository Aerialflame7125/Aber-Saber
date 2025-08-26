using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the <see langword="sessionPageState" /> section. This class cannot be inherited. </summary>
public sealed class SessionPageStateSection : ConfigurationSection
{
	private static ConfigurationProperty historySizeProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Defines the size of the page history.</summary>
	public const int DefaultHistorySize = 9;

	/// <summary>Gets or sets the size of the page history.</summary>
	/// <returns>The size of the page history.</returns>
	[IntegerValidator(MinValue = 1, MaxValue = int.MaxValue)]
	[ConfigurationProperty("historySize", DefaultValue = "9")]
	public int HistorySize
	{
		get
		{
			return (int)base[historySizeProp];
		}
		set
		{
			base[historySizeProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static SessionPageStateSection()
	{
		historySizeProp = new ConfigurationProperty("historySize", typeof(int), 9, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None);
		properties = new ConfigurationPropertyCollection();
		properties.Add(historySizeProp);
	}

	/// <summary>Creates a new instance of <see cref="T:System.Web.Configuration.SessionPageStateSection" />.</summary>
	public SessionPageStateSection()
	{
	}
}
