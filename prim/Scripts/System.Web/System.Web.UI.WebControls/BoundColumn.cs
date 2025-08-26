using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>A column type for the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that is bound to a field in a data source. </summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class BoundColumn : DataGridColumn
{
	private string data_format_string;

	/// <summary>Represents the string "!". This field is read-only.</summary>
	public static readonly string thisExpr = "!";

	/// <summary>Gets or sets the field name from the data source to bind to the <see cref="T:System.Web.UI.WebControls.BoundColumn" />.</summary>
	/// <returns>The name of the field to bind to the <see cref="T:System.Web.UI.WebControls.BoundColumn" />. The default value is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public virtual string DataField
	{
		get
		{
			return base.ViewState.GetString("DataField", string.Empty);
		}
		set
		{
			base.ViewState["DataField"] = value;
		}
	}

	/// <summary>Gets or sets the string that specifies the display format for items in the column.</summary>
	/// <returns>A formatting string that specifies the display format of items in the column. The default value is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public virtual string DataFormatString
	{
		get
		{
			return base.ViewState.GetString("DataFormatString", string.Empty);
		}
		set
		{
			base.ViewState["DataFormatString"] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the items in the <see cref="T:System.Web.UI.WebControls.BoundColumn" /> can be edited.</summary>
	/// <returns>
	///     <see langword="true" /> if the items in the <see cref="T:System.Web.UI.WebControls.BoundColumn" /> cannot be edited; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public virtual bool ReadOnly
	{
		get
		{
			return base.ViewState.GetBool("ReadOnly", def: false);
		}
		set
		{
			base.ViewState["ReadOnly"] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.BoundColumn" /> class.</summary>
	public BoundColumn()
	{
	}

	/// <summary>Resets the <see cref="T:System.Web.UI.WebControls.BoundColumn" /> to its initial state.</summary>
	public override void Initialize()
	{
		data_format_string = DataFormatString;
	}

	/// <summary>Resets the specified cell in the <see cref="T:System.Web.UI.WebControls.BoundColumn" /> to its initial state.</summary>
	/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> object that represents the cell to reset. </param>
	/// <param name="columnIndex">The column number where the cell is located. </param>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
	public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
	{
		base.InitializeCell(cell, columnIndex, itemType);
		string dataField = DataField;
		switch (itemType)
		{
		case ListItemType.Item:
		case ListItemType.AlternatingItem:
		case ListItemType.SelectedItem:
			if (dataField != null && dataField.Length != 0)
			{
				cell.DataBinding += ItemDataBinding;
			}
			break;
		case ListItemType.EditItem:
		{
			if (ReadOnly && dataField != null && dataField.Length != 0)
			{
				cell.DataBinding += ItemDataBinding;
				break;
			}
			TextBox textBox = new TextBox();
			if (dataField != null && dataField.Length != 0)
			{
				textBox.DataBinding += ItemDataBinding;
			}
			cell.Controls.Add(textBox);
			break;
		}
		}
	}

	/// <summary>Converts the specified value to the format indicated by the <see cref="P:System.Web.UI.WebControls.BoundColumn.DataFormatString" /> property.</summary>
	/// <param name="dataValue">The value to format. </param>
	/// <returns>The specified value converted to the format indicated by the <see cref="P:System.Web.UI.WebControls.BoundColumn.DataFormatString" /> property.</returns>
	protected virtual string FormatDataValue(object dataValue)
	{
		if (dataValue == null)
		{
			return "";
		}
		if (data_format_string == string.Empty)
		{
			return dataValue.ToString();
		}
		return string.Format(data_format_string, dataValue);
	}

	private string GetValueFromItem(DataGridItem item)
	{
		object dataValue = ((!(DataField != thisExpr)) ? item.DataItem : DataBinder.Eval(item.DataItem, DataField));
		string text = FormatDataValue(dataValue);
		if (!(text != ""))
		{
			return "&nbsp;";
		}
		return text;
	}

	private void ItemDataBinding(object sender, EventArgs e)
	{
		Control control = (Control)sender;
		string valueFromItem = GetValueFromItem((DataGridItem)control.NamingContainer);
		if (!(sender is TableCell tableCell))
		{
			((TextBox)sender).Text = valueFromItem;
		}
		else
		{
			tableCell.Text = valueFromItem;
		}
	}
}
