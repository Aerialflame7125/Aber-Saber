namespace System.Windows.Forms;

/// <summary>Specifies the direction to move when getting items with the <see cref="M:System.Windows.Forms.ToolStrip.GetNextItem(System.Windows.Forms.ToolStripItem,System.Windows.Forms.ArrowDirection)" /> method.</summary>
public enum ArrowDirection
{
	/// <summary>The direction is left (<see cref="F:System.Windows.Forms.Orientation.Horizontal" />).</summary>
	Left = 0,
	/// <summary>The direction is up (<see cref="F:System.Windows.Forms.Orientation.Vertical" />).</summary>
	Up = 1,
	/// <summary>The direction is right (<see cref="F:System.Windows.Forms.Orientation.Horizontal" />).</summary>
	Right = 16,
	/// <summary>The direction is down (<see cref="F:System.Windows.Forms.Orientation.Vertical" />).</summary>
	Down = 17
}
