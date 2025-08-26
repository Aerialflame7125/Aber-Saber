namespace System.Web.Services.Protocols;

/// <summary>Applying this attribute to an XML Web service client using HTTP-GET or HTTP-POST, sets the types that serialize the parameters sent to an XML Web service method and read the response from the XML Web service method. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class HttpMethodAttribute : Attribute
{
	private Type returnFormatter;

	private Type parameterFormatter;

	/// <summary>Gets or sets a <see cref="T:System.Type" /> that deserializes the response from an XML Web service method.</summary>
	/// <returns>A <see cref="T:System.Type" /> that deserializes the response from an XML Web service method. There is no default.</returns>
	public Type ReturnFormatter
	{
		get
		{
			return returnFormatter;
		}
		set
		{
			returnFormatter = value;
		}
	}

	/// <summary>Gets or sets a <see cref="T:System.Type" /> that serializes parameters sent from an XML Web service client to the XML Web service method.</summary>
	/// <returns>A <see cref="T:System.Type" /> that serializes parameters sent from an XML Web service client to an XML Web service method. There is no default.</returns>
	public Type ParameterFormatter
	{
		get
		{
			return parameterFormatter;
		}
		set
		{
			parameterFormatter = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.HttpMethodAttribute" /> class.</summary>
	public HttpMethodAttribute()
	{
		returnFormatter = null;
		parameterFormatter = null;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.HttpMethodAttribute" />.</summary>
	/// <param name="returnFormatter">Initializes the <see cref="P:System.Web.Services.Protocols.HttpMethodAttribute.ReturnFormatter" /> property to a <see cref="T:System.Type" /> that deserializes the response from an XML Web service method. </param>
	/// <param name="parameterFormatter">Initializes the <see cref="P:System.Web.Services.Protocols.HttpMethodAttribute.ParameterFormatter" /> property to a <see cref="T:System.Type" /> that serializes parameters sent from an XML Web service client to an XML Web service method. </param>
	public HttpMethodAttribute(Type returnFormatter, Type parameterFormatter)
	{
		this.returnFormatter = returnFormatter;
		this.parameterFormatter = parameterFormatter;
	}
}
