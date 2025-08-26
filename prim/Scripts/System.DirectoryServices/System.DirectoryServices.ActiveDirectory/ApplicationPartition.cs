using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> class represents an application partition for a particular domain.</summary>
[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
public class ApplicationPartition : ActiveDirectoryPartition
{
	/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServerCollection" /> object that contains the directory servers that host this application partition.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServerCollection" /> object that contains <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> objects that represent directory servers for this application partition.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object has not yet been committed to the underlying directory store.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DirectoryServerCollection DirectoryServers
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the security reference domain for this application partition.</summary>
	/// <returns>The distinguished name of the security reference domain for this application.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object has not yet been committed to the underlying directory store (applies to set only).</exception>
	/// <exception cref="T:System.NotSupportedException">The application partition is an AD LDS application partition.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public string SecurityReferenceDomain
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

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> class, using the specified distinguished name.</summary>
	/// <param name="context">The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that is used to create this application partition.</param>
	/// <param name="distinguishedName">The <see cref="T:System.String" /> that specifies the distinguished name for this application partition.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">The target in the <paramref name="context" /> parameter is not a server, or the <paramref name="distinguishedName" /> parameter is not in a valid distinguished name format.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> or <paramref name="distinguishedName" /> is <see langword="null" />.</exception>
	public ApplicationPartition(DirectoryContext context, string distinguishedName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> class, using the specified distinguished name and object class.</summary>
	/// <param name="context">The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that is used to create this application partition.</param>
	/// <param name="distinguishedName">The <see cref="T:System.String" /> that specifies the distinguished name for this application partition.</param>
	/// <param name="objectClass">The <see cref="T:System.String" /> that specifies the object class that represents this application partition.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">The target in the <paramref name="context" /> parameter is not a server, or the <paramref name="distinguishedName" /> parameter is not in a valid distinguished name format.  This exception is also thrown if the application partition is being created within an Active Directory forest, rather than an AD LDS configuration set, because the <paramref name="objectClass" /> can only be specified for AD LDS configuration sets.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> or <paramref name="distinguishedName" /> is <see langword="null" />.</exception>
	public ApplicationPartition(DirectoryContext context, string distinguishedName, string objectClass)
	{
		throw new NotImplementedException();
	}

	/// <summary>Releases the managed resources that are used by the <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object and optionally releases unmanaged resources.</summary>
	/// <param name="disposing">A <see cref="T:System.Boolean" /> value that determines if the managed resources should be released. <see langword="true" /> if the managed resources should be released; <see langword="false" /> if only the unmanaged resources should be released.</param>
	protected override void Dispose(bool disposing)
	{
	}

	/// <summary>Returns an <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object for a specified directory context.</summary>
	/// <param name="context">An <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use to retrieve the application partition object.  The directory context must specify the DNS name of the application partition.</param>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object that represents the application partition for the specified directory context.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A target specified in <paramref name="context" /> could not be found.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="context" /> is not valid.</exception>
	public static ApplicationPartition GetApplicationPartition(DirectoryContext context)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns an <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object for a given <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object and distinguished name.</summary>
	/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use for the search.</param>
	/// <param name="distinguishedName">A <see cref="T:System.String" /> that contains the distinguished name of the application partition to search for.</param>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object that represents the results of the search.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">This exception will occur for any one of the following reasons:  
	///
	/// A target specified in the <paramref name="context" /> parameter could not be found.  
	///
	/// The target is a configuration set and no AD LDS instance was found in that configuration set.  
	///
	/// The target is a forest and the application partition was not found in that forest.  
	///
	/// The target does not host the current application partition.  
	///
	/// No AD LDS instance was found for the application partition.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> or <paramref name="distinguishedName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons:  
	///
	/// The <paramref name="context" /> parameter is not a valid forest, configuration set, or directory server.  
	///
	/// The <paramref name="distinguishedName" /> has a zero length.
	///
	/// The <paramref name="distinguishedName" /> parameter is in an invalid format.</exception>
	public static ApplicationPartition FindByName(DirectoryContext context, string distinguishedName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object for this application partition and current site.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object for this application partition and site.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object has not yet been committed to the underlying directory store.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DirectoryServer FindDirectoryServer()
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object for the application partition and a specified site.</summary>
	/// <param name="siteName">A <see cref="T:System.String" /> that specifies a site name.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object for this application partition and specified site.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="siteName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="siteName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object has not yet been committed to the underlying directory store.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DirectoryServer FindDirectoryServer(string siteName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object for the application partition and current site with an option to ignore cached information.</summary>
	/// <param name="forceRediscovery">A <see cref="T:System.Boolean" /> value that indicates whether cached information should be ignored. <see langword="true" /> if cached information should be ignored; otherwise, <see langword="false" />.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object for this application partition and site.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object has not yet been committed to the underlying directory store.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DirectoryServer FindDirectoryServer(bool forceRediscovery)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object for this application partition for a specified site, with an option to ignore cached information.</summary>
	/// <param name="siteName">A <see cref="T:System.String" /> that specifies a site name.</param>
	/// <param name="forceRediscovery">A <see cref="T:System.Boolean" /> value that indicates whether cached information should be ignored. <see langword="true" /> if cached information should be ignored; otherwise, <see langword="false" />.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object for this application partition and specified site.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="siteName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="siteName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object has not yet been committed to the underlying directory store.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DirectoryServer FindDirectoryServer(string siteName, bool forceRediscovery)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyDirectoryServerCollection" /> object that contains all of the directory servers that host this application partition.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyDirectoryServerCollection" /> object that contains all of the directory servers that host this application partition.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object has not yet been committed to the underlying directory store.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ReadOnlyDirectoryServerCollection FindAllDirectoryServers()
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns an <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyDirectoryServerCollection" /> object that contains all of the directory servers that host this application partition in the specified site.</summary>
	/// <param name="siteName">A <see cref="T:System.String" /> that specifies the name of the site to search.</param>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyDirectoryServerCollection" /> object that contains the directory servers that host this application partition in the specified site.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="siteName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="siteName" /> is an empty string.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object has not yet been committed to the underlying directory store.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ReadOnlyDirectoryServerCollection FindAllDirectoryServers(string siteName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyDirectoryServerCollection" /> object that contains all of the directory servers that have registered either a site-specific DNS record for the current site or a non-site-specific DNS record for the application partition.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyDirectoryServerCollection" /> object that contains the directory servers that were found.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object has not yet been committed to the underlying directory store.</exception>
	/// <exception cref="T:System.NotSupportedException">The application partition is an AD LDS application partition.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ReadOnlyDirectoryServerCollection FindAllDiscoverableDirectoryServers()
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyDirectoryServerCollection" /> object that contains all of the directory servers that have registered a site-specific DNS record, for the specified site, for the application partition.</summary>
	/// <param name="siteName">A <see cref="T:System.String" /> that specifies the name of the site to search.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyDirectoryServerCollection" /> object that contains the directory servers that are found.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="siteName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="siteName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object has not yet been committed to the underlying directory store.</exception>
	/// <exception cref="T:System.NotSupportedException">The application partition is an AD LDS application partition.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ReadOnlyDirectoryServerCollection FindAllDiscoverableDirectoryServers(string siteName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Deletes this application partition.</summary>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object has not yet been saved in the underlying directory store.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public void Delete()
	{
		throw new NotImplementedException();
	}

	/// <summary>Commits all changes to the current application partition object to the underlying directory store.</summary>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException">An application partition with the same name already exists.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public void Save()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a <see cref="T:System.DirectoryServices.DirectoryEntry" /> object for this application partition.</summary>
	/// <returns>The <see cref="T:System.DirectoryServices.DirectoryEntry" /> for this application partition.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public override DirectoryEntry GetDirectoryEntry()
	{
		throw new NotImplementedException();
	}
}
