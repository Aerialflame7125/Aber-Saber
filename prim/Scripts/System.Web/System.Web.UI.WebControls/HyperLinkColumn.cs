using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>A column type for the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that contains a hyperlink for each item in the column.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HyperLinkColumn : DataGridColumn
{
	/// <summary>Gets or sets the field from a data source to bind to the URL of the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" />.</summary>
	/// <returns>The field from a data source to bind to the URL of the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" />.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public virtual string DataNavigateUrlField
	{
		get
		{
			return base.ViewState.GetString("DataNavigateUrlField", string.Empty);
		}
		set
		{
			base.ViewState["DataNavigateUrlField"] = value;
		}
	}

	/// <summary>Gets or sets the display format for the URL of the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" /> when the URL is data-bound to a field in a data source.</summary>
	/// <returns>The string that specifies the display format for the URL of the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" /> when the URL is data-bound to a field in a data source. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[Description("The formatting applied to the value bound to the NavigateUrl property.")]
	[WebCategory("Misc")]
	public virtual string DataNavigateUrlFormatString
	{
		get
		{
			return base.ViewState.GetString("DataNavigateUrlFormatString", string.Empty);
		}
		set
		{
			base.ViewState["DataNavigateUrlFormatString"] = value;
		}
	}

	/// <summary>Gets or sets the field from a data source to bind to the text caption of the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" />.</summary>
	/// <returns>The field name from a data source to bind to the text caption of the hyperlinks in <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" />.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
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

	/// <summary>Gets or sets the display format for the text caption of the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" /> column.</summary>
	/// <returns>The string that specifies the display format for the text caption of the hyperlinks in the column. The default value is <see cref="F:System.String.Empty" />.</returns>
	[Description("The formatting applied to the value bound to the Text property.")]
	[DefaultValue("")]
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
		}
	}

	/// <summary>Gets or sets the URL to link to when a hyperlink in the column is clicked.</summary>
	/// <returns>The URL to link to when a hyperlink in the column is clicked.</returns>
	[UrlProperty]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public virtual string NavigateUrl
	{
		get
		{
			return base.ViewState.GetString("NavigateUrl", string.Empty);
		}
		set
		{
			base.ViewState["NavigateUrl"] = value;
		}
	}

	/// <summary>Gets or sets the target window or frame to display the Web page content that is linked to when the hyperlink in the column is clicked.</summary>
	/// <returns>The target window or frame to load the Web page linked to when a hyperlink in the column is clicked. The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	[TypeConverter("System.Web.UI.WebControls.TargetConverter")]
	public virtual string Target
	{
		get
		{
			return base.ViewState.GetString("Target", string.Empty);
		}
		set
		{
			base.ViewState["Target"] = value;
		}
	}

	/// <summary>Gets or sets the text caption to display for the hyperlinks in the column.</summary>
	/// <returns>The text caption for the hyperlinks in the column. The default value is <see cref="F:System.String.Empty" />.</returns>
	[Localizable(true)]
	[DefaultValue("")]
	[WebSysDescription("")]
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.HyperLinkColumn" /> class.</summary>
	public HyperLinkColumn()
	{
	}

	/// <summary>Formats a data-bound URL using the format specified by the <see cref="P:System.Web.UI.WebControls.HyperLinkColumn.DataNavigateUrlFormatString" /> property.</summary>
	/// <param name="dataUrlValue">The data-bound URL to format. </param>
	/// <returns>The data-bound URL in the format specified by the <see cref="P:System.Web.UI.WebControls.HyperLinkColumn.DataNavigateUrlFormatString" /> property.</returns>
	protected virtual string FormatDataNavigateUrlValue(object dataUrlValue)
	{
		string text = DataNavigateUrlFormatString;
		if (text == "")
		{
			text = null;
		}
		return DataBinder.FormatResult(dataUrlValue, text);
	}

	/// <summary>Formats a data-bound text caption using the format specified by the <see cref="P:System.Web.UI.WebControls.HyperLinkColumn.DataTextFormatString" /> property.</summary>
	/// <param name="dataTextValue">The data-bound URL to format. </param>
	/// <returns>The data-bound text caption in the format specified by the <see cref="P:System.Web.UI.WebControls.HyperLinkColumn.DataTextFormatString" /> property.</returns>
	protected virtual string FormatDataTextValue(object dataTextValue)
	{
		string text = DataTextFormatString;
		if (text == "")
		{
			text = null;
		}
		return DataBinder.FormatResult(dataTextValue, text);
	}

	/// <summary>Provides the base implementation to reset a column derived from the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> class to its initial state.</summary>
	public override void Initialize()
	{
		base.Initialize();
	}

	private void ItemDataBinding(object sender, EventArgs args)
	{
		TableCell obj = (TableCell)sender;
		HyperLink hyperLink = (HyperLink)obj.Controls[0];
		DataGridItem dataGridItem = (DataGridItem)obj.NamingContainer;
		if (DataNavigateUrlField != "")
		{
			hyperLink.NavigateUrl = FormatDataNavigateUrlValue(DataBinder.Eval(dataGridItem.DataItem, DataNavigateUrlField));
		}
		else
		{
			hyperLink.NavigateUrl = NavigateUrl;
		}
		if (DataTextField != "")
		{
			hyperLink.Text = FormatDataTextValue(DataBinder.Eval(dataGridItem.DataItem, DataTextField));
		}
		else
		{
			hyperLink.Text = Text;
		}
		hyperLink.Target = Target;
	}

	/// <summary>Initializes the cell representing this column with the contained hyperlink.</summary>
	/// <param name="cell">The cell to be initialized. </param>
	/// <param name="columnIndex">The index of the column that contains the cell. </param>
	/// <param name="itemType">The type of item that the cell is part of. </param>
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
		case ListItemType.EditItem:
			cell.DataBinding += ItemDataBinding;
			cell.Controls.Add(new HyperLink());
			break;
		case ListItemType.SelectedItem:
			break;
		}
	}
}
