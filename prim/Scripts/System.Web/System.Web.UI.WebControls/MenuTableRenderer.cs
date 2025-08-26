using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.Adapters;

namespace System.Web.UI.WebControls;

internal sealed class MenuTableRenderer : BaseMenuRenderer
{
	private const string onPreRenderScript = "var {0} = new Object ();\n{0}.webForm = {1};\n{0}.disappearAfter = {2};\n{0}.vertical = {3};";

	public override HtmlTextWriterTag Tag => HtmlTextWriterTag.Table;

	public MenuTableRenderer(Menu owner)
		: base(owner)
	{
	}

	public override void AddAttributesToRender(HtmlTextWriter writer)
	{
		writer.AddAttribute("cellpadding", "0", fEndode: false);
		writer.AddAttribute("cellspacing", "0", fEndode: false);
		writer.AddAttribute("border", "0", fEndode: false);
		base.AddAttributesToRender(writer);
	}

	public override void PreRender(Page page, HtmlHead head, ClientScriptManager csm, string cmenu, StringBuilder script)
	{
		Menu owner = base.Owner;
		MenuItemStyle staticMenuItemStyleInternal = owner.StaticMenuItemStyleInternal;
		SubMenuStyle staticMenuStyleInternal = owner.StaticMenuStyleInternal;
		MenuItemStyle dynamicMenuItemStyleInternal = owner.DynamicMenuItemStyleInternal;
		SubMenuStyle dynamicMenuStyleInternal = owner.DynamicMenuStyleInternal;
		MenuItemStyleCollection levelMenuItemStyles = owner.LevelMenuItemStyles;
		List<Style> levelMenuItemLinkStyles = owner.LevelMenuItemLinkStyles;
		SubMenuStyleCollection levelSubMenuStylesInternal = owner.LevelSubMenuStylesInternal;
		MenuItemStyle staticSelectedStyleInternal = owner.StaticSelectedStyleInternal;
		MenuItemStyle dynamicSelectedStyleInternal = owner.DynamicSelectedStyleInternal;
		MenuItemStyleCollection levelSelectedStylesInternal = owner.LevelSelectedStylesInternal;
		List<Style> levelSelectedLinkStyles = owner.LevelSelectedLinkStyles;
		Style staticHoverStyleInternal = owner.StaticHoverStyleInternal;
		Style dynamicHoverStyleInternal = owner.DynamicHoverStyleInternal;
		if (!csm.IsClientScriptIncludeRegistered(typeof(Menu), "Menu.js"))
		{
			string webResourceUrl = csm.GetWebResourceUrl(typeof(Menu), "Menu.js");
			csm.RegisterClientScriptInclude(typeof(Menu), "Menu.js", webResourceUrl);
		}
		script.AppendFormat("var {0} = new Object ();\n{0}.webForm = {1};\n{0}.disappearAfter = {2};\n{0}.vertical = {3};", cmenu, page.IsMultiForm ? page.theForm : "window", ClientScriptManager.GetScriptLiteral(owner.DisappearAfter), ClientScriptManager.GetScriptLiteral(owner.Orientation == Orientation.Vertical));
		if (owner.DynamicHorizontalOffset != 0)
		{
			script.Append(cmenu + ".dho = " + ClientScriptManager.GetScriptLiteral(owner.DynamicHorizontalOffset) + ";\n");
		}
		if (owner.DynamicVerticalOffset != 0)
		{
			script.Append(cmenu + ".dvo = " + ClientScriptManager.GetScriptLiteral(owner.DynamicVerticalOffset) + ";\n");
		}
		RegisterStyle(owner.PopOutBoxStyle, head);
		RegisterStyle(owner.ControlStyle, owner.ControlLinkStyle, head);
		if (staticMenuItemStyleInternal != null)
		{
			RegisterStyle(owner.StaticMenuItemStyle, owner.StaticMenuItemLinkStyle, head);
		}
		if (staticMenuStyleInternal != null)
		{
			RegisterStyle(owner.StaticMenuStyle, head);
		}
		if (dynamicMenuItemStyleInternal != null)
		{
			RegisterStyle(owner.DynamicMenuItemStyle, owner.DynamicMenuItemLinkStyle, head);
		}
		if (dynamicMenuStyleInternal != null)
		{
			RegisterStyle(owner.DynamicMenuStyle, head);
		}
		if (levelMenuItemStyles != null && levelMenuItemStyles.Count > 0)
		{
			levelMenuItemLinkStyles = new List<Style>(levelMenuItemStyles.Count);
			foreach (Style item in levelMenuItemStyles)
			{
				Style style = new Style();
				levelMenuItemLinkStyles.Add(style);
				RegisterStyle(item, style, head);
			}
		}
		if (levelSubMenuStylesInternal != null)
		{
			foreach (Style item2 in levelSubMenuStylesInternal)
			{
				RegisterStyle(item2, head);
			}
		}
		if (staticSelectedStyleInternal != null)
		{
			RegisterStyle(staticSelectedStyleInternal, owner.StaticSelectedLinkStyle, head);
		}
		if (dynamicSelectedStyleInternal != null)
		{
			RegisterStyle(dynamicSelectedStyleInternal, owner.DynamicSelectedLinkStyle, head);
		}
		if (levelSelectedStylesInternal != null && levelSelectedStylesInternal.Count > 0)
		{
			levelSelectedLinkStyles = new List<Style>(levelSelectedStylesInternal.Count);
			foreach (Style item3 in levelSelectedStylesInternal)
			{
				Style style2 = new Style();
				levelSelectedLinkStyles.Add(style2);
				RegisterStyle(item3, style2, head);
			}
		}
		if (staticHoverStyleInternal != null)
		{
			if (head == null)
			{
				throw new InvalidOperationException("Using Menu.StaticHoverStyle requires Page.Header to be non-null (e.g. <head runat=\"server\" />).");
			}
			RegisterStyle(staticHoverStyleInternal, owner.StaticHoverLinkStyle, head);
			script.Append(cmenu + ".staticHover = " + ClientScriptManager.GetScriptLiteral(staticHoverStyleInternal.RegisteredCssClass) + ";\n");
			script.Append(cmenu + ".staticLinkHover = " + ClientScriptManager.GetScriptLiteral(owner.StaticHoverLinkStyle.RegisteredCssClass) + ";\n");
		}
		if (dynamicHoverStyleInternal != null)
		{
			if (head == null)
			{
				throw new InvalidOperationException("Using Menu.DynamicHoverStyle requires Page.Header to be non-null (e.g. <head runat=\"server\" />).");
			}
			RegisterStyle(dynamicHoverStyleInternal, owner.DynamicHoverLinkStyle, head);
			script.Append(cmenu + ".dynamicHover = " + ClientScriptManager.GetScriptLiteral(dynamicHoverStyleInternal.RegisteredCssClass) + ";\n");
			script.Append(cmenu + ".dynamicLinkHover = " + ClientScriptManager.GetScriptLiteral(owner.DynamicHoverLinkStyle.RegisteredCssClass) + ";\n");
		}
	}

