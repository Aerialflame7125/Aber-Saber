namespace System.Windows.Forms;

/// <summary>Specifies values for navigating among accessible objects.</summary>
/// <filterpriority>2</filterpriority>
public enum AccessibleNavigation
{
	/// <summary>Navigation to a sibling object located above the starting object.</summary>
	Up = 1,
	/// <summary>Navigation to a sibling object located below the starting object.</summary>
	Down,
	/// <summary>Navigation to the sibling object located to the left of the starting object.</summary>
	Left,
	/// <summary>Navigation to the sibling object located to the right of the starting object.</summary>
	Right,
	/// <summary>Navigation to the next logical object, typically from a sibling object to the starting object.</summary>
	Next,
	/// <summary>Navigation to the previous logical object, typically from a sibling object to the starting object.</summary>
	Previous,
	/// <summary>Navigation to the first child of the object.</summary>
	FirstChild,
	/// <summary>Navigation to the last child of the object.</summary>
	LastChild
}
