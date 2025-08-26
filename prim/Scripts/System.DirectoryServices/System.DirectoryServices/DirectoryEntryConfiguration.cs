using System.Security.Permissions;

namespace System.DirectoryServices;

/// <summary>The <see cref="T:System.DirectoryServices.DirectoryEntryConfiguration" /> class provides a direct way to specify and obtain provider-specific options for manipulating a directory object. Typically, the options apply to search operations of the underlying directory store. The supported options are provider-specific.</summary>
[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
public class DirectoryEntryConfiguration
{
	/// <summary>Gets or sets a value that determines if and how referral chasing is pursued.</summary>
	/// <returns>A combination of one or more of the <see cref="T:System.DirectoryServices.ReferralChasingOption" /> enumeration members that specifies if and how referral chasing is pursued.</returns>
	public ReferralChasingOption Referral
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

	/// <summary>Gets or sets a security mask to use with <see cref="T:System.DirectoryServices.DirectoryEntryConfiguration" />.</summary>
	/// <returns>A combination of one or more of the <see cref="T:System.DirectoryServices.SecurityMasks" /> enumeration members that specifies the security mask.</returns>
	public SecurityMasks SecurityMasks
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

	/// <summary>Gets or sets the page size in a paged search.</summary>
	/// <returns>The number of entries in a page.</returns>
	public int PageSize
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

	/// <summary>Gets or sets the port number to use to establish an SSL connection when the password is set or changed.</summary>
	/// <returns>The port number to use to establish an SSL connection when the password is set or changed.</returns>
	public int PasswordPort
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

	/// <summary>Gets or sets the password encoding method.</summary>
	/// <returns>One of the <see cref="T:System.DirectoryServices.PasswordEncodingMethod" /> enumeration members that indicates the type of password encoding.</returns>
	public PasswordEncodingMethod PasswordEncoding
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

	/// <summary>Gets the host name of the server for the current binding to this directory object.</summary>
	/// <returns>The name of the server.</returns>
	public string GetCurrentServerName()
	{
		throw new NotImplementedException();
	}

	/// <summary>Determines if mutual authentication is performed by the SSPI layer.</summary>
	/// <returns>
	///   <see langword="true" /> if mutual authentication has been performed; otherwise, <see langword="false" />.</returns>
	public bool IsMutuallyAuthenticated()
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets the name of a security principal so that when the principal is accessed, its quota information will also be returned.</summary>
	/// <param name="accountName">The account name that is being set to allow queries on its principal name.</param>
	public void SetUserNameQueryQuota(string accountName)
	{
		throw new NotImplementedException();
	}
}
