using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a standard Windows vertical scroll bar.</summary>
/// <filterpriority>2</filterpriority>
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class VScrollBar : ScrollBar
{
	/// <summary>Gets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.</summary>
	/// <returns>The <see cref="F:System.Windows.Forms.RightToLeft.No" /> value.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override RightToLeft RightToLeft
	{
		get
		{
			return base.RightToLeft;
		}
		set
		{
			if (RightToLeft != value)
			{
				base.RightToLeft = value;
				OnRightToLeftChanged(EventArgs.Empty);
			}
		}
	}

	/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
	protected override Size DefaultSize => ThemeEngine.Current.VScrollBarDefaultSize;

	protected override CreateParams CreateParams => base.CreateParams;

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.VScrollBar.RightToLeft" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler RightToLeftChanged
	{
		add
		{
			base.RightToLeftChanged += value;
		}
		remove
		{
			base.RightToLeftChanged -= value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.VScrollBar" /> class. </summary>
	public VScrollBar()
	{
		vert = true;
	}
}
