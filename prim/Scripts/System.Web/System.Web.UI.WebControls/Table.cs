using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Displays a table on a Web page.</summary>
[DefaultProperty("Rows")]
[Designer("System.Web.UI.Design.WebControls.TableDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ParseChildren(true, "Rows")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class Table : WebControl, IPostBackEventHandler
{
	/// <summary>Represents the collection of <see cref="T:System.Web.UI.WebControls.TableRow" /> objects in a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
	protected class RowControlCollection : ControlCollection
	{
		internal RowControlCollection(Table owner)
			: base(owner)
		{
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the <see cref="T:System.Web.UI.WebControls.Table.RowControlCollection" /> collection.</summary>
		/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the <see cref="T:System.Web.UI.WebControls.Table.RowControlCollection" />. </param>
		/// <exception cref="T:System.ArgumentException">The object specified by <paramref name="child" /> is not a <see cref="T:System.Web.UI.WebControls.TableRow" />. </exception>
		public override void Add(Control child)
		{
			if (child == null)
			{
				throw new NullReferenceException("null");
			}
			if (!(child is TableRow))
			{
				throw new ArgumentException("child", Locale.GetText("Must be an TableRow instance."));
			}
			base.Add(child);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the <see cref="T:System.Web.UI.WebControls.Table.RowControlCollection" /> collection. The new control is added to the array at the specified index location.</summary>
		/// <param name="index">The location in the array at which to add the child control. </param>
		/// <param name="child">The <see langword="Control" /> object to add to the <see cref="T:System.Web.UI.WebControls.Table.RowControlCollection" />. </param>
		/// <exception cref="T:System.Web.HttpException">The control does not allow child controls. </exception>
		/// <exception cref="T:System.ArgumentException">The child value is <see langword="null" />. -or- The object is not a <see cref="T:System.Web.UI.WebControls.TableRow" />.</exception>
		public override void AddAt(int index, Control child)
		{
			if (child == null)
			{
				throw new NullReferenceException("null");
			}
			if (!(child is TableRow))
			{
				throw new ArgumentException("child", Locale.GetText("Must be an TableRow instance."));
			}
			base.AddAt(index, child);
		}
	}

	private TableRowCollection rows;

	private bool generateTableSections;

	internal bool GenerateTableSections
	{
		get
		{
			return generateTableSections;
		}
		set
		{
			generateTableSections = value;
		}
	}

	/// <summary>Gets or sets the URL of the background image to display behind the <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
	/// <returns>The URL of the background image for the <see cref="T:System.Web.UI.WebControls.Table" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string BackImageUrl
	{
		get
		{
			if (!base.ControlStyleCreated)
			{
				return string.Empty;
			}
			return TableStyle.BackImageUrl;
		}
		set
		{
			TableStyle.BackImageUrl = value;
		}
	}

	/// <summary>Gets or sets the text to render in an HTML caption element in a <see cref="T:System.Web.UI.WebControls.Table" /> control. This property is provided to make the control more accessible to users of Assistive Technology devices.</summary>
	/// <returns>A string that represents the text to render in an HTML caption element in a <see cref="T:System.Web.UI.WebControls.Table" /> control. The default value is an empty string ("").</returns>
	[DefaultValue("")]
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Accessibility")]
	public virtual string Caption
	{
		get
		{
			object obj = ViewState["Caption"];
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
				ViewState.Remove("Caption");
			}
			else
			{
				ViewState["Caption"] = value;
			}
		}
	}

	/// <summary>Gets or sets the horizontal or vertical position of the HTML caption element in a <see cref="T:System.Web.UI.WebControls.Table" /> control. This property is provided to make the control more accessible to users of Assistive Technology devices.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> enumeration values. The default value is <see langword="NotSet" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> enumeration values. </exception>
	[DefaultValue(TableCaptionAlign.NotSet)]
	[WebCategory("Accessibility")]
	public virtual TableCaptionAlign CaptionAlign
	{
		get
		{
			object obj = ViewState["CaptionAlign"];
			if (obj != null)
			{
				return (TableCaptionAlign)obj;
			}
			return TableCaptionAlign.NotSet;
		}
		set
		{
			if (value < TableCaptionAlign.NotSet || value > TableCaptionAlign.Right)
			{
				throw new ArgumentOutOfRangeException(Locale.GetText("Invalid TableCaptionAlign value."));
			}
			ViewState["CaptionAlign"] = value;
		}
	}

	/// <summary>Gets or sets the amount of space between the contents of a cell and the cell's border. </summary>
	/// <returns>The amount of space, in pixels, between the contents of a cell and the cell's border. The default value is -1, which indicates that the property has not been set.</returns>
	[DefaultValue(-1)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual int CellPadding
	{
		get
		{
			if (!base.ControlStyleCreated)
			{
				return -1;
			}
			return TableStyle.CellPadding;
		}
		set
		{
			TableStyle.CellPadding = value;
		}
	}

	/// <summary>Gets or sets the amount of space between cells. </summary>
	/// <returns>The amount of space, in pixels, between cells. The default value is -1, which indicates that the property has not been set.</returns>
	[DefaultValue(-1)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual int CellSpacing
	{
		get
		{
			if (!base.ControlStyleCreated)
			{
				return -1;
			}
			return TableStyle.CellSpacing;
		}
		set
		{
			TableStyle.CellSpacing = value;
		}
	}

	/// <summary>Gets or sets the grid line style to display in the <see cref="T:System.Web.UI.WebControls.Table" /> control. </summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.GridLines" /> enumeration values. The default value is <see langword="None" />.</returns>
	[DefaultValue(GridLines.None)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual GridLines GridLines
	{
		get
		{
			if (!base.ControlStyleCreated)
			{
				return GridLines.None;
			}
			return TableStyle.GridLines;
		}
		set
		{
			TableStyle.GridLines = value;
		}
	}

	/// <summary>Gets or sets the horizontal alignment of the <see cref="T:System.Web.UI.WebControls.Table" /> control on the page.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> enumeration values. The default value is <see langword="NotSet" />.</returns>
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
			return TableStyle.HorizontalAlign;
		}
		set
		{
			TableStyle.HorizontalAlign = value;
		}
	}

	/// <summary>Gets the collection of rows in the <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> that contains the <see cref="T:System.Web.UI.WebControls.TableRow" /> objects in the <see cref="T:System.Web.UI.WebControls.Table" /> control.</returns>
	[MergableProperty(false)]
	[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
	[WebSysDescription("")]
	public virtual TableRowCollection Rows
	{
		get
		{
			if (rows == null)
			{
				rows = new TableRowCollection(this);
			}
			return rows;
		}
	}

	private TableStyle TableStyle => base.ControlStyle as TableStyle;

	/// <summary>Gets a value that indicates whether the control should set the <see langword="disabled" /> attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is <see langword="false" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, <see langword="false" />.</returns>
	public override bool SupportsDisabledAttribute => base.RenderingCompatibilityLessThan40;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Table" /> class.</summary>
	public Table()
		: base(HtmlTextWriterTag.Table)
	{
	}

	/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" />.</summary>
	/// <param name="writer">The output stream that renders HTML content to the client. </param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		base.AddAttributesToRender(writer);
	}

	/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object to hold the <see cref="T:System.Web.UI.WebControls.TableRow" /> controls of the current <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> object to contain the <see cref="T:System.Web.UI.WebControls.TableRow" /> controls of the current <see cref="T:System.Web.UI.WebControls.Table" /> control.</returns>
	protected override ControlCollection CreateControlCollection()
	{
		return new RowControlCollection(this);
	}

	/// <summary>Gets a reference to a collection of properties that define the appearance of a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> object that contains the properties that define the appearance of the <see cref="T:System.Web.UI.WebControls.Table" /> control.</returns>
	protected override Style CreateControlStyle()
	{
		return new TableStyle(ViewState);
	}

	/// <summary>Renders the rows in the table control to the specified writer.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
	/// <exception cref="T:System.Web.HttpException">The table sections are not in order.</exception>
	protected internal override void RenderContents(HtmlTextWriter writer)
	{
		TableRowSection tableRowSection = TableRowSection.TableHeader;
		bool flag = false;
		if (Rows.Count <= 0)
		{
			return;
		}
		foreach (TableRow row in Rows)
		{
			if (generateTableSections)
			{
				TableRowSection tableSection = row.TableSection;
				if (tableSection < tableRowSection)
				{
					throw new HttpException("The table " + ID + " must contain row sections in order of header, body, then footer.");
				}
				if (tableRowSection != tableSection)
				{
					if (flag)
					{
						writer.RenderEndTag();
						flag = false;
					}
					tableRowSection = tableSection;
				}
				if (!flag)
				{
					switch (tableSection)
					{
					case TableRowSection.TableHeader:
						writer.RenderBeginTag(HtmlTextWriterTag.Thead);
						break;
					case TableRowSection.TableBody:
						writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
						break;
					case TableRowSection.TableFooter:
						writer.RenderBeginTag(HtmlTextWriterTag.Tfoot);
						break;
					}
					flag = true;
				}
			}
			row?.RenderControl(writer);
		}
		if (flag)
		{
			writer.RenderEndTag();
		}
	}

	/// <summary>Renders the HTML opening tag of the <see cref="T:System.Web.UI.WebControls.Table" /> control to the specified writer. </summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	public override void RenderBeginTag(HtmlTextWriter writer)
	{
		base.RenderBeginTag(writer);
		string caption = Caption;
		if (caption.Length > 0)
		{
			TableCaptionAlign captionAlign = CaptionAlign;
			if (captionAlign != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Align, captionAlign.ToString());
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Caption);
			writer.Write(caption);
			writer.RenderEndTag();
		}
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" />.</summary>
	/// <param name="eventArgument">The argument for the event.</param>
	void IPostBackEventHandler.RaisePostBackEvent(string argument)
	{
		RaisePostBackEvent(argument);
	}

	/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.Table" /> control when a form is posted back to the server.</summary>
	/// <param name="argument">A <see cref="T:System.String" /> that represents the argument for the event. </param>
	protected virtual void RaisePostBackEvent(string argument)
	{
		ValidateEvent(UniqueID, argument);
	}
}
