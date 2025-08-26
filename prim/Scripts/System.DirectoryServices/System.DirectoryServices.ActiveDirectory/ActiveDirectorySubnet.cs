using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> class represents a subnet in a <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" />.</summary>
[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
public class ActiveDirectorySubnet : IDisposable
{
	/// <summary>Gets the subnet name of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</summary>
	/// <returns>The name of the subnet.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public string Name
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the site that the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object is a member of.</summary>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object for the site that the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object is a member of.</returns>
	/// <exception cref="T:System.InvalidOperationException">Applies to set only. The specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object does not exist. If it was newly created, it must be committed to the directory store before assigning it to the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ActiveDirectorySite Site
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

	/// <summary>Gets or sets the location description of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</summary>
	/// <returns>The location description of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</returns>
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

	/// <summary>Returns a subnet that is based on a subnet name.</summary>
	/// <param name="context">An <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that is valid for this subnet.</param>
	/// <param name="subnetName">The name of the subnet to find.</param>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> for the requested subnet.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">In the <paramref name="context" /> parameter that was specified, the site could not be found for the given <paramref name="subnetName" /> parameter.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons:  
	///
	/// The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or AD LDS server.  
	///
	/// <paramref name="subnetName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> or <paramref name="subnetName" /> is <see langword="null" />.</exception>
	public static ActiveDirectorySubnet FindByName(DirectoryContext context, string subnetName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes an instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> class, using the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object and subnet name.</summary>
	/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that specifies the context for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</param>
	/// <param name="subnetName">A <see cref="T:System.String" /> that specifies the name of the subnet.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">
	///   <paramref name="context" /> specifies a configuration set, but no AD LDS instance was found.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons:  
	///
	/// <paramref name="context" /> does not refer a valid forest, configuration set, domain controller, or AD LDS server.  
	///
	/// <paramref name="subnetName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> or <paramref name="subnetName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
	public ActiveDirectorySubnet(DirectoryContext context, string subnetName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes an instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> class, using the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object, subnet name, and site name.</summary>
	/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that specifies the context for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</param>
	/// <param name="subnetName">A <see cref="T:System.String" /> that specifies the name of the subnet.</param>
	/// <param name="siteName">A <see cref="T:System.String" /> that specifies the name of the site that contains the subnet.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">
	///   <paramref name="context" /> specifies a configuration set, but no AD LDS instance was found.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons:  
	///
	/// <paramref name="context" /> does not refer to a valid forest, configuration set, domain controller, or AD LDS server.  
	///
	/// <paramref name="subnetName" /> or <paramref name="siteName" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" />, <paramref name="subnetName" />, or <paramref name="siteName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
	public ActiveDirectorySubnet(DirectoryContext context, string subnetName, string siteName)
		: this(context, subnetName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes any changes to the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object to the Active Directory Domain Services store.</summary>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException">The subnet object already exists.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void Save()
	{
		throw new NotImplementedException();
	}

	/// <summary>Deletes the subnet that is represented by this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</summary>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
	public void Delete()
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the name of the subnet.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents the name of the subnet.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public override string ToString()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the <see cref="T:System.DirectoryServices.DirectoryEntry" /> object for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</summary>
	/// <returns>The <see cref="T:System.DirectoryServices.DirectoryEntry" /> object for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DirectoryEntry GetDirectoryEntry()
	{
		throw new NotImplementedException();
	}

	/// <summary>Releases all resources that are used by the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</summary>
	public void Dispose()
	{
	}

	/// <summary>Releases the unmanaged resources that are used by the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> if the managed resources should be released; <see langword="false" /> if only the unmanaged resources should be released.</param>
	protected virtual void Dispose(bool disposing)
	{
	}
}
