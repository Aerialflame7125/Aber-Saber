using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace System.Windows.Forms;

/// <summary>Represents a text box in a <see cref="T:System.Windows.Forms.ToolStrip" /> that allows the user to enter text.</summary>
/// <filterpriority>2</filterpriority>
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
public class ToolStripTextBox : ToolStripControlHost
{
	private class ToolStripTextBoxControl : TextBox
	{
		private BorderStyle border;

		private Timer tooltip_timer;

		private ToolTip tooltip_window;

		private ToolStripItem owner_item;

		internal BorderStyle Border
		{
			set
			{
				border = value;
				Invalidate();
			}
		}

		internal ToolStripItem OwnerItem
		{
			set
			{
				owner_item = value;
			}
		}

		private bool ShowToolTips
		{
			get
			{
				if (base.Parent == null)
				{
					return false;
				}
				return (base.Parent as ToolStrip).ShowItemToolTips;
			}
		}

		private Timer ToolTipTimer
		{
			get
			{
				if (tooltip_timer == null)
				{
					tooltip_timer = new Timer();
					tooltip_timer.Enabled = false;
					tooltip_timer.Interval = 500;
					tooltip_timer.Tick += ToolTipTimer_Tick;
				}
				return tooltip_timer;
			}
		}

		private ToolTip ToolTipWindow
		{
			get
			{
				if (tooltip_window == null)
				{
					tooltip_window = new ToolTip();
				}
				return tooltip_window;
			}
		}

		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			Invalidate();
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			Invalidate();
			if (ShowToolTips)
			{
				ToolTipTimer.Start();
			}
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			Invalidate();
			ToolTipTimer.Stop();
			ToolTipWindow.Hide(this);
		}

