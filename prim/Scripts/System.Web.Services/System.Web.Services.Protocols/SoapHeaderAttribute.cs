namespace System.Web.Services.Protocols;

/// <summary>This attribute is applied to an XML Web service method or an XML Web service client to specify a SOAP header that the XML Web service method or XML Web service client can process. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class SoapHeaderAttribute : Attribute
{
	private string memberName;

	private SoapHeaderDirection direction = SoapHeaderDirection.In;

	private bool required = true;

	/// <summary>Gets or sets the member of the XML Web service class representing the SOAP header contents.</summary>
	/// <returns>The member of the XML Web service class representing the SOAP header contents. There is no default.</returns>
	public string MemberName
	{
		get
		{
			if (memberName != null)
			{
				return memberName;
			}
			return string.Empty;
		}
		set
		{
			memberName = value;
		}
	}

	/// <summary>Gets or sets whether the SOAP header is intended for the XML Web service or the XML Web service client or both.</summary>
	/// <returns>The intended recipient of the SOAP header. The default is <see cref="F:System.Web.Services.Protocols.SoapHeaderDirection.In" />, which means the intended recipient is just the XML Web service.</returns>
	public SoapHeaderDirection Direction
	{
		get
		{
			return direction;
		}
		set
		{
			direction = value;
		}
	}

	/// <summary>This member is obsolete and has no functionality.</summary>
	/// <returns>A <see cref="T:System.Boolean" /> value.</returns>
	[Obsolete("This property will be removed from a future version. The presence of a particular header in a SOAP message is no longer enforced", false)]
	public bool Required
	{
		get
		{
			return required;
		}
		set
		{
			required = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapHeaderAttribute" /> class, setting the member of the XML Web service class representing the SOAP header contents.</summary>
	/// <param name="memberName">The member of the XML Web service class representing the SOAP header contents. The <see cref="P:System.Web.Services.Protocols.SoapHeaderAttribute.MemberName" /> property will be set to the value of this parameter. </param>
	public SoapHeaderAttribute(string memberName)
	{
		this.memberName = memberName;
	}
}
