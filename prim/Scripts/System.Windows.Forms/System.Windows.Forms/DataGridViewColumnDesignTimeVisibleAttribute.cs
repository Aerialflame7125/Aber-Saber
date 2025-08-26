namespace System.Windows.Forms;

/// <summary>Specifies whether a column type is visible in the <see cref="T:System.Windows.Forms.DataGridView" /> designer. This class cannot be inherited. </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class DataGridViewColumnDesignTimeVisibleAttribute : Attribute
{
	/// <summary>The default <see cref="T:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute" /> value, which is <see cref="F:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Yes" />, indicating that the column is visible in the <see cref="T:System.Windows.Forms.DataGridView" /> designer. </summary>
	public static readonly DataGridViewColumnDesignTimeVisibleAttribute Default = new DataGridViewColumnDesignTimeVisibleAttribute(visible: true);

	/// <summary>A <see cref="T:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute" /> value indicating that the column is not visible in the <see cref="T:System.Windows.Forms.DataGridView" /> designer. </summary>
	public static readonly DataGridViewColumnDesignTimeVisibleAttribute No = new DataGridViewColumnDesignTimeVisibleAttribute(visible: false);

	/// <summary>A <see cref="T:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute" /> value indicating that the column is visible in the <see cref="T:System.Windows.Forms.DataGridView" /> designer. </summary>
	public static readonly DataGridViewColumnDesignTimeVisibleAttribute Yes = new DataGridViewColumnDesignTimeVisibleAttribute(visible: true);

	private bool visible;

	/// <summary>Gets a value indicating whether the column type is visible in the <see cref="T:System.Windows.Forms.DataGridView" /> designer.</summary>
	/// <returns>true to indicate that the column type is visible in the <see cref="T:System.Windows.Forms.DataGridView" /> designer; otherwise, false.</returns>
	public bool Visible => visible;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute" /> class using the default <see cref="P:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Visible" /> property value of true. </summary>
	public DataGridViewColumnDesignTimeVisibleAttribute()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute" /> class using the specified value to initialize the <see cref="P:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Visible" /> property. </summary>
	/// <param name="visible">The value of the <see cref="P:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Visible" /> property.</param>
	public DataGridViewColumnDesignTimeVisibleAttribute(bool visible)
	{
		this.visible = visible;
	}

	/// <summary>Gets a value indicating whether this object is equivalent to the specified object.</summary>
	/// <returns>true to indicate that the specified object is a <see cref="T:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute" /> instance with the same <see cref="P:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Visible" /> property value as this instance; otherwise, false.</returns>
	/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
	public override bool Equals(object obj)
	{
		if (!(obj is DataGridViewColumnDesignTimeVisibleAttribute))
		{
			return false;
		}
		if ((obj as DataGridViewColumnDesignTimeVisibleAttribute).visible != visible)
		{
			return false;
		}
		return base.Equals(obj);
	}

	/// <returns>A 32-bit signed integer hash code.</returns>
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	/// <summary>Gets a value indicating whether this attribute instance is equal to the <see cref="F:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Default" /> attribute value.</summary>
	/// <returns>true to indicate that this instance is equal to the <see cref="F:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Default" /> instance; otherwise, false.</returns>
	public override bool IsDefaultAttribute()
	{
		return Equals(Default);
	}
}
