using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.DrawItem" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class DrawListViewItemEventArgs : EventArgs
{
	private Rectangle bounds;

	private bool drawDefault;

	private Graphics graphics;

	private ListViewItem item;

	private int itemIndex;

	private ListViewItemStates state;

	/// <summary>Gets or sets a property indicating whether the <see cref="T:System.Windows.Forms.ListView" /> control will use the default drawing for the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
	/// <returns>true if the system draws the item; false if the event handler draws the item. The default value is false.</returns>
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

	/// <summary>Gets the size and location of the <see cref="T:System.Windows.Forms.ListViewItem" /> to draw.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.ListViewItem" /> to draw.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle Bounds => bounds;

	/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to draw the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Graphics" /> used to draw the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
	/// <filterpriority>1</filterpriority>
	public Graphics Graphics => graphics;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ListViewItem" /> to draw.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> to draw.</returns>
	/// <filterpriority>1</filterpriority>
	public ListViewItem Item => item;

	/// <summary>Gets the index of the <see cref="T:System.Windows.Forms.ListViewItem" /> within the <see cref="P:System.Windows.Forms.ListView.Items" /> collection of the containing <see cref="T:System.Windows.Forms.ListView" />.</summary>
	/// <returns>The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> within the <see cref="P:System.Windows.Forms.ListView.Items" /> collection.</returns>
	/// <filterpriority>1</filterpriority>
	public int ItemIndex => itemIndex;

	/// <summary>Gets the current state of the <see cref="T:System.Windows.Forms.ListViewItem" /> to draw.</summary>
	/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
	/// <filterpriority>1</filterpriority>
	public ListViewItemStates State => state;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawListViewItemEventArgs" /> class.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
	/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> to draw. </param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> within which to draw. </param>
	/// <param name="itemIndex">The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> within the <see cref="P:System.Windows.Forms.ListView.Items" /> collection. </param>
	/// <param name="state">A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the <see cref="T:System.Windows.Forms.ListViewItem" /> to draw. </param>
	public DrawListViewItemEventArgs(Graphics graphics, ListViewItem item, Rectangle bounds, int itemIndex, ListViewItemStates state)
	{
		this.graphics = graphics;
		this.item = item;
		this.bounds = bounds;
		this.itemIndex = itemIndex;
		this.state = state;
	}

	/// <summary>Draws the background of the <see cref="T:System.Windows.Forms.ListViewItem" /> using its current background color.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void DrawBackground()
	{
		graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(item.BackColor), bounds);
	}

	/// <summary>Draws a focus rectangle for the <see cref="T:System.Windows.Forms.ListViewItem" /> if it has focus.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void DrawFocusRectangle()
	{
		if ((state & ListViewItemStates.Focused) != 0)
		{
			ThemeEngine.Current.CPDrawFocusRectangle(graphics, bounds, item.ListView.ForeColor, item.ListView.BackColor);
		}
	}

	/// <summary>Draws the text of the <see cref="T:System.Windows.Forms.ListViewItem" /> using its current foreground color.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void DrawText()
	{
		DrawText(TextFormatFlags.Left);
	}

	/// <summary>Draws the text of the <see cref="T:System.Windows.Forms.ListViewItem" /> using its current foreground color and formatting it with the specified <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</summary>
	/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values. </param>
	public void DrawText(TextFormatFlags flags)
	{
		TextRenderer.DrawText(graphics, item.Text, item.Font, bounds, item.ForeColor, flags);
	}
}
