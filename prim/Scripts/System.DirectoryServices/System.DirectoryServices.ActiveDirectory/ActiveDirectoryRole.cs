namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Identifies specific roles within a domain.</summary>
public enum ActiveDirectoryRole
{
	/// <summary>Identifies the schema master role.</summary>
	SchemaRole,
	/// <summary>Identifies the domain naming master role.</summary>
	NamingRole,
	/// <summary>Identifies the primary domain controller (PDC) emulator role.</summary>
	PdcRole,
	/// <summary>Identifies the relative identifier (RID) master role.</summary>
	RidRole,
	/// <summary>Identifies the infrastructure role.</summary>
	InfrastructureRole
}
