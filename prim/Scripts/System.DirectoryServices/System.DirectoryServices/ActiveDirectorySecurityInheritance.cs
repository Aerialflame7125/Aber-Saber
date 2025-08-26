namespace System.DirectoryServices;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration specifies if, and how, ACE information is applied to an object and its descendents.</summary>
public enum ActiveDirectorySecurityInheritance
{
	/// <summary>Indicates no inheritance. The ACE information is only used on the object on which the ACE is set.  ACE information is not inherited by any descendents of the object.</summary>
	None,
	/// <summary>Indicates inheritance that includes the object to which the ACE is applied, the object's immediate children, and the descendents of the object's children.</summary>
	All,
	/// <summary>Indicates inheritance that includes the object's immediate children and the descendants of the object's children, but not the object itself.</summary>
	Descendents,
	/// <summary>Indicates inheritance that includes the object itself and its immediate children.  It does not include the descendents of its children.</summary>
	SelfAndChildren,
	/// <summary>Indicates inheritance that includes the object's immediate children only, not the object itself or the descendents of its children.</summary>
	Children
}
