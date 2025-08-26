using System.ComponentModel;
using System.Security.Permissions;
using System.Text;
using System.Web.Util;

namespace System.Web.UI.WebControls;

/// <summary>Represents a cell in a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
[ControlBuilder(typeof(TableCellControlBuilder))]
[DefaultProperty("Text")]
[ParseChildren(false)]
[ToolboxItem("")]
[Bindable(false)]
[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class TableCell : WebControl
{
	/// <summary>Gets or sets a space-separated list of table header cells associated with the <see cref="T:System.Web.UI.WebControls.TableCell" /> control.</summary>
	/// <returns>An array of strings containing the identifiers of the associated table header cells.</returns>
	[DefaultValue(null)]
	[TypeConverter(typeof(StringArrayConverter))]
	public virtual string[] AssociatedHeaderCellID
	{
		get
		{
			object obj = ViewState["AssociatedHeaderCellID"];
			if (obj != null)
			{
				return (string[])obj;
			}
			return new string[0];
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("AssociatedHeaderCellID");
			}
			else
			{
				ViewState["AssociatedHeaderCellID"] = value;
			}
		}
	}

	/// <summary>Gets or sets the number of columns in the <see cref="T:System.Web.UI.WebControls.Table" /> control that the cell spans.</summary>
	/// <returns>The number of columns in the rendered table that the cell spans. The default value is <see langword="0" />, which indicates that this property is not set.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than <see langword="0" />.</exception>
	[DefaultValue(0)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual int ColumnSpan
	{
		get
		{
			object obj = ViewState["ColumnSpan"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("< 0");
			}
			ViewState["ColumnSpan"] = value;
		}
	}

	/// <summary>Gets or sets the horizontal alignment of the contents in the cell.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> enumeration values. The default is <see langword="NotSet" />.</returns>
	[DefaultValue(HorizontalAlign.NotSet)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual HorizontalAlign HorizontalAlign
	{
		get
		{
			if (!base.ControlStyleCreated)
			{
				return HorizontalAlign.NotSet;
			}
			return TableItemStyle.HorizontalAlign;
		}
		set
		{
			TableItemStyle.HorizontalAlign = value;
		}
	}

	/// <summary>Gets or sets the number of rows in the <see cref="T:System.Web.UI.WebControls.Table" /> control that the cell spans.</summary>
	/// <returns>The number of rows in the rendered table that the cell spans. The default value is <see langword="0" />, which indicates that this property is not set.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than <see langword="0" />.</exception>
	[DefaultValue(0)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual int RowSpan
	{
		get
		{
			object obj = ViewState["RowSpan"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("< 0");
			}
			ViewState["RowSpan"] = value;
		}
	}

	/// <summary>Gets or sets the text contents of the cell.</summary>
	/// <returns>The text contents of the cell. The default value is <see cref="F:System.String.Empty" />.</returns>
	[Localizable(true)]
	[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string Text
	{
		get
		{
			object obj = ViewState["Text"];
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
				ViewState.Remove("Text");
				return;
			}
			ViewState["Text"] = value;
			if (HasControls())
			{
				Controls.Clear();
			}
		}
	}

	/// <summary>Gets or sets the vertical alignment of the contents in the cell.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.VerticalAlign" /> enumeration values. The default is <see langword="NotSet" />.</returns>
	[DefaultValue(VerticalAlign.NotSet)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual VerticalAlign VerticalAlign
	{
		get
		{
			if (!base.ControlStyleCreated)
			{
				return VerticalAlign.NotSet;
			}
			return TableItemStyle.VerticalAlign;
		}
		set
		{
			TableItemStyle.VerticalAlign = value;
		}
	}

	/// <summary>Gets or sets a value that indicating whether the contents of the cell wrap.</summary>
	/// <returns>
	///     <see langword="true" /> if the contents of the cell wrap in the cell; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual bool Wrap
	{
		get
		{
			if (!base.ControlStyleCreated)
			{
				return true;
			}
			return TableItemStyle.Wrap;
		}
		set
		{
			TableItemStyle.Wrap = value;
		}
	}

	/// <summary>Gets a value that indicates whether the control should set the <see langword="disabled" /> attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is <see langword="false" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, <see langword="false" />.</returns>
	public override bool SupportsDisabledAttribute => base.RenderingCompatibilityLessThan40;

	private TableItemStyle TableItemStyle => base.ControlStyle as TableItemStyle;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TableCell" /> class.</summary>
	public TableCell()
		: base(HtmlTextWriterTag.Td)
	{
		base.AutoID = false;
	}

	internal TableCell(HtmlTextWriterTag tag)
		: base(tag)
	{
		base.AutoID = false;
	}

	/// <summary>Adds properties specific to the <see cref="T:System.Web.UI.WebControls.TableCell" /> control to the list of attributes to render.</summary>
	/// <param name="writer">The output stream that renders HTML content to the client. </param>
	/// <exception cref="T:System.Web.HttpException">A cell listed as an associated header cell was not found.</exception>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		base.AddAttributesToRender(writer);
		if (writer == null)
		{
			return;
		}
		int columnSpan = ColumnSpan;
		if (columnSpan > 0)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Colspan, columnSpan.ToString(Helpers.InvariantCulture), fEncode: false);
		}
		columnSpan = RowSpan;
		if (columnSpan > 0)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Rowspan, columnSpan.ToString(Helpers.InvariantCulture), fEncode: false);
		}
		string[] associatedHeaderCellID = AssociatedHeaderCellID;
		if (associatedHeaderCellID.Length > 1)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (columnSpan = 0; columnSpan < associatedHeaderCellID.Length - 1; columnSpan++)
			{
				stringBuilder.Append(associatedHeaderCellID[columnSpan]);
				stringBuilder.Append(",");
			}
			stringBuilder.Append(associatedHeaderCellID.Length - 1);
			writer.AddAttribute(HtmlTextWriterAttribute.Headers, stringBuilder.ToString());
		}
		else if (associatedHeaderCellID.Length == 1)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Headers, associatedHeaderCellID[0]);
		}
	}

	/// <summary>Adds a parsed child control to the <see cref="T:System.Web.UI.WebControls.TableCell" /> control.</summary>
	/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element</param>
	protected override void AddParsedSubObject(object obj)
	{
		if (HasControls())
		{
			base.AddParsedSubObject(obj);
		}
		else if (!(obj is LiteralControl literalControl))
		{
			string text = Text;
			if (text.Length > 0)
			{
				Controls.Add(new LiteralControl(text));
				Text = null;
			}
			base.AddParsedSubObject(obj);
		}
		else
		{
			Text = literalControl.Text;
		}
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> for the <see cref="T:System.Web.UI.WebControls.TableCell" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> for the <see cref="T:System.Web.UI.WebControls.TableCell" /> control. </returns>
	protected override Style CreateControlStyle()
	{
		return new TableItemStyle(ViewState);
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.WebControls.TableCell" /> contents to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object. </summary>
	/// <param name="writer">The output stream that renders HTML content to the client. </param>
	protected internal override void RenderContents(HtmlTextWriter writer)
	{
		if (HasControls() || HasRenderMethodDelegate())
		{
			base.RenderContents(writer);
		}
		else
		{
			writer.Write(Text);
		}
	}
}
