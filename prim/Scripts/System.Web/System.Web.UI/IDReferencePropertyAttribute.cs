namespace System.Web.UI;

/// <summary>Defines an attribute applied to properties that contain ID references. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class IDReferencePropertyAttribute : Attribute
{
	private Type _referencedControlType;

	/// <summary>Gets the type of the controls allowed by the property to which the <see cref="T:System.Web.UI.IDReferencePropertyAttribute" /> attribute is applied.</summary>
	/// <returns>A <see cref="T:System.Type" /> that represents the type of control allowed by the property to which the <see cref="T:System.Web.UI.IDReferencePropertyAttribute" /> is applied. The default is <see cref="T:System.Web.UI.Control" />.</returns>
	public Type ReferencedControlType => _referencedControlType;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.IDReferencePropertyAttribute" /> class.</summary>
	public IDReferencePropertyAttribute()
		: this(typeof(Control))
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.IDReferencePropertyAttribute" /> class using the specified type.</summary>
	/// <param name="referencedControlType">A <see cref="T:System.Type" /> that specifies the type of the control represented by the property to which the <see cref="T:System.Web.UI.IDReferencePropertyAttribute" /> is applied.</param>
	public IDReferencePropertyAttribute(Type referencedControlType)
	{
		_referencedControlType = referencedControlType;
	}

	/// <summary>Returns the hash code for this instance.</summary>
	/// <returns>A 32-bit signed integer hash code.</returns>
	public override int GetHashCode()
	{
		if (!(ReferencedControlType != null))
		{
			return 0;
		}
		return ReferencedControlType.GetHashCode();
	}

	/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
	/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or a null reference (<see langword="Nothing" /> in Visual Basic).</param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj == this)
		{
			return true;
		}
		if (obj is IDReferencePropertyAttribute iDReferencePropertyAttribute)
		{
			return ReferencedControlType == iDReferencePropertyAttribute.ReferencedControlType;
		}
		return false;
	}
}
