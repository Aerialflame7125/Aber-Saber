using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides methods used to paint common Windows controls and their elements. This class cannot be inherited.</summary>
/// <filterpriority>2</filterpriority>
public sealed class ControlPaint
{
	private static int RGBMax = 255;

	private static int HLSMax = 255;

	[System.MonoTODO("Stub, does nothing")]
	private static bool DSFNotImpl;

	/// <summary>Gets the color to use as the <see cref="P:System.Drawing.SystemColors.ControlDark" /> color.</summary>
	/// <returns>The <see cref="T:System.Drawing.Color" /> to use as the <see cref="P:System.Drawing.SystemColors.ControlDark" /> color.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Color ContrastControlDark => SystemColors.ControlDark;

	private ControlPaint()
	{
	}

	internal static void Color2HBS(Color color, out int h, out int l, out int s)
	{
		int r = color.R;
		int g = color.G;
		int b = color.B;
		int num = Math.Max(Math.Max(r, g), b);
		int num2 = Math.Min(Math.Min(r, g), b);
		l = ((num + num2) * HLSMax + RGBMax) / (2 * RGBMax);
		if (num == num2)
		{
			h = 0;
			s = 0;
			return;
		}
		if (l <= HLSMax / 2)
		{
			s = ((num - num2) * HLSMax + (num + num2) / 2) / (num + num2);
		}
		else
		{
			s = ((num - num2) * HLSMax + (2 * RGBMax - num - num2) / 2) / (2 * RGBMax - num - num2);
		}
		int num3 = ((num - r) * (HLSMax / 6) + (num - num2) / 2) / (num - num2);
		int num4 = ((num - g) * (HLSMax / 6) + (num - num2) / 2) / (num - num2);
		int num5 = ((num - b) * (HLSMax / 6) + (num - num2) / 2) / (num - num2);
		if (r == num)
		{
			h = num5 - num4;
		}
		else if (g == num)
		{
			h = HLSMax / 3 + num3 - num5;
		}
		else
		{
			h = 2 * HLSMax / 3 + num4 - num3;
		}
		if (h < 0)
		{
			h += HLSMax;
		}
		if (h > HLSMax)
		{
			h -= HLSMax;
		}
	}

	private static int HueToRGB(int n1, int n2, int hue)
	{
		if (hue < 0)
		{
			hue += HLSMax;
		}
		if (hue > HLSMax)
		{
			hue -= HLSMax;
		}
		if (hue < HLSMax / 6)
		{
			return n1 + ((n2 - n1) * hue + HLSMax / 12) / (HLSMax / 6);
		}
		if (hue < HLSMax / 2)
		{
			return n2;
		}
		if (hue < HLSMax * 2 / 3)
		{
			return n1 + ((n2 - n1) * (HLSMax * 2 / 3 - hue) + HLSMax / 12) / (HLSMax / 6);
		}
		return n1;
	}

	internal static Color HBS2Color(int hue, int lum, int sat)
	{
		int red;
		int green;
		int blue;
		if (sat == 0)
		{
			red = (green = (blue = lum * RGBMax / HLSMax));
		}
		else
		{
			int num = ((lum > HLSMax / 2) ? (sat + lum - (sat * lum + HLSMax / 2) / HLSMax) : ((lum * (HLSMax + sat) + HLSMax / 2) / HLSMax));
			int n = 2 * lum - num;
			red = Math.Min(255, (HueToRGB(n, num, hue + HLSMax / 3) * RGBMax + HLSMax / 2) / HLSMax);
			green = Math.Min(255, (HueToRGB(n, num, hue) * RGBMax + HLSMax / 2) / HLSMax);
			blue = Math.Min(255, (HueToRGB(n, num, hue - HLSMax / 3) * RGBMax + HLSMax / 2) / HLSMax);
		}
		return Color.FromArgb(red, green, blue);
	}

