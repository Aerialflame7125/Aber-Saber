using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms;

/// <summary>Uses a mask to distinguish between proper and improper user input.</summary>
/// <filterpriority>1</filterpriority>
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[DefaultBindingProperty("Text")]
[Designer("System.Windows.Forms.Design.MaskedTextBoxDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[DefaultEvent("MaskInputRejected")]
[DefaultProperty("Mask")]
[ComVisible(true)]
public class MaskedTextBox : TextBoxBase
{
	private MaskedTextProvider provider;

	private bool beep_on_error;

	private IFormatProvider format_provider;

	private bool hide_prompt_on_leave;

	private InsertKeyMode insert_key_mode;

	private bool insert_key_overwriting;

	private bool reject_input_on_first_failure;

	private HorizontalAlignment text_align;

	private MaskFormat cut_copy_mask_format;

	private bool use_system_password_char;

	private Type validating_type;

	private bool is_empty_mask;

	private bool setting_text;

	private static object AcceptsTabChangedEvent;

	private static object IsOverwriteModeChangedEvent;

	private static object MaskChangedEvent;

	private static object MaskInputRejectedEvent;

	private static object MultilineChangedEvent;

	private static object TextAlignChangedEvent;

	private static object TypeValidationCompletedEvent;

	/// <summary>Gets or sets a value determining how TAB keys are handled for multiline configurations. This property is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />. </summary>
	/// <returns>false in all cases.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new bool AcceptsTab
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	/// <summary>Gets or sets a value indicating whether <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar" /> can be entered as valid data by the user. </summary>
	/// <returns>true if the user can enter the prompt character into the control; otherwise, false. The default is true. </returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	public bool AllowPromptAsInput
	{
		get
		{
			return provider.AllowPromptAsInput;
		}
		set
		{
			provider = new MaskedTextProvider(provider.Mask, provider.Culture, value, provider.PromptChar, provider.PasswordChar, provider.AsciiOnly);
			UpdateVisibleText();
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control accepts characters outside of the ASCII character set.</summary>
	/// <returns>true if only ASCII is accepted; false if the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control can accept any arbitrary Unicode character. The default is false.</returns>
	[DefaultValue(false)]
	[RefreshProperties(RefreshProperties.Repaint)]
	public bool AsciiOnly
	{
		get
		{
			return provider.AsciiOnly;
		}
		set
		{
			provider = new MaskedTextProvider(provider.Mask, provider.Culture, provider.AllowPromptAsInput, provider.PromptChar, provider.PasswordChar, value);
			UpdateVisibleText();
		}
	}

	/// <summary>Gets or sets a value indicating whether the masked text box control raises the system beep for each user key stroke that it rejects.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control should beep on invalid input; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	public bool BeepOnError
	{
		get
		{
			return beep_on_error;
		}
		set
		{
			beep_on_error = value;
		}
	}

	/// <summary>Gets a value indicating whether the user can undo the previous operation. This property is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />. </summary>
	/// <returns>false in all cases. </returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new bool CanUndo => false;

	/// <summary>Gets the required creation parameters when the control handle is created.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> representing the information needed when creating a control.</returns>
	protected override CreateParams CreateParams => base.CreateParams;

	/// <summary>Gets or sets the culture information associated with the masked text box.</summary>
	/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> representing the culture supported by the <see cref="T:System.Windows.Forms.MaskedTextBox" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <see cref="P:System.Windows.Forms.MaskedTextBox.Culture" /> was set to null.</exception>
	[RefreshProperties(RefreshProperties.Repaint)]
	public CultureInfo Culture
	{
		get
		{
			return provider.Culture;
		}
		set
		{
			provider = new MaskedTextProvider(provider.Mask, value, provider.AllowPromptAsInput, provider.PromptChar, provider.PasswordChar, provider.AsciiOnly);
			UpdateVisibleText();
		}
	}

	/// <summary>Gets or sets a value that determines whether literals and prompt characters are copied to the clipboard.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.MaskFormat" /> values. The default is <see cref="F:System.Windows.Forms.MaskFormat.IncludeLiterals" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Property set with a <see cref="T:System.Windows.Forms.MaskFormat" />  value that is not valid. </exception>
	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue(MaskFormat.IncludeLiterals)]
	public MaskFormat CutCopyMaskFormat
	{
		get
		{
			return cut_copy_mask_format;
		}
		set
		{
			if (!Enum.IsDefined(typeof(MaskFormat), value))
			{
				throw new InvalidEnumArgumentException("value", (int)value, typeof(MaskFormat));
			}
			cut_copy_mask_format = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.IFormatProvider" /> to use when performing type validation.</summary>
	/// <returns>An object that implements the <see cref="T:System.IFormatProvider" /> interface. </returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public IFormatProvider FormatProvider
	{
		get
		{
			return format_provider;
		}
		set
		{
			format_provider = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the prompt characters in the input mask are hidden when the masked text box loses focus.</summary>
	/// <returns>true if <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar" /> is hidden when <see cref="T:System.Windows.Forms.MaskedTextBox" /> does not have focus; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	[RefreshProperties(RefreshProperties.Repaint)]
	public bool HidePromptOnLeave
	{
		get
		{
			return hide_prompt_on_leave;
		}
		set
		{
			hide_prompt_on_leave = value;
		}
	}

	/// <summary>Gets or sets the text insertion mode of the masked text box control.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.InsertKeyMode" /> value that indicates the current insertion mode. The default is <see cref="F:System.Windows.Forms.InsertKeyMode.Default" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">An invalid <see cref="T:System.Windows.Forms.InsertKeyMode" /> value was supplied when setting this property.</exception>
	[DefaultValue(InsertKeyMode.Default)]
	public InsertKeyMode InsertKeyMode
	{
		get
		{
			return insert_key_mode;
		}
		set
		{
			if (!Enum.IsDefined(typeof(InsertKeyMode), value))
			{
				throw new InvalidEnumArgumentException("value", (int)value, typeof(InsertKeyMode));
			}
			insert_key_mode = value;
		}
	}

	/// <summary>Gets a value that specifies whether new user input overwrites existing input.</summary>
	/// <returns>true if <see cref="T:System.Windows.Forms.MaskedTextBox" /> will overwrite existing characters as the user enters new ones; false if <see cref="T:System.Windows.Forms.MaskedTextBox" /> will shift existing characters forward. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public bool IsOverwriteMode
	{
		get
		{
			if (insert_key_mode == InsertKeyMode.Default)
			{
				return insert_key_overwriting;
			}
			return insert_key_mode == InsertKeyMode.Overwrite;
		}
	}

	/// <summary>Gets or sets the lines of text in multiline configurations. This property is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
	/// <returns>An array of type <see cref="T:System.String" /> that contains a single line. </returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new string[] Lines
	{
		get
		{
			string text = Text;
			if (text == null || text == string.Empty)
			{
				return new string[0];
			}
			return Text.Split(new string[3] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
		}
		set
		{
		}
	}

	/// <summary>Gets or sets the input mask to use at run time. </summary>
	/// <returns>A <see cref="T:System.String" /> representing the current mask. The default value is the empty string which allows any input.</returns>
	/// <exception cref="T:System.ArgumentException">The string supplied to the <see cref="P:System.Windows.Forms.MaskedTextBox.Mask" /> property is not a valid mask. Invalid masks include masks containing non-printable characters.</exception>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[RefreshProperties(RefreshProperties.Repaint)]
	[Editor("System.Windows.Forms.Design.MaskPropertyEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[DefaultValue("")]
	[MergableProperty(false)]
	public string Mask
	{
		get
		{
			if (is_empty_mask)
			{
				return string.Empty;
			}
			return provider.Mask;
		}
		set
		{
			is_empty_mask = value == string.Empty || value == null;
			if (is_empty_mask)
			{
				value = "<>";
			}
			provider = new MaskedTextProvider(value, provider.Culture, provider.AllowPromptAsInput, provider.PromptChar, provider.PasswordChar, provider.AsciiOnly);
			ReCalculatePasswordChar();
			UpdateVisibleText();
		}
	}

	/// <summary>Gets a value indicating whether all required inputs have been entered into the input mask.</summary>
	/// <returns>true if all required input has been entered into the mask; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public bool MaskCompleted => provider.MaskCompleted;

	/// <summary>Gets a clone of the mask provider associated with this instance of the masked text box control.</summary>
	/// <returns>A masking language provider of type <see cref="P:System.Windows.Forms.MaskedTextBox.MaskedTextProvider" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public MaskedTextProvider MaskedTextProvider
	{
		get
		{
			if (is_empty_mask)
			{
				return null;
			}
			return provider.Clone() as MaskedTextProvider;
		}
	}

	/// <summary>Gets a value indicating whether all required and optional inputs have been entered into the input mask. </summary>
	/// <returns>true if all required and optional inputs have been entered; otherwise, false. </returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public bool MaskFull => provider.MaskFull;

	/// <summary>Gets or sets the maximum number of characters the user can type or paste into the text box control. This property is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />. </summary>
	/// <returns>This property always returns 0. </returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override int MaxLength
	{
		get
		{
			return base.MaxLength;
		}
		set
		{
		}
	}

	/// <summary>Gets or sets a value indicating whether this is a multiline text box control. This property is not fully supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />. </summary>
	/// <returns>This property always returns false.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override bool Multiline
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	/// <summary>Gets or sets the character to be displayed in substitute for user input.</summary>
	/// <returns>The <see cref="T:System.Char" /> value used as the password character.</returns>
	/// <exception cref="T:System.ArgumentException">The character specified when setting this property is not a valid password character, as determined by the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidPasswordChar(System.Char)" /> method of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class.</exception>
	/// <exception cref="T:System.InvalidOperationException">The password character specified is the same as the current prompt character, <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar" />. The two are required to be different.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue('\0')]
	public char PasswordChar
	{
		get
		{
			if (use_system_password_char)
			{
				return '*';
			}
			return provider.PasswordChar;
		}
		set
		{
			provider.PasswordChar = value;
			if (value != 0)
			{
				provider.IsPassword = true;
			}
			else
			{
				provider.IsPassword = false;
			}
			ReCalculatePasswordChar(using_password: true);
			CalculateDocument();
			UpdateVisibleText();
		}
	}

	/// <summary>Gets or sets the character used to represent the absence of user input in <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
	/// <returns>The character used to prompt the user for input. The default is an underscore (_). </returns>
	/// <exception cref="T:System.ArgumentException">The character specified when setting this property is not a valid prompt character, as determined by the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidPasswordChar(System.Char)" /> method of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class.</exception>
	/// <exception cref="T:System.InvalidOperationException">The prompt character specified is the same as the current password character, <see cref="P:System.Windows.Forms.MaskedTextBox.PasswordChar" />. The two are required to be different.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue('_')]
	[Localizable(true)]
	[RefreshProperties(RefreshProperties.Repaint)]
	public char PromptChar
	{
		get
		{
			return provider.PromptChar;
		}
		set
		{
			provider.PromptChar = value;
			UpdateVisibleText();
		}
	}

	public new bool ReadOnly
	{
		get
		{
			return base.ReadOnly;
		}
		set
		{
			base.ReadOnly = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the parsing of user input should stop after the first invalid character is reached.</summary>
	/// <returns>true if processing of the input string should be terminated at the first parsing error; otherwise, false if processing should ignore all errors. The default is false.</returns>
	[DefaultValue(false)]
	public bool RejectInputOnFirstFailure
	{
		get
		{
			return reject_input_on_first_failure;
		}
		set
		{
			reject_input_on_first_failure = value;
		}
	}

	/// <summary>Gets or sets a value that determines how an input character that matches the prompt character should be handled.</summary>
	/// <returns>true if the prompt character entered as input causes the current editable position in the mask to be reset; otherwise, false to indicate that the prompt character is to be processed as a normal input character. The default is true.</returns>
	[DefaultValue(true)]
	public bool ResetOnPrompt
	{
		get
		{
			return provider.ResetOnPrompt;
		}
		set
		{
			provider.ResetOnPrompt = value;
		}
	}

	/// <summary>Gets or sets a value that determines how a space input character should be handled.</summary>
	/// <returns>true if the space input character causes the current editable position in the mask to be reset; otherwise, false to indicate that it is to be processed as a normal input character. The default is true.</returns>
	[DefaultValue(true)]
	public bool ResetOnSpace
	{
		get
		{
			return provider.ResetOnSpace;
		}
		set
		{
			provider.ResetOnSpace = value;
		}
	}

	/// <summary>Gets or sets the current selection in the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control.</summary>
	/// <returns>The currently selected text as a <see cref="T:System.String" />. If no text is currently selected, this property resolves to an empty string.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlThread, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override string SelectedText
	{
		get
		{
			return base.SelectedText;
		}
		set
		{
			base.SelectedText = value;
			UpdateVisibleText();
		}
	}

	/// <summary>Gets or sets a value indicating whether the user is allowed to reenter literal values.</summary>
	/// <returns>true to allow literals to be reentered; otherwise, false to prevent the user from overwriting literal characters. The default is true.</returns>
	[DefaultValue(true)]
	public bool SkipLiterals
	{
		get
		{
			return provider.SkipLiterals;
		}
		set
		{
			provider.SkipLiterals = value;
		}
	}

	/// <summary>Gets or sets the text as it is currently displayed to the user. </summary>
	/// <returns>A <see cref="T:System.String" /> containing the text currently displayed by the control. The default is an empty string.</returns>
	/// <filterpriority>1</filterpriority>
	[Editor("System.Windows.Forms.Design.MaskedTextBoxTextEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[DefaultValue("")]
	[RefreshProperties(RefreshProperties.Repaint)]
	[Localizable(true)]
	[Bindable(true)]
	public override string Text
	{
		get
		{
			if (is_empty_mask || setting_text)
			{
				return base.Text;
			}
			if (provider == null)
			{
				return string.Empty;
			}
			return provider.ToString();
		}
		set
		{
			if (is_empty_mask)
			{
				setting_text = true;
				base.Text = value;
				setting_text = false;
			}
			else
			{
				InputText(value);
			}
			UpdateVisibleText();
		}
	}

	/// <summary>Gets or sets how text is aligned in a masked text box control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> enumeration values that specifies how text is aligned relative to the control. The default is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to this property is not of type <see cref="T:System.Windows.Forms.HorizontalAlignment" />.</exception>
	[DefaultValue(HorizontalAlignment.Left)]
	[Localizable(true)]
	public HorizontalAlignment TextAlign
	{
		get
		{
			return text_align;
		}
		set
		{
			if (text_align != value)
			{
				if (!Enum.IsDefined(typeof(HorizontalAlignment), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(HorizontalAlignment));
				}
				text_align = value;
				OnTextAlignChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets the length of the displayed text. </summary>
	/// <returns>An Int32 representing the number of characters in the <see cref="P:System.Windows.Forms.MaskedTextBox.Text" /> property. <see cref="P:System.Windows.Forms.MaskedTextBox.TextLength" /> respects properties such as <see cref="P:System.Windows.Forms.MaskedTextBox.HidePromptOnLeave" />, which means that the return results may be different depending on whether the control has focus.</returns>
	[Browsable(false)]
	public override int TextLength => Text.Length;

	/// <summary>Gets or sets a value that determines whether literals and prompt characters are included in the formatted string.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.MaskFormat" /> values. The default is <see cref="F:System.Windows.Forms.MaskFormat.IncludeLiterals" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Property set with a <see cref="T:System.Windows.Forms.MaskFormat" /> value that is not valid. </exception>
	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue(MaskFormat.IncludeLiterals)]
	public MaskFormat TextMaskFormat
	{
		get
		{
			if (provider.IncludePrompt && provider.IncludeLiterals)
			{
				return MaskFormat.IncludePromptAndLiterals;
			}
			if (provider.IncludeLiterals)
			{
				return MaskFormat.IncludeLiterals;
			}
			if (provider.IncludePrompt)
			{
				return MaskFormat.IncludePrompt;
			}
			return MaskFormat.ExcludePromptAndLiterals;
		}
		set
		{
			if (!Enum.IsDefined(typeof(MaskFormat), value))
			{
				throw new InvalidEnumArgumentException("value", (int)value, typeof(MaskFormat));
			}
			provider.IncludeLiterals = (value & MaskFormat.IncludeLiterals) == MaskFormat.IncludeLiterals;
			provider.IncludePrompt = (value & MaskFormat.IncludePrompt) == MaskFormat.IncludePrompt;
		}
	}

	/// <summary>Gets or sets a value indicating whether the operating system-supplied password character should be used.</summary>
	/// <returns>true if the system password should be used as the prompt character; otherwise, false. The default is false.</returns>
	/// <exception cref="T:System.InvalidOperationException">The password character specified is the same as the current prompt character, <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar" />. The two are required to be different.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue(false)]
	public bool UseSystemPasswordChar
	{
		get
		{
			return use_system_password_char;
		}
		set
		{
			if (use_system_password_char != value)
			{
				use_system_password_char = value;
				if (use_system_password_char)
				{
					PasswordChar = PasswordChar;
				}
				else
				{
					PasswordChar = '\0';
				}
			}
		}
	}

	/// <summary>Gets or sets the data type used to verify the data input by the user. </summary>
	/// <returns>A <see cref="T:System.Type" /> representing the data type used in validation. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(null)]
	[Browsable(false)]
	public Type ValidatingType
	{
		get
		{
			return validating_type;
		}
		set
		{
			validating_type = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether a multiline text box control automatically wraps words to the beginning of the next line when necessary. This property is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />. </summary>
	/// <returns>The <see cref="P:System.Windows.Forms.MaskedTextBox.WordWrap" /> property always returns false. </returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new bool WordWrap
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.MaskedTextBox.AcceptsTab" /> property has changed. This event is not raised by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler AcceptsTabChanged
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

	/// <summary>Occurs after the insert mode has changed. </summary>
	public event EventHandler IsOverwriteModeChanged
	{
		add
		{
			base.Events.AddHandler(IsOverwriteModeChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(IsOverwriteModeChangedEvent, value);
		}
	}

	/// <summary>Occurs after the input mask is changed.</summary>
	public event EventHandler MaskChanged
	{
		add
		{
			base.Events.AddHandler(MaskChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MaskChangedEvent, value);
		}
	}

	/// <summary>Occurs when the user's input or assigned character does not match the corresponding format element of the input mask.</summary>
	/// <filterpriority>1</filterpriority>
	public event MaskInputRejectedEventHandler MaskInputRejected
	{
		add
		{
			base.Events.AddHandler(MaskInputRejectedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MaskInputRejectedEvent, value);
		}
	}

	/// <summary>Typically occurs when the value of the <see cref="P:System.Windows.Forms.MaskedTextBox.Multiline" /> property has changed; however, this event is not raised by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler MultilineChanged
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

	/// <summary>Occurs when the text alignment is changed. </summary>
	public event EventHandler TextAlignChanged
	{
		add
		{
			base.Events.AddHandler(TextAlignChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(TextAlignChangedEvent, value);
		}
	}

	/// <summary>Occurs when <see cref="T:System.Windows.Forms.MaskedTextBox" /> has finished parsing the current value using the <see cref="P:System.Windows.Forms.MaskedTextBox.ValidatingType" /> property.</summary>
	public event TypeValidationEventHandler TypeValidationCompleted
	{
		add
		{
			base.Events.AddHandler(TypeValidationCompletedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(TypeValidationCompletedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MaskedTextBox" /> class using defaults.</summary>
	public MaskedTextBox()
	{
		provider = new MaskedTextProvider("<>", CultureInfo.CurrentCulture);
		is_empty_mask = true;
		Init();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MaskedTextBox" /> class using the specified custom mask language provider.</summary>
	/// <param name="maskedTextProvider">A custom mask language provider, derived from the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="maskedTextProvider" /> is null.</exception>
	public MaskedTextBox(MaskedTextProvider maskedTextProvider)
	{
		if (maskedTextProvider == null)
		{
			throw new ArgumentNullException();
		}
		provider = maskedTextProvider;
		is_empty_mask = false;
		Init();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MaskedTextBox" /> class using the specified input mask.</summary>
	/// <param name="mask">A <see cref="T:System.String" /> representing the input mask. The initial value of the <see cref="P:System.Windows.Forms.MaskedTextBox.Mask" /> property.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="mask" /> is null.</exception>
	public MaskedTextBox(string mask)
	{
		if (mask == null)
		{
			throw new ArgumentNullException();
		}
		provider = new MaskedTextProvider(mask, CultureInfo.CurrentCulture);
		is_empty_mask = false;
		Init();
	}

	static MaskedTextBox()
	{
		AcceptsTabChanged = new object();
		IsOverwriteModeChanged = new object();
		MaskChanged = new object();
		MaskInputRejected = new object();
		MultilineChanged = new object();
		TextAlignChanged = new object();
		TypeValidationCompleted = new object();
	}

	private void Init()
	{
		BackColor = SystemColors.Window;
		cut_copy_mask_format = MaskFormat.IncludeLiterals;
		insert_key_overwriting = false;
		UpdateVisibleText();
	}

	/// <summary>Clears information about the most recent operation from the undo buffer of the text box. This method is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new void ClearUndo()
	{
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PermissionSet(SecurityAction.InheritanceDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.UIPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nWindow=\"AllWindows\"/>\n</PermissionSet>\n")]
	protected override void CreateHandle()
	{
		base.CreateHandle();
	}

	/// <returns>The character at the specified location.</returns>
	/// <param name="pt">The location from which to seek the nearest character. </param>
	public override char GetCharFromPosition(Point pt)
	{
		return base.GetCharFromPosition(pt);
	}

	/// <returns>The zero-based character index at the specified location.</returns>
	/// <param name="pt">The location to search. </param>
	public override int GetCharIndexFromPosition(Point pt)
	{
		return base.GetCharIndexFromPosition(pt);
	}

	/// <summary>Retrieves the index of the first character of a given line. This method is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />. </summary>
	/// <returns>This method will always return 0. </returns>
	/// <param name="lineNumber">This parameter is not used.</param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new int GetFirstCharIndexFromLine(int lineNumber)
	{
		return 0;
	}

	/// <summary>Retrieves the index of the first character of the current line. This method is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />. </summary>
	/// <returns>This method will always return 0. </returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new int GetFirstCharIndexOfCurrentLine()
	{
		return 0;
	}

	/// <summary>Retrieves the line number from the specified character position within the text of the control. This method is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />. </summary>
	/// <returns>This method will always return 0.</returns>
	/// <param name="index">This parameter is not used.</param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override int GetLineFromCharIndex(int index)
	{
		return 0;
	}

	/// <returns>The location of the specified character within the client rectangle of the control.</returns>
	/// <param name="index">The index of the character for which to retrieve the location. </param>
	public override Point GetPositionFromCharIndex(int index)
	{
		return base.GetPositionFromCharIndex(index);
	}

	/// <param name="keyData"></param>
	protected override bool IsInputKey(Keys keyData)
	{
		return base.IsInputKey(keyData);
	}

	/// <param name="e"></param>
	protected override void OnBackColorChanged(EventArgs e)
	{
		base.OnBackColorChanged(e);
	}

	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.MaskedTextBox.IsOverwriteModeChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnIsOverwriteModeChanged(EventArgs e)
	{
		((EventHandler)base.Events[IsOverwriteModeChanged])?.Invoke(this, e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	protected override void OnKeyDown(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Insert && insert_key_mode == InsertKeyMode.Default)
		{
			insert_key_overwriting = !insert_key_overwriting;
			OnIsOverwriteModeChanged(EventArgs.Empty);
			e.Handled = true;
		}
		else if (e.KeyCode != Keys.Delete || is_empty_mask)
		{
			base.OnKeyDown(e);
		}
		else
		{
			int endPosition = ((SelectionLength != 0) ? (base.SelectionStart + SelectionLength - 1) : base.SelectionStart);
			int testPosition;
			MaskedTextResultHint resultHint;
			bool result = provider.RemoveAt(base.SelectionStart, endPosition, out testPosition, out resultHint);
			PostprocessKeyboardInput(result, testPosition, testPosition, resultHint);
			e.Handled = true;
		}
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data. </param>
	protected override void OnKeyPress(KeyPressEventArgs e)
	{
		if (is_empty_mask)
		{
			base.OnKeyPress(e);
			return;
		}
		bool result;
		int testPosition;
		MaskedTextResultHint resultHint;
		int newPosition;
		if (e.KeyChar == '\b')
		{
			result = ((SelectionLength != 0) ? provider.RemoveAt(base.SelectionStart, base.SelectionStart + SelectionLength - 1, out testPosition, out resultHint) : provider.RemoveAt(base.SelectionStart - 1, base.SelectionStart - 1, out testPosition, out resultHint));
			newPosition = testPosition;
		}
		else if (IsOverwriteMode || SelectionLength > 0)
		{
			int num = provider.FindEditPositionFrom(base.SelectionStart, direction: true);
			int endPosition = ((SelectionLength <= 0) ? num : (base.SelectionStart + SelectionLength - 1));
			result = provider.Replace(e.KeyChar, num, endPosition, out testPosition, out resultHint);
			newPosition = testPosition + 1;
		}
		else
		{
			result = provider.InsertAt(e.KeyChar, base.SelectionStart, out testPosition, out resultHint);
			newPosition = testPosition + 1;
		}
		PostprocessKeyboardInput(result, newPosition, testPosition, resultHint);
		e.Handled = true;
	}

	private void PostprocessKeyboardInput(bool result, int newPosition, int testPosition, MaskedTextResultHint resultHint)
	{
		if (!result)
		{
			OnMaskInputRejected(new MaskInputRejectedEventArgs(testPosition, resultHint));
			return;
		}
		if (newPosition != MaskedTextProvider.InvalidIndex)
		{
			base.SelectionStart = newPosition;
		}
		else
		{
			base.SelectionStart = provider.Length;
		}
		UpdateVisibleText();
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	protected override void OnKeyUp(KeyEventArgs e)
	{
		base.OnKeyUp(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.MaskedTextBox.MaskChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnMaskChanged(EventArgs e)
	{
		((EventHandler)base.Events[MaskChanged])?.Invoke(this, e);
	}

	private void OnMaskInputRejected(MaskInputRejectedEventArgs e)
	{
		((MaskInputRejectedEventHandler)base.Events[MaskInputRejected])?.Invoke(this, e);
	}

	/// <summary>Typically raises the <see cref="E:System.Windows.Forms.MaskedTextBox.MultilineChanged" /> event, but disabled for <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected override void OnMultilineChanged(EventArgs e)
	{
		((EventHandler)base.Events[MultilineChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.MaskedTextBox.TextAlignChanged" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
	protected virtual void OnTextAlignChanged(EventArgs e)
	{
		((EventHandler)base.Events[TextAlignChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
	protected override void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Validating" /> event.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains event data. </param>
	/// <exception cref="T:System.Exception">A critical exception occurred during the parsing of the input string.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnValidating(CancelEventArgs e)
	{
		base.OnValidating(e);
	}

	/// <returns>true if the command key was processed by the control; otherwise, false.</returns>
	/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference that represents the window message to process. </param>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the shortcut key to process. </param>
	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		return base.ProcessCmdKey(ref msg, keyData);
	}

	/// <summary>Overrides the base implementation of this method to handle input language changes.</summary>
	/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> object containing a description of the key pressed.</param>
	protected internal override bool ProcessKeyMessage(ref Message m)
	{
		return base.ProcessKeyMessage(ref m);
	}

	/// <summary>Scrolls the contents of the control to the current caret position. This method is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new void ScrollToCaret()
	{
	}

	/// <summary>Returns a string that represents the current masked text box. This method overrides <see cref="M:System.Windows.Forms.TextBoxBase.ToString" />.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains information about the current <see cref="T:System.Windows.Forms.MaskedTextBox" />. The string includes the type, a simplified view of the input string, and the formatted input string.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return base.ToString() + ", Text: " + provider.ToString(includePrompt: false, includeLiterals: false);
	}

	/// <summary>Undoes the last edit operation in the text box. This method is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new void Undo()
	{
	}

	/// <summary>Converts the user input string to an instance of the validating type.</summary>
	/// <returns>If successful, an <see cref="T:System.Object" /> of the type specified by the <see cref="P:System.Windows.Forms.MaskedTextBox.ValidatingType" /> property; otherwise, null to indicate conversion failure.</returns>
	/// <exception cref="T:System.Exception">A critical exception occurred during the parsing of the input string.</exception>
	public object ValidateText()
	{
		throw new NotImplementedException();
	}

	/// <param name="m">A Windows Message Object. </param>
	protected override void WndProc(ref Message m)
	{
		Msg msg = (Msg)m.Msg;
		base.WndProc(ref m);
	}

	private void ReCalculatePasswordChar()
	{
		ReCalculatePasswordChar(PasswordChar != '\0');
	}

	private void ReCalculatePasswordChar(bool using_password)
	{
		if (using_password)
		{
			if (is_empty_mask)
			{
				document.PasswordChar = PasswordChar.ToString();
			}
			else
			{
				document.PasswordChar = string.Empty;
			}
		}
	}

	internal override void OnPaintInternal(PaintEventArgs pevent)
	{
		base.OnPaintInternal(pevent);
	}

	internal override Color ChangeBackColor(Color backColor)
	{
		return backColor;
	}

	private void UpdateVisibleText()
	{
		string text = null;
		text = ((is_empty_mask || setting_text) ? base.Text : ((provider != null) ? provider.ToDisplayString() : string.Empty));
		setting_text = true;
		if (base.Text != text)
		{
			int selectionStart = base.SelectionStart;
			base.Text = text;
			base.SelectionStart = selectionStart;
		}
		setting_text = false;
	}

	private void InputText(string text)
	{
		MaskedTextResultHint resultHint;
		int testPosition;
		if (RejectInputOnFirstFailure)
		{
			if (!provider.Set(text, out testPosition, out resultHint))
			{
				OnMaskInputRejected(new MaskInputRejectedEventArgs(testPosition, resultHint));
			}
			return;
		}
		provider.Clear();
		testPosition = 0;
		foreach (char input in text)
		{
			if (provider.InsertAt(input, testPosition, out testPosition, out resultHint))
			{
				testPosition++;
			}
			else
			{
				OnMaskInputRejected(new MaskInputRejectedEventArgs(testPosition, resultHint));
			}
		}
	}
}
