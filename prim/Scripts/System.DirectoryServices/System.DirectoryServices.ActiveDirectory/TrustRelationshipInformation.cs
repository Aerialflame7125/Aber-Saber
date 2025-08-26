namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> class contains information for a trust relationship between a pair of <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> objects.</summary>
public class TrustRelationshipInformation
{
	/// <summary>Obtains the name of the source <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> objects for this trust relationship.</summary>
	/// <returns>A string that contains the name of the source <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> objects for this trust relationship.</returns>
	public string SourceName
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Obtains the name of the target <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> objects for this trust relationship.</summary>
	/// <returns>A string that contains the name of the target <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> objects for this trust relationship.</returns>
	public string TargetName
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Obtains the <see cref="T:System.DirectoryServices.ActiveDirectory.TrustType" /> object of the trust relationship.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.TrustType" /> value that represents the type of the trust relationship.</returns>
	public TrustType TrustType
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Obtains the <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> objects for this trust relationship relative to the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> objects that created the trust.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value for this trust relationship relative to the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> objects that created the trust.</returns>
	public TrustDirection TrustDirection
	{
		get
		{
			throw new NotImplementedException();
		}
	}
}
