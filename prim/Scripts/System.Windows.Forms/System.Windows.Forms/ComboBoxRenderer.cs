using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms;

/// <summary>Provides methods used to render a combo box control with visual styles. This class cannot be inherited.</summary>
/// <filterpriority>2</filterpriority>
public sealed class ComboBoxRenderer
{
	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ComboBoxRenderer" /> class can be used to draw a combo box with visual styles.</summary>
	/// <returns>true if the user has enabled visual styles in the operating system and visual styles are applied to the client area of application windows; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool IsSupported => VisualStyleInformation.IsEnabledByUser && (Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled || Application.VisualStyleState == VisualStyleState.ClientAreaEnabled);

	private ComboBoxRenderer()
	{
	}

	/// <summary>Draws a drop-down arrow with the current visual style of the operating system.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the drop-down arrow.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the drop-down arrow.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ComboBoxState" /> values that specifies the visual state of the drop-down arrow.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawDropDownButton(Graphics g, Rectangle bounds, ComboBoxState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		GetComboRenderer(state).DrawBackground(g, bounds);
	}

	/// <summary>Draws a text box in the specified state and bounds, with the specified text, text formatting, and text bounds.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
	/// <param name="comboBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="comboBoxText" />.</param>
	/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds in which to draw <paramref name="comboBoxText" />.</param>
	/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ComboBoxState" /> values that specifies the visual state of the text box.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, Rectangle textBounds, TextFormatFlags flags, ComboBoxState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		GetTextBoxRenderer(state).DrawBackground(g, bounds);
		if (textBounds == Rectangle.Empty)
		{
			textBounds = new Rectangle(bounds.Left + 3, bounds.Top, bounds.Width - 4, bounds.Height);
		}
		if (comboBoxText != string.Empty)
		{
			if (state == ComboBoxState.Disabled)
			{
				TextRenderer.DrawText(g, comboBoxText, font, textBounds, SystemColors.GrayText, flags);
			}
			else
			{
				TextRenderer.DrawText(g, comboBoxText, font, textBounds, SystemColors.ControlText, flags);
			}
		}
	}

	/// <summary>Draws a text box in the specified state and bounds.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ComboBoxState" /> values that specifies the visual state of the text box.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawTextBox(Graphics g, Rectangle bounds, ComboBoxState state)
	{
		DrawTextBox(g, bounds, string.Empty, null, Rectangle.Empty, TextFormatFlags.VerticalCenter, state);
	}

	/// <summary>Draws a text box in the specified state and bounds, with the specified text.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
	/// <param name="comboBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="comboBoxText" />.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ComboBoxState" /> values that specifies the visual state of the text box.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, ComboBoxState state)
	{
		DrawTextBox(g, bounds, comboBoxText, font, Rectangle.Empty, TextFormatFlags.VerticalCenter, state);
	}

	/// <summary>Draws a text box in the specified state and bounds, with the specified text and text bounds.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
	/// <param name="comboBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="comboBoxText" />.</param>
	/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds in which to draw <paramref name="comboBoxText" />.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ComboBoxState" /> values that specifies the visual state of the text box.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, Rectangle textBounds, ComboBoxState state)
	{
		DrawTextBox(g, bounds, comboBoxText, font, textBounds, TextFormatFlags.Left, state);
	}

	/// <summary>Draws a text box in the specified state and bounds, with the specified text and text formatting.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
	/// <param name="comboBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="comboBoxText" />.</param>
	/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ComboBoxState" /> values that specifies the visual state of the text box.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, TextFormatFlags flags, ComboBoxState state)
	{
		DrawTextBox(g, bounds, comboBoxText, font, Rectangle.Empty, flags |= TextFormatFlags.VerticalCenter, state);
	}

	private static VisualStyleRenderer GetComboRenderer(ComboBoxState state)
	{
		return state switch
		{
			ComboBoxState.Disabled => new VisualStyleRenderer(VisualStyleElement.ComboBox.DropDownButton.Disabled), 
			ComboBoxState.Hot => new VisualStyleRenderer(VisualStyleElement.ComboBox.DropDownButton.Hot), 
			ComboBoxState.Pressed => new VisualStyleRenderer(VisualStyleElement.ComboBox.DropDownButton.Pressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.ComboBox.DropDownButton.Normal), 
		};
	}

	private static VisualStyleRenderer GetTextBoxRenderer(ComboBoxState state)
	{
		return state switch
		{
			ComboBoxState.Disabled => new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Disabled), 
			ComboBoxState.Hot => new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Hot), 
			_ => new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Normal), 
		};
	}
}
