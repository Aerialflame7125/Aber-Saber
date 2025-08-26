using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides data for the Scroll event.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public class ScrollEventArgs : EventArgs
{
	private ScrollEventType type;

	private int new_value;

	private int old_value;

	private ScrollOrientation scroll_orientation;

	/// <summary>Gets or sets the new <see cref="P:System.Windows.Forms.ScrollBar.Value" /> of the scroll bar.</summary>
	/// <returns>The numeric value that the <see cref="P:System.Windows.Forms.ScrollBar.Value" /> property will be changed to.</returns>
	/// <filterpriority>1</filterpriority>
	public int NewValue
	{
		get
		{
			return new_value;
		}
		set
		{
			new_value = value;
		}
	}

	/// <summary>Gets the old <see cref="P:System.Windows.Forms.ScrollBar.Value" /> of the scroll bar.</summary>
	/// <returns>The numeric value that the <see cref="P:System.Windows.Forms.ScrollBar.Value" /> property contained before it changed.</returns>
	/// <filterpriority>1</filterpriority>
	public int OldValue => old_value;

	/// <summary>Gets the scroll bar orientation that raised the Scroll event.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ScrollOrientation" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	public ScrollOrientation ScrollOrientation => scroll_orientation;

	/// <summary>Gets the type of scroll event that occurred.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	public ScrollEventType Type => type;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollEventArgs" /> class using the given values for the <see cref="P:System.Windows.Forms.ScrollEventArgs.Type" /> and <see cref="P:System.Windows.Forms.ScrollEventArgs.NewValue" /> properties.</summary>
	/// <param name="type">One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values. </param>
	/// <param name="newValue">The new value for the scroll bar. </param>
	public ScrollEventArgs(ScrollEventType type, int newValue)
		: this(type, -1, newValue, ScrollOrientation.HorizontalScroll)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollEventArgs" /> class using the given values for the <see cref="P:System.Windows.Forms.ScrollEventArgs.Type" />, <see cref="P:System.Windows.Forms.ScrollEventArgs.OldValue" />, and <see cref="P:System.Windows.Forms.ScrollEventArgs.NewValue" /> properties.</summary>
	/// <param name="type">One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values. </param>
	/// <param name="oldValue">The old value for the scroll bar. </param>
	/// <param name="newValue">The new value for the scroll bar. </param>
	public ScrollEventArgs(ScrollEventType type, int oldValue, int newValue)
		: this(type, oldValue, newValue, ScrollOrientation.HorizontalScroll)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollEventArgs" /> class using the given values for the <see cref="P:System.Windows.Forms.ScrollEventArgs.Type" />, <see cref="P:System.Windows.Forms.ScrollEventArgs.NewValue" />, and <see cref="P:System.Windows.Forms.ScrollEventArgs.ScrollOrientation" /> properties.</summary>
	/// <param name="type">One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values. </param>
	/// <param name="newValue">The new value for the scroll bar. </param>
	/// <param name="scroll">One of the <see cref="T:System.Windows.Forms.ScrollOrientation" /> values. </param>
	public ScrollEventArgs(ScrollEventType type, int newValue, ScrollOrientation scroll)
		: this(type, -1, newValue, scroll)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollEventArgs" /> class using the given values for the <see cref="P:System.Windows.Forms.ScrollEventArgs.Type" />, <see cref="P:System.Windows.Forms.ScrollEventArgs.OldValue" />, <see cref="P:System.Windows.Forms.ScrollEventArgs.NewValue" />, and <see cref="P:System.Windows.Forms.ScrollEventArgs.ScrollOrientation" /> properties.</summary>
	/// <param name="type">One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values. </param>
	/// <param name="oldValue">The old value for the scroll bar. </param>
	/// <param name="newValue">The new value for the scroll bar. </param>
	/// <param name="scroll">One of the <see cref="T:System.Windows.Forms.ScrollOrientation" /> values. </param>
	public ScrollEventArgs(ScrollEventType type, int oldValue, int newValue, ScrollOrientation scroll)
	{
		new_value = newValue;
		old_value = oldValue;
		scroll_orientation = scroll;
		this.type = type;
	}
}
