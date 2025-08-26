using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>Represents the <see langword="&lt;tr&gt;" /> HTML element in an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
[ParseChildren(true, "Cells")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlTableRow : HtmlContainerControl
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> objects that are the cells of an <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> control. </summary>
	protected class HtmlTableCellControlCollection : ControlCollection
	{
		internal HtmlTableCellControlCollection(HtmlTableRow owner)
			: base(owner)
		{
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the collection.</summary>
		/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the collection.</param>
		/// <exception cref="T:System.ArgumentException">The added control must be of type <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />.</exception>
		public override void Add(Control child)
		{
			if (child == null)
			{
				throw new NullReferenceException("null");
			}
			if (!(child is HtmlTableCell))
			{
				throw new ArgumentException("child", Locale.GetText("Must be an HtmlTableCell instance."));
			}
			base.Add(child);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the collection at the specified index location.</summary>
		/// <param name="index">The location in the array at which to add the child control. </param>
		/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the collection. </param>
		/// <exception cref="T:System.ArgumentException">The added control must be of type <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />.</exception>
		public override void AddAt(int index, Control child)
		{
			if (child == null)
			{
				throw new NullReferenceException("null");
			}
			if (!(child is HtmlTableCell))
			{
				throw new ArgumentException("child", Locale.GetText("Must be an HtmlTableCell instance."));
			}
			base.AddAt(index, child);
		}
	}

	private HtmlTableCellCollection _cells;

	/// <summary>Gets or sets the horizontal alignment of the content in the cells of a row in an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
	/// <returns>The horizontal alignment of the content in the cells of a row in an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public string Align
	{
		get
		{
			string text = base.Attributes["align"];
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("align");
			}
			else
			{
				base.Attributes["align"] = value;
			}
		}
	}

	/// <summary>Gets or sets the background color of the row represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> class.</summary>
	/// <returns>The background color of the row represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" />.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public string BgColor
	{
		get
		{
			string text = base.Attributes["bgcolor"];
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("bgcolor");
			}
			else
			{
				base.Attributes["bgcolor"] = value;
			}
		}
	}

	/// <summary>Gets or sets the border color of the row represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> class.</summary>
	/// <returns>The border color of the row represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" />.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public string BorderColor
	{
		get
		{
			string text = base.Attributes["bordercolor"];
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("bordercolor");
			}
			else
			{
				base.Attributes["bordercolor"] = value;
			}
		}
	}

	/// <summary>Gets a collection of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> objects that represent the cells contained in a row of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
	/// <returns>An <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> that contains the cells of a row in an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual HtmlTableCellCollection Cells
	{
		get
		{
			if (_cells == null)
			{
				_cells = new HtmlTableCellCollection(this);
			}
			return _cells;
		}
	}

	/// <summary>Gets or sets the height (in pixels) of the row represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> class.</summary>
	/// <returns>The height (in pixels) of the row represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" />. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public string Height
	{
		get
		{
			string text = base.Attributes["height"];
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("height");
			}
			else
			{
				base.Attributes["height"] = value;
			}
		}
	}

	/// <summary>Gets or sets the content between the opening and closing tags of the control without automatically converting special characters to their equivalent HTML entities. This property is not supported for this control.</summary>
	/// <returns>The content between the opening and closing tags of the control.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt was made to read from or assign a value to this property. </exception>
	public override string InnerHtml
	{
		get
		{
			throw new NotSupportedException();
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Gets or sets the content between the opening and closing tags of the control with automatic conversion of special characters to their equivalent HTML entities. This property is not supported for this control.</summary>
	/// <returns>The content between the opening and closing tags of the control.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt was made to read from or assign a value to this property. </exception>
	public override string InnerText
	{
		get
		{
			throw new NotSupportedException();
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Gets or sets the vertical alignment of the content in the cells of a row in an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
	/// <returns>The vertical alignment of the content in the cells of a row in an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public string VAlign
	{
		get
		{
			string text = base.Attributes["valign"];
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("valign");
			}
			else
			{
				base.Attributes["valign"] = value;
			}
		}
	}

	private int Count
	{
		get
		{
			if (_cells != null)
			{
				return _cells.Count;
			}
			return 0;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> class.</summary>
	public HtmlTableRow()
		: base("tr")
	{
	}

	/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object for the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> control's child server controls.</returns>
	protected override ControlCollection CreateControlCollection()
	{
		return new HtmlTableCellControlCollection(this);
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> control's child controls to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the rendered content.</param>
	protected internal override void RenderChildren(HtmlTextWriter writer)
	{
		if (HasControls())
		{
			writer.Indent++;
			base.RenderChildren(writer);
			writer.Indent--;
			writer.WriteLine();
		}
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> control's end tag.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the rendered content.</param>
	protected override void RenderEndTag(HtmlTextWriter writer)
	{
		if (Count == 0)
		{
			writer.WriteLine();
		}
		writer.WriteEndTag(TagName);
		if (writer.Indent == 0)
		{
			writer.WriteLine();
		}
	}
}
