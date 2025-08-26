using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents the style properties for an element of a control that renders as a <see cref="T:System.Web.UI.WebControls.TableRow" /> or <see cref="T:System.Web.UI.WebControls.TableCell" />.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class TableItemStyle : Style
{
	[Flags]
	private enum TableItemStyles
	{
		HorizontalAlign = 0x10000,
		VerticalAlign = 0x20000,
		Wrap = 0x40000
	}

	/// <summary>Gets or sets the horizontal alignment of the contents in a cell.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> enumeration values. The default is <see langword="NotSet" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified horizontal alignment is not one of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> enumeration values. </exception>
	[DefaultValue(HorizontalAlign.NotSet)]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual HorizontalAlign HorizontalAlign
	{
		get
		{
			if (!CheckBit(65536))
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
			SetBit(65536);
		}
	}

	/// <summary>Gets or sets the vertical alignment of the contents in a cell.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.VerticalAlign" /> enumeration values. The default is <see langword="NotSet" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified vertical alignment was not one of the <see cref="T:System.Web.UI.WebControls.VerticalAlign" /> enumeration values. </exception>
	[DefaultValue(VerticalAlign.NotSet)]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual VerticalAlign VerticalAlign
	{
		get
		{
			if (!CheckBit(131072))
			{
				return VerticalAlign.NotSet;
			}
			return (VerticalAlign)base.ViewState["VerticalAlign"];
		}
		set
		{
			if (value < VerticalAlign.NotSet || value > VerticalAlign.Bottom)
			{
				throw new ArgumentOutOfRangeException(Locale.GetText("Invalid VerticalAlign value."));
			}
			base.ViewState["VerticalAlign"] = value;
			SetBit(131072);
		}
	}

	/// <summary>Gets or sets a value indicating whether the contents of a cell wrap in the cell.</summary>
	/// <returns>
	///     <see langword="true" /> if the contents of the cell wrap in the cell; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual bool Wrap
	{
		get
		{
			if (!CheckBit(262144))
			{
				return true;
			}
			return (bool)base.ViewState["Wrap"];
		}
		set
		{
			base.ViewState["Wrap"] = value;
			SetBit(262144);
		}
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> class using default values.</summary>
	public TableItemStyle()
	{
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> class with the specified state bag.</summary>
	/// <param name="bag">A <see cref="T:System.Web.UI.StateBag" /> that represents the state bag in which to store style information. </param>
	public TableItemStyle(StateBag bag)
		: base(bag)
	{
	}

	/// <summary>Adds information about horizontal alignment, vertical alignment, and wrap to the list of attributes to render.</summary>
	/// <param name="writer">The output stream that renders HTML content to the client. </param>
	/// <param name="owner">The control that the style refers to. </param>
	public override void AddAttributesToRender(HtmlTextWriter writer, WebControl owner)
	{
		base.AddAttributesToRender(writer, owner);
		if (writer != null)
		{
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
			switch (VerticalAlign)
			{
			case VerticalAlign.Top:
				writer.AddAttribute(HtmlTextWriterAttribute.Valign, "top", fEncode: false);
				break;
			case VerticalAlign.Middle:
				writer.AddAttribute(HtmlTextWriterAttribute.Valign, "middle", fEncode: false);
				break;
			case VerticalAlign.Bottom:
				writer.AddAttribute(HtmlTextWriterAttribute.Valign, "bottom", fEncode: false);
				break;
			}
			if (!Wrap)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
			}
		}
	}

	private void Copy(string name, TableItemStyles s, Style source)
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

	/// <summary>Duplicates the non-empty style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> into the instance of the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> class that this method is called from.</summary>
	/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style to copy. </param>
	public override void CopyFrom(Style s)
	{
		base.CopyFrom(s);
		if (s != null && !s.IsEmpty)
		{
			Copy("HorizontalAlign", TableItemStyles.HorizontalAlign, s);
			Copy("VerticalAlign", TableItemStyles.VerticalAlign, s);
			Copy("Wrap", TableItemStyles.Wrap, s);
		}
	}

	private void Merge(string name, TableItemStyles s, Style source)
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

	/// <summary>Combines the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> into the instance of the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> class that this method is called from.</summary>
	/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style to combine. </param>
	public override void MergeWith(Style s)
	{
		if (IsEmpty)
		{
			CopyFrom(s);
			return;
		}
		base.MergeWith(s);
		if (s != null)
		{
			Merge("HorizontalAlign", TableItemStyles.HorizontalAlign, s);
			Merge("VerticalAlign", TableItemStyles.VerticalAlign, s);
			Merge("Wrap", TableItemStyles.Wrap, s);
		}
	}

	/// <summary>Removes any defined style elements from the style.</summary>
	public override void Reset()
	{
		if (CheckBit(65536))
		{
			base.ViewState.Remove("HorizontalAlign");
		}
		if (CheckBit(131072))
		{
			base.ViewState.Remove("VerticalAlign");
		}
		if (CheckBit(262144))
		{
			base.ViewState.Remove("Wrap");
		}
		base.Reset();
	}
}
