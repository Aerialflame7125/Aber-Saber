using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides methods used to measure and render text. This class cannot be inherited. </summary>
public sealed class TextRenderer
{
	private TextRenderer()
	{
	}

	/// <summary>Draws the specified text at the specified location using the specified device context, font, and color.</summary>
	/// <param name="dc">The device context in which to draw the text.</param>
	/// <param name="text">The text to draw.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
	/// <param name="pt">The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the drawn text.</param>
	/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor)
	{
		DrawTextInternal(dc, text, font, pt, foreColor, Color.Transparent, TextFormatFlags.Left, useDrawString: false);
	}

	/// <summary>Draws the specified text within the specified bounds, using the specified device context, font, and color.</summary>
	/// <param name="dc">The device context in which to draw the text.</param>
	/// <param name="text">The text to draw.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the text.</param>
	/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor)
	{
		DrawTextInternal(dc, text, font, bounds, foreColor, Color.Transparent, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter, useDrawString: false);
	}

	/// <summary>Draws the specified text at the specified location, using the specified device context, font, color, and back color.</summary>
	/// <param name="dc">The device context in which to draw the text.</param>
	/// <param name="text">The text to draw.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
	/// <param name="pt">The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the drawn text.</param>
	/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
	/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> to apply to the background area of the drawn text.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, Color backColor)
	{
		DrawTextInternal(dc, text, font, pt, foreColor, backColor, TextFormatFlags.Left, useDrawString: false);
	}

	/// <summary>Draws the specified text at the specified location using the specified device context, font, color, and formatting instructions. </summary>
	/// <param name="dc">The device context in which to draw the text.</param>
	/// <param name="text">The text to draw.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
	/// <param name="pt">The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the drawn text. </param>
	/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
	/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, TextFormatFlags flags)
	{
		DrawTextInternal(dc, text, font, pt, foreColor, Color.Transparent, flags, useDrawString: false);
	}

	/// <summary>Draws the specified text within the specified bounds using the specified device context, font, color, and back color.</summary>
	/// <param name="dc">The device context in which to draw the text.</param>
	/// <param name="text">The text to draw.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the text.</param>
	/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
	/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> to apply to the area represented by <paramref name="bounds" />. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, Color backColor)
	{
		DrawTextInternal(dc, text, font, bounds, foreColor, backColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter, useDrawString: false);
	}

	/// <summary>Draws the specified text within the specified bounds using the specified device context, font, color, and formatting instructions.</summary>
	/// <param name="dc">The device context in which to draw the text.</param>
	/// <param name="text">The text to draw.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the text.</param>
	/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
	/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, TextFormatFlags flags)
	{
		DrawTextInternal(dc, text, font, bounds, foreColor, Color.Transparent, flags, useDrawString: false);
	}

	/// <summary>Draws the specified text at the specified location using the specified device context, font, color, back color, and formatting instructions </summary>
	/// <param name="dc">The device context in which to draw the text.</param>
	/// <param name="text">The text to draw.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
	/// <param name="pt">The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the drawn text.</param>
	/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the text.</param>
	/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> to apply to the background area of the drawn text.</param>
	/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, Color backColor, TextFormatFlags flags)
	{
		DrawTextInternal(dc, text, font, pt, foreColor, backColor, flags, useDrawString: false);
	}

	/// <summary>Draws the specified text within the specified bounds using the specified device context, font, color, back color, and formatting instructions.</summary>
	/// <param name="dc">The device context in which to draw the text.</param>
	/// <param name="text">The text to draw.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the text.</param>
	/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the text.</param>
	/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> to apply to the area represented by <paramref name="bounds" />.</param>
	/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, Color backColor, TextFormatFlags flags)
	{
		DrawTextInternal(dc, text, font, bounds, foreColor, backColor, flags, useDrawString: false);
	}

	/// <summary>Provides the size, in pixels, of the specified text when drawn with the specified font.</summary>
	/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn on a single line with the specified <paramref name="font" />. You can manipulate how the text is drawn by using one of the <see cref="M:System.Windows.Forms.TextRenderer.DrawText(System.Drawing.IDeviceContext,System.String,System.Drawing.Font,System.Drawing.Rectangle,System.Drawing.Color,System.Windows.Forms.TextFormatFlags)" /> overloads that takes a <see cref="T:System.Windows.Forms.TextFormatFlags" /> parameter. For example, the default behavior of the <see cref="T:System.Windows.Forms.TextRenderer" /> is to add padding to the bounding rectangle of the drawn text to accommodate overhanging glyphs. If you need to draw a line of text without these extra spaces you should use the versions of <see cref="M:System.Windows.Forms.TextRenderer.DrawText(System.Drawing.IDeviceContext,System.String,System.Drawing.Font,System.Drawing.Point,System.Drawing.Color)" /> and <see cref="M:System.Windows.Forms.TextRenderer.MeasureText(System.Drawing.IDeviceContext,System.String,System.Drawing.Font)" /> that take a <see cref="T:System.Drawing.Size" /> and <see cref="T:System.Windows.Forms.TextFormatFlags" /> parameter. For an example, see <see cref="M:System.Windows.Forms.TextRenderer.MeasureText(System.Drawing.IDeviceContext,System.String,System.Drawing.Font,System.Drawing.Size,System.Windows.Forms.TextFormatFlags)" />.</returns>
	/// <param name="text">The text to measure.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
	public static Size MeasureText(string text, Font font)
	{
		return MeasureTextInternal(Hwnd.GraphicsContext, text, font, Size.Empty, TextFormatFlags.Left, useMeasureString: false);
	}

	/// <summary>Provides the size, in pixels, of the specified text drawn with the specified font in the specified device context.</summary>
	/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn in a single line with the specified <paramref name="font" /> in the specified device context.</returns>
	/// <param name="dc">The device context in which to measure the text.</param>
	/// <param name="text">The text to measure.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
	public static Size MeasureText(IDeviceContext dc, string text, Font font)
	{
		return MeasureTextInternal(dc, text, font, Size.Empty, TextFormatFlags.Left, useMeasureString: false);
	}

	/// <summary>Provides the size, in pixels, of the specified text when drawn with the specified font, using the specified size to create an initial bounding rectangle.</summary>
	/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn with the specified <paramref name="font" />.</returns>
	/// <param name="text">The text to measure.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
	/// <param name="proposedSize">The <see cref="T:System.Drawing.Size" /> of the initial bounding rectangle.</param>
	public static Size MeasureText(string text, Font font, Size proposedSize)
	{
		return MeasureTextInternal(Hwnd.GraphicsContext, text, font, proposedSize, TextFormatFlags.Left, useMeasureString: false);
	}

	/// <summary>Provides the size, in pixels, of the specified text when drawn with the specified font in the specified device context, using the specified size to create an initial bounding rectangle for the text.</summary>
	/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn with the specified <paramref name="font" />.</returns>
	/// <param name="dc">The device context in which to measure the text.</param>
	/// <param name="text">The text to measure.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
	/// <param name="proposedSize">The <see cref="T:System.Drawing.Size" /> of the initial bounding rectangle.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public static Size MeasureText(IDeviceContext dc, string text, Font font, Size proposedSize)
	{
		return MeasureTextInternal(dc, text, font, proposedSize, TextFormatFlags.Left, useMeasureString: false);
	}

	/// <summary>Provides the size, in pixels, of the specified text when drawn with the specified font and formatting instructions, using the specified size to create the initial bounding rectangle for the text.</summary>
	/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn with the specified <paramref name="font" /> and format.</returns>
	/// <param name="text">The text to measure.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
	/// <param name="proposedSize">The <see cref="T:System.Drawing.Size" /> of the initial bounding rectangle.</param>
	/// <param name="flags">The formatting instructions to apply to the measured text.</param>
	public static Size MeasureText(string text, Font font, Size proposedSize, TextFormatFlags flags)
	{
		return MeasureTextInternal(Hwnd.GraphicsContext, text, font, proposedSize, flags, useMeasureString: false);
	}

	/// <summary>Provides the size, in pixels, of the specified text when drawn with the specified device context, font, and formatting instructions, using the specified size to create the initial bounding rectangle for the text.</summary>
	/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn with the specified <paramref name="font" /> and format.</returns>
	/// <param name="dc">The device context in which to measure the text.</param>
	/// <param name="text">The text to measure.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
	/// <param name="proposedSize">The <see cref="T:System.Drawing.Size" /> of the initial bounding rectangle.</param>
	/// <param name="flags">The formatting instructions to apply to the measured text.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public static Size MeasureText(IDeviceContext dc, string text, Font font, Size proposedSize, TextFormatFlags flags)
	{
		return MeasureTextInternal(dc, text, font, proposedSize, flags, useMeasureString: false);
	}

	internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, Color backColor, TextFormatFlags flags, bool useDrawString)
	{
		if (dc == null)
		{
			throw new ArgumentNullException("dc");
		}
		if (text == null || text.Length == 0)
		{
			return;
		}
		if (!useDrawString && !XplatUI.RunningOnUnix)
		{
			if ((flags & TextFormatFlags.VerticalCenter) == TextFormatFlags.VerticalCenter || (flags & TextFormatFlags.Bottom) == TextFormatFlags.Bottom)
			{
				flags |= TextFormatFlags.SingleLine;
			}
			Rectangle rectangle = PadRectangle(bounds, flags);
			rectangle.Offset((int)(dc as Graphics).Transform.OffsetX, (int)(dc as Graphics).Transform.OffsetY);
			IntPtr intPtr = IntPtr.Zero;
			bool flag = false;
			if ((flags & TextFormatFlags.PreserveGraphicsClipping) == TextFormatFlags.PreserveGraphicsClipping)
			{
				Graphics graphics = (Graphics)dc;
				Region clip = graphics.Clip;
				if (!clip.IsInfinite(graphics))
				{
					IntPtr hrgn = clip.GetHrgn(graphics);
					intPtr = dc.GetHdc();
					SelectClipRgn(intPtr, hrgn);
					DeleteObject(hrgn);
					flag = true;
				}
			}
			if (intPtr == IntPtr.Zero)
			{
				intPtr = dc.GetHdc();
			}
			if (foreColor != Color.Empty)
			{
				SetTextColor(intPtr, ColorTranslator.ToWin32(foreColor));
			}
			if (backColor != Color.Transparent && backColor != Color.Empty)
			{
				SetBkMode(intPtr, 2);
				SetBkColor(intPtr, ColorTranslator.ToWin32(backColor));
			}
			else
			{
				SetBkMode(intPtr, 1);
			}
			XplatUIWin32.RECT lpRect = XplatUIWin32.RECT.FromRectangle(rectangle);
			if (font != null)
			{
				IntPtr hObject = SelectObject(intPtr, font.ToHfont());
				Win32DrawText(intPtr, text, text.Length, ref lpRect, (int)flags);
				hObject = SelectObject(intPtr, hObject);
				DeleteObject(hObject);
			}
			else
			{
				Win32DrawText(intPtr, text, text.Length, ref lpRect, (int)flags);
			}
			if (flag)
			{
				SelectClipRgn(intPtr, IntPtr.Zero);
			}
			dc.ReleaseHdc();
		}
		else
		{
			IntPtr zero = IntPtr.Zero;
			Graphics graphics2;
			if (dc is Graphics)
			{
				graphics2 = (Graphics)dc;
			}
			else
			{
				zero = dc.GetHdc();
				graphics2 = Graphics.FromHdc(zero);
			}
			StringFormat format = FlagsToStringFormat(flags);
			Rectangle rectangle2 = PadDrawStringRectangle(bounds, flags);
			graphics2.DrawString(text, font, ThemeEngine.Current.ResPool.GetSolidBrush(foreColor), rectangle2, format);
			if (!(dc is Graphics))
			{
				graphics2.Dispose();
				dc.ReleaseHdc();
			}
		}
	}

	internal static Size MeasureTextInternal(IDeviceContext dc, string text, Font font, Size proposedSize, TextFormatFlags flags, bool useMeasureString)
	{
		if (!useMeasureString && !XplatUI.RunningOnUnix)
		{
			flags |= (TextFormatFlags)1024;
			IntPtr hdc = dc.GetHdc();
			XplatUIWin32.RECT lpRect = XplatUIWin32.RECT.FromRectangle(new Rectangle(Point.Empty, proposedSize));
			if (font != null)
			{
				IntPtr hObject = SelectObject(hdc, font.ToHfont());
				Win32DrawText(hdc, text, text.Length, ref lpRect, (int)flags);
				hObject = SelectObject(hdc, hObject);
				DeleteObject(hObject);
			}
			else
			{
				Win32DrawText(hdc, text, text.Length, ref lpRect, (int)flags);
			}
			dc.ReleaseHdc();
			Size size = lpRect.ToRectangle().Size;
			if (size.Width > 0 && (flags & TextFormatFlags.NoPadding) == 0)
			{
				size.Width += 6;
				size.Width += size.Height / 8;
			}
			return size;
		}
		StringFormat format = FlagsToStringFormat(flags);
		Size result = ((!(dc is Graphics)) ? MeasureString(text, font, (proposedSize.Width != 0) ? proposedSize.Width : int.MaxValue, format).ToSize() : (dc as Graphics).MeasureString(text, font, (proposedSize.Width != 0) ? proposedSize.Width : int.MaxValue, format).ToSize());
		if (result.Width > 0 && (flags & TextFormatFlags.NoPadding) == 0)
		{
			result.Width += 9;
		}
		return result;
	}

	internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, bool useDrawString)
	{
		DrawTextInternal(dc, text, font, pt, foreColor, Color.Transparent, TextFormatFlags.Left, useDrawString);
	}

	internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, bool useDrawString)
	{
		DrawTextInternal(dc, text, font, bounds, foreColor, Color.Transparent, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter, useDrawString);
	}

	internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, Color backColor, bool useDrawString)
	{
		DrawTextInternal(dc, text, font, pt, foreColor, backColor, TextFormatFlags.Left, useDrawString);
	}

	internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, TextFormatFlags flags, bool useDrawString)
	{
		DrawTextInternal(dc, text, font, pt, foreColor, Color.Transparent, flags, useDrawString);
	}

	internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, Color backColor, bool useDrawString)
	{
		DrawTextInternal(dc, text, font, bounds, foreColor, backColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter, useDrawString);
	}

	internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, TextFormatFlags flags, bool useDrawString)
	{
		DrawTextInternal(dc, text, font, bounds, foreColor, Color.Transparent, flags, useDrawString);
	}

	internal static Size MeasureTextInternal(string text, Font font, bool useMeasureString)
	{
		return MeasureTextInternal(Hwnd.GraphicsContext, text, font, Size.Empty, TextFormatFlags.Left, useMeasureString);
	}

	internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, Color backColor, TextFormatFlags flags, bool useDrawString)
	{
		Size size = MeasureTextInternal(dc, text, font, useDrawString);
		DrawTextInternal(dc, text, font, new Rectangle(pt, size), foreColor, backColor, flags, useDrawString);
	}

	internal static Size MeasureTextInternal(IDeviceContext dc, string text, Font font, bool useMeasureString)
	{
		return MeasureTextInternal(dc, text, font, Size.Empty, TextFormatFlags.Left, useMeasureString);
	}

	internal static Size MeasureTextInternal(string text, Font font, Size proposedSize, bool useMeasureString)
	{
		return MeasureTextInternal(Hwnd.GraphicsContext, text, font, proposedSize, TextFormatFlags.Left, useMeasureString);
	}

	internal static Size MeasureTextInternal(IDeviceContext dc, string text, Font font, Size proposedSize, bool useMeasureString)
	{
		return MeasureTextInternal(dc, text, font, proposedSize, TextFormatFlags.Left, useMeasureString);
	}

	internal static Size MeasureTextInternal(string text, Font font, Size proposedSize, TextFormatFlags flags, bool useMeasureString)
	{
		return MeasureTextInternal(Hwnd.GraphicsContext, text, font, proposedSize, flags, useMeasureString);
	}

	internal static SizeF MeasureString(string text, Font font)
	{
		return Hwnd.GraphicsContext.MeasureString(text, font);
	}

	internal static SizeF MeasureString(string text, Font font, int width)
	{
		return Hwnd.GraphicsContext.MeasureString(text, font, width);
	}

	internal static SizeF MeasureString(string text, Font font, SizeF layoutArea)
	{
		return Hwnd.GraphicsContext.MeasureString(text, font, layoutArea);
	}

	internal static SizeF MeasureString(string text, Font font, int width, StringFormat format)
	{
		return Hwnd.GraphicsContext.MeasureString(text, font, width, format);
	}

	internal static SizeF MeasureString(string text, Font font, PointF origin, StringFormat stringFormat)
	{
		return Hwnd.GraphicsContext.MeasureString(text, font, origin, stringFormat);
	}

	internal static SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat)
	{
		return Hwnd.GraphicsContext.MeasureString(text, font, layoutArea, stringFormat);
	}

	internal static SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat, out int charactersFitted, out int linesFilled)
	{
		return Hwnd.GraphicsContext.MeasureString(text, font, layoutArea, stringFormat, out charactersFitted, out linesFilled);
	}

	internal static Region[] MeasureCharacterRanges(string text, Font font, RectangleF layoutRect, StringFormat stringFormat)
	{
		return Hwnd.GraphicsContext.MeasureCharacterRanges(text, font, layoutRect, stringFormat);
	}

	internal static SizeF GetDpi()
	{
		return new SizeF(Hwnd.GraphicsContext.DpiX, Hwnd.GraphicsContext.DpiY);
	}

	private static StringFormat FlagsToStringFormat(TextFormatFlags flags)
	{
		StringFormat stringFormat = new StringFormat();
		if ((flags & TextFormatFlags.HorizontalCenter) == TextFormatFlags.HorizontalCenter)
		{
			stringFormat.Alignment = StringAlignment.Center;
		}
		else if ((flags & TextFormatFlags.Right) == TextFormatFlags.Right)
		{
			stringFormat.Alignment = StringAlignment.Far;
		}
		else
		{
			stringFormat.Alignment = StringAlignment.Near;
		}
		if ((flags & TextFormatFlags.Bottom) == TextFormatFlags.Bottom)
		{
			stringFormat.LineAlignment = StringAlignment.Far;
		}
		else if ((flags & TextFormatFlags.VerticalCenter) == TextFormatFlags.VerticalCenter)
		{
			stringFormat.LineAlignment = StringAlignment.Center;
		}
		else
		{
			stringFormat.LineAlignment = StringAlignment.Near;
		}
		if ((flags & TextFormatFlags.EndEllipsis) == TextFormatFlags.EndEllipsis)
		{
			stringFormat.Trimming = StringTrimming.EllipsisCharacter;
		}
		else if ((flags & TextFormatFlags.PathEllipsis) == TextFormatFlags.PathEllipsis)
		{
			stringFormat.Trimming = StringTrimming.EllipsisPath;
		}
		else if ((flags & TextFormatFlags.WordEllipsis) == TextFormatFlags.WordEllipsis)
		{
			stringFormat.Trimming = StringTrimming.EllipsisWord;
		}
		else
		{
			stringFormat.Trimming = StringTrimming.Character;
		}
		if ((flags & TextFormatFlags.NoPrefix) == TextFormatFlags.NoPrefix)
		{
			stringFormat.HotkeyPrefix = HotkeyPrefix.None;
		}
		else if ((flags & TextFormatFlags.HidePrefix) == TextFormatFlags.HidePrefix)
		{
			stringFormat.HotkeyPrefix = HotkeyPrefix.Hide;
		}
		else
		{
			stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
		}
		if ((flags & TextFormatFlags.NoPadding) == TextFormatFlags.NoPadding)
		{
			stringFormat.FormatFlags |= StringFormatFlags.FitBlackBox;
		}
		if ((flags & TextFormatFlags.SingleLine) == TextFormatFlags.SingleLine)
		{
			stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
		}
		else if ((flags & TextFormatFlags.TextBoxControl) == TextFormatFlags.TextBoxControl)
		{
			stringFormat.FormatFlags |= StringFormatFlags.LineLimit;
		}
		if ((flags & TextFormatFlags.NoClipping) == TextFormatFlags.NoClipping)
		{
			stringFormat.FormatFlags |= StringFormatFlags.NoClip;
		}
		return stringFormat;
	}

	private static Rectangle PadRectangle(Rectangle r, TextFormatFlags flags)
	{
		if ((flags & TextFormatFlags.NoPadding) == 0 && (flags & TextFormatFlags.Right) == 0 && (flags & TextFormatFlags.HorizontalCenter) == 0)
		{
			r.X += 3;
			r.Width -= 3;
		}
		if ((flags & TextFormatFlags.NoPadding) == 0 && (flags & TextFormatFlags.Right) == TextFormatFlags.Right)
		{
			r.Width -= 4;
		}
		if ((flags & TextFormatFlags.LeftAndRightPadding) == TextFormatFlags.LeftAndRightPadding)
		{
			r.X += 2;
			r.Width -= 2;
		}
		if ((flags & TextFormatFlags.WordEllipsis) == TextFormatFlags.WordEllipsis || (flags & TextFormatFlags.EndEllipsis) == TextFormatFlags.EndEllipsis || (flags & TextFormatFlags.WordBreak) == TextFormatFlags.WordBreak)
		{
			r.Width -= 4;
		}
		if ((flags & TextFormatFlags.VerticalCenter) == TextFormatFlags.VerticalCenter)
		{
			r.Y++;
		}
		return r;
	}

	private static Rectangle PadDrawStringRectangle(Rectangle r, TextFormatFlags flags)
	{
		if ((flags & TextFormatFlags.NoPadding) == 0 && (flags & TextFormatFlags.Right) == 0 && (flags & TextFormatFlags.HorizontalCenter) == 0)
		{
			r.X++;
			r.Width--;
		}
		if ((flags & TextFormatFlags.NoPadding) == 0 && (flags & TextFormatFlags.Right) == TextFormatFlags.Right)
		{
			r.Width -= 4;
		}
		if ((flags & TextFormatFlags.NoPadding) == TextFormatFlags.NoPadding)
		{
			r.X -= 2;
		}
		if ((flags & TextFormatFlags.NoPadding) == 0 && (flags & TextFormatFlags.Bottom) == TextFormatFlags.Bottom)
		{
			r.Y++;
		}
		if ((flags & TextFormatFlags.LeftAndRightPadding) == TextFormatFlags.LeftAndRightPadding)
		{
			r.X += 2;
			r.Width -= 2;
		}
		if ((flags & TextFormatFlags.WordEllipsis) == TextFormatFlags.WordEllipsis || (flags & TextFormatFlags.EndEllipsis) == TextFormatFlags.EndEllipsis || (flags & TextFormatFlags.WordBreak) == TextFormatFlags.WordBreak)
		{
			r.Width -= 4;
		}
		if ((flags & TextFormatFlags.VerticalCenter) == TextFormatFlags.VerticalCenter)
		{
			r.Y++;
		}
		return r;
	}

	[DllImport("user32", CharSet = CharSet.Unicode, EntryPoint = "DrawText")]
	private static extern int Win32DrawText(IntPtr hdc, string lpStr, int nCount, ref XplatUIWin32.RECT lpRect, int wFormat);

	[DllImport("gdi32")]
	private static extern int SetTextColor(IntPtr hdc, int crColor);

	[DllImport("gdi32")]
	private static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

	[DllImport("gdi32")]
	private static extern int SetBkColor(IntPtr hdc, int crColor);

	[DllImport("gdi32")]
	private static extern int SetBkMode(IntPtr hdc, int iBkMode);

	[DllImport("gdi32")]
	private static extern bool DeleteObject(IntPtr objectHandle);

	[DllImport("gdi32")]
	private static extern bool SelectClipRgn(IntPtr hdc, IntPtr hrgn);
}
