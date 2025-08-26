using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs;

[Description("Displays a task dialog.")]
[Designer(typeof(TaskDialogDesigner))]
[DefaultProperty("MainInstruction")]
[DefaultEvent("ButtonClicked")]
public class TaskDialog : Component, IWin32Window
{
	private TaskDialogItemCollection<TaskDialogButton> _buttons;

	private TaskDialogItemCollection<TaskDialogRadioButton> _radioButtons;

	private NativeMethods.TASKDIALOGCONFIG _config = default(NativeMethods.TASKDIALOGCONFIG);

	private TaskDialogIcon _mainIcon;

	private Icon _customMainIcon;

	private Icon _customFooterIcon;

	private TaskDialogIcon _footerIcon;

	private Dictionary<int, TaskDialogButton> _buttonsById;

	private Dictionary<int, TaskDialogRadioButton> _radioButtonsById;

	private IntPtr _handle;

	private int _progressBarMarqueeAnimationSpeed = 100;

	private int _progressBarMinimimum;

	private int _progressBarMaximum = 100;

	private int _progressBarValue;

	private ProgressBarState _progressBarState;

	private int _inEventHandler;

	private bool _updatePending;

	private object _tag;

	private Icon _windowIcon;

	private IContainer components;

	public static bool OSSupportsTaskDialogs => NativeMethods.IsWindowsVistaOrLater;

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[Localizable(true)]
	[Category("Appearance")]
	[Description("A list of the buttons on the Task Dialog.")]
	public TaskDialogItemCollection<TaskDialogButton> Buttons => _buttons ?? (_buttons = new TaskDialogItemCollection<TaskDialogButton>(this));

