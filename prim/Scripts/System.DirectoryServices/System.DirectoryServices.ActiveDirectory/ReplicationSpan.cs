namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Determines the span of a replication connection.</summary>
public enum ReplicationSpan
{
	/// <summary>The source and destination servers are in the same site.</summary>
	IntraSite,
	/// <summary>The source and destination servers are in different sites.</summary>
	InterSite
}
