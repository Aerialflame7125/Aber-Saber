namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Indicates the mode that a domain is operating in.</summary>
public enum DomainMode
{
	/// <summary>The domain is operating in Windows 2000 mixed mode.</summary>
	Windows2000MixedDomain,
	/// <summary>The domain is operating in Windows 2000 native mode.</summary>
	Windows2000NativeDomain,
	/// <summary>The domain is operating in Windows Server 2003 domain-function mode.</summary>
	Windows2003InterimDomain,
	/// <summary>The domain is operating in Windows Server 2003 mode.</summary>
	Windows2003Domain,
	/// <summary>The domain is operating in Windows Server 2008 mode.</summary>
	Windows2008Domain,
	/// <summary>The domain is operating in Windows Server 2008 R2 mode.</summary>
	Windows2008R2Domain
}
