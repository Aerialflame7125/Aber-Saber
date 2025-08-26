using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.Adapters;

namespace System.Web.UI.WebControls;

internal sealed class MenuListRenderer : BaseMenuRenderer
{
	private bool haveDynamicPopOut;

	public override HtmlTextWriterTag Tag => HtmlTextWriterTag.Div;

	public MenuListRenderer(Menu owner)
		: base(owner)
	{
	}

	public override void PreRender(Page page, HtmlHead head, ClientScriptManager csm, string cmenu, StringBuilder script)
	{
		Menu owner = base.Owner;
		script.AppendFormat("new Sys.WebForms.Menu ({{ element: '{0}', disappearAfter: {1}, orientation: '{2}', tabIndex: {3}, disabled: {4} }});", owner.ClientID, ClientScriptManager.GetScriptLiteral(owner.DisappearAfter), owner.Orientation.ToString().ToLowerInvariant(), ClientScriptManager.GetScriptLiteral(owner.TabIndex), (!owner.Enabled).ToString().ToLowerInvariant());
		Type typeFromHandle = typeof(Menu);
		if (!csm.IsClientScriptIncludeRegistered(typeFromHandle, "MenuModern.js"))
		{
			string webResourceUrl = csm.GetWebResourceUrl(typeFromHandle, "MenuModern.js");
			csm.RegisterClientScriptInclude(typeFromHandle, "MenuModern.js", webResourceUrl);
		}
		if (!owner.IncludeStyleBlock)
		{
			return;
		}
		if (head == null)
		{
			throw new InvalidOperationException("Using Menu.IncludeStyleBlock requires Page.Header to be non-null (e.g. <head runat=\"server\" />).");
		}
		StyleBlock styleBlock = new StyleBlock(owner.ClientID);
		Style controlStyle = owner.ControlStyle;
		bool num = owner.Orientation == Orientation.Horizontal;
		if (controlStyle != null)
		{
			styleBlock.RegisterStyle(controlStyle);
		}
		styleBlock.RegisterStyle(HtmlTextWriterStyle.BorderStyle, "none", "img.icon").Add(HtmlTextWriterStyle.VerticalAlign, "middle");
		styleBlock.RegisterStyle(HtmlTextWriterStyle.BorderStyle, "none", "img.separator").Add(HtmlTextWriterStyle.Display, "block");
		if (num)
		{
			styleBlock.RegisterStyle(HtmlTextWriterStyle.BorderStyle, "none", "img.horizontal-separator").Add(HtmlTextWriterStyle.VerticalAlign, "middle");
		}
		styleBlock.RegisterStyle(HtmlTextWriterStyle.ListStyleType, "none", "ul").Add(HtmlTextWriterStyle.Margin, "0").Add(HtmlTextWriterStyle.Padding, "0")
			.Add(HtmlTextWriterStyle.Width, "auto");
		SubMenuStyle staticMenuStyleInternal = owner.StaticMenuStyleInternal;
		if (staticMenuStyleInternal != null)
		{
			styleBlock.RegisterStyle(staticMenuStyleInternal, "ul.static");
		}
		NamedCssStyleCollection namedCssStyleCollection = styleBlock.RegisterStyle("ul.dynamic");
		staticMenuStyleInternal = owner.DynamicMenuStyleInternal;
		if (staticMenuStyleInternal != null)
		{
			staticMenuStyleInternal.ForeColor = Color.Empty;
			namedCssStyleCollection.Add(staticMenuStyleInternal);
		}
		namedCssStyleCollection.Add(HtmlTextWriterStyle.ZIndex, "1");
		int dynamicHorizontalOffset = owner.DynamicHorizontalOffset;
		if (dynamicHorizontalOffset != 0)
		{
			namedCssStyleCollection.Add(HtmlTextWriterStyle.MarginLeft, dynamicHorizontalOffset + "px");
		}
		dynamicHorizontalOffset = owner.DynamicVerticalOffset;
		if (dynamicHorizontalOffset != 0)
		{
			namedCssStyleCollection.Add(HtmlTextWriterStyle.MarginTop, dynamicHorizontalOffset + "px");
		}
		RenderLevelStyles(styleBlock, dynamicHorizontalOffset, owner.LevelSubMenuStyles, "ul.level");
		styleBlock.RegisterStyle(HtmlTextWriterStyle.TextDecoration, "none", "a").Add(HtmlTextWriterStyle.WhiteSpace, "nowrap").Add(HtmlTextWriterStyle.Display, "block");
		RenderAnchorStyle(styleBlock, owner.StaticMenuItemStyleInternal, "a.static");
		bool flag = false;
		string staticPopOutImageUrl = owner.StaticPopOutImageUrl;
		namedCssStyleCollection = null;
		string format = "url(\"{0}\")";
		if (string.IsNullOrEmpty(staticPopOutImageUrl))
		{
			if (owner.StaticEnableDefaultPopOutImage)
			{
				namedCssStyleCollection = styleBlock.RegisterStyle(HtmlTextWriterStyle.BackgroundImage, string.Format(format, GetArrowResourceUrl(owner)), "a.popout");
			}
			else
			{
				flag = true;
			}
		}
		else
		{
			namedCssStyleCollection = styleBlock.RegisterStyle(HtmlTextWriterStyle.BackgroundImage, string.Format(format, staticPopOutImageUrl), "a.popout");
			flag = true;
		}
		namedCssStyleCollection?.Add("background-repeat", "no-repeat").Add("background-position", "right center").Add(HtmlTextWriterStyle.PaddingRight, "14px");
		staticPopOutImageUrl = owner.DynamicPopOutImageUrl;
		bool flag2 = !string.IsNullOrEmpty(staticPopOutImageUrl);
		namedCssStyleCollection = null;
		if (flag || flag2)
		{
			format = "url(\"{0}\") no-repeat right center";
			if (!flag2)
			{
				if (owner.DynamicEnableDefaultPopOutImage)
				{
					namedCssStyleCollection = styleBlock.RegisterStyle(HtmlTextWriterStyle.BackgroundImage, string.Format(format, GetArrowResourceUrl(owner)), "a.popout-dynamic");
				}
			}
			else
			{
				namedCssStyleCollection = styleBlock.RegisterStyle(HtmlTextWriterStyle.BackgroundImage, string.Format(format, staticPopOutImageUrl), "a.popout-dynamic");
			}
		}
		if (namedCssStyleCollection != null)
		{
			haveDynamicPopOut = true;
			namedCssStyleCollection.Add(HtmlTextWriterStyle.PaddingRight, "14px");
		}
		RenderAnchorStyle(styleBlock, owner.DynamicMenuItemStyleInternal, "a.dynamic");
		dynamicHorizontalOffset = owner.StaticDisplayLevels;
		Unit staticSubMenuIndent = owner.StaticSubMenuIndent;
		string unitName;
		double indent;
		if (staticSubMenuIndent == Unit.Empty)
		{
			unitName = "em";
			indent = 1.0;
		}
		else
		{
			unitName = Unit.GetExtension(staticSubMenuIndent.Type);
			indent = staticSubMenuIndent.Value;
		}
		RenderLevelStyles(styleBlock, dynamicHorizontalOffset, owner.LevelMenuItemStyles, "a.level", unitName, indent);
		RenderLevelStyles(styleBlock, dynamicHorizontalOffset, owner.LevelSelectedStyles, "a.selected.level");
		RenderAnchorStyle(styleBlock, owner.StaticSelectedStyleInternal, "a.static.selected");
		RenderAnchorStyle(styleBlock, owner.DynamicSelectedStyleInternal, "a.dynamic.selected");
		controlStyle = owner.StaticHoverStyleInternal;
		if (controlStyle != null)
		{
			styleBlock.RegisterStyle(controlStyle, "a.static.highlighted");
		}
		controlStyle = owner.DynamicHoverStyleInternal;
		if (controlStyle != null)
		{
			styleBlock.RegisterStyle(controlStyle, "a.dynamic.highlighted");
		}
		head.Controls.Add(styleBlock);
	}

