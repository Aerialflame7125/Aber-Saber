using System.IO;
using System.Net;
using System.Text;

namespace System.Web.Services.Protocols;

/// <summary>Provides a common base implementation for writers of out-going request parameters for Web service clients implemented using HTTP but without SOAP.</summary>
public abstract class MimeParameterWriter : MimeFormatter
{
	/// <summary>Gets a value that indicates whether Web method parameter values are serialized to the out-going HTTP request body.</summary>
	/// <returns>
	///     <see langword="true" /> if the Web method parameter values are serialized to the out-going HTTP request body; otherwise <see langword="false" />.</returns>
	public virtual bool UsesWriteRequest => false;

	/// <summary>Gets or sets the encoding used to write parameters to the HTTP request.</summary>
	/// <returns>The encoding used to write parameters to the HTTP request.</returns>
	public virtual Encoding RequestEncoding
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	/// <summary>When overridden in a derived class, modifies the outgoing HTTP request's Uniform Request Locator (URL).</summary>
	/// <param name="url">The HTTP request's original Uniform Resource Locator (URL).</param>
	/// <param name="parameters">The Web method parameter values to be added to the URL, if necessary.</param>
	/// <returns>A <see cref="T:System.String" /> object that contains the modified, outgoing HTTP request's Uniform Request Locator (URL).</returns>
	public virtual string GetRequestUrl(string url, object[] parameters)
	{
		return url;
	}

	/// <summary>When overridden in a derived class, initializes the out-going HTTP request.</summary>
	/// <param name="request">The out-going request, where the <see cref="T:System.Net.WebRequest" /> class allows transport protocols besides HTTP.</param>
	/// <param name="values">The Web method parameter values.</param>
	public virtual void InitializeRequest(WebRequest request, object[] values)
	{
	}

	/// <summary>When overridden in a derived class, serializes Web method parameter values into a stream representing the outgoing HTTP request body.</summary>
	/// <param name="requestStream">An input stream for the outgoing HTTP request's body.</param>
	/// <param name="values">The Web method parameter values.</param>
	public virtual void WriteRequest(Stream requestStream, object[] values)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.MimeParameterWriter" /> class. </summary>
	protected MimeParameterWriter()
	{
	}
}
