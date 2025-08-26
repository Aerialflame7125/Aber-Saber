namespace System.Windows.Forms;

/// <summary>Provides basic properties for the <see cref="T:System.Windows.Forms.VScrollBar" /> class.</summary>
/// <filterpriority>2</filterpriority>
public class VScrollProperties : ScrollProperties
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.VScrollProperties" /> class. </summary>
	/// <param name="container">A <see cref="T:System.Windows.Forms.ScrollableControl" /> that contains the scroll bar.</param>
	public VScrollProperties(ScrollableControl container)
		: base(container)
	{
		scroll_bar = container.vscrollbar;
	}
}
