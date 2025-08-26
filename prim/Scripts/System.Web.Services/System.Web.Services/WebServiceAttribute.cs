namespace System.Web.Services;

/// <summary>Used to add additional information to an XML Web service, such as a string describing its functionality.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public sealed class WebServiceAttribute : Attribute
{
	private string description;

	private string ns = "http://tempuri.org/";

	private string name;

	/// <summary>The default value for the <see cref="P:System.Web.Services.WebServiceAttribute.Namespace" /> property. This field is constant.</summary>
	public const string DefaultNamespace = "http://tempuri.org/";

	/// <summary>A descriptive message for the XML Web service.</summary>
	/// <returns>The text describing the functionality of the XML Web service.</returns>
	public string Description
	{
		get
		{
			if (description != null)
			{
				return description;
			}
			return string.Empty;
		}
		set
		{
			description = value;
		}
	}

	/// <summary>Gets or sets the default XML namespace to use for the XML Web service.</summary>
	/// <returns>The default XML namespace to use for the XML Web service. The default is specified in the <see cref="F:System.Web.Services.WebServiceAttribute.DefaultNamespace" /> property.</returns>
	public string Namespace
	{
		get
		{
			return ns;
		}
		set
		{
			ns = value;
		}
	}

	/// <summary>Gets or sets the name of the XML Web service.</summary>
	/// <returns>The name for the XML Web service. Default value is the name of the class implementing the XML Web service.</returns>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebServiceAttribute" /> class.</summary>
	public WebServiceAttribute()
	{
	}
}
