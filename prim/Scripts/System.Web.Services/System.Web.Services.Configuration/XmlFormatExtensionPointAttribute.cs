namespace System.Web.Services.Configuration;

/// <summary>Specifies the XML namespace and XML namespace prefix to use for a service description format extension within a service description. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class XmlFormatExtensionPointAttribute : Attribute
{
	private string name;

	private bool allowElements = true;

	/// <summary>Specifies that the member of the class that implements the service description format extension can have a service description format extension associated with it.</summary>
	/// <returns>The member of the class that implements the service description format extension that can have a service description format extension associated with it.</returns>
	public string MemberName
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

	/// <summary>Gets or sets a value that indicates whether the member of the class that implements the service description format extension specified in the <see cref="P:System.Web.Services.Configuration.XmlFormatExtensionPointAttribute.MemberName" /> property can accept raw XML elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the member of the class that implements the service description format extension specified in the <see cref="P:System.Web.Services.Configuration.XmlFormatExtensionPointAttribute.MemberName" /> property can accept raw XML elements; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public bool AllowElements
	{
		get
		{
			return allowElements;
		}
		set
		{
			allowElements = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.XmlFormatExtensionPointAttribute" /> class.</summary>
	/// <param name="memberName">The member of the class that implements the service description format extension that can have a service description format extension associated with it.</param>
	public XmlFormatExtensionPointAttribute(string memberName)
	{
		name = memberName;
	}
}
