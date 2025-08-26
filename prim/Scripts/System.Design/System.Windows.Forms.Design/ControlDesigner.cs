using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design;

/// <summary>Extends the design mode behavior of a <see cref="T:System.Windows.Forms.Control" />.</summary>
public class ControlDesigner : ComponentDesigner, IMessageReceiver
{
	/// <summary>Provides an <see cref="T:System.Windows.Forms.AccessibleObject" /> for <see cref="T:System.Windows.Forms.Design.ControlDesigner" />.</summary>
	[ComVisible(true)]
	public class ControlDesignerAccessibleObject : AccessibleObject
	{
		/// <summary>Gets the points that define the boundaries of the accessible object for the designer.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that indicates the boundaries of the accessible object for the designer.</returns>
		[System.MonoTODO]
		public override Rectangle Bounds
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a string that describes the default action of the specified object.</summary>
		/// <returns>A description of the default action for a specified object.</returns>
		[System.MonoTODO]
		public override string DefaultAction
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a string that describes the visual appearance of the specified object.</summary>
		/// <returns>A description of the object's visual appearance to the user, or <see langword="null" /> if the object does not have a description.</returns>
		[System.MonoTODO]
		public override string Description
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the object name.</summary>
		/// <returns>The object name, or <see langword="null" /> if the property has not been set.</returns>
		[System.MonoTODO]
		public override string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the parent of an accessible object.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the parent of an accessible object, or <see langword="null" /> if there is no parent object.</returns>
		[System.MonoTODO]
		public override AccessibleObject Parent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the role of this accessible object.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values, or <see cref="F:System.Windows.Forms.AccessibleRole.None" /> if no role has been specified.</returns>
		[System.MonoTODO]
		public override AccessibleRole Role
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the state of this accessible object.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values, or <see cref="F:System.Windows.Forms.AccessibleStates.None" />, if no state has been set.</returns>
		[System.MonoTODO]
		public override AccessibleStates State
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the value of an accessible object.</summary>
		/// <returns>The value of an accessible object, or <see langword="null" /> if the object has no value set.</returns>
		[System.MonoTODO]
		public override string Value
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ControlDesigner.ControlDesignerAccessibleObject" /> class using the specified designer and control.</summary>
		/// <param name="designer">The <see cref="T:System.Windows.Forms.Design.ControlDesigner" /> for the accessible object.</param>
		/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> for the accessible object.</param>
		[System.MonoTODO]
		public ControlDesignerAccessibleObject(ControlDesigner designer, Control control)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the accessible child corresponding to the specified index.</summary>
		/// <param name="index">The zero-based index of the accessible child.</param>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the accessible child corresponding to the specified index.</returns>
		[System.MonoTODO]
		public override AccessibleObject GetChild(int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of children belonging to an accessible object.</summary>
		/// <returns>The number of children belonging to an accessible object.</returns>
		[System.MonoTODO]
		public override int GetChildCount()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the object that has the keyboard focus.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that specifies the currently focused child. This method returns the calling object if the object itself is focused. Returns <see langword="null" /> if no object has focus.</returns>
		[System.MonoTODO]
		public override AccessibleObject GetFocused()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the currently selected child.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the currently selected child. This method returns the calling object if the object itself is selected. Returns <see langword="null" /> if is no child is currently selected and the object itself does not have focus.</returns>
		[System.MonoTODO]
		public override AccessibleObject GetSelected()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the child object at the specified screen coordinates.</summary>
		/// <param name="x">The horizontal screen coordinate.</param>
		/// <param name="y">The vertical screen coordinate.</param>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the child object at the given screen coordinates. This method returns the calling object if the object itself is at the location specified. Returns <see langword="null" /> if no object is at the tested location.</returns>
		[System.MonoTODO]
		public override AccessibleObject HitTest(int x, int y)
		{
			throw new NotImplementedException();
		}
	}

	private WndProcRouter _messageRouter;

	private bool _locked;

	private bool _mouseDown;

	private bool _mouseMoveAfterMouseDown;

	private bool _mouseDownFirstMove;

	private bool _firstMouseMoveInClient = true;

	/// <summary>Defines a local <see cref="T:System.Drawing.Point" /> that represents the values of an invalid <see cref="T:System.Drawing.Point" />.</summary>
	protected static readonly Point InvalidPoint = new Point(int.MinValue, int.MinValue);

