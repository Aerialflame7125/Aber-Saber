using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> class represents a global catalog server. A global catalog server is a domain controller that hosts a replica of the global catalog.</summary>
[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
public class GlobalCatalog : DomainController
{
	/// <summary>Retrieves a <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> object for the specified context.</summary>
	/// <param name="context">An  <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use to retrieve the object. The target of the context must be a domain controller.</param>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> object that represents the global catalog server that was found.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A connection to the target specified in <paramref name="context" /> could not be made.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="context" /> is not valid.</exception>
	public static GlobalCatalog GetGlobalCatalog(DirectoryContext context)
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds a single global catalog server in the specified context.</summary>
	/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use for the search.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> object that represents the global catalog server found by the search.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">No global catalog server was found.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="context" /> is not valid.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> is <see langword="null" />.</exception>
	public new static GlobalCatalog FindOne(DirectoryContext context)
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds a single global catalog server in the specified context and site.</summary>
	/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use for the search.</param>
	/// <param name="siteName">The name of the site to search for a global catalog server.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> object that represents the global catalog server found by the search.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">No global catalog server was found.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="context" /> or <paramref name="siteName" /> is not valid.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> or <paramref name="siteName" /> is <see langword="null" />.</exception>
	public new static GlobalCatalog FindOne(DirectoryContext context, string siteName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds a single global catalog server in the specified context, allowing for additional search options.</summary>
	/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use for the search.</param>
	/// <param name="flag">A combination of one or more of the <see cref="T:System.DirectoryServices.ActiveDirectory.LocatorOptions" /> members that defines the type of global catalog server to find.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> object that represents the global catalog server found by the search.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">No global catalog server was found.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="context" /> or <paramref name="flag" /> is not valid.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> is null.</exception>
	public new static GlobalCatalog FindOne(DirectoryContext context, LocatorOptions flag)
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds a single global catalog server in the specified context and site, allowing for additional search options.</summary>
	/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use for the search.</param>
	/// <param name="siteName">The name of the site to search for a global catalog server.</param>
	/// <param name="flag">A combination of one or more of the <see cref="T:System.DirectoryServices.ActiveDirectory.LocatorOptions" /> members that defines the type of global catalog server to find.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> object that represents the global catalog server found by the search.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">No global catalog server was found.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="context" />, <paramref name="siteName" />, or <paramref name="flag" /> is not valid.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> or <paramref name="siteName" /> is null.</exception>
	public new static GlobalCatalog FindOne(DirectoryContext context, string siteName, LocatorOptions flag)
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds all global catalog servers in the specified context.</summary>
	/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use for the search. The target of the context must be a forest.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalogCollection" /> object that contains the global catalog servers found by the search.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="context" /> is not valid.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> is <see langword="null" />.</exception>
	public new static GlobalCatalogCollection FindAll(DirectoryContext context)
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds all global catalog servers in the specified context and site.</summary>
	/// <param name="context">An <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use for the search. The target of the context must be a forest.</param>
	/// <param name="siteName">The name of the site to search for global catalog servers.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalogCollection" /> object that contains the global catalog servers found by the search.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="context" /> or <paramref name="siteName" /> is not valid.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> or <paramref name="siteName" /> is <see langword="null" />.</exception>
	public new static GlobalCatalogCollection FindAll(DirectoryContext context, string siteName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Not supported for the <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> class because this domain controller is already a global catalog server.</summary>
	/// <returns>Not applicable. This method will always throw an <see cref="T:System.InvalidOperationException" /> exception.</returns>
	/// <exception cref="T:System.InvalidOperationException">The domain controller is already a global catalog server.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public override GlobalCatalog EnableGlobalCatalog()
	{
		throw new NotImplementedException();
	}

	/// <summary>Demotes this domain controller from a global catalog server.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that represents the domain controller.</returns>
	/// <exception cref="T:System.InvalidOperationException">The domain controller is not a global catalog server or the global catalog server has already been disabled.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	public DomainController DisableGlobalCatalog()
	{
		throw new NotImplementedException();
	}

	/// <summary>Determines if this domain controller is a global catalog server.</summary>
	/// <returns>
	///   <see langword="true" /> if this domain controller is a global catalog server; <see langword="false" /> if it is not a global catalog server.</returns>
	/// <exception cref="T:System.InvalidOperationException">The global catalog server has been disabled.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public override bool IsGlobalCatalog()
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves all of the Active Directory Domain Services properties that are stored on this global catalog server.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaPropertyCollection" /> object that contains the Active Directory Domain Services properties that are stored on this global catalog server.</returns>
	/// <exception cref="T:System.InvalidOperationException">The domain controller is not a global catalog server or the global catalog server has been disabled.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	public ReadOnlyActiveDirectorySchemaPropertyCollection FindAllProperties()
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves a <see cref="T:System.DirectoryServices.DirectorySearcher" /> object for the global catalog server.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.DirectorySearcher" /> object for the global catalog server.</returns>
	/// <exception cref="T:System.InvalidOperationException">The global catalog server has been disabled.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public override DirectorySearcher GetDirectorySearcher()
	{
		throw new NotImplementedException();
	}
}
