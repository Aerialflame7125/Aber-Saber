namespace System.Web.UI.WebControls;

/// <summary>Specifies the visibility and position of scroll bars in a <see cref="T:System.Web.UI.WebControls.Panel" /> control.</summary>
[Flags]
public enum ScrollBars
{
	/// <summary>Displays no scroll bars.</summary>
	None = 0,
	/// <summary>Displays only a horizontal scroll bar.</summary>
	Horizontal = 1,
	/// <summary>Displays only a vertical scroll bar.</summary>
	Vertical = 2,
	/// <summary>Displays both a horizontal and a vertical scroll bar.</summary>
	Both = 3,
	/// <summary>Displays, horizontal, vertical, or both scroll bars as necessary. Otherwise, no scroll bars are shown.</summary>
	Auto = 4
}
