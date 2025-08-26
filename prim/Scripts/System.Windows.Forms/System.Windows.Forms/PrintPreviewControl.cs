using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents the raw preview part of print previewing from a Windows Forms application, without any dialog boxes or buttons. Most <see cref="T:System.Windows.Forms.PrintPreviewControl" /> objects are found on <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> objects, but they do not have to be.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
[DefaultProperty("Document")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class PrintPreviewControl : Control
{
	private bool autozoom;

	private int columns;

	private int rows;

	private int startPage;

	private double zoom;

	private int padding = ThemeEngine.Current.PrintPreviewControlPadding;

	private PrintDocument document;

	internal PreviewPrintController controller;

	internal PreviewPageInfo[] page_infos;

	private VScrollBar vbar;

	private HScrollBar hbar;

	internal Rectangle ViewPort;

	internal Image[] image_cache;

	private Size image_size;

	private static object StartPageChangedEvent;

	internal int vbar_value;

	internal int hbar_value;

	/// <summary>Gets or sets a value indicating whether resizing the control or changing the number of pages shown automatically adjusts the <see cref="P:System.Windows.Forms.PrintPreviewControl.Zoom" /> property.</summary>
	/// <returns>true if the changing the control size or number of pages adjusts the <see cref="P:System.Windows.Forms.PrintPreviewControl.Zoom" /> property; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	public bool AutoZoom
	{
		get
		{
			return autozoom;
		}
		set
		{
			autozoom = value;
			InvalidateLayout();
		}
	}

	/// <summary>Gets or sets the number of pages displayed horizontally across the screen.</summary>
	/// <returns>The number of pages displayed horizontally across the screen. The default is 1.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The set value is less than 1.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(1)]
	public int Columns
	{
		get
		{
			return columns;
		}
		set
		{
			columns = value;
			InvalidateLayout();
		}
	}

	/// <summary>Gets or sets a value indicating the document to preview.</summary>
	/// <returns>The <see cref="T:System.Drawing.Printing.PrintDocument" /> representing the document to preview.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(null)]
	public PrintDocument Document
	{
		get
		{
			return document;
		}
		set
		{
			document = value;
		}
	}

	/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values. The default is <see cref="F:System.Windows.Forms.RightToLeft.Inherit" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[AmbientValue(RightToLeft.Inherit)]
	[Localizable(true)]
	public override RightToLeft RightToLeft
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

	/// <summary>Gets or sets the number of pages displayed vertically down the screen.</summary>
	/// <returns>The number of pages displayed vertically down the screen. The default is 0.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The set value is less than 1.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(1)]
	public int Rows
	{
		get
		{
			return rows;
		}
		set
		{
			rows = value;
			InvalidateLayout();
		}
	}

	/// <summary>Gets or sets the page number of the upper left page.</summary>
	/// <returns>The page number of the upper left page. The default is 0.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The set value is less than 0.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(0)]
	public int StartPage
	{
		get
		{
			return startPage;
		}
		set
		{
			if (value < 1)
			{
				return;
			}
			if (document != null && value + (Rows + 1) * Columns > page_infos.Length + 1)
			{
				value = page_infos.Length + 1 - (Rows + 1) * Columns;
				if (value < 1)
				{
					value = 1;
				}
			}
			int num = StartPage;
			startPage = value;
			if (num != startPage)
			{
				InvalidateLayout();
				OnStartPageChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the text associated with this control.</summary>
	/// <returns>The text associated with this control.</returns>
	/// <filterpriority>1</filterpriority>
	[Bindable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override string Text
	{
		get
		{
			return base.Text;
		}
		set
		{
			base.Text = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether printing uses the anti-aliasing features of the operating system.</summary>
	/// <returns>true if anti-aliasing is used; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	public bool UseAntiAlias
	{
		get
		{
			return controller.UseAntiAlias;
		}
		set
		{
			controller.UseAntiAlias = value;
		}
	}

	/// <summary>Gets or sets a value indicating how large the pages will appear.</summary>
	/// <returns>A value indicating how large the pages will appear. A value of 1.0 indicates full size.</returns>
	/// <exception cref="T:System.ArgumentException">The value is less than or equal to 0. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(0.3)]
	public double Zoom
	{
		get
		{
			return zoom;
		}
		set
		{
			if (value <= 0.0)
			{
				throw new ArgumentException("zoom");
			}
			autozoom = false;
			zoom = value;
			InvalidateLayout();
		}
	}

	/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.CreateParams" /> property.</summary>
	protected override CreateParams CreateParams => base.CreateParams;

	internal ScrollBar UIAVScrollBar => vbar;

	internal ScrollBar UIAHScrollBar => hbar;

	/// <summary>Occurs when the start page changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler StartPageChanged
	{
		add
		{
			base.Events.AddHandler(StartPageChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(StartPageChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewControl.Text" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler TextChanged
	{
		add
		{
			base.TextChanged += value;
		}
		remove
		{
			base.TextChanged -= value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PrintPreviewControl" /> class.</summary>
	public PrintPreviewControl()
	{
		autozoom = true;
		columns = 1;
		rows = 0;
		startPage = 1;
		BackColor = SystemColors.AppWorkspace;
		controller = new PreviewPrintController();
		vbar = new ImplicitVScrollBar();
		hbar = new ImplicitHScrollBar();
		vbar.Visible = false;
		hbar.Visible = false;
		vbar.ValueChanged += VScrollBarValueChanged;
		hbar.ValueChanged += HScrollBarValueChanged;
		SuspendLayout();
		base.Controls.AddImplicit(vbar);
		base.Controls.AddImplicit(hbar);
		ResumeLayout();
	}

	static PrintPreviewControl()
	{
		StartPageChanged = new object();
	}

	internal void GeneratePreview()
	{
		if (document == null)
		{
			return;
		}
		try
		{
			if (page_infos == null)
			{
				if (document.PrintController == null || !(document.PrintController is PrintControllerWithStatusDialog))
				{
					document.PrintController = new PrintControllerWithStatusDialog(controller);
				}
				document.Print();
				page_infos = controller.GetPreviewPageInfo();
			}
			if (image_cache == null)
			{
				image_cache = new Image[page_infos.Length];
				if (page_infos.Length > 0)
				{
					image_size = ThemeEngine.Current.PrintPreviewControlGetPageSize(this);
					if (image_size.Width >= 0 && image_size.Width < page_infos[0].Image.Width && image_size.Height >= 0 && image_size.Height < page_infos[0].Image.Height)
					{
						for (int i = 0; i < page_infos.Length; i++)
						{
							image_cache[i] = new Bitmap(image_size.Width, image_size.Height);
							Graphics graphics = Graphics.FromImage(image_cache[i]);
							graphics.DrawImage(page_infos[i].Image, new Rectangle(new Point(0, 0), image_size), 0, 0, page_infos[i].Image.Width, page_infos[i].Image.Height, GraphicsUnit.Pixel);
							graphics.Dispose();
						}
					}
				}
			}
			UpdateScrollBars();
		}
		catch (Exception ex)
		{
			page_infos = new PreviewPageInfo[0];
			image_cache = new Image[0];
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}

	/// <summary>Refreshes the preview of the document.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void InvalidatePreview()
	{
		if (page_infos != null)
		{
			for (int i = 0; i < page_infos.Length; i++)
			{
				if (page_infos[i].Image != null)
				{
					page_infos[i].Image.Dispose();
				}
			}
			page_infos = null;
		}
		InvalidateLayout();
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.BackColor" /> property to <see cref="P:System.Drawing.SystemColors.AppWorkspace" />, which is the default color.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override void ResetBackColor()
	{
		base.ResetBackColor();
	}

	/// <summary>Resets the foreground color of the <see cref="T:System.Windows.Forms.PrintPreviewControl" /> to <see cref="P:System.Drawing.Color.White" />, which is the default color.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override void ResetForeColor()
	{
		base.ResetForeColor();
	}

	/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.OnPaint(System.Windows.Forms.PaintEventArgs)" /> method.</summary>
	protected override void OnPaint(PaintEventArgs pevent)
	{
		if (page_infos == null || image_cache == null)
		{
			GeneratePreview();
		}
		ThemeEngine.Current.PrintPreviewControlPaint(pevent, this, image_size);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
	/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnResize(EventArgs eventargs)
	{
		InvalidateLayout();
		base.OnResize(eventargs);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.PrintPreviewControl.StartPageChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnStartPageChanged(EventArgs e)
	{
		((EventHandler)base.Events[StartPageChanged])?.Invoke(this, e);
	}

	/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" /> method.</summary>
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
	}

	private void VScrollBarValueChanged(object sender, EventArgs e)
	{
		int yAmount = ((vbar.Value <= vbar_value) ? (vbar_value - vbar.Value) : (-1 * (vbar.Value - vbar_value)));
		vbar_value = vbar.Value;
		XplatUI.ScrollWindow(Handle, ViewPort, 0, yAmount, with_children: false);
	}

	private void HScrollBarValueChanged(object sender, EventArgs e)
	{
		int xAmount = ((hbar.Value <= hbar_value) ? (hbar_value - hbar.Value) : (-1 * (hbar.Value - hbar_value)));
		hbar_value = hbar.Value;
		XplatUI.ScrollWindow(Handle, ViewPort, xAmount, 0, with_children: false);
	}

	private void UpdateScrollBars()
	{
		ViewPort = base.ClientRectangle;
		if (!AutoZoom)
		{
			int num = image_size.Width * Columns + (Columns + 1) * padding;
			int num2 = image_size.Height * (Rows + 1) + (Rows + 2) * padding;
			bool flag = false;
			bool flag2 = false;
			if (num > base.ClientRectangle.Width)
			{
				flag2 = true;
				ViewPort.Height -= hbar.Height;
			}
			if (num2 > ViewPort.Height)
			{
				flag = true;
				ViewPort.Width -= vbar.Width;
			}
			if (!flag2 && num > ViewPort.Width)
			{
				flag2 = true;
				ViewPort.Height -= hbar.Height;
			}
			SuspendLayout();
			if (flag)
			{
				vbar.SetValues(num2, ViewPort.Height);
				vbar.Bounds = new Rectangle(base.ClientRectangle.Width - vbar.Width, 0, vbar.Width, base.ClientRectangle.Height - (flag2 ? SystemInformation.VerticalScrollBarWidth : 0));
				vbar.Visible = true;
				vbar_value = vbar.Value;
			}
			else
			{
				vbar.Visible = false;
			}
			if (flag2)
			{
				hbar.SetValues(num, ViewPort.Width);
				hbar.Bounds = new Rectangle(0, base.ClientRectangle.Height - hbar.Height, base.ClientRectangle.Width - (flag ? SystemInformation.HorizontalScrollBarHeight : 0), hbar.Height);
				hbar.Visible = true;
				hbar_value = hbar.Value;
			}
			else
			{
				hbar.Visible = false;
			}
			ResumeLayout(performLayout: false);
		}
	}

	private void InvalidateLayout()
	{
		if (image_cache != null)
		{
			for (int i = 0; i < image_cache.Length; i++)
			{
				if (image_cache[i] != null)
				{
					image_cache[i].Dispose();
				}
			}
			image_cache = null;
		}
		Invalidate();
	}
}
