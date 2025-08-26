using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Enables the user to select a single option from a group of choices when paired with other <see cref="T:System.Windows.Forms.RadioButton" /> controls.</summary>
/// <filterpriority>1</filterpriority>
[DefaultProperty("Checked")]
[DefaultBindingProperty("Checked")]
[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[Designer("System.Windows.Forms.Design.RadioButtonDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[DefaultEvent("CheckedChanged")]
[ComVisible(true)]
public class RadioButton : ButtonBase
{
	/// <summary>Provides information about the <see cref="T:System.Windows.Forms.RadioButton" /> control to accessibility client applications.</summary>
	[ComVisible(true)]
	public class RadioButtonAccessibleObject : ButtonBaseAccessibleObject
	{
		private new RadioButton owner;

		/// <summary>Gets a string that describes the default action of the <see cref="T:System.Windows.Forms.RadioButton" /> control.</summary>
		/// <returns>A description of the default action of the <see cref="T:System.Windows.Forms.RadioButton" /> control.</returns>
		public override string DefaultAction => "Select";

		/// <summary>Gets the role of this accessible object.</summary>
		/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.RadioButton" /> value.</returns>
		public override AccessibleRole Role => AccessibleRole.RadioButton;

		/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.RadioButton" /> control.</summary>
		/// <returns>If the <see cref="P:System.Windows.Forms.RadioButton.Checked" /> property is set to true, returns <see cref="F:System.Windows.Forms.AccessibleStates.Checked" />.</returns>
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

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.RadioButton.RadioButtonAccessibleObject" /> class. </summary>
		public RadioButtonAccessibleObject(RadioButton owner)
			: base(owner)
		{
			this.owner = owner;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.RadioButton.Click" /> event.</summary>
		public override void DoDefaultAction()
		{
			owner.PerformClick();
		}
	}

	internal Appearance appearance;

	internal bool auto_check;

	internal ContentAlignment radiobutton_alignment;

	internal CheckState check_state;

	private static object AppearanceChangedEvent;

	private static object CheckedChangedEvent;

	/// <summary>Gets or sets a value determining the appearance of the <see cref="T:System.Windows.Forms.RadioButton" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.Appearance" /> values. The default value is <see cref="F:System.Windows.Forms.Appearance.Normal" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.Appearance" /> values. </exception>
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
				((EventHandler)base.Events[AppearanceChanged])?.Invoke(this, EventArgs.Empty);
				if (base.Parent != null)
				{
					base.Parent.PerformLayout(this, "Appearance");
				}
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.RadioButton.Checked" /> value and the appearance of the control automatically change when the control is clicked.</summary>
	/// <returns>true if the <see cref="P:System.Windows.Forms.RadioButton.Checked" /> value and the appearance of the control automatically change on the <see cref="E:System.Windows.Forms.Control.Click" /> event; otherwise, false. The default value is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
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

	/// <summary>Gets or sets the location of the check box portion of the <see cref="T:System.Windows.Forms.RadioButton" />.</summary>
	/// <returns>One of the valid <see cref="T:System.Drawing.ContentAlignment" /> values. The default value is <see cref="F:System.Drawing.ContentAlignment.MiddleLeft" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(ContentAlignment.MiddleLeft)]
	[Localizable(true)]
	public ContentAlignment CheckAlign
	{
		get
		{
			return radiobutton_alignment;
		}
		set
		{
			if (value != radiobutton_alignment)
			{
				radiobutton_alignment = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the control is checked.</summary>
	/// <returns>true if the check box is checked; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Bindable(true, BindingDirection.OneWay)]
	[DefaultValue(false)]
	[SettingsBindable(true)]
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
				UpdateSiblings();
				OnCheckedChanged(EventArgs.Empty);
			}
			else if (!value && check_state != 0)
			{
				TabStop = false;
				check_state = CheckState.Unchecked;
				Invalidate();
				OnCheckedChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.</summary>
	/// <returns>true if the user can give focus to this control using the TAB key; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public new bool TabStop
	{
		get
		{
			return base.TabStop;
		}
		set
		{
			base.TabStop = value;
		}
	}

	/// <summary>Gets or sets the alignment of the text on the <see cref="T:System.Windows.Forms.RadioButton" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.MiddleLeft" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(ContentAlignment.MiddleLeft)]
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

	/// <summary>Overrides <see cref="P:System.Windows.Forms.Control.CreateParams" />.</summary>
	protected override CreateParams CreateParams
	{
		get
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
			SetStyle(ControlStyles.UserPaint, value: true);
			return base.CreateParams;
		}
	}

	protected override Size DefaultSize => ThemeEngine.Current.RadioButtonDefaultSize;

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.RadioButton.Appearance" /> property value changes.</summary>
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.RadioButton.Checked" /> property changes.</summary>
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

	/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.RadioButton" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.RadioButton" /> control with the mouse.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.RadioButton" /> class.</summary>
	public RadioButton()
	{
		appearance = Appearance.Normal;
		auto_check = true;
		radiobutton_alignment = ContentAlignment.MiddleLeft;
		TextAlign = ContentAlignment.MiddleLeft;
		TabStop = false;
	}

	static RadioButton()
	{
		AppearanceChanged = new object();
		CheckedChanged = new object();
	}

	private void PerformDefaultCheck()
	{
		if (!auto_check || Checked)
		{
			return;
		}
		bool flag = false;
		Control control = base.Parent;
		if (control != null)
		{
			for (int i = 0; i < control.Controls.Count; i++)
			{
				if (control.Controls[i] is RadioButton { auto_check: not false, check_state: CheckState.Checked })
				{
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			Checked = true;
		}
	}

	private void UpdateSiblings()
	{
		if (!auto_check)
		{
			return;
		}
		Control control = base.Parent;
		if (control != null)
		{
			for (int i = 0; i < control.Controls.Count; i++)
			{
				if (this != control.Controls[i] && control.Controls[i] is RadioButton && ((RadioButton)control.Controls[i]).auto_check)
				{
					control.Controls[i].TabStop = false;
					((RadioButton)control.Controls[i]).Checked = false;
				}
			}
		}
		TabStop = true;
	}

	internal override void Draw(PaintEventArgs pe)
	{
		ThemeEngine.Current.CalculateRadioButtonTextAndImageLayout(this, Point.Empty, out var glyphArea, out var textRectangle, out var imageRectangle);
		if (base.FlatStyle != FlatStyle.System && Appearance != Appearance.Button)
		{
			ThemeEngine.Current.DrawRadioButton(pe.Graphics, this, glyphArea, textRectangle, imageRectangle, pe.ClipRectangle);
		}
		else
		{
			ThemeEngine.Current.DrawRadioButton(pe.Graphics, base.ClientRectangle, this);
		}
	}

	internal override Size GetPreferredSizeCore(Size proposedSize)
	{
		if (AutoSize)
		{
			return ThemeEngine.Current.CalculateRadioButtonAutoSize(this);
		}
		return base.GetPreferredSizeCore(proposedSize);
	}

	/// <summary>Generates a <see cref="E:System.Windows.Forms.Control.Click" /> event for the control, simulating a click by a user.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void PerformClick()
	{
		OnClick(EventArgs.Empty);
	}

	/// <summary>Overrides the <see cref="M:System.ComponentModel.Component.ToString" /> method.</summary>
	/// <filterpriority>2</filterpriority>
	public override string ToString()
	{
		return base.ToString() + ", Checked: " + Checked;
	}

	/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.RadioButton" /> control.</summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.RadioButton.RadioButtonAccessibleObject" /> for the control.</returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		AccessibleObject accessibleObject = base.CreateAccessibilityInstance();
		accessibleObject.role = AccessibleRole.RadioButton;
		return accessibleObject;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckBox.CheckedChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnCheckedChanged(EventArgs e)
	{
		((EventHandler)base.Events[CheckedChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnClick(EventArgs e)
	{
		if (auto_check)
		{
			if (!Checked)
			{
				Checked = true;
			}
		}
		else
		{
			Checked = !Checked;
		}
		base.OnClick(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnEnter(EventArgs e)
	{
		PerformDefaultCheck();
		base.OnEnter(e);
	}

	/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.OnHandleCreated(System.EventArgs)" /> method.</summary>
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
	/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseUp(MouseEventArgs mevent)
	{
		base.OnMouseUp(mevent);
	}

	/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.ProcessMnemonic(System.Char)" /> method.</summary>
	protected override bool ProcessMnemonic(char charCode)
	{
		if (Control.IsMnemonic(charCode, Text))
		{
			Select();
			PerformClick();
			return true;
		}
		return base.ProcessMnemonic(charCode);
	}
}
