namespace System.Security.Cryptography;

/// <summary>Specifies the padding mode and parameters to use with RSA encryption or decryption operations.</summary>
public sealed class RSAEncryptionPadding : IEquatable<RSAEncryptionPadding>
{
	private static readonly RSAEncryptionPadding s_pkcs1 = new RSAEncryptionPadding(RSAEncryptionPaddingMode.Pkcs1, default(HashAlgorithmName));

	private static readonly RSAEncryptionPadding s_oaepSHA1 = CreateOaep(HashAlgorithmName.SHA1);

	private static readonly RSAEncryptionPadding s_oaepSHA256 = CreateOaep(HashAlgorithmName.SHA256);

	private static readonly RSAEncryptionPadding s_oaepSHA384 = CreateOaep(HashAlgorithmName.SHA384);

	private static readonly RSAEncryptionPadding s_oaepSHA512 = CreateOaep(HashAlgorithmName.SHA512);

	private RSAEncryptionPaddingMode _mode;

	private HashAlgorithmName _oaepHashAlgorithm;

	/// <summary>Gets an object that represents the PKCS #1 encryption standard.</summary>
	/// <returns>An object that represents the PKCS #1 encryption standard.</returns>
	public static RSAEncryptionPadding Pkcs1 => s_pkcs1;

	/// <summary>Gets an object that represents the Optimal Asymmetric Encryption Padding (OAEP) encryption standard with a SHA1 hash algorithm.</summary>
	/// <returns>An object that represents the OAEP encryption standard with a SHA1 hash algorithm.</returns>
	public static RSAEncryptionPadding OaepSHA1 => s_oaepSHA1;

	/// <summary>Gets an object that represents the Optimal Asymmetric Encryption Padding (OAEP) encryption standard with a SHA256 hash algorithm.</summary>
	/// <returns>An object that represents the OAEP encryption standard with a SHA256 hash algorithm.</returns>
	public static RSAEncryptionPadding OaepSHA256 => s_oaepSHA256;

	/// <summary>Gets an object that represents the Optimal Asymmetric Encryption Padding (OAEP) encryption standard with a SHA-384 hash algorithm.</summary>
	/// <returns>An object that represents the OAEP encryption standard with a SHA384 hash algorithm.</returns>
	public static RSAEncryptionPadding OaepSHA384 => s_oaepSHA384;

	/// <summary>Gets an object that represents the Optimal Asymmetric Encryption Padding (OAEP) encryption standard with a SHA512 hash algorithm.</summary>
	/// <returns>An object that represents the OAEP encryption standard with a SHA512 hash algorithm.</returns>
	public static RSAEncryptionPadding OaepSHA512 => s_oaepSHA512;

	/// <summary>Gets the padding mode represented by this <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" /> instance.</summary>
	/// <returns>A padding mode.</returns>
	public RSAEncryptionPaddingMode Mode => _mode;

	/// <summary>Gets the hash algorithm used in conjunction with the <see cref="F:System.Security.Cryptography.RSAEncryptionPaddingMode.Oaep" /> padding mode. If the value of the <see cref="P:System.Security.Cryptography.RSAEncryptionPadding.Mode" /> property is not <see cref="F:System.Security.Cryptography.RSAEncryptionPaddingMode.Oaep" />, <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> is <see langword="null" />.</summary>
	/// <returns>The hash algorithm.</returns>
	public HashAlgorithmName OaepHashAlgorithm => _oaepHashAlgorithm;

	private RSAEncryptionPadding(RSAEncryptionPaddingMode mode, HashAlgorithmName oaepHashAlgorithm)
	{
		_mode = mode;
		_oaepHashAlgorithm = oaepHashAlgorithm;
	}

	/// <summary>Creates a new <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" /> instance whose <see cref="P:System.Security.Cryptography.RSAEncryptionPadding.Mode" /> is <see cref="F:System.Security.Cryptography.RSAEncryptionPaddingMode.Oaep" /> with the given hash algorithm.</summary>
	/// <param name="hashAlgorithm">The hash algorithm.</param>
	/// <returns>An object whose mode is <see cref="P:System.Security.Cryptography.RSAEncryptionPadding.Mode" /> is <see cref="F:System.Security.Cryptography.RSAEncryptionPaddingMode.Oaep" /> with the hash algorithm specified by <paramref name="hashAlgorithm" />.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> property of <paramref name="hashAlgorithm" /> is either <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
	public static RSAEncryptionPadding CreateOaep(HashAlgorithmName hashAlgorithm)
	{
		if (string.IsNullOrEmpty(hashAlgorithm.Name))
		{
			throw new ArgumentException(Environment.GetResourceString("The hash algorithm name cannot be null or empty."), "hashAlgorithm");
		}
		return new RSAEncryptionPadding(RSAEncryptionPaddingMode.Oaep, hashAlgorithm);
	}

	/// <summary>Returns the hash code of this <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" /> object.</summary>
	/// <returns>The hash code of this instance.</returns>
	public override int GetHashCode()
	{
		return CombineHashCodes(_mode.GetHashCode(), _oaepHashAlgorithm.GetHashCode());
	}

	private static int CombineHashCodes(int h1, int h2)
	{
		return ((h1 << 5) + h1) ^ h2;
	}

	/// <summary>Determines whether the current instance is equal to the specified object.</summary>
	/// <param name="obj">The object to compare.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="obj" /> is equal to the current instance; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		return Equals(obj as RSAEncryptionPadding);
	}

	/// <summary>Determines whether the current instance is equal to the specified <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" /> object.</summary>
	/// <param name="other">The object to compare.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="other" /> is equal to the current instance; otherwise, <see langword="false" />.</returns>
	public bool Equals(RSAEncryptionPadding other)
	{
		if (other != null && _mode == other._mode)
		{
			return _oaepHashAlgorithm == other._oaepHashAlgorithm;
		}
		return false;
	}

	/// <summary>Indicates whether two specified <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" /> objects are equal.</summary>
	/// <param name="left">The first object to compare.</param>
	/// <param name="right">The second object to compare.</param>
	/// <returns>
	///   <see langword="true" /> if <see langword="left" /> and <see langword="right" /> are equal; otherwise, <see langword="false" />.</returns>
	public static bool operator ==(RSAEncryptionPadding left, RSAEncryptionPadding right)
	{
		return left?.Equals(right) ?? ((object)right == null);
	}

	/// <summary>Indicates whether two specified <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" /> objects are unequal.</summary>
	/// <param name="left">The first object to compare.</param>
	/// <param name="right">The second object to compare.</param>
	/// <returns>
	///   <see langword="true" /> if <see langword="left" /> and <see langword="right" /> are not equal; otherwise, <see langword="false" />.</returns>
	public static bool operator !=(RSAEncryptionPadding left, RSAEncryptionPadding right)
	{
		return !(left == right);
	}

	/// <summary>Returns the string representation of the current <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" /> instance.</summary>
	/// <returns>The string representation of the current object.</returns>
	public override string ToString()
	{
		return _mode.ToString() + _oaepHashAlgorithm.Name;
	}
}
