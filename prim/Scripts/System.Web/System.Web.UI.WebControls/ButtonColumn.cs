using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>A column type for the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that contains a user-defined button.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class ButtonColumn : DataGridColumn
{
	private string text_field;

	private string format;

	/// <summary>Gets or sets the type of button to display in the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonColumnType" /> values. The default is <see langword="LinkButton" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified column type is not one of the <see cref="T:System.Web.UI.WebControls.ButtonColumnType" /> values. </exception>
	[DefaultValue(ButtonColumnType.LinkButton)]
	[WebSysDescription("The type of button contained within the column.")]
	[WebCategory("Misc")]
	public virtual ButtonColumnType ButtonType
	{
		get
		{
			return (ButtonColumnType)base.ViewState.GetInt("LinkButton", 0);
		}
		set
		{
			base.ViewState["LinkButton"] = value;
		}
	}

	/// <summary>Gets or sets a string that represents the command to perform when a button in the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object is clicked.</summary>
	/// <returns>A string that represents the command to perform when a button in the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> is clicked. The default is an empty string ("").</returns>
	[DefaultValue("")]
	[WebSysDescription("The command associated with the button.")]
	[WebCategory("Misc")]
	public virtual string CommandName
	{
		get
		{
			return base.ViewState.GetString("CommandName", string.Empty);
		}
		set
		{
			base.ViewState["CommandName"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether validation is performed when a button in the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object is clicked.</summary>
	/// <returns>
	///     <see langword="true" /> if validation is performed when a button in the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> is clicked; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool CausesValidation
	{
		get
		{
			return base.ViewState.GetBool("CausesValidation", def: false);
		}
		set
		{
			base.ViewState["CausesValidation"] = value;
		}
	}

	/// <summary>Gets or sets the field name from a data source to bind to the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object.</summary>
	/// <returns>The field name to bind to the <see cref="T:System.Web.UI.WebControls.ButtonColumn" />. The default is an empty string ("").</returns>
	[DefaultValue("")]
	[WebSysDescription("The field bound to the text property of the button.")]
	[WebCategory("Misc")]
	public virtual string DataTextField
	{
		get
		{
			return base.ViewState.GetString("DataTextField", string.Empty);
		}
		set
		{
			base.ViewState["DataTextField"] = value;
		}
	}

	/// <summary>Gets or sets the string that specifies the display format for the caption in each button.</summary>
	/// <returns>The string that specifies the display format for the caption in each button. The default is an empty string ("").</returns>
	[DefaultValue("")]
	[WebSysDescription("The formatting applied to the value bound to the Text property.")]
	[WebCategory("Misc")]
	public virtual string DataTextFormatString
	{
		get
		{
			return base.ViewState.GetString("DataTextFormatString", string.Empty);
		}
		set
		{
			base.ViewState["DataTextFormatString"] = value;
			format = null;
		}
	}

	/// <summary>Gets or sets the caption that is displayed in the buttons of the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object.</summary>
	/// <returns>The caption displayed in the buttons of the <see cref="T:System.Web.UI.WebControls.ButtonColumn" />. The default is an empty string ("").</returns>
	[DefaultValue("")]
	[Localizable(true)]
	[WebSysDescription("The text used for the button.")]
	[WebCategory("Misc")]
	public virtual string Text
	{
		get
		{
			return base.ViewState.GetString("Text", string.Empty);
		}
		set
		{
			base.ViewState["Text"] = value;
		}
	}

	/// <summary>Gets or sets the group of validation controls for which the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object causes validation when it posts back to the server.</summary>
	/// <returns>The group of validation controls for which the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object causes validation when it posts back to the server. The default is an empty string ("").</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
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

	/// <summary>Resets the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object to its initial state.</summary>
	public override void Initialize()
	{
		base.Initialize();
	}

	/// <summary>Resets a cell in the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> object to its initial state.</summary>
	/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> that represents the cell to reset. </param>
	/// <param name="columnIndex">The column number where the cell is located. </param>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
	public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
	{
		base.InitializeCell(cell, columnIndex, itemType);
		if (itemType == ListItemType.Header || itemType == ListItemType.Footer)
		{
			return;
		}
		switch (ButtonType)
		{
		case ButtonColumnType.LinkButton:
		{
			LinkButton linkButton = new ForeColorLinkButton();
			linkButton.Text = Text;
			linkButton.CommandName = CommandName;
			if (!string.IsNullOrEmpty(DataTextField))
			{
				linkButton.DataBinding += DoDataBind;
			}
			cell.Controls.Add(linkButton);
			break;
		}
		case ButtonColumnType.PushButton:
		{
			Button button = new Button();
			button.Text = Text;
			button.CommandName = CommandName;
			if (!string.IsNullOrEmpty(DataTextField))
			{
				button.DataBinding += DoDataBind;
			}
			cell.Controls.Add(button);
			break;
		}
		}
	}

	private string GetValueFromItem(DataGridItem item)
	{
		object dataTextValue = null;
		if (text_field == null)
		{
			text_field = DataTextField;
		}
		if (!string.IsNullOrEmpty(text_field))
		{
			dataTextValue = DataBinder.Eval(item.DataItem, text_field);
		}
		return FormatDataTextValue(dataTextValue);
	}

	private void DoDataBind(object sender, EventArgs e)
	{
		Control control = (Control)sender;
		string valueFromItem = GetValueFromItem((DataGridItem)control.NamingContainer);
		if (!(sender is LinkButton linkButton))
		{
			((Button)sender).Text = valueFromItem;
		}
		else
		{
			linkButton.Text = valueFromItem;
		}
	}

	/// <summary>Converts the specified value to the format that is indicated by the <see cref="P:System.Web.UI.WebControls.ButtonColumn.DataTextFormatString" /> property.</summary>
	/// <param name="dataTextValue">The value to format. </param>
	/// <returns>The <paramref name="dataTextValue" /> converted to the format indicated by the <see cref="P:System.Web.UI.WebControls.ButtonColumn.DataTextFormatString" />.</returns>
	protected virtual string FormatDataTextValue(object dataTextValue)
	{
		if (dataTextValue == null)
		{
			return string.Empty;
		}
		if (format == null)
		{
			format = DataTextFormatString;
		}
		if (string.IsNullOrEmpty(format))
		{
			return dataTextValue.ToString();
		}
		return string.Format(format, dataTextValue);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ButtonColumn" /> class.</summary>
	public ButtonColumn()
	{
	}
}
