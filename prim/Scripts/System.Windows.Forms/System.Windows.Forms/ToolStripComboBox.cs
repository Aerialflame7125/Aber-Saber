using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace System.Windows.Forms;

/// <summary>Represents a <see cref="T:System.Windows.Forms.ToolStripComboBox" /> that is properly rendered in a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
/// <filterpriority>2</filterpriority>
[DefaultProperty("Items")]
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
public class ToolStripComboBox : ToolStripControlHost
{
	private class ToolStripComboBoxControl : ComboBox
	{
		public ToolStripComboBoxControl()
		{
			border_style = BorderStyle.None;
			base.FlatStyle = FlatStyle.Popup;
		}
	}

	private static object DropDownEvent;

	private static object DropDownClosedEvent;

	private static object DropDownStyleChangedEvent;

	private static object SelectedIndexChangedEvent;

	private static object TextUpdateEvent;

	/// <summary>Gets or sets the custom string collection to use when the <see cref="P:System.Windows.Forms.ToolStripComboBox.AutoCompleteSource" /> property is set to <see cref="F:System.Windows.Forms.AutoCompleteSource.CustomSource" />.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" /> that contains the strings.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public AutoCompleteStringCollection AutoCompleteCustomSource
	{
		get
		{
			return ComboBox.AutoCompleteCustomSource;
		}
		set
		{
			ComboBox.AutoCompleteCustomSource = value;
		}
	}

	/// <summary>Gets or sets a value that indicates the text completion behavior of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.AutoCompleteMode" /> values. The default is <see cref="F:System.Windows.Forms.AutoCompleteMode.None" />.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(AutoCompleteMode.None)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
	public AutoCompleteMode AutoCompleteMode
	{
		get
		{
			return ComboBox.AutoCompleteMode;
		}
		set
		{
			ComboBox.AutoCompleteMode = value;
		}
	}

