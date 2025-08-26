namespace System.Security.Cryptography;

/// <summary>Represents an elliptic curve.</summary>
public struct ECCurve
{
	/// <summary>Indicates how to interpret the data contained in an <see cref="T:System.Security.Cryptography.ECCurve" /> object.</summary>
	public enum ECCurveType
	{
		/// <summary>No curve data is interpreted. The caller is assumed to know what the curve is.</summary>
		Implicit,
		/// <summary>The curve parameters represent a prime curve with the formula y^2 = x^3 + A*x + B in the prime field P.</summary>
		PrimeShortWeierstrass,
		/// <summary>The curve parameters represent a prime curve with the formula A*x^2 + y^2 = 1 + B*x^2*y^2 in the prime field P.</summary>
		PrimeTwistedEdwards,
		/// <summary>The curve parameters represent a prime curve with the formula B*y^2 = x^3 + A*x^2 + x.</summary>
		PrimeMontgomery,
		/// <summary>The curve parameters represent a characteristic 2 curve.</summary>
		Characteristic2,
		/// <summary>The curve parameters represent a named curve.</summary>
		Named
	}

	/// <summary>Represents a factory class for creating named curves.</summary>
	public static class NamedCurves
	{
		/// <summary>Gets a brainpoolP160r1 named curve.</summary>
		/// <returns>A brainpoolP160r1 named curve.</returns>
		public static ECCurve brainpoolP160r1
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a brainpoolP160t1 named curve.</summary>
		/// <returns>A brainpoolP160t1 named curve.</returns>
		public static ECCurve brainpoolP160t1
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a brainpoolP192r1 named curve.</summary>
		/// <returns>A brainpoolP192r1 named curve.</returns>
		public static ECCurve brainpoolP192r1
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a brainpoolP192t1 named curve.</summary>
		/// <returns>A brainpoolP192t1 named curve.</returns>
		public static ECCurve brainpoolP192t1
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a brainpoolP224r1 named curve.</summary>
		/// <returns>A brainpoolP224r1 named curve.</returns>
		public static ECCurve brainpoolP224r1
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a brainpoolP224t1 named curve.</summary>
		/// <returns>A brainpoolP224t1 named curve.</returns>
		public static ECCurve brainpoolP224t1
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a brainpoolP256r1 named curve.</summary>
		/// <returns>A brainpoolP256r1 named curve.</returns>
		public static ECCurve brainpoolP256r1
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a brainpoolP256t1 named curve.</summary>
		/// <returns>A brainpoolP256t1 named curve.</returns>
		public static ECCurve brainpoolP256t1
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a brainpoolP320r1 named curve.</summary>
		/// <returns>A brainpoolP320r1 named curve.</returns>
		public static ECCurve brainpoolP320r1
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a brainpoolP320t1 named curve.</summary>
		/// <returns>A brainpoolP320t1 named curve.</returns>
		public static ECCurve brainpoolP320t1
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a brainpoolP384r1 named curve.</summary>
		/// <returns>A brainpoolP384r1 named curve.</returns>
		public static ECCurve brainpoolP384r1
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a brainpoolP384t1 named curve.</summary>
		/// <returns>A brainpoolP384t1 named curve.</returns>
		public static ECCurve brainpoolP384t1
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a brainpoolP512r1 named curve.</summary>
		/// <returns>A brainpoolP512r1 named curve.</returns>
		public static ECCurve brainpoolP512r1
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a brainpoolP512t1 named curve.</summary>
		/// <returns>A brainpoolP512t1 named curve.</returns>
		public static ECCurve brainpoolP512t1
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a nistP256 named curve.</summary>
		/// <returns>A nistP256 named curve.</returns>
		public static ECCurve nistP256
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a nistP384 named curve.</summary>
		/// <returns>A nistP384 named curve.</returns>
		public static ECCurve nistP384
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a nistP521 named curve.</summary>
		/// <returns>A nistP521 named curve.</returns>
		public static ECCurve nistP521
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}

	/// <summary>The first coefficient for an explicit curve. A for short Weierstrass, Montgomery, and Twisted Edwards curves.</summary>
	/// <returns>Coefficient A.</returns>
	public byte[] A;

	/// <summary>The second coefficient for an explicit curve. B for short Weierstrass and d for Twisted Edwards curves.</summary>
	/// <returns>Coefficient B.</returns>
	public byte[] B;

