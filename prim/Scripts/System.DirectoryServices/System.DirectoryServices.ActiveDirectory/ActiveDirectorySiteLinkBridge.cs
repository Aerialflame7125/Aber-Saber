using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> class represents a set of site links that communicate through a transport.</summary>
[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
public class ActiveDirectorySiteLinkBridge : IDisposable
{
	/// <summary>Gets the name of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents the name of the current site link bridge object.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public string Name
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a collection of site link objects that are associated with the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkCollection" /> object that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> objects that are associated with the current site link bridge object.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ActiveDirectorySiteLinkCollection SiteLinks
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the transport type for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value that represents the transport type that is used by the current site link bridge object.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ActiveDirectoryTransportType TransportType
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> class using the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object and name.</summary>
	/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that specifies the context for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</param>
	/// <param name="bridgeName">A <see cref="T:System.String" /> that specifies the name for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons:  
	///
	/// The <paramref name="context" /> parameter does not refer to a valid forest, configuration set,  domain controller, or AD LDS server.  
	///
	/// The <paramref name="bridgeName" /> parameter is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="context" /> parameter or the <paramref name="bridgeName" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
	public ActiveDirectorySiteLinkBridge(DirectoryContext context, string bridgeName)
		: this(context, bridgeName, ActiveDirectoryTransportType.Rpc)
	{
	}

	/// <summary>Initializes an instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> class using the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object, name, and transport type.</summary>
	/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that specifies the context for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</param>
	/// <param name="bridgeName">A <see cref="T:System.String" /> that specifies the name for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</param>
	/// <param name="transport">A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value that specifies the transport type to be used.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons:  
	///
	/// The <paramref name="context" /> parameter does not refer to a valid forest, configuration set, domain controller, or AD LDS server.  
	///
	/// The <paramref name="bridgeName" /> parameter is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="context" /> parameter or the <paramref name="bridgeName" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="transport" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value.</exception>
	/// <exception cref="T:System.NotSupportedException">The transport type specified in the <paramref name="transport" /> parameter is not supported.</exception>
	public ActiveDirectorySiteLinkBridge(DirectoryContext context, string bridgeName, ActiveDirectoryTransportType transport)
	{
	}

	/// <summary>Gets a <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object that matches a given directory context and name for the RPC transport protocol only.</summary>
	/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that specifies the context for the search.</param>
	/// <param name="bridgeName">A <see cref="T:System.String" /> that specifies the name of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object to search for.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object. <see langword="null" /> if the object was not found.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">In the <paramref name="context" /> parameter that was specified, the site link bridge could not be found for the given <paramref name="bridgeName" /> parameter.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons.  
	///
	/// The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or an AD LDS server.  
	///
	/// The <paramref name="bridgeName" /> parameter is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="context" /> or the <paramref name="bridgeName" /> parameter is <see langword="null" />.</exception>
	public static ActiveDirectorySiteLinkBridge FindByName(DirectoryContext context, string bridgeName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object that matches a given directory context, name, and transport type.</summary>
	/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that specifies the context for the search.</param>
	/// <param name="bridgeName">A <see cref="T:System.String" /> that specifies the name of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object to search for.</param>
	/// <param name="transport">A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value that specifies the transport type of the object to search for.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object. <see langword="null" /> if the object was not found.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">In the <paramref name="context" /> parameter that was specified, the site link bridge could not be found for the given <paramref name="bridgeName" /> parameter.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons:  
	///
	/// The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or an AD LDS server.  
	///
	/// The <paramref name="bridgeName" /> parameter is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="context" /> or the <paramref name="bridgeName" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transport" /> parameter is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value.</exception>
	public static ActiveDirectorySiteLinkBridge FindByName(DirectoryContext context, string bridgeName, ActiveDirectoryTransportType transport)
	{
		throw new NotImplementedException();
	}

	/// <summary>Commits all changes to the current <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object to the underlying directory store.</summary>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException">The site link bridge object already exists.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void Save()
	{
		throw new NotImplementedException();
	}

	/// <summary>Deletes the site link bridge.</summary>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void Delete()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the name of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents the name of the current <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public override string ToString()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the <see cref="T:System.DirectoryServices.DirectoryEntry" /> object for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the directory entry for the site link bridge object.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DirectoryEntry GetDirectoryEntry()
	{
		throw new NotImplementedException();
	}

	/// <summary>Releases the resources that are used by the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</summary>
	public void Dispose()
	{
	}

	/// <summary>Releases the unmanaged resources that are used by the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object and, optionally, releases unmanaged resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> if the managed resources should be released; <see langword="false" /> if only the unmanaged resources should be released.</param>
	protected virtual void Dispose(bool disposing)
	{
		throw new NotImplementedException();
	}
}
