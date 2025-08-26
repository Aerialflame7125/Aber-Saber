using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms;

/// <summary>Hosts a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that displays items that overflow the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
/// <filterpriority>2</filterpriority>
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.None)]
public class ToolStripOverflowButton : ToolStripDropDownButton
{
	private class ToolStripOverflowButtonAccessibleObject : AccessibleObject
	{
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> has items that overflow the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> has overflow items; otherwise, false. </returns>
	/// <filterpriority>1</filterpriority>
	public override bool HasDropDownItems
	{
		get
		{
			if (drop_down == null)
			{
				return false;
			}
			return base.DropDown.DisplayedItems.Count > 0;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true to enable automatic mirroring; otherwise, false.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new bool RightToLeftAutoMirrorImage
	{
		get
		{
			return base.RightToLeftAutoMirrorImage;
		}
		set
		{
			base.RightToLeftAutoMirrorImage = value;
		}
	}

	/// <summary>Gets the space, in pixels, that is specified by default between controls.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the space between controls.</returns>
	protected internal override Padding DefaultMargin => new Padding(0, 1, 0, 2);

	internal ToolStripOverflowButton(ToolStrip ts)
	{
		base.InternalOwner = ts;
		base.Parent = ts;
		base.Visible = false;
	}

	/// <summary>Retrieves the size of a rectangular area into which a control can fit.</summary>
	/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
	/// <param name="constrainingSize">The custom-sized area for a control. </param>
	/// <filterpriority>1</filterpriority>
	public override Size GetPreferredSize(Size constrainingSize)
	{
		return new Size(16, base.Parent.Height);
	}

	/// <summary>Creates a new accessibility object for the control.</summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return new ToolStripOverflowButtonAccessibleObject();
	}

	/// <summary>Creates an empty <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that can be dropped down and to which events can be attached.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control.</returns>
	protected override ToolStripDropDown CreateDefaultDropDown()
	{
		ToolStripDropDown toolStripDropDown = new ToolStripOverflow(this);
		toolStripDropDown.DefaultDropDownDirection = ToolStripDropDownDirection.BelowLeft;
		toolStripDropDown.OwnerItem = this;
		return toolStripDropDown;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnPaint(PaintEventArgs e)
	{
		if (base.Owner != null)
		{
			base.Owner.Renderer.DrawOverflowButtonBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
		}
	}

	/// <summary>Sets the size and location of the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> representing the size and location of the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</param>
	protected internal override void SetBounds(Rectangle bounds)
	{
		base.SetBounds(bounds);
	}
}
