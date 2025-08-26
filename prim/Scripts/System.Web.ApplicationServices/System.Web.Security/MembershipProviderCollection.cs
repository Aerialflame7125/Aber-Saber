using System.Configuration.Provider;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Web.Security;

/// <summary>A collection of objects that inherit the <see cref="T:System.Web.Security.MembershipProvider" /> abstract class.</summary>
[TypeForwardedFrom("System.Web, Version=2.0.0.0, Culture=Neutral, PublicKeyToken=b03f5f7f11d50a3a")]
public sealed class MembershipProviderCollection : ProviderCollection
{
	/// <summary>Gets the membership provider in the collection referenced by the specified provider name.</summary>
	/// <param name="name">The name of the membership provider.</param>
	/// <returns>An object that inherits the <see cref="T:System.Web.Security.MembershipProvider" /> abstract class.</returns>
	public new MembershipProvider this[string name] => (MembershipProvider)base[name];

	/// <summary>Adds a membership provider to the collection.</summary>
	/// <param name="provider">The membership provider to add to the collection.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="provider" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="provider" /> is not of a type that inherits the <see cref="T:System.Web.Security.MembershipProvider" /> abstract class.</exception>
	public override void Add(ProviderBase provider)
	{
		if (provider == null)
		{
			throw new ArgumentNullException("provider");
		}
		if (!(provider is MembershipProvider))
		{
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Provider must implement the class '{0}'.", typeof(MembershipProvider).ToString()), "provider");
		}
		base.Add(provider);
	}

	/// <summary>Copies the membership provider collection to a one-dimensional array.</summary>
	/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Web.Security.MembershipProviderCollection" />. The array must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="array" /> is multidimensional.  
	/// -or-  
	/// The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
	/// <exception cref="T:System.InvalidCastException">The type of the source array cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
	public void CopyTo(MembershipProvider[] array, int index)
	{
		CopyTo((ProviderBase[])array, index);
	}

	/// <summary>Creates a new, empty membership provider collection.</summary>
	public MembershipProviderCollection()
	{
	}
}
