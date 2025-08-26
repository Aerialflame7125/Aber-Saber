namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> class is used to contain replication metadata for an Active Directory Domain Services attribute.</summary>
public class AttributeMetadata
{
	/// <summary>Gets the name of the attribute.</summary>
	/// <returns>The LDAP display name of the attribute.</returns>
	public string Name
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the version number of this attribute.</summary>
	/// <returns>The version number of this attribute. Each originating modification of the attribute increases this value by one.</returns>
	public int Version
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the time at which the last originating change was made to this attribute.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> object that contains the last originating change time for this attribute.</returns>
	public DateTime LastOriginatingChangeTime
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the invocation identifier of the server on which the last change was made to this attribute.</summary>
	/// <returns>A <see cref="T:System.Guid" /> structure that contains the identifier.</returns>
	public Guid LastOriginatingInvocationId
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the update sequence number (USN) on the originating server at which the last change to this attribute was made.</summary>
	/// <returns>An <see cref="T:System.Int64" /> value that contains the USN.</returns>
	public long OriginatingChangeUsn
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the update sequence number (USN) on the destination server at which the last change to this attribute was applied.</summary>
	/// <returns>An <see cref="T:System.Int64" /> value that contains the USN.</returns>
	public long LocalChangeUsn
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the name of the originating server.</summary>
	/// <returns>The distinguished name of the originating server.</returns>
	public string OriginatingServer
	{
		get
		{
			throw new NotImplementedException();
		}
	}
}
