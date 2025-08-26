using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms;

/// <summary>Provides methods used to render a check box control with or without visual styles. This class cannot be inherited.</summary>
/// <filterpriority>2</filterpriority>
public sealed class CheckBoxRenderer
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

	private CheckBoxRenderer()
	{
	}

	/// <summary>Draws a check box control in the specified state and location.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the check box.</param>
	/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the check box glyph at.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.CheckBoxState" /> values that specifies the visual state of the check box.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawCheckBox(Graphics g, Point glyphLocation, CheckBoxState state)
	{
		DrawCheckBox(g, glyphLocation, Rectangle.Empty, string.Empty, null, TextFormatFlags.HorizontalCenter, null, Rectangle.Empty, focused: false, state);
	}

	/// <summary>Draws a check box control in the specified state and location, with the specified text, and with an optional focus rectangle.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the check box.</param>
	/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the check box glyph at.</param>
	/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="checkBoxText" /> in.</param>
	/// <param name="checkBoxText">The <see cref="T:System.String" /> to draw with the check box.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="checkBoxText" />.</param>
	/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.CheckBoxState" /> values that specifies the visual state of the check box.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, bool focused, CheckBoxState state)
	{
		DrawCheckBox(g, glyphLocation, textBounds, checkBoxText, font, TextFormatFlags.HorizontalCenter, null, Rectangle.Empty, focused, state);
	}

	/// <summary>Draws a check box control in the specified state and location, with the specified text and text formatting, and with an optional focus rectangle.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the check box.</param>
	/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the check box glyph at.</param>
	/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="checkBoxText" /> in.</param>
	/// <param name="checkBoxText">The <see cref="T:System.String" /> to draw with the check box.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="checkBoxText" />.</param>
	/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
	/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.CheckBoxState" /> values that specifies the visual state of the check box.</param>
	public static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, TextFormatFlags flags, bool focused, CheckBoxState state)
	{
		DrawCheckBox(g, glyphLocation, textBounds, checkBoxText, font, flags, null, Rectangle.Empty, focused, state);
	}

	/// <summary>Draws a check box control in the specified state and location, with the specified text and image, and with an optional focus rectangle.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the check box.</param>
	/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the check box glyph at.</param>
	/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="checkBoxText" /> in.</param>
	/// <param name="checkBoxText">The <see cref="T:System.String" /> to draw with the check box.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="checkBoxText" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw with the check box.</param>
	/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of <paramref name="image" />.</param>
	/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.CheckBoxState" /> values that specifies the visual state of the check box.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, Image image, Rectangle imageBounds, bool focused, CheckBoxState state)
	{
		DrawCheckBox(g, glyphLocation, textBounds, checkBoxText, font, TextFormatFlags.HorizontalCenter, image, imageBounds, focused, state);
	}

	/// <summary>Draws a check box control in the specified state and location; with the specified text, text formatting, and image; and with an optional focus rectangle.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the check box.</param>
	/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the check box glyph at.</param>
	/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="checkBoxText" /> in.</param>
	/// <param name="checkBoxText">The <see cref="T:System.String" /> to draw with the check box.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="checkBoxText" />.</param>
	/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw with the check box.</param>
	/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of <paramref name="image" />.</param>
	/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.CheckBoxState" /> values that specifies the visual state of the check box.</param>
	public static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, TextFormatFlags flags, Image image, Rectangle imageBounds, bool focused, CheckBoxState state)
	{
		Rectangle rectangle = new Rectangle(glyphLocation, GetGlyphSize(g, state));
		if (Application.RenderWithVisualStyles || always_use_visual_styles)
		{
			VisualStyleRenderer checkBoxRenderer = GetCheckBoxRenderer(state);
			checkBoxRenderer.DrawBackground(g, rectangle);
			if (image != null)
			{
				checkBoxRenderer.DrawImage(g, imageBounds, image);
			}
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, textBounds);
			}
			if (checkBoxText != string.Empty)
			{
				if (state == CheckBoxState.CheckedDisabled || state == CheckBoxState.MixedDisabled || state == CheckBoxState.UncheckedDisabled)
				{
					TextRenderer.DrawText(g, checkBoxText, font, textBounds, SystemColors.GrayText, flags);
				}
				else
				{
					TextRenderer.DrawText(g, checkBoxText, font, textBounds, SystemColors.ControlText, flags);
				}
			}
			return;
		}
		switch (state)
		{
		case CheckBoxState.CheckedDisabled:
		case CheckBoxState.MixedPressed:
		case CheckBoxState.MixedDisabled:
			ControlPaint.DrawCheckBox(g, rectangle, ButtonState.Inactive | ButtonState.Checked);
			break;
		case CheckBoxState.CheckedNormal:
		case CheckBoxState.CheckedHot:
			ControlPaint.DrawCheckBox(g, rectangle, ButtonState.Checked);
			break;
		case CheckBoxState.CheckedPressed:
			ControlPaint.DrawCheckBox(g, rectangle, ButtonState.Pushed | ButtonState.Checked);
			break;
		case CheckBoxState.MixedNormal:
		case CheckBoxState.MixedHot:
			ControlPaint.DrawMixedCheckBox(g, rectangle, ButtonState.Checked);
			break;
		case CheckBoxState.UncheckedPressed:
		case CheckBoxState.UncheckedDisabled:
			ControlPaint.DrawCheckBox(g, rectangle, ButtonState.Inactive);
			break;
		case CheckBoxState.UncheckedNormal:
		case CheckBoxState.UncheckedHot:
			ControlPaint.DrawCheckBox(g, rectangle, ButtonState.Normal);
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
		if (checkBoxText != string.Empty)
		{
			TextRenderer.DrawText(g, checkBoxText, font, textBounds, SystemColors.ControlText, flags);
		}
	}

	/// <summary>Indicates whether the background of the check box has semitransparent or alpha-blended pieces.</summary>
	/// <returns>true if the background of the check box has semitransparent or alpha-blended pieces; otherwise, false.</returns>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.CheckBoxState" /> values that specifies the visual state of the check box.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool IsBackgroundPartiallyTransparent(CheckBoxState state)
	{
		if (!VisualStyleRenderer.IsSupported)
		{
			return false;
		}
		VisualStyleRenderer checkBoxRenderer = GetCheckBoxRenderer(state);
		return checkBoxRenderer.IsBackgroundPartiallyTransparent();
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
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.UncheckedNormal);
			visualStyleRenderer.DrawParentBackground(g, bounds, childControl);
		}
	}

	/// <summary>Returns the size of the check box glyph.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the check box glyph.</returns>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.CheckBoxState" /> values that specifies the visual state of the check box.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Size GetGlyphSize(Graphics g, CheckBoxState state)
	{
		if (!VisualStyleRenderer.IsSupported)
		{
			return new Size(13, 13);
		}
		VisualStyleRenderer checkBoxRenderer = GetCheckBoxRenderer(state);
		return checkBoxRenderer.GetPartSize(g, ThemeSizeType.Draw);
	}

	private static VisualStyleRenderer GetCheckBoxRenderer(CheckBoxState state)
	{
		return state switch
		{
			CheckBoxState.CheckedDisabled => new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.CheckedDisabled), 
			CheckBoxState.CheckedHot => new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.CheckedHot), 
			CheckBoxState.CheckedNormal => new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.CheckedNormal), 
			CheckBoxState.CheckedPressed => new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.CheckedPressed), 
			CheckBoxState.MixedDisabled => new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.MixedDisabled), 
			CheckBoxState.MixedHot => new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.MixedHot), 
			CheckBoxState.MixedNormal => new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.MixedNormal), 
			CheckBoxState.MixedPressed => new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.MixedPressed), 
			CheckBoxState.UncheckedDisabled => new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.UncheckedDisabled), 
			CheckBoxState.UncheckedHot => new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.UncheckedHot), 
			CheckBoxState.UncheckedPressed => new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.UncheckedPressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.UncheckedNormal), 
		};
	}
}
