using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;

namespace System.Web.UI.WebControls;

internal abstract class BaseMenuRenderer : IMenuRenderer
{
	protected sealed class OwnerContext
	{
		private BaseMenuRenderer container;

		private string staticPopOutImageTextFormatString;

		private string dynamicPopOutImageTextFormatString;

		private string dynamicTopSeparatorImageUrl;

		private string dynamicBottomSeparatorImageUrl;

		private string staticTopSeparatorImageUrl;

		private string staticBottomSeparatorImageUrl;

		private List<Style> levelMenuItemLinkStyles;

		private List<Style> levelSelectedLinkStyles;

		private Style staticMenuItemLinkStyle;

		private Style dynamicMenuItemLinkStyle;

		private MenuItemStyle staticSelectedStyle;

		private Style staticSelectedLinkStyle;

		private MenuItemStyle dynamicSelectedStyle;

		private Style dynamicSelectedLinkStyle;

		private MenuItemStyleCollection levelSelectedStyles;

		private ITemplate dynamicItemTemplate;

		private bool dynamicItemTemplateQueried;

		public readonly MenuItemStyle StaticMenuItemStyle;

		public readonly MenuItemStyle DynamicMenuItemStyle;

		public readonly MenuItemStyleCollection LevelMenuItemStyles;

		public readonly Style ControlLinkStyle;

		public readonly HtmlHead Header;

		public readonly string ClientID;

		public readonly int StaticDisplayLevels;

		public readonly bool IsVertical;

		public readonly MenuItem SelectedItem;

		public readonly Unit StaticSubMenuIndent;

		public string StaticPopOutImageTextFormatString
		{
			get
			{
				if (staticPopOutImageTextFormatString == null)
				{
					staticPopOutImageTextFormatString = container.Owner.StaticPopOutImageTextFormatString;
				}
				return staticPopOutImageTextFormatString;
			}
		}

		public string DynamicPopOutImageTextFormatString
		{
			get
			{
				if (dynamicPopOutImageTextFormatString == null)
				{
					dynamicPopOutImageTextFormatString = container.Owner.DynamicPopOutImageTextFormatString;
				}
				return dynamicPopOutImageTextFormatString;
			}
		}

		public string DynamicTopSeparatorImageUrl
		{
			get
			{
				if (dynamicTopSeparatorImageUrl == null)
				{
					dynamicTopSeparatorImageUrl = container.Owner.DynamicTopSeparatorImageUrl;
				}
				return dynamicTopSeparatorImageUrl;
			}
		}

		public string DynamicBottomSeparatorImageUrl
		{
			get
			{
				if (dynamicBottomSeparatorImageUrl == null)
				{
					dynamicBottomSeparatorImageUrl = container.Owner.DynamicBottomSeparatorImageUrl;
				}
				return dynamicBottomSeparatorImageUrl;
			}
		}

		public string StaticTopSeparatorImageUrl
		{
			get
			{
				if (staticTopSeparatorImageUrl == null)
				{
					staticTopSeparatorImageUrl = container.Owner.StaticTopSeparatorImageUrl;
				}
				return staticBottomSeparatorImageUrl;
			}
		}

		public string StaticBottomSeparatorImageUrl
		{
			get
			{
				if (staticBottomSeparatorImageUrl == null)
				{
					staticBottomSeparatorImageUrl = container.Owner.StaticBottomSeparatorImageUrl;
				}
				return staticBottomSeparatorImageUrl;
			}
		}

		public List<Style> LevelMenuItemLinkStyles
		{
			get
			{
				if (levelMenuItemLinkStyles == null)
				{
					levelMenuItemLinkStyles = container.Owner.LevelMenuItemLinkStyles;
				}
				return levelMenuItemLinkStyles;
			}
		}

		public List<Style> LevelSelectedLinkStyles
		{
			get
			{
				if (levelSelectedLinkStyles == null)
				{
					levelSelectedLinkStyles = container.Owner.LevelSelectedLinkStyles;
				}
				return levelSelectedLinkStyles;
			}
		}

		public Style StaticMenuItemLinkStyle
		{
			get
			{
				if (staticMenuItemLinkStyle == null)
				{
					staticMenuItemLinkStyle = container.Owner.StaticMenuItemLinkStyle;
				}
				return staticMenuItemLinkStyle;
			}
		}

