namespace System.Windows.Forms.Design.Behavior;

/// <summary>Represents the methods that will handle the <see cref="E:System.Windows.Forms.Design.Behavior.BehaviorService.BeginDrag" /> and <see cref="E:System.Windows.Forms.Design.Behavior.BehaviorService.EndDrag" /> events of a <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />. This class cannot be inherited.</summary>
/// <param name="sender">The source of the event.</param>
/// <param name="e">A <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorDragDropEventArgs" /> that contains the event data.</param>
public delegate void BehaviorDragDropEventHandler(object sender, BehaviorDragDropEventArgs e);
