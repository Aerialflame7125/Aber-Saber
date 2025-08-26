using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms;

/// <summary>Represents a panel that dynamically lays out its contents horizontally or vertically.</summary>
/// <filterpriority>1</filterpriority>
[Designer("System.Windows.Forms.Design.FlowLayoutPanelDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ProvideProperty("FlowBreak", typeof(Control))]
[ComVisible(true)]
[DefaultProperty("FlowDirection")]
[Docking(DockingBehavior.Ask)]
public class FlowLayoutPanel : Panel, IExtenderProvider
{
	private FlowLayoutSettings settings;

	/// <summary>Gets or sets a value indicating the flow direction of the <see cref="T:System.Windows.Forms.FlowLayoutPanel" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.FlowDirection" /> values indicating the direction of consecutive placement of controls in the panel. The default is <see cref="F:System.Windows.Forms.FlowDirection.LeftToRight" />.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(FlowDirection.LeftToRight)]
	[Localizable(true)]
	public FlowDirection FlowDirection
	{
		get
		{
			return LayoutSettings.FlowDirection;
		}
		set
		{
			LayoutSettings.FlowDirection = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.FlowLayoutPanel" /> control should wrap its contents or let the contents be clipped.</summary>
	/// <returns>true if the contents should be wrapped; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	[Localizable(true)]
	public bool WrapContents
	{
		get
		{
			return LayoutSettings.WrapContents;
		}
		set
		{
			LayoutSettings.WrapContents = value;
		}
	}

	/// <summary>Gets a cached instance of the panel's layout engine.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> for the panel's contents.</returns>
	/// <filterpriority>1</filterpriority>
	public override LayoutEngine LayoutEngine => LayoutSettings.LayoutEngine;

	internal FlowLayoutSettings LayoutSettings
	{
		get
		{
			if (settings == null)
			{
				settings = new FlowLayoutSettings(this);
			}
			return settings;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FlowLayoutPanel" /> class.</summary>
	public FlowLayoutPanel()
	{
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IExtenderProvider.CanExtend(System.Object)" />.</summary>
	/// <returns>true if this object can provide extender properties to the specified object; otherwise, false.</returns>
	/// <param name="obj">The <see cref="T:System.Object" /> to receive the extender properties.</param>
	bool IExtenderProvider.CanExtend(object obj)
	{
		if (obj is Control && (obj as Control).Parent == this)
		{
			return true;
		}
		return false;
	}

	/// <summary>Returns a value that represents the flow-break setting of the <see cref="T:System.Windows.Forms.FlowLayoutPanel" /> control.</summary>
	/// <returns>true if the flow break is set; otherwise, false.</returns>
	/// <param name="control">The child control.</param>
	[DisplayName("FlowBreak")]
	[DefaultValue(false)]
	public bool GetFlowBreak(Control control)
	{
		return LayoutSettings.GetFlowBreak(control);
	}

	/// <summary>Sets the value that represents the flow-break setting of the <see cref="T:System.Windows.Forms.FlowLayoutPanel" /> control.</summary>
	/// <param name="control">The child control.</param>
	/// <param name="value">The flow-break value to set.</param>
	[DisplayName("FlowBreak")]
	public void SetFlowBreak(Control control, bool value)
	{
		LayoutSettings.SetFlowBreak(control, value);
	}

	internal override void CalculateCanvasSize(bool canOverride)
	{
		if (canOverride)
		{
			canvas_size = base.ClientSize;
		}
		else
		{
			base.CalculateCanvasSize(canOverride);
		}
	}

	internal override Size GetPreferredSizeCore(Size proposedSize)
	{
		int num = 0;
		int num2 = 0;
		bool flag = FlowDirection == FlowDirection.LeftToRight || FlowDirection == FlowDirection.RightToLeft;
		if (!WrapContents || (flag && proposedSize.Width == 0) || (!flag && proposedSize.Height == 0))
		{
			foreach (Control control3 in base.Controls)
			{
				Size size = ((!control3.AutoSize) ? control3.Size : control3.PreferredSize);
				Padding padding = control3.Margin;
				if (flag)
				{
					num += size.Width + padding.Horizontal;
					num2 = Math.Max(num2, size.Height + padding.Vertical);
				}
				else
				{
					num2 += size.Height + padding.Vertical;
					num = Math.Max(num, size.Width + padding.Horizontal);
				}
			}
		}
		else
		{
			int num3 = 0;
			int num4 = 0;
			foreach (Control control4 in base.Controls)
			{
				Size size2 = ((!control4.AutoSize) ? control4.ExplicitBounds.Size : control4.PreferredSize);
				Padding padding2 = control4.Margin;
				if (flag)
				{
					int num5 = size2.Width + padding2.Horizontal;
					if (num3 != 0 && num3 + num5 >= proposedSize.Width)
					{
						num = Math.Max(num, num3);
						num3 = 0;
						num2 += num4;
						num4 = 0;
					}
					num3 += num5;
					num4 = Math.Max(num4, size2.Height + padding2.Vertical);
				}
				else
				{
					int num5 = size2.Height + padding2.Vertical;
					if (num3 != 0 && num3 + num5 >= proposedSize.Height)
					{
						num2 = Math.Max(num2, num3);
						num3 = 0;
						num += num4;
						num4 = 0;
					}
					num3 += num5;
					num4 = Math.Max(num4, size2.Width + padding2.Horizontal);
				}
			}
			if (flag)
			{
				num = Math.Max(num, num3);
				num2 += num4;
			}
			else
			{
				num2 = Math.Max(num2, num3);
				num += num4;
			}
		}
		return new Size(num, num2);
	}
}
