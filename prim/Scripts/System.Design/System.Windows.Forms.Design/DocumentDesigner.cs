using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design;

/// <summary>Base designer class for extending the design mode behavior of, and providing a root-level design mode view for, a <see cref="T:System.Windows.Forms.Control" /> that supports nested controls and should receive scroll messages.</summary>
[ToolboxItemFilter("System.Windows.Forms")]
public class DocumentDesigner : ScrollableControlDesigner, IRootDesigner, IDesigner, IDisposable, IToolboxUser
{
	public class DesignerViewFrame : UserControl
	{
		private Panel DesignerPanel;

		private Splitter splitter1;

		private Panel ComponentTrayPanel;

		private ComponentTray _componentTray;

		private Control _designedControl;

		private bool _mouseDown;

		private bool _firstMove;

		public ComponentTray ComponentTray
		{
			get
			{
				return _componentTray;
			}
			set
			{
				SuspendLayout();
				ComponentTrayPanel.Controls.Remove(_componentTray);
				ComponentTrayPanel.Controls.Add(value);
				ResumeLayout();
				_componentTray = value;
				_componentTray.Visible = false;
			}
		}

		public Control DesignedControl
		{
			get
			{
				return _designedControl;
			}
			set
			{
			}
		}

		public DesignerViewFrame(Control designedControl, ComponentTray tray)
		{
			if (designedControl == null)
			{
				throw new ArgumentNullException("designedControl");
			}
			if (tray == null)
			{
				throw new ArgumentNullException("tray");
			}
			InitializeComponent();
			_designedControl = designedControl;
			SuspendLayout();
			DesignerPanel.Controls.Add(designedControl);
			ResumeLayout();
			ComponentTray = tray;
		}

		private void InitializeComponent()
		{
			this.ComponentTrayPanel = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.DesignerPanel = new System.Windows.Forms.Panel();
			base.SuspendLayout();
			this.ComponentTrayPanel.BackColor = System.Drawing.Color.LemonChiffon;
			this.ComponentTrayPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ComponentTrayPanel.Location = new System.Drawing.Point(0, 194);
			this.ComponentTrayPanel.Name = "ComponentTrayPanel";
			this.ComponentTrayPanel.Size = new System.Drawing.Size(292, 72);
			this.ComponentTrayPanel.TabIndex = 1;
			this.ComponentTrayPanel.Visible = false;
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter1.Location = new System.Drawing.Point(0, 186);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(292, 8);
			this.splitter1.TabIndex = 2;
			this.splitter1.TabStop = false;
			this.splitter1.Visible = false;
			this.DesignerPanel.AutoScroll = true;
			this.DesignerPanel.BackColor = System.Drawing.Color.White;
			this.DesignerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DesignerPanel.Location = new System.Drawing.Point(0, 0);
			this.DesignerPanel.Name = "DesignerPanel";
			this.DesignerPanel.Size = new System.Drawing.Size(292, 266);
			this.DesignerPanel.TabIndex = 0;
			this.DesignerPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(DesignerPanel_MouseUp);
			this.DesignerPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(DesignerPanel_MouseMove);
			this.DesignerPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(DesignerPanel_MouseDown);
			this.DesignerPanel.Paint += new System.Windows.Forms.PaintEventHandler(DesignerPanel_Paint);
			base.Controls.Add(this.splitter1);
			base.Controls.Add(this.ComponentTrayPanel);
			base.Controls.Add(this.DesignerPanel);
			base.Name = "UserControl1";
			base.Size = new System.Drawing.Size(292, 266);
			this.Dock = System.Windows.Forms.DockStyle.Fill;
			base.ResumeLayout(false);
		}

		private void DesignerPanel_Paint(object sender, PaintEventArgs e)
		{
			if (DesignedControl.Site.GetService(typeof(IUISelectionService)) is IUISelectionService iUISelectionService)
			{
				iUISelectionService.PaintAdornments(DesignerPanel, e.Graphics);
			}
		}

		private void DesignerPanel_MouseDown(object sender, MouseEventArgs e)
		{
			_mouseDown = true;
			_firstMove = true;
		}

		private void DesignerPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (!(DesignedControl.Site.GetService(typeof(IUISelectionService)) is IUISelectionService iUISelectionService))
			{
				return;
			}
			iUISelectionService.SetCursor(e.X, e.Y);
			if (_mouseDown)
			{
				if (_firstMove)
				{
					iUISelectionService.MouseDragBegin(DesignerPanel, e.X, e.Y);
					_firstMove = false;
				}
				else
				{
					iUISelectionService.MouseDragMove(e.X, e.Y);
				}
			}
			else if (iUISelectionService.SelectionInProgress)
			{
				iUISelectionService.MouseDragMove(e.X, e.Y);
			}
		}