	/// <summary>Creates a 16-bit color bitmap.</summary>
	/// <returns>An <see cref="T:System.IntPtr" /> representing the handle to the bitmap.</returns>
	/// <param name="bitmap">The <see cref="T:System.Drawing.Bitmap" /> to create.</param>
	/// <param name="background">The <see cref="T:System.Drawing.Color" /> of the background.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	[System.MonoTODO("Not implemented, will throw NotImplementedException")]
	public static IntPtr CreateHBitmap16Bit(Bitmap bitmap, Color background)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates a Win32 HBITMAP out of the image. </summary>
	/// <returns>An <see cref="T:System.IntPtr" /> representing the handle to the bitmap.</returns>
	/// <param name="bitmap">The <see cref="T:System.Drawing.Bitmap" /> to create.</param>
	/// <param name="monochromeMask">A pointer to the monochrome mask.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	[System.MonoTODO("Not implemented, will throw NotImplementedException")]
	public static IntPtr CreateHBitmapColorMask(Bitmap bitmap, IntPtr monochromeMask)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates a color mask for the specified bitmap that indicates which color should be displayed as transparent.</summary>
	/// <returns>The handle to the <see cref="T:System.Drawing.Bitmap" /> mask.</returns>
	/// <param name="bitmap">The <see cref="T:System.Drawing.Bitmap" /> to create the transparency mask for. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	[System.MonoTODO("Not implemented, will throw NotImplementedException")]
	public static IntPtr CreateHBitmapTransparencyMask(Bitmap bitmap)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates a new light color object for the control from the specified color.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the light color on the control.</returns>
	/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> to be lightened. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Color Light(Color baseColor)
	{
		return Light(baseColor, 0.5f);
	}

	/// <summary>Creates a new light color object for the control from the specified color and lightens it by the specified percentage.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the light color on the control.</returns>
	/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> to be lightened. </param>
	/// <param name="percOfLightLight">The percentage to lighten the specified <see cref="T:System.Drawing.Color" />. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Color Light(Color baseColor, float percOfLightLight)
	{
		if (baseColor.ToArgb() == ThemeEngine.Current.ColorControl.ToArgb())
		{
			if (percOfLightLight <= 0f)
			{
				return ThemeEngine.Current.ColorControlLight;
			}
			if (percOfLightLight == 1f)
			{
				return ThemeEngine.Current.ColorControlLightLight;
			}
			int num = ThemeEngine.Current.ColorControlLightLight.R - ThemeEngine.Current.ColorControlLight.R;
			int num2 = ThemeEngine.Current.ColorControlLightLight.G - ThemeEngine.Current.ColorControlLight.G;
			int num3 = ThemeEngine.Current.ColorControlLightLight.B - ThemeEngine.Current.ColorControlLight.B;
			return Color.FromArgb(ThemeEngine.Current.ColorControlLight.A, (int)((float)(int)ThemeEngine.Current.ColorControlLight.R + (float)num * percOfLightLight), (int)((float)(int)ThemeEngine.Current.ColorControlLight.G + (float)num2 * percOfLightLight), (int)((float)(int)ThemeEngine.Current.ColorControlLight.B + (float)num3 * percOfLightLight));
		}
		Color2HBS(baseColor, out var h, out var l, out var s);
		int lum = Math.Min(255, l + (int)((float)(255 - l) * 0.5f * percOfLightLight));
		return HBS2Color(h, lum, s);
	}

	/// <summary>Creates a new light color object for the control from the specified color.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the light color on the control.</returns>
	/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> to be lightened. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Color LightLight(Color baseColor)
	{
		return Light(baseColor, 1f);
	}

	/// <summary>Creates a new dark color object for the control from the specified color.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the dark color on the control.</returns>
	/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> to be darkened. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Color Dark(Color baseColor)
	{
		return Dark(baseColor, 0.5f);
	}

	/// <summary>Creates a new dark color object for the control from the specified color and darkens it by the specified percentage.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represent the dark color on the control.</returns>
	/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> to be darkened. </param>
	/// <param name="percOfDarkDark">The percentage to darken the specified <see cref="T:System.Drawing.Color" />. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Color Dark(Color baseColor, float percOfDarkDark)
	{
		if (baseColor.ToArgb() == ThemeEngine.Current.ColorControl.ToArgb())
		{
			if (percOfDarkDark <= 0f)
			{
				return ThemeEngine.Current.ColorControlDark;
			}
			if (percOfDarkDark == 1f)
			{
				return ThemeEngine.Current.ColorControlDarkDark;
			}
			int num = ThemeEngine.Current.ColorControlDarkDark.R - ThemeEngine.Current.ColorControlDark.R;
			int num2 = ThemeEngine.Current.ColorControlDarkDark.G - ThemeEngine.Current.ColorControlDark.G;
			int num3 = ThemeEngine.Current.ColorControlDarkDark.B - ThemeEngine.Current.ColorControlDark.B;
			return Color.FromArgb(ThemeEngine.Current.ColorControlDark.A, (int)((float)(int)ThemeEngine.Current.ColorControlDark.R + (float)num * percOfDarkDark), (int)((float)(int)ThemeEngine.Current.ColorControlDark.G + (float)num2 * percOfDarkDark), (int)((float)(int)ThemeEngine.Current.ColorControlDark.B + (float)num3 * percOfDarkDark));
		}
		Color2HBS(baseColor, out var h, out var l, out var s);
		int num4 = Math.Max(0, l - (int)((float)l * 0.333f));
		int lum = Math.Max(0, num4 - (int)((float)num4 * percOfDarkDark));
		return HBS2Color(h, lum, s);
	}

