using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design;

/// <summary>Extends the design mode behavior of a <see cref="T:System.Windows.Forms.Control" /> that supports nested controls.</summary>
public class ParentControlDesigner : ControlDesigner
{
	private bool _defaultDrawGrid;

	private bool _defaultSnapToGrid;

	private Size _defaultGridSize;

	private bool _drawGrid;

	private bool _snapToGrid;

	private Size _gridSize;

	private Point _mouseDownPoint = Point.Empty;

	/// <summary>Gets the default location for a control added to the designer.</summary>
	/// <returns>A <see cref="T:System.Drawing.Point" /> that indicates the default location for a control added to the designer.</returns>
	protected virtual Point DefaultControlLocation => new Point(0, 0);

	/// <summary>Gets a value indicating whether drag rectangles are drawn by the designer.</summary>
	/// <returns>
	///   <see langword="true" /> if drag rectangles are drawn; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	protected override bool EnableDragRect => true;

	/// <summary>Gets or sets a value indicating whether a grid should be drawn on the control for this designer.</summary>
	/// <returns>
	///   <see langword="true" /> if a grid should be drawn on the control in the designer; otherwise, <see langword="false" />.</returns>
	protected virtual bool DrawGrid
	{
		get
		{
			return _drawGrid;
		}
		set
		{
			_drawGrid = value;
			if (!value)
			{
				SetValue(base.Component, "SnapToGrid", false);
			}
			PopulateGridProperties();
		}
	}

	private bool SnapToGrid
	{
		get
		{
			return _snapToGrid;
		}
		set
		{
			_snapToGrid = value;
			PopulateGridProperties();
		}
	}

	/// <summary>Gets or sets the size of each square of the grid that is drawn when the designer is in grid draw mode.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of each square of the grid drawn on a form or user control.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <see cref="T:System.Drawing.Size" /> is outside the allowed range for <see cref="P:System.Windows.Forms.Design.ParentControlDesigner.GridSize" />. The default minimum value is 2, and the default maximum value is 200.</exception>
	protected Size GridSize
	{
		get
		{
			return _gridSize;
		}
		set
		{
			_gridSize = value;
			PopulateGridProperties();
		}
	}

	/// <summary>Gets a value indicating whether selected controls will be re-parented.</summary>
	/// <returns>
	///   <see langword="true" /> if the controls that were selected by lassoing on the designer's surface will be re-parented to this designer's control.</returns>
	[System.MonoTODO]
	protected virtual bool AllowControlLasso => false;

	/// <summary>Gets a value indicating whether a generic drag box should be drawn when dragging a toolbox item over the designer's surface.</summary>
	/// <returns>
	///   <see langword="true" /> if a generic drag box should be drawn when dragging a toolbox item over the designer's surface; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[System.MonoTODO]
	protected virtual bool AllowGenericDragBox => false;

	/// <summary>Gets a value indicating whether the z-order of dragged controls should be maintained when dropped on a <see cref="T:System.Windows.Forms.Design.ParentControlDesigner" />.</summary>
	/// <returns>
	///   <see langword="true" /> if the z-order of dragged controls should be maintained when dropped on a <see cref="T:System.Windows.Forms.Design.ParentControlDesigner" />; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	protected internal virtual bool AllowSetChildIndexOnDrop => false;

	/// <summary>Gets a list of <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> objects representing significant alignment points for this control.</summary>
	/// <returns>A list of <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> objects representing significant alignment points for this control.</returns>
	[System.MonoTODO]
	public override IList SnapLines => new object[0];

	/// <summary>Gets a value indicating whether the designer has a valid tool during a drag operation.</summary>
	/// <returns>The tool being dragged, if creating a component, or <see langword="null" /> if there is no tool.</returns>
	[System.MonoTODO]
	protected ToolboxItem MouseDragTool => null;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ParentControlDesigner" /> class.</summary>
	public ParentControlDesigner()
	{
	}

