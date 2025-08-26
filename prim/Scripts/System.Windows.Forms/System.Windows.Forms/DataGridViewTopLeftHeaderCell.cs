using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Represents the cell in the top left corner of the <see cref="T:System.Windows.Forms.DataGridView" /> that sits above the row headers and to the left of the column headers.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewTopLeftHeaderCell : DataGridViewColumnHeaderCell
{
	/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" /> to accessibility client applications.</summary>
	protected class DataGridViewTopLeftHeaderCellAccessibleObject : DataGridViewColumnHeaderCellAccessibleObject
	{
		public override Rectangle Bounds
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a description of the default action of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" />.</summary>
		/// <returns>The string "Press to Select All" if the <see cref="P:System.Windows.Forms.DataGridView.MultiSelect" /> property is true; otherwise, an empty string ("").</returns>
		public override string DefaultAction
		{
			get
			{
				if (base.Owner.DataGridView != null && base.Owner.DataGridView.MultiSelect)
				{
					return "Press to Select All";
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the name of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" />.</summary>
		/// <returns>The name of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" />.</returns>
		public override string Name => base.Name;

		/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" />.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates" /> values. The default is <see cref="F:System.Windows.Forms.AccessibleStates.Selectable" />.</returns>
		public override AccessibleStates State => base.State;

		/// <summary>The value of the containing <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" />.</summary>
		/// <returns>Always returns <see cref="F:System.String.Empty" />.</returns>
		public override string Value => base.Value;

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" /> class. </summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" />.</param>
		public DataGridViewTopLeftHeaderCellAccessibleObject(DataGridViewTopLeftHeaderCell owner)
			: base(owner)
		{
		}

		/// <summary>Performs the default action of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" />.</summary>
		public override void DoDefaultAction()
		{
			if (base.Owner.DataGridView != null)
			{
				base.Owner.DataGridView.SelectAll();
			}
		}

		/// <summary>Navigates to another accessible object.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents an object in the specified direction.</returns>
		/// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</param>
		public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
		{
			throw new NotImplementedException();
		}

		/// <summary>Modifies the selection in the <see cref="T:System.Windows.Forms.DataGridView" /> control or sets input focus to the control. </summary>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.AccessibleSelection" /> values. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property value is null.</exception>
		public override void Select(AccessibleSelection flags)
		{
			base.Select(flags);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" /> class.</summary>
	public DataGridViewTopLeftHeaderCell()
	{
	}

	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return GetType().Name;
	}

	/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" />. </summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" />. </returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return new DataGridViewTopLeftHeaderCellAccessibleObject(this);
	}

	/// <summary>Returns the bounding rectangle that encloses the cell's content area, which is calculated using the specified <see cref="T:System.Drawing.Graphics" /> object and cell style.</summary>
	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
	/// <param name="graphics">The graphics context for the cell.</param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> does not equal -1.</exception>
	protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
	{
		if (base.DataGridView == null)
		{
			return Rectangle.Empty;
		}
		Size size = new Size(36, 13);
		return new Rectangle(2, (base.DataGridView.ColumnHeadersHeight - size.Height) / 2, size.Width, size.Height);
	}

	/// <summary>Returns the bounding rectangle that encloses the cell's error icon, if one is displayed.</summary>
	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
	/// <param name="graphics">The graphics context for the cell.</param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> does not equal -1.</exception>
	protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
	{
		if (base.DataGridView == null || string.IsNullOrEmpty(base.ErrorText))
		{
			return Rectangle.Empty;
		}
		Size size = new Size(12, 11);
		return new Rectangle(new Point(base.Size.Width - size.Width - 5, (base.Size.Height - size.Height) / 2), size);
	}

	/// <summary>Calculates the preferred size, in pixels, of the cell.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
	/// <param name="rowIndex">The zero-based row index of the cell.</param>
	/// <param name="constraintSize">The cell's maximum allowable size.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> does not equal -1.</exception>
	protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
	{
		object value = base.Value;
		if (value != null)
		{
			Size result = DataGridViewCell.MeasureTextSize(graphics, value.ToString(), cellStyle.Font, TextFormatFlags.Left);
			result.Height = Math.Max(result.Height, 17);
			result.Width += 29;
			return result;
		}
		return new Size(39, 17);
	}

	protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
	}

	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the border.</param>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the area of the border that is being painted.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the current cell.</param>
	/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles of the border that is being painted.</param>
	protected override void PaintBorder(Graphics graphics, Rectangle clipBounds, Rectangle bounds, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle)
	{
		base.PaintBorder(graphics, clipBounds, bounds, cellStyle, advancedBorderStyle);
	}
}
