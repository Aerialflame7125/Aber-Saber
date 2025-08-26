using System.ComponentModel;
using System.Drawing.Design;

namespace System.Drawing;

/// <summary>Represents an ARGB (alpha, red, green, blue) color.</summary>
[Serializable]
[TypeConverter(typeof(ColorConverter))]
[Editor("System.Drawing.Design.ColorEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
public struct Color
{
	[Flags]
	internal enum ColorType : short
	{
		Empty = 0,
		Known = 1,
		ARGB = 2,
		Named = 4,
		System = 8
	}

	private long value;

	internal short state;

	internal short knownColor;

	internal string name;

	/// <summary>Represents a color that is <see langword="null" />.</summary>
	public static readonly Color Empty;

	/// <summary>Gets the name of this <see cref="T:System.Drawing.Color" />.</summary>
	/// <returns>The name of this <see cref="T:System.Drawing.Color" />.</returns>
	public string Name
	{
		get
		{
			if (name == null)
			{
				if (IsNamedColor)
				{
					name = KnownColors.GetName(knownColor);
				}
				else
				{
					name = $"{ToArgb():x}";
				}
			}
			return name;
		}
	}

	/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Color" /> structure is a predefined color. Predefined colors are represented by the elements of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</summary>
	/// <returns>
	///   <see langword="true" /> if this <see cref="T:System.Drawing.Color" /> was created from a predefined color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, <see langword="false" />.</returns>
	public bool IsKnownColor => (state & 1) != 0;

	/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Color" /> structure is a system color. A system color is a color that is used in a Windows display element. System colors are represented by elements of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</summary>
	/// <returns>
	///   <see langword="true" /> if this <see cref="T:System.Drawing.Color" /> was created from a system color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, <see langword="false" />.</returns>
	public bool IsSystemColor => (state & 8) != 0;

	/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Color" /> structure is a named color or a member of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</summary>
	/// <returns>
	///   <see langword="true" /> if this <see cref="T:System.Drawing.Color" /> was created by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, <see langword="false" />.</returns>
	public bool IsNamedColor => (state & 5) != 0;

	internal long Value
	{
		get
		{
			if (value == 0L && IsKnownColor)
			{
				value = FromKnownColor((KnownColor)knownColor).ToArgb() & 0xFFFFFFFFu;
			}
			return value;
		}
		set
		{
			this.value = value;
		}
	}

	/// <summary>Specifies whether this <see cref="T:System.Drawing.Color" /> structure is uninitialized.</summary>
	/// <returns>This property returns <see langword="true" /> if this color is uninitialized; otherwise, <see langword="false" />.</returns>
	public bool IsEmpty => state == 0;

	/// <summary>Gets the alpha component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
	/// <returns>The alpha component value of this <see cref="T:System.Drawing.Color" />.</returns>
	public byte A => (byte)(Value >> 24);

	/// <summary>Gets the red component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
	/// <returns>The red component value of this <see cref="T:System.Drawing.Color" />.</returns>
	public byte R => (byte)(Value >> 16);

	/// <summary>Gets the green component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
	/// <returns>The green component value of this <see cref="T:System.Drawing.Color" />.</returns>
	public byte G => (byte)(Value >> 8);

	/// <summary>Gets the blue component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
	/// <returns>The blue component value of this <see cref="T:System.Drawing.Color" />.</returns>
	public byte B => (byte)Value;

	/// <summary>Gets a system-defined color.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Transparent => FromKnownColor(KnownColor.Transparent);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFF0F8FF.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color AliceBlue => FromKnownColor(KnownColor.AliceBlue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFAEBD7.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color AntiqueWhite => FromKnownColor(KnownColor.AntiqueWhite);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF00FFFF.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Aqua => FromKnownColor(KnownColor.Aqua);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF7FFFD4.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Aquamarine => FromKnownColor(KnownColor.Aquamarine);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFF0FFFF.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Azure => FromKnownColor(KnownColor.Azure);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFF5F5DC.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Beige => FromKnownColor(KnownColor.Beige);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFE4C4.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Bisque => FromKnownColor(KnownColor.Bisque);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF000000.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Black => FromKnownColor(KnownColor.Black);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFEBCD.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color BlanchedAlmond => FromKnownColor(KnownColor.BlanchedAlmond);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF0000FF.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Blue => FromKnownColor(KnownColor.Blue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF8A2BE2.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color BlueViolet => FromKnownColor(KnownColor.BlueViolet);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFA52A2A.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Brown => FromKnownColor(KnownColor.Brown);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFDEB887.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color BurlyWood => FromKnownColor(KnownColor.BurlyWood);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF5F9EA0.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color CadetBlue => FromKnownColor(KnownColor.CadetBlue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF7FFF00.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Chartreuse => FromKnownColor(KnownColor.Chartreuse);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFD2691E.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Chocolate => FromKnownColor(KnownColor.Chocolate);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFF7F50.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Coral => FromKnownColor(KnownColor.Coral);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF6495ED.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color CornflowerBlue => FromKnownColor(KnownColor.CornflowerBlue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFF8DC.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Cornsilk => FromKnownColor(KnownColor.Cornsilk);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFDC143C.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Crimson => FromKnownColor(KnownColor.Crimson);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF00FFFF.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Cyan => FromKnownColor(KnownColor.Cyan);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF00008B.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DarkBlue => FromKnownColor(KnownColor.DarkBlue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF008B8B.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DarkCyan => FromKnownColor(KnownColor.DarkCyan);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFB8860B.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DarkGoldenrod => FromKnownColor(KnownColor.DarkGoldenrod);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFA9A9A9.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DarkGray => FromKnownColor(KnownColor.DarkGray);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF006400.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DarkGreen => FromKnownColor(KnownColor.DarkGreen);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFBDB76B.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DarkKhaki => FromKnownColor(KnownColor.DarkKhaki);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF8B008B.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DarkMagenta => FromKnownColor(KnownColor.DarkMagenta);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF556B2F.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DarkOliveGreen => FromKnownColor(KnownColor.DarkOliveGreen);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFF8C00.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DarkOrange => FromKnownColor(KnownColor.DarkOrange);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF9932CC.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DarkOrchid => FromKnownColor(KnownColor.DarkOrchid);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF8B0000.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DarkRed => FromKnownColor(KnownColor.DarkRed);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFE9967A.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DarkSalmon => FromKnownColor(KnownColor.DarkSalmon);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF8FBC8F.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DarkSeaGreen => FromKnownColor(KnownColor.DarkSeaGreen);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF483D8B.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DarkSlateBlue => FromKnownColor(KnownColor.DarkSlateBlue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF2F4F4F.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DarkSlateGray => FromKnownColor(KnownColor.DarkSlateGray);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF00CED1.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DarkTurquoise => FromKnownColor(KnownColor.DarkTurquoise);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF9400D3.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DarkViolet => FromKnownColor(KnownColor.DarkViolet);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFF1493.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DeepPink => FromKnownColor(KnownColor.DeepPink);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF00BFFF.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DeepSkyBlue => FromKnownColor(KnownColor.DeepSkyBlue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF696969.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DimGray => FromKnownColor(KnownColor.DimGray);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF1E90FF.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color DodgerBlue => FromKnownColor(KnownColor.DodgerBlue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFB22222.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Firebrick => FromKnownColor(KnownColor.Firebrick);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFAF0.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color FloralWhite => FromKnownColor(KnownColor.FloralWhite);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF228B22.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color ForestGreen => FromKnownColor(KnownColor.ForestGreen);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFF00FF.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Fuchsia => FromKnownColor(KnownColor.Fuchsia);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFDCDCDC.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Gainsboro => FromKnownColor(KnownColor.Gainsboro);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFF8F8FF.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color GhostWhite => FromKnownColor(KnownColor.GhostWhite);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFD700.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Gold => FromKnownColor(KnownColor.Gold);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFDAA520.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Goldenrod => FromKnownColor(KnownColor.Goldenrod);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF808080.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> strcture representing a system-defined color.</returns>
	public static Color Gray => FromKnownColor(KnownColor.Gray);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF008000.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Green => FromKnownColor(KnownColor.Green);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFADFF2F.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color GreenYellow => FromKnownColor(KnownColor.GreenYellow);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFF0FFF0.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Honeydew => FromKnownColor(KnownColor.Honeydew);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFF69B4.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color HotPink => FromKnownColor(KnownColor.HotPink);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFCD5C5C.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color IndianRed => FromKnownColor(KnownColor.IndianRed);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF4B0082.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Indigo => FromKnownColor(KnownColor.Indigo);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFFF0.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Ivory => FromKnownColor(KnownColor.Ivory);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFF0E68C.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Khaki => FromKnownColor(KnownColor.Khaki);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFE6E6FA.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Lavender => FromKnownColor(KnownColor.Lavender);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFF0F5.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color LavenderBlush => FromKnownColor(KnownColor.LavenderBlush);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF7CFC00.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color LawnGreen => FromKnownColor(KnownColor.LawnGreen);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFACD.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color LemonChiffon => FromKnownColor(KnownColor.LemonChiffon);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFADD8E6.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color LightBlue => FromKnownColor(KnownColor.LightBlue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFF08080.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color LightCoral => FromKnownColor(KnownColor.LightCoral);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFE0FFFF.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color LightCyan => FromKnownColor(KnownColor.LightCyan);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFAFAD2.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color LightGoldenrodYellow => FromKnownColor(KnownColor.LightGoldenrodYellow);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF90EE90.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color LightGreen => FromKnownColor(KnownColor.LightGreen);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFD3D3D3.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color LightGray => FromKnownColor(KnownColor.LightGray);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFB6C1.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color LightPink => FromKnownColor(KnownColor.LightPink);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFA07A.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color LightSalmon => FromKnownColor(KnownColor.LightSalmon);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF20B2AA.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color LightSeaGreen => FromKnownColor(KnownColor.LightSeaGreen);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF87CEFA.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color LightSkyBlue => FromKnownColor(KnownColor.LightSkyBlue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF778899.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color LightSlateGray => FromKnownColor(KnownColor.LightSlateGray);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFB0C4DE.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color LightSteelBlue => FromKnownColor(KnownColor.LightSteelBlue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFFE0.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color LightYellow => FromKnownColor(KnownColor.LightYellow);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF00FF00.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Lime => FromKnownColor(KnownColor.Lime);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF32CD32.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color LimeGreen => FromKnownColor(KnownColor.LimeGreen);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFAF0E6.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Linen => FromKnownColor(KnownColor.Linen);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFF00FF.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Magenta => FromKnownColor(KnownColor.Magenta);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF800000.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Maroon => FromKnownColor(KnownColor.Maroon);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF66CDAA.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color MediumAquamarine => FromKnownColor(KnownColor.MediumAquamarine);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF0000CD.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color MediumBlue => FromKnownColor(KnownColor.MediumBlue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFBA55D3.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color MediumOrchid => FromKnownColor(KnownColor.MediumOrchid);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF9370DB.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color MediumPurple => FromKnownColor(KnownColor.MediumPurple);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF3CB371.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color MediumSeaGreen => FromKnownColor(KnownColor.MediumSeaGreen);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF7B68EE.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color MediumSlateBlue => FromKnownColor(KnownColor.MediumSlateBlue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF00FA9A.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color MediumSpringGreen => FromKnownColor(KnownColor.MediumSpringGreen);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF48D1CC.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color MediumTurquoise => FromKnownColor(KnownColor.MediumTurquoise);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFC71585.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color MediumVioletRed => FromKnownColor(KnownColor.MediumVioletRed);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF191970.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color MidnightBlue => FromKnownColor(KnownColor.MidnightBlue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFF5FFFA.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color MintCream => FromKnownColor(KnownColor.MintCream);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFE4E1.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color MistyRose => FromKnownColor(KnownColor.MistyRose);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFE4B5.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Moccasin => FromKnownColor(KnownColor.Moccasin);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFDEAD.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color NavajoWhite => FromKnownColor(KnownColor.NavajoWhite);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF000080.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Navy => FromKnownColor(KnownColor.Navy);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFDF5E6.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color OldLace => FromKnownColor(KnownColor.OldLace);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF808000.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Olive => FromKnownColor(KnownColor.Olive);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF6B8E23.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color OliveDrab => FromKnownColor(KnownColor.OliveDrab);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFA500.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Orange => FromKnownColor(KnownColor.Orange);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFF4500.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color OrangeRed => FromKnownColor(KnownColor.OrangeRed);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFDA70D6.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Orchid => FromKnownColor(KnownColor.Orchid);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFEEE8AA.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color PaleGoldenrod => FromKnownColor(KnownColor.PaleGoldenrod);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF98FB98.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color PaleGreen => FromKnownColor(KnownColor.PaleGreen);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFAFEEEE.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color PaleTurquoise => FromKnownColor(KnownColor.PaleTurquoise);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFDB7093.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color PaleVioletRed => FromKnownColor(KnownColor.PaleVioletRed);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFEFD5.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color PapayaWhip => FromKnownColor(KnownColor.PapayaWhip);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFDAB9.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color PeachPuff => FromKnownColor(KnownColor.PeachPuff);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFCD853F.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Peru => FromKnownColor(KnownColor.Peru);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFC0CB.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Pink => FromKnownColor(KnownColor.Pink);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFDDA0DD.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Plum => FromKnownColor(KnownColor.Plum);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFB0E0E6.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color PowderBlue => FromKnownColor(KnownColor.PowderBlue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF800080.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Purple => FromKnownColor(KnownColor.Purple);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFF0000.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Red => FromKnownColor(KnownColor.Red);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFBC8F8F.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color RosyBrown => FromKnownColor(KnownColor.RosyBrown);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF4169E1.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color RoyalBlue => FromKnownColor(KnownColor.RoyalBlue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF8B4513.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color SaddleBrown => FromKnownColor(KnownColor.SaddleBrown);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFA8072.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Salmon => FromKnownColor(KnownColor.Salmon);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFF4A460.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color SandyBrown => FromKnownColor(KnownColor.SandyBrown);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF2E8B57.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color SeaGreen => FromKnownColor(KnownColor.SeaGreen);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFF5EE.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color SeaShell => FromKnownColor(KnownColor.SeaShell);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFA0522D.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Sienna => FromKnownColor(KnownColor.Sienna);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFC0C0C0.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Silver => FromKnownColor(KnownColor.Silver);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF87CEEB.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color SkyBlue => FromKnownColor(KnownColor.SkyBlue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF6A5ACD.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color SlateBlue => FromKnownColor(KnownColor.SlateBlue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF708090.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color SlateGray => FromKnownColor(KnownColor.SlateGray);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFAFA.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Snow => FromKnownColor(KnownColor.Snow);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF00FF7F.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color SpringGreen => FromKnownColor(KnownColor.SpringGreen);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF4682B4.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color SteelBlue => FromKnownColor(KnownColor.SteelBlue);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFD2B48C.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Tan => FromKnownColor(KnownColor.Tan);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF008080.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Teal => FromKnownColor(KnownColor.Teal);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFD8BFD8.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Thistle => FromKnownColor(KnownColor.Thistle);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFF6347.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Tomato => FromKnownColor(KnownColor.Tomato);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF40E0D0.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Turquoise => FromKnownColor(KnownColor.Turquoise);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFEE82EE.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Violet => FromKnownColor(KnownColor.Violet);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFF5DEB3.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Wheat => FromKnownColor(KnownColor.Wheat);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFFFF.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color White => FromKnownColor(KnownColor.White);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFF5F5F5.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color WhiteSmoke => FromKnownColor(KnownColor.WhiteSmoke);

	/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFF00.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color Yellow => FromKnownColor(KnownColor.Yellow);

	/// <summary>Gets a system-defined color that has an ARGB value of #FF9ACD32.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
	public static Color YellowGreen => FromKnownColor(KnownColor.YellowGreen);

	/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified 8-bit color values (red, green, and blue). The alpha value is implicitly 255 (fully opaque). Although this method allows a 32-bit value to be passed for each color component, the value of each component is limited to 8 bits.</summary>
	/// <param name="red">The red component value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255.</param>
	/// <param name="green">The green component value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255.</param>
	/// <param name="blue">The blue component value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255.</param>
	/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="red" />, <paramref name="green" />, or <paramref name="blue" /> is less than 0 or greater than 255.</exception>
	public static Color FromArgb(int red, int green, int blue)
	{
		return FromArgb(255, red, green, blue);
	}

	/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the four ARGB component (alpha, red, green, and blue) values. Although this method allows a 32-bit value to be passed for each component, the value of each component is limited to 8 bits.</summary>
	/// <param name="alpha">The alpha component. Valid values are 0 through 255.</param>
	/// <param name="red">The red component. Valid values are 0 through 255.</param>
	/// <param name="green">The green component. Valid values are 0 through 255.</param>
	/// <param name="blue">The blue component. Valid values are 0 through 255.</param>
	/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="alpha" />, <paramref name="red" />, <paramref name="green" />, or <paramref name="blue" /> is less than 0 or greater than 255.</exception>
	public static Color FromArgb(int alpha, int red, int green, int blue)
	{
		CheckARGBValues(alpha, red, green, blue);
		Color result = default(Color);
		result.state = 2;
		result.Value = (alpha << 24) + (red << 16) + (green << 8) + blue;
		return result;
	}

	/// <summary>Gets the 32-bit ARGB value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
	/// <returns>The 32-bit ARGB value of this <see cref="T:System.Drawing.Color" />.</returns>
	public int ToArgb()
	{
		return (int)Value;
	}

	/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified <see cref="T:System.Drawing.Color" /> structure, but with the new specified alpha value. Although this method allows a 32-bit value to be passed for the alpha value, the value is limited to 8 bits.</summary>
	/// <param name="alpha">The alpha value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255.</param>
	/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> from which to create the new <see cref="T:System.Drawing.Color" />.</param>
	/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="alpha" /> is less than 0 or greater than 255.</exception>
	public static Color FromArgb(int alpha, Color baseColor)
	{
		return FromArgb(alpha, baseColor.R, baseColor.G, baseColor.B);
	}

	/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from a 32-bit ARGB value.</summary>
	/// <param name="argb">A value specifying the 32-bit ARGB value.</param>
	/// <returns>The <see cref="T:System.Drawing.Color" /> structure that this method creates.</returns>
	public static Color FromArgb(int argb)
	{
		return FromArgb((argb >> 24) & 0xFF, (argb >> 16) & 0xFF, (argb >> 8) & 0xFF, argb & 0xFF);
	}

	/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified predefined color.</summary>
	/// <param name="color">An element of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</param>
	/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
	public static Color FromKnownColor(KnownColor color)
	{
		short num = (short)color;
		Color result;
		if (num <= 0 || num >= KnownColors.ArgbValues.Length)
		{
			result = FromArgb(0, 0, 0, 0);
			result.state |= 4;
		}
		else
		{
			result = default(Color);
			result.state = 7;
			if (num < 27 || num > 169)
			{
				result.state |= 8;
			}
			result.Value = KnownColors.ArgbValues[num];
		}
		result.knownColor = num;
		return result;
	}

	/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified name of a predefined color.</summary>
	/// <param name="name">A string that is the name of a predefined color. Valid names are the same as the names of the elements of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</param>
	/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
	public static Color FromName(string name)
	{
		try
		{
			return FromKnownColor((KnownColor)Enum.Parse(typeof(KnownColor), name, ignoreCase: true));
		}
		catch
		{
			Color result = FromArgb(0, 0, 0, 0);
			result.name = name;
			result.state |= 4;
			return result;
		}
	}

	/// <summary>Tests whether two specified <see cref="T:System.Drawing.Color" /> structures are equivalent.</summary>
	/// <param name="left">The <see cref="T:System.Drawing.Color" /> that is to the left of the equality operator.</param>
	/// <param name="right">The <see cref="T:System.Drawing.Color" /> that is to the right of the equality operator.</param>
	/// <returns>
	///   <see langword="true" /> if the two <see cref="T:System.Drawing.Color" /> structures are equal; otherwise, <see langword="false" />.</returns>
	public static bool operator ==(Color left, Color right)
	{
		if (left.Value != right.Value)
		{
			return false;
		}
		if (left.IsNamedColor != right.IsNamedColor)
		{
			return false;
		}
		if (left.IsSystemColor != right.IsSystemColor)
		{
			return false;
		}
		if (left.IsEmpty != right.IsEmpty)
		{
			return false;
		}
		if (left.IsNamedColor && left.Name != right.Name)
		{
			return false;
		}
		return true;
	}

	/// <summary>Tests whether two specified <see cref="T:System.Drawing.Color" /> structures are different.</summary>
	/// <param name="left">The <see cref="T:System.Drawing.Color" /> that is to the left of the inequality operator.</param>
	/// <param name="right">The <see cref="T:System.Drawing.Color" /> that is to the right of the inequality operator.</param>
	/// <returns>
	///   <see langword="true" /> if the two <see cref="T:System.Drawing.Color" /> structures are different; otherwise, <see langword="false" />.</returns>
	public static bool operator !=(Color left, Color right)
	{
		return !(left == right);
	}

	/// <summary>Gets the hue-saturation-lightness (HSL) lightness value for this <see cref="T:System.Drawing.Color" /> structure.</summary>
	/// <returns>The lightness of this <see cref="T:System.Drawing.Color" />. The lightness ranges from 0.0 through 1.0, where 0.0 represents black and 1.0 represents white.</returns>
	public float GetBrightness()
	{
		byte b = Math.Min(R, Math.Min(G, B));
		return (float)(Math.Max(R, Math.Max(G, B)) + b) / 510f;
	}

	/// <summary>Gets the hue-saturation-lightness (HSL) saturation value for this <see cref="T:System.Drawing.Color" /> structure.</summary>
	/// <returns>The saturation of this <see cref="T:System.Drawing.Color" />. The saturation ranges from 0.0 through 1.0, where 0.0 is grayscale and 1.0 is the most saturated.</returns>
	public float GetSaturation()
	{
		byte b = Math.Min(R, Math.Min(G, B));
		byte b2 = Math.Max(R, Math.Max(G, B));
		if (b2 == b)
		{
			return 0f;
		}
		int num = b2 + b;
		if (num > 255)
		{
			num = 510 - num;
		}
		return (float)(b2 - b) / (float)num;
	}

	/// <summary>Gets the hue-saturation-lightness (HSL) hue value, in degrees, for this <see cref="T:System.Drawing.Color" /> structure.</summary>
	/// <returns>The hue, in degrees, of this <see cref="T:System.Drawing.Color" />. The hue is measured in degrees, ranging from 0.0 through 360.0, in HSL color space.</returns>
	public float GetHue()
	{
		int r = R;
		int g = G;
		int b = B;
		byte b2 = (byte)Math.Min(r, Math.Min(g, b));
		byte b3 = (byte)Math.Max(r, Math.Max(g, b));
		if (b3 == b2)
		{
			return 0f;
		}
		float num = b3 - b2;
		float num2 = (float)(b3 - r) / num;
		float num3 = (float)(b3 - g) / num;
		float num4 = (float)(b3 - b) / num;
		float num5 = 0f;
		if (r == b3)
		{
			num5 = 60f * (6f + num4 - num3);
		}
		if (g == b3)
		{
			num5 = 60f * (2f + num2 - num4);
		}
		if (b == b3)
		{
			num5 = 60f * (4f + num3 - num2);
		}
		if (num5 > 360f)
		{
			num5 -= 360f;
		}
		return num5;
	}

	/// <summary>Gets the <see cref="T:System.Drawing.KnownColor" /> value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
	/// <returns>An element of the <see cref="T:System.Drawing.KnownColor" /> enumeration, if the <see cref="T:System.Drawing.Color" /> is created from a predefined color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, 0.</returns>
	public KnownColor ToKnownColor()
	{
		return (KnownColor)knownColor;
	}

	/// <summary>Tests whether the specified object is a <see cref="T:System.Drawing.Color" /> structure and is equivalent to this <see cref="T:System.Drawing.Color" /> structure.</summary>
	/// <param name="obj">The object to test.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.Color" /> structure equivalent to this <see cref="T:System.Drawing.Color" /> structure; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (!(obj is Color color))
		{
			return false;
		}
		return this == color;
	}

	/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Color" /> structure.</summary>
	/// <returns>An integer value that specifies the hash code for this <see cref="T:System.Drawing.Color" />.</returns>
	public override int GetHashCode()
	{
		int num = (int)(Value ^ (Value >> 32) ^ state ^ (knownColor >> 16));
		if (IsNamedColor)
		{
			num ^= Name.GetHashCode();
		}
		return num;
	}

	/// <summary>Converts this <see cref="T:System.Drawing.Color" /> structure to a human-readable string.</summary>
	/// <returns>A string that is the name of this <see cref="T:System.Drawing.Color" />, if the <see cref="T:System.Drawing.Color" /> is created from a predefined color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, a string that consists of the ARGB component names and their values.</returns>
	public override string ToString()
	{
		if (IsEmpty)
		{
			return "Color [Empty]";
		}
		if (IsNamedColor)
		{
			return "Color [" + Name + "]";
		}
		return $"Color [A={A}, R={R}, G={G}, B={B}]";
	}

	private static void CheckRGBValues(int red, int green, int blue)
	{
		if (red > 255 || red < 0)
		{
			throw CreateColorArgumentException(red, "red");
		}
		if (green > 255 || green < 0)
		{
			throw CreateColorArgumentException(green, "green");
		}
		if (blue > 255 || blue < 0)
		{
			throw CreateColorArgumentException(blue, "blue");
		}
	}

	private static ArgumentException CreateColorArgumentException(int value, string color)
	{
		return new ArgumentException(string.Format("'{0}' is not a valid value for '{1}'. '{1}' should be greater or equal to 0 and less than or equal to 255.", value, color));
	}

	private static void CheckARGBValues(int alpha, int red, int green, int blue)
	{
		if (alpha > 255 || alpha < 0)
		{
			throw CreateColorArgumentException(alpha, "alpha");
		}
		CheckRGBValues(red, green, blue);
	}
}
