using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms;

/// <summary>Provides methods used to render a scroll bar control with visual styles. This class cannot be inherited.</summary>
/// <filterpriority>2</filterpriority>
public sealed class ScrollBarRenderer
{
	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ScrollBarRenderer" /> class can be used to draw a scroll bar with visual styles.</summary>
	/// <returns>true if the user has enabled visual styles in the operating system and visual styles are applied to the client areas of application windows; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool IsSupported => VisualStyleInformation.IsEnabledByUser && (Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled || Application.VisualStyleState == VisualStyleState.ClientAreaEnabled);

	private ScrollBarRenderer()
	{
	}

	/// <summary>Draws a scroll arrow with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll arrow.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll arrow.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarArrowButtonState" /> values that specifies the visual state of the scroll arrow.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawArrowButton(Graphics g, Rectangle bounds, ScrollBarArrowButtonState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		(state switch
		{
			ScrollBarArrowButtonState.DownDisabled => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.DownDisabled), 
			ScrollBarArrowButtonState.DownHot => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.DownHot), 
			ScrollBarArrowButtonState.DownPressed => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.DownPressed), 
			ScrollBarArrowButtonState.LeftDisabled => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.LeftDisabled), 
			ScrollBarArrowButtonState.LeftHot => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.LeftHot), 
			ScrollBarArrowButtonState.LeftNormal => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.LeftNormal), 
			ScrollBarArrowButtonState.LeftPressed => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.LeftPressed), 
			ScrollBarArrowButtonState.RightDisabled => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.RightDisabled), 
			ScrollBarArrowButtonState.RightHot => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.RightHot), 
			ScrollBarArrowButtonState.RightNormal => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.RightNormal), 
			ScrollBarArrowButtonState.RightPressed => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.RightPressed), 
			ScrollBarArrowButtonState.UpDisabled => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.UpDisabled), 
			ScrollBarArrowButtonState.UpHot => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.UpHot), 
			ScrollBarArrowButtonState.UpNormal => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.UpNormal), 
			ScrollBarArrowButtonState.UpPressed => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.UpPressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.DownNormal), 
		}).DrawBackground(g, bounds);
	}

	/// <summary>Draws a horizontal scroll box (also known as the thumb) with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll box.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll box.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll box.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawHorizontalThumb(Graphics g, Rectangle bounds, ScrollBarState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		(state switch
		{
			ScrollBarState.Disabled => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Disabled), 
			ScrollBarState.Hot => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Hot), 
			ScrollBarState.Pressed => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Pressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Normal), 
		}).DrawBackground(g, bounds);
	}

	/// <summary>Draws a grip on a horizontal scroll box (also known as the thumb) with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll box grip.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll box grip.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll box grip.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawHorizontalThumbGrip(Graphics g, Rectangle bounds, ScrollBarState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.GripperHorizontal.Normal);
		visualStyleRenderer.DrawBackground(g, bounds);
	}

	/// <summary>Draws a horizontal scroll bar track with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll bar track.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll bar track.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll bar track.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawLeftHorizontalTrack(Graphics g, Rectangle bounds, ScrollBarState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		(state switch
		{
			ScrollBarState.Disabled => new VisualStyleRenderer(VisualStyleElement.ScrollBar.LeftTrackHorizontal.Disabled), 
			ScrollBarState.Hot => new VisualStyleRenderer(VisualStyleElement.ScrollBar.LeftTrackHorizontal.Hot), 
			ScrollBarState.Pressed => new VisualStyleRenderer(VisualStyleElement.ScrollBar.LeftTrackHorizontal.Pressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.ScrollBar.LeftTrackHorizontal.Normal), 
		}).DrawBackground(g, bounds);
	}

	/// <summary>Draws a vertical scroll bar track with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll bar track.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll bar track.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll bar track.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawLowerVerticalTrack(Graphics g, Rectangle bounds, ScrollBarState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		(state switch
		{
			ScrollBarState.Disabled => new VisualStyleRenderer(VisualStyleElement.ScrollBar.LowerTrackVertical.Disabled), 
			ScrollBarState.Hot => new VisualStyleRenderer(VisualStyleElement.ScrollBar.LowerTrackVertical.Hot), 
			ScrollBarState.Pressed => new VisualStyleRenderer(VisualStyleElement.ScrollBar.LowerTrackVertical.Pressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.ScrollBar.LowerTrackVertical.Normal), 
		}).DrawBackground(g, bounds);
	}

	/// <summary>Draws a horizontal scroll bar track with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll bar track.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll bar track.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll bar track.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawRightHorizontalTrack(Graphics g, Rectangle bounds, ScrollBarState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		(state switch
		{
			ScrollBarState.Disabled => new VisualStyleRenderer(VisualStyleElement.ScrollBar.RightTrackHorizontal.Disabled), 
			ScrollBarState.Hot => new VisualStyleRenderer(VisualStyleElement.ScrollBar.RightTrackHorizontal.Hot), 
			ScrollBarState.Pressed => new VisualStyleRenderer(VisualStyleElement.ScrollBar.RightTrackHorizontal.Pressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.ScrollBar.RightTrackHorizontal.Normal), 
		}).DrawBackground(g, bounds);
	}

	/// <summary>Draws a scroll bar sizing handle with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the sizing handle.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the sizing handle.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarSizeBoxState" /> values that specifies the visual state of the sizing handle.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawSizeBox(Graphics g, Rectangle bounds, ScrollBarSizeBoxState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		(state switch
		{
			ScrollBarSizeBoxState.RightAlign => new VisualStyleRenderer(VisualStyleElement.ScrollBar.SizeBox.RightAlign), 
			_ => new VisualStyleRenderer(VisualStyleElement.ScrollBar.SizeBox.LeftAlign), 
		}).DrawBackground(g, bounds);
	}

	/// <summary>Draws a vertical scroll bar track with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll bar track.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll bar track.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll bar track.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawUpperVerticalTrack(Graphics g, Rectangle bounds, ScrollBarState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		(state switch
		{
			ScrollBarState.Disabled => new VisualStyleRenderer(VisualStyleElement.ScrollBar.UpperTrackVertical.Disabled), 
			ScrollBarState.Hot => new VisualStyleRenderer(VisualStyleElement.ScrollBar.UpperTrackVertical.Hot), 
			ScrollBarState.Pressed => new VisualStyleRenderer(VisualStyleElement.ScrollBar.UpperTrackVertical.Pressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.ScrollBar.UpperTrackVertical.Normal), 
		}).DrawBackground(g, bounds);
	}

	/// <summary>Draws a vertical scroll box (also known as the thumb) with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll box.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll box.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll box.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawVerticalThumb(Graphics g, Rectangle bounds, ScrollBarState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		(state switch
		{
			ScrollBarState.Disabled => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonVertical.Disabled), 
			ScrollBarState.Hot => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonVertical.Hot), 
			ScrollBarState.Pressed => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonVertical.Pressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonVertical.Normal), 
		}).DrawBackground(g, bounds);
	}

	/// <summary>Draws a grip on a vertical scroll box (also known as the thumb) with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll box grip.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll box grip.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll box grip.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawVerticalThumbGrip(Graphics g, Rectangle bounds, ScrollBarState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.GripperVertical.Normal);
		visualStyleRenderer.DrawBackground(g, bounds);
	}

	/// <summary>Returns the size of the sizing handle.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size of the sizing handle.</returns>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the sizing handle.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Size GetSizeBoxSize(Graphics g, ScrollBarState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.SizeBox.LeftAlign);
		return visualStyleRenderer.GetPartSize(g, ThemeSizeType.Draw);
	}

	/// <summary>Returns the size of the scroll box grip.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size of the scroll box grip.</returns>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll box grip.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Size GetThumbGripSize(Graphics g, ScrollBarState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.GripperVertical.Normal);
		return visualStyleRenderer.GetPartSize(g, ThemeSizeType.Draw);
	}
}
