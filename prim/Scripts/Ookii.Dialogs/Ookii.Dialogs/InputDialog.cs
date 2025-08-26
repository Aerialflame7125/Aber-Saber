using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Ookii.Dialogs;

[DefaultProperty("MainInstruction")]
[DefaultEvent("ButtonClicked")]
[Description("A dialog that allows the user to input a single text value.")]
public class InputDialog : Component, IBindableComponent, IComponent, IDisposable
{
	private string _mainInstruction;

	private string _content;

	private string _windowTitle;

	private string _input;

	private int _maxLength = 32767;

	private bool _usePasswordMasking;

	private BindingContext _context;

	private ControlBindingsCollection _bindings;

	private IContainer components;

	[DefaultValue("")]
	[Localizable(true)]
	[Category("Appearance")]
	[Description("The dialog's main instruction.")]
	[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
	public string MainInstruction
	{
		get
		{
			return _mainInstruction ?? string.Empty;
		}
		set
		{
			_mainInstruction = (string.IsNullOrEmpty(value) ? null : value);
		}
	}

	[Localizable(true)]
	[Description("The dialog's primary content.")]
	[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
	[Category("Appearance")]
	[DefaultValue("")]
	public string Content
	{
		get
		{
			return _content ?? string.Empty;
		}
		set
		{
			_content = (string.IsNullOrEmpty(value) ? null : value);
		}
	}

	[Description("The window title of the task dialog.")]
	[Localizable(true)]
	[Category("Appearance")]
	[DefaultValue("")]
	public string WindowTitle
	{
		get
		{
			return _windowTitle ?? string.Empty;
		}
		set
		{
			_windowTitle = (string.IsNullOrEmpty(value) ? null : value);
		}
	}

	[Localizable(true)]
	[Category("Appearance")]
	[Description("The text specified by the user.")]
	[DefaultValue("")]
	public string Input
	{
		get
		{
			return _input ?? string.Empty;
		}
		set
		{
			_input = (value = (string.IsNullOrEmpty(value) ? null : value));
			OnInputChanged(EventArgs.Empty);
		}
	}

	[Category("Behavior")]
	[DefaultValue(32767)]
	[Localizable(true)]
	[Description("The maximum number of characters that can be entered into the input field of the dialog.")]
	public int MaxLength
	{
		get
		{
			return _maxLength;
		}
		set
		{
			_maxLength = value;
		}
	}

	[Description("Indicates whether the input will be masked using the system password character.")]
	[DefaultValue(false)]
	[Category("Behavior")]
	public bool UsePasswordMasking
	{
		get
		{
			return _usePasswordMasking;
		}
		set
		{
			_usePasswordMasking = value;
		}
	}

	[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public BindingContext BindingContext
	{
		get
		{
			return _context ?? (_context = new BindingContext());
		}
		set
		{
			_context = value;
		}
	}

	[RefreshProperties(RefreshProperties.All)]
	[ParenthesizePropertyName(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[Category("Data")]
	public ControlBindingsCollection DataBindings => _bindings ?? (_bindings = new ControlBindingsCollection(this));

	[Category("Property Changed")]
	[Description("Event raised when the value of the Input property changes.")]
	public event EventHandler InputChanged;

	[Description("Event raised when the user clicks the OK button on the dialog.")]
	[Category("Action")]
	public event EventHandler<OkButtonClickedEventArgs> OkButtonClicked;

	public InputDialog()
	{
		InitializeComponent();
	}

	public InputDialog(IContainer container)
	{
		container?.Add(this);
		InitializeComponent();
	}

	protected virtual void OnInputChanged(EventArgs e)
	{
		if (this.InputChanged != null)
		{
			this.InputChanged(this, e);
		}
	}

	protected virtual void OnOkButtonClicked(OkButtonClickedEventArgs e)
	{
		if (this.OkButtonClicked != null)
		{
			this.OkButtonClicked(this, e);
		}
	}

	public DialogResult ShowDialog()
	{
		return ShowDialog(null);
	}

	public DialogResult ShowDialog(IWin32Window owner)
	{
		using InputDialogForm inputDialogForm = new InputDialogForm();
		inputDialogForm.MainInstruction = MainInstruction;
		inputDialogForm.Content = Content;
		inputDialogForm.Text = WindowTitle;
		inputDialogForm.Input = Input;
		inputDialogForm.UsePasswordMasking = UsePasswordMasking;
		inputDialogForm.MaxLength = MaxLength;
		inputDialogForm.OkButtonClicked += InputBoxForm_OkButtonClicked;
		DialogResult dialogResult = inputDialogForm.ShowDialog(owner);
		if (dialogResult == DialogResult.OK)
		{
			Input = inputDialogForm.Input;
		}
		return dialogResult;
	}

	private void InputBoxForm_OkButtonClicked(object sender, OkButtonClickedEventArgs e)
	{
		OnOkButtonClicked(e);
	}

	protected override void Dispose(bool disposing)
	{
		try
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
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
