using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms;

/// <summary>Provides methods used to render a track bar control with visual styles. This class cannot be inherited.</summary>
/// <filterpriority>2</filterpriority>
public sealed class TrackBarRenderer
{
	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.TrackBarRenderer" /> class can be used to draw a track bar with visual styles.</summary>
	/// <returns>true if the user has enabled visual styles in the operating system and visual styles are applied to the client area of application windows; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool IsSupported => VisualStyleInformation.IsEnabledByUser && (Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled || Application.VisualStyleState == VisualStyleState.ClientAreaEnabled);

	private TrackBarRenderer()
	{
	}

	/// <summary>Draws a downward-pointing track bar slider (also known as the thumb) with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track bar slider.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track bar slider.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawBottomPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		(state switch
		{
			TrackBarThumbState.Disabled => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbBottom.Disabled), 
			TrackBarThumbState.Hot => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbBottom.Hot), 
			TrackBarThumbState.Pressed => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbBottom.Pressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbBottom.Normal), 
		}).DrawBackground(g, bounds);
	}

	/// <summary>Draws a horizontal track bar slider (also known as the thumb) with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track bar slider.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track bar slider.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawHorizontalThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		(state switch
		{
			TrackBarThumbState.Disabled => new VisualStyleRenderer(VisualStyleElement.TrackBar.Thumb.Disabled), 
			TrackBarThumbState.Hot => new VisualStyleRenderer(VisualStyleElement.TrackBar.Thumb.Hot), 
			TrackBarThumbState.Pressed => new VisualStyleRenderer(VisualStyleElement.TrackBar.Thumb.Pressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.TrackBar.Thumb.Normal), 
		}).DrawBackground(g, bounds);
	}

	/// <summary>Draws the specified number of horizontal track bar ticks with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the ticks.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the ticks.</param>
	/// <param name="numTicks">The number of ticks to draw.</param>
	/// <param name="edgeStyle">One of the <see cref="T:System.Windows.Forms.VisualStyles.EdgeStyle" /> values.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	public static void DrawHorizontalTicks(Graphics g, Rectangle bounds, int numTicks, EdgeStyle edgeStyle)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		if (bounds.Height > 0 && bounds.Width > 0 && numTicks > 0)
		{
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.Ticks.Normal);
			double num = bounds.Left;
			double num2 = (double)(bounds.Width - 2) / (double)(numTicks - 1);
			for (int i = 0; i < numTicks; i++)
			{
				visualStyleRenderer.DrawEdge(g, new Rectangle((int)Math.Round(num), bounds.Top, 5, bounds.Height), Edges.Left, edgeStyle, EdgeEffects.None);
				num += num2;
			}
		}
	}

	/// <summary>Draws the track for a horizontal track bar with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawHorizontalTrack(Graphics g, Rectangle bounds)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.Track.Normal);
		visualStyleRenderer.DrawBackground(g, bounds);
	}

	/// <summary>Draws a left-pointing track bar slider (also known as the thumb) with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track bar slider.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track bar slider.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawLeftPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		(state switch
		{
			TrackBarThumbState.Disabled => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbLeft.Disabled), 
			TrackBarThumbState.Hot => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbLeft.Hot), 
			TrackBarThumbState.Pressed => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbLeft.Pressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbLeft.Normal), 
		}).DrawBackground(g, bounds);
	}

	/// <summary>Draws a right-pointing track bar slider (also known as the thumb) with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track bar slider.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track bar slider.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawRightPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		(state switch
		{
			TrackBarThumbState.Disabled => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbRight.Disabled), 
			TrackBarThumbState.Hot => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbRight.Hot), 
			TrackBarThumbState.Pressed => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbRight.Pressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbRight.Normal), 
		}).DrawBackground(g, bounds);
	}

	/// <summary>Draws an upward-pointing track bar slider (also known as the thumb) with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track bar slider.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track bar slider.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawTopPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		(state switch
		{
			TrackBarThumbState.Disabled => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbTop.Disabled), 
			TrackBarThumbState.Hot => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbTop.Hot), 
			TrackBarThumbState.Pressed => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbTop.Pressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbTop.Normal), 
		}).DrawBackground(g, bounds);
	}

	/// <summary>Draws a vertical track bar slider (also known as the thumb) with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track bar slider.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track bar slider.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawVerticalThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		(state switch
		{
			TrackBarThumbState.Disabled => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbVertical.Disabled), 
			TrackBarThumbState.Hot => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbVertical.Hot), 
			TrackBarThumbState.Pressed => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbVertical.Pressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbVertical.Normal), 
		}).DrawBackground(g, bounds);
	}

	/// <summary>Draws the specified number of vertical track bar ticks with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the ticks.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the ticks.</param>
	/// <param name="numTicks">The number of ticks to draw.</param>
	/// <param name="edgeStyle">One of the <see cref="T:System.Windows.Forms.VisualStyles.EdgeStyle" /> values.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	public static void DrawVerticalTicks(Graphics g, Rectangle bounds, int numTicks, EdgeStyle edgeStyle)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		if (bounds.Height > 0 && bounds.Width > 0 && numTicks > 0)
		{
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.TicksVertical.Normal);
			double num = bounds.Top;
			double num2 = (double)(bounds.Height - 2) / (double)(numTicks - 1);
			for (int i = 0; i < numTicks; i++)
			{
				visualStyleRenderer.DrawEdge(g, new Rectangle(bounds.Left, (int)Math.Round(num), bounds.Width, 5), Edges.Top, edgeStyle, EdgeEffects.None);
				num += num2;
			}
		}
	}

	/// <summary>Draws the track for a vertical track bar with visual styles.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawVerticalTrack(Graphics g, Rectangle bounds)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.Track.Normal);
		visualStyleRenderer.DrawBackground(g, bounds);
	}

	/// <summary>Returns the size, in pixels, of the track bar slider (also known as the thumb) that points down.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size, in pixels, of the slider.</returns>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Size GetBottomPointingThumbSize(Graphics g, TrackBarThumbState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		return (state switch
		{
			TrackBarThumbState.Disabled => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbBottom.Disabled), 
			TrackBarThumbState.Hot => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbBottom.Hot), 
			TrackBarThumbState.Pressed => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbBottom.Pressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbBottom.Normal), 
		}).GetPartSize(g, ThemeSizeType.Draw);
	}

	/// <summary>Returns the size, in pixels, of the track bar slider (also known as the thumb) that points to the left.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size, in pixels, of the slider.</returns>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the slider.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Size GetLeftPointingThumbSize(Graphics g, TrackBarThumbState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		return (state switch
		{
			TrackBarThumbState.Disabled => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbLeft.Disabled), 
			TrackBarThumbState.Hot => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbLeft.Hot), 
			TrackBarThumbState.Pressed => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbLeft.Pressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbLeft.Normal), 
		}).GetPartSize(g, ThemeSizeType.Draw);
	}

	/// <summary>Returns the size, in pixels, of the track bar slider (also known as the thumb) that points to the right.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size, in pixels, of the slider.</returns>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the slider.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Size GetRightPointingThumbSize(Graphics g, TrackBarThumbState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		return (state switch
		{
			TrackBarThumbState.Disabled => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbRight.Disabled), 
			TrackBarThumbState.Hot => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbRight.Hot), 
			TrackBarThumbState.Pressed => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbRight.Pressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbRight.Normal), 
		}).GetPartSize(g, ThemeSizeType.Draw);
	}

	/// <summary>Returns the size, in pixels, of the track bar slider (also known as the thumb) that points up.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size, in pixels, of the slider.</returns>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
	/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the slider.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Size GetTopPointingThumbSize(Graphics g, TrackBarThumbState state)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException();
		}
		return (state switch
		{
			TrackBarThumbState.Disabled => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbTop.Disabled), 
			TrackBarThumbState.Hot => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbTop.Hot), 
			TrackBarThumbState.Pressed => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbTop.Pressed), 
			_ => new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbTop.Normal), 
		}).GetPartSize(g, ThemeSizeType.Draw);
	}
}
