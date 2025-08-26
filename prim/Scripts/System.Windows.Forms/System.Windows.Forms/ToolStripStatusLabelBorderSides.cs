using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Specifies which sides of a <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> have borders.</summary>
[ComVisible(true)]
[Flags]
[Editor("System.Windows.Forms.Design.BorderSidesEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
public enum ToolStripStatusLabelBorderSides
{
	/// <summary>The <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> has no borders.</summary>
	None = 0,
	/// <summary>Only the left side of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> has borders.</summary>
	Left = 1,
	/// <summary>Only the top side of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> has borders.</summary>
	Top = 2,
	/// <summary>Only the right side of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> has borders.</summary>
	Right = 4,
	/// <summary>Only the bottom side of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> has borders.</summary>
	Bottom = 8,
	/// <summary>All sides of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> have borders.</summary>
	All = 0xF
}
