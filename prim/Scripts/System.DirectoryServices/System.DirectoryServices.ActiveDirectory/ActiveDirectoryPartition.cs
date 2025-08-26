using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryPartition" /> class is an abstract class that represents a directory partition in a domain.</summary>
public abstract class ActiveDirectoryPartition : IDisposable
{
	/// <summary>Gets the partition name.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains the partition name.</returns>
	public string Name
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryPartition" /> class.</summary>
	protected ActiveDirectoryPartition()
	{
	}

	/// <summary>Releases all managed and unmanaged resources that are held by the object.</summary>
	public void Dispose()
	{
	}

	/// <summary>Releases the managed resources that are used by the object and, optionally, releases unmanaged resources.</summary>
	/// <param name="disposing">A <see cref="T:System.Boolean" /> value that determines if the managed resources should be released. <see langword="true" /> if the managed resources are released; <see langword="false" /> if only the unmanaged resources are released.</param>
	protected virtual void Dispose(bool disposing)
	{
	}

	/// <summary>Retrieves a string representation of the current directory partition.</summary>
	/// <returns>Returns a string representation of the current directory partition.</returns>
	public override string ToString()
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves a <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the directory partition.</summary>
	/// <returns>Returns a <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the directory partition.</returns>
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public abstract DirectoryEntry GetDirectoryEntry();
}
