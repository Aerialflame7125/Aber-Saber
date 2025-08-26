namespace System.Windows.Forms;

/// <summary>Specifies constants that define the state of the link.</summary>
/// <filterpriority>2</filterpriority>
public enum LinkState
{
	/// <summary>The state of a link in its normal state (none of the other states apply).</summary>
	Normal = 0,
	/// <summary>The state of a link over which a mouse pointer is resting.</summary>
	Hover = 1,
	/// <summary>The state of a link that has been clicked.</summary>
	Active = 2,
	/// <summary>The state of a link that has been visited.</summary>
	Visited = 4
}
