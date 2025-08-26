using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Specifies the painting style applied to one <see cref="T:System.Windows.Forms.ToolStrip" /> contained in a form.</summary>
/// <filterpriority>2</filterpriority>
public enum ToolStripRenderMode
{
	/// <summary>Indicates that the <see cref="P:System.Windows.Forms.ToolStrip.RenderMode" /> is not determined by the <see cref="T:System.Windows.Forms.ToolStripManager" /> or the use of a <see cref="T:System.Windows.Forms.ToolStripRenderer" /> other than <see cref="T:System.Windows.Forms.ToolStripProfessionalRenderer" />, <see cref="T:System.Windows.Forms.ToolStripSystemRenderer" /></summary>
	[Browsable(false)]
	Custom,
	/// <summary>Indicates the use of a <see cref="T:System.Windows.Forms.ToolStripSystemRenderer" /> to paint.</summary>
	System,
	/// <summary>Indicates the use of a <see cref="T:System.Windows.Forms.ToolStripProfessionalRenderer" /> to paint.</summary>
	Professional,
	/// <summary>Indicates that the <see cref="P:System.Windows.Forms.ToolStripManager.RenderMode" /> or <see cref="P:System.Windows.Forms.ToolStripManager.Renderer" /> determines the painting style.</summary>
	ManagerRenderMode
}
