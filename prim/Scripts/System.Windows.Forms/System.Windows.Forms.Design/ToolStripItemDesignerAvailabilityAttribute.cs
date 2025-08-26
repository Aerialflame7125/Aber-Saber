namespace System.Windows.Forms.Design;

/// <summary>Specifies which types a <see cref="T:System.Windows.Forms.ToolStripItem" /> can appear in. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class ToolStripItemDesignerAvailabilityAttribute : Attribute
{
	private ToolStripItemDesignerAvailability visibility;

	/// <summary>Specifies the default value of the <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailabilityAttribute" />. This field is read-only.</summary>
	public static readonly ToolStripItemDesignerAvailabilityAttribute Default = new ToolStripItemDesignerAvailabilityAttribute();

	/// <summary>Gets the visibility of a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailability" /> representing the visibility.</returns>
	public ToolStripItemDesignerAvailability ItemAdditionVisibility => visibility;

	/// <summary>Initializes a new default instance of the <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailabilityAttribute" /> class. </summary>
	public ToolStripItemDesignerAvailabilityAttribute()
	{
		visibility = ToolStripItemDesignerAvailability.None;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailabilityAttribute" /> class with the specified visibility. </summary>
	/// <param name="visibility">A <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailability" /> value specifying the visibility.</param>
	public ToolStripItemDesignerAvailabilityAttribute(ToolStripItemDesignerAvailability visibility)
	{
		this.visibility = visibility;
	}

	/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
	/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
	public override bool Equals(object obj)
	{
		if (!(obj is ToolStripItemDesignerAvailabilityAttribute))
		{
			return false;
		}
		return ItemAdditionVisibility == (obj as ToolStripItemDesignerAvailabilityAttribute).ItemAdditionVisibility;
	}

	/// <returns>A 32-bit signed integer hash code.</returns>
	public override int GetHashCode()
	{
		return (int)visibility;
	}

	/// <summary>When overriden in a derived class, indicates whether the value of this instance is the default value for the derived class.</summary>
	/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
	public override bool IsDefaultAttribute()
	{
		return visibility == ToolStripItemDesignerAvailability.None;
	}
}
