namespace System.Windows.Forms;

/// <summary>Specifies which child controls to skip.</summary>
[Flags]
public enum GetChildAtPointSkip
{
	/// <summary>Does not skip any child windows.</summary>
	None = 0,
	/// <summary>Skips invisible child windows.</summary>
	Invisible = 1,
	/// <summary>Skips disabled child windows.</summary>
	Disabled = 2,
	/// <summary>Skips transparent child windows.</summary>
	Transparent = 4
}
