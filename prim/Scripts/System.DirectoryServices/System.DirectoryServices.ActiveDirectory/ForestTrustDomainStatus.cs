namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Indicates the status of a forest trust relationship.</summary>
public enum ForestTrustDomainStatus
{
	/// <summary>The forest trust relationship is enabled.</summary>
	Enabled = 0,
	/// <summary>The forest trust SID is disabled by administrative action.</summary>
	SidAdminDisabled = 1,
	/// <summary>The forest trust SID is disabled due to a conflict with an existing SID.</summary>
	SidConflictDisabled = 2,
	/// <summary>The forest trust NetBIOS record is disabled by administrative action.</summary>
	NetBiosNameAdminDisabled = 4,
	/// <summary>The forest trust NetBIOS record is disabled due to a conflict with an existing NetBIOS record.</summary>
	NetBiosNameConflictDisabled = 8
}
