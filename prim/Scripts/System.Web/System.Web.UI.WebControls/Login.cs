using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web.Security;
using System.Web.Util;

namespace System.Web.UI.WebControls;

/// <summary>Provides user interface (UI) elements for logging in to a Web site.</summary>
[Bindable(false)]
[DefaultEvent("Authenticate")]
[Designer("System.Web.UI.Design.WebControls.LoginDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class Login : CompositeControl, IRenderOuterTable
{
	private sealed class LoginContainer : Control
	{
		private readonly Login _owner;

		private bool renderOuterTable;

		private Table _table;

		private TableCell _containerCell;

		public override string ID
		{
			get
			{
				return _owner.ID;
			}
			set
			{
				_owner.ID = value;
			}
		}

		public override string ClientID => _owner.ClientID;

		public Control UserNameTextBox => FindControl("UserName");

		public Control PasswordTextBox => FindControl("Password");

		public Control RememberMeCheckBox => FindControl("RememberMe");

		public ITextControl FailureTextLiteral => FindControl("FailureText") as ITextControl;

		public LoginContainer(Login owner)
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
			_containerCell = new TableCell();
			TableRow tableRow = new TableRow();
			tableRow.Cells.Add(_containerCell);
			_table.Rows.Add(tableRow);
			Controls.AddAt(0, _table);
		}

		protected internal override void Render(HtmlTextWriter writer)
		{
			if (_table != null)
			{
				_table.CellSpacing = 0;
				_table.CellPadding = _owner.BorderPadding;
				_table.ApplyStyle(_owner.ControlStyle);
				_table.Attributes.CopyFrom(_owner.Attributes);
			}
			base.Render(writer);
		}
	}

	private sealed class LoginTemplate : WebControl, ITemplate
	{
		private readonly Login _login;

		public LoginTemplate(Login login)
		{
			_login = login;
		}

		void ITemplate.InstantiateIn(Control container)
		{
			LiteralControl c = new LiteralControl(_login.TitleText);
			LiteralControl c2 = new LiteralControl(_login.InstructionText);
			TextBox textBox = new TextBox();
			textBox.ID = "UserName";
			textBox.Text = _login.UserName;
			_login.RegisterApplyStyle(textBox, _login.TextBoxStyle);
			Label label = new Label();
			label.ID = "UserNameLabel";
			label.AssociatedControlID = "UserName";
			label.Text = _login.UserNameLabelText;
			RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
			requiredFieldValidator.ID = "UserNameRequired";
			requiredFieldValidator.ControlToValidate = "UserName";
			requiredFieldValidator.ErrorMessage = _login.UserNameRequiredErrorMessage;
			requiredFieldValidator.ToolTip = _login.UserNameRequiredErrorMessage;
			requiredFieldValidator.Text = "*";
			requiredFieldValidator.ValidationGroup = _login.ID;
			_login.RegisterApplyStyle(requiredFieldValidator, _login.ValidatorTextStyle);
			TextBox textBox2 = new TextBox();
			textBox2.ID = "Password";
			textBox2.TextMode = TextBoxMode.Password;
			_login.RegisterApplyStyle(textBox2, _login.TextBoxStyle);
			Label label2 = new Label();
			label2.ID = "PasswordLabel";
			label2.AssociatedControlID = "PasswordLabel";
			label2.Text = _login.PasswordLabelText;
			RequiredFieldValidator requiredFieldValidator2 = new RequiredFieldValidator();
			requiredFieldValidator2.ID = "PasswordRequired";
			requiredFieldValidator2.ControlToValidate = "Password";
			requiredFieldValidator2.ErrorMessage = _login.PasswordRequiredErrorMessage;
			requiredFieldValidator2.ToolTip = _login.PasswordRequiredErrorMessage;
			requiredFieldValidator2.Text = "*";
			requiredFieldValidator2.ValidationGroup = _login.ID;
			_login.RegisterApplyStyle(requiredFieldValidator2, _login.ValidatorTextStyle);
			bool flag = _login == null || _login.DisplayRememberMe;
			CheckBox checkBox;
			if (flag)
			{
				checkBox = new CheckBox();
				checkBox.ID = "RememberMe";
				checkBox.Checked = _login.RememberMeSet;
				checkBox.Text = _login.RememberMeText;
				_login.RegisterApplyStyle(checkBox, _login.CheckBoxStyle);
			}
			else
			{
				checkBox = null;
			}
			Literal literal = new Literal();
			literal.ID = "FailureText";
			literal.EnableViewState = false;
			WebControl webControl = null;
			switch (_login.LoginButtonType)
			{
			case ButtonType.Button:
				webControl = new Button();
				webControl.ID = "LoginButton";
				break;
			case ButtonType.Link:
				webControl = new LinkButton();
				webControl.ID = "LoginLinkButton";
				break;
			case ButtonType.Image:
				webControl = new ImageButton();
				webControl.ID = "LoginImageButton";
				break;
			}
			_login.RegisterApplyStyle(webControl, _login.LoginButtonStyle);
			webControl.ID = "LoginButton";
			((IButtonControl)webControl).Text = _login.LoginButtonText;
			((IButtonControl)webControl).CommandName = LoginButtonCommandName;
			((IButtonControl)webControl).Command += _login.LoginClick;
			((IButtonControl)webControl).ValidationGroup = _login.ID;
			Table table = new Table();
			table.CellPadding = 0;
			table.Rows.Add(CreateRow(CreateCell(c, null, _login.TitleTextStyle, HorizontalAlign.Center)));
			if (_login.InstructionText.Length > 0)
			{
				table.Rows.Add(CreateRow(CreateCell(c2, null, _login.instructionTextStyle, HorizontalAlign.Center)));
			}
			if (_login.Orientation == Orientation.Horizontal)
			{
				TableRow tableRow = new TableRow();
				TableRow tableRow2 = new TableRow();
				if (_login.TextLayout == LoginTextLayout.TextOnTop)
				{
					tableRow.Cells.Add(CreateCell(label, null, _login.LabelStyle));
				}
				else
				{
					tableRow2.Cells.Add(CreateCell(label, null, _login.LabelStyle));
				}
				tableRow2.Cells.Add(CreateCell(textBox, requiredFieldValidator, null));
				if (_login.TextLayout == LoginTextLayout.TextOnTop)
				{
					tableRow.Cells.Add(CreateCell(label2, null, _login.LabelStyle));
				}
				else
				{
					tableRow2.Cells.Add(CreateCell(label2, null, _login.LabelStyle));
				}
				tableRow2.Cells.Add(CreateCell(textBox2, requiredFieldValidator2, null));
				if (flag)
				{
					tableRow2.Cells.Add(CreateCell(checkBox, null, null));
				}
				tableRow2.Cells.Add(CreateCell(webControl, null, null));
				if (tableRow.Cells.Count > 0)
				{
					table.Rows.Add(tableRow);
				}
				table.Rows.Add(tableRow2);
			}
			else
			{
				if (_login.TextLayout == LoginTextLayout.TextOnLeft)
				{
					table.Rows.Add(CreateRow(label, textBox, requiredFieldValidator, _login.LabelStyle));
				}
				else
				{
					table.Rows.Add(CreateRow(label, null, null, _login.LabelStyle));
					table.Rows.Add(CreateRow(null, textBox, requiredFieldValidator, null));
				}
				if (_login.TextLayout == LoginTextLayout.TextOnLeft)
				{
					table.Rows.Add(CreateRow(label2, textBox2, requiredFieldValidator2, _login.LabelStyle));
				}
				else
				{
					table.Rows.Add(CreateRow(label2, null, null, _login.LabelStyle));
					table.Rows.Add(CreateRow(null, textBox2, requiredFieldValidator2, null));
				}
				if (flag)
				{
					table.Rows.Add(CreateRow(CreateCell(checkBox, null, null)));
				}
				table.Rows.Add(CreateRow(CreateCell(webControl, null, null, HorizontalAlign.Right)));
			}
			if (_login.FailureTextStyle.ForeColor.IsEmpty)
			{
				_login.FailureTextStyle.ForeColor = Color.Red;
			}
			table.Rows.Add(CreateRow(CreateCell(literal, null, _login.FailureTextStyle)));
			TableCell tableCell = new TableCell();
			_login.RegisterApplyStyle(tableCell, _login.HyperLinkStyle);
			if (AddLink(_login.CreateUserUrl, _login.CreateUserText, _login.CreateUserIconUrl, tableCell, _login.HyperLinkStyle))
			{
				if (_login.Orientation == Orientation.Vertical)
				{
					tableCell.Controls.Add(new LiteralControl("<br/>"));
				}
				else
				{
					tableCell.Controls.Add(new LiteralControl(" "));
				}
			}
			if (AddLink(_login.PasswordRecoveryUrl, _login.PasswordRecoveryText, _login.PasswordRecoveryIconUrl, tableCell, _login.HyperLinkStyle))
			{
				if (_login.Orientation == Orientation.Vertical)
				{
					tableCell.Controls.Add(new LiteralControl("<br/>"));
				}
				else
				{
					tableCell.Controls.Add(new LiteralControl(" "));
				}
			}
			AddLink(_login.HelpPageUrl, _login.HelpPageText, _login.HelpPageIconUrl, tableCell, _login.HyperLinkStyle);
			table.Rows.Add(CreateRow(tableCell));
			FixTableColumnSpans(table);
			container.Controls.Add(table);
		}

		private TableRow CreateRow(TableCell cell)
		{
			return new TableRow
			{
				Cells = { cell }
			};
		}

		private TableRow CreateRow(Control c0, Control c1, Control c2, Style s)
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			TableCell tableCell2 = new TableCell();
			if (c0 != null)
			{
				tableCell.Controls.Add(c0);
				tableRow.Controls.Add(tableCell);
			}
			if (s != null)
			{
				tableCell.ApplyStyle(s);
			}
			if (c1 != null && c2 != null)
			{
				tableCell2.Controls.Add(c1);
				tableCell2.Controls.Add(c2);
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Controls.Add(tableCell2);
			}
			return tableRow;
		}

		private TableCell CreateCell(Control c0, Control c1, Style s, HorizontalAlign align)
		{
			TableCell tableCell = CreateCell(c0, c1, s);
			tableCell.HorizontalAlign = align;
			return tableCell;
		}

		private TableCell CreateCell(Control c0, Control c1, Style s)
		{
			TableCell tableCell = new TableCell();
			if (s != null)
			{
				tableCell.ApplyStyle(s);
			}
			tableCell.Controls.Add(c0);
			if (c1 != null)
			{
				tableCell.Controls.Add(c1);
			}
			return tableCell;
		}

		private bool AddLink(string pageUrl, string linkText, string linkIcon, WebControl container, Style style)
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
				_login.RegisterApplyStyle(hyperLink, style);
				container.Controls.Add(hyperLink);
				result = true;
			}
			return result;
		}

		private void FixTableColumnSpans(Table table)
		{
			int num = 0;
			for (int i = 0; i < table.Rows.Count; i++)
			{
				if (num < table.Rows[i].Cells.Count)
				{
					num = table.Rows[i].Cells.Count;
				}
			}
			for (int j = 0; j < table.Rows.Count; j++)
			{
				if (table.Rows[j].Cells.Count == 1 && num > 1)
				{
					table.Rows[j].Cells[0].ColumnSpan = num;
				}
			}
		}
	}

	/// <summary>Represents the command name associated with the login button.</summary>
	public static readonly string LoginButtonCommandName = "Login";

	private static readonly object authenticateEvent = new object();

	private static readonly object loggedInEvent = new object();

	private static readonly object loggingInEvent = new object();

	private static readonly object loginErrorEvent = new object();

	private TableItemStyle checkBoxStyle;

	private TableItemStyle failureTextStyle;

	private TableItemStyle hyperLinkStyle;

	private TableItemStyle instructionTextStyle;

	private TableItemStyle labelStyle;

	private Style logonButtonStyle;

	private Style textBoxStyle;

	private TableItemStyle titleTextStyle;

	private Style validatorTextStyle;

	private ArrayList styles = new ArrayList();

	private ITemplate layoutTemplate;

	private LoginContainer container;

	private string _password;

	private bool renderOuterTable = true;

	/// <summary>Gets or sets the amount of padding inside the borders of the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
	/// <returns>The amount of space (in pixels) between the contents of a <see cref="T:System.Web.UI.WebControls.Login" /> control and the <see cref="T:System.Web.UI.WebControls.Login" /> control's border. The default value is <see langword="1" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.Login.BorderPadding" /> property is set to a value less than -1.</exception>
	[DefaultValue(1)]
	public virtual int BorderPadding
	{
		get
		{
			object obj = ViewState["BorderPadding"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 1;
		}
		set
		{
			if (value < -1)
			{
				throw new ArgumentOutOfRangeException("BorderPadding", "< -1");
			}
			ViewState["BorderPadding"] = value;
		}
	}

	/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the Remember Me check box.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the <see cref="T:System.Web.UI.WebControls.Login" /> control's Remember Me check box.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public TableItemStyle CheckBoxStyle
	{
		get
		{
			if (checkBoxStyle == null)
			{
				checkBoxStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					((IStateManager)checkBoxStyle).TrackViewState();
				}
			}
			return checkBoxStyle;
		}
	}

	/// <summary>Gets the location of an image to display next to the link to a registration page for new users.</summary>
	/// <returns>The URL of the image to display. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	public virtual string CreateUserIconUrl
	{
		get
		{
			object obj = ViewState["CreateUserIconUrl"];
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
				ViewState.Remove("CreateUserIconUrl");
			}
			else
			{
				ViewState["CreateUserIconUrl"] = value;
			}
		}
	}

	/// <summary>Gets or sets the text of a link to a registration page for new users.</summary>
	/// <returns>The text of the link to the new-user registration page. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public virtual string CreateUserText
	{
		get
		{
			object obj = ViewState["CreateUserText"];
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
				ViewState.Remove("CreateUserText");
			}
			else
			{
				ViewState["CreateUserText"] = value;
			}
		}
	}

	/// <summary>Gets or sets the URL of the new-user registration page.</summary>
	/// <returns>The URL of the new-user registration page. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	public virtual string CreateUserUrl
	{
		get
		{
			object obj = ViewState["CreateUserUrl"];
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
				ViewState.Remove("CreateUserUrl");
			}
			else
			{
				ViewState["CreateUserUrl"] = value;
			}
		}
	}

	/// <summary>Gets or sets the URL of the page displayed to the user when a login attempt is successful.</summary>
	/// <returns>The URL of the page the user is redirected to when a login attempt is successful. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Themeable(false)]
	[UrlProperty]
	public virtual string DestinationPageUrl
	{
		get
		{
			object obj = ViewState["DestinationPageUrl"];
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
				ViewState.Remove("DestinationPageUrl");
			}
			else
			{
				ViewState["DestinationPageUrl"] = value;
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether to display a check box to enable the user to control whether a persistent cookie is sent to their browser.</summary>
	/// <returns>
	///     <see langword="true" /> to display the check box; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[Themeable(false)]
	public virtual bool DisplayRememberMe
	{
		get
		{
			object obj = ViewState["DisplayRememberMe"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return true;
		}
		set
		{
			ViewState["DisplayRememberMe"] = value;
		}
	}

	/// <summary>Gets or sets the action that occurs when a login attempt fails.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.LoginFailureAction" /> enumeration values. The default is <see cref="F:System.Web.UI.WebControls.LoginFailureAction.Refresh" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.LoginFailureAction" /> enumeration values.</exception>
	[DefaultValue(LoginFailureAction.Refresh)]
	[Themeable(false)]
	[MonoTODO("RedirectToLoginPage not yet implemented in FormsAuthentication")]
	public virtual LoginFailureAction FailureAction
	{
		get
		{
			object obj = ViewState["FailureAction"];
			if (obj != null)
			{
				return (LoginFailureAction)obj;
			}
			return LoginFailureAction.Refresh;
		}
		set
		{
			if (value < LoginFailureAction.Refresh || value > LoginFailureAction.RedirectToLoginPage)
			{
				throw new ArgumentOutOfRangeException("FailureAction");
			}
			ViewState["FailureAction"] = (int)value;
		}
	}

	/// <summary>Gets or sets the text displayed when a login attempt fails.</summary>
	/// <returns>The text to display to the user when a login attempt fails. The default is "Your login attempt has failed. Please try again." </returns>
	[Localizable(true)]
	public virtual string FailureText
	{
		get
		{
			object obj = ViewState["FailureText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Your login attempt was not successful. Please try again.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("FailureText");
			}
			else
			{
				ViewState["FailureText"] = value;
			}
		}
	}

	/// <summary>Gets a reference to a collection of properties that define the appearance of error text in the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of error text.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public TableItemStyle FailureTextStyle
	{
		get
		{
			if (failureTextStyle == null)
			{
				failureTextStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					((IStateManager)failureTextStyle).TrackViewState();
				}
			}
			return failureTextStyle;
		}
	}

	/// <summary>Gets the location of an image to display next to the link to the login Help page.</summary>
	/// <returns>The URL of the image to display. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	public virtual string HelpPageIconUrl
	{
		get
		{
			object obj = ViewState["HelpPageIconUrl"];
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
				ViewState.Remove("HelpPageIconUrl");
			}
			else
			{
				ViewState["HelpPageIconUrl"] = value;
			}
		}
	}

	/// <summary>Gets or sets the text of a link to the login Help page.</summary>
	/// <returns>The text of the link to the login Help page. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public virtual string HelpPageText
	{
		get
		{
			object obj = ViewState["HelpPageText"];
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
				ViewState.Remove("HelpPageText");
			}
			else
			{
				ViewState["HelpPageText"] = value;
			}
		}
	}

	/// <summary>Gets or sets the URL of the login Help page.</summary>
	/// <returns>The URL of the login Help page. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	public virtual string HelpPageUrl
	{
		get
		{
			object obj = ViewState["HelpPageUrl"];
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
				ViewState.Remove("HelpPageUrl");
			}
			else
			{
				ViewState["HelpPageUrl"] = value;
			}
		}
	}

	/// <summary>Gets a reference to a collection of properties that define the appearance of hyperlinks in the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of hyperlinks.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public TableItemStyle HyperLinkStyle
	{
		get
		{
			if (hyperLinkStyle == null)
			{
				hyperLinkStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					((IStateManager)hyperLinkStyle).TrackViewState();
				}
			}
			return hyperLinkStyle;
		}
	}

	/// <summary>Gets or sets login instruction text for the user.</summary>
	/// <returns>The login instruction text to display to the user. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public virtual string InstructionText
	{
		get
		{
			object obj = ViewState["InstructionText"];
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
				ViewState.Remove("InstructionText");
			}
			else
			{
				ViewState["InstructionText"] = value;
			}
		}
	}

	/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that defines the settings for instruction text in the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style settings of the <see cref="T:System.Web.UI.WebControls.Login" /> control instruction text.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public TableItemStyle InstructionTextStyle
	{
		get
		{
			if (instructionTextStyle == null)
			{
				instructionTextStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					((IStateManager)instructionTextStyle).TrackViewState();
				}
			}
			return instructionTextStyle;
		}
	}

	/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that defines the settings for <see cref="T:System.Web.UI.WebControls.Login" /> control labels.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that defines the style settings of the <see cref="T:System.Web.UI.WebControls.Login" /> control labels.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public TableItemStyle LabelStyle
	{
		get
		{
			if (labelStyle == null)
			{
				labelStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					((IStateManager)labelStyle).TrackViewState();
				}
			}
			return labelStyle;
		}
	}

	/// <summary>Gets or sets the template used to display the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the <see cref="T:System.Web.UI.WebControls.Login" /> control. The default value is <see langword="null" />.</returns>
	[Browsable(false)]
	[TemplateContainer(typeof(Login))]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public virtual ITemplate LayoutTemplate
	{
		get
		{
			return layoutTemplate;
		}
		set
		{
			layoutTemplate = value;
		}
	}

	/// <summary>Gets or sets the URL of an image to use for the login button.</summary>
	/// <returns>The URL of the image used for the login button. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	public virtual string LoginButtonImageUrl
	{
		get
		{
			object obj = ViewState["LoginButtonImageUrl"];
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
				ViewState.Remove("LoginButtonImageUrl");
			}
			else
			{
				ViewState["LoginButtonImageUrl"] = value;
			}
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.Style" /> object that allows you to set the appearance of the login button in the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
	/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style of the login button.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public Style LoginButtonStyle
	{
		get
		{
			if (logonButtonStyle == null)
			{
				logonButtonStyle = new Style();
				if (base.IsTrackingViewState)
				{
					((IStateManager)logonButtonStyle).TrackViewState();
				}
			}
			return logonButtonStyle;
		}
	}

	/// <summary>Gets or sets the text for the <see cref="T:System.Web.UI.WebControls.Login" /> control's login button.</summary>
	/// <returns>The text used for the <see cref="T:System.Web.UI.WebControls.Login" /> control's login button. The default is "Login".</returns>
	[Localizable(true)]
	public virtual string LoginButtonText
	{
		get
		{
			object obj = ViewState["LoginButtonText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Log In");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("LoginButtonText");
			}
			else
			{
				ViewState["LoginButtonText"] = value;
			}
		}
	}

	/// <summary>Gets or sets the type of button to use when rendering the <see cref="T:System.Web.UI.WebControls.Login" /> button.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> enumeration values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.Login.LoginButtonType" /> property is not set to a valid <see cref="T:System.Web.UI.WebControls.ButtonType" /> enumeration value. </exception>
	[DefaultValue(ButtonType.Button)]
	public virtual ButtonType LoginButtonType
	{
		get
		{
			object obj = ViewState["LoginButtonType"];
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
				throw new ArgumentOutOfRangeException("LoginButtonType");
			}
			ViewState["LoginButtonType"] = (int)value;
		}
	}

	/// <summary>Gets or sets the name of the membership data provider used by the control.</summary>
	/// <returns>The name of the membership data provider used by the control. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Themeable(false)]
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
		}
	}

	/// <summary>Gets or sets a value that specifies the position of the elements of the <see cref="T:System.Web.UI.WebControls.Login" /> control on the page.</summary>
	/// <returns>One the <see cref="T:System.Web.UI.WebControls.Orientation" /> enumeration values. The default is <see cref="F:System.Web.UI.WebControls.Orientation.Vertical" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.Login.Orientation" /> property is not set to a valid <see cref="T:System.Web.UI.WebControls.Orientation" /> enumeration value. </exception>
	[DefaultValue(Orientation.Vertical)]
	public virtual Orientation Orientation
	{
		get
		{
			object obj = ViewState["Orientation"];
			if (obj != null)
			{
				return (Orientation)obj;
			}
			return Orientation.Vertical;
		}
		set
		{
			if (value < Orientation.Horizontal || value > Orientation.Vertical)
			{
				throw new ArgumentOutOfRangeException("Orientation");
			}
			ViewState["Orientation"] = (int)value;
		}
	}

	/// <summary>Gets the password entered by the user.</summary>
	/// <returns>The password entered by the user. The default is <see langword="null" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual string Password
	{
		get
		{
			if (_password == null)
			{
				return string.Empty;
			}
			return _password;
		}
	}

	/// <summary>Gets or sets the text of the label for the <see cref="P:System.Web.UI.WebControls.Login.Password" /> text box.</summary>
	/// <returns>The text of the label for the <see cref="P:System.Web.UI.WebControls.Login.Password" /> text box. The default is "Password:".</returns>
	[Localizable(true)]
	public virtual string PasswordLabelText
	{
		get
		{
			object obj = ViewState["PasswordLabelText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "Password:";
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("PasswordLabelText");
			}
			else
			{
				ViewState["PasswordLabelText"] = value;
			}
		}
	}

	/// <summary>Gets the location of an image to display next to the link to the password recovery page.</summary>
	/// <returns>The URL of the image to display. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	public virtual string PasswordRecoveryIconUrl
	{
		get
		{
			object obj = ViewState["PasswordRecoveryIconUrl"];
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
				ViewState.Remove("PasswordRecoveryIconUrl");
			}
			else
			{
				ViewState["PasswordRecoveryIconUrl"] = value;
			}
		}
	}

	/// <summary>Gets or sets the text of a link to the password recovery page.</summary>
	/// <returns>The text of the link to the password recovery page. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public virtual string PasswordRecoveryText
	{
		get
		{
			object obj = ViewState["PasswordRecoveryText"];
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
				ViewState.Remove("PasswordRecoveryText");
			}
			else
			{
				ViewState["PasswordRecoveryText"] = value;
			}
		}
	}

	/// <summary>Gets or sets the URL of the password recovery page.</summary>
	/// <returns>The URL of the password recovery page. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	public virtual string PasswordRecoveryUrl
	{
		get
		{
			object obj = ViewState["PasswordRecoveryUrl"];
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
				ViewState.Remove("PasswordRecoveryUrl");
			}
			else
			{
				ViewState["PasswordRecoveryUrl"] = value;
			}
		}
	}

	/// <summary>Gets or sets the error message to display in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when the password field is left blank.</summary>
	/// <returns>The error message to display in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when the password field is left blank. The default is "Password." </returns>
	[Localizable(true)]
	public virtual string PasswordRequiredErrorMessage
	{
		get
		{
			object obj = ViewState["PasswordRequiredErrorMessage"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Password is required.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("PasswordRequiredErrorMessage");
			}
			else
			{
				ViewState["PasswordRequiredErrorMessage"] = value;
			}
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

	/// <summary>Gets or sets a value indicating whether to send a persistent authentication cookie to the user's browser.</summary>
	/// <returns>
	///     <see langword="true" /> to send a persistent authentication cookie; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[Themeable(false)]
	public virtual bool RememberMeSet
	{
		get
		{
			object obj = ViewState["RememberMeSet"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return false;
		}
		set
		{
			ViewState["RememberMeSet"] = value;
		}
	}

	/// <summary>Gets or sets the text of the label for the Remember Me check box.</summary>
	/// <returns>The text of the label for the Remember Me check box. The default is "Remember me next time." </returns>
	[Localizable(true)]
	public virtual string RememberMeText
	{
		get
		{
			object obj = ViewState["RememberMeText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Remember me next time.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("RememberMeText");
			}
			else
			{
				ViewState["RememberMeText"] = value;
			}
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to a <see cref="T:System.Web.UI.WebControls.Login" /> control. This property is used primarily by control developers.</summary>
	/// <returns>Always returns <see cref="F:System.Web.UI.HtmlTextWriterTag.Table" />.</returns>
	protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Table;

	/// <summary>Gets a reference to a collection of properties that define the appearance of text boxes in the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that contains properties that define the appearance of text boxes.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public Style TextBoxStyle
	{
		get
		{
			if (textBoxStyle == null)
			{
				textBoxStyle = new Style();
				if (base.IsTrackingViewState)
				{
					((IStateManager)textBoxStyle).TrackViewState();
				}
			}
			return textBoxStyle;
		}
	}

	/// <summary>Specifies the position of each label relative to its associated text box for the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.LoginTextLayout" /> enumeration values. The default is <see cref="F:System.Web.UI.WebControls.LoginTextLayout.TextOnLeft" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.LoginTextLayout" /> enumeration values.</exception>
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

	/// <summary>Gets or sets the title of the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
	/// <returns>The title of the <see cref="T:System.Web.UI.WebControls.Login" /> control. The default is "Login". </returns>
	[Localizable(true)]
	public virtual string TitleText
	{
		get
		{
			object obj = ViewState["TitleText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Log In");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("TitleText");
			}
			else
			{
				ViewState["TitleText"] = value;
			}
		}
	}

	/// <summary>Gets a reference to a collection of properties that define the appearance of the title text in the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of title text.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public TableItemStyle TitleTextStyle
	{
		get
		{
			if (titleTextStyle == null)
			{
				titleTextStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					((IStateManager)titleTextStyle).TrackViewState();
				}
			}
			return titleTextStyle;
		}
	}

	/// <summary>Gets the user name entered by the user.</summary>
	/// <returns>The user name entered by the user. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	public virtual string UserName
	{
		get
		{
			object obj = ViewState["UserName"];
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
				ViewState.Remove("UserName");
			}
			else
			{
				ViewState["UserName"] = value;
			}
		}
	}

	/// <summary>Gets or sets the text of the label for the <see cref="P:System.Web.UI.WebControls.Login.UserName" /> text box.</summary>
	/// <returns>The text of the label for the <see cref="P:System.Web.UI.WebControls.Login.UserName" /> text box. The default is "User Name:".</returns>
	[Localizable(true)]
	public virtual string UserNameLabelText
	{
		get
		{
			object obj = ViewState["UserNameLabelText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("User Name:");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("UserNameLabelText");
			}
			else
			{
				ViewState["UserNameLabelText"] = value;
			}
		}
	}

	/// <summary>Gets or sets the error message to display in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when the user name field is left blank.</summary>
	/// <returns>The error message to display in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when the user name field is left blank. The default is "User Name." </returns>
	[Localizable(true)]
	public virtual string UserNameRequiredErrorMessage
	{
		get
		{
			object obj = ViewState["UserNameRequiredErrorMessage"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("User Name is required.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("UserNameRequiredErrorMessage");
			}
			else
			{
				ViewState["UserNameRequiredErrorMessage"] = value;
			}
		}
	}

	/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of error messages associated with validators used by the <see cref="T:System.Web.UI.WebControls.Login" /> control. </summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> containing the style settings.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public Style ValidatorTextStyle
	{
		get
		{
			if (validatorTextStyle == null)
			{
				validatorTextStyle = new Style();
				if (base.IsTrackingViewState)
				{
					((IStateManager)validatorTextStyle).TrackViewState();
				}
			}
			return validatorTextStyle;
		}
	}

	/// <summary>Gets or sets a value indicating whether to show the <see cref="T:System.Web.UI.WebControls.Login" /> control after the user is authenticated.</summary>
	/// <returns>
	///     <see langword="false" /> if the <see cref="T:System.Web.UI.WebControls.Login" /> control should be hidden when the user is authenticated; otherwise, <see langword="true" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[Themeable(false)]
	public virtual bool VisibleWhenLoggedIn
	{
		get
		{
			object obj = ViewState["VisibleWhenLoggedIn"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return true;
		}
		set
		{
			ViewState["VisibleWhenLoggedIn"] = value;
		}
	}

	private LoginContainer LoginTemplateContainer
	{
		get
		{
			if (container == null)
			{
				container = new LoginContainer(this);
			}
			return container;
		}
	}

	/// <summary>Occurs when a user is authenticated.</summary>
	public event AuthenticateEventHandler Authenticate
	{
		add
		{
			base.Events.AddHandler(authenticateEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(authenticateEvent, value);
		}
	}

	/// <summary>Occurs when the user logs in to the Web site and has been authenticated.</summary>
	public event EventHandler LoggedIn
	{
		add
		{
			base.Events.AddHandler(loggedInEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(loggedInEvent, value);
		}
	}

	/// <summary>Occurs when a user submits login information, before authentication takes place.</summary>
	public event LoginCancelEventHandler LoggingIn
	{
		add
		{
			base.Events.AddHandler(loggingInEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(loggingInEvent, value);
		}
	}

	/// <summary>Occurs when a login error is detected.</summary>
	public event EventHandler LoginError
	{
		add
		{
			base.Events.AddHandler(loginErrorEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(loginErrorEvent, value);
		}
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
	public Login()
	{
	}

	/// <summary>Creates the individual controls that make up the <see cref="T:System.Web.UI.WebControls.Login" /> control and associates event handlers with their events.</summary>
	protected internal override void CreateChildControls()
	{
		Controls.Clear();
		ITemplate template = LayoutTemplate;
		if (template == null)
		{
			template = new LoginTemplate(this);
		}
		LoginTemplateContainer.InstantiateTemplate(template);
		Controls.Add(container);
		if (container.UserNameTextBox is IEditableTextControl editableTextControl)
		{
			editableTextControl.Text = UserName;
			editableTextControl.TextChanged += UserName_TextChanged;
			if (container.PasswordTextBox is IEditableTextControl editableTextControl2)
			{
				editableTextControl2.TextChanged += Password_TextChanged;
				if (container.RememberMeCheckBox is ICheckBoxControl checkBoxControl)
				{
					checkBoxControl.CheckedChanged += RememberMe_CheckedChanged;
				}
				return;
			}
			throw new HttpException("LayoutTemplate does not contain an IEditableTextControl with ID Password for the password.");
		}
		throw new HttpException("LayoutTemplate does not contain an IEditableTextControl with ID UserName for the username.");
	}

	/// <summary>Restores view-state information from a previous request that was saved with the <see cref="M:System.Web.UI.WebControls.WebControl.SaveViewState" /> method.</summary>
	/// <param name="savedState">An object that represents the control state to restore.</param>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="savedState" /> is not a valid <see cref="P:System.Web.UI.PageStatePersister.ViewState" />.</exception>
	protected override void LoadViewState(object savedState)
	{
		if (savedState == null)
		{
			base.LoadViewState((object)null);
			return;
		}
		object[] array = (object[])savedState;
		base.LoadViewState(array[0]);
		if (array[1] != null)
		{
			((IStateManager)LoginButtonStyle).LoadViewState(array[1]);
		}
		if (array[2] != null)
		{
			((IStateManager)LabelStyle).LoadViewState(array[2]);
		}
		if (array[3] != null)
		{
			((IStateManager)TextBoxStyle).LoadViewState(array[3]);
		}
		if (array[4] != null)
		{
			((IStateManager)HyperLinkStyle).LoadViewState(array[4]);
		}
		if (array[5] != null)
		{
			((IStateManager)InstructionTextStyle).LoadViewState(array[5]);
		}
		if (array[6] != null)
		{
			((IStateManager)TitleTextStyle).LoadViewState(array[6]);
		}
		if (array[7] != null)
		{
			((IStateManager)CheckBoxStyle).LoadViewState(array[7]);
		}
		if (array[8] != null)
		{
			((IStateManager)FailureTextStyle).LoadViewState(array[8]);
		}
		if (array[9] != null)
		{
			((IStateManager)ValidatorTextStyle).LoadViewState(array[9]);
		}
	}

	private bool HasOnAuthenticateHandler()
	{
		return (object)base.Events[authenticateEvent] != null;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Login.Authenticate" /> event to authenticate the user.</summary>
	/// <param name="e">An <see cref="T:System.Web.UI.WebControls.AuthenticateEventArgs" /> that contains the event data. </param>
	protected virtual void OnAuthenticate(AuthenticateEventArgs e)
	{
		((AuthenticateEventHandler)base.Events[authenticateEvent])?.Invoke(this, e);
	}

	/// <summary>Determines whether to pass an event up the page's user interface (UI) server control hierarchy.</summary>
	/// <param name="source">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> containing the data for the event. </param>
	/// <returns>
	///     <see langword="true" /> if the event has been canceled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	protected override bool OnBubbleEvent(object source, EventArgs e)
	{
		if (e is CommandEventArgs commandEventArgs && string.Equals(commandEventArgs.CommandName, LoginButtonCommandName, StringComparison.InvariantCultureIgnoreCase))
		{
			if (!AuthenticateUser())
			{
				ITextControl failureTextLiteral = LoginTemplateContainer.FailureTextLiteral;
				if (failureTextLiteral != null)
				{
					failureTextLiteral.Text = FailureText;
				}
			}
			return true;
		}
		return false;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Login.LoggedIn" /> event after the user logs in to the Web site and has been authenticated.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnLoggedIn(EventArgs e)
	{
		((EventHandler)base.Events[loggedInEvent])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Login.LoggingIn" /> event when a user submits login information but before the authentication takes place.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.LoginCancelEventArgs" /> containing the event data.</param>
	protected virtual void OnLoggingIn(LoginCancelEventArgs e)
	{
		((LoginCancelEventHandler)base.Events[loggingInEvent])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Login.LoginError" /> event when a login attempt fails.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnLoginError(EventArgs e)
	{
		((EventHandler)base.Events[loginErrorEvent])?.Invoke(this, e);
	}

	/// <summary>Implements the base <see cref="M:System.Web.UI.Control.OnPreRender(System.EventArgs)" /> method.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
	[MonoTODO("overriden for ?")]
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
	}

	/// <summary>Renders the login form using the specified HTML writer.</summary>
	/// <param name="writer">The HMTL writer.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		VerifyInlinePropertiesNotSet();
		if (!VisibleWhenLoggedIn && !IsDefaultLoginPage() && IsLoggedIn())
		{
			return;
		}
		Page?.VerifyRenderingInServerForm(this);
		EnsureChildControls();
		foreach (object[] style in styles)
		{
			((WebControl)style[0]).ApplyStyle((Style)style[1]);
		}
		RenderContents(writer);
	}

	/// <summary>Saves any state that was modified after the <see cref="M:System.Web.UI.WebControls.Style.TrackViewState" /> method was invoked.</summary>
	/// <returns>An object that contains the current view state of the control; otherwise, if there is no view state associated with the control, <see langword="null" />.
	/// </returns>
	protected override object SaveViewState()
	{
		object[] array = new object[10]
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
			null
		};
		if (logonButtonStyle != null)
		{
			array[1] = ((IStateManager)logonButtonStyle).SaveViewState();
		}
		if (labelStyle != null)
		{
			array[2] = ((IStateManager)labelStyle).SaveViewState();
		}
		if (textBoxStyle != null)
		{
			array[3] = ((IStateManager)textBoxStyle).SaveViewState();
		}
		if (hyperLinkStyle != null)
		{
			array[4] = ((IStateManager)hyperLinkStyle).SaveViewState();
		}
		if (instructionTextStyle != null)
		{
			array[5] = ((IStateManager)instructionTextStyle).SaveViewState();
		}
		if (titleTextStyle != null)
		{
			array[6] = ((IStateManager)titleTextStyle).SaveViewState();
		}
		if (checkBoxStyle != null)
		{
			array[7] = ((IStateManager)checkBoxStyle).SaveViewState();
		}
		if (failureTextStyle != null)
		{
			array[8] = ((IStateManager)failureTextStyle).SaveViewState();
		}
		if (validatorTextStyle != null)
		{
			array[9] = ((IStateManager)validatorTextStyle).SaveViewState();
		}
		for (int i = 0; i < array.Length; i++)
		{
			if (array[0] != null)
			{
				return array;
			}
		}
		return null;
	}

	/// <summary>Sets design-time data for a control.</summary>
	/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> containing the design-time data for the control.</param>
	[MonoTODO("for design-time usage - no more details available")]
	protected override void SetDesignModeState(IDictionary data)
	{
		base.SetDesignModeState(data);
	}

	/// <summary>Overrides the base <see cref="M:System.Web.UI.Control.TrackViewState" /> method.</summary>
	protected override void TrackViewState()
	{
		base.TrackViewState();
		if (logonButtonStyle != null)
		{
			((IStateManager)logonButtonStyle).TrackViewState();
		}
		if (labelStyle != null)
		{
			((IStateManager)labelStyle).TrackViewState();
		}
		if (textBoxStyle != null)
		{
			((IStateManager)textBoxStyle).TrackViewState();
		}
		if (hyperLinkStyle != null)
		{
			((IStateManager)hyperLinkStyle).TrackViewState();
		}
		if (instructionTextStyle != null)
		{
			((IStateManager)instructionTextStyle).TrackViewState();
		}
		if (titleTextStyle != null)
		{
			((IStateManager)titleTextStyle).TrackViewState();
		}
		if (checkBoxStyle != null)
		{
			((IStateManager)checkBoxStyle).TrackViewState();
		}
		if (failureTextStyle != null)
		{
			((IStateManager)failureTextStyle).TrackViewState();
		}
		if (validatorTextStyle != null)
		{
			((IStateManager)validatorTextStyle).TrackViewState();
		}
	}

	internal void RegisterApplyStyle(WebControl control, Style style)
	{
		styles.Add(new object[2] { control, style });
	}

	private bool AuthenticateUser()
	{
		if (!Page.IsValid)
		{
			return true;
		}
		LoginCancelEventArgs loginCancelEventArgs = new LoginCancelEventArgs();
		OnLoggingIn(loginCancelEventArgs);
		if (loginCancelEventArgs.Cancel)
		{
			return true;
		}
		AuthenticateEventArgs authenticateEventArgs = new AuthenticateEventArgs();
		if (!HasOnAuthenticateHandler())
		{
			string membershipProvider = MembershipProvider;
			MembershipProvider provider = ((membershipProvider.Length != 0) ? Membership.Providers[membershipProvider] : (provider = Membership.Provider));
			if (provider == null)
			{
				throw new HttpException(Locale.GetText("No provider named '{0}' could be found.", membershipProvider));
			}
			authenticateEventArgs.Authenticated = provider.ValidateUser(UserName, Password);
		}
		OnAuthenticate(authenticateEventArgs);
		if (authenticateEventArgs.Authenticated)
		{
			FormsAuthentication.SetAuthCookie(UserName, RememberMeSet);
			OnLoggedIn(EventArgs.Empty);
			string destinationPageUrl = DestinationPageUrl;
			if (Page.Request.Path.StartsWith(FormsAuthentication.LoginUrl, StringComparison.InvariantCultureIgnoreCase))
			{
				if (!string.IsNullOrEmpty(FormsAuthentication.ReturnUrl))
				{
					Redirect(FormsAuthentication.ReturnUrl);
				}
				else if (!string.IsNullOrEmpty(DestinationPageUrl))
				{
					Redirect(destinationPageUrl);
				}
				else if (!string.IsNullOrEmpty(FormsAuthentication.DefaultUrl))
				{
					Redirect(FormsAuthentication.DefaultUrl);
				}
				else if (destinationPageUrl.Length == 0)
				{
					Refresh();
				}
			}
			else if (!string.IsNullOrEmpty(DestinationPageUrl))
			{
				Redirect(destinationPageUrl);
			}
			else
			{
				Refresh();
			}
			return true;
		}
		OnLoginError(EventArgs.Empty);
		if (FailureAction == LoginFailureAction.RedirectToLoginPage)
		{
			FormsAuthentication.RedirectToLoginPage();
		}
		return false;
	}

	[MonoTODO]
	private void LoginClick(object sender, CommandEventArgs e)
	{
		RaiseBubbleEvent(sender, e);
	}

	private bool IsDefaultLoginPage()
	{
		if (Page == null || Page.Request == null)
		{
			return false;
		}
		string loginUrl = FormsAuthentication.LoginUrl;
		if (loginUrl == null)
		{
			return false;
		}
		string absolutePath = Page.Request.Url.AbsolutePath;
		return string.Compare(loginUrl, 0, absolutePath, absolutePath.Length - loginUrl.Length, loginUrl.Length, ignoreCase: true, Helpers.InvariantCulture) == 0;
	}

	private bool IsLoggedIn()
	{
		if (Page == null || Page.Request == null)
		{
			return false;
		}
		return Page.Request.IsAuthenticated;
	}

	private void Redirect(string url)
	{
		if (Page != null && Page.Response != null)
		{
			Page.Response.Redirect(url);
		}
	}

	private void Refresh()
	{
		if (Page != null && Page.Response != null)
		{
			Page.Response.Redirect(Page.Request.RawUrl);
		}
	}

	private void UserName_TextChanged(object sender, EventArgs e)
	{
		UserName = ((ITextControl)sender).Text;
	}

	private void Password_TextChanged(object sender, EventArgs e)
	{
		_password = ((ITextControl)sender).Text;
	}

	private void RememberMe_CheckedChanged(object sender, EventArgs e)
	{
		RememberMeSet = ((ICheckBoxControl)sender).Checked;
	}
}
