using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters;

/// <summary>Allows access to field names and field types of objects that support the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface.</summary>
[ComVisible(true)]
public interface IFieldInfo
{
	/// <summary>Gets or sets the field names of serialized objects.</summary>
	/// <returns>The field names of serialized objects.</returns>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	string[] FieldNames
	{
		[SecurityCritical]
		get;
		[SecurityCritical]
		set;
	}

	/// <summary>Gets or sets the field types of the serialized objects.</summary>
	/// <returns>The field types of the serialized objects.</returns>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	Type[] FieldTypes
	{
		[SecurityCritical]
		get;
		[SecurityCritical]
		set;
	}
}
