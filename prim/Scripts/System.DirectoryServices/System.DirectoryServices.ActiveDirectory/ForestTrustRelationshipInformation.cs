using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipInformation" /> class contains information about a trust relationship between two <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> objects.</summary>
[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
public class ForestTrustRelationshipInformation : TrustRelationshipInformation
{
	/// <summary>Gets the top-level names in the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipInformation" /> object.</summary>
	/// <returns>A read-only <see cref="T:System.DirectoryServices.ActiveDirectory.TopLevelNameCollection" /> object that contains the top-level names in the current object.</returns>
	public TopLevelNameCollection TopLevelNames
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the excluded top-level names in the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipInformation" /> object.</summary>
	/// <returns>A read/write <see cref="T:System.Collections.Specialized.StringCollection" /> that contains the excluded top-level names in the current object.</returns>
	public StringCollection ExcludedTopLevelNames
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the trusted domain information for this <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustDomainInformation" /> object.</summary>
	/// <returns>A read-only <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustDomainInfoCollection" /> object that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustDomainInformation" /> object  for the current object.</returns>
	public ForestTrustDomainInfoCollection TrustedDomainInformation
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Commits any changes to the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipInformation" /> properties to the Active Directory Domain Services store.</summary>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionException">A collision occurred with an existing trust relationship.</exception>
	public void Save()
	{
		throw new NotImplementedException();
	}
}