	/// <summary>Specifies the accessibility object for the designer.</summary>
	protected AccessibleObject accessibilityObj;

	private MouseButtons _mouseButtonDown;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> from the design environment.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />, or <see langword="null" /> if the service is unavailable.</returns>
	protected internal BehaviorService BehaviorService
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the control that the designer is designing.</summary>
	/// <returns>The control that the designer is designing.</returns>
	public virtual Control Control => (Control)base.Component;

	/// <summary>Gets a value indicating whether drag rectangles can be drawn on this designer component.</summary>
	/// <returns>
	///   <see langword="true" /> if drag rectangles can be drawn; otherwise, <see langword="false" />.</returns>
	protected virtual bool EnableDragRect => true;

	/// <summary>Gets the selection rules that indicate the movement capabilities of a component.</summary>
	/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.Design.SelectionRules" /> values.</returns>
	public virtual SelectionRules SelectionRules
	{
		get
		{
			if (Control == null)
			{
				return SelectionRules.None;
			}
			SelectionRules selectionRules = SelectionRules.Visible;
			if ((bool)GetValue(base.Component, "Locked"))
			{
				selectionRules |= SelectionRules.Locked;
			}
			else
			{
				switch ((DockStyle)GetValue(base.Component, "Dock", typeof(DockStyle)))
				{
				case DockStyle.Top:
					selectionRules |= SelectionRules.BottomSizeable;
					break;
				case DockStyle.Left:
					selectionRules |= SelectionRules.RightSizeable;
					break;
				case DockStyle.Right:
					selectionRules |= SelectionRules.LeftSizeable;
					break;
				case DockStyle.Bottom:
					selectionRules |= SelectionRules.TopSizeable;
					break;
				default:
					selectionRules |= SelectionRules.Moveable;
					selectionRules |= SelectionRules.AllSizeable;
					break;
				case DockStyle.Fill:
					break;
				}
			}
			return selectionRules;
		}
	}

	/// <summary>Gets the collection of components associated with the component managed by the designer.</summary>
	/// <returns>The components that are associated with the component managed by the designer.</returns>
	public override ICollection AssociatedComponents
	{
		get
		{
			ArrayList arrayList = new ArrayList();
			foreach (Control control in Control.Controls)
			{
				if (control.Site != null)
				{
					arrayList.Add(control);
				}
			}
			return arrayList;
		}
	}

	/// <summary>Gets the parent component for the <see cref="T:System.Windows.Forms.Design.ControlDesigner" />.</summary>
	/// <returns>The parent component for the <see cref="T:System.Windows.Forms.Design.ControlDesigner" />; otherwise, <see langword="null" /> if there is no parent component.</returns>
	protected override IComponent ParentComponent => GetValue(Control, "Parent") as Control;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.AccessibleObject" /> assigned to the control.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.AccessibleObject" /> assigned to the control.</returns>
	public virtual AccessibleObject AccessibilityObject
	{
		get
		{
			if (accessibilityObj == null)
			{
				accessibilityObj = new AccessibleObject();
			}
			return accessibilityObj;
		}
	}

	internal MouseButtons MouseButtonDown => _mouseButtonDown;

	private bool Visible
	{
		get
		{
			return (bool)base.ShadowProperties["Visible"];
		}
		set
		{
			base.ShadowProperties["Visible"] = value;
		}
	}

	private bool Enabled
	{
		get
		{
			return (bool)base.ShadowProperties["Enabled"];
		}
		set
		{
			base.ShadowProperties["Enabled"] = value;
		}
	}

	private bool Locked
	{
		get
		{
			return _locked;
		}
		set
		{
			_locked = value;
		}
	}

	private bool AllowDrop
	{
		get
		{
			return (bool)base.ShadowProperties["AllowDrop"];
		}
		set
		{
			base.ShadowProperties["AllowDrop"] = value;
		}
	}

	private string Name
	{
		get
		{
			return base.Component.Site.Name;
		}
		set
		{
			base.Component.Site.Name = value;
		}
	}

	private ContextMenu ContextMenu
	{
		get
		{
			return (ContextMenu)base.ShadowProperties["ContextMenu"];
		}
		set
		{
			base.ShadowProperties["ContextMenu"] = value;
		}
	}

