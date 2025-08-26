using System.IO;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography;

/// <summary>Performs asymmetric encryption and decryption using the implementation of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm provided by the cryptographic service provider (CSP). This class cannot be inherited.</summary>
[ComVisible(true)]
public sealed class RSACryptoServiceProvider : RSA, ICspAsymmetricAlgorithm
{
	private static volatile CspProviderFlags s_UseMachineKeyStore;

	private const int PROV_RSA_FULL = 1;

	private const int AT_KEYEXCHANGE = 1;

	private const int AT_SIGNATURE = 2;

	private KeyPairPersistence store;

	private bool persistKey;

	private bool persisted;

	private bool privateKeyExportable = true;

	private bool m_disposed;

	private RSAManaged rsa;

	/// <summary>Gets the name of the signature algorithm available with this implementation of <see cref="T:System.Security.Cryptography.RSA" />.</summary>
	/// <returns>The name of the signature algorithm.</returns>
	public override string SignatureAlgorithm => "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

	/// <summary>Gets or sets a value indicating whether the key should be persisted in the computer's key store instead of the user profile store.</summary>
	/// <returns>
	///   <see langword="true" /> if the key should be persisted in the computer key store; otherwise, <see langword="false" />.</returns>
	public static bool UseMachineKeyStore
	{
		get
		{
			return s_UseMachineKeyStore == CspProviderFlags.UseMachineKeyStore;
		}
		set
		{
			s_UseMachineKeyStore = (value ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags);
		}
	}

	/// <summary>Gets the name of the key exchange algorithm available with this implementation of <see cref="T:System.Security.Cryptography.RSA" />.</summary>
	/// <returns>The name of the key exchange algorithm if it exists; otherwise, <see langword="null" />.</returns>
	public override string KeyExchangeAlgorithm => "RSA-PKCS1-KeyEx";

	/// <summary>Gets the size of the current key.</summary>
	/// <returns>The size of the key in bits.</returns>
	public override int KeySize
	{
		get
		{
			if (rsa == null)
			{
				return KeySizeValue;
			}
			return rsa.KeySize;
		}
	}

	/// <summary>Gets or sets a value indicating whether the key should be persisted in the cryptographic service provider (CSP).</summary>
	/// <returns>
	///   <see langword="true" /> if the key should be persisted in the CSP; otherwise, <see langword="false" />.</returns>
	public bool PersistKeyInCsp
	{
		get
		{
			return persistKey;
		}
		set
		{
			persistKey = value;
			if (persistKey)
			{
				OnKeyGenerated(rsa, null);
			}
		}
	}

	/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> object contains only a public key.</summary>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> object contains only a public key; otherwise, <see langword="false" />.</returns>
	[ComVisible(false)]
	public bool PublicOnly => rsa.PublicOnly;

	/// <summary>Gets a <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> object that describes additional information about a cryptographic key pair.</summary>
	/// <returns>A <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> object that describes additional information about a cryptographic key pair.</returns>
	[ComVisible(false)]
	public CspKeyContainerInfo CspKeyContainerInfo => new CspKeyContainerInfo(store.Parameters);

	[SecuritySafeCritical]
	protected override byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
	{
		return HashAlgorithm.Create(hashAlgorithm.Name).ComputeHash(data, offset, count);
	}

	[SecuritySafeCritical]
	protected override byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
	{
		return HashAlgorithm.Create(hashAlgorithm.Name).ComputeHash(data);
	}

	private static int GetAlgorithmId(HashAlgorithmName hashAlgorithm)
	{
		return hashAlgorithm.Name switch
		{
			"MD5" => 32771, 
			"SHA1" => 32772, 
			"SHA256" => 32780, 
			"SHA384" => 32781, 
			"SHA512" => 32782, 
			_ => throw new CryptographicException(Environment.GetResourceString("'{0}' is not a known hash algorithm.", hashAlgorithm.Name)), 
		};
	}

	/// <summary>Encrypts data with the <see cref="T:System.Security.Cryptography.RSA" /> algorithm using the specified padding.</summary>
	/// <param name="data">The data to encrypt.</param>
	/// <param name="padding">The padding.</param>
	/// <returns>The encrypted data.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="data" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="padding" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Security.Cryptography.CryptographicException">The padding mode is not supported.</exception>
	public override byte[] Encrypt(byte[] data, RSAEncryptionPadding padding)
	{
		if (data == null)
		{
			throw new ArgumentNullException("data");
		}
		if (padding == null)
		{
			throw new ArgumentNullException("padding");
		}
		if (padding == RSAEncryptionPadding.Pkcs1)
		{
			return Encrypt(data, fOAEP: false);
		}
		if (padding == RSAEncryptionPadding.OaepSHA1)
		{
			return Encrypt(data, fOAEP: true);
		}
		throw PaddingModeNotSupported();
	}

