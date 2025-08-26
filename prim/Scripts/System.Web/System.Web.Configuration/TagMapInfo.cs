using System.ComponentModel;
using System.Configuration;
using System.Xml;

namespace System.Web.Configuration;

/// <summary>Contains a single configuration tag remapping statement. This class cannot be inherited.</summary>
public sealed class TagMapInfo : ConfigurationElement
{
	private static ConfigurationPropertyCollection properties;

	private static ConfigurationProperty mappedTagTypeProp;

	private static ConfigurationProperty tagTypeProp;

	/// <summary>Gets or sets the name of the type to which the tag is remapped.</summary>
	/// <returns>The name of the type to which the tag is remapped. The default is an empty string.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("mappedTagType")]
	public string MappedTagType
	{
		get
		{
			return (string)base[mappedTagTypeProp];
		}
		set
		{
			base[mappedTagTypeProp] = value;
		}
	}

	/// <summary>Gets or sets the name of the original type for the tag that is being remapped.</summary>
	/// <returns>The name of the original type for the tag that is being remapped. </returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("tagType", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
	public string TagType
	{
		get
		{
			return (string)base[tagTypeProp];
		}
		set
		{
			base[tagTypeProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static TagMapInfo()
	{
		mappedTagTypeProp = new ConfigurationProperty("mappedTagType", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);
		tagTypeProp = new ConfigurationProperty("tagType", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		properties = new ConfigurationPropertyCollection();
		properties.Add(mappedTagTypeProp);
		properties.Add(tagTypeProp);
	}

	internal TagMapInfo()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.TagMapInfo" /> class based on the passed property values.</summary>
	/// <param name="tagTypeName">The fully qualified name of the type for the tag that is being remapped.</param>
	/// <param name="mappedTagTypeName">The name of the type to which the tag is remapped, along with the supporting details.</param>
	public TagMapInfo(string tagTypeName, string mappedTagTypeName)
	{
		TagType = tagTypeName;
		MappedTagType = mappedTagTypeName;
	}

	/// <summary>Compares this instance to another object.</summary>
	/// <param name="o">Object to compare.</param>
	/// <returns>
	///     <see langword="true" /> if the objects are identical; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object o)
	{
		if (!(o is TagMapInfo tagMapInfo))
		{
			return false;
		}
		if (MappedTagType == tagMapInfo.MappedTagType)
		{
			return TagType == tagMapInfo.TagType;
		}
		return false;
	}

	/// <summary>Returns a hash value for the current instance.</summary>
	/// <returns>A hash value for the current instance.</returns>
	public override int GetHashCode()
	{
		return MappedTagType.GetHashCode() + TagType.GetHashCode();
	}

	protected internal override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
	{
		return base.SerializeElement(writer, serializeCollectionKey);
	}
}
