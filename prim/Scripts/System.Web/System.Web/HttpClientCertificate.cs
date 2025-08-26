using System.Collections.Specialized;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web;

/// <summary>Provides the client certificate fields issued by the client in response to the server's request for the client's identity.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HttpClientCertificate : NameValueCollection
{
	private HttpWorkerRequest hwr;

	private int flags;

	private DateTime from;

	private DateTime until;

	/// <summary>Gets or sets the certificate issuer, in binary format.</summary>
	/// <returns>The certificate issuer, expressed in binary format.</returns>
	public byte[] BinaryIssuer => hwr.GetClientCertificateBinaryIssuer();

	/// <summary>Gets the encoding of the certificate.</summary>
	/// <returns>One of the CERT_CONTEXT.dwCertEncodingType values.</returns>
	public int CertEncoding => hwr.GetClientCertificateEncoding();

	/// <summary>Gets a string containing the binary stream of the entire certificate content, in ASN.1 format.</summary>
	/// <returns>The client certificate.</returns>
	public byte[] Certificate => hwr.GetClientCertificate();

	/// <summary>Gets the unique ID for the client certificate, if provided.</summary>
	/// <returns>The client certificate ID.</returns>
	public string Cookie => GetString("CERT_COOKIE");

	/// <summary>A set of flags that provide additional client certificate information.</summary>
	/// <returns>A set of Boolean flags.</returns>
	public int Flags => flags;

	/// <summary>Gets a value that indicates whether the client certificate is present.</summary>
	/// <returns>
	///     <see langword="true" /> if the client certificate is present; otherwise, <see langword="false" />.</returns>
	public bool IsPresent => (flags & 1) == 1;

	/// <summary>A string that contains a list of subfield values containing information about the certificate issuer.</summary>
	/// <returns>The certificate issuer's information.</returns>
	public string Issuer => GetString("CERT_ISSUER");

	/// <summary>Gets a value that indicates whether the client certificate is valid.</summary>
	/// <returns>
	///     <see langword="true" /> if the client certificate is valid; otherwise, <see langword="false" />.</returns>
	public bool IsValid
	{
		get
		{
			if (!IsPresent)
			{
				return true;
			}
			return (flags & 2) == 0;
		}
	}

	/// <summary>Gets the number of bits in the digital certificate key size. For example, 128.</summary>
	/// <returns>The number of bits in the key size.</returns>
	public int KeySize => GetInt("CERT_KEYSIZE");

	/// <summary>Gets the public key binary value from the certificate.</summary>
	/// <returns>A byte array that contains the public key value.</returns>
	public byte[] PublicKey => hwr.GetClientCertificatePublicKey();

	/// <summary>Gets the number of bits in the server certificate private key. For example, 1024.</summary>
	/// <returns>The number of bits in the server certificate private key.</returns>
	public int SecretKeySize => GetInt("CERT_SECRETKEYSIZE");

	/// <summary>Provides the certificate serial number as an ASCII representation of hexadecimal bytes separated by hyphens. For example, 04-67-F3-02.</summary>
	/// <returns>The certificate serial number.</returns>
	public string SerialNumber => GetString("CERT_SERIALNUMBER");

	/// <summary>Gets the issuer field of the server certificate.</summary>
	/// <returns>The issuer field of the server certificate.</returns>
	public string ServerIssuer => GetString("CERT_SERVER_ISSUER");

	/// <summary>Gets the subject field of the server certificate.</summary>
	/// <returns>The subject field of the server certificate.</returns>
	public string ServerSubject => GetString("CERT_SERVER_SUBJECT");

	/// <summary>Gets the subject field of the client certificate.</summary>
	/// <returns>The subject field of the client certificate.</returns>
	public string Subject => GetString("CERT_SUBJECT");

	/// <summary>Gets the date when the certificate becomes valid. The date varies with international settings.</summary>
	/// <returns>The date when the certificate becomes valid.</returns>
	public DateTime ValidFrom => from;

	/// <summary>Gets the certificate expiration date.</summary>
	/// <returns>The certificate expiration date.</returns>
	public DateTime ValidUntil => until;

	internal HttpClientCertificate(HttpWorkerRequest hwr)
	{
		this.hwr = hwr;
		flags = GetIntNoPresense("CERT_FLAGS");
		if (IsPresent)
		{
			from = hwr.GetClientCertificateValidFrom();
			until = hwr.GetClientCertificateValidUntil();
		}
		else
		{
			from = DateTime.Now;
			until = from;
		}
	}

	/// <summary>Returns individual client certificate fields by name.</summary>
	/// <param name="field">The item in the collection to retrieve. </param>
	/// <returns>The value of the item specified by <paramref name="field" />.</returns>
	public override string Get(string field)
	{
		return string.Empty;
	}

	private int GetInt(string variable)
	{
		if (!IsPresent)
		{
			return 0;
		}
		return GetIntNoPresense(variable);
	}

	private int GetIntNoPresense(string variable)
	{
		string serverVariable = hwr.GetServerVariable(variable);
		if (serverVariable == null)
		{
			return 0;
		}
		try
		{
			return int.Parse(serverVariable, Helpers.InvariantCulture);
		}
		catch
		{
			return 0;
		}
	}

	private string GetString(string variable)
	{
		if (!IsPresent)
		{
			return string.Empty;
		}
		string serverVariable = hwr.GetServerVariable(variable);
		if (serverVariable != null)
		{
			return serverVariable;
		}
		return string.Empty;
	}
}
