using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms;

/// <summary>Manages the overflow behavior of a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class ToolStripOverflow : ToolStripDropDown, IDisposable, IComponent
{
	private class ToolStripOverflowAccessibleObject : AccessibleObject
	{
	}

	private LayoutEngine layout_engine;

	/// <summary>Gets all of the items on the <see cref="T:System.Windows.Forms.ToolStrip" />, whether they are currently being displayed or not.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> containing all of the items.</returns>
	public override ToolStripItemCollection Items => base.Items;

	/// <filterpriority>1</filterpriority>
	public override LayoutEngine LayoutEngine
	{
		get
		{
			if (layout_engine == null)
			{
				layout_engine = new FlowLayout();
			}
			return base.LayoutEngine;
		}
	}

	/// <summary>Gets all of the items that are currently being displayed on the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> that includes all items on this <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
	protected internal override ToolStripItemCollection DisplayedItems => base.DisplayedItems;

	internal ToolStrip ParentToolStrip => base.OwnerItem.Parent;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripOverflow" /> class derived from a base <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <param name="parentItem">The <see cref="T:System.Windows.Forms.ToolStripItem" /> from which to derive this <see cref="T:System.Windows.Forms.ToolStripOverflow" /> instance. </param>
	public ToolStripOverflow(ToolStripItem parentItem)
	{
		base.OwnerItem = parentItem;
	}

	/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
	/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
	/// <param name="constrainingSize">The custom-sized area for a control.</param>
	public override Size GetPreferredSize(Size constrainingSize)
	{
		return base.GetToolStripPreferredSize(constrainingSize);
	}

	/// <summary>Creates a new accessibility object for the control.</summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return new ToolStripOverflowAccessibleObject();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
	[System.MonoInternalNote("This should stack in rows of ~3, but for now 1 column will work.")]
	protected override void OnLayout(LayoutEventArgs e)
	{
		SetDisplayedItems();
		int num = 0;
		foreach (ToolStripItem displayedItem in DisplayedItems)
		{
			if (displayedItem.Available && displayedItem.GetPreferredSize(Size.Empty).Width > num)
			{
				num = displayedItem.GetPreferredSize(Size.Empty).Width;
			}
		}
		int left = base.Padding.Left;
		num += base.Padding.Horizontal;
		int num2 = base.Padding.Top;
		foreach (ToolStripItem displayedItem2 in DisplayedItems)
		{
			if (displayedItem2.Available)
			{
				num2 += displayedItem2.Margin.Top;
				int num3 = 0;
				num3 = ((!(displayedItem2 is ToolStripSeparator)) ? displayedItem2.GetPreferredSize(Size.Empty).Height : 7);
				displayedItem2.SetBounds(new Rectangle(left, num2, num, num3));
				num2 += displayedItem2.Height + displayedItem2.Margin.Bottom;
			}
		}
		base.Size = new Size(num + base.Padding.Horizontal, num2 + base.Padding.Bottom);
	}

	/// <summary>Resets the collection of displayed and overflow items after a layout is done.</summary>
	protected override void SetDisplayedItems()
	{
		displayed_items.Clear();
		if (base.OwnerItem != null && base.OwnerItem.Parent != null)
		{
			foreach (ToolStripItem item in base.OwnerItem.Parent.Items)
			{
				if (item.Placement == ToolStripItemPlacement.Overflow && item.Available && !(item is ToolStripSeparator))
				{
					displayed_items.AddNoOwnerOrLayout(item);
				}
			}
		}
		PerformLayout();
	}
}
