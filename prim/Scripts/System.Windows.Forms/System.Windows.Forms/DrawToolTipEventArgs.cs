using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolTip.Draw" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class DrawToolTipEventArgs : EventArgs
{
	private Control associated_control;

	private IWin32Window associated_window;

	private Color back_color;

	private Font font;

	private Rectangle bounds;

	private Color fore_color;

	private Graphics graphics;

	private string tooltip_text;

	/// <summary>Gets the control for which the <see cref="T:System.Windows.Forms.ToolTip" /> is being drawn.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that is associated with the <see cref="T:System.Windows.Forms.ToolTip" /> when the <see cref="E:System.Windows.Forms.ToolTip.Draw" /> event occurs. The return value will be null if the ToolTip is not associated with a control.</returns>
	/// <filterpriority>1</filterpriority>
	public Control AssociatedControl => associated_control;

	/// <summary>Gets the window to which this <see cref="T:System.Windows.Forms.ToolTip" /> is bound.</summary>
	/// <returns>The window which owns the <see cref="T:System.Windows.Forms.ToolTip" />.</returns>
	/// <filterpriority>1</filterpriority>
	public IWin32Window AssociatedWindow => associated_window;

	/// <summary>Gets the size and location of the <see cref="T:System.Windows.Forms.ToolTip" /> to draw.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.ToolTip" /> to draw.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle Bounds => bounds;

	/// <summary>Gets the font used to draw the <see cref="T:System.Windows.Forms.ToolTip" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Font" /> object.</returns>
	/// <filterpriority>1</filterpriority>
	public Font Font => font;

	/// <summary>Gets the graphics surface used to draw the <see cref="T:System.Windows.Forms.ToolTip" />.</summary>
	/// <returns>The <see cref="T:System.Drawing.Graphics" /> on which to draw the <see cref="T:System.Windows.Forms.ToolTip" />.</returns>
	/// <filterpriority>1</filterpriority>
	public Graphics Graphics => graphics;

	/// <summary>Gets the text for the <see cref="T:System.Windows.Forms.ToolTip" /> that is being drawn.</summary>
	/// <returns>The text that is associated with the <see cref="T:System.Windows.Forms.ToolTip" /> when the <see cref="E:System.Windows.Forms.ToolTip.Draw" /> event occurs.</returns>
	public string ToolTipText => tooltip_text;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawToolTipEventArgs" /> class.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> context used to draw the ToolTip. </param>
	/// <param name="associatedWindow">The <see cref="T:System.Windows.Forms.IWin32Window" /> that the ToolTip is bound to.</param>
	/// <param name="associatedControl">The <see cref="T:System.Windows.Forms.Control" /> that the ToolTip is being created for.</param>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that outlines the area where the ToolTip is to be displayed.</param>
	/// <param name="toolTipText">A <see cref="T:System.String" /> containing the text for the ToolTip.</param>
	/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the ToolTip background.</param>
	/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> of the ToolTip text. </param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> used to draw the ToolTip text.</param>
	public DrawToolTipEventArgs(Graphics graphics, IWin32Window associatedWindow, Control associatedControl, Rectangle bounds, string toolTipText, Color backColor, Color foreColor, Font font)
	{
		this.graphics = graphics;
		associated_window = associatedWindow;
		associated_control = associatedControl;
		this.bounds = bounds;
		tooltip_text = toolTipText;
		back_color = backColor;
		fore_color = foreColor;
		this.font = font;
	}

	/// <summary>Draws the background of the <see cref="T:System.Windows.Forms.ToolTip" /> using the system background color.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void DrawBackground()
	{
		graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(back_color), bounds);
	}

	/// <summary>Draws the border of the <see cref="T:System.Windows.Forms.ToolTip" /> using the system border color.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void DrawBorder()
	{
		ControlPaint.DrawBorder(graphics, bounds, SystemColors.WindowFrame, ButtonBorderStyle.Solid);
	}

	/// <summary>Draws the text of the <see cref="T:System.Windows.Forms.ToolTip" /> using the system text color and font.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void DrawText()
	{
		DrawText(TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.HidePrefix);
	}

	/// <summary>Draws the text of the <see cref="T:System.Windows.Forms.ToolTip" /> using the system text color and font, and the specified text layout.</summary>
	/// <param name="flags">A <see cref="T:System.Windows.Forms.TextFormatFlags" /> containing a bitwise combination of values that specifies the display and layout for the <see cref="P:System.Windows.Forms.DrawToolTipEventArgs.ToolTipText" />.</param>
	public void DrawText(TextFormatFlags flags)
	{
		TextRenderer.DrawTextInternal(graphics, tooltip_text, font, bounds, fore_color, flags, useDrawString: false);
	}
}
