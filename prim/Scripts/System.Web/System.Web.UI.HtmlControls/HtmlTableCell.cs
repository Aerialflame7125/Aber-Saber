using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls;

/// <summary>Represents the <see langword="&lt;td&gt;" /> and <see langword="&lt;th&gt;" /> HTML elements in an <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> object.</summary>
[ConstructorNeedsTag(true)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlTableCell : HtmlContainerControl
{
	/// <summary>Gets or sets the horizontal alignment of the content in the cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
	/// <returns>The horizontal alignment of the content in the cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
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

	/// <summary>Gets or sets the background color of the cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
	/// <returns>The background color of the cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />.</returns>
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

	/// <summary>Gets or sets the border color of the cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
	/// <returns>The border color of the cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />.</returns>
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

	/// <summary>Gets or sets the number of columns occupied by a cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
	/// <returns>The number of columns occupied by the cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />. The default value is <see langword="-1" />, which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public int ColSpan
	{
		get
		{
			string text = base.Attributes["colspan"];
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
				base.Attributes.Remove("colspan");
			}
			else
			{
				base.Attributes["colspan"] = value.ToString(Helpers.InvariantCulture);
			}
		}
	}

	/// <summary>Gets or sets the height (in pixels) of the cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
	/// <returns>The height (in pixels) of the cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
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
				base.Attributes.Remove("align");
			}
			else
			{
				base.Attributes["height"] = value;
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the text in a cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class automatically continues on the next line when it reaches the end of the cell.</summary>
	/// <returns>
	///     <see langword="true" /> if the text does not automatically wrap in the cell; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[TypeConverter(typeof(MinimizableAttributeTypeConverter))]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public bool NoWrap
	{
		get
		{
			return base.Attributes["nowrap"] == "nowrap";
		}
		set
		{
			if (value)
			{
				base.Attributes["nowrap"] = "nowrap";
			}
			else
			{
				base.Attributes.Remove("nowrap");
			}
		}
	}

	/// <summary>Gets or sets the number of rows occupied by a cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
	/// <returns>The number of rows occupied by a cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class. The default value is <see langword="-1" />, which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public int RowSpan
	{
		get
		{
			string text = base.Attributes["rowspan"];
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
				base.Attributes.Remove("rowspan");
			}
			else
			{
				base.Attributes["rowspan"] = value.ToString(Helpers.InvariantCulture);
			}
		}
	}

	/// <summary>Gets or sets the vertical alignment for the content of a cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
	/// <returns>The vertical alignment for the content of a cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
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

	/// <summary>Gets or sets the width (in pixels) of the cell represented by an instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class.</summary>
	/// <returns>The width (in pixels) of the cell represented by an instance of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class using default values.</summary>
	public HtmlTableCell()
		: base("td")
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> class, using the specified tag name.</summary>
	/// <param name="tagName">The element name of the tag. </param>
	public HtmlTableCell(string tagName)
		: base(tagName)
	{
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> control's end tag.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
	protected override void RenderEndTag(HtmlTextWriter writer)
	{
		writer.WriteEndTag(TagName);
		if (writer.Indent == 0)
		{
			writer.WriteLine();
		}
	}
}
