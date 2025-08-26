using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Net.Mail;
using System.Web.Security;

namespace System.Web.UI.WebControls;

/// <summary>Provides user interface (UI) elements that enable a user to recover or reset a lost password and receive it in e-mail.</summary>
[Bindable(false)]
[DefaultEvent("SendingMail")]
[Designer("System.Web.UI.Design.WebControls.PasswordRecoveryDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
public class PasswordRecovery : CompositeControl, IRenderOuterTable
{
	private abstract class BasePasswordRecoveryContainer : Control, INamingContainer
	{
		protected readonly PasswordRecovery _owner;

		private bool renderOuterTable;

		private Table _table;

		private TableCell _containerCell;

		public BasePasswordRecoveryContainer(PasswordRecovery owner)
		{
			_owner = owner;
			renderOuterTable = _owner.RenderOuterTable;
			if (renderOuterTable)
			{
				InitTable();
			}
		}

		public void InstantiateTemplate(ITemplate template)
		{
			if (!renderOuterTable)
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
			string iD = _owner.ID;
			if (!string.IsNullOrEmpty(iD))
			{
				_table.Attributes.Add("id", iD);
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

		public abstract void UpdateChildControls();
	}

	private sealed class QuestionContainer : BasePasswordRecoveryContainer
	{
		public IEditableTextControl AnswerTextBox => (FindControl("Answer") ?? throw new HttpException("QuestionTemplate does not contain an IEditableTextControl with ID Answer for the username.")) as IEditableTextControl;

		public Literal UserNameLiteral => FindControl("UserName") as Literal;

		public Literal QuestionLiteral => FindControl("Question") as Literal;

		public Literal FailureTextLiteral => FindControl("FailureText") as Literal;

		public QuestionContainer(PasswordRecovery owner)
			: base(owner)
		{
		}

		public override void UpdateChildControls()
		{
			if (UserNameLiteral != null)
			{
				UserNameLiteral.Text = _owner.UserName;
			}
			if (QuestionLiteral != null)
			{
				QuestionLiteral.Text = _owner.Question;
			}
		}
	}

	private sealed class SuccessContainer : BasePasswordRecoveryContainer
	{
		public SuccessContainer(PasswordRecovery owner)
			: base(owner)
		{
		}

		public override void UpdateChildControls()
		{
		}
	}

	private sealed class UserNameContainer : BasePasswordRecoveryContainer
	{
		public IEditableTextControl UserNameTextBox => (FindControl("UserName") ?? throw new HttpException("UserNameTemplate does not contain an IEditableTextControl with ID UserName for the username.")) as IEditableTextControl;

		public ITextControl FailureTextLiteral => FindControl("FailureText") as ITextControl;

		public UserNameContainer(PasswordRecovery owner)
			: base(owner)
		{
		}

		public override void UpdateChildControls()
		{
		}
	}

	private class TemplateUtils
	{
		public static TableRow CreateRow(Control c1, Control c2, Style s1, Style s2, bool twoCells)
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.Controls.Add(c1);
			if (s1 != null)
			{
				tableCell.ApplyStyle(s1);
			}
			tableRow.Cells.Add(tableCell);
			if (c2 != null)
			{
				TableCell tableCell2 = new TableCell();
				tableCell2.Controls.Add(c2);
				if (s2 != null)
				{
					tableCell2.ApplyStyle(s2);
				}
				tableRow.Cells.Add(tableCell2);
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell2.HorizontalAlign = HorizontalAlign.Left;
			}
			else
			{
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				if (twoCells)
				{
					tableCell.ColumnSpan = 2;
				}
			}
			return tableRow;
		}

		public static TableRow CreateHelpRow(string pageUrl, string linkText, string linkIcon, Style linkStyle, bool twoCells)
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			if (linkIcon.Length > 0)
			{
				Image child = new Image
				{
					ImageUrl = linkIcon
				};
				tableCell.Controls.Add(child);
			}
			if (linkText.Length > 0)
			{
				HyperLink hyperLink = new HyperLink();
				hyperLink.NavigateUrl = pageUrl;
				hyperLink.Text = linkText;
				hyperLink.ControlStyle.CopyTextStylesFrom(linkStyle);
				tableCell.Controls.Add(hyperLink);
			}
			if (twoCells)
			{
				tableCell.ColumnSpan = 2;
			}
			tableRow.ControlStyle.CopyFrom(linkStyle);
			tableRow.Cells.Add(tableCell);
			return tableRow;
		}
	}

	private sealed class UserNameDefaultTemplate : ITemplate
	{
		private readonly PasswordRecovery _owner;

		public UserNameDefaultTemplate(PasswordRecovery _owner)
		{
			this._owner = _owner;
		}

		public void InstantiateIn(Control container)
		{
			Table table = new Table();
			table.CellPadding = 0;
			bool flag = _owner.TextLayout == LoginTextLayout.TextOnLeft;
			table.Rows.Add(TemplateUtils.CreateRow(new LiteralControl(_owner.UserNameTitleText), null, _owner.TitleTextStyle, null, flag));
			table.Rows.Add(TemplateUtils.CreateRow(new LiteralControl(_owner.UserNameInstructionText), null, _owner.InstructionTextStyle, null, flag));
			TextBox textBox = new TextBox();
			textBox.ID = "UserName";
			textBox.Text = _owner.UserName;
			textBox.ApplyStyle(_owner.TextBoxStyle);
			Label label = new Label();
			label.ID = "UserNameLabel";
			label.AssociatedControlID = "UserName";
			label.Text = _owner.UserNameLabelText;
			label.ApplyStyle(_owner.LabelStyle);
			RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
			requiredFieldValidator.ID = "UserNameRequired";
			requiredFieldValidator.ControlToValidate = "UserName";
			requiredFieldValidator.ErrorMessage = _owner.UserNameRequiredErrorMessage;
			requiredFieldValidator.ToolTip = _owner.UserNameRequiredErrorMessage;
			requiredFieldValidator.Text = "*";
			requiredFieldValidator.ValidationGroup = _owner.ID;
			requiredFieldValidator.ApplyStyle(_owner.ValidatorTextStyle);
			if (flag)
			{
				TableRow tableRow = TemplateUtils.CreateRow(label, textBox, null, null, flag);
				tableRow.Cells[1].Controls.Add(requiredFieldValidator);
				table.Rows.Add(tableRow);
			}
			else
			{
				table.Rows.Add(TemplateUtils.CreateRow(label, null, null, null, flag));
				TableRow tableRow2 = TemplateUtils.CreateRow(textBox, null, null, null, flag);
				tableRow2.Cells[0].Controls.Add(requiredFieldValidator);
				table.Rows.Add(tableRow2);
			}
			Literal literal = new Literal();
			literal.ID = "FailureText";
			if (_owner.FailureTextStyle.ForeColor.IsEmpty)
			{
				_owner.FailureTextStyle.ForeColor = Color.Red;
			}
			table.Rows.Add(TemplateUtils.CreateRow(literal, null, _owner.FailureTextStyle, null, flag));
			WebControl webControl = null;
			switch (_owner.SubmitButtonType)
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
			webControl.ID = "SubmitButton";
			webControl.ApplyStyle(_owner.SubmitButtonStyle);
			((IButtonControl)webControl).CommandName = SubmitButtonCommandName;
			((IButtonControl)webControl).Text = _owner.SubmitButtonText;
			((IButtonControl)webControl).ValidationGroup = _owner.ID;
			TableRow tableRow3 = TemplateUtils.CreateRow(webControl, null, null, null, flag);
			tableRow3.Cells[0].HorizontalAlign = HorizontalAlign.Right;
			table.Rows.Add(tableRow3);
			table.Rows.Add(TemplateUtils.CreateHelpRow(_owner.HelpPageUrl, _owner.HelpPageText, _owner.HelpPageIconUrl, _owner.HyperLinkStyle, flag));
			container.Controls.Add(table);
		}
	}

	private sealed class QuestionDefaultTemplate : ITemplate
	{
		private readonly PasswordRecovery _owner;

		public QuestionDefaultTemplate(PasswordRecovery _owner)
		{
			this._owner = _owner;
		}

		public void InstantiateIn(Control container)
		{
			Table table = new Table();
			table.CellPadding = 0;
			bool flag = _owner.TextLayout == LoginTextLayout.TextOnLeft;
			table.Rows.Add(TemplateUtils.CreateRow(new LiteralControl(_owner.QuestionTitleText), null, _owner.TitleTextStyle, null, flag));
			table.Rows.Add(TemplateUtils.CreateRow(new LiteralControl(_owner.QuestionInstructionText), null, _owner.InstructionTextStyle, null, flag));
			Literal literal = new Literal();
			literal.ID = "UserName";
			table.Rows.Add(TemplateUtils.CreateRow(new LiteralControl(_owner.UserNameLabelText), literal, _owner.LabelStyle, _owner.LabelStyle, flag));
			Literal literal2 = new Literal();
			literal2.ID = "Question";
			table.Rows.Add(TemplateUtils.CreateRow(new LiteralControl(_owner.QuestionLabelText), literal2, _owner.LabelStyle, _owner.LabelStyle, flag));
			TextBox textBox = new TextBox();
			textBox.ID = "Answer";
			textBox.ApplyStyle(_owner.TextBoxStyle);
			Label label = new Label();
			label.ID = "AnswerLabel";
			label.AssociatedControlID = "Answer";
			label.Text = _owner.AnswerLabelText;
			label.ApplyStyle(_owner.LabelStyle);
			RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
			requiredFieldValidator.ID = "AnswerRequired";
			requiredFieldValidator.ControlToValidate = "Answer";
			requiredFieldValidator.ErrorMessage = _owner.AnswerRequiredErrorMessage;
			requiredFieldValidator.ToolTip = _owner.AnswerRequiredErrorMessage;
			requiredFieldValidator.Text = "*";
			requiredFieldValidator.ValidationGroup = _owner.ID;
			requiredFieldValidator.ApplyStyle(_owner.ValidatorTextStyle);
			if (flag)
			{
				TableRow tableRow = TemplateUtils.CreateRow(label, textBox, null, null, flag);
				tableRow.Cells[1].Controls.Add(requiredFieldValidator);
				table.Rows.Add(tableRow);
			}
			else
			{
				table.Rows.Add(TemplateUtils.CreateRow(label, null, null, null, flag));
				TableRow tableRow2 = TemplateUtils.CreateRow(textBox, null, null, null, flag);
				tableRow2.Cells[0].Controls.Add(requiredFieldValidator);
				table.Rows.Add(tableRow2);
			}
			Literal literal3 = new Literal();
			literal3.ID = "FailureText";
			if (_owner.FailureTextStyle.ForeColor.IsEmpty)
			{
				_owner.FailureTextStyle.ForeColor = Color.Red;
			}
			table.Rows.Add(TemplateUtils.CreateRow(literal3, null, _owner.FailureTextStyle, null, flag));
			WebControl webControl = null;
			switch (_owner.SubmitButtonType)
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
			webControl.ID = "SubmitButton";
			webControl.ApplyStyle(_owner.SubmitButtonStyle);
			((IButtonControl)webControl).CommandName = SubmitButtonCommandName;
			((IButtonControl)webControl).Text = _owner.SubmitButtonText;
			((IButtonControl)webControl).ValidationGroup = _owner.ID;
			TableRow tableRow3 = TemplateUtils.CreateRow(webControl, null, null, null, flag);
			tableRow3.Cells[0].HorizontalAlign = HorizontalAlign.Right;
			table.Rows.Add(tableRow3);
			table.Rows.Add(TemplateUtils.CreateHelpRow(_owner.HelpPageUrl, _owner.HelpPageText, _owner.HelpPageIconUrl, _owner.HyperLinkStyle, flag));
			container.Controls.Add(table);
		}
	}

	private sealed class SuccessDefaultTemplate : ITemplate
	{
		private readonly PasswordRecovery _owner;

		public SuccessDefaultTemplate(PasswordRecovery _owner)
		{
			this._owner = _owner;
		}

		public void InstantiateIn(Control container)
		{
			Table table = new Table();
			table.CellPadding = 0;
			bool twoCells = _owner.TextLayout == LoginTextLayout.TextOnLeft;
			table.Rows.Add(TemplateUtils.CreateRow(new LiteralControl(_owner.SuccessText), null, _owner.SuccessTextStyle, null, twoCells));
			container.Controls.Add(table);
		}
	}

	private enum PasswordReciveryStep
	{
		StepUserName,
		StepAnswer,
		StepSuccess
	}

	private static readonly object answerLookupErrorEvent = new object();

	private static readonly object sendingMailEvent = new object();

	private static readonly object sendMailErrorEvent = new object();

	private static readonly object userLookupErrorEvent = new object();

	private static readonly object verifyingAnswerEvent = new object();

	private static readonly object verifyingUserEvent = new object();

	/// <summary>Represents the command to perform when the Submit button is clicked.</summary>
	public static readonly string SubmitButtonCommandName = "Submit";

	private bool renderOuterTable = true;

	private TableItemStyle _failureTextStyle;

	private TableItemStyle _hyperLinkStyle;

	private TableItemStyle _instructionTextStyle;

	private TableItemStyle _labelStyle;

	private Style _submitButtonStyle;

	private TableItemStyle _successTextStyle;

	private Style _textBoxStyle;

	private TableItemStyle _titleTextStyle;

	private Style _validatorTextStyle;

	private MailDefinition _mailDefinition;

	private MembershipProvider _provider;

	private ITemplate _questionTemplate;

	private ITemplate _successTemplate;

	private ITemplate _userNameTemplate;

	private QuestionContainer _questionTemplateContainer;

	private SuccessContainer _successTemplateContainer;

	private UserNameContainer _userNameTemplateContainer;

	private PasswordReciveryStep _currentStep;

	private string _username;

	private string _answer;

	private EventHandlerList events = new EventHandlerList();

	/// <summary>Gets the answer to the password recovery confirmation question entered by the user.</summary>
	/// <returns>The answer to the password recovery confirmation question entered by the user.</returns>
	[Browsable(false)]
	[Filterable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Themeable(false)]
	public virtual string Answer
	{
		get
		{
			if (_answer == null)
			{
				return string.Empty;
			}
			return _answer;
		}
	}

	/// <summary>Gets or sets the label text for the password confirmation answer text box.</summary>
	/// <returns>The label for the password confirmation answer text box. The default is "Answer:" </returns>
	[Localizable(true)]
	public virtual string AnswerLabelText
	{
		get
		{
			return ViewState.GetString("AnswerLabelText", "Answer:");
		}
		set
		{
			ViewState["AnswerLabelText"] = value;
		}
	}

	/// <summary>Gets or sets the error message displayed to the user when the Answer text box is blank.</summary>
	/// <returns>The error message displayed when the Answer text box is empty. The default is "Answer." </returns>
	[Localizable(true)]
	public virtual string AnswerRequiredErrorMessage
	{
		get
		{
			return ViewState.GetString("AnswerRequiredErrorMessage", "Answer is required.");
		}
		set
		{
			ViewState["AnswerRequiredErrorMessage"] = value;
		}
	}

	/// <summary>Gets or sets the amount of padding inside the borders of the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>The amount of space (in pixels) between the contents of a <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control and the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control's border. The default value is 1.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Web.UI.WebControls.PasswordRecovery.BorderPadding" /> property is set to less than -1.</exception>
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

	/// <summary>Gets or sets the error message to display when there is a problem with the membership provider for the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>The error message displayed when the user's password will not be sent by e-mail because of a problem with the membership provider. The default is "Your attempt to retrieve your password has failed. Please try again." </returns>
	[Localizable(true)]
	public virtual string GeneralFailureText
	{
		get
		{
			return ViewState.GetString("GeneralFailureText", "Your attempt to retrieve your password was not successful. Please try again.");
		}
		set
		{
			ViewState["GeneralFailureText"] = value;
		}
	}

	/// <summary>Gets or sets the URL of an image to display next to the link to the Help page.</summary>
	/// <returns>The URL of an image to display next to the link to the Help page. The default value is an empty string ("").</returns>
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

	/// <summary>Gets or sets the text of the link to the password recovery Help page.</summary>
	/// <returns>The text of the link to the password recovery Help page. The default is <see cref="F:System.String.Empty" />.</returns>
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

	/// <summary>Gets or sets the URL of the password recovery Help page.</summary>
	/// <returns>The URL of the password recovery Help page. The default is <see cref="F:System.String.Empty" />.</returns>
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

	/// <summary>Gets a reference to a collection of properties that define the characteristics of e-mail messages used to send new or recovered passwords to users.</summary>
	/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.MailDefinition" /> that contains properties that define the characteristics of e-mail messages used to send users their passwords.</returns>
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[Themeable(false)]
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

	/// <summary>Gets or sets the membership provider used to look up user information.</summary>
	/// <returns>The membership provider used to look up user information. The default value is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Themeable(false)]
	public virtual string MembershipProvider
	{
		get
		{
			return ViewState.GetString("MembershipProvider", string.Empty);
		}
		set
		{
			ViewState["MembershipProvider"] = value;
		}
	}

	/// <summary>Gets the password recovery confirmation question established by the user on the Web site.</summary>
	/// <returns>The password recovery confirmation question. The default value is <see cref="F:System.String.Empty" />.</returns>
	[Browsable(false)]
	[Filterable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Themeable(false)]
	public virtual string Question
	{
		get
		{
			return ViewState.GetString("Question", "");
		}
		private set
		{
			ViewState["Question"] = value;
		}
	}

	/// <summary>Gets or sets the text to display when the user's answer to the password recovery confirmation question does not match the answer stored in the Web site data store.</summary>
	/// <returns>The text to display when the user's answer to the password recovery confirmation question does not match the answer stored in the Web site data store. The default value is "Your answer could not be verified. Please try again." </returns>
	[Localizable(true)]
	public virtual string QuestionFailureText
	{
		get
		{
			return ViewState.GetString("QuestionFailureText", "Your answer could not be verified. Please try again.");
		}
		set
		{
			ViewState["QuestionFailureText"] = value;
		}
	}

	/// <summary>Gets or sets the text to display in the Question view to instruct the user to answer the password recovery confirmation question.</summary>
	/// <returns>The instruction text to display in the Question view. The default is "Answer the following question to receive your password." </returns>
	[Localizable(true)]
	public virtual string QuestionInstructionText
	{
		get
		{
			return ViewState.GetString("QuestionInstructionText", "Answer the following question to receive your password.");
		}
		set
		{
			ViewState["QuestionInstructionText"] = value;
		}
	}

	/// <summary>Gets or sets the text of the label for the <see cref="P:System.Web.UI.WebControls.PasswordRecovery.Question" /> text box.</summary>
	/// <returns>The label for the <see cref="P:System.Web.UI.WebControls.PasswordRecovery.Question" /> text box. The default is "Question:" </returns>
	[Localizable(true)]
	public virtual string QuestionLabelText
	{
		get
		{
			return ViewState.GetString("QuestionLabelText", "Question:");
		}
		set
		{
			ViewState["QuestionLabelText"] = value;
		}
	}

	/// <summary>Gets or sets the title for the Question view of the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>The title for the Question view. The default is "Identity Confirmation".</returns>
	[Localizable(true)]
	public virtual string QuestionTitleText
	{
		get
		{
			return ViewState.GetString("QuestionTitleText", "Identity Confirmation");
		}
		set
		{
			ViewState["QuestionTitleText"] = value;
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

	/// <summary>Gets or sets the URL of an image to use as the Submit button.</summary>
	/// <returns>The URL of the image to use as the Submit button. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string SubmitButtonImageUrl
	{
		get
		{
			return ViewState.GetString("SubmitButtonImageUrl", string.Empty);
		}
		set
		{
			ViewState["SubmitButtonImageUrl"] = value;
		}
	}

	/// <summary>Gets or sets the text of the button that submits the form.</summary>
	/// <returns>The text of the button. The default is "Submit".</returns>
	[Localizable(true)]
	public virtual string SubmitButtonText
	{
		get
		{
			return ViewState.GetString("SubmitButtonText", "Submit");
		}
		set
		{
			ViewState["SubmitButtonText"] = value;
		}
	}

	/// <summary>Gets or sets the type of Submit button to use when rendering the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.PasswordRecovery.SubmitButtonType" /> property is not set to a valid <see cref="T:System.Web.UI.WebControls.ButtonType" /> value. </exception>
	[DefaultValue(ButtonType.Button)]
	public virtual ButtonType SubmitButtonType
	{
		get
		{
			object obj = ViewState["SubmitButtonType"];
			if (obj != null)
			{
				return (ButtonType)obj;
			}
			return ButtonType.Button;
		}
		set
		{
			if (value < ButtonType.Button || value > ButtonType.Link)
			{
				throw new ArgumentOutOfRangeException("SubmitButtonType");
			}
			ViewState["SubmitButtonType"] = (int)value;
		}
	}

	/// <summary>Gets or sets the URL of the page to display after sending a password successfully.</summary>
	/// <returns>The URL of the password success page. The default is <see cref="F:System.String.Empty" />.</returns>
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

	/// <summary>Gets or sets the text to display after sending a password successfully.</summary>
	/// <returns>The text to display when a password has been successfully sent. The default is "Your password has been sent to you." </returns>
	[Localizable(true)]
	public virtual string SuccessText
	{
		get
		{
			return ViewState.GetString("SuccessText", "Your password has been sent to you.");
		}
		set
		{
			ViewState["SuccessText"] = value;
		}
	}

	/// <summary>Gets or sets a value that specifies whether to display the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control in a horizontal or vertical layout.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.LoginTextLayout" /> enumeration values. The default is <see cref="F:System.Web.UI.WebControls.LoginTextLayout.TextOnLeft" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.PasswordRecovery.TextLayout" /> property is not set to a valid <see cref="T:System.Web.UI.WebControls.LoginTextLayout" /> enumeration value. </exception>
	[DefaultValue(LoginTextLayout.TextOnLeft)]
	public virtual LoginTextLayout TextLayout
	{
		get
		{
			object obj = ViewState["TextLayout"];
			if (obj != null)
			{
				return (LoginTextLayout)obj;
			}
			return LoginTextLayout.TextOnLeft;
		}
		set
		{
			if (value < LoginTextLayout.TextOnLeft || value > LoginTextLayout.TextOnTop)
			{
				throw new ArgumentOutOfRangeException("TextLayout");
			}
			ViewState["TextLayout"] = (int)value;
		}
	}

	/// <summary>Gets or sets the text that appears in the User Name text box.</summary>
	/// <returns>The user name entered by the user. The default value is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public virtual string UserName
	{
		get
		{
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

	/// <summary>Gets or sets the text displayed when the user name entered by the user is not a valid user name for the Web site.</summary>
	/// <returns>The text displayed when the user name entered by the user is not a valid user name for the Web site. The default is "We were unable to access your information. Please try again." </returns>
	[Localizable(true)]
	public virtual string UserNameFailureText
	{
		get
		{
			return ViewState.GetString("UserNameFailureText", "We were unable to access your information. Please try again.");
		}
		set
		{
			ViewState["UserNameFailureText"] = value;
		}
	}

	/// <summary>Gets or sets the text to display in the UserName view of the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control to instruct the user to enter a user name.</summary>
	/// <returns>The instruction text to display in the UserName view. The default is "Enter your user name to receive your password." </returns>
	[Localizable(true)]
	public virtual string UserNameInstructionText
	{
		get
		{
			return ViewState.GetString("UserNameInstructionText", "Enter your User Name to receive your password.");
		}
		set
		{
			ViewState["UserNameInstructionText"] = value;
		}
	}

	/// <summary>Gets or sets the text of the label for the User Name text box.</summary>
	/// <returns>The label for the User Name text box. The default is "User Name:".</returns>
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

	/// <summary>Gets or sets the error message displayed when a user leaves the User Name text box empty.</summary>
	/// <returns>The error message displayed when the User Name text box is empty. The default is "User Name".</returns>
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

	/// <summary>Gets or sets the title for the UserName view of the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>The title for the UserName view. The default is "Forgot Your Password?" </returns>
	[Localizable(true)]
	public virtual string UserNameTitleText
	{
		get
		{
			return ViewState.GetString("UserNameTitleText", "Forgot Your Password?");
		}
		set
		{
			ViewState["UserNameTitleText"] = value;
		}
	}

	/// <summary>Gets or sets the template used to display the Question view of the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control in Question view. The default is <see langword="null" />.</returns>
	[Browsable(false)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[TemplateContainer(typeof(PasswordRecovery))]
	public virtual ITemplate QuestionTemplate
	{
		get
		{
			return _questionTemplate;
		}
		set
		{
			_questionTemplate = value;
		}
	}

	/// <summary>Gets the container that a <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control used to create an instance of the <see cref="P:System.Web.UI.WebControls.PasswordRecovery.QuestionTemplate" /> template. This property provides programmatic access to child controls.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Control" /> that contains a <see cref="P:System.Web.UI.WebControls.PasswordRecovery.QuestionTemplate" /> template.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Control QuestionTemplateContainer
	{
		get
		{
			if (_questionTemplateContainer == null)
			{
				_questionTemplateContainer = new QuestionContainer(this);
				ITemplate questionTemplate = QuestionTemplate;
				if (questionTemplate != null)
				{
					_questionTemplateContainer.InstantiateTemplate(questionTemplate);
				}
			}
			return _questionTemplateContainer;
		}
	}

	/// <summary>Gets or sets the template used to display the Success view of the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control in Success view. The default is <see langword="null" />.</returns>
	[Browsable(false)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[TemplateContainer(typeof(PasswordRecovery))]
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

	/// <summary>Gets the container that a <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control used to create an instance of the <see cref="P:System.Web.UI.WebControls.PasswordRecovery.SuccessTemplate" /> template. This property provides programmatic access to child controls.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Control" /> that contains a <see cref="P:System.Web.UI.WebControls.PasswordRecovery.SuccessTemplate" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Control SuccessTemplateContainer
	{
		get
		{
			if (_successTemplateContainer == null)
			{
				_successTemplateContainer = new SuccessContainer(this);
				ITemplate successTemplate = SuccessTemplate;
				if (successTemplate != null)
				{
					_successTemplateContainer.InstantiateTemplate(successTemplate);
				}
			}
			return _successTemplateContainer;
		}
	}

	/// <summary>Gets or sets the template used to display the UserName view of the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control in UserName view. The default is <see langword="null" />.</returns>
	[Browsable(false)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[TemplateContainer(typeof(PasswordRecovery))]
	public virtual ITemplate UserNameTemplate
	{
		get
		{
			return _userNameTemplate;
		}
		set
		{
			_userNameTemplate = value;
		}
	}

	/// <summary>Gets the container that a <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control used to create an instance of the <see cref="P:System.Web.UI.WebControls.PasswordRecovery.UserNameTemplate" /> template. This property provides programmatic access to child controls.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Control" /> that contains a <see cref="P:System.Web.UI.WebControls.PasswordRecovery.UserNameTemplate" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Control UserNameTemplateContainer
	{
		get
		{
			if (_userNameTemplateContainer == null)
			{
				_userNameTemplateContainer = new UserNameContainer(this);
				ITemplate userNameTemplate = UserNameTemplate;
				if (userNameTemplate != null)
				{
					_userNameTemplateContainer.InstantiateTemplate(userNameTemplate);
				}
			}
			return _userNameTemplateContainer;
		}
	}

	/// <summary>Gets a reference to a collection of properties that define the appearance of error text in the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of error text.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
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

	/// <summary>Gets a reference to a collection of properties that define the appearance of hyperlinks on the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the settings that define the appearance of hyperlinks.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
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

	/// <summary>Gets a reference to a collection of style properties that define the appearance of explanatory text in the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of explanatory text.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
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

	/// <summary>Gets a reference to a collection of style properties that define the appearance of text box labels in the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of text box labels.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
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

	/// <summary>Gets a reference to a collection of properties that define the appearance of Submit buttons in the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that contains properties that define the appearance of the Submit buttons.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	public Style SubmitButtonStyle
	{
		get
		{
			if (_submitButtonStyle == null)
			{
				_submitButtonStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					_submitButtonStyle.TrackViewState();
				}
			}
			return _submitButtonStyle;
		}
	}

	/// <summary>Gets a reference to a collection of style properties that define the appearance of text displayed in the Success view of the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of text displayed in the Success view.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
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

	/// <summary>Gets a reference to a collection of style properties that define the appearance of text boxes in the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.Style" /> that contains properties that define the appearance of text boxes in the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	public Style TextBoxStyle
	{
		get
		{
			if (_textBoxStyle == null)
			{
				_textBoxStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					_textBoxStyle.TrackViewState();
				}
			}
			return _textBoxStyle;
		}
	}

	/// <summary>Gets a reference to a collection of style properties that define the appearance of title text that appears in the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of title text in the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
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

	/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of error messages that are associated with any input validation used by the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the appearance of error messages that are associated with any input validation used by the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control. The default is <see langword="null" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	public Style ValidatorTextStyle
	{
		get
		{
			if (_validatorTextStyle == null)
			{
				_validatorTextStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					_validatorTextStyle.TrackViewState();
				}
			}
			return _validatorTextStyle;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to a <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control. </summary>
	/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control. Always returns <see langword="HtmlTextWriterTag.Table." /></returns>
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

	/// <summary>Occurs when the user enters an incorrect answer to the password recovery confirmation question.</summary>
	public event EventHandler AnswerLookupError
	{
		add
		{
			events.AddHandler(answerLookupErrorEvent, value);
		}
		remove
		{
			events.RemoveHandler(answerLookupErrorEvent, value);
		}
	}

	/// <summary>Occurs before the user is sent a password in e-mail.</summary>
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

	/// <summary>Occurs when the SMTP Mail system throws an error while attempting to send an e-mail message.</summary>
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

	/// <summary>Occurs when the membership provider cannot find the user name entered by the user.</summary>
	public event EventHandler UserLookupError
	{
		add
		{
			events.AddHandler(userLookupErrorEvent, value);
		}
		remove
		{
			events.RemoveHandler(userLookupErrorEvent, value);
		}
	}

	/// <summary>Occurs when the user has submitted an answer to the password recovery confirmation question.</summary>
	public event LoginCancelEventHandler VerifyingAnswer
	{
		add
		{
			events.AddHandler(verifyingAnswerEvent, value);
		}
		remove
		{
			events.RemoveHandler(verifyingAnswerEvent, value);
		}
	}

	/// <summary>Occurs before the user name is validated by the membership provider.</summary>
	public event LoginCancelEventHandler VerifyingUser
	{
		add
		{
			events.AddHandler(verifyingUserEvent, value);
		}
		remove
		{
			events.RemoveHandler(verifyingUserEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> class.</summary>
	public PasswordRecovery()
	{
	}

	/// <summary>Creates the individual controls that make up the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
	protected internal override void CreateChildControls()
	{
		ITemplate userNameTemplate = UserNameTemplate;
		if (userNameTemplate == null)
		{
			userNameTemplate = new UserNameDefaultTemplate(this);
			((UserNameContainer)UserNameTemplateContainer).InstantiateTemplate(userNameTemplate);
		}
		ITemplate questionTemplate = QuestionTemplate;
		if (questionTemplate == null)
		{
			questionTemplate = new QuestionDefaultTemplate(this);
			((QuestionContainer)QuestionTemplateContainer).InstantiateTemplate(questionTemplate);
		}
		ITemplate successTemplate = SuccessTemplate;
		if (successTemplate == null)
		{
			successTemplate = new SuccessDefaultTemplate(this);
			((SuccessContainer)SuccessTemplateContainer).InstantiateTemplate(successTemplate);
		}
		Controls.AddAt(0, UserNameTemplateContainer);
		Controls.AddAt(1, QuestionTemplateContainer);
		Controls.AddAt(2, SuccessTemplateContainer);
		IEditableTextControl userNameTextBox = ((UserNameContainer)UserNameTemplateContainer).UserNameTextBox;
		if (userNameTextBox != null)
		{
			userNameTextBox.TextChanged += UserName_TextChanged;
		}
		userNameTextBox = ((QuestionContainer)QuestionTemplateContainer).AnswerTextBox;
		if (userNameTextBox != null)
		{
			userNameTextBox.TextChanged += Answer_TextChanged;
		}
	}

	/// <summary>Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered output.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		((QuestionContainer)QuestionTemplateContainer).UpdateChildControls();
		for (int i = 0; i < Controls.Count; i++)
		{
			if (Controls[i].Visible)
			{
				Controls[i].Render(writer);
			}
		}
	}

	/// <summary>Implements the base <see cref="M:System.Web.UI.Control.LoadControlState(System.Object)" /> method.</summary>
	/// <param name="savedState">An object that represents the control state to be restored.</param>
	protected internal override void LoadControlState(object savedState)
	{
		if (savedState != null)
		{
			object[] array = (object[])savedState;
			base.LoadControlState(array[0]);
			_currentStep = (PasswordReciveryStep)array[1];
			_username = (string)array[2];
		}
	}

	/// <summary>Saves any server control state changes that have occurred since the time the page was posted back to the server.</summary>
	/// <returns>Returns the server control's current state. If there is no state associated with the control, this method returns <see langword="null" />.</returns>
	protected internal override object SaveControlState()
	{
		object obj = base.SaveControlState();
		return new object[3] { obj, _currentStep, _username };
	}

	/// <summary>Implements the base <see cref="M:System.Web.UI.Control.TrackViewState" /> method.</summary>
	protected override void TrackViewState()
	{
		base.TrackViewState();
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
		if (_submitButtonStyle != null)
		{
			_submitButtonStyle.TrackViewState();
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

	/// <summary>Implements the base <see cref="M:System.Web.UI.Control.LoadViewState(System.Object)" /> method.</summary>
	/// <param name="savedState">An object that represents the control state to restore.</param>
	/// <exception cref="T:System.ArgumentException">The view state is invalid.</exception>
	protected override void LoadViewState(object savedState)
	{
		if (savedState != null)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				FailureTextStyle.LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				HyperLinkStyle.LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				InstructionTextStyle.LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				LabelStyle.LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				SubmitButtonStyle.LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				SuccessTextStyle.LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				TextBoxStyle.LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				TitleTextStyle.LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				ValidatorTextStyle.LoadViewState(array[9]);
			}
			if (array[10] != null)
			{
				((IStateManager)MailDefinition).LoadViewState(array[10]);
			}
		}
	}

	/// <summary>Saves any state that was modified after the <see cref="M:System.Web.UI.WebControls.Style.TrackViewState" /> method was invoked.</summary>
	/// <returns>An object that contains the current view state of the control; otherwise, if there is no view state associated with the control, <see langword="null" />.</returns>
	protected override object SaveViewState()
	{
		object[] array = new object[11]
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
			null
		};
		if (_failureTextStyle != null)
		{
			array[1] = _failureTextStyle.SaveViewState();
		}
		if (_hyperLinkStyle != null)
		{
			array[2] = _hyperLinkStyle.SaveViewState();
		}
		if (_instructionTextStyle != null)
		{
			array[3] = _instructionTextStyle.SaveViewState();
		}
		if (_labelStyle != null)
		{
			array[4] = _labelStyle.SaveViewState();
		}
		if (_submitButtonStyle != null)
		{
			array[5] = _submitButtonStyle.SaveViewState();
		}
		if (_successTextStyle != null)
		{
			array[6] = _successTextStyle.SaveViewState();
		}
		if (_textBoxStyle != null)
		{
			array[7] = _textBoxStyle.SaveViewState();
		}
		if (_titleTextStyle != null)
		{
			array[8] = _titleTextStyle.SaveViewState();
		}
		if (_validatorTextStyle != null)
		{
			array[9] = _validatorTextStyle.SaveViewState();
		}
		if (_mailDefinition != null)
		{
			array[10] = ((IStateManager)_mailDefinition).SaveViewState();
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

	private void ProcessCommand(CommandEventArgs args)
	{
		if (Page.IsValid)
		{
			switch (_currentStep)
			{
			case PasswordReciveryStep.StepUserName:
				ProcessUserName();
				break;
			case PasswordReciveryStep.StepAnswer:
				ProcessUserAnswer();
				break;
			}
		}
	}

	private void ProcessUserName()
	{
		LoginCancelEventArgs loginCancelEventArgs = new LoginCancelEventArgs();
		OnVerifyingUser(loginCancelEventArgs);
		if (!loginCancelEventArgs.Cancel)
		{
			MembershipUser user = MembershipProviderInternal.GetUser(UserName, userIsOnline: false);
			if (user == null)
			{
				OnUserLookupError(EventArgs.Empty);
				((UserNameContainer)UserNameTemplateContainer).FailureTextLiteral.Text = UserNameFailureText;
			}
			else if (!MembershipProviderInternal.RequiresQuestionAndAnswer)
			{
				GenerateAndSendEmail();
				_currentStep = PasswordReciveryStep.StepSuccess;
			}
			else
			{
				Question = user.PasswordQuestion;
				_currentStep = PasswordReciveryStep.StepAnswer;
			}
		}
	}

	private void ProcessUserAnswer()
	{
		LoginCancelEventArgs loginCancelEventArgs = new LoginCancelEventArgs();
		OnVerifyingAnswer(loginCancelEventArgs);
		if (!loginCancelEventArgs.Cancel)
		{
			MembershipUser user = MembershipProviderInternal.GetUser(UserName, userIsOnline: false);
			if (user == null || string.IsNullOrEmpty(user.Email))
			{
				((QuestionContainer)QuestionTemplateContainer).FailureTextLiteral.Text = GeneralFailureText;
				return;
			}
			GenerateAndSendEmail();
			_currentStep = PasswordReciveryStep.StepSuccess;
		}
	}

	private void GenerateAndSendEmail()
	{
		string text = "";
		try
		{
			if (MembershipProviderInternal.EnablePasswordRetrieval)
			{
				text = MembershipProviderInternal.GetPassword(UserName, Answer);
			}
			else
			{
				if (!MembershipProviderInternal.EnablePasswordReset)
				{
					throw new HttpException("Membership provider does not support password retrieval or reset.");
				}
				text = MembershipProviderInternal.ResetPassword(UserName, Answer);
			}
		}
		catch (MembershipPasswordException)
		{
			OnAnswerLookupError(EventArgs.Empty);
			((QuestionContainer)QuestionTemplateContainer).FailureTextLiteral.Text = QuestionFailureText;
			return;
		}
		SendPasswordByMail(UserName, text);
	}

	private void InitMemberShipProvider()
	{
		string membershipProvider = MembershipProvider;
		_provider = ((membershipProvider.Length == 0) ? (_provider = Membership.Provider) : Membership.Providers[membershipProvider]);
		if (_provider == null)
		{
			throw new HttpException(Locale.GetText("No provider named '{0}' could be found.", membershipProvider));
		}
	}

	private void SendPasswordByMail(string username, string password)
	{
		MembershipUser user = MembershipProviderInternal.GetUser(UserName, userIsOnline: false);
		if (user == null)
		{
			return;
		}
		string body = "Please return to the site and log in using the following information.\nUser Name: <%USERNAME%>\nPassword: <%PASSWORD%>\n";
		ListDictionary listDictionary = new ListDictionary(StringComparer.OrdinalIgnoreCase);
		listDictionary.Add("<%USERNAME%>", username);
		listDictionary.Add("<% UserName %>", username);
		listDictionary.Add("<%PASSWORD%>", password);
		listDictionary.Add("<% Password %>", password);
		MailMessage mailMessage = null;
		mailMessage = ((MailDefinition.BodyFileName.Length != 0) ? MailDefinition.CreateMailMessage(user.Email, listDictionary, this) : MailDefinition.CreateMailMessage(user.Email, listDictionary, body, this));
		if (string.IsNullOrEmpty(mailMessage.Subject))
		{
			mailMessage.Subject = "Password";
		}
		MailMessageEventArgs e = new MailMessageEventArgs(mailMessage);
		OnSendingMail(e);
		SmtpClient smtpClient = new SmtpClient();
		try
		{
			smtpClient.Send(mailMessage);
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

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.PasswordRecovery.AnswerLookupError" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnAnswerLookupError(EventArgs e)
	{
	}

	/// <summary>Determines whether the event for the server control is passed up the page's UI server control hierarchy.</summary>
	/// <param name="source">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	/// <returns>
	///     <see langword="true" /> if the event has been canceled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	protected override bool OnBubbleEvent(object source, EventArgs e)
	{
		CommandEventArgs commandEventArgs = e as CommandEventArgs;
		if (e != null && commandEventArgs.CommandName == SubmitButtonCommandName)
		{
			ProcessCommand(commandEventArgs);
			return true;
		}
		return base.OnBubbleEvent(source, e);
	}

	/// <summary>Implements the base <see cref="M:System.Web.UI.Control.OnInit(System.EventArgs)" /> method.</summary>
	/// <param name="e">The event data.</param>
	protected internal override void OnInit(EventArgs e)
	{
		Page.RegisterRequiresControlState(this);
		base.OnInit(e);
	}

	/// <summary>Implements the base <see cref="M:System.Web.UI.Control.OnPreRender(System.EventArgs)" /> method.</summary>
	/// <param name="e">The event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		UserNameTemplateContainer.Visible = false;
		QuestionTemplateContainer.Visible = false;
		SuccessTemplateContainer.Visible = false;
		switch (_currentStep)
		{
		case PasswordReciveryStep.StepUserName:
			UserNameTemplateContainer.Visible = true;
			break;
		case PasswordReciveryStep.StepAnswer:
			QuestionTemplateContainer.Visible = true;
			break;
		case PasswordReciveryStep.StepSuccess:
			SuccessTemplateContainer.Visible = true;
			break;
		}
		base.OnPreRender(e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.PasswordRecovery.SendingMail" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.MailMessageEventArgs" /> that contains the event data. </param>
	protected virtual void OnSendingMail(MailMessageEventArgs e)
	{
		if (events[sendingMailEvent] is MailMessageEventHandler mailMessageEventHandler)
		{
			mailMessageEventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.PasswordRecovery.SendMailError" /> event when an e-mail message cannot be sent to the user.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SendMailErrorEventArgs" /> that contains the event data.</param>
	protected virtual void OnSendMailError(SendMailErrorEventArgs e)
	{
		if (events[sendingMailEvent] is SendMailErrorEventHandler sendMailErrorEventHandler)
		{
			sendMailErrorEventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.PasswordRecovery.UserLookupError" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnUserLookupError(EventArgs e)
	{
		if (events[userLookupErrorEvent] is EventHandler eventHandler)
		{
			eventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.PasswordRecovery.VerifyingAnswer" /> event.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
	protected virtual void OnVerifyingAnswer(LoginCancelEventArgs e)
	{
		if (events[verifyingAnswerEvent] is LoginCancelEventHandler loginCancelEventHandler)
		{
			loginCancelEventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.PasswordRecovery.VerifyingUser" /> event.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
	protected virtual void OnVerifyingUser(LoginCancelEventArgs e)
	{
		if (events[verifyingUserEvent] is LoginCancelEventHandler loginCancelEventHandler)
		{
			loginCancelEventHandler(this, e);
		}
	}

	private void UserName_TextChanged(object sender, EventArgs e)
	{
		UserName = ((ITextControl)sender).Text;
	}

	private void Answer_TextChanged(object sender, EventArgs e)
	{
		_answer = ((ITextControl)sender).Text;
	}

	/// <summary>Implements the base <see cref="M:System.Web.UI.Control.System#Web#UI#IControlDesignerAccessor#SetDesignModeState(System.Collections.IDictionary)" /> method.</summary>
	/// <param name="data">The design-time data for the control.</param>
	[MonoTODO("Not implemented")]
	protected override void SetDesignModeState(IDictionary data)
	{
		throw new NotImplementedException();
	}
}