	private Point Location
	{
		get
		{
			return Control.Location;
		}
		set
		{
			Control.Location = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.ComponentModel.InheritanceAttribute" /> of the designer.</summary>
	/// <returns>
	///   <see cref="F:System.ComponentModel.InheritanceAttribute.Inherited" /> if the designer is a root designer; otherwise, the value of the parent designer's <see cref="P:System.ComponentModel.Design.ComponentDesigner.InheritanceAttribute" /> property.</returns>
	[System.MonoTODO]
	protected override InheritanceAttribute InheritanceAttribute
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a list of <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> objects representing significant alignment points for this control.</summary>
	/// <returns>A list of <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> objects representing significant alignment points for this control.</returns>
	[System.MonoTODO]
	public virtual IList SnapLines
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.Design.ControlDesigner" /> will allow snapline alignment during a drag operation.</summary>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.Design.ControlDesigner" /> will allow snapline alignment during a drag operation when the primary drag control is over this designer; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public virtual bool ParticipatesWithSnapLines
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value indicating whether resize handle allocation depends on the value of the <see cref="P:System.Windows.Forms.Control.AutoSize" /> property.</summary>
	/// <returns>
	///   <see langword="true" /> if resize handle allocation depends on the value of the <see cref="P:System.Windows.Forms.Control.AutoSize" /> and <see langword="AutoSizeMode" /> properties; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[System.MonoTODO]
	public bool AutoResizeHandles
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ControlDesigner" /> class.</summary>
	public ControlDesigner()
	{
	}

	/// <summary>Initializes the designer with the specified component.</summary>
	/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to associate the designer with. This component must always be an instance of, or derive from, <see cref="T:System.Windows.Forms.Control" />.</param>
	public override void Initialize(IComponent component)
	{
		base.Initialize(component);
		if (!(component is Control))
		{
			throw new ArgumentException("Component is not a Control.");
		}
		Control.Text = component.Site.Name;
		_messageRouter = new WndProcRouter((Control)component, this);
		Control.WindowTarget = _messageRouter;
		Visible = true;
		Enabled = true;
		Locked = false;
		AllowDrop = false;
		Control.Enabled = true;
		Control.Visible = true;
		Control.AllowDrop = false;
		Control.DragDrop += OnDragDrop;
		Control.DragEnter += OnDragEnter;
		Control.DragLeave += OnDragLeave;
		Control.DragOver += OnDragOver;
		if (Control.IsHandleCreated)
		{
			OnCreateHandle();
		}
	}

	/// <summary>Called when the designer is intialized.</summary>
	public override void OnSetComponentDefaults()
	{
		if (base.Component != null && base.Component.Site != null)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Text"];
			if (propertyDescriptor != null && !propertyDescriptor.IsReadOnly && propertyDescriptor.PropertyType == typeof(string))
			{
				propertyDescriptor.SetValue(base.Component, base.Component.Site.Name);
			}
		}
	}

	/// <summary>Provides default processing for Windows messages.</summary>
	/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> to process.</param>
	protected void DefWndProc(ref Message m)
	{
		_messageRouter.ToControl(ref m);
	}

	/// <summary>Processes Windows messages.</summary>
	/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> to process.</param>
	protected void BaseWndProc(ref Message m)
	{
		_messageRouter.ToSystem(ref m);
	}

	void IMessageReceiver.WndProc(ref Message m)
	{
		WndProc(ref m);
	}

	/// <summary>Processes Windows messages and optionally routes them to the control.</summary>
	/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> to process.</param>
	protected virtual void WndProc(ref Message m)
	{
		if (m.Msg >= 256 && m.Msg <= 264)
		{
			return;
		}
		if (IsMouseMessage((Native.Msg)m.Msg) && GetHitTest(new Point(Native.LoWord((int)m.LParam), Native.HiWord((int)m.LParam))))
		{
			DefWndProc(ref m);
			return;
		}
		switch ((Native.Msg)m.Msg)
		{
		case Native.Msg.WM_CREATE:
			DefWndProc(ref m);
			if (m.HWnd == Control.Handle)
			{
				OnCreateHandle();
			}
			break;
		case Native.Msg.WM_CONTEXTMENU:
			OnContextMenu(Native.LoWord((int)m.LParam), Native.HiWord((int)m.LParam));
			break;
		case Native.Msg.WM_SETCURSOR:
			if (GetHitTest(new Point(Native.LoWord((int)m.LParam), Native.HiWord((int)m.LParam))))
			{
				DefWndProc(ref m);
			}
			else
			{
				OnSetCursor();
			}
			break;
		case Native.Msg.WM_SETFOCUS:
			DefWndProc(ref m);
			break;
		case Native.Msg.WM_PAINT:
		{
			DefWndProc(ref m);
			Graphics graphics = Graphics.FromHwnd(m.HWnd);
			PaintEventArgs paintEventArgs = new PaintEventArgs(graphics, Control.Bounds);
			OnPaintAdornments(paintEventArgs);
			graphics.Dispose();
			paintEventArgs.Dispose();
			break;
		}
		case Native.Msg.WM_LBUTTONDBLCLK:
		case Native.Msg.WM_RBUTTONDBLCLK:
		case Native.Msg.WM_MBUTTONDBLCLK:
			if (m.Msg == 515)
			{
				_mouseButtonDown = MouseButtons.Left;
			}
			else if (m.Msg == 518)
			{
				_mouseButtonDown = MouseButtons.Right;
			}
			else if (m.Msg == 521)
			{
				_mouseButtonDown = MouseButtons.Middle;
			}
			OnMouseDoubleClick();
			BaseWndProc(ref m);
			break;
		case Native.Msg.WM_MOUSEHOVER:
			OnMouseHover();
			break;
		case Native.Msg.WM_LBUTTONDOWN:
		case Native.Msg.WM_RBUTTONDOWN:
		case Native.Msg.WM_MBUTTONDOWN:
			_mouseMoveAfterMouseDown = true;
			if (m.Msg == 513)
			{
				_mouseButtonDown = MouseButtons.Left;
			}
			else if (m.Msg == 516)
			{
				_mouseButtonDown = MouseButtons.Right;
			}
			else if (m.Msg == 519)
			{
				_mouseButtonDown = MouseButtons.Middle;
			}
			if (_firstMouseMoveInClient)
			{
				OnMouseEnter();
				_firstMouseMoveInClient = false;
			}
			OnMouseDown(Native.LoWord((int)m.LParam), Native.HiWord((int)m.LParam));
			BaseWndProc(ref m);
			break;
		case Native.Msg.WM_MOUSELEAVE:
			_firstMouseMoveInClient = false;
			OnMouseLeave();
			BaseWndProc(ref m);
			break;
		case Native.Msg.WM_CANCELMODE:
			OnMouseDragEnd(cancel: true);
			DefWndProc(ref m);
			break;
		case Native.Msg.WM_NCLBUTTONUP:
		case Native.Msg.WM_NCRBUTTONUP:
		case Native.Msg.WM_NCMBUTTONUP:
		case Native.Msg.WM_LBUTTONUP:
		case Native.Msg.WM_RBUTTONUP:
		case Native.Msg.WM_MBUTTONUP:
			_mouseMoveAfterMouseDown = false;
			OnMouseUp();
			BaseWndProc(ref m);
			break;
		case Native.Msg.WM_MOUSEFIRST:
		{
			if (_mouseMoveAfterMouseDown)
			{
				_mouseMoveAfterMouseDown = false;
				BaseWndProc(ref m);
				break;
			}
			IUISelectionService iUISelectionService = GetService(typeof(IUISelectionService)) as IUISelectionService;
			ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
			IDesignerHost designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (iUISelectionService != null && selectionService != null && designerHost != null)
			{
				Control control = selectionService.PrimarySelection as Control;
				Point p = new Point(Native.LoWord((int)m.LParam), Native.HiWord((int)m.LParam));
				if (iUISelectionService.SelectionInProgress && base.Component != designerHost.RootComponent && base.Component != selectionService.PrimarySelection)
				{
					p = control.PointToClient(Control.PointToScreen(p));
					Native.SendMessage(control.Handle, (Native.Msg)m.Msg, m.WParam, Native.LParam(p.X, p.Y));
				}
				else if (iUISelectionService.ResizeInProgress && Control.Parent == ((Control)selectionService.PrimarySelection).Parent)
				{
					p = Control.Parent.PointToClient(Control.PointToScreen(p));
					Native.SendMessage(Control.Parent.Handle, (Native.Msg)m.Msg, m.WParam, Native.LParam(p.X, p.Y));
				}
				else
				{
					OnMouseMove(p.X, p.Y);
				}
			}
			else
			{
				OnMouseMove(Native.LoWord((int)m.LParam), Native.HiWord((int)m.LParam));
			}
			BaseWndProc(ref m);
			break;
		}
		default:
			DefWndProc(ref m);
			break;
		case Native.Msg.WM_NCLBUTTONDOWN:
		case Native.Msg.WM_NCLBUTTONDBLCLK:
		case Native.Msg.WM_NCRBUTTONDOWN:
		case Native.Msg.WM_NCRBUTTONDBLCLK:
		case Native.Msg.WM_NCMBUTTONDOWN:
			break;
		}
	}

	/// <summary>Indicates whether a mouse click at the specified point should be handled by the control.</summary>
	/// <param name="point">A <see cref="T:System.Drawing.Point" /> indicating the position at which the mouse was clicked, in screen coordinates.</param>
	/// <returns>
	///   <see langword="true" /> if a click at the specified point is to be handled by the control; otherwise, <see langword="false" />.</returns>
	protected virtual bool GetHitTest(Point point)
	{
		return false;
	}

	private bool IsMouseMessage(Native.Msg msg)
	{
		if (msg >= Native.Msg.WM_MOUSEFIRST && msg <= Native.Msg.WM_MOUSEWHEEL)
		{
			return true;
		}
		if (msg >= Native.Msg.WM_NCLBUTTONDOWN && msg <= Native.Msg.WM_NCMBUTTONDBLCLK)
		{
			return true;
		}
		if (msg == Native.Msg.WM_MOUSEHOVER || msg == Native.Msg.WM_MOUSELEAVE)
		{
			return true;
		}
		return false;
	}

	/// <summary>Receives a call each time the cursor needs to be set.</summary>
	protected virtual void OnSetCursor()
	{
	}

	private void OnMouseDoubleClick()
	{
		try
		{
			base.DoDefaultAction();
		}
		catch (Exception e)
		{
			DisplayError(e);
		}
	}

	internal virtual void OnMouseDown(int x, int y)
	{
		_mouseDown = true;
		_mouseDownFirstMove = true;
		if ((!(GetService(typeof(IUISelectionService)) is IUISelectionService iUISelectionService) || !iUISelectionService.AdornmentsHitTest(Control, x, y)) && GetService(typeof(ISelectionService)) is ISelectionService selectionService)
		{
			selectionService.SetSelectedComponents(new IComponent[1] { base.Component });
		}
	}

	internal virtual void OnMouseMove(int x, int y)
	{
		if (_mouseDown)
		{
			if (_mouseDownFirstMove)
			{
				OnMouseDragBegin(x, y);
				_mouseDownFirstMove = false;
			}
			else
			{
				OnMouseDragMove(x, y);
			}
		}
	}

	internal virtual void OnMouseUp()
	{
		IUISelectionService iUISelectionService = GetService(typeof(IUISelectionService)) as IUISelectionService;
		if (_mouseDown)
		{
			OnMouseDragEnd(cancel: false);
			if (iUISelectionService != null && (iUISelectionService.SelectionInProgress || iUISelectionService.ResizeInProgress))
			{
				iUISelectionService.MouseDragEnd(cancel: false);
			}
			_mouseDown = false;
		}
		else if (iUISelectionService != null && (iUISelectionService.SelectionInProgress || iUISelectionService.ResizeInProgress))
		{
			iUISelectionService.MouseDragEnd(cancel: false);
		}
	}

	/// <summary>Shows the context menu and provides an opportunity to perform additional processing when the context menu is about to be displayed.</summary>
	/// <param name="x">The x coordinate at which to display the context menu.</param>
	/// <param name="y">The y coordinate at which to display the context menu.</param>
	protected virtual void OnContextMenu(int x, int y)
	{
		if (GetService(typeof(IMenuCommandService)) is IMenuCommandService menuCommandService)
		{
			menuCommandService.ShowContextMenu(MenuCommands.SelectionMenu, x, y);
		}
	}

	/// <summary>Receives a call when the mouse first enters the control.</summary>
	protected virtual void OnMouseEnter()
	{
	}

	/// <summary>Receives a call after the mouse hovers over the control.</summary>
	protected virtual void OnMouseHover()
	{
	}

	/// <summary>Receives a call when the mouse first enters the control.</summary>
	protected virtual void OnMouseLeave()
	{
	}

	/// <summary>Provides an opportunity to perform additional processing immediately after the control handle has been created.</summary>
	protected virtual void OnCreateHandle()
	{
	}

	/// <summary>Receives a call when the control that the designer is managing has painted its surface so the designer can paint any additional adornments on top of the control.</summary>
	/// <param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> the designer can use to draw on the control.</param>
	protected virtual void OnPaintAdornments(PaintEventArgs pe)
	{
	}

	/// <summary>Receives a call in response to the left mouse button being pressed and held while over the component.</summary>
	/// <param name="x">The x position of the mouse in screen coordinates.</param>
	/// <param name="y">The y position of the mouse in screen coordinates.</param>
	protected virtual void OnMouseDragBegin(int x, int y)
	{
		if (GetService(typeof(IUISelectionService)) is IUISelectionService iUISelectionService && (SelectionRules & SelectionRules.Moveable) == SelectionRules.Moveable)
		{
			iUISelectionService.DragBegin();
		}
	}

	/// <summary>Receives a call for each movement of the mouse during a drag-and-drop operation.</summary>
	/// <param name="x">The x position of the mouse in screen coordinates.</param>
	/// <param name="y">The y position of the mouse in screen coordinates.</param>
	protected virtual void OnMouseDragMove(int x, int y)
	{
	}

	/// <summary>Receives a call at the end of a drag-and-drop operation to complete or cancel the operation.</summary>
	/// <param name="cancel">
	///   <see langword="true" /> to cancel the drag; <see langword="false" /> to commit it.</param>
	protected virtual void OnMouseDragEnd(bool cancel)
	{
	}

	/// <summary>Routes messages from the child controls of the specified control to the designer.</summary>
	/// <param name="firstChild">The first child <see cref="T:System.Windows.Forms.Control" /> to process. This method may recursively call itself for children of the control.</param>
	protected void HookChildControls(Control firstChild)
	{
		if (firstChild == null)
		{
			return;
		}
		foreach (Control control in firstChild.Controls)
		{
			control.WindowTarget = new WndProcRouter(control, this);
		}
	}

	/// <summary>Routes messages for the children of the specified control to each control rather than to a parent designer.</summary>
	/// <param name="firstChild">The first child <see cref="T:System.Windows.Forms.Control" /> to process. This method may recursively call itself for children of the control.</param>
	protected void UnhookChildControls(Control firstChild)
	{
		if (firstChild == null)
		{
			return;
		}
		foreach (Control control in firstChild.Controls)
		{
			if (control.WindowTarget is WndProcRouter)
			{
				((WndProcRouter)control.WindowTarget).Dispose();
			}
		}
	}

	/// <summary>Indicates if this designer's control can be parented by the control of the specified designer.</summary>
	/// <param name="parentDesigner">The <see cref="T:System.ComponentModel.Design.IDesigner" /> that manages the control to check.</param>
	/// <returns>
	///   <see langword="true" /> if the control managed by the specified designer can parent the control managed by this designer; otherwise, <see langword="false" />.</returns>
	public virtual bool CanBeParentedTo(IDesigner parentDesigner)
	{
		IDesignerHost designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
		if (parentDesigner is ParentControlDesigner && base.Component != designerHost.RootComponent && !Control.Controls.Contains(((ParentControlDesigner)parentDesigner).Control))
		{
			return true;
		}
		return false;
	}

	/// <summary>Displays information about the specified exception to the user.</summary>
	/// <param name="e">The <see cref="T:System.Exception" /> to display.</param>
	protected void DisplayError(Exception e)
	{
		if (e == null)
		{
			return;
		}
		if (GetService(typeof(IUIService)) is IUIService iUIService)
		{
			iUIService.ShowError(e);
			return;
		}
		string text = e.Message;
		if (text == null || text == string.Empty)
		{
			text = e.ToString();
		}
		MessageBox.Show(Control, text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
	}

	/// <summary>Enables or disables drag-and-drop support for the control being designed.</summary>
	/// <param name="value">
	///   <see langword="true" /> to enable drag-and-drop support for the control; <see langword="false" /> if the control should not have drag-and-drop support. The default is <see langword="false" />.</param>
	protected void EnableDragDrop(bool value)
	{
		if (Control != null)
		{
			if (value)
			{
				Control.DragDrop += OnDragDrop;
				Control.DragOver += OnDragOver;
				Control.DragEnter += OnDragEnter;
				Control.DragLeave += OnDragLeave;
				Control.GiveFeedback += OnGiveFeedback;
				Control.AllowDrop = true;
			}
			else
			{
				Control.DragDrop -= OnDragDrop;
				Control.DragOver -= OnDragOver;
				Control.DragEnter -= OnDragEnter;
				Control.DragLeave -= OnDragLeave;
				Control.GiveFeedback -= OnGiveFeedback;
				Control.AllowDrop = false;
			}
		}
	}

	private void OnGiveFeedback(object sender, GiveFeedbackEventArgs e)
	{
		OnGiveFeedback(e);
	}

	private void OnDragDrop(object sender, DragEventArgs e)
	{
		OnDragDrop(e);
	}

	private void OnDragEnter(object sender, DragEventArgs e)
	{
		OnDragEnter(e);
	}

	private void OnDragLeave(object sender, EventArgs e)
	{
		OnDragLeave(e);
	}

	private void OnDragOver(object sender, DragEventArgs e)
	{
		OnDragOver(e);
	}

	/// <summary>Receives a call when a drag-and-drop operation is in progress to provide visual cues based on the location of the mouse while a drag operation is in progress.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.GiveFeedbackEventArgs" /> that provides data for the event.</param>
	protected virtual void OnGiveFeedback(GiveFeedbackEventArgs e)
	{
		e.UseDefaultCursors = false;
	}

	/// <summary>Receives a call when a drag-and-drop object is dropped onto the control designer view.</summary>
	/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event.</param>
	protected virtual void OnDragDrop(DragEventArgs de)
	{
	}

	/// <summary>Receives a call when a drag-and-drop operation enters the control designer view.</summary>
	/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event.</param>
	protected virtual void OnDragEnter(DragEventArgs de)
	{
	}

	/// <summary>Receives a call when a drag-and-drop operation leaves the control designer view.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that provides data for the event.</param>
	protected virtual void OnDragLeave(EventArgs e)
	{
	}

	/// <summary>Receives a call when a drag-and-drop object is dragged over the control designer view.</summary>
	/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event.</param>
	protected virtual void OnDragOver(DragEventArgs de)
	{
	}

	/// <summary>Adjusts the set of properties the component exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	/// <param name="properties">An <see cref="T:System.Collections.IDictionary" /> containing the properties for the class of the component.</param>
	protected override void PreFilterProperties(IDictionary properties)
	{
		base.PreFilterProperties(properties);
		string[] array = new string[6] { "Visible", "Enabled", "ContextMenu", "AllowDrop", "Location", "Name" };
		Attribute[][] array2 = new Attribute[6][]
		{
			new Attribute[1]
			{
				new DefaultValueAttribute(value: true)
			},
			new Attribute[1]
			{
				new DefaultValueAttribute(value: true)
			},
			new Attribute[1]
			{
				new DefaultValueAttribute(null)
			},
			new Attribute[1]
			{
				new DefaultValueAttribute(value: false)
			},
			new Attribute[1]
			{
				new DefaultValueAttribute(typeof(Point), "0, 0")
			},
			new Attribute[0]
		};
		PropertyDescriptor propertyDescriptor = null;
		for (int i = 0; i < array.Length; i++)
		{
			if (properties[array[i]] is PropertyDescriptor oldPropertyDescriptor)
			{
				properties[array[i]] = TypeDescriptor.CreateProperty(typeof(ControlDesigner), oldPropertyDescriptor, array2[i]);
			}
		}
		properties["Locked"] = TypeDescriptor.CreateProperty(typeof(ControlDesigner), "Locked", typeof(bool), DesignOnlyAttribute.Yes, BrowsableAttribute.Yes, CategoryAttribute.Design, new DefaultValueAttribute(value: false), new DescriptionAttribute("The Locked property determines if we can move or resize the control."));
	}

	internal object GetValue(object component, string propertyName)
	{
		return GetValue(component, propertyName, null);
	}

	internal object GetValue(object component, string propertyName, Type propertyType)
	{
		PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)[propertyName];
		if (propertyDescriptor == null)
		{
			throw new InvalidOperationException("Property \"" + propertyName + "\" is missing on " + component.GetType().AssemblyQualifiedName);
		}
		if (propertyType != null && !propertyType.IsAssignableFrom(propertyDescriptor.PropertyType))
		{
			throw new InvalidOperationException("Types do not match: " + propertyDescriptor.PropertyType.AssemblyQualifiedName + " : " + propertyType.AssemblyQualifiedName);
		}
		return propertyDescriptor.GetValue(component);
	}

	internal void SetValue(object component, string propertyName, object value)
	{
		PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)[propertyName];
		if (propertyDescriptor == null)
		{
			throw new InvalidOperationException("Property \"" + propertyName + "\" is missing on " + component.GetType().AssemblyQualifiedName);
		}
		if (!propertyDescriptor.PropertyType.IsAssignableFrom(value.GetType()))
		{
			throw new InvalidOperationException("Types do not match: " + value.GetType().AssemblyQualifiedName + " : " + propertyDescriptor.PropertyType.AssemblyQualifiedName);
		}
		if (!propertyDescriptor.IsReadOnly)
		{
			propertyDescriptor.SetValue(component, value);
		}
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Design.ControlDesigner" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && Control != null)
		{
			UnhookChildControls(Control);
			OnMouseDragEnd(cancel: true);
			_messageRouter.Dispose();
			Control.DragDrop -= OnDragDrop;
			Control.DragEnter -= OnDragEnter;
			Control.DragLeave -= OnDragLeave;
			Control.DragOver -= OnDragOver;
		}
		base.Dispose(disposing: true);
	}

