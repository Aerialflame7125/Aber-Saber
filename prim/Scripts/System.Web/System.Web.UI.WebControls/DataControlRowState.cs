namespace System.Web.UI.WebControls;

/// <summary>Specifies the state of a row in a data control, such as <see cref="T:System.Web.UI.WebControls.DetailsView" /> or <see cref="T:System.Web.UI.WebControls.GridView" />.</summary>
[Flags]
public enum DataControlRowState
{
	/// <summary>Indicates that the data control row is in a normal state. The <see cref="F:System.Web.UI.WebControls.DataControlRowState.Normal" /> state is mutually exclusive with other states except the <see cref="F:System.Web.UI.WebControls.DataControlRowState.Alternate" /> state.</summary>
	Normal = 0,
	/// <summary>Indicates that the data control row is an alternate row. </summary>
	Alternate = 1,
	/// <summary>Indicates that the row has been selected by the user.</summary>
	Selected = 2,
	/// <summary>Indicates that the row is in an edit state, often the result of clicking an edit button for the row. Typically, the <see cref="F:System.Web.UI.WebControls.DataControlRowState.Edit" /> and <see cref="F:System.Web.UI.WebControls.DataControlRowState.Insert" /> states are mutually exclusive.</summary>
	Edit = 4,
	/// <summary>Indicates that the row is a new row, often the result of clicking an insert button to add a new row. Typically, the <see cref="F:System.Web.UI.WebControls.DataControlRowState.Insert" /> and <see cref="F:System.Web.UI.WebControls.DataControlRowState.Edit" /> states are mutually exclusive.</summary>
	Insert = 8
}