	[Localizable(true)]
	[Category("Appearance")]
	[Description("A list of the radio buttons on the Task Dialog.")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public TaskDialogItemCollection<TaskDialogRadioButton> RadioButtons => _radioButtons ?? (_radioButtons = new TaskDialogItemCollection<TaskDialogRadioButton>(this));

	[Localizable(true)]
	[Description("The window title of the task dialog.")]
	[DefaultValue("")]
	[Category("Appearance")]
	public string WindowTitle
	{
		get
		{
			return _config.pszWindowTitle ?? string.Empty;
		}
		set
		{
			_config.pszWindowTitle = (string.IsNullOrEmpty(value) ? null : value);
			UpdateDialog();
		}
	}

	[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
	[Localizable(true)]
	[DefaultValue("")]
	[Category("Appearance")]
	[Description("The dialog's main instruction.")]
	public string MainInstruction
	{
		get
		{
			return _config.pszMainInstruction ?? string.Empty;
		}
		set
		{
			_config.pszMainInstruction = (string.IsNullOrEmpty(value) ? null : value);
			SetElementText(NativeMethods.TaskDialogElements.MainInstruction, MainInstruction);
		}
	}

	[Category("Appearance")]
	[Localizable(true)]
	[Description("The dialog's primary content.")]
	[DefaultValue("")]
	[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
	public string Content
	{
		get
		{
			return _config.pszContent ?? string.Empty;
		}
		set
		{
			_config.pszContent = (string.IsNullOrEmpty(value) ? null : value);
			SetElementText(NativeMethods.TaskDialogElements.Content, Content);
		}
	}

	[DefaultValue(null)]
	[Category("Appearance")]
	[Description("The icon to be used in the title bar of the dialog. Used only when the dialog is shown as a modeless dialog.")]
	[Localizable(true)]
	public Icon WindowIcon
	{
		get
		{
			if (IsDialogRunning)
			{
				IntPtr handle = NativeMethods.SendMessage(Handle, 127, new IntPtr(0), IntPtr.Zero);
				return Icon.FromHandle(handle);
			}
			return _windowIcon;
		}
		set
		{
			_windowIcon = value;
		}
	}

	[Description("The icon to display in the task dialog.")]
	[Category("Appearance")]
	[Localizable(true)]
	[DefaultValue(TaskDialogIcon.Custom)]
	public TaskDialogIcon MainIcon
	{
		get
		{
			return _mainIcon;
		}
		set
		{
			if (_mainIcon != value)
			{
				_mainIcon = value;
				UpdateDialog();
			}
		}
	}

	[Localizable(true)]
	[Category("Appearance")]
	[Description("A custom icon to display in the dialog.")]
	[DefaultValue(null)]
	public Icon CustomMainIcon
	{
		get
		{
			return _customMainIcon;
		}
		set
		{
			if (_customMainIcon != value)
			{
				_customMainIcon = value;
				UpdateDialog();
			}
		}
	}

	[Category("Appearance")]
	[Description("The icon to display in the footer area of the task dialog.")]
	[DefaultValue(TaskDialogIcon.Custom)]
	[Localizable(true)]
	public TaskDialogIcon FooterIcon
	{
		get
		{
			return _footerIcon;
		}
		set
		{
			if (_footerIcon != value)
			{
				_footerIcon = value;
				UpdateDialog();
			}
		}
	}

	[Description("A custom icon to display in the footer area of the task dialog.")]
	[Category("Appearance")]
	[Localizable(true)]
	[DefaultValue(null)]
	public Icon CustomFooterIcon
	{
		get
		{
			return _customFooterIcon;
		}
		set
		{
			if (_customFooterIcon != value)
			{
				_customFooterIcon = value;
				UpdateDialog();
			}
		}
	}

	[Category("Behavior")]
	[Description("Indicates whether custom buttons should be displayed as normal buttons or command links.")]
	[DefaultValue(TaskDialogButtonStyle.Standard)]
	public TaskDialogButtonStyle ButtonStyle
	{
		get
		{
			if (!GetFlag(NativeMethods.TaskDialogFlags.UseCommandLinksNoIcon))
			{
				if (!GetFlag(NativeMethods.TaskDialogFlags.UseCommandLinks))
				{
					return TaskDialogButtonStyle.Standard;
				}
				return TaskDialogButtonStyle.CommandLinks;
			}
			return TaskDialogButtonStyle.CommandLinksNoIcon;
		}
		set
		{
			SetFlag(NativeMethods.TaskDialogFlags.UseCommandLinks, value == TaskDialogButtonStyle.CommandLinks);
			SetFlag(NativeMethods.TaskDialogFlags.UseCommandLinksNoIcon, value == TaskDialogButtonStyle.CommandLinksNoIcon);
			UpdateDialog();
		}
	}

	[DefaultValue("")]
	[Description("The label for the verification checkbox.")]
	[Localizable(true)]
	[Category("Appearance")]
	public string VerificationText
	{
		get
		{
			return _config.pszVerificationText ?? string.Empty;
		}
		set
		{
			string text = (string.IsNullOrEmpty(value) ? null : value);
			if (_config.pszVerificationText != text)
			{
				_config.pszVerificationText = text;
				UpdateDialog();
			}
		}
	}

	[DefaultValue(false)]
	[Category("Behavior")]
	[Description("Indicates whether the verification checkbox is checked ot not.")]
	public bool IsVerificationChecked
	{
		get
		{
			return GetFlag(NativeMethods.TaskDialogFlags.VerificationFlagChecked);
		}
		set
		{
			if (value != IsVerificationChecked)
			{
				SetFlag(NativeMethods.TaskDialogFlags.VerificationFlagChecked, value);
				if (IsDialogRunning)
				{
					ClickVerification(value, setFocus: false);
				}
			}
		}
	}

	[Localizable(true)]
	[DefaultValue("")]
	[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
	[Description("Additional information to be displayed on the dialog.")]
	[Category("Appearance")]
	public string ExpandedInformation
	{
		get
		{
			return _config.pszExpandedInformation ?? string.Empty;
		}
		set
		{
			_config.pszExpandedInformation = (string.IsNullOrEmpty(value) ? null : value);
			SetElementText(NativeMethods.TaskDialogElements.ExpandedInformation, ExpandedInformation);
		}
	}

	[Category("Appearance")]
	[Description("The text to use for the control for collapsing the expandable information.")]
	[Localizable(true)]
	[DefaultValue("")]
	public string ExpandedControlText
	{
		get
		{
			return _config.pszExpandedControlText ?? string.Empty;
		}
		set
		{
			string text = (string.IsNullOrEmpty(value) ? null : value);
			if (_config.pszExpandedControlText != text)
			{
				_config.pszExpandedControlText = text;
				UpdateDialog();
			}
		}
	}

	[Category("Appearance")]
	[Description("The text to use for the control for expanding the expandable information.")]
	[DefaultValue("")]
	[Localizable(true)]
	public string CollapsedControlText
	{
		get
		{
			return _config.pszCollapsedControlText ?? string.Empty;
		}
		set
		{
			string text = (string.IsNullOrEmpty(value) ? null : value);
			if (_config.pszCollapsedControlText != text)
			{
				_config.pszCollapsedControlText = (string.IsNullOrEmpty(value) ? null : value);
				UpdateDialog();
			}
		}
	}

	[Localizable(true)]
	[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
	[Category("Appearance")]
	[Description("The text to be used in the footer area of the task dialog.")]
	[DefaultValue("")]
	public string Footer
	{
		get
		{
			return _config.pszFooterText ?? string.Empty;
		}
		set
		{
			_config.pszFooterText = (string.IsNullOrEmpty(value) ? null : value);
			SetElementText(NativeMethods.TaskDialogElements.Footer, Footer);
		}
	}

	[DefaultValue(0)]
	[Category("Appearance")]
	[Description("the width of the task dialog's client area in DLU's. If 0, task dialog will calculate the ideal width.")]
	[Localizable(true)]
	public int Width
	{
		get
		{
			return (int)_config.cxWidth;
		}
		set
		{
			if (_config.cxWidth != (uint)value)
			{
				_config.cxWidth = (uint)value;
				UpdateDialog();
			}
		}
	}

	[DefaultValue(false)]
	[Description("Indicates whether hyperlinks are allowed for the Content, ExpandedInformation and Footer properties.")]
	[Category("Behavior")]
	public bool EnableHyperlinks
	{
		get
		{
			return GetFlag(NativeMethods.TaskDialogFlags.EnableHyperLinks);
		}
		set
		{
			if (EnableHyperlinks != value)
			{
				SetFlag(NativeMethods.TaskDialogFlags.EnableHyperLinks, value);
				UpdateDialog();
			}
		}
	}

	[DefaultValue(false)]
	[Category("Behavior")]
	[Description("Indicates that the dialog should be able to be closed using Alt-F4, Escape and the title bar's close button even if no cancel button is specified.")]
	public bool AllowDialogCancellation
	{
		get
		{
			return GetFlag(NativeMethods.TaskDialogFlags.AllowDialogCancellation);
		}
		set
		{
			if (AllowDialogCancellation != value)
			{
				SetFlag(NativeMethods.TaskDialogFlags.AllowDialogCancellation, value);
				UpdateDialog();
			}
		}
	}

	[Category("Behavior")]
	[Description("Indicates that the string specified by the ExpandedInformation property should be displayed at the bottom of the dialog's footer area instead of immediately after the dialog's content.")]
	[DefaultValue(false)]
	public bool ExpandFooterArea
	{
		get
		{
			return GetFlag(NativeMethods.TaskDialogFlags.ExpandFooterArea);
		}
		set
		{
			if (ExpandFooterArea != value)
			{
				SetFlag(NativeMethods.TaskDialogFlags.ExpandFooterArea, value);
				UpdateDialog();
			}
		}
	}

	[DefaultValue(false)]
	[Category("Behavior")]
	[Description("Indicates that the string specified by the ExpandedInformation property should be displayed by default.")]
	public bool ExpandedByDefault
	{
		get
		{
			return GetFlag(NativeMethods.TaskDialogFlags.ExpandedByDefault);
		}
		set
		{
			if (ExpandedByDefault != value)
			{
				SetFlag(NativeMethods.TaskDialogFlags.ExpandedByDefault, value);
				UpdateDialog();
			}
		}
	}

	[Category("Behavior")]
	[Description("Indicates whether the Timer event is raised periodically while the dialog is visible.")]
	[DefaultValue(false)]
	public bool RaiseTimerEvent
	{
		get
		{
			return GetFlag(NativeMethods.TaskDialogFlags.CallbackTimer);
		}
		set
		{
			if (RaiseTimerEvent != value)
			{
				SetFlag(NativeMethods.TaskDialogFlags.CallbackTimer, value);
				UpdateDialog();
			}
		}
	}

	[Category("Layout")]
	[Description("Indicates whether the dialog is centered in the parent window instead of the screen.")]
	[DefaultValue(false)]
	public bool CenterParent
	{
		get
		{
			return GetFlag(NativeMethods.TaskDialogFlags.PositionRelativeToWindow);
		}
		set
		{
			if (CenterParent != value)
			{
				SetFlag(NativeMethods.TaskDialogFlags.PositionRelativeToWindow, value);
				UpdateDialog();
			}
		}
	}

	[Localizable(true)]
	[Category("Appearance")]
	[DefaultValue(false)]
	[Description("Indicates whether text is displayed right to left.")]
	public bool RightToLeft
	{
		get
		{
			return GetFlag(NativeMethods.TaskDialogFlags.RtlLayout);
		}
		set
		{
			if (RightToLeft != value)
			{
				SetFlag(NativeMethods.TaskDialogFlags.RtlLayout, value);
				UpdateDialog();
			}
		}
	}

	[Category("Window Style")]
	[DefaultValue(false)]
	[Description("Indicates whether the dialog has a minimize box on its caption bar.")]
	public bool MinimizeBox
	{
		get
		{
			return GetFlag(NativeMethods.TaskDialogFlags.CanBeMinimized);
		}
		set
		{
			if (MinimizeBox != value)
			{
				SetFlag(NativeMethods.TaskDialogFlags.CanBeMinimized, value);
				UpdateDialog();
			}
		}
	}

	[Description("The type of progress bar displayed on the dialog.")]
	[DefaultValue(ProgressBarStyle.None)]
	[Category("Behavior")]
	public ProgressBarStyle ProgressBarStyle
	{
		get
		{
			if (GetFlag(NativeMethods.TaskDialogFlags.ShowMarqueeProgressBar))
			{
				return ProgressBarStyle.MarqueeProgressBar;
			}
			if (GetFlag(NativeMethods.TaskDialogFlags.ShowProgressBar))
			{
				return ProgressBarStyle.ProgressBar;
			}
			return ProgressBarStyle.None;
		}
		set
		{
			SetFlag(NativeMethods.TaskDialogFlags.ShowMarqueeProgressBar, value == ProgressBarStyle.MarqueeProgressBar);
			SetFlag(NativeMethods.TaskDialogFlags.ShowProgressBar, value == ProgressBarStyle.ProgressBar);
			UpdateProgressBarStyle();
		}
	}

	[Category("Behavior")]
	[DefaultValue(100)]
	[Description("The marquee animation speed of the progress bar in milliseconds.")]
	public int ProgressBarMarqueeAnimationSpeed
	{
		get
		{
			return _progressBarMarqueeAnimationSpeed;
		}
		set
		{
			_progressBarMarqueeAnimationSpeed = value;
			UpdateProgressBarMarqueeSpeed();
		}
	}

	[Description("The lower bound of the range of the task dialog's progress bar.")]
	[DefaultValue(0)]
	[Category("Behavior")]
	public int ProgressBarMinimum
	{
		get
		{
			return _progressBarMinimimum;
		}
		set
		{
			if (_progressBarMaximum <= value)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			_progressBarMinimimum = value;
			UpdateProgressBarRange();
		}
	}

	[Category("Behavior")]
	[Description("The upper bound of the range of the task dialog's progress bar.")]
	[DefaultValue(100)]
	public int ProgressBarMaximum
	{
		get
		{
			return _progressBarMaximum;
		}
		set
		{
			if (value <= _progressBarMinimimum)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			_progressBarMaximum = value;
			UpdateProgressBarRange();
		}
	}

	[Description("The current value of the task dialog's progress bar.")]
	[DefaultValue(0)]
	[Category("Behavior")]
	public int ProgressBarValue
	{
		get
		{
			return _progressBarValue;
		}
		set
		{
			if (value < ProgressBarMinimum || value > ProgressBarMaximum)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			_progressBarValue = value;
			UpdateProgressBarValue();
		}
	}

	[DefaultValue(ProgressBarState.Normal)]
	[Category("Behavior")]
	[Description("The state of the task dialog's progress bar.")]
	public ProgressBarState ProgressBarState
	{
		get
		{
			return _progressBarState;
		}
		set
		{
			_progressBarState = value;
			UpdateProgressBarState();
		}
	}

	[Description("User-defined data about the component.")]
	[DefaultValue(null)]
	[Category("Data")]
	public object Tag
	{
		get
		{
			return _tag;
		}
		set
		{
			_tag = value;
		}
	}

	private bool IsDialogRunning => _handle != IntPtr.Zero;

	[Browsable(false)]
	public IntPtr Handle
	{
		get
		{
			CheckCrossThreadCall();
			return _handle;
		}
	}

	[Description("Event raised when the task dialog has been created.")]
	[Category("Behavior")]
	public event EventHandler Created;

	[Description("Event raised when the task dialog has been destroyed.")]
	[Category("Behavior")]
	public event EventHandler Destroyed;

	[Description("Event raised when the user clicks a button.")]
	[Category("Action")]
	public event EventHandler<TaskDialogItemClickedEventArgs> ButtonClicked;

	[Category("Action")]
	[Description("Event raised when the user clicks a button.")]
	public event EventHandler<TaskDialogItemClickedEventArgs> RadioButtonClicked;

	[Description("Event raised when the user clicks a hyperlink.")]
	[Category("Action")]
	public event EventHandler<HyperlinkClickedEventArgs> HyperlinkClicked;

	[Description("Event raised when the user clicks the verification check box.")]
	[Category("Action")]
	public event EventHandler VerificationClicked;

	[Description("Event raised periodically while the dialog is displayed.")]
	[Category("Behavior")]
	public event EventHandler<TimerEventArgs> Timer;

	[Description("Event raised when the user clicks the expand button on the task dialog.")]
	[Category("Action")]
	public event EventHandler<ExpandButtonClickedEventArgs> ExpandButtonClicked;

	[Description("Event raised when the user presses F1 while the dialog has focus.")]
	[Category("Action")]
	public event EventHandler HelpRequested;

	public TaskDialog()
	{
		InitializeComponent();
		_config.cbSize = (uint)Marshal.SizeOf((object)_config);
		_config.pfCallback = TaskDialogCallback;
	}

	public TaskDialog(IContainer container)
	{
		container?.Add(this);
		InitializeComponent();
		_config.cbSize = (uint)Marshal.SizeOf((object)_config);
		_config.pfCallback = TaskDialogCallback;
	}

	public TaskDialogButton Show()
	{
		return ShowDialog(IntPtr.Zero);
	}

	public TaskDialogButton ShowDialog()
	{
		return ShowDialog(null);
	}

	public TaskDialogButton ShowDialog(IWin32Window owner)
	{
		IntPtr owner2 = owner?.Handle ?? NativeMethods.GetActiveWindow();
		return ShowDialog(owner2);
	}

	public void ClickVerification(bool checkState, bool setFocus)
	{
		if (!IsDialogRunning)
		{
			throw new InvalidOperationException(Resources.TaskDialogNotRunningError);
		}
		NativeMethods.SendMessage(Handle, 1137, new IntPtr(checkState ? 1 : 0), new IntPtr(setFocus ? 1 : 0));
	}

	protected virtual void OnHyperlinkClicked(HyperlinkClickedEventArgs e)
	{
		if (this.HyperlinkClicked != null)
		{
			this.HyperlinkClicked(this, e);
		}
	}

	protected virtual void OnButtonClicked(TaskDialogItemClickedEventArgs e)
	{
		if (this.ButtonClicked != null)
		{
			this.ButtonClicked(this, e);
		}
	}

	protected virtual void OnRadioButtonClicked(TaskDialogItemClickedEventArgs e)
	{
		if (this.RadioButtonClicked != null)
		{
			this.RadioButtonClicked(this, e);
		}
	}

	protected virtual void OnVerificationClicked(EventArgs e)
	{
		if (this.VerificationClicked != null)
		{
			this.VerificationClicked(this, e);
		}
	}

	protected virtual void OnCreated(EventArgs e)
	{
		if (this.Created != null)
		{
			this.Created(this, e);
		}
	}

	protected virtual void OnTimer(TimerEventArgs e)
	{
		if (this.Timer != null)
		{
			this.Timer(this, e);
		}
	}

	protected virtual void OnDestroyed(EventArgs e)
	{
		if (this.Destroyed != null)
		{
			this.Destroyed(this, e);
		}
	}

	protected virtual void OnExpandButtonClicked(ExpandButtonClickedEventArgs e)
	{
		if (this.ExpandButtonClicked != null)
		{
			this.ExpandButtonClicked(this, e);
		}
	}

	protected virtual void OnHelpRequested(EventArgs e)
	{
		if (this.HelpRequested != null)
		{
			this.HelpRequested(this, e);
		}
	}

	internal void SetItemEnabled(TaskDialogItem item)
	{
		if (IsDialogRunning)
		{
			NativeMethods.SendMessage(Handle, (item is TaskDialogButton) ? 1135 : 1136, new IntPtr(item.Id), new IntPtr(item.Enabled ? 1 : 0));
		}
	}

	internal void SetButtonElevationRequired(TaskDialogButton button)
	{
		if (IsDialogRunning)
		{
			NativeMethods.SendMessage(Handle, 1139, new IntPtr(button.Id), new IntPtr(button.ElevationRequired ? 1 : 0));
		}
	}

	internal void ClickItem(TaskDialogItem item)
	{
		if (!IsDialogRunning)
		{
			throw new InvalidOperationException(Resources.TaskDialogNotRunningError);
		}
		NativeMethods.SendMessage(Handle, (item is TaskDialogButton) ? 1126 : 1134, new IntPtr(item.Id), IntPtr.Zero);
	}

	private TaskDialogButton ShowDialog(IntPtr owner)
	{
		if (!OSSupportsTaskDialogs)
		{
			throw new NotSupportedException(Resources.TaskDialogsNotSupportedError);
		}
		if (IsDialogRunning)
		{
			throw new InvalidOperationException(Resources.TaskDialogRunningError);
		}
		if (_buttons.Count == 0)
		{
			throw new InvalidOperationException(Resources.TaskDialogNoButtonsError);
		}
		_config.hwndParent = owner;
		_config.dwCommonButtons = (NativeMethods.TaskDialogCommonButtonFlags)0;
		_config.pButtons = IntPtr.Zero;
		_config.cButtons = 0u;
		List<NativeMethods.TASKDIALOG_BUTTON> buttons = SetupButtons();
		List<NativeMethods.TASKDIALOG_BUTTON> buttons2 = SetupRadioButtons();
		SetupIcon();
		try
		{
			MarshalButtons(buttons, out _config.pButtons, out _config.cButtons);
			MarshalButtons(buttons2, out _config.pRadioButtons, out _config.cRadioButtons);
			int pnButton;
			int pnRadioButton;
			bool pfVerificationFlagChecked;
			using (new ComCtlv6ActivationContext(enable: true))
			{
				NativeMethods.TaskDialogIndirect(ref _config, out pnButton, out pnRadioButton, out pfVerificationFlagChecked);
			}
			IsVerificationChecked = pfVerificationFlagChecked;
			if (_radioButtonsById.TryGetValue(pnRadioButton, out var value))
			{
				value.Checked = true;
			}
			if (_buttonsById.TryGetValue(pnButton, out var value2))
			{
				return value2;
			}
			return null;
		}
		finally
		{
			CleanUpButtons(ref _config.pButtons, ref _config.cButtons);
			CleanUpButtons(ref _config.pRadioButtons, ref _config.cRadioButtons);
		}
	}

	internal void UpdateDialog()
	{
		if (!IsDialogRunning)
		{
			return;
		}
		if (_inEventHandler > 0)
		{
			_updatePending = true;
			return;
		}
		_updatePending = false;
		CleanUpButtons(ref _config.pButtons, ref _config.cButtons);
		CleanUpButtons(ref _config.pRadioButtons, ref _config.cRadioButtons);
		_config.dwCommonButtons = (NativeMethods.TaskDialogCommonButtonFlags)0;
		List<NativeMethods.TASKDIALOG_BUTTON> buttons = SetupButtons();
		List<NativeMethods.TASKDIALOG_BUTTON> buttons2 = SetupRadioButtons();
		SetupIcon();
		MarshalButtons(buttons, out _config.pButtons, out _config.cButtons);
		MarshalButtons(buttons2, out _config.pRadioButtons, out _config.cRadioButtons);
		int cb = Marshal.SizeOf((object)_config);
		IntPtr intPtr = Marshal.AllocHGlobal(cb);
		try
		{
			Marshal.StructureToPtr((object)_config, intPtr, fDeleteOld: false);
			NativeMethods.SendMessage(Handle, 1125, IntPtr.Zero, intPtr);
		}
		finally
		{
			Marshal.DestroyStructure(intPtr, typeof(NativeMethods.TASKDIALOGCONFIG));
			Marshal.FreeHGlobal(intPtr);
		}
	}

	private void SetElementText(NativeMethods.TaskDialogElements element, string text)
	{
		if (!IsDialogRunning)
		{
			return;
		}
		IntPtr intPtr = Marshal.StringToHGlobalUni(text);
		try
		{
			NativeMethods.SendMessage(Handle, 1132, new IntPtr((int)element), intPtr);
		}
		finally
		{
			if (intPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}
	}

	private void SetupIcon()
	{
		SetupIcon(MainIcon, CustomMainIcon, NativeMethods.TaskDialogFlags.UseHIconMain);
		SetupIcon(FooterIcon, CustomFooterIcon, NativeMethods.TaskDialogFlags.UseHIconFooter);
	}

	private void SetupIcon(TaskDialogIcon icon, Icon customIcon, NativeMethods.TaskDialogFlags flag)
	{
		SetFlag(flag, value: false);
		if (icon == TaskDialogIcon.Custom)
		{
			if (customIcon != null)
			{
				SetFlag(flag, value: true);
				if (flag == NativeMethods.TaskDialogFlags.UseHIconMain)
				{
					_config.hMainIcon = customIcon.Handle;
				}
				else
				{
					_config.hFooterIcon = customIcon.Handle;
				}
			}
		}
		else if (flag == NativeMethods.TaskDialogFlags.UseHIconMain)
		{
			_config.hMainIcon = new IntPtr((int)icon);
		}
		else
		{
			_config.hFooterIcon = new IntPtr((int)icon);
		}
	}

	private static void CleanUpButtons(ref IntPtr buttons, ref uint count)
	{
		if (buttons != IntPtr.Zero)
		{
			int num = Marshal.SizeOf(typeof(NativeMethods.TASKDIALOG_BUTTON));
			for (int i = 0; i < count; i++)
			{
				IntPtr ptr = new IntPtr(buttons.ToInt64() + i * num);
				Marshal.DestroyStructure(ptr, typeof(NativeMethods.TASKDIALOG_BUTTON));
			}
			Marshal.FreeHGlobal(buttons);
			buttons = IntPtr.Zero;
			count = 0u;
		}
	}

	private static void MarshalButtons(List<NativeMethods.TASKDIALOG_BUTTON> buttons, out IntPtr buttonsPtr, out uint count)
	{
		buttonsPtr = IntPtr.Zero;
		count = 0u;
		if (buttons.Count > 0)
		{
			int num = Marshal.SizeOf(typeof(NativeMethods.TASKDIALOG_BUTTON));
			buttonsPtr = Marshal.AllocHGlobal(num * buttons.Count);
			for (int i = 0; i < buttons.Count; i++)
			{
				Marshal.StructureToPtr(ptr: new IntPtr(buttonsPtr.ToInt64() + i * num), structure: (object)buttons[i], fDeleteOld: false);
			}
			count = (uint)buttons.Count;
		}
	}

	private List<NativeMethods.TASKDIALOG_BUTTON> SetupButtons()
	{
		_buttonsById = new Dictionary<int, TaskDialogButton>();
		List<NativeMethods.TASKDIALOG_BUTTON> list = new List<NativeMethods.TASKDIALOG_BUTTON>();
		_config.nDefaultButton = 0;
		foreach (TaskDialogButton button in Buttons)
		{
			if (button.Id < 1)
			{
				throw new InvalidOperationException(Resources.InvalidTaskDialogItemIdError);
			}
			_buttonsById.Add(button.Id, button);
			if (button.Default)
			{
				_config.nDefaultButton = button.Id;
			}
			if (button.ButtonType == ButtonType.Custom)
			{
				if (string.IsNullOrEmpty(button.Text))
				{
					throw new InvalidOperationException(Resources.TaskDialogEmptyButtonLabelError);
				}
				NativeMethods.TASKDIALOG_BUTTON item = default(NativeMethods.TASKDIALOG_BUTTON);
				item.nButtonID = button.Id;
				item.pszButtonText = button.Text;
				if (ButtonStyle == TaskDialogButtonStyle.CommandLinks || (ButtonStyle == TaskDialogButtonStyle.CommandLinksNoIcon && !string.IsNullOrEmpty(button.CommandLinkNote)))
				{
					item.pszButtonText = item.pszButtonText + "\n" + button.CommandLinkNote;
				}
				list.Add(item);
			}
			else
			{
				_config.dwCommonButtons |= button.ButtonFlag;
			}
		}
		return list;
	}

	private List<NativeMethods.TASKDIALOG_BUTTON> SetupRadioButtons()
	{
		_radioButtonsById = new Dictionary<int, TaskDialogRadioButton>();
		List<NativeMethods.TASKDIALOG_BUTTON> list = new List<NativeMethods.TASKDIALOG_BUTTON>();
		_config.nDefaultRadioButton = 0;
		foreach (TaskDialogRadioButton radioButton in RadioButtons)
		{
			if (string.IsNullOrEmpty(radioButton.Text))
			{
				throw new InvalidOperationException(Resources.TaskDialogEmptyButtonLabelError);
			}
			if (radioButton.Id < 1)
			{
				throw new InvalidOperationException(Resources.InvalidTaskDialogItemIdError);
			}
			_radioButtonsById.Add(radioButton.Id, radioButton);
			if (radioButton.Checked)
			{
				_config.nDefaultRadioButton = radioButton.Id;
			}
			NativeMethods.TASKDIALOG_BUTTON item = default(NativeMethods.TASKDIALOG_BUTTON);
			item.nButtonID = radioButton.Id;
			item.pszButtonText = radioButton.Text;
			list.Add(item);
		}
		SetFlag(NativeMethods.TaskDialogFlags.NoDefaultRadioButton, _config.nDefaultRadioButton == 0);
		return list;
	}

	private void SetFlag(NativeMethods.TaskDialogFlags flag, bool value)
	{
		if (value)
		{
			_config.dwFlags |= flag;
		}
		else
		{
			_config.dwFlags &= ~flag;
		}
	}

	private bool GetFlag(NativeMethods.TaskDialogFlags flag)
	{
		return (_config.dwFlags & flag) != 0;
	}

	private uint TaskDialogCallback(IntPtr hwnd, uint uNotification, IntPtr wParam, IntPtr lParam, IntPtr dwRefData)
	{
		Interlocked.Increment(ref _inEventHandler);
		try
		{
			switch ((NativeMethods.TaskDialogNotifications)uNotification)
			{
			case NativeMethods.TaskDialogNotifications.Created:
				_handle = hwnd;
				DialogCreated();
				OnCreated(EventArgs.Empty);
				break;
			case NativeMethods.TaskDialogNotifications.Destroyed:
				_handle = IntPtr.Zero;
				OnDestroyed(EventArgs.Empty);
				break;
			case NativeMethods.TaskDialogNotifications.Navigated:
				DialogCreated();
				break;
			case NativeMethods.TaskDialogNotifications.HyperlinkClicked:
			{
				string href = Marshal.PtrToStringUni(lParam);
				OnHyperlinkClicked(new HyperlinkClickedEventArgs(href));
				break;
			}
			case NativeMethods.TaskDialogNotifications.ButtonClicked:
			{
				if (_buttonsById.TryGetValue((int)wParam, out var value2))
				{
					TaskDialogItemClickedEventArgs taskDialogItemClickedEventArgs = new TaskDialogItemClickedEventArgs(value2);
					OnButtonClicked(taskDialogItemClickedEventArgs);
					if (taskDialogItemClickedEventArgs.Cancel)
					{
						return 1u;
					}
				}
				break;
			}
			case NativeMethods.TaskDialogNotifications.VerificationClicked:
				IsVerificationChecked = (int)wParam == 1;
				OnVerificationClicked(EventArgs.Empty);
				break;
			case NativeMethods.TaskDialogNotifications.RadioButtonClicked:
			{
				if (_radioButtonsById.TryGetValue((int)wParam, out var value))
				{
					value.Checked = true;
					TaskDialogItemClickedEventArgs e = new TaskDialogItemClickedEventArgs(value);
					OnRadioButtonClicked(e);
				}
				break;
			}
			case NativeMethods.TaskDialogNotifications.Timer:
			{
				TimerEventArgs timerEventArgs = new TimerEventArgs(wParam.ToInt32());
				OnTimer(timerEventArgs);
				return timerEventArgs.ResetTickCount ? 1u : 0u;
			}
			case NativeMethods.TaskDialogNotifications.ExpandoButtonClicked:
				OnExpandButtonClicked(new ExpandButtonClickedEventArgs(wParam.ToInt32() != 0));
				break;
			case NativeMethods.TaskDialogNotifications.Help:
				OnHelpRequested(EventArgs.Empty);
				break;
			}
			return 0u;
		}
		finally
		{
			Interlocked.Decrement(ref _inEventHandler);
			if (_updatePending)
			{
				UpdateDialog();
			}
		}
	}

	private void DialogCreated()
	{
		if (_config.hwndParent == IntPtr.Zero && _windowIcon != null)
		{
			NativeMethods.SendMessage(Handle, 128, new IntPtr(0), _windowIcon.Handle);
		}
		foreach (TaskDialogButton button in Buttons)
		{
			if (!button.Enabled)
			{
				SetItemEnabled(button);
			}
			if (button.ElevationRequired)
			{
				SetButtonElevationRequired(button);
			}
		}
		UpdateProgressBarStyle();
		UpdateProgressBarMarqueeSpeed();
		UpdateProgressBarRange();
		UpdateProgressBarValue();
		UpdateProgressBarState();
	}

	private void UpdateProgressBarStyle()
	{
		if (IsDialogRunning)
		{
			NativeMethods.SendMessage(Handle, 1127, new IntPtr((ProgressBarStyle == ProgressBarStyle.MarqueeProgressBar) ? 1 : 0), IntPtr.Zero);
		}
	}

	private void UpdateProgressBarMarqueeSpeed()
	{
		if (IsDialogRunning)
		{
			NativeMethods.SendMessage(Handle, 1131, new IntPtr((ProgressBarMarqueeAnimationSpeed > 0) ? 1 : 0), new IntPtr(ProgressBarMarqueeAnimationSpeed));
		}
	}

	private void UpdateProgressBarRange()
	{
		if (IsDialogRunning)
		{
			NativeMethods.SendMessage(Handle, 1129, IntPtr.Zero, new IntPtr((ProgressBarMaximum << 16) | ProgressBarMinimum));
		}
		if (ProgressBarValue < ProgressBarMinimum)
		{
			ProgressBarValue = ProgressBarMinimum;
		}
		if (ProgressBarValue > ProgressBarMaximum)
		{
			ProgressBarValue = ProgressBarMaximum;
		}
	}

	private void UpdateProgressBarValue()
	{
		if (IsDialogRunning)
		{
			NativeMethods.SendMessage(Handle, 1130, new IntPtr(ProgressBarValue), IntPtr.Zero);
		}
	}

	private void UpdateProgressBarState()
	{
		if (IsDialogRunning)
		{
			NativeMethods.SendMessage(Handle, 1128, new IntPtr((int)(ProgressBarState + 1)), IntPtr.Zero);
		}
	}

	private void CheckCrossThreadCall()
	{
		IntPtr handle = _handle;
		if (handle != IntPtr.Zero)
		{
			int lpdwProcessId;
			int windowThreadProcessId = NativeMethods.GetWindowThreadProcessId(handle, out lpdwProcessId);
			int currentThreadId = NativeMethods.GetCurrentThreadId();
			if (windowThreadProcessId != currentThreadId)
			{
				throw new InvalidOperationException(Resources.TaskDialogIllegalCrossThreadCallError);
			}
		}
	}

	protected override void Dispose(bool disposing)
	{
		try
		{
			if (!disposing)
			{
				return;
			}
			if (components != null)
			{
				components.Dispose();
				components = null;
			}
			if (_buttons != null)
			{
				foreach (TaskDialogButton button in _buttons)
				{
					button.Dispose();
				}
				_buttons.Clear();
			}
			if (_radioButtons == null)
			{
				return;
			}
			foreach (TaskDialogRadioButton radioButton in _radioButtons)
			{
				radioButton.Dispose();
			}
			_radioButtons.Clear();
		}
		finally
		{
			base.Dispose(disposing);
		}
	}

	private void InitializeComponent()
	{
		components = new Container();
	}
}
