using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.Util;

namespace System.Web.UI.WebControls;

/// <summary>Displays hierarchical data, such as a table of contents, in a tree structure.</summary>
[SupportsEventValidation]
[ControlValueProperty("SelectedValue")]
[DefaultEvent("SelectedNodeChanged")]
[Designer("System.Web.UI.Design.WebControls.TreeViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class TreeView : HierarchicalDataBoundControl, IPostBackEventHandler, IPostBackDataHandler, ICallbackEventHandler
{
	private class TreeViewExpandDepthConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string) || sourceType == typeof(int))
			{
				return true;
			}
			return base.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(string) || destinationType == typeof(int))
			{
				return true;
			}
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != typeof(int) && destinationType != typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value is string)
			{
				if (destinationType == typeof(int))
				{
					if (string.Compare("FullyExpand", (string)value, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return -1;
					}
					try
					{
						return int.Parse((string)value);
					}
					catch (Exception)
					{
						return -1;
					}
				}
				return value;
			}
			int num = (int)value;
			if (destinationType == typeof(string))
			{
				if (num == -1)
				{
					return "FullyExpand";
				}
				return num.ToString();
			}
			return value;
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string) && !(value is int))
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (value is string)
			{
				if (string.Compare("FullyExpand", (string)value, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return -1;
				}
				try
				{
					return int.Parse((string)value);
				}
				catch (Exception)
				{
					return null;
				}
			}
			return value;
		}
	}

	private class ImageStyle
	{
		public string Expand;

		public string Collapse;

		public string NoExpand;

		public string RootIcon;

		public string ParentIcon;

		public string LeafIcon;

		public ImageStyle(string expand, string collapse, string noExpand, string icon, string iconLeaf, string iconRoot)
		{
			Expand = expand;
			Collapse = collapse;
			NoExpand = noExpand;
			RootIcon = iconRoot;
			ParentIcon = icon;
			LeafIcon = iconLeaf;
		}
	}

	private static readonly char[] postDataSplitChars;

	private string activeSiteMapPath;

	private bool stylesPrepared;

	private Style hoverNodeStyle;

	private TreeNodeStyle leafNodeStyle;

	private TreeNodeStyle nodeStyle;

	private TreeNodeStyle parentNodeStyle;

	private TreeNodeStyle rootNodeStyle;

	private TreeNodeStyle selectedNodeStyle;

	private TreeNodeStyleCollection levelStyles;

	private TreeNodeCollection nodes;

	private TreeNodeBindingCollection dataBindings;

	private TreeNode selectedNode;

	private Hashtable bindings;

	private int registeredStylesCounter = -1;

	private List<Style> levelLinkStyles;

	private Style controlLinkStyle;

	private Style nodeLinkStyle;

	private Style rootNodeLinkStyle;

	private Style parentNodeLinkStyle;

	private Style leafNodeLinkStyle;

	private Style selectedNodeLinkStyle;

	private Style hoverNodeLinkStyle;

	private static readonly object TreeNodeCheckChangedEvent;

	private static readonly object SelectedNodeChangedEvent;

	private static readonly object TreeNodeCollapsedEvent;

	private static readonly object TreeNodeDataBoundEvent;

	private static readonly object TreeNodeExpandedEvent;

	private static readonly object TreeNodePopulateEvent;

	private static Hashtable imageStyles;

	private string callbackResult;

	private const string _OnPreRender_Script_Preamble = "var {0} = new Object ();\n{0}.treeId = {1};\n{0}.uid = {2};\n{0}.showImage = {3};\n";

	private const string _OnPreRender_Script_ShowExpandCollapse = "{0}.expandImage = {1};\n{0}.collapseImage = {2};\n";

	private const string _OnPreRender_Script_ShowExpandCollapse_Populate = "{0}.noExpandImage = {1};\n";

	private const string _OnPreRender_Script_PopulateCallback = "{0}.form = {1};\n{0}.PopulateNode = function (nodeId, nodeValue, nodeImageUrl, nodeNavigateUrl, nodeTarget) {{\n\t{2}.__theFormPostData = \"\";\n\t{2}.__theFormPostCollection = new Array ();\n\t{2}.WebForm_InitCallback ();\n\tTreeView_PopulateNode (this.uid, this.treeId, nodeId, nodeValue, nodeImageUrl, nodeNavigateUrl, nodeTarget)\n}};\n";

	private const string _OnPreRender_Script_CallbackOptions = "{0}.populateFromClient = {1};\n{0}.expandAlt = {2};\n{0}.collapseAlt = {3};\n";

	private const string _OnPreRender_Script_HoverStyle = "{0}.hoverClass = {1};\n{0}.hoverLinkClass = {2};\n";

	/// <summary>Gets or sets the ToolTip for the image that is displayed for the collapsible node indicator.</summary>
	/// <returns>The ToolTip for the image displayed for the collapsible node indicator.</returns>
	[Localizable(true)]
	public string CollapseImageToolTip
	{
		get
		{
			return ViewState.GetString("CollapseImageToolTip", "Collapse {0}");
		}
		set
		{
			ViewState["CollapseImageToolTip"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.TreeView" /> control automatically generates tree node bindings.</summary>
	/// <returns>
	///     <see langword="true" /> to have the <see cref="T:System.Web.UI.WebControls.TreeView" /> control automatically generate tree node bindings; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[MonoTODO("Implement support for this")]
	[WebCategory("Behavior")]
	[WebSysDescription("Whether the tree will automatically generate bindings.")]
	[DefaultValue(true)]
	public bool AutoGenerateDataBindings
	{
		get
		{
			return ViewState.GetBool("AutoGenerateDataBindings", def: true);
		}
		set
		{
			ViewState["AutoGenerateDataBindings"] = value;
		}
	}

	/// <summary>Gets or sets the URL to a custom image for the collapsible node indicator.</summary>
	/// <returns>The URL to a custom image to display for collapsible nodes. The default is an empty string (""), which displays the default minus sign (-) image.</returns>
	[DefaultValue("")]
	[WebSysDescription("The url of the image to show when a node can be collapsed.")]
	[UrlProperty]
	[WebCategory("Appearance")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string CollapseImageUrl
	{
		get
		{
			return ViewState.GetString("CollapseImageUrl", string.Empty);
		}
		set
		{
			ViewState["CollapseImageUrl"] = value;
		}
	}

	/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> objects that define the relationship between a data item and the node that it is binding to.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> that represents the relationship between a data item and the node that it is binding to.</returns>
	[WebCategory("Data")]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("Bindings for tree nodes.")]
	[Editor("System.Web.UI.Design.WebControls.TreeViewBindingsEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue(null)]
	[MergableProperty(false)]
	public TreeNodeBindingCollection DataBindings
	{
		get
		{
			if (dataBindings == null)
			{
				dataBindings = new TreeNodeBindingCollection();
				if (base.IsTrackingViewState)
				{
					((IStateManager)dataBindings).TrackViewState();
				}
			}
			return dataBindings;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.TreeView" /> control renders client-side script to handle expanding and collapsing events.</summary>
	/// <returns>
	///     <see langword="true" /> to render the client-side script on compatible browsers; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[WebCategory("Behavior")]
	[WebSysDescription("Whether the tree view can use client-side script to expand and collapse nodes.")]
	[Themeable(false)]
	[DefaultValue(true)]
	public bool EnableClientScript
	{
		get
		{
			return ViewState.GetBool("EnableClientScript", def: true);
		}
		set
		{
			ViewState["EnableClientScript"] = value;
		}
	}

	/// <summary>Gets or sets the number of levels that are expanded when a <see cref="T:System.Web.UI.WebControls.TreeView" /> control is displayed for the first time.</summary>
	/// <returns>The depth to display when the <see cref="T:System.Web.UI.WebControls.TreeView" /> is initially displayed. The default is -1, which displays all the nodes.</returns>
	[DefaultValue(-1)]
	[WebCategory("Behavior")]
	[WebSysDescription("The initial expand depth.")]
	[TypeConverter("System.Web.UI.WebControls.TreeView+TreeViewExpandDepthConverter, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public int ExpandDepth
	{
		get
		{
			return ViewState.GetInt("ExpandDepth", -1);
		}
		set
		{
			ViewState["ExpandDepth"] = value;
		}
	}

	/// <summary>Gets or sets the ToolTip for the image that is displayed for the expandable node indicator.</summary>
	/// <returns>The ToolTip for the image displayed for the expandable node indicator.</returns>
	[Localizable(true)]
	public string ExpandImageToolTip
	{
		get
		{
			return ViewState.GetString("ExpandImageToolTip", "Expand {0}");
		}
		set
		{
			ViewState["ExpandImageToolTip"] = value;
		}
	}

	/// <summary>Gets or sets the URL to a custom image for the expandable node indicator.</summary>
	/// <returns>The URL to a custom image to display for expandable nodes. The default is an empty string (""), which displays the default plus sign (+) image.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[WebSysDescription("The url of the image to show when a node can be expanded.")]
	[WebCategory("Appearance")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string ExpandImageUrl
	{
		get
		{
			return ViewState.GetString("ExpandImageUrl", string.Empty);
		}
		set
		{
			ViewState["ExpandImageUrl"] = value;
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object that allows you to set the appearance of a node when the mouse pointer is positioned over it.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> that represents the style of a node when the mouse pointer is positioned over it.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DefaultValue(null)]
	[WebCategory("Styles")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public Style HoverNodeStyle
	{
		get
		{
			if (hoverNodeStyle == null)
			{
				hoverNodeStyle = new Style();
				if (base.IsTrackingViewState)
				{
					hoverNodeStyle.TrackViewState();
				}
			}
			return hoverNodeStyle;
		}
	}

	/// <summary>Gets or sets the group of images to use for the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TreeViewImageSet" /> values. The default is <see langword="TreeViewImageSet.Custom" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified image set is not one of the <see cref="T:System.Web.UI.WebControls.TreeViewImageSet" /> values. </exception>
	[DefaultValue(TreeViewImageSet.Custom)]
	public TreeViewImageSet ImageSet
	{
		get
		{
			return (TreeViewImageSet)ViewState.GetInt("ImageSet", 0);
		}
		set
		{
			if (!Enum.IsDefined(typeof(TreeViewImageSet), value))
			{
				throw new ArgumentOutOfRangeException();
			}
			ViewState["ImageSet"] = value;
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object that allows you to set the appearance of leaf nodes.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> that represents the style of the leaf nodes in the <see cref="T:System.Web.UI.WebControls.TreeView" />.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DefaultValue(null)]
	[WebCategory("Styles")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public TreeNodeStyle LeafNodeStyle
	{
		get
		{
			if (leafNodeStyle == null)
			{
				leafNodeStyle = new TreeNodeStyle();
				if (base.IsTrackingViewState)
				{
					leafNodeStyle.TrackViewState();
				}
			}
			return leafNodeStyle;
		}
	}

	/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.Style" /> objects that represent the node styles at the individual levels of the tree.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.StyleCollection" /> that represents the node styles at the individual levels of the tree. </returns>
	[DefaultValue(null)]
	[WebCategory("Styles")]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Editor("System.Web.UI.Design.WebControls.TreeNodeStyleCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public TreeNodeStyleCollection LevelStyles
	{
		get
		{
			if (levelStyles == null)
			{
				levelStyles = new TreeNodeStyleCollection();
				if (base.IsTrackingViewState)
				{
					((IStateManager)levelStyles).TrackViewState();
				}
			}
			return levelStyles;
		}
	}

	/// <summary>Gets or sets the path to a folder that contains the line images that are used to connect child nodes to parent nodes.</summary>
	/// <returns>The path to a folder that contains the line images used to connect nodes. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeView.LineImagesFolder" /> property is not set.</returns>
	[DefaultValue("")]
	public string LineImagesFolder
	{
		get
		{
			return ViewState.GetString("LineImagesFolder", string.Empty);
		}
		set
		{
			ViewState["LineImagesFolder"] = value;
		}
	}

	/// <summary>Gets or sets the maximum number of tree levels to bind to the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <returns>The maximum number of tree levels to bind to the <see cref="T:System.Web.UI.WebControls.TreeView" /> control. The default is -1, which binds all the tree levels in the data source to the control.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than <see langword="-1" />.</exception>
	[DefaultValue(-1)]
	public int MaxDataBindDepth
	{
		get
		{
			return ViewState.GetInt("MaxDataBindDepth", -1);
		}
		set
		{
			ViewState["MaxDataBindDepth"] = value;
		}
	}

	/// <summary>Gets or sets the indentation amount (in pixels) for the child nodes of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <returns>The amount of space (in pixels) between a child node's left edge and its parent node's left edge. The default is 20.</returns>
	[DefaultValue(20)]
	public int NodeIndent
	{
		get
		{
			return ViewState.GetInt("NodeIndent", 20);
		}
		set
		{
			ViewState["NodeIndent"] = value;
		}
	}

	/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.TreeNode" /> objects that represents the root nodes in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> that contains the root nodes in the <see cref="T:System.Web.UI.WebControls.TreeView" />.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Editor("System.Web.UI.Design.WebControls.TreeNodeCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue(null)]
	[MergableProperty(false)]
	public TreeNodeCollection Nodes
	{
		get
		{
			if (nodes == null)
			{
				nodes = new TreeNodeCollection(this);
				if (base.IsTrackingViewState)
				{
					((IStateManager)nodes).TrackViewState();
				}
			}
			return nodes;
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object that allows you to set the default appearance of the nodes in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> that represents the default style of a node.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DefaultValue(null)]
	[WebCategory("Styles")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public TreeNodeStyle NodeStyle
	{
		get
		{
			if (nodeStyle == null)
			{
				nodeStyle = new TreeNodeStyle();
				if (base.IsTrackingViewState)
				{
					nodeStyle.TrackViewState();
				}
			}
			return nodeStyle;
		}
	}

	/// <summary>Gets or sets a value indicating whether text wraps in a node when the node runs out of space.</summary>
	/// <returns>
	///     <see langword="true" /> to wrap the text; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	public bool NodeWrap
	{
		get
		{
			return ViewState.GetBool("NodeWrap", def: false);
		}
		set
		{
			ViewState["NodeWrap"] = value;
		}
	}

	/// <summary>Gets or sets the URL to a custom image for the non-expandable node indicator.</summary>
	/// <returns>The URL to a custom image to display for non-expandable nodes. The default is an empty string (""), which displays the default blank image.</returns>
	[UrlProperty]
	[DefaultValue("")]
	[WebSysDescription("The url of the image to show for leaf nodes.")]
	[WebCategory("Appearance")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string NoExpandImageUrl
	{
		get
		{
			return ViewState.GetString("NoExpandImageUrl", string.Empty);
		}
		set
		{
			ViewState["NoExpandImageUrl"] = value;
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object that allows you to set the appearance of parent nodes in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> that represents the style of the parent nodes in the <see cref="T:System.Web.UI.WebControls.TreeView" />.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DefaultValue(null)]
	[WebCategory("Styles")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public TreeNodeStyle ParentNodeStyle
	{
		get
		{
			if (parentNodeStyle == null)
			{
				parentNodeStyle = new TreeNodeStyle();
				if (base.IsTrackingViewState)
				{
					parentNodeStyle.TrackViewState();
				}
			}
			return parentNodeStyle;
		}
	}

	/// <summary>Gets or sets the character that is used to delimit the node values that are specified by the <see cref="P:System.Web.UI.WebControls.TreeNode.ValuePath" /> property.</summary>
	/// <returns>The character used to delimit the node values specified in the <see cref="P:System.Web.UI.WebControls.TreeNode.ValuePath" /> property. The default is a slash mark (/).</returns>
	[DefaultValue('/')]
	public char PathSeparator
	{
		get
		{
			return ViewState.GetChar("PathSeparator", '/');
		}
		set
		{
			ViewState["PathSeparator"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether node data is populated on demand from the client.</summary>
	/// <returns>
	///     <see langword="true" /> to populate tree node data on demand from the client; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	public bool PopulateNodesFromClient
	{
		get
		{
			return ViewState.GetBool("PopulateNodesFromClient", def: true);
		}
		set
		{
			ViewState["PopulateNodesFromClient"] = value;
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object that allows you to set the appearance of the root node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> that represents the style of the root node in the <see cref="T:System.Web.UI.WebControls.TreeView" />.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DefaultValue(null)]
	[WebCategory("Styles")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public TreeNodeStyle RootNodeStyle
	{
		get
		{
			if (rootNodeStyle == null)
			{
				rootNodeStyle = new TreeNodeStyle();
				if (base.IsTrackingViewState)
				{
					rootNodeStyle.TrackViewState();
				}
			}
			return rootNodeStyle;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object that controls the appearance of the selected node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> that represents the style of the selected node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control. The default is <see langword="null" />, which indicates that the <see cref="P:System.Web.UI.WebControls.TreeView.SelectedNodeStyle" /> property is not set.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DefaultValue(null)]
	[WebCategory("Styles")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public TreeNodeStyle SelectedNodeStyle
	{
		get
		{
			if (selectedNodeStyle == null)
			{
				selectedNodeStyle = new TreeNodeStyle();
				if (base.IsTrackingViewState)
				{
					selectedNodeStyle.TrackViewState();
				}
			}
			return selectedNodeStyle;
		}
	}

	private Style ControlLinkStyle
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

	private Style NodeLinkStyle
	{
		get
		{
			if (nodeLinkStyle == null)
			{
				nodeLinkStyle = new Style();
			}
			return nodeLinkStyle;
		}
	}

	private Style RootNodeLinkStyle
	{
		get
		{
			if (rootNodeLinkStyle == null)
			{
				rootNodeLinkStyle = new Style();
			}
			return rootNodeLinkStyle;
		}
	}

	private Style ParentNodeLinkStyle
	{
		get
		{
			if (parentNodeLinkStyle == null)
			{
				parentNodeLinkStyle = new Style();
			}
			return parentNodeLinkStyle;
		}
	}

	private Style SelectedNodeLinkStyle
	{
		get
		{
			if (selectedNodeLinkStyle == null)
			{
				selectedNodeLinkStyle = new Style();
			}
			return selectedNodeLinkStyle;
		}
	}

	private Style LeafNodeLinkStyle
	{
		get
		{
			if (leafNodeLinkStyle == null)
			{
				leafNodeLinkStyle = new Style();
			}
			return leafNodeLinkStyle;
		}
	}

	private Style HoverNodeLinkStyle
	{
		get
		{
			if (hoverNodeLinkStyle == null)
			{
				hoverNodeLinkStyle = new Style();
			}
			return hoverNodeLinkStyle;
		}
	}

	/// <summary>Gets or sets a value indicating which node types will display a check box in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <returns>A bitwise combination of the <see cref="T:System.Web.UI.WebControls.TreeNodeTypes" /> values. The default is <see langword="TreeNodeType.None" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The bitwise combination value is outside the range of the <see cref="T:System.Web.UI.WebControls.TreeNodeTypes" /> enumeration. </exception>
	[DefaultValue(TreeNodeTypes.None)]
	public TreeNodeTypes ShowCheckBoxes
	{
		get
		{
			return (TreeNodeTypes)ViewState.GetInt("ShowCheckBoxes", 0);
		}
		set
		{
			if (value > TreeNodeTypes.All)
			{
				throw new ArgumentOutOfRangeException();
			}
			ViewState["ShowCheckBoxes"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether expansion node indicators are displayed.</summary>
	/// <returns>
	///     <see langword="true" /> to show the expansion node indicators; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	public bool ShowExpandCollapse
	{
		get
		{
			return ViewState.GetBool("ShowExpandCollapse", def: true);
		}
		set
		{
			ViewState["ShowExpandCollapse"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether lines connecting child nodes to parent nodes are displayed.</summary>
	/// <returns>
	///     <see langword="true" /> to display lines connecting nodes; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	public bool ShowLines
	{
		get
		{
			return ViewState.GetBool("ShowLines", def: false);
		}
		set
		{
			ViewState["ShowLines"] = value;
		}
	}

	/// <summary>Gets or sets a value that is used to render alternate text for screen readers to skip the content for the control. </summary>
	/// <returns>A string that the <see cref="T:System.Web.UI.WebControls.TreeView" /> renders as alternate text with an invisible image as a hint to screen readers. The default is "Skip Navigation Links." </returns>
	[Localizable(true)]
	public string SkipLinkText
	{
		get
		{
			return ViewState.GetString("SkipLinkText", "Skip Navigation Links.");
		}
		set
		{
			ViewState["SkipLinkText"] = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object that represents the selected node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNode" /> that represents the selected node in the <see cref="T:System.Web.UI.WebControls.TreeView" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public TreeNode SelectedNode => selectedNode;

	/// <summary>Gets the value of the selected node.</summary>
	/// <returns>The value of the selected node.</returns>
	[Browsable(false)]
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string SelectedValue
	{
		get
		{
			if (selectedNode == null)
			{
				return string.Empty;
			}
			return selectedNode.Value;
		}
	}

	/// <summary>Gets or sets the target window or frame in which to display the Web page content that is associated with a node.</summary>
	/// <returns>The target window or frame in which to display the linked Web page content. Values must begin with a letter in the range of A through Z (case insensitive), except for certain special values that begin with an underscore, as shown in the following table.Target value Renders the content in 
	///             <see langword="_blank" />
	///           A new window without frames. 
	///             <see langword="_parent" />
	///           The immediate frameset parent. 
	///             <see langword="_search" />
	///           The search pane.
	///             <see langword="_self" />
	///           The frame with focus. 
	///             <see langword="_top" />
	///           The full window without frames. Check your browser documentation to determine if the <see langword="_search" /> value is supported.  For example, Microsoft Internet Explorer 5.0 and later supports the <see langword="_search" /> target value.The default is an empty string (""), which refreshes the window or frame with focus.</returns>
	[DefaultValue("")]
	public string Target
	{
		get
		{
			return ViewState.GetString("Target", string.Empty);
		}
		set
		{
			ViewState["Target"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the control is rendered as UI on the page. </summary>
	/// <returns>
	///     <see langword="true" />, if the control is visible on the page; otherwise, <see langword="false" />. </returns>
	[MonoTODO("why override?")]
	public override bool Visible
	{
		get
		{
			return base.Visible;
		}
		set
		{
			base.Visible = value;
		}
	}

	/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.TreeNode" /> objects that represent the nodes in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control that display a selected check box.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> that contains the nodes in the <see cref="T:System.Web.UI.WebControls.TreeView" /> that display a selected check box.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public TreeNodeCollection CheckedNodes
	{
		get
		{
			TreeNodeCollection result = new TreeNodeCollection();
			FindCheckedNodes(Nodes, result);
			return result;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <returns>Always returns a <see cref="F:System.Web.UI.HtmlTextWriterTag.Div" /> value.</returns>
	protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

	/// <summary>Occurs when a check box in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control changes state between posts to the server.</summary>
	public event TreeNodeEventHandler TreeNodeCheckChanged
	{
		add
		{
			base.Events.AddHandler(TreeNodeCheckChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(TreeNodeCheckChangedEvent, value);
		}
	}

	/// <summary>Occurs when a node is selected in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	public event EventHandler SelectedNodeChanged
	{
		add
		{
			base.Events.AddHandler(SelectedNodeChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SelectedNodeChangedEvent, value);
		}
	}

	/// <summary>Occurs when a node is collapsed in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	public event TreeNodeEventHandler TreeNodeCollapsed
	{
		add
		{
			base.Events.AddHandler(TreeNodeCollapsedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(TreeNodeCollapsedEvent, value);
		}
	}

	/// <summary>Occurs when a data item is bound to a node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	public event TreeNodeEventHandler TreeNodeDataBound
	{
		add
		{
			base.Events.AddHandler(TreeNodeDataBoundEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(TreeNodeDataBoundEvent, value);
		}
	}

	/// <summary>Occurs when a node is expanded in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	public event TreeNodeEventHandler TreeNodeExpanded
	{
		add
		{
			base.Events.AddHandler(TreeNodeExpandedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(TreeNodeExpandedEvent, value);
		}
	}

	/// <summary>Occurs when a node with its <see cref="P:System.Web.UI.WebControls.TreeNode.PopulateOnDemand" /> property set to <see langword="true" /> is expanded in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	public event TreeNodeEventHandler TreeNodePopulate
	{
		add
		{
			base.Events.AddHandler(TreeNodePopulateEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(TreeNodePopulateEvent, value);
		}
	}

	static TreeView()
	{
		postDataSplitChars = new char[1] { '|' };
		TreeNodeCheckChanged = new object();
		SelectedNodeChanged = new object();
		TreeNodeCollapsed = new object();
		TreeNodeDataBound = new object();
		TreeNodeExpanded = new object();
		TreeNodePopulate = new object();
		imageStyles = new Hashtable();
		imageStyles[TreeViewImageSet.Arrows] = new ImageStyle("arrow_plus", "arrow_minus", "arrow_noexpand", null, null, null);
		imageStyles[TreeViewImageSet.BulletedList] = new ImageStyle(null, null, null, "dot_full", "dot_empty", "dot_full");
		imageStyles[TreeViewImageSet.BulletedList2] = new ImageStyle(null, null, null, "box_full", "box_empty", "box_full");
		imageStyles[TreeViewImageSet.BulletedList3] = new ImageStyle(null, null, null, "star_full", "star_empty", "star_full");
		imageStyles[TreeViewImageSet.BulletedList4] = new ImageStyle(null, null, null, "star_full", "star_empty", "dots");
		imageStyles[TreeViewImageSet.Contacts] = new ImageStyle("TreeView_plus", "TreeView_minus", "contact", null, null, null);
		imageStyles[TreeViewImageSet.Events] = new ImageStyle(null, null, null, "warning", "warning", "warning");
		imageStyles[TreeViewImageSet.Inbox] = new ImageStyle(null, null, null, "inbox", "inbox", "inbox");
		imageStyles[TreeViewImageSet.Msdn] = new ImageStyle("box_plus", "box_minus", "box_noexpand", null, null, null);
		imageStyles[TreeViewImageSet.Simple] = new ImageStyle(null, null, "box_full", null, null, null);
		imageStyles[TreeViewImageSet.Simple2] = new ImageStyle(null, null, "box_empty", null, null, null);
		imageStyles[TreeViewImageSet.News] = new ImageStyle("TreeView_plus", "TreeView_minus", "TreeView_noexpand", null, null, null);
		imageStyles[TreeViewImageSet.Faq] = new ImageStyle("TreeView_plus", "TreeView_minus", "TreeView_noexpand", null, null, null);
		imageStyles[TreeViewImageSet.WindowsHelp] = new ImageStyle("TreeView_plus", "TreeView_minus", "TreeView_noexpand", null, null, null);
		imageStyles[TreeViewImageSet.XPFileExplorer] = new ImageStyle("TreeView_plus", "TreeView_minus", "TreeView_noexpand", "folder", "file", "computer");
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeCheckChanged" /> event of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.TreeNodeEventArgs" /> that contains event data. </param>
	protected virtual void OnTreeNodeCheckChanged(TreeNodeEventArgs e)
	{
		if (base.Events != null)
		{
			((TreeNodeEventHandler)base.Events[TreeNodeCheckChanged])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TreeView.SelectedNodeChanged" /> event of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
	protected virtual void OnSelectedNodeChanged(EventArgs e)
	{
		if (base.Events != null)
		{
			((EventHandler)base.Events[SelectedNodeChanged])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeCollapsed" /> event of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.TreeNodeEventArgs" /> that contains event data. </param>
	protected virtual void OnTreeNodeCollapsed(TreeNodeEventArgs e)
	{
		if (base.Events != null)
		{
			((TreeNodeEventHandler)base.Events[TreeNodeCollapsed])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeDataBound" /> event of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.TreeNodeEventArgs" /> that contains event data. </param>
	protected virtual void OnTreeNodeDataBound(TreeNodeEventArgs e)
	{
		if (base.Events != null)
		{
			((TreeNodeEventHandler)base.Events[TreeNodeDataBound])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeExpanded" /> event of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.TreeNodeEventArgs" /> that contains event data. </param>
	protected virtual void OnTreeNodeExpanded(TreeNodeEventArgs e)
	{
		if (base.Events != null)
		{
			((TreeNodeEventHandler)base.Events[TreeNodeExpanded])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodePopulate" /> event of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.TreeNodeEventArgs" /> that contains event data. </param>
	protected virtual void OnTreeNodePopulate(TreeNodeEventArgs e)
	{
		if (base.Events != null)
		{
			((TreeNodeEventHandler)base.Events[TreeNodePopulate])?.Invoke(this, e);
		}
	}

	private void FindCheckedNodes(TreeNodeCollection nodeList, TreeNodeCollection result)
	{
		foreach (TreeNode node in nodeList)
		{
			if (node.Checked)
			{
				result.Add(node, updateParent: false);
			}
			FindCheckedNodes(node.ChildNodes, result);
		}
	}

	/// <summary>Opens every node in the tree.</summary>
	public void ExpandAll()
	{
		foreach (TreeNode node in Nodes)
		{
			node.ExpandAll();
		}
	}

	/// <summary>Closes every node in the tree.</summary>
	public void CollapseAll()
	{
		foreach (TreeNode node in Nodes)
		{
			node.CollapseAll();
		}
	}

	/// <summary>Retrieves the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control at the specified value path.</summary>
	/// <param name="valuePath">The value path of a node. </param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.TreeNode" /> at the specified value path.</returns>
	public TreeNode FindNode(string valuePath)
	{
		if (valuePath == null)
		{
			throw new ArgumentNullException("valuePath");
		}
		string[] array = valuePath.Split(PathSeparator);
		int num = 0;
		TreeNodeCollection childNodes = Nodes;
		bool flag = true;
		while (childNodes.Count > 0 && flag)
		{
			flag = false;
			foreach (TreeNode item in childNodes)
			{
				if (item.Value == array[num])
				{
					if (++num == array.Length)
					{
						return item;
					}
					childNodes = item.ChildNodes;
					flag = true;
					break;
				}
			}
		}
		return null;
	}

	private ImageStyle GetImageStyle()
	{
		if (ImageSet != 0)
		{
			return (ImageStyle)imageStyles[ImageSet];
		}
		return null;
	}

	/// <summary>Returns a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class. The <see cref="M:System.Web.UI.WebControls.TreeView.CreateNode" /> is a helper method.</summary>
	/// <returns>A new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" />.</returns>
	protected internal virtual TreeNode CreateNode()
	{
		return new TreeNode(this);
	}

	/// <summary>Calls the <see cref="M:System.Web.UI.WebControls.BaseDataBoundControl.DataBind" /> method of the base class. </summary>
	public sealed override void DataBind()
	{
		base.DataBind();
	}

	/// <summary>Allows a derived class to set whether the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> control is data-bound.</summary>
	/// <param name="node">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> to set. </param>
	/// <param name="dataBound">
	///       <see langword="true" /> to set the node as data-bound; otherwise, <see langword="false" />. </param>
	protected void SetNodeDataBound(TreeNode node, bool dataBound)
	{
		node.SetDataBound(dataBound);
	}

	/// <summary>Allows a derived class to set the data path for the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> control.</summary>
	/// <param name="node">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> to set. </param>
	/// <param name="dataPath">The data path for the <see cref="T:System.Web.UI.WebControls.TreeNode" />. </param>
	protected void SetNodeDataPath(TreeNode node, string dataPath)
	{
		node.SetDataPath(dataPath);
	}

	/// <summary>Allows a derived class to set the data item for the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> control.</summary>
	/// <param name="node">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> to set. </param>
	/// <param name="dataItem">The data item for the <see cref="T:System.Web.UI.WebControls.TreeNode" />. </param>
	protected void SetNodeDataItem(TreeNode node, object dataItem)
	{
		node.SetDataItem(dataItem);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal override void OnInit(EventArgs e)
	{
		base.OnInit(e);
	}

	internal void SetSelectedNode(TreeNode node, bool loading)
	{
		if (selectedNode != node)
		{
			if (selectedNode != null)
			{
				selectedNode.SelectedFlag = false;
			}
			selectedNode = node;
			if (!loading)
			{
				OnSelectedNodeChanged(new TreeNodeEventArgs(selectedNode));
			}
		}
	}

	internal void NotifyCheckChanged(TreeNode node)
	{
		OnTreeNodeCheckChanged(new TreeNodeEventArgs(node));
	}

	internal void NotifyExpandedChanged(TreeNode node)
	{
		if (node.Expanded.HasValue && node.Expanded.Value)
		{
			OnTreeNodeExpanded(new TreeNodeEventArgs(node));
		}
		else if (node.Expanded.HasValue && node.IsParentNode)
		{
			OnTreeNodeCollapsed(new TreeNodeEventArgs(node));
		}
	}

	internal void NotifyPopulateRequired(TreeNode node)
	{
		OnTreeNodePopulate(new TreeNodeEventArgs(node));
	}

	/// <summary>Tracks view-state changes to the <see cref="T:System.Web.UI.WebControls.TreeView" /> control so that they can be stored in the <see cref="T:System.Web.UI.StateBag" /> object for the control. This <see cref="T:System.Web.UI.StateBag" /> is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
	protected override void TrackViewState()
	{
		EnsureDataBound();
		base.TrackViewState();
		if (hoverNodeStyle != null)
		{
			hoverNodeStyle.TrackViewState();
		}
		if (leafNodeStyle != null)
		{
			leafNodeStyle.TrackViewState();
		}
		if (levelStyles != null && levelStyles.Count > 0)
		{
			((IStateManager)levelStyles).TrackViewState();
		}
		if (nodeStyle != null)
		{
			nodeStyle.TrackViewState();
		}
		if (parentNodeStyle != null)
		{
			parentNodeStyle.TrackViewState();
		}
		if (rootNodeStyle != null)
		{
			rootNodeStyle.TrackViewState();
		}
		if (selectedNodeStyle != null)
		{
			selectedNodeStyle.TrackViewState();
		}
		if (dataBindings != null)
		{
			((IStateManager)dataBindings).TrackViewState();
		}
		if (nodes != null)
		{
			((IStateManager)nodes).TrackViewState();
		}
	}

	/// <summary>Saves the state of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <returns>The server control's current view state; otherwise, <see langword="null" />, if there is no view state associated with the control.</returns>
	protected override object SaveViewState()
	{
		object[] array = new object[10]
		{
			base.SaveViewState(),
			(hoverNodeStyle == null) ? null : hoverNodeStyle.SaveViewState(),
			(leafNodeStyle == null) ? null : leafNodeStyle.SaveViewState(),
			(levelStyles == null) ? null : ((IStateManager)levelStyles).SaveViewState(),
			(nodeStyle == null) ? null : nodeStyle.SaveViewState(),
			(parentNodeStyle == null) ? null : parentNodeStyle.SaveViewState(),
			(rootNodeStyle == null) ? null : rootNodeStyle.SaveViewState(),
			(selectedNodeStyle == null) ? null : selectedNodeStyle.SaveViewState(),
			(dataBindings == null) ? null : ((IStateManager)dataBindings).SaveViewState(),
			(nodes == null) ? null : ((IStateManager)nodes).SaveViewState()
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

	/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <param name="state">A object that contains the saved view state values for the control. </param>
	protected override void LoadViewState(object state)
	{
		if (state != null)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				HoverNodeStyle.LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				LeafNodeStyle.LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)LevelStyles).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				NodeStyle.LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				ParentNodeStyle.LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				RootNodeStyle.LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				SelectedNodeStyle.LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				((IStateManager)DataBindings).LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				((IStateManager)Nodes).LoadViewState(array[9]);
			}
		}
	}

	/// <summary>Enables the <see cref="T:System.Web.UI.WebControls.TreeView" /> control to process an event that is raised when a form is posted to the server. The <see cref="M:System.Web.UI.WebControls.TreeView.RaisePostBackEvent(System.String)" /> method is a helper method for the <see cref="M:System.Web.UI.WebControls.TreeView.System#Web#UI#ICallbackEventHandler#RaiseCallbackEvent(System.String)" /> method.</summary>
	/// <param name="eventArgument">A string that represents an optional event argument to pass to the event handler. </param>
	protected virtual void RaisePostBackEvent(string eventArgument)
	{
		ValidateEvent(UniqueID, eventArgument);
		string[] array = eventArgument.Split('|');
		TreeNode treeNode = FindNodeByPos(array[1]);
		if (treeNode != null)
		{
			if (array[0] == "sel")
			{
				HandleSelectEvent(treeNode);
			}
			else if (array[0] == "ec")
			{
				HandleExpandCollapseEvent(treeNode);
			}
		}
	}

	private void HandleSelectEvent(TreeNode node)
	{
		switch (node.SelectAction)
		{
		case TreeNodeSelectAction.Select:
			node.Select();
			break;
		case TreeNodeSelectAction.Expand:
			node.Expand();
			break;
		case TreeNodeSelectAction.SelectExpand:
			node.Select();
			node.Expand();
			break;
		}
	}

	private void HandleExpandCollapseEvent(TreeNode node)
	{
		node.ToggleExpandState();
	}

	/// <summary>Signals the <see cref="T:System.Web.UI.WebControls.TreeView" /> control to notify the ASP.NET application that the state of the control has changed.</summary>
	protected virtual void RaisePostDataChangedEvent()
	{
	}

	private TreeNode MakeNodeTree(string[] args)
	{
		string[] array = args[0].Split('_');
		TreeNode treeNode = null;
		string[] array2 = array;
		foreach (string obj in array2)
		{
			int index = int.Parse(obj);
			TreeNode treeNode2 = new TreeNode(obj);
			if (treeNode != null)
			{
				treeNode.ChildNodes.Add(treeNode2);
				treeNode2.Index = index;
			}
			treeNode = treeNode2;
		}
		treeNode.Value = args[1].Replace("U+007C", "|");
		treeNode.ImageUrl = args[2].Replace("U+007C", "|");
		treeNode.NavigateUrl = args[3].Replace("U+007C", "|");
		treeNode.Target = args[4].Replace("U+007C", "|");
		treeNode.Tree = this;
		NotifyPopulateRequired(treeNode);
		return treeNode;
	}

	/// <summary>Raises the callback event using the specified arguments. </summary>
	/// <param name="eventArgument">A string that represents an optional event argument to pass to the event handler.</param>
	protected virtual void RaiseCallbackEvent(string eventArgument)
	{
		string[] args = eventArgument.Split('|');
		base.RequiresDataBinding = true;
		EnsureDataBound();
		TreeNode treeNode = MakeNodeTree(args);
		ArrayList arrayList = new ArrayList();
		for (TreeNode treeNode2 = treeNode; treeNode2 != null; treeNode2 = treeNode2.Parent)
		{
			int num = ((treeNode2.Parent != null) ? treeNode2.Parent.ChildNodes.Count : Nodes.Count);
			arrayList.Insert(0, (treeNode2.Index < num - 1) ? this : null);
		}
		StringWriter stringWriter = new StringWriter();
		HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
		EnsureStylesPrepared();
		treeNode.Expanded = true;
		int count = treeNode.ChildNodes.Count;
		for (int i = 0; i < count; i++)
		{
			RenderNode(writer, treeNode.ChildNodes[i], treeNode.Depth + 1, arrayList, hasPrevious: true, i < count - 1);
		}
		string text = stringWriter.ToString();
		callbackResult = ((text.Length > 0) ? text : "*");
	}

	/// <summary>Returns the result of a callback event that targets a control.</summary>
	/// <returns>The results of the callback.</returns>
	protected virtual string GetCallbackResult()
	{
		return callbackResult;
	}

	/// <summary>Enables the <see cref="T:System.Web.UI.WebControls.TreeView" /> control to process an event that is raised when a form is posted to the server.</summary>
	/// <param name="eventArgument">A string that represents an optional event argument to pass to the event handler. </param>
	void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
	{
		RaisePostBackEvent(eventArgument);
	}

	/// <summary>Processes postback data for the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <param name="postDataKey">The key identifier for the control. </param>
	/// <param name="postCollection">The collection of all incoming name values. </param>
	/// <returns>
	///     <see langword="true" />, if the <see cref="T:System.Web.UI.WebControls.TreeView" /> control's state changes as a result of the postback event; otherwise, <see langword="false" />.</returns>
	bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostData(postDataKey, postCollection);
	}

	/// <summary>Signals the <see cref="T:System.Web.UI.WebControls.TreeView" /> control to notify the ASP.NET application that the state of the control has changed.</summary>
	void IPostBackDataHandler.RaisePostDataChangedEvent()
	{
		RaisePostDataChangedEvent();
	}

	/// <summary>Raises the callback event using the specified arguments.</summary>
	/// <param name="eventArgument">A string that represents an optional event argument to pass to the event handler.</param>
	void ICallbackEventHandler.RaiseCallbackEvent(string eventArgs)
	{
		RaiseCallbackEvent(eventArgs);
	}

	/// <summary>Returns the result of a callback event that targets a control.</summary>
	/// <returns>The results of the callback.</returns>
	string ICallbackEventHandler.GetCallbackResult()
	{
		return GetCallbackResult();
	}

	/// <summary>Creates a collection to store child controls.</summary>
	/// <returns>Always returns an <see cref="T:System.Web.UI.EmptyControlCollection" />.</returns>
	protected override ControlCollection CreateControlCollection()
	{
		return new EmptyControlCollection(this);
	}

	/// <summary>Creates all the nodes based on the data source.</summary>
	protected internal override void PerformDataBinding()
	{
		base.PerformDataBinding();
		InitializeDataBindings();
		HierarchicalDataSourceView data = GetData(string.Empty);
		if (data != null)
		{
			Nodes.Clear();
			IHierarchicalEnumerable hEnumerable = data.Select();
			FillBoundChildrenRecursive(hEnumerable, Nodes);
		}
	}

	private void FillBoundChildrenRecursive(IHierarchicalEnumerable hEnumerable, TreeNodeCollection nodeCollection)
	{
		if (hEnumerable == null)
		{
			return;
		}
		foreach (object item in hEnumerable)
		{
			IHierarchyData hierarchyData = hEnumerable.GetHierarchyData(item);
			TreeNode treeNode = new TreeNode();
			nodeCollection.Add(treeNode);
			treeNode.Bind(hierarchyData);
			OnTreeNodeDataBound(new TreeNodeEventArgs(treeNode));
			if ((MaxDataBindDepth < 0 || treeNode.Depth != MaxDataBindDepth) && hierarchyData != null && hierarchyData.HasChildren)
			{
				IHierarchicalEnumerable children = hierarchyData.GetChildren();
				FillBoundChildrenRecursive(children, treeNode.ChildNodes);
			}
		}
	}

	/// <summary>Processes postback data for the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <param name="postDataKey">The key identifier for the control. </param>
	/// <param name="postCollection">The collection of all incoming name values. </param>
	/// <returns>
	///     <see langword="true" />, if the <see cref="T:System.Web.UI.WebControls.TreeView" /> control's state changes as a result of the postback event; otherwise, <see langword="false" />.</returns>
	protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		bool result = false;
		if (EnableClientScript && PopulateNodesFromClient)
		{
			string text = postCollection[ClientID + "_PopulatedStates"];
			if (text != null)
			{
				string[] array = text.Split(postDataSplitChars, StringSplitOptions.RemoveEmptyEntries);
				foreach (string path in array)
				{
					TreeNode treeNode = FindNodeByPos(path);
					if (treeNode != null && treeNode.PopulateOnDemand && !treeNode.Populated)
					{
						Page page = Page;
						if (page != null && page.IsCallback)
						{
							treeNode.Populated = true;
						}
						else
						{
							treeNode.Populate();
						}
					}
				}
			}
			result = true;
		}
		UnsetCheckStates(Nodes, postCollection);
		SetCheckStates(postCollection);
		if (EnableClientScript)
		{
			string text2 = postCollection[ClientID + "_ExpandStates"];
			if (text2 != null)
			{
				string[] array2 = text2.Split(postDataSplitChars, StringSplitOptions.RemoveEmptyEntries);
				UnsetExpandStates(Nodes, array2);
				SetExpandStates(array2);
			}
			else
			{
				UnsetExpandStates(Nodes, new string[0]);
			}
			result = true;
		}
		return result;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
		Page page = Page;
		if (page != null)
		{
			if (base.IsEnabled)
			{
				page.RegisterRequiresPostBack(this);
			}
			if (EnableClientScript && !page.ClientScript.IsClientScriptIncludeRegistered(typeof(TreeView), "TreeView.js"))
			{
				string webResourceUrl = page.ClientScript.GetWebResourceUrl(typeof(TreeView), "TreeView.js");
				page.ClientScript.RegisterClientScriptInclude(typeof(TreeView), "TreeView.js", webResourceUrl);
			}
		}
		string text = ClientID + "_data";
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("var {0} = new Object ();\n{0}.treeId = {1};\n{0}.uid = {2};\n{0}.showImage = {3};\n", text, ClientScriptManager.GetScriptLiteral(ClientID), ClientScriptManager.GetScriptLiteral(UniqueID), ClientScriptManager.GetScriptLiteral(ShowExpandCollapse));
		if (ShowExpandCollapse)
		{
			ImageStyle imageStyle = GetImageStyle();
			stringBuilder.AppendFormat("{0}.expandImage = {1};\n{0}.collapseImage = {2};\n", text, ClientScriptManager.GetScriptLiteral(GetNodeImageUrl("plus", imageStyle)), ClientScriptManager.GetScriptLiteral(GetNodeImageUrl("minus", imageStyle)));
			if (PopulateNodesFromClient)
			{
				stringBuilder.AppendFormat("{0}.noExpandImage = {1};\n", text, ClientScriptManager.GetScriptLiteral(GetNodeImageUrl("noexpand", imageStyle)));
			}
		}
		if (page == null)
		{
			return;
		}
		stringBuilder.AppendFormat("{0}.form = {1};\n{0}.PopulateNode = function (nodeId, nodeValue, nodeImageUrl, nodeNavigateUrl, nodeTarget) {{\n\t{2}.__theFormPostData = \"\";\n\t{2}.__theFormPostCollection = new Array ();\n\t{2}.WebForm_InitCallback ();\n\tTreeView_PopulateNode (this.uid, this.treeId, nodeId, nodeValue, nodeImageUrl, nodeNavigateUrl, nodeTarget)\n}};\n", text, page.theForm, page.WebFormScriptReference);
		stringBuilder.AppendFormat("{0}.populateFromClient = {1};\n{0}.expandAlt = {2};\n{0}.collapseAlt = {3};\n", text, ClientScriptManager.GetScriptLiteral(PopulateNodesFromClient), ClientScriptManager.GetScriptLiteral(GetNodeImageToolTip(expand: true, null)), ClientScriptManager.GetScriptLiteral(GetNodeImageToolTip(expand: false, null)));
		if (!page.IsPostBack)
		{
			SetNodesExpandedToDepthRecursive(Nodes);
		}
		bool enableClientScript = EnableClientScript;
		if (enableClientScript)
		{
			page.ClientScript.RegisterHiddenField(ClientID + "_ExpandStates", GetExpandStates());
			page.ClientScript.RegisterWebFormClientScript();
		}
		if (enableClientScript && PopulateNodesFromClient)
		{
			page.ClientScript.RegisterHiddenField(ClientID + "_PopulatedStates", "|");
		}
		EnsureStylesPrepared();
		if (hoverNodeStyle != null)
		{
			if (page.Header == null)
			{
				throw new InvalidOperationException("Using TreeView.HoverNodeStyle requires Page.Header to be non-null (e.g. <head runat=\"server\" />).");
			}
			RegisterStyle(HoverNodeStyle, HoverNodeLinkStyle);
			stringBuilder.AppendFormat("{0}.hoverClass = {1};\n{0}.hoverLinkClass = {2};\n", text, ClientScriptManager.GetScriptLiteral(HoverNodeStyle.RegisteredCssClass), ClientScriptManager.GetScriptLiteral(HoverNodeLinkStyle.RegisteredCssClass));
		}
		page.ClientScript.RegisterStartupScript(typeof(TreeView), UniqueID, stringBuilder.ToString(), addScriptTags: true);
		stringBuilder = null;
	}

	private void EnsureStylesPrepared()
	{
		if (!stylesPrepared)
		{
			stylesPrepared = true;
			PrepareStyles();
		}
	}

	private void PrepareStyles()
	{
		ControlLinkStyle.CopyTextStylesFrom(base.ControlStyle);
		RegisterStyle(ControlLinkStyle);
		if (nodeStyle != null)
		{
			RegisterStyle(NodeStyle, NodeLinkStyle);
		}
		if (rootNodeStyle != null)
		{
			RegisterStyle(RootNodeStyle, RootNodeLinkStyle);
		}
		if (parentNodeStyle != null)
		{
			RegisterStyle(ParentNodeStyle, ParentNodeLinkStyle);
		}
		if (leafNodeStyle != null)
		{
			RegisterStyle(LeafNodeStyle, LeafNodeLinkStyle);
		}
		if (levelStyles != null && levelStyles.Count > 0)
		{
			levelLinkStyles = new List<Style>(levelStyles.Count);
			foreach (Style levelStyle in levelStyles)
			{
				Style style = new Style();
				levelLinkStyles.Add(style);
				RegisterStyle(levelStyle, style);
			}
		}
		if (selectedNodeStyle != null)
		{
			RegisterStyle(SelectedNodeStyle, SelectedNodeLinkStyle);
		}
	}

	private void SetNodesExpandedToDepthRecursive(TreeNodeCollection nodes)
	{
		foreach (TreeNode node in nodes)
		{
			if (!node.Expanded.HasValue && (ExpandDepth < 0 || node.Depth < ExpandDepth))
			{
				node.Expanded = true;
			}
			SetNodesExpandedToDepthRecursive(node.ChildNodes);
		}
	}

	private string IncrementStyleClassName()
	{
		registeredStylesCounter++;
		return ClientID + "_" + registeredStylesCounter;
	}

	private void RegisterStyle(Style baseStyle, Style linkStyle)
	{
		linkStyle.CopyTextStylesFrom(baseStyle);
		linkStyle.BorderStyle = BorderStyle.None;
		linkStyle.AddCssClass(baseStyle.CssClass);
		baseStyle.Font.Reset();
		RegisterStyle(linkStyle);
		RegisterStyle(baseStyle);
	}

	private void RegisterStyle(Style baseStyle)
	{
		if (Page.Header != null)
		{
			string text = IncrementStyleClassName().Trim('_');
			baseStyle.SetRegisteredCssClass(text);
			Page.Header.StyleSheet.CreateStyleRule(baseStyle, this, "." + text);
		}
	}

	private string GetBindingKey(string dataMember, int depth)
	{
		return dataMember + " " + depth;
	}

	private void InitializeDataBindings()
	{
		if (dataBindings != null && dataBindings.Count > 0)
		{
			bindings = new Hashtable();
			{
				foreach (TreeNodeBinding dataBinding in dataBindings)
				{
					string bindingKey = GetBindingKey(dataBinding.DataMember, dataBinding.Depth);
					if (!bindings.ContainsKey(bindingKey))
					{
						bindings[bindingKey] = dataBinding;
					}
				}
				return;
			}
		}
		bindings = null;
	}

	internal TreeNodeBinding FindBindingForNode(string type, int depth)
	{
		if (bindings == null)
		{
			return null;
		}
		TreeNodeBinding treeNodeBinding = (TreeNodeBinding)bindings[GetBindingKey(type, depth)];
		if (treeNodeBinding != null)
		{
			return treeNodeBinding;
		}
		treeNodeBinding = (TreeNodeBinding)bindings[GetBindingKey(type, -1)];
		if (treeNodeBinding != null)
		{
			return treeNodeBinding;
		}
		treeNodeBinding = (TreeNodeBinding)bindings[GetBindingKey(string.Empty, depth)];
		if (treeNodeBinding != null)
		{
			return treeNodeBinding;
		}
		return (TreeNodeBinding)bindings[GetBindingKey(string.Empty, -1)];
	}

	internal void DecorateNode(TreeNode node)
	{
		if (node != null && (node.ImageUrl == null || node.ImageUrl.Length <= 0))
		{
			if (node.IsRootNode && rootNodeStyle != null)
			{
				node.ImageUrl = rootNodeStyle.ImageUrl;
			}
			else if (node.IsParentNode && parentNodeStyle != null)
			{
				node.ImageUrl = parentNodeStyle.ImageUrl;
			}
			else if (node.IsLeafNode && leafNodeStyle != null)
			{
				node.ImageUrl = leafNodeStyle.ImageUrl;
			}
		}
	}

	/// <summary>Renders the nodes in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream used to write content to a Web page. </param>
	protected internal override void RenderContents(HtmlTextWriter writer)
	{
		SiteMapDataSource siteMapDataSource = GetDataSource() as SiteMapDataSource;
		if (base.IsBoundUsingDataSourceID && siteMapDataSource != null)
		{
			IHierarchyData currentNode = siteMapDataSource.Provider.CurrentNode;
			if (currentNode != null)
			{
				activeSiteMapPath = currentNode.Path;
			}
		}
		ArrayList levelLines = new ArrayList();
		int count = Nodes.Count;
		for (int i = 0; i < count; i++)
		{
			RenderNode(writer, Nodes[i], 0, levelLines, i > 0, i < count - 1);
		}
	}

	/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> control.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		base.AddAttributesToRender(writer);
	}

	/// <summary>Renders the HTML opening tag of the control to the specified writer.  </summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
	public override void RenderBeginTag(HtmlTextWriter writer)
	{
		string skipLinkText = SkipLinkText;
		if (!string.IsNullOrEmpty(skipLinkText))
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "#" + ClientID + "_SkipLink");
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			ClientScriptManager clientScriptManager = new ClientScriptManager(null);
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, skipLinkText);
			writer.AddAttribute(HtmlTextWriterAttribute.Src, clientScriptManager.GetWebResourceUrl(typeof(SiteMapPath), "transparent.gif"));
			writer.AddAttribute(HtmlTextWriterAttribute.Height, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Width, "0");
			writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}
		base.RenderBeginTag(writer);
	}

	/// <summary>Renders the HTML closing tag of the control to the specified writer.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
	public override void RenderEndTag(HtmlTextWriter writer)
	{
		base.RenderEndTag(writer);
		if (!string.IsNullOrEmpty(SkipLinkText))
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_SkipLink");
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.RenderEndTag();
		}
	}

	private void RenderNode(HtmlTextWriter writer, TreeNode node, int level, ArrayList levelLines, bool hasPrevious, bool hasNext)
	{
		DecorateNode(node);
		bool flag = EnableClientScript && (object)base.Events[TreeNodeCollapsed] == null && (object)base.Events[TreeNodeExpanded] == null;
		ImageStyle imageStyle = GetImageStyle();
		bool flag2 = node.Expanded.HasValue && node.Expanded.Value;
		if (flag && !flag2)
		{
			flag2 = !node.PopulateOnDemand || node.Populated;
		}
		bool flag3 = ((!flag2) ? ((node.PopulateOnDemand && !node.Populated) || node.ChildNodes.Count > 0) : (node.ChildNodes.Count > 0));
		writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0", fEncode: false);
		writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0", fEncode: false);
		writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0");
		writer.RenderBeginTag(HtmlTextWriterTag.Table);
		writer.RenderBeginTag(HtmlTextWriterTag.Tr);
		string nodeImageUrl = GetNodeImageUrl("i", imageStyle);
		for (int i = 0; i < level; i++)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, NodeIndent + "px");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "1px");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (ShowLines && levelLines[i] != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Src, nodeImageUrl);
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty, fEncode: false);
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}
		bool showExpandCollapse = ShowExpandCollapse;
		bool showLines = ShowLines;
		if (showExpandCollapse || showLines)
		{
			bool flag4 = false;
			string value = string.Empty;
			string text = string.Empty;
			if (showLines)
			{
				text = ((hasPrevious && hasNext) ? "t" : ((hasPrevious && !hasNext) ? "l" : ((!(!hasPrevious && hasNext)) ? "dash" : "r")));
			}
			if (showExpandCollapse)
			{
				if (flag3)
				{
					flag4 = true;
					text = ((!node.Expanded.HasValue || !node.Expanded.Value) ? (text + "plus") : (text + "minus"));
					value = GetNodeImageToolTip(!node.Expanded.HasValue || !node.Expanded.Value, node.Text);
				}
				else if (!showLines)
				{
					text = "noexpand";
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				nodeImageUrl = GetNodeImageUrl(text, imageStyle);
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				if (flag4)
				{
					if (!flag || (!PopulateNodesFromClient && node.PopulateOnDemand && !node.Populated))
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Href, GetClientEvent(node, "ec"));
					}
					else
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Href, GetClientExpandEvent(node));
					}
					writer.RenderBeginTag(HtmlTextWriterTag.A);
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, value);
				if (flag4 && flag)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Id, GetNodeClientId(node, "img"));
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Src, nodeImageUrl);
				if (flag4)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0");
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				if (flag4)
				{
					writer.RenderEndTag();
				}
				writer.RenderEndTag();
			}
		}
		string value2 = ((node.ImageUrl.Length > 0) ? ResolveClientUrl(node.ImageUrl) : null);
		if (string.IsNullOrEmpty(value2) && imageStyle != null)
		{
			if (imageStyle.RootIcon != null && node.IsRootNode)
			{
				value2 = GetNodeIconUrl(imageStyle.RootIcon);
			}
			else if (imageStyle.ParentIcon != null && node.IsParentNode)
			{
				value2 = GetNodeIconUrl(imageStyle.ParentIcon);
			}
			else if (imageStyle.LeafIcon != null && node.IsLeafNode)
			{
				value2 = GetNodeIconUrl(imageStyle.LeafIcon);
			}
		}
		if (level < LevelStyles.Count && LevelStyles[level].ImageUrl != null)
		{
			value2 = ResolveClientUrl(LevelStyles[level].ImageUrl);
		}
		if (!string.IsNullOrEmpty(value2))
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, "-1");
			BeginNodeTag(writer, node, flag);
			writer.AddAttribute(HtmlTextWriterAttribute.Src, value2);
			writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, node.ImageToolTip);
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}
		if (!NodeWrap)
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
		}
		bool flag5 = node == SelectedNode && selectedNodeStyle != null;
		if (!flag5 && selectedNodeStyle != null && !string.IsNullOrEmpty(activeSiteMapPath))
		{
			flag5 = string.Compare(activeSiteMapPath, node.NavigateUrl, RuntimeHelpers.StringComparison) == 0;
		}
		AddNodeStyle(writer, node, level, flag5);
		if (EnableClientScript)
		{
			writer.AddAttribute("onmouseout", "TreeView_UnhoverNode(this)", fEndode: false);
			writer.AddAttribute("onmouseover", "TreeView_HoverNode('" + ClientID + "', this)");
		}
		writer.RenderBeginTag(HtmlTextWriterTag.Td);
		if (node.ShowCheckBoxInternal)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Name, ClientID + "_cs_" + node.Path);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox", fEncode: false);
			string toolTip = node.ToolTip;
			if (!string.IsNullOrEmpty(toolTip))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, toolTip);
			}
			if (node.Checked)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked", fEncode: false);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}
		node.BeginRenderText(writer);
		if (flag)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, GetNodeClientId(node, "txt"));
		}
		AddNodeLinkStyle(writer, node, level, flag5);
		BeginNodeTag(writer, node, flag);
		writer.Write(node.Text);
		writer.RenderEndTag();
		node.EndRenderText(writer);
		writer.RenderEndTag();
		writer.RenderEndTag();
		writer.RenderEndTag();
		if (!flag3)
		{
			return;
		}
		if (level >= levelLines.Count)
		{
			if (hasNext)
			{
				levelLines.Add(this);
			}
			else
			{
				levelLines.Add(null);
			}
		}
		else if (hasNext)
		{
			levelLines[level] = this;
		}
		else
		{
			levelLines[level] = null;
		}
		if (flag)
		{
			if (!node.Expanded.HasValue || !node.Expanded.Value)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			else
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "block");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Id, GetNodeClientId(node, null));
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			if (flag2)
			{
				AddChildrenPadding(writer, node);
				int count = node.ChildNodes.Count;
				for (int j = 0; j < count; j++)
				{
					RenderNode(writer, node.ChildNodes[j], level + 1, levelLines, hasPrevious: true, j < count - 1);
				}
				if (hasNext)
				{
					AddChildrenPadding(writer, node);
				}
			}
			writer.RenderEndTag();
		}
		else if (flag2)
		{
			AddChildrenPadding(writer, node);
			int count2 = node.ChildNodes.Count;
			for (int k = 0; k < count2; k++)
			{
				RenderNode(writer, node.ChildNodes[k], level + 1, levelLines, hasPrevious: true, k < count2 - 1);
			}
			if (hasNext)
			{
				AddChildrenPadding(writer, node);
			}
		}
	}

	private void AddChildrenPadding(HtmlTextWriter writer, TreeNode node)
	{
		int depth = node.Depth;
		Unit unit = Unit.Empty;
		if (levelStyles != null && depth < levelStyles.Count)
		{
			unit = levelStyles[depth].ChildNodesPadding;
		}
		if (unit.IsEmpty && nodeStyle != null)
		{
			unit = nodeStyle.ChildNodesPadding;
		}
		double value;
		if (!unit.IsEmpty && (value = unit.Value) != 0.0 && unit.Type == UnitType.Pixel)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.AddAttribute(HtmlTextWriterAttribute.Height, ((int)value).ToString(), fEncode: false);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}
	}

	private void RenderMenuItemSpacing(HtmlTextWriter writer, Unit itemSpacing)
	{
		writer.RenderBeginTag(HtmlTextWriterTag.Tr);
		writer.RenderBeginTag(HtmlTextWriterTag.Td);
		writer.RenderEndTag();
		writer.RenderEndTag();
	}

	private Unit GetNodeSpacing(TreeNode node)
	{
		if (node.Selected && selectedNodeStyle != null && selectedNodeStyle.NodeSpacing != Unit.Empty)
		{
			return selectedNodeStyle.NodeSpacing;
		}
		if (levelStyles != null && node.Depth < levelStyles.Count && levelStyles[node.Depth].NodeSpacing != Unit.Empty)
		{
			return levelStyles[node.Depth].NodeSpacing;
		}
		if (node.IsLeafNode)
		{
			if (leafNodeStyle != null && leafNodeStyle.NodeSpacing != Unit.Empty)
			{
				return leafNodeStyle.NodeSpacing;
			}
		}
		else if (node.IsRootNode)
		{
			if (rootNodeStyle != null && rootNodeStyle.NodeSpacing != Unit.Empty)
			{
				return rootNodeStyle.NodeSpacing;
			}
		}
		else if (node.IsParentNode && parentNodeStyle != null && parentNodeStyle.NodeSpacing != Unit.Empty)
		{
			return parentNodeStyle.NodeSpacing;
		}
		if (nodeStyle != null)
		{
			return nodeStyle.NodeSpacing;
		}
		return Unit.Empty;
	}

	private void AddNodeStyle(HtmlTextWriter writer, TreeNode node, int level, bool nodeIsSelected)
	{
		TreeNodeStyle treeNodeStyle = new TreeNodeStyle();
		if (Page.Header != null)
		{
			if (nodeStyle != null)
			{
				treeNodeStyle.PrependCssClass(nodeStyle.RegisteredCssClass);
				treeNodeStyle.PrependCssClass(nodeStyle.CssClass);
			}
			if (node.IsLeafNode)
			{
				if (leafNodeStyle != null)
				{
					treeNodeStyle.PrependCssClass(leafNodeStyle.RegisteredCssClass);
					treeNodeStyle.PrependCssClass(leafNodeStyle.CssClass);
				}
			}
			else if (node.IsRootNode)
			{
				if (rootNodeStyle != null)
				{
					treeNodeStyle.PrependCssClass(rootNodeStyle.RegisteredCssClass);
					treeNodeStyle.PrependCssClass(rootNodeStyle.CssClass);
				}
			}
			else if (node.IsParentNode && parentNodeStyle != null)
			{
				treeNodeStyle.AddCssClass(parentNodeStyle.RegisteredCssClass);
				treeNodeStyle.AddCssClass(parentNodeStyle.CssClass);
			}
			if (levelStyles != null && levelStyles.Count > level)
			{
				treeNodeStyle.PrependCssClass(levelStyles[level].RegisteredCssClass);
				treeNodeStyle.PrependCssClass(levelStyles[level].CssClass);
			}
			if (nodeIsSelected)
			{
				treeNodeStyle.AddCssClass(selectedNodeStyle.RegisteredCssClass);
				treeNodeStyle.AddCssClass(selectedNodeStyle.CssClass);
			}
		}
		else
		{
			if (nodeStyle != null)
			{
				treeNodeStyle.CopyFrom(nodeStyle);
			}
			if (node.IsLeafNode)
			{
				if (leafNodeStyle != null)
				{
					treeNodeStyle.CopyFrom(leafNodeStyle);
				}
			}
			else if (node.IsRootNode)
			{
				if (rootNodeStyle != null)
				{
					treeNodeStyle.CopyFrom(rootNodeStyle);
				}
			}
			else if (node.IsParentNode && parentNodeStyle != null)
			{
				treeNodeStyle.CopyFrom(parentNodeStyle);
			}
			if (levelStyles != null && levelStyles.Count > level)
			{
				treeNodeStyle.CopyFrom(levelStyles[level]);
			}
			if (nodeIsSelected)
			{
				treeNodeStyle.CopyFrom(selectedNodeStyle);
			}
		}
		treeNodeStyle.AddAttributesToRender(writer);
	}

	private void AddNodeLinkStyle(HtmlTextWriter writer, TreeNode node, int level, bool nodeIsSelected)
	{
		Style style = new Style();
		bool flag = false;
		if (Page.Header != null)
		{
			style.AddCssClass(ControlLinkStyle.RegisteredCssClass);
			if (nodeStyle != null)
			{
				style.AddCssClass(nodeLinkStyle.CssClass);
				style.AddCssClass(nodeLinkStyle.RegisteredCssClass);
			}
			if (levelLinkStyles != null && levelLinkStyles.Count > level)
			{
				style.AddCssClass(levelLinkStyles[level].CssClass);
				style.AddCssClass(levelLinkStyles[level].RegisteredCssClass);
				flag = true;
			}
			if (node.IsLeafNode)
			{
				if (leafNodeStyle != null)
				{
					style.AddCssClass(leafNodeLinkStyle.CssClass);
					style.AddCssClass(leafNodeLinkStyle.RegisteredCssClass);
				}
			}
			else if (node.IsRootNode)
			{
				if (rootNodeStyle != null)
				{
					style.AddCssClass(rootNodeLinkStyle.CssClass);
					style.AddCssClass(rootNodeLinkStyle.RegisteredCssClass);
				}
			}
			else if (node.IsParentNode && parentNodeStyle != null)
			{
				style.AddCssClass(parentNodeLinkStyle.CssClass);
				style.AddCssClass(parentNodeLinkStyle.RegisteredCssClass);
			}
			if (nodeIsSelected)
			{
				style.AddCssClass(selectedNodeLinkStyle.CssClass);
				style.AddCssClass(selectedNodeLinkStyle.RegisteredCssClass);
			}
		}
		else
		{
			style.CopyFrom(ControlLinkStyle);
			if (nodeStyle != null)
			{
				style.CopyFrom(nodeLinkStyle);
			}
			if (levelLinkStyles != null && levelLinkStyles.Count > level)
			{
				style.CopyFrom(levelLinkStyles[level]);
				flag = true;
			}
			if (node.IsLeafNode)
			{
				if (node.IsLeafNode && leafNodeStyle != null)
				{
					style.CopyFrom(leafNodeLinkStyle);
				}
			}
			else if (node.IsRootNode)
			{
				if (node.IsRootNode && rootNodeStyle != null)
				{
					style.CopyFrom(rootNodeLinkStyle);
				}
			}
			else if (node.IsParentNode && node.IsParentNode && parentNodeStyle != null)
			{
				style.CopyFrom(parentNodeLinkStyle);
			}
			if (nodeIsSelected)
			{
				style.CopyFrom(selectedNodeLinkStyle);
			}
			style.AlwaysRenderTextDecoration = true;
		}
		if (flag)
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
			writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "1em");
		}
		style.AddAttributesToRender(writer);
	}

	private void BeginNodeTag(HtmlTextWriter writer, TreeNode node, bool clientExpand)
	{
		if (node.ToolTip.Length > 0)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Title, node.ToolTip);
		}
		string navigateUrl = node.NavigateUrl;
		if (!string.IsNullOrEmpty(navigateUrl))
		{
			string text = ((node.Target.Length > 0) ? node.Target : Target);
			string value = ResolveClientUrl(navigateUrl);
			writer.AddAttribute(HtmlTextWriterAttribute.Href, value);
			if (text.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Target, text);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.A);
		}
		else if (node.SelectAction != TreeNodeSelectAction.None)
		{
			if (node.SelectAction == TreeNodeSelectAction.Expand && clientExpand)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, GetClientExpandEvent(node));
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, GetClientEvent(node, "sel"));
			}
			writer.RenderBeginTag(HtmlTextWriterTag.A);
		}
		else
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
		}
	}

	private string GetNodeImageToolTip(bool expand, string txt)
	{
		if (expand)
		{
			string expandImageToolTip = ExpandImageToolTip;
			if (!string.IsNullOrEmpty(expandImageToolTip))
			{
				return string.Format(expandImageToolTip, HttpUtility.HtmlAttributeEncode(txt));
			}
			if (txt != null)
			{
				return "Expand " + HttpUtility.HtmlAttributeEncode(txt);
			}
			return "Expand {0}";
		}
		string collapseImageToolTip = CollapseImageToolTip;
		if (!string.IsNullOrEmpty(collapseImageToolTip))
		{
			return string.Format(collapseImageToolTip, HttpUtility.HtmlAttributeEncode(txt));
		}
		if (txt != null)
		{
			return "Collapse " + HttpUtility.HtmlAttributeEncode(txt);
		}
		return "Collapse {0}";
	}

	private string GetNodeClientId(TreeNode node, string sufix)
	{
		return ClientID + "_" + node.Path + ((sufix != null) ? ("_" + sufix) : string.Empty);
	}

	private string GetNodeImageUrl(string shape, ImageStyle imageStyle)
	{
		if (ShowLines)
		{
			if (!string.IsNullOrEmpty(LineImagesFolder))
			{
				return ResolveClientUrl(LineImagesFolder + "/" + shape + ".gif");
			}
		}
		else
		{
			if (imageStyle != null)
			{
				switch (shape)
				{
				case "plus":
					if (!string.IsNullOrEmpty(imageStyle.Expand))
					{
						return GetNodeIconUrl(imageStyle.Expand);
					}
					break;
				case "minus":
					if (!string.IsNullOrEmpty(imageStyle.Collapse))
					{
						return GetNodeIconUrl(imageStyle.Collapse);
					}
					break;
				case "noexpand":
					if (!string.IsNullOrEmpty(imageStyle.NoExpand))
					{
						return GetNodeIconUrl(imageStyle.NoExpand);
					}
					break;
				}
			}
			else
			{
				switch (shape)
				{
				case "plus":
					if (!string.IsNullOrEmpty(ExpandImageUrl))
					{
						return ResolveClientUrl(ExpandImageUrl);
					}
					break;
				case "minus":
					if (!string.IsNullOrEmpty(CollapseImageUrl))
					{
						return ResolveClientUrl(CollapseImageUrl);
					}
					break;
				case "noexpand":
					if (!string.IsNullOrEmpty(NoExpandImageUrl))
					{
						return ResolveClientUrl(NoExpandImageUrl);
					}
					break;
				}
			}
			if (!string.IsNullOrEmpty(LineImagesFolder))
			{
				return ResolveClientUrl(LineImagesFolder + "/" + shape + ".gif");
			}
		}
		return Page.ClientScript.GetWebResourceUrl(typeof(TreeView), "TreeView_" + shape + ".gif");
	}

	private string GetNodeIconUrl(string icon)
	{
		return Page.ClientScript.GetWebResourceUrl(typeof(TreeView), icon + ".gif");
	}

	private string GetClientEvent(TreeNode node, string ev)
	{
		return Page.ClientScript.GetPostBackClientHyperlink(this, ev + "|" + node.Path, registerForEventValidation: true);
	}

	private string GetClientExpandEvent(TreeNode node)
	{
		return string.Format("javascript:TreeView_ToggleExpand ('{0}','{1}','{2}','{3}','{4}','{5}')", ClientID, node.Path, HttpUtility.HtmlAttributeEncode(node.Value).Replace("'", "\\'").Replace("|", "U+007C"), HttpUtility.HtmlAttributeEncode(node.ImageUrl).Replace("'", "\\'").Replace("|", "U+007c"), HttpUtility.HtmlAttributeEncode(node.NavigateUrl).Replace("'", "\\'").Replace("|", "U+007C"), HttpUtility.HtmlAttributeEncode(node.Target).Replace("'", "\\'").Replace("|", "U+007C"));
	}

	private TreeNode FindNodeByPos(string path)
	{
		string[] array = path.Split('_');
		TreeNode treeNode = null;
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			int num = int.Parse(array2[i]);
			if (treeNode == null)
			{
				if (num >= Nodes.Count)
				{
					return null;
				}
				treeNode = Nodes[num];
			}
			else
			{
				if (num >= treeNode.ChildNodes.Count)
				{
					return null;
				}
				treeNode = treeNode.ChildNodes[num];
			}
		}
		return treeNode;
	}

	private void UnsetCheckStates(TreeNodeCollection col, NameValueCollection states)
	{
		foreach (TreeNode item in col)
		{
			if (item.ShowCheckBoxInternal && item.Checked && (states == null || states[ClientID + "_cs_" + item.Path] == null))
			{
				item.Checked = false;
			}
			if (item.HasChildData)
			{
				UnsetCheckStates(item.ChildNodes, states);
			}
		}
	}

	private void SetCheckStates(NameValueCollection states)
	{
		if (states == null)
		{
			return;
		}
		string text = ClientID + "_cs_";
		foreach (string state in states)
		{
			if (state.StartsWith(text, StringComparison.Ordinal))
			{
				string path = state.Substring(text.Length);
				TreeNode treeNode = FindNodeByPos(path);
				if (treeNode != null && !treeNode.Checked)
				{
					treeNode.Checked = true;
				}
			}
		}
	}

	private void UnsetExpandStates(TreeNodeCollection col, string[] states)
	{
		foreach (TreeNode item in col)
		{
			if (item.Expanded.HasValue && item.Expanded.Value && Array.IndexOf(states, item.Path) == -1)
			{
				item.Expanded = false;
			}
			if (item.HasChildData)
			{
				UnsetExpandStates(item.ChildNodes, states);
			}
		}
	}

	private void SetExpandStates(string[] states)
	{
		foreach (string text in states)
		{
			if (!string.IsNullOrEmpty(text))
			{
				TreeNode treeNode = FindNodeByPos(text);
				if (treeNode != null)
				{
					treeNode.Expanded = true;
				}
			}
		}
	}

	private string GetExpandStates()
	{
		StringBuilder stringBuilder = new StringBuilder("|");
		foreach (TreeNode node in Nodes)
		{
			GetExpandStates(stringBuilder, node);
		}
		return stringBuilder.ToString();
	}

	private void GetExpandStates(StringBuilder sb, TreeNode node)
	{
		if (node.Expanded.HasValue && node.Expanded.Value)
		{
			sb.Append(node.Path);
			sb.Append('|');
		}
		if (!node.HasChildData)
		{
			return;
		}
		foreach (TreeNode childNode in node.ChildNodes)
		{
			GetExpandStates(sb, childNode);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeView" /> class.</summary>
	public TreeView()
	{
	}
}
