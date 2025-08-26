namespace System.Web.Services.Configuration;

/// <summary>Specifies the transmission protocols that are used to decrypt data sent from a client browser in the HTTP request.</summary>
[Flags]
public enum WebServiceProtocols
{
	/// <summary>Unknown protocol.</summary>
	Unknown = 0,
	/// <summary>The HTTP SOAP protocol.</summary>
	HttpSoap = 1,
	/// <summary>The HTTP GET protocol.</summary>
	HttpGet = 2,
	/// <summary>The HTTP POST protocol.</summary>
	HttpPost = 4,
	/// <summary>The Web Services Documentation protocol.</summary>
	Documentation = 8,
	/// <summary>The HTTP POST LOCALHOST protocol.</summary>
	HttpPostLocalhost = 0x10,
	/// <summary>The HTTP SOAP version 1.2 protocol.</summary>
	HttpSoap12 = 0x20,
	/// <summary>Any version of the HTTP SOAP protocol.</summary>
	AnyHttpSoap = 0x21
}