	public override void RenderBeginTag(HtmlTextWriter writer, string skipLinkText)
	{
		Menu owner = base.Owner;
		writer.AddAttribute(HtmlTextWriterAttribute.Href, "#" + owner.ClientID + "_SkipLink");
		writer.RenderBeginTag(HtmlTextWriterTag.A);
		writer.AddAttribute(HtmlTextWriterAttribute.Alt, skipLinkText);
		writer.AddAttribute(HtmlTextWriterAttribute.Height, "0");
		writer.AddAttribute(HtmlTextWriterAttribute.Width, "0");
		Page page = owner.Page;
		ClientScriptManager clientScriptManager = ((page != null) ? page.ClientScript : new ClientScriptManager(null));
		writer.AddAttribute(HtmlTextWriterAttribute.Src, clientScriptManager.GetWebResourceUrl(typeof(SiteMapPath), "transparent.gif"));
		writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
		writer.RenderBeginTag(HtmlTextWriterTag.Img);
		writer.RenderEndTag();
		writer.RenderEndTag();
	}

	public override void RenderEndTag(HtmlTextWriter writer)
	{
		Menu owner = base.Owner;
		if (owner.StaticDisplayLevels == 1 && owner.MaximumDynamicDisplayLevels > 0)
		{
			owner.RenderDynamicMenu(writer, owner.Items);
		}
	}

	public override void RenderContents(HtmlTextWriter writer)
	{
		Menu owner = base.Owner;
		RenderMenuBody(writer, owner.Items, owner.Orientation == Orientation.Vertical, dynamic: false, notLast: false);
	}

