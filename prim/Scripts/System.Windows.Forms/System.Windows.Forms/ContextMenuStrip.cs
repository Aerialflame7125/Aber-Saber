using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a shortcut menu. </summary>
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
[DefaultEvent("Opening")]
public class ContextMenuStrip : ToolStripDropDownMenu
{
	private Control source_control;

	internal Control container;

	/// <summary>Gets the last control that caused this <see cref="T:System.Windows.Forms.ContextMenuStrip" /> to be displayed.</summary>
	/// <returns>The control that caused this <see cref="T:System.Windows.Forms.ContextMenuStrip" /> to be displayed.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public Control SourceControl => source_control;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> class. </summary>
	public ContextMenuStrip()
	{
		source_control = null;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> class and associates it with the specified container.</summary>
	/// <param name="container">A component that implements <see cref="T:System.ComponentModel.IContainer" /> that is the container of the <see cref="T:System.Windows.Forms.ContextMenuStrip" />.</param>
	public ContextMenuStrip(IContainer container)
	{
		source_control = null;
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> and optionally releases the managed resources. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	/// <param name="visible">true to make the control visible; otherwise, false.</param>
	protected override void SetVisibleCore(bool visible)
	{
		base.SetVisibleCore(visible);
		if (visible)
		{
			XplatUI.SetTopmost(Handle, Enabled: true);
		}
	}

	internal void SetSourceControl(Control source_control)
	{
		container = (this.source_control = source_control);
	}
}
