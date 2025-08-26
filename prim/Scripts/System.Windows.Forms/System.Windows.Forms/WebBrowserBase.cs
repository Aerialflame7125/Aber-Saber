using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.WebBrowserDialogs;
using Mono.WebBrowser;

namespace System.Windows.Forms;

/// <summary>Provides a wrapper for a generic ActiveX control for use as a base class by the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
[DefaultEvent("Enter")]
[Designer("System.Windows.Forms.Design.AxDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
[DefaultProperty("Name")]
public class WebBrowserBase : Control
{
	private enum State
	{
		Unloaded,
		Loaded,
		Active
	}

	internal bool documentReady;

	private bool suppressDialogs;

	protected string status;

	private State state;

	private IWebBrowser webHost;

	internal bool SuppressDialogs
	{
		get
		{
			return suppressDialogs;
		}
		set
		{
			suppressDialogs = value;
			webHost.Alert -= OnWebHostAlert;
			if (!suppressDialogs)
			{
				webHost.Alert += OnWebHostAlert;
			}
		}
	}

	/// <summary>Gets the underlying ActiveX WebBrowser control.</summary>
	/// <returns>An <see cref="T:System.Object" /> representing the underlying ActiveX WebBrowser control.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public object ActiveXInstance
	{
		get
		{
			throw new NotSupportedException("Retrieving a reference to an activex interface is not supported. Sorry.");
		}
	}

	/// <summary>This property is not supported by this control.</summary>
	/// <returns>false in all cases.</returns>
	/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override bool AllowDrop
	{
		get
		{
			return base.AllowDrop;
		}
		set
		{
			base.AllowDrop = value;
		}
	}

	/// <summary>This property is not meaningful for this control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override Color BackColor
	{
		get
		{
			return base.BackColor;
		}
		set
		{
			base.BackColor = value;
		}
	}

	/// <summary>This property is not supported by this control.</summary>
	/// <returns>null.</returns>
	/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override Image BackgroundImage
	{
		get
		{
			return base.BackgroundImage;
		}
		set
		{
			base.BackgroundImage = value;
		}
	}

	/// <summary>This property is not supported by this control.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" />.</returns>
	/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override ImageLayout BackgroundImageLayout
	{
		get
		{
			return base.BackgroundImageLayout;
		}
		set
		{
			base.BackgroundImageLayout = value;
		}
	}

	/// <summary>This property is not supported by this control.</summary>
	/// <returns>The value of this property is not meaningful for this control.</returns>
	/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override Cursor Cursor
	{
		get
		{
			return base.Cursor;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>This property is not supported by this control.</summary>
	/// <returns>true in all cases.</returns>
	/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new bool Enabled
	{
		get
		{
			return base.Enabled;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>This property is not meaningful for this control.</summary>
	/// <returns>The value of this property is not meaningful for this control.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override Font Font
	{
		get
		{
			return base.Font;
		}
		set
		{
			base.Font = value;
		}
	}

	/// <summary>This property is not meaningful for this control.</summary>
	/// <returns>The value of this property is not meaningful for this control.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override Color ForeColor
	{
		get
		{
			return base.ForeColor;
		}
		set
		{
			base.ForeColor = value;
		}
	}

	/// <summary>This property is not meaningful for this control.</summary>
	/// <returns>The value of this property is not meaningful for this control.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new ImeMode ImeMode
	{
		get
		{
			return base.ImeMode;
		}
		set
		{
			base.ImeMode = value;
		}
	}

	/// <summary>This property is not supported by this control.</summary>
	/// <returns>The value of this property is not meaningful for this control.</returns>
	/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Localizable(false)]
	public new virtual RightToLeft RightToLeft
	{
		get
		{
			return base.RightToLeft;
		}
		set
		{
			base.RightToLeft = value;
		}
	}

	/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.Windows.Forms.Control" />, if any.</returns>
	public override ISite Site
	{
		set
		{
			base.Site = value;
		}
	}

	/// <summary>This property is not supported by this control.</summary>
	/// <returns>
	///   <see cref="F:System.String.Empty" />.</returns>
	/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
	[Bindable(false)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override string Text
	{
		get
		{
			return string.Empty;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>This property is not supported by this control.</summary>
	/// <returns>false in all cases.</returns>
	/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new bool UseWaitCursor
	{
		get
		{
			return base.UseWaitCursor;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
	protected override Size DefaultSize => new Size(100, 100);

	internal IWebBrowser WebHost => webHost;

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler BackColorChanged
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for BackColorChanged");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler BackgroundImageChanged
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for BackgroundImageChanged");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler BackgroundImageLayoutChanged
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for BackgroundImageLayoutChanged");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler BindingContextChanged
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for BindingContextChanged");
		}
		remove
		{
		}
	}

	/// <summary>Occurs when the focus or keyboard user interface (UI) cues change.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event UICuesEventHandler ChangeUICues
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for ChangeUICues");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler Click
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for Click");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler CursorChanged
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for CursorChanged");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler DoubleClick
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for DoubleClick");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event DragEventHandler DragDrop
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for DragDrop");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event DragEventHandler DragEnter
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for DragEnter");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler DragLeave
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for DragLeave");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event DragEventHandler DragOver
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for DragOver");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler EnabledChanged
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for EnabledChanged");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler Enter
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for Enter");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler FontChanged
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for FontChanged");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler ForeColorChanged
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for ForeColorChanged");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event GiveFeedbackEventHandler GiveFeedback
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for GiveFeedback");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event HelpEventHandler HelpRequested
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for HelpRequested");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler ImeModeChanged
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for ImeModeChanged");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event KeyEventHandler KeyDown
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for KeyDown");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event KeyPressEventHandler KeyPress
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for KeyPress");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event KeyEventHandler KeyUp
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for KeyUp");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event LayoutEventHandler Layout
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for Layout");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler Leave
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for Leave");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler MouseCaptureChanged
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for MouseCaptureChanged");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event MouseEventHandler MouseClick
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for MouseClick");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event MouseEventHandler MouseDoubleClick
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for MouseDoubleClick");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event MouseEventHandler MouseDown
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for MouseDown");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler MouseEnter
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for MouseEnter");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler MouseHover
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for MouseHover");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler MouseLeave
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for MouseLeave");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event MouseEventHandler MouseMove
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for MouseMove");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event MouseEventHandler MouseUp
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for MouseUp");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event MouseEventHandler MouseWheel
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for MouseWheel");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event PaintEventHandler Paint
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for Paint");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for QueryAccessibilityHelp");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event QueryContinueDragEventHandler QueryContinueDrag
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for QueryContinueDrag");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler RightToLeftChanged
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for RightToLeftChanged");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler StyleChanged
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for StyleChanged");
		}
		remove
		{
		}
	}

	/// <summary>This event is not supported by this control.</summary>
	/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler TextChanged
	{
		add
		{
			throw new NotSupportedException("Invalid event handler for TextChanged");
		}
		remove
		{
		}
	}

	internal WebBrowserBase()
	{
		webHost = Manager.GetNewInstance();
		if (webHost.Load(Handle, base.Width, base.Height))
		{
			state = State.Loaded;
			webHost.MouseClick += OnWebHostMouseClick;
			webHost.Focus += OnWebHostFocus;
			webHost.CreateNewWindow += OnWebHostCreateNewWindow;
			webHost.LoadStarted += OnWebHostLoadStarted;
			webHost.LoadCommited += OnWebHostLoadCommited;
			webHost.ProgressChanged += OnWebHostProgressChanged;
			webHost.LoadFinished += OnWebHostLoadFinished;
			if (!suppressDialogs)
			{
				webHost.Alert += OnWebHostAlert;
			}
			webHost.StatusChanged += OnWebHostStatusChanged;
			webHost.SecurityChanged += OnWebHostSecurityChanged;
			webHost.ContextMenuShown += OnWebHostContextMenuShown;
		}
	}

	/// <summary>This method is not supported by this control.</summary>
	/// <param name="bitmap">A <see cref="T:System.Drawing.Bitmap" />.</param>
	/// <param name="targetBounds">A <see cref="T:System.Drawing.Rectangle" />. </param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
	{
		base.DrawToBitmap(bitmap, targetBounds);
	}

	/// <returns>true if the message was processed by the control; otherwise, false.</returns>
	/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the message to process. The possible values are WM_KEYDOWN, WM_SYSKEYDOWN, WM_CHAR, and WM_SYSCHAR. </param>
	public override bool PreProcessMessage(ref Message msg)
	{
		return base.PreProcessMessage(ref msg);
	}

	/// <summary>Called by the control when the underlying ActiveX control is created.</summary>
	/// <param name="nativeActiveXObject">An object that represents the underlying ActiveX control.</param>
	protected virtual void AttachInterfaces(object nativeActiveXObject)
	{
		throw new NotSupportedException("Retrieving a reference to an activex interface is not supported. Sorry.");
	}

	/// <summary>Called by the control to prepare it for listening to events. </summary>
	protected virtual void CreateSink()
	{
		throw new NotSupportedException("Retrieving a reference to an activex interface is not supported. Sorry.");
	}

	/// <summary>Returns a reference to the unmanaged ActiveX control site.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.WebBrowserSiteBase" /> that represents the site of the underlying ActiveX control.</returns>
	protected virtual WebBrowserSiteBase CreateWebBrowserSiteBase()
	{
		throw new NotSupportedException("Retrieving a reference to an activex interface is not supported. Sorry.");
	}

	/// <summary>Called by the control when the underlying ActiveX control is discarded.</summary>
	protected virtual void DetachInterfaces()
	{
		throw new NotSupportedException("Retrieving a reference to an activex interface is not supported. Sorry.");
	}

	/// <summary>Called by the control when it stops listening to events.</summary>
	protected virtual void DetachSink()
	{
		throw new NotSupportedException("Retrieving a reference to an activex interface is not supported. Sorry.");
	}

	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		WebHost.Shutdown();
		base.Dispose(disposing);
	}

	/// <returns>true if the character should be sent directly to the control and not preprocessed; otherwise, false.</returns>
	/// <param name="charCode">The character to test. </param>
	protected override bool IsInputChar(char charCode)
	{
		return base.IsInputChar(charCode);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnBackColorChanged(EventArgs e)
	{
		base.OnBackColorChanged(e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnFontChanged(EventArgs e)
	{
		base.OnFontChanged(e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnForeColorChanged(EventArgs e)
	{
		base.OnForeColorChanged(e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnGotFocus(EventArgs e)
	{
		base.OnGotFocus(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	/// <exception cref="T:System.Threading.ThreadStateException">The <see cref="P:System.Threading.Thread.ApartmentState" /> property of the application is not set to <see cref="F:System.Threading.ApartmentState.STA" />. </exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnLostFocus(EventArgs e)
	{
		base.OnLostFocus(e);
		WebHost.FocusOut();
	}

	/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.OnParentChanged(System.EventArgs)" />.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	/// <exception cref="T:System.Reflection.TargetInvocationException">Unable to get the window handle for the ActiveX control. Windowless ActiveX controls are not supported.</exception>
	protected override void OnParentChanged(EventArgs e)
	{
		base.OnParentChanged(e);
	}

	/// <summary>This method is not meaningful for this control.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object.</param>
	protected override void OnRightToLeftChanged(EventArgs e)
	{
		base.OnRightToLeftChanged(e);
	}

	/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.OnVisibleChanged(System.EventArgs)" />.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	/// <exception cref="T:System.Reflection.TargetInvocationException">Unable to get the window handle for the ActiveX control. Windowless ActiveX controls are not supported.</exception>
	protected override void OnVisibleChanged(EventArgs e)
	{
		base.OnVisibleChanged(e);
		if (base.Visible && !base.Disposing && !base.IsDisposed && state == State.Loaded)
		{
			state = State.Active;
			webHost.Activate();
		}
		else if (!base.Visible && state == State.Active)
		{
			state = State.Loaded;
			webHost.Deactivate();
		}
	}

	/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
	/// <param name="charCode">The character to process. </param>
	protected override bool ProcessMnemonic(char charCode)
	{
		return base.ProcessMnemonic(charCode);
	}

	/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" />.</summary>
	/// <param name="m">The windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
	}

	internal override void SetBoundsCoreInternal(int x, int y, int width, int height, BoundsSpecified specified)
	{
		base.SetBoundsCoreInternal(x, y, width, height, specified);
		webHost.Resize(width, height);
	}

	private void OnWebHostAlert(object sender, AlertEventArgs e)
	{
		switch (e.Type)
		{
		case DialogType.Alert:
			MessageBox.Show(e.Text, e.Title);
			break;
		case DialogType.AlertCheck:
		{
			AlertCheck alertCheck = new AlertCheck(e.Title, e.Text, e.CheckMessage, e.CheckState);
			alertCheck.Show();
			e.CheckState = alertCheck.Checked;
			e.BoolReturn = true;
			break;
		}
		case DialogType.Confirm:
		{
			DialogResult dialogResult3 = MessageBox.Show(e.Text, e.Title, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
			e.BoolReturn = dialogResult3 == DialogResult.OK;
			break;
		}
		case DialogType.ConfirmCheck:
		{
			ConfirmCheck confirmCheck = new ConfirmCheck(e.Title, e.Text, e.CheckMessage, e.CheckState);
			DialogResult dialogResult2 = confirmCheck.Show();
			e.CheckState = confirmCheck.Checked;
			e.BoolReturn = dialogResult2 == DialogResult.OK;
			break;
		}
		case DialogType.ConfirmEx:
			MessageBox.Show(e.Text, e.Title);
			break;
		case DialogType.Prompt:
		{
			Prompt prompt = new Prompt(e.Title, e.Text, e.Text2);
			DialogResult dialogResult = prompt.Show();
			e.StringReturn = prompt.Text;
			e.BoolReturn = dialogResult == DialogResult.OK;
			break;
		}
		case DialogType.PromptPassword:
			MessageBox.Show(e.Text, e.Title);
			break;
		case DialogType.PromptUsernamePassword:
			MessageBox.Show(e.Text, e.Title);
			break;
		case DialogType.Select:
			MessageBox.Show(e.Text, e.Title);
			break;
		}
	}

	private bool OnWebHostCreateNewWindow(object sender, CreateNewWindowEventArgs e)
	{
		return OnNewWindowInternal();
	}

	internal override void OnResizeInternal(EventArgs e)
	{
		base.OnResizeInternal(e);
		if (state == State.Active)
		{
			webHost.Resize(base.Width, base.Height);
		}
	}

	private void OnWebHostMouseClick(object sender, EventArgs e)
	{
	}

	private void OnWebHostFocus(object sender, EventArgs e)
	{
		Focus();
	}

	internal virtual bool OnNewWindowInternal()
	{
		return false;
	}

	internal virtual void OnWebHostLoadStarted(object sender, LoadStartedEventArgs e)
	{
	}

	internal virtual void OnWebHostLoadCommited(object sender, LoadCommitedEventArgs e)
	{
	}

	internal virtual void OnWebHostProgressChanged(object sender, Mono.WebBrowser.ProgressChangedEventArgs e)
	{
	}

	internal virtual void OnWebHostLoadFinished(object sender, LoadFinishedEventArgs e)
	{
	}

	internal virtual void OnWebHostSecurityChanged(object sender, SecurityChangedEventArgs e)
	{
	}

	internal virtual void OnWebHostContextMenuShown(object sender, ContextMenuEventArgs e)
	{
	}

	internal virtual void OnWebHostStatusChanged(object sender, StatusChangedEventArgs e)
	{
	}
}
