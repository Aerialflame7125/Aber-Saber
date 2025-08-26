namespace System.Web.UI.WebControls;

/// <summary>Represents a cell in the rendered table of a tabular ASP.NET data-bound control, such as <see cref="T:System.Web.UI.WebControls.DetailsView" /> or <see cref="T:System.Web.UI.WebControls.GridView" />.</summary>
public class DataControlFieldCell : TableCell
{
	private DataControlField containerField;

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object that contains the current cell.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.DataControlField" /> that contains the current cell, or <see langword="null" />, if no <see cref="T:System.Web.UI.WebControls.DataControlField" /> is passed to the class constructor.</returns>
	public DataControlField ContainingField => containerField;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> class, setting the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object as the cell's container.</summary>
	/// <param name="containingField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> that contains the current cell.</param>
	public DataControlFieldCell(DataControlField containingField)
		: this(HtmlTextWriterTag.Td, containingField)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> class, setting the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object as the cell's container.</summary>
	/// <param name="tagKey">An <see cref="T:System.Web.UI.HtmlTextWriterTag" /> that specifies the HTML tag to render for the cell. The default tag used by the <see cref="T:System.Web.UI.WebControls.TableCell" /> class is <see cref="F:System.Web.UI.HtmlTextWriterTag.Td" />.</param>
	/// <param name="containingField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> that contains the current cell.</param>
	protected DataControlFieldCell(HtmlTextWriterTag tagKey, DataControlField containingField)
		: base(tagKey)
	{
		containerField = containingField;
	}
}
