using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Retrieves a dynamic resource during compilation.</summary>
public sealed class ExpressionBuilder : ConfigurationElement
{
	private static ConfigurationProperty expressionPrefixProp;

	private static ConfigurationProperty typeProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets a string that identifies the type of expression to retrieve.</summary>
	/// <returns>A string that identifies the type of expression to retrieve.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("expressionPrefix", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
	public string ExpressionPrefix
	{
		get
		{
			return (string)base[expressionPrefixProp];
		}
		set
		{
			base[expressionPrefixProp] = value;
		}
	}

	/// <summary>Gets or sets a string that specifies the expression type.</summary>
	/// <returns>A string that specifies the expression type.</returns>
	[StringValidator(MinLength = 1)]
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

	internal Type TypeInternal => System.Type.GetType(Type, throwOnError: true);

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static ExpressionBuilder()
	{
		expressionPrefixProp = new ConfigurationProperty("expressionPrefix", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		typeProp = new ConfigurationProperty("type", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);
		properties = new ConfigurationPropertyCollection();
		properties.Add(expressionPrefixProp);
		properties.Add(typeProp);
	}

	internal ExpressionBuilder()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ExpressionBuilder" /> class. </summary>
	/// <param name="expressionPrefix">A string that identifies the type of expression to retrieve.</param>
	/// <param name="theType">A string that specifies the expression type.</param>
	public ExpressionBuilder(string expressionPrefix, string theType)
	{
		ExpressionPrefix = expressionPrefix;
		Type = theType;
	}
}
