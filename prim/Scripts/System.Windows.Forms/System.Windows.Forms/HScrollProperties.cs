namespace System.Windows.Forms;

/// <summary>Provides basic properties for the <see cref="T:System.Windows.Forms.HScrollBar" /></summary>
/// <filterpriority>2</filterpriority>
public class HScrollProperties : ScrollProperties
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HScrollProperties" /> class. </summary>
	/// <param name="container">A <see cref="T:System.Windows.Forms.ScrollableControl" /> that contains the scroll bar.</param>
	public HScrollProperties(ScrollableControl container)
		: base(container)
	{
		scroll_bar = container.hscrollbar;
	}
}