		public Style DynamicMenuItemLinkStyle
		{
			get
			{
				if (dynamicMenuItemLinkStyle == null)
				{
					dynamicMenuItemLinkStyle = container.Owner.DynamicMenuItemLinkStyle;
				}
				return dynamicMenuItemLinkStyle;
			}
		}

		public MenuItemStyle StaticSelectedStyle
		{
			get
			{
				if (staticSelectedStyle == null)
				{
					staticSelectedStyle = container.Owner.StaticSelectedStyle;
				}
				return staticSelectedStyle;
			}
		}

		public MenuItemStyle DynamicSelectedStyle
		{
			get
			{
				if (dynamicSelectedStyle == null)
				{
					dynamicSelectedStyle = container.Owner.DynamicSelectedStyle;
				}
				return dynamicSelectedStyle;
			}
		}

		public Style StaticSelectedLinkStyle
		{
			get
			{
				if (staticSelectedLinkStyle == null)
				{
					staticSelectedLinkStyle = container.Owner.StaticSelectedLinkStyle;
				}
				return staticSelectedLinkStyle;
			}
		}

		public Style DynamicSelectedLinkStyle
		{
			get
			{
				if (dynamicSelectedLinkStyle == null)
				{
					dynamicSelectedLinkStyle = container.Owner.DynamicSelectedLinkStyle;
				}
				return dynamicSelectedLinkStyle;
			}
		}

		public MenuItemStyleCollection LevelSelectedStyles
		{
			get
			{
				if (levelSelectedStyles == null)
				{
					levelSelectedStyles = container.Owner.LevelSelectedStyles;
				}
				return levelSelectedStyles;
			}
		}

		public ITemplate DynamicItemTemplate
		{
			get
			{
				if (!dynamicItemTemplateQueried && dynamicItemTemplate == null)
				{
					dynamicItemTemplate = container.Owner.DynamicItemTemplate;
					dynamicItemTemplateQueried = true;
				}
				return dynamicItemTemplate;
			}
		}

		public OwnerContext(BaseMenuRenderer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			this.container = container;
			Menu owner = container.Owner;
			Header = owner.Page?.Header;
			ClientID = owner.ClientID;
			IsVertical = owner.Orientation == Orientation.Vertical;
			StaticSubMenuIndent = owner.StaticSubMenuIndent;
			SelectedItem = owner.SelectedItem;
			ControlLinkStyle = owner.ControlLinkStyle;
			StaticDisplayLevels = owner.StaticDisplayLevels;
			StaticMenuItemStyle = owner.StaticMenuItemStyleInternal;
			DynamicMenuItemStyle = owner.DynamicMenuItemStyleInternal;
			LevelMenuItemStyles = owner.LevelMenuItemStyles;
		}
	}

	private int registeredStylesCounter = -1;

	public abstract HtmlTextWriterTag Tag { get; }

	protected Menu Owner { get; private set; }

	public BaseMenuRenderer(Menu owner)
	{
		if (owner == null)
		{
			throw new ArgumentNullException("owner");
		}
		Owner = owner;
	}

	public virtual void AddAttributesToRender(HtmlTextWriter writer)
	{
		Menu owner = Owner;
		Page page = owner.Page;
		SubMenuStyle staticMenuStyleInternal = owner.StaticMenuStyleInternal;
		SubMenuStyleCollection levelSubMenuStylesInternal = owner.LevelSubMenuStylesInternal;
		bool flag = levelSubMenuStylesInternal != null && levelSubMenuStylesInternal.Count > 0;
		Style style = ((flag || staticMenuStyleInternal != null) ? owner.ControlStyle : null);
		if (page != null && page.Header != null)
		{
			if (staticMenuStyleInternal != null)
			{
				AddCssClass(style, staticMenuStyleInternal.CssClass);
				AddCssClass(style, staticMenuStyleInternal.RegisteredCssClass);
			}
			if (flag)
			{
				AddCssClass(style, levelSubMenuStylesInternal[0].CssClass);
				AddCssClass(style, levelSubMenuStylesInternal[0].RegisteredCssClass);
			}
		}
		else
		{
			if (staticMenuStyleInternal != null)
			{
				style.CopyFrom(staticMenuStyleInternal);
			}
			if (flag)
			{
				style.CopyFrom(levelSubMenuStylesInternal[0]);
			}
		}
	}