		private void DesignerPanel_MouseUp(object sender, MouseEventArgs e)
		{
			IUISelectionService iUISelectionService = DesignedControl.Site.GetService(typeof(IUISelectionService)) as IUISelectionService;
			if (_mouseDown)
			{
				iUISelectionService?.MouseDragEnd(cancel: false);
				_mouseDown = false;
			}
			else if (iUISelectionService.SelectionInProgress)
			{
				iUISelectionService.MouseDragEnd(cancel: false);
			}
		}

		public void ShowComponentTray()
		{
			if (!ComponentTray.Visible)
			{
				ComponentTrayPanel.Visible = true;
				ComponentTray.Visible = true;
				splitter1.Visible = true;
			}
		}

		public void HideComponentTray()
		{
			if (!ComponentTray.Visible)
			{
				ComponentTrayPanel.Visible = true;
				ComponentTray.Visible = true;
				splitter1.Visible = true;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (_designedControl != null)
			{
				DesignerPanel.Controls.Remove(_designedControl);
				_designedControl = null;
			}
			if (_componentTray != null)
			{
				ComponentTrayPanel.Controls.Remove(_componentTray);
				_componentTray.Dispose();
				_componentTray = null;
			}
			base.Dispose(disposing);
		}
	}

	private DesignerViewFrame _designerViewFrame;

	/// <summary>Initializes the menuEditorService variable to <see langword="null" />.</summary>
	protected IMenuEditorService menuEditorService;

	private DesignerViewFrame View => _designerViewFrame;

	/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.Design.IRootDesigner.SupportedTechnologies" />.</summary>
	/// <returns>An array of supported <see cref="T:System.ComponentModel.Design.ViewTechnology" /> values.</returns>
	ViewTechnology[] IRootDesigner.SupportedTechnologies => new ViewTechnology[1] { ViewTechnology.Default };

	/// <summary>Gets the <see cref="T:System.Windows.Forms.Design.SelectionRules" /> for the designer.</summary>
	/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.Design.SelectionRules" /> values.</returns>
	public override SelectionRules SelectionRules => SelectionRules.BottomSizeable | SelectionRules.RightSizeable | SelectionRules.Visible;

	private Color BackColor
	{
		get
		{
			return (Color)base.ShadowProperties["BackColor"];
		}
		set
		{
			base.ShadowProperties["BackColor"] = value;
			Control.BackColor = value;
		}
	}

	private Point Location
	{
		get
		{
			return (Point)base.ShadowProperties["Location"];
		}
		set
		{
			base.ShadowProperties["Location"] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.DocumentDesigner" /> class.</summary>
	public DocumentDesigner()
	{
	}

	/// <summary>Initializes the designer with the specified component.</summary>
	/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to associate with the designer.</param>
	public override void Initialize(IComponent component)
	{
		base.Initialize(component);
		_designerViewFrame = new DesignerViewFrame(Control, new ComponentTray(this, component.Site));
		_designerViewFrame.DesignedControl.Location = new Point(15, 15);
		SetValue(base.Component, "Location", new Point(0, 0));
		if (GetService(typeof(IComponentChangeService)) is IComponentChangeService componentChangeService)
		{
			componentChangeService.ComponentAdded += OnComponentAdded;
			componentChangeService.ComponentRemoved += OnComponentRemoved;
		}
		IMenuCommandService menuCommandService = GetService(typeof(IMenuCommandService)) as IMenuCommandService;
		IServiceContainer serviceContainer = GetService(typeof(IServiceContainer)) as IServiceContainer;
		if (menuCommandService != null && serviceContainer != null)
		{
			new DefaultMenuCommands(serviceContainer).AddTo(menuCommandService);
		}
		InitializeSelectionService();
	}

	private void InitializeSelectionService()
	{
		if (!(GetService(typeof(IUISelectionService)) is IUISelectionService))
		{
			IServiceContainer serviceContainer = GetService(typeof(IServiceContainer)) as IServiceContainer;
			serviceContainer.AddService(typeof(IUISelectionService), new UISelectionService(serviceContainer));
		}
		(GetService(typeof(ISelectionService)) as ISelectionService).SetSelectedComponents(new IComponent[1] { base.Component });
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Design.DocumentDesigner" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			if (_designerViewFrame != null)
			{
				_designerViewFrame.Dispose();
				_designerViewFrame = null;
			}
			if (GetService(typeof(IComponentChangeService)) is IComponentChangeService componentChangeService)
			{
				componentChangeService.ComponentAdded -= OnComponentAdded;
				componentChangeService.ComponentRemoved -= OnComponentRemoved;
			}
		}
		base.Dispose(disposing);
	}

	/// <summary>Gets a <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> representing the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> objects.</summary>
	/// <param name="selectionType">A <see cref="T:System.Windows.Forms.Design.Behavior.GlyphSelectionType" /> value that specifies the selection state.</param>
	/// <returns>A collection of <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> objects.</returns>
	public override GlyphCollection GetGlyphs(GlyphSelectionType selectionType)
	{
		return base.GetGlyphs(selectionType);
	}

	/// <summary>Processes Windows messages.</summary>
	/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> to process.</param>
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
	}

