namespace System.Windows.Forms;

/// <summary>Represents the method that will handle the <see cref="E:System.Windows.Forms.Control.KeyUp" /> or <see cref="E:System.Windows.Forms.Control.KeyDown" /> event of a <see cref="T:System.Windows.Forms.Control" />.</summary>
/// <param name="sender">The source of the event. </param>
/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
/// <filterpriority>2</filterpriority>
public delegate void KeyEventHandler(object sender, KeyEventArgs e);
