using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Defines a base class for controls that support auto-scrolling behavior.</summary>
/// <filterpriority>2</filterpriority>
[Designer("System.Windows.Forms.Design.ScrollableControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class ScrollableControl : Control
{
	/// <summary>Determines the border padding for docked controls.</summary>
	[TypeConverter(typeof(DockPaddingEdgesConverter))]
	public class DockPaddingEdges : ICloneable
	{
		private Control owner;

		/// <summary>Gets or sets the padding width for all edges of a docked control.</summary>
		/// <returns>The padding width, in pixels.</returns>
		[RefreshProperties(RefreshProperties.All)]
		public int All
		{
			get
			{
				return owner.Padding.All;
			}
			set
			{
				owner.Padding = new Padding(value);
			}
		}

		/// <summary>Gets or sets the padding width for the bottom edge of a docked control.</summary>
		/// <returns>The padding width, in pixels.</returns>
		[RefreshProperties(RefreshProperties.All)]
		public int Bottom
		{
			get
			{
				return owner.Padding.Bottom;
			}
			set
			{
				owner.Padding = new Padding(Left, Top, Right, value);
			}
		}

		/// <summary>Gets or sets the padding width for the left edge of a docked control.</summary>
		/// <returns>The padding width, in pixels.</returns>
		[RefreshProperties(RefreshProperties.All)]
		public int Left
		{
			get
			{
				return owner.Padding.Left;
			}
			set
			{
				owner.Padding = new Padding(value, Top, Right, Bottom);
			}
		}

		/// <summary>Gets or sets the padding width for the right edge of a docked control.</summary>
		/// <returns>The padding width, in pixels.</returns>
		[RefreshProperties(RefreshProperties.All)]
		public int Right
		{
			get
			{
				return owner.Padding.Right;
			}
			set
			{
				owner.Padding = new Padding(Left, Top, value, Bottom);
			}
		}

		/// <summary>Gets or sets the padding width for the top edge of a docked control.</summary>
		/// <returns>The padding width, in pixels.</returns>
		[RefreshProperties(RefreshProperties.All)]
		public int Top
		{
			get
			{
				return owner.Padding.Top;
			}
			set
			{
				owner.Padding = new Padding(Left, value, Right, Bottom);
			}
		}

		internal DockPaddingEdges(Control owner)
		{
			this.owner = owner;
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			return new DockPaddingEdges(owner);
		}

		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="other"></param>
		public override bool Equals(object other)
		{
			if (!(other is DockPaddingEdges))
			{
				return false;
			}
			if (All == ((DockPaddingEdges)other).All && Left == ((DockPaddingEdges)other).Left && Right == ((DockPaddingEdges)other).Right && Top == ((DockPaddingEdges)other).Top && Bottom == ((DockPaddingEdges)other).Bottom)
			{
				return true;
			}
			return false;
		}

		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		public override int GetHashCode()
		{
			return All * Top * Bottom * Right * Left;
		}

		/// <summary>Returns an empty string.</summary>
		/// <returns>An empty string.</returns>
		public override string ToString()
		{
			return "All = " + All + " Top = " + Top + " Left = " + Left + " Bottom = " + Bottom + " Right = " + Right;
		}

		internal void Scale(float dx, float dy)
		{
			Left = (int)((float)Left * dx);
			Right = (int)((float)Right * dx);
			Top = (int)((float)Top * dy);
			Bottom = (int)((float)Bottom * dy);
		}
	}

	/// <summary>A <see cref="T:System.ComponentModel.TypeConverter" /> for the <see cref="T:System.Windows.Forms.ScrollableControl.DockPaddingEdges" /> class.</summary>
	public class DockPaddingEdgesConverter : TypeConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollableControl.DockPaddingEdgesConverter" /> class. </summary>
		public DockPaddingEdgesConverter()
		{
		}

		/// <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for the <see cref="T:System.Windows.Forms.ScrollableControl" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An object that specifies the type of array for which to get properties.</param>
		/// <param name="attributes">An array of type attribute that is used as a filter.</param>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(DockPaddingEdges), attributes);
		}

		/// <summary>Returns whether the current object supports properties, using the specified context.</summary>
		/// <returns>true in all cases.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}

	/// <summary>Determines the value of the <see cref="P:System.Windows.Forms.ScrollableControl.AutoScroll" /> property.</summary>
	protected const int ScrollStateAutoScrolling = 1;

	/// <summary>Determines whether the user has enabled full window drag.</summary>
	protected const int ScrollStateFullDrag = 16;

	/// <summary>Determines whether the value of the <see cref="P:System.Windows.Forms.ScrollableControl.HScroll" /> property is set to true.</summary>
	protected const int ScrollStateHScrollVisible = 2;

	/// <summary>Determines whether the user had scrolled through the <see cref="T:System.Windows.Forms.ScrollableControl" /> control.</summary>
	protected const int ScrollStateUserHasScrolled = 8;

	/// <summary>Determines whether the value of the <see cref="P:System.Windows.Forms.ScrollableControl.VScroll" /> property is set to true.</summary>
	protected const int ScrollStateVScrollVisible = 4;

	private bool force_hscroll_visible;

	private bool force_vscroll_visible;

	private bool auto_scroll;

	private Size auto_scroll_margin;

	private Size auto_scroll_min_size;

	private Point scroll_position;

	private DockPaddingEdges dock_padding;

	private SizeGrip sizegrip;

	internal ImplicitHScrollBar hscrollbar;

	internal ImplicitVScrollBar vscrollbar;

	internal Size canvas_size;

	private Rectangle display_rectangle;

	private Control old_parent;

	private HScrollProperties horizontalScroll;

	private VScrollProperties verticalScroll;

	private static object OnScrollEvent = new object();

	/// <summary>Gets or sets a value indicating whether the container enables the user to scroll to any controls placed outside of its visible boundaries.</summary>
	/// <returns>true if the container enables auto-scrolling; otherwise, false. The default value is false. </returns>
	/// <filterpriority>1</filterpriority>
	[MWFCategory("Layout")]
	[Localizable(true)]
	[DefaultValue(false)]
	public virtual bool AutoScroll
	{
		get
		{
			return auto_scroll;
		}
		set
		{
			if (auto_scroll != value)
			{
				auto_scroll = value;
				PerformLayout(this, "AutoScroll");
			}
		}
	}

	/// <summary>Gets or sets the size of the auto-scroll margin.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the height and width of the auto-scroll margin in pixels.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Drawing.Size.Height" /> or <see cref="P:System.Drawing.Size.Width" /> value assigned is less than 0. </exception>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[MWFCategory("Layout")]
	public Size AutoScrollMargin
	{
		get
		{
			return auto_scroll_margin;
		}
		set
		{
			if (value.Width < 0)
			{
				throw new ArgumentException("Width is assigned less than 0", "value.Width");
			}
			if (value.Height < 0)
			{
				throw new ArgumentException("Height is assigned less than 0", "value.Height");
			}
			auto_scroll_margin = value;
		}
	}

	/// <summary>Gets or sets the minimum size of the auto-scroll.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that determines the minimum size of the virtual area through which the user can scroll.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[MWFCategory("Layout")]
	public Size AutoScrollMinSize
	{
		get
		{
			return auto_scroll_min_size;
		}
		set
		{
			if (value != auto_scroll_min_size)
			{
				auto_scroll_min_size = value;
				AutoScroll = true;
				PerformLayout(this, "AutoScrollMinSize");
			}
		}
	}

	/// <summary>Gets or sets the location of the auto-scroll position.</summary>
	/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the auto-scroll position in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Point AutoScrollPosition
	{
		get
		{
			return DisplayRectangle.Location;
		}
		set
		{
			if (value != AutoScrollPosition)
			{
				int xOffset = 0;
				int yOffset = 0;
				if (hscrollbar.VisibleInternal)
				{
					int num = hscrollbar.Maximum - hscrollbar.LargeChange + 1;
					value.X = ((value.X >= hscrollbar.Minimum) ? value.X : hscrollbar.Minimum);
					value.X = ((value.X <= num) ? value.X : num);
					xOffset = value.X - scroll_position.X;
				}
				if (vscrollbar.VisibleInternal)
				{
					int num2 = vscrollbar.Maximum - vscrollbar.LargeChange + 1;
					value.Y = ((value.Y >= vscrollbar.Minimum) ? value.Y : vscrollbar.Minimum);
					value.Y = ((value.Y <= num2) ? value.Y : num2);
					yOffset = value.Y - scroll_position.Y;
				}
				ScrollWindow(xOffset, yOffset);
				if (hscrollbar.VisibleInternal && scroll_position.X >= hscrollbar.Minimum && scroll_position.X <= hscrollbar.Maximum)
				{
					hscrollbar.Value = scroll_position.X;
				}
				if (vscrollbar.VisibleInternal && scroll_position.Y >= vscrollbar.Minimum && scroll_position.Y <= vscrollbar.Maximum)
				{
					vscrollbar.Value = scroll_position.Y;
				}
			}
		}
	}

	/// <summary>Gets the rectangle that represents the virtual display area of the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the display area of the control.</returns>
	/// <filterpriority>1</filterpriority>
	public override Rectangle DisplayRectangle
	{
		get
		{
			if (auto_scroll)
			{
				int num;
				if (canvas_size.Width <= base.DisplayRectangle.Width)
				{
					num = base.DisplayRectangle.Width;
					if (vscrollbar.VisibleInternal)
					{
						num -= vscrollbar.Width;
					}
				}
				else
				{
					num = canvas_size.Width;
				}
				int num2;
				if (canvas_size.Height <= base.DisplayRectangle.Height)
				{
					num2 = base.DisplayRectangle.Height;
					if (hscrollbar.VisibleInternal)
					{
						num2 -= hscrollbar.Height;
					}
				}
				else
				{
					num2 = canvas_size.Height;
				}
				display_rectangle.X = -scroll_position.X;
				display_rectangle.Y = -scroll_position.Y;
				display_rectangle.Width = Math.Max(auto_scroll_min_size.Width, num);
				display_rectangle.Height = Math.Max(auto_scroll_min_size.Height, num2);
			}
			else
			{
				display_rectangle = base.DisplayRectangle;
			}
			display_rectangle.X += dock_padding.Left;
			display_rectangle.Y += dock_padding.Top;
			display_rectangle.Width -= dock_padding.Left + dock_padding.Right;
			display_rectangle.Height -= dock_padding.Top + dock_padding.Bottom;
			return display_rectangle;
		}
	}

	/// <summary>Gets the dock padding settings for all edges of the control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ScrollableControl.DockPaddingEdges" /> that represents the padding for all the edges of a docked control.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[MWFCategory("Layout")]
	[Browsable(false)]
	public DockPaddingEdges DockPadding => dock_padding;

	/// <summary>Gets the characteristics associated with the horizontal scroll bar.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.HScrollProperties" /> that contains information about the horizontal scroll bar.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(false)]
	public HScrollProperties HorizontalScroll => horizontalScroll;

	/// <summary>Gets the characteristics associated with the vertical scroll bar.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.VScrollProperties" /> that contains information about the vertical scroll bar.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public VScrollProperties VerticalScroll => verticalScroll;

	/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
	protected override CreateParams CreateParams => base.CreateParams;

	/// <summary>Gets or sets a value indicating whether the horizontal scroll bar is visible.</summary>
	/// <returns>true if the horizontal scroll bar is visible; otherwise, false.</returns>
	protected bool HScroll
	{
		get
		{
			return hscrollbar.VisibleInternal;
		}
		set
		{
			if (!AutoScroll && hscrollbar.VisibleInternal != value)
			{
				force_hscroll_visible = value;
				Recalculate(doLayout: false);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the vertical scroll bar is visible.</summary>
	/// <returns>true if the vertical scroll bar is visible; otherwise, false.</returns>
	protected bool VScroll
	{
		get
		{
			return vscrollbar.VisibleInternal;
		}
		set
		{
			if (!AutoScroll && vscrollbar.VisibleInternal != value)
			{
				force_vscroll_visible = value;
				Recalculate(doLayout: false);
			}
		}
	}

	/// <summary>Occurs when the user or code scrolls through the client area.</summary>
	/// <filterpriority>1</filterpriority>
	public event ScrollEventHandler Scroll
	{
		add
		{
			base.Events.AddHandler(OnScrollEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(OnScrollEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollableControl" /> class.</summary>
	public ScrollableControl()
	{
		SetStyle(ControlStyles.ContainerControl, value: true);
		SetStyle(ControlStyles.AllPaintingInWmPaint, value: false);
		auto_scroll = false;
		force_hscroll_visible = false;
		force_vscroll_visible = false;
		auto_scroll_margin = new Size(0, 0);
		auto_scroll_min_size = new Size(0, 0);
		scroll_position = new Point(0, 0);
		dock_padding = new DockPaddingEdges(this);
		base.SizeChanged += Recalculate;
		base.VisibleChanged += VisibleChangedHandler;
		base.LocationChanged += LocationChangedHandler;
		base.ParentChanged += ParentChangedHandler;
		base.HandleCreated += AddScrollbars;
		CreateScrollbars();
		horizontalScroll = new HScrollProperties(this);
		verticalScroll = new VScrollProperties(this);
	}

	private void VisibleChangedHandler(object sender, EventArgs e)
	{
		Recalculate(doLayout: false);
	}

	private void LocationChangedHandler(object sender, EventArgs e)
	{
		UpdateSizeGripVisible();
	}

	private void ParentChangedHandler(object sender, EventArgs e)
	{
		if (old_parent != base.Parent)
		{
			if (old_parent != null)
			{
				old_parent.SizeChanged -= Parent_SizeChanged;
				old_parent.PaddingChanged -= Parent_PaddingChanged;
			}
			if (base.Parent != null)
			{
				base.Parent.SizeChanged += Parent_SizeChanged;
				base.Parent.PaddingChanged += Parent_PaddingChanged;
			}
			old_parent = base.Parent;
		}
	}

	private void Parent_PaddingChanged(object sender, EventArgs e)
	{
		UpdateSizeGripVisible();
	}

	private void Parent_SizeChanged(object sender, EventArgs e)
	{
		UpdateSizeGripVisible();
	}

	internal bool ShouldSerializeAutoScrollMargin()
	{
		return AutoScrollMargin != new Size(0, 0);
	}

	internal bool ShouldSerializeAutoScrollMinSize()
	{
		return AutoScrollMinSize != new Size(0, 0);
	}

	/// <summary>Scrolls the specified child control into view on an auto-scroll enabled control.</summary>
	/// <param name="activeControl">The child control to scroll into view. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ScrollControlIntoView(Control activeControl)
	{
		Rectangle rectangle = default(Rectangle);
		rectangle.Size = base.ClientSize;
		if (!AutoScroll || (!hscrollbar.VisibleInternal && !vscrollbar.VisibleInternal) || !Contains(activeControl))
		{
			return;
		}
		if (vscrollbar.Visible)
		{
			rectangle.Width -= vscrollbar.Width;
		}
		if (hscrollbar.Visible)
		{
			rectangle.Height -= hscrollbar.Height;
		}
		if (rectangle.Contains(activeControl.Location) && rectangle.Contains(activeControl.Right, activeControl.Bottom))
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		if (activeControl.Top <= 0 || activeControl.Height >= rectangle.Height)
		{
			num2 = -activeControl.Top;
		}
		else if (activeControl.Bottom > rectangle.Height)
		{
			num2 = rectangle.Height - activeControl.Bottom;
		}
		if (activeControl.Left <= 0 || activeControl.Width >= rectangle.Width)
		{
			num = -activeControl.Left;
		}
		else if (activeControl.Right > rectangle.Width)
		{
			num = rectangle.Width - activeControl.Right;
		}
		int num3 = hscrollbar.Value - num;
		int num4 = vscrollbar.Value - num2;
		if (hscrollbar.VisibleInternal)
		{
			if (num3 > hscrollbar.Maximum)
			{
				num3 = hscrollbar.Maximum;
			}
			else if (num3 < hscrollbar.Minimum)
			{
				num3 = hscrollbar.Minimum;
			}
			if (num3 != hscrollbar.Value)
			{
				hscrollbar.Value = num3;
			}
		}
		if (vscrollbar.VisibleInternal)
		{
			if (num4 > vscrollbar.Maximum)
			{
				num4 = vscrollbar.Maximum;
			}
			else if (num4 < vscrollbar.Minimum)
			{
				num4 = vscrollbar.Minimum;
			}
			if (num4 != vscrollbar.Value)
			{
				vscrollbar.Value = num4;
			}
		}
	}

	/// <summary>Sets the size of the auto-scroll margins.</summary>
	/// <param name="x">The <see cref="P:System.Drawing.Size.Width" /> value. </param>
	/// <param name="y">The <see cref="P:System.Drawing.Size.Height" /> value. </param>
	/// <filterpriority>1</filterpriority>
	public void SetAutoScrollMargin(int x, int y)
	{
		if (x < 0)
		{
			x = 0;
		}
		if (y < 0)
		{
			y = 0;
		}
		auto_scroll_margin = new Size(x, y);
		Recalculate(doLayout: false);
	}

	/// <summary>Adjusts the scroll bars on the container based on the current control positions and the control currently selected. </summary>
	/// <param name="displayScrollbars">true to show the scroll bars; otherwise, false. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void AdjustFormScrollbars(bool displayScrollbars)
	{
		Recalculate(doLayout: false);
	}

	/// <summary>Determines whether the specified flag has been set.</summary>
	/// <returns>true if the specified flag has been set; otherwise, false.</returns>
	/// <param name="bit">The flag to check.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected bool GetScrollState(int bit)
	{
		return false;
	}

	/// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnLayout(LayoutEventArgs levent)
	{
		CalculateCanvasSize(canOverride: true);
		AdjustFormScrollbars(AutoScroll);
		base.OnLayout(levent);
		if (this is FlowLayoutPanel)
		{
			CalculateCanvasSize(canOverride: false);
			AdjustFormScrollbars(AutoScroll);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnMouseWheel(MouseEventArgs e)
	{
		if (vscrollbar.VisibleInternal)
		{
			if (e.Delta > 0)
			{
				if (vscrollbar.Minimum < vscrollbar.Value - vscrollbar.LargeChange)
				{
					vscrollbar.Value -= vscrollbar.LargeChange;
				}
				else
				{
					vscrollbar.Value = vscrollbar.Minimum;
				}
			}
			else
			{
				int num = vscrollbar.Maximum - vscrollbar.LargeChange + 1;
				if (num > vscrollbar.Value + vscrollbar.LargeChange)
				{
					vscrollbar.Value += vscrollbar.LargeChange;
				}
				else
				{
					vscrollbar.Value = num;
				}
			}
		}
		base.OnMouseWheel(e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnVisibleChanged(EventArgs e)
	{
		if (base.Visible)
		{
			UpdateChildrenZOrder();
			PerformLayout(this, "Visible");
		}
		base.OnVisibleChanged(e);
	}

	/// <param name="dx">The horizontal scaling factor.</param>
	/// <param name="dy">The vertical scaling factor.</param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected override void ScaleCore(float dx, float dy)
	{
		dock_padding.Scale(dx, dy);
		base.ScaleCore(dx, dy);
	}

	/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
	/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
	protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
	{
		base.ScaleControl(factor, specified);
	}

	/// <summary>Calculates the scroll offset to the specified child control. </summary>
	/// <returns>The upper-left hand <see cref="T:System.Drawing.Point" /> of the display area relative to the client area required to scroll the control into view.</returns>
	/// <param name="activeControl">The child control to scroll into view. </param>
	protected virtual Point ScrollToControl(Control activeControl)
	{
		Rectangle rectangle = default(Rectangle);
		rectangle.Size = base.ClientSize;
		if (vscrollbar.Visible)
		{
			rectangle.Width -= vscrollbar.Width;
		}
		if (hscrollbar.Visible)
		{
			rectangle.Height -= hscrollbar.Height;
		}
		int num = 0;
		int num2 = 0;
		if (activeControl.Top <= 0 || activeControl.Height >= rectangle.Height)
		{
			num2 = -activeControl.Top;
		}
		else if (activeControl.Bottom > rectangle.Height)
		{
			num2 = rectangle.Height - activeControl.Bottom;
		}
		if (activeControl.Left <= 0 || activeControl.Width >= rectangle.Width)
		{
			num = -activeControl.Left;
		}
		else if (activeControl.Right > rectangle.Width)
		{
			num = rectangle.Width - activeControl.Right;
		}
		int x = AutoScrollPosition.X + num;
		int y = AutoScrollPosition.Y + num2;
		return new Point(x, y);
	}

	/// <summary>Positions the display window to the specified value.</summary>
	/// <param name="x">The horizontal offset at which to position the <see cref="T:System.Windows.Forms.ScrollableControl" />.</param>
	/// <param name="y">The vertical offset at which to position the <see cref="T:System.Windows.Forms.ScrollableControl" />.</param>
	protected void SetDisplayRectLocation(int x, int y)
	{
		if (x > 0)
		{
			x = 0;
		}
		if (y > 0)
		{
			y = 0;
		}
		ScrollWindow(scroll_position.X - x, scroll_position.Y - y);
	}

	/// <summary>Sets the specified scroll state flag.</summary>
	/// <param name="bit">The scroll state flag to set. </param>
	/// <param name="value">The value to set the flag. </param>
	protected void SetScrollState(int bit, bool value)
	{
	}

	/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
	}

	internal override IntPtr AfterTopMostControl()
	{
		if (hscrollbar != null && hscrollbar.Visible)
		{
			return hscrollbar.Handle;
		}
		if (vscrollbar != null && vscrollbar.Visible)
		{
			return hscrollbar.Handle;
		}
		return base.AfterTopMostControl();
	}

	internal virtual void CalculateCanvasSize(bool canOverride)
	{
		int count = base.Controls.Count;
		int num = 0;
		int num2 = 0;
		int num3 = dock_padding.Right + hscrollbar.Value;
		int num4 = dock_padding.Bottom + vscrollbar.Value;
		for (int i = 0; i < count; i++)
		{
			Control control = base.Controls[i];
			if (control.Dock == DockStyle.Right)
			{
				num3 += control.Width;
			}
			else if (control.Dock == DockStyle.Bottom)
			{
				num4 += control.Height;
			}
		}
		if (!auto_scroll_min_size.IsEmpty)
		{
			num = auto_scroll_min_size.Width;
			num2 = auto_scroll_min_size.Height;
		}
		for (int j = 0; j < count; j++)
		{
			Control control = base.Controls[j];
			switch (control.Dock)
			{
			case DockStyle.Left:
				if (control.Right + num3 > num)
				{
					num = control.Right + num3;
				}
				continue;
			case DockStyle.Top:
				if (control.Bottom + num4 > num2)
				{
					num2 = control.Bottom + num4;
				}
				continue;
			case DockStyle.Bottom:
			case DockStyle.Right:
			case DockStyle.Fill:
				continue;
			}
			AnchorStyles anchor = control.Anchor;
			if ((anchor & AnchorStyles.Left) != 0 && (anchor & AnchorStyles.Right) == 0 && control.Right + num3 > num)
			{
				num = control.Right + num3;
			}
			if (((anchor & AnchorStyles.Top) != 0 || (anchor & AnchorStyles.Bottom) == 0) && control.Bottom + num4 > num2)
			{
				num2 = control.Bottom + num4;
			}
		}
		canvas_size.Width = num;
		canvas_size.Height = num2;
	}

	private void Recalculate(object sender, EventArgs e)
	{
		Recalculate(doLayout: true);
	}

	private void Recalculate(bool doLayout)
	{
		if (!base.IsHandleCreated)
		{
			return;
		}
		Size size = canvas_size;
		Size clientSize = base.ClientSize;
		size.Width += auto_scroll_margin.Width;
		size.Height += auto_scroll_margin.Height;
		int num = clientSize.Width;
		int num2 = clientSize.Height;
		int num3;
		int num4;
		bool flag;
		bool flag2;
		do
		{
			num3 = num;
			num4 = num2;
			if ((force_hscroll_visible || (size.Width > num && auto_scroll)) && clientSize.Width > 0)
			{
				flag = true;
				num2 = clientSize.Height - SystemInformation.HorizontalScrollBarHeight;
			}
			else
			{
				flag = false;
				num2 = clientSize.Height;
			}
			if ((force_vscroll_visible || (size.Height > num2 && auto_scroll)) && clientSize.Height > 0)
			{
				flag2 = true;
				num = clientSize.Width - SystemInformation.VerticalScrollBarWidth;
			}
			else
			{
				flag2 = false;
				num = clientSize.Width;
			}
		}
		while (num != num3 || num2 != num4);
		if (num < 0)
		{
			num = 0;
		}
		if (num2 < 0)
		{
			num2 = 0;
		}
		Rectangle rectangle = new Rectangle(0, clientSize.Height - SystemInformation.HorizontalScrollBarHeight, base.ClientRectangle.Width, SystemInformation.HorizontalScrollBarHeight);
		Rectangle rectangle2 = new Rectangle(clientSize.Width - SystemInformation.VerticalScrollBarWidth, 0, SystemInformation.VerticalScrollBarWidth, base.ClientRectangle.Height);
		if (!vscrollbar.Visible)
		{
			vscrollbar.Value = 0;
		}
		if (!hscrollbar.Visible)
		{
			hscrollbar.Value = 0;
		}
		if (flag)
		{
			hscrollbar.manual_thumb_size = num;
			hscrollbar.LargeChange = num;
			hscrollbar.SmallChange = 5;
			hscrollbar.Maximum = size.Width - 1;
		}
		else
		{
			if (hscrollbar != null && hscrollbar.VisibleInternal)
			{
				ScrollWindow(-scroll_position.X, 0);
			}
			scroll_position.X = 0;
		}
		if (flag2)
		{
			vscrollbar.manual_thumb_size = num2;
			vscrollbar.LargeChange = num2;
			vscrollbar.SmallChange = 5;
			vscrollbar.Maximum = size.Height - 1;
		}
		else
		{
			if (vscrollbar != null && vscrollbar.VisibleInternal)
			{
				ScrollWindow(0, -scroll_position.Y);
			}
			scroll_position.Y = 0;
		}
		if (flag && flag2)
		{
			rectangle.Width -= SystemInformation.VerticalScrollBarWidth;
			rectangle2.Height -= SystemInformation.HorizontalScrollBarHeight;
			sizegrip.Bounds = new Rectangle(rectangle.Right, rectangle2.Bottom, SystemInformation.VerticalScrollBarWidth, SystemInformation.HorizontalScrollBarHeight);
		}
		SuspendLayout();
		hscrollbar.SetBoundsInternal(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, BoundsSpecified.None);
		hscrollbar.Visible = flag;
		if (hscrollbar.Visible)
		{
			XplatUI.SetZOrder(hscrollbar.Handle, IntPtr.Zero, Top: true, Bottom: false);
		}
		vscrollbar.SetBoundsInternal(rectangle2.X, rectangle2.Y, rectangle2.Width, rectangle2.Height, BoundsSpecified.None);
		vscrollbar.Visible = flag2;
		if (vscrollbar.Visible)
		{
			XplatUI.SetZOrder(vscrollbar.Handle, IntPtr.Zero, Top: true, Bottom: false);
		}
		UpdateSizeGripVisible();
		ResumeLayout(doLayout);
		if (this is ContainerControl { ActiveControl: not null } containerControl)
		{
			ScrollControlIntoView(containerControl.ActiveControl);
		}
	}

	internal void UpdateSizeGripVisible()
	{
		if (base.IsHandleCreated)
		{
			sizegrip.CapturedControl = base.Parent;
			bool flag = hscrollbar.VisibleInternal && vscrollbar.VisibleInternal;
			bool flag2 = false;
			if (flag && base.Parent != null)
			{
				Point point = new Point(base.Parent.ClientRectangle.Bottom - base.Bottom, base.Parent.ClientRectangle.Right - base.Right);
				flag2 = point.X <= 2 && point.X >= 0 && point.Y <= 2 && point.Y >= 0;
			}
			sizegrip.Visible = flag;
			sizegrip.Enabled = flag2 || sizegrip.Capture;
			if (sizegrip.Visible)
			{
				XplatUI.SetZOrder(sizegrip.Handle, vscrollbar.Handle, Top: false, Bottom: false);
			}
		}
	}

	private void HandleScrollBar(object sender, EventArgs e)
	{
		if (sender == vscrollbar)
		{
			if (vscrollbar.Visible)
			{
				ScrollWindow(0, vscrollbar.Value - scroll_position.Y);
			}
		}
		else if (hscrollbar.Visible)
		{
			ScrollWindow(hscrollbar.Value - scroll_position.X, 0);
		}
	}

	private void HandleScrollEvent(object sender, ScrollEventArgs args)
	{
		OnScroll(args);
	}

	private void AddScrollbars(object o, EventArgs e)
	{
		base.Controls.AddRangeImplicit(new Control[3] { hscrollbar, vscrollbar, sizegrip });
		base.HandleCreated -= AddScrollbars;
	}

	private void CreateScrollbars()
	{
		hscrollbar = new ImplicitHScrollBar();
		hscrollbar.Visible = false;
		hscrollbar.ValueChanged += HandleScrollBar;
		hscrollbar.Height = SystemInformation.HorizontalScrollBarHeight;
		hscrollbar.use_manual_thumb_size = true;
		hscrollbar.Scroll += HandleScrollEvent;
		vscrollbar = new ImplicitVScrollBar();
		vscrollbar.Visible = false;
		vscrollbar.ValueChanged += HandleScrollBar;
		vscrollbar.Width = SystemInformation.VerticalScrollBarWidth;
		vscrollbar.use_manual_thumb_size = true;
		vscrollbar.Scroll += HandleScrollEvent;
		sizegrip = new SizeGrip(this);
		sizegrip.Visible = false;
	}

	private void ScrollWindow(int XOffset, int YOffset)
	{
		if (XOffset != 0 || YOffset != 0)
		{
			SuspendLayout();
			int count = base.Controls.Count;
			for (int i = 0; i < count; i++)
			{
				base.Controls[i].Location = new Point(base.Controls[i].Left - XOffset, base.Controls[i].Top - YOffset);
			}
			scroll_position.X += XOffset;
			scroll_position.Y += YOffset;
			XplatUI.ScrollWindow(Handle, base.ClientRectangle, -XOffset, -YOffset, with_children: false);
			ResumeLayout(performLayout: false);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ScrollableControl.Scroll" /> event.</summary>
	/// <param name="se">A <see cref="T:System.Windows.Forms.ScrollEventArgs" /> that contains the event data. </param>
	protected virtual void OnScroll(ScrollEventArgs se)
	{
		((ScrollEventHandler)base.Events[OnScrollEvent])?.Invoke(this, se);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.PaddingChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnPaddingChanged(EventArgs e)
	{
		base.OnPaddingChanged(e);
	}

	/// <summary>Paints the background of the control.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
	protected override void OnPaintBackground(PaintEventArgs e)
	{
		base.OnPaintBackground(e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnRightToLeftChanged(EventArgs e)
	{
		base.OnRightToLeftChanged(e);
	}
}
