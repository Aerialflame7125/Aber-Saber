namespace System.Windows.Forms;

/// <summary>Specifies the state of the user interface.</summary>
/// <filterpriority>2</filterpriority>
[Flags]
public enum UICues
{
	/// <summary>No change was made.</summary>
	None = 0,
	/// <summary>Focus rectangles are displayed after the change.</summary>
	ShowFocus = 1,
	/// <summary>Keyboard cues are underlined after the change.</summary>
	ShowKeyboard = 2,
	/// <summary>Focus rectangles are displayed and keyboard cues are underlined after the change.</summary>
	Shown = 3,
	/// <summary>The state of the focus cues has changed.</summary>
	ChangeFocus = 4,
	/// <summary>The state of the keyboard cues has changed.</summary>
	ChangeKeyboard = 8,
	/// <summary>The state of the focus cues and keyboard cues has changed.</summary>
	Changed = 0xC
}