	/// <summary>Called when the context menu should be displayed.</summary>
	/// <param name="x">The horizontal screen coordinate to display the context menu at.</param>
	/// <param name="y">The vertical screen coordinate to display the context menu at.</param>
	protected override void OnContextMenu(int x, int y)
	{
		base.OnContextMenu(x, y);
	}

	/// <summary>Called immediately after the handle for the designer has been created.</summary>
	protected override void OnCreateHandle()
	{
		base.OnCreateHandle();
	}

	private void OnComponentAdded(object sender, ComponentEventArgs args)
	{
		if (!(args.Component is Control))
		{
			View.ComponentTray.AddComponent(args.Component);
			if (View.ComponentTray.ComponentCount > 0 && !View.ComponentTray.Visible)
			{
				View.ShowComponentTray();
			}
		}
	}

	private void OnComponentRemoved(object sender, ComponentEventArgs args)
	{
		if (!(args.Component is Control))
		{
			View.ComponentTray.RemoveComponent(args.Component);
			if (View.ComponentTray.ComponentCount == 0 && View.ComponentTray.Visible)
			{
				View.HideComponentTray();
			}
		}
	}

	/// <summary>For a description of this member, see <see cref="T:System.ComponentModel.Design.ViewTechnology" />.</summary>
	/// <param name="technology">A <see cref="T:System.ComponentModel.Design.ViewTechnology" /> that indicates a particular view technology.</param>
	/// <returns>An object that represents the view for this designer.</returns>
	object IRootDesigner.GetView(ViewTechnology technology)
	{
		if (technology != ViewTechnology.Default)
		{
			throw new ArgumentException("Only ViewTechnology.WindowsForms is supported.");
		}
		return _designerViewFrame;
	}

	/// <summary>For a description of this member, see <see cref="M:System.Drawing.Design.IToolboxUser.GetToolSupported(System.Drawing.Design.ToolboxItem)" />.</summary>
	/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to be tested for toolbox support.</param>
	/// <returns>
	///   <see langword="true" /> if the tool is supported by the toolbox and can be enabled; <see langword="false" /> if the document designer does not know how to use the tool.</returns>
	bool IToolboxUser.GetToolSupported(ToolboxItem tool)
	{
		return GetToolSupported(tool);
	}

	/// <summary>Indicates whether the specified tool is supported by the designer.</summary>
	/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to test for toolbox support.</param>
	/// <returns>
	///   <see langword="true" /> if the tool should be enabled on the toolbox; <see langword="false" /> if the document designer doesn't know how to use the tool.</returns>
	protected virtual bool GetToolSupported(ToolboxItem tool)
	{
		return true;
	}

	/// <summary>For a description of this member, see <see cref="M:System.Drawing.Design.IToolboxUser.ToolPicked(System.Drawing.Design.ToolboxItem)" />.</summary>
	/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to select.</param>
	void IToolboxUser.ToolPicked(ToolboxItem tool)
	{
		ToolPicked(tool);
	}

	/// <summary>Selects the specified tool.</summary>
	/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to create a component for.</param>
	protected virtual void ToolPicked(ToolboxItem tool)
	{
		ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
		IDesignerHost designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
		if (selectionService != null && designerHost != null)
		{
			IDesigner designer = designerHost.GetDesigner((IComponent)selectionService.PrimarySelection);
			if (designer is ParentControlDesigner)
			{
				ParentControlDesigner.InvokeCreateTool((ParentControlDesigner)designer, tool);
			}
			else
			{
				CreateTool(tool);
			}
		}
		else
		{
			CreateTool(tool);
		}
		(GetService(typeof(IToolboxService)) as IToolboxService).SelectedToolboxItemUsed();
	}

	/// <summary>Adjusts the set of properties the component exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	/// <param name="properties">An <see cref="T:System.Collections.IDictionary" /> that contains the properties for the class of the component.</param>
	protected override void PreFilterProperties(IDictionary properties)
	{
		base.PreFilterProperties(properties);
		if (properties["BackColor"] is PropertyDescriptor oldPropertyDescriptor)
		{
			properties["BackColor"] = TypeDescriptor.CreateProperty(typeof(DocumentDesigner), oldPropertyDescriptor, new DefaultValueAttribute(SystemColors.Control));
		}
		if (properties["Location"] is PropertyDescriptor oldPropertyDescriptor2)
		{
			properties["Location"] = TypeDescriptor.CreateProperty(typeof(DocumentDesigner), oldPropertyDescriptor2, new DefaultValueAttribute(typeof(Point), "0, 0"));
		}
	}

	/// <summary>Checks for the existence of a menu editor service and creates one if one does not already exist.</summary>
	/// <param name="c">The <see cref="T:System.ComponentModel.IComponent" /> to ensure has a context menu service.</param>
	protected virtual void EnsureMenuEditorService(IComponent c)
	{
		if (menuEditorService == null && c is ContextMenu)
		{
			menuEditorService = (IMenuEditorService)GetService(typeof(IMenuEditorService));
		}
	}
}
