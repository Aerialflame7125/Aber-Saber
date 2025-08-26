using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the DrawItem event.</summary>
/// <filterpriority>2</filterpriority>
public class DrawItemEventArgs : EventArgs
{
	private Graphics graphics;

	private Font font;

	private Rectangle rect;

	private int index;

	private DrawItemState state;

	private Color fore_color;

	private Color back_color;

	/// <summary>Gets the graphics surface to draw the item on.</summary>
	/// <returns>The <see cref="T:System.Drawing.Graphics" /> surface to draw the item on.</returns>
	/// <filterpriority>1</filterpriority>
	public Graphics Graphics => graphics;

	/// <summary>Gets the font assigned to the item being drawn.</summary>
	/// <returns>The <see cref="T:System.Drawing.Font" /> assigned to the item being drawn.</returns>
	/// <filterpriority>1</filterpriority>
	public Font Font => font;

	/// <summary>Gets the rectangle that represents the bounds of the item that is being drawn.</summary>
	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the item that is being drawn.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle Bounds => rect;

	/// <summary>Gets the index value of the item that is being drawn.</summary>
	/// <returns>The numeric value that represents the <see cref="P:System.Windows.Forms.Control.ControlCollection.Item(System.Int32)" /> value of the item being drawn.</returns>
	/// <filterpriority>1</filterpriority>
	public int Index => index;

	/// <summary>Gets the state of the item being drawn.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DrawItemState" /> that represents the state of the item being drawn.</returns>
	/// <filterpriority>1</filterpriority>
	public DrawItemState State => state;

	/// <summary>Gets the background color of the item that is being drawn.</summary>
	/// <returns>The background <see cref="T:System.Drawing.Color" /> of the item that is being drawn.</returns>
	/// <filterpriority>1</filterpriority>
	public Color BackColor => back_color;

	/// <summary>Gets the foreground color of the of the item being drawn.</summary>
	/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the item being drawn.</returns>
	/// <filterpriority>1</filterpriority>
	public Color ForeColor => fore_color;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> class for the specified control with the specified font, state, surface to draw on, and the bounds to draw within.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to use, usually the parent control's <see cref="T:System.Drawing.Font" /> property. </param>
	/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> bounds to draw within. </param>
	/// <param name="index">The <see cref="T:System.Windows.Forms.Control.ControlCollection" /> index value of the item that is being drawn. </param>
	/// <param name="state">The control's <see cref="T:System.Windows.Forms.DrawItemState" /> information. </param>
	public DrawItemEventArgs(Graphics graphics, Font font, Rectangle rect, int index, DrawItemState state)
		: this(graphics, font, rect, index, state, Control.DefaultForeColor, Control.DefaultBackColor)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> class for the specified control with the specified font, state, foreground color, background color, surface to draw on, and the bounds to draw within.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to use, usually the parent control's <see cref="T:System.Drawing.Font" /> property. </param>
	/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> bounds to draw within. </param>
	/// <param name="index">The <see cref="T:System.Windows.Forms.Control.ControlCollection" /> index value of the item that is being drawn. </param>
	/// <param name="state">The control's <see cref="T:System.Windows.Forms.DrawItemState" /> information. </param>
	/// <param name="foreColor">The foreground <see cref="T:System.Drawing.Color" /> to draw the control with. </param>
	/// <param name="backColor">The background <see cref="T:System.Drawing.Color" /> to draw the control with. </param>
	public DrawItemEventArgs(Graphics graphics, Font font, Rectangle rect, int index, DrawItemState state, Color foreColor, Color backColor)
	{
		this.graphics = graphics;
		this.font = font;
		this.rect = rect;
		this.index = index;
		this.state = state;
		fore_color = foreColor;
		back_color = backColor;
	}

	/// <summary>Draws the background within the bounds specified in the <see cref="Overload:System.Windows.Forms.DrawItemEventArgs.#ctor" /> constructor and with the appropriate color.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void DrawBackground()
	{
		ThemeEngine.Current.DrawOwnerDrawBackground(this);
	}

	/// <summary>Draws a focus rectangle within the bounds specified in the <see cref="Overload:System.Windows.Forms.DrawItemEventArgs.#ctor" /> constructor.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void DrawFocusRectangle()
	{
		ThemeEngine.Current.DrawOwnerDrawFocusRectangle(this);
	}
}
