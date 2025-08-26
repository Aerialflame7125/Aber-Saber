namespace System.Windows.Forms.VisualStyles;

/// <summary>Describes the location of a point in the background specified by a visual style.</summary>
public enum HitTestCode
{
	/// <summary>The hit test succeeded outside the control or on a transparent area.</summary>
	Nowhere = 0,
	/// <summary>The hit test succeeded in the middle background segment.</summary>
	Client = 1,
	/// <summary>The hit test succeeded in the left border segment.</summary>
	Left = 10,
	/// <summary>The hit test succeeded in the right border segment.</summary>
	Right = 11,
	/// <summary>The hit test succeeded in the top border segment.</summary>
	Top = 12,
	/// <summary>The hit test succeeded in the top and left border intersection.</summary>
	TopLeft = 13,
	/// <summary>The hit test succeeded in the top and right border intersection.</summary>
	TopRight = 14,
	/// <summary>The hit test succeeded in the bottom border segment.</summary>
	Bottom = 15,
	/// <summary>The hit test succeeded in the bottom and left border intersection.</summary>
	BottomLeft = 16,
	/// <summary>The hit test succeeded in the bottom and right border intersection.</summary>
	BottomRight = 17
}
