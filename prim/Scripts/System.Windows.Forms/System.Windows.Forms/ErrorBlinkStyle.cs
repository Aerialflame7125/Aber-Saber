namespace System.Windows.Forms;

/// <summary>Specifies constants indicating when the error icon, supplied by an <see cref="T:System.Windows.Forms.ErrorProvider" />, should blink to alert the user that an error has occurred.</summary>
/// <filterpriority>2</filterpriority>
public enum ErrorBlinkStyle
{
	/// <summary>Blinks when the icon is already displayed and a new error string is set for the control.</summary>
	BlinkIfDifferentError,
	/// <summary>Always blink when the error icon is first displayed, or when a error description string is set for the control and the error icon is already displayed.</summary>
	AlwaysBlink,
	/// <summary>Never blink the error icon.</summary>
	NeverBlink
}
