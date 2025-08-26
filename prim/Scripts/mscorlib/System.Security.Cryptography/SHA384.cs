using System.Runtime.InteropServices;

namespace System.Security.Cryptography;

/// <summary>Computes the <see cref="T:System.Security.Cryptography.SHA384" /> hash for the input data.</summary>
[ComVisible(true)]
public abstract class SHA384 : HashAlgorithm
{
	/// <summary>Initializes a new instance of <see cref="T:System.Security.Cryptography.SHA384" />.</summary>
	protected SHA384()
	{
		HashSizeValue = 384;
	}

	/// <summary>Creates an instance of the default implementation of <see cref="T:System.Security.Cryptography.SHA384" />.</summary>
	/// <returns>A new instance of <see cref="T:System.Security.Cryptography.SHA384" />.</returns>
	/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
	public new static SHA384 Create()
	{
		return Create("System.Security.Cryptography.SHA384");
	}

	/// <summary>Creates an instance of a specified implementation of <see cref="T:System.Security.Cryptography.SHA384" />.</summary>
	/// <param name="hashName">The name of the specific implementation of <see cref="T:System.Security.Cryptography.SHA384" /> to be used.</param>
	/// <returns>A new instance of <see cref="T:System.Security.Cryptography.SHA384" /> using the specified implementation.</returns>
	/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="hashName" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
	public new static SHA384 Create(string hashName)
	{
		return (SHA384)CryptoConfig.CreateFromName(hashName);
	}
}
