namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Indicates the mode in which a <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> is operating.</summary>
public enum ForestMode
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> is operating in Windows 2000 mode.</summary>
	Windows2000Forest,
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> is operating in Windows Server 2003 domain-function mode.</summary>
	Windows2003InterimForest,
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> is operating in Windows Server 2003 mode.</summary>
	Windows2003Forest,
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> is operating in Windows Server 2008 mode.</summary>
	Windows2008Forest,
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> is operating in Windows Server 2008 R2 mode.</summary>
	Windows2008R2Forest
}
