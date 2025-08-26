using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides focus-management functionality for controls that can function as a container for other controls. </summary>
/// <filterpriority>2</filterpriority>
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class ContainerControl : ScrollableControl, IContainerControl
{
	private Control active_control;

	private Control unvalidated_control;

	private ArrayList pending_validation_chain;

	internal bool auto_select_child = true;

	private SizeF auto_scale_dimensions;

	private AutoScaleMode auto_scale_mode;

	private bool auto_scale_mode_set;

	private bool auto_scale_pending;

	private bool is_auto_scaling;

	internal bool validation_failed;

	[System.MonoTODO("Stub, not implemented")]
	private static bool ValidateWarned;

	private AutoValidate auto_validate = AutoValidate.Inherit;

	private static object OnValidateChanged = new object();

	/// <summary>Gets or sets the active control on the container control.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that is currently active on the <see cref="T:System.Windows.Forms.ContainerControl" />.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Windows.Forms.Control" /> assigned could not be activated. </exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Control ActiveControl
	{
		get
		{
			return active_control;
		}
		set
		{
			if (value == null || (active_control == value && active_control.Focused))
			{
				return;
			}
			if (!Contains(value))
			{
				throw new ArgumentException("Cannot activate invisible or disabled control.");
			}
			Form form = FindForm();
			Control mostDeeplyNestedActiveControl = GetMostDeeplyNestedActiveControl((form != null) ? form : this);
			Control commonContainer = GetCommonContainer(mostDeeplyNestedActiveControl, value);
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			Control control = mostDeeplyNestedActiveControl;
			bool flag = true;
			Control control2 = commonContainer;
			active_control = value;
			while (control != commonContainer && control != null)
			{
				if (control == value)
				{
					control2 = value;
					flag = false;
					break;
				}
				control.FireLeave();
				if (control is ContainerControl)
				{
					((ContainerControl)control).active_control = null;
				}
				if (control.CausesValidation)
				{
					arrayList2.Add(control);
				}
				control = control.Parent;
			}
			Control topmost_under_root = null;
			bool postpone_validation;
			if (value == control2)
			{
				postpone_validation = false;
			}
			else
			{
				postpone_validation = true;
				control = value;
				while (control != control2 && control != null)
				{
					if (control.CausesValidation)
					{
						postpone_validation = false;
					}
					topmost_under_root = control;
					control = control.Parent;
				}
			}
			Control control3 = PerformValidation((form != null) ? form : this, postpone_validation, arrayList2, topmost_under_root);
			if (control3 != null)
			{
				active_control = (value = control3);
				flag = true;
			}
			if (flag)
			{
				control = value;
				while (control != control2 && control != null)
				{
					arrayList.Add(control);
					control = control.Parent;
				}
				if (control2 != null && control == control2 && !(control2 is ContainerControl))
				{
					arrayList.Add(control);
				}
				for (int num = arrayList.Count - 1; num >= 0; num--)
				{
					control = (Control)arrayList[num];
					control.FireEnter();
				}
			}
			control = this;
			Control control4 = this;
			while (control != null)
			{
				if (control.Parent is ContainerControl)
				{
					((ContainerControl)control.Parent).active_control = control4;
					control4 = control.Parent;
				}
				control = control.Parent;
			}
			if (this is Form)
			{
				CheckAcceptButton();
			}
			ScrollControlIntoView(active_control);
			control = this;
			control4 = this;
			while (control != null)
			{
				if (control.Parent is ContainerControl)
				{
					control4 = control.Parent;
				}
				control = control.Parent;
			}
			if (control4.InternalContainsFocus)
			{
				SendControlFocus(active_control);
			}
		}
	}

	/// <summary>Gets or sets the dimensions that the control was designed to.</summary>
	/// <returns>A <see cref="T:System.Drawing.SizeF" /> containing the dots per inch (DPI) or <see cref="T:System.Drawing.Font" /> size that the control was designed to.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The width or height of the <see cref="T:System.Drawing.SizeF" /> value is less than 0 when setting this value.</exception>
	[Localizable(true)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public SizeF AutoScaleDimensions
	{
		get
		{
			return auto_scale_dimensions;
		}
		set
		{
			if (auto_scale_dimensions != value)
			{
				auto_scale_dimensions = value;
				PerformAutoScale();
			}
		}
	}

	/// <summary>Gets the scaling factor between the current and design-time automatic scaling dimensions. </summary>
	/// <returns>A <see cref="T:System.Drawing.SizeF" /> containing the scaling ratio between the current and design-time scaling automatic scaling dimensions.</returns>
	protected SizeF AutoScaleFactor
	{
		get
		{
			if (auto_scale_dimensions.IsEmpty)
			{
				return new SizeF(1f, 1f);
			}
			return new SizeF(CurrentAutoScaleDimensions.Width / auto_scale_dimensions.Width, CurrentAutoScaleDimensions.Height / auto_scale_dimensions.Height);
		}
	}

	/// <summary>Gets or sets the automatic scaling mode of the control.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.AutoScaleMode" /> that represents the current scaling mode. The default is <see cref="F:System.Windows.Forms.AutoScaleMode.None" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">An <see cref="T:System.Windows.Forms.AutoScaleMode" /> value that is not valid was used to set this property.</exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public AutoScaleMode AutoScaleMode
	{
		get
		{
			return auto_scale_mode;
		}
		set
		{
			if (this is Form)
			{
				(this as Form).AutoScale = false;
			}
			if (auto_scale_mode != value)
			{
				auto_scale_mode = value;
				if (auto_scale_mode_set)
				{
					auto_scale_dimensions = SizeF.Empty;
				}
				auto_scale_mode_set = true;
				PerformAutoScale();
			}
		}
	}

	/// <returns>A <see cref="T:System.Windows.Forms.BindingContext" /> for the control.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public override BindingContext BindingContext
	{
		get
		{
			if (base.BindingContext == null)
			{
				base.BindingContext = new BindingContext();
			}
			return base.BindingContext;
		}
		set
		{
			base.BindingContext = value;
		}
	}

	/// <summary>Gets the current run-time dimensions of the screen.</summary>
	/// <returns>A <see cref="T:System.Drawing.SizeF" /> containing the current dots per inch (DPI) or <see cref="T:System.Drawing.Font" /> size of the screen.</returns>
	/// <exception cref="T:System.ComponentModel.Win32Exception">A Win32 device context could not be created for the current screen.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public SizeF CurrentAutoScaleDimensions
	{
		get
		{
			switch (auto_scale_mode)
			{
			case AutoScaleMode.Dpi:
				return TextRenderer.GetDpi();
			case AutoScaleMode.Font:
			{
				Size size = TextRenderer.MeasureText("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890", Font);
				int num = (int)Math.Round((float)size.Width / 62f);
				return new SizeF(num, size.Height);
			}
			default:
				return auto_scale_dimensions;
			}
		}
	}

	/// <summary>Gets the form that the container control is assigned to.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Form" /> that the container control is assigned to. This property will return null if the control is hosted inside of Internet Explorer or in another hosting context where there is no parent form. </returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Form ParentForm
	{
		get
		{
			for (Control control = base.Parent; control != null; control = control.Parent)
			{
				if (control is Form)
				{
					return (Form)control;
				}
			}
			return null;
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.Control.ImeMode" /> property can be set to an active value, to enable IME support.</summary>
	/// <returns>false in all cases.</returns>
	protected override bool CanEnableIme => false;

	protected override CreateParams CreateParams => base.CreateParams;

	internal bool IsAutoScaling => is_auto_scaling;

	/// <summary>Gets or sets a value that indicates whether controls in this container will be automatically validated when the focus changes.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.AutoValidate" /> enumerated value that indicates whether contained controls are implicitly validated on focus change. The default is <see cref="F:System.Windows.Forms.AutoValidate.Inherit" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A <see cref="T:System.Windows.Forms.AutoValidate" /> value that is not valid was used to set this property.</exception>
	/// <filterpriority>2</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[AmbientValue(AutoValidate.Inherit)]
	public virtual AutoValidate AutoValidate
	{
		get
		{
			return auto_validate;
		}
		[System.MonoTODO("Currently does nothing with the setting")]
		set
		{
			if (auto_validate != value)
			{
				auto_validate = value;
				OnAutoValidateChanged(new EventArgs());
			}
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ContainerControl.AutoValidate" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public event EventHandler AutoValidateChanged
	{
		add
		{
			base.Events.AddHandler(OnValidateChanged, value);
		}
		remove
		{
			base.Events.RemoveHandler(OnValidateChanged, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContainerControl" /> class.</summary>
	public ContainerControl()
	{
		active_control = null;
		unvalidated_control = null;
		base.ControlRemoved += OnControlRemoved;
		auto_scale_dimensions = SizeF.Empty;
		auto_scale_mode = AutoScaleMode.Inherit;
	}

	/// <summary>Activates the specified control.</summary>
	/// <returns>true if the control is successfully activated; otherwise, false.</returns>
	/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to activate.</param>
	bool IContainerControl.ActivateControl(Control control)
	{
		return Select(control);
	}

	private Control PerformValidation(ContainerControl top_container, bool postpone_validation, ArrayList validation_chain, Control topmost_under_root)
	{
		validation_failed = false;
		if (postpone_validation)
		{
			AddValidationChain(top_container, validation_chain);
			return null;
		}
		if (top_container.pending_validation_chain != null)
		{
			int index = top_container.pending_validation_chain.Count - 1;
			if (topmost_under_root == top_container.pending_validation_chain[index])
			{
				top_container.pending_validation_chain.RemoveAt(index);
			}
			AddValidationChain(top_container, validation_chain);
			validation_chain = top_container.pending_validation_chain;
			top_container.pending_validation_chain = null;
		}
		for (int i = 0; i < validation_chain.Count; i++)
		{
			if (!ValidateControl((Control)validation_chain[i]))
			{
				validation_failed = true;
				return (Control)validation_chain[i];
			}
		}
		return null;
	}

	private void AddValidationChain(ContainerControl top_container, ArrayList validation_chain)
	{
		if (validation_chain.Count == 0)
		{
			return;
		}
		if (top_container.pending_validation_chain == null || top_container.pending_validation_chain.Count == 0)
		{
			top_container.pending_validation_chain = validation_chain;
			return;
		}
		foreach (Control item in validation_chain)
		{
			if (!top_container.pending_validation_chain.Contains(item))
			{
				top_container.pending_validation_chain.Add(item);
			}
		}
	}

	private bool ValidateControl(Control c)
	{
		CancelEventArgs cancelEventArgs = new CancelEventArgs();
		c.FireValidating(cancelEventArgs);
		if (cancelEventArgs.Cancel)
		{
			return false;
		}
		c.FireValidated();
		return true;
	}

	private Control GetMostDeeplyNestedActiveControl(ContainerControl container)
	{
		Control activeControl = container.ActiveControl;
		while (activeControl is ContainerControl && ((ContainerControl)activeControl).ActiveControl != null)
		{
			activeControl = ((ContainerControl)activeControl).ActiveControl;
		}
		return activeControl;
	}

	private Control GetCommonContainer(Control active_control, Control value)
	{
		Control control = null;
		for (Control control2 = active_control; control2 != null; control2 = control2.Parent)
		{
			for (control = value.Parent; control != null; control = control.Parent)
			{
				if (control == control2)
				{
					return control;
				}
			}
		}
		return null;
	}

	internal void SendControlFocus(Control c)
	{
		if (c.IsHandleCreated)
		{
			XplatUI.SetFocus(c.window.Handle);
		}
	}

	internal void PerformAutoScale(bool called_by_scale)
	{
		if (AutoScaleMode == AutoScaleMode.Inherit && !called_by_scale)
		{
			return;
		}
		if (layout_suspended > 0 && !called_by_scale)
		{
			auto_scale_pending = true;
			return;
		}
		auto_scale_pending = false;
		SizeF autoScaleFactor = AutoScaleFactor;
		if (AutoScaleMode == AutoScaleMode.Inherit)
		{
			ContainerControl containerControl = FindContainer(base.Parent);
			if (containerControl != null)
			{
				autoScaleFactor = containerControl.AutoScaleFactor;
			}
		}
		if (autoScaleFactor != new SizeF(1f, 1f))
		{
			is_auto_scaling = true;
			SuspendLayout();
			Scale(autoScaleFactor);
			ResumeLayout(performLayout: false);
			is_auto_scaling = false;
		}
		auto_scale_dimensions = CurrentAutoScaleDimensions;
	}

	/// <summary>Performs scaling of the container control and its children.</summary>
	public void PerformAutoScale()
	{
		PerformAutoScale(called_by_scale: false);
	}

	internal void PerformDelayedAutoScale()
	{
		if (auto_scale_pending)
		{
			PerformAutoScale();
		}
	}

	/// <summary>Verifies the value of the control losing focus by causing the <see cref="E:System.Windows.Forms.Control.Validating" /> and <see cref="E:System.Windows.Forms.Control.Validated" /> events to occur, in that order. </summary>
	/// <returns>true if validation is successful; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return false.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public bool Validate()
	{
		if (!ValidateWarned)
		{
			Console.WriteLine("ContainerControl.Validate is not yet implemented");
			ValidateWarned = true;
		}
		return true;
	}

	/// <summary>Verifies the value of the control that is losing focus; conditionally dependent on whether automatic validation is turned on. </summary>
	/// <returns>true if validation is successful; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return false.</returns>
	/// <param name="checkAutoValidate">If true, the value of the <see cref="P:System.Windows.Forms.ContainerControl.AutoValidate" /> property is used to determine if validation should be performed; if false, validation is unconditionally performed.</param>
	public bool Validate(bool checkAutoValidate)
	{
		if ((checkAutoValidate && AutoValidate != 0) || !checkAutoValidate)
		{
			return Validate();
		}
		return true;
	}

	/// <summary>Causes all of the child controls within a control that support validation to validate their data. </summary>
	/// <returns>true if all of the children validated successfully; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return false.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public virtual bool ValidateChildren()
	{
		return ValidateChildren(ValidationConstraints.Selectable);
	}

	/// <summary>Causes all of the child controls within a control that support validation to validate their data. </summary>
	/// <returns>true if all of the children validated successfully; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return false.</returns>
	/// <param name="validationConstraints">Places restrictions on which controls have their <see cref="E:System.Windows.Forms.Control.Validating" /> event raised.</param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public virtual bool ValidateChildren(ValidationConstraints validationConstraints)
	{
		bool recurse = (validationConstraints & ValidationConstraints.ImmediateChildren) != ValidationConstraints.ImmediateChildren;
		foreach (Control control in base.Controls)
		{
			if (!ValidateNestedControls(control, validationConstraints, recurse))
			{
				return false;
			}
		}
		return true;
	}

	/// <param name="displayScrollbars">true to show the scroll bars; otherwise, false. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void AdjustFormScrollbars(bool displayScrollbars)
	{
		base.AdjustFormScrollbars(displayScrollbars);
	}

	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	private void OnControlRemoved(object sender, ControlEventArgs e)
	{
		if (e.Control == unvalidated_control)
		{
			unvalidated_control = null;
		}
		if (e.Control == active_control)
		{
			unvalidated_control = null;
		}
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();
		OnBindingContextChanged(EventArgs.Empty);
	}

	/// <returns>true if the character was processed by the control; otherwise, false.</returns>
	/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (ToolStripManager.ProcessCmdKey(ref msg, keyData))
		{
			return true;
		}
		return base.ProcessCmdKey(ref msg, keyData);
	}

	/// <returns>true if the character was processed by the control; otherwise, false.</returns>
	/// <param name="charCode">The character to process. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override bool ProcessDialogChar(char charCode)
	{
		if (GetTopLevel() && ProcessMnemonic(charCode))
		{
			return true;
		}
		return base.ProcessDialogChar(charCode);
	}

	/// <returns>true if the key was processed by the control; otherwise, false.</returns>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
	protected override bool ProcessDialogKey(Keys keyData)
	{
		Keys keys = keyData & Keys.KeyCode;
		bool forward = true;
		switch (keys)
		{
		case Keys.Tab:
			if ((keyData & (Keys.Control | Keys.Alt)) == 0 && ProcessTabKey((Control.ModifierKeys & Keys.Shift) == 0))
			{
				return true;
			}
			break;
		case Keys.Left:
			forward = false;
			goto case Keys.Right;
		case Keys.Up:
			forward = false;
			goto case Keys.Right;
		case Keys.Right:
		case Keys.Down:
			if (SelectNextControl(active_control, forward, tabStopOnly: false, nested: false, wrap: true))
			{
				return true;
			}
			break;
		}
		return base.ProcessDialogKey(keyData);
	}

	/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
	/// <param name="charCode">The character to process. </param>
	protected override bool ProcessMnemonic(char charCode)
	{
		bool flag = false;
		Control nextControl = active_control;
		do
		{
			nextControl = GetNextControl(nextControl, forward: true);
			if (nextControl != null)
			{
				if (nextControl.ProcessControlMnemonic(charCode))
				{
					return true;
				}
				continue;
			}
			if (flag)
			{
				break;
			}
			flag = true;
		}
		while (nextControl != active_control);
		return false;
	}

	/// <summary>Selects the next available control and makes it the active control.</summary>
	/// <returns>true if a control is selected; otherwise, false.</returns>
	/// <param name="forward">true to cycle forward through the controls in the <see cref="T:System.Windows.Forms.ContainerControl" />; otherwise, false. </param>
	protected virtual bool ProcessTabKey(bool forward)
	{
		return SelectNextControl(active_control, forward, tabStopOnly: true, nested: true, wrap: false);
	}

	/// <param name="directed">true to specify the direction of the control to select; otherwise, false. </param>
	/// <param name="forward">true to move forward in the tab order; false to move backward in the tab order. </param>
	protected override void Select(bool directed, bool forward)
	{
		if (base.Parent != null)
		{
			IContainerControl containerControl = base.Parent.GetContainerControl();
			if (containerControl != null)
			{
				containerControl.ActiveControl = this;
			}
		}
		if (directed && auto_select_child)
		{
			SelectNextControl(null, forward, tabStopOnly: true, nested: true, wrap: false);
		}
	}

	/// <summary>When overridden by a derived class, updates which button is the default button.</summary>
	protected virtual void UpdateDefaultButton()
	{
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void WndProc(ref Message m)
	{
		Msg msg = (Msg)m.Msg;
		if (msg == Msg.WM_SETFOCUS)
		{
			if (active_control != null)
			{
				Select(active_control);
			}
			else
			{
				base.WndProc(ref m);
			}
		}
		else
		{
			base.WndProc(ref m);
		}
	}

	internal void ChildControlRemoved(Control control)
	{
		ContainerControl containerControl = FindForm();
		if (containerControl == null)
		{
			containerControl = this;
		}
		ArrayList arrayList = containerControl.pending_validation_chain;
		if (arrayList != null)
		{
			RemoveChildrenFromValidation(arrayList, control);
			if (arrayList.Count == 0)
			{
				containerControl.pending_validation_chain = null;
			}
		}
		if (control == active_control || control.Contains(active_control))
		{
			SelectNextControl(this, forward: true, tabStopOnly: true, nested: true, wrap: true);
			if (control == active_control || control.Contains(active_control))
			{
				active_control = null;
			}
		}
	}

	private bool RemoveChildrenFromValidation(ArrayList validation_chain, Control c)
	{
		if (RemoveFromValidationChain(validation_chain, c))
		{
			return true;
		}
		foreach (Control control in c.Controls)
		{
			if (RemoveChildrenFromValidation(validation_chain, control))
			{
				return true;
			}
		}
		return false;
	}

	private bool RemoveFromValidationChain(ArrayList validation_chain, Control c)
	{
		int num = validation_chain.IndexOf(c);
		if (num > -1)
		{
			pending_validation_chain.RemoveAt(num--);
			return true;
		}
		return false;
	}

	internal virtual void CheckAcceptButton()
	{
	}

	private bool ValidateNestedControls(Control c, ValidationConstraints constraints, bool recurse)
	{
		bool result = true;
		if (!c.CausesValidation)
		{
			result = true;
		}
		else if (!ValidateThisControl(c, constraints))
		{
			result = true;
		}
		else if (!ValidateControl(c))
		{
			result = false;
		}
		if (recurse)
		{
			foreach (Control control in c.Controls)
			{
				if (!ValidateNestedControls(control, constraints, recurse))
				{
					return false;
				}
			}
		}
		return result;
	}

	private bool ValidateThisControl(Control c, ValidationConstraints constraints)
	{
		if (constraints == ValidationConstraints.None)
		{
			return true;
		}
		if ((constraints & ValidationConstraints.Enabled) == ValidationConstraints.Enabled && !c.Enabled)
		{
			return false;
		}
		if ((constraints & ValidationConstraints.Selectable) == ValidationConstraints.Selectable && !c.GetStyle(ControlStyles.Selectable))
		{
			return false;
		}
		if ((constraints & ValidationConstraints.TabStop) == ValidationConstraints.TabStop && !c.TabStop)
		{
			return false;
		}
		if ((constraints & ValidationConstraints.Visible) == ValidationConstraints.Visible && !c.Visible)
		{
			return false;
		}
		return true;
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnParentChanged(EventArgs e)
	{
		base.OnParentChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnFontChanged(EventArgs e)
	{
		base.OnFontChanged(e);
		if (AutoScaleMode == AutoScaleMode.Font)
		{
			PerformAutoScale();
		}
	}

	protected override void OnLayout(LayoutEventArgs e)
	{
		base.OnLayout(e);
	}

	internal bool ShouldSerializeAutoValidate()
	{
		return AutoValidate != AutoValidate.Inherit;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ContainerControl.AutoValidateChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnAutoValidateChanged(EventArgs e)
	{
		((EventHandler)base.Events[OnValidateChanged])?.Invoke(this, e);
	}
}
