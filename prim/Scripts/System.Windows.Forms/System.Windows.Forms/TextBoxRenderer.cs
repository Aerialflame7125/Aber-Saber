using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms;

/// <summary>Provides methods used to render a text box control with visual styles. This class cannot be inherited.</summary>
/// <filterpriority>2</filterpriority>
public sealed class TextBoxRenderer
{
	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.TextBoxRenderer" /> class can be used to draw a text box with visual styles.</summary>
	/// <returns>true if the user has enabled visual styles in the operating system and visual styles are applied to the client area of application windows; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool IsSupported => VisualStyleInformation.IsEnabledByUser && (Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled || Application.VisualStyleState == VisualStyleState.ClientAreaEnabled);

	private TextBoxRenderer()
	{
	}

	/// <summary>Draws a text box control in the specified state and bounds.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TextBoxState" /> values that specifies the visual state of the text box.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawTextBox(Graphics g, Rectangle bounds, TextBoxState state)
	{
		DrawTextBox(g, bounds, string.Empty, null, Rectangle.Empty, TextFormatFlags.Left, state);
	}

	/// <summary>Draws a text box control in the specified state and bounds, and with the specified text.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
	/// <param name="textBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="textBoxText" />.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TextBoxState" /> values that specifies the visual state of the text box.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawTextBox(Graphics g, Rectangle bounds, string textBoxText, Font font, TextBoxState state)
	{
		DrawTextBox(g, bounds, textBoxText, font, Rectangle.Empty, TextFormatFlags.Left, state);
	}

	/// <summary>Draws a text box control in the specified state and bounds, and with the specified text and text bounds.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
	/// <param name="textBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="textBoxText" />.</param>
	/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of <paramref name="textBoxText" />.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TextBoxState" /> values that specifies the visual state of the text box.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawTextBox(Graphics g, Rectangle bounds, string textBoxText, Font font, Rectangle textBounds, TextBoxState state)
	{
		DrawTextBox(g, bounds, textBoxText, font, textBounds, TextFormatFlags.Left, state);
	}

	/// <summary>Draws a text box control in the specified state and bounds, and with the specified text and text formatting.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
	/// <param name="textBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="textBoxText" />.</param>
	/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TextBoxState" /> values that specifies the visual state of the text box.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	public static void DrawTextBox(Graphics g, Rectangle bounds, string textBoxText, Font font, TextFormatFlags flags, TextBoxState state)
	{
		DrawTextBox(g, bounds, textBoxText, font, Rectangle.Empty, flags, state);
	}

	/// <summary>Draws a text box control in the specified state and bounds, and with the specified text, text bounds, and text formatting.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
	/// <param name="textBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="textBoxText" />.</param>
	/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of <paramref name="textBoxText" />.</param>
	/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TextBoxState" /> values that specifies the visual state of the text box.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	public static void DrawTextBox(Graphics g, Rectangle bounds, string textBoxText, Font font, Rectangle textBounds, TextFormatFlags flags, TextBoxState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		(state switch
		{
			TextBoxState.Assist => new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Assist), 
			TextBoxState.Disabled => new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Disabled), 
			TextBoxState.Hot => new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Hot), 
			TextBoxState.Selected => new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Selected), 
			_ => new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Normal), 
		}).DrawBackground(g, bounds);
		if (textBounds == Rectangle.Empty)
		{
			textBounds = new Rectangle(bounds.Left + 3, bounds.Top + 3, bounds.Width - 6, bounds.Height - 6);
		}
		if (textBoxText != string.Empty)
		{
			if (state == TextBoxState.Disabled)
			{
				TextRenderer.DrawText(g, textBoxText, font, textBounds, SystemColors.GrayText, flags);
			}
			else
			{
				TextRenderer.DrawText(g, textBoxText, font, textBounds, SystemColors.ControlText, flags);
			}
		}
	}
}