	public override void RenderBeginTag(HtmlTextWriter writer, string skipLinkText)
	{
		Menu owner = base.Owner;
		writer.AddAttribute(HtmlTextWriterAttribute.Href, "#" + owner.ClientID + "_SkipLink");
		writer.RenderBeginTag(HtmlTextWriterTag.A);
		writer.AddAttribute(HtmlTextWriterAttribute.Alt, skipLinkText);
		Page page = owner.Page;
		ClientScriptManager clientScriptManager = ((page != null) ? page.ClientScript : new ClientScriptManager(null));
		writer.AddAttribute(HtmlTextWriterAttribute.Src, clientScriptManager.GetWebResourceUrl(typeof(SiteMapPath), "transparent.gif"));
		writer.AddAttribute(HtmlTextWriterAttribute.Width, "0");
		writer.AddAttribute(HtmlTextWriterAttribute.Height, "0");
		writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
		writer.RenderBeginTag(HtmlTextWriterTag.Img);
		writer.RenderEndTag();
		writer.RenderEndTag();
	}

	public override void RenderEndTag(HtmlTextWriter writer)
	{
	}

	public override void AddAttributesToRender(HtmlTextWriter writer)
	{
	}

	public override void RenderContents(HtmlTextWriter writer)
	{
		Menu owner = base.Owner;
		MenuItemCollection items = owner.Items;
		owner.RenderMenu(writer, items, owner.Orientation == Orientation.Vertical, dynamic: false, 0, items.Count > 1);
	}

	public override void RenderMenuBeginTag(HtmlTextWriter writer, bool dynamic, int menuLevel)
	{
		if (dynamic || menuLevel == 0)
		{
			SubMenuStyle subMenuStyle = new SubMenuStyle();
			AddCssClass(subMenuStyle, "level" + (menuLevel + 1));
			FillMenuStyle(null, dynamic, menuLevel, subMenuStyle);
			subMenuStyle.AddAttributesToRender(writer);
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
		}
	}

