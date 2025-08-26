namespace System.Windows.Forms;

/// <summary>Represents the method that will handle the <see cref="E:System.Windows.Forms.Control.DragDrop" />, <see cref="E:System.Windows.Forms.Control.DragEnter" />, or <see cref="E:System.Windows.Forms.Control.DragOver" /> event of a <see cref="T:System.Windows.Forms.Control" />.</summary>
/// <param name="sender">The source of the event. </param>
/// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
/// <filterpriority>2</filterpriority>
public delegate void DragEventHandler(object sender, DragEventArgs e);
