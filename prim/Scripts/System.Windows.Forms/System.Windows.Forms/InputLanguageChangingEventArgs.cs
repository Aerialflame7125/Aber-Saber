using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Form.InputLanguageChanging" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class InputLanguageChangingEventArgs : CancelEventArgs
{
	private CultureInfo culture;

	private bool system_charset;

	private InputLanguage input_language;

	/// <summary>Gets a value indicating whether the system default font supports the character set required for the requested input language.</summary>
	/// <returns>true if the system default font supports the character set required for the requested input language; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool SysCharSet => system_charset;

	/// <summary>Gets the locale of the requested input language.</summary>
	/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> that specifies the locale of the requested input language.</returns>
	/// <filterpriority>1</filterpriority>
	public CultureInfo Culture => culture;

	/// <summary>Gets a value indicating the input language.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.InputLanguage" />.</returns>
	/// <filterpriority>1</filterpriority>
	public InputLanguage InputLanguage => input_language;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.InputLanguageChangingEventArgs" /> class with the specified locale, character set, and acceptance.</summary>
	/// <param name="culture">The locale of the requested input language. </param>
	/// <param name="sysCharSet">true if the system default font supports the character set required for the requested input language; otherwise, false. </param>
	public InputLanguageChangingEventArgs(CultureInfo culture, bool sysCharSet)
	{
		this.culture = culture;
		system_charset = sysCharSet;
		input_language = InputLanguage.FromCulture(culture);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.InputLanguageChangingEventArgs" /> class with the specified input language, character set, and acceptance of a language change.</summary>
	/// <param name="inputLanguage">The requested input language. </param>
	/// <param name="sysCharSet">true if the system default font supports the character set required for the requested input language; otherwise, false. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="inputLanguage" /> is null. </exception>
	public InputLanguageChangingEventArgs(InputLanguage inputLanguage, bool sysCharSet)
	{
		culture = inputLanguage.Culture;
		system_charset = sysCharSet;
		input_language = inputLanguage;
	}
}
