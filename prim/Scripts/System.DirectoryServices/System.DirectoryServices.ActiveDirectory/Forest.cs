using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> class represents an Active Directory Domain Services forest.</summary>
[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
public class Forest : IDisposable
{
	/// <summary>Gets the name of the forest.</summary>
	/// <returns>The name of this <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" />.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public string Name
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a collection of sites that are contained in the forest.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlySiteCollection" /> that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> sites in the current <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" />.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ReadOnlySiteCollection Sites
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a collection of all domains in the forest.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainCollection" /> collection that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> domains in the current <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" />.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DomainCollection Domains
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a collection of all global catalogs in the forest.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalogCollection" /> collection that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> global catalogs in the current <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" />.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public GlobalCatalogCollection GlobalCatalogs
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a collection of all application partitions in the forest.</summary>
	/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartitionCollection" /> object that contains all of the application partitions in this forest.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ApplicationPartitionCollection ApplicationPartitions
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the operating mode of the forest.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ForestMode" /> value that represents the current forest mode of this <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" />.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ForestMode ForestMode
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the first domain that was created in a forest.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the first domain that was created in this <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" />.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public Domain RootDomain
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the schema of the forest.</summary>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchema" /> object that represents the schema of the current <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" />.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ActiveDirectorySchema Schema
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the domain controller that holds the FSMO schema master role for the forest.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that represents the domain controller that is the FSMO schema master of this <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" />.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DomainController SchemaRoleOwner
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the domain controller that holds the FSMO naming master role for the forest.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that represents the domain controller that is the FSMO naming master of this <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" />.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DomainController NamingRoleOwner
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Releases all managed and unmanaged resources that are held by the object.</summary>
	public void Dispose()
	{
		Dispose(disposing: true);
	}

	/// <summary>Releases the managed resources that are used by the object and, optionally, releases unmanaged resources.</summary>
	/// <param name="disposing">A <see cref="T:System.Boolean" /> value that determines if the managed resources should be released. <see langword="true" /> if the managed resources should be released; <see langword="false" /> if only the unmanaged resources should be released.</param>
	protected void Dispose(bool disposing)
	{
	}

	/// <summary>Gets a <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object for the specified context.</summary>
	/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use to retrieve the object.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object that represents the forest that was retrieved by this method.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A connection to the target that is specified in <paramref name="context" /> could not be made.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="context" /> is not valid.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> is <see langword="null" />.</exception>
	public static Forest GetForest(DirectoryContext context)
	{
		throw new NotImplementedException();
	}

	/// <summary>Raises the mode of operation for the forest.</summary>
	/// <param name="forestMode">A <see cref="T:System.DirectoryServices.ActiveDirectory.ForestMode" /> enumeration value that specifies the new operation level for the forest.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">Based on the current operating mode of the forest, the value specified for <paramref name="forestMode" /> is not valid.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="forestMode" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.ForestMode" /> enumeration value.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void RaiseForestFunctionality(ForestMode forestMode)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a string representation of the current forest.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents the DNS name of the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object.</returns>
	public override string ToString()
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds any global catalog in this forest.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> of this <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" />.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A global catalog cannot be located.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public GlobalCatalog FindGlobalCatalog()
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds a global catalog in this forest for a specified site.</summary>
	/// <param name="siteName">The name of the site to search.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> object for the specified site.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A global catalog cannot be located.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="siteName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="siteName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public GlobalCatalog FindGlobalCatalog(string siteName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds a global catalog in this forest for a specified location criteria.</summary>
	/// <param name="flag">A <see cref="T:System.DirectoryServices.ActiveDirectory.LocatorOptions" /> value specifying the location criteria.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> object for the specified location criteria.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A global catalog cannot be located.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public GlobalCatalog FindGlobalCatalog(LocatorOptions flag)
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds a global catalog in this forest for a specified site and location criteria.</summary>
	/// <param name="siteName">The name of the site to search.</param>
	/// <param name="flag">A <see cref="T:System.DirectoryServices.ActiveDirectory.LocatorOptions" /> value that specifies the location criteria.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> object for the specified site and location criteria.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A global catalog cannot be located.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="siteName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="siteName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public GlobalCatalog FindGlobalCatalog(string siteName, LocatorOptions flag)
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds all the global catalogs in this forest.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalogCollection" /> that contains the collection of global catalogs that were found.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public GlobalCatalogCollection FindAllGlobalCatalogs()
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds all the global catalogs in this forest for a given site.</summary>
	/// <param name="siteName">The name of the site to search.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalogCollection" /> object that contains the collection of global catalogs that were found.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="siteName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="siteName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public GlobalCatalogCollection FindAllGlobalCatalogs(string siteName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds all of the discoverable global catalogs in this forest.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalogCollection" /> that contains the collection of global catalogs that were found.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public GlobalCatalogCollection FindAllDiscoverableGlobalCatalogs()
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds all of the discoverable global catalogs in this forest in a specified site.</summary>
	/// <param name="siteName">The name of the site to search.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalogCollection" /> that contains the collection of global catalogs that were found.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="siteName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="siteName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public GlobalCatalogCollection FindAllDiscoverableGlobalCatalogs(string siteName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a collection of the trust relationships of the current forest.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformationCollection" /> collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> objects that represents trust relationships of the current <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" />.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public TrustRelationshipInformationCollection GetAllTrustRelationships()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the trust relationship between this forest and the specified forest.</summary>
	/// <param name="targetForestName">The name of the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object for which the trust relationship information is to be obtained.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipInformation" /> object that represents the trust relationship between this forest and the specified forest.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> that is specified by <paramref name="targetForestName" />.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetForestName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetForestName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public ForestTrustRelationshipInformation GetTrustRelationship(string targetForestName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a Boolean value that indicates whether selective authentication is enabled on the inbound trust relationship with the specified forest.  <see langword="true" /> if selective authentication is enabled; otherwise, <see langword="false" />.</summary>
	/// <param name="targetForestName">The DNS name of the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> with which the inbound trust relationship exists.</param>
	/// <returns>
	///   <see langword="true" /> if selective authentication is enabled; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> that is specified by <paramref name="targetForestName" />.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">The call to <c>LsaQueryTrustedDomainInfoByName</c> failed. For more information, see the LsaQueryTrustedDomainInfoByName.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetForestName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetForestName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public bool GetSelectiveAuthenticationStatus(string targetForestName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Enables or disables selective authentication for an inbound trust.</summary>
	/// <param name="targetForestName">The DNS name of the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object with which the inbound trust relationship exists.</param>
	/// <param name="enable">
	///   <see langword="true" /> if selective authentication is to be enabled; otherwise, <see langword="false" />.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the forest that is specified by <paramref name="targetForestName" />.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetForestName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetForestName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void SetSelectiveAuthenticationStatus(string targetForestName, bool enable)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the SID filtering status of a trust.</summary>
	/// <param name="targetForestName">The DNS name of the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object with which the trust relationship exists.</param>
	/// <returns>
	///   <see langword="true" /> if SID filtering is enabled; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object that is specified by <paramref name="targetForestName" />.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetForestName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetForestName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public bool GetSidFilteringStatus(string targetForestName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets the SID filtering state with the specified forest.</summary>
	/// <param name="targetForestName">The DNS name of the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object with which the trust relationship exists.</param>
	/// <param name="enable">
	///   <see langword="true" /> if SID filtering is to be enabled; otherwise, <see langword="false" />.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the forest that is specified by <paramref name="targetForestName" />.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetForestName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetForestName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	public void SetSidFilteringStatus(string targetForestName, bool enable)
	{
		throw new NotImplementedException();
	}

	/// <summary>Deletes the local side of a trust relationship.</summary>
	/// <param name="targetForestName">The DNS name of the forest that the trust exists with.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> that is specified by the <paramref name="targetForestName" /> parameter.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetForestName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetForestName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void DeleteLocalSideOfTrustRelationship(string targetForestName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Deletes both sides of a trust relationship.</summary>
	/// <param name="targetForest">A <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object that represents the forest that the trust exists with.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the specified <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" />.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetForest" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void DeleteTrustRelationship(Forest targetForest)
	{
		throw new NotImplementedException();
	}

	/// <summary>Verifies that a previously established outbound trust with the specified forest is valid.</summary>
	/// <param name="targetForestName">The DNS name of the domain with which the trust exists.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no outbound trust relationship with the forest that is specified by <paramref name="targetForestName" />.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetForestName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetForestName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void VerifyOutboundTrustRelationship(string targetForestName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Verifies that a previously established trust with the specified forest is valid.</summary>
	/// <param name="targetForest">A <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object for which to verify the trust relationship.</param>
	/// <param name="direction">A <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value that specifies the direction of the trust, relative to this forest.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the forest that is specified by <paramref name="targetForest" />, or the target forest does not have the trust direction that is specified by <paramref name="direction" />.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetForest" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="direction" /> parameter is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void VerifyTrustRelationship(Forest targetForest, TrustDirection direction)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates the local side of a trust relationship with the specified forest.</summary>
	/// <param name="targetForestName">The DNS name of the forest that the trust is created with.</param>
	/// <param name="direction">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> members that determines the direction of the trust, relative to this forest.</param>
	/// <param name="trustPassword">The password for the trust.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException">The trust relationship already exists.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetForest" /> name or <paramref name="trustPassword" /> string is empty.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetForest" /> or <paramref name="trustPassword" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="direction" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void CreateLocalSideOfTrustRelationship(string targetForestName, TrustDirection direction, string trustPassword)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates both sides of a trust relationship with the specified forest.</summary>
	/// <param name="targetForest">A <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object that represents the forest that the trust is being created with.</param>
	/// <param name="direction">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> members that determines the direction of the trust, relative to this forest.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException">The trust relationship already exists.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetForest" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="direction" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void CreateTrustRelationship(Forest targetForest, TrustDirection direction)
	{
		throw new NotImplementedException();
	}

	/// <summary>Updates the password for the local side of a trust relationship.</summary>
	/// <param name="targetForestName">The name of the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object for which the local trust password is to be changed.</param>
	/// <param name="newTrustPassword">The new password for the trust relationship.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the forest that is specified by <paramref name="targetForestName" />.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetForestName" /> or <paramref name="newTrustPassword" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetForestName" /> or <paramref name="newTrustPassword" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void UpdateLocalSideOfTrustRelationship(string targetForestName, string newTrustPassword)
	{
		throw new NotImplementedException();
	}

	/// <summary>Updates the password and trust direction for the local side of a trust relationship.</summary>
	/// <param name="targetForestName">The name of the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object for which the local trust direction and password must be changed.</param>
	/// <param name="newTrustDirection">A <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value for the new trust direction for the trust relationship.</param>
	/// <param name="newTrustPassword">The new password for the trust relationship.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object that is specified by <paramref name="targetForestName" />.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetForestName" /> or <paramref name="newTrustPassword" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetForestName" /> or <paramref name="newTrustPassword" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="newTrustDirection" /> parameter is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void UpdateLocalSideOfTrustRelationship(string targetForestName, TrustDirection newTrustDirection, string newTrustPassword)
	{
		throw new NotImplementedException();
	}

	/// <summary>Updates the trust direction for a trust relationship. The trust directions are updated on both sides of the trust.</summary>
	/// <param name="targetForest">A <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object for which the trust directions must be changed.</param>
	/// <param name="newTrustDirection">A <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value that specifies the new trust direction for the trust relationship.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the forest that is specified by <paramref name="targetForest" />.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetForest" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="newTrustDirection" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void UpdateTrustRelationship(Forest targetForest, TrustDirection newTrustDirection)
	{
		throw new NotImplementedException();
	}

	/// <summary>Repairs a trust relationship.</summary>
	/// <param name="targetForest">A <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object that represents the forest object with which the trust exists.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object that is specified by <paramref name="targetForest" />.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetForest" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void RepairTrustRelationship(Forest targetForest)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object for the current user context.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object that represents the current forest.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	public static Forest GetCurrentForest()
	{
		throw new NotImplementedException();
	}
}