	public abstract void PreRender(Page page, HtmlHead head, ClientScriptManager csm, string cmenu, StringBuilder script);

	public abstract void RenderMenuBeginTag(HtmlTextWriter writer, bool dynamic, int menuLevel);

	public abstract void RenderMenuBody(HtmlTextWriter writer, MenuItemCollection items, bool vertical, bool dynamic, bool notLast);

	public abstract void RenderBeginTag(HtmlTextWriter writer, string skipLinkText);

	public abstract void RenderEndTag(HtmlTextWriter writer);

	public abstract void RenderContents(HtmlTextWriter writer);

	public abstract bool IsDynamicItem(Menu owner, MenuItem item);

	protected abstract void RenderMenuItem(HtmlTextWriter writer, MenuItem item, bool vertical, bool notLast, bool isFirst, OwnerContext oc);

	public virtual void RenderMenuItem(HtmlTextWriter writer, MenuItem item, bool notLast, bool isFirst)
	{
		OwnerContext ownerContext = new OwnerContext(this);
		RenderMenuItem(writer, item, ownerContext.IsVertical, notLast, isFirst, ownerContext);
	}

	public virtual void RenderMenuEndTag(HtmlTextWriter writer, bool dynamic, int menuLevel)
	{
		writer.RenderEndTag();
	}

