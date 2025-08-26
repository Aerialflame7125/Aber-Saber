using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> class defines a set of domain controllers that are well-connected in terms of speed and cost. A site object consists of a set of one or more IP subnets.</summary>
[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
public class ActiveDirectorySite : IDisposable
{
	/// <summary>Gets the name of the site.</summary>
	/// <returns>A string value that contains the name of the site.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public string Name
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets all domains in the site.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainCollection" /> object containing all domains in the site.</returns>
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

	/// <summary>Returns a writable collection of subnets in the site.</summary>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnetCollection" /> object that contains a writable collection of subnets in the site.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ActiveDirectorySubnetCollection Subnets
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Returns a read-only collection of directory servers in the site.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyDirectoryServerCollection" /> that contains a read-only collection of directory servers in the site.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ReadOnlyDirectoryServerCollection Servers
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a read-only collection of sites that are connected through a common site link with this site object.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlySiteCollection" /> collection that contains a read-only collection of sites that are connected through a common site link with this site.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ReadOnlySiteCollection AdjacentSites
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a read-only collection of site links that involve this site.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlySiteLinkCollection" /> object that contains a read-only collection of site links that this site is in.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ReadOnlySiteLinkCollection SiteLinks
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the directory server that serves as the inter-site topology generator.</summary>
	/// <returns>A read/write <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object that represents the directory server that serves as the inter-site topology generator.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.NotSupportedException">The transport type is not supported.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DirectoryServer InterSiteTopologyGenerator
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the site options.</summary>
	/// <returns>A read/write <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteOptions" /> value that gets or sets the site options.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ActiveDirectorySiteOptions Options
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the location of the site.</summary>
	/// <returns>A string value that gets or sets the location of the site.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public string Location
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a read-only collection of bridgehead servers for this site.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyDirectoryServerCollection" /> collection that contains a read-only collection of directory servers in this site.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ReadOnlyDirectoryServerCollection BridgeheadServers
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Returns a collection of directory servers that are designated as preferred bridgehead servers for the SMTP transport.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServerCollection" /> object that contains a collection of directory servers that are designated as preferred bridgehead servers for the SMTP transport.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DirectoryServerCollection PreferredSmtpBridgeheadServers
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Returns a collection of directory servers that are designated as preferred bridgehead servers for the RPC transport.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServerCollection" /> object that contains the directory servers that are designated as preferred bridgehead servers for the RPC transport.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DirectoryServerCollection PreferredRpcBridgeheadServers
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the default setting for the replication schedule for the site.</summary>
	/// <returns>A read/write <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchedule" /> that represents the default setting for the replication schedule for the site.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ActiveDirectorySchedule IntraSiteReplicationSchedule
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Returns a site based on a site name.</summary>
	/// <param name="context">An <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that is valid for this site.</param>
	/// <param name="siteName">The name of the site to find.</param>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object for the requested site.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">The site could not be found for the given <paramref name="siteName" /> in the <paramref name="context" /> specified.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">This exception will occur for any one of the following reasons:  
	///
	/// The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or an AD LDS server.  
	///
	/// <paramref name="siteName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> or <paramref name="siteName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
	public static ActiveDirectorySite FindByName(DirectoryContext context, string siteName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> class, using the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object.</summary>
	/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object for creating this site.</param>
	/// <param name="siteName">The name for the new site.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">This exception will occur for any one of the following reasons:  
	///
	/// The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or an AD LDS server.  
	///
	/// <paramref name="siteName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> or <paramref name="siteName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
	public ActiveDirectorySite(DirectoryContext context, string siteName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the site that this computer is a member of.</summary>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object that contains the caller's current site.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">The caller's computer does not belong to a site.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	public static ActiveDirectorySite GetComputerSite()
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes any changes to the object to the Active Directory Domain Services store.</summary>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException">The site object already exists.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.NotSupportedException">The transport type is not supported.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void Save()
	{
		throw new NotImplementedException();
	}

	/// <summary>Deletes the current site.</summary>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void Delete()
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the name of the site.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents the name of the site.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public override string ToString()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the <see cref="T:System.DirectoryServices.DirectoryEntry" /> object for this site.</summary>
	/// <returns>The <see cref="T:System.DirectoryServices.DirectoryEntry" /> object for this site.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DirectoryEntry GetDirectoryEntry()
	{
		throw new NotImplementedException();
	}

	/// <summary>Releases all resources used by the object.</summary>
	public void Dispose()
	{
	}

	/// <summary>Releases the unmanaged resources used by the object and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> if the managed resources should be released; <see langword="false" /> if only the unmanaged resources should be released.</param>
	protected virtual void Dispose(bool disposing)
	{
	}
}
