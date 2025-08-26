using System.ComponentModel;
using System.Configuration;

namespace System.Web.Services.Configuration;

/// <summary>Represents the SoapExtensionElement in the Web Services configuration file. This element adds a SOAP extension to run with all XML Web services within the scope of the configuration file. The class cannot be inherited.</summary>
public sealed class SoapExtensionTypeElement : ConfigurationElement
{
	private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

	private readonly ConfigurationProperty group = new ConfigurationProperty("group", typeof(PriorityGroup), PriorityGroup.Low, new EnumConverter(typeof(PriorityGroup)), null, ConfigurationPropertyOptions.IsKey);

	private readonly ConfigurationProperty priority = new ConfigurationProperty("priority", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue), ConfigurationPropertyOptions.IsKey);

	private readonly ConfigurationProperty type = new ConfigurationProperty("type", typeof(Type), null, new TypeTypeConverter(), null, ConfigurationPropertyOptions.IsKey);

	/// <summary>Gets or sets the relative order in which a SOAP extension runs when multiple SOAP extensions are configured to run.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Configuration.PriorityGroup" /> object whose value determines relative order in which a SOAP extension runs.</returns>
	[ConfigurationProperty("group", IsKey = true, DefaultValue = PriorityGroup.Low)]
	public PriorityGroup Group
	{
		get
		{
			return (PriorityGroup)base[group];
		}
		set
		{
			if (Enum.IsDefined(typeof(PriorityGroup), value))
			{
				base[group] = value;
				return;
			}
			throw new ArgumentException(Res.GetString("Invalid_priority_group_value"), "value");
		}
	}

	/// <summary>Gets or sets the value that indicates the relative order in which a SOAP extension runs when multiple SOAP extensions are specified.</summary>
	/// <returns>A <see cref="T:System.Int32" /> whose value determines relative order in which a SOAP extension runs.</returns>
	[ConfigurationProperty("priority", IsKey = true, DefaultValue = 0)]
	[IntegerValidator(MinValue = 0)]
	public int Priority
	{
		get
		{
			return (int)base[priority];
		}
		set
		{
			base[priority] = value;
		}
	}

	/// <summary>Gets or sets the SOAP extension class to add to the SoapExtensionType element of the Web Services configuration file.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains the type name of the SoapExtensionType element.</returns>
	[ConfigurationProperty("type", IsKey = true)]
	[TypeConverter(typeof(TypeTypeConverter))]
	public Type Type
	{
		get
		{
			return (Type)base[type];
		}
		set
		{
			base[type] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties => properties;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> class.</summary>
	public SoapExtensionTypeElement()
	{
		properties.Add(group);
		properties.Add(priority);
		properties.Add(type);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> class.</summary>
	/// <param name="type">Specifies the SOAP extension class to add.</param>
	/// <param name="priority">Indicates the relative order in which a SOAP extension runs when multiple SOAP extensions are specified. Within each group, the priority attribute distinguishes the overall relative priority of the SOAP extension. A lower priority number indicates a higher priority for the SOAP extension. The lowest possible value for the priority attribute is 1.</param>
	/// <param name="group">Along with priority, specifies the relative order in which a SOAP extension runs when multiple SOAP extensions are configured to run.</param>
	public SoapExtensionTypeElement(string type, int priority, PriorityGroup group)
		: this()
	{
		Type = Type.GetType(type, throwOnError: true, ignoreCase: true);
		Priority = priority;
		Group = group;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> class.</summary>
	/// <param name="type">Specifies the SOAP extension class to add.</param>
	/// <param name="priority">Indicates the relative order in which a SOAP extension runs when multiple SOAP extensions are specified.</param>
	/// <param name="group">Along with priority, specifies the relative order in which a SOAP extension runs when multiple SOAP extensions are configured to run.</param>
	public SoapExtensionTypeElement(Type type, int priority, PriorityGroup group)
		: this(type.AssemblyQualifiedName, priority, group)
	{
	}
}
