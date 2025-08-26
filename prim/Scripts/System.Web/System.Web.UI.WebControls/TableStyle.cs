using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.WebControls;

/// <summary>Represents the style for the <see cref="T:System.Web.UI.WebControls.Table" /> control and some Web Parts.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class TableStyle : Style
{
	[Flags]
	private enum TableStyles
	{
		BackImageUrl = 0x10000,
		CellPadding = 0x20000,
		CellSpacing = 0x40000,
		GridLines = 0x80000,
		HorizontalAlign = 0x100000
	}

	/// <summary>Gets or sets the URL of an image to display in the background of a table control.</summary>
	/// <returns>The URL of an image to display in the background of a table control. The default is <see cref="F:System.String.Empty" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The URL of the background image was set to <see langword="null" />. </exception>
	[NotifyParentProperty(true)]
	[UrlProperty]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string BackImageUrl
	{
		get
		{
			if (!CheckBit(65536))
			{
				return string.Empty;
			}
			return (string)base.ViewState["BackImageUrl"];
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException("BackImageUrl");
			}
			base.ViewState["BackImageUrl"] = value;
			SetBit(65536);
		}
	}

	/// <summary>Gets or sets the amount of space between the contents of the cell and the cell's border.</summary>
	/// <returns>The distance (in pixels) between the contents of a cell and the cell's border. The default is <see langword="-1" />, which indicates that this property is not set.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified distance is set to a value less than <see langword="-1" />. </exception>
	[NotifyParentProperty(true)]
	[DefaultValue(-1)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual int CellPadding
	{
		get
		{
			if (!CheckBit(131072))
			{
				return -1;
			}
			return (int)base.ViewState["CellPadding"];
		}
		set
		{
			if (value < -1)
			{
				throw new ArgumentOutOfRangeException("< -1");
			}
			base.ViewState["CellPadding"] = value;
			SetBit(131072);
		}
	}

	/// <summary>Gets or sets the distance between table cells.</summary>
	/// <returns>The distance (in pixels) between table cells. The default is <see langword="-1" />, which indicates that this property is not set.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified distance is set to a value less than <see langword="-1" />. </exception>
	[NotifyParentProperty(true)]
	[DefaultValue(-1)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual int CellSpacing
	{
		get
		{
			if (!CheckBit(262144))
			{
				return -1;
			}
			return (int)base.ViewState["CellSpacing"];
		}
		set
		{
			if (value < -1)
			{
				throw new ArgumentOutOfRangeException("< -1");
			}
			base.ViewState["CellSpacing"] = value;
			SetBit(262144);
		}
	}

	/// <summary>Gets or sets a value that specifies whether the border between the cells of the table control is displayed.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.GridLines" /> enumeration values. The default is <see langword="Both" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.GridLines" /> enumeration values. </exception>
	[NotifyParentProperty(true)]
	[DefaultValue(GridLines.None)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual GridLines GridLines
	{
		get
		{
			if (!CheckBit(524288))
			{
				return GridLines.None;
			}
			return (GridLines)base.ViewState["GridLines"];
		}
		set
		{
			if (value < GridLines.None || value > GridLines.Both)
			{
				throw new ArgumentOutOfRangeException(Locale.GetText("Invalid GridLines value."));
			}
			base.ViewState["GridLines"] = value;
			SetBit(524288);
		}
	}

	/// <summary>Gets or sets the horizontal alignment of the table within its container.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> enumeration values. The default is <see langword="NotSet" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified horizontal alignment is not one of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> enumeration values. </exception>
	[NotifyParentProperty(true)]
	[DefaultValue(HorizontalAlign.NotSet)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual HorizontalAlign HorizontalAlign
	{
		get
		{
			if (!CheckBit(1048576))
			{
				return HorizontalAlign.NotSet;
			}
			return (HorizontalAlign)base.ViewState["HorizontalAlign"];
		}
		set
		{
			if (value < HorizontalAlign.NotSet || value > HorizontalAlign.Justify)
			{
				throw new ArgumentOutOfRangeException(Locale.GetText("Invalid HorizontalAlign value."));
			}
			base.ViewState["HorizontalAlign"] = value;
			SetBit(1048576);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TableStyle" /> class using default values.</summary>
	public TableStyle()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TableStyle" /> class with the specified state bag information.</summary>
	/// <param name="bag">A <see cref="T:System.Web.UI.StateBag" /> that represents the state bag in which to store style information. </param>
	public TableStyle(StateBag bag)
		: base(bag)
	{
	}

	/// <summary>Adds information about the background image, cell spacing, cell padding, gridlines, and alignment to the list of attributes to render.</summary>
	/// <param name="writer">The output stream that renders HTML content to the client. </param>
	/// <param name="owner">The control associated with the style. </param>
	[MonoTODO("collapse style should be rendered only for browsers which support that.")]
	public override void AddAttributesToRender(HtmlTextWriter writer, WebControl owner)
	{
		base.AddAttributesToRender(writer, owner);
		if (writer == null)
		{
			return;
		}
		int cellSpacing = CellSpacing;
		if (cellSpacing != -1)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, cellSpacing.ToString(Helpers.InvariantCulture), fEncode: false);
			if (cellSpacing == 0)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderCollapse, "collapse");
			}
		}
		cellSpacing = CellPadding;
		if (cellSpacing != -1)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, cellSpacing.ToString(Helpers.InvariantCulture), fEncode: false);
		}
		GridLines gridLines = GridLines;
		switch (gridLines)
		{
		case GridLines.Horizontal:
			writer.AddAttribute(HtmlTextWriterAttribute.Rules, "rows", fEncode: false);
			break;
		case GridLines.Vertical:
			writer.AddAttribute(HtmlTextWriterAttribute.Rules, "cols", fEncode: false);
			break;
		case GridLines.Both:
			writer.AddAttribute(HtmlTextWriterAttribute.Rules, "all", fEncode: false);
			break;
		}
		switch (HorizontalAlign)
		{
		case HorizontalAlign.Left:
			writer.AddAttribute(HtmlTextWriterAttribute.Align, "left", fEncode: false);
			break;
		case HorizontalAlign.Center:
			writer.AddAttribute(HtmlTextWriterAttribute.Align, "center", fEncode: false);
			break;
		case HorizontalAlign.Right:
			writer.AddAttribute(HtmlTextWriterAttribute.Align, "right", fEncode: false);
			break;
		case HorizontalAlign.Justify:
			writer.AddAttribute(HtmlTextWriterAttribute.Align, "justify", fEncode: false);
			break;
		}
		if (gridLines != 0 && base.BorderWidth.IsEmpty)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "1", fEncode: false);
		}
	}

	private void Copy(string name, TableStyles s, Style source)
	{
		if (source.CheckBit((int)s))
		{
			object obj = source.ViewState[name];
			if (obj != null)
			{
				base.ViewState[name] = obj;
				SetBit((int)s);
			}
		}
	}

	/// <summary>Copies non-blank elements from the specified style, overwriting existing style elements if necessary.</summary>
	/// <param name="s">The style to copy. </param>
	public override void CopyFrom(Style s)
	{
		base.CopyFrom(s);
		if (s != null && !s.IsEmpty)
		{
			Copy("BackImageUrl", TableStyles.BackImageUrl, s);
			Copy("CellPadding", TableStyles.CellPadding, s);
			Copy("CellSpacing", TableStyles.CellSpacing, s);
			Copy("GridLines", TableStyles.GridLines, s);
			Copy("HorizontalAlign", TableStyles.HorizontalAlign, s);
		}
	}

	private void Merge(string name, TableStyles s, Style source)
	{
		if (!CheckBit((int)s) && source.CheckBit((int)s))
		{
			object obj = source.ViewState[name];
			if (obj != null)
			{
				base.ViewState[name] = obj;
				SetBit((int)s);
			}
		}
	}

	/// <summary>Copies non-blank elements from the specified style, but will not overwrite any existing style elements.</summary>
	/// <param name="s">The style to copy. </param>
	public override void MergeWith(Style s)
	{
		if (IsEmpty)
		{
			CopyFrom(s);
			return;
		}
		base.MergeWith(s);
		if (s != null && !s.IsEmpty)
		{
			Merge("BackImageUrl", TableStyles.BackImageUrl, s);
			Merge("CellPadding", TableStyles.CellPadding, s);
			Merge("CellSpacing", TableStyles.CellSpacing, s);
			Merge("GridLines", TableStyles.GridLines, s);
			Merge("HorizontalAlign", TableStyles.HorizontalAlign, s);
		}
	}

	/// <summary>Clears any defined style elements of the style.</summary>
	public override void Reset()
	{
		if (CheckBit(65536))
		{
			base.ViewState.Remove("BackImageUrl");
		}
		if (CheckBit(131072))
		{
			base.ViewState.Remove("CellPadding");
		}
		if (CheckBit(262144))
		{
			base.ViewState.Remove("CellSpacing");
		}
		if (CheckBit(524288))
		{
			base.ViewState.Remove("GridLines");
		}
		if (CheckBit(1048576))
		{
			base.ViewState.Remove("HorizontalAlign");
		}
		base.Reset();
	}

	/// <summary>Adds the style properties of the <see cref="T:System.Web.UI.WebControls.TableStyle" /> object to the specified <see cref="T:System.Web.UI.CssStyleCollection" /> collection.</summary>
	/// <param name="attributes">The <see cref="T:System.Web.UI.CssStyleCollection" /> to which to add the style properties. </param>
	/// <param name="urlResolver">An object implemented by the <see cref="T:System.Web.UI.IUrlResolutionService" /> that contains the context information for the current location (URL). </param>
	protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
	{
		if (attributes != null)
		{
			string text = BackImageUrl;
			if (text.Length > 0)
			{
				if (urlResolver != null)
				{
					text = urlResolver.ResolveClientUrl(text);
				}
				attributes.Add(HtmlTextWriterStyle.BackgroundImage, text);
			}
		}
		base.FillStyleAttributes(attributes, urlResolver);
	}
}
