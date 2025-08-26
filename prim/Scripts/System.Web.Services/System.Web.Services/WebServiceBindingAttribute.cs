namespace System.Web.Services;

/// <summary>Declares a binding that defines one or more XML Web service methods. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
public sealed class WebServiceBindingAttribute : Attribute
{
	private string name;

	private string ns;

	private string location;

	private WsiProfiles claims;

	private bool emitClaims;

	/// <summary>Gets or sets the Web Services Interoperability (WSI) specification to which the binding claims to conform.</summary>
	/// <returns>One of the <see cref="T:System.Web.Services.WsiProfiles" /> values, indicating a WSI specification.</returns>
	public WsiProfiles ConformsTo
	{
		get
		{
			return claims;
		}
		set
		{
			claims = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the binding emits conformance claims.</summary>
	/// <returns>
	///     <see langword="true" /> if the binding emits conformance claims; otherwise, <see langword="false" />.</returns>
	public bool EmitConformanceClaims
	{
		get
		{
			return emitClaims;
		}
		set
		{
			emitClaims = value;
		}
	}

	/// <summary>Gets or sets the location where the binding is defined.</summary>
	/// <returns>The location where the binding is defined. The default is the URL of the XML Web service to which the attribute is applied.</returns>
	public string Location
	{
		get
		{
			if (location != null)
			{
				return location;
			}
			return string.Empty;
		}
		set
		{
			location = value;
		}
	}

	/// <summary>Gets or sets the name of the binding.</summary>
	/// <returns>The name of the binding. The default is the name of the XML Web service with "Soap" appended.</returns>
	public string Name
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

	/// <summary>Gets or sets the namespace associated with the binding.</summary>
	/// <returns>The namespace for the binding. The default is http://tempuri.org/.</returns>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebServiceBindingAttribute" /> class.</summary>
	public WebServiceBindingAttribute()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebServiceBindingAttribute" /> class setting the name of the binding the XML Web service method is implementing.</summary>
	/// <param name="name">The name of the binding an XML Web service method is implementing an operation for. Sets the <see cref="P:System.Web.Services.WebServiceBindingAttribute.Name" /> property. </param>
	public WebServiceBindingAttribute(string name)
	{
		this.name = name;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebServiceBindingAttribute" /> class.</summary>
	/// <param name="name">The name of the binding an XML Web service method is implementing an operation for. Sets the <see cref="P:System.Web.Services.WebServiceBindingAttribute.Name" /> property. </param>
	/// <param name="ns">The namespace associated with the binding. Sets the <see cref="P:System.Web.Services.WebServiceBindingAttribute.Namespace" /> property. </param>
	public WebServiceBindingAttribute(string name, string ns)
	{
		this.name = name;
		this.ns = ns;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebServiceBindingAttribute" /> class.</summary>
	/// <param name="name">The name of the binding an XML Web service method is implementing an operation for. Sets the <see cref="P:System.Web.Services.WebServiceBindingAttribute.Name" /> property. </param>
	/// <param name="ns">The namespace associated with the binding. Sets the <see cref="P:System.Web.Services.WebServiceBindingAttribute.Namespace" /> property. </param>
	/// <param name="location">The location where the binding is defined. </param>
	public WebServiceBindingAttribute(string name, string ns, string location)
	{
		this.name = name;
		this.ns = ns;
		this.location = location;
	}
}