	/// <summary>Creates a new dark color object for the control from the specified color.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the dark color on the control.</returns>
	/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> to be darkened. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Color DarkDark(Color baseColor)
	{
		return Dark(baseColor, 1f);
	}

	/// <summary>Draws a border with the specified style and color, on the specified graphics surface, and within the specified bounds on a button-style control.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the border. </param>
	/// <param name="color">The <see cref="T:System.Drawing.Color" /> of the border. </param>
	/// <param name="style">One of the <see cref="T:System.Windows.Forms.ButtonBorderStyle" /> values that specifies the style of the border. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawBorder(Graphics graphics, Rectangle bounds, Color color, ButtonBorderStyle style)
	{
		int num = 1;
		int num2 = 1;
		if (style == ButtonBorderStyle.Inset)
		{
			num = 2;
		}
		if (style == ButtonBorderStyle.Outset)
		{
			num2 = 2;
			num = 2;
		}
		DrawBorder(graphics, bounds, color, num, style, color, num, style, color, num2, style, color, num2, style);
	}

	internal static void DrawBorder(Graphics graphics, RectangleF bounds, Color color, ButtonBorderStyle style)
	{
		int num = 1;
		int num2 = 1;
		if (style == ButtonBorderStyle.Inset)
		{
			num = 2;
		}
		if (style == ButtonBorderStyle.Outset)
		{
			num2 = 2;
			num = 2;
		}
		ThemeEngine.Current.CPDrawBorder(graphics, bounds, color, num, style, color, num, style, color, num2, style, color, num2, style);
	}

	/// <summary>Draws a border on a button-style control with the specified styles, colors, and border widths; on the specified graphics surface; and within the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the border. </param>
	/// <param name="leftColor">The <see cref="T:System.Drawing.Color" /> of the left of the border. </param>
	/// <param name="leftWidth">The width of the left border. </param>
	/// <param name="leftStyle">One of the <see cref="T:System.Windows.Forms.ButtonBorderStyle" /> values that specifies the style of the left border. </param>
	/// <param name="topColor">The <see cref="T:System.Drawing.Color" /> of the top of the border. </param>
	/// <param name="topWidth">The width of the top border. </param>
	/// <param name="topStyle">One of the <see cref="T:System.Windows.Forms.ButtonBorderStyle" /> values that specifies the style of the top border. </param>
	/// <param name="rightColor">The <see cref="T:System.Drawing.Color" /> of the right of the border. </param>
	/// <param name="rightWidth">The width of the right border. </param>
	/// <param name="rightStyle">One of the <see cref="T:System.Windows.Forms.ButtonBorderStyle" /> values that specifies the style of the right border. </param>
	/// <param name="bottomColor">The <see cref="T:System.Drawing.Color" /> of the bottom of the border. </param>
	/// <param name="bottomWidth">The width of the bottom border. </param>
	/// <param name="bottomStyle">One of the <see cref="T:System.Windows.Forms.ButtonBorderStyle" /> values that specifies the style of the bottom border. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawBorder(Graphics graphics, Rectangle bounds, Color leftColor, int leftWidth, ButtonBorderStyle leftStyle, Color topColor, int topWidth, ButtonBorderStyle topStyle, Color rightColor, int rightWidth, ButtonBorderStyle rightStyle, Color bottomColor, int bottomWidth, ButtonBorderStyle bottomStyle)
	{
		ThemeEngine.Current.CPDrawBorder(graphics, bounds, leftColor, leftWidth, leftStyle, topColor, topWidth, topStyle, rightColor, rightWidth, rightStyle, bottomColor, bottomWidth, bottomStyle);
	}