		internal override void OnPaintInternal(PaintEventArgs e)
		{
			base.OnPaintInternal(e);
			if ((!Focused && !base.Entered && border != BorderStyle.FixedSingle) || border == BorderStyle.None)
			{
				return;
			}
			ToolStripRenderer renderer = (base.Parent as ToolStrip).Renderer;
			if (renderer is ToolStripProfessionalRenderer)
			{
				using (Pen pen = new Pen((renderer as ToolStripProfessionalRenderer).ColorTable.ButtonSelectedBorder))
				{
					e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, base.Width - 1, base.Height - 1));
				}
			}
		}

		private void ToolTipTimer_Tick(object o, EventArgs args)
		{
			string toolTip = owner_item.GetToolTip();
			if (!string.IsNullOrEmpty(toolTip))
			{
				ToolTipWindow.Present(this, toolTip);
			}
			ToolTipTimer.Stop();
		}
	}

	private BorderStyle border_style;

	private static object AcceptsTabChangedEvent;

	private static object BorderStyleChangedEvent;

	private static object HideSelectionChangedEvent;

	private static object ModifiedChangedEvent;

	private static object MultilineChangedEvent;

	private static object ReadOnlyChangedEvent;

	private static object TextBoxTextAlignChangedEvent;

	/// <summary>Gets or sets a value indicating whether pressing ENTER in a multiline <see cref="T:System.Windows.Forms.TextBox" /> control creates a new line of text in the control or activates the default button for the form.</summary>
	/// <returns>true if the ENTER key creates a new line of text in a multiline version of the control; false if the ENTER key activates the default button for the form. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	public bool AcceptsReturn
	{
		get
		{
			return TextBox.AcceptsReturn;
		}
		set
		{
			TextBox.AcceptsReturn = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether pressing the TAB key in a multiline text box control types a TAB character in the control instead of moving the focus to the next control in the tab order.</summary>
	/// <returns>true if users can enter tabs in a multiline text box using the TAB key; false if pressing the TAB key moves the focus. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	public bool AcceptsTab
	{
		get
		{
			return TextBox.AcceptsTab;
		}
		set
		{
			TextBox.AcceptsTab = value;
		}
	}

	/// <summary>Gets or sets a custom string collection to use when the <see cref="P:System.Windows.Forms.ToolStripTextBox.AutoCompleteSource" /> property is set to CustomSource.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" /> to use with <see cref="P:System.Windows.Forms.TextBox.AutoCompleteSource" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[System.MonoTODO("AutoCompletion algorithm is currently not implemented.")]
	[Browsable(true)]
	[Localizable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public AutoCompleteStringCollection AutoCompleteCustomSource
	{
		get
		{
			return TextBox.AutoCompleteCustomSource;
		}
		set
		{
			TextBox.AutoCompleteCustomSource = value;
		}
	}

	/// <summary>Gets or sets an option that controls how automatic completion works for the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.AutoCompleteMode" /> values. The default is <see cref="F:System.Windows.Forms.AutoCompleteMode.None" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[DefaultValue(AutoCompleteMode.None)]
	[Browsable(true)]
	[System.MonoTODO("AutoCompletion algorithm is currently not implemented.")]
	public AutoCompleteMode AutoCompleteMode
	{
		get
		{
			return TextBox.AutoCompleteMode;
		}
		set
		{
			TextBox.AutoCompleteMode = value;
		}
	}

	/// <summary>Gets or sets a value specifying the source of complete strings used for automatic completion.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.AutoCompleteSource" /> values. The default is <see cref="F:System.Windows.Forms.AutoCompleteSource.None" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(AutoCompleteSource.None)]
	[System.MonoTODO("AutoCompletion algorithm is currently not implemented.")]
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public AutoCompleteSource AutoCompleteSource
	{
		get
		{
			return TextBox.AutoCompleteSource;
		}
		set
		{
			TextBox.AutoCompleteSource = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>Gets or sets the border type of the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DispId(-504)]
	[DefaultValue(BorderStyle.Fixed3D)]
	public BorderStyle BorderStyle
	{
		get
		{
			return border_style;
		}
		set
		{
			if (border_style != value)
			{
				border_style = value;
				(base.Control as ToolStripTextBoxControl).Border = value;
				OnBorderStyleChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets a value indicating whether the user can undo the previous operation in a <see cref="T:System.Windows.Forms.ToolStripTextBox" /> control.</summary>
	/// <returns>true if the user can undo the previous operation performed in a text box control; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanUndo => TextBox.CanUndo;

	/// <summary>Gets or sets whether the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> control modifies the case of characters as they are typed.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.CharacterCasing" /> values. The default is <see cref="F:System.Windows.Forms.CharacterCasing.Normal" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(CharacterCasing.Normal)]
	public CharacterCasing CharacterCasing
	{
		get
		{
			return TextBox.CharacterCasing;
		}
		set
		{
			TextBox.CharacterCasing = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the selected text in the text box control remains highlighted when the control loses focus.</summary>
	/// <returns>true if the selected text does not appear highlighted when the text box control loses focus; false, if the selected text remains highlighted when the text box control loses focus. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	public bool HideSelection
	{
		get
		{
			return TextBox.HideSelection;
		}
		set
		{
			TextBox.HideSelection = value;
		}
	}

	/// <summary>Gets or sets the lines of text in a <see cref="T:System.Windows.Forms.ToolStripTextBox" /> control.</summary>
	/// <returns>An array of strings that contains the text in a text box control.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string[] Lines
	{
		get
		{
			return TextBox.Lines;
		}
		set
		{
			TextBox.Lines = value;
		}
	}

	/// <summary>Gets or sets the maximum number of characters the user can type or paste into the text box control.</summary>
	/// <returns>The number of characters that can be entered into the control. The default is 32767 characters.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(32767)]
	public int MaxLength
	{
		get
		{
			return TextBox.MaxLength;
		}
		set
		{
			TextBox.MaxLength = value;
		}
	}

	/// <summary>Gets or sets a value that indicates that the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> control has been modified by the user since the control was created or its contents were last set.</summary>
	/// <returns>true if the control's contents have been modified; otherwise, false. </returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool Modified
	{
		get
		{
			return TextBox.Modified;
		}
		set
		{
			TextBox.Modified = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true if enabled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DefaultValue(false)]
	[RefreshProperties(RefreshProperties.All)]
	[Localizable(true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool Multiline
	{
		get
		{
			return TextBox.Multiline;
		}
		set
		{
			TextBox.Multiline = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether text in the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> is read-only.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> is read-only; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool ReadOnly
	{
		get
		{
			return TextBox.ReadOnly;
		}
		set
		{
			TextBox.ReadOnly = value;
		}
	}

	/// <summary>Gets or sets a value indicating the currently selected text in the control.</summary>
	/// <returns>A string that represents the currently selected text in the text box.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string SelectedText
	{
		get
		{
			return TextBox.SelectedText;
		}
		set
		{
			TextBox.SelectedText = value;
		}
	}

	/// <summary>Gets or sets the number of characters selected in the<see cref="T:System.Windows.Forms.ToolStripTextBox" />.</summary>
	/// <returns>The number of characters selected in the<see cref="T:System.Windows.Forms.ToolStripTextBox" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int SelectionLength
	{
		get
		{
			return (TextBox.SelectionLength != -1) ? TextBox.SelectionLength : 0;
		}
		set
		{
			TextBox.SelectionLength = value;
		}
	}

	/// <summary>Gets or sets the starting point of text selected in the<see cref="T:System.Windows.Forms.ToolStripTextBox" />.</summary>
	/// <returns>The starting position of text selected in the<see cref="T:System.Windows.Forms.ToolStripTextBox" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int SelectionStart
	{
		get
		{
			return TextBox.SelectionStart;
		}
		set
		{
			TextBox.SelectionStart = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the defined shortcuts are enabled.</summary>
	/// <returns>true to enable the shortcuts; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	public bool ShortcutsEnabled
	{
		get
		{
			return TextBox.ShortcutsEnabled;
		}
		set
		{
			TextBox.ShortcutsEnabled = value;
		}
	}

	/// <summary>Gets the hosted <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
	/// <returns>The hosted <see cref="T:System.Windows.Forms.TextBox" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public TextBox TextBox => (TextBox)base.Control;

	/// <summary>Gets or sets how text is aligned in a <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> enumeration values that specifies how text is aligned in the control. The default is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
	[Localizable(true)]
	[DefaultValue(HorizontalAlignment.Left)]
	public HorizontalAlignment TextBoxTextAlign
	{
		get
		{
			return TextBox.TextAlign;
		}
		set
		{
			TextBox.TextAlign = value;
		}
	}

	/// <summary>Gets the length of text in the control.</summary>
	/// <returns>The number of characters contained in the text of the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public int TextLength => TextBox.TextLength;

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true if enabled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	[Localizable(true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public bool WordWrap
	{
		get
		{
			return TextBox.WordWrap;
		}
		set
		{
			TextBox.WordWrap = value;
		}
	}

	/// <summary>Gets the spacing, in pixels, between the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> and adjacent items.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing.</returns>
	protected internal override Padding DefaultMargin => new Padding(1, 0, 1, 0);

	/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</summary>
	/// <returns>The default <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> in pixels. The default size is 100 pixels by 25 pixels.</returns>
	protected override Size DefaultSize => new Size(100, 22);

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.AcceptsTab" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler AcceptsTabChanged
	{
		add
		{
			base.Events.AddHandler(AcceptsTabChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(AcceptsTabChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.BorderStyle" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler BorderStyleChanged
	{
		add
		{
			base.Events.AddHandler(BorderStyleChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(BorderStyleChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.HideSelection" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler HideSelectionChanged
	{
		add
		{
			base.Events.AddHandler(HideSelectionChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(HideSelectionChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.Modified" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler ModifiedChanged
	{
		add
		{
			base.Events.AddHandler(ModifiedChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ModifiedChangedEvent, value);
		}
	}

	/// <summary>This event is not relevant to this class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public event EventHandler MultilineChanged
	{
		add
		{
			base.Events.AddHandler(MultilineChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MultilineChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.ReadOnly" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler ReadOnlyChanged
	{
		add
		{
			base.Events.AddHandler(ReadOnlyChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ReadOnlyChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.TextBoxTextAlign" /> property changes.</summary>
	public event EventHandler TextBoxTextAlignChanged
	{
		add
		{
			base.Events.AddHandler(TextBoxTextAlignChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(TextBoxTextAlignChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> class.</summary>
	public ToolStripTextBox()
		: base(new ToolStripTextBoxControl())
	{
		ToolStripTextBoxControl toolStripTextBoxControl = TextBox as ToolStripTextBoxControl;
		toolStripTextBoxControl.OwnerItem = this;
		toolStripTextBoxControl.border_style = BorderStyle.None;
		toolStripTextBoxControl.TopMargin = 3;
		toolStripTextBoxControl.Border = BorderStyle.Fixed3D;
		border_style = BorderStyle.Fixed3D;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> class derived from a base control.</summary>
	/// <param name="c">The control from which to derive the <see cref="T:System.Windows.Forms.ToolStripTextBox" />. </param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public ToolStripTextBox(Control c)
		: base(c)
	{
		throw new NotSupportedException("This construtor cannot be used.");
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> class with the specified name. </summary>
	/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</param>
	public ToolStripTextBox(string name)
		: this()
	{
		base.Name = name;
	}

	static ToolStripTextBox()
	{
		AcceptsTabChanged = new object();
		BorderStyleChanged = new object();
		HideSelectionChanged = new object();
		ModifiedChanged = new object();
		MultilineChanged = new object();
		ReadOnlyChanged = new object();
		TextBoxTextAlignChanged = new object();
	}

	/// <summary>Appends text to the current text of the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</summary>
	/// <param name="text">The text to append to the current contents of the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void AppendText(string text)
	{
		TextBox.AppendText(text);
	}

	/// <summary>Clears all text from the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Clear()
	{
		TextBox.Clear();
	}

	/// <summary>Clears information about the most recent operation from the undo buffer of the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ClearUndo()
	{
		TextBox.ClearUndo();
	}

	/// <summary>Copies the current selection in the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> to the Clipboard.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Copy()
	{
		TextBox.Copy();
	}

	/// <summary>Moves the current selection in the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> to the Clipboard.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Cut()
	{
		TextBox.Cut();
	}

	/// <summary>Specifies that the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.SelectionLength" /> property is zero so that no characters are selected in the control.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void DeselectAll()
	{
		TextBox.DeselectAll();
	}

	/// <summary>Retrieves the character that is closest to the specified location within the control.</summary>
	/// <returns>The character at the specified location.</returns>
	/// <param name="pt">The location from which to seek the nearest character.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public char GetCharFromPosition(Point pt)
	{
		return TextBox.GetCharFromPosition(pt);
	}

	/// <summary>Retrieves the index of the character nearest to the specified location.</summary>
	/// <returns>The zero-based character index at the specified location.</returns>
	/// <param name="pt">The location to search.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int GetCharIndexFromPosition(Point pt)
	{
		return TextBox.GetCharIndexFromPosition(pt);
	}

	/// <summary>Retrieves the index of the first character of a given line.</summary>
	/// <returns>The zero-based character index in the specified line.</returns>
	/// <param name="lineNumber">The line for which to get the index of its first character.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int GetFirstCharIndexFromLine(int lineNumber)
	{
		return TextBox.GetFirstCharIndexFromLine(lineNumber);
	}

	/// <summary>Retrieves the index of the first character of the current line.</summary>
	/// <returns>The zero-based character index in the current line.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int GetFirstCharIndexOfCurrentLine()
	{
		return TextBox.GetFirstCharIndexOfCurrentLine();
	}

	/// <summary>Retrieves the line number from the specified character position within the text of the control.</summary>
	/// <returns>The zero-based line number in which the character index is located.</returns>
	/// <param name="index">The character index position to search.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int GetLineFromCharIndex(int index)
	{
		return TextBox.GetLineFromCharIndex(index);
	}

	/// <summary>Retrieves the location within the control at the specified character index.</summary>
	/// <returns>The location of the specified character.</returns>
	/// <param name="index">The index of the character for which to retrieve the location.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Point GetPositionFromCharIndex(int index)
	{
		return TextBox.GetPositionFromCharIndex(index);
	}

	/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
	/// <param name="constrainingSize">The custom-sized area for a control. </param>
	/// <filterpriority>1</filterpriority>
	public override Size GetPreferredSize(Size constrainingSize)
	{
		return base.GetPreferredSize(constrainingSize);
	}

	/// <summary>Replaces the current selection in the text box with the contents of the Clipboard.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Paste()
	{
		TextBox.Paste();
	}

	/// <summary>Scrolls the contents of the control to the current caret position.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ScrollToCaret()
	{
		TextBox.ScrollToCaret();
	}

	/// <summary>Selects a range of text in the text box.</summary>
	/// <param name="start">The position of the first character in the current text selection within the text box.</param>
	/// <param name="length">The number of characters to select.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Select(int start, int length)
	{
		TextBox.Select(start, length);
	}

	/// <summary>Selects all text in the text box.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SelectAll()
	{
		TextBox.SelectAll();
	}

	/// <summary>Undoes the last edit operation in the text box.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Undo()
	{
		TextBox.Undo();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripTextBox.AcceptsTabChanged" /> event. </summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnAcceptsTabChanged(EventArgs e)
	{
		((EventHandler)base.Events[AcceptsTabChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripTextBox.BorderStyleChanged" /> event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnBorderStyleChanged(EventArgs e)
	{
		((EventHandler)base.Events[BorderStyleChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripTextBox.HideSelectionChanged" /> event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnHideSelectionChanged(EventArgs e)
	{
		((EventHandler)base.Events[HideSelectionChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripTextBox.ModifiedChanged" /> event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnModifiedChanged(EventArgs e)
	{
		((EventHandler)base.Events[ModifiedChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripTextBox.MultilineChanged" /> event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnMultilineChanged(EventArgs e)
	{
		((EventHandler)base.Events[MultilineChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripTextBox.ReadOnlyChanged" /> event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnReadOnlyChanged(EventArgs e)
	{
		((EventHandler)base.Events[ReadOnlyChanged])?.Invoke(this, e);
	}

	/// <param name="control">The control from which to subscribe events.</param>
	protected override void OnSubscribeControlEvents(Control control)
	{
		base.OnSubscribeControlEvents(control);
		TextBox.AcceptsTabChanged += HandleAcceptsTabChanged;
		TextBox.HideSelectionChanged += HandleHideSelectionChanged;
		TextBox.ModifiedChanged += HandleModifiedChanged;
		TextBox.MultilineChanged += HandleMultilineChanged;
		TextBox.ReadOnlyChanged += HandleReadOnlyChanged;
		TextBox.TextAlignChanged += HandleTextAlignChanged;
		TextBox.TextChanged += HandleTextChanged;
	}

	/// <param name="control">The control from which to unsubscribe events.</param>
	protected override void OnUnsubscribeControlEvents(Control control)
	{
		base.OnUnsubscribeControlEvents(control);
	}

	private void HandleTextAlignChanged(object sender, EventArgs e)
	{
		((EventHandler)base.Events[TextBoxTextAlignChanged])?.Invoke(this, e);
	}

	private void HandleReadOnlyChanged(object sender, EventArgs e)
	{
		OnReadOnlyChanged(e);
	}

	private void HandleMultilineChanged(object sender, EventArgs e)
	{
		OnMultilineChanged(e);
	}

	private void HandleModifiedChanged(object sender, EventArgs e)
	{
		OnModifiedChanged(e);
	}

	private void HandleHideSelectionChanged(object sender, EventArgs e)
	{
		OnHideSelectionChanged(e);
	}

	private void HandleAcceptsTabChanged(object sender, EventArgs e)
	{
		OnAcceptsTabChanged(e);
	}

	private void HandleTextChanged(object sender, EventArgs e)
	{
		OnTextChanged(e);
	}
}
