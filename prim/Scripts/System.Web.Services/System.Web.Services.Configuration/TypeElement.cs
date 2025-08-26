using System.ComponentModel;
using System.Configuration;

namespace System.Web.Services.Configuration;

/// <summary>Represents the <see langword="type" /> element in the Web services configuration file.</summary>
public sealed class TypeElement : ConfigurationElement
{
	private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

	private readonly ConfigurationProperty type = new ConfigurationProperty("type", typeof(TypeAndName), null, new TypeAndNameConverter(), null, ConfigurationPropertyOptions.IsKey);

	/// <summary>Gets or sets the type of the configuration attribute.</summary>
	/// <returns>The type of the configuration attribute.</returns>
	[ConfigurationProperty("type", IsKey = true)]
	[TypeConverter(typeof(TypeAndNameConverter))]
	public Type Type
	{
		get
		{
			return ((TypeAndName)base[type]).type;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base[type] = new TypeAndName(value);
		}
	}

	protected override ConfigurationPropertyCollection Properties => properties;

	/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Configuration.TypeElement" /> class.</summary>
	public TypeElement()
	{
		properties.Add(type);
	}

	/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Configuration.TypeElement" /> class.</summary>
	/// <param name="type">The type of the configuration attribute.</param>
	public TypeElement(string type)
		: this()
	{
		base[this.type] = new TypeAndName(type);
	}

	/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Configuration.TypeElement" /> class.</summary>
	/// <param name="type">The type of the configuration attribute.</param>
	public TypeElement(Type type)
		: this(type.AssemblyQualifiedName)
	{
	}
}
