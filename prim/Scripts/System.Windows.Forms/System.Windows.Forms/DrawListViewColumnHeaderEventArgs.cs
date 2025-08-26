using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.DrawColumnHeader" /> event. </summary>
/// <filterpriority>2</filterpriority>
public class DrawListViewColumnHeaderEventArgs : EventArgs
{
	private Color backColor;

	private Rectangle bounds;

	private int columnIndex;

	private bool drawDefault;

	private Font font;

	private Color foreColor;

	private Graphics graphics;

	private ColumnHeader header;

	private ListViewItemStates state;

	/// <summary>Gets the background color of the header.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing the background color of the header.</returns>
	/// <filterpriority>1</filterpriority>
	public Color BackColor => backColor;

	/// <summary>Gets the size and location of the column header to draw.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the column header.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle Bounds => bounds;

	/// <summary>Gets the index of the <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the header to draw.</summary>
	/// <returns>The index of the column header within the <see cref="P:System.Windows.Forms.ListView.Columns" /> collection.</returns>
	/// <filterpriority>1</filterpriority>
	public int ColumnIndex => columnIndex;

	/// <summary>Gets or sets a value indicating whether the column header should be drawn by the operating system instead of owner-drawn.</summary>
	/// <returns>true if the header should be drawn by the operating system; otherwise, false. The default is false.</returns>
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

	/// <summary>Gets the font used to draw the column header text.</summary>
	/// <returns>A <see cref="T:System.Drawing.Font" /> representing the font of the header text.</returns>
	/// <filterpriority>1</filterpriority>
	public Font Font => font;

	/// <summary>Gets the foreground color of the header.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing the foreground color of the header.</returns>
	/// <filterpriority>1</filterpriority>
	public Color ForeColor => foreColor;

	/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to draw the column header.</summary>
	/// <returns>A <see cref="T:System.Drawing.Graphics" /> used to draw the column header.</returns>
	/// <filterpriority>1</filterpriority>
	public Graphics Graphics => graphics;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the column header to draw.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the column header.</returns>
	/// <filterpriority>1</filterpriority>
	public ColumnHeader Header => header;

	/// <summary>Gets the current state of the column header.</summary>
	/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the column header.</returns>
	/// <filterpriority>1</filterpriority>
	public ListViewItemStates State => state;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawListViewColumnHeaderEventArgs" /> class. </summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> within which to draw.</param>
	/// <param name="columnIndex">The index of the header's column within the <see cref="P:System.Windows.Forms.ListView.Columns" /> collection.</param>
	/// <param name="header">The <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the header to draw.</param>
	/// <param name="state">A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the column header.</param>
	/// <param name="foreColor">The foreground <see cref="T:System.Drawing.Color" /> of the header.</param>
	/// <param name="backColor">The background <see cref="T:System.Drawing.Color" /> of the header.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> used for the header text.</param>
	public DrawListViewColumnHeaderEventArgs(Graphics graphics, Rectangle bounds, int columnIndex, ColumnHeader header, ListViewItemStates state, Color foreColor, Color backColor, Font font)
	{
		this.backColor = backColor;
		this.bounds = bounds;
		this.columnIndex = columnIndex;
		this.font = font;
		this.foreColor = foreColor;
		this.graphics = graphics;
		this.header = header;
		this.state = state;
	}

	/// <summary>Draws the background of the column header.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void DrawBackground()
	{
		ThemeEngine.Current.CPDrawButton(graphics, bounds, ButtonState.Normal);
	}

	/// <summary>Draws the column header text using the default formatting.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void DrawText()
	{
		DrawText(TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
	}

	/// <summary>Draws the column header text, formatting it with the specified <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</summary>
	/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values. </param>
	public void DrawText(TextFormatFlags flags)
	{
		TextRenderer.DrawText(bounds: new Rectangle(bounds.X + 8, bounds.Y, bounds.Width - 13, bounds.Height), dc: graphics, text: header.Text, font: font, foreColor: foreColor, flags: flags);
	}
}
