namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Specifies the class type of an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</summary>
public enum SchemaClassType
{
	/// <summary>The class is a type 88 class. Classes defined before 1993 are not required to be included in another category; assigning classes to categories was not required in the X.500 1988 specification. Classes defined prior to the X.500 1993 standards default to the 1988 class. This type of class is specified by a value of 0 in the objectClassCategory attribute.</summary>
	Type88,
	/// <summary>The class is a structural class.</summary>
	Structural,
	/// <summary>The class is an abstract class.</summary>
	Abstract,
	/// <summary>The class is an auxiliary class.</summary>
	Auxiliary
}
