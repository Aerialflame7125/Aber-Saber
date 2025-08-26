namespace System.Windows.Forms;

/// <summary>Specifies constants defining which information to display.</summary>
/// <filterpriority>2</filterpriority>
public enum MessageBoxIcon
{
	/// <summary>The message box contain no symbols.</summary>
	None = 0,
	/// <summary>The message box contains a symbol consisting of white X in a circle with a red background.</summary>
	Error = 16,
	/// <summary>The message box contains a symbol consisting of a white X in a circle with a red background.</summary>
	Hand = 16,
	/// <summary>The message box contains a symbol consisting of white X in a circle with a red background.</summary>
	Stop = 16,
	/// <summary>The message box contains a symbol consisting of a question mark in a circle. The question-mark message icon is no longer recommended because it does not clearly represent a specific type of message and because the phrasing of a message as a question could apply to any message type. In addition, users can confuse the message symbol question mark with Help information. Therefore, do not use this question mark message symbol in your message boxes. The system continues to support its inclusion only for backward compatibility.</summary>
	Question = 32,
	/// <summary>The message box contains a symbol consisting of an exclamation point in a triangle with a yellow background.</summary>
	Exclamation = 48,
	/// <summary>The message box contains a symbol consisting of an exclamation point in a triangle with a yellow background.</summary>
	Warning = 48,
	/// <summary>The message box contains a symbol consisting of a lowercase letter i in a circle.</summary>
	Asterisk = 64,
	/// <summary>The message box contains a symbol consisting of a lowercase letter i in a circle.</summary>
	Information = 64
}
