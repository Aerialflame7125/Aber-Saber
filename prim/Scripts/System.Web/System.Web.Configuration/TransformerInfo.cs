using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Specifies a custom class that extends the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class for use by Web Part connections.</summary>
public sealed class TransformerInfo : ConfigurationElement
{
	private static ConfigurationProperty nameProp;

	private static ConfigurationProperty typeProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets a friendly name for a type that that extends the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</summary>
	/// <returns>A friendly name for a type that that extends the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</returns>
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

	/// <summary>Gets or sets the type reference for a class that extends the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</summary>
	/// <returns>A type reference for a class that extends the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</returns>
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

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static TransformerInfo()
	{
		nameProp = new ConfigurationProperty("name", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		typeProp = new ConfigurationProperty("type", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);
		properties = new ConfigurationPropertyCollection();
		properties.Add(nameProp);
		properties.Add(typeProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.TransformerInfo" /> class with the specified name and type reference.</summary>
	/// <param name="name">The name of this transformer type.</param>
	/// <param name="type">A reference to a type that extends the transformer <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</param>
	public TransformerInfo(string name, string type)
	{
		Name = name;
		Type = type;
	}

	/// <summary>Compares the current <see cref="T:System.Web.Configuration.TransformerInfo" /> object to another <see cref="T:System.Web.Configuration.TransformerInfo" /> object.</summary>
	/// <param name="o">The object to compare to the current object.</param>
	/// <returns>
	///     <see langword="true" /> if the passed object is equal to the current object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object o)
	{
		TransformerInfo transformerInfo = o as TransformerInfo;
		if (Name == transformerInfo.Name)
		{
			return Type == transformerInfo.Type;
		}
		return false;
	}

	/// <summary>Generates a hash code for the collection.</summary>
	/// <returns>Unique integer hash code for the current object.</returns>
	public override int GetHashCode()
	{
		return Name.GetHashCode() + Type.GetHashCode();
	}
}