	/// <summary>The cofactor of the curve.</summary>
	/// <returns>The cofactor of the curve.</returns>
	public byte[] Cofactor;

	/// <summary>Identifies the composition of the <see cref="T:System.Security.Cryptography.ECCurve" /> object.</summary>
	/// <returns>The curve type.</returns>
	public ECCurveType CurveType;

	/// <summary>The generator, or base point, for operations on the curve.</summary>
	/// <returns>The base point.</returns>
	public ECPoint G;

	/// <summary>The name of the hash algorithm which was used to generate the curve coefficients (<see cref="F:System.Security.Cryptography.ECCurve.A" /> and <see cref="F:System.Security.Cryptography.ECCurve.B" />) from the <see cref="F:System.Security.Cryptography.ECCurve.Seed" /> under the ANSI X9.62 generation algorithm. Applies only to explicit curves.</summary>
	/// <returns>The name of the hash algorithm used to generate the curve coefficients.</returns>
	public HashAlgorithmName? Hash;

	/// <summary>The order of the curve. Applies only to explicit curves.</summary>
	/// <returns>The order of the curve. </returns>
	public byte[] Order;

	/// <summary>The curve polynomial. Applies only to characteristic 2 curves.</summary>
	/// <returns>The curve polynomial.</returns>
	public byte[] Polynomial;

	/// <summary>The prime specifying the base field. Applies only to prime curves.</summary>
	/// <returns>The prime P.</returns>
	public byte[] Prime;

	/// <summary>The seed value for coefficient generation under the ANSI X9.62 generation algorithm. Applies only to explicit curves.</summary>
	/// <returns>The seed value.</returns>
	public byte[] Seed;

	/// <summary>Gets a value that indicates whether the curve type indicates an explicit characteristic 2 curve.</summary>
	/// <returns>
	///     <see langword="true" /> if the curve is an explicit characteristic 2 curve; <see langword="false" /> if the curve is a named characteristic 2, prime, or implicit curve.</returns>
	public bool IsCharacteristic2
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value that indicates whether the curve type indicates an explicit curve (either prime or characteristic 2).</summary>
	/// <returns>
	///     <see langword="true" /> if the curve is an explicit curve (either prime or characteristic 2); <see langword="false" /> if the curve is a named or implicit curve.</returns>
	public bool IsExplicit
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value that indicates whether the curve type indicates a named curve.</summary>
	/// <returns>
	///     <see langword="true" /> if the curve is a named curve; <see langword="false" /> if the curve is an implict or an  explicit curve (either prime or characteristic 2).</returns>
	public bool IsNamed
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value that indicates whether the curve type indicates an explicit prime curve.</summary>
	/// <returns>
	///     <see langword="true" /> if the curve is an explicit prime curve; <see langword="false" /> if the curve is a named prime, characteristic 2 or implicit curves.</returns>
	public bool IsPrime
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the identifier of a named curve.</summary>
	/// <returns>The identifier of a named curve.</returns>
	public Oid Oid
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Creates a named curve using the specified friendly name of the identifier.</summary>
	/// <param name="oidFriendlyName">The friendly name of the identifier.</param>
	/// <returns>An object representing the named curve.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="oidFriendlyName" /> is <see langword="null" />.</exception>
	public static ECCurve CreateFromFriendlyName(string oidFriendlyName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates a named curve using the specified <see cref="T:System.Security.Cryptography.Oid" /> object.</summary>
	/// <param name="curveOid">The object identifier to use.</param>
	/// <returns>An object representing the named curve.</returns>
	public static ECCurve CreateFromOid(Oid curveOid)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates a named curve using the specified dotted-decimal representation of the identifier.</summary>
	/// <param name="oidValue">The dotted number of the identifier.</param>
	/// <returns>An object representing the named curve.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="oidValue" /> is <see langword="null" />.</exception>
	public static ECCurve CreateFromValue(string oidValue)
	{
		throw new NotImplementedException();
	}

	/// <summary>Validates the integrity of the current curve. Throws a <see cref="T:System.Security.Cryptography.CryptographicException" /> exception if the structure is not valid.</summary>
	/// <exception cref="T:System.Security.Cryptography.CryptographicException">The curve parameters are not valid for the current curve type.</exception>
	public void Validate()
	{
		throw new NotImplementedException();
	}
}
