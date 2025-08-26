namespace System.Windows.Forms;

/// <summary>Specifies the appearance of a control.</summary>
/// <filterpriority>2</filterpriority>
public enum FlatStyle
{
	/// <summary>The control appears flat.</summary>
	Flat,
	/// <summary>A control appears flat until the mouse pointer moves over it, at which point it appears three-dimensional.</summary>
	Popup,
	/// <summary>The control appears three-dimensional.</summary>
	Standard,
	/// <summary>The appearance of the control is determined by the user's operating system.</summary>
	System
}
