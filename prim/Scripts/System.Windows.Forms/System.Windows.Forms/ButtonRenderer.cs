using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms;

/// <summary>Provides methods used to render a button control with or without visual styles. This class cannot be inherited.</summary>
/// <filterpriority>2</filterpriority>
public sealed class ButtonRenderer
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

	private ButtonRenderer()
	{
	}

	/// <summary>Draws a button control in the specified state and bounds.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawButton(Graphics g, Rectangle bounds, PushButtonState state)
	{
		DrawButton(g, bounds, string.Empty, null, TextFormatFlags.Left, null, Rectangle.Empty, focused: false, state);
	}

	/// <summary>Draws a button control in the specified state and bounds, and with an optional focus rectangle.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
	/// <param name="focused">true to draw a focus rectangle on the button; otherwise, false.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawButton(Graphics g, Rectangle bounds, bool focused, PushButtonState state)
	{
		DrawButton(g, bounds, string.Empty, null, TextFormatFlags.Left, null, Rectangle.Empty, focused, state);
	}

	/// <summary>Draws a button control in the specified state and bounds, with the specified image, and with an optional focus rectangle.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw on the button.</param>
	/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of <paramref name="image" />.</param>
	/// <param name="focused">true to draw a focus rectangle on the button; otherwise, false.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawButton(Graphics g, Rectangle bounds, Image image, Rectangle imageBounds, bool focused, PushButtonState state)
	{
		DrawButton(g, bounds, string.Empty, null, TextFormatFlags.Left, image, imageBounds, focused, state);
	}

	/// <summary>Draws a button control in the specified state and bounds, with the specified text, and with an optional focus rectangle.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
	/// <param name="buttonText">The <see cref="T:System.String" /> to draw on the button.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="buttonText" />.</param>
	/// <param name="focused">true to draw a focus rectangle on the button; otherwise, false.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawButton(Graphics g, Rectangle bounds, string buttonText, Font font, bool focused, PushButtonState state)
	{
		DrawButton(g, bounds, buttonText, font, TextFormatFlags.HorizontalCenter, null, Rectangle.Empty, focused, state);
	}

	/// <summary>Draws a button control in the specified state and bounds, with the specified text and text formatting, and with an optional focus rectangle.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
	/// <param name="buttonText">The <see cref="T:System.String" /> to draw on the button.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="buttonText" />.</param>
	/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values to apply to <paramref name="buttonText" />.</param>
	/// <param name="focused">true to draw a focus rectangle on the button; otherwise, false.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
	public static void DrawButton(Graphics g, Rectangle bounds, string buttonText, Font font, TextFormatFlags flags, bool focused, PushButtonState state)
	{
		DrawButton(g, bounds, buttonText, font, flags, null, Rectangle.Empty, focused, state);
	}

	/// <summary>Draws a button control in the specified state and bounds, with the specified text and image, and with an optional focus rectangle.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
	/// <param name="buttonText">The <see cref="T:System.String" /> to draw on the button.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="buttonText" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw on the button.</param>
	/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of <paramref name="image" />.</param>
	/// <param name="focused">true to draw a focus rectangle on the button; otherwise, false.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawButton(Graphics g, Rectangle bounds, string buttonText, Font font, Image image, Rectangle imageBounds, bool focused, PushButtonState state)
	{
		DrawButton(g, bounds, buttonText, font, TextFormatFlags.HorizontalCenter, image, imageBounds, focused, state);
	}

	/// <summary>Draws a button control in the specified state and bounds; with the specified text, text formatting, and image; and with an optional focus rectangle.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
	/// <param name="buttonText">The <see cref="T:System.String" /> to draw on the button.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="buttonText" />.</param>
	/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values to apply to <paramref name="buttonText" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw on the button.</param>
	/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of <paramref name="image" />.</param>
	/// <param name="focused">true to draw a focus rectangle on the button; otherwise, false.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
	public static void DrawButton(Graphics g, Rectangle bounds, string buttonText, Font font, TextFormatFlags flags, Image image, Rectangle imageBounds, bool focused, PushButtonState state)
	{
		if (Application.RenderWithVisualStyles || always_use_visual_styles)
		{
			VisualStyleRenderer pushButtonRenderer = GetPushButtonRenderer(state);
			pushButtonRenderer.DrawBackground(g, bounds);
			if (image != null)
			{
				pushButtonRenderer.DrawImage(g, imageBounds, image);
			}
		}
		else
		{
			if (state == PushButtonState.Pressed)
			{
				ControlPaint.DrawButton(g, bounds, ButtonState.Pushed);
			}
			else
			{
				ControlPaint.DrawButton(g, bounds, ButtonState.Normal);
			}
			if (image != null)
			{
				g.DrawImage(image, imageBounds);
			}
		}
		Rectangle rectangle = bounds;
		rectangle.Inflate(-3, -3);
		if (focused)
		{
			ControlPaint.DrawFocusRectangle(g, rectangle);
		}
		if (buttonText != string.Empty)
		{
			if (state == PushButtonState.Disabled)
			{
				TextRenderer.DrawText(g, buttonText, font, rectangle, SystemColors.GrayText, flags);
			}
			else
			{
				TextRenderer.DrawText(g, buttonText, font, rectangle, SystemColors.ControlText, flags);
			}
		}
	}

	/// <summary>Indicates whether the background of the button has semitransparent or alpha-blended pieces.</summary>
	/// <returns>true if the background of the button has semitransparent or alpha-blended pieces; otherwise, false.</returns>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool IsBackgroundPartiallyTransparent(PushButtonState state)
	{
		if (!VisualStyleRenderer.IsSupported)
		{
			return false;
		}
		VisualStyleRenderer pushButtonRenderer = GetPushButtonRenderer(state);
		return pushButtonRenderer.IsBackgroundPartiallyTransparent();
	}

	/// <summary>Draws the background of a control's parent in the specified area.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the background of the parent of <paramref name="childControl" />.</param>
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
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Default);
			visualStyleRenderer.DrawParentBackground(g, bounds, childControl);
		}
	}

	internal static VisualStyleRenderer GetPushButtonRenderer(PushButtonState state)
	{
		return state switch
		{
			PushButtonState.Normal => new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal), 
			PushButtonState.Hot => new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Hot), 
			PushButtonState.Pressed => new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Pressed), 
			PushButtonState.Disabled => new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Disabled), 
			_ => new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Default), 
		};
	}
}
