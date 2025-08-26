using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;

namespace System.Web.UI.WebControls;

/// <summary>Displays a menu in an ASP.NET Web page.</summary>
[DefaultEvent("MenuItemClick")]
[ControlValueProperty("SelectedValue")]
[Designer("System.Web.UI.Design.WebControls.MenuDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[SupportsEventValidation]
public class Menu : HierarchicalDataBoundControl, IPostBackEventHandler, INamingContainer
{
	private class MenuTemplateWriter : TextWriter
	{
		private char[] _buffer;

		private int _ptr;

		public override Encoding Encoding => Encoding.Unicode;

		public MenuTemplateWriter(char[] buffer)
		{
			_buffer = buffer;
		}

		public override void Write(char value)
		{
			if (_ptr == _buffer.Length)
			{
				EnsureCapacity();
			}
			_buffer[_ptr++] = value;
		}

		public override void Write(string value)
		{
			if (value != null)
			{
				if (_ptr + value.Length >= _buffer.Length)
				{
					EnsureCapacity();
				}
				for (int i = 0; i < value.Length; i++)
				{
					_buffer[_ptr++] = value[i];
				}
			}
		}

		private void EnsureCapacity()
		{
			char[] array = new char[_buffer.Length * 2];
			Array.Copy(_buffer, array, _buffer.Length);
			_buffer = array;
		}
	}

	private class MenuRenderHtmlTemplate
	{
		public const string Marker = "\u093a\u093bॱ";

		private char[] _templateHtml;

		private MenuTemplateWriter _templateWriter;

		private ArrayList idxs = new ArrayList(32);

		public MenuRenderHtmlTemplate()
		{
			_templateHtml = new char[1024];
			_templateWriter = new MenuTemplateWriter(_templateHtml);
		}

		public static string GetMarker(int num)
		{
			char c = (char)(2417 + num);
			return "\u093a\u093bॱ" + c;
		}

		public HtmlTextWriter GetMenuTemplateWriter()
		{
			return new HtmlTextWriter(_templateWriter);
		}

		public void Parse()
		{
			int num = 0;
			for (int i = 0; i < _templateHtml.Length; i++)
			{
				if (_templateHtml[i] == '\0')
				{
					idxs.Add(i);
					break;
				}
				if (_templateHtml[i] != "\u093a\u093bॱ"[num])
				{
					num = 0;
					continue;
				}
				num++;
				if (num == "\u093a\u093bॱ".Length)
				{
					num = 0;
					idxs.Add(i - "\u093a\u093bॱ".Length + 1);
				}
			}
		}

		public void RenderTemplate(HtmlTextWriter writer, string[] dynamicParts, int start, int count)
		{
			if (idxs.Count != 0)
			{
				int num = 0;
				int num2 = ((start == 0) ? (-"\u093a\u093bॱ".Length - 1) : ((int)idxs[start - 1]));
				int num3 = 0;
				int i = start;
				for (int num4 = start + count; i < num4; i++)
				{
					num = num2 + "\u093a\u093bॱ".Length + 1;
					num2 = (int)idxs[i];
					writer.Write(_templateHtml, num, num2 - num);
					num3 = _templateHtml[num2 + "\u093a\u093bॱ".Length] - 2417;
					writer.Write(dynamicParts[num3]);
				}
				num = num2 + "\u093a\u093bॱ".Length + 1;
				num2 = (int)idxs[i];
				writer.Write(_templateHtml, num, num2 - num);
			}
		}
	}

	private IMenuRenderer renderer;

	private MenuItemStyle dynamicMenuItemStyle;

	private SubMenuStyle dynamicMenuStyle;

	private MenuItemStyle dynamicSelectedStyle;

	private MenuItemStyle staticMenuItemStyle;

	private SubMenuStyle staticMenuStyle;

	private MenuItemStyle staticSelectedStyle;

	private Style staticHoverStyle;

	private Style dynamicHoverStyle;

	private MenuItemStyleCollection levelMenuItemStyles;

	private MenuItemStyleCollection levelSelectedStyles;

	private SubMenuStyleCollection levelSubMenuStyles;

	private ITemplate staticItemTemplate;

	private ITemplate dynamicItemTemplate;

	private MenuItemCollection items;

	private MenuItemBindingCollection dataBindings;

	private MenuItem selectedItem;

	private string selectedItemPath;

	private Hashtable bindings;

	private Hashtable _menuItemControls;

	private bool _requiresChildControlsDataBinding;

	private SiteMapNode _currSiteMapNode;

	private Style popOutBoxStyle;

	private Style controlLinkStyle;

	private Style dynamicMenuItemLinkStyle;

	private Style staticMenuItemLinkStyle;

	private Style dynamicSelectedLinkStyle;

	private Style staticSelectedLinkStyle;

	private Style dynamicHoverLinkStyle;

	private Style staticHoverLinkStyle;

	private bool? renderList;

	private bool includeStyleBlock = true;

	private MenuRenderingMode renderingMode;

	private static readonly object MenuItemClickEvent;

	private static readonly object MenuItemDataBoundEvent;

	/// <summary>Contains the command name.</summary>
	public static readonly string MenuItemClickCommandName;

	private MenuRenderHtmlTemplate _dynamicTemplate;

	private IMenuRenderer Renderer
	{
		get
		{
			if (renderer == null)
			{
				renderer = CreateRenderer(null);
			}
			return renderer;
		}
	}

	private bool RenderList
	{
		get
		{
			if (!renderList.HasValue)
			{
				switch (RenderingMode)
				{
				case MenuRenderingMode.List:
					renderList = true;
					break;
				case MenuRenderingMode.Table:
					renderList = false;
					break;
				default:
					if (base.RenderingCompatibilityLessThan40)
					{
						renderList = false;
					}
					else
					{
						renderList = true;
					}
					break;
				}
			}
			return renderList.Value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether ASP.NET should render a block of cascading style sheet (CSS) definitions for the styles that are used in the menu.</summary>
	/// <returns>A value that indicates whether ASP.NET should render a block of CSS definitions for the styles that are used in the menu. The default value is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[Description("Determines whether or not to render the inline style block (only used in standards compliance mode)")]
	public bool IncludeStyleBlock
	{
		get
		{
			return includeStyleBlock;
		}
		set
		{
			includeStyleBlock = value;
		}
	}

	/// <summary>Gets or sets a value that specifies whether the <see cref="T:System.Web.UI.WebControls.Menu" /> control renders HTML <see langword="table" /> elements and inline styles, or <see langword="listitem" /> elements and cascading style sheet (CSS) styles.</summary>
	/// <returns>A value that specifies whether the <see cref="T:System.Web.UI.WebControls.Menu" /> control renders HTML <see langword="table" /> elements and inline styles, or <see langword="listitem" /> elements and cascading style sheet (CSS) styles. The default value is <see cref="F:System.Web.UI.WebControls.MenuRenderingMode.Default" />.</returns>
	[DefaultValue(MenuRenderingMode.Default)]
	public MenuRenderingMode RenderingMode
	{
		get
		{
			return renderingMode;
		}
		set
		{
			if (value < MenuRenderingMode.Default || value > MenuRenderingMode.List)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			renderingMode = value;
			renderer = CreateRenderer(renderer);
		}
	}

	/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> objects that define the relationship between a data item and the menu item it is binding to. </summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItemBindingCollection" /> that represents the relationship between a data item and the menu item it is binding to.</returns>
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Editor("System.Web.UI.Design.WebControls.MenuBindingsEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[MergableProperty(false)]
	public MenuItemBindingCollection DataBindings
	{
		get
		{
			if (dataBindings == null)
			{
				dataBindings = new MenuItemBindingCollection();
				if (base.IsTrackingViewState)
				{
					((IStateManager)dataBindings).TrackViewState();
				}
			}
			return dataBindings;
		}
	}

	/// <summary>Gets or sets the duration for which a dynamic menu is displayed after the mouse pointer is no longer positioned over the menu.</summary>
	/// <returns>The amount of time (in milliseconds) a dynamic menu is displayed after the mouse pointer is no longer positioned over the menu. The default is 500.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than -1.</exception>
	[DefaultValue(500)]
	[Themeable(false)]
	public int DisappearAfter
	{
		get
		{
			object obj = ViewState["DisappearAfter"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 500;
		}
		set
		{
			ViewState["DisappearAfter"] = value;
		}
	}

	/// <summary>Gets or sets the URL to an image to display at the bottom of each dynamic menu item to separate it from other menu items.</summary>
	/// <returns>The URL to a separator image displayed at the bottom of each dynamic menu item. The default value is an empty string (""), which indicates that this property is not set.</returns>
	[Themeable(true)]
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string DynamicBottomSeparatorImageUrl
	{
		get
		{
			object obj = ViewState["dbsiu"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			ViewState["dbsiu"] = value;
		}
	}

	/// <summary>Gets or sets additional text shown with all menu items that are dynamically displayed.</summary>
	/// <returns>The additional text or characters that appear with all menu items. The default value for this property is "{0}."</returns>
	[DefaultValue("")]
	public string DynamicItemFormatString
	{
		get
		{
			object obj = ViewState["DynamicItemFormatString"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["DynamicItemFormatString"] = value;
		}
	}

	/// <summary>Gets or sets the URL to an image to display at the top of each dynamic menu item to separate it from other menu items.</summary>
	/// <returns>The URL to a separator image displayed at the top of each dynamic menu item. The default value is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[WebCategory("Appearance")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string DynamicTopSeparatorImageUrl
	{
		get
		{
			object obj = ViewState["dtsiu"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["dtsiu"] = value;
		}
	}

	/// <summary>Gets or sets the URL to an image displayed as the separator at the bottom of each static menu item.</summary>
	/// <returns>The URL to an image displayed as the separator at the bottom of each static menu item. The default value is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[WebCategory("Appearance")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string StaticBottomSeparatorImageUrl
	{
		get
		{
			object obj = ViewState["sbsiu"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["sbsiu"] = value;
		}
	}

	/// <summary>Gets or sets the URL to an image displayed as the separator at the top of each static menu item.</summary>
	/// <returns>The URL to an image displayed as the separator at the top of each static menu item. The default value is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[WebCategory("Appearance")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string StaticTopSeparatorImageUrl
	{
		get
		{
			object obj = ViewState["stsiu"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["stsiu"] = value;
		}
	}

	/// <summary>Gets or sets the direction in which to render the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.Orientation" /> enumeration values. The default is <see langword="Orientation.Vertical" />.</returns>
	[DefaultValue(Orientation.Vertical)]
	public Orientation Orientation
	{
		get
		{
			object obj = ViewState["Orientation"];
			if (obj != null)
			{
				return (Orientation)obj;
			}
			return Orientation.Vertical;
		}
		set
		{
			ViewState["Orientation"] = value;
		}
	}

	/// <summary>Gets or sets the number of menu levels to display in a static menu.</summary>
	/// <returns>The number of menu levels to display in a static menu. The default is 1.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than 1.</exception>
	[DefaultValue(1)]
	[Themeable(true)]
	public int StaticDisplayLevels
	{
		get
		{
			object obj = ViewState["StaticDisplayLevels"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 1;
		}
		set
		{
			if (value < 1)
			{
				throw new ArgumentOutOfRangeException();
			}
			ViewState["StaticDisplayLevels"] = value;
		}
	}

	/// <summary>Gets or sets additional text shown with all menu items that are statically displayed.</summary>
	/// <returns>The additional text or characters that appear with all menu items. The default value for this property is "{0}."</returns>
	[DefaultValue("")]
	public string StaticItemFormatString
	{
		get
		{
			object obj = ViewState["StaticItemFormatString"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["StaticItemFormatString"] = value;
		}
	}

	/// <summary>Gets or sets the amount of space, in pixels, to indent submenus within a static menu.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the amount of space, in pixels, to indent submenus within a static menu. The default is 0.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the selected <see cref="T:System.Web.UI.WebControls.Unit" /> is less than 0.</exception>
	[DefaultValue(typeof(Unit), "16px")]
	[Themeable(true)]
	public Unit StaticSubMenuIndent
	{
		get
		{
			object obj = ViewState["StaticSubMenuIndent"];
			if (obj != null)
			{
				return (Unit)obj;
			}
			return Unit.Empty;
		}
		set
		{
			ViewState["StaticSubMenuIndent"] = value;
		}
	}

	/// <summary>Gets or sets the number of menu levels to render for a dynamic menu.</summary>
	/// <returns>The number of menu levels to render for a dynamic menu. The default is 3.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.Menu.MaximumDynamicDisplayLevels" /> property is set to a value less than 0.</exception>
	[Themeable(true)]
	[DefaultValue(3)]
	public int MaximumDynamicDisplayLevels
	{
		get
		{
			object obj = ViewState["MaximumDynamicDisplayLevels"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 3;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			ViewState["MaximumDynamicDisplayLevels"] = value;
		}
	}

	/// <summary>Gets or sets the number of pixels to shift a dynamic menu vertically relative to its parent menu item.</summary>
	/// <returns>The number of pixels to shift a dynamic menu vertically relative to its parent menu item. The default is 0.</returns>
	[DefaultValue(0)]
	public int DynamicVerticalOffset
	{
		get
		{
			object obj = ViewState["DynamicVerticalOffset"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}
		set
		{
			ViewState["DynamicVerticalOffset"] = value;
		}
	}

	/// <summary>Gets or sets the number of pixels to shift a dynamic menu horizontally relative to its parent menu item.</summary>
	/// <returns>The number of pixels to shift a dynamic menu horizontally relative to its parent menu item. The default is 0.</returns>
	[DefaultValue(0)]
	public int DynamicHorizontalOffset
	{
		get
		{
			object obj = ViewState["DynamicHorizontalOffset"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}
		set
		{
			ViewState["DynamicHorizontalOffset"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the built-in image that indicates that a dynamic menu item has a submenu is displayed.</summary>
	/// <returns>
	///     <see langword="true" /> to display the built-in image for dynamic menu items with submenus; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	public bool DynamicEnableDefaultPopOutImage
	{
		get
		{
			object obj = ViewState["dedpoi"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return true;
		}
		set
		{
			ViewState["dedpoi"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the built-in image is displayed to indicate that a static menu item has a submenu.</summary>
	/// <returns>
	///     <see langword="true" /> to display the built-in image for static menu items with submenus; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	public bool StaticEnableDefaultPopOutImage
	{
		get
		{
			object obj = ViewState["sedpoi"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return true;
		}
		set
		{
			ViewState["sedpoi"] = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object that contains all menu items in the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> that contains all menu items in the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</returns>
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Editor("System.Web.UI.Design.MenuItemCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[MergableProperty(false)]
	public MenuItemCollection Items
	{
		get
		{
			if (items == null)
			{
				items = new MenuItemCollection(this);
				if (base.IsTrackingViewState)
				{
					((IStateManager)items).TrackViewState();
				}
			}
			return items;
		}
	}

	/// <summary>Gets or sets the character used to delimit the path of a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	/// <returns>The character used to delimit the path of a menu item. The default value is a slash mark (/).</returns>
	[DefaultValue('/')]
	public char PathSeparator
	{
		get
		{
			object obj = ViewState["PathSeparator"];
			if (obj != null)
			{
				return (char)obj;
			}
			return '/';
		}
		set
		{
			ViewState["PathSeparator"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the text for menu items should wrap.</summary>
	/// <returns>
	///     <see langword="true" /> to wrap the menu item text; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	public bool ItemWrap
	{
		get
		{
			object obj = ViewState["ItemWrap"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return false;
		}
		set
		{
			ViewState["ItemWrap"] = value;
		}
	}

	internal Style PopOutBoxStyle
	{
		get
		{
			if (popOutBoxStyle == null)
			{
				popOutBoxStyle = new Style();
				popOutBoxStyle.BackColor = Color.White;
			}
			return popOutBoxStyle;
		}
	}

	internal Style ControlLinkStyle
	{
		get
		{
			if (controlLinkStyle == null)
			{
				controlLinkStyle = new Style();
				controlLinkStyle.AlwaysRenderTextDecoration = true;
			}
			return controlLinkStyle;
		}
	}

	internal Style DynamicMenuItemLinkStyle
	{
		get
		{
			if (dynamicMenuItemLinkStyle == null)
			{
				dynamicMenuItemLinkStyle = new Style();
			}
			return dynamicMenuItemLinkStyle;
		}
	}

	internal Style StaticMenuItemLinkStyle
	{
		get
		{
			if (staticMenuItemLinkStyle == null)
			{
				staticMenuItemLinkStyle = new Style();
			}
			return staticMenuItemLinkStyle;
		}
	}

	internal Style DynamicSelectedLinkStyle
	{
		get
		{
			if (dynamicSelectedLinkStyle == null)
			{
				dynamicSelectedLinkStyle = new Style();
			}
			return dynamicSelectedLinkStyle;
		}
	}

	internal Style StaticSelectedLinkStyle
	{
		get
		{
			if (staticSelectedLinkStyle == null)
			{
				staticSelectedLinkStyle = new Style();
			}
			return staticSelectedLinkStyle;
		}
	}

	internal Style DynamicHoverLinkStyle
	{
		get
		{
			if (dynamicHoverLinkStyle == null)
			{
				dynamicHoverLinkStyle = new Style();
			}
			return dynamicHoverLinkStyle;
		}
	}

	internal Style StaticHoverLinkStyle
	{
		get
		{
			if (staticHoverLinkStyle == null)
			{
				staticHoverLinkStyle = new Style();
			}
			return staticHoverLinkStyle;
		}
	}

	internal MenuItemStyle StaticMenuItemStyleInternal => staticMenuItemStyle;

	internal SubMenuStyle StaticMenuStyleInternal => staticMenuStyle;

	internal MenuItemStyle DynamicMenuItemStyleInternal => dynamicMenuItemStyle;

	internal SubMenuStyle DynamicMenuStyleInternal => dynamicMenuStyle;

	internal MenuItemStyleCollection LevelMenuItemStylesInternal => levelMenuItemStyles;

	internal List<Style> LevelMenuItemLinkStyles => null;

	internal SubMenuStyleCollection LevelSubMenuStylesInternal => levelSubMenuStyles;

	internal MenuItemStyle StaticSelectedStyleInternal => staticSelectedStyle;

	internal MenuItemStyle DynamicSelectedStyleInternal => dynamicSelectedStyle;

	internal MenuItemStyleCollection LevelSelectedStylesInternal => levelSelectedStyles;

	internal List<Style> LevelSelectedLinkStyles => null;

	internal Style StaticHoverStyleInternal => staticHoverStyle;

	internal Style DynamicHoverStyleInternal => dynamicHoverStyle;

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object that allows you to set the appearance of the menu items within a dynamic menu.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> that represents the style of the menu items within a dynamic menu.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public MenuItemStyle DynamicMenuItemStyle
	{
		get
		{
			if (dynamicMenuItemStyle == null)
			{
				dynamicMenuItemStyle = new MenuItemStyle();
				if (base.IsTrackingViewState)
				{
					dynamicMenuItemStyle.TrackViewState();
				}
			}
			return dynamicMenuItemStyle;
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object that allows you to set the appearance of the dynamic menu item selected by the user.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> that represents the style of the selected dynamic menu item.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public MenuItemStyle DynamicSelectedStyle
	{
		get
		{
			if (dynamicSelectedStyle == null)
			{
				dynamicSelectedStyle = new MenuItemStyle();
				if (base.IsTrackingViewState)
				{
					dynamicSelectedStyle.TrackViewState();
				}
			}
			return dynamicSelectedStyle;
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object that allows you to set the appearance of a dynamic menu.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> that represents the style of a dynamic menu.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public SubMenuStyle DynamicMenuStyle
	{
		get
		{
			if (dynamicMenuStyle == null)
			{
				dynamicMenuStyle = new SubMenuStyle();
				if (base.IsTrackingViewState)
				{
					dynamicMenuStyle.TrackViewState();
				}
			}
			return dynamicMenuStyle;
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object that allows you to set the appearance of the menu items in a static menu.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> that represents the style of the menu items in a static menu.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public MenuItemStyle StaticMenuItemStyle
	{
		get
		{
			if (staticMenuItemStyle == null)
			{
				staticMenuItemStyle = new MenuItemStyle();
				if (base.IsTrackingViewState)
				{
					staticMenuItemStyle.TrackViewState();
				}
			}
			return staticMenuItemStyle;
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object that allows you to set the appearance of the menu item selected by the user in a static menu.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> that represents the style of the selected menu item in a static menu.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public MenuItemStyle StaticSelectedStyle
	{
		get
		{
			if (staticSelectedStyle == null)
			{
				staticSelectedStyle = new MenuItemStyle();
				if (base.IsTrackingViewState)
				{
					staticSelectedStyle.TrackViewState();
				}
			}
			return staticSelectedStyle;
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object that allows you to set the appearance of a static menu.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> that represents the style of a static menu.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public SubMenuStyle StaticMenuStyle
	{
		get
		{
			if (staticMenuStyle == null)
			{
				staticMenuStyle = new SubMenuStyle();
				if (base.IsTrackingViewState)
				{
					staticMenuStyle.TrackViewState();
				}
			}
			return staticMenuStyle;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.MenuItemStyleCollection" /> object that contains the style settings that are applied to menu items based on their level in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItemStyleCollection" /> that contains the style settings that are applied to menu items based on their level in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</returns>
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Editor("System.Web.UI.Design.WebControls.MenuItemStyleCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public MenuItemStyleCollection LevelMenuItemStyles
	{
		get
		{
			if (levelMenuItemStyles == null)
			{
				levelMenuItemStyles = new MenuItemStyleCollection();
				if (base.IsTrackingViewState)
				{
					((IStateManager)levelMenuItemStyles).TrackViewState();
				}
			}
			return levelMenuItemStyles;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.MenuItemStyleCollection" /> object that contains the style settings that are applied to the selected menu item based on its level in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItemStyleCollection" /> that contains the style settings that are applied to the selected menu item based on its level in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</returns>
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Editor("System.Web.UI.Design.WebControls.MenuItemStyleCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public MenuItemStyleCollection LevelSelectedStyles
	{
		get
		{
			if (levelSelectedStyles == null)
			{
				levelSelectedStyles = new MenuItemStyleCollection();
				if (base.IsTrackingViewState)
				{
					((IStateManager)levelSelectedStyles).TrackViewState();
				}
			}
			return levelSelectedStyles;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.MenuItemStyleCollection" /> object that contains the style settings that are applied to the submenu items in the static menu based on their level in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItemStyleCollection" /> that contains the style settings that are applied to the submenu items in the static menu based on their level in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</returns>
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Editor("System.Web.UI.Design.WebControls.SubMenuStyleCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public SubMenuStyleCollection LevelSubMenuStyles
	{
		get
		{
			if (levelSubMenuStyles == null)
			{
				levelSubMenuStyles = new SubMenuStyleCollection();
				if (base.IsTrackingViewState)
				{
					((IStateManager)levelSubMenuStyles).TrackViewState();
				}
			}
			return levelSubMenuStyles;
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.Style" /> object that allows you to set the appearance of a dynamic menu item when the mouse pointer is positioned over it.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style of a dynamic menu item when the mouse pointer is positioned over it.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public Style DynamicHoverStyle
	{
		get
		{
			if (dynamicHoverStyle == null)
			{
				dynamicHoverStyle = new Style();
				if (base.IsTrackingViewState)
				{
					dynamicHoverStyle.TrackViewState();
				}
			}
			return dynamicHoverStyle;
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.Style" /> object that allows you to set the appearance of a static menu item when the mouse pointer is positioned over it.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style of a static menu item when the mouse pointer is positioned over it.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public Style StaticHoverStyle
	{
		get
		{
			if (staticHoverStyle == null)
			{
				staticHoverStyle = new Style();
				if (base.IsTrackingViewState)
				{
					staticHoverStyle.TrackViewState();
				}
			}
			return staticHoverStyle;
		}
	}

	/// <summary>Gets or sets the URL to an image displayed in a dynamic menu to indicate that the user can scroll down for additional menu items.</summary>
	/// <returns>The URL to an image displayed in a dynamic menu to indicate that the user can scroll down for additional menu items. The default value is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string ScrollDownImageUrl
	{
		get
		{
			object obj = ViewState["sdiu"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["sdiu"] = value;
		}
	}

	/// <summary>Gets or sets the URL to an image displayed in a dynamic menu to indicate that the user can scroll up for additional menu items.</summary>
	/// <returns>The URL to an image displayed in a dynamic menu to indicate that the user can scroll up for additional menu items. The default value is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string ScrollUpImageUrl
	{
		get
		{
			object obj = ViewState["suiu"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["suiu"] = value;
		}
	}

	/// <summary>Gets or sets the alternate text for the image specified in the <see cref="P:System.Web.UI.WebControls.Menu.ScrollDownImageUrl" /> property.</summary>
	/// <returns>The alternate text for the image specified in the <see cref="P:System.Web.UI.WebControls.Menu.ScrollDownImageUrl" /> property. The default is an empty string (""), which indicates that this property is not set.</returns>
	[Localizable(true)]
	public string ScrollDownText
	{
		get
		{
			object obj = ViewState["ScrollDownText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Scroll down");
		}
		set
		{
			ViewState["ScrollDownText"] = value;
		}
	}

	/// <summary>Gets or sets the alternate text for the image specified in the <see cref="P:System.Web.UI.WebControls.Menu.ScrollUpImageUrl" /> property.</summary>
	/// <returns>The alternate text for the image specified in the <see cref="P:System.Web.UI.WebControls.Menu.ScrollUpImageUrl" /> property. The default is an empty string (""), which indicates that this property is not set.</returns>
	[Localizable(true)]
	public string ScrollUpText
	{
		get
		{
			object obj = ViewState["ScrollUpText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Scroll up");
		}
		set
		{
			ViewState["ScrollUpText"] = value;
		}
	}

	/// <summary>Gets or sets the alternate text for the image used to indicate that a dynamic menu item has a submenu.</summary>
	/// <returns>The alternate text for the image used to indicate that a dynamic menu item has a submenu. The default is an empty string (""), which indicates that this property is not set.</returns>
	public string DynamicPopOutImageTextFormatString
	{
		get
		{
			object obj = ViewState["dpoitf"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Expand {0}");
		}
		set
		{
			ViewState["dpoitf"] = value;
		}
	}

	/// <summary>Gets or sets the URL to a custom image that is displayed in a dynamic menu item when the dynamic menu item has a submenu.</summary>
	/// <returns>The URL to an image used to indicate that a dynamic menu item has a submenu. The default value is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string DynamicPopOutImageUrl
	{
		get
		{
			object obj = ViewState["dpoiu"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["dpoiu"] = value;
		}
	}

	/// <summary>Gets or sets the alternate text for the pop-out image used to indicate that a static menu item has a submenu.</summary>
	/// <returns>The alternate text for the pop-out image. The default is an empty string (""), which indicates that this property is not set.</returns>
	public string StaticPopOutImageTextFormatString
	{
		get
		{
			object obj = ViewState["spoitf"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Expand {0}");
		}
		set
		{
			ViewState["spoitf"] = value;
		}
	}

	/// <summary>Gets or sets the URL to an image displayed to indicate that a static menu item has a submenu.</summary>
	/// <returns>The URL to an image displayed to indicate that a static menu item has a submenu. The default value is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string StaticPopOutImageUrl
	{
		get
		{
			object obj = ViewState["spoiu"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["spoiu"] = value;
		}
	}

	/// <summary>Gets or sets the target window or frame in which to display the Web page content associated with a menu item.</summary>
	/// <returns>The target window or frame in which to display the linked Web page content. The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
	[DefaultValue("")]
	public string Target
	{
		get
		{
			object obj = ViewState["Target"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["Target"] = value;
		}
	}

	/// <summary>Gets or sets the template that contains the custom content to render for a static menu.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for a static menu. The default value is null, which indicates that this property is not set.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(MenuItemTemplateContainer), BindingDirection.OneWay)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public ITemplate StaticItemTemplate
	{
		get
		{
			return staticItemTemplate;
		}
		set
		{
			staticItemTemplate = value;
		}
	}

	/// <summary>Gets or sets the template that contains the custom content to render for a dynamic menu.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for a dynamic menu. The default value is null, which indicates that this property is not set.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(MenuItemTemplateContainer), BindingDirection.OneWay)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public ITemplate DynamicItemTemplate
	{
		get
		{
			return dynamicItemTemplate;
		}
		set
		{
			dynamicItemTemplate = value;
		}
	}

	/// <summary>Gets the selected menu item.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItem" /> that represents the selected menu item.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public MenuItem SelectedItem
	{
		get
		{
			if (selectedItem == null && selectedItemPath != null)
			{
				selectedItem = FindItemByPos(selectedItemPath);
			}
			return selectedItem;
		}
	}

	/// <summary>Gets the value of the selected menu item.</summary>
	/// <returns>The value of the selected menu item. The default is <see cref="F:System.String.Empty" />, which indicates that no menu item is currently selected.</returns>
	[Browsable(false)]
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string SelectedValue
	{
		get
		{
			if (selectedItem == null)
			{
				return "";
			}
			return selectedItem.Value;
		}
	}

	/// <summary>Gets or sets the alternate text for a hidden image read by screen readers to provide the ability to skip the list of links.</summary>
	/// <returns>The alternate text of a hidden image read by screen readers to provide the ability to skip the list of links. The default is an empty string (""), which indicates that this property is not set.</returns>
	[Localizable(true)]
	public string SkipLinkText
	{
		get
		{
			object obj = ViewState["SkipLinkText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "Skip Navigation Links";
		}
		set
		{
			ViewState["SkipLinkText"] = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to a <see cref="T:System.Web.UI.WebControls.Menu" /> control. This property is used primarily by control developers.</summary>
	/// <returns>Always returns <see langword="HtmlTextWriterTag.Table" />.</returns>
	protected override HtmlTextWriterTag TagKey => Renderer.Tag;

	/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> that contains the child controls of the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains the child controls</returns>
	public override ControlCollection Controls => base.Controls;

	/// <summary>Occurs when a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control is clicked.</summary>
	public event MenuEventHandler MenuItemClick
	{
		add
		{
			base.Events.AddHandler(MenuItemClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MenuItemClickEvent, value);
		}
	}

	/// <summary>Occurs when a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control is bound to data.</summary>
	public event MenuEventHandler MenuItemDataBound
	{
		add
		{
			base.Events.AddHandler(MenuItemDataBoundEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MenuItemDataBoundEvent, value);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Menu.MenuItemClick" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.MenuEventArgs" /> that contains the event data.</param>
	protected virtual void OnMenuItemClick(MenuEventArgs e)
	{
		if (base.Events != null)
		{
			((MenuEventHandler)base.Events[MenuItemClick])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Menu.MenuItemDataBound" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.MenuEventArgs" /> that contains the event data.</param>
	protected virtual void OnMenuItemDataBound(MenuEventArgs e)
	{
		if (base.Events != null)
		{
			((MenuEventHandler)base.Events[MenuItemDataBound])?.Invoke(this, e);
		}
	}

	private IMenuRenderer CreateRenderer(IMenuRenderer current)
	{
		Type type = null;
		switch (RenderingMode)
		{
		case MenuRenderingMode.Default:
			type = ((!base.RenderingCompatibilityLessThan40) ? typeof(MenuListRenderer) : typeof(MenuTableRenderer));
			break;
		case MenuRenderingMode.Table:
			type = typeof(MenuTableRenderer);
			break;
		case MenuRenderingMode.List:
			type = typeof(MenuListRenderer);
			break;
		}
		if (type == null)
		{
			return null;
		}
		if (current == null || current.GetType() != type)
		{
			return Activator.CreateInstance(type, this) as IMenuRenderer;
		}
		return current;
	}

	internal void SetSelectedItem(MenuItem item)
	{
		if (selectedItem != item)
		{
			selectedItem = item;
			selectedItemPath = item.Path;
		}
	}

	/// <summary>Retrieves the menu item at the specified value path.</summary>
	/// <param name="valuePath">The value path to the menu item to retrieve.</param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItem" /> that represents the menu item at the specified value path.</returns>
	public MenuItem FindItem(string valuePath)
	{
		if (valuePath == null)
		{
			throw new ArgumentNullException("valuePath");
		}
		string[] array = valuePath.Split(PathSeparator);
		int num = 0;
		MenuItemCollection childItems = Items;
		bool flag = true;
		while (childItems.Count > 0 && flag)
		{
			flag = false;
			foreach (MenuItem item in childItems)
			{
				if (item.Value == array[num])
				{
					if (++num == array.Length)
					{
						return item;
					}
					childItems = item.ChildItems;
					flag = true;
					break;
				}
			}
		}
		return null;
	}

	private string GetBindingKey(string dataMember, int depth)
	{
		return dataMember + " " + depth;
	}

	internal MenuItemBinding FindBindingForItem(string type, int depth)
	{
		if (bindings == null)
		{
			return null;
		}
		MenuItemBinding menuItemBinding = (MenuItemBinding)bindings[GetBindingKey(type, depth)];
		if (menuItemBinding != null)
		{
			return menuItemBinding;
		}
		menuItemBinding = (MenuItemBinding)bindings[GetBindingKey(type, -1)];
		if (menuItemBinding != null)
		{
			return menuItemBinding;
		}
		menuItemBinding = (MenuItemBinding)bindings[GetBindingKey("", depth)];
		if (menuItemBinding != null)
		{
			return menuItemBinding;
		}
		return (MenuItemBinding)bindings[GetBindingKey("", -1)];
	}

	/// <summary>Binds the items from the data source to the menu items in the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	protected internal override void PerformDataBinding()
	{
		base.PerformDataBinding();
		if (!base.IsBoundUsingDataSourceID && DataSource == null)
		{
			EnsureChildControlsDataBound();
			return;
		}
		InitializeDataBindings();
		HierarchicalDataSourceView obj = GetData("") ?? throw new InvalidOperationException("No view returned by data source control.");
		Items.Clear();
		IHierarchicalEnumerable hEnumerable = obj.Select();
		FillBoundChildrenRecursive(hEnumerable, Items);
		CreateChildControlsForItems();
		base.ChildControlsCreated = true;
		EnsureChildControlsDataBound();
	}

	private void FillBoundChildrenRecursive(IHierarchicalEnumerable hEnumerable, MenuItemCollection itemCollection)
	{
		if (hEnumerable == null)
		{
			return;
		}
		foreach (object item in hEnumerable)
		{
			IHierarchyData hierarchyData = hEnumerable.GetHierarchyData(item);
			MenuItem menuItem = new MenuItem();
			itemCollection.Add(menuItem);
			menuItem.Bind(hierarchyData);
			if (hierarchyData is SiteMapNode siteMapNode)
			{
				if (_currSiteMapNode == null)
				{
					_currSiteMapNode = siteMapNode.Provider.CurrentNode;
				}
				if (siteMapNode == _currSiteMapNode)
				{
					menuItem.Selected = true;
				}
			}
			OnMenuItemDataBound(new MenuEventArgs(menuItem));
			if (hierarchyData != null && hierarchyData.HasChildren)
			{
				IHierarchicalEnumerable children = hierarchyData.GetChildren();
				FillBoundChildrenRecursive(children, menuItem.ChildItems);
			}
		}
	}

	/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.MenuItem.DataBound" /> property of the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object with the specified value.</summary>
	/// <param name="node">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> to set.</param>
	/// <param name="dataBound">
	///       <see langword="true" /> to set the node as data-bound; otherwise, <see langword="false" />.</param>
	protected void SetItemDataBound(MenuItem node, bool dataBound)
	{
		node.SetDataBound(dataBound);
	}

	/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.MenuItem.DataPath" /> property of the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object with the specified value.</summary>
	/// <param name="node">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> to set.</param>
	/// <param name="dataPath">The data path for the <see cref="T:System.Web.UI.WebControls.MenuItem" />.</param>
	protected void SetItemDataPath(MenuItem node, string dataPath)
	{
		node.SetDataPath(dataPath);
	}

	/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.MenuItem.DataItem" /> property of the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object with the specified value.</summary>
	/// <param name="node">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> to set.</param>
	/// <param name="dataItem">The data item for the <see cref="T:System.Web.UI.WebControls.MenuItem" />.</param>
	protected void SetItemDataItem(MenuItem node, object dataItem)
	{
		node.SetDataItem(dataItem);
	}

	/// <summary>Processes an event raised when a form is posted to the server.</summary>
	/// <param name="eventArgument">A <see cref="T:System.String" /> that represents the event argument passed to the event handler.</param>
	protected internal virtual void RaisePostBackEvent(string eventArgument)
	{
		ValidateEvent(UniqueID, eventArgument);
		if (base.IsEnabled)
		{
			EnsureChildControls();
			MenuItem menuItem = FindItemByPos(eventArgument);
			if (menuItem != null)
			{
				menuItem.Selected = true;
				OnMenuItemClick(new MenuEventArgs(menuItem));
			}
		}
	}

	/// <summary>Processes an event raised when a form is posted to the server.</summary>
	/// <param name="eventArgument">A <see cref="T:System.String" /> that represents the event argument passed to the event handler.</param>
	void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
	{
		RaisePostBackEvent(eventArgument);
	}

	private MenuItem FindItemByPos(string path)
	{
		string[] array = path.Split('_');
		MenuItem menuItem = null;
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			int num = int.Parse(array2[i]);
			if (menuItem == null)
			{
				if (num >= Items.Count)
				{
					return null;
				}
				menuItem = Items[num];
			}
			else
			{
				if (num >= menuItem.ChildItems.Count)
				{
					return null;
				}
				menuItem = menuItem.ChildItems[num];
			}
		}
		return menuItem;
	}

	/// <summary>Tracks view-state changes to the <see cref="T:System.Web.UI.WebControls.Menu" /> control so they can be stored in the control's <see cref="T:System.Web.UI.StateBag" /> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
	protected override void TrackViewState()
	{
		EnsureDataBound();
		base.TrackViewState();
		if (dataBindings != null)
		{
			((IStateManager)dataBindings).TrackViewState();
		}
		if (items != null)
		{
			((IStateManager)items).TrackViewState();
		}
		if (dynamicMenuItemStyle != null)
		{
			dynamicMenuItemStyle.TrackViewState();
		}
		if (dynamicMenuStyle != null)
		{
			dynamicMenuStyle.TrackViewState();
		}
		if (levelMenuItemStyles != null && levelMenuItemStyles.Count > 0)
		{
			((IStateManager)levelMenuItemStyles).TrackViewState();
		}
		if (levelSelectedStyles != null && levelMenuItemStyles.Count > 0)
		{
			((IStateManager)levelSelectedStyles).TrackViewState();
		}
		if (levelSubMenuStyles != null && levelSubMenuStyles.Count > 0)
		{
			((IStateManager)levelSubMenuStyles).TrackViewState();
		}
		if (dynamicSelectedStyle != null)
		{
			dynamicSelectedStyle.TrackViewState();
		}
		if (staticMenuItemStyle != null)
		{
			staticMenuItemStyle.TrackViewState();
		}
		if (staticMenuStyle != null)
		{
			staticMenuStyle.TrackViewState();
		}
		if (staticSelectedStyle != null)
		{
			staticSelectedStyle.TrackViewState();
		}
		if (staticHoverStyle != null)
		{
			staticHoverStyle.TrackViewState();
		}
		if (dynamicHoverStyle != null)
		{
			dynamicHoverStyle.TrackViewState();
		}
	}

	/// <summary>Saves the state of the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains the state of the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</returns>
	protected override object SaveViewState()
	{
		object[] array = new object[14]
		{
			base.SaveViewState(),
			(dataBindings == null) ? null : ((IStateManager)dataBindings).SaveViewState(),
			(items == null) ? null : ((IStateManager)items).SaveViewState(),
			(dynamicMenuItemStyle == null) ? null : dynamicMenuItemStyle.SaveViewState(),
			(dynamicMenuStyle == null) ? null : dynamicMenuStyle.SaveViewState(),
			(levelMenuItemStyles == null) ? null : ((IStateManager)levelMenuItemStyles).SaveViewState(),
			(levelSelectedStyles == null) ? null : ((IStateManager)levelSelectedStyles).SaveViewState(),
			(dynamicSelectedStyle == null) ? null : dynamicSelectedStyle.SaveViewState(),
			(staticMenuItemStyle == null) ? null : staticMenuItemStyle.SaveViewState(),
			(staticMenuStyle == null) ? null : staticMenuStyle.SaveViewState(),
			(staticSelectedStyle == null) ? null : staticSelectedStyle.SaveViewState(),
			(staticHoverStyle == null) ? null : staticHoverStyle.SaveViewState(),
			(dynamicHoverStyle == null) ? null : dynamicHoverStyle.SaveViewState(),
			(levelSubMenuStyles == null) ? null : ((IStateManager)levelSubMenuStyles).SaveViewState()
		};
		for (int num = array.Length - 1; num >= 0; num--)
		{
			if (array[num] != null)
			{
				return array;
			}
		}
		return null;
	}

	/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view-state values for the control.</param>
	protected override void LoadViewState(object state)
	{
		if (state != null)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)DataBindings).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)Items).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				DynamicMenuItemStyle.LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				DynamicMenuStyle.LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)LevelMenuItemStyles).LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				((IStateManager)LevelSelectedStyles).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				DynamicSelectedStyle.LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				StaticMenuItemStyle.LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				StaticMenuStyle.LoadViewState(array[9]);
			}
			if (array[10] != null)
			{
				StaticSelectedStyle.LoadViewState(array[10]);
			}
			if (array[11] != null)
			{
				StaticHoverStyle.LoadViewState(array[11]);
			}
			if (array[12] != null)
			{
				DynamicHoverStyle.LoadViewState(array[12]);
			}
			if (array[13] != null)
			{
				((IStateManager)LevelSubMenuStyles).LoadViewState(array[13]);
			}
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.MenuEventArgs" /> that contains the event data.</param>
	protected internal override void OnInit(EventArgs e)
	{
		Page.RegisterRequiresControlState(this);
		base.OnInit(e);
	}

	/// <summary>Loads the state of the properties in the <see cref="T:System.Web.UI.WebControls.Menu" /> control that need to be persisted.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored.</param>
	protected internal override void LoadControlState(object savedState)
	{
		if (savedState != null)
		{
			object[] array = (object[])savedState;
			base.LoadControlState(array[0]);
			selectedItemPath = array[1] as string;
		}
	}

	/// <summary>Saves the state of the properties in the <see cref="T:System.Web.UI.WebControls.Menu" /> control that need to be persisted.</summary>
	/// <returns>An object that contains the state data for the control. If there have been no changes to the state, this method returns <see langword="null" />.</returns>
	protected internal override object SaveControlState()
	{
		object obj = base.SaveControlState();
		object obj2 = selectedItemPath;
		if (obj != null || obj2 != null)
		{
			return new object[2] { obj, obj2 };
		}
		return null;
	}

	/// <summary>Creates the child controls of a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	protected internal override void CreateChildControls()
	{
		if (!base.IsBoundUsingDataSourceID && DataSource == null)
		{
			CreateChildControlsForItems();
		}
		else
		{
			EnsureDataBound();
		}
	}

	private void CreateChildControlsForItems()
	{
		Controls.Clear();
		if (base.HasChildViewState)
		{
			ClearChildViewState();
		}
		_menuItemControls = new Hashtable();
		CreateChildControlsForItems(Items);
		_requiresChildControlsDataBinding = true;
	}

	private void CreateChildControlsForItems(MenuItemCollection items)
	{
		IMenuRenderer menuRenderer = Renderer;
		foreach (MenuItem item in items)
		{
			bool flag = menuRenderer.IsDynamicItem(this, item);
			if (flag && dynamicItemTemplate != null)
			{
				MenuItemTemplateContainer menuItemTemplateContainer = new MenuItemTemplateContainer(item.Index, item);
				dynamicItemTemplate.InstantiateIn(menuItemTemplateContainer);
				_menuItemControls[item] = menuItemTemplateContainer;
				Controls.Add(menuItemTemplateContainer);
			}
			else if (!flag && staticItemTemplate != null)
			{
				MenuItemTemplateContainer menuItemTemplateContainer2 = new MenuItemTemplateContainer(item.Index, item);
				staticItemTemplate.InstantiateIn(menuItemTemplateContainer2);
				_menuItemControls[item] = menuItemTemplateContainer2;
				Controls.Add(menuItemTemplateContainer2);
			}
			if (item.HasChildData)
			{
				CreateChildControlsForItems(item.ChildItems);
			}
		}
	}

	/// <summary>Verifies that the menu control requires data binding and that a valid data source control is specified before calling the <see cref="M:System.Web.UI.WebControls.Menu.DataBind" /> method.</summary>
	protected override void EnsureDataBound()
	{
		base.EnsureDataBound();
		EnsureChildControlsDataBound();
	}

	private void EnsureChildControlsDataBound()
	{
		if (_requiresChildControlsDataBinding)
		{
			DataBindChildren();
			_requiresChildControlsDataBinding = false;
		}
	}

	/// <summary>Retrieves the design-time state of the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing the design-time state of the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</returns>
	[MonoTODO("Not implemented")]
	protected override IDictionary GetDesignModeState()
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets design-time data for the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> that contains state data for displaying the control.</param>
	[MonoTODO("Not implemented")]
	protected override void SetDesignModeState(IDictionary data)
	{
		throw new NotImplementedException();
	}

	/// <summary>Binds the data source to the <see cref="T:System.Web.UI.WebControls.Menu" /> control. This method cannot be inherited.</summary>
	public sealed override void DataBind()
	{
		base.DataBind();
	}

	/// <summary>Determines whether the event for the <see cref="T:System.Web.UI.WebControls.Menu" /> control is passed up the page's user interface (UI) server control hierarchy.</summary>
	/// <param name="source">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
	/// <returns>
	///     <see langword="true" /> if the event has been canceled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	protected override bool OnBubbleEvent(object source, EventArgs e)
	{
		if (!(e is CommandEventArgs))
		{
			return false;
		}
		if (e is MenuEventArgs menuEventArgs && string.Equals(menuEventArgs.CommandName, MenuItemClickCommandName))
		{
			OnMenuItemClick(menuEventArgs);
		}
		return true;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.MenuEventArgs" /> that contains the event data.</param>
	protected override void OnDataBinding(EventArgs e)
	{
		EnsureChildControls();
		base.OnDataBinding(e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
		string cmenu = ClientID + "_data";
		StringBuilder stringBuilder = new StringBuilder();
		Page page = Page;
		HtmlHead head;
		ClientScriptManager clientScriptManager;
		if (page != null)
		{
			head = page.Header;
			clientScriptManager = page.ClientScript;
		}
		else
		{
			head = null;
			clientScriptManager = null;
		}
		Renderer.PreRender(page, head, clientScriptManager, cmenu, stringBuilder);
		if (clientScriptManager != null)
		{
			clientScriptManager.RegisterWebFormClientScript();
			clientScriptManager.RegisterStartupScript(typeof(Menu), ClientID, stringBuilder.ToString(), addScriptTags: true);
		}
	}

	private void InitializeDataBindings()
	{
		if (dataBindings != null && dataBindings.Count > 0)
		{
			bindings = new Hashtable();
			{
				foreach (MenuItemBinding dataBinding in dataBindings)
				{
					string bindingKey = GetBindingKey(dataBinding.DataMember, dataBinding.Depth);
					bindings[bindingKey] = dataBinding;
				}
				return;
			}
		}
		bindings = null;
	}

	/// <summary>Renders the menu control on the client browser.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream used to write content to a Web page.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		if (Items.Count > 0)
		{
			base.Render(writer);
		}
	}

	/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The output stream that renders HTML contents to the client.</param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		Renderer.AddAttributesToRender(writer);
		base.AddAttributesToRender(writer);
	}

	/// <summary>Adds tag attributes and writes the markup for the opening tag of the control to the output stream emitted to the browser or device.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to build and render the device-specific output.</param>
	public override void RenderBeginTag(HtmlTextWriter writer)
	{
		string skipLinkText = SkipLinkText;
		if (!string.IsNullOrEmpty(skipLinkText))
		{
			Renderer.RenderBeginTag(writer, skipLinkText);
		}
		base.RenderBeginTag(writer);
	}

	/// <summary>Performs final markup and writes the HTML closing tag of the control to the output stream emitted to the browser or device.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to build and render the device-specific output.</param>
	public override void RenderEndTag(HtmlTextWriter writer)
	{
		base.RenderEndTag(writer);
		Renderer.RenderEndTag(writer);
		if (!string.IsNullOrEmpty(SkipLinkText))
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_SkipLink");
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.RenderEndTag();
		}
	}

	/// <summary>This member overrides <see cref="M:System.Web.UI.WebControls.WebControl.RenderContents(System.Web.UI.HtmlTextWriter)" />.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to build and render the device-specific output.</param>
	protected internal override void RenderContents(HtmlTextWriter writer)
	{
		Renderer.RenderContents(writer);
	}

	internal void RenderDynamicMenu(HtmlTextWriter writer, MenuItemCollection items)
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (DisplayChildren(items[i]))
			{
				RenderDynamicMenu(writer, items[i]);
				RenderDynamicMenu(writer, items[i].ChildItems);
			}
		}
	}

	private MenuRenderHtmlTemplate GetDynamicMenuTemplate(MenuItem item)
	{
		if (_dynamicTemplate != null)
		{
			return _dynamicTemplate;
		}
		_dynamicTemplate = new MenuRenderHtmlTemplate();
		HtmlTextWriter menuTemplateWriter = _dynamicTemplate.GetMenuTemplateWriter();
		if (Page.Header != null)
		{
			menuTemplateWriter.AddAttribute(HtmlTextWriterAttribute.Class, MenuRenderHtmlTemplate.GetMarker(0));
		}
		else
		{
			menuTemplateWriter.AddAttribute(HtmlTextWriterAttribute.Style, MenuRenderHtmlTemplate.GetMarker(0));
		}
		menuTemplateWriter.AddStyleAttribute("visibility", "hidden");
		menuTemplateWriter.AddStyleAttribute("position", "absolute");
		menuTemplateWriter.AddStyleAttribute("z-index", "1");
		menuTemplateWriter.AddStyleAttribute("left", "0px");
		menuTemplateWriter.AddStyleAttribute("top", "0px");
		menuTemplateWriter.AddAttribute("id", MenuRenderHtmlTemplate.GetMarker(1));
		menuTemplateWriter.RenderBeginTag(HtmlTextWriterTag.Div);
		menuTemplateWriter.AddAttribute("id", MenuRenderHtmlTemplate.GetMarker(2));
		menuTemplateWriter.AddStyleAttribute("display", "block");
		menuTemplateWriter.AddStyleAttribute("text-align", "center");
		menuTemplateWriter.AddAttribute("onmouseover", "Menu_OverScrollBtn ('" + ClientID + "','" + MenuRenderHtmlTemplate.GetMarker(3) + "','u')");
		menuTemplateWriter.AddAttribute("onmouseout", "Menu_OutScrollBtn ('" + ClientID + "','" + MenuRenderHtmlTemplate.GetMarker(4) + "','u')");
		menuTemplateWriter.RenderBeginTag(HtmlTextWriterTag.Div);
		menuTemplateWriter.AddAttribute("src", MenuRenderHtmlTemplate.GetMarker(5));
		menuTemplateWriter.AddAttribute("alt", MenuRenderHtmlTemplate.GetMarker(6));
		menuTemplateWriter.RenderBeginTag(HtmlTextWriterTag.Img);
		menuTemplateWriter.RenderEndTag();
		menuTemplateWriter.RenderEndTag();
		menuTemplateWriter.AddAttribute("id", MenuRenderHtmlTemplate.GetMarker(7));
		menuTemplateWriter.RenderBeginTag(HtmlTextWriterTag.Div);
		menuTemplateWriter.AddAttribute("id", MenuRenderHtmlTemplate.GetMarker(8));
		menuTemplateWriter.RenderBeginTag(HtmlTextWriterTag.Div);
		menuTemplateWriter.Write(MenuRenderHtmlTemplate.GetMarker(9));
		menuTemplateWriter.RenderEndTag();
		menuTemplateWriter.RenderEndTag();
		menuTemplateWriter.AddAttribute("id", MenuRenderHtmlTemplate.GetMarker(0));
		menuTemplateWriter.AddStyleAttribute("display", "block");
		menuTemplateWriter.AddStyleAttribute("text-align", "center");
		menuTemplateWriter.AddAttribute("onmouseover", "Menu_OverScrollBtn ('" + ClientID + "','" + MenuRenderHtmlTemplate.GetMarker(1) + "','d')");
		menuTemplateWriter.AddAttribute("onmouseout", "Menu_OutScrollBtn ('" + ClientID + "','" + MenuRenderHtmlTemplate.GetMarker(2) + "','d')");
		menuTemplateWriter.RenderBeginTag(HtmlTextWriterTag.Div);
		menuTemplateWriter.AddAttribute("src", MenuRenderHtmlTemplate.GetMarker(3));
		menuTemplateWriter.AddAttribute("alt", MenuRenderHtmlTemplate.GetMarker(4));
		menuTemplateWriter.RenderBeginTag(HtmlTextWriterTag.Img);
		menuTemplateWriter.RenderEndTag();
		menuTemplateWriter.RenderEndTag();
		menuTemplateWriter.RenderEndTag();
		_dynamicTemplate.Parse();
		return _dynamicTemplate;
	}

	private void RenderDynamicMenu(HtmlTextWriter writer, MenuItem item)
	{
		_dynamicTemplate = GetDynamicMenuTemplate(item);
		string text = ClientID + "_" + item.Path;
		string[] array = new string[9]
		{
			GetCssMenuStyle(dynamic: true, item.Depth + 1),
			text + "s",
			text + "cu",
			item.Path,
			item.Path,
			(ScrollUpImageUrl != "") ? ScrollUpImageUrl : Page.ClientScript.GetWebResourceUrl(typeof(Menu), "arrow_up.gif"),
			ScrollUpText,
			text + "cb",
			text + "cc"
		};
		_dynamicTemplate.RenderTemplate(writer, array, 0, array.Length);
		RenderMenu(writer, item.ChildItems, vertical: true, dynamic: true, item.Depth + 1, notLast: false);
		string[] array2 = new string[5]
		{
			text + "cd",
			item.Path,
			item.Path,
			(ScrollDownImageUrl != "") ? ScrollDownImageUrl : Page.ClientScript.GetWebResourceUrl(typeof(Menu), "arrow_down.gif"),
			ScrollDownText
		};
		_dynamicTemplate.RenderTemplate(writer, array2, array.Length + 1, array2.Length);
	}

	private string GetCssMenuStyle(bool dynamic, int menuLevel)
	{
		if (Page.Header != null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!dynamic && staticMenuStyle != null)
			{
				stringBuilder.Append(staticMenuStyle.CssClass);
				stringBuilder.Append(' ');
				stringBuilder.Append(staticMenuStyle.RegisteredCssClass);
			}
			if (dynamic && dynamicMenuStyle != null)
			{
				stringBuilder.Append(PopOutBoxStyle.RegisteredCssClass);
				stringBuilder.Append(' ');
				stringBuilder.Append(dynamicMenuStyle.CssClass);
				stringBuilder.Append(' ');
				stringBuilder.Append(dynamicMenuStyle.RegisteredCssClass);
			}
			if (levelSubMenuStyles != null && levelSubMenuStyles.Count > menuLevel)
			{
				stringBuilder.Append(levelSubMenuStyles[menuLevel].CssClass);
				stringBuilder.Append(' ');
				stringBuilder.Append(levelSubMenuStyles[menuLevel].RegisteredCssClass);
			}
			return stringBuilder.ToString();
		}
		SubMenuStyle subMenuStyle = new SubMenuStyle();
		if (!dynamic && staticMenuStyle != null)
		{
			subMenuStyle.CopyFrom(staticMenuStyle);
		}
		if (dynamic && dynamicMenuStyle != null)
		{
			subMenuStyle.CopyFrom(PopOutBoxStyle);
			subMenuStyle.CopyFrom(dynamicMenuStyle);
		}
		if (levelSubMenuStyles != null && levelSubMenuStyles.Count > menuLevel)
		{
			subMenuStyle.CopyFrom(levelSubMenuStyles[menuLevel]);
		}
		return subMenuStyle.GetStyleAttributes(null).Value;
	}

	internal void RenderMenu(HtmlTextWriter writer, MenuItemCollection items, bool vertical, bool dynamic, int menuLevel, bool notLast)
	{
		IMenuRenderer menuRenderer = Renderer;
		menuRenderer.RenderMenuBeginTag(writer, dynamic, menuLevel);
		menuRenderer.RenderMenuBody(writer, items, vertical, dynamic, notLast);
		menuRenderer.RenderMenuEndTag(writer, dynamic, menuLevel);
	}

	internal bool DisplayChildren(MenuItem item)
	{
		if (item.Depth + 1 < StaticDisplayLevels + MaximumDynamicDisplayLevels)
		{
			return item.ChildItems.Count > 0;
		}
		return false;
	}

	internal void RenderItem(HtmlTextWriter writer, MenuItem item, int position)
	{
		bool notLast = false;
		MenuItem menuItem = item;
		MenuItem parent;
		while ((parent = menuItem.Parent) != null)
		{
			if (menuItem.Index != parent.ChildItems.Count - 1)
			{
				notLast = true;
				break;
			}
			menuItem = parent;
		}
		Renderer.RenderMenuItem(writer, item, notLast, position == 0);
	}

	internal void RenderItemContent(HtmlTextWriter writer, MenuItem item, bool isDynamicItem)
	{
		if (_menuItemControls != null && _menuItemControls[item] != null)
		{
			((Control)_menuItemControls[item]).Render(writer);
		}
		Renderer.RenderItemContent(writer, item, isDynamicItem);
	}

	internal Unit GetItemSpacing(MenuItem item, bool dynamic)
	{
		Unit unit = Unit.Empty;
		if (item.Selected)
		{
			if (levelSelectedStyles != null && item.Depth < levelSelectedStyles.Count)
			{
				unit = levelSelectedStyles[item.Depth].ItemSpacing;
				if (unit != Unit.Empty)
				{
					return unit;
				}
			}
			if (dynamic && dynamicSelectedStyle != null)
			{
				unit = dynamicSelectedStyle.ItemSpacing;
			}
			else if (!dynamic && staticSelectedStyle != null)
			{
				unit = staticSelectedStyle.ItemSpacing;
			}
			if (unit != Unit.Empty)
			{
				return unit;
			}
		}
		if (levelMenuItemStyles != null && item.Depth < levelMenuItemStyles.Count)
		{
			unit = levelMenuItemStyles[item.Depth].ItemSpacing;
			if (unit != Unit.Empty)
			{
				return unit;
			}
		}
		if (dynamic && dynamicMenuItemStyle != null)
		{
			return dynamicMenuItemStyle.ItemSpacing;
		}
		if (!dynamic && staticMenuItemStyle != null)
		{
			return staticMenuItemStyle.ItemSpacing;
		}
		return Unit.Empty;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Menu" /> class.</summary>
	public Menu()
	{
	}

	static Menu()
	{
		MenuItemClick = new object();
		MenuItemDataBound = new object();
		MenuItemClickCommandName = "Click";
	}
}
