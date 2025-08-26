using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides data for <see cref="E:System.Windows.Forms.Splitter.SplitterMoving" /> and the <see cref="E:System.Windows.Forms.Splitter.SplitterMoved" /> events.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public class SplitterEventArgs : EventArgs
{
	internal int split_x;

	internal int split_y;

	internal int x;

	internal int y;

	/// <summary>Gets or sets the x-coordinate of the upper-left corner of the <see cref="T:System.Windows.Forms.Splitter" /> (in client coordinates).</summary>
	/// <returns>The x-coordinate of the upper-left corner of the control.</returns>
	/// <filterpriority>1</filterpriority>
	public int SplitX
	{
		get
		{
			return split_x;
		}
		set
		{
			split_x = value;
		}
	}

	/// <summary>Gets or sets the y-coordinate of the upper-left corner of the <see cref="T:System.Windows.Forms.Splitter" /> (in client coordinates).</summary>
	/// <returns>The y-coordinate of the upper-left corner of the control.</returns>
	/// <filterpriority>1</filterpriority>
	public int SplitY
	{
		get
		{
			return split_y;
		}
		set
		{
			split_y = value;
		}
	}

	/// <summary>Gets the x-coordinate of the mouse pointer (in client coordinates).</summary>
	/// <returns>The x-coordinate of the mouse pointer.</returns>
	/// <filterpriority>1</filterpriority>
	public int X => x;

	/// <summary>Gets the y-coordinate of the mouse pointer (in client coordinates).</summary>
	/// <returns>The y-coordinate of the mouse pointer.</returns>
	/// <filterpriority>1</filterpriority>
	public int Y => y;

	/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.SplitterEventArgs" /> class with the specified coordinates of the mouse pointer and the coordinates of the upper-left corner of the <see cref="T:System.Windows.Forms.Splitter" /> control.</summary>
	/// <param name="x">The x-coordinate of the mouse pointer (in client coordinates). </param>
	/// <param name="y">The y-coordinate of the mouse pointer (in client coordinates). </param>
	/// <param name="splitX">The x-coordinate of the upper-left corner of the <see cref="T:System.Windows.Forms.Splitter" /> (in client coordinates). </param>
	/// <param name="splitY">The y-coordinate of the upper-left corner of the <see cref="T:System.Windows.Forms.Splitter" /> (in client coordinates). </param>
	public SplitterEventArgs(int x, int y, int splitX, int splitY)
	{
		this.x = x;
		this.y = y;
		SplitX = splitX;
		SplitY = splitY;
	}
}
