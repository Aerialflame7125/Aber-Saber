using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the MeasureItem event of the <see cref="T:System.Windows.Forms.ListBox" />, <see cref="T:System.Windows.Forms.ComboBox" />, <see cref="T:System.Windows.Forms.CheckedListBox" />, and <see cref="T:System.Windows.Forms.MenuItem" /> controls.</summary>
/// <filterpriority>2</filterpriority>
public class MeasureItemEventArgs : EventArgs
{
	private Graphics graphics;

	private int index;

	private int itemHeight;

	private int itemWidth;

	/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> object to measure against.</summary>
	/// <returns>The <see cref="T:System.Drawing.Graphics" /> object to use to determine the scale of the item you are drawing.</returns>
	/// <filterpriority>1</filterpriority>
	public Graphics Graphics => graphics;

	/// <summary>Gets the index of the item for which the height and width is needed.</summary>
	/// <returns>The index of the item to be measured.</returns>
	/// <filterpriority>1</filterpriority>
	public int Index => index;

	/// <summary>Gets or sets the height of the item specified by the <see cref="P:System.Windows.Forms.MeasureItemEventArgs.Index" />.</summary>
	/// <returns>The height of the item measured.</returns>
	/// <filterpriority>1</filterpriority>
	public int ItemHeight
	{
		get
		{
			return itemHeight;
		}
		set
		{
			itemHeight = value;
		}
	}

	/// <summary>Gets or sets the width of the item specified by the <see cref="P:System.Windows.Forms.MeasureItemEventArgs.Index" />.</summary>
	/// <returns>The width of the item measured.</returns>
	/// <filterpriority>1</filterpriority>
	public int ItemWidth
	{
		get
		{
			return itemWidth;
		}
		set
		{
			itemWidth = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> class.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> object being written to. </param>
	/// <param name="index">The index of the item for which you need the height or width. </param>
	public MeasureItemEventArgs(Graphics graphics, int index)
	{
		this.graphics = graphics;
		this.index = index;
		itemHeight = 0;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> class providing a parameter for the item height.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> object being written to. </param>
	/// <param name="index">The index of the item for which you need the height or width. </param>
	/// <param name="itemHeight">The height of the item to measure relative to the <paramref name="graphics" /> object. </param>
	public MeasureItemEventArgs(Graphics graphics, int index, int itemHeight)
	{
		this.graphics = graphics;
		this.index = index;
		this.itemHeight = itemHeight;
	}
}
