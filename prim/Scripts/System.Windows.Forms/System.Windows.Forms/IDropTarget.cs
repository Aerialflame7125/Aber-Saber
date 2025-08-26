namespace System.Windows.Forms;

/// <summary>Defines mouse events.</summary>
/// <filterpriority>2</filterpriority>
public interface IDropTarget
{
	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragDrop" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
	/// <filterpriority>1</filterpriority>
	void OnDragDrop(DragEventArgs e);

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragEnter" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
	/// <filterpriority>1</filterpriority>
	void OnDragEnter(DragEventArgs e);

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragLeave" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	/// <filterpriority>1</filterpriority>
	void OnDragLeave(EventArgs e);

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragOver" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
	/// <filterpriority>1</filterpriority>
	void OnDragOver(DragEventArgs e);
}
