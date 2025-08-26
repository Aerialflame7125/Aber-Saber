namespace System.Windows.Forms;

/// <summary>Specifies the default docking behavior for a control.</summary>
/// <filterpriority>1</filterpriority>
[AttributeUsage(AttributeTargets.Class)]
public sealed class DockingAttribute : Attribute
{
	private DockingBehavior dockingBehavior;

	/// <summary>The default <see cref="T:System.Windows.Forms.DockingAttribute" /> for this control.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly DockingAttribute Default = new DockingAttribute();

	/// <summary>Gets the docking behavior supplied to this attribute.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DockingBehavior" /> value.</returns>
	/// <filterpriority>1</filterpriority>
	public DockingBehavior DockingBehavior => dockingBehavior;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DockingAttribute" /> class. </summary>
	public DockingAttribute()
	{
		dockingBehavior = DockingBehavior.Never;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DockingAttribute" /> class with the given docking behavior. </summary>
	/// <param name="dockingBehavior">A <see cref="T:System.Windows.Forms.DockingBehavior" /> value specifying the default behavior.</param>
	public DockingAttribute(DockingBehavior dockingBehavior)
	{
		this.dockingBehavior = dockingBehavior;
	}

	/// <summary>Compares an arbitrary object with the <see cref="T:System.Windows.Forms.DockingAttribute" /> object for equality.</summary>
	/// <returns>true is <paramref name="obj" /> is equal to this <see cref="T:System.Windows.Forms.DockingAttribute" />; otherwise, false.</returns>
	/// <param name="obj">The <see cref="T:System.Object" /> against which to compare this <see cref="T:System.Windows.Forms.DockingAttribute" />.</param>
	/// <filterpriority>1</filterpriority>
	public override bool Equals(object obj)
	{
		if (obj is DockingAttribute)
		{
			return dockingBehavior == ((DockingAttribute)obj).DockingBehavior;
		}
		return false;
	}

	/// <summary>The hash code for this object.</summary>
	/// <returns>An <see cref="T:System.Int32" /> representing an in-memory hash of this object.</returns>
	/// <filterpriority>1</filterpriority>
	public override int GetHashCode()
	{
		return dockingBehavior.GetHashCode();
	}

	/// <summary>Specifies whether this <see cref="T:System.Windows.Forms.DockingAttribute" /> is the default docking attribute.</summary>
	/// <returns>true is the current <see cref="T:System.Windows.Forms.DockingAttribute" /> is the default; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public override bool IsDefaultAttribute()
	{
		return Default.Equals(this);
	}
}
