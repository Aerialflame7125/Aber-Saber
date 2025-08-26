using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> class represents an Active Directory domain.</summary>
[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
public class Domain : ActiveDirectoryPartition
{
	/// <summary>Gets the forest that this domain is a member of.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object that represents the forest that this domain is a member of.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public Forest Forest
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the domain controllers in this domain.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainControllerCollection" /> object that contains the domain controllers in this domain.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DomainControllerCollection DomainControllers
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the domains that are children of this domain.</summary>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.DomainCollection" /> object that contains the child domains.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DomainCollection Children
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the mode that this domain is operating in.</summary>
	/// <returns>One of the <see cref="T:System.DirectoryServices.ActiveDirectory.DomainMode" /> values that indicates the mode that this domain is operating in.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DomainMode DomainMode
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the parent domain of this domain.</summary>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the parent domain of this domain. <see langword="null" /> if this domain has no parent domain.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public Domain Parent
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that holds the primary domain controller (PDC) for this domain.</summary>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that represents the domain controller that holds the PDC emulator role for this domain.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DomainController PdcRoleOwner
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the RID master role holder for this domain.</summary>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that represents the domain controller that holds the relative identifier (RID) master role for this domain.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DomainController RidRoleOwner
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the infrastructure role owner for this domain.</summary>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that represents the domain controller that holds the infrastructure owner role.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DomainController InfrastructureRoleOwner
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object for the specified context.</summary>
	/// <param name="context">An <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use to retrieve the object. The type of the context must be a domain or directory server.</param>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain for the specified context.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A connection to the target specified in <paramref name="context" /> could not be made.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="context" /> is not valid.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> is <see langword="null" />.</exception>
	public static Domain GetDomain(DirectoryContext context)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain to which the local computer is joined.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain to which the local machine is joined.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A connection to the domain could not be made.</exception>
	public static Domain GetComputerDomain()
	{
		throw new NotImplementedException();
	}

	/// <summary>Raises the mode of operation for the domain.</summary>
	/// <param name="domainMode">An <see cref="T:System.DirectoryServices.ActiveDirectory.DomainMode" /> enumeration value that specifies the new operation level for the domain.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">Based on the current operating mode of the domain, the value specified for <paramref name="domainMode" /> is not valid.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="domainMode" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.DomainMode" /> enumeration value.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void RaiseDomainFunctionality(DomainMode domainMode)
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds any domain controller in this domain.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> that represents the domain controller that is found by this method.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A domain controller cannot be located.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DomainController FindDomainController()
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds a domain controller in this domain and in the specified site.</summary>
	/// <param name="siteName">The name of the site to search for the domain controller.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that represents the domain controller that is found by this method.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A domain controller cannot be located.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="siteName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="siteName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DomainController FindDomainController(string siteName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds a domain controller in this domain that meets the specified criteria.</summary>
	/// <param name="flag">A combination of one or more of the <see cref="T:System.DirectoryServices.ActiveDirectory.LocatorOptions" /> members that defines the type of domain controller to find.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> that represents the domain controller that is found by this method.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A domain controller cannot be located.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter contains an invalid value.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DomainController FindDomainController(LocatorOptions flag)
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds a domain controller in this domain and in the specified site that meets the specified criteria.</summary>
	/// <param name="siteName">The name of the site to search for the domain controller.</param>
	/// <param name="flag">A combination of one or more of the <see cref="T:System.DirectoryServices.ActiveDirectory.LocatorOptions" /> members that defines the type of domain controller to find.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that represents the domain controller that is found by this method.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A domain controller cannot be located.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons.  
	///
	/// <paramref name="siteName" /> is an empty string.  
	///
	/// <paramref name="flag" /> contains an invalid value.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="siteName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DomainController FindDomainController(string siteName, LocatorOptions flag)
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds all of the domain controllers in this domain.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainControllerCollection" /> that contains the domain controller objects that were found by this method.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DomainControllerCollection FindAllDomainControllers()
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds all of the domain controllers in this domain that are also in the specified site.</summary>
	/// <param name="siteName">The name of the site to search for the domain controllers.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainControllerCollection" /> that contains the domain controller objects that were found by this method.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="siteName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="siteName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DomainControllerCollection FindAllDomainControllers(string siteName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds all of the discoverable domain controllers in this domain.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainControllerCollection" /> that contains the domain controller objects that were found by this method.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DomainControllerCollection FindAllDiscoverableDomainControllers()
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds all of the discoverable domain controllers in this domain that are also in the specified site.</summary>
	/// <param name="siteName">The name of the site to search for the domain controllers.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainControllerCollection" /> that contains the domain controller objects that were found by this method.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="siteName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="siteName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DomainControllerCollection FindAllDiscoverableDomainControllers(string siteName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves a <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the default naming context of the domain.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the domain.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public override DirectoryEntry GetDirectoryEntry()
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves all of the trust relationships for this domain.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformationCollection" /> object that contains all of the trust relationships for this domain.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public TrustRelationshipInformationCollection GetAllTrustRelationships()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the trust relationship between this domain and the specified domain.</summary>
	/// <param name="targetDomainName">The DNS name of the domain with which the trust relationship exists.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object that represents the trust relationship between this domain and the specified domain.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> that is specified by the <paramref name="targetForestName" /> parameter.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetDomainName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetDomainName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public TrustRelationshipInformation GetTrustRelationship(string targetDomainName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Determines the authentication type of an inbound trust.</summary>
	/// <param name="targetDomainName">The DNS name of the domain which with the trust exists.</param>
	/// <returns>
	///   <see langword="true" /> if the authentication of the trust is selective; <see langword="false" /> if the authentication is domain-wide.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> that is specified by the <paramref name="targetForestName" /> parameter.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">The call to <c>LsaQueryTrustedDomainInfoByName</c> failed. For more information, see the LsaQueryTrustedDomainInfoByName.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetDomainName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetDomainName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public bool GetSelectiveAuthenticationStatus(string targetDomainName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Enables or disables selective authentication for an inbound trust.</summary>
	/// <param name="targetDomainName">The DNS name of the domain with which the inbound trust exists.</param>
	/// <param name="enable">
	///   <see langword="true" /> if selective authentication is enabled; otherwise, <see langword="false" />.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the domain controller that is specified by <paramref name="targetDomainName" />.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetDomainName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetDomainName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void SetSelectiveAuthenticationStatus(string targetDomainName, bool enable)
	{
		throw new NotImplementedException();
	}

	/// <summary>Determines the SID filtering status of a trust.</summary>
	/// <param name="targetDomainName">The DNS name of the domain which with the trust relationship exists.</param>
	/// <returns>
	///   <see langword="true" /> if SID filtering is enabled; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> that is specified by the <paramref name="targetForestName" /> parameter.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetDomainName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetDomainName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public bool GetSidFilteringStatus(string targetDomainName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets the SID filtering state for the specified domain.</summary>
	/// <param name="targetDomainName">The DNS name of the domain with which the trust exists.</param>
	/// <param name="enable">
	///   <see langword="true" /> if SID filtering must be enabled; otherwise, <see langword="false" />.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the domain that is specified by <paramref name="targetDomainName" />.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetDomainName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetDomainName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	public void SetSidFilteringStatus(string targetDomainName, bool enable)
	{
		throw new NotImplementedException();
	}

	/// <summary>Deletes the local side of a trust relationship.</summary>
	/// <param name="targetDomainName">The DNS name of the domain that the trust exists with.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> that is specified by the <paramref name="targetDomainName" /> parameter.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetDomainName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetDomainName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void DeleteLocalSideOfTrustRelationship(string targetDomainName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Deletes both sides of a trust relationship.</summary>
	/// <param name="targetDomain">A <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain that the trust exists with.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the specified <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" />.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetDomain" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void DeleteTrustRelationship(Domain targetDomain)
	{
		throw new NotImplementedException();
	}

	/// <summary>Verifies that a previously established outbound trust with the specified domain is valid.</summary>
	/// <param name="targetDomainName">The DNS name of the domain with which the trust exists.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no outbound trust relationship with the domain that is specified by <paramref name="targetDomainName" />.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetDomainName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetDomainName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void VerifyOutboundTrustRelationship(string targetDomainName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Verifies that a previously established trust with the specified domain is valid.</summary>
	/// <param name="targetDomain">A <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain with which the trust exists.</param>
	/// <param name="direction">A <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value that specifies the direction, relative to this domain, of the trust.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the domain that is specified by the <paramref name="targetDomain" /> parameter, or the target domain does not have the trust direction that is specified by the <paramref name="direction" /> parameter.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetDomain" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="direction" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void VerifyTrustRelationship(Domain targetDomain, TrustDirection direction)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates the local side of a trust relationship with the specified domain.</summary>
	/// <param name="targetDomainName">The DNS name of the domain that the trust is created with.</param>
	/// <param name="direction">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> members that determines the direction of the trust, relative to this domain.</param>
	/// <param name="trustPassword">The password for the trust.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException">The trust relationship already exists.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetDomainName" /> or <paramref name="trustPassword" /> is empty.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetDomainName" /> or <paramref name="trustPassword" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="direction" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void CreateLocalSideOfTrustRelationship(string targetDomainName, TrustDirection direction, string trustPassword)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates both sides of a trust relationship with the specified domain.</summary>
	/// <param name="targetDomain">A <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain that the trust is being created with.</param>
	/// <param name="direction">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> members that determines the direction of the trust, relative to this domain.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException">The trust relationship already exists.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetDomain" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="direction" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void CreateTrustRelationship(Domain targetDomain, TrustDirection direction)
	{
		throw new NotImplementedException();
	}

	/// <summary>Updates the password for the local side of a trust relationship.</summary>
	/// <param name="targetDomainName">The DNS name of the domain with which a trust exists.</param>
	/// <param name="newTrustPassword">The new password for the trust.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the domain that is specified by <paramref name="targetDomainName" />.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetDomainName" /> or <paramref name="newTrustPassword" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetDomainName" /> or <paramref name="newTrustPassword" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void UpdateLocalSideOfTrustRelationship(string targetDomainName, string newTrustPassword)
	{
		throw new NotImplementedException();
	}

	/// <summary>Updates the password and trust direction for the local side of a trust relationship.</summary>
	/// <param name="targetDomainName">The DNS name of the domain with which a trust exists.</param>
	/// <param name="newTrustDirection">An <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value for the new trust direction for the trust relationship.</param>
	/// <param name="newTrustPassword">The new password for the trust.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the domain that is specified by the <paramref name="targetDomainName" /> parameter.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="targetDomainName" /> or <paramref name="newTrustPassword" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetDomainName" /> or <paramref name="newTrustPassword" /> are <see langword="null" />.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="newTrustDirection" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void UpdateLocalSideOfTrustRelationship(string targetDomainName, TrustDirection newTrustDirection, string newTrustPassword)
	{
		throw new NotImplementedException();
	}

	/// <summary>Updates the trust direction for an existing trust relationship. The trust directions are updated on both sides of the trust.</summary>
	/// <param name="targetDomain">An <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain with which the trust exists.</param>
	/// <param name="newTrustDirection">An <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value that specifies the new trust direction for the trust relationship.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the domain that is specified by the <paramref name="targetDomain" /> parameter.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetDomain" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="newTrustDirection" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void UpdateTrustRelationship(Domain targetDomain, TrustDirection newTrustDirection)
	{
		throw new NotImplementedException();
	}

	/// <summary>Repairs a trust relationship.</summary>
	/// <param name="targetDomain">An <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain with which the trust exists.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> that is specified by <paramref name="targetDomain" />.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target or source server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="targetDomain" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void RepairTrustRelationship(Domain targetDomain)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object for the current user credentials in effect for the security context under which the application is running.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain for the specified user credentials in effect for the security context under which the application is running.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A connection to the current context could not be made.</exception>
	public static Domain GetCurrentDomain()
	{
		throw new NotImplementedException();
	}
}
