namespace System.Web.UI.Design;

/// <summary>Provides data for the <see cref="E:System.Web.UI.Design.IControlDesignerView.ViewEvent" /> event.</summary>
public class ViewEventArgs : EventArgs
{
	private ViewEvent event_type;

	private DesignerRegion region;

	private EventArgs event_args;

	/// <summary>Gets the type of action that raised the event.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Design.ViewEvent" /> that specifies the type of action that raised the event.</returns>
	public ViewEvent EventType => event_type;

	/// <summary>Gets the designer region that the event applies to.</summary>
	/// <returns>The <see cref="T:System.Web.UI.Design.DesignerRegion" /> that the action applies to.</returns>
	public DesignerRegion Region => region;

	/// <summary>Gets the event arguments that are associated with the action that raised the event.</summary>
	/// <returns>An <see cref="P:System.Web.UI.Design.ViewEventArgs.EventArgs" /> that contains additional event data that is specific to the type of event.</returns>
	public EventArgs EventArgs => event_args;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ViewEventArgs" /> class for the type of view event on the design surface.</summary>
	/// <param name="eventType">The type of action that raised the event; used to initialize the <see cref="P:System.Web.UI.Design.ViewEventArgs.EventType" />.</param>
	/// <param name="region">The designer region that the action applies to; used to initialize the <see cref="P:System.Web.UI.Design.ViewEventArgs.Region" />.</param>
	/// <param name="eventArgs">The event arguments associated with <paramref name="eventType" />; used to initialize the <see cref="P:System.Web.UI.Design.ViewEventArgs.EventArgs" />.</param>
	public ViewEventArgs(ViewEvent eventType, DesignerRegion region, EventArgs eventArgs)
	{
		event_type = eventType;
		this.region = region;
		event_args = eventArgs;
	}
}
