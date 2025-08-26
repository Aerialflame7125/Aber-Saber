using System.Runtime.InteropServices;

namespace System.Security.Cryptography;

/// <summary>Represents the abstract class from which all implementations of the <see cref="T:System.Security.Cryptography.MD5" /> hash algorithm inherit.</summary>
[ComVisible(true)]
public abstract class MD5 : HashAlgorithm
{
	/// <summary>Initializes a new instance of <see cref="T:System.Security.Cryptography.MD5" />.</summary>
	protected MD5()
	{
		HashSizeValue = 128;
	}

	/// <summary>Creates an instance of the default implementation of the <see cref="T:System.Security.Cryptography.MD5" /> hash algorithm.</summary>
	/// <returns>A new instance of the <see cref="T:System.Security.Cryptography.MD5" /> hash algorithm.</returns>
	/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
	public new static MD5 Create()
	{
		return Create("System.Security.Cryptography.MD5");
	}

	/// <summary>Creates an instance of the specified implementation of the <see cref="T:System.Security.Cryptography.MD5" /> hash algorithm.</summary>
	/// <param name="algName">The name of the specific implementation of <see cref="T:System.Security.Cryptography.MD5" /> to use.</param>
	/// <returns>A new instance of the specified implementation of <see cref="T:System.Security.Cryptography.MD5" />.</returns>
	/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="algName" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
	public new static MD5 Create(string algName)
	{
		return (MD5)CryptoConfig.CreateFromName(algName);
	}
}
