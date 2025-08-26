namespace System.Windows.Forms.Design.Behavior;

/// <summary>Specifies the relative importance of a snapline.</summary>
public enum SnapLinePriority
{
	/// <summary>The lowest priority category.</summary>
	Low = 1,
	/// <summary>The middle priority category.</summary>
	Medium,
	/// <summary>The highest priority category.</summary>
	High,
	/// <summary>The priority category that is equivalent to the highest priority of all the current snaplines. Indicates that this category of snapline should always be active.</summary>
	Always
}