	/// <summary>Initializes the designer with the specified component.</summary>
	/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to associate with the designer.</param>
	public override void Initialize(IComponent component)
	{
		base.Initialize(component);
		Control.AllowDrop = true;
		_defaultDrawGrid = true;
		_defaultSnapToGrid = true;
		_defaultGridSize = new Size(8, 8);
		if (Control.Parent != null)
		{
			ParentControlDesigner parentControlDesignerOf = GetParentControlDesignerOf(Control.Parent);
			if (parentControlDesignerOf != null)
			{
				_defaultDrawGrid = (bool)GetValue(parentControlDesignerOf.Component, "DrawGrid");
				_defaultSnapToGrid = (bool)GetValue(parentControlDesignerOf.Component, "SnapToGrid");
				_defaultGridSize = (Size)GetValue(parentControlDesignerOf.Component, "GridSize");
			}
		}
		else if (GetService(typeof(IDesignerOptionService)) is IDesignerOptionService designerOptionService)
		{
			object obj = null;
			obj = designerOptionService.GetOptionValue("WindowsFormsDesigner\\General", "DrawGrid");
			if (obj is bool)
			{
				_defaultDrawGrid = (bool)obj;
			}
			obj = designerOptionService.GetOptionValue("WindowsFormsDesigner\\General", "SnapToGrid");
			if (obj is bool)
			{
				_defaultSnapToGrid = (bool)obj;
			}
			obj = designerOptionService.GetOptionValue("WindowsFormsDesigner\\General", "GridSize");
			if (obj is Size)
			{
				_defaultGridSize = (Size)obj;
			}
		}
		if (GetService(typeof(IComponentChangeService)) is IComponentChangeService componentChangeService)
		{
			componentChangeService.ComponentRemoving += OnComponentRemoving;
			componentChangeService.ComponentRemoved += OnComponentRemoved;
		}
		_drawGrid = _defaultDrawGrid;
		_snapToGrid = _defaultSnapToGrid;
		_gridSize = _defaultGridSize;
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Design.ParentControlDesigner" />, and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			EnableDragDrop(value: false);
			OnMouseDragEnd(cancel: true);
		}
		base.Dispose(disposing);
	}

	/// <summary>Creates a tool from the specified <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
	/// <param name="toInvoke">The <see cref="T:System.Windows.Forms.Design.ParentControlDesigner" /> that the tool is to be used with.</param>
	/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to create a tool from.</param>
	protected static void InvokeCreateTool(ParentControlDesigner toInvoke, ToolboxItem tool)
	{
		toInvoke?.CreateTool(tool);
	}

	/// <summary>Creates a component or control from the specified tool and adds it to the current design document.</summary>
	/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to create a component from.</param>
	protected void CreateTool(ToolboxItem tool)
	{
		CreateToolCore(tool, DefaultControlLocation.X, DefaultControlLocation.Y, 0, 0, hasLocation: true, hasSize: false);
	}

	/// <summary>Creates a component or control from the specified tool and adds it to the current design document at the specified location.</summary>
	/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to create a component from.</param>
	/// <param name="location">The <see cref="T:System.Drawing.Point" />, in design-time view screen coordinates, at which to center the component.</param>
	protected void CreateTool(ToolboxItem tool, Point location)
	{
		CreateToolCore(tool, location.X, location.Y, 0, 0, hasLocation: true, hasSize: false);
	}

	/// <summary>Creates a component or control from the specified tool and adds it to the current design document within the bounds of the specified rectangle.</summary>
	/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to create a component from.</param>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> indicating the location and size for the component created from the tool. The <see cref="P:System.Drawing.Rectangle.X" /> and <see cref="P:System.Drawing.Rectangle.Y" /> values of the <see cref="T:System.Drawing.Rectangle" /> indicate the design-time view screen coordinates of the upper-left corner of the component.</param>
	protected void CreateTool(ToolboxItem tool, Rectangle bounds)
	{
		CreateToolCore(tool, bounds.X, bounds.Y, bounds.Width, bounds.Width, hasLocation: true, hasSize: true);
	}

	/// <summary>Provides core functionality for all the <see cref="M:System.Windows.Forms.Design.ParentControlDesigner.CreateTool(System.Drawing.Design.ToolboxItem)" /> methods.</summary>
	/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to create a component from.</param>
	/// <param name="x">The horizontal position, in design-time view coordinates, of the location of the left edge of the tool, if a size is specified; the horizontal position of the center of the tool, if no size is specified.</param>
	/// <param name="y">The vertical position, in design-time view coordinates, of the location of the top edge of the tool, if a size is specified; the vertical position of the center of the tool, if no size is specified.</param>
	/// <param name="width">The width of the tool. This parameter is ignored if the <paramref name="hasSize" /> parameter is set to <see langword="false" />.</param>
	/// <param name="height">The height of the tool. This parameter is ignored if the <paramref name="hasSize" /> parameter is set to <see langword="false" />.</param>
	/// <param name="hasLocation">
	///   <see langword="true" /> if a location for the component is specified; <see langword="false" /> if the component is to be positioned in the center of the currently selected control.</param>
	/// <param name="hasSize">
	///   <see langword="true" /> if a size for the component is specified; <see langword="false" /> if the default height and width values for the component are to be used.</param>
	/// <returns>An array of components created from the tool.</returns>
	protected virtual IComponent[] CreateToolCore(ToolboxItem tool, int x, int y, int width, int height, bool hasLocation, bool hasSize)
	{
		if (tool == null)
		{
			throw new ArgumentNullException("tool");
		}
		IDesignerHost designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
		DesignerTransaction designerTransaction = designerHost.CreateTransaction("Create components in tool '" + tool.DisplayName + "'");
		IComponent[] array = tool.CreateComponents(designerHost);
		IComponent[] array2 = array;
		foreach (IComponent component in array2)
		{
			if (!(designerHost.GetDesigner(component) is ControlDesigner controlDesigner))
			{
				continue;
			}
			if (!CanParent(controlDesigner))
			{
				designerHost.DestroyComponent(component);
			}
			else if (component is Control component2)
			{
				Control.SuspendLayout();
				TypeDescriptor.GetProperties(component2)["Parent"].SetValue(component2, Control);
				Control.SuspendLayout();
				if (hasLocation)
				{
					SetValue(component, "Location", SnapPointToGrid(new Point(x, y)));
				}
				else
				{
					SetValue(component, "Location", SnapPointToGrid(DefaultControlLocation));
				}
				if (hasSize)
				{
					SetValue(component, "Size", new Size(width, height));
				}
				Control.Refresh();
			}
		}
		if (GetService(typeof(ISelectionService)) is ISelectionService selectionService)
		{
			selectionService.SetSelectedComponents(array, SelectionTypes.Replace);
		}
		designerTransaction.Commit();
		return array;
	}

	/// <summary>Indicates whether the specified control can be a child of the control managed by this designer.</summary>
	/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to test.</param>
	/// <returns>
	///   <see langword="true" /> if the specified control can be a child of the control managed by this designer; otherwise, <see langword="false" />.</returns>
	public virtual bool CanParent(Control control)
	{
		if (control != null)
		{
			return !control.Contains(Control);
		}
		return false;
	}

	/// <summary>Indicates whether the control managed by the specified designer can be a child of the control managed by this designer.</summary>
	/// <param name="controlDesigner">The designer for the control to test.</param>
	/// <returns>
	///   <see langword="true" /> if the control managed by the specified designer can be a child of the control managed by this designer; otherwise, <see langword="false" />.</returns>
	public virtual bool CanParent(ControlDesigner controlDesigner)
	{
		return CanParent(controlDesigner.Control);
	}

	/// <summary>Called when a drag-and-drop object is dropped onto the control designer view.</summary>
	/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event.</param>
	protected override void OnDragDrop(DragEventArgs de)
	{
		if (GetService(typeof(IUISelectionService)) is IUISelectionService iUISelectionService)
		{
			Point point = SnapPointToGrid(Control.PointToClient(new Point(de.X, de.Y)));
			iUISelectionService.DragDrop(cancel: false, Control, point.X, point.Y);
		}
	}

	/// <summary>Called when a drag-and-drop operation enters the control designer view.</summary>
	/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event.</param>
	protected override void OnDragEnter(DragEventArgs de)
	{
		Control.Refresh();
	}

	/// <summary>Called when a drag-and-drop operation leaves the control designer view.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that provides data for the event.</param>
	protected override void OnDragLeave(EventArgs e)
	{
		Control.Refresh();
	}

	/// <summary>Called when a drag-and-drop object is dragged over the control designer view.</summary>
	/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event.</param>
	protected override void OnDragOver(DragEventArgs de)
	{
		if (GetService(typeof(IUISelectionService)) is IUISelectionService iUISelectionService)
		{
			Point point = SnapPointToGrid(Control.PointToClient(new Point(de.X, de.Y)));
			iUISelectionService.DragOver(Control, point.X, point.Y);
		}
		de.Effect = DragDropEffects.Move;
	}

	private void OnComponentRemoving(object sender, ComponentEventArgs args)
	{
		IComponentChangeService componentChangeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
		if (args.Component is Control control && control.Parent == Control)
		{
			componentChangeService?.OnComponentChanging(args.Component, TypeDescriptor.GetProperties(args.Component)["Parent"]);
		}
	}

	private void OnComponentRemoved(object sender, ComponentEventArgs args)
	{
		IComponentChangeService componentChangeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
		if (args.Component is Control control && control.Parent == Control && componentChangeService != null)
		{
			control.Parent = null;
			componentChangeService.OnComponentChanged(args.Component, TypeDescriptor.GetProperties(args.Component)["Parent"], Control, null);
		}
	}

	/// <summary>Adjusts the set of properties the component will expose through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	/// <param name="properties">An <see cref="T:System.Collections.IDictionary" /> that contains the properties for the class of the component.</param>
	protected override void PreFilterProperties(IDictionary properties)
	{
		base.PreFilterProperties(properties);
		properties["DrawGrid"] = TypeDescriptor.CreateProperty(typeof(ParentControlDesigner), "DrawGrid", typeof(bool), BrowsableAttribute.Yes, DesignOnlyAttribute.Yes, new DescriptionAttribute("Indicates whether or not to draw the positioning grid."), CategoryAttribute.Design);
		properties["SnapToGrid"] = TypeDescriptor.CreateProperty(typeof(ParentControlDesigner), "SnapToGrid", typeof(bool), BrowsableAttribute.Yes, DesignOnlyAttribute.Yes, new DescriptionAttribute("Determines if controls should snap to the positioning grid."), CategoryAttribute.Design);
		properties["GridSize"] = TypeDescriptor.CreateProperty(typeof(ParentControlDesigner), "GridSize", typeof(Size), BrowsableAttribute.Yes, DesignOnlyAttribute.Yes, new DescriptionAttribute("Determines the size of the positioning grid."), CategoryAttribute.Design);
	}

	private void PopulateGridProperties()
	{
		Control.Invalidate(invalidateChildren: false);
		if (Control == null)
		{
			return;
		}
		ParentControlDesigner parentControlDesigner = null;
		foreach (Control control in Control.Controls)
		{
			GetParentControlDesignerOf(control)?.OnParentGridPropertiesChanged(this);
		}
	}

	private void OnParentGridPropertiesChanged(ParentControlDesigner parentDesigner)
	{
		SetValue(base.Component, "DrawGrid", (bool)GetValue(parentDesigner.Component, "DrawGrid"));
		SetValue(base.Component, "SnapToGrid", (bool)GetValue(parentDesigner.Component, "SnapToGrid"));
		SetValue(base.Component, "GridSize", (Size)GetValue(parentDesigner.Component, "GridSize"));
		_defaultDrawGrid = (bool)GetValue(parentDesigner.Component, "DrawGrid");
		_defaultSnapToGrid = (bool)GetValue(parentDesigner.Component, "SnapToGrid");
		_defaultGridSize = (Size)GetValue(parentDesigner.Component, "GridSize");
		PopulateGridProperties();
	}

	private ParentControlDesigner GetParentControlDesignerOf(Control control)
	{
		if (control != null && GetService(typeof(IDesignerHost)) is IDesignerHost designerHost)
		{
			ParentControlDesigner parentControlDesigner = null;
			if (designerHost.GetDesigner(Control.Parent) is ParentControlDesigner result)
			{
				return result;
			}
		}
		return null;
	}

	private bool ShouldSerializeDrawGrid()
	{
		return DrawGrid != _defaultDrawGrid;
	}

	private void ResetDrawGrid()
	{
		DrawGrid = _defaultDrawGrid;
	}

	private bool ShouldSerializeSnapToGrid()
	{
		return _drawGrid != _defaultDrawGrid;
	}

	private void ResetSnapToGrid()
	{
		SnapToGrid = _defaultSnapToGrid;
	}

	private bool ShouldSerializeGridSize()
	{
		return GridSize != _defaultGridSize;
	}

	private void ResetGridSize()
	{
		GridSize = _defaultGridSize;
	}

	/// <summary>Called in response to the left mouse button being pressed and held while over the component.</summary>
	/// <param name="x">The x-coordinate of the mouse in screen coordinates.</param>
	/// <param name="y">The y-coordinate of the mouse in screen coordinates.</param>
	protected override void OnMouseDragBegin(int x, int y)
	{
		if (GetService(typeof(IUISelectionService)) is IUISelectionService iUISelectionService)
		{
			Point point = new Point(x, y);
			IDesignerHost designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (base.MouseButtonDown == MouseButtons.Middle && designerHost != null && designerHost.RootComponent != Control)
			{
				point = Control.Parent.PointToClient(Control.PointToScreen(new Point(x, y)));
				Control.AllowDrop = false;
				iUISelectionService.DragBegin();
			}
			else
			{
				iUISelectionService.MouseDragBegin(Control, point.X, point.Y);
			}
		}
	}

	/// <summary>Called for each movement of the mouse during a drag-and-drop operation.</summary>
	/// <param name="x">The x-coordinate of the mouse in screen coordinates.</param>
	/// <param name="y">The y-coordinate of the mouse in screen coordinates.</param>
	protected override void OnMouseDragMove(int x, int y)
	{
		if (GetService(typeof(IUISelectionService)) is IUISelectionService iUISelectionService)
		{
			Point point = new Point(x, y);
			if (!iUISelectionService.SelectionInProgress)
			{
				point = SnapPointToGrid(new Point(x, y));
			}
			iUISelectionService.MouseDragMove(point.X, point.Y);
		}
	}

	/// <summary>Called at the end of a drag-and-drop operation to complete or cancel the operation.</summary>
	/// <param name="cancel">
	///   <see langword="true" /> to cancel the drag operation; <see langword="false" /> to commit it.</param>
	protected override void OnMouseDragEnd(bool cancel)
	{
		if (!(GetService(typeof(IUISelectionService)) is IUISelectionService iUISelectionService))
		{
			return;
		}
		IToolboxService toolboxService = GetService(typeof(IToolboxService)) as IToolboxService;
		if (!cancel && toolboxService != null && toolboxService.GetSelectedToolboxItem() != null)
		{
			if (iUISelectionService.SelectionInProgress)
			{
				bool hasSize = iUISelectionService.SelectionBounds.Width > 0 && iUISelectionService.SelectionBounds.Height > 0;
				CreateToolCore(toolboxService.GetSelectedToolboxItem(), iUISelectionService.SelectionBounds.X, iUISelectionService.SelectionBounds.Y, iUISelectionService.SelectionBounds.Width, iUISelectionService.SelectionBounds.Height, hasLocation: true, hasSize);
				toolboxService.SelectedToolboxItemUsed();
				cancel = true;
			}
			else if (!iUISelectionService.SelectionInProgress && !iUISelectionService.ResizeInProgress && !iUISelectionService.DragDropInProgress)
			{
				CreateTool(toolboxService.GetSelectedToolboxItem(), _mouseDownPoint);
				toolboxService.SelectedToolboxItemUsed();
				cancel = true;
			}
		}
		if (iUISelectionService.SelectionInProgress || iUISelectionService.ResizeInProgress)
		{
			iUISelectionService.MouseDragEnd(cancel);
		}
	}

	/// <summary>Called in order to clean up a drag-and-drop operation.</summary>
	/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event.</param>
	protected override void OnDragComplete(DragEventArgs de)
	{
		base.OnDragComplete(de);
	}

	internal override void OnMouseDown(int x, int y)
	{
		_mouseDownPoint.X = x;
		_mouseDownPoint.Y = y;
		base.OnMouseDown(x, y);
	}

	internal override void OnMouseUp()
	{
		base.OnMouseUp();
		if (!Control.AllowDrop)
		{
			Control.AllowDrop = true;
		}
		_mouseDownPoint = Point.Empty;
	}

	internal override void OnMouseMove(int x, int y)
	{
		if (GetService(typeof(IUISelectionService)) is IUISelectionService iUISelectionService)
		{
			iUISelectionService.SetCursor(x, y);
		}
		base.OnMouseMove(x, y);
	}

	private Point SnapPointToGrid(Point location)
	{
		Rectangle bounds = Control.Bounds;
		Size size = (Size)GetValue(base.Component, "GridSize");
		if ((bool)GetValue(base.Component, "SnapToGrid"))
		{
			int num = location.X + (size.Width - location.X % size.Width);
			if (num > bounds.Width)
			{
				num = bounds.Width - size.Width;
			}
			location.X = num;
			int num2 = location.Y + (size.Height - location.Y % size.Height);
			if (num2 > bounds.Height)
			{
				num2 = bounds.Height - size.Height;
			}
			location.Y = num2;
		}
		return location;
	}

	/// <summary>Provides an opportunity to change the current mouse cursor.</summary>
	protected override void OnSetCursor()
	{
		if (Control != null)
		{
			if (GetService(typeof(IToolboxService)) is IToolboxService toolboxService)
			{
				toolboxService.SetCursor();
			}
			else
			{
				base.OnSetCursor();
			}
		}
	}

	/// <summary>Called when the control that the designer is managing has painted its surface so the designer can paint any additional adornments on top of the control.</summary>
	/// <param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that provides data for the event.</param>
	protected override void OnPaintAdornments(PaintEventArgs pe)
	{
		base.OnPaintAdornments(pe);
		bool flag;
		try
		{
			flag = (bool)GetValue(base.Component, "DrawGrid");
		}
		catch
		{
			flag = DrawGrid;
		}
		Size pixelsBetweenDots;
		try
		{
			pixelsBetweenDots = (Size)GetValue(base.Component, "GridSize");
		}
		catch
		{
			pixelsBetweenDots = GridSize;
		}
		if (flag)
		{
			GraphicsState gstate = pe.Graphics.Save();
			pe.Graphics.TranslateTransform(Control.ClientRectangle.X, Control.ClientRectangle.Y);
			ControlPaint.DrawGrid(pe.Graphics, Control.ClientRectangle, pixelsBetweenDots, Control.BackColor);
			pe.Graphics.Restore(gstate);
		}
		if (GetService(typeof(IUISelectionService)) is IUISelectionService iUISelectionService)
		{
			iUISelectionService.PaintAdornments(Control, pe.Graphics);
		}
	}

	/// <summary>Gets the control from the designer of the specified component.</summary>
	/// <param name="component">The component to retrieve the control for.</param>
	/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that the specified component belongs to.</returns>
	protected Control GetControl(object component)
	{
		if (component is IComponent { Site: not null } component2 && component2.Site.GetService(typeof(IDesignerHost)) is IDesignerHost designerHost && designerHost.GetDesigner(component2) is ControlDesigner controlDesigner)
		{
			return controlDesigner.Control;
		}
		return null;
	}

	/// <summary>Initializes a newly created component.</summary>
	/// <param name="defaultValues">A name/value dictionary of default values to apply to properties. May be <see langword="null" /> if no default values are specified.</param>
	[System.MonoTODO]
	public override void InitializeNewComponent(IDictionary defaultValues)
	{
		base.InitializeNewComponent(defaultValues);
	}

	/// <summary>Adds padding snaplines.</summary>
	/// <param name="snapLines">An <see cref="T:System.Collections.ArrayList" /> that contains <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> objects.</param>
	[System.MonoTODO]
	protected void AddPaddingSnapLines(ref ArrayList snapLines)
	{
		throw new NotImplementedException();
	}

	/// <summary>Used by deriving classes to determine if it returns the control being designed or some other <see cref="T:System.ComponentModel.Container" /> while adding a component to it.</summary>
	/// <param name="component">The component for which to retrieve the parent <see cref="T:System.Windows.Forms.Control" />.</param>
	/// <returns>The parent <see cref="T:System.Windows.Forms.Control" /> for the component.</returns>
	[System.MonoTODO]
	protected virtual Control GetParentForComponent(IComponent component)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a body glyph that represents the bounds of the control.</summary>
	/// <param name="selectionType">A <see cref="T:System.Windows.Forms.Design.Behavior.GlyphSelectionType" /> value that specifies the selection state.</param>
	/// <returns>A body glyph that represents the bounds of the control.</returns>
	[System.MonoTODO]
	protected override ControlBodyGlyph GetControlGlyph(GlyphSelectionType selectionType)
	{
		return base.GetControlGlyph(selectionType);
	}

	/// <summary>Gets a collection of <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> objects representing the selection borders and grab handles for a standard control.</summary>
	/// <param name="selectionType">A <see cref="T:System.Windows.Forms.Design.Behavior.GlyphSelectionType" /> value that specifies the selection state.</param>
	/// <returns>A collection of <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> objects.</returns>
	[System.MonoTODO]
	public override GlyphCollection GetGlyphs(GlyphSelectionType selectionType)
	{
		return base.GetGlyphs(selectionType);
	}

	/// <summary>Updates the position of the specified rectangle, adjusting it for grid alignment if grid alignment mode is enabled.</summary>
	/// <param name="originalRect">A <see cref="T:System.Drawing.Rectangle" /> indicating the initial position of the component being updated.</param>
	/// <param name="dragRect">A <see cref="T:System.Drawing.Rectangle" /> indicating the new position of the component.</param>
	/// <param name="updateSize">
	///   <see langword="true" /> to update the size of the rectangle, if there has been any change; otherwise, <see langword="false" />.</param>
	/// <returns>A rectangle indicating the position of the component in design-time view screen coordinates. If no changes have been made, this method returns the original rectangle.</returns>
	[System.MonoTODO]
	protected Rectangle GetUpdatedRect(Rectangle originalRect, Rectangle dragRect, bool updateSize)
	{
		throw new NotImplementedException();
	}
}
