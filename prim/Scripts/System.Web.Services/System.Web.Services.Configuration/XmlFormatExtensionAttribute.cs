namespace System.Web.Services.Configuration;

/// <summary>Specifies that a service description format extension runs at one or more extension points. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class XmlFormatExtensionAttribute : Attribute
{
	private Type[] types;

	private string name;

	private string ns;

	/// <summary>The stages at which the service description format extension is to run.</summary>
	/// <returns>An array of <see cref="T:System.Type" /> that specifies the stage at which the service description format extension is to run.</returns>
	public Type[] ExtensionPoints
	{
		get
		{
			if (types != null)
			{
				return types;
			}
			return new Type[0];
		}
		set
		{
			types = value;
		}
	}

	/// <summary>Gets or sets the XML namespace for the XML element added to the service description by the service description format extension.</summary>
	/// <returns>The XML namespace for the XML element added to the service description by the service description format extension.</returns>
	public string Namespace
	{
		get
		{
			if (ns != null)
			{
				return ns;
			}
			return string.Empty;
		}
		set
		{
			ns = value;
		}
	}

	/// <summary>Gets or sets the XML element added to the service description by the service description format extension.</summary>
	/// <returns>The XML element added to the service description by the service description format extension.</returns>
	public string ElementName
	{
		get
		{
			if (name != null)
			{
				return name;
			}
			return string.Empty;
		}
		set
		{
			name = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.XmlFormatExtensionAttribute" /> class.</summary>
	public XmlFormatExtensionAttribute()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.XmlFormatExtensionAttribute" /> class that specifies the XML element and namespace to add when running at the specified extension point.</summary>
	/// <param name="elementName">The XML element added to the service description by the service description format extension.</param>
	/// <param name="ns">The XML namespace for the XML element added to the service description by the service description format extension.</param>
	/// <param name="extensionPoint1">The extension point at which to run the service description format extension.</param>
	public XmlFormatExtensionAttribute(string elementName, string ns, Type extensionPoint1)
		: this(elementName, ns, new Type[1] { extensionPoint1 })
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.XmlFormatExtensionAttribute" /> class that specifies the XML element and namespace to add when running at the specified extension points.</summary>
	/// <param name="elementName">The XML element added to the service description by the service description format extension.</param>
	/// <param name="ns">The XML namespace for the XML element added to the service description by the service description format extension.</param>
	/// <param name="extensionPoint1">An extension point at which to run the service description format extension.</param>
	/// <param name="extensionPoint2">An extension point at which to run the service description format extension.</param>
	public XmlFormatExtensionAttribute(string elementName, string ns, Type extensionPoint1, Type extensionPoint2)
		: this(elementName, ns, new Type[2] { extensionPoint1, extensionPoint2 })
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.XmlFormatExtensionAttribute" /> class that specifies the XML element and namespace to add when running at the specified extension points.</summary>
	/// <param name="elementName">The XML element added to the service description by the service description format extension.</param>
	/// <param name="ns">The XML namespace for the XML element added to the service description by the service description format extension.</param>
	/// <param name="extensionPoint1">An extension point at which to run the service description format extension.</param>
	/// <param name="extensionPoint2">An extension point at which to run the service description format extension.</param>
	/// <param name="extensionPoint3">An extension point at which to run the service description format extension.</param>
	public XmlFormatExtensionAttribute(string elementName, string ns, Type extensionPoint1, Type extensionPoint2, Type extensionPoint3)
		: this(elementName, ns, new Type[3] { extensionPoint1, extensionPoint2, extensionPoint3 })
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.XmlFormatExtensionAttribute" /> class that specifies the XML element and namespace to add when running at the specified extension points.</summary>
	/// <param name="elementName">The XML element added to the service description by the service description format extension.</param>
	/// <param name="ns">The XML namespace for the XML element added to the service description by the service description format extension.</param>
	/// <param name="extensionPoint1">An extension point at which to run the service description format extension.</param>
	/// <param name="extensionPoint2">An extension point at which to run the service description format extension.</param>
	/// <param name="extensionPoint3">An extension point at which to run the service description format extension.</param>
	/// <param name="extensionPoint4">An extension point at which to run the service description format extension. </param>
	public XmlFormatExtensionAttribute(string elementName, string ns, Type extensionPoint1, Type extensionPoint2, Type extensionPoint3, Type extensionPoint4)
		: this(elementName, ns, new Type[4] { extensionPoint1, extensionPoint2, extensionPoint3, extensionPoint4 })
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.XmlFormatExtensionAttribute" /> class that specifies the XML element and namespace to add when running at the specified extension points.</summary>
	/// <param name="elementName">The XML element added to the service description by the service description format extension. </param>
	/// <param name="ns">The XML namespace for the XML element added to the service description by the service description format extension. </param>
	/// <param name="extensionPoints">An array of extension points at which to run the service description format extension. </param>
	public XmlFormatExtensionAttribute(string elementName, string ns, Type[] extensionPoints)
	{
		name = elementName;
		this.ns = ns;
		types = extensionPoints;
	}
}
