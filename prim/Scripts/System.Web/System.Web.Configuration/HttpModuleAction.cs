using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the <see cref="T:System.Web.Configuration.HttpModulesSection" /> modules. This class cannot be inherited.</summary>
public sealed class HttpModuleAction : ConfigurationElement
{
	private static ConfigurationPropertyCollection properties;

	private static ConfigurationProperty nameProp;

	private static ConfigurationProperty typeProp;

	private static ConfigurationElementProperty elementProperty;

	protected override ConfigurationElementProperty ElementProperty
	{
		protected internal get
		{
			return elementProperty;
		}
	}

	/// <summary>Gets or sets the module name.</summary>
	/// <returns>The module name.</returns>
	[StringValidator(MinLength = 1)]
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

	/// <summary>Gets or sets the module type.</summary>
	/// <returns>A comma-separated list containing the module type name and the assembly information. </returns>
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

	static HttpModuleAction()
	{
		nameProp = new ConfigurationProperty("name", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		typeProp = new ConfigurationProperty("type", typeof(string), "hoho", ConfigurationPropertyOptions.IsRequired);
		properties = new ConfigurationPropertyCollection();
		properties.Add(nameProp);
		properties.Add(typeProp);
		elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(HttpModuleAction), ValidateElement));
	}

	internal HttpModuleAction()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpModuleAction" /> class using the passed parameters.</summary>
	/// <param name="name">The module name.</param>
	/// <param name="type">A comma-separated list containing the module type name and the assembly information. </param>
	public HttpModuleAction(string name, string type)
	{
		Name = name;
		Type = type;
	}

	private static void ValidateElement(object o)
	{
	}
}
