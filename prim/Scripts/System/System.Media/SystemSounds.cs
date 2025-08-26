namespace System.Media;

/// <summary>Retrieves sounds associated with a set of Windows operating system sound-event types. This class cannot be inherited.</summary>
public sealed class SystemSounds
{
	/// <summary>Gets the sound associated with the <see langword="Asterisk" /> program event in the current Windows sound scheme.</summary>
	/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the <see langword="Asterisk" /> program event in the current Windows sound scheme.</returns>
	public static SystemSound Asterisk => new SystemSound("Asterisk");

	/// <summary>Gets the sound associated with the <see langword="Beep" /> program event in the current Windows sound scheme.</summary>
	/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the <see langword="Beep" /> program event in the current Windows sound scheme.</returns>
	public static SystemSound Beep => new SystemSound("Beep");

	/// <summary>Gets the sound associated with the <see langword="Exclamation" /> program event in the current Windows sound scheme.</summary>
	/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the <see langword="Exclamation" /> program event in the current Windows sound scheme.</returns>
	public static SystemSound Exclamation => new SystemSound("Exclamation");

	/// <summary>Gets the sound associated with the <see langword="Hand" /> program event in the current Windows sound scheme.</summary>
	/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the <see langword="Hand" /> program event in the current Windows sound scheme.</returns>
	public static SystemSound Hand => new SystemSound("Hand");

	/// <summary>Gets the sound associated with the <see langword="Question" /> program event in the current Windows sound scheme.</summary>
	/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the <see langword="Question" /> program event in the current Windows sound scheme.</returns>
	public static SystemSound Question => new SystemSound("Question");

	private SystemSounds()
	{
	}
}