	/// <summary>Decrypts data that was previously encrypted with the <see cref="T:System.Security.Cryptography.RSA" /> algorithm by using the specified padding.</summary>
	/// <param name="data">The data to decrypt.</param>
	/// <param name="padding">The padding.</param>
	/// <returns>The decrypted data.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="data" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="padding" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Security.Cryptography.CryptographicException">The padding mode is not supported.</exception>
	public override byte[] Decrypt(byte[] data, RSAEncryptionPadding padding)
	{
		if (data == null)
		{
			throw new ArgumentNullException("data");
		}
		if (padding == null)
		{
			throw new ArgumentNullException("padding");
		}
		if (padding == RSAEncryptionPadding.Pkcs1)
		{
			return Decrypt(data, fOAEP: false);
		}
		if (padding == RSAEncryptionPadding.OaepSHA1)
		{
			return Decrypt(data, fOAEP: true);
		}
		throw PaddingModeNotSupported();
	}

	/// <summary>Computes the signature for the specified hash value by encrypting it with the private key using the specified padding.</summary>
	/// <param name="hash">The hash value of the data to be signed.</param>
	/// <param name="hashAlgorithm">The hash algorithm name used to create the hash value of the data.</param>
	/// <param name="padding">The padding.</param>
	/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified hash value.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="hashAlgorithm" /> is <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="hash" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="padding" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Security.Cryptography.CryptographicException">
	///   <paramref name="padding" /> does not equal <see cref="P:System.Security.Cryptography.RSASignaturePadding.Pkcs1" />.</exception>
	public override byte[] SignHash(byte[] hash, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
	{
		if (hash == null)
		{
			throw new ArgumentNullException("hash");
		}
		if (string.IsNullOrEmpty(hashAlgorithm.Name))
		{
			throw RSA.HashAlgorithmNameNullOrEmpty();
		}
		if (padding == null)
		{
			throw new ArgumentNullException("padding");
		}
		if (padding != RSASignaturePadding.Pkcs1)
		{
			throw PaddingModeNotSupported();
		}
		return SignHash(hash, GetAlgorithmId(hashAlgorithm));
	}

	/// <summary>Verifies that a digital signature is valid by determining the hash value in the signature using the specified hashing algorithm and padding, and comparing it to the provided hash value.</summary>
	/// <param name="hash">The hash value of the signed data.</param>
	/// <param name="signature">The signature data to be verified.</param>
	/// <param name="hashAlgorithm">The hash algorithm name used to create the hash value.</param>
	/// <param name="padding">The padding.</param>
	/// <returns>
	///   <see langword="true" /> if the signature is valid; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="hashAlgorithm" /> is <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="hash" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="padding" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Security.Cryptography.CryptographicException">
	///   <paramref name="padding" /> does not equal <see cref="P:System.Security.Cryptography.RSASignaturePadding.Pkcs1" />.</exception>
	public override bool VerifyHash(byte[] hash, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
	{
		if (hash == null)
		{
			throw new ArgumentNullException("hash");
		}
		if (signature == null)
		{
			throw new ArgumentNullException("signature");
		}
		if (string.IsNullOrEmpty(hashAlgorithm.Name))
		{
			throw RSA.HashAlgorithmNameNullOrEmpty();
		}
		if (padding == null)
		{
			throw new ArgumentNullException("padding");
		}
		if (padding != RSASignaturePadding.Pkcs1)
		{
			throw PaddingModeNotSupported();
		}
		return VerifyHash(hash, GetAlgorithmId(hashAlgorithm), signature);
	}

	private static Exception PaddingModeNotSupported()
	{
		return new CryptographicException(Environment.GetResourceString("Specified padding mode is not valid for this algorithm."));
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class using the default key.</summary>
	/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.</exception>
	public RSACryptoServiceProvider()
		: this(1024)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class with the specified parameters.</summary>
	/// <param name="parameters">The parameters to be passed to the cryptographic service provider (CSP).</param>
	/// <exception cref="T:System.Security.Cryptography.CryptographicException">The CSP cannot be acquired.</exception>
	public RSACryptoServiceProvider(CspParameters parameters)
		: this(1024, parameters)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class with the specified key size.</summary>
	/// <param name="dwKeySize">The size of the key to use in bits.</param>
	/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.</exception>
	public RSACryptoServiceProvider(int dwKeySize)
	{
		Common(dwKeySize, parameters: false);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class with the specified key size and parameters.</summary>
	/// <param name="dwKeySize">The size of the key to use in bits.</param>
	/// <param name="parameters">The parameters to be passed to the cryptographic service provider (CSP).</param>
	/// <exception cref="T:System.Security.Cryptography.CryptographicException">The CSP cannot be acquired.  
	///  -or-  
	///  The key cannot be created.</exception>
	public RSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
	{
		bool flag = parameters != null;
		Common(dwKeySize, flag);
		if (flag)
		{
			Common(parameters);
		}
	}

	private void Common(int dwKeySize, bool parameters)
	{
		LegalKeySizesValue = new KeySizes[1];
		LegalKeySizesValue[0] = new KeySizes(384, 16384, 8);
		base.KeySize = dwKeySize;
		rsa = new RSAManaged(KeySize);
		rsa.KeyGenerated += OnKeyGenerated;
		persistKey = parameters;
		if (!parameters)
		{
			CspParameters cspParameters = new CspParameters(1);
			if (UseMachineKeyStore)
			{
				cspParameters.Flags |= CspProviderFlags.UseMachineKeyStore;
			}
			store = new KeyPairPersistence(cspParameters);
		}
	}

	private void Common(CspParameters p)
	{
		store = new KeyPairPersistence(p);
		bool flag = store.Load();
		bool num = (p.Flags & CspProviderFlags.UseExistingKey) != 0;
		privateKeyExportable = (p.Flags & CspProviderFlags.UseNonExportableKey) == 0;
		if (num && !flag)
		{
			throw new CryptographicException("Keyset does not exist");
		}
		if (store.KeyValue != null)
		{
			persisted = true;
			FromXmlString(store.KeyValue);
		}
	}

	~RSACryptoServiceProvider()
	{
		Dispose(disposing: false);
	}

	/// <summary>Decrypts data with the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
	/// <param name="rgb">The data to be decrypted.</param>
	/// <param name="fOAEP">
	///   <see langword="true" /> to perform direct <see cref="T:System.Security.Cryptography.RSA" /> decryption using OAEP padding (only available on a computer running Microsoft Windows XP or later); otherwise, <see langword="false" /> to use PKCS#1 v1.5 padding.</param>
	/// <returns>The decrypted data, which is the original plain text before encryption.</returns>
	/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.  
	///  -or-  
	///  The <paramref name="fOAEP" /> parameter is <see langword="true" /> and the length of the <paramref name="rgb" /> parameter is greater than <see cref="P:System.Security.Cryptography.RSACryptoServiceProvider.KeySize" />.  
	///  -or-  
	///  The <paramref name="fOAEP" /> parameter is <see langword="true" /> and OAEP is not supported.  
	///  -or-  
	///  The key does not match the encrypted data. However, the exception wording may not be accurate. For example, it may say Not enough storage is available to process this command.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="rgb" /> is <see langword="null" />.</exception>
	public byte[] Decrypt(byte[] rgb, bool fOAEP)
	{
		if (rgb == null)
		{
			throw new ArgumentNullException("rgb");
		}
		if (rgb.Length > KeySize / 8)
		{
			throw new CryptographicException(Environment.GetResourceString("The data to be decrypted exceeds the maximum for this modulus of {0} bytes.", KeySize / 8));
		}
		if (m_disposed)
		{
			throw new ObjectDisposedException("rsa");
		}
		AsymmetricKeyExchangeDeformatter asymmetricKeyExchangeDeformatter = null;
		asymmetricKeyExchangeDeformatter = ((!fOAEP) ? ((AsymmetricKeyExchangeDeformatter)new RSAPKCS1KeyExchangeDeformatter(rsa)) : ((AsymmetricKeyExchangeDeformatter)new RSAOAEPKeyExchangeDeformatter(rsa)));
		return asymmetricKeyExchangeDeformatter.DecryptKeyExchange(rgb);
	}

	/// <summary>This method is not supported in the current version.</summary>
	/// <param name="rgb">The data to be decrypted.</param>
	/// <returns>The decrypted data, which is the original plain text before encryption.</returns>
	/// <exception cref="T:System.NotSupportedException">This method is not supported in the current version.</exception>
	public override byte[] DecryptValue(byte[] rgb)
	{
		if (!rsa.IsCrtPossible)
		{
			throw new CryptographicException("Incomplete private key - missing CRT.");
		}
		return rsa.DecryptValue(rgb);
	}

	/// <summary>Encrypts data with the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
	/// <param name="rgb">The data to be encrypted.</param>
	/// <param name="fOAEP">
	///   <see langword="true" /> to perform direct <see cref="T:System.Security.Cryptography.RSA" /> encryption using OAEP padding (only available on a computer running Windows XP or later); otherwise, <see langword="false" /> to use PKCS#1 v1.5 padding.</param>
	/// <returns>The encrypted data.</returns>
	/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.  
	///  -or-  
	///  The length of the <paramref name="rgb" /> parameter is greater than the maximum allowed length.  
	///  -or-  
	///  The <paramref name="fOAEP" /> parameter is <see langword="true" /> and OAEP padding is not supported.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="rgb" /> is <see langword="null" />.</exception>
	public byte[] Encrypt(byte[] rgb, bool fOAEP)
	{
		AsymmetricKeyExchangeFormatter asymmetricKeyExchangeFormatter = null;
		asymmetricKeyExchangeFormatter = ((!fOAEP) ? ((AsymmetricKeyExchangeFormatter)new RSAPKCS1KeyExchangeFormatter(rsa)) : ((AsymmetricKeyExchangeFormatter)new RSAOAEPKeyExchangeFormatter(rsa)));
		return asymmetricKeyExchangeFormatter.CreateKeyExchange(rgb);
	}

	/// <summary>This method is not supported in the current version.</summary>
	/// <param name="rgb">The data to be encrypted.</param>
	/// <returns>The encrypted data.</returns>
	/// <exception cref="T:System.NotSupportedException">This method is not supported in the current version.</exception>
	public override byte[] EncryptValue(byte[] rgb)
	{
		return rsa.EncryptValue(rgb);
	}

	/// <summary>Exports the <see cref="T:System.Security.Cryptography.RSAParameters" />.</summary>
	/// <param name="includePrivateParameters">
	///   <see langword="true" /> to include private parameters; otherwise, <see langword="false" />.</param>
	/// <returns>The parameters for <see cref="T:System.Security.Cryptography.RSA" />.</returns>
	/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key cannot be exported.</exception>
	public override RSAParameters ExportParameters(bool includePrivateParameters)
	{
		if (includePrivateParameters && !privateKeyExportable)
		{
			throw new CryptographicException("cannot export private key");
		}
		RSAParameters result = rsa.ExportParameters(includePrivateParameters);
		if (includePrivateParameters)
		{
			if (result.D == null)
			{
				throw new ArgumentNullException("Missing D parameter for the private key.");
			}
			if (result.P == null || result.Q == null || result.DP == null || result.DQ == null || result.InverseQ == null)
			{
				throw new CryptographicException("Missing some CRT parameters for the private key.");
			}
		}
		return result;
	}

	/// <summary>Imports the specified <see cref="T:System.Security.Cryptography.RSAParameters" />.</summary>
	/// <param name="parameters">The parameters for <see cref="T:System.Security.Cryptography.RSA" />.</param>
	/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.  
	///  -or-  
	///  The <paramref name="parameters" /> parameter has missing fields.</exception>
	public override void ImportParameters(RSAParameters parameters)
	{
		rsa.ImportParameters(parameters);
	}

	private HashAlgorithm GetHash(object halg)
	{
		if (halg == null)
		{
			throw new ArgumentNullException("halg");
		}
		HashAlgorithm hashAlgorithm = null;
		if (halg is string)
		{
			hashAlgorithm = GetHashFromString((string)halg);
		}
		else if (halg is HashAlgorithm)
		{
			hashAlgorithm = (HashAlgorithm)halg;
		}
		else
		{
			if (!(halg is Type))
			{
				throw new ArgumentException("halg");
			}
			hashAlgorithm = (HashAlgorithm)Activator.CreateInstance((Type)halg);
		}
		if (hashAlgorithm == null)
		{
			throw new ArgumentException(string.Concat("Could not find provider for halg='", halg, "'."), "halg");
		}
		return hashAlgorithm;
	}

	private HashAlgorithm GetHashFromString(string name)
	{
		HashAlgorithm hashAlgorithm = HashAlgorithm.Create(name);
		if (hashAlgorithm != null)
		{
			return hashAlgorithm;
		}
		try
		{
			return HashAlgorithm.Create(GetHashNameFromOID(name));
		}
		catch (CryptographicException ex)
		{
			throw new ArgumentException(ex.Message, "halg", ex);
		}
	}

	/// <summary>Computes the hash value of the specified byte array using the specified hash algorithm, and signs the resulting hash value.</summary>
	/// <param name="buffer">The input data for which to compute the hash.</param>
	/// <param name="halg">The hash algorithm to use to create the hash value.</param>
	/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified data.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="halg" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="halg" /> parameter is not a valid type.</exception>
	public byte[] SignData(byte[] buffer, object halg)
	{
		if (buffer == null)
		{
			throw new ArgumentNullException("buffer");
		}
		return SignData(buffer, 0, buffer.Length, halg);
	}

	/// <summary>Computes the hash value of the specified input stream using the specified hash algorithm, and signs the resulting hash value.</summary>
	/// <param name="inputStream">The input data for which to compute the hash.</param>
	/// <param name="halg">The hash algorithm to use to create the hash value.</param>
	/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified data.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="halg" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="halg" /> parameter is not a valid type.</exception>
	public byte[] SignData(Stream inputStream, object halg)
	{
		HashAlgorithm hash = GetHash(halg);
		byte[] hashValue = hash.ComputeHash(inputStream);
		return PKCS1.Sign_v15(this, hash, hashValue);
	}

	/// <summary>Computes the hash value of a subset of the specified byte array using the specified hash algorithm, and signs the resulting hash value.</summary>
	/// <param name="buffer">The input data for which to compute the hash.</param>
	/// <param name="offset">The offset into the array from which to begin using data.</param>
	/// <param name="count">The number of bytes in the array to use as data.</param>
	/// <param name="halg">The hash algorithm to use to create the hash value.</param>
	/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified data.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="halg" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="halg" /> parameter is not a valid type.</exception>
	public byte[] SignData(byte[] buffer, int offset, int count, object halg)
	{
		HashAlgorithm hash = GetHash(halg);
		byte[] hashValue = hash.ComputeHash(buffer, offset, count);
		return PKCS1.Sign_v15(this, hash, hashValue);
	}

	private string GetHashNameFromOID(string oid)
	{
		return oid switch
		{
			"1.3.14.3.2.26" => "SHA1", 
			"1.2.840.113549.2.5" => "MD5", 
			"2.16.840.1.101.3.4.2.1" => "SHA256", 
			"2.16.840.1.101.3.4.2.2" => "SHA384", 
			"2.16.840.1.101.3.4.2.3" => "SHA512", 
			_ => throw new CryptographicException(oid + " is an unsupported hash algorithm for RSA signing"), 
		};
	}

	/// <summary>Computes the signature for the specified hash value by encrypting it with the private key.</summary>
	/// <param name="rgbHash">The hash value of the data to be signed.</param>
	/// <param name="str">The hash algorithm identifier (OID) used to create the hash value of the data.</param>
	/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified hash value.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.  
	///  -or-  
	///  There is no private key.</exception>
	public byte[] SignHash(byte[] rgbHash, string str)
	{
		if (rgbHash == null)
		{
			throw new ArgumentNullException("rgbHash");
		}
		HashAlgorithm hash = HashAlgorithm.Create((str == null) ? "SHA1" : GetHashNameFromOID(str));
		return PKCS1.Sign_v15(this, hash, rgbHash);
	}

	private byte[] SignHash(byte[] rgbHash, int calgHash)
	{
		return PKCS1.Sign_v15(this, InternalHashToHashAlgorithm(calgHash), rgbHash);
	}

	private static HashAlgorithm InternalHashToHashAlgorithm(int calgHash)
	{
		return calgHash switch
		{
			32771 => MD5.Create(), 
			32772 => SHA1.Create(), 
			32780 => SHA256.Create(), 
			32781 => SHA384.Create(), 
			32782 => SHA512.Create(), 
			_ => throw new NotImplementedException(calgHash.ToString()), 
		};
	}

	/// <summary>Verifies that a digital signature is valid by determining the hash value in the signature using the provided public key and comparing it to the hash value of the provided data.</summary>
	/// <param name="buffer">The data that was signed.</param>
	/// <param name="halg">The name of the hash algorithm used to create the hash value of the data.</param>
	/// <param name="signature">The signature data to be verified.</param>
	/// <returns>
	///   <see langword="true" /> if the signature is valid; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="halg" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="halg" /> parameter is not a valid type.</exception>
	public bool VerifyData(byte[] buffer, object halg, byte[] signature)
	{
		if (buffer == null)
		{
			throw new ArgumentNullException("buffer");
		}
		if (signature == null)
		{
			throw new ArgumentNullException("signature");
		}
		HashAlgorithm hash = GetHash(halg);
		byte[] hashValue = hash.ComputeHash(buffer);
		return PKCS1.Verify_v15(this, hash, hashValue, signature);
	}

	/// <summary>Verifies that a digital signature is valid by determining the hash value in the signature using the provided public key and comparing it to the provided hash value.</summary>
	/// <param name="rgbHash">The hash value of the signed data.</param>
	/// <param name="str">The hash algorithm identifier (OID) used to create the hash value of the data.</param>
	/// <param name="rgbSignature">The signature data to be verified.</param>
	/// <returns>
	///   <see langword="true" /> if the signature is valid; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is <see langword="null" />.  
	///  -or-  
	///  The <paramref name="rgbSignature" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.  
	///  -or-  
	///  The signature cannot be verified.</exception>
	public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
	{
		if (rgbHash == null)
		{
			throw new ArgumentNullException("rgbHash");
		}
		if (rgbSignature == null)
		{
			throw new ArgumentNullException("rgbSignature");
		}
		HashAlgorithm hash = HashAlgorithm.Create((str == null) ? "SHA1" : GetHashNameFromOID(str));
		return PKCS1.Verify_v15(this, hash, rgbHash, rgbSignature);
	}

	private bool VerifyHash(byte[] rgbHash, int calgHash, byte[] rgbSignature)
	{
		return PKCS1.Verify_v15(this, InternalHashToHashAlgorithm(calgHash), rgbHash, rgbSignature);
	}

	protected override void Dispose(bool disposing)
	{
		if (!m_disposed)
		{
			if (persisted && !persistKey)
			{
				store.Remove();
			}
			if (rsa != null)
			{
				rsa.Clear();
			}
			m_disposed = true;
		}
	}

	private void OnKeyGenerated(object sender, EventArgs e)
	{
		if (persistKey && !persisted)
		{
			store.KeyValue = ToXmlString(!rsa.PublicOnly);
			store.Save();
			persisted = true;
		}
	}

	/// <summary>Exports a blob containing the key information associated with an <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> object.</summary>
	/// <param name="includePrivateParameters">
	///   <see langword="true" /> to include the private key; otherwise, <see langword="false" />.</param>
	/// <returns>A byte array containing the key information associated with an <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> object.</returns>
	[ComVisible(false)]
	public byte[] ExportCspBlob(bool includePrivateParameters)
	{
		byte[] array = null;
		array = ((!includePrivateParameters) ? CryptoConvert.ToCapiPublicKeyBlob(this) : CryptoConvert.ToCapiPrivateKeyBlob(this));
		array[5] = (byte)((store != null && store.Parameters.KeyNumber == 2) ? 36u : 164u);
		return array;
	}

	/// <summary>Imports a blob that represents RSA key information.</summary>
	/// <param name="keyBlob">A byte array that represents an RSA key blob.</param>
	[ComVisible(false)]
	public void ImportCspBlob(byte[] keyBlob)
	{
		if (keyBlob == null)
		{
			throw new ArgumentNullException("keyBlob");
		}
		RSA rSA = CryptoConvert.FromCapiKeyBlob(keyBlob);
		if (rSA is RSACryptoServiceProvider)
		{
			RSAParameters parameters = rSA.ExportParameters(!(rSA as RSACryptoServiceProvider).PublicOnly);
			ImportParameters(parameters);
		}
		else
		{
			try
			{
				RSAParameters parameters2 = rSA.ExportParameters(includePrivateParameters: true);
				ImportParameters(parameters2);
			}
			catch
			{
				RSAParameters parameters3 = rSA.ExportParameters(includePrivateParameters: false);
				ImportParameters(parameters3);
			}
		}
		CspParameters cspParameters = new CspParameters(1);
		cspParameters.KeyNumber = ((keyBlob[5] != 36) ? 1 : 2);
		if (UseMachineKeyStore)
		{
			cspParameters.Flags |= CspProviderFlags.UseMachineKeyStore;
		}
		store = new KeyPairPersistence(cspParameters);
	}
}
