using System.Runtime.InteropServices;

namespace System.Web.Services.Protocols;

/// <summary>Represents the data in a SOAP request sent or a SOAP response received by an XML Web service client at a specific <see cref="T:System.Web.Services.Protocols.SoapMessageStage" />. This class cannot be inherited.</summary>
public sealed class SoapClientMessage : SoapMessage
{
	private SoapClientMethod method;

	private SoapHttpClientProtocol protocol;

	private string url;

	internal SoapExtension[] initializedExtensions;

	/// <summary>Gets a value indicating whether the client waits for the server to finish processing an XML Web service method.</summary>
	/// <returns>
	///     <see langword="true" /> if the client does not wait for the server to completely process a method.</returns>
	public override bool OneWay => method.oneWay;

	/// <summary>Gets an instance of the client proxy class, which derives from <see cref="T:System.Web.Services.Protocols.SoapHttpClientProtocol" />.</summary>
	/// <returns>An instance of the client proxy class.</returns>
	public SoapHttpClientProtocol Client => protocol;

	/// <summary>Gets a representation of the method prototype of the XML Web service method for which the SOAP request is intended.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> representing the XML Web service method for which the SOAP request is intended.</returns>
	public override LogicalMethodInfo MethodInfo => method.methodInfo;

	/// <summary>Gets the URL of the XML Web service.</summary>
	/// <returns>The URL of the XML Web service.</returns>
	public override string Url => url;

	/// <summary>Gets the <see langword="SOAPAction" /> HTTP request header field for the SOAP request or SOAP response.</summary>
	/// <returns>The <see langword="SOAPAction" /> HTTP request header field for the SOAP request or SOAP response.</returns>
	public override string Action => method.action;

	/// <summary>Gets the version of the SOAP protocol used to communicate with the XML Web service.</summary>
	/// <returns>One of the <see cref="T:System.Web.Services.Protocols.SoapProtocolVersion" /> values. The default is <see cref="F:System.Web.Services.Protocols.SoapProtocolVersion.Default" />.</returns>
	[ComVisible(false)]
	public override SoapProtocolVersion SoapVersion
	{
		get
		{
			if (protocol.SoapVersion != 0)
			{
				return protocol.SoapVersion;
			}
			return SoapProtocolVersion.Soap11;
		}
	}

	internal SoapClientMethod Method => method;

	internal SoapClientMessage(SoapHttpClientProtocol protocol, SoapClientMethod method, string url)
	{
		this.method = method;
		this.protocol = protocol;
		this.url = url;
	}

	protected override void EnsureOutStage()
	{
		EnsureStage(SoapMessageStage.AfterDeserialize);
	}

	protected override void EnsureInStage()
	{
		EnsureStage(SoapMessageStage.BeforeSerialize);
	}
}
