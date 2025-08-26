using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms;

/// <summary>Provides methods used to render an option button control (also known as a radio button) with or without visual styles. This class cannot be inherited.</summary>
/// <filterpriority>2</filterpriority>
public sealed class RadioButtonRenderer
{
	private static bool always_use_visual_styles;

	/// <summary>Gets or sets a value indicating whether the renderer uses the application state to determine rendering style.</summary>
	/// <returns>true if the application state is used to determine rendering style; otherwise, false. The default is true.</returns>
	public static bool RenderMatchingApplicationState
	{
		get
		{
			return !always_use_visual_styles;
		}
		set
		{
			always_use_visual_styles = !value;
		}
	}

	private RadioButtonRenderer()
	{
	}

	/// <summary>Draws an option button control (also known as a radio button) in the specified state and location.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the option button.</param>
	/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the option button glyph at.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawRadioButton(Graphics g, Point glyphLocation, RadioButtonState state)
	{
		DrawRadioButton(g, glyphLocation, Rectangle.Empty, string.Empty, null, TextFormatFlags.HorizontalCenter, null, Rectangle.Empty, focused: false, state);
	}

	/// <summary>Draws an option button control (also known as a radio button) in the specified state and location, with the specified text, and with an optional focus rectangle.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the option button.</param>
	/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the option button glyph at.</param>
	/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="radioButtonText" /> in.</param>
	/// <param name="radioButtonText">The <see cref="T:System.String" /> to draw with the option button.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="radioButtonText" />.</param>
	/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, bool focused, RadioButtonState state)
	{
		DrawRadioButton(g, glyphLocation, textBounds, radioButtonText, font, TextFormatFlags.HorizontalCenter, null, Rectangle.Empty, focused, state);
	}

	/// <summary>Draws an option button control (also known as a radio button) in the specified state and location, with the specified text and text formatting, and with an optional focus rectangle.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the option button.</param>
	/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the option button glyph at.</param>
	/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="radioButtonText" /> in.</param>
	/// <param name="radioButtonText">The <see cref="T:System.String" /> to draw with the option button.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="radioButtonText" />.</param>
	/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
	/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
	public static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, TextFormatFlags flags, bool focused, RadioButtonState state)
	{
		DrawRadioButton(g, glyphLocation, textBounds, radioButtonText, font, flags, null, Rectangle.Empty, focused, state);
	}

	/// <summary>Draws an option button control (also known as a radio button) in the specified state and location, with the specified text and image, and with an optional focus rectangle.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the option button.</param>
	/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the option button glyph at.</param>
	/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="radioButtonText" /> in.</param>
	/// <param name="radioButtonText">The <see cref="T:System.String" /> to draw with the option button.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="radioButtonText" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw with the option button.</param>
	/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="image" /> in.</param>
	/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, Image image, Rectangle imageBounds, bool focused, RadioButtonState state)
	{
		DrawRadioButton(g, glyphLocation, textBounds, radioButtonText, font, TextFormatFlags.HorizontalCenter, image, imageBounds, focused, state);
	}

	/// <summary>Draws an option button control (also known as a radio button) in the specified state and location; with the specified text, text formatting, and image; and with an optional focus rectangle.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the option button.</param>
	/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the option button glyph at.</param>
	/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="radioButtonText" /> in.</param>
	/// <param name="radioButtonText">The <see cref="T:System.String" /> to draw with the option button.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="radioButtonText" />.</param>
	/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw with the option button.</param>
	/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="image" /> in.</param>
	/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
	public static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, TextFormatFlags flags, Image image, Rectangle imageBounds, bool focused, RadioButtonState state)
	{
		Rectangle rectangle = new Rectangle(glyphLocation, GetGlyphSize(g, state));
		if (Application.RenderWithVisualStyles || always_use_visual_styles)
		{
			VisualStyleRenderer radioButtonRenderer = GetRadioButtonRenderer(state);
			radioButtonRenderer.DrawBackground(g, rectangle);
			if (image != null)
			{
				radioButtonRenderer.DrawImage(g, imageBounds, image);
			}
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, textBounds);
			}
			if (radioButtonText != string.Empty)
			{
				if (state == RadioButtonState.CheckedDisabled || state == RadioButtonState.UncheckedDisabled)
				{
					TextRenderer.DrawText(g, radioButtonText, font, textBounds, SystemColors.GrayText, flags);
				}
				else
				{
					TextRenderer.DrawText(g, radioButtonText, font, textBounds, SystemColors.ControlText, flags);
				}
			}
			return;
		}
		switch (state)
		{
		case RadioButtonState.CheckedDisabled:
			ControlPaint.DrawRadioButton(g, rectangle, ButtonState.Inactive | ButtonState.Checked);
			break;
		case RadioButtonState.CheckedNormal:
		case RadioButtonState.CheckedHot:
			ControlPaint.DrawRadioButton(g, rectangle, ButtonState.Checked);
			break;
		case RadioButtonState.CheckedPressed:
			ControlPaint.DrawRadioButton(g, rectangle, ButtonState.Pushed | ButtonState.Checked);
			break;
		case RadioButtonState.UncheckedPressed:
		case RadioButtonState.UncheckedDisabled:
			ControlPaint.DrawRadioButton(g, rectangle, ButtonState.Inactive);
			break;
		case RadioButtonState.UncheckedNormal:
		case RadioButtonState.UncheckedHot:
			ControlPaint.DrawRadioButton(g, rectangle, ButtonState.Normal);
			break;
		}
		if (image != null)
		{
			g.DrawImage(image, imageBounds);
		}
		if (focused)
		{
			ControlPaint.DrawFocusRectangle(g, textBounds);
		}
		if (radioButtonText != string.Empty)
		{
			TextRenderer.DrawText(g, radioButtonText, font, textBounds, SystemColors.ControlText, flags);
		}
	}

	/// <summary>Indicates whether the background of the option button (also known as a radio button) has semitransparent or alpha-blended pieces.</summary>
	/// <returns>true if the background of the option button has semitransparent or alpha-blended pieces; otherwise, false.</returns>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool IsBackgroundPartiallyTransparent(RadioButtonState state)
	{
		if (!VisualStyleRenderer.IsSupported)
		{
			return false;
		}
		VisualStyleRenderer radioButtonRenderer = GetRadioButtonRenderer(state);
		return radioButtonRenderer.IsBackgroundPartiallyTransparent();
	}

	/// <summary>Draws the background of a control's parent in the specified area.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the background of the parent of <paramref name="childControl" />. </param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which to draw the parent control's background. This rectangle should be inside the child control’s bounds.</param>
	/// <param name="childControl">The control whose parent's background will be drawn.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawParentBackground(Graphics g, Rectangle bounds, Control childControl)
	{
		if (VisualStyleRenderer.IsSupported)
		{
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.UncheckedNormal);
			visualStyleRenderer.DrawParentBackground(g, bounds, childControl);
		}
	}

	/// <summary>Returns the size, in pixels, of the option button (also known as a radio button) glyph.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size, in pixels, of the option button glyph.</returns>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the option button.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Size GetGlyphSize(Graphics g, RadioButtonState state)
	{
		if (!VisualStyleRenderer.IsSupported)
		{
			return new Size(13, 13);
		}
		VisualStyleRenderer radioButtonRenderer = GetRadioButtonRenderer(state);
		return radioButtonRenderer.GetPartSize(g, ThemeSizeType.Draw);
	}

	private static VisualStyleRenderer GetRadioButtonRenderer(RadioButtonState state)
	{
		return state switch
		{
			RadioButtonState.CheckedDisabled => new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.CheckedDisabled), 
			RadioButtonState.CheckedHot => new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.CheckedHot), 
			RadioButtonState.CheckedNormal => new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.CheckedNormal), 
			RadioButtonState.CheckedPressed => new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.CheckedPressed), 
			RadioButtonState.UncheckedDisabled => new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.UncheckedDisabled), 
			RadioButtonState.UncheckedHot => new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.UncheckedHot), 
			RadioButtonState.UncheckedPressed => new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.UncheckedPressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.UncheckedNormal), 
		};
	}
}