	public override void RenderMenuEndTag(HtmlTextWriter writer, bool dynamic, int menuLevel)
	{
		if (dynamic || menuLevel == 0)
		{
			base.RenderMenuEndTag(writer, dynamic, menuLevel);
		}
	}

	public override void RenderMenuBody(HtmlTextWriter writer, MenuItemCollection items, bool vertical, bool dynamic, bool notLast)
	{
		Menu owner = base.Owner;
		int count = items.Count;
		OwnerContext oc = new OwnerContext(this);
		for (int i = 0; i < count; i++)
		{
			MenuItem item = items[i];
			if (owner.Adapter is MenuAdapter menuAdapter)
			{
				menuAdapter.RenderItem(writer, item, i);
			}
			else
			{
				RenderMenuItem(writer, item, vertical, i + 1 != count || notLast, i == 0, oc);
			}
		}
	}

	protected override void RenderMenuItem(HtmlTextWriter writer, MenuItem item, bool vertical, bool notLast, bool isFirst, OwnerContext oc)
	{
		Menu owner = base.Owner;
		bool num = owner.DisplayChildren(item);
		bool flag = IsDynamicItem(owner, item);
		int num2 = item.Depth + 1;
		writer.RenderBeginTag(HtmlTextWriterTag.Li);
		if (flag)
		{
			RenderSeparatorImage(owner, writer, oc.DynamicTopSeparatorImageUrl, standardsCompliant: true);
		}
		else
		{
			RenderSeparatorImage(owner, writer, oc.StaticTopSeparatorImageUrl, standardsCompliant: true);
		}
		Style style = new Style();
		if (num && (flag || num2 >= oc.StaticDisplayLevels))
		{
			AddCssClass(style, (flag && haveDynamicPopOut) ? "popout-dynamic" : "popout");
		}
		AddCssClass(style, "level" + num2);
		MenuItemStyleCollection levelMenuItemStyles = oc.LevelMenuItemStyles;
		if (levelMenuItemStyles != null && levelMenuItemStyles.Count >= num2)
		{
			string cssClass = levelMenuItemStyles[num2 - 1].CssClass;
			if (!string.IsNullOrEmpty(cssClass))
			{
				AddCssClass(style, cssClass);
			}
		}
		if (owner.SelectedItem == item)
		{
			AddCssClass(style, "selected");
		}
		string toolTip = item.ToolTip;
		if (!string.IsNullOrEmpty(toolTip))
		{
			writer.AddAttribute("title", toolTip);
		}
		style.AddAttributesToRender(writer);
		RenderItemHref(owner, writer, item);
		writer.RenderBeginTag(HtmlTextWriterTag.A);
		owner.RenderItemContent(writer, item, flag);
		writer.RenderEndTag();
		toolTip = item.SeparatorImageUrl;
		if (string.IsNullOrEmpty(toolTip))
		{
			toolTip = ((!flag) ? oc.StaticBottomSeparatorImageUrl : oc.DynamicBottomSeparatorImageUrl);
		}
		RenderSeparatorImage(owner, writer, toolTip, standardsCompliant: true);
		if (num)
		{
			owner.RenderMenu(writer, item.ChildItems, vertical, flag, num2, notLast);
		}
		if (num2 > 0)
		{
			writer.RenderEndTag();
		}
	}

	public override bool IsDynamicItem(Menu owner, MenuItem item)
	{
		if (owner == null)
		{
			throw new ArgumentNullException("owner");
		}
		if (item == null)
		{
			throw new ArgumentNullException("item");
		}
		return item.Depth + 1 >= base.Owner.StaticDisplayLevels;
	}

	private NamedCssStyleCollection RenderAnchorStyle(StyleBlock block, Style style, string styleName)
	{
		if (style == null || block == null)
		{
			return null;
		}
		style.AlwaysRenderTextDecoration = true;
		NamedCssStyleCollection namedCssStyleCollection = block.RegisterStyle(style, styleName);
		if (style.BorderStyle == BorderStyle.NotSet)
		{
			namedCssStyleCollection.Add(HtmlTextWriterStyle.BorderStyle, "none");
		}
		return namedCssStyleCollection;
	}

	private void RenderLevelStyles(StyleBlock block, int num, IList levelStyles, string name, string unitName = null, double indent = 0.0)
	{
		int num2 = levelStyles?.Count ?? 0;
		bool flag = num2 > 0;
		if (!flag || block == null)
		{
			return;
		}
		bool flag2 = !string.IsNullOrEmpty(unitName) && indent != 0.0;
		for (int i = 0; i < num2; i++)
		{
			if (i != 0 || flag)
			{
				NamedCssStyleCollection namedCssStyleCollection = block.RegisterStyle(name + (i + 1));
				if (flag && num2 > i && levelStyles[i] is Style style)
				{
					style.AlwaysRenderTextDecoration = true;
					namedCssStyleCollection.CopyFrom(style.GetStyleAttributes(null));
				}
				if (flag2 && i > 0 && i < num)
				{
					namedCssStyleCollection.Add(HtmlTextWriterStyle.PaddingLeft, indent.ToString(CultureInfo.InvariantCulture) + unitName);
					indent += indent;
				}
			}
		}
	}
}
