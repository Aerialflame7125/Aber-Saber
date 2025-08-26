using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TableLayoutPanel.CellPaint" /> event.</summary>
public class TableLayoutCellPaintEventArgs : PaintEventArgs
{
	private Rectangle cell_bounds;

	private int column;

	private int row;

	/// <summary>Gets the size and location of the cell.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the size and location of the cell.</returns>
	public Rectangle CellBounds => cell_bounds;

	/// <summary>Gets the column of the cell.</summary>
	/// <returns>The column position of the cell.</returns>
	public int Column => column;

	/// <summary>Gets the row of the cell.</summary>
	/// <returns>The row position of the cell.</returns>
	public int Row => row;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TableLayoutCellPaintEventArgs" /> class.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to paint the item.</param>
	/// <param name="clipRectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle in which to paint.</param>
	/// <param name="cellBounds">The bounds of the cell.</param>
	/// <param name="column">The column of the cell.</param>
	/// <param name="row">The row of the cell.</param>
	public TableLayoutCellPaintEventArgs(Graphics g, Rectangle clipRectangle, Rectangle cellBounds, int column, int row)
		: base(g, clipRectangle)
	{
		cell_bounds = cellBounds;
		this.column = column;
		this.row = row;
	}
}
