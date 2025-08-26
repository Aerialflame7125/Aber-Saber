using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a Windows <see cref="T:System.Windows.Forms.CheckBox" />.</summary>
/// <filterpriority>1</filterpriority>
[DefaultBindingProperty("CheckState")]
[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
[DefaultEvent("CheckedChanged")]
[DefaultProperty("Checked")]
public class CheckBox : ButtonBase
{
	/// <summary>Provides information about the <see cref="T:System.Windows.Forms.CheckBox" /> control to accessibility client applications.</summary>
	[ComVisible(true)]
	public class CheckBoxAccessibleObject : ButtonBaseAccessibleObject
	{
		private new CheckBox owner;

		/// <summary>Gets a string that describes the default action of the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		/// <returns>The description of the default action of the <see cref="T:System.Windows.Forms.CheckBox" /> control.</returns>
		public override string DefaultAction => "Select";

		/// <summary>Gets the role of this accessible object.</summary>
		/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.CheckButton" /> value.</returns>
		public override AccessibleRole Role => AccessibleRole.CheckButton;

		/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values. If the <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> property is set to <see cref="F:System.Windows.Forms.CheckState.Checked" />, this property returns <see cref="F:System.Windows.Forms.AccessibleStates.Checked" />. If <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> is set to <see cref="F:System.Windows.Forms.CheckState.Indeterminate" />, this property returns <see cref="F:System.Windows.Forms.AccessibleStates.Indeterminate" />.</returns>
		public override AccessibleStates State
		{
			get
			{
				AccessibleStates accessibleStates = AccessibleStates.Default;
				if (owner.check_state == CheckState.Checked)
				{
					accessibleStates |= AccessibleStates.Checked;
				}
				if (owner.Focused)
				{
					accessibleStates |= AccessibleStates.Focused;
				}
				if (owner.CanFocus)
				{
					accessibleStates |= AccessibleStates.Focusable;
				}
				return accessibleStates;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CheckBox.CheckBoxAccessibleObject" /> class. </summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.CheckBox" /> that owns the <see cref="T:System.Windows.Forms.CheckBox.CheckBoxAccessibleObject" />.</param>
		public CheckBoxAccessibleObject(Control owner)
			: base(owner)
		{
			this.owner = (CheckBox)owner;
		}

		/// <summary>Performs the default action associated with this accessible object.</summary>
		public override void DoDefaultAction()
		{
			owner.Checked = !owner.Checked;
		}
	}

	internal Appearance appearance;

	internal bool auto_check;

	internal ContentAlignment check_alignment;

	internal CheckState check_state;

	internal bool three_state;

	private static object AppearanceChangedEvent;

	private static object CheckedChangedEvent;

	private static object CheckStateChangedEvent;

	/// <summary>Gets or sets the value that determines the appearance of a <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.Appearance" /> values. The default value is <see cref="F:System.Windows.Forms.Appearance.Normal" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.Appearance" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(Appearance.Normal)]
	[Localizable(true)]
	public Appearance Appearance
	{
		get
		{
			return appearance;
		}
		set
		{
			if (value != appearance)
			{
				appearance = value;
				OnAppearanceChanged(EventArgs.Empty);
				if (base.Parent != null)
				{
					base.Parent.PerformLayout(this, "Appearance");
				}
				Invalidate();
			}
		}
	}

	/// <summary>Gets or set a value indicating whether the <see cref="P:System.Windows.Forms.CheckBox.Checked" /> or <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> values and the <see cref="T:System.Windows.Forms.CheckBox" />'s appearance are automatically changed when the <see cref="T:System.Windows.Forms.CheckBox" /> is clicked.</summary>
	/// <returns>true if the <see cref="P:System.Windows.Forms.CheckBox.Checked" /> value or <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> value and the appearance of the control are automatically changed on the <see cref="E:System.Windows.Forms.Control.Click" /> event; otherwise, false. The default value is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	public bool AutoCheck
	{
		get
		{
			return auto_check;
		}
		set
		{
			auto_check = value;
		}
	}

	/// <summary>Gets or sets the horizontal and vertical alignment of the check mark on a <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default value is MiddleLeft.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> enumeration values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(ContentAlignment.MiddleLeft)]
	[Bindable(true)]
	public ContentAlignment CheckAlign
	{
		get
		{
			return check_alignment;
		}
		set
		{
			if (value != check_alignment)
			{
				check_alignment = value;
				if (base.Parent != null)
				{
					base.Parent.PerformLayout(this, "CheckAlign");
				}
				Invalidate();
			}
		}
	}

	/// <summary>Gets or set a value indicating whether the <see cref="T:System.Windows.Forms.CheckBox" /> is in the checked state.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.CheckBox" /> is in the checked state; otherwise, false. The default value is false.Note:If the <see cref="P:System.Windows.Forms.CheckBox.ThreeState" /> property is set to true, the <see cref="P:System.Windows.Forms.CheckBox.Checked" /> property will return true for either a Checked or Indeterminate<see cref="P:System.Windows.Forms.CheckBox.CheckState" />.</returns>
	/// <filterpriority>1</filterpriority>
	[RefreshProperties(RefreshProperties.All)]
	[DefaultValue(false)]
	[SettingsBindable(true)]
	[Bindable(true)]
	public bool Checked
	{
		get
		{
			if (check_state != 0)
			{
				return true;
			}
			return false;
		}
		set
		{
			if (value && check_state != CheckState.Checked)
			{
				check_state = CheckState.Checked;
				Invalidate();
				OnCheckedChanged(EventArgs.Empty);
			}
			else if (!value && check_state != 0)
			{
				check_state = CheckState.Unchecked;
				Invalidate();
				OnCheckedChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the state of the <see cref="T:System.Windows.Forms.CheckBox" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> enumeration values. The default value is Unchecked.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.CheckState" /> enumeration values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(CheckState.Unchecked)]
	[Bindable(true)]
	[RefreshProperties(RefreshProperties.All)]
	public CheckState CheckState
	{
		get
		{
			return check_state;
		}
		set
		{
			if (value != check_state)
			{
				bool flag = check_state != CheckState.Unchecked;
				check_state = value;
				if (flag != (check_state != CheckState.Unchecked))
				{
					OnCheckedChanged(EventArgs.Empty);
				}
				OnCheckStateChanged(EventArgs.Empty);
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the alignment of the text on the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.MiddleLeft" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(ContentAlignment.MiddleLeft)]
	[Localizable(true)]
	public override ContentAlignment TextAlign
	{
		get
		{
			return base.TextAlign;
		}
		set
		{
			base.TextAlign = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.CheckBox" /> will allow three check states rather than two.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.CheckBox" /> is able to display three check states; otherwise, false. The default value is false.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	public bool ThreeState
	{
		get
		{
			return three_state;
		}
		set
		{
			three_state = value;
		}
	}

	/// <summary>Gets the required creation parameters when the control handle is created.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
	protected override CreateParams CreateParams => base.CreateParams;

	protected override Size DefaultSize => new Size(104, 24);

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.CheckBox.Appearance" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler AppearanceChanged
	{
		add
		{
			base.Events.AddHandler(AppearanceChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(AppearanceChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.CheckBox.Checked" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler CheckedChanged
	{
		add
		{
			base.Events.AddHandler(CheckedChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(CheckedChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler CheckStateChanged
	{
		add
		{
			base.Events.AddHandler(CheckStateChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(CheckStateChangedEvent, value);
		}
	}

	/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event MouseEventHandler MouseDoubleClick
	{
		add
		{
			base.MouseDoubleClick += value;
		}
		remove
		{
			base.MouseDoubleClick -= value;
		}
	}

	/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler DoubleClick;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CheckBox" /> class.</summary>
	public CheckBox()
	{
		appearance = Appearance.Normal;
		auto_check = true;
		check_alignment = ContentAlignment.MiddleLeft;
		TextAlign = ContentAlignment.MiddleLeft;
		SetStyle(ControlStyles.StandardDoubleClick, value: false);
		SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
	}

	static CheckBox()
	{
		AppearanceChanged = new object();
		CheckedChanged = new object();
		CheckStateChanged = new object();
	}

	internal override void Draw(PaintEventArgs pe)
	{
		ThemeEngine.Current.CalculateCheckBoxTextAndImageLayout(this, Point.Empty, out var glyphArea, out var textRectangle, out var imageRectangle);
		if (base.FlatStyle != FlatStyle.System)
		{
			ThemeEngine.Current.DrawCheckBox(pe.Graphics, this, glyphArea, textRectangle, imageRectangle, pe.ClipRectangle);
		}
		else
		{
			ThemeEngine.Current.DrawCheckBox(pe.Graphics, base.ClientRectangle, this);
		}
	}

	internal override Size GetPreferredSizeCore(Size proposedSize)
	{
		if (AutoSize)
		{
			return ThemeEngine.Current.CalculateCheckBoxAutoSize(this);
		}
		return base.GetPreferredSizeCore(proposedSize);
	}

	internal override void HaveDoubleClick()
	{
		if (this.DoubleClick != null)
		{
			this.DoubleClick(this, EventArgs.Empty);
		}
	}

	/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
	/// <returns>A string that states the control type and the state of the <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> property.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return base.ToString() + ", CheckState: " + (int)check_state;
	}

	/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.CheckBox.CheckBoxAccessibleObject" /> for the control.</returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		AccessibleObject accessibleObject = base.CreateAccessibilityInstance();
		accessibleObject.role = AccessibleRole.CheckButton;
		return accessibleObject;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckBox.AppearanceChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnAppearanceChanged(EventArgs e)
	{
		((EventHandler)base.Events[AppearanceChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckBox.CheckedChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnCheckedChanged(EventArgs e)
	{
		((EventHandler)base.Events[CheckedChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckBox.CheckStateChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnCheckStateChanged(EventArgs e)
	{
		((EventHandler)base.Events[CheckStateChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnClick(EventArgs e)
	{
		if (auto_check)
		{
			switch (check_state)
			{
			case CheckState.Unchecked:
				if (three_state)
				{
					CheckState = CheckState.Indeterminate;
				}
				else
				{
					CheckState = CheckState.Checked;
				}
				break;
			case CheckState.Indeterminate:
				CheckState = CheckState.Checked;
				break;
			case CheckState.Checked:
				CheckState = CheckState.Unchecked;
				break;
			}
		}
		base.OnClick(e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);
	}

	/// <param name="e"></param>
	protected override void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown(e);
	}

	/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseUp(MouseEventArgs mevent)
	{
		base.OnMouseUp(mevent);
	}

	/// <summary>Processes a mnemonic character.</summary>
	/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
	/// <param name="charCode">The character to process.</param>
	protected override bool ProcessMnemonic(char charCode)
	{
		if (Control.IsMnemonic(charCode, Text))
		{
			Select();
			OnClick(EventArgs.Empty);
			return true;
		}
		return base.ProcessMnemonic(charCode);
	}
}
