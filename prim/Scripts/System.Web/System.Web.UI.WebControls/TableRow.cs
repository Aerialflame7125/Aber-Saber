using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a row in a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
[DefaultProperty("Cells")]
[ParseChildren(true, "Cells")]
[ToolboxItem("")]
[Bindable(false)]
[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class TableRow : WebControl
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.TableCell" /> objects that are the cells of a <see cref="T:System.Web.UI.WebControls.TableRow" /> control. </summary>
	protected class CellControlCollection : ControlCollection
	{
		internal CellControlCollection(TableRow owner)
			: base(owner)
		{
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the <see cref="T:System.Web.UI.WebControls.TableRow.CellControlCollection" /> collection.</summary>
		/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the <see cref="T:System.Web.UI.WebControls.TableRow.CellControlCollection" />. </param>
		/// <exception cref="T:System.ArgumentException">The added <see cref="T:System.Web.UI.Control" /> must be of the type <see cref="T:System.Web.UI.WebControls.TableCell" />. </exception>
		public override void Add(Control child)
		{
			if (child == null)
			{
				throw new NullReferenceException("null");
			}
			if (!(child is TableCell))
			{
				throw new ArgumentException("child", Locale.GetText("Must be an TableCell instance."));
			}
			base.Add(child);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the <see cref="T:System.Web.UI.WebControls.TableRow.CellControlCollection" /> collection. The new control is added to the array at the specified index location.</summary>
		/// <param name="index">The location in the array to add the child control. </param>
		/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the <see cref="T:System.Web.UI.WebControls.TableRow.CellControlCollection" />. </param>
		/// <exception cref="T:System.ArgumentException">The added <see cref="T:System.Web.UI.Control" /> must be of the type <see cref="T:System.Web.UI.WebControls.TableCell" />.</exception>
		public override void AddAt(int index, Control child)
		{
			if (child == null)
			{
				throw new NullReferenceException("null");
			}
			if (!(child is TableCell))
			{
				throw new ArgumentException("child", Locale.GetText("Must be an TableCell instance."));
			}
			base.AddAt(index, child);
		}
	}

	private TableCellCollection cells;

	private bool tableRowSectionSet;

	internal TableRowCollection Container { get; set; }

	internal bool TableRowSectionSet => tableRowSectionSet;

	/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.TableCell" /> objects that represent the cells of a row in a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> object that represents a collection of cells from a row of a <see cref="T:System.Web.UI.WebControls.Table" /> control.</returns>
	[MergableProperty(false)]
	[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual TableCellCollection Cells
	{
		get
		{
			if (cells == null)
			{
				cells = new TableCellCollection(this);
			}
			return cells;
		}
	}

	/// <summary>Gets or sets the horizontal alignment of the contents in the row.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. The default value is <see langword="NotSet" />.</returns>
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

	/// <summary>Gets or sets the vertical alignment of the contents in the row.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.VerticalAlign" /> values. The default value is <see langword="NotSet" />.</returns>
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

	private TableItemStyle TableItemStyle => base.ControlStyle as TableItemStyle;

	/// <summary>Gets a value that indicates whether the control should set the <see langword="disabled" /> attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is <see langword="false" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, <see langword="false" />.</returns>
	public override bool SupportsDisabledAttribute => base.RenderingCompatibilityLessThan40;

	/// <summary>Gets or sets the location for a <see cref="T:System.Web.UI.WebControls.TableRow" /> object in a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableRowSection" /> value. The default is <see cref="F:System.Web.UI.WebControls.TableRowSection.TableBody" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="T:System.Web.UI.WebControls.TableRowSection" /> is not valid.</exception>
	[DefaultValue(TableRowSection.TableBody)]
	public virtual TableRowSection TableSection
	{
		get
		{
			object obj = ViewState["TableSection"];
			if (obj != null)
			{
				return (TableRowSection)obj;
			}
			return TableRowSection.TableBody;
		}
		set
		{
			if (value < TableRowSection.TableHeader || value > TableRowSection.TableFooter)
			{
				throw new ArgumentOutOfRangeException("TableSection");
			}
			ViewState["TableSection"] = (int)value;
			tableRowSectionSet = true;
			Container?.RowTableSectionSet();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TableRow" /> class.</summary>
	public TableRow()
		: base(HtmlTextWriterTag.Tr)
	{
		base.AutoID = false;
	}

	/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object for the <see cref="T:System.Web.UI.WebControls.TableRow" /> control. </summary>
	/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> object that contains the <see cref="T:System.Web.UI.WebControls.TableRow" /> control's child server controls.</returns>
	protected override ControlCollection CreateControlCollection()
	{
		return new CellControlCollection(this);
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object for the <see cref="T:System.Web.UI.WebControls.TableRow" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object that specifies the style properties for the <see cref="T:System.Web.UI.WebControls.TableRow" /> control.The <see cref="M:System.Web.UI.WebControls.TableRow.CreateControlCollection" /> method is primarily of interest to control developers extending the functionality of the <see cref="T:System.Web.UI.WebControls.TableRow" /> control.</returns>
	protected override Style CreateControlStyle()
	{
		return new TableItemStyle(ViewState);
	}
}
