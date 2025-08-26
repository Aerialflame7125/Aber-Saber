namespace System.Drawing;

/// <summary>Specifies the fonts used to display text in Windows display elements.</summary>
public sealed class SystemFonts
{
	/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used to display text in the title bars of windows.</summary>
	/// <returns>A <see cref="T:System.Drawing.Font" /> that is used to display text in the title bars of windows.</returns>
	public static Font CaptionFont => new Font("Microsoft Sans Serif", 11f, "CaptionFont");

	/// <summary>Gets the default font that applications can use for dialog boxes and forms.</summary>
	/// <returns>The default <see cref="T:System.Drawing.Font" /> of the system. The value returned will vary depending on the user's operating system and the local culture setting of their system.</returns>
	public static Font DefaultFont => new Font("Microsoft Sans Serif", 8.25f, "DefaultFont");

	/// <summary>Gets a font that applications can use for dialog boxes and forms.</summary>
	/// <returns>A <see cref="T:System.Drawing.Font" /> that can be used for dialog boxes and forms, depending on the operating system and local culture setting of the system.</returns>
	public static Font DialogFont => new Font("Tahoma", 8f, "DialogFont");

	/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used for icon titles.</summary>
	/// <returns>A <see cref="T:System.Drawing.Font" /> that is used for icon titles.</returns>
	public static Font IconTitleFont => new Font("Microsoft Sans Serif", 11f, "IconTitleFont");

	/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used for menus.</summary>
	/// <returns>A <see cref="T:System.Drawing.Font" /> that is used for menus.</returns>
	public static Font MenuFont => new Font("Microsoft Sans Serif", 11f, "MenuFont");

	/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used for message boxes.</summary>
	/// <returns>A <see cref="T:System.Drawing.Font" /> that is used for message boxes</returns>
	public static Font MessageBoxFont => new Font("Microsoft Sans Serif", 11f, "MessageBoxFont");

	/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used to display text in the title bars of small windows, such as tool windows.</summary>
	/// <returns>A <see cref="T:System.Drawing.Font" /> that is used to display text in the title bars of small windows, such as tool windows.</returns>
	public static Font SmallCaptionFont => new Font("Microsoft Sans Serif", 11f, "SmallCaptionFont");

	/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used to display text in the status bar.</summary>
	/// <returns>A <see cref="T:System.Drawing.Font" /> that is used to display text in the status bar.</returns>
	public static Font StatusFont => new Font("Microsoft Sans Serif", 11f, "StatusFont");

	static SystemFonts()
	{
	}

	private SystemFonts()
	{
	}

	/// <summary>Returns a font object that corresponds to the specified system font name.</summary>
	/// <param name="systemFontName">The name of the system font you need a font object for.</param>
	/// <returns>A <see cref="T:System.Drawing.Font" /> if the specified name matches a value in <see cref="T:System.Drawing.SystemFonts" />; otherwise, <see langword="null" />.</returns>
	public static Font GetFontByName(string systemFontName)
	{
		return systemFontName switch
		{
			"CaptionFont" => CaptionFont, 
			"DefaultFont" => DefaultFont, 
			"DialogFont" => DialogFont, 
			"IconTitleFont" => IconTitleFont, 
			"MenuFont" => MenuFont, 
			"MessageBoxFont" => MessageBoxFont, 
			"SmallCaptionFont" => SmallCaptionFont, 
			"StatusFont" => StatusFont, 
			_ => null, 
		};
	}
}
