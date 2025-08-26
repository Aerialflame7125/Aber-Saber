using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography;

/// <summary>Creates an <see cref="T:System.Security.Cryptography.RSA" /> PKCS #1 version 1.5 signature.</summary>
[ComVisible(true)]
public class RSAPKCS1SignatureFormatter : AsymmetricSignatureFormatter
{
	private RSA rsa;

	private string hash;

	/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1SignatureFormatter" /> class.</summary>
	public RSAPKCS1SignatureFormatter()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1SignatureFormatter" /> class with the specified key.</summary>
	/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the private key.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="key" /> is <see langword="null" />.</exception>
	public RSAPKCS1SignatureFormatter(AsymmetricAlgorithm key)
	{
		SetKey(key);
	}

	/// <summary>Creates the <see cref="T:System.Security.Cryptography.RSA" /> PKCS #1 signature for the specified data.</summary>
	/// <param name="rgbHash">The data to be signed.</param>
	/// <returns>The digital signature for <paramref name="rgbHash" />.</returns>
	/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The key is <see langword="null" />.  
	///  -or-  
	///  The hash algorithm is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is <see langword="null" />.</exception>
	public override byte[] CreateSignature(byte[] rgbHash)
	{
		if (rsa == null)
		{
			throw new CryptographicUnexpectedOperationException(Locale.GetText("No key pair available."));
		}
		if (hash == null)
		{
			throw new CryptographicUnexpectedOperationException(Locale.GetText("Missing hash algorithm."));
		}
		if (rgbHash == null)
		{
			throw new ArgumentNullException("rgbHash");
		}
		return PKCS1.Sign_v15(rsa, hash, rgbHash);
	}

	/// <summary>Sets the hash algorithm to use for creating the signature.</summary>
	/// <param name="strName">The name of the hash algorithm to use for creating the signature.</param>
	public override void SetHashAlgorithm(string strName)
	{
		if (strName == null)
		{
			throw new ArgumentNullException("strName");
		}
		hash = strName;
	}

	/// <summary>Sets the private key to use for creating the signature.</summary>
	/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the private key.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="key" /> is <see langword="null" />.</exception>
	public override void SetKey(AsymmetricAlgorithm key)
	{
		if (key == null)
		{
			throw new ArgumentNullException("key");
		}
		rsa = (RSA)key;
	}
}
