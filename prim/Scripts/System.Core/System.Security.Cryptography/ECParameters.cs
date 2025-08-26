namespace System.Security.Cryptography;

/// <summary>Represents the standard parameters for the elliptic curve cryptography (ECC) algorithm.</summary>
public struct ECParameters
{
	/// <summary>Represents the curve associated with the public key (<see cref="F:System.Security.Cryptography.ECParameters.Q" />) and the optional private key (<see cref="F:System.Security.Cryptography.ECParameters.D" />).</summary>
	/// <returns>The curve.</returns>
	public ECCurve Curve;

	/// <summary>Represents the private key <see langword="D" /> for the elliptic curve cryptography (ECC) algorithm, stored in big-endian format.</summary>
	/// <returns>The <see langword="D" /> parameter for the elliptic curve cryptography (ECC) algorithm.</returns>
	public byte[] D;

	/// <summary>Represents the public key <see langword="Q" /> for the elliptic curve cryptography (ECC) algorithm.</summary>
	/// <returns>The <see langword="Q" /> parameter for the elliptic curve cryptography (ECC) algorithm.</returns>
	public ECPoint Q;

	/// <summary>Validates the current object.</summary>
	/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key or curve parameters are not valid for the current curve type.</exception>
	public void Validate()
	{
		throw new NotImplementedException();
	}
}
