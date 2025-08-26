using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms.RTF;

namespace System.Windows.Forms;

/// <summary>Represents a Windows rich text box control.</summary>
/// <filterpriority>2</filterpriority>
[Docking(DockingBehavior.Ask)]
[Designer("System.Windows.Forms.Design.RichTextBoxDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class RichTextBox : TextBoxBase
{
	private class RtfSectionStyle : ICloneable
	{
		internal System.Drawing.Color rtf_color;

		internal System.Windows.Forms.RTF.Font rtf_rtffont;

		internal int rtf_rtffont_size;

		internal FontStyle rtf_rtfstyle;

		internal HorizontalAlignment rtf_rtfalign;

		internal int rtf_par_line_left_indent;

		internal bool rtf_visible;

		internal int rtf_skip_width;

		public object Clone()
		{
			RtfSectionStyle rtfSectionStyle = new RtfSectionStyle();
			rtfSectionStyle.rtf_color = rtf_color;
			rtfSectionStyle.rtf_par_line_left_indent = rtf_par_line_left_indent;
			rtfSectionStyle.rtf_rtfalign = rtf_rtfalign;
			rtfSectionStyle.rtf_rtffont = rtf_rtffont;
			rtfSectionStyle.rtf_rtffont_size = rtf_rtffont_size;
			rtfSectionStyle.rtf_rtfstyle = rtf_rtfstyle;
			rtfSectionStyle.rtf_visible = rtf_visible;
			rtfSectionStyle.rtf_skip_width = rtf_skip_width;
			return rtfSectionStyle;
		}
	}

	internal bool auto_word_select;

	internal int bullet_indent;

	internal bool detect_urls;

	private bool reuse_line;

	internal int margin_right;

	internal float zoom;

	private StringBuilder rtf_line;

	private RtfSectionStyle rtf_style;

	private Stack rtf_section_stack;

	private TextMap rtf_text_map;

	private int rtf_skip_count;

	private int rtf_cursor_x;

	private int rtf_cursor_y;

	private int rtf_chars;

	private bool enable_auto_drag_drop;

	private RichTextBoxLanguageOptions language_option;

	private bool rich_text_shortcuts_enabled;

	private System.Drawing.Color selection_back_color;

	private static object ContentsResizedEvent;

	private static object HScrollEvent;

	private static object ImeChangeEvent;

	private static object LinkClickedEvent;

	private static object ProtectedEvent;

	private static object SelectionChangedEvent;

	private static object VScrollEvent;

	private static readonly char[] ReservedRTFChars;

	/// <summary>Gets or sets a value indicating whether the control will enable drag-and-drop operations.</summary>
	/// <returns>true if drag-and-drop is enabled in the control; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
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

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true if enabled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DefaultValue(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	[RefreshProperties(RefreshProperties.Repaint)]
	[Browsable(false)]
	public override bool AutoSize
	{
		get
		{
			return auto_size;
		}
		set
		{
			base.AutoSize = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether automatic word selection is enabled.</summary>
	/// <returns>true if automatic word selection is enabled; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[System.MonoTODO("Value not respected, always true")]
	[DefaultValue(false)]
	public bool AutoWordSelection
	{
		get
		{
			return auto_word_select;
		}
		set
		{
			auto_word_select = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
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

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Gets or sets the indentation used in the <see cref="T:System.Windows.Forms.RichTextBox" /> control when the bullet style is applied to the text.</summary>
	/// <returns>The number of pixels inserted as the indentation after a bullet. The default is zero.</returns>
	/// <exception cref="T:System.ArgumentException">The specified indentation was less than zero. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(0)]
	[Localizable(true)]
	public int BulletIndent
	{
		get
		{
			return bullet_indent;
		}
		set
		{
			bullet_indent = value;
		}
	}

	/// <summary>Gets a value indicating whether there are actions that have occurred within the <see cref="T:System.Windows.Forms.RichTextBox" /> that can be reapplied.</summary>
	/// <returns>true if there are operations that have been undone that can be reapplied to the content of the control; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanRedo => document.undo.CanRedo;

	/// <summary>Gets or sets a value indicating whether or not the <see cref="T:System.Windows.Forms.RichTextBox" /> will automatically format a Uniform Resource Locator (URL) when it is typed into the control.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.RichTextBox" /> will automatically format URLs that are typed into the control as a link; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	public bool DetectUrls
	{
		get
		{
			return base.EnableLinks;
		}
		set
		{
			base.EnableLinks = value;
		}
	}

	/// <summary>Gets or sets a value that enables drag-and-drop operations on text, pictures, and other data.</summary>
	/// <returns>true to enable drag-and-drop operations; otherwise, false. The default is false.</returns>
	[DefaultValue(false)]
	[System.MonoTODO("Stub, does nothing")]
	public bool EnableAutoDragDrop
	{
		get
		{
			return enable_auto_drag_drop;
		}
		set
		{
			enable_auto_drag_drop = value;
		}
	}

	/// <summary>Gets or sets the font used when displaying text in the control.</summary>
	/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override System.Drawing.Font Font
	{
		get
		{
			return base.Font;
		}
		set
		{
			if (font != value)
			{
				if (auto_size && base.PreferredHeight != base.Height)
				{
					base.Height = base.PreferredHeight;
				}
				base.Font = value;
				Line line = document.GetLine(1);
				Line line2 = document.GetLine(document.Lines);
				document.FormatText(line, 1, line2, line2.text.Length + 1, base.Font, System.Drawing.Color.Empty, System.Drawing.Color.Empty, FormatSpecified.Font);
			}
		}
	}

	/// <summary>Gets or sets the font color used when displaying text in the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the control's foreground color.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override System.Drawing.Color ForeColor
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

	/// <summary>Gets or sets a value that indicates <see cref="T:System.Windows.Forms.RichTextBox" /> settings for Input Method Editor (IME) and Asian language support.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.RichTextBoxLanguageOptions" /> values. The default is <see cref="F:System.Windows.Forms.RichTextBoxLanguageOptions.AutoFontSizeAdjust" />.</returns>
	[System.MonoTODO("Stub, does nothing")]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public RichTextBoxLanguageOptions LanguageOption
	{
		get
		{
			return language_option;
		}
		set
		{
			language_option = value;
		}
	}

	/// <summary>Gets or sets the maximum number of characters the user can type or paste into the rich text box control.</summary>
	/// <returns>The number of characters that can be entered into the control. The default is <see cref="F:System.Int32.MaxValue" />.</returns>
	/// <exception cref="T:System.ArgumentException">The value assigned to the property is less than 0. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(int.MaxValue)]
	public override int MaxLength
	{
		get
		{
			return base.MaxLength;
		}
		set
		{
			base.MaxLength = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether this is a multiline <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	/// <returns>true if the control is a multiline <see cref="T:System.Windows.Forms.RichTextBox" /> control; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	public override bool Multiline
	{
		get
		{
			return base.Multiline;
		}
		set
		{
			base.Multiline = value;
		}
	}

	/// <summary>Gets the name of the action that can be reapplied to the control when the <see cref="M:System.Windows.Forms.RichTextBox.Redo" /> method is called.</summary>
	/// <returns>A string that represents the name of the action that will be performed when a call to the <see cref="M:System.Windows.Forms.RichTextBox.Redo" /> method is made.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string RedoActionName => document.undo.RedoActionName;

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>true if shortcut keys are enabled; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[System.MonoTODO("Stub, does nothing")]
	[Browsable(false)]
	[DefaultValue(true)]
	public bool RichTextShortcutsEnabled
	{
		get
		{
			return rich_text_shortcuts_enabled;
		}
		set
		{
			rich_text_shortcuts_enabled = value;
		}
	}

	/// <summary>Gets or sets the size of a single line of text within the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	/// <returns>The size, in pixels, of a single line of text in the control. The default is zero.</returns>
	/// <exception cref="T:System.ArgumentException">The specified value was less than zero. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[System.MonoInternalNote("Teach TextControl.RecalculateLine to consider the right margin as well")]
	[System.MonoTODO("Stub, does nothing")]
	[Localizable(true)]
	[DefaultValue(0)]
	public int RightMargin
	{
		get
		{
			return margin_right;
		}
		set
		{
			margin_right = value;
		}
	}

	/// <summary>Gets or sets the text of the <see cref="T:System.Windows.Forms.RichTextBox" /> control, including all rich text format (RTF) codes.</summary>
	/// <returns>The text of the control in RTF format.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[RefreshProperties(RefreshProperties.All)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string Rtf
	{
		get
		{
			Line line = document.GetLine(1);
			Line line2 = document.GetLine(document.Lines);
			return GenerateRTF(line, 0, line2, line2.text.Length).ToString();
		}
		set
		{
			document.Empty();
			MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(value), writable: false);
			InsertRTFFromStream(memoryStream, 0, 1);
			memoryStream.Close();
			Invalidate();
		}
	}

	/// <summary>Gets or sets the type of scroll bars to display in the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.RichTextBoxScrollBars" /> values. The default is RichTextBoxScrollBars.Both.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not defined in the <see cref="T:System.Windows.Forms.RichTextBoxScrollBars" /> enumeration. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(RichTextBoxScrollBars.Both)]
	public RichTextBoxScrollBars ScrollBars
	{
		get
		{
			return scrollbars;
		}
		set
		{
			if (!Enum.IsDefined(typeof(RichTextBoxScrollBars), value))
			{
				throw new InvalidEnumArgumentException("value", (int)value, typeof(RichTextBoxScrollBars));
			}
			if (value != scrollbars)
			{
				scrollbars = value;
				CalculateDocument();
			}
		}
	}

	/// <summary>Gets or sets the currently selected rich text format (RTF) formatted text in the control.</summary>
	/// <returns>The selected RTF text in the control.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string SelectedRtf
	{
		get
		{
			return GenerateRTF(document.selection_start.line, document.selection_start.pos, document.selection_end.line, document.selection_end.pos).ToString();
		}
		set
		{
			if (document.selection_visible)
			{
				document.ReplaceSelection(string.Empty, select_new: false);
			}
			int pos = document.LineTagToCharIndex(document.selection_start.line, document.selection_start.pos);
			MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(value), writable: false);
			int pos2 = document.selection_start.pos;
			int line_no = document.selection_start.line.line_no;
			if (pos2 == 0)
			{
				reuse_line = true;
			}
			InsertRTFFromStream(memoryStream, pos2, line_no, out var _, out var to_y, out var chars);
			memoryStream.Close();
			int num = document.LineEndingLength((!XplatUI.RunningOnUnix) ? LineEnding.Hard : LineEnding.Rich);
			document.CharIndexToLineTag(pos + chars + (to_y - document.selection_start.line.line_no) * num, out var line_out, out var _, out pos);
			if (pos >= line_out.text.Length)
			{
				pos = line_out.text.Length - 1;
			}
			document.SetSelection(line_out, pos);
			document.PositionCaret(line_out, pos);
			document.DisplayCaret();
			ScrollToCaret();
			OnTextChanged(EventArgs.Empty);
		}
	}

	/// <summary>Gets or sets the selected text within the <see cref="T:System.Windows.Forms.RichTextBox" />.</summary>
	/// <returns>A string that represents the selected text in the control.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[DefaultValue("")]
	public override string SelectedText
	{
		get
		{
			return base.SelectedText;
		}
		set
		{
			base.Modified = true;
			base.SelectedText = value;
		}
	}

	/// <summary>Gets or sets the alignment to apply to the current selection or insertion point.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values defined in the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> class. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[DefaultValue(HorizontalAlignment.Left)]
	public HorizontalAlignment SelectionAlignment
	{
		get
		{
			Line line = document.ParagraphStart(document.selection_start.line);
			HorizontalAlignment horizontalAlignment = line.alignment;
			Line line2 = document.ParagraphEnd(document.selection_end.line);
			Line line3 = line;
			while (true)
			{
				if (line3.alignment != horizontalAlignment)
				{
					return HorizontalAlignment.Left;
				}
				if (line3 == line2)
				{
					break;
				}
				line3 = document.GetLine(line3.line_no + 1);
			}
			return horizontalAlignment;
		}
		set
		{
			Line line = document.ParagraphStart(document.selection_start.line);
			Line line2 = document.ParagraphEnd(document.selection_end.line);
			Line line3 = line;
			while (true)
			{
				line3.alignment = value;
				if (line3 == line2)
				{
					break;
				}
				line3 = document.GetLine(line3.line_no + 1);
			}
			CalculateDocument();
		}
	}

	/// <summary>Gets or sets the color of text when the text is selected in a <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the text color when the text is selected. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[System.MonoTODO("Stub, does nothing")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public System.Drawing.Color SelectionBackColor
	{
		get
		{
			return selection_back_color;
		}
		set
		{
			selection_back_color = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the bullet style is applied to the current selection or insertion point.</summary>
	/// <returns>true if the current selection or insertion point has the bullet style applied; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	[Browsable(false)]
	[System.MonoTODO("Stub, does nothing")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool SelectionBullet
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	/// <summary>Gets or sets whether text in the control appears on the baseline, as a superscript, or as a subscript below the baseline.</summary>
	/// <returns>A number that specifies the character offset.</returns>
	/// <exception cref="T:System.ArgumentException">The specified value was less than -2000 or greater than 2000. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(0)]
	[Browsable(false)]
	[System.MonoTODO("Stub, does nothing")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int SelectionCharOffset
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	/// <summary>Gets or sets the text color of the current text selection or insertion point.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color to apply to the current text selection or to text entered after the insertion point.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public System.Drawing.Color SelectionColor
	{
		get
		{
			LineTag lineTag;
			LineTag lineTag2;
			if (selection_length > 0)
			{
				lineTag = document.selection_start.line.FindTag(document.selection_start.pos + 1);
				lineTag2 = document.selection_start.line.FindTag(document.selection_end.pos);
			}
			else
			{
				lineTag = document.selection_start.line.FindTag(document.selection_start.pos);
				lineTag2 = lineTag;
			}
			System.Drawing.Color color = lineTag.Color;
			for (LineTag lineTag3 = lineTag; lineTag3 != null; lineTag3 = document.NextTag(lineTag3))
			{
				if (!color.Equals(lineTag3.Color))
				{
					return System.Drawing.Color.Empty;
				}
				if (lineTag3 == lineTag2)
				{
					break;
				}
			}
			return color;
		}
		set
		{
			if (value == System.Drawing.Color.Empty)
			{
				value = Control.DefaultForeColor;
			}
			int index = document.LineTagToCharIndex(document.selection_start.line, document.selection_start.pos);
			int index2 = document.LineTagToCharIndex(document.selection_end.line, document.selection_end.pos);
			document.FormatText(document.selection_start.line, document.selection_start.pos + 1, document.selection_end.line, document.selection_end.pos + 1, null, value, System.Drawing.Color.Empty, FormatSpecified.Color);
			document.CharIndexToLineTag(index, out document.selection_start.line, out document.selection_start.tag, out document.selection_start.pos);
			document.CharIndexToLineTag(index2, out document.selection_end.line, out document.selection_end.tag, out document.selection_end.pos);
			document.UpdateView(document.selection_start.line, 0);
			document.AlignCaret(changeCaretTag: false);
		}
	}

	/// <summary>Gets or sets the font of the current text selection or insertion point.</summary>
	/// <returns>A <see cref="T:System.Drawing.Font" /> that represents the font to apply to the current text selection or to text entered after the insertion point.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public System.Drawing.Font SelectionFont
	{
		get
		{
			LineTag lineTag;
			LineTag lineTag2;
			if (selection_length > 0)
			{
				lineTag = document.selection_start.line.FindTag(document.selection_start.pos + 1);
				lineTag2 = document.selection_start.line.FindTag(document.selection_end.pos);
			}
			else
			{
				lineTag = document.selection_start.line.FindTag(document.selection_start.pos);
				lineTag2 = lineTag;
			}
			System.Drawing.Font font = lineTag.Font;
			if (selection_length > 1)
			{
				for (LineTag lineTag3 = lineTag; lineTag3 != null; lineTag3 = document.NextTag(lineTag3))
				{
					if (!font.Equals(lineTag3.Font))
					{
						return null;
					}
					if (lineTag3 == lineTag2)
					{
						break;
					}
				}
			}
			return font;
		}
		set
		{
			int index = document.LineTagToCharIndex(document.selection_start.line, document.selection_start.pos);
			int index2 = document.LineTagToCharIndex(document.selection_end.line, document.selection_end.pos);
			document.FormatText(document.selection_start.line, document.selection_start.pos + 1, document.selection_end.line, document.selection_end.pos + 1, value, System.Drawing.Color.Empty, System.Drawing.Color.Empty, FormatSpecified.Font);
			document.CharIndexToLineTag(index, out document.selection_start.line, out document.selection_start.tag, out document.selection_start.pos);
			document.CharIndexToLineTag(index2, out document.selection_end.line, out document.selection_end.tag, out document.selection_end.pos);
			document.UpdateView(document.selection_start.line, 0);
			base.Document.AlignCaret(changeCaretTag: false);
		}
	}

	/// <summary>Gets or sets the distance between the left edge of the first line of text in the selected paragraph and the left edge of subsequent lines in the same paragraph.</summary>
	/// <returns>The distance, in pixels, for the hanging indent applied to the current text selection or the insertion point.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[System.MonoTODO("Stub, does nothing")]
	[DefaultValue(0)]
	public int SelectionHangingIndent
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	/// <summary>Gets or sets the length, in pixels, of the indentation of the line where the selection starts.</summary>
	/// <returns>The current distance, in pixels, of the indentation applied to the left of the current text selection or the insertion point.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[System.MonoTODO("Stub, does nothing")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[DefaultValue(0)]
	[Browsable(false)]
	public int SelectionIndent
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	/// <summary>Gets or sets the number of characters selected in control.</summary>
	/// <returns>The number of characters selected in the text box.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public override int SelectionLength
	{
		get
		{
			return base.SelectionLength;
		}
		set
		{
			base.SelectionLength = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the current text selection is protected.</summary>
	/// <returns>true if the current selection prevents any changes to its content; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	[System.MonoTODO("Stub, does nothing")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool SelectionProtected
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	/// <summary>The distance (in pixels) between the right edge of the <see cref="T:System.Windows.Forms.RichTextBox" /> control and the right edge of the text that is selected or added at the current insertion point.</summary>
	/// <returns>The indentation space, in pixels, at the right of the current selection or insertion point.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[System.MonoTODO("Stub, does nothing")]
	[Browsable(false)]
	[DefaultValue(0)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int SelectionRightIndent
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	/// <summary>Gets or sets the absolute tab stop positions in a <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	/// <returns>An array in which each member specifies a tab offset, in pixels.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The array has more than the maximum 32 elements. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[System.MonoTODO("Stub, does nothing")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int[] SelectionTabs
	{
		get
		{
			return new int[0];
		}
		set
		{
		}
	}

	/// <summary>Gets the selection type within the control.</summary>
	/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.RichTextBoxSelectionTypes" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public RichTextBoxSelectionTypes SelectionType
	{
		get
		{
			if (document.selection_start == document.selection_end)
			{
				return RichTextBoxSelectionTypes.Empty;
			}
			if (SelectedText.Length > 1)
			{
				return RichTextBoxSelectionTypes.Text | RichTextBoxSelectionTypes.MultiChar;
			}
			return RichTextBoxSelectionTypes.Text;
		}
	}

	/// <summary>Gets or sets a value indicating whether a selection margin is displayed in the <see cref="T:System.Windows.Forms.RichTextBox" />.</summary>
	/// <returns>true if a selection margin is enabled in the control; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	[System.MonoTODO("Stub, does nothing")]
	public bool ShowSelectionMargin
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	/// <summary>Gets or sets the current text in the rich text box.</summary>
	/// <returns>The text displayed in the control.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[RefreshProperties(RefreshProperties.All)]
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

	/// <returns>The number of characters contained in the text of the control.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public override int TextLength => base.TextLength;

	/// <summary>Gets the name of the action that can be undone in the control when the <see cref="M:System.Windows.Forms.TextBoxBase.Undo" /> method is called.</summary>
	/// <returns>The text name of the action that can be undone.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string UndoActionName => document.undo.UndoActionName;

	/// <summary>Gets or sets the current zoom level of the <see cref="T:System.Windows.Forms.RichTextBox" />.</summary>
	/// <returns>The factor by which the contents of the control is zoomed.</returns>
	/// <exception cref="T:System.ArgumentException">The specified zoom factor did not fall within the permissible range. </exception>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[DefaultValue(1)]
	public float ZoomFactor
	{
		get
		{
			return zoom;
		}
		set
		{
			zoom = value;
		}
	}

	/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> representing the information needed when creating a control.</returns>
	protected override CreateParams CreateParams => base.CreateParams;

	/// <summary>Gets the default size of the control. </summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> value.</returns>
	protected override Size DefaultSize => new Size(100, 96);

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.RichTextBox.BackgroundImage" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler BackgroundImageChanged
	{
		add
		{
			base.BackgroundImageChanged += value;
		}
		remove
		{
			base.BackgroundImageChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.RichTextBox.BackgroundImageLayout" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler BackgroundImageLayoutChanged
	{
		add
		{
			base.BackgroundImageLayoutChanged += value;
		}
		remove
		{
			base.BackgroundImageLayoutChanged -= value;
		}
	}

	/// <summary>Occurs when contents within the control are resized.</summary>
	/// <filterpriority>1</filterpriority>
	public event ContentsResizedEventHandler ContentsResized
	{
		add
		{
			base.Events.AddHandler(ContentsResizedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ContentsResizedEvent, value);
		}
	}

	/// <summary>Occurs when the user completes a drag-and-drop </summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public new event DragEventHandler DragDrop
	{
		add
		{
			base.DragDrop += value;
		}
		remove
		{
			base.DragDrop -= value;
		}
	}

	/// <summary>Occurs when an object is dragged into the control's bounds.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public new event DragEventHandler DragEnter
	{
		add
		{
			base.DragEnter += value;
		}
		remove
		{
			base.DragEnter -= value;
		}
	}

	/// <summary>Occurs when an object is dragged out of the control's bounds.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler DragLeave
	{
		add
		{
			base.DragLeave += value;
		}
		remove
		{
			base.DragLeave -= value;
		}
	}

	/// <summary>Occurs when an object is dragged over the control's bounds.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event DragEventHandler DragOver
	{
		add
		{
			base.DragOver += value;
		}
		remove
		{
			base.DragOver -= value;
		}
	}

	/// <summary>Occurs during a drag operation.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event GiveFeedbackEventHandler GiveFeedback
	{
		add
		{
			base.GiveFeedback += value;
		}
		remove
		{
			base.GiveFeedback -= value;
		}
	}

	/// <summary>Occurs when the user clicks the horizontal scroll bar of the control.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler HScroll
	{
		add
		{
			base.Events.AddHandler(HScrollEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(HScrollEvent, value);
		}
	}

	/// <summary>Occurs when the user switches input methods on an Asian version of the Windows operating system.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler ImeChange
	{
		add
		{
			base.Events.AddHandler(ImeChangeEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ImeChangeEvent, value);
		}
	}

	/// <summary>Occurs when the user clicks on a link within the text of the control.</summary>
	/// <filterpriority>1</filterpriority>
	public event LinkClickedEventHandler LinkClicked
	{
		add
		{
			base.Events.AddHandler(LinkClickedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(LinkClickedEvent, value);
		}
	}

	/// <summary>Occurs when the user attempts to modify protected text in the control.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler Protected
	{
		add
		{
			base.Events.AddHandler(ProtectedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ProtectedEvent, value);
		}
	}

	/// <summary>This event is not relevant to this class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event QueryContinueDragEventHandler QueryContinueDrag
	{
		add
		{
			base.QueryContinueDrag += value;
		}
		remove
		{
			base.QueryContinueDrag -= value;
		}
	}

	/// <summary>Occurs when the selection of text within the control has changed.</summary>
	/// <filterpriority>1</filterpriority>
	[System.MonoTODO("Event never raised")]
	public event EventHandler SelectionChanged
	{
		add
		{
			base.Events.AddHandler(SelectionChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SelectionChangedEvent, value);
		}
	}

	/// <summary>Occurs when the user clicks the vertical scroll bars of the control.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler VScroll
	{
		add
		{
			base.Events.AddHandler(VScrollEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(VScrollEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.RichTextBox" /> class.</summary>
	public RichTextBox()
	{
		accepts_return = true;
		auto_size = false;
		auto_word_select = false;
		bullet_indent = 0;
		base.MaxLength = int.MaxValue;
		margin_right = 0;
		zoom = 1f;
		base.Multiline = true;
		document.CRLFSize = 1;
		shortcuts_enabled = true;
		base.EnableLinks = true;
		richtext = true;
		rtf_style = new RtfSectionStyle();
		rtf_section_stack = null;
		scrollbars = RichTextBoxScrollBars.Both;
		alignment = HorizontalAlignment.Left;
		base.LostFocus += RichTextBox_LostFocus;
		base.GotFocus += RichTextBox_GotFocus;
		BackColor = ThemeEngine.Current.ColorWindow;
		backcolor_set = false;
		language_option = RichTextBoxLanguageOptions.AutoFontSizeAdjust;
		rich_text_shortcuts_enabled = true;
		selection_back_color = Control.DefaultBackColor;
		ForeColor = ThemeEngine.Current.ColorWindowText;
		base.HScrolled += RichTextBox_HScrolled;
		base.VScrolled += RichTextBox_VScrolled;
		SetStyle(ControlStyles.StandardDoubleClick, value: false);
	}

	static RichTextBox()
	{
		ContentsResized = new object();
		HScroll = new object();
		ImeChange = new object();
		LinkClicked = new object();
		Protected = new object();
		SelectionChanged = new object();
		VScroll = new object();
		ReservedRTFChars = new char[3] { '\\', '{', '}' };
	}

	internal override void HandleLinkClicked(LinkRectangle link)
	{
		OnLinkClicked(new LinkClickedEventArgs(link.LinkTag.LinkText));
	}

	internal override System.Drawing.Color ChangeBackColor(System.Drawing.Color backColor)
	{
		if (backColor == System.Drawing.Color.Empty)
		{
			backcolor_set = false;
			if (!base.ReadOnly)
			{
				backColor = SystemColors.Window;
			}
		}
		return backColor;
	}

	internal override void RaiseSelectionChanged()
	{
		OnSelectionChanged(EventArgs.Empty);
	}

	private void RichTextBox_LostFocus(object sender, EventArgs e)
	{
		Invalidate();
	}

	private void RichTextBox_GotFocus(object sender, EventArgs e)
	{
		Invalidate();
	}

	/// <summary>Determines whether you can paste information from the Clipboard in the specified data format.</summary>
	/// <returns>true if you can paste data from the Clipboard in the specified data format; otherwise, false.</returns>
	/// <param name="clipFormat">One of the <see cref="T:System.Windows.Forms.DataFormats.Format" /> values. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public bool CanPaste(DataFormats.Format clipFormat)
	{
		if (clipFormat.Name == DataFormats.Rtf || clipFormat.Name == DataFormats.Text || clipFormat.Name == DataFormats.UnicodeText)
		{
			return true;
		}
		return false;
	}

	/// <summary>Searches the text of a <see cref="T:System.Windows.Forms.RichTextBox" /> control for the first instance of a character from a list of characters.</summary>
	/// <returns>The location within the control where the search characters were found or -1 if the search characters are not found or an empty search character set is specified in the <paramref name="char" /> parameter.</returns>
	/// <param name="characterSet">The array of characters to search for. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int Find(char[] characterSet)
	{
		return Find(characterSet, -1, -1);
	}

	/// <summary>Searches the text of a <see cref="T:System.Windows.Forms.RichTextBox" /> control, at a specific starting point, for the first instance of a character from a list of characters.</summary>
	/// <returns>The location within the control where the search characters are found.</returns>
	/// <param name="characterSet">The array of characters to search for. </param>
	/// <param name="start">The location within the control's text at which to begin searching. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int Find(char[] characterSet, int start)
	{
		return Find(characterSet, start, -1);
	}

	/// <summary>Searches a range of text in a <see cref="T:System.Windows.Forms.RichTextBox" /> control for the first instance of a character from a list of characters.</summary>
	/// <returns>The location within the control where the search characters are found.</returns>
	/// <param name="characterSet">The array of characters to search for. </param>
	/// <param name="start">The location within the control's text at which to begin searching. </param>
	/// <param name="end">The location within the control's text at which to end searching. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="characterSet" /> is null. </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="start" /> is less than 0 or greater than the length of the text in the control. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int Find(char[] characterSet, int start, int end)
	{
		Document.Marker mark;
		if (start == -1)
		{
			document.GetMarker(out mark, start: true);
		}
		else
		{
			mark = default(Document.Marker);
			document.CharIndexToLineTag(start, out var line_out, out var tag_out, out var pos);
			mark.line = line_out;
			mark.tag = tag_out;
			mark.pos = pos;
		}
		Document.Marker mark2;
		if (end == -1)
		{
			document.GetMarker(out mark2, start: false);
		}
		else
		{
			mark2 = default(Document.Marker);
			document.CharIndexToLineTag(end, out var line_out2, out var tag_out2, out var pos2);
			mark2.line = line_out2;
			mark2.tag = tag_out2;
			mark2.pos = pos2;
		}
		if (document.FindChars(characterSet, mark, mark2, out var result))
		{
			return document.LineTagToCharIndex(result.line, result.pos);
		}
		return -1;
	}

	/// <summary>Searches the text in a <see cref="T:System.Windows.Forms.RichTextBox" /> control for a string.</summary>
	/// <returns>The location within the control where the search text was found or -1 if the search string is not found or an empty search string is specified in the <paramref name="str" /> parameter.</returns>
	/// <param name="str">The text to locate in the control. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int Find(string str)
	{
		return Find(str, -1, -1, RichTextBoxFinds.None);
	}

	/// <summary>Searches the text in a <see cref="T:System.Windows.Forms.RichTextBox" /> control for a string within a range of text within the control and with specific options applied to the search.</summary>
	/// <returns>The location within the control where the search text was found.</returns>
	/// <param name="str">The text to locate in the control. </param>
	/// <param name="start">The location within the control's text at which to begin searching. </param>
	/// <param name="end">The location within the control's text at which to end searching. This value must be equal to negative one (-1) or greater than or equal to the <paramref name="start" /> parameter. </param>
	/// <param name="options">A bitwise combination of the <see cref="T:System.Windows.Forms.RichTextBoxFinds" /> values. </param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="str" /> parameter was null. </exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="start" /> parameter was less than zero. </exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="end" /> parameter was less the <paramref name="start" /> parameter. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int Find(string str, int start, int end, RichTextBoxFinds options)
	{
		Document.Marker mark;
		if (start == -1)
		{
			document.GetMarker(out mark, start: true);
		}
		else
		{
			mark = default(Document.Marker);
			document.CharIndexToLineTag(start, out var line_out, out var tag_out, out var pos);
			mark.line = line_out;
			mark.tag = tag_out;
			mark.pos = pos;
		}
		Document.Marker mark2;
		if (end == -1)
		{
			document.GetMarker(out mark2, start: false);
		}
		else
		{
			mark2 = default(Document.Marker);
			document.CharIndexToLineTag(end, out var line_out2, out var tag_out2, out var pos2);
			mark2.line = line_out2;
			mark2.tag = tag_out2;
			mark2.pos = pos2;
		}
		if (document.Find(str, mark, mark2, out var result, options))
		{
			return document.LineTagToCharIndex(result.line, result.pos);
		}
		return -1;
	}

	/// <summary>Searches the text in a <see cref="T:System.Windows.Forms.RichTextBox" /> control for a string at a specific location within the control and with specific options applied to the search.</summary>
	/// <returns>The location within the control where the search text was found.</returns>
	/// <param name="str">The text to locate in the control. </param>
	/// <param name="start">The location within the control's text at which to begin searching. </param>
	/// <param name="options">A bitwise combination of the <see cref="T:System.Windows.Forms.RichTextBoxFinds" /> values. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int Find(string str, int start, RichTextBoxFinds options)
	{
		return Find(str, start, -1, options);
	}

	/// <summary>Searches the text in a <see cref="T:System.Windows.Forms.RichTextBox" /> control for a string with specific options applied to the search.</summary>
	/// <returns>The location within the control where the search text was found.</returns>
	/// <param name="str">The text to locate in the control. </param>
	/// <param name="options">A bitwise combination of the <see cref="T:System.Windows.Forms.RichTextBoxFinds" /> values. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int Find(string str, RichTextBoxFinds options)
	{
		return Find(str, -1, -1, options);
	}

	internal override char GetCharFromPositionInternal(Point p)
	{
		PointToTagPos(p, out var tag, out var pos);
		if (pos >= tag.Line.text.Length)
		{
			return '\n';
		}
		return tag.Line.text[pos];
	}

	/// <summary>Retrieves the index of the character nearest to the specified location.</summary>
	/// <returns>The zero-based character index at the specified location.</returns>
	/// <param name="pt">The location to search. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override int GetCharIndexFromPosition(Point pt)
	{
		PointToTagPos(pt, out var tag, out var pos);
		return document.LineTagToCharIndex(tag.Line, pos);
	}

	/// <summary>Retrieves the line number from the specified character position within the text of the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	/// <returns>The zero-based line number in which the character index is located.</returns>
	/// <param name="index">The character index position to search. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override int GetLineFromCharIndex(int index)
	{
		document.CharIndexToLineTag(index, out var line_out, out var _, out var _);
		return line_out.LineNo - 1;
	}

	/// <summary>Retrieves the location within the control at the specified character index.</summary>
	/// <returns>The location of the specified character.</returns>
	/// <param name="index">The index of the character for which to retrieve the location. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override Point GetPositionFromCharIndex(int index)
	{
		document.CharIndexToLineTag(index, out var line_out, out var _, out var pos);
		return new Point(line_out.X + (int)line_out.widths[pos] + document.OffsetX - document.ViewPortX, line_out.Y + document.OffsetY - document.ViewPortY);
	}

	/// <summary>Loads the contents of an existing data stream into the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	/// <param name="data">A stream of data to load into the <see cref="T:System.Windows.Forms.RichTextBox" /> control. </param>
	/// <param name="fileType">One of the <see cref="T:System.Windows.Forms.RichTextBoxStreamType" /> values. </param>
	/// <exception cref="T:System.IO.IOException">An error occurred while loading the file into the control. </exception>
	/// <exception cref="T:System.ArgumentException">The file being loaded is not an RTF document. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void LoadFile(Stream data, RichTextBoxStreamType fileType)
	{
		document.Empty();
		if (fileType == RichTextBoxStreamType.PlainText)
		{
			StringBuilder stringBuilder;
			char[] array;
			try
			{
				stringBuilder = new StringBuilder((int)data.Length);
				array = new char[1024];
			}
			catch
			{
				throw new IOException("Not enough memory to load document");
			}
			StreamReader streamReader = new StreamReader(data, Encoding.Default, detectEncodingFromByteOrderMarks: true);
			for (int num = streamReader.Read(array, 0, array.Length); num > 0; num = streamReader.Read(array, 0, array.Length))
			{
				stringBuilder.Append(array, 0, num);
			}
			if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '\n')
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			base.Text = stringBuilder.ToString();
		}
		else
		{
			InsertRTFFromStream(data, 0, 1);
			document.PositionCaret(document.GetLine(1), 0);
			document.SetSelectionToCaret(start: true);
			ScrollToCaret();
		}
	}

	/// <summary>Loads a rich text format (RTF) or standard ASCII text file into the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	/// <param name="path">The name and location of the file to load into the control. </param>
	/// <exception cref="T:System.IO.IOException">An error occurred while loading the file into the control. </exception>
	/// <exception cref="T:System.ArgumentException">The file being loaded is not an RTF document. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void LoadFile(string path)
	{
		LoadFile(path, RichTextBoxStreamType.RichText);
	}

	/// <summary>Loads a specific type of file into the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	/// <param name="path">The name and location of the file to load into the control. </param>
	/// <param name="fileType">One of the <see cref="T:System.Windows.Forms.RichTextBoxStreamType" /> values. </param>
	/// <exception cref="T:System.IO.IOException">An error occurred while loading the file into the control. </exception>
	/// <exception cref="T:System.ArgumentException">The file being loaded is not an RTF document. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void LoadFile(string path, RichTextBoxStreamType fileType)
	{
		FileStream fileStream = null;
		try
		{
			fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 1024);
			LoadFile(fileStream, fileType);
		}
		catch (Exception innerException)
		{
			throw new IOException("Could not open file " + path, innerException);
		}
		finally
		{
			fileStream?.Close();
		}
	}

	/// <summary>Pastes the contents of the Clipboard in the specified Clipboard format.</summary>
	/// <param name="clipFormat">The Clipboard format in which the data should be obtained from the Clipboard. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Paste(DataFormats.Format clipFormat)
	{
		Paste(Clipboard.GetDataObject(), clipFormat, obey_length: false);
	}

	/// <summary>Reapplies the last operation that was undone in the control.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Redo()
	{
		if (document.undo.Redo())
		{
			OnTextChanged(EventArgs.Empty);
		}
	}

	/// <summary>Saves the contents of a <see cref="T:System.Windows.Forms.RichTextBox" /> control to an open data stream.</summary>
	/// <param name="data">The data stream that contains the file to save to. </param>
	/// <param name="fileType">One of the <see cref="T:System.Windows.Forms.RichTextBoxStreamType" /> values. </param>
	/// <exception cref="T:System.ArgumentException">An invalid file type is specified in the <paramref name="fileType" /> parameter. </exception>
	/// <exception cref="T:System.IO.IOException">An error occurs in saving the contents of the control to a file. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SaveFile(Stream data, RichTextBoxStreamType fileType)
	{
		Encoding encoding = ((fileType != RichTextBoxStreamType.UnicodePlainText) ? Encoding.ASCII : Encoding.Unicode);
		byte[] bytes;
		switch (fileType)
		{
		case RichTextBoxStreamType.PlainText:
		case RichTextBoxStreamType.TextTextOleObjs:
		case RichTextBoxStreamType.UnicodePlainText:
		{
			if (!Multiline)
			{
				bytes = encoding.GetBytes(document.Root.text.ToString());
				data.Write(bytes, 0, bytes.Length);
				return;
			}
			for (int i = 1; i < document.Lines; i++)
			{
				string s = document.GetLine(i).TextWithoutEnding() + Environment.NewLine;
				bytes = encoding.GetBytes(s);
				data.Write(bytes, 0, bytes.Length);
			}
			bytes = encoding.GetBytes(document.GetLine(document.Lines).text.ToString());
			data.Write(bytes, 0, bytes.Length);
			return;
		}
		}
		Line line = document.GetLine(1);
		Line line2 = document.GetLine(document.Lines);
		StringBuilder stringBuilder = GenerateRTF(line, 0, line2, line2.text.Length);
		int length = stringBuilder.Length;
		bytes = new byte[4096];
		for (int i = 0; i < length; i += 1024)
		{
			int bytes2;
			if (i + 1024 < length)
			{
				bytes2 = encoding.GetBytes(stringBuilder.ToString(i, 1024), 0, 1024, bytes, 0);
			}
			else
			{
				bytes2 = length - i;
				bytes2 = encoding.GetBytes(stringBuilder.ToString(i, bytes2), 0, bytes2, bytes, 0);
			}
			data.Write(bytes, 0, bytes2);
		}
	}

	/// <summary>Saves the contents of the <see cref="T:System.Windows.Forms.RichTextBox" /> to a rich text format (RTF) file.</summary>
	/// <param name="path">The name and location of the file to save. </param>
	/// <exception cref="T:System.IO.IOException">An error occurs in saving the contents of the control to a file. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SaveFile(string path)
	{
		if (path.EndsWith(".rtf"))
		{
			SaveFile(path, RichTextBoxStreamType.RichText);
		}
		else
		{
			SaveFile(path, RichTextBoxStreamType.PlainText);
		}
	}

	/// <summary>Saves the contents of the <see cref="T:System.Windows.Forms.RichTextBox" /> to a specific type of file.</summary>
	/// <param name="path">The name and location of the file to save. </param>
	/// <param name="fileType">One of the <see cref="T:System.Windows.Forms.RichTextBoxStreamType" /> values. </param>
	/// <exception cref="T:System.ArgumentException">An invalid file type is specified in the <paramref name="fileType" /> parameter. </exception>
	/// <exception cref="T:System.IO.IOException">An error occurs in saving the contents of the control to a file. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SaveFile(string path, RichTextBoxStreamType fileType)
	{
		FileStream fileStream = null;
		fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024, useAsync: false);
		SaveFile(fileStream, fileType);
		fileStream?.Close();
	}

	/// <summary>This method is not relevant for this class.</summary>
	/// <param name="bitmap">A <see cref="T:System.Drawing.Bitmap" />.</param>
	/// <param name="targetBounds">A <see cref="T:System.Drawing.Rectangle" />.</param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
	{
		Graphics g = Graphics.FromImage(bitmap);
		Draw(g, targetBounds);
	}

	/// <summary>Creates an IRichEditOleCallback-compatible object for handling rich-edit callback operations.</summary>
	/// <returns>An object that implements the IRichEditOleCallback interface.</returns>
	protected virtual object CreateRichEditOleCallback()
	{
		throw new NotImplementedException();
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnBackColorChanged(EventArgs e)
	{
		base.OnBackColorChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.ContentsResized" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.ContentsResizedEventArgs" /> that contains the event data. </param>
	protected virtual void OnContentsResized(ContentsResizedEventArgs e)
	{
		((ContentsResizedEventHandler)base.Events[ContentsResized])?.Invoke(this, e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnContextMenuChanged(EventArgs e)
	{
		base.OnContextMenuChanged(e);
	}

	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);
	}

	protected override void OnHandleDestroyed(EventArgs e)
	{
		base.OnHandleDestroyed(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.HScroll" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnHScroll(EventArgs e)
	{
		((EventHandler)base.Events[HScroll])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.ImeChange" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[System.MonoTODO("Stub, never called")]
	protected virtual void OnImeChange(EventArgs e)
	{
		((EventHandler)base.Events[ImeChange])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.LinkClicked" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.LinkClickedEventArgs" /> that contains the event data. </param>
	protected virtual void OnLinkClicked(LinkClickedEventArgs e)
	{
		((LinkClickedEventHandler)base.Events[LinkClicked])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.Protected" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnProtected(EventArgs e)
	{
		((EventHandler)base.Events[Protected])?.Invoke(this, e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnRightToLeftChanged(EventArgs e)
	{
		base.OnRightToLeftChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.SelectionChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnSelectionChanged(EventArgs e)
	{
		((EventHandler)base.Events[SelectionChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.VScroll" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnVScroll(EventArgs e)
	{
		((EventHandler)base.Events[VScroll])?.Invoke(this, e);
	}

	/// <param name="m">A Windows Message Object. </param>
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
	}

	/// <returns>true if the command key was processed by the control; otherwise, false.</returns>
	/// <param name="m"></param>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the shortcut key to process. </param>
	protected override bool ProcessCmdKey(ref Message m, Keys keyData)
	{
		return base.ProcessCmdKey(ref m, keyData);
	}

	internal override void SelectWord()
	{
		document.ExpandSelection(CaretSelection.Word, to_caret: false);
	}

	private void HandleGroup(System.Windows.Forms.RTF.RTF rtf)
	{
		if (rtf_section_stack == null)
		{
			rtf_section_stack = new Stack();
		}
		if (rtf.Major == Major.BeginGroup)
		{
			rtf_section_stack.Push(rtf_style.Clone());
			rtf_skip_count = 0;
		}
		else if (rtf.Major == Major.EndGroup && rtf_section_stack.Count > 0)
		{
			FlushText(rtf, newline: false);
			rtf_style = (RtfSectionStyle)rtf_section_stack.Pop();
		}
	}

	[System.MonoInternalNote("Add QuadJust support for justified alignment")]
	private void HandleControl(System.Windows.Forms.RTF.RTF rtf)
	{
		switch (rtf.Major)
		{
		case Major.Unicode:
			switch (rtf.Minor)
			{
			case Minor.UnicodeCharBytes:
				rtf_style.rtf_skip_width = rtf.Param;
				break;
			case Minor.UnicodeChar:
				FlushText(rtf, newline: false);
				rtf_skip_count += rtf_style.rtf_skip_width;
				rtf_line.Append((char)rtf.Param);
				break;
			}
			break;
		case Major.Destination:
			rtf.SkipGroup();
			break;
		case Major.PictAttr:
			if (rtf.Picture != null && rtf.Picture.IsValid())
			{
				Line line = document.GetLine(rtf_cursor_y);
				document.InsertPicture(line, 0, rtf.Picture);
				rtf_cursor_x++;
				FlushText(rtf, newline: true);
				rtf.Picture = null;
			}
			break;
		case Major.CharAttr:
			switch (rtf.Minor)
			{
			case Minor.ForeColor:
			{
				System.Windows.Forms.RTF.Color color = System.Windows.Forms.RTF.Color.GetColor(rtf, rtf.Param);
				if (color != null)
				{
					FlushText(rtf, newline: false);
					if (color.Red == -1 && color.Green == -1 && color.Blue == -1)
					{
						rtf_style.rtf_color = ForeColor;
					}
					else
					{
						rtf_style.rtf_color = System.Drawing.Color.FromArgb(color.Red, color.Green, color.Blue);
					}
					FlushText(rtf, newline: false);
				}
				break;
			}
			case Minor.FontSize:
				FlushText(rtf, newline: false);
				rtf_style.rtf_rtffont_size = rtf.Param / 2;
				break;
			case Minor.FontNum:
			{
				System.Windows.Forms.RTF.Font font = System.Windows.Forms.RTF.Font.GetFont(rtf, rtf.Param);
				if (font != null)
				{
					FlushText(rtf, newline: false);
					rtf_style.rtf_rtffont = font;
				}
				break;
			}
			case Minor.Plain:
				FlushText(rtf, newline: false);
				rtf_style.rtf_rtfstyle = FontStyle.Regular;
				break;
			case Minor.Bold:
				FlushText(rtf, newline: false);
				if (rtf.Param == -1000000)
				{
					rtf_style.rtf_rtfstyle |= FontStyle.Bold;
				}
				else
				{
					rtf_style.rtf_rtfstyle &= ~FontStyle.Bold;
				}
				break;
			case Minor.Italic:
				FlushText(rtf, newline: false);
				if (rtf.Param == -1000000)
				{
					rtf_style.rtf_rtfstyle |= FontStyle.Italic;
				}
				else
				{
					rtf_style.rtf_rtfstyle &= ~FontStyle.Italic;
				}
				break;
			case Minor.StrikeThru:
				FlushText(rtf, newline: false);
				if (rtf.Param == -1000000)
				{
					rtf_style.rtf_rtfstyle |= FontStyle.Strikeout;
				}
				else
				{
					rtf_style.rtf_rtfstyle &= ~FontStyle.Strikeout;
				}
				break;
			case Minor.Underline:
				FlushText(rtf, newline: false);
				if (rtf.Param == -1000000)
				{
					rtf_style.rtf_rtfstyle |= FontStyle.Underline;
				}
				else
				{
					rtf_style.rtf_rtfstyle &= ~FontStyle.Underline;
				}
				break;
			case Minor.Invisible:
				FlushText(rtf, newline: false);
				rtf_style.rtf_visible = false;
				break;
			case Minor.NoUnderline:
				FlushText(rtf, newline: false);
				rtf_style.rtf_rtfstyle &= ~FontStyle.Underline;
				break;
			}
			break;
		case Major.ParAttr:
			switch (rtf.Minor)
			{
			case Minor.ParDef:
				FlushText(rtf, newline: false);
				rtf_style.rtf_par_line_left_indent = 0;
				rtf_style.rtf_rtfalign = HorizontalAlignment.Left;
				break;
			case Minor.LeftIndent:
				rtf_style.rtf_par_line_left_indent = (int)((float)rtf.Param / 1440f * CreateGraphics().DpiX + 0.5f);
				break;
			case Minor.QuadCenter:
				FlushText(rtf, newline: false);
				rtf_style.rtf_rtfalign = HorizontalAlignment.Center;
				break;
			case Minor.QuadJust:
				FlushText(rtf, newline: false);
				rtf_style.rtf_rtfalign = HorizontalAlignment.Center;
				break;
			case Minor.QuadLeft:
				FlushText(rtf, newline: false);
				rtf_style.rtf_rtfalign = HorizontalAlignment.Left;
				break;
			case Minor.QuadRight:
				FlushText(rtf, newline: false);
				rtf_style.rtf_rtfalign = HorizontalAlignment.Right;
				break;
			}
			break;
		case Major.SpecialChar:
			SpecialChar(rtf);
			break;
		}
	}

	private void SpecialChar(System.Windows.Forms.RTF.RTF rtf)
	{
		switch (rtf.Minor)
		{
		case Minor.Row:
		case Minor.Par:
		case Minor.Sect:
		case Minor.Page:
		case Minor.Line:
			FlushText(rtf, newline: true);
			break;
		case Minor.Cell:
			Console.Write(" ");
			break;
		case Minor.NoBrkSpace:
			Console.Write(" ");
			break;
		case Minor.Tab:
			rtf_line.Append("\t");
			break;
		case Minor.NoReqHyphen:
		case Minor.NoBrkHyphen:
			rtf_line.Append("-");
			break;
		case Minor.Bullet:
			Console.WriteLine("*");
			break;
		case Minor.EmDash:
			rtf_line.Append("—");
			break;
		case Minor.EnDash:
			rtf_line.Append("–");
			break;
		}
	}

	private void HandleText(System.Windows.Forms.RTF.RTF rtf)
	{
		string text = rtf.EncodedText;
		if (rtf_skip_count > 0 && text.Length > 0)
		{
			int num = Math.Min(rtf_skip_count, text.Length);
			text = text.Substring(num);
			rtf_skip_count -= num;
		}
		if (rtf_style.rtf_visible)
		{
			rtf_line.Append(text);
		}
	}

	private void FlushText(System.Windows.Forms.RTF.RTF rtf, bool newline)
	{
		int length = rtf_line.Length;
		if (!newline && length == 0)
		{
			return;
		}
		if (rtf_style.rtf_rtffont == null)
		{
			rtf_style.rtf_rtffont = System.Windows.Forms.RTF.Font.GetFont(rtf, 0);
		}
		System.Drawing.Font font = new System.Drawing.Font(rtf_style.rtf_rtffont.Name, rtf_style.rtf_rtffont_size, rtf_style.rtf_rtfstyle);
		if (rtf_style.rtf_color == System.Drawing.Color.Empty)
		{
			System.Windows.Forms.RTF.Color color = System.Windows.Forms.RTF.Color.GetColor(rtf, 0);
			if (color == null || (color.Red == -1 && color.Green == -1 && color.Blue == -1))
			{
				rtf_style.rtf_color = ForeColor;
			}
			else
			{
				rtf_style.rtf_color = System.Drawing.Color.FromArgb(color.Red, color.Green, color.Blue);
			}
		}
		rtf_chars += rtf_line.Length;
		if (rtf_cursor_x == 0 && !reuse_line)
		{
			if (newline && !rtf_line.ToString().EndsWith(Environment.NewLine))
			{
				rtf_line.Append(Environment.NewLine);
			}
			document.Add(rtf_cursor_y, rtf_line.ToString(), rtf_style.rtf_rtfalign, font, rtf_style.rtf_color, (!newline) ? LineEnding.Wrap : LineEnding.Rich);
			if (rtf_style.rtf_par_line_left_indent != 0)
			{
				Line line = document.GetLine(rtf_cursor_y);
				line.indent = rtf_style.rtf_par_line_left_indent;
			}
		}
		else
		{
			Line line2 = document.GetLine(rtf_cursor_y);
			line2.indent = rtf_style.rtf_par_line_left_indent;
			if (rtf_line.Length > 0)
			{
				document.InsertString(line2, rtf_cursor_x, rtf_line.ToString());
				document.FormatText(line2, rtf_cursor_x + 1, line2, rtf_cursor_x + 1 + length, font, rtf_style.rtf_color, System.Drawing.Color.Empty, FormatSpecified.Font | FormatSpecified.Color);
			}
			if (newline)
			{
				line2 = document.GetLine(rtf_cursor_y);
				line2.ending = LineEnding.Rich;
				if (!line2.Text.EndsWith(Environment.NewLine))
				{
					line2.Text += Environment.NewLine;
				}
			}
			reuse_line = false;
		}
		if (newline)
		{
			rtf_cursor_x = 0;
			rtf_cursor_y++;
		}
		else
		{
			rtf_cursor_x += length;
		}
		rtf_line.Length = 0;
	}

	private void InsertRTFFromStream(Stream data, int cursor_x, int cursor_y)
	{
		InsertRTFFromStream(data, cursor_x, cursor_y, out var _, out var _, out var _);
	}

	private void InsertRTFFromStream(Stream data, int cursor_x, int cursor_y, out int to_x, out int to_y, out int chars)
	{
		System.Windows.Forms.RTF.RTF rTF = new System.Windows.Forms.RTF.RTF(data);
		rTF.ClassCallback[TokenClass.Text] = HandleText;
		rTF.ClassCallback[TokenClass.Control] = HandleControl;
		rTF.ClassCallback[TokenClass.Group] = HandleGroup;
		rtf_skip_count = 0;
		rtf_line = new StringBuilder();
		rtf_style.rtf_color = System.Drawing.Color.Empty;
		rtf_style.rtf_rtffont_size = (int)Font.Size;
		rtf_style.rtf_rtfalign = HorizontalAlignment.Left;
		rtf_style.rtf_rtfstyle = FontStyle.Regular;
		rtf_style.rtf_rtffont = null;
		rtf_style.rtf_visible = true;
		rtf_style.rtf_skip_width = 1;
		rtf_cursor_x = cursor_x;
		rtf_cursor_y = cursor_y;
		rtf_chars = 0;
		rTF.DefaultFont(Font.Name);
		rtf_text_map = new TextMap();
		TextMap.SetupStandardTable(rtf_text_map.Table);
		document.SuspendRecalc();
		try
		{
			rTF.Read();
			FlushText(rTF, newline: false);
		}
		catch (RTFException ex)
		{
			Console.WriteLine("RTF Parsing failure: {0}", ex.Message);
		}
		to_x = rtf_cursor_x;
		to_y = rtf_cursor_y;
		chars = rtf_chars;
		if (rtf_section_stack != null)
		{
			rtf_section_stack.Clear();
		}
		document.RecalculateDocument(CreateGraphicsInternal(), cursor_y, document.Lines, optimize: false);
		document.ResumeRecalc(immediate_update: true);
		document.Invalidate(document.GetLine(cursor_y), 0, document.GetLine(document.Lines), -1);
	}

	private void RichTextBox_HScrolled(object sender, EventArgs e)
	{
		OnHScroll(e);
	}

	private void RichTextBox_VScrolled(object sender, EventArgs e)
	{
		OnVScroll(e);
	}

	private void PointToTagPos(Point pt, out LineTag tag, out int pos)
	{
		Point point = pt;
		if (point.X >= document.ViewPortWidth)
		{
			point.X = document.ViewPortWidth - 1;
		}
		else if (point.X < 0)
		{
			point.X = 0;
		}
		if (point.Y >= document.ViewPortHeight)
		{
			point.Y = document.ViewPortHeight - 1;
		}
		else if (point.Y < 0)
		{
			point.Y = 0;
		}
		tag = document.FindCursor(point.X + document.ViewPortX, point.Y + document.ViewPortY, out pos);
	}

	private void EmitRTFFontProperties(StringBuilder rtf, int prev_index, int font_index, System.Drawing.Font prev_font, System.Drawing.Font font)
	{
		if (prev_index != font_index)
		{
			rtf.Append($"\\f{font_index}");
		}
		if (prev_font == null || prev_font.Size != font.Size)
		{
			rtf.Append($"\\fs{(int)(font.Size * 2f)}");
		}
		if (prev_font == null || font.Bold != prev_font.Bold)
		{
			if (font.Bold)
			{
				rtf.Append("\\b");
			}
			else if (prev_font != null)
			{
				rtf.Append("\\b0");
			}
		}
		if (prev_font == null || font.Italic != prev_font.Italic)
		{
			if (font.Italic)
			{
				rtf.Append("\\i");
			}
			else if (prev_font != null)
			{
				rtf.Append("\\i0");
			}
		}
		if (prev_font == null || font.Strikeout != prev_font.Strikeout)
		{
			if (font.Strikeout)
			{
				rtf.Append("\\strike");
			}
			else if (prev_font != null)
			{
				rtf.Append("\\strike0");
			}
		}
		if (prev_font == null || font.Underline != prev_font.Underline)
		{
			if (font.Underline)
			{
				rtf.Append("\\ul");
			}
			else if (prev_font != null)
			{
				rtf.Append("\\ul0");
			}
		}
	}

	[System.MonoInternalNote("Emit unicode and other special characters properly")]
	private void EmitRTFText(StringBuilder rtf, string text)
	{
		int length = rtf.Length;
		int length2 = text.Length;
		rtf.Append(text);
		if (text.IndexOfAny(ReservedRTFChars) > -1)
		{
			rtf.Replace("\\", "\\\\", length, length2);
			rtf.Replace("{", "\\{", length, length2);
			rtf.Replace("}", "\\}", length, length2);
		}
	}

	private StringBuilder GenerateRTF(Line start_line, int start_pos, Line end_line, int end_pos)
	{
		StringBuilder stringBuilder = new StringBuilder();
		ArrayList arrayList = new ArrayList(10);
		ArrayList arrayList2 = new ArrayList(10);
		Line line = start_line;
		int i = start_line.line_no;
		int num = start_pos;
		LineTag lineTag = LineTag.FindTag(start_line, num);
		System.Drawing.Font font = lineTag.Font;
		System.Drawing.Color color = lineTag.Color;
		arrayList.Add(font.Name);
		arrayList2.Add(color);
		for (; i <= end_line.line_no; i++)
		{
			line = document.GetLine(i);
			lineTag = LineTag.FindTag(line, num);
			int num2 = ((i == end_line.line_no) ? end_pos : line.text.Length);
			while (num < num2)
			{
				if (lineTag.Font.Name != font.Name)
				{
					font = lineTag.Font;
					if (!arrayList.Contains(font.Name))
					{
						arrayList.Add(font.Name);
					}
				}
				if (lineTag.Color != color)
				{
					color = lineTag.Color;
					if (!arrayList2.Contains(color))
					{
						arrayList2.Add(color);
					}
				}
				num = lineTag.Start + lineTag.Length - 1;
				lineTag = lineTag.Next;
			}
			num = 0;
		}
		stringBuilder.Append("{\\rtf1\\ansi");
		stringBuilder.Append("\\ansicpg1252");
		stringBuilder.Append($"\\deff{arrayList.IndexOf(Font.Name)}");
		stringBuilder.Append("\\deflang1033" + Environment.NewLine);
		stringBuilder.Append("{\\fonttbl");
		for (int j = 0; j < arrayList.Count; j++)
		{
			stringBuilder.Append($"{{\\f{j}");
			stringBuilder.Append("\\fnil");
			stringBuilder.Append("\\fcharset0 ");
			stringBuilder.Append((string)arrayList[j]);
			stringBuilder.Append(";}");
		}
		stringBuilder.Append("}");
		stringBuilder.Append(Environment.NewLine);
		if (arrayList2.Count > 1 || ((System.Drawing.Color)arrayList2[0]).R != ForeColor.R || ((System.Drawing.Color)arrayList2[0]).G != ForeColor.G || ((System.Drawing.Color)arrayList2[0]).B != ForeColor.B)
		{
			stringBuilder.Append("{\\colortbl ");
			for (int j = 0; j < arrayList2.Count; j++)
			{
				stringBuilder.Append($"\\red{((System.Drawing.Color)arrayList2[j]).R}");
				stringBuilder.Append($"\\green{((System.Drawing.Color)arrayList2[j]).G}");
				stringBuilder.Append($"\\blue{((System.Drawing.Color)arrayList2[j]).B}");
				stringBuilder.Append(";");
			}
			stringBuilder.Append("}");
			stringBuilder.Append(Environment.NewLine);
		}
		stringBuilder.Append("{\\*\\generator Mono RichTextBox;}");
		lineTag = LineTag.FindTag(start_line, start_pos);
		stringBuilder.Append("\\pard");
		EmitRTFFontProperties(stringBuilder, -1, arrayList.IndexOf(lineTag.Font.Name), null, lineTag.Font);
		stringBuilder.Append(" ");
		font = lineTag.Font;
		color = (System.Drawing.Color)arrayList2[0];
		line = start_line;
		i = start_line.line_no;
		num = start_pos;
		for (; i <= end_line.line_no; i++)
		{
			line = document.GetLine(i);
			lineTag = LineTag.FindTag(line, num);
			int num2 = ((i == end_line.line_no) ? end_pos : line.text.Length);
			while (num < num2)
			{
				int length = stringBuilder.Length;
				if (lineTag.Font != font)
				{
					EmitRTFFontProperties(stringBuilder, arrayList.IndexOf(font.Name), arrayList.IndexOf(lineTag.Font.Name), font, lineTag.Font);
					font = lineTag.Font;
				}
				if (lineTag.Color != color)
				{
					color = lineTag.Color;
					stringBuilder.Append($"\\cf{arrayList2.IndexOf(color)}");
				}
				if (length != stringBuilder.Length)
				{
					stringBuilder.Append(" ");
				}
				if (i != end_line.line_no)
				{
					EmitRTFText(stringBuilder, lineTag.Line.text.ToString(num, lineTag.Start + lineTag.Length - num - 1));
				}
				else if (end_pos < lineTag.Start + lineTag.Length - 1)
				{
					EmitRTFText(stringBuilder, lineTag.Line.text.ToString(num, end_pos - num));
				}
				else
				{
					EmitRTFText(stringBuilder, lineTag.Line.text.ToString(num, lineTag.Start + lineTag.Length - num - 1));
				}
				num = lineTag.Start + lineTag.Length - 1;
				lineTag = lineTag.Next;
			}
			if (num >= line.text.Length && line.ending != LineEnding.Wrap)
			{
				stringBuilder.Append("\\par");
				stringBuilder.Append(Environment.NewLine);
			}
			num = 0;
		}
		stringBuilder.Append("}");
		stringBuilder.Append(Environment.NewLine);
		return stringBuilder;
	}
}
