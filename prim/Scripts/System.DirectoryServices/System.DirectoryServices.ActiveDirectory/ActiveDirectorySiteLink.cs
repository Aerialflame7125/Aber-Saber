using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> class represents a set of two or more sites that can be scheduled, for replication, to communicate at uniform cost and through a particular transport.</summary>
[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
public class ActiveDirectorySiteLink : IDisposable
{
	/// <summary>Gets the name of the site link.</summary>
	/// <returns>The name of the site link.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public string Name
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the transport type of the site link.</summary>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value indicating the transport type of this site link.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ActiveDirectoryTransportType TransportType
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a read/write collection of sites that this site link contains.</summary>
	/// <returns>A writable <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteCollection" /> collection of sites that this site link contains. Sites can be added and deleted from this collection.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">An Active Directory Domain Services operation failed. See the exception for details.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ActiveDirectorySiteCollection Sites
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the site link cost.</summary>
	/// <returns>A cost that is associated with this site link. The default value is 100.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">An Active Directory Domain Services operation failed. See the exception for details.</exception>
	/// <exception cref="T:System.ArgumentException">The cost is less than zero. (applies to set only)</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public int Cost
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

	/// <summary>Gets or sets the replication interval between sites.</summary>
	/// <returns>The replication interval between sites.</returns>
	/// <exception cref="T:System.ArgumentException">Invalid <paramref name="ReplicationInterval" /> specified.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public TimeSpan ReplicationInterval
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

	/// <summary>Gets or sets the mode for reciprocal replication between sites.</summary>
	/// <returns>
	///   <see langword="true" /> if reciprocal replication is enabled; <see langword="false" /> if reciprocal replication is disabled.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">An Active Directory Domain Services operation failed. See the exception for details.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public bool ReciprocalReplicationEnabled
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

	/// <summary>Gets or sets a value indicating whether notifications are enabled.</summary>
	/// <returns>
	///   <see langword="true" /> if notifications are enabled; <see langword="false" /> if notifications are disabled.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">An Active Directory Domain Services operation failed. See the exception for details.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public bool NotificationEnabled
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

	/// <summary>Gets or sets the data compression mode of the site link.</summary>
	/// <returns>
	///   <see langword="true" /> if data compression mode is enabled; <see langword="false" /> if data compression is disabled.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">An Active Directory Domain Services operation failed. See the exception for details.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public bool DataCompressionEnabled
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

	/// <summary>Gets or sets the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchedule" /> object for the current site link object.</summary>
	/// <returns>Gets or sets the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchedule" /> object for the current site link object. Setting this property changes the replication schedule for the site link.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">An Active Directory Domain Services operation failed. See the exception for details.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ActiveDirectorySchedule InterSiteReplicationSchedule
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

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> class using the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object and name.</summary>
	/// <param name="context">An <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object for creating this site link.</param>
	/// <param name="siteLinkName">The name for the site link.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons.  
	///
	/// The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or an AD LDS server.  
	///
	/// <paramref name="siteLinkName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> or <paramref name="siteLinkName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
	public ActiveDirectorySiteLink(DirectoryContext context, string siteLinkName)
		: this(context, siteLinkName, ActiveDirectoryTransportType.Rpc, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> class using the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object, name, and transport type.</summary>
	/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object for creating this site link.</param>
	/// <param name="siteLinkName">The name for the site link.</param>
	/// <param name="transport">An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> object that specifies the transport type.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons.  
	///
	/// The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or an AD LDS server.  
	///
	/// <paramref name="siteLinkName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> parameter or <paramref name="siteLinkName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="transport" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value.</exception>
	/// <exception cref="T:System.NotSupportedException">The <paramref name="transport" /> type is not supported.</exception>
	public ActiveDirectorySiteLink(DirectoryContext context, string siteLinkName, ActiveDirectoryTransportType transport)
		: this(context, siteLinkName, transport, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> class using the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object, name, transport type, and replication schedule.</summary>
	/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object for creating this site link.</param>
	/// <param name="siteLinkName">The name for the site link.</param>
	/// <param name="transport">An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> object that specifies the transport type.</param>
	/// <param name="schedule">An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchedule" /> object that specifies the replication schedule for this site link.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons.  
	///
	/// The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or an AD LDS server.  
	///
	/// <paramref name="siteLinkName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> or the <paramref name="siteLinkName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="transport" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value.</exception>
	/// <exception cref="T:System.NotSupportedException">The <paramref name="transport" /> type is not supported.</exception>
	public ActiveDirectorySiteLink(DirectoryContext context, string siteLinkName, ActiveDirectoryTransportType transport, ActiveDirectorySchedule schedule)
	{
	}

	/// <summary>Returns a site link based on a site link name.</summary>
	/// <param name="context">An <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that is valid for this site link.</param>
	/// <param name="siteLinkName">The name of the site link to find.</param>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object for the requested site link.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">The site could not be found for the given <paramref name="siteLinkName" /> in the <paramref name="context" /> specified.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">This exception will occur for any one of the following reasons:  
	///
	/// The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or an AD LDS server.  
	///
	/// <paramref name="siteLinkName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> or <paramref name="siteLinkName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
	public static ActiveDirectorySiteLink FindByName(DirectoryContext context, string siteLinkName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a site link based on a site link name and transport.</summary>
	/// <param name="context">An <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that is valid for this site link.</param>
	/// <param name="siteLinkName">The name of the site link to find.</param>
	/// <param name="transport">An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> object that specifies the transport type.</param>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object for the requested site link.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">The site could not be found for the given <paramref name="siteLinkName" /> in the <paramref name="context" /> specified.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">This exception will occur for any one of the following reasons:  
	///
	/// The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or an AD LDS server.  
	///
	/// <paramref name="siteLinkName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> or <paramref name="siteLinkName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transport" /> parameter is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value.</exception>
	/// <exception cref="T:System.NotSupportedException">The <paramref name="transport" /> type is not supported.</exception>
	/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
	public static ActiveDirectorySiteLink FindByName(DirectoryContext context, string siteLinkName, ActiveDirectoryTransportType transport)
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes any changes to the object to the Active Directory Domain Services store.</summary>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException">The site object already exists.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void Save()
	{
		throw new NotImplementedException();
	}

	/// <summary>Deletes the current site link.</summary>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void Delete()
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the site link name.</summary>
	/// <returns>A string that contains the name of the site link.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public override string ToString()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the <see cref="T:System.DirectoryServices.DirectoryEntry" /> for this object.</summary>
	/// <returns>The <see cref="T:System.DirectoryServices.DirectoryEntry" /> object for this site link.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
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