	/// <summary>Draws a three-dimensional style border on the specified graphics surface and within the specified bounds on a control.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the border. </param>
	/// <filterpriority>1</filterpriority>
	public static void DrawBorder3D(Graphics graphics, Rectangle rectangle)
	{
		DrawBorder3D(graphics, rectangle, Border3DStyle.Etched, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
	}

	/// <summary>Draws a three-dimensional style border with the specified style, on the specified graphics surface, and within the specified bounds on a control.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the border. </param>
	/// <param name="style">One of the <see cref="T:System.Windows.Forms.Border3DStyle" /> values that specifies the style of the border. </param>
	/// <filterpriority>1</filterpriority>
	public static void DrawBorder3D(Graphics graphics, Rectangle rectangle, Border3DStyle style)
	{
		DrawBorder3D(graphics, rectangle, style, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
	}

	/// <summary>Draws a three-dimensional style border on the specified graphics surface and within the specified bounds on a control.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="x">The x-coordinate of the top left of the border rectangle. </param>
	/// <param name="y">The y-coordinate of the top left of the border rectangle. </param>
	/// <param name="width">The width of the border rectangle. </param>
	/// <param name="height">The height of the border rectangle. </param>
	/// <filterpriority>1</filterpriority>
	public static void DrawBorder3D(Graphics graphics, int x, int y, int width, int height)
	{
		DrawBorder3D(graphics, new Rectangle(x, y, width, height), Border3DStyle.Etched, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
	}

	/// <summary>Draws a three-dimensional style border with the specified style, on the specified graphics surface, and within the specified bounds on a control.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="x">The x-coordinate of the top left of the border rectangle. </param>
	/// <param name="y">The y-coordinate of the top left of the border rectangle. </param>
	/// <param name="width">The width of the border rectangle. </param>
	/// <param name="height">The height of the border rectangle. </param>
	/// <param name="style">One of the <see cref="T:System.Windows.Forms.Border3DStyle" /> values that specifies the style of the border. </param>
	/// <filterpriority>1</filterpriority>
	public static void DrawBorder3D(Graphics graphics, int x, int y, int width, int height, Border3DStyle style)
	{
		DrawBorder3D(graphics, new Rectangle(x, y, width, height), style, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
	}

	/// <summary>Draws a three-dimensional style border with the specified style, on the specified graphics surface and side, and within the specified bounds on a control.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="x">The x-coordinate of the top left of the border rectangle. </param>
	/// <param name="y">The y-coordinate of the top left of the border rectangle. </param>
	/// <param name="width">The width of the border rectangle. </param>
	/// <param name="height">The height of the border rectangle. </param>
	/// <param name="style">One of the <see cref="T:System.Windows.Forms.Border3DStyle" /> values that specifies the style of the border. </param>
	/// <param name="sides">The <see cref="T:System.Windows.Forms.Border3DSide" /> of the rectangle to draw the border on. </param>
	/// <filterpriority>1</filterpriority>
	public static void DrawBorder3D(Graphics graphics, int x, int y, int width, int height, Border3DStyle style, Border3DSide sides)
	{
		DrawBorder3D(graphics, new Rectangle(x, y, width, height), style, sides);
	}

	/// <summary>Draws a three-dimensional style border with the specified style, on the specified graphics surface and sides, and within the specified bounds on a control.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the border. </param>
	/// <param name="style">One of the <see cref="T:System.Windows.Forms.Border3DStyle" /> values that specifies the style of the border. </param>
	/// <param name="sides">One of the <see cref="T:System.Windows.Forms.Border3DSide" /> values that specifies the side of the rectangle to draw the border on. </param>
	/// <filterpriority>1</filterpriority>
	public static void DrawBorder3D(Graphics graphics, Rectangle rectangle, Border3DStyle style, Border3DSide sides)
	{
		ThemeEngine.Current.CPDrawBorder3D(graphics, rectangle, style, sides);
	}

	/// <summary>Draws a button control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="x">The x-coordinate of the upper left corner of the drawing rectangle. </param>
	/// <param name="y">The y-coordinate of the upper left corner of the drawing rectangle. </param>
	/// <param name="width">The width of the button. </param>
	/// <param name="height">The height of the button. </param>
	/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the button in. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawButton(Graphics graphics, int x, int y, int width, int height, ButtonState state)
	{
		DrawButton(graphics, new Rectangle(x, y, width, height), state);
	}

	/// <summary>Draws a button control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the button. </param>
	/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the button in. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawButton(Graphics graphics, Rectangle rectangle, ButtonState state)
	{
		ThemeEngine.Current.CPDrawButton(graphics, rectangle, state);
	}

	/// <summary>Draws the specified caption button control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="x">The x-coordinate of the top left of the drawing rectangle. </param>
	/// <param name="y">The y-coordinate of the top left of the drawing rectangle. </param>
	/// <param name="width">The width of the drawing rectangle. </param>
	/// <param name="height">The height of the drawing rectangle. </param>
	/// <param name="button">One of the <see cref="T:System.Windows.Forms.CaptionButton" /> values that specifies the type of caption button to draw. </param>
	/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the button in. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawCaptionButton(Graphics graphics, int x, int y, int width, int height, CaptionButton button, ButtonState state)
	{
		DrawCaptionButton(graphics, new Rectangle(x, y, width, height), button, state);
	}

	/// <summary>Draws the specified caption button control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the caption button. </param>
	/// <param name="button">One of the <see cref="T:System.Windows.Forms.CaptionButton" /> values that specifies the type of caption button to draw. </param>
	/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the button in. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawCaptionButton(Graphics graphics, Rectangle rectangle, CaptionButton button, ButtonState state)
	{
		ThemeEngine.Current.CPDrawCaptionButton(graphics, rectangle, button, state);
	}

	/// <summary>Draws a check box control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="x">The x-coordinate of the upper left corner of the drawing rectangle. </param>
	/// <param name="y">The y-coordinate of the upper left corner of the drawing rectangle. </param>
	/// <param name="width">The width of the check box. </param>
	/// <param name="height">The height of the check box. </param>
	/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the check box in. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawCheckBox(Graphics graphics, int x, int y, int width, int height, ButtonState state)
	{
		DrawCheckBox(graphics, new Rectangle(x, y, width, height), state);
	}

	/// <summary>Draws a check box control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the check box. </param>
	/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the check box in. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawCheckBox(Graphics graphics, Rectangle rectangle, ButtonState state)
	{
		ThemeEngine.Current.CPDrawCheckBox(graphics, rectangle, state);
	}

	/// <summary>Draws a drop-down button on a combo box control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the combo box. </param>
	/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the combo box in. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawComboButton(Graphics graphics, Rectangle rectangle, ButtonState state)
	{
		ThemeEngine.Current.CPDrawComboButton(graphics, rectangle, state);
	}

	/// <summary>Draws a drop-down button on a combo box control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="x">The x-coordinate of the top left of the border rectangle. </param>
	/// <param name="y">The y-coordinate of the top left of the border rectangle. </param>
	/// <param name="width">The width of the combo box. </param>
	/// <param name="height">The height of the combo box. </param>
	/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the combo box in. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawComboButton(Graphics graphics, int x, int y, int width, int height, ButtonState state)
	{
		DrawComboButton(graphics, new Rectangle(x, y, width, height), state);
	}

	/// <summary>Draws a container control grab handle glyph on the specified graphics surface and within the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the grab handle glyph. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawContainerGrabHandle(Graphics graphics, Rectangle bounds)
	{
		ThemeEngine.Current.CPDrawContainerGrabHandle(graphics, bounds);
	}

	/// <summary>Draws a focus rectangle on the specified graphics surface and within the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the grab handle glyph. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawFocusRectangle(Graphics graphics, Rectangle rectangle)
	{
		DrawFocusRectangle(graphics, rectangle, SystemColors.Control, SystemColors.ControlText);
	}

	/// <summary>Draws a focus rectangle on the specified graphics surface and within the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the grab handle glyph. </param>
	/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> that is the foreground color of the object to draw the focus rectangle on. </param>
	/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> that is the background color of the object to draw the focus rectangle on. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawFocusRectangle(Graphics graphics, Rectangle rectangle, Color foreColor, Color backColor)
	{
		ThemeEngine.Current.CPDrawFocusRectangle(graphics, rectangle, foreColor, backColor);
	}

	/// <summary>Draws a standard selection grab handle glyph on the specified graphics surface, within the specified bounds, and in the specified state and style.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the grab handle glyph. </param>
	/// <param name="primary">true to draw the handle as a primary grab handle; otherwise, false. </param>
	/// <param name="enabled">true to draw the handle in an enabled state; otherwise, false. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawGrabHandle(Graphics graphics, Rectangle rectangle, bool primary, bool enabled)
	{
		ThemeEngine.Current.CPDrawGrabHandle(graphics, rectangle, primary, enabled);
	}

	/// <summary>Draws a grid of one-pixel dots with the specified spacing, within the specified bounds, on the specified graphics surface, and in the specified color.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="area">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the grid. </param>
	/// <param name="pixelsBetweenDots">The <see cref="T:System.Drawing.Size" /> that specified the height and width between the dots of the grid. </param>
	/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the background behind the grid. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawGrid(Graphics graphics, Rectangle area, Size pixelsBetweenDots, Color backColor)
	{
		ThemeEngine.Current.CPDrawGrid(graphics, area, pixelsBetweenDots, backColor);
	}

	/// <summary>Draws the specified image in a disabled state.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw. </param>
	/// <param name="x">The x-coordinate of the top left of the border image. </param>
	/// <param name="y">The y-coordinate of the top left of the border image. </param>
	/// <param name="background">The <see cref="T:System.Drawing.Color" /> of the background behind the image. </param>
	/// <filterpriority>1</filterpriority>
	public static void DrawImageDisabled(Graphics graphics, Image image, int x, int y, Color background)
	{
		ThemeEngine.Current.CPDrawImageDisabled(graphics, image, x, y, background);
	}

	/// <summary>Draws a locked selection frame on the screen within the specified bounds and on the specified graphics surface. Specifies whether to draw the frame with the primary selected colors.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the frame. </param>
	/// <param name="primary">true to draw this frame with the colors used for the primary selection; otherwise, false. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawLockedFrame(Graphics graphics, Rectangle rectangle, bool primary)
	{
		ThemeEngine.Current.CPDrawLockedFrame(graphics, rectangle, primary);
	}

	/// <summary>Draws the specified menu glyph on a menu item control within the specified bounds and on the specified surface.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the glyph. </param>
	/// <param name="glyph">One of the <see cref="T:System.Windows.Forms.MenuGlyph" /> values that specifies the image to draw. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawMenuGlyph(Graphics graphics, Rectangle rectangle, MenuGlyph glyph)
	{
		ThemeEngine.Current.CPDrawMenuGlyph(graphics, rectangle, glyph, ThemeEngine.Current.ColorMenuText, Color.Empty);
	}

	/// <summary>Draws the specified menu glyph on a menu item control within the specified bounds and on the specified surface, replacing <see cref="P:System.Drawing.Color.White" /> with the color specified in the <paramref name="backColor" /> parameter and replacing <see cref="P:System.Drawing.Color.Black" /> with the color specified in the <paramref name="foreColor" /> parameter.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on.</param>
	/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the glyph. </param>
	/// <param name="glyph">One of the <see cref="T:System.Windows.Forms.MenuGlyph" /> values that specifies the image to draw. </param>
	/// <param name="foreColor">The color that replaces <see cref="P:System.Drawing.Color.White" /> as the foreground color.</param>
	/// <param name="backColor">The color that replaces <see cref="P:System.Drawing.Color.Black" /> as the background color.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawMenuGlyph(Graphics graphics, Rectangle rectangle, MenuGlyph glyph, Color foreColor, Color backColor)
	{
		ThemeEngine.Current.CPDrawMenuGlyph(graphics, rectangle, glyph, foreColor, backColor);
	}

	/// <summary>Draws the specified menu glyph on a menu item control with the specified bounds and on the specified surface.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="x">The x-coordinate of the upper left corner of the drawing rectangle. </param>
	/// <param name="y">The y-coordinate of the upper left corner of the drawing rectangle. </param>
	/// <param name="width">The width of the menu glyph. </param>
	/// <param name="height">The height of the menu glyph. </param>
	/// <param name="glyph">One of the <see cref="T:System.Windows.Forms.MenuGlyph" /> values that specifies the image to draw. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawMenuGlyph(Graphics graphics, int x, int y, int width, int height, MenuGlyph glyph)
	{
		DrawMenuGlyph(graphics, new Rectangle(x, y, width, height), glyph);
	}

	/// <summary>Draws the specified menu glyph on a menu item control within the specified coordinates, height, and width on the specified surface, replacing <see cref="P:System.Drawing.Color.White" /> with the color specified in the <paramref name="backColor" /> parameter and replacing <see cref="P:System.Drawing.Color.Black" /> with the color specified in the <paramref name="foreColor" /> parameter.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="x">The x-coordinate of the upper left corner of the drawing rectangle.</param>
	/// <param name="y">The y-coordinate of the upper left corner of the drawing rectangle. </param>
	/// <param name="width">The width of the menu glyph.</param>
	/// <param name="height">The height of the menu glyph.</param>
	/// <param name="glyph">One of the <see cref="T:System.Windows.Forms.MenuGlyph" /> values that specifies the image to draw.</param>
	/// <param name="foreColor">The color that replaces <see cref="P:System.Drawing.Color.White" /> as the foreground color.</param>
	/// <param name="backColor">The color that replaces <see cref="P:System.Drawing.Color.Black" /> as the background color.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawMenuGlyph(Graphics graphics, int x, int y, int width, int height, MenuGlyph glyph, Color foreColor, Color backColor)
	{
		DrawMenuGlyph(graphics, new Rectangle(x, y, width, height), glyph, foreColor, backColor);
	}

	/// <summary>Draws a three-state check box control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the check box. </param>
	/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the check box in. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawMixedCheckBox(Graphics graphics, Rectangle rectangle, ButtonState state)
	{
		ThemeEngine.Current.CPDrawMixedCheckBox(graphics, rectangle, state);
	}

	/// <summary>Draws a three-state check box control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="x">The x-coordinate of the upper left corner of the drawing rectangle. </param>
	/// <param name="y">The y-coordinate of the upper left corner of the drawing rectangle. </param>
	/// <param name="width">The width of the check box. </param>
	/// <param name="height">The height of the check box. </param>
	/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the check box in. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawMixedCheckBox(Graphics graphics, int x, int y, int width, int height, ButtonState state)
	{
		DrawMixedCheckBox(graphics, new Rectangle(x, y, width, height), state);
	}

	/// <summary>Draws a radio button control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="x">The x-coordinate of the upper left corner of the drawing rectangle. </param>
	/// <param name="y">The y-coordinate of the upper left corner of the drawing rectangle. </param>
	/// <param name="width">The width of the radio button. </param>
	/// <param name="height">The height of the radio button. </param>
	/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the radio button in. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawRadioButton(Graphics graphics, int x, int y, int width, int height, ButtonState state)
	{
		DrawRadioButton(graphics, new Rectangle(x, y, width, height), state);
	}

	/// <summary>Draws a radio button control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the radio button. </param>
	/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the radio button in. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawRadioButton(Graphics graphics, Rectangle rectangle, ButtonState state)
	{
		ThemeEngine.Current.CPDrawRadioButton(graphics, rectangle, state);
	}

	/// <summary>Draws a reversible frame on the screen within the specified bounds, with the specified background color, and in the specified state.</summary>
	/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the rectangle to draw, in screen coordinates. </param>
	/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the background behind the frame. </param>
	/// <param name="style">One of the <see cref="T:System.Windows.Forms.FrameStyle" /> values that specifies the style of the frame. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
	/// </PermissionSet>
	public static void DrawReversibleFrame(Rectangle rectangle, Color backColor, FrameStyle style)
	{
		XplatUI.DrawReversibleFrame(rectangle, backColor, style);
	}

	/// <summary>Draws a reversible line on the screen within the specified starting and ending points and with the specified background color.</summary>
	/// <param name="start">The starting <see cref="T:System.Drawing.Point" /> of the line, in screen coordinates. </param>
	/// <param name="end">The ending <see cref="T:System.Drawing.Point" /> of the line, in screen coordinates. </param>
	/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the background behind the line. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
	/// </PermissionSet>
	public static void DrawReversibleLine(Point start, Point end, Color backColor)
	{
		XplatUI.DrawReversibleLine(start, end, backColor);
	}

	/// <summary>Draws a filled, reversible rectangle on the screen.</summary>
	/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the rectangle to fill, in screen coordinates. </param>
	/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the background behind the fill. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
	/// </PermissionSet>
	public static void FillReversibleRectangle(Rectangle rectangle, Color backColor)
	{
		XplatUI.FillReversibleRectangle(rectangle, backColor);
	}

	/// <summary>Draws the specified scroll button on a scroll bar control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="x">The x-coordinate of the upper left corner of the drawing rectangle. </param>
	/// <param name="y">The y-coordinate of the upper left corner of the drawing rectangle. </param>
	/// <param name="width">The width of the scroll button. </param>
	/// <param name="height">The height of the scroll button. </param>
	/// <param name="button">One of the <see cref="T:System.Windows.Forms.ScrollButton" /> values that specifies the type of scroll arrow to draw. </param>
	/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the scroll button in. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawScrollButton(Graphics graphics, int x, int y, int width, int height, ScrollButton button, ButtonState state)
	{
		ThemeEngine.Current.CPDrawScrollButton(graphics, new Rectangle(x, y, width, height), button, state);
	}

	/// <summary>Draws the specified scroll button on a scroll bar control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the glyph. </param>
	/// <param name="button">One of the <see cref="T:System.Windows.Forms.ScrollButton" /> values that specifies the type of scroll arrow to draw. </param>
	/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the scroll button in. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawScrollButton(Graphics graphics, Rectangle rectangle, ScrollButton button, ButtonState state)
	{
		ThemeEngine.Current.CPDrawScrollButton(graphics, rectangle, button, state);
	}

	/// <summary>Draws a standard selection frame in the specified state, on the specified graphics surface, with the specified inner and outer dimensions, and with the specified background color.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="active">true to draw the selection frame in an active state; otherwise, false. </param>
	/// <param name="outsideRect">The <see cref="T:System.Drawing.Rectangle" /> that represents the outer boundary of the selection frame. </param>
	/// <param name="insideRect">The <see cref="T:System.Drawing.Rectangle" /> that represents the inner boundary of the selection frame.</param>
	/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the background behind the frame. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawSelectionFrame(Graphics graphics, bool active, Rectangle outsideRect, Rectangle insideRect, Color backColor)
	{
		if (!DSFNotImpl)
		{
			DSFNotImpl = true;
			Console.WriteLine("NOT IMPLEMENTED: DrawSelectionFrame(Graphics graphics, bool active, Rectangle outsideRect, Rectangle insideRect, Color backColor)");
		}
	}

	/// <summary>Draws a size grip on a form with the specified bounds and background color and on the specified graphics surface.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the background used to determine the colors of the size grip.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the size grip.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawSizeGrip(Graphics graphics, Color backColor, Rectangle bounds)
	{
		ThemeEngine.Current.CPDrawSizeGrip(graphics, backColor, bounds);
	}

	/// <summary>Draws a size grip on a form with the specified bounds and background color and on the specified graphics surface.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the background used to determine the colors of the size grip. </param>
	/// <param name="x">The x-coordinate of the upper left corner of the size grip. </param>
	/// <param name="y">The y-coordinate of the upper left corner of the size grip. </param>
	/// <param name="width">The width of the size grip. </param>
	/// <param name="height">The height of the size grip. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawSizeGrip(Graphics graphics, Color backColor, int x, int y, int width, int height)
	{
		DrawSizeGrip(graphics, backColor, new Rectangle(x, y, width, height));
	}

	/// <summary>Draws the specified string in a disabled state on the specified graphics surface; within the specified bounds; and in the specified font, color, and format.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="s">The string to draw. </param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to draw the string with. </param>
	/// <param name="color">The <see cref="T:System.Drawing.Color" /> of the background behind the string. </param>
	/// <param name="layoutRectangle">The <see cref="T:System.Drawing.RectangleF" /> that represents the dimensions of the string. </param>
	/// <param name="format">The <see cref="T:System.Drawing.StringFormat" /> to apply to the string. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DrawStringDisabled(Graphics graphics, string s, Font font, Color color, RectangleF layoutRectangle, StringFormat format)
	{
		ThemeEngine.Current.CPDrawStringDisabled(graphics, s, font, color, layoutRectangle, format);
	}

	/// <summary>Draws the specified string in a disabled state on the specified graphics surface, within the specified bounds, and in the specified font, color, and format, using the specified GDI-based <see cref="T:System.Windows.Forms.TextRenderer" />.</summary>
	/// <param name="dc">The GDI-based <see cref="T:System.Windows.Forms.TextRenderer" />.</param>
	/// <param name="s">The string to draw. </param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to draw the string with.</param>
	/// <param name="color">The <see cref="T:System.Drawing.Color" /> of the background behind the string.</param>
	/// <param name="layoutRectangle">The <see cref="T:System.Drawing.RectangleF" /> that represents the dimensions of the string.</param>
	/// <param name="format">The <see cref="T:System.Drawing.StringFormat" /> to apply to the string.</param>
	public static void DrawStringDisabled(IDeviceContext dc, string s, Font font, Color color, Rectangle layoutRectangle, TextFormatFlags format)
	{
		ThemeEngine.Current.CPDrawStringDisabled(dc, s, font, color, layoutRectangle, format);
	}

	/// <summary>Draws a border in the style appropriate for disabled items.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the border.</param>
	public static void DrawVisualStyleBorder(Graphics graphics, Rectangle bounds)
	{
		ThemeEngine.Current.CPDrawVisualStyleBorder(graphics, bounds);
	}
}
