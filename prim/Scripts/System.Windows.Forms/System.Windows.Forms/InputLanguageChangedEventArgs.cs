using System.Globalization;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Form.InputLanguageChanged" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class InputLanguageChangedEventArgs : EventArgs
{
	private CultureInfo culture;

	private byte charset;

	private InputLanguage input_language;

	/// <summary>Gets the character set associated with the new input language.</summary>
	/// <returns>An 8-bit unsigned integer that corresponds to the character set, as shown in the following table.Character Set Value ANSI_CHARSET 0 DEFAULT_CHARSET 1 SYMBOL_CHARSET 2 MAC_CHARSET 77 SHIFTJI_CHARSET 128 HANGEUL_CHARSET 129 HANGUL_CHARSET 129 JOHAB_CHARSET 130 GB2312_CHARSET 134 CHINESEBIG5_CHARSET 136 GREEK_CHARSET 161 TURKISH_CHARSET 162 VIETNAMESE_CHARSET 163 HEBREW_CHARSET 177 ARABIC_CHARSET 178 BALTIC_CHARSET 186 RUSSIAN_CHARSET 204 THAI_CHARSET 222 EASTEUROPE_CHARSET 238 OEM_CHARSET 255 </returns>
	/// <filterpriority>1</filterpriority>
	public byte CharSet => charset;

	/// <summary>Gets the locale of the input language.</summary>
	/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> that specifies the locale of the input language.</returns>
	/// <filterpriority>1</filterpriority>
	public CultureInfo Culture => culture;

	/// <summary>Gets a value indicating the input language.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.InputLanguage" />.</returns>
	/// <filterpriority>1</filterpriority>
	public InputLanguage InputLanguage => input_language;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.InputLanguageChangedEventArgs" /> class with the specified locale and character set.</summary>
	/// <param name="culture">The locale of the input language. </param>
	/// <param name="charSet">The character set associated with the new input language. </param>
	public InputLanguageChangedEventArgs(CultureInfo culture, byte charSet)
	{
		this.culture = culture;
		charset = charSet;
		input_language = InputLanguage.FromCulture(culture);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.InputLanguageChangedEventArgs" /> class with the specified input language and character set.</summary>
	/// <param name="inputLanguage">The input language. </param>
	/// <param name="charSet">The character set associated with the new input language. </param>
	public InputLanguageChangedEventArgs(InputLanguage inputLanguage, byte charSet)
	{
		culture = inputLanguage.Culture;
		charset = charSet;
		input_language = inputLanguage;
	}
}
