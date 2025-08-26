namespace System.DirectoryServices;

/// <summary>The <see cref="T:System.DirectoryServices.SortDirection" /> enumeration specifies how to sort the results of an Active Directory Domain Services query.</summary>
[Serializable]
public enum SortDirection
{
	/// <summary>Sort from smallest to largest. For example, A to Z.</summary>
	Ascending,
	/// <summary>Sort from largest to smallest. For example, Z to A.</summary>
	Descending
}
