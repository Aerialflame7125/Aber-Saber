using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.DrawSubItem" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class DrawListViewSubItemEventArgs : EventArgs
{
	private Rectangle bounds;

	private int columnIndex;

	private bool drawDefault;

	private Graphics graphics;

	private ColumnHeader header;

	private ListViewItem item;

	private int itemIndex;

	private ListViewItemStates itemState;

	private ListViewItem.ListViewSubItem subItem;

	/// <summary>Gets the size and location of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle Bounds => bounds;

	/// <summary>Gets the index of the <see cref="T:System.Windows.Forms.ListView" /> column in which the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> is displayed.</summary>
	/// <returns>The index of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> column within the <see cref="P:System.Windows.Forms.ListView.Columns" /> collection.</returns>
	/// <filterpriority>1</filterpriority>
	public int ColumnIndex => columnIndex;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> should be drawn by the operating system instead of owner-drawn.</summary>
	/// <returns>true if the subitem should be drawn by the operating system; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool DrawDefault
	{
		get
		{
			return drawDefault;
		}
		set
		{
			drawDefault = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to draw the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Graphics" /> used to draw the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</returns>
	/// <filterpriority>1</filterpriority>
	public Graphics Graphics => graphics;

	/// <summary>Gets the header of the <see cref="T:System.Windows.Forms.ListView" /> column in which the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> is displayed.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> for the column in which the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> is displayed.</returns>
	/// <filterpriority>1</filterpriority>
	public ColumnHeader Header => header;

	/// <summary>Gets the parent <see cref="T:System.Windows.Forms.ListViewItem" /> of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the parent of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</returns>
	/// <filterpriority>1</filterpriority>
	public ListViewItem Item => item;

	/// <summary>Gets the index of the parent <see cref="T:System.Windows.Forms.ListViewItem" /> of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</summary>
	/// <returns>The index of the parent <see cref="T:System.Windows.Forms.ListViewItem" /> within the <see cref="P:System.Windows.Forms.ListView.Items" /> collection.</returns>
	/// <filterpriority>1</filterpriority>
	public int ItemIndex => itemIndex;

	/// <summary>Gets the current state of the parent <see cref="T:System.Windows.Forms.ListViewItem" /> of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</summary>
	/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the parent <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
	/// <filterpriority>1</filterpriority>
	public ListViewItemStates ItemState => itemState;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</returns>
	/// <filterpriority>1</filterpriority>
	public ListViewItem.ListViewSubItem SubItem => subItem;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawListViewSubItemEventArgs" /> class.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> within which to draw. </param>
	/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> parent of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw. </param>
	/// <param name="subItem">The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</param>
	/// <param name="itemIndex">The index of the parent <see cref="T:System.Windows.Forms.ListViewItem" /> within the <see cref="P:System.Windows.Forms.ListView.Items" /> collection. </param>
	/// <param name="columnIndex">The index of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> column within the <see cref="P:System.Windows.Forms.ListView.Columns" /> collection. </param>
	/// <param name="header">The <see cref="T:System.Windows.Forms.ColumnHeader" /> for the column in which the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> is displayed. </param>
	/// <param name="itemState">A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the <see cref="T:System.Windows.Forms.ListViewItem" /> parent of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw. </param>
	public DrawListViewSubItemEventArgs(Graphics graphics, Rectangle bounds, ListViewItem item, ListViewItem.ListViewSubItem subItem, int itemIndex, int columnIndex, ColumnHeader header, ListViewItemStates itemState)
	{
		this.bounds = bounds;
		this.columnIndex = columnIndex;
		this.graphics = graphics;
		this.header = header;
		this.item = item;
		this.itemIndex = itemIndex;
		this.itemState = itemState;
		this.subItem = subItem;
	}

	/// <summary>Draws the background of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> using its current background color.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void DrawBackground()
	{
		graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(subItem.BackColor), bounds);
	}

	/// <summary>Draws a focus rectangle for the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> if the parent <see cref="T:System.Windows.Forms.ListViewItem" /> has focus.</summary>
	/// <param name="bounds">The area within which to draw the focus rectangle.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void DrawFocusRectangle(Rectangle bounds)
	{
		if ((itemState & ListViewItemStates.Focused) != 0)
		{
			Rectangle rectangle = new Rectangle(bounds.X + 1, bounds.Y + 1, bounds.Width - 1, bounds.Height - 1);
			ThemeEngine.Current.CPDrawFocusRectangle(graphics, rectangle, subItem.ForeColor, subItem.BackColor);
		}
	}

	/// <summary>Draws the text of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> using its current foreground color.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void DrawText()
	{
		DrawText(TextFormatFlags.HorizontalCenter | TextFormatFlags.EndEllipsis);
	}

	/// <summary>Draws the text of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> using its current foreground color and formatting it with the specified <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</summary>
	/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values. </param>
	public void DrawText(TextFormatFlags flags)
	{
		TextRenderer.DrawText(bounds: new Rectangle(bounds.X + 8, bounds.Y, bounds.Width - 13, bounds.Height), dc: graphics, text: subItem.Text, font: subItem.Font, foreColor: subItem.ForeColor, flags: flags);
	}
}
