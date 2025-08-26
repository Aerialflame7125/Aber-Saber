using System.Configuration.Provider;

namespace System.Web.Security;

/// <summary>A collection of objects that inherit the <see cref="T:System.Web.Security.RoleProvider" /> abstract class.</summary>
public sealed class RoleProviderCollection : ProviderCollection
{
	/// <summary>Gets the role provider in the collection referenced by the specified provider name.</summary>
	/// <param name="name">The name of the role provider.</param>
	/// <returns>An object that inherits the <see cref="T:System.Web.Security.RoleProvider" /> abstract class.</returns>
	public new RoleProvider this[string name] => (RoleProvider)base[name];

	/// <summary>Adds a role provider to the collection.</summary>
	/// <param name="provider">The role provider to add to the collection.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="provider" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="provider" /> is not of a type that inherits the <see cref="T:System.Web.Security.RoleProvider" /> abstract class.</exception>
	public override void Add(ProviderBase provider)
	{
		if (provider is RoleProvider)
		{
			base.Add(provider);
			return;
		}
		throw new HttpException();
	}

	/// <summary>Copies the role provider collection to a one-dimensional array.</summary>
	/// <param name="array">A one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Web.Security.RoleProviderCollection" />. The array must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	public void CopyTo(RoleProvider[] array, int index)
	{
		CopyTo((ProviderBase[])array, index);
	}

	/// <summary>Creates a new, empty role-provider collection.</summary>
	public RoleProviderCollection()
	{
	}
}
