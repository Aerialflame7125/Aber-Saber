namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustDomainInformation" /> class contains information about a <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object and is contained in a <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustDomainInfoCollection" /> object.</summary>
public class ForestTrustDomainInformation
{
	/// <summary>Gets the DNS name of the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object.</summary>
	/// <returns>A string that contains the DNS name of the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object.</returns>
	public string DnsName
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the NetBIOS name of the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object.</summary>
	/// <returns>A string that contains the NetBIOS name of the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object.</returns>
	public string NetBiosName
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the SID of the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object.</summary>
	/// <returns>A string that contains the SID of the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object.</returns>
	public string DomainSid
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the status of the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object.</summary>
	/// <returns>One of the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustDomainStatus" /> values that represents the status of the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="Status" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustDomainStatus" /> enumeration value.</exception>
	public ForestTrustDomainStatus Status
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
}
