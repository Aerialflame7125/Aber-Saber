namespace System.Web.UI.WebControls;

/// <summary>Specifies the function of a row in a data control, such as a <see cref="T:System.Web.UI.WebControls.DetailsView" /> or <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
public enum DataControlRowType
{
	/// <summary>A header row of a data control. Header rows cannot be data-bound.</summary>
	Header,
	/// <summary>A footer row of a data control. Footer rows cannot be data-bound.</summary>
	Footer,
	/// <summary>A data row of a data control. Only <see cref="F:System.Web.UI.WebControls.DataControlRowType.DataRow" /> rows can be data-bound.</summary>
	DataRow,
	/// <summary>A row separator. Row separators cannot be data-bound.</summary>
	Separator,
	/// <summary>A row that displays pager buttons or a pager control. Pager rows cannot be data-bound.</summary>
	Pager,
	/// <summary>The empty row of a data-bound control. The empty row is displayed when the data-bound control has no records to display and the <see langword="EmptyDataTemplate" /> template is not <see langword="null" />.</summary>
	EmptyDataRow
}
