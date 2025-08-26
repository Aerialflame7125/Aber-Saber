namespace System.DirectoryServices;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> enumeration specifies the access rights that are assigned to an Active Directory Domain Services object.</summary>
[Flags]
public enum ActiveDirectoryRights
{
	/// <summary>The right to delete the object.</summary>
	Delete = 0x10000,
	/// <summary>The right to read data from the security descriptor of the object, not including the data in the SACL.</summary>
	ReadControl = 0x20000,
	/// <summary>The right to modify the DACL in the object security descriptor.</summary>
	WriteDacl = 0x40000,
	/// <summary>The right to assume ownership of the object. The user must be an object trustee. The user cannot transfer the ownership to other users.</summary>
	WriteOwner = 0x80000,
	/// <summary>The right to use the object for synchronization. This right enables a thread to wait until that object is in the signaled state.</summary>
	Synchronize = 0x100000,
	/// <summary>The right to get or set the SACL in the object security descriptor.</summary>
	AccessSystemSecurity = 0x1000000,
	/// <summary>The right to read permissions on this object, read all the properties on this object, list this object name when the parent container is listed, and list the contents of this object if it is a container.</summary>
	GenericRead = 0x20094,
	/// <summary>The right to read permissions on this object, write all the properties on this object, and perform all validated writes to this object.</summary>
	GenericWrite = 0x20028,
	/// <summary>The right to read permissions on, and list the contents of, a container object.</summary>
	GenericExecute = 0x20004,
	/// <summary>The right to create or delete children, delete a subtree, read and write properties, examine children and the object itself, add and remove the object from the directory, and read or write with an extended right.</summary>
	GenericAll = 0xF01FF,
	/// <summary>The right to create children of the object.</summary>
	CreateChild = 1,
	/// <summary>The right to delete children of the object.</summary>
	DeleteChild = 2,
	/// <summary>The right to list children of this object. For more information about this right, see the topic "Controlling Object Visibility" in the MSDN Library http://msdn.microsoft.com/library.</summary>
	ListChildren = 4,
	/// <summary>The right to perform an operation that is controlled by a validated write access right.</summary>
	Self = 8,
	/// <summary>The right to read properties of the object.</summary>
	ReadProperty = 0x10,
	/// <summary>The right to write properties of the object.</summary>
	WriteProperty = 0x20,
	/// <summary>The right to delete all children of this object, regardless of the permissions of the children.</summary>
	DeleteTree = 0x40,
	/// <summary>The right to list a particular object. For more information about this right, see the topic "Controlling Object Visibility" in the MSDN Library at http://msdn.microsoft.com/library.</summary>
	ListObject = 0x80,
	/// <summary>A customized control access right. For a list of possible extended rights, see the topic "Extended Rights" in the MSDN Library at http://msdn.microsoft.com. For more information about extended rights, see the topic "Control Access Rights" in the MSDN Library at http://msdn.microsoft.com.</summary>
	ExtendedRight = 0x100
}
