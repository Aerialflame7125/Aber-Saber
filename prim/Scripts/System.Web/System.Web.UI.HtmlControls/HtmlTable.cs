using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls;

/// <summary>Allows programmatic access on the server to the HTML <see langword="&lt;table&gt;" /> element.</summary>
[ParseChildren(true, "Rows")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlTable : HtmlContainerControl
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects that are the rows of an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. </summary>
	protected class HtmlTableRowControlCollection : ControlCollection
	{
		internal HtmlTableRowControlCollection(HtmlTable owner)
			: base(owner)
		{
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the collection.</summary>
		/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the collection.</param>
		/// <exception cref="T:System.ArgumentException">The added control must be of type <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" />.</exception>
		public override void Add(Control child)
		{
			if (child == null)
			{
				throw new NullReferenceException("null");
			}
			if (!(child is HtmlTableRow))
			{
				throw new ArgumentException("child", Locale.GetText("Must be an HtmlTableRow instance."));
			}
			base.Add(child);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the collection. The new control is added to the array at the specified index location.</summary>
		/// <param name="index">The location in the array at which to add the child control. </param>
		/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the collection. </param>
		/// <exception cref="T:System.ArgumentException">The added control must be of type <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" />. </exception>
		public override void AddAt(int index, Control child)
		{
			if (child == null)
			{
				throw new NullReferenceException("null");
			}
			if (!(child is HtmlTableRow))
			{
				throw new ArgumentException("child", Locale.GetText("Must be an HtmlTableRow instance."));
			}
			base.AddAt(index, child);
		}
	}

	private HtmlTableRowCollection _rows;

	/// <summary>Gets or sets the alignment of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control in relation to other elements on the Web page.</summary>
	/// <returns>The alignment of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control in relation to other elements on the Web page. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
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

	/// <summary>Gets or sets the background color of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
	/// <returns>The background color of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
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

	/// <summary>Gets or sets the width (in pixels) of the border of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
	/// <returns>The width (in pixels) of the border of an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default is -1, which indicates that the border width is not set.</returns>
	[DefaultValue(-1)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public int Border
	{
		get
		{
			string text = base.Attributes["border"];
			if (text != null)
			{
				return Convert.ToInt32(text);
			}
			return -1;
		}
		set
		{
			if (value == -1)
			{
				base.Attributes.Remove("border");
			}
			else
			{
				base.Attributes["border"] = value.ToString(Helpers.InvariantCulture);
			}
		}
	}

	/// <summary>Gets or sets the border color of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
	/// <returns>The border color of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
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

	/// <summary>Gets or sets the amount of space (in pixels) between the contents of a cell and the cell's border in the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
	/// <returns>The amount of space (in pixels) between the contents of a cell and the cell's border in the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default value is -1, which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public int CellPadding
	{
		get
		{
			string text = base.Attributes["cellpadding"];
			if (text != null)
			{
				return Convert.ToInt32(text);
			}
			return -1;
		}
		set
		{
			if (value == -1)
			{
				base.Attributes.Remove("cellpadding");
			}
			else
			{
				base.Attributes["cellpadding"] = value.ToString(Helpers.InvariantCulture);
			}
		}
	}

	/// <summary>Gets or sets the amount of space (in pixels) between adjacent cells in the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
	/// <returns>The amount of space (in pixels) between adjacent cells in the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. The default value is -1, which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public int CellSpacing
	{
		get
		{
			string text = base.Attributes["cellspacing"];
			if (text != null)
			{
				return Convert.ToInt32(text);
			}
			return -1;
		}
		set
		{
			if (value == -1)
			{
				base.Attributes.Remove("cellspacing");
			}
			else
			{
				base.Attributes["cellspacing"] = value.ToString(Helpers.InvariantCulture);
			}
		}
	}

	/// <summary>Gets or sets the height of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
	/// <returns>The height of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</returns>
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

	/// <summary>Gets or sets the content between the opening and closing tags of the control, without automatically converting special characters to their equivalent HTML entities. This property is not supported for this control.</summary>
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

	/// <summary>Gets or sets the content between the opening and closing tags of the control, with automatic conversion of special characters to their equivalent HTML entities. This property is not supported for this control.</summary>
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

	/// <summary>Gets an <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection that contains all the rows in the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
	/// <returns>An <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> that contains all the rows in the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual HtmlTableRowCollection Rows
	{
		get
		{
			if (_rows == null)
			{
				_rows = new HtmlTableRowCollection(this);
			}
			return _rows;
		}
	}

	/// <summary>Gets or sets the width of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</summary>
	/// <returns>The width of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public string Width
	{
		get
		{
			string text = base.Attributes["width"];
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
				base.Attributes.Remove("width");
			}
			else
			{
				base.Attributes["width"] = value;
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> class.</summary>
	public HtmlTable()
		: base("table")
	{
	}

	/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object for the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. </summary>
	/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control's child server controls.</returns>
	protected override ControlCollection CreateControlCollection()
	{
		return new HtmlTableRowControlCollection(this);
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control's child controls to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content. </param>
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

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control's end tag.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content. </param>
	protected override void RenderEndTag(HtmlTextWriter writer)
	{
		writer.WriteLine();
		writer.WriteEndTag(TagName);
		writer.WriteLine();
	}
}
