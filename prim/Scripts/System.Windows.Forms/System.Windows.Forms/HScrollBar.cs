using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a standard Windows horizontal scroll bar.</summary>
/// <filterpriority>2</filterpriority>
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class HScrollBar : ScrollBar
{
	/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
	protected override Size DefaultSize => ThemeEngine.Current.HScrollBarDefaultSize;

	protected override CreateParams CreateParams => base.CreateParams;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HScrollBar" /> class. </summary>
	public HScrollBar()
	{
		vert = false;
	}
}
