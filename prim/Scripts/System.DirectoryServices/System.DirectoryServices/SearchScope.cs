namespace System.DirectoryServices;

/// <summary>Specifies the possible scopes for a directory search that is performed using the <see cref="T:System.DirectoryServices.DirectorySearcher" /> object.</summary>
[Serializable]
public enum SearchScope
{
	/// <summary>Limits the search to the base object. The result contains a maximum of one object.  When the <see cref="P:System.DirectoryServices.DirectorySearcher.AttributeScopeQuery" /> property is specified for a search, the scope of the search must be set to <see cref="F:System.DirectoryServices.SearchScope.Base" />.</summary>
	Base,
	/// <summary>Searches the immediate child objects of the base object, excluding the base object.</summary>
	OneLevel,
	/// <summary>Searches the whole subtree, including the base object and all its child objects. If the scope of a directory search is not specified, a <see cref="F:System.DirectoryServices.SearchScope.Subtree" /> type of search is performed.</summary>
	Subtree
}
