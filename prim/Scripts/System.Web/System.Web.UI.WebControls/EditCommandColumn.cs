using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>A special column type for the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that contains the <see langword="Edit" /> buttons for editing data items in each row.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class EditCommandColumn : DataGridColumn
{
	/// <summary>Gets or sets the button type for the column.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonColumnType" /> values. The default value is <see langword="LinkButton" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified button type is not one of the <see cref="T:System.Web.UI.WebControls.ButtonColumnType" /> values. </exception>
	[DefaultValue(ButtonColumnType.LinkButton)]
	public virtual ButtonColumnType ButtonType
	{
		get
		{
			object obj = base.ViewState["ButtonType"];
			if (obj != null)
			{
				return (ButtonColumnType)obj;
			}
			return ButtonColumnType.LinkButton;
		}
		set
		{
			base.ViewState["ButtonType"] = value;
		}
	}

	/// <summary>Gets or sets the text to display for the <see langword="Cancel" /> command button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" />.</summary>
	/// <returns>The caption to display for the <see langword="Cancel" /> command button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" />.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public virtual string CancelText
	{
		get
		{
			return base.ViewState.GetString("CancelText", string.Empty);
		}
		set
		{
			base.ViewState["CancelText"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether validation is performed when an <see langword="Update" /> button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" /> object is clicked.</summary>
	/// <returns>
	///     <see langword="true" /> if validation is performed when an <see langword="Update" /> button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" /> is clicked; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	public virtual bool CausesValidation
	{
		get
		{
			return base.ViewState.GetBool("CausesValidation", def: true);
		}
		set
		{
			base.ViewState["CausesValidation"] = value;
		}
	}

	/// <summary>Gets or sets the group of validation controls for which the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" /> object causes validation when it posts back to the server.</summary>
	/// <returns>The group of validation controls for which the Update button in an <see cref="T:System.Web.UI.WebControls.EditCommandColumn" /> causes validation when it posts back to the server. The default is an empty string ("").</returns>
	[DefaultValue("")]
	public virtual string ValidationGroup
	{
		get
		{
			return base.ViewState.GetString("ValidationGroup", string.Empty);
		}
		set
		{
			base.ViewState["ValidationGroup"] = value;
		}
	}

	/// <summary>Gets or sets the text to display for the <see langword="Edit" /> button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" />.</summary>
	/// <returns>The caption to display for the <see langword="Edit" /> button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" />.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public virtual string EditText
	{
		get
		{
			return base.ViewState.GetString("EditText", string.Empty);
		}
		set
		{
			base.ViewState["EditText"] = value;
		}
	}

	/// <summary>Gets or sets the text to display for the <see langword="Update" /> command button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" />.</summary>
	/// <returns>The caption to display for the <see langword="Update" /> command button in the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" />.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public virtual string UpdateText
	{
		get
		{
			return base.ViewState.GetString("UpdateText", string.Empty);
		}
		set
		{
			base.ViewState["UpdateText"] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.EditCommandColumn" /> class.</summary>
	public EditCommandColumn()
	{
	}

	/// <summary>Initializes a cell within the column.</summary>
	/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> that contains information about the cell to initialize. </param>
	/// <param name="columnIndex">The column number where the cell is located. </param>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
	public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
	{
		base.InitializeCell(cell, columnIndex, itemType);
		switch (itemType)
		{
		case ListItemType.Header:
		case ListItemType.Footer:
		case ListItemType.Separator:
		case ListItemType.Pager:
			break;
		case ListItemType.Item:
		case ListItemType.AlternatingItem:
		case ListItemType.SelectedItem:
			cell.Controls.Add(CreateButton(ButtonType, EditText, "Edit", valid: false));
			break;
		case ListItemType.EditItem:
			cell.Controls.Add(CreateButton(ButtonType, UpdateText, "Update", CausesValidation));
			cell.Controls.Add(new LiteralControl("&nbsp;"));
			cell.Controls.Add(CreateButton(ButtonType, CancelText, "Cancel", valid: false));
			break;
		}
	}

	private Control CreateButton(ButtonColumnType type, string text, string command, bool valid)
	{
		if (type == ButtonColumnType.LinkButton)
		{
			LinkButton linkButton = new ForeColorLinkButton();
			linkButton.Text = text;
			linkButton.CommandName = command;
			linkButton.CausesValidation = valid;
			if (valid)
			{
				linkButton.ValidationGroup = ValidationGroup;
			}
			return linkButton;
		}
		Button button = new Button();
		button.Text = text;
		button.CommandName = command;
		button.CausesValidation = valid;
		if (valid)
		{
			button.ValidationGroup = ValidationGroup;
		}
		return button;
	}
}
