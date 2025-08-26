namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Indicates the forest trust account status of a top-level domain in a forest.</summary>
public enum TopLevelNameStatus
{
	/// <summary>The forest trust account is enabled.</summary>
	Enabled = 0,
	/// <summary>The forest trust account was disabled on creation.</summary>
	NewlyCreated = 1,
	/// <summary>The forest trust account is disabled by administrative action.</summary>
	AdminDisabled = 2,
	/// <summary>The forest trust account is disabled due to a conflict with an existing forest trust account.</summary>
	ConflictDisabled = 4
}
