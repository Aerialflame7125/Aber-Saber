namespace System.Web.UI.WebControls;

/// <summary>In accessibility scenarios, represents a header cell in the rendered table of a tabular ASP.NET data-bound control, such as <see cref="T:System.Web.UI.WebControls.GridView" />.</summary>
public class DataControlFieldHeaderCell : DataControlFieldCell
{
	private TableHeaderScope scope;

	/// <summary>Gets or sets the header cell's scope within an HTML table.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TableHeaderScope" /> values. The default is <see cref="F:System.Web.UI.WebControls.TableHeaderScope.NotSet" />.</returns>
	public virtual TableHeaderScope Scope
	{
		get
		{
			object obj = ViewState["Scope"];
			if (obj != null)
			{
				return (TableHeaderScope)obj;
			}
			return TableHeaderScope.NotSet;
		}
		set
		{
			ViewState["Scope"] = value;
		}
	}

	/// <summary>Gets or sets abbreviated text, which is rendered in an HTML <see langword="abbr" /> attribute and is used by screen readers.</summary>
	/// <returns>A shortened version of the table header text, which is read by screen readers. The default value is <see cref="F:System.String.Empty" />.</returns>
	public virtual string AbbreviatedText
	{
		get
		{
			object obj = ViewState["AbbreviatedText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			ViewState["AbbreviatedText"] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataControlFieldHeaderCell" /> class, setting the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object as the cell's container.</summary>
	/// <param name="containingField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> that contains the current cell.</param>
	public DataControlFieldHeaderCell(DataControlField containingField)
		: base(HtmlTextWriterTag.Th, containingField)
	{
	}

	internal DataControlFieldHeaderCell(DataControlField containerField, TableHeaderScope scope)
		: this(containerField)
	{
		this.scope = scope;
	}

	/// <summary>Adds information about the table cell to the list of attributes to render.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream that renders HTML content to the client.</param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		base.AddAttributesToRender(writer);
		switch (scope)
		{
		case TableHeaderScope.Column:
			writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col", fEncode: false);
			break;
		case TableHeaderScope.Row:
			writer.AddAttribute(HtmlTextWriterAttribute.Scope, "row", fEncode: false);
			break;
		}
		if (AbbreviatedText.Length > 0)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Abbr, AbbreviatedText);
		}
	}
}
