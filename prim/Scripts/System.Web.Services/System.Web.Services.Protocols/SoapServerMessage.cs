using System.Runtime.InteropServices;

namespace System.Web.Services.Protocols;

/// <summary>Represents the data in a SOAP request received or a SOAP response sent by an XML Web service method at a specific <see cref="T:System.Web.Services.Protocols.SoapMessageStage" />. This class cannot be inherited.</summary>
public sealed class SoapServerMessage : SoapMessage
{
	private SoapServerProtocol protocol;

	internal SoapExtension[] highPriConfigExtensions;

	internal SoapExtension[] otherExtensions;

	internal SoapExtension[] allExtensions;

	/// <summary>Gets a value indicating whether the client waits for the server to finish processing an XML Web service method.</summary>
	/// <returns>
	///     <see langword="true" /> if the client waits for the server to completely process a method; otherwise, <see langword="false" />.</returns>
	public override bool OneWay => protocol.ServerMethod.oneWay;

	/// <summary>Gets the base URL of the XML Web service.</summary>
	/// <returns>The base URL of the XML Web service.</returns>
	public override string Url => RuntimeUtils.EscapeUri(protocol.Request.Url);

	/// <summary>Gets the SOAPAction HTTP request header field for the SOAP request or SOAP response.</summary>
	/// <returns>The SOAPAction HTTP request header field for the SOAP request or SOAP response.</returns>
	public override string Action => protocol.ServerMethod.action;

	/// <summary>Gets the version of the SOAP protocol used to communicate with the XML Web service.</summary>
	/// <returns>One of the <see cref="T:System.Web.Services.Protocols.SoapProtocolVersion" /> values. The default is <see cref="F:System.Web.Services.Protocols.SoapProtocolVersion.Default" />.</returns>
	[ComVisible(false)]
	public override SoapProtocolVersion SoapVersion => protocol.Version;

	/// <summary>Gets the instance of the class handling the method invocation on the Web server.</summary>
	/// <returns>The instance of the class implementing the XML Web service.</returns>
	/// <exception cref="T:System.InvalidOperationException">
	///         <see cref="T:System.Web.Services.Protocols.SoapMessageStage" /> is not <see cref="F:System.Web.Services.Protocols.SoapMessageStage.AfterDeserialize" /> or <see cref="F:System.Web.Services.Protocols.SoapMessageStage.BeforeSerialize" />. </exception>
	public object Server
	{
		get
		{
			EnsureStage((SoapMessageStage)9);
			return protocol.Target;
		}
	}

	/// <summary>Gets a representation of the method prototype for the XML Web service method for which the SOAP request is intended.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> representing the XML Web service method for which the SOAP request is intended.</returns>
	public override LogicalMethodInfo MethodInfo => protocol.MethodInfo;

	internal SoapServerMessage(SoapServerProtocol protocol)
	{
		this.protocol = protocol;
	}

	protected override void EnsureOutStage()
	{
		EnsureStage(SoapMessageStage.BeforeSerialize);
	}

	protected override void EnsureInStage()
	{
		EnsureStage(SoapMessageStage.AfterDeserialize);
	}
}
