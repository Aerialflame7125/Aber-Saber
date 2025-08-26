using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Contains border styles for the cells in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
/// <filterpriority>2</filterpriority>
public sealed class DataGridViewAdvancedBorderStyle : ICloneable
{
	private DataGridViewAdvancedCellBorderStyle bottom;

	private DataGridViewAdvancedCellBorderStyle left;

	private DataGridViewAdvancedCellBorderStyle right;

	private DataGridViewAdvancedCellBorderStyle top;

	/// <summary>Gets or sets the border style for all of the borders of a cell.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</exception>
	/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet" />.-or-The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.OutsetDouble" />, <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.OutsetPartial" />, or <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.InsetDouble" /> and this <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> instance was retrieved through the <see cref="P:System.Windows.Forms.DataGridView.AdvancedCellBorderStyle" /> property.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public DataGridViewAdvancedCellBorderStyle All
	{
		get
		{
			if (bottom == left && left == right && right == top)
			{
				return bottom;
			}
			return DataGridViewAdvancedCellBorderStyle.NotSet;
		}
		set
		{
			if (!Enum.IsDefined(typeof(DataGridViewAdvancedCellBorderStyle), value))
			{
				throw new InvalidEnumArgumentException("Value is not valid DataGridViewAdvancedCellBorderStyle.");
			}
			bottom = (left = (right = (top = value)));
		}
	}

	/// <summary>Gets or sets the style for the bottom border of a cell.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</exception>
	/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet" />.</exception>
	/// <filterpriority>1</filterpriority>
	public DataGridViewAdvancedCellBorderStyle Bottom
	{
		get
		{
			return bottom;
		}
		set
		{
			if (!Enum.IsDefined(typeof(DataGridViewAdvancedCellBorderStyle), value))
			{
				throw new InvalidEnumArgumentException("Value is not valid DataGridViewAdvancedCellBorderStyle.");
			}
			if (value == DataGridViewAdvancedCellBorderStyle.NotSet)
			{
				throw new ArgumentException("Invlid Bottom value.");
			}
			bottom = value;
		}
	}

	/// <summary>Gets the style for the left border of a cell.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" />.</exception>
	/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet" />.-or-The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.InsetDouble" /> or <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.OutsetDouble" /> and this <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> instance has an associated <see cref="T:System.Windows.Forms.DataGridView" /> control with a <see cref="P:System.Windows.Forms.Control.RightToLeft" /> property value of true.</exception>
	/// <filterpriority>1</filterpriority>
	public DataGridViewAdvancedCellBorderStyle Left
	{
		get
		{
			return left;
		}
		set
		{
			if (!Enum.IsDefined(typeof(DataGridViewAdvancedCellBorderStyle), value))
			{
				throw new InvalidEnumArgumentException("Value is not valid DataGridViewAdvancedCellBorderStyle.");
			}
			if (value == DataGridViewAdvancedCellBorderStyle.NotSet)
			{
				throw new ArgumentException("Invlid Left value.");
			}
			left = value;
		}
	}

	/// <summary>Gets the style for the right border of a cell.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" />.</exception>
	/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet" />.-or-The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.InsetDouble" /> or <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.OutsetDouble" /> and this <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> instance has an associated <see cref="T:System.Windows.Forms.DataGridView" /> control with a <see cref="P:System.Windows.Forms.Control.RightToLeft" /> property value of false.</exception>
	/// <filterpriority>1</filterpriority>
	public DataGridViewAdvancedCellBorderStyle Right
	{
		get
		{
			return right;
		}
		set
		{
			if (!Enum.IsDefined(typeof(DataGridViewAdvancedCellBorderStyle), value))
			{
				throw new InvalidEnumArgumentException("Value is not valid DataGridViewAdvancedCellBorderStyle.");
			}
			if (value == DataGridViewAdvancedCellBorderStyle.NotSet)
			{
				throw new ArgumentException("Invlid Right value.");
			}
			right = value;
		}
	}

	/// <summary>Gets the style for the top border of a cell.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" /> values.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle" />.</exception>
	/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet" />.</exception>
	/// <filterpriority>1</filterpriority>
	public DataGridViewAdvancedCellBorderStyle Top
	{
		get
		{
			return top;
		}
		set
		{
			if (!Enum.IsDefined(typeof(DataGridViewAdvancedCellBorderStyle), value))
			{
				throw new InvalidEnumArgumentException("Value is not valid DataGridViewAdvancedCellBorderStyle.");
			}
			if (value == DataGridViewAdvancedCellBorderStyle.NotSet)
			{
				throw new ArgumentException("Invlid Top value.");
			}
			top = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> class. </summary>
	public DataGridViewAdvancedBorderStyle()
	{
		All = DataGridViewAdvancedCellBorderStyle.None;
	}

	/// <summary>Creates a new object that is a copy of the current instance.</summary>
	/// <returns>A copy of the current instance.</returns>
	object ICloneable.Clone()
	{
		DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle = new DataGridViewAdvancedBorderStyle();
		dataGridViewAdvancedBorderStyle.bottom = bottom;
		dataGridViewAdvancedBorderStyle.left = left;
		dataGridViewAdvancedBorderStyle.right = right;
		dataGridViewAdvancedBorderStyle.top = top;
		return dataGridViewAdvancedBorderStyle;
	}

	/// <summary>Determines whether the specified object is equal to the current <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" />.</summary>
	/// <returns>true if <paramref name="other" /> is a <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> and the values for the <see cref="P:System.Windows.Forms.DataGridViewAdvancedBorderStyle.Top" />, <see cref="P:System.Windows.Forms.DataGridViewAdvancedBorderStyle.Bottom" />, <see cref="P:System.Windows.Forms.DataGridViewAdvancedBorderStyle.Left" />, and <see cref="P:System.Windows.Forms.DataGridViewAdvancedBorderStyle.Right" /> properties are equal to their counterpart in the current <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" />; otherwise, false.</returns>
	/// <param name="other">An <see cref="T:System.Object" /> to be compared.</param>
	/// <filterpriority>1</filterpriority>
	public override bool Equals(object other)
	{
		if (other is DataGridViewAdvancedBorderStyle)
		{
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle = (DataGridViewAdvancedBorderStyle)other;
			return bottom == dataGridViewAdvancedBorderStyle.bottom && left == dataGridViewAdvancedBorderStyle.left && right == dataGridViewAdvancedBorderStyle.right && top == dataGridViewAdvancedBorderStyle.top;
		}
		return false;
	}

	/// <filterpriority>1</filterpriority>
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" />.</summary>
	/// <returns>A string that represents the <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return string.Format("DataGridViewAdvancedBorderStyle { All={0}, Left={1}, Right={2}, Top={3}, Bottom={4} }", All, Left, Right, Top, Bottom);
	}
}
