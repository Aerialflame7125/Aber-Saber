using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Mail;
using System.Web.Security;

namespace System.Web.UI.WebControls;

/// <summary>Provides a user interface for creating new Web site user accounts.</summary>
[DefaultEvent("CreatedUser")]
[Designer("System.Web.UI.Design.WebControls.CreateUserWizardDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ToolboxData("   ")]
[Bindable(false)]
public class CreateUserWizard : Wizard
{
	private class SideBarLabelTemplate : ITemplate
	{
		private Wizard wizard;

		public SideBarLabelTemplate(Wizard wizard)
		{
			this.wizard = wizard;
		}

		public void InstantiateIn(Control control)
		{
			Label label = new Label();
			wizard.RegisterApplyStyle(label, wizard.SideBarButtonStyle);
			control.Controls.Add(label);
			control.DataBinding += Bound;
		}

		private void Bound(object s, EventArgs args)
		{
			if (DataBinder.GetDataItem(s) is WizardStepBase wizardStepBase)
			{
				Label obj = (Label)((Control)s).Controls[0];
				obj.ID = Wizard.SideBarButtonID;
				obj.Text = wizardStepBase.Title;
			}
		}
	}

	private sealed class CreateUserNavigationContainer : DefaultNavigationContainer
	{
		private CreateUserWizard _createUserWizard;

		public CreateUserNavigationContainer(CreateUserWizard createUserWizard)
			: base(createUserWizard)
		{
			_createUserWizard = createUserWizard;
		}

		protected override void UpdateState()
		{
			int num = _createUserWizard.ActiveStepIndex - 1;
			if (num >= 0 && _createUserWizard.AllowNavigationToStep(num))
			{
				UpdateNavButtonState(Wizard.StepPreviousButtonID + base.Wizard.StepPreviousButtonType, base.Wizard.StepPreviousButtonText, base.Wizard.StepPreviousButtonImageUrl, base.Wizard.StepPreviousButtonStyle);
			}
			else
			{
				((Table)Controls[0]).Rows[0].Cells[0].Visible = false;
			}
			UpdateNavButtonState(Wizard.StepNextButtonID + _createUserWizard.CreateUserButtonType, _createUserWizard.CreateUserButtonText, _createUserWizard.CreateUserButtonImageUrl, _createUserWizard.CreateUserButtonStyle);
			if (base.Wizard.DisplayCancelButton)
			{
				UpdateNavButtonState(Wizard.CancelButtonID + base.Wizard.CancelButtonType, base.Wizard.CancelButtonText, base.Wizard.CancelButtonImageUrl, base.Wizard.CancelButtonStyle);
			}
			else
			{
				((Table)Controls[0]).Rows[0].Cells[2].Visible = false;
			}
		}
	}

	private sealed class CreateUserStepNavigationTemplate : ITemplate
	{
		private readonly CreateUserWizard _createUserWizard;

		public CreateUserStepNavigationTemplate(CreateUserWizard createUserWizard)
		{
			_createUserWizard = createUserWizard;
		}

		public void InstantiateIn(Control container)
		{
			Table table = new Table();
			table.CellPadding = 5;
			table.CellSpacing = 5;
			table.Width = Unit.Percentage(100.0);
			table.Height = Unit.Percentage(100.0);
			TableRow row = new TableRow();
			AddButtonCell(row, _createUserWizard.CreateButtonSet(Wizard.StepPreviousButtonID, Wizard.MovePreviousCommandName, causesValidation: false, _createUserWizard.ID));
			AddButtonCell(row, _createUserWizard.CreateButtonSet(Wizard.StepNextButtonID, Wizard.MoveNextCommandName, causesValidation: true, _createUserWizard.ID));
			AddButtonCell(row, _createUserWizard.CreateButtonSet(Wizard.CancelButtonID, Wizard.CancelCommandName, causesValidation: false, _createUserWizard.ID));
			table.Rows.Add(row);
			container.Controls.Add(table);
		}

		private void AddButtonCell(TableRow row, params Control[] controls)
		{
			TableCell tableCell = new TableCell();
			tableCell.HorizontalAlign = HorizontalAlign.Right;
			for (int i = 0; i < controls.Length; i++)
			{
				tableCell.Controls.Add(controls[i]);
			}
			row.Cells.Add(tableCell);
		}
	}

	private sealed class CreateUserStepContainer : DefaultContentContainer
	{
		private CreateUserWizard _createUserWizard;

		public Control UserNameTextBox => FindControl("UserName") ?? throw new HttpException("CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID UserName for the username.");

		public Control PasswordTextBox => FindControl("Password") ?? throw new HttpException("CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID Password for the new password, this is required if AutoGeneratePassword = true.");

		public Control ConfirmPasswordTextBox => FindControl("Password");

		public Control EmailTextBox => FindControl("Email") ?? throw new HttpException("CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID Email for the e-mail, this is required if RequireEmail = true.");

		public Control QuestionTextBox => FindControl("Question") ?? throw new HttpException("CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID Question for the security question, this is required if your membership provider requires a question and answer.");

		public Control AnswerTextBox => FindControl("Answer") ?? throw new HttpException("CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID Answer for the security answer, this is required if your membership provider requires a question and answer.");

		public ITextControl ErrorMessageLabel => FindControl("ErrorMessage") as ITextControl;

		public CreateUserStepContainer(CreateUserWizard createUserWizard)
			: base(createUserWizard)
		{
			_createUserWizard = createUserWizard;
		}

		protected override void UpdateState()
		{
			if (string.IsNullOrEmpty(_createUserWizard.CreateUserStep.Title))
			{
				((Table)base.InnerCell.Controls[0]).Rows[0].Visible = false;
			}
			else
			{
				((Table)base.InnerCell.Controls[0]).Rows[0].Cells[0].Text = _createUserWizard.CreateUserStep.Title;
			}
			if (string.IsNullOrEmpty(_createUserWizard.InstructionText))
			{
				((Table)base.InnerCell.Controls[0]).Rows[1].Visible = false;
			}
			else
			{
				((Table)base.InnerCell.Controls[0]).Rows[1].Cells[0].Text = _createUserWizard.InstructionText;
			}
			((Label)((Table)base.InnerCell.Controls[0]).Rows[2].Cells[0].Controls[0]).Text = _createUserWizard.UserNameLabelText;
			RequiredFieldValidator obj = (RequiredFieldValidator)FindControl("UserNameRequired");
			obj.ErrorMessage = _createUserWizard.UserNameRequiredErrorMessage;
			obj.ToolTip = _createUserWizard.UserNameRequiredErrorMessage;
			if (_createUserWizard.AutoGeneratePassword)
			{
				((Table)base.InnerCell.Controls[0]).Rows[3].Visible = false;
				((Table)base.InnerCell.Controls[0]).Rows[4].Visible = false;
				((Table)base.InnerCell.Controls[0]).Rows[5].Visible = false;
			}
			else
			{
				((Label)((Table)base.InnerCell.Controls[0]).Rows[3].Cells[0].Controls[0]).Text = _createUserWizard.PasswordLabelText;
				RequiredFieldValidator obj2 = (RequiredFieldValidator)FindControl("PasswordRequired");
				obj2.ErrorMessage = _createUserWizard.PasswordRequiredErrorMessage;
				obj2.ToolTip = _createUserWizard.PasswordRequiredErrorMessage;
				if (string.IsNullOrEmpty(_createUserWizard.PasswordHintText))
				{
					((Table)base.InnerCell.Controls[0]).Rows[4].Visible = false;
				}
				else
				{
					((Table)base.InnerCell.Controls[0]).Rows[4].Cells[1].Text = _createUserWizard.PasswordHintText;
				}
				((Label)((Table)base.InnerCell.Controls[0]).Rows[5].Cells[0].Controls[0]).Text = _createUserWizard.ConfirmPasswordLabelText;
				RequiredFieldValidator obj3 = (RequiredFieldValidator)FindControl("ConfirmPasswordRequired");
				obj3.ErrorMessage = _createUserWizard.ConfirmPasswordRequiredErrorMessage;
				obj3.ToolTip = _createUserWizard.ConfirmPasswordRequiredErrorMessage;
			}
			if (_createUserWizard.RequireEmail)
			{
				((Label)((Table)base.InnerCell.Controls[0]).Rows[6].Cells[0].Controls[0]).Text = _createUserWizard.EmailLabelText;
				RequiredFieldValidator obj4 = (RequiredFieldValidator)FindControl("EmailRequired");
				obj4.ErrorMessage = _createUserWizard.EmailRequiredErrorMessage;
				obj4.ToolTip = _createUserWizard.EmailRequiredErrorMessage;
			}
			else
			{
				((Table)base.InnerCell.Controls[0]).Rows[6].Visible = false;
			}
			if (_createUserWizard.QuestionAndAnswerRequired)
			{
				((Label)((Table)base.InnerCell.Controls[0]).Rows[7].Cells[0].Controls[0]).Text = _createUserWizard.QuestionLabelText;
				RequiredFieldValidator obj5 = (RequiredFieldValidator)FindControl("QuestionRequired");
				obj5.ErrorMessage = _createUserWizard.QuestionRequiredErrorMessage;
				obj5.ToolTip = _createUserWizard.QuestionRequiredErrorMessage;
				((Label)((Table)base.InnerCell.Controls[0]).Rows[8].Cells[0].Controls[0]).Text = _createUserWizard.AnswerLabelText;
				RequiredFieldValidator obj6 = (RequiredFieldValidator)FindControl("AnswerRequired");
				obj6.ErrorMessage = _createUserWizard.AnswerRequiredErrorMessage;
				obj6.ToolTip = _createUserWizard.AnswerRequiredErrorMessage;
			}
			else
			{
				((Table)base.InnerCell.Controls[0]).Rows[7].Visible = false;
				((Table)base.InnerCell.Controls[0]).Rows[8].Visible = false;
			}
			if (_createUserWizard.AutoGeneratePassword)
			{
				((Table)base.InnerCell.Controls[0]).Rows[9].Visible = false;
			}
			else
			{
				((CompareValidator)FindControl("PasswordCompare")).ErrorMessage = _createUserWizard.ConfirmPasswordCompareErrorMessage;
			}
			if (_createUserWizard.AutoGeneratePassword || string.IsNullOrEmpty(_createUserWizard.PasswordRegularExpression))
			{
				((Table)base.InnerCell.Controls[0]).Rows[10].Visible = false;
			}
			else
			{
				RegularExpressionValidator obj7 = (RegularExpressionValidator)FindControl("PasswordRegEx");
				obj7.ValidationExpression = _createUserWizard.PasswordRegularExpression;
				obj7.ErrorMessage = _createUserWizard.PasswordRegularExpressionErrorMessage;
			}
			if (!_createUserWizard.RequireEmail || string.IsNullOrEmpty(_createUserWizard.EmailRegularExpression))
			{
				((Table)base.InnerCell.Controls[0]).Rows[11].Visible = false;
			}
			else
			{
				RegularExpressionValidator obj8 = (RegularExpressionValidator)FindControl("EmailRegEx");
				obj8.ErrorMessage = _createUserWizard.EmailRegularExpressionErrorMessage;
				obj8.ValidationExpression = _createUserWizard.EmailRegularExpression;
			}
			if (string.IsNullOrEmpty(ErrorMessageLabel.Text))
			{
				((Table)base.InnerCell.Controls[0]).Rows[12].Visible = false;
			}
			Image image = (Image)((Table)base.InnerCell.Controls[0]).Rows[13].Cells[0].Controls[0];
			if (string.IsNullOrEmpty(_createUserWizard.HelpPageIconUrl))
			{
				image.Visible = false;
			}
			else
			{
				image.ImageUrl = _createUserWizard.HelpPageIconUrl;
				image.AlternateText = _createUserWizard.HelpPageText;
			}
			HyperLink hyperLink = (HyperLink)((Table)base.InnerCell.Controls[0]).Rows[13].Cells[0].Controls[1];
			if (string.IsNullOrEmpty(_createUserWizard.HelpPageText))
			{
				hyperLink.Visible = false;
			}
			else
			{
				hyperLink.Text = _createUserWizard.HelpPageText;
				hyperLink.NavigateUrl = _createUserWizard.HelpPageUrl;
			}
			((Table)base.InnerCell.Controls[0]).Rows[13].Visible = image.Visible || hyperLink.Visible;
		}

		public void EnsureValidatorsState()
		{
			if (base.IsDefaultTemplate)
			{
				((RequiredFieldValidator)FindControl("PasswordRequired")).Enabled = !_createUserWizard.AutoGeneratePassword;
				((RequiredFieldValidator)FindControl("ConfirmPasswordRequired")).Enabled = !_createUserWizard.AutoGeneratePassword;
				((CompareValidator)FindControl("PasswordCompare")).Enabled = !_createUserWizard.AutoGeneratePassword;
				RegularExpressionValidator obj = (RegularExpressionValidator)FindControl("PasswordRegEx");
				obj.Enabled = !_createUserWizard.AutoGeneratePassword && !string.IsNullOrEmpty(_createUserWizard.PasswordRegularExpression);
				obj.ValidationExpression = _createUserWizard.PasswordRegularExpression;
				((RequiredFieldValidator)FindControl("EmailRequired")).Enabled = _createUserWizard.RequireEmail;
				RegularExpressionValidator obj2 = (RegularExpressionValidator)FindControl("EmailRegEx");
				obj2.Enabled = _createUserWizard.RequireEmail && !string.IsNullOrEmpty(_createUserWizard.EmailRegularExpression);
				obj2.ValidationExpression = _createUserWizard.EmailRegularExpression;
				((RequiredFieldValidator)FindControl("QuestionRequired")).Enabled = _createUserWizard.QuestionAndAnswerRequired;
				((RequiredFieldValidator)FindControl("AnswerRequired")).Enabled = _createUserWizard.QuestionAndAnswerRequired;
			}
		}
	}

	private sealed class CreateUserStepTemplate : ITemplate
	{
		private readonly CreateUserWizard _createUserWizard;

		public CreateUserStepTemplate(CreateUserWizard createUserWizard)
		{
			_createUserWizard = createUserWizard;
		}

		private TableRow CreateRow(Control c0, Control c1, Control c2, Style s0, Style s1)
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			TableCell tableCell2 = new TableCell();
			if (c0 != null)
			{
				tableCell.Controls.Add(c0);
			}
			tableRow.Controls.Add(tableCell);
			if (c1 != null && c2 != null)
			{
				tableCell2.Controls.Add(c1);
				tableCell2.Controls.Add(c2);
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				if (s0 != null)
				{
					_createUserWizard.RegisterApplyStyle(tableCell, s0);
				}
				if (s1 != null)
				{
					_createUserWizard.RegisterApplyStyle(tableCell2, s1);
				}
				tableRow.Controls.Add(tableCell2);
			}
			else
			{
				tableCell.ColumnSpan = 2;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				if (s0 != null)
				{
					_createUserWizard.RegisterApplyStyle(tableCell, s0);
				}
			}
			return tableRow;
		}

		public void InstantiateIn(Control container)
		{
			Table table = new Table();
			table.ControlStyle.Width = Unit.Percentage(100.0);
			table.ControlStyle.Height = Unit.Percentage(100.0);
			table.Controls.Add(CreateRow(null, null, null, _createUserWizard.TitleTextStyle, null));
			table.Controls.Add(CreateRow(null, null, null, _createUserWizard.InstructionTextStyle, null));
			TextBox textBox = new TextBox();
			textBox.ID = "UserName";
			_createUserWizard.RegisterApplyStyle(textBox, _createUserWizard.TextBoxStyle);
			Label label = new Label();
			label.AssociatedControlID = "UserName";
			RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
			requiredFieldValidator.ID = "UserNameRequired";
			requiredFieldValidator.EnableViewState = false;
			requiredFieldValidator.ControlToValidate = "UserName";
			requiredFieldValidator.Text = "*";
			requiredFieldValidator.ValidationGroup = _createUserWizard.ID;
			_createUserWizard.RegisterApplyStyle(requiredFieldValidator, _createUserWizard.ValidatorTextStyle);
			table.Controls.Add(CreateRow(label, textBox, requiredFieldValidator, _createUserWizard.LabelStyle, null));
			TextBox textBox2 = new TextBox();
			textBox2.ID = "Password";
			textBox2.TextMode = TextBoxMode.Password;
			_createUserWizard.RegisterApplyStyle(textBox2, _createUserWizard.TextBoxStyle);
			Label label2 = new Label();
			label2.AssociatedControlID = "Password";
			RequiredFieldValidator requiredFieldValidator2 = new RequiredFieldValidator();
			requiredFieldValidator2.ID = "PasswordRequired";
			requiredFieldValidator2.EnableViewState = false;
			requiredFieldValidator2.ControlToValidate = "Password";
			requiredFieldValidator2.Text = "*";
			requiredFieldValidator2.ValidationGroup = _createUserWizard.ID;
			_createUserWizard.RegisterApplyStyle(requiredFieldValidator2, _createUserWizard.ValidatorTextStyle);
			table.Controls.Add(CreateRow(label2, textBox2, requiredFieldValidator2, _createUserWizard.LabelStyle, null));
			table.Controls.Add(CreateRow(new LiteralControl(string.Empty), new LiteralControl(string.Empty), new LiteralControl(string.Empty), null, _createUserWizard.PasswordHintStyle));
			TextBox textBox3 = new TextBox();
			textBox3.ID = "ConfirmPassword";
			textBox3.TextMode = TextBoxMode.Password;
			_createUserWizard.RegisterApplyStyle(textBox3, _createUserWizard.TextBoxStyle);
			Label label3 = new Label();
			label3.AssociatedControlID = "ConfirmPassword";
			RequiredFieldValidator requiredFieldValidator3 = new RequiredFieldValidator();
			requiredFieldValidator3.ID = "ConfirmPasswordRequired";
			requiredFieldValidator3.EnableViewState = false;
			requiredFieldValidator3.ControlToValidate = "ConfirmPassword";
			requiredFieldValidator3.Text = "*";
			requiredFieldValidator3.ValidationGroup = _createUserWizard.ID;
			_createUserWizard.RegisterApplyStyle(requiredFieldValidator3, _createUserWizard.ValidatorTextStyle);
			table.Controls.Add(CreateRow(label3, textBox3, requiredFieldValidator3, _createUserWizard.LabelStyle, null));
			TextBox textBox4 = new TextBox();
			textBox4.ID = "Email";
			_createUserWizard.RegisterApplyStyle(textBox4, _createUserWizard.TextBoxStyle);
			Label label4 = new Label();
			label4.AssociatedControlID = "Email";
			RequiredFieldValidator requiredFieldValidator4 = new RequiredFieldValidator();
			requiredFieldValidator4.ID = "EmailRequired";
			requiredFieldValidator4.EnableViewState = false;
			requiredFieldValidator4.ControlToValidate = "Email";
			requiredFieldValidator4.Text = "*";
			requiredFieldValidator4.ValidationGroup = _createUserWizard.ID;
			_createUserWizard.RegisterApplyStyle(requiredFieldValidator4, _createUserWizard.ValidatorTextStyle);
			table.Controls.Add(CreateRow(label4, textBox4, requiredFieldValidator4, _createUserWizard.LabelStyle, null));
			TextBox textBox5 = new TextBox();
			textBox5.ID = "Question";
			_createUserWizard.RegisterApplyStyle(textBox5, _createUserWizard.TextBoxStyle);
			Label label5 = new Label();
			label5.AssociatedControlID = "Question";
			RequiredFieldValidator requiredFieldValidator5 = new RequiredFieldValidator();
			requiredFieldValidator5.ID = "QuestionRequired";
			requiredFieldValidator5.EnableViewState = false;
			requiredFieldValidator5.ControlToValidate = "Question";
			requiredFieldValidator5.Text = "*";
			requiredFieldValidator5.ValidationGroup = _createUserWizard.ID;
			_createUserWizard.RegisterApplyStyle(requiredFieldValidator5, _createUserWizard.ValidatorTextStyle);
			table.Controls.Add(CreateRow(label5, textBox5, requiredFieldValidator5, _createUserWizard.LabelStyle, null));
			TextBox textBox6 = new TextBox();
			textBox6.ID = "Answer";
			_createUserWizard.RegisterApplyStyle(textBox6, _createUserWizard.TextBoxStyle);
			Label label6 = new Label();
			label6.AssociatedControlID = "Answer";
			RequiredFieldValidator requiredFieldValidator6 = new RequiredFieldValidator();
			requiredFieldValidator6.ID = "AnswerRequired";
			requiredFieldValidator6.EnableViewState = false;
			requiredFieldValidator6.ControlToValidate = "Answer";
			requiredFieldValidator6.Text = "*";
			requiredFieldValidator6.ValidationGroup = _createUserWizard.ID;
			_createUserWizard.RegisterApplyStyle(requiredFieldValidator6, _createUserWizard.ValidatorTextStyle);
			table.Controls.Add(CreateRow(label6, textBox6, requiredFieldValidator6, _createUserWizard.LabelStyle, null));
			CompareValidator compareValidator = new CompareValidator();
			compareValidator.ID = "PasswordCompare";
			compareValidator.EnableViewState = false;
			compareValidator.ControlToCompare = "Password";
			compareValidator.ControlToValidate = "ConfirmPassword";
			compareValidator.Display = ValidatorDisplay.Static;
			compareValidator.ValidationGroup = _createUserWizard.ID;
			compareValidator.Display = ValidatorDisplay.Dynamic;
			_createUserWizard.RegisterApplyStyle(compareValidator, _createUserWizard.ValidatorTextStyle);
			table.Controls.Add(CreateRow(compareValidator, null, null, null, null));
			RegularExpressionValidator regularExpressionValidator = new RegularExpressionValidator();
			regularExpressionValidator.ID = "PasswordRegEx";
			regularExpressionValidator.EnableViewState = false;
			regularExpressionValidator.ControlToValidate = "Password";
			regularExpressionValidator.Display = ValidatorDisplay.Static;
			regularExpressionValidator.ValidationGroup = _createUserWizard.ID;
			regularExpressionValidator.Display = ValidatorDisplay.Dynamic;
			_createUserWizard.RegisterApplyStyle(regularExpressionValidator, _createUserWizard.ValidatorTextStyle);
			table.Controls.Add(CreateRow(regularExpressionValidator, null, null, null, null));
			RegularExpressionValidator regularExpressionValidator2 = new RegularExpressionValidator();
			regularExpressionValidator2.ID = "EmailRegEx";
			regularExpressionValidator2.EnableViewState = false;
			regularExpressionValidator2.ControlToValidate = "Email";
			regularExpressionValidator2.Display = ValidatorDisplay.Static;
			regularExpressionValidator2.ValidationGroup = _createUserWizard.ID;
			regularExpressionValidator2.Display = ValidatorDisplay.Dynamic;
			_createUserWizard.RegisterApplyStyle(regularExpressionValidator2, _createUserWizard.ValidatorTextStyle);
			table.Controls.Add(CreateRow(regularExpressionValidator2, null, null, null, null));
			Label label7 = new Label();
			label7.ID = "ErrorMessage";
			label7.EnableViewState = false;
			_createUserWizard.RegisterApplyStyle(label7, _createUserWizard.ValidatorTextStyle);
			table.Controls.Add(CreateRow(label7, null, null, null, null));
			TableRow tableRow = CreateRow(new Image(), null, null, null, null);
			HyperLink hyperLink = new HyperLink();
			hyperLink.ID = "HelpLink";
			_createUserWizard.RegisterApplyStyle(hyperLink, _createUserWizard.HyperLinkStyle);
			tableRow.Cells[0].Controls.Add(hyperLink);
			tableRow.Cells[0].HorizontalAlign = HorizontalAlign.Left;
			table.Controls.Add(tableRow);
			container.Controls.Add(table);
		}
	}

	private sealed class CompleteStepContainer : DefaultContentContainer
	{
		private CreateUserWizard _createUserWizard;

		public CompleteStepContainer(CreateUserWizard createUserWizard)
			: base(createUserWizard)
		{
			_createUserWizard = createUserWizard;
		}

		protected override void UpdateState()
		{
			if (string.IsNullOrEmpty(_createUserWizard.CompleteStep.Title))
			{
				((Table)base.InnerCell.Controls[0]).Rows[0].Visible = false;
			}
			else
			{
				((Table)base.InnerCell.Controls[0]).Rows[0].Cells[0].Text = _createUserWizard.CompleteStep.Title;
			}
			if (string.IsNullOrEmpty(_createUserWizard.CompleteSuccessText))
			{
				((Table)base.InnerCell.Controls[0]).Rows[1].Visible = false;
			}
			else
			{
				((Table)base.InnerCell.Controls[0]).Rows[1].Cells[0].Text = _createUserWizard.CompleteSuccessText;
			}
			UpdateNavButtonState("ContinueButton" + _createUserWizard.ContinueButtonType, _createUserWizard.ContinueButtonText, _createUserWizard.ContinueButtonImageUrl, _createUserWizard.ContinueButtonStyle);
			Image image = (Image)((Table)base.InnerCell.Controls[0]).Rows[3].Cells[0].Controls[0];
			if (string.IsNullOrEmpty(_createUserWizard.EditProfileIconUrl))
			{
				image.Visible = false;
			}
			else
			{
				image.ImageUrl = _createUserWizard.EditProfileIconUrl;
				image.AlternateText = _createUserWizard.EditProfileText;
			}
			HyperLink hyperLink = (HyperLink)((Table)base.InnerCell.Controls[0]).Rows[3].Cells[0].Controls[1];
			if (string.IsNullOrEmpty(_createUserWizard.EditProfileText))
			{
				hyperLink.Visible = false;
			}
			else
			{
				hyperLink.Text = _createUserWizard.EditProfileText;
				hyperLink.NavigateUrl = _createUserWizard.EditProfileUrl;
			}
			((Table)base.InnerCell.Controls[0]).Rows[3].Visible = image.Visible || hyperLink.Visible;
		}

		private void UpdateNavButtonState(string id, string text, string image, Style style)
		{
			WebControl webControl = (WebControl)FindControl(id);
			foreach (Control control in webControl.Parent.Controls)
			{
				control.Visible = webControl == control;
			}
			((IButtonControl)webControl).Text = text;
			if (webControl is ImageButton imageButton)
			{
				imageButton.ImageUrl = image;
			}
			webControl.ApplyStyle(style);
		}
	}

	private sealed class CompleteStepTemplate : ITemplate
	{
		private readonly CreateUserWizard _createUserWizard;

		public CompleteStepTemplate(CreateUserWizard createUserWizard)
		{
			_createUserWizard = createUserWizard;
		}

		public void InstantiateIn(Control container)
		{
			Table table = new Table();
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.HorizontalAlign = HorizontalAlign.Center;
			tableCell.ColumnSpan = 2;
			_createUserWizard.RegisterApplyStyle(tableCell, _createUserWizard.TitleTextStyle);
			tableRow.Cells.Add(tableCell);
			TableRow tableRow2 = new TableRow();
			TableCell tableCell2 = new TableCell();
			tableCell2.HorizontalAlign = HorizontalAlign.Center;
			_createUserWizard.RegisterApplyStyle(tableCell2, _createUserWizard.CompleteSuccessTextStyle);
			tableRow2.Cells.Add(tableCell2);
			TableRow tableRow3 = new TableRow();
			TableCell tableCell3 = new TableCell();
			tableCell3.HorizontalAlign = HorizontalAlign.Right;
			tableCell3.ColumnSpan = 2;
			tableRow3.Cells.Add(tableCell3);
			Control[] array = _createUserWizard.CreateButtonSet("ContinueButton", ContinueButtonCommandName, causesValidation: false, _createUserWizard.ID);
			for (int i = 0; i < array.Length; i++)
			{
				tableCell3.Controls.Add(array[i]);
			}
			TableRow tableRow4 = new TableRow();
			TableCell tableCell4 = new TableCell();
			tableCell4.Controls.Add(new Image());
			HyperLink hyperLink = new HyperLink();
			hyperLink.ID = "EditProfileLink";
			_createUserWizard.RegisterApplyStyle(hyperLink, _createUserWizard.HyperLinkStyle);
			tableCell4.Controls.Add(hyperLink);
			tableRow4.Cells.Add(tableCell4);
			table.Rows.Add(tableRow);
			table.Rows.Add(tableRow2);
			table.Rows.Add(tableRow3);
			table.Rows.Add(tableRow4);
			container.Controls.Add(table);
		}
	}

	/// <summary>Represents the <see cref="P:System.Web.UI.WebControls.Button.CommandName" /> value of the Continue button on the final step for creating a user account. The <see cref="F:System.Web.UI.WebControls.CreateUserWizard.ContinueButtonCommandName" /> field is read-only. </summary>
	public static readonly string ContinueButtonCommandName = "Continue";

	private string _password = string.Empty;

	private string _confirmPassword = string.Empty;

	private MembershipProvider _provider;

	private ITextControl _errorMessageLabel;

	private MailDefinition _mailDefinition;

	private Style _textBoxStyle;

	private Style _validatorTextStyle;

	private TableItemStyle _completeSuccessTextStyle;

	private TableItemStyle _errorMessageStyle;

	private TableItemStyle _hyperLinkStyle;

	private TableItemStyle _instructionTextStyle;

	private TableItemStyle _labelStyle;

	private TableItemStyle _passwordHintStyle;

	private TableItemStyle _titleTextStyle;

	private Style _createUserButtonStyle;

	private Style _continueButtonStyle;

	private static readonly object CreatedUserEvent;

	private static readonly object CreateUserErrorEvent;

	private static readonly object CreatingUserEvent;

	private static readonly object ContinueButtonClickEvent;

	private static readonly object SendingMailEvent;

	private static readonly object SendMailErrorEvent;

	private CompleteWizardStep _completeWizardStep;

	private CreateUserWizardStep _createUserWizardStep;

	/// <summary>Gets or sets the step that is currently displayed to the user.</summary>
	/// <returns>The index of the step that is currently displayed to the user.</returns>
	[DefaultValue(0)]
	public override int ActiveStepIndex
	{
		get
		{
			return base.ActiveStepIndex;
		}
		set
		{
			base.ActiveStepIndex = value;
		}
	}

	/// <summary>Gets or sets the end user's answer to the password recovery confirmation question.</summary>
	/// <returns>The end user's answer to the password recovery confirmation question. The default value is an empty string ("").</returns>
	[DefaultValue("")]
	[Localizable(true)]
	[Themeable(false)]
	public virtual string Answer
	{
		get
		{
			object obj = ViewState["Answer"];
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
				ViewState.Remove("Answer");
			}
			else
			{
				ViewState["Answer"] = value;
			}
		}
	}

	/// <summary>Gets or sets the text of the label that identifies the password confirmation answer text box.</summary>
	/// <returns>The label text that identifies the password confirmation answer text box. The default value is "Security Answer:". The default text for the control is localized based on the server's current locale.</returns>
	[Localizable(true)]
	public virtual string AnswerLabelText
	{
		get
		{
			object obj = ViewState["AnswerLabelText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Security Answer:");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("AnswerLabelText");
			}
			else
			{
				ViewState["AnswerLabelText"] = value;
			}
		}
	}

	/// <summary>Gets or sets the error message shown when the user does not enter an answer to the password confirmation question.</summary>
	/// <returns>The error message shown when the user does not enter an answer to the password confirmation question. The default value is "Security answer is required." The default text for the control is localized based on the server's current locale.</returns>
	[Localizable(true)]
	public virtual string AnswerRequiredErrorMessage
	{
		get
		{
			object obj = ViewState["AnswerRequiredErrorMessage"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Security answer is required.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("AnswerRequiredErrorMessage");
			}
			else
			{
				ViewState["AnswerRequiredErrorMessage"] = value;
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether or not to automatically generate a password for the new user account.</summary>
	/// <returns>
	///     <see langword="true" /> to automatically generate a password for the new user account; otherwise, <see langword="false" />. The default value is <see langword="false" />.This property cannot be set by themes or style sheet themes. For more information, see <see cref="T:System.Web.UI.ThemeableAttribute" /> and ASP.NET Themes and Skins.</returns>
	[DefaultValue(false)]
	[Themeable(false)]
	public virtual bool AutoGeneratePassword
	{
		get
		{
			object obj = ViewState["AutoGeneratePassword"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return false;
		}
		set
		{
			ViewState["AutoGeneratePassword"] = value;
		}
	}

	/// <summary>Gets a reference to the final user account creation step.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.CompleteWizardStep" /> object that represents the final user account creation step.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public CompleteWizardStep CompleteStep
	{
		get
		{
			if (_completeWizardStep == null)
			{
				for (int i = 0; i < WizardSteps.Count; i++)
				{
					if (WizardSteps[i] is CompleteWizardStep)
					{
						_completeWizardStep = (CompleteWizardStep)WizardSteps[i];
						if (_completeWizardStep.Wizard == null)
						{
							_completeWizardStep.SetWizard(this);
						}
					}
				}
			}
			return _completeWizardStep;
		}
	}

	/// <summary>Gets or sets the text displayed when a Web site user account is created successfully.</summary>
	/// <returns>The text displayed when a Web site user account is created successfully. The default is "Your account has been successfully created." The default text for the control is localized based on the server's current locale.</returns>
	[Localizable(true)]
	public virtual string CompleteSuccessText
	{
		get
		{
			object obj = ViewState["CompleteSuccessText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Your account has been successfully created.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("CompleteSuccessText");
			}
			else
			{
				ViewState["CompleteSuccessText"] = value;
			}
		}
	}

	/// <summary>Gets a reference to a collection of properties that define the appearance of the text displayed when a Web site user account is created successfully. </summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of the text displayed when a Web site user account is created successfully.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public TableItemStyle CompleteSuccessTextStyle
	{
		get
		{
			if (_completeSuccessTextStyle == null)
			{
				_completeSuccessTextStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					((IStateManager)_completeSuccessTextStyle).TrackViewState();
				}
			}
			return _completeSuccessTextStyle;
		}
	}

	/// <summary>Gets the second password entered by the user.</summary>
	/// <returns>The second password entered by the user. The default value is an empty string ("").</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual string ConfirmPassword => _confirmPassword;

	/// <summary>Gets or sets the error message shown when the user enters two different passwords in the password and confirm password text boxes.</summary>
	/// <returns>The error message shown when the user enters two different passwords in the password and confirm password text boxes. The default value is "The Password and Confirmation Password must match." The default text for the control is localized based on the server's current locale.</returns>
	[Localizable(true)]
	public virtual string ConfirmPasswordCompareErrorMessage
	{
		get
		{
			object obj = ViewState["ConfirmPasswordCompareErrorMessage"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("The Password and Confirmation Password must match.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("ConfirmPasswordCompareErrorMessage");
			}
			else
			{
				ViewState["ConfirmPasswordCompareErrorMessage"] = value;
			}
		}
	}

	/// <summary>Gets or sets text of the label for the second password text box.</summary>
	/// <returns>The label text that identifies the confirm password text box. The default value is "Confirm Password:". The default text for the control is localized based on the server's current locale.</returns>
	[Localizable(true)]
	public virtual string ConfirmPasswordLabelText
	{
		get
		{
			object obj = ViewState["ConfirmPasswordLabelText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Confirm Password:");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("ConfirmPasswordLabelText");
			}
			else
			{
				ViewState["ConfirmPasswordLabelText"] = value;
			}
		}
	}

	/// <summary>Gets or sets the error message displayed when the user leaves the confirm password text box empty.</summary>
	/// <returns>The error message displayed when the user leaves the confirm password text box empty. The default value is "Confirm Password is required." The default text for the control is localized based on the server's current locale.</returns>
	[Localizable(true)]
	public virtual string ConfirmPasswordRequiredErrorMessage
	{
		get
		{
			object obj = ViewState["ConfirmPasswordRequiredErrorMessage"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Confirm Password is required.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("ConfirmPasswordRequiredErrorMessage");
			}
			else
			{
				ViewState["ConfirmPasswordRequiredErrorMessage"] = value;
			}
		}
	}

	/// <summary>Gets or sets the URL of an image used for the Continue button on the final user account creation step.</summary>
	/// <returns>The URL of an image used for the Continue button on the final user account creation step. The default value is an empty string ("").</returns>
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

	/// <summary>Gets a reference to a collection of properties that define the appearance of the Continue button.</summary>
	/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.Style" /> that contains the properties that define the appearance of the Continue button.</returns>
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
					((IStateManager)_continueButtonStyle).TrackViewState();
				}
			}
			return _continueButtonStyle;
		}
	}

	/// <summary>Gets or sets the text caption displayed on the Continue button.</summary>
	/// <returns>The text caption displayed on the Continue button. The default value is "Continue". The default text for the control is localized based on the server's current locale.</returns>
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

	/// <summary>Gets or sets the type of button rendered as the Continue button.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> enumeration values. The default value is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified button type is not one of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values.</exception>
	[DefaultValue(ButtonType.Button)]
	public virtual ButtonType ContinueButtonType
	{
		get
		{
			object obj = ViewState["ContinueButtonType"];
			if (obj == null)
			{
				return ButtonType.Button;
			}
			return (ButtonType)obj;
		}
		set
		{
			ViewState["ContinueButtonType"] = value;
		}
	}

	/// <summary>Gets or sets the URL of the page that the user will see after clicking the Continue button on the success page.</summary>
	/// <returns>The URL of the destination page. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Themeable(false)]
	public virtual string ContinueDestinationPageUrl
	{
		get
		{
			object obj = ViewState["ContinueDestinationPageUrl"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("ContinueDestinationPageUrl");
			}
			else
			{
				ViewState["ContinueDestinationPageUrl"] = value;
			}
		}
	}

	/// <summary>Gets or sets the URL of an image displayed for the Create User button.</summary>
	/// <returns>The URL of the image displayed for the Create User button. The default value is an empty string ("").</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string CreateUserButtonImageUrl
	{
		get
		{
			return ViewState.GetString("CreateUserButtonImageUrl", string.Empty);
		}
		set
		{
			ViewState["CreateUserButtonImageUrl"] = value;
		}
	}

	/// <summary>Gets a reference to a collection of properties that define the appearance of the Create User button.</summary>
	/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.Style" /> that contains the properties that define the appearance of the Create User button.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public Style CreateUserButtonStyle
	{
		get
		{
			if (_createUserButtonStyle == null)
			{
				_createUserButtonStyle = new Style();
				if (base.IsTrackingViewState)
				{
					((IStateManager)_createUserButtonStyle).TrackViewState();
				}
			}
			return _createUserButtonStyle;
		}
	}

	/// <summary>Gets or sets the text caption displayed on the Create User button.</summary>
	/// <returns>The text caption displayed on the Create User button for the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control. The default value is "Submit". The default text for the control is localized based on the server's current locale.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified button type is not one of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values.</exception>
	[Localizable(true)]
	public virtual string CreateUserButtonText
	{
		get
		{
			return ViewState.GetString("CreateUserButtonText", "Create User");
		}
		set
		{
			ViewState["CreateUserButtonText"] = value;
		}
	}

	/// <summary>Gets or sets the type of button rendered as the Create User button.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> enumeration values. The default value is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified button type is not one of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values.</exception>
	[DefaultValue(ButtonType.Button)]
	public virtual ButtonType CreateUserButtonType
	{
		get
		{
			object obj = ViewState["CreateUserButtonType"];
			if (obj == null)
			{
				return ButtonType.Button;
			}
			return (ButtonType)obj;
		}
		set
		{
			ViewState["CreateUserButtonType"] = value;
		}
	}

	/// <summary>Gets a reference to the template for the user account creation step.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.CreateUserWizardStep" /> value that represents the user account creation step.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public CreateUserWizardStep CreateUserStep
	{
		get
		{
			if (_createUserWizardStep == null)
			{
				for (int i = 0; i < WizardSteps.Count; i++)
				{
					if (WizardSteps[i] is CreateUserWizardStep)
					{
						_createUserWizardStep = (CreateUserWizardStep)WizardSteps[i];
						if (_createUserWizardStep.Wizard == null)
						{
							_createUserWizardStep.SetWizard(this);
						}
					}
				}
			}
			return _createUserWizardStep;
		}
	}

	/// <summary>Gets or sets a value indicating whether the new user should be allowed to log on to the Web site.</summary>
	/// <returns>
	///     <see langword="true" /> if the new user is allowed to log on to the Web site; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[Themeable(false)]
	public virtual bool DisableCreatedUser
	{
		get
		{
			object obj = ViewState["DisableCreatedUser"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return false;
		}
		set
		{
			ViewState["DisableCreatedUser"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether to display the sidebar area of the control.</summary>
	/// <returns>
	///     <see langword="true" /> if the sidebar area should be displayed for the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	public override bool DisplaySideBar
	{
		get
		{
			return ViewState.GetBool("DisplaySideBar", def: false);
		}
		set
		{
			ViewState["DisplaySideBar"] = value;
			base.ChildControlsCreated = false;
		}
	}

	/// <summary>Gets or sets the error message displayed when the user enters an e-mail address that is already in use in the membership provider.</summary>
	/// <returns>The error message displayed when the user enters an e-mail address that is already in use in the membership provider. The default value is "The e-mail address that you entered is already in use. Please enter a different e-mail address." The default text for the control is localized based on the server's current locale.</returns>
	[Localizable(true)]
	public virtual string DuplicateEmailErrorMessage
	{
		get
		{
			object obj = ViewState["DuplicateEmailErrorMessage"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("The e-mail address that you entered is already in use. Please enter a different e-mail address.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("DuplicateEmailErrorMessage");
			}
			else
			{
				ViewState["DuplicateEmailErrorMessage"] = value;
			}
		}
	}

	/// <summary>Gets or sets the error message displayed when the user enters a user name that is already in use in the membership provider.</summary>
	/// <returns>The error message displayed when the user enters a user name that is already in the membership provider. The default value is "Please enter a different user name." The default text for the control is localized based on the server's current locale.</returns>
	[Localizable(true)]
	public virtual string DuplicateUserNameErrorMessage
	{
		get
		{
			object obj = ViewState["DuplicateUserNameErrorMessage"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Please enter a different user name.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("DuplicateUserNameErrorMessage");
			}
			else
			{
				ViewState["DuplicateUserNameErrorMessage"] = value;
			}
		}
	}

	/// <summary>Gets or sets the URL of an image to display next to the link to the user profile editing page.</summary>
	/// <returns>The URL of an image to display next to the link to the user profile editing page. The default value is an empty string ("").</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string EditProfileIconUrl
	{
		get
		{
			object obj = ViewState["EditProfileIconUrl"];
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
				ViewState.Remove("EditProfileIconUrl");
			}
			else
			{
				ViewState["EditProfileIconUrl"] = value;
			}
		}
	}

	/// <summary>Gets or sets the text caption for the link to the user profile editing page.</summary>
	/// <returns>The text caption for the link to the user profile editing page. The default value is an empty string ("").</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public virtual string EditProfileText
	{
		get
		{
			object obj = ViewState["EditProfileText"];
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
				ViewState.Remove("EditProfileText");
			}
			else
			{
				ViewState["EditProfileText"] = value;
			}
		}
	}

	/// <summary>Gets or sets the URL of the user profile editing page.</summary>
	/// <returns>The URL of the user profile editing page. The default value is an empty string ("").</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string EditProfileUrl
	{
		get
		{
			object obj = ViewState["EditProfileUrl"];
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
				ViewState.Remove("EditProfileUrl");
			}
			else
			{
				ViewState["EditProfileUrl"] = value;
			}
		}
	}

	/// <summary>Gets or sets the e-mail address entered by the user.</summary>
	/// <returns>The e-mail address entered by the user. The default value is an empty string ("").</returns>
	[DefaultValue("")]
	public virtual string Email
	{
		get
		{
			object obj = ViewState["Email"];
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
				ViewState.Remove("Email");
			}
			else
			{
				ViewState["Email"] = value;
			}
		}
	}

	/// <summary>Gets or sets the text of the label for the e-mail text box.</summary>
	/// <returns>The label text that identifies the e-mail text box. The default value is "E-mail:". The default text for the control is localized based on the server's current locale.</returns>
	[Localizable(true)]
	public virtual string EmailLabelText
	{
		get
		{
			object obj = ViewState["EmailLabelText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("E-mail:");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("EmailLabelText");
			}
			else
			{
				ViewState["EmailLabelText"] = value;
			}
		}
	}

	/// <summary>Gets or sets a regular expression used to validate the provided e-mail address.</summary>
	/// <returns>A string containing the regular expression used to validate an e-mail address. The default value is an empty string ("").</returns>
	public virtual string EmailRegularExpression
	{
		get
		{
			object obj = ViewState["EmailRegularExpression"];
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
				ViewState.Remove("EmailRegularExpression");
			}
			else
			{
				ViewState["EmailRegularExpression"] = value;
			}
		}
	}

	/// <summary>Gets or sets the error message displayed when the entered e-mail address does not pass the site's criteria for e-mail addresses.</summary>
	/// <returns>The error message displayed when the entered e-mail address does not pass the regular expression defined in the <see cref="P:System.Web.UI.WebControls.CreateUserWizard.EmailRegularExpression" /> property. The default is "Please enter a different e-mail address." The default text for the control is localized based on the server's current locale.</returns>
	public virtual string EmailRegularExpressionErrorMessage
	{
		get
		{
			object obj = ViewState["EmailRegularExpressionErrorMessage"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Please enter a different e-mail.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("EmailRegularExpressionErrorMessage");
			}
			else
			{
				ViewState["EmailRegularExpressionErrorMessage"] = value;
			}
		}
	}

	/// <summary>Gets or sets the error message shown to the user when an e-mail address is not entered in the e-mail text box.</summary>
	/// <returns>The error message shown to the user when an e-mail address is not entered in the e-mail text box. The default value is "E-mail is required." The default text for the control is localized based on the server's current locale.</returns>
	[Localizable(true)]
	public virtual string EmailRequiredErrorMessage
	{
		get
		{
			object obj = ViewState["EmailRequiredErrorMessage"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("E-mail is required.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("EmailRequiredErrorMessage");
			}
			else
			{
				ViewState["EmailRequiredErrorMessage"] = value;
			}
		}
	}

	/// <summary>Gets a reference to a collection of style properties that define the appearance of error messages.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> containing the style properties that define the appearance of error messages on the control. The default is null.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public TableItemStyle ErrorMessageStyle
	{
		get
		{
			if (_errorMessageStyle == null)
			{
				_errorMessageStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					((IStateManager)_errorMessageStyle).TrackViewState();
				}
			}
			return _errorMessageStyle;
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

	/// <summary>Gets or sets the text caption for the link to the Help page.</summary>
	/// <returns>The text caption for the link to the Help page. The default value is an empty string ("").</returns>
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

	/// <summary>Gets or sets the URL of the Help page.</summary>
	/// <returns>The URL of the Help page. The default value is an empty string ("").</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

	/// <summary>Gets or sets a collection of properties that define the appearance of hyperlinks.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of hyperlinks.</returns>
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
					((IStateManager)_hyperLinkStyle).TrackViewState();
				}
			}
			return _hyperLinkStyle;
		}
	}

	/// <summary>Gets or sets instructions for creating a new user account.</summary>
	/// <returns>The instruction text for creating a new user account. The default value is an empty string ("").</returns>
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

	/// <summary>Gets a reference to a collection of properties that define the appearance of instruction text.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of instruction text.</returns>
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
					((IStateManager)_instructionTextStyle).TrackViewState();
				}
			}
			return _instructionTextStyle;
		}
	}

	/// <summary>Gets or sets the message displayed when the password retrieval answer is not valid.</summary>
	/// <returns>The message displayed when the password retrieval answer is not valid. The default is "Please enter a different security answer." The default text for the control is localized based on the server's current locale.</returns>
	[Localizable(true)]
	public virtual string InvalidAnswerErrorMessage
	{
		get
		{
			object obj = ViewState["InvalidAnswerErrorMessage"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Please enter a different security answer.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("InvalidAnswerErrorMessage");
			}
			else
			{
				ViewState["InvalidAnswerErrorMessage"] = value;
			}
		}
	}

	/// <summary>Gets or sets the message displayed when the entered e-mail address is not valid.</summary>
	/// <returns>The message displayed when the e-mail address entered is not valid. The default is "Please enter a valid e-mail address." The default text for the control is localized based on the server's current locale.</returns>
	[Localizable(true)]
	public virtual string InvalidEmailErrorMessage
	{
		get
		{
			object obj = ViewState["InvalidEmailErrorMessage"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Please enter a valid e-mail address.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("InvalidEmailErrorMessage");
			}
			else
			{
				ViewState["InvalidEmailErrorMessage"] = value;
			}
		}
	}

	/// <summary>Gets or sets the message displayed when the password entered is not valid.</summary>
	/// <returns>The message displayed when the password entered is not valid. The default is "Please enter a valid password." The default text for the control is localized based on the server's current locale.</returns>
	[MonoTODO("take the values from membership provider")]
	[Localizable(true)]
	public virtual string InvalidPasswordErrorMessage
	{
		get
		{
			object obj = ViewState["InvalidPasswordErrorMessage"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Password length minimum: {0}. Non-alphanumeric characters required: {1}.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("InvalidPasswordErrorMessage");
			}
			else
			{
				ViewState["InvalidPasswordErrorMessage"] = value;
			}
		}
	}

	/// <summary>Gets or sets the message displayed when the password retrieval question entered is not valid.</summary>
	/// <returns>The message displayed when the password retrieval question is not valid. The default is "Please enter a valid answer." The default text for the control is localized based on the server's current locale.</returns>
	[Localizable(true)]
	public virtual string InvalidQuestionErrorMessage
	{
		get
		{
			object obj = ViewState["InvalidQuestionErrorMessage"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Please enter a different security question.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("InvalidQuestionErrorMessage");
			}
			else
			{
				ViewState["InvalidQuestionErrorMessage"] = value;
			}
		}
	}

	/// <summary>Gets a reference to a collection of properties that define the appearance of labels.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of labels.</returns>
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
					((IStateManager)_labelStyle).TrackViewState();
				}
			}
			return _labelStyle;
		}
	}

	/// <summary>Gets or sets a value indicating whether to log in the new user after creating the user account.</summary>
	/// <returns>
	///     <see langword="true" /> if the new user should be logged in after creating the user account; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[Themeable(false)]
	public virtual bool LoginCreatedUser
	{
		get
		{
			object obj = ViewState["LoginCreatedUser"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return true;
		}
		set
		{
			ViewState["LoginCreatedUser"] = value;
		}
	}

	/// <summary>Gets a reference to a collection of properties that define the characteristics of the e-mail message sent to new users.</summary>
	/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.MailDefinition" /> object that defines the e-mail message sent to a new user.</returns>
	/// <exception cref="T:System.Web.HttpException">
	///         <see cref="P:System.Web.UI.WebControls.MailDefinition.From" /> is not set to an e-mail address.</exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
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

	/// <summary>Gets or sets the membership provider called to create user accounts.</summary>
	/// <returns>The <see cref="T:System.Web.Security.MembershipProvider" /> used to create user accounts. The default is <see cref="F:System.String.Empty" />.</returns>
	/// <exception cref="T:System.Web.HttpException">The specified membership provider is not defined in the Web.config file.</exception>
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
			_provider = null;
		}
	}

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

	/// <summary>Gets the password entered by the user.</summary>
	/// <returns>The password entered by the user. The default value is an empty string ("").</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public virtual string Password => _password;

	/// <summary>Gets a reference to a collection of properties that define the appearance of the text that describes password requirements.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of the text that describes password requirements.</returns>
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
					((IStateManager)_passwordHintStyle).TrackViewState();
				}
			}
			return _passwordHintStyle;
		}
	}

	/// <summary>Gets or sets the text that describes password requirements.</summary>
	/// <returns>The text that describes password requirements. The default value is an empty string ("").</returns>
	[Localizable(true)]
	public virtual string PasswordHintText
	{
		get
		{
			object obj = ViewState["PasswordHintText"];
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
				ViewState.Remove("PasswordHintText");
			}
			else
			{
				ViewState["PasswordHintText"] = value;
			}
		}
	}

	/// <summary>Gets or sets the text of the label for the password text box.</summary>
	/// <returns>The text of the label for the password text box. The default value is "Password:". The default text for the control is localized based on the server's current locale.</returns>
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
			return Locale.GetText("Password:");
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

	/// <summary>Gets or sets a regular expression used to validate the provided password.</summary>
	/// <returns>A string containing the regular expression used to validate the provided password. The default value is an empty string ("").</returns>
	public virtual string PasswordRegularExpression
	{
		get
		{
			object obj = ViewState["PasswordRegularExpression"];
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
				ViewState.Remove("PasswordRegularExpression");
			}
			else
			{
				ViewState["PasswordRegularExpression"] = value;
			}
		}
	}

	/// <summary>Gets or sets the error message shown when the password entered does not conform to the site's password requirements.</summary>
	/// <returns>The error message shown when the password entered does not pass the regular expression defined in the <see cref="P:System.Web.UI.WebControls.CreateUserWizard.PasswordRegularExpression" /> property. The default is "Please enter a different password." The default text for the control is localized based on the server's current locale.</returns>
	public virtual string PasswordRegularExpressionErrorMessage
	{
		get
		{
			object obj = ViewState["PasswordRegularExpressionErrorMessage"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Please enter a different password.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("PasswordRegularExpressionErrorMessage");
			}
			else
			{
				ViewState["PasswordRegularExpressionErrorMessage"] = value;
			}
		}
	}

	/// <summary>Gets or sets the text of the error message shown when the user does not enter a password.</summary>
	/// <returns>The error message shown when the user does not enter a password. The default value is "Password is required." The default text for the control is localized based on the server's current locale.</returns>
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

	/// <summary>Gets or sets the password recovery confirmation question entered by the user.</summary>
	/// <returns>The password recovery confirmation question entered by the user. The default value is an empty string ("").</returns>
	[DefaultValue("")]
	[Localizable(true)]
	[Themeable(false)]
	public virtual string Question
	{
		get
		{
			object obj = ViewState["Question"];
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
				ViewState.Remove("Question");
			}
			else
			{
				ViewState["Question"] = value;
			}
		}
	}

	/// <summary>Gets or sets the text of the label for the question text box.</summary>
	/// <returns>The text of the label for the question text box. The default value is "Security Question:". The default text for the control is localized based on the server's current locale.</returns>
	[Localizable(true)]
	public virtual string QuestionLabelText
	{
		get
		{
			object obj = ViewState["QuestionLabelText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Security Question:");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("QuestionLabelText");
			}
			else
			{
				ViewState["QuestionLabelText"] = value;
			}
		}
	}

	/// <summary>Gets or sets the error message that is displayed when the user does not enter a password confirmation question.</summary>
	/// <returns>The error message that is displayed when the user does not enter a password confirmation question. The default value is "Security question is required." The default text for the control is localized based on the server's current locale.</returns>
	[Localizable(true)]
	public virtual string QuestionRequiredErrorMessage
	{
		get
		{
			object obj = ViewState["QuestionRequiredErrorMessage"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Security question is required.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("QuestionRequiredErrorMessage");
			}
			else
			{
				ViewState["QuestionRequiredErrorMessage"] = value;
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether an e-mail address is required for the Web site user.</summary>
	/// <returns>
	///     <see langword="true" /> if an e-mail address is required; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[Themeable(false)]
	public virtual bool RequireEmail
	{
		get
		{
			object obj = ViewState["RequireEmail"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return true;
		}
		set
		{
			ViewState["RequireEmail"] = value;
		}
	}

	/// <summary>Gets or sets a value that is used to render alternate text that notifies screen readers to skip the sidebar area's content.</summary>
	/// <returns>A string that the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> renders as alternate text with an invisible image, as a hint to screen readers. The default is an empty string ("").</returns>
	[DefaultValue("")]
	[MonoTODO("doesnt work")]
	public override string SkipLinkText
	{
		get
		{
			object obj = ViewState["SkipLinkText"];
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
				ViewState.Remove("SkipLinkText");
			}
			else
			{
				ViewState["SkipLinkText"] = value;
			}
		}
	}

	/// <summary>Gets a reference to a collection of properties that define the appearance of text box controls.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that contains properties that define the appearance of text box controls.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public Style TextBoxStyle
	{
		get
		{
			if (_textBoxStyle == null)
			{
				_textBoxStyle = new Style();
				if (base.IsTrackingViewState)
				{
					((IStateManager)_textBoxStyle).TrackViewState();
				}
			}
			return _textBoxStyle;
		}
	}

	/// <summary>Gets a reference to a collection of properties that define the appearance of titles.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of titles.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public TableItemStyle TitleTextStyle
	{
		get
		{
			if (_titleTextStyle == null)
			{
				_titleTextStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					((IStateManager)_titleTextStyle).TrackViewState();
				}
			}
			return _titleTextStyle;
		}
	}

	/// <summary>Gets or sets the error message displayed when an error returned by the membership provider is not defined.</summary>
	/// <returns>The error message displayed when an error returned by the membership provider is not defined. The default value is "Your account was not created. Please try again." The default text of the control is localized based on the server's current locale.</returns>
	[Localizable(true)]
	public virtual string UnknownErrorMessage
	{
		get
		{
			object obj = ViewState["UnknownErrorMessage"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Your account was not created. Please try again.");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("UnknownErrorMessage");
			}
			else
			{
				ViewState["UnknownErrorMessage"] = value;
			}
		}
	}

	/// <summary>Gets or sets the user name entered by the user.</summary>
	/// <returns>The user name entered by the user. The default value is an empty string ("").</returns>
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

	/// <summary>Gets or sets the text of the label for the user name text box.</summary>
	/// <returns>The text of the label for the user name text box. The default value is "User Name:". The default text for the control is localized based on the server's current locale.</returns>
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

	/// <summary>Gets or sets the error message displayed when the user name text box is left blank.</summary>
	/// <returns>The error message displayed when the user name text box is left blank. The default value is "User Name is required." The default text for the control is localized based on the server's current locale.</returns>
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

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.Style" /> object that allows you to set the appearance of the validation error messages.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> containing the style properties that define the appearance of validation error messages on the control. The default is null.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public Style ValidatorTextStyle
	{
		get
		{
			if (_validatorTextStyle == null)
			{
				_validatorTextStyle = new Style();
				if (base.IsTrackingViewState)
				{
					((IStateManager)_validatorTextStyle).TrackViewState();
				}
			}
			return _validatorTextStyle;
		}
	}

	/// <summary>Gets a reference to a collection containing all the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects defined for the control. </summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> representing all the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects defined for the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control.</returns>
	[Editor("System.Web.UI.Design.WebControls.CreateUserWizardStepCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public override WizardStepCollection WizardSteps => base.WizardSteps;

	/// <summary>Gets a value indicating whether the user is required to enter a password confirmation question and answer.</summary>
	/// <returns>
	///     <see langword="true" /> if the user is required to enter a password confirmation question and answer; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[DefaultValue(true)]
	protected internal bool QuestionAndAnswerRequired => MembershipProviderInternal.RequiresQuestionAndAnswer;

	internal override ITemplate SideBarItemTemplate => new SideBarLabelTemplate(this);

	/// <summary>Occurs when the user clicks the Continue button in the final user account creation step.</summary>
	public event EventHandler ContinueButtonClick
	{
		add
		{
			base.Events.AddHandler(ContinueButtonClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ContinueButtonClickEvent, value);
		}
	}

	/// <summary>Occurs after the membership provider has created the new Web site user account.</summary>
	public event EventHandler CreatedUser
	{
		add
		{
			base.Events.AddHandler(CreatedUserEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(CreatedUserEvent, value);
		}
	}

	/// <summary>Occurs when the membership provider cannot create the specified user account.</summary>
	public event CreateUserErrorEventHandler CreateUserError
	{
		add
		{
			base.Events.AddHandler(CreateUserErrorEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(CreateUserErrorEvent, value);
		}
	}

	/// <summary>Occurs before the membership provider is called to create the new Web site user account.</summary>
	public event LoginCancelEventHandler CreatingUser
	{
		add
		{
			base.Events.AddHandler(CreatingUserEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(CreatingUserEvent, value);
		}
	}

	/// <summary>Occurs before the user is sent an e-mail confirmation that an account has been created.</summary>
	public event MailMessageEventHandler SendingMail
	{
		add
		{
			base.Events.AddHandler(SendingMailEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SendingMailEvent, value);
		}
	}

	/// <summary>Occurs when there is an SMTP error sending e-mail to the new user.</summary>
	public event SendMailErrorEventHandler SendMailError
	{
		add
		{
			base.Events.AddHandler(SendMailErrorEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SendMailErrorEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> class.</summary>
	public CreateUserWizard()
	{
	}

	internal override void InstantiateTemplateStep(TemplatedWizardStep step)
	{
		if (step is CreateUserWizardStep)
		{
			InstantiateCreateUserWizardStep((CreateUserWizardStep)step);
		}
		else if (step is CompleteWizardStep)
		{
			InstantiateCompleteWizardStep((CompleteWizardStep)step);
		}
		else
		{
			base.InstantiateTemplateStep(step);
		}
	}

	private void InstantiateCompleteWizardStep(CompleteWizardStep step)
	{
		CompleteStepContainer completeStepContainer = new CompleteStepContainer(this);
		if (step.ContentTemplate != null)
		{
			step.ContentTemplate.InstantiateIn(completeStepContainer.InnerCell);
		}
		else
		{
			new CompleteStepTemplate(this).InstantiateIn(completeStepContainer.InnerCell);
			completeStepContainer.ConfirmDefaultTemplate();
		}
		step.ContentTemplateContainer = completeStepContainer;
		step.Controls.Clear();
		step.Controls.Add(completeStepContainer);
		BaseWizardNavigationContainer baseWizardNavigationContainer = new BaseWizardNavigationContainer();
		if (step.CustomNavigationTemplate != null)
		{
			step.CustomNavigationTemplate.InstantiateIn(baseWizardNavigationContainer);
			RegisterCustomNavigation(step, baseWizardNavigationContainer);
		}
		step.CustomNavigationTemplateContainer = baseWizardNavigationContainer;
	}

	private void InstantiateCreateUserWizardStep(CreateUserWizardStep step)
	{
		CreateUserStepContainer createUserStepContainer = new CreateUserStepContainer(this);
		if (step.ContentTemplate != null)
		{
			step.ContentTemplate.InstantiateIn(createUserStepContainer.InnerCell);
		}
		else
		{
			new CreateUserStepTemplate(this).InstantiateIn(createUserStepContainer.InnerCell);
			createUserStepContainer.ConfirmDefaultTemplate();
			createUserStepContainer.EnsureValidatorsState();
		}
		step.ContentTemplateContainer = createUserStepContainer;
		step.Controls.Clear();
		step.Controls.Add(createUserStepContainer);
		CreateUserNavigationContainer createUserNavigationContainer = new CreateUserNavigationContainer(this);
		if (step.CustomNavigationTemplate != null)
		{
			step.CustomNavigationTemplate.InstantiateIn(createUserNavigationContainer);
		}
		else
		{
			new CreateUserStepNavigationTemplate(this).InstantiateIn(createUserNavigationContainer);
			createUserNavigationContainer.ConfirmDefaultTemplate();
		}
		RegisterCustomNavigation(step, createUserNavigationContainer);
		step.CustomNavigationTemplateContainer = createUserNavigationContainer;
	}

	/// <summary>Called by the ASP.NET page framework to notify this control to create any child controls that it contains in preparation for posting back or rendering.</summary>
	protected internal override void CreateChildControls()
	{
		if (CreateUserStep == null)
		{
			WizardSteps.AddAt(0, new CreateUserWizardStep());
		}
		if (CompleteStep == null)
		{
			WizardSteps.AddAt(WizardSteps.Count, new CompleteWizardStep());
		}
		base.CreateChildControls();
	}

	protected override void CreateControlHierarchy()
	{
		base.CreateControlHierarchy();
		if (!(CreateUserStep.ContentTemplateContainer is CreateUserStepContainer createUserStepContainer))
		{
			return;
		}
		if (createUserStepContainer.UserNameTextBox is IEditableTextControl editableTextControl)
		{
			editableTextControl.TextChanged += UserName_TextChanged;
		}
		if (!AutoGeneratePassword)
		{
			if (createUserStepContainer.PasswordTextBox is IEditableTextControl editableTextControl2)
			{
				editableTextControl2.TextChanged += Password_TextChanged;
			}
			if (createUserStepContainer.ConfirmPasswordTextBox is IEditableTextControl editableTextControl3)
			{
				editableTextControl3.TextChanged += ConfirmPassword_TextChanged;
			}
		}
		if (RequireEmail && createUserStepContainer.EmailTextBox is IEditableTextControl editableTextControl4)
		{
			editableTextControl4.TextChanged += Email_TextChanged;
		}
		if (QuestionAndAnswerRequired)
		{
			if (createUserStepContainer.QuestionTextBox is IEditableTextControl editableTextControl5)
			{
				editableTextControl5.TextChanged += Question_TextChanged;
			}
			if (createUserStepContainer.AnswerTextBox is IEditableTextControl editableTextControl6)
			{
				editableTextControl6.TextChanged += Answer_TextChanged;
			}
		}
		_errorMessageLabel = createUserStepContainer.ErrorMessageLabel;
	}

	/// <summary>Gets design-time data for a control.</summary>
	/// <returns>None</returns>
	[MonoTODO("Not Implemented")]
	protected override IDictionary GetDesignModeState()
	{
		throw new NotImplementedException();
	}

	/// <summary>Determines whether the event for the server control is passed up the page's UI server control hierarchy.</summary>
	/// <param name="source"> None</param>
	/// <param name="e"> None</param>
	/// <returns>A Boolean value.</returns>
	protected override bool OnBubbleEvent(object source, EventArgs e)
	{
		CommandEventArgs commandEventArgs = e as CommandEventArgs;
		if (e != null && commandEventArgs.CommandName == ContinueButtonCommandName)
		{
			ProcessContinueEvent();
			return true;
		}
		return base.OnBubbleEvent(source, e);
	}

	private void ProcessContinueEvent()
	{
		OnContinueButtonClick(EventArgs.Empty);
		if (ContinueDestinationPageUrl.Length > 0)
		{
			Context.Response.Redirect(ContinueDestinationPageUrl);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.CreateUserWizard.ContinueButtonClick" /> event when the user clicks the Continue button on the final user account creation step.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnContinueButtonClick(EventArgs e)
	{
		if (base.Events != null)
		{
			((EventHandler)base.Events[ContinueButtonClick])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.CreateUserWizard.CreatedUser" /> event after the membership provider creates the user account.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnCreatedUser(EventArgs e)
	{
		if (base.Events != null)
		{
			((EventHandler)base.Events[CreatedUser])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.CreateUserWizard.CreateUserError" /> event when there is a problem creating the specified user account.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.CreateUserErrorEventArgs" /> with the data for the event.</param>
	protected virtual void OnCreateUserError(CreateUserErrorEventArgs e)
	{
		if (base.Events != null)
		{
			((CreateUserErrorEventHandler)base.Events[CreateUserError])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.CreateUserWizard.CreatingUser" /> event prior to calling the membership provider to create the new user account.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.LoginCancelEventArgs" /> containing the event data.</param>
	protected virtual void OnCreatingUser(LoginCancelEventArgs e)
	{
		if (base.Events != null)
		{
			((LoginCancelEventHandler)base.Events[CreatingUser])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.NextButtonClick" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WizardNavigationEventArgs" /> containing the event data.</param>
	protected override void OnNextButtonClick(WizardNavigationEventArgs e)
	{
		if (base.ActiveStep == CreateUserStep)
		{
			if (!CreateUser())
			{
				e.Cancel = true;
			}
			else if (LoginCreatedUser)
			{
				Login();
			}
		}
		base.OnNextButtonClick(e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	/// <exception cref="T:System.Web.HttpException">The membership provider for the page cannot be found. For more information, see <see cref="P:System.Web.Security.Membership.Providers" />.</exception>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.CreateUserWizard.SendingMail" /> event before an e-mail message is sent to a new user.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.MailMessageEventArgs" /> containing the event data.</param>
	protected virtual void OnSendingMail(MailMessageEventArgs e)
	{
		if (base.Events != null)
		{
			((MailMessageEventHandler)base.Events[SendingMail])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.CreateUserWizard.SendMailError" /> event when e-mail cannot be sent to the new user.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SendMailErrorEventArgs" /> containing the event data.</param>
	protected virtual void OnSendMailError(SendMailErrorEventArgs e)
	{
		if (base.Events != null)
		{
			((SendMailErrorEventHandler)base.Events[SendMailError])?.Invoke(this, e);
		}
	}

	/// <summary>Restores view-state information from a previous page request that was saved by the SaveViewState method.</summary>
	/// <param name="savedState">An object that represents the control state to restore.</param>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="savedState" /> is not a valid <see cref="P:System.Web.UI.Control.ViewState" />.</exception>
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
			((IStateManager)TextBoxStyle).LoadViewState(array[1]);
		}
		if (array[2] != null)
		{
			((IStateManager)ValidatorTextStyle).LoadViewState(array[2]);
		}
		if (array[3] != null)
		{
			((IStateManager)CompleteSuccessTextStyle).LoadViewState(array[3]);
		}
		if (array[4] != null)
		{
			((IStateManager)ErrorMessageStyle).LoadViewState(array[4]);
		}
		if (array[5] != null)
		{
			((IStateManager)HyperLinkStyle).LoadViewState(array[5]);
		}
		if (array[6] != null)
		{
			((IStateManager)InstructionTextStyle).LoadViewState(array[6]);
		}
		if (array[7] != null)
		{
			((IStateManager)LabelStyle).LoadViewState(array[7]);
		}
		if (array[8] != null)
		{
			((IStateManager)PasswordHintStyle).LoadViewState(array[8]);
		}
		if (array[9] != null)
		{
			((IStateManager)TitleTextStyle).LoadViewState(array[9]);
		}
		if (array[10] != null)
		{
			((IStateManager)CreateUserButtonStyle).LoadViewState(array[10]);
		}
		if (array[11] != null)
		{
			((IStateManager)ContinueButtonStyle).LoadViewState(array[11]);
		}
		if (array[12] != null)
		{
			((IStateManager)MailDefinition).LoadViewState(array[12]);
		}
		((CreateUserStepContainer)CreateUserStep.ContentTemplateContainer).EnsureValidatorsState();
	}

	/// <summary>Saves any state that was modified after the <see cref="M:System.Web.UI.WebControls.Style.TrackViewState" /> method was invoked.</summary>
	/// <returns>An object that contains the current view state of the control; otherwise, if there is no view state associated with the control, <see langword="null" />.
	/// </returns>
	protected override object SaveViewState()
	{
		object[] array = new object[13]
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
			null
		};
		if (_textBoxStyle != null)
		{
			array[1] = ((IStateManager)_textBoxStyle).SaveViewState();
		}
		if (_validatorTextStyle != null)
		{
			array[2] = ((IStateManager)_validatorTextStyle).SaveViewState();
		}
		if (_completeSuccessTextStyle != null)
		{
			array[3] = ((IStateManager)_completeSuccessTextStyle).SaveViewState();
		}
		if (_errorMessageStyle != null)
		{
			array[4] = ((IStateManager)_errorMessageStyle).SaveViewState();
		}
		if (_hyperLinkStyle != null)
		{
			array[5] = ((IStateManager)_hyperLinkStyle).SaveViewState();
		}
		if (_instructionTextStyle != null)
		{
			array[6] = ((IStateManager)_instructionTextStyle).SaveViewState();
		}
		if (_labelStyle != null)
		{
			array[7] = ((IStateManager)_labelStyle).SaveViewState();
		}
		if (_passwordHintStyle != null)
		{
			array[8] = ((IStateManager)_passwordHintStyle).SaveViewState();
		}
		if (_titleTextStyle != null)
		{
			array[9] = ((IStateManager)_titleTextStyle).SaveViewState();
		}
		if (_createUserButtonStyle != null)
		{
			array[10] = ((IStateManager)_createUserButtonStyle).SaveViewState();
		}
		if (_continueButtonStyle != null)
		{
			array[11] = ((IStateManager)_continueButtonStyle).SaveViewState();
		}
		if (_mailDefinition != null)
		{
			array[12] = ((IStateManager)_mailDefinition).SaveViewState();
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

	/// <summary>Sets design-time data for a control.</summary>
	/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> containing the design-time data for the control.</param>
	[MonoTODO("for design-time usage - no more details available")]
	protected override void SetDesignModeState(IDictionary data)
	{
		base.SetDesignModeState(data);
	}

	/// <summary>Marks the starting point to begin tracking changes to the control as part of the control viewstate.</summary>
	protected override void TrackViewState()
	{
		base.TrackViewState();
		if (_textBoxStyle != null)
		{
			((IStateManager)_textBoxStyle).TrackViewState();
		}
		if (_validatorTextStyle != null)
		{
			((IStateManager)_validatorTextStyle).TrackViewState();
		}
		if (_completeSuccessTextStyle != null)
		{
			((IStateManager)_completeSuccessTextStyle).TrackViewState();
		}
		if (_errorMessageStyle != null)
		{
			((IStateManager)_errorMessageStyle).TrackViewState();
		}
		if (_hyperLinkStyle != null)
		{
			((IStateManager)_hyperLinkStyle).TrackViewState();
		}
		if (_instructionTextStyle != null)
		{
			((IStateManager)_instructionTextStyle).TrackViewState();
		}
		if (_labelStyle != null)
		{
			((IStateManager)_labelStyle).TrackViewState();
		}
		if (_passwordHintStyle != null)
		{
			((IStateManager)_passwordHintStyle).TrackViewState();
		}
		if (_titleTextStyle != null)
		{
			((IStateManager)_titleTextStyle).TrackViewState();
		}
		if (_createUserButtonStyle != null)
		{
			((IStateManager)_createUserButtonStyle).TrackViewState();
		}
		if (_continueButtonStyle != null)
		{
			((IStateManager)_continueButtonStyle).TrackViewState();
		}
		if (_mailDefinition != null)
		{
			((IStateManager)_mailDefinition).TrackViewState();
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

	private void ConfirmPassword_TextChanged(object sender, EventArgs e)
	{
		_confirmPassword = ((ITextControl)sender).Text;
	}

	private void Email_TextChanged(object sender, EventArgs e)
	{
		Email = ((ITextControl)sender).Text;
	}

	private void Question_TextChanged(object sender, EventArgs e)
	{
		Question = ((ITextControl)sender).Text;
	}

	private void Answer_TextChanged(object sender, EventArgs e)
	{
		Answer = ((ITextControl)sender).Text;
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

	private bool CreateUser()
	{
		if (!Page.IsValid)
		{
			return false;
		}
		if (AutoGeneratePassword)
		{
			_password = GeneratePassword();
		}
		OnCreatingUser(new LoginCancelEventArgs(cancel: false));
		MembershipCreateStatus status;
		MembershipUser membershipUser = MembershipProviderInternal.CreateUser(UserName, Password, Email, Question, Answer, !DisableCreatedUser, null, out status);
		if (membershipUser != null && status == MembershipCreateStatus.Success)
		{
			OnCreatedUser(new EventArgs());
			SendPasswordByMail(membershipUser, Password);
			return true;
		}
		switch (status)
		{
		case MembershipCreateStatus.DuplicateUserName:
			ShowErrorMessage(DuplicateUserNameErrorMessage);
			break;
		case MembershipCreateStatus.InvalidPassword:
			ShowErrorMessage(string.Format(InvalidPasswordErrorMessage, MembershipProviderInternal.MinRequiredPasswordLength, MembershipProviderInternal.MinRequiredNonAlphanumericCharacters));
			break;
		case MembershipCreateStatus.DuplicateEmail:
			ShowErrorMessage(DuplicateEmailErrorMessage);
			break;
		case MembershipCreateStatus.InvalidEmail:
			ShowErrorMessage(InvalidEmailErrorMessage);
			break;
		case MembershipCreateStatus.InvalidQuestion:
			ShowErrorMessage(InvalidQuestionErrorMessage);
			break;
		case MembershipCreateStatus.InvalidAnswer:
			ShowErrorMessage(InvalidAnswerErrorMessage);
			break;
		case MembershipCreateStatus.InvalidUserName:
		case MembershipCreateStatus.UserRejected:
		case MembershipCreateStatus.InvalidProviderUserKey:
		case MembershipCreateStatus.ProviderError:
			ShowErrorMessage(UnknownErrorMessage);
			break;
		}
		OnCreateUserError(new CreateUserErrorEventArgs(status));
		return false;
	}

	private void SendPasswordByMail(MembershipUser user, string password)
	{
		if (user == null || _mailDefinition == null)
		{
			return;
		}
		string body = "A new account has been created for you. Please go to the site and log in using the following information.\nUser Name: <%USERNAME%>\nPassword: <%PASSWORD%>";
		ListDictionary listDictionary = new ListDictionary();
		listDictionary.Add("<%USERNAME%>", user.UserName);
		listDictionary.Add("<%PASSWORD%>", password);
		MailMessage mailMessage = null;
		mailMessage = ((MailDefinition.BodyFileName.Length != 0) ? MailDefinition.CreateMailMessage(user.Email, listDictionary, this) : MailDefinition.CreateMailMessage(user.Email, listDictionary, body, this));
		if (string.IsNullOrEmpty(mailMessage.Subject))
		{
			mailMessage.Subject = "Account information";
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

	private void Login()
	{
		if (MembershipProviderInternal.ValidateUser(UserName, Password))
		{
			FormsAuthentication.SetAuthCookie(UserName, createPersistentCookie: false);
		}
	}

	private void ShowErrorMessage(string errorMessage)
	{
		if (_errorMessageLabel != null)
		{
			_errorMessageLabel.Text = errorMessage;
		}
	}

	private string GeneratePassword()
	{
		return Membership.GeneratePassword(8, 3);
	}

	static CreateUserWizard()
	{
		CreatedUser = new object();
		CreateUserError = new object();
		CreatingUser = new object();
		ContinueButtonClick = new object();
		SendingMail = new object();
		SendMailError = new object();
	}
}
