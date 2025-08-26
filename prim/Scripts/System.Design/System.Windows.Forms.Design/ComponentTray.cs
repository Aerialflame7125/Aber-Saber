using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms.Design;

/// <summary>Provides behavior for the component tray of a designer.</summary>
[DesignTimeVisible(false)]
[ToolboxItem(false)]
[ProvideProperty("Location", typeof(IComponent))]
public class ComponentTray : ScrollableControl, IExtenderProvider
{
	private IServiceProvider _serviceProvider;

	private IDesigner _mainDesigner;

	private bool _showLargeIcons;

	private bool _autoArrange;

	/// <summary>Gets or sets a value indicating whether the tray items are automatically aligned.</summary>
	/// <returns>
	///   <see langword="true" /> if the tray items are automatically arranged; otherwise, <see langword="false" />.</returns>
	public bool AutoArrange
	{
		get
		{
			return _autoArrange;
		}
		set
		{
			_autoArrange = value;
		}
	}

	/// <summary>Gets the number of components contained in the tray.</summary>
	/// <returns>The number of components in the tray.</returns>
	[System.MonoTODO]
	public int ComponentCount => 0;

	/// <summary>Gets or sets a value indicating whether the tray displays a large icon to represent each component in the tray.</summary>
	/// <returns>
	///   <see langword="true" /> if large icons are displayed; otherwise, <see langword="false" />.</returns>
	public bool ShowLargeIcons
	{
		get
		{
			return _showLargeIcons;
		}
		set
		{
			_showLargeIcons = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ComponentTray" /> class using the specified designer and service provider.</summary>
	/// <param name="mainDesigner">The <see cref="T:System.ComponentModel.Design.IDesigner" /> that is the main or document designer for the current project.</param>
	/// <param name="serviceProvider">An <see cref="T:System.IServiceProvider" /> that can be used to obtain design-time services.</param>
	public ComponentTray(IDesigner mainDesigner, IServiceProvider serviceProvider)
	{
		if (mainDesigner == null)
		{
			throw new ArgumentNullException("mainDesigner");
		}
		if (serviceProvider == null)
		{
			throw new ArgumentNullException("serviceProvider");
		}
		_mainDesigner = mainDesigner;
		_serviceProvider = serviceProvider;
	}

	/// <summary>Adds a component to the tray.</summary>
	/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to add to the tray.</param>
	[System.MonoTODO]
	public virtual void AddComponent(IComponent component)
	{
	}

	/// <summary>Gets a value indicating whether the specified tool can be used to create a new component.</summary>
	/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to test.</param>
	/// <returns>
	///   <see langword="true" /> if the specified tool can be used to create a component; otherwise, <see langword="false" />.</returns>
	protected virtual bool CanCreateComponentFromTool(ToolboxItem tool)
	{
		return true;
	}

	/// <summary>Gets a value indicating whether the specified component can be displayed.</summary>
	/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to test.</param>
	/// <returns>
	///   <see langword="true" /> if the component can be displayed; otherwise, <see langword="false" />.</returns>
	protected virtual bool CanDisplayComponent(IComponent component)
	{
		return false;
	}

	/// <summary>Creates a component from the specified toolbox item, adds the component to the current document, and displays a representation for the component in the component tray.</summary>
	/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to create a component from.</param>
	[System.MonoTODO]
	public void CreateComponentFromTool(ToolboxItem tool)
	{
	}

	/// <summary>Displays an error message to the user with information about the specified exception.</summary>
	/// <param name="e">The exception about which to display information.</param>
	[System.MonoTODO]
	protected void DisplayError(Exception e)
	{
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Design.ComponentTray" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected override void Dispose(bool disposing)
	{
	}

	/// <summary>Gets the location of the specified component, relative to the upper-left corner of the component tray.</summary>
	/// <param name="receiver">The <see cref="T:System.ComponentModel.IComponent" /> to retrieve the location of.</param>
	/// <returns>A <see cref="T:System.Drawing.Point" /> indicating the coordinates of the specified component, or an empty <see cref="T:System.Drawing.Point" /> if the specified component could not be found in the component tray. An empty <see cref="T:System.Drawing.Point" /> has an <see cref="P:System.Drawing.Point.IsEmpty" /> property equal to <see langword="true" /> and typically has <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> properties that are each equal to zero.</returns>
	[Browsable(false)]
	[Category("Layout")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[DesignOnly(true)]
	[Localizable(false)]
	[System.MonoTODO]
	public Point GetLocation(IComponent receiver)
	{
		return new Point(0, 0);
	}

	/// <summary>Sets the location of the specified component to the specified location.</summary>
	/// <param name="receiver">The <see cref="T:System.ComponentModel.IComponent" /> to set the location of.</param>
	/// <param name="location">A <see cref="T:System.Drawing.Point" /> indicating the new location for the specified component.</param>
	[System.MonoTODO]
	public void SetLocation(IComponent receiver, Point location)
	{
	}

	/// <summary>Similar to <see cref="M:System.Windows.Forms.Control.GetNextControl(System.Windows.Forms.Control,System.Boolean)" />, this method returns the next component in the tray, given a starting component.</summary>
	/// <param name="component">The component from which to start enumerating.</param>
	/// <param name="forward">
	///   <see langword="true" /> to enumerate forward through the list; otherwise, <see langword="false" /> to enumerate backward.</param>
	/// <returns>The next component in the component tray, or <see langword="null" />, if the end of the list is encountered (or the beginning, if <paramref name="forward" /> is <see langword="false" />).</returns>
	[System.MonoTODO]
	public IComponent GetNextComponent(IComponent component, bool forward)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the value of the <see langword="Location" /> extender property.</summary>
	/// <param name="receiver">The <see cref="T:System.ComponentModel.IComponent" /> that receives the location extender property.</param>
	/// <returns>A <see cref="T:System.Drawing.Point" /> representing the location of <paramref name="receiver" />.</returns>
	[Browsable(false)]
	[Category("Layout")]
	[DesignOnly(true)]
	[Localizable(false)]
	[System.MonoTODO]
	public Point GetTrayLocation(IComponent receiver)
	{
		throw new NotImplementedException();
	}

	/// <summary>Tests a component for presence in the component tray.</summary>
	/// <param name="comp">The component to test for presence in the component tray.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="comp" /> is being shown on the tray; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public bool IsTrayComponent(IComponent comp)
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets the value of the <see langword="Location" /> extender property.</summary>
	/// <param name="receiver">The <see cref="T:System.ComponentModel.IComponent" /> that receives the location extender property.</param>
	/// <param name="location">A <see cref="T:System.Drawing.Point" /> representing the location of <paramref name="receiver" />.</param>
	[System.MonoTODO]
	public void SetTrayLocation(IComponent receiver, Point location)
	{
		throw new NotImplementedException();
	}

	/// <summary>Called when the mouse is double clicked over the component tray.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that provides data for the event.</param>
	[System.MonoTODO]
	protected override void OnMouseDoubleClick(MouseEventArgs e)
	{
	}

	/// <summary>Called when an object that has been dragged is dropped on the component tray.</summary>
	/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event.</param>
	[System.MonoTODO]
	protected override void OnDragDrop(DragEventArgs de)
	{
	}

	/// <summary>Called when an object is dragged over, and has entered the area over, the component tray.</summary>
	/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event.</param>
	[System.MonoTODO]
	protected override void OnDragEnter(DragEventArgs de)
	{
	}

	/// <summary>Called when an object is dragged out of the area over the component tray.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that provides data for the event.</param>
	[System.MonoTODO]
	protected override void OnDragLeave(EventArgs e)
	{
	}

	/// <summary>Called when an object is dragged over the component tray.</summary>
	/// <param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that provides data for the event.</param>
	[System.MonoTODO]
	protected override void OnDragOver(DragEventArgs de)
	{
	}

	/// <summary>Called during an OLE drag and drop operation to provide an opportunity for the component tray to give feedback to the user about the results of dropping the object at a specific point.</summary>
	/// <param name="gfevent">A <see cref="T:System.Windows.Forms.GiveFeedbackEventArgs" /> that provides data for the event.</param>
	[System.MonoTODO]
	protected override void OnGiveFeedback(GiveFeedbackEventArgs gfevent)
	{
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
	/// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
	[System.MonoTODO]
	protected override void OnLayout(LayoutEventArgs levent)
	{
	}

	/// <summary>Called when a mouse drag selection operation is canceled.</summary>
	[System.MonoTODO]
	protected virtual void OnLostCapture()
	{
	}

	/// <summary>Called when the mouse button is pressed.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that provides data for the event.</param>
	[System.MonoTODO]
	protected override void OnMouseDown(MouseEventArgs e)
	{
	}

	/// <summary>Called when the mouse cursor position has changed.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that provides data for the event.</param>
	[System.MonoTODO]
	protected override void OnMouseMove(MouseEventArgs e)
	{
	}

	/// <summary>Called when the mouse button has been released.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that provides data for the event.</param>
	[System.MonoTODO]
	protected override void OnMouseUp(MouseEventArgs e)
	{
	}

	/// <summary>Called when the view for the component tray should be refreshed.</summary>
	/// <param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that provides data for the event.</param>
	[System.MonoTODO]
	protected override void OnPaint(PaintEventArgs pe)
	{
	}

	/// <summary>Called to set the mouse cursor.</summary>
	[System.MonoTODO]
	protected virtual void OnSetCursor()
	{
	}

	/// <summary>Removes the specified component from the tray.</summary>
	/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to remove from the tray.</param>
	[System.MonoTODO]
	public virtual void RemoveComponent(IComponent component)
	{
	}

	/// <summary>Processes Windows messages.</summary>
	/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> to process.</param>
	[System.MonoTODO]
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IExtenderProvider.CanExtend(System.Object)" />.</summary>
	/// <param name="component">The <see cref="T:System.Object" /> to receive the extender properties.</param>
	/// <returns>
	///   <see langword="true" /> if this object can provide extender properties to the specified object; otherwise, <see langword="false" />.</returns>
	bool IExtenderProvider.CanExtend(object component)
	{
		return false;
	}

	/// <summary>Gets the requested service type.</summary>
	/// <param name="serviceType">The type of the service to retrieve.</param>
	/// <returns>An instance of the requested service, or <see langword="null" /> if the service could not be found.</returns>
	protected override object GetService(Type serviceType)
	{
		if (_serviceProvider != null)
		{
			return _serviceProvider.GetService(serviceType);
		}
		return null;
	}
}
