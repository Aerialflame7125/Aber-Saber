namespace System.ComponentModel.DataAnnotations.Schema;

/// <summary>Denotes that the class is a complex type. Complex types are non-scalar properties of entity types that enable scalar properties to be organized within entities. Complex types do not have keys and cannot be managed by the Entity Framework apart from the parent object.</summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ComplexTypeAttribute : Attribute
{
	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.Schema.ComplexTypeAttribute" /> class.</summary>
	public ComplexTypeAttribute()
	{
	}
}
