using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Net.Mail;
using System.Web.Security;

namespace System.Web.UI.WebControls;

/// <summary>Provides a user interface that enable users to change their Web site password.</summary>
[Bindable(true)]
[DefaultEvent("ChangedPassword")]
[Designer("System.Web.UI.Design.WebControls.ChangePasswordDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
public class ChangePassword : CompositeControl, INamingContainer, IRenderOuterTable
{
	private class BaseChangePasswordContainer : Control, INamingContainer, INonBindingContainer
	{
		protected readonly ChangePassword _owner;

		private bool renderOuterTable;

		private Table _table;

		private TableCell _containerCell;

		public BaseChangePasswordContainer(ChangePassword owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			_owner = owner;
			renderOuterTable = _owner.RenderOuterTable;
			if (renderOuterTable)
			{
				InitTable();
			}
		}

		public void InstantiateTemplate(ITemplate template)
		{
			if (!_owner.RenderOuterTable)
			{
				template.InstantiateIn(this);
			}
			else
			{
				template.InstantiateIn(_containerCell);
			}
		}

		private void InitTable()
		{
			_table = new Table();
			if (!string.IsNullOrEmpty(_owner.ID))
			{
				_table.Attributes.Add("id", _owner.ID);
			}
			_table.CellSpacing = 0;
			_table.CellPadding = _owner.BorderPadding;
			_containerCell = new TableCell();
			TableRow tableRow = new TableRow();
			tableRow.Cells.Add(_containerCell);
			_table.Rows.Add(tableRow);
			Controls.AddAt(0, _table);
		}

		protected internal override void OnPreRender(EventArgs e)
		{
			if (_table != null)
			{
				_table.ApplyStyle(_owner.ControlStyle);
			}
			base.OnPreRender(e);
		}

		protected override void EnsureChildControls()
		{
			base.EnsureChildControls();
			if (_owner != null)
			{
				_owner.EnsureChildControls();
			}
		}
	}

	private sealed class ChangePasswordContainer : BaseChangePasswordContainer
	{
		public IEditableTextControl UserNameTextBox => (FindControl("UserName") ?? throw new HttpException("ChangePasswordTemplate does not contain an IEditableTextControl with ID UserName for the username, this is required if DisplayUserName=true.")) as IEditableTextControl;

		public IEditableTextControl CurrentPasswordTextBox => (FindControl("CurrentPassword") ?? throw new HttpException("ChangePasswordTemplate does not contain an IEditableTextControl with ID CurrentPassword for the current password.")) as IEditableTextControl;

		public IEditableTextControl NewPasswordTextBox => (FindControl("NewPassword") ?? throw new HttpException("ChangePasswordTemplate does not contain an IEditableTextControl with ID NewPassword for the new password.")) as IEditableTextControl;

		public IEditableTextControl ConfirmNewPasswordTextBox => FindControl("ConfirmNewPassword") as IEditableTextControl;

		public Control CancelButton => FindControl("Cancel");

		public Control ChangePasswordButton => FindControl("ChangePassword");

		public ITextControl FailureTextLiteral => FindControl("FailureText") as ITextControl;

		public ChangePasswordContainer(ChangePassword owner)
			: base(owner)
		{
			ID = "ChangePasswordContainerID";
		}
	}

	private sealed class ChangePasswordDeafultTemplate : ITemplate
	{
		private readonly ChangePassword _owner;

		internal ChangePasswordDeafultTemplate(ChangePassword cPassword)
		{
			_owner = cPassword;
		}

		private TableRow CreateRow(Control c0, Control c1, Control c2, Style s0, Style s1)
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			TableCell tableCell2 = new TableCell();
			tableCell.Controls.Add(c0);
			tableRow.Controls.Add(tableCell);
			if (c1 != null && c2 != null)
			{
				tableCell2.Controls.Add(c1);
				tableCell2.Controls.Add(c2);
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				if (s0 != null)
				{
					tableCell.ApplyStyle(s0);
				}
				if (s1 != null)
				{
					tableCell2.ApplyStyle(s1);
				}
				tableRow.Controls.Add(tableCell2);
			}
			else
			{
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				if (s0 != null)
				{
					tableCell.ApplyStyle(s0);
				}
			}
			return tableRow;
		}

		private bool AddLink(string pageUrl, string linkText, string linkIcon, WebControl container)
		{
			bool result = false;
			if (linkIcon.Length > 0)
			{
				Image image = new Image();
				image.ImageUrl = linkIcon;
				container.Controls.Add(image);
				result = true;
			}
			if (linkText.Length > 0)
			{
				HyperLink hyperLink = new HyperLink();
				hyperLink.NavigateUrl = pageUrl;
				hyperLink.Text = linkText;
				hyperLink.ControlStyle.CopyTextStylesFrom(container.ControlStyle);
				container.Controls.Add(hyperLink);
				result = true;
			}
			return result;
		}

		public void InstantiateIn(Control container)
		{
			Table table = new Table();
			table.CellPadding = 0;
			Style controlStyle = _owner.ControlStyle;
			Style controlStyle2 = table.ControlStyle;
			FontInfo font = controlStyle.Font;
			controlStyle2.Font.CopyFrom(font);
			font.ClearDefaults();
			Color foreColor = controlStyle.ForeColor;
			if (foreColor != Color.Empty)
			{
				controlStyle2.ForeColor = foreColor;
				controlStyle.RemoveBit(4);
			}
			table.Controls.Add(CreateRow(new LiteralControl(_owner.ChangePasswordTitleText), null, null, _owner.TitleTextStyle, null));
			if (_owner.InstructionText.Length > 0)
			{
				table.Controls.Add(CreateRow(new LiteralControl(_owner.InstructionText), null, null, _owner.InstructionTextStyle, null));
			}
			if (_owner.DisplayUserName)
			{
				TextBox textBox = new TextBox();
				textBox.ID = "UserName";
				textBox.Text = _owner.UserName;
				textBox.ApplyStyle(_owner.TextBoxStyle);
				Label label = new Label();
				label.ID = "UserNameLabel";
				label.AssociatedControlID = "UserName";
				label.Text = _owner.UserNameLabelText;
				RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
				requiredFieldValidator.ID = "UserNameRequired";
				requiredFieldValidator.ControlToValidate = "UserName";
				requiredFieldValidator.ErrorMessage = _owner.UserNameRequiredErrorMessage;
				requiredFieldValidator.ToolTip = _owner.UserNameRequiredErrorMessage;
				requiredFieldValidator.Text = "*";
				requiredFieldValidator.ValidationGroup = _owner.ID;
				requiredFieldValidator.ApplyStyle(_owner.ValidatorTextStyle);
				table.Controls.Add(CreateRow(label, textBox, requiredFieldValidator, _owner.LabelStyle, null));
			}
			TextBox textBox2 = new TextBox();
			textBox2.ID = "CurrentPassword";
			textBox2.TextMode = TextBoxMode.Password;
			textBox2.ApplyStyle(_owner.TextBoxStyle);
			Label label2 = new Label();
			label2.ID = "CurrentPasswordLabel";
			label2.AssociatedControlID = "CurrentPasswordLabel";
			label2.Text = _owner.PasswordLabelText;
			RequiredFieldValidator requiredFieldValidator2 = new RequiredFieldValidator();
			requiredFieldValidator2.ID = "CurrentPasswordRequired";
			requiredFieldValidator2.ControlToValidate = "CurrentPassword";
			requiredFieldValidator2.ErrorMessage = _owner.PasswordRequiredErrorMessage;
			requiredFieldValidator2.ToolTip = _owner.PasswordRequiredErrorMessage;
			requiredFieldValidator2.Text = "*";
			requiredFieldValidator2.ValidationGroup = _owner.ID;
			requiredFieldValidator2.ApplyStyle(_owner.ValidatorTextStyle);
			table.Controls.Add(CreateRow(label2, textBox2, requiredFieldValidator2, _owner.LabelStyle, null));
			TextBox textBox3 = new TextBox();
			textBox3.ID = "NewPassword";
			textBox3.TextMode = TextBoxMode.Password;
			textBox3.ApplyStyle(_owner.TextBoxStyle);
			Label label3 = new Label();
			label3.ID = "NewPasswordLabel";
			label3.AssociatedControlID = "NewPassword";
			label3.Text = _owner.NewPasswordLabelText;
			RequiredFieldValidator requiredFieldValidator3 = new RequiredFieldValidator();
			requiredFieldValidator3.ID = "NewPasswordRequired";
			requiredFieldValidator3.ControlToValidate = "NewPassword";
			requiredFieldValidator3.ErrorMessage = _owner.PasswordRequiredErrorMessage;
			requiredFieldValidator3.ToolTip = _owner.PasswordRequiredErrorMessage;
			requiredFieldValidator3.Text = "*";
			requiredFieldValidator3.ValidationGroup = _owner.ID;
			requiredFieldValidator3.ApplyStyle(_owner.ValidatorTextStyle);
			table.Controls.Add(CreateRow(label3, textBox3, requiredFieldValidator3, _owner.LabelStyle, null));
			if (_owner.PasswordHintText.Length > 0)
			{
				table.Controls.Add(CreateRow(new LiteralControl(string.Empty), new LiteralControl(_owner.PasswordHintText), new LiteralControl(string.Empty), null, _owner.PasswordHintStyle));
			}
			TextBox textBox4 = new TextBox();
			textBox4.ID = "ConfirmNewPassword";
			textBox4.TextMode = TextBoxMode.Password;
			textBox4.ApplyStyle(_owner.TextBoxStyle);
			Label label4 = new Label();
			label4.ID = "ConfirmNewPasswordLabel";
			label4.AssociatedControlID = "ConfirmNewPasswordLabel";
			label4.Text = _owner.ConfirmNewPasswordLabelText;
			RequiredFieldValidator requiredFieldValidator4 = new RequiredFieldValidator();
			requiredFieldValidator4.ID = "ConfirmNewPasswordRequired";
			requiredFieldValidator4.ControlToValidate = "ConfirmNewPassword";
			requiredFieldValidator4.ErrorMessage = _owner.PasswordRequiredErrorMessage;
			requiredFieldValidator4.ToolTip = _owner.PasswordRequiredErrorMessage;
			requiredFieldValidator4.Text = "*";
			requiredFieldValidator4.ValidationGroup = _owner.ID;
			requiredFieldValidator4.ApplyStyle(_owner.ValidatorTextStyle);
			table.Controls.Add(CreateRow(label4, textBox4, requiredFieldValidator4, _owner.LabelStyle, null));
			CompareValidator compareValidator = new CompareValidator();
			compareValidator.ID = "NewPasswordCompare";
			compareValidator.ControlToCompare = "NewPassword";
			compareValidator.ControlToValidate = "ConfirmNewPassword";
			compareValidator.Display = ValidatorDisplay.Dynamic;
			compareValidator.ErrorMessage = _owner.ConfirmPasswordCompareErrorMessage;
			compareValidator.ValidationGroup = _owner.ID;
			table.Controls.Add(CreateRow(compareValidator, null, null, null, null));
			Literal literal = new Literal();
			literal.ID = "FailureText";
			literal.EnableViewState = false;
			if (_owner.FailureTextStyle.ForeColor.IsEmpty)
			{
				_owner.FailureTextStyle.ForeColor = Color.Red;
			}
			table.Controls.Add(CreateRow(literal, null, null, _owner.FailureTextStyle, null));
			WebControl webControl = null;
			switch (_owner.ChangePasswordButtonType)
			{
			case ButtonType.Button:
				webControl = new Button();
				break;
			case ButtonType.Image:
				webControl = new ImageButton();
				break;
			case ButtonType.Link:
				webControl = new LinkButton();
				break;
			}
			webControl.ID = "ChangePasswordPushButton";
			webControl.ApplyStyle(_owner.ChangePasswordButtonStyle);
			((IButtonControl)webControl).CommandName = ChangePasswordButtonCommandName;
			((IButtonControl)webControl).Text = _owner.ChangePasswordButtonText;
			((IButtonControl)webControl).ValidationGroup = _owner.ID;
			WebControl webControl2 = null;
			switch (_owner.CancelButtonType)
			{
			case ButtonType.Button:
				webControl2 = new Button();
				break;
			case ButtonType.Image:
				webControl2 = new ImageButton();
				break;
			case ButtonType.Link:
				webControl2 = new LinkButton();
				break;
			}
			webControl2.ID = "CancelPushButton";
			webControl2.ApplyStyle(_owner.CancelButtonStyle);
			((IButtonControl)webControl2).CommandName = CancelButtonCommandName;
			((IButtonControl)webControl2).Text = _owner.CancelButtonText;
			((IButtonControl)webControl2).CausesValidation = false;
			table.Controls.Add(CreateRow(webControl, webControl2, new LiteralControl(string.Empty), null, null));
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.ColumnSpan = 2;
			tableCell.ControlStyle.CopyFrom(_owner.HyperLinkStyle);
			tableRow.Cells.Add(tableCell);
			if (AddLink(_owner.HelpPageUrl, _owner.HelpPageText, _owner.HelpPageIconUrl, tableCell))
			{
				tableCell.Controls.Add(new LiteralControl("<br/>"));
			}
			if (AddLink(_owner.CreateUserUrl, _owner.CreateUserText, _owner.CreateUserIconUrl, tableCell))
			{
				tableCell.Controls.Add(new LiteralControl("<br/>"));
			}
			if (AddLink(_owner.PasswordRecoveryUrl, _owner.PasswordRecoveryText, _owner.PasswordRecoveryIconUrl, tableCell))
			{
				tableCell.Controls.Add(new LiteralControl("<br/>"));
			}
			AddLink(_owner.EditProfileUrl, _owner.EditProfileText, _owner.EditProfileIconUrl, tableCell);
			table.Controls.Add(tableRow);
			container.Controls.Add(table);
		}
	}

	private sealed class SuccessDefaultTemplate : ITemplate
	{
		private readonly ChangePassword _cPassword;

		internal SuccessDefaultTemplate(ChangePassword cPassword)
		{
			_cPassword = cPassword;
		}

		private TableRow CreateRow(Control c0, Style s0, HorizontalAlign align)
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell
			{
				Controls = { c0 },
				HorizontalAlign = align
			};
			if (s0 != null)
			{
				tableCell.ApplyStyle(s0);
			}
			tableRow.Controls.Add(tableCell);
			return tableRow;
		}

		public void InstantiateIn(Control container)
		{
			Table table = new Table();
			table.ControlStyle.Width = Unit.Percentage(100.0);
			table.ControlStyle.Height = Unit.Percentage(100.0);
			table.Controls.Add(CreateRow(new LiteralControl(_cPassword.SuccessTitleText), _cPassword.TitleTextStyle, HorizontalAlign.Center));
			table.Controls.Add(CreateRow(new LiteralControl(_cPassword.SuccessText), _cPassword.SuccessTextStyle, HorizontalAlign.Center));
			WebControl webControl = null;
			switch (_cPassword.ChangePasswordButtonType)
			{
			case ButtonType.Button:
				webControl = new Button();
				break;
			case ButtonType.Image:
				webControl = new ImageButton();
				break;
			case ButtonType.Link:
				webControl = new LinkButton();
				break;
			}
			webControl.ID = "ContinuePushButton";
			webControl.ApplyStyle(_cPassword.ContinueButtonStyle);
			((IButtonControl)webControl).CommandName = ContinueButtonCommandName;
			((IButtonControl)webControl).Text = _cPassword.ContinueButtonText;
			((IButtonControl)webControl).CausesValidation = false;
			table.Controls.Add(CreateRow(webControl, null, HorizontalAlign.Right));
			container.Controls.Add(table);
		}
	}

	private sealed class SuccessContainer : BaseChangePasswordContainer
	{
		public Control ChangePasswordButton => FindControl("Continue");

		public SuccessContainer(ChangePassword owner)
			: base(owner)
		{
			ID = "SuccessContainerID";
		}
	}

	private static readonly object cancelButtonClickEvent = new object();

	private static readonly object changedPasswordEvent = new object();

	private static readonly object changePasswordErrorEvent = new object();

	private static readonly object changingPasswordEvent = new object();

	private static readonly object continueButtonClickEvent = new object();

	private static readonly object sendingMailEvent = new object();

	private static readonly object sendMailErrorEvent = new object();

	/// <summary>Represents the <see langword="CommandName" /> value of the Cancel button. This field is read-only.</summary>
	public static readonly string CancelButtonCommandName = "Cancel";

	/// <summary>Represents the <see langword="CommandName" /> value of the Change Password button. This field is read-only.</summary>
	public static readonly string ChangePasswordButtonCommandName = "ChangePassword";

	/// <summary>Represents <see langword="CommandName" /> value of the Continue button. This field is read-only.</summary>
	public static readonly string ContinueButtonCommandName = "Continue";

	private bool renderOuterTable = true;

	private Style _cancelButtonStyle;

	private Style _changePasswordButtonStyle;

	private Style _continueButtonStyle;

	private TableItemStyle _failureTextStyle;

	private TableItemStyle _hyperLinkStyle;

	private TableItemStyle _instructionTextStyle;

	private TableItemStyle _labelStyle;

	private TableItemStyle _passwordHintStyle;

	private TableItemStyle _successTextStyle;

	private Style _textBoxStyle;

	private TableItemStyle _titleTextStyle;

	private Style _validatorTextStyle;

	private MailDefinition _mailDefinition;

	private MembershipProvider _provider;

	private ITemplate _changePasswordTemplate;

	private ITemplate _successTemplate;

	private Control _changePasswordTemplateContainer;

	private Control _successTemplateContainer;

	private string _username;

	private string _currentPassword;

	private string _newPassword;

	private string _newPasswordConfirm;

	private bool _showContinue;

	private EventHandlerList events = new EventHandlerList();

	/// <summary>Gets or sets the amount of padding, in pixels, inside the border and the designated area for the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>The number of pixels of space between the contents of a <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control and the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control's border. The default value is 1.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Web.UI.WebControls.ChangePassword.BorderPadding" /> property is less than -1.</exception>
	[DefaultValue(1)]
	public virtual int BorderPadding
	{
		get
		{
			return ViewState.GetInt("BorderPadding", 1);
		}
		set
		{
			if (value < -1)
			{
				throw new ArgumentOutOfRangeException();
			}
			ViewState["BorderPadding"] = value;
		}
	}

	/// <summary>Gets or sets the URL of an image to display with the Cancel button, if the Cancel button is configured by the <see cref="P:System.Web.UI.WebControls.ChangePassword.CancelButtonType" /> property to be an image button.</summary>
	/// <returns>The URL of the image to display with the Cancel button. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string CancelButtonImageUrl
	{
		get
		{
			return ViewState.GetString("CancelButtonImageUrl", string.Empty);
		}
		set
		{
			ViewState["CancelButtonImageUrl"] = value;
		}
	}

	/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of the Cancel button on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the appearance of the Cancel button. The default is <see langword="null" />.</returns>
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	public Style CancelButtonStyle
	{
		get
		{
			if (_cancelButtonStyle == null)
			{
				_cancelButtonStyle = new Style();
				if (base.IsTrackingViewState)
				{
					_cancelButtonStyle.TrackViewState();
				}
			}
			return _cancelButtonStyle;
		}
	}

	/// <summary>Gets or sets the text displayed on the Cancel button.</summary>
	/// <returns>The text to display on the Cancel button. The default is "Cancel".</returns>
	[Localizable(true)]
	public virtual string CancelButtonText
	{
		get
		{
			return ViewState.GetString("CancelButtonText", "Cancel");
		}
		set
		{
			ViewState["CancelButtonText"] = value;
		}
	}

	/// <summary>Gets or sets the type of button to use for the Cancel button when rendering the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ButtonType" /> object that defines the type of button to render for the Cancel button. The property value can be one of the three <see cref="T:System.Web.UI.WebControls.ButtonType" /> enumeration values: <see langword="Button" />, <see langword="Image" />, or <see langword="Link" />. The default is <see langword="Button" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <see cref="T:System.Web.UI.WebControls.ButtonType" /> is not one of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values.</exception>
	[DefaultValue(ButtonType.Button)]
	public virtual ButtonType CancelButtonType
	{
		get
		{
			if (ViewState["CancelButtonType"] != null)
			{
				return (ButtonType)ViewState["CancelButtonType"];
			}
			return ButtonType.Button;
		}
		set
		{
			ViewState["CancelButtonType"] = value;
		}
	}

	/// <summary>Gets or sets the URL of the page that the user is shown after clicking the Cancel button in the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>The URL of the page the user is redirected to after clicking the Cancel button. The default is <see cref="F:System.String.Empty" />.</returns>
	[Themeable(false)]
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string CancelDestinationPageUrl
	{
		get
		{
			return ViewState.GetString("CancelDestinationPageUrl", string.Empty);
		}
		set
		{
			ViewState["CancelDestinationPageUrl"] = value;
		}
	}

	/// <summary>Gets or sets the URL of an image displayed next to the Change Password button on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control if the Change Password button is configured by the <see cref="P:System.Web.UI.WebControls.ChangePassword.ChangePasswordButtonType" /> property to be an image button.</summary>
	/// <returns>The URL of the image to display next to the Change Password button. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string ChangePasswordButtonImageUrl
	{
		get
		{
			return ViewState.GetString("ChangePasswordButtonImageUrl", string.Empty);
		}
		set
		{
			ViewState["ChangePasswordButtonImageUrl"] = value;
		}
	}

	/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of the Change Password button on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the appearance of the Change Password button. The default is <see langword="null" />.</returns>
	[DefaultValue("")]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	public Style ChangePasswordButtonStyle
	{
		get
		{
			if (_changePasswordButtonStyle == null)
			{
				_changePasswordButtonStyle = new Style();
				if (base.IsTrackingViewState)
				{
					_changePasswordButtonStyle.TrackViewState();
				}
			}
			return _changePasswordButtonStyle;
		}
	}

	/// <summary>Gets or sets the text displayed on the Change Password button.</summary>
	/// <returns>The text to display on the Change Password button. The default is "Change Password".</returns>
	[Localizable(true)]
	public virtual string ChangePasswordButtonText
	{
		get
		{
			return ViewState.GetString("ChangePasswordButtonText", "Change Password");
		}
		set
		{
			ViewState["ChangePasswordButtonText"] = value;
		}
	}

	/// <summary>Gets or sets the type of button to use when rendering the Change Password button of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ButtonType" /> object that defines the type of button to render for the Change Password button. The property value can be one of the three <see cref="T:System.Web.UI.WebControls.ButtonType" /> enumeration values: <see langword="Button" />, <see langword="Image" />, or <see langword="Link" />. The default is <see langword="Button" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <see cref="T:System.Web.UI.WebControls.ButtonType" /> is not one of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values.</exception>
	[DefaultValue(ButtonType.Button)]
	public virtual ButtonType ChangePasswordButtonType
	{
		get
		{
			if (ViewState["ChangePasswordButtonType"] != null)
			{
				return (ButtonType)ViewState["ChangePasswordButtonType"];
			}
			return ButtonType.Button;
		}
		set
		{
			ViewState["ChangePasswordButtonType"] = value;
		}
	}

	/// <summary>Gets or sets the message that is shown when the user's password is not changed.</summary>
	/// <returns>The error message to display when the attempt to change the user's password is not successful. The default is "Your attempt to change passwords was unsuccessful. Please try again."</returns>
	[Localizable(true)]
	public virtual string ChangePasswordFailureText
	{
		get
		{
			return ViewState.GetString("ChangePasswordFailureText", "Your attempt to change passwords was unsuccessful. Please try again.");
		}
		set
		{
			ViewState["ChangePasswordFailureText"] = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.UI.ITemplate" /> object used to display the Change Password view of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for displaying the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control in the Change Password view. The default is <see langword="null" />.</returns>
	[Browsable(false)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[TemplateContainer(typeof(ChangePassword))]
	public virtual ITemplate ChangePasswordTemplate
	{
		get
		{
			return _changePasswordTemplate;
		}
		set
		{
			_changePasswordTemplate = value;
		}
	}

	/// <summary>Gets the container that a <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control uses to create an instance of the <see cref="P:System.Web.UI.WebControls.ChangePassword.ChangePasswordTemplate" /> template. This provides programmatic access to child controls.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Control" /> that contains a <see cref="P:System.Web.UI.WebControls.ChangePassword.ChangePasswordTemplate" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Control ChangePasswordTemplateContainer
	{
		get
		{
			if (_changePasswordTemplateContainer == null)
			{
				_changePasswordTemplateContainer = new ChangePasswordContainer(this);
			}
			return _changePasswordTemplateContainer;
		}
	}

	/// <summary>Gets or sets the text displayed at the top of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control in Change Password view.</summary>
	/// <returns>The text to display at the top of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is "Change Your Password".</returns>
	[Localizable(true)]
	public virtual string ChangePasswordTitleText
	{
		get
		{
			return ViewState.GetString("ChangePasswordTitleText", "Change Your Password");
		}
		set
		{
			ViewState["ChangePasswordTitleText"] = value;
		}
	}

	/// <summary>Gets the duplicate password entered by the user.</summary>
	/// <returns>The duplicate new password string entered by the user.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[Themeable(false)]
	[Filterable(false)]
	public virtual string ConfirmNewPassword
	{
		get
		{
			if (_newPasswordConfirm == null)
			{
				return string.Empty;
			}
			return _newPasswordConfirm;
		}
	}

	/// <summary>Gets or sets the label text for the <see cref="P:System.Web.UI.WebControls.ChangePassword.ConfirmNewPassword" /> text box.</summary>
	/// <returns>The text to display with the <see cref="P:System.Web.UI.WebControls.ChangePassword.ConfirmNewPassword" /> text box. The default is "Confirm New Password:".</returns>
	[Localizable(true)]
	public virtual string ConfirmNewPasswordLabelText
	{
		get
		{
			return ViewState.GetString("ConfirmNewPasswordLabelText", "Confirm New Password:");
		}
		set
		{
			ViewState["ConfirmNewPasswordLabelText"] = value;
		}
	}

	/// <summary>Gets or sets the message that is displayed when the new password and the duplicate password entered by the user are not identical.</summary>
	/// <returns>The error message displayed when the new password and confirmed password are not identical. The default is "The confirm New Password entry must match the New Password entry."</returns>
	[Localizable(true)]
	public virtual string ConfirmPasswordCompareErrorMessage
	{
		get
		{
			return ViewState.GetString("ConfirmPasswordCompareErrorMessage", "The Confirm New Password must match the New Password entry.");
		}
		set
		{
			ViewState["ConfirmPasswordCompareErrorMessage"] = value;
		}
	}

	/// <summary>Gets or sets the error message that is displayed when the Confirm New Password text box is left empty.</summary>
	/// <returns>The error message that is displayed when users attempt to change their password without entering the new password in the <see cref="P:System.Web.UI.WebControls.ChangePassword.ConfirmNewPassword" /> input box.</returns>
	[Localizable(true)]
	public virtual string ConfirmPasswordRequiredErrorMessage
	{
		get
		{
			return ViewState.GetString("ConfirmPasswordRequiredErrorMessage", "Confirm New Password is required.");
		}
		set
		{
			ViewState["ConfirmPasswordRequiredErrorMessage"] = value;
		}
	}

	/// <summary>Gets or sets the URL of an image to use for the Continue button on the Success view of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control if the Continue button is configured by the <see cref="P:System.Web.UI.WebControls.ChangePassword.ContinueButtonType" /> property to be an image button.</summary>
	/// <returns>The URL of the image to display with the Continue button. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string ContinueButtonImageUrl
	{
		get
		{
			return ViewState.GetString("ContinueButtonImageUrl", string.Empty);
		}
		set
		{
			ViewState["ContinueButtonImageUrl"] = value;
		}
	}

	/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of the Continue button on the Success view of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the appearance of the Continue button. The default is <see langword="null" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public Style ContinueButtonStyle
	{
		get
		{
			if (_continueButtonStyle == null)
			{
				_continueButtonStyle = new Style();
				if (base.IsTrackingViewState)
				{
					_continueButtonStyle.TrackViewState();
				}
			}
			return _continueButtonStyle;
		}
	}

	/// <summary>Gets or sets the text that is displayed on the Continue button on the Success view of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>The text to display on the Continue button. The default is "Continue".</returns>
	[Localizable(true)]
	public virtual string ContinueButtonText
	{
		get
		{
			return ViewState.GetString("ContinueButtonText", "Continue");
		}
		set
		{
			ViewState["ContinueButtonText"] = value;
		}
	}

	/// <summary>Gets or sets the type of button to use when rendering the Continue button for the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ButtonType" /> object that defines the type of button to render for the Continue button. The property value can be one of the three <see cref="T:System.Web.UI.WebControls.ButtonType" /> enumeration values: <see langword="Button" />, <see langword="Image" />, or <see langword="Link" />. The default is <see langword="Button" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <see cref="T:System.Web.UI.WebControls.ButtonType" /> is not one of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values.</exception>
	[DefaultValue(ButtonType.Button)]
	public virtual ButtonType ContinueButtonType
	{
		get
		{
			if (ViewState["ContinueButtonType"] != null)
			{
				return (ButtonType)ViewState["ContinueButtonType"];
			}
			return ButtonType.Button;
		}
		set
		{
			ViewState["ContinueButtonType"] = value;
		}
	}

	/// <summary>Gets or sets the URL of the page that the user will see after clicking the Continue button on the Success view.</summary>
	/// <returns>The URL of the page the user is redirected to after clicking the Continue button. The default is <see cref="F:System.String.Empty" />.</returns>
	[Themeable(false)]
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string ContinueDestinationPageUrl
	{
		get
		{
			return ViewState.GetString("ContinueDestinationPageUrl", string.Empty);
		}
		set
		{
			ViewState["ContinueDestinationPageUrl"] = value;
		}
	}

	/// <summary>Gets or sets the URL of an image to display next to the link to the Web page that contains a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control for the Web site.</summary>
	/// <returns>The URL of an image to display next to the link to the Web page that contains a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string CreateUserIconUrl
	{
		get
		{
			return ViewState.GetString("CreateUserIconUrl", string.Empty);
		}
		set
		{
			ViewState["CreateUserIconUrl"] = value;
		}
	}

	/// <summary>Gets or sets the text of the link to the Web page that contains a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control for the Web site.</summary>
	/// <returns>The text to display next to the link to the Web page that contains a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public virtual string CreateUserText
	{
		get
		{
			return ViewState.GetString("CreateUserText", string.Empty);
		}
		set
		{
			ViewState["CreateUserText"] = value;
		}
	}

	/// <summary>Gets or sets the URL of the Web page that contains a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control for the Web site.</summary>
	/// <returns>The URL of the Web page that contains a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string CreateUserUrl
	{
		get
		{
			return ViewState.GetString("CreateUserUrl", string.Empty);
		}
		set
		{
			ViewState["CreateUserUrl"] = value;
		}
	}

	/// <summary>Gets the current password for the user.</summary>
	/// <returns>The current password entered by the user.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[Themeable(false)]
	[Filterable(false)]
	public virtual string CurrentPassword
	{
		get
		{
			if (_currentPassword == null)
			{
				return string.Empty;
			}
			return _currentPassword;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control should display the <see cref="P:System.Web.UI.WebControls.ChangePassword.UserName" /> control and label.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control should display the <see cref="P:System.Web.UI.WebControls.ChangePassword.UserName" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	public virtual bool DisplayUserName
	{
		get
		{
			return ViewState.GetBool("DisplayUserName", def: false);
		}
		set
		{
			ViewState["DisplayUserName"] = value;
		}
	}

	/// <summary>Gets or sets the URL of an image to display next to the link to the user profile editing page for the Web site.</summary>
	/// <returns>The URL of the image to display with the link to the user profile editing page for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string EditProfileIconUrl
	{
		get
		{
			return ViewState.GetString("EditProfileIconUrl", string.Empty);
		}
		set
		{
			ViewState["EditProfileIconUrl"] = value;
		}
	}

	/// <summary>Gets or sets the text of the link to the user profile editing page for the Web site.</summary>
	/// <returns>The text to display for the link to the user profile editing page for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
	[Localizable(true)]
	[DefaultValue("")]
	public virtual string EditProfileText
	{
		get
		{
			return ViewState.GetString("EditProfileText", string.Empty);
		}
		set
		{
			ViewState["EditProfileText"] = value;
		}
	}

	/// <summary>Gets or sets the URL of the user profile editing page for the Web site.</summary>
	/// <returns>The URL of the user profile editing page for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string EditProfileUrl
	{
		get
		{
			return ViewState.GetString("EditProfileUrl", string.Empty);
		}
		set
		{
			ViewState["EditProfileUrl"] = value;
		}
	}

	/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of error messages on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of error messages on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is <see langword="null" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public TableItemStyle FailureTextStyle
	{
		get
		{
			if (_failureTextStyle == null)
			{
				_failureTextStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					_failureTextStyle.TrackViewState();
				}
			}
			return _failureTextStyle;
		}
	}

	/// <summary>Gets or sets the URL of an image to display next to the Change Password help page for the Web site.</summary>
	/// <returns>The URL of the image to display next to the Change Password help page for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string HelpPageIconUrl
	{
		get
		{
			return ViewState.GetString("HelpPageIconUrl", string.Empty);
		}
		set
		{
			ViewState["HelpPageIconUrl"] = value;
		}
	}

	/// <summary>Gets or sets the link text to the Change Password help page for the Web site.</summary>
	/// <returns>The text to display for the link to the Change Password help page for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public virtual string HelpPageText
	{
		get
		{
			return ViewState.GetString("HelpPageText", string.Empty);
		}
		set
		{
			ViewState["HelpPageText"] = value;
		}
	}

	/// <summary>Gets or sets the URL of the Change Password help page for the Web site.</summary>
	/// <returns>The URL of the Change Password help page for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string HelpPageUrl
	{
		get
		{
			return ViewState.GetString("HelpPageUrl", string.Empty);
		}
		set
		{
			ViewState["HelpPageUrl"] = value;
		}
	}

	/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of hyperlinks on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of hyperlinks on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is <see langword="null" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public TableItemStyle HyperLinkStyle
	{
		get
		{
			if (_hyperLinkStyle == null)
			{
				_hyperLinkStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					_hyperLinkStyle.TrackViewState();
				}
			}
			return _hyperLinkStyle;
		}
	}

	/// <summary>Gets or sets informational text that appears on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control between the <see cref="P:System.Web.UI.WebControls.ChangePassword.ChangePasswordTitleText" /> and the input boxes.</summary>
	/// <returns>The informational text to display on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control between the <see cref="P:System.Web.UI.WebControls.ChangePassword.ChangePasswordTitleText" /> and the input boxes. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public virtual string InstructionText
	{
		get
		{
			return ViewState.GetString("InstructionText", string.Empty);
		}
		set
		{
			ViewState["InstructionText"] = value;
		}
	}

	/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of the instructional text on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of the instructional text contained in the <see cref="P:System.Web.UI.WebControls.ChangePassword.InstructionText" /> property. The default is <see langword="null" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public TableItemStyle InstructionTextStyle
	{
		get
		{
			if (_instructionTextStyle == null)
			{
				_instructionTextStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					_instructionTextStyle.TrackViewState();
				}
			}
			return _instructionTextStyle;
		}
	}

	/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> objects that define the appearance of text box labels on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of text box labels on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is <see langword="null" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public TableItemStyle LabelStyle
	{
		get
		{
			if (_labelStyle == null)
			{
				_labelStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					_labelStyle.TrackViewState();
				}
			}
			return _labelStyle;
		}
	}

	/// <summary>Gets a reference to a collection of properties that define the e-mail message that is sent to users after they have changed their password.</summary>
	/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.MailDefinition" /> object that defines the e-mail message sent to a new user.</returns>
	/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.WebControls.MailDefinition.From" /> property is not set to an e-mail address.</exception>
	[Themeable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public MailDefinition MailDefinition
	{
		get
		{
			if (_mailDefinition == null)
			{
				_mailDefinition = new MailDefinition();
				if (base.IsTrackingViewState)
				{
					((IStateManager)_mailDefinition).TrackViewState();
				}
			}
			return _mailDefinition;
		}
	}

	/// <summary>Gets or sets the membership provider that is used to manage member information.</summary>
	/// <returns>The name of the <see cref="T:System.Web.Security.MembershipProvider" /> for the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is the membership provider for the application.</returns>
	[Themeable(false)]
	[DefaultValue("")]
	public virtual string MembershipProvider
	{
		get
		{
			object obj = ViewState["MembershipProvider"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("MembershipProvider");
			}
			else
			{
				ViewState["MembershipProvider"] = value;
			}
			_provider = null;
		}
	}

	/// <summary>Gets the new password entered by the user.</summary>
	/// <returns>The new password entered by the user.</returns>
	[Filterable(false)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Themeable(false)]
	public virtual string NewPassword
	{
		get
		{
			if (_newPassword == null)
			{
				return string.Empty;
			}
			return _newPassword;
		}
	}

	/// <summary>Gets or sets the label text for the New Password text box.</summary>
	/// <returns>The text to display next to the New Password text box. The default is "New Password:".</returns>
	[Localizable(true)]
	public virtual string NewPasswordLabelText
	{
		get
		{
			return ViewState.GetString("NewPasswordLabelText", "New Password:");
		}
		set
		{
			ViewState["NewPasswordLabelText"] = value;
		}
	}

	/// <summary>Gets or sets the regular expression that is used to validate the password provided by the user.</summary>
	/// <returns>The regular expression string used to validate the new password provided by the user. The default is <see cref="F:System.String.Empty" />.</returns>
	public virtual string NewPasswordRegularExpression
	{
		get
		{
			return ViewState.GetString("NewPasswordRegularExpression", string.Empty);
		}
		set
		{
			ViewState["NewPasswordRegularExpression"] = value;
		}
	}

	/// <summary>Gets or sets the error message that is shown when the password entered does not pass the regular expression criteria defined in the <see cref="P:System.Web.UI.WebControls.ChangePassword.NewPasswordRegularExpression" /> property.</summary>
	/// <returns>The error message shown when the password entered does not pass the regular expression defined in the <see cref="P:System.Web.UI.WebControls.ChangePassword.NewPasswordRegularExpression" />. The default is <see cref="F:System.String.Empty" />.</returns>
	public virtual string NewPasswordRegularExpressionErrorMessage
	{
		get
		{
			return ViewState.GetString("NewPasswordRegularExpressionErrorMessage", string.Empty);
		}
		set
		{
			ViewState["NewPasswordRegularExpressionErrorMessage"] = value;
		}
	}

	/// <summary>Gets or sets the error message that is displayed when the user leaves the New Password text box empty.</summary>
	/// <returns>The error message to display if the user leaves the New Password text box empty. The default is "New Password is required."</returns>
	[Localizable(true)]
	public virtual string NewPasswordRequiredErrorMessage
	{
		get
		{
			return ViewState.GetString("NewPasswordRequiredErrorMessage", "New Password is required.");
		}
		set
		{
			ViewState["NewPasswordRequiredErrorMessage"] = value;
		}
	}

	/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of hint text that appears on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of the text contained in the <see cref="P:System.Web.UI.WebControls.ChangePassword.PasswordHintText" /> property. The default is <see langword="null" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public TableItemStyle PasswordHintStyle
	{
		get
		{
			if (_passwordHintStyle == null)
			{
				_passwordHintStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					_passwordHintStyle.TrackViewState();
				}
			}
			return _passwordHintStyle;
		}
	}

	/// <summary>Gets or sets informational text about the requirements for creating a password for the Web site.</summary>
	/// <returns>The informational text to display about the criteria for the new password. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public virtual string PasswordHintText
	{
		get
		{
			return ViewState.GetString("PasswordHintText", string.Empty);
		}
		set
		{
			ViewState["PasswordHintText"] = value;
		}
	}

	/// <summary>Gets or sets the label text for the Current Password text box.</summary>
	/// <returns>The text to display next to the Current Password text box. The default is "Password:".</returns>
	[Localizable(true)]
	public virtual string PasswordLabelText
	{
		get
		{
			return ViewState.GetString("PasswordLabelText", "Password:");
		}
		set
		{
			ViewState["PasswordLabelText"] = value;
		}
	}

	/// <summary>Gets or sets the URL of an image to display next to a link to the Web page that contains the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>The URL of the image to display next to a link to the password recovery page for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string PasswordRecoveryIconUrl
	{
		get
		{
			return ViewState.GetString("PasswordRecoveryIconUrl", string.Empty);
		}
		set
		{
			ViewState["PasswordRecoveryIconUrl"] = value;
		}
	}

	/// <summary>Gets or sets the text of the link to the Web page that contains the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>The text to display for the link to the Web page that contains the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public virtual string PasswordRecoveryText
	{
		get
		{
			return ViewState.GetString("PasswordRecoveryText", string.Empty);
		}
		set
		{
			ViewState["PasswordRecoveryText"] = value;
		}
	}

	/// <summary>Gets or sets the URL of the Web page that contains the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>The URL for the Web page that contains the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string PasswordRecoveryUrl
	{
		get
		{
			return ViewState.GetString("PasswordRecoveryUrl", string.Empty);
		}
		set
		{
			ViewState["PasswordRecoveryUrl"] = value;
		}
	}

	/// <summary>Gets or sets the error message that is displayed when the user leaves the Current Password text box empty.</summary>
	/// <returns>The error message to display if the user leaves the Current Password text box empty.</returns>
	[Localizable(true)]
	public virtual string PasswordRequiredErrorMessage
	{
		get
		{
			return ViewState.GetString("PasswordRequiredErrorMessage", string.Empty);
		}
		set
		{
			ViewState["PasswordRequiredErrorMessage"] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the control encloses rendered HTML in a <see langword="table" /> element in order to apply inline styles.</summary>
	/// <returns>
	///     <see langword="true" /> if the control encloses rendered HTML in a <see langword="table" /> element; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	public virtual bool RenderOuterTable
	{
		get
		{
			return renderOuterTable;
		}
		set
		{
			renderOuterTable = value;
		}
	}

	/// <summary>Gets or sets the URL of the page that is shown to users after they have changed their password successfully.</summary>
	/// <returns>The URL of the destination page after the password is changed. The default is <see cref="F:System.String.Empty" />. </returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Themeable(false)]
	public virtual string SuccessPageUrl
	{
		get
		{
			return ViewState.GetString("SuccessPageUrl", string.Empty);
		}
		set
		{
			ViewState["SuccessPageUrl"] = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.UI.ITemplate" /> object that is used to display the Success and Change Password views of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for displaying the Success and Change Password views of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is <see langword="null" />.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[TemplateContainer(typeof(ChangePassword))]
	[Browsable(false)]
	public virtual ITemplate SuccessTemplate
	{
		get
		{
			return _successTemplate;
		}
		set
		{
			_successTemplate = value;
		}
	}

	/// <summary>Gets the container that a <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control used to create an instance of the <see cref="P:System.Web.UI.WebControls.ChangePassword.SuccessTemplate" /> template. This provides programmatic access to child controls.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Control" /> that contains a <see cref="P:System.Web.UI.WebControls.ChangePassword.SuccessTemplate" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Control SuccessTemplateContainer
	{
		get
		{
			if (_successTemplateContainer == null)
			{
				_successTemplateContainer = new SuccessContainer(this);
			}
			return _successTemplateContainer;
		}
	}

	/// <summary>Gets or sets the text that is displayed on the Success view between the <see cref="P:System.Web.UI.WebControls.ChangePassword.SuccessTitleText" /> and the Continue button.</summary>
	/// <returns>The text to display on the Success view between the <see cref="P:System.Web.UI.WebControls.ChangePassword.SuccessTitleText" /> and the Continue button. The default is <see cref="F:System.String.Empty" />.</returns>
	[Localizable(true)]
	public virtual string SuccessText
	{
		get
		{
			return ViewState.GetString("SuccessText", "Your password has been changed!");
		}
		set
		{
			ViewState["SuccessText"] = value;
		}
	}

	/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of text on the Success view.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of the text contained in the <see cref="P:System.Web.UI.WebControls.ChangePassword.SuccessText" /> property. The default is <see langword="null" />.</returns>
	[DefaultValue(null)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public TableItemStyle SuccessTextStyle
	{
		get
		{
			if (_successTextStyle == null)
			{
				_successTextStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					_successTextStyle.TrackViewState();
				}
			}
			return _successTextStyle;
		}
	}

	/// <summary>Gets or sets the title of the Success view.</summary>
	/// <returns>The text to display as the title in the Success view of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is "Change Password Complete".</returns>
	[Localizable(true)]
	public virtual string SuccessTitleText
	{
		get
		{
			return ViewState.GetString("SuccessTitleText", "Change Password Complete");
		}
		set
		{
			ViewState["SuccessTitleText"] = value;
		}
	}

	/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of text box controls on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the appearance of text box controls on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is <see langword="null" />.</returns>
	[DefaultValue(null)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public Style TextBoxStyle
	{
		get
		{
			if (_textBoxStyle == null)
			{
				_textBoxStyle = new Style();
				if (base.IsTrackingViewState)
				{
					_textBoxStyle.TrackViewState();
				}
			}
			return _textBoxStyle;
		}
	}

	/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of titles on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of error messages titles on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is <see langword="null" />.</returns>
	[DefaultValue(null)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public TableItemStyle TitleTextStyle
	{
		get
		{
			if (_titleTextStyle == null)
			{
				_titleTextStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					_titleTextStyle.TrackViewState();
				}
			}
			return _titleTextStyle;
		}
	}

	/// <summary>Gets or sets the Web site user name for which to change the password.</summary>
	/// <returns>The user name for which to change the password.</returns>
	[DefaultValue("")]
	public virtual string UserName
	{
		get
		{
			if (_username == null && HttpContext.Current.Request.IsAuthenticated)
			{
				_username = HttpContext.Current.User.Identity.Name;
			}
			if (_username == null)
			{
				return string.Empty;
			}
			return _username;
		}
		set
		{
			_username = value;
		}
	}

	/// <summary>Gets or sets the label for the User Name text box.</summary>
	/// <returns>The text to display next to the User Name textbox. The default string is "User Name:".</returns>
	[Localizable(true)]
	public virtual string UserNameLabelText
	{
		get
		{
			return ViewState.GetString("UserNameLabelText", "User Name:");
		}
		set
		{
			ViewState["UserNameLabelText"] = value;
		}
	}

	/// <summary>Gets or sets the error message that is displayed when the user leaves the User Name text box empty.</summary>
	/// <returns>The error message to display if the user leaves the User Name text box empty. The default string is "User Name is required.".</returns>
	[Localizable(true)]
	public virtual string UserNameRequiredErrorMessage
	{
		get
		{
			return ViewState.GetString("UserNameRequiredErrorMessage", "User Name is required.");
		}
		set
		{
			ViewState["UserNameRequiredErrorMessage"] = value;
		}
	}

	/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of error messages that are associated with any input validation used by the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the appearance of error messages that are associated with any input validation used by the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is <see langword="null" />.</returns>
	[DefaultValue(null)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public Style ValidatorTextStyle
	{
		get
		{
			if (_validatorTextStyle == null)
			{
				_validatorTextStyle = new Style();
				if (base.IsTrackingViewState)
				{
					_validatorTextStyle.TrackViewState();
				}
			}
			return _validatorTextStyle;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to a <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. This property is used primarily by control developers.</summary>
	/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. Always returns <see langword="HtmlTextWriterTag.Table." /></returns>
	protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Table;

	internal virtual MembershipProvider MembershipProviderInternal
	{
		get
		{
			if (_provider == null)
			{
				InitMemberShipProvider();
			}
			return _provider;
		}
	}

	/// <summary>Occurs when the user clicks the Cancel button to cancel changing a password.</summary>
	public event EventHandler CancelButtonClick
	{
		add
		{
			events.AddHandler(cancelButtonClickEvent, value);
		}
		remove
		{
			events.RemoveHandler(cancelButtonClickEvent, value);
		}
	}

	/// <summary>Occurs when the password is changed for a user account.</summary>
	public event EventHandler ChangedPassword
	{
		add
		{
			events.AddHandler(changedPasswordEvent, value);
		}
		remove
		{
			events.RemoveHandler(changedPasswordEvent, value);
		}
	}

	/// <summary>Occurs when there is an error changing the password for the user account.</summary>
	public event EventHandler ChangePasswordError
	{
		add
		{
			events.AddHandler(changePasswordErrorEvent, value);
		}
		remove
		{
			events.RemoveHandler(changePasswordErrorEvent, value);
		}
	}

	/// <summary>Occurs before the password for a user account is changed by the membership provider.</summary>
	public event LoginCancelEventHandler ChangingPassword
	{
		add
		{
			events.AddHandler(changingPasswordEvent, value);
		}
		remove
		{
			events.RemoveHandler(changingPasswordEvent, value);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ChangePassword.ContinueButtonClick" /> event when the user clicks the Continue button.</summary>
	public event EventHandler ContinueButtonClick
	{
		add
		{
			events.AddHandler(continueButtonClickEvent, value);
		}
		remove
		{
			events.RemoveHandler(continueButtonClickEvent, value);
		}
	}

	/// <summary>Occurs before the user is sent an e-mail confirmation that the password has been changed.</summary>
	public event MailMessageEventHandler SendingMail
	{
		add
		{
			events.AddHandler(sendingMailEvent, value);
		}
		remove
		{
			events.RemoveHandler(sendingMailEvent, value);
		}
	}

	/// <summary>Occurs when there is an SMTP error sending an e-mail message to the user.</summary>
	public event SendMailErrorEventHandler SendMailError
	{
		add
		{
			events.AddHandler(sendMailErrorEvent, value);
		}
		remove
		{
			events.RemoveHandler(sendMailErrorEvent, value);
		}
	}

	/// <summary>Creates the individual controls that make up the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control in preparation for posting back or rendering.</summary>
	/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.WebControls.ChangePassword.DisplayUserName" /> property is set to <see langword="false" />, the <see cref="P:System.Web.UI.WebControls.ChangePassword.ChangePasswordTemplate" /> contains a control that implements the <see cref="T:System.Web.UI.IEditableTextControl" /> interface, and the <see cref="P:System.Web.UI.Control.ID" /> property of the control is set to "UserName".-or-The <see cref="P:System.Web.UI.WebControls.ChangePassword.DisplayUserName" /> property is set to <see langword="true" />, the <see cref="P:System.Web.UI.WebControls.ChangePassword.ChangePasswordTemplate" /> does not contain a control that implements the <see cref="T:System.Web.UI.IEditableTextControl" /> interface, and the <see cref="P:System.Web.UI.Control.ID" /> property of the control is set to "UserName".-or-The <see cref="P:System.Web.UI.WebControls.ChangePassword.ChangePasswordTemplate" /> does not contain a control that implements the <see cref="T:System.Web.UI.IEditableTextControl" /> interface, and the <see cref="P:System.Web.UI.Control.ID" /> property of the control is set to "CurrentPassword".-or-The <see cref="P:System.Web.UI.WebControls.ChangePassword.ChangePasswordTemplate" /> does not contain a control that implements the <see cref="T:System.Web.UI.IEditableTextControl" /> interface, and the <see cref="P:System.Web.UI.Control.ID" /> property of the control is set to "NewPassword".</exception>
	protected internal override void CreateChildControls()
	{
		Controls.Clear();
		ITemplate template = ChangePasswordTemplate;
		if (template == null)
		{
			template = new ChangePasswordDeafultTemplate(this);
		}
		((ChangePasswordContainer)ChangePasswordTemplateContainer).InstantiateTemplate(template);
		ITemplate template2 = SuccessTemplate;
		if (template2 == null)
		{
			template2 = new SuccessDefaultTemplate(this);
		}
		((SuccessContainer)SuccessTemplateContainer).InstantiateTemplate(template2);
		Controls.AddAt(0, ChangePasswordTemplateContainer);
		Controls.AddAt(1, SuccessTemplateContainer);
		ChangePasswordContainer changePasswordContainer = (ChangePasswordContainer)ChangePasswordTemplateContainer;
		IEditableTextControl userNameTextBox;
		if (DisplayUserName)
		{
			userNameTextBox = changePasswordContainer.UserNameTextBox;
			if (userNameTextBox != null)
			{
				userNameTextBox.TextChanged += UserName_TextChanged;
			}
		}
		userNameTextBox = changePasswordContainer.CurrentPasswordTextBox;
		if (userNameTextBox != null)
		{
			userNameTextBox.TextChanged += CurrentPassword_TextChanged;
		}
		userNameTextBox = changePasswordContainer.NewPasswordTextBox;
		if (userNameTextBox != null)
		{
			userNameTextBox.TextChanged += NewPassword_TextChanged;
		}
		userNameTextBox = changePasswordContainer.ConfirmNewPasswordTextBox;
		if (userNameTextBox != null)
		{
			userNameTextBox.TextChanged += NewPasswordConfirm_TextChanged;
		}
	}

	/// <summary>Writes the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream used to write content to a Web page.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		VerifyInlinePropertiesNotSet();
		for (int i = 0; i < Controls.Count; i++)
		{
			if (Controls[i].Visible)
			{
				Controls[i].Render(writer);
			}
		}
	}

	/// <summary>Sets design-time data for a control.</summary>
	/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> containing the design-time data for the control. </param>
	[MonoTODO("Not implemented")]
	protected override void SetDesignModeState(IDictionary data)
	{
		throw new NotImplementedException();
	}

	private void InitMemberShipProvider()
	{
		string membershipProvider = MembershipProvider;
		_provider = ((membershipProvider.Length == 0) ? Membership.Provider : Membership.Providers[membershipProvider]);
		if (_provider == null)
		{
			throw new HttpException(Locale.GetText("No provider named '{0}' could be found.", membershipProvider));
		}
	}

	private void ProcessChangePasswordEvent(CommandEventArgs args)
	{
		if (!Page.IsValid)
		{
			return;
		}
		LoginCancelEventArgs loginCancelEventArgs = new LoginCancelEventArgs();
		OnChangingPassword(loginCancelEventArgs);
		if (loginCancelEventArgs.Cancel)
		{
			return;
		}
		bool flag = false;
		try
		{
			flag = MembershipProviderInternal.ChangePassword(UserName, CurrentPassword, NewPassword);
		}
		catch
		{
		}
		if (flag)
		{
			OnChangedPassword(args);
			_showContinue = true;
			if (_mailDefinition != null)
			{
				SendMail(UserName, NewPassword);
			}
		}
		else
		{
			OnChangePasswordError(EventArgs.Empty);
			string text = $"Password incorrect or New Password invalid. New Password length minimum: {MembershipProviderInternal.MinRequiredPasswordLength}. Non-alphanumeric characters required: {MembershipProviderInternal.MinRequiredNonAlphanumericCharacters}.";
			((ChangePasswordContainer)ChangePasswordTemplateContainer).FailureTextLiteral.Text = text;
			_showContinue = false;
		}
	}

	private void ProcessCancelEvent(CommandEventArgs args)
	{
		OnCancelButtonClick(args);
		if (ContinueDestinationPageUrl.Length > 0)
		{
			Context.Response.Redirect(ContinueDestinationPageUrl);
		}
	}

	private void ProcessContinueEvent(CommandEventArgs args)
	{
		OnContinueButtonClick(args);
		if (ContinueDestinationPageUrl.Length > 0)
		{
			Context.Response.Redirect(ContinueDestinationPageUrl);
		}
	}

	private void SendMail(string username, string password)
	{
		MembershipUser user = MembershipProviderInternal.GetUser(UserName, userIsOnline: false);
		if (user == null)
		{
			return;
		}
		ListDictionary listDictionary = new ListDictionary();
		listDictionary.Add("<%USERNAME%>", username);
		listDictionary.Add("<%PASSWORD%>", password);
		MailMessage message = MailDefinition.CreateMailMessage(user.Email, listDictionary, this);
		MailMessageEventArgs e = new MailMessageEventArgs(message);
		OnSendingMail(e);
		SmtpClient smtpClient = new SmtpClient();
		try
		{
			smtpClient.Send(message);
		}
		catch (Exception ex)
		{
			SendMailErrorEventArgs sendMailErrorEventArgs = new SendMailErrorEventArgs(ex);
			OnSendMailError(sendMailErrorEventArgs);
			if (!sendMailErrorEventArgs.Handled)
			{
				throw ex;
			}
		}
	}

	/// <summary>Restores control state information from a previous page request that was saved by the <see cref="M:System.Web.UI.WebControls.ChangePassword.SaveControlState" /> method.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to restore.</param>
	protected internal override void LoadControlState(object savedState)
	{
		if (savedState != null)
		{
			object[] array = (object[])savedState;
			base.LoadControlState(array[0]);
			_showContinue = (bool)array[1];
			_username = (string)array[2];
		}
	}

	/// <summary>Saves any server control state changes that have occurred since the time the page was posted back to the server.</summary>
	/// <returns>The server control's current state; otherwise, <see langword="null" />.</returns>
	protected internal override object SaveControlState()
	{
		object obj = base.SaveControlState();
		return new object[3] { obj, _showContinue, _username };
	}

	/// <summary>Restores view state information from a previous page request that was saved by the <see cref="M:System.Web.UI.WebControls.ChangePassword.SaveViewState" /> method.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to restore.</param>
	/// <exception cref="T:System.ArgumentException">The <paramref name="savedState" /> parameter cannot be resolved to a valid <see cref="P:System.Web.UI.Control.ViewState" />.</exception>
	protected override void LoadViewState(object savedState)
	{
		if (savedState != null)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				CancelButtonStyle.LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				ChangePasswordButtonStyle.LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				ContinueButtonStyle.LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				FailureTextStyle.LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				HyperLinkStyle.LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				InstructionTextStyle.LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				LabelStyle.LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				PasswordHintStyle.LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				SuccessTextStyle.LoadViewState(array[9]);
			}
			if (array[10] != null)
			{
				TextBoxStyle.LoadViewState(array[10]);
			}
			if (array[11] != null)
			{
				TitleTextStyle.LoadViewState(array[11]);
			}
			if (array[12] != null)
			{
				ValidatorTextStyle.LoadViewState(array[12]);
			}
			if (array[13] != null)
			{
				((IStateManager)MailDefinition).LoadViewState(array[13]);
			}
		}
	}

	/// <summary>Saves any server control view state changes that have occurred since the time the page was posted back to the server.</summary>
	/// <returns>The server control's current view state; otherwise, <see langword="null" />.</returns>
	protected override object SaveViewState()
	{
		object[] array = new object[14]
		{
			base.SaveViewState(),
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null
		};
		if (_cancelButtonStyle != null)
		{
			array[1] = _cancelButtonStyle.SaveViewState();
		}
		if (_changePasswordButtonStyle != null)
		{
			array[2] = _changePasswordButtonStyle.SaveViewState();
		}
		if (_continueButtonStyle != null)
		{
			array[3] = _continueButtonStyle.SaveViewState();
		}
		if (_failureTextStyle != null)
		{
			array[4] = _failureTextStyle.SaveViewState();
		}
		if (_hyperLinkStyle != null)
		{
			array[5] = _hyperLinkStyle.SaveViewState();
		}
		if (_instructionTextStyle != null)
		{
			array[6] = _instructionTextStyle.SaveViewState();
		}
		if (_labelStyle != null)
		{
			array[7] = _labelStyle.SaveViewState();
		}
		if (_passwordHintStyle != null)
		{
			array[8] = _passwordHintStyle.SaveViewState();
		}
		if (_successTextStyle != null)
		{
			array[9] = _successTextStyle.SaveViewState();
		}
		if (_textBoxStyle != null)
		{
			array[10] = _textBoxStyle.SaveViewState();
		}
		if (_titleTextStyle != null)
		{
			array[11] = _titleTextStyle.SaveViewState();
		}
		if (_validatorTextStyle != null)
		{
			array[12] = _validatorTextStyle.SaveViewState();
		}
		if (_mailDefinition != null)
		{
			array[13] = ((IStateManager)_mailDefinition).SaveViewState();
		}
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null)
			{
				return array;
			}
		}
		return null;
	}

	/// <summary>Causes tracking of view-state changes to the server control so that they can be stored in the server control's <see cref="T:System.Web.UI.StateBag" /> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property. </summary>
	protected override void TrackViewState()
	{
		base.TrackViewState();
		if (_cancelButtonStyle != null)
		{
			_cancelButtonStyle.TrackViewState();
		}
		if (_changePasswordButtonStyle != null)
		{
			_changePasswordButtonStyle.TrackViewState();
		}
		if (_continueButtonStyle != null)
		{
			_continueButtonStyle.TrackViewState();
		}
		if (_failureTextStyle != null)
		{
			_failureTextStyle.TrackViewState();
		}
		if (_hyperLinkStyle != null)
		{
			_hyperLinkStyle.TrackViewState();
		}
		if (_instructionTextStyle != null)
		{
			_instructionTextStyle.TrackViewState();
		}
		if (_labelStyle != null)
		{
			_labelStyle.TrackViewState();
		}
		if (_passwordHintStyle != null)
		{
			_passwordHintStyle.TrackViewState();
		}
		if (_successTextStyle != null)
		{
			_successTextStyle.TrackViewState();
		}
		if (_textBoxStyle != null)
		{
			_textBoxStyle.TrackViewState();
		}
		if (_titleTextStyle != null)
		{
			_titleTextStyle.TrackViewState();
		}
		if (_validatorTextStyle != null)
		{
			_validatorTextStyle.TrackViewState();
		}
		if (_mailDefinition != null)
		{
			((IStateManager)_mailDefinition).TrackViewState();
		}
	}

	/// <summary>Determines whether the event for the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control is passed up the Web server control hierarchy for the page.</summary>
	/// <param name="source">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	/// <returns>
	///     <see langword="true" /> if the event has been canceled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	protected override bool OnBubbleEvent(object source, EventArgs e)
	{
		CommandEventArgs commandEventArgs = e as CommandEventArgs;
		if (e != null)
		{
			if (commandEventArgs.CommandName == ChangePasswordButtonCommandName)
			{
				ProcessChangePasswordEvent(commandEventArgs);
				return true;
			}
			if (commandEventArgs.CommandName == CancelButtonCommandName)
			{
				ProcessCancelEvent(commandEventArgs);
				return true;
			}
			if (commandEventArgs.CommandName == ContinueButtonCommandName)
			{
				ProcessContinueEvent(commandEventArgs);
				return true;
			}
		}
		return base.OnBubbleEvent(source, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ChangePassword.CancelButtonClick" /> event when a user clicks the Cancel button.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	protected virtual void OnCancelButtonClick(EventArgs e)
	{
		if (events[cancelButtonClickEvent] is EventHandler eventHandler)
		{
			eventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ChangePassword.ChangedPassword" /> event after the password is changed.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	protected virtual void OnChangedPassword(EventArgs e)
	{
		if (events[changedPasswordEvent] is EventHandler eventHandler)
		{
			eventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ChangePassword.ChangePasswordError" /> event when the user's password is not changed.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	protected virtual void OnChangePasswordError(EventArgs e)
	{
		if (events[changePasswordErrorEvent] is EventHandler eventHandler)
		{
			eventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ChangePassword.ChangingPassword" /> event before the user's password is changed by the membership provider.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> object containing the event data.</param>
	protected virtual void OnChangingPassword(LoginCancelEventArgs e)
	{
		if (events[changingPasswordEvent] is LoginCancelEventHandler loginCancelEventHandler)
		{
			loginCancelEventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ChangePassword.ContinueButtonClick" /> event when a user clicks the Continue button.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	protected virtual void OnContinueButtonClick(EventArgs e)
	{
		if (events[continueButtonClickEvent] is EventHandler eventHandler)
		{
			eventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event for the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control to allow the control to register itself with the page.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object containing the event data.</param>
	protected internal override void OnInit(EventArgs e)
	{
		Page.RegisterRequiresControlState(this);
		base.OnInit(e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object containing the event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		ChangePasswordTemplateContainer.Visible = !_showContinue;
		SuccessTemplateContainer.Visible = _showContinue;
		base.OnPreRender(e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ChangePassword.SendingMail" /> event before an e-mail message is sent to the SMTP server for processing. The SMTP server then sends the e-mail message to the user.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.MailMessageEventArgs" /> object containing the event data.</param>
	protected virtual void OnSendingMail(MailMessageEventArgs e)
	{
		if (events[sendingMailEvent] is MailMessageEventHandler mailMessageEventHandler)
		{
			mailMessageEventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ChangePassword.SendMailError" /> event when an e-mail message cannot be sent to the user.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SendMailErrorEventArgs" /> object containing the event data.</param>
	protected virtual void OnSendMailError(SendMailErrorEventArgs e)
	{
		if (events[sendMailErrorEvent] is SendMailErrorEventHandler sendMailErrorEventHandler)
		{
			sendMailErrorEventHandler(this, e);
		}
	}

	private void UserName_TextChanged(object sender, EventArgs e)
	{
		UserName = ((ITextControl)sender).Text;
	}

	private void CurrentPassword_TextChanged(object sender, EventArgs e)
	{
		_currentPassword = ((ITextControl)sender).Text;
	}

	private void NewPassword_TextChanged(object sender, EventArgs e)
	{
		_newPassword = ((ITextControl)sender).Text;
	}

	private void NewPasswordConfirm_TextChanged(object sender, EventArgs e)
	{
		_newPasswordConfirm = ((ITextControl)sender).Text;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> class. </summary>
	public ChangePassword()
	{
	}
}
