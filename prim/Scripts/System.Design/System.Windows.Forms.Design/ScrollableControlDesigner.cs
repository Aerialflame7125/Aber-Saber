using System.Drawing;

namespace System.Windows.Forms.Design;

/// <summary>Base designer class for extending the design mode behavior of a <see cref="T:System.Windows.Forms.Control" /> which should receive scroll messages.</summary>
public class ScrollableControlDesigner : ParentControlDesigner
{
	private const int HTHSCROLL = 6;

	private const int HTVSCROLL = 7;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ScrollableControlDesigner" /> class.</summary>
	public ScrollableControlDesigner()
	{
	}

	/// <summary>Indicates whether a mouse click at the specified point should be handled by the control.</summary>
	/// <param name="pt">A <see cref="T:System.Drawing.Point" /> indicating the position at which the mouse was clicked, in screen coordinates.</param>
	/// <returns>
	///   <see langword="true" /> if a click at the specified point is to be handled by the control; otherwise, <see langword="false" />.</returns>
	protected override bool GetHitTest(Point pt)
	{
		if (base.GetHitTest(pt))
		{
			return true;
		}
		if (Control is ScrollableControl && ((ScrollableControl)Control).AutoScroll)
		{
			int num = (int)Native.SendMessage(Control.Handle, Native.Msg.WM_NCHITTEST, IntPtr.Zero, Native.LParam(pt.X, pt.Y));
			if (num == 6 || num == 7)
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>Processes Windows messages and passes WM_HSCROLL and WM_VSCROLL messages to the control at design time.</summary>
	/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> to process.</param>
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
		if (m.Msg == 276 || m.Msg == 277)
		{
			DefWndProc(ref m);
		}
	}
}
