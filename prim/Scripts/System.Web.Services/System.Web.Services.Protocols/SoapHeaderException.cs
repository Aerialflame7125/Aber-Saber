using System.Runtime.Serialization;
using System.Xml;

namespace System.Web.Services.Protocols;

/// <summary>The SOAP representation of a server error.</summary>
[Serializable]
public class SoapHeaderException : SoapException
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapHeaderException" /> class. </summary>
	public SoapHeaderException()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapHeaderException" /> class.</summary>
	/// <param name="message">A message that identifies the reason the exception occurred. This parameter sets the <see cref="P:System.Exception.Message" /> property.</param>
	/// <param name="code">The type of error that occurred. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Code" /> property.</param>
	/// <param name="actor">The piece of code that caused the exception. Typically, this is a URL to an XML Web service method. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Actor" /> property.</param>
	public SoapHeaderException(string message, XmlQualifiedName code, string actor)
		: base(message, code, actor)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapHeaderException" /> class.</summary>
	/// <param name="message">A message that identifies the reason the exception occurred. This parameter sets the <see cref="P:System.Exception.Message" /> property.</param>
	/// <param name="code">The type of error that occurred. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Code" /> property.</param>
	/// <param name="actor">The piece of code that caused the exception. Typically, this is a URL to an XML Web service method. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Actor" /> property.</param>
	/// <param name="innerException">A reference to the root cause of an exception. This parameter sets the <see cref="P:System.Exception.InnerException" /> property.</param>
	public SoapHeaderException(string message, XmlQualifiedName code, string actor, Exception innerException)
		: base(message, code, actor, innerException)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapHeaderException" /> class.</summary>
	/// <param name="message">A message that identifies the reason the exception occurred. This parameter sets the <see cref="P:System.Exception.Message" /> property.</param>
	/// <param name="code">The type of error that occurred. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Code" /> property.</param>
	public SoapHeaderException(string message, XmlQualifiedName code)
		: base(message, code)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapHeaderException" /> class.</summary>
	/// <param name="message">A message that identifies the reason the exception occurred. This parameter sets the <see cref="P:System.Exception.Message" /> property.</param>
	/// <param name="code">The type of error that occurred. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Code" /> property.</param>
	/// <param name="innerException">A reference to the root cause of an exception. This parameter sets the <see cref="P:System.Exception.InnerException" /> property.</param>
	public SoapHeaderException(string message, XmlQualifiedName code, Exception innerException)
		: base(message, code, innerException)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapHeaderException" /> class with the associated data.</summary>
	/// <param name="message">A message that identifies the reason the exception occurred. This parameter sets the <see cref="P:System.Exception.Message" /> property.</param>
	/// <param name="code">The type of error that occurred. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Code" /> property.</param>
	/// <param name="actor">The piece of code that caused the exception. Typically, this is a URL to an XML Web service method. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Actor" /> property.</param>
	/// <param name="role">An URI that represents the role of the XML Web service in the processing of the SOAP message. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Role" /> property.</param>
	/// <param name="subCode">A <see cref="T:System.Web.Services.Protocols.SoapFaultSubCode" /> that contains the contents of the <see langword="&lt;subcode&gt;" /> element of a SOAP fault.</param>
	/// <param name="innerException">A reference to the root cause of the exception. This parameter sets the <see cref="P:System.Exception.InnerException" /> property.</param>
	public SoapHeaderException(string message, XmlQualifiedName code, string actor, string role, SoapFaultSubCode subCode, Exception innerException)
		: base(message, code, actor, role, null, null, subCode, innerException)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapHeaderException" /> class with the associated data.</summary>
	/// <param name="message">A message that identifies the reason the exception occurred. This parameter sets the <see cref="P:System.Exception.Message" /> property.</param>
	/// <param name="code">The type of error that occurred. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Code" /> property.</param>
	/// <param name="actor">The piece of code that caused the exception. Typically, this is a URL to an XML Web service method. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Actor" /> property.</param>
	/// <param name="role">An URI that represents the role of the XML Web service in the processing of the SOAP message. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Role" /> property.</param>
	/// <param name="lang">A string that identifies the human language associated with the exception. This parameter sets the <see cref="P:System.Web.Services.Protocols.SoapException.Lang" /> property.</param>
	/// <param name="subCode">A <see cref="T:System.Web.Services.Protocols.SoapFaultSubCode" /> that contains the contents of the <see langword="subcode" /> element of a SOAP fault.</param>
	/// <param name="innerException">A reference to the root cause of an exception. This parameter sets the <see cref="P:System.Exception.InnerException" /> property.</param>
	public SoapHeaderException(string message, XmlQualifiedName code, string actor, string role, string lang, SoapFaultSubCode subCode, Exception innerException)
		: base(message, code, actor, role, lang, null, subCode, innerException)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapHeaderException" /> class with parameters for controlling serialization.</summary>
	/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains values that are used during serialization and deserialization.</param>
	/// <param name="context">A <see cref="M:System.Web.Services.Protocols.SoapHeaderException.#ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> that contains data about the source and destination of the serialization stream.</param>
	protected SoapHeaderException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
}
