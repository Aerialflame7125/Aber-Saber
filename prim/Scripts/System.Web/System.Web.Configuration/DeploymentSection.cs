using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Defines a configuration setting that is typically used on a production server to override application-level settings that are appropriate only on development computers. </summary>
public sealed class DeploymentSection : ConfigurationSection
{
	private static ConfigurationProperty retailProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets a value that specifies whether Web applications on the computer are deployed in <see langword="retail" /> mode.</summary>
	/// <returns>
	///     <see langword="true" /> if Web applications are deployed in <see langword="retail" /> mode; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("retail", DefaultValue = "False")]
	public bool Retail
	{
		get
		{
			return (bool)base[retailProp];
		}
		set
		{
			base[retailProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static DeploymentSection()
	{
		retailProp = new ConfigurationProperty("retail", typeof(bool), false);
		properties = new ConfigurationPropertyCollection();
		properties.Add(retailProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.DeploymentSection" /> class.</summary>
	public DeploymentSection()
	{
	}
}
