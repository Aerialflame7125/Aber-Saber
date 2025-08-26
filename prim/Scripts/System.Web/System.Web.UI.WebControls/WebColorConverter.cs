using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web.Util;

namespace System.Web.UI.WebControls;

/// <summary>Converts a predefined color name or an RGB color value to and from a <see cref="T:System.Drawing.Color" /> object.</summary>
public class WebColorConverter : ColorConverter
{
	private static Hashtable htmlSysColorTable;

	/// <summary>Converts the given value to the type of the converter.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context of the object to convert.</param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that represents information about a culture such as language, calendar system, and so on. This parameter is not used in this method. It is reserved for future versions of this method. You can optionally pass in <see langword="null" /> for this parameter.</param>
	/// <param name="value">The object to convert.</param>
	/// <returns>The object resulting from conversion.</returns>
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value is string)
		{
			string text = ((string)value).Trim();
			Color empty = Color.Empty;
			if (string.IsNullOrEmpty(text))
			{
				return empty;
			}
			if (text[0] == '#')
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (StringUtil.EqualsIgnoreCase(text, "LightGrey"))
			{
				return Color.LightGray;
			}
			if (htmlSysColorTable == null)
			{
				InitializeHTMLSysColorTable();
			}
			object obj = htmlSysColorTable[text];
			if (obj != null)
			{
				return (Color)obj;
			}
		}
		return base.ConvertFrom(context, culture, value);
	}

	/// <summary>Converts the specified object to a specified type.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> instance that indicates the context of the object to convert.</param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that represents information about a culture such as language, calendar system, and so on. This parameter is not used in this method. It is reserved for future versions of this method. You can optionally pass in <see langword="null" /> for this parameter.</param>
	/// <param name="value">The object to convert.</param>
	/// <param name="destinationType">The type to convert to.</param>
	/// <returns>The object resulting from conversion.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="destinationType" /> is <see langword="null" />.</exception>
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if (destinationType == null)
		{
			throw new ArgumentNullException("destinationType");
		}
		if (destinationType == typeof(string) && value != null)
		{
			Color color = (Color)value;
			if (color == Color.Empty)
			{
				return string.Empty;
			}
			if (!color.IsKnownColor)
			{
				StringBuilder stringBuilder = new StringBuilder("#", 7);
				stringBuilder.Append(color.R.ToString("X2", CultureInfo.InvariantCulture));
				stringBuilder.Append(color.G.ToString("X2", CultureInfo.InvariantCulture));
				stringBuilder.Append(color.B.ToString("X2", CultureInfo.InvariantCulture));
				return stringBuilder.ToString();
			}
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}

	private static void InitializeHTMLSysColorTable()
	{
		htmlSysColorTable = new Hashtable(StringComparer.OrdinalIgnoreCase)
		{
			["activeborder"] = Color.FromKnownColor(KnownColor.ActiveBorder),
			["activecaption"] = Color.FromKnownColor(KnownColor.ActiveCaption),
			["appworkspace"] = Color.FromKnownColor(KnownColor.AppWorkspace),
			["background"] = Color.FromKnownColor(KnownColor.Desktop),
			["buttonface"] = Color.FromKnownColor(KnownColor.Control),
			["buttonhighlight"] = Color.FromKnownColor(KnownColor.ControlLightLight),
			["buttonshadow"] = Color.FromKnownColor(KnownColor.ControlDark),
			["buttontext"] = Color.FromKnownColor(KnownColor.ControlText),
			["captiontext"] = Color.FromKnownColor(KnownColor.ActiveCaptionText),
			["graytext"] = Color.FromKnownColor(KnownColor.GrayText),
			["highlight"] = Color.FromKnownColor(KnownColor.Highlight),
			["highlighttext"] = Color.FromKnownColor(KnownColor.HighlightText),
			["inactiveborder"] = Color.FromKnownColor(KnownColor.InactiveBorder),
			["inactivecaption"] = Color.FromKnownColor(KnownColor.InactiveCaption),
			["inactivecaptiontext"] = Color.FromKnownColor(KnownColor.InactiveCaptionText),
			["infobackground"] = Color.FromKnownColor(KnownColor.Info),
			["infotext"] = Color.FromKnownColor(KnownColor.InfoText),
			["menu"] = Color.FromKnownColor(KnownColor.Menu),
			["menutext"] = Color.FromKnownColor(KnownColor.MenuText),
			["scrollbar"] = Color.FromKnownColor(KnownColor.ScrollBar),
			["threeddarkshadow"] = Color.FromKnownColor(KnownColor.ControlDarkDark),
			["threedface"] = Color.FromKnownColor(KnownColor.Control),
			["threedhighlight"] = Color.FromKnownColor(KnownColor.ControlLight),
			["threedlightshadow"] = Color.FromKnownColor(KnownColor.ControlLightLight),
			["window"] = Color.FromKnownColor(KnownColor.Window),
			["windowframe"] = Color.FromKnownColor(KnownColor.WindowFrame),
			["windowtext"] = Color.FromKnownColor(KnownColor.WindowText)
		};
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebColorConverter" /> class. </summary>
	public WebColorConverter()
	{
	}
}
