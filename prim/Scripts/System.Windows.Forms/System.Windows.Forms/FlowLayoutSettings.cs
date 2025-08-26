using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms;

/// <summary>Collects the characteristics associated with flow layouts.</summary>
/// <filterpriority>2</filterpriority>
[DefaultProperty("FlowDirection")]
public class FlowLayoutSettings : LayoutSettings
{
	private FlowDirection flow_direction;

	private bool wrap_contents;

	private LayoutEngine layout_engine;

	private Dictionary<object, bool> flow_breaks;

	private Control owner;

	/// <summary>Gets or sets a value indicating the flow direction of consecutive controls.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.FlowDirection" /> indicating the flow direction of consecutive controls in the container. The default is <see cref="F:System.Windows.Forms.FlowDirection.LeftToRight" />.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(FlowDirection.LeftToRight)]
	public FlowDirection FlowDirection
	{
		get
		{
			return flow_direction;
		}
		set
		{
			if (flow_direction != value)
			{
				flow_direction = value;
				if (owner != null)
				{
					owner.PerformLayout(owner, "FlowDirection");
				}
			}
		}
	}

	/// <summary>Gets the current flow layout engine.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> currently being used. </returns>
	/// <filterpriority>1</filterpriority>
	public override LayoutEngine LayoutEngine
	{
		get
		{
			if (layout_engine == null)
			{
				layout_engine = new FlowLayout();
			}
			return layout_engine;
		}
	}

	/// <summary>Gets or sets a value indicating whether the contents should be wrapped or clipped when they exceed the original boundaries of their container.</summary>
	/// <returns>true if the contents should be wrapped; otherwise, false if the contents should be clipped. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	public bool WrapContents
	{
		get
		{
			return wrap_contents;
		}
		set
		{
			if (wrap_contents != value)
			{
				wrap_contents = value;
				if (owner != null)
				{
					owner.PerformLayout(owner, "WrapContents");
				}
			}
		}
	}

	internal FlowLayoutSettings()
		: this(null)
	{
	}

	internal FlowLayoutSettings(Control owner)
	{
		flow_breaks = new Dictionary<object, bool>();
		wrap_contents = true;
		flow_direction = FlowDirection.LeftToRight;
		this.owner = owner;
	}

	/// <summary>Returns a value that represents the flow break setting of the control.</summary>
	/// <returns>true if the flow break is set; otherwise, false.</returns>
	/// <param name="child">The child control.</param>
	public bool GetFlowBreak(object child)
	{
		if (flow_breaks.TryGetValue(child, out var value))
		{
			return value;
		}
		return false;
	}

	/// <summary>Sets the value that represents the flow break setting of the control.</summary>
	/// <param name="child">The child control.</param>
	/// <param name="value">The flow break value to set.</param>
	public void SetFlowBreak(object child, bool value)
	{
		flow_breaks[child] = value;
		if (owner != null)
		{
			owner.PerformLayout((Control)child, "FlowBreak");
		}
	}
}