	/// <summary>Returns the internal control designer with the specified index in the <see cref="T:System.Windows.Forms.Design.ControlDesigner" />.</summary>
	/// <param name="internalControlIndex">A specified index to select the internal control designer. This index is zero-based.</param>
	/// <returns>A <see cref="T:System.Windows.Forms.Design.ControlDesigner" /> at the specified index.</returns>
	public virtual ControlDesigner InternalControlDesigner(int internalControlIndex)
	{
		return null;
	}

	/// <summary>Returns the number of internal control designers in the <see cref="T:System.Windows.Forms.Design.ControlDesigner" />.</summary>
	/// <returns>The number of internal control designers in the <see cref="T:System.Windows.Forms.Design.ControlDesigner" />.</returns>
	public virtual int NumberOfInternalControlDesigners()
	{
		return 0;
	}

	/// <summary>Enables design time functionality for a child control.</summary>
	/// <param name="child">The child control for which design mode will be enabled.</param>
	/// <param name="name">The name of <paramref name="child" /> as exposed to the end user.</param>
	/// <returns>
	///   <see langword="true" /> if the child control could be enabled for design time; <see langword="false" /> if the hosting infrastructure does not support it.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="child" /> or <paramref name="name" /> is <see langword="null" />.</exception>
	protected bool EnableDesignMode(Control child, string name)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		if (child == null)
		{
			throw new ArgumentNullException("child");
		}
		bool result = false;
		if (GetService(typeof(INestedContainer)) is INestedContainer nestedContainer)
		{
			nestedContainer.Add(child, name);
			result = true;
		}
		return result;
	}

