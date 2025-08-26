namespace System.Windows.Forms;

/// <summary>Represents the method that will handle the <see cref="E:System.Windows.Forms.TabControl.Selecting" /> or <see cref="E:System.Windows.Forms.TabControl.Deselecting" /> event of a <see cref="T:System.Windows.Forms.TabControl" /> control. </summary>
/// <param name="sender">The source of the event. </param>
/// <param name="e">A <see cref="T:System.Windows.Forms.TabControlCancelEventArgs" /> that contains the event data. </param>
/// <filterpriority>2</filterpriority>
public delegate void TabControlCancelEventHandler(object sender, TabControlCancelEventArgs e);