	private void RenderMenuBeginTagAttributes(HtmlTextWriter writer, bool dynamic, int menuLevel)
	{
		writer.AddAttribute("cellpadding", "0", fEndode: false);
		writer.AddAttribute("cellspacing", "0", fEndode: false);
		writer.AddAttribute("border", "0", fEndode: false);
		if (!dynamic)
		{
			SubMenuStyle subMenuStyle = new SubMenuStyle();
			FillMenuStyle(null, dynamic, menuLevel, subMenuStyle);
			subMenuStyle.AddAttributesToRender(writer);
		}
	}

	public override void RenderMenuBeginTag(HtmlTextWriter writer, bool dynamic, int menuLevel)
	{
		RenderMenuBeginTagAttributes(writer, dynamic, menuLevel);
		writer.RenderBeginTag(HtmlTextWriterTag.Table);
	}

	private void RenderMenuItemSpacing(HtmlTextWriter writer, Unit itemSpacing, bool vertical)
	{
		if (vertical)
		{
			writer.AddStyleAttribute("height", itemSpacing.ToString());
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}
		else
		{
			writer.AddStyleAttribute("width", itemSpacing.ToString());
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.RenderEndTag();
		}
	}

	public override void RenderMenuBody(HtmlTextWriter writer, MenuItemCollection items, bool vertical, bool dynamic, bool notLast)
	{
		Menu owner = base.Owner;
		if (!vertical)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
		}
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
		if (!vertical)
		{
			writer.RenderEndTag();
		}
	}

	protected override void RenderMenuItem(HtmlTextWriter writer, MenuItem item, bool vertical, bool notLast, bool isFirst, OwnerContext oc)
	{
		Menu owner = base.Owner;
		string clientID = oc.ClientID;
		bool flag = owner.DisplayChildren(item);
		bool flag2 = flag && item.Depth + 1 >= oc.StaticDisplayLevels;
		bool flag3 = IsDynamicItem(owner, item);
		bool flag4 = oc.IsVertical || flag3;
		Unit itemSpacing = owner.GetItemSpacing(item, flag3);
		if (itemSpacing != Unit.Empty && (item.Depth > 0 || !isFirst))
		{
			RenderMenuItemSpacing(writer, itemSpacing, flag4);
		}
		if (!string.IsNullOrEmpty(item.ToolTip))
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Title, item.ToolTip);
		}
		if (flag4)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
		}
		string text = (flag3 ? ("'" + item.Parent.Path + "'") : "null");
		if (flag2)
		{
			writer.AddAttribute("onmouseover", "javascript:Menu_OverItem ('" + clientID + "','" + item.Path + "'," + text + ")");
			writer.AddAttribute("onmouseout", "javascript:Menu_OutItem ('" + clientID + "','" + item.Path + "')");
		}
		else if (flag3)
		{
			writer.AddAttribute("onmouseover", "javascript:Menu_OverDynamicLeafItem ('" + clientID + "','" + item.Path + "'," + text + ")");
			writer.AddAttribute("onmouseout", "javascript:Menu_OutItem ('" + clientID + "','" + item.Path + "'," + text + ")");
		}
		else
		{
			writer.AddAttribute("onmouseover", "javascript:Menu_OverStaticLeafItem ('" + clientID + "','" + item.Path + "')");
			writer.AddAttribute("onmouseout", "javascript:Menu_OutItem ('" + clientID + "','" + item.Path + "')");
		}
		writer.RenderBeginTag(HtmlTextWriterTag.Td);
		if (flag3)
		{
			RenderSeparatorImage(owner, writer, oc.DynamicTopSeparatorImageUrl, standardsCompliant: false);
		}
		else
		{
			RenderSeparatorImage(owner, writer, oc.StaticTopSeparatorImageUrl, standardsCompliant: false);
		}
		MenuItemStyle menuItemStyle = new MenuItemStyle();
		if (oc.Header != null)
		{
			if (!flag3 && oc.StaticMenuItemStyle != null)
			{
				AddCssClass(menuItemStyle, oc.StaticMenuItemStyle.CssClass);
				AddCssClass(menuItemStyle, oc.StaticMenuItemStyle.RegisteredCssClass);
			}
			if (flag3 && oc.DynamicMenuItemStyle != null)
			{
				AddCssClass(menuItemStyle, oc.DynamicMenuItemStyle.CssClass);
				AddCssClass(menuItemStyle, oc.DynamicMenuItemStyle.RegisteredCssClass);
			}
			if (oc.LevelMenuItemStyles != null && oc.LevelMenuItemStyles.Count > item.Depth)
			{
				AddCssClass(menuItemStyle, oc.LevelMenuItemStyles[item.Depth].CssClass);
				AddCssClass(menuItemStyle, oc.LevelMenuItemStyles[item.Depth].RegisteredCssClass);
			}
			if (item == oc.SelectedItem)
			{
				if (!flag3 && oc.StaticSelectedStyle != null)
				{
					AddCssClass(menuItemStyle, oc.StaticSelectedStyle.CssClass);
					AddCssClass(menuItemStyle, oc.StaticSelectedStyle.RegisteredCssClass);
				}
				if (flag3 && oc.DynamicSelectedStyle != null)
				{
					AddCssClass(menuItemStyle, oc.DynamicSelectedStyle.CssClass);
					AddCssClass(menuItemStyle, oc.DynamicSelectedStyle.RegisteredCssClass);
				}
				if (oc.LevelSelectedStyles != null && oc.LevelSelectedStyles.Count > item.Depth)
				{
					AddCssClass(menuItemStyle, oc.LevelSelectedStyles[item.Depth].CssClass);
					AddCssClass(menuItemStyle, oc.LevelSelectedStyles[item.Depth].RegisteredCssClass);
				}
			}
		}
		else
		{
			if (!flag3 && oc.StaticMenuItemStyle != null)
			{
				menuItemStyle.CopyFrom(oc.StaticMenuItemStyle);
			}
			if (flag3 && oc.DynamicMenuItemStyle != null)
			{
				menuItemStyle.CopyFrom(oc.DynamicMenuItemStyle);
			}
			if (oc.LevelMenuItemStyles != null && oc.LevelMenuItemStyles.Count > item.Depth)
			{
				menuItemStyle.CopyFrom(oc.LevelMenuItemStyles[item.Depth]);
			}
			if (item == oc.SelectedItem)
			{
				if (!flag3 && oc.StaticSelectedStyle != null)
				{
					menuItemStyle.CopyFrom(oc.StaticSelectedStyle);
				}
				if (flag3 && oc.DynamicSelectedStyle != null)
				{
					menuItemStyle.CopyFrom(oc.DynamicSelectedStyle);
				}
				if (oc.LevelSelectedStyles != null && oc.LevelSelectedStyles.Count > item.Depth)
				{
					menuItemStyle.CopyFrom(oc.LevelSelectedStyles[item.Depth]);
				}
			}
		}
		menuItemStyle.AddAttributesToRender(writer);
		writer.AddAttribute("id", GetItemClientId(clientID, item, "i"));
		writer.AddAttribute("cellpadding", "0", fEndode: false);
		writer.AddAttribute("cellspacing", "0", fEndode: false);
		writer.AddAttribute("border", "0", fEndode: false);
		writer.AddAttribute("width", "100%", fEndode: false);
		writer.RenderBeginTag(HtmlTextWriterTag.Table);
		writer.RenderBeginTag(HtmlTextWriterTag.Tr);
		if (flag4)
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
		}
		if (!owner.ItemWrap)
		{
			writer.AddStyleAttribute("white-space", "nowrap");
		}
		writer.RenderBeginTag(HtmlTextWriterTag.Td);
		RenderItemHref(owner, writer, item);
		Style style = new Style();
		if (oc.Header != null)
		{
			AddCssClass(style, oc.ControlLinkStyle.RegisteredCssClass);
			if (!flag3 && oc.StaticMenuItemStyle != null)
			{
				AddCssClass(style, oc.StaticMenuItemStyle.CssClass);
				AddCssClass(style, oc.StaticMenuItemLinkStyle.RegisteredCssClass);
			}
			if (flag3 && oc.DynamicMenuItemStyle != null)
			{
				AddCssClass(style, oc.DynamicMenuItemStyle.CssClass);
				AddCssClass(style, oc.DynamicMenuItemLinkStyle.RegisteredCssClass);
			}
			if (oc.LevelMenuItemStyles != null && oc.LevelMenuItemStyles.Count > item.Depth)
			{
				AddCssClass(style, oc.LevelMenuItemStyles[item.Depth].CssClass);
				AddCssClass(style, oc.LevelMenuItemLinkStyles[item.Depth].RegisteredCssClass);
			}
			if (item == oc.SelectedItem)
			{
				if (!flag3 && oc.StaticSelectedStyle != null)
				{
					AddCssClass(style, oc.StaticSelectedStyle.CssClass);
					AddCssClass(style, oc.StaticSelectedLinkStyle.RegisteredCssClass);
				}
				if (flag3 && oc.DynamicSelectedStyle != null)
				{
					AddCssClass(style, oc.DynamicSelectedStyle.CssClass);
					AddCssClass(style, oc.DynamicSelectedLinkStyle.RegisteredCssClass);
				}
				if (oc.LevelSelectedStyles != null && oc.LevelSelectedStyles.Count > item.Depth)
				{
					AddCssClass(style, oc.LevelSelectedStyles[item.Depth].CssClass);
					AddCssClass(style, oc.LevelSelectedLinkStyles[item.Depth].RegisteredCssClass);
				}
			}
		}
		else
		{
			style.CopyFrom(oc.ControlLinkStyle);
			if (!flag3 && oc.StaticMenuItemStyle != null)
			{
				style.CopyFrom(oc.StaticMenuItemLinkStyle);
			}
			if (flag3 && oc.DynamicMenuItemStyle != null)
			{
				style.CopyFrom(oc.DynamicMenuItemLinkStyle);
			}
			if (oc.LevelMenuItemStyles != null && oc.LevelMenuItemStyles.Count > item.Depth)
			{
				style.CopyFrom(oc.LevelMenuItemLinkStyles[item.Depth]);
			}
			if (item == oc.SelectedItem)
			{
				if (!flag3 && oc.StaticSelectedStyle != null)
				{
					style.CopyFrom(oc.StaticSelectedLinkStyle);
				}
				if (flag3 && oc.DynamicSelectedStyle != null)
				{
					style.CopyFrom(oc.DynamicSelectedLinkStyle);
				}
				if (oc.LevelSelectedStyles != null && oc.LevelSelectedStyles.Count > item.Depth)
				{
					style.CopyFrom(oc.LevelSelectedLinkStyles[item.Depth]);
				}
			}
			style.AlwaysRenderTextDecoration = true;
		}
		style.AddAttributesToRender(writer);
		writer.AddAttribute("id", GetItemClientId(clientID, item, "l"));
		if (item.Depth > 0 && !flag3)
		{
			Unit staticSubMenuIndent = oc.StaticSubMenuIndent;
			double num = ((!(staticSubMenuIndent == Unit.Empty)) ? staticSubMenuIndent.Value : 16.0);
			Unit unit = new Unit(num * (double)item.Depth, oc.StaticSubMenuIndent.Type);
			writer.AddStyleAttribute(HtmlTextWriterStyle.MarginLeft, unit.ToString());
		}
		writer.RenderBeginTag(HtmlTextWriterTag.A);
		owner.RenderItemContent(writer, item, flag3);
		writer.RenderEndTag();
		writer.RenderEndTag();
		if (flag2)
		{
			string popOutImage = GetPopOutImage(owner, item, flag3);
			if (popOutImage != null)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.AddAttribute("src", owner.ResolveClientUrl(popOutImage));
				writer.AddAttribute("border", "0");
				string value = string.Format(flag3 ? oc.DynamicPopOutImageTextFormatString : oc.StaticPopOutImageTextFormatString, item.Text);
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, value);
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
		}
		writer.RenderEndTag();
		writer.RenderEndTag();
		writer.RenderEndTag();
		if (!flag4 && itemSpacing == Unit.Empty && (notLast || (flag && !flag2)))
		{
			writer.AddStyleAttribute("width", "3px");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.RenderEndTag();
		}
		string text2 = item.SeparatorImageUrl;
		if (text2.Length == 0)
		{
			text2 = ((!flag3) ? oc.StaticBottomSeparatorImageUrl : oc.DynamicBottomSeparatorImageUrl);
		}
		if (text2.Length > 0)
		{
			if (!flag4)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
			}
			RenderSeparatorImage(owner, writer, text2, standardsCompliant: false);
			if (!flag4)
			{
				writer.RenderEndTag();
			}
		}
		if (flag4)
		{
			writer.RenderEndTag();
		}
		if (itemSpacing != Unit.Empty)
		{
			RenderMenuItemSpacing(writer, itemSpacing, flag4);
		}
		if (flag && !flag2)
		{
			if (flag4)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.AddAttribute("width", "100%");
			owner.RenderMenu(writer, item.ChildItems, vertical, dynamic: false, item.Depth + 1, notLast);
			if (item.Depth + 2 == oc.StaticDisplayLevels)
			{
				owner.RenderDynamicMenu(writer, item.ChildItems);
			}
			writer.RenderEndTag();
			if (flag4)
			{
				writer.RenderEndTag();
			}
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
		return item.Depth + 1 > owner.StaticDisplayLevels;
	}
}
