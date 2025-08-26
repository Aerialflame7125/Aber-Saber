namespace System.Security.Cryptography.X509Certificates;

/// <summary>Provides extension methods for retrieving <see cref="T:System.Security.Cryptography.ECDsa" /> implementations for the
///     public and private keys of a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> certificate.</summary>
public static class ECDsaCertificateExtensions
{
	/// <summary>Gets the <see cref="T:System.Security.Cryptography.ECDsa" /> private key from the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> certificate.</summary>
	/// <param name="certificate">The certificate. </param>
	/// <returns>The private key, or <see langword="null" /> if the certificate does not have an ECDsa private key. </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="certificate" /> is <see langword="null" />. </exception>
	[MonoTODO]
	public static ECDsa GetECDsaPrivateKey(this X509Certificate2 certificate)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the <see cref="T:System.Security.Cryptography.ECDsa" /> public key from the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> certificate.</summary>
	/// <param name="certificate">The certificate. </param>
	/// <returns>The public key, or <see langword="null" /> if the certificate does not have an ECDsa public key.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="certificate" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.Security.Cryptography.CryptographicException">The handle is invalid. </exception>
	[MonoTODO]
	public static ECDsa GetECDsaPublicKey(this X509Certificate2 certificate)
	{
		throw new NotImplementedException();
	}
}