	public virtual void RenderItemContent(HtmlTextWriter writer, MenuItem item, bool isDynamicItem)
	{
		Menu owner = Owner;
		if (!string.IsNullOrEmpty(item.ImageUrl))
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Src, owner.ResolveClientUrl(item.ImageUrl));
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, item.ToolTip);
			writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
			writer.AddStyleAttribute(HtmlTextWriterStyle.VerticalAlign, "middle");
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}
		string dynamicItemFormatString;
		if (isDynamicItem && (dynamicItemFormatString = owner.DynamicItemFormatString).Length > 0)
		{
			writer.Write(string.Format(dynamicItemFormatString, item.Text));
		}
		else if (!isDynamicItem && (dynamicItemFormatString = owner.StaticItemFormatString).Length > 0)
		{
			writer.Write(string.Format(dynamicItemFormatString, item.Text));
		}
		else
		{
			writer.Write(item.Text);
		}
	}

	public void AddCssClass(Style style, string cssClass)
	{
		style.AddCssClass(cssClass);
	}

	public string GetItemClientId(string ownerClientID, MenuItem item, string suffix)
	{
		return ownerClientID + "_" + item.Path + suffix;
	}

	public virtual void RenderItemHref(Menu owner, HtmlTextWriter writer, MenuItem item)
	{
		if (!item.BranchEnabled)
		{
			writer.AddAttribute("disabled", "true", fEndode: false);
		}
		else if (!item.Selectable)
		{
			writer.AddAttribute("href", "#", fEndode: false);
			writer.AddStyleAttribute("cursor", "text");
		}
		else if (item.NavigateUrl != string.Empty)
		{
			string text = ((item.Target != string.Empty) ? item.Target : owner.Target);
			string value = owner.ResolveClientUrl(item.NavigateUrl);
			writer.AddAttribute("href", value);
			if (text != string.Empty)
			{
				writer.AddAttribute("target", text);
			}
		}
		else
		{
			writer.AddAttribute("href", GetClientEvent(owner, item));
		}
	}

	public string GetPopOutImage(Menu owner, MenuItem item, bool isDynamicItem)
	{
		if (owner == null)
		{
			owner = Owner;
		}
		if (item.PopOutImageUrl != string.Empty)
		{
			return item.PopOutImageUrl;
		}
		bool flag = false;
		if (isDynamicItem)
		{
			if (owner.DynamicPopOutImageUrl != string.Empty)
			{
				return owner.DynamicPopOutImageUrl;
			}
			if (owner.DynamicEnableDefaultPopOutImage)
			{
				flag = true;
			}
		}
		else
		{
			if (owner.StaticPopOutImageUrl != string.Empty)
			{
				return owner.StaticPopOutImageUrl;
			}
			if (owner.StaticEnableDefaultPopOutImage)
			{
				flag = true;
			}
		}
		if (flag)
		{
			return GetArrowResourceUrl(owner);
		}
		return null;
	}

	public string GetArrowResourceUrl(Menu owner)
	{
		return (owner.Page?.ClientScript)?.GetWebResourceUrl(typeof(Menu), "arrow_plus.gif");
	}

	public void FillMenuStyle(HtmlHead header, bool dynamic, int menuLevel, SubMenuStyle style)
	{
		Menu owner = Owner;
		if (header == null)
		{
			header = owner.Page?.Header;
		}
		SubMenuStyle staticMenuStyleInternal = owner.StaticMenuStyleInternal;
		SubMenuStyle dynamicMenuStyleInternal = owner.DynamicMenuStyleInternal;
		SubMenuStyleCollection levelSubMenuStylesInternal = owner.LevelSubMenuStylesInternal;
		if (header != null)
		{
			if (!dynamic && staticMenuStyleInternal != null)
			{
				AddCssClass(style, staticMenuStyleInternal.CssClass);
				AddCssClass(style, staticMenuStyleInternal.RegisteredCssClass);
			}
			if (dynamic && dynamicMenuStyleInternal != null)
			{
				AddCssClass(style, dynamicMenuStyleInternal.CssClass);
				AddCssClass(style, dynamicMenuStyleInternal.RegisteredCssClass);
			}
			if (levelSubMenuStylesInternal != null && levelSubMenuStylesInternal.Count > menuLevel)
			{
				AddCssClass(style, levelSubMenuStylesInternal[menuLevel].CssClass);
				AddCssClass(style, levelSubMenuStylesInternal[menuLevel].RegisteredCssClass);
			}
		}
		else
		{
			if (!dynamic && staticMenuStyleInternal != null)
			{
				style.CopyFrom(staticMenuStyleInternal);
			}
			if (dynamic && dynamicMenuStyleInternal != null)
			{
				style.CopyFrom(dynamicMenuStyleInternal);
			}
			if (levelSubMenuStylesInternal != null && levelSubMenuStylesInternal.Count > menuLevel)
			{
				style.CopyFrom(levelSubMenuStylesInternal[menuLevel]);
			}
		}
	}

	public void RegisterStyle(Style baseStyle, Style linkStyle, HtmlHead head)
	{
		RegisterStyle(baseStyle, linkStyle, null, head);
	}

	public void RegisterStyle(Style baseStyle, Style linkStyle, string className, HtmlHead head)
	{
		if (head != null)
		{
			linkStyle.CopyTextStylesFrom(baseStyle);
			linkStyle.BorderStyle = BorderStyle.None;
			RegisterStyle(linkStyle, className, head);
			RegisterStyle(baseStyle, className, head);
		}
	}

	public void RegisterStyle(Style baseStyle, HtmlHead head)
	{
		RegisterStyle(baseStyle, (string)null, head);
	}

	public void RegisterStyle(Style baseStyle, string className, HtmlHead head)
	{
		if (head != null)
		{
			if (string.IsNullOrEmpty(className))
			{
				className = IncrementStyleClassName();
			}
			baseStyle.SetRegisteredCssClass(className);
			head.StyleSheet.CreateStyleRule(baseStyle, Owner, "." + className);
		}
	}

	public void RenderSeparatorImage(Menu owner, HtmlTextWriter writer, string url, bool standardsCompliant)
	{
		if (!string.IsNullOrEmpty(url))
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Src, owner.ResolveClientUrl(url));
			if (standardsCompliant)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "separator");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}
	}

	public bool IsDynamicItem(MenuItem item)
	{
		return IsDynamicItem(Owner, item);
	}

	private string GetClientEvent(Menu owner, MenuItem item)
	{
		if (owner == null)
		{
			owner = Owner;
		}
		ClientScriptManager clientScriptManager = owner.Page?.ClientScript;
		if (clientScriptManager == null)
		{
			return string.Empty;
		}
		return clientScriptManager.GetPostBackClientHyperlink(owner, item.Path, registerForEventValidation: true);
	}

	private string IncrementStyleClassName()
	{
		registeredStylesCounter++;
		return Owner.ClientID + "_" + registeredStylesCounter;
	}
}
