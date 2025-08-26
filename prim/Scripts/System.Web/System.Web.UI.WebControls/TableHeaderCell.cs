using System.ComponentModel;
using System.Security.Permissions;
using System.Text;

namespace System.Web.UI.WebControls;

/// <summary>Represents a heading cell within a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class TableHeaderCell : TableCell
{
	/// <summary>Gets or sets the <see langword="abbr" /> attribute of the HTML <see langword="th" /> element for the <see cref="T:System.Web.UI.WebControls.TableHeaderCell" /> control.</summary>
	/// <returns>A string representing the abbreviated text. The default is an empty string ("").</returns>
	[DefaultValue("")]
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
			if (value == null)
			{
				ViewState.Remove("AbbreviatedText");
			}
			else
			{
				ViewState["AbbreviatedText"] = value;
			}
		}
	}

	/// <summary>Gets or sets the <see langword="axis" /> attribute of the HTML <see langword="th" /> element for the <see cref="T:System.Web.UI.WebControls.TableHeaderCell" /> control.</summary>
	/// <returns>An array of string values representing the <see cref="T:System.Web.UI.WebControls.TableHeaderCell" /> categories. </returns>
	[DefaultValue(null)]
	[TypeConverter(typeof(StringArrayConverter))]
	public virtual string[] CategoryText
	{
		get
		{
			object obj = ViewState["CategoryText"];
			if (obj != null)
			{
				return (string[])obj;
			}
			return new string[0];
		}
		set
		{
			ViewState["CategoryText"] = value;
		}
	}

	/// <summary>Gets or sets the scope of the <see cref="T:System.Web.UI.WebControls.TableHeaderCell" /> control when it is rendered.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableHeaderScope" /> value. The default is <see cref="F:System.Web.UI.WebControls.TableHeaderScope.NotSet" />. </returns>
	[DefaultValue(TableHeaderScope.NotSet)]
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
			ViewState["Scope"] = (int)value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TableHeaderCell" /> class.</summary>
	public TableHeaderCell()
		: base(HtmlTextWriterTag.Th)
	{
	}

	/// <summary>Applies attributes to render to the <see cref="T:System.Web.UI.WebControls.TableHeaderCell" /> control.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		base.AddAttributesToRender(writer);
		if (writer == null)
		{
			return;
		}
		object obj = ViewState["AbbreviatedText"];
		if (obj != null)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Abbr, (string)obj);
		}
		switch (Scope)
		{
		case TableHeaderScope.Column:
			writer.AddAttribute(HtmlTextWriterAttribute.Scope, "column", fEncode: false);
			break;
		case TableHeaderScope.Row:
			writer.AddAttribute(HtmlTextWriterAttribute.Scope, "row", fEncode: false);
			break;
		}
		string[] categoryText = CategoryText;
		if (categoryText.Length == 1)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Axis, categoryText[0]);
		}
		else if (categoryText.Length > 1)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < categoryText.Length - 1; i++)
			{
				stringBuilder.Append(categoryText[i]);
				stringBuilder.Append(",");
			}
			stringBuilder.Append(categoryText[categoryText.Length - 1]);
			writer.AddAttribute(HtmlTextWriterAttribute.Axis, stringBuilder.ToString());
		}
	}
}