	/// <summary>Returns a <see cref="T:System.Windows.Forms.Design.Behavior.ControlBodyGlyph" /> representing the bounds of this control.</summary>
	/// <param name="selectionType">A <see cref="T:System.Windows.Forms.Design.Behavior.GlyphSelectionType" /> value that specifies the selection state.</param>
	/// <returns>A <see cref="T:System.Windows.Forms.Design.Behavior.ControlBodyGlyph" /> representing the bounds of this control.</returns>
	[System.MonoTODO]
	protected virtual ControlBodyGlyph GetControlGlyph(GlyphSelectionType selectionType)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a collection of <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> objects representing the selection borders and grab handles for a standard control.</summary>
	/// <param name="selectionType">A <see cref="T:System.Windows.Forms.Design.Behavior.GlyphSelectionType" /> value that specifies the selection state.</param>
	/// <returns>A collection of <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> objects.</returns>
	[System.MonoTODO]
	public virtual GlyphCollection GetGlyphs(GlyphSelectionType selectionType)
	{
		throw new NotImplementedException();
	}

	/// <summary>Re-initializes an existing component.</summary>
	/// <param name="defaultValues">A name/value dictionary of default values to apply to properties. May be <see langword="null" /> if no default values are specified.</param>
	[System.MonoTODO]
	public override void InitializeExistingComponent(IDictionary defaultValues)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a newly created component.</summary>
	/// <param name="defaultValues">A name/value dictionary of default values to apply to properties. May be <see langword="null" /> if no default values are specified.</param>
	[System.MonoTODO]
	public override void InitializeNewComponent(IDictionary defaultValues)
	{
		throw new NotImplementedException();
	}

	/// <summary>Receives a call to clean up a drag-and-drop operation.</summary>
	/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event.</param>
	[System.MonoTODO]
	protected virtual void OnDragComplete(DragEventArgs de)
	{
		throw new NotImplementedException();
	}
}
