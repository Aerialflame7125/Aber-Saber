namespace System.ComponentModel;

/// <summary>Specifies that an object has no subproperties capable of being edited. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.All)]
public sealed class ImmutableObjectAttribute : Attribute
{
	/// <summary>Specifies that an object has no subproperties that can be edited. This <see langword="static" /> field is read-only.</summary>
	public static readonly ImmutableObjectAttribute Yes = new ImmutableObjectAttribute(immutable: true);

	/// <summary>Specifies that an object has at least one editable subproperty. This <see langword="static" /> field is read-only.</summary>
	public static readonly ImmutableObjectAttribute No = new ImmutableObjectAttribute(immutable: false);

	/// <summary>Represents the default value for <see cref="T:System.ComponentModel.ImmutableObjectAttribute" />.</summary>
	public static readonly ImmutableObjectAttribute Default = No;

	private bool immutable = true;

	/// <summary>Gets whether the object is immutable.</summary>
	/// <returns>
	///   <see langword="true" /> if the object is immutable; otherwise, <see langword="false" />.</returns>
	public bool Immutable => immutable;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ImmutableObjectAttribute" /> class.</summary>
	/// <param name="immutable">
	///   <see langword="true" /> if the object is immutable; otherwise, <see langword="false" />.</param>
	public ImmutableObjectAttribute(bool immutable)
	{
		this.immutable = immutable;
	}

	/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
	/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or a null reference (<see langword="Nothing" /> in Visual Basic).</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj == this)
		{
			return true;
		}
		if (obj is ImmutableObjectAttribute immutableObjectAttribute)
		{
			return immutableObjectAttribute.Immutable == immutable;
		}
		return false;
	}

	/// <summary>Returns the hash code for this instance.</summary>
	/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ImmutableObjectAttribute" />.</returns>
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	/// <summary>Indicates whether the value of this instance is the default value.</summary>
	/// <returns>
	///   <see langword="true" /> if this instance is the default attribute for the class; otherwise, <see langword="false" />.</returns>
	public override bool IsDefaultAttribute()
	{
		return Equals(Default);
	}
}