	/// <summary>Gets or sets the source of complete strings used for automatic completion.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.AutoCompleteSource" /> values. The default is <see cref="F:System.Windows.Forms.AutoCompleteSource.None" />.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
	[DefaultValue(AutoCompleteSource.None)]
	public AutoCompleteSource AutoCompleteSource
	{
		get
		{
			return ComboBox.AutoCompleteSource;
		}
		set
		{
			ComboBox.AutoCompleteSource = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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
	/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" />.</returns>
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

	/// <summary>Gets a <see cref="T:System.Windows.Forms.ComboBox" /> in which the user can enter text, along with a list from which the user can select.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ComboBox" /> for a <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public ComboBox ComboBox => (ComboBox)base.Control;

	/// <summary>Gets or sets the height, in pixels, of the drop-down portion box of a <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
	/// <returns>The height, in pixels, of the drop-down box.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(106)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
	public int DropDownHeight
	{
		get
		{
			return ComboBox.DropDownHeight;
		}
		set
		{
			ComboBox.DropDownHeight = value;
		}
	}

	/// <summary>Gets or sets a value specifying the style of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ComboBoxStyle" /> values. The default is <see cref="F:System.Windows.Forms.ComboBoxStyle.DropDown" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(ComboBoxStyle.DropDown)]
	[RefreshProperties(RefreshProperties.Repaint)]
	public ComboBoxStyle DropDownStyle
	{
		get
		{
			return ComboBox.DropDownStyle;
		}
		set
		{
			ComboBox.DropDownStyle = value;
		}
	}

	/// <summary>Gets or sets the width, in pixels, of the drop-down portion of a <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
	/// <returns>The width, in pixels, of the drop-down box.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int DropDownWidth
	{
		get
		{
			return ComboBox.DropDownWidth;
		}
		set
		{
			ComboBox.DropDownWidth = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> currently displays its drop-down portion.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> currently displays its drop-down portion; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool DroppedDown
	{
		get
		{
			return ComboBox.DroppedDown;
		}
		set
		{
			ComboBox.DroppedDown = value;
		}
	}

	/// <summary>Gets or sets the appearance of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
	/// <returns>One of the values of <see cref="T:System.Windows.Forms.FlatStyle" />. The options are <see cref="F:System.Windows.Forms.FlatStyle.Flat" />, <see cref="F:System.Windows.Forms.FlatStyle.Popup" />, <see cref="F:System.Windows.Forms.FlatStyle.Standard" />, and <see cref="F:System.Windows.Forms.FlatStyle.System" />. The default is <see cref="F:System.Windows.Forms.FlatStyle.Popup" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(FlatStyle.Popup)]
	[Localizable(true)]
	public FlatStyle FlatStyle
	{
		get
		{
			return ComboBox.FlatStyle;
		}
		set
		{
			ComboBox.FlatStyle = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> should resize to avoid showing partial items.</summary>
	/// <returns>true if the list portion can contain only complete items; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	[Localizable(true)]
	public bool IntegralHeight
	{
		get
		{
			return ComboBox.IntegralHeight;
		}
		set
		{
			ComboBox.IntegralHeight = value;
		}
	}

	/// <summary>Gets a collection of the items contained in this <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
	/// <returns>A collection of items.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[Localizable(true)]
	[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public ComboBox.ObjectCollection Items => ComboBox.Items;

	/// <summary>Gets or sets the maximum number of items to be shown in the drop-down portion of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
	/// <returns>The maximum number of items in the drop-down portion. The minimum for this property is 1 and the maximum is 100.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[DefaultValue(8)]
	public int MaxDropDownItems
	{
		get
		{
			return ComboBox.MaxDropDownItems;
		}
		set
		{
			ComboBox.MaxDropDownItems = value;
		}
	}

	/// <summary>Gets or sets the maximum number of characters allowed in the editable portion of a combo box.</summary>
	/// <returns>The maximum number of characters the user can enter. Values of less than zero are reset to zero, which is the default value.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[DefaultValue(0)]
	public int MaxLength
	{
		get
		{
			return ComboBox.MaxLength;
		}
		set
		{
			ComboBox.MaxLength = value;
		}
	}

	/// <summary>Gets or sets the index specifying the currently selected item.</summary>
	/// <returns>A zero-based index of the currently selected item. A value of negative one (-1) is returned if no item is selected.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public int SelectedIndex
	{
		get
		{
			return ComboBox.SelectedIndex;
		}
		set
		{
			ComboBox.SelectedIndex = value;
			if (ComboBox.SelectedIndex >= 0)
			{
				Text = Items[value].ToString();
			}
		}
	}

	/// <summary>Gets or sets currently selected item in the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
	/// <returns>The object that is the currently selected item or null if there is no currently selected item.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Bindable(true)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public object SelectedItem
	{
		get
		{
			return ComboBox.SelectedItem;
		}
		set
		{
			ComboBox.SelectedItem = value;
		}
	}

	/// <summary>Gets or sets the text that is selected in the editable portion of a <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
	/// <returns>A string that represents the currently selected text in the combo box. If <see cref="P:System.Windows.Forms.ToolStripComboBox.DropDownStyle" /> is set to DropDownList, the return value is an empty string ("").</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public string SelectedText
	{
		get
		{
			return ComboBox.SelectedText;
		}
		set
		{
			ComboBox.SelectedText = value;
		}
	}

	/// <summary>Gets or sets the number of characters selected in the editable portion of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
	/// <returns>The number of characters selected in the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</returns>
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
			return ComboBox.SelectionLength;
		}
		set
		{
			ComboBox.SelectionLength = value;
		}
	}

	/// <summary>Gets or sets the starting index of text selected in the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
	/// <returns>The zero-based index of the first character in the string of the current text selection.</returns>
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
			return ComboBox.SelectionStart;
		}
		set
		{
			ComboBox.SelectionStart = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the items in the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> are sorted.</summary>
	/// <returns>true if the combo box is sorted; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool Sorted
	{
		get
		{
			return ComboBox.Sorted;
		}
		set
		{
			ComboBox.Sorted = value;
		}
	}

	/// <summary>Gets the default spacing, in pixels, between the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> and an adjacent item.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value.</returns>
	protected internal override Padding DefaultMargin => new Padding(1, 0, 1, 0);

	/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
	/// <returns>The default <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> in pixels. The default size is 100 x 20 pixels.</returns>
	protected override Size DefaultSize => new Size(100, 22);

	/// <summary>This event is not relevant to this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler DoubleClick
	{
		add
		{
			base.DoubleClick += value;
		}
		remove
		{
			base.DoubleClick -= value;
		}
	}

	/// <summary>Occurs when the drop-down portion of a <see cref="T:System.Windows.Forms.ToolStripComboBox" /> is shown.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler DropDown
	{
		add
		{
			base.Events.AddHandler(DropDownEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DropDownEvent, value);
		}
	}

	/// <summary>Occurs when the drop-down portion of the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> has closed.</summary>
	public event EventHandler DropDownClosed
	{
		add
		{
			base.Events.AddHandler(DropDownClosedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DropDownClosedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripComboBox.DropDownStyle" /> property has changed.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler DropDownStyleChanged
	{
		add
		{
			base.Events.AddHandler(DropDownStyleChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DropDownStyleChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripComboBox.SelectedIndex" /> property has changed.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler SelectedIndexChanged
	{
		add
		{
			base.Events.AddHandler(SelectedIndexChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SelectedIndexChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> text has changed.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler TextUpdate
	{
		add
		{
			base.Events.AddHandler(TextUpdateEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(TextUpdateEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> class.</summary>
	public ToolStripComboBox()
		: base(new ToolStripComboBoxControl())
	{
		Size = new Size(121, 21);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> class derived from a base control.</summary>
	/// <param name="c">The base control. </param>
	/// <exception cref="T:System.NotSupportedException">The operation is not supported. </exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public ToolStripComboBox(Control c)
		: base(c)
	{
		throw new NotSupportedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> class with the specified name. </summary>
	/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</param>
	public ToolStripComboBox(string name)
		: this()
	{
		base.Name = name;
	}

	static ToolStripComboBox()
	{
		DropDown = new object();
		DropDownClosed = new object();
		DropDownStyleChanged = new object();
		SelectedIndexChanged = new object();
		TextUpdate = new object();
	}

	/// <summary>Maintains performance when items are added to the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> one at a time.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void BeginUpdate()
	{
		ComboBox.BeginUpdate();
	}

	/// <summary>Resumes painting the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> control after painting is suspended by the <see cref="M:System.Windows.Forms.ToolStripComboBox.BeginUpdate" /> method.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void EndUpdate()
	{
		ComboBox.EndUpdate();
	}

	/// <summary>Finds the first item in the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> that starts with the specified string.</summary>
	/// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
	/// <param name="s">The <see cref="T:System.String" /> to search for.</param>
	/// <filterpriority>1</filterpriority>
	public int FindString(string s)
	{
		return ComboBox.FindString(s);
	}

	/// <summary>Finds the first item after the given index which starts with the given string. </summary>
	/// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
	/// <param name="s">The <see cref="T:System.String" /> to search for.</param>
	/// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
	/// <filterpriority>1</filterpriority>
	public int FindString(string s, int startIndex)
	{
		return ComboBox.FindString(s, startIndex);
	}

	/// <summary>Finds the first item in the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> that exactly matches the specified string.</summary>
	/// <returns>The zero-based index of the first item found; -1 if no match is found.</returns>
	/// <param name="s">The <see cref="T:System.String" /> to search for.</param>
	/// <filterpriority>1</filterpriority>
	public int FindStringExact(string s)
	{
		return ComboBox.FindStringExact(s);
	}

	/// <summary>Finds the first item after the specified index that exactly matches the specified string.</summary>
	/// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
	/// <param name="s">The <see cref="T:System.String" /> to search for.</param>
	/// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
	/// <filterpriority>1</filterpriority>
	public int FindStringExact(string s, int startIndex)
	{
		return ComboBox.FindStringExact(s, startIndex);
	}

	/// <summary>Returns the height, in pixels, of an item in the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
	/// <returns>The height, in pixels, of the item at the specified index.</returns>
	/// <param name="index">The index of the item to return the height of.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int GetItemHeight(int index)
	{
		return ComboBox.GetItemHeight(index);
	}

	/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
	/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
	/// <param name="constrainingSize">The custom-sized area for a control. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override Size GetPreferredSize(Size constrainingSize)
	{
		return base.GetPreferredSize(constrainingSize);
	}

	/// <summary>Selects a range of text in the editable portion of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
	/// <param name="start">The position of the first character in the current text selection within the text box.</param>
	/// <param name="length">The number of characters to select.</param>
	/// <exception cref="T:System.ArgumentException">The <paramref name="start" /> is less than zero.-or- <paramref name="start" /> minus <paramref name="length" /> is less than zero. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Select(int start, int length)
	{
		ComboBox.Select(start, length);
	}

	/// <summary>Selects all the text in the editable portion of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SelectAll()
	{
		ComboBox.SelectAll();
	}

	public override string ToString()
	{
		return ComboBox.ToString();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripComboBox.DropDown" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnDropDown(EventArgs e)
	{
		((EventHandler)base.Events[DropDown])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripComboBox.DropDownClosed" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnDropDownClosed(EventArgs e)
	{
		((EventHandler)base.Events[DropDownClosed])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripComboBox.DropDownStyleChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnDropDownStyleChanged(EventArgs e)
	{
		((EventHandler)base.Events[DropDownStyleChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripComboBox.SelectedIndexChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnSelectedIndexChanged(EventArgs e)
	{
		((EventHandler)base.Events[SelectedIndexChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.SelectionChangeCommitted" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnSelectionChangeCommitted(EventArgs e)
	{
	}

	/// <summary>Subscribes events from the specified control.</summary>
	/// <param name="control">The control from which to subscribe events.</param>
	protected override void OnSubscribeControlEvents(Control control)
	{
		base.OnSubscribeControlEvents(control);
		ComboBox.DropDown += HandleDropDown;
		ComboBox.DropDownClosed += HandleDropDownClosed;
		ComboBox.DropDownStyleChanged += HandleDropDownStyleChanged;
		ComboBox.SelectedIndexChanged += HandleSelectedIndexChanged;
		ComboBox.TextChanged += HandleTextChanged;
		ComboBox.TextUpdate += HandleTextUpdate;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripComboBox.TextUpdate" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnTextUpdate(EventArgs e)
	{
		((EventHandler)base.Events[TextUpdate])?.Invoke(this, e);
	}

	/// <summary>Unsubscribes events from the specified control.</summary>
	/// <param name="control">The control from which to unsubscribe events.</param>
	protected override void OnUnsubscribeControlEvents(Control control)
	{
		base.OnUnsubscribeControlEvents(control);
	}

	private void HandleDropDown(object sender, EventArgs e)
	{
		OnDropDown(e);
	}

	private void HandleDropDownClosed(object sender, EventArgs e)
	{
		OnDropDownClosed(e);
	}

	private void HandleDropDownStyleChanged(object sender, EventArgs e)
	{
		OnDropDownStyleChanged(e);
	}

	private void HandleSelectedIndexChanged(object sender, EventArgs e)
	{
		OnSelectedIndexChanged(e);
	}

	private void HandleTextChanged(object sender, EventArgs e)
	{
		OnTextChanged(e);
	}

	private void HandleTextUpdate(object sender, EventArgs e)
	{
		OnTextUpdate(e);
	}
}
