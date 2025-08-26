using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;

namespace System.Web.UI.WebControls;

/// <summary>Represents a node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
[ParseChildren(true, "ChildNodes")]
public class TreeNode : IStateManager, ICloneable
{
	private StateBag ViewState = new StateBag();

	private TreeNodeCollection nodes;

	private bool marked;

	private TreeView tree;

	private TreeNode parent;

	private int index;

	private string path;

	private int depth = -1;

	private object dataItem;

	private IHierarchyData hierarchyData;

	private bool gotBinding;

	private TreeNodeBinding binding;

	private PropertyDescriptorCollection boundProperties;

	private bool populating;

	private bool hadChildrenBeforePopulating;

	/// <summary>Gets the depth of the node.</summary>
	/// <returns>The depth of the node.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public int Depth
	{
		get
		{
			if (depth != -1)
			{
				return depth;
			}
			depth = 0;
			for (TreeNode treeNode = parent; treeNode != null; treeNode = treeNode.parent)
			{
				depth++;
			}
			return depth;
		}
	}

	internal TreeView Tree
	{
		get
		{
			return tree;
		}
		set
		{
			if (SelectedFlag)
			{
				value?.SetSelectedNode(this, loading: false);
			}
			tree = value;
			if (nodes != null)
			{
				nodes.SetTree(tree);
			}
			ResetPathData();
			if (PopulateOnDemand && !Populated && Expanded.HasValue && Expanded.Value)
			{
				Populate();
			}
		}
	}

	/// <summary>Gets a value that indicates whether the node was created through data binding.</summary>
	/// <returns>
	///     <see langword="true" /> if the node was created through data binding; otherwise, <see langword="false" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[DefaultValue(false)]
	[Browsable(false)]
	public bool DataBound
	{
		get
		{
			if (ViewState["DataBound"] != null)
			{
				return (bool)ViewState["DataBound"];
			}
			return false;
		}
		private set
		{
			ViewState["DataBound"] = value;
		}
	}

	/// <summary>Gets the data item that is bound to the control.</summary>
	/// <returns>A <see cref="T:System.Object" /> that represents the data item that is bound to the control. The default value is <see langword="null" />, which indicates that the node is not bound to any data item.</returns>
	[DefaultValue(null)]
	[Browsable(false)]
	public object DataItem => dataItem;

	/// <summary>Gets the path to the data bound to the node.</summary>
	/// <returns>The path to the data bound to the node. This value comes from the hierarchical data source control to which the <see cref="T:System.Web.UI.WebControls.TreeView" /> control is bound. The default value is an empty string ("").</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[DefaultValue("")]
	[Browsable(false)]
	public string DataPath
	{
		get
		{
			if (ViewState["DataPath"] != null)
			{
				return (string)ViewState["DataPath"];
			}
			return string.Empty;
		}
		private set
		{
			ViewState["DataPath"] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the node's check box is selected.</summary>
	/// <returns>
	///     <see langword="true" /> if the node's check box is selected; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	public bool Checked
	{
		get
		{
			object obj = ViewState["Checked"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return false;
		}
		set
		{
			ViewState["Checked"] = value;
			if (tree != null)
			{
				tree.NotifyCheckChanged(this);
			}
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> collection that contains the first-level child nodes of the current node.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> that contains the first-level child nodes of the current node.</returns>
	[DefaultValue(null)]
	[MergableProperty(false)]
	[Browsable(false)]
	[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
	public TreeNodeCollection ChildNodes
	{
		get
		{
			if (nodes == null)
			{
				nodes = new TreeNodeCollection(this);
				if (IsTrackingViewState)
				{
					((IStateManager)nodes).TrackViewState();
				}
			}
			return nodes;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the node is expanded.</summary>
	/// <returns>
	///     <see langword="true" /> if the node is expanded, <see langword="false" /> if the node is not expanded, or <see langword="null" />.</returns>
	[DefaultValue(null)]
	public bool? Expanded
	{
		get
		{
			return (bool?)ViewState["Expanded"];
		}
		set
		{
			if ((bool?)ViewState["Expanded"] != value)
			{
				ViewState["Expanded"] = value;
				if (tree != null)
				{
					tree.NotifyExpandedChanged(this);
				}
				if (PopulateOnDemand && !Populated && value.HasValue && value.Value)
				{
					Populate();
				}
			}
		}
	}

	/// <summary>Gets or sets the ToolTip text for the image displayed next to a node.</summary>
	/// <returns>The ToolTip text for the image displayed next to a node. The default is an empty string (""), which indicates that this property is not set.</returns>
	[Localizable(true)]
	[DefaultValue("")]
	public string ImageToolTip
	{
		get
		{
			object obj = ViewState["ImageToolTip"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			ViewState["ImageToolTip"] = value;
		}
	}

	/// <summary>Gets or sets the URL to an image that is displayed next to the node.</summary>
	/// <returns>The URL to a custom image that is displayed next to the node. The default value is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public string ImageUrl
	{
		get
		{
			object obj = ViewState["ImageUrl"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			ViewState["ImageUrl"] = value;
		}
	}

	/// <summary>Gets or sets the URL to navigate to when the node is clicked.</summary>
	/// <returns>The URL to navigate to when the node is clicked. The default value is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public string NavigateUrl
	{
		get
		{
			object obj = ViewState["NavigateUrl"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			ViewState["NavigateUrl"] = value;
		}
	}

	internal bool HadChildrenBeforePopulating
	{
		get
		{
			return hadChildrenBeforePopulating;
		}
		set
		{
			if (!populating)
			{
				hadChildrenBeforePopulating = value;
			}
		}
	}

	/// <summary>Gets or sets a value that indicates whether the node is populated dynamically.</summary>
	/// <returns>
	///     <see langword="true" /> to populate the node dynamically; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	public bool PopulateOnDemand
	{
		get
		{
			object obj = ViewState["PopulateOnDemand"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return false;
		}
		set
		{
			ViewState["PopulateOnDemand"] = value;
			if (value && nodes != null && nodes.Count > 0)
			{
				HadChildrenBeforePopulating = true;
			}
			else
			{
				HadChildrenBeforePopulating = false;
			}
		}
	}

	/// <summary>Gets or sets the event or events to raise when a node is selected.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TreeNodeSelectAction" /> values. The default is <see langword="TreeNodeSelectAction.Select" />.</returns>
	[DefaultValue(TreeNodeSelectAction.Select)]
	public TreeNodeSelectAction SelectAction
	{
		get
		{
			object obj = ViewState["SelectAction"];
			if (obj != null)
			{
				return (TreeNodeSelectAction)obj;
			}
			return TreeNodeSelectAction.Select;
		}
		set
		{
			ViewState["SelectAction"] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether a check box is displayed next to the node.</summary>
	/// <returns>
	///     <see langword="true" /> to display the check box; otherwise, <see langword="false" />.</returns>
	[DefaultValue(null)]
	public bool? ShowCheckBox
	{
		get
		{
			return (bool?)ViewState["ShowCheckBox"];
		}
		set
		{
			ViewState["ShowCheckBox"] = value;
		}
	}

	internal bool ShowCheckBoxInternal
	{
		get
		{
			if (ShowCheckBox.HasValue)
			{
				return ShowCheckBox.Value;
			}
			if (Tree.ShowCheckBoxes != TreeNodeTypes.All && ((Tree.ShowCheckBoxes & TreeNodeTypes.Leaf) <= TreeNodeTypes.None || !IsLeafNode) && ((Tree.ShowCheckBoxes & TreeNodeTypes.Parent) <= TreeNodeTypes.None || !IsParentNode || Parent == null))
			{
				if ((Tree.ShowCheckBoxes & TreeNodeTypes.Root) > TreeNodeTypes.None && Parent == null)
				{
					return ChildNodes.Count > 0;
				}
				return false;
			}
			return true;
		}
	}

	/// <summary>Gets or sets the target window or frame in which to display the Web page content associated with a node.</summary>
	/// <returns>The target window or frame in which to display the linked Web page content. Values must begin with a letter in the range of A through Z (case-insensitive), except for certain special values that begin with an underscore, as shown in the following table.Target value Description 
	///             <see langword="_blank" />
	///           Renders the content in a new window without frames. 
	///             <see langword="_parent" />
	///           Renders the content in the immediate frameset parent. 
	///             <see langword="_search" />
	///           Renders the content in the search pane.
	///             <see langword="_self" />
	///           Renders the content in the frame with focus. 
	///             <see langword="_top" />
	///           Renders the content in the full window without frames. Check your browser documentation to determine whether the <see langword="_search" /> value is supported. For example, Microsoft Internet Explorer 5.0 and later support the <see langword="_search" /> target value.The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
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
			return string.Empty;
		}
		set
		{
			ViewState["Target"] = value;
		}
	}

	/// <summary>Gets or sets the text displayed for the node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	/// <returns>The text displayed for the node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control. The default is an empty string ("").</returns>
	[Localizable(true)]
	[DefaultValue("")]
	[WebSysDescription("The display text of the tree node.")]
	public string Text
	{
		get
		{
			object obj = ViewState["Text"];
			if (obj == null)
			{
				obj = ViewState["Value"];
			}
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			ViewState["Text"] = value;
		}
	}

	/// <summary>Gets or sets the ToolTip text for the node.</summary>
	/// <returns>The ToolTip text for the node. The default is an empty string ("").</returns>
	[Localizable(true)]
	[DefaultValue("")]
	public string ToolTip
	{
		get
		{
			object obj = ViewState["ToolTip"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			ViewState["ToolTip"] = value;
		}
	}

	/// <summary>Gets or sets a non-displayed value used to store any additional data about the node, such as data used for handling postback events.</summary>
	/// <returns>Supplemental data about the node that is not displayed. The default value is an empty string ("").</returns>
	[Localizable(true)]
	[DefaultValue("")]
	public string Value
	{
		get
		{
			object obj = ViewState["Value"];
			if (obj == null)
			{
				obj = ViewState["Text"];
			}
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			ViewState["Value"] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the node is selected.</summary>
	/// <returns>
	///     <see langword="true" /> if the node is selected; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	public bool Selected
	{
		get
		{
			return SelectedFlag;
		}
		set
		{
			SelectedFlag = value;
			if (tree != null)
			{
				if (!value && tree.SelectedNode == this)
				{
					tree.SetSelectedNode(null, loading: false);
				}
				else if (value)
				{
					tree.SetSelectedNode(this, loading: false);
				}
			}
		}
	}

	internal virtual bool SelectedFlag
	{
		get
		{
			object obj = ViewState["Selected"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return false;
		}
		set
		{
			ViewState["Selected"] = value;
		}
	}

	/// <summary>Gets the parent node of the current node.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNode" /> that represents the parent node of the current node.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public TreeNode Parent => parent;

	/// <summary>Gets the path from the root node to the current node.</summary>
	/// <returns>A delimiter-separated list of node values that form a path from the root node to the current node.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public string ValuePath
	{
		get
		{
			if (tree == null)
			{
				return Value;
			}
			StringBuilder stringBuilder = new StringBuilder(Value);
			for (TreeNode treeNode = parent; treeNode != null; treeNode = treeNode.Parent)
			{
				stringBuilder.Insert(0, tree.PathSeparator);
				stringBuilder.Insert(0, treeNode.Value);
			}
			return stringBuilder.ToString();
		}
	}

	internal int Index
	{
		get
		{
			return index;
		}
		set
		{
			index = value;
			ResetPathData();
		}
	}

	internal string Path
	{
		get
		{
			if (path != null)
			{
				return path;
			}
			StringBuilder stringBuilder = new StringBuilder(index.ToString());
			for (TreeNode treeNode = parent; treeNode != null; treeNode = treeNode.Parent)
			{
				stringBuilder.Insert(0, '_');
				stringBuilder.Insert(0, treeNode.Index.ToString());
			}
			path = stringBuilder.ToString();
			return path;
		}
	}

	internal bool Populated
	{
		get
		{
			object obj = ViewState["Populated"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return false;
		}
		set
		{
			ViewState["Populated"] = value;
		}
	}

	internal bool HasChildData => nodes != null;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" />.</summary>
	/// <returns>A value that indicates whether the node is saving changes to its view state. </returns>
	bool IStateManager.IsTrackingViewState => IsTrackingViewState;

	/// <summary>Gets a value that indicates whether the node is saving changes to its view state. </summary>
	/// <returns>
	///     <see langword="true" /> if the control is marked to save its state; otherwise, <see langword="false" />. </returns>
	protected bool IsTrackingViewState => marked;

	internal bool IsParentNode
	{
		get
		{
			if (ChildNodes.Count <= 0)
			{
				if (PopulateOnDemand)
				{
					return !Populated;
				}
				return false;
			}
			return true;
		}
	}

	internal bool IsLeafNode => !IsParentNode;

	internal bool IsRootNode => Depth == 0;

	internal TreeNode(TreeView tree)
	{
		Tree = tree;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class without text or a value.</summary>
	public TreeNode()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class using the specified text.</summary>
	/// <param name="text">The text that is displayed in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control for the node. </param>
	public TreeNode(string text)
	{
		Text = text;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class using the specified text and value.</summary>
	/// <param name="text">The text that is displayed in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control for the node. </param>
	/// <param name="value">The supplemental data associated with the node, such as data used for handling postback events. </param>
	public TreeNode(string text, string value)
	{
		Text = text;
		Value = value;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class using the specified text, value, and image URL.</summary>
	/// <param name="text">The text that is displayed in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control for the node. </param>
	/// <param name="value">The supplemental data associated with the node, such as data used for handling postback events. </param>
	/// <param name="imageUrl">The URL to an image that is displayed next to the node. </param>
	public TreeNode(string text, string value, string imageUrl)
	{
		Text = text;
		Value = value;
		ImageUrl = imageUrl;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class using the specified text, value, image URL, navigation URL, and target.</summary>
	/// <param name="text">The text that is displayed in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control for the node. </param>
	/// <param name="value">The supplemental data associated with the node, such as data used for handling postback events. </param>
	/// <param name="imageUrl">The URL to an image that is displayed next to the node. </param>
	/// <param name="navigateUrl">The URL to link to when the node is clicked. </param>
	/// <param name="target">The target window or frame in which to display the Web page content linked to when the node is clicked. </param>
	public TreeNode(string text, string value, string imageUrl, string navigateUrl, string target)
	{
		Text = text;
		Value = value;
		ImageUrl = imageUrl;
		NavigateUrl = navigateUrl;
		Target = target;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class using the specified owner.</summary>
	/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.TreeView" /> that will contain the new <see cref="T:System.Web.UI.WebControls.TreeNode" />.</param>
	/// <param name="isRoot">
	///       <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.TreeNode" /> is a root node; otherwise, <see langword="false" />.</param>
	[MonoTODO("Not implemented")]
	protected TreeNode(TreeView owner, bool isRoot)
	{
		throw new NotImplementedException();
	}

	private void ResetPathData()
	{
		path = null;
		depth = -1;
		gotBinding = false;
		if (nodes == null)
		{
			return;
		}
		foreach (TreeNode node in nodes)
		{
			node.ResetPathData();
		}
	}

	internal void SetParent(TreeNode node)
	{
		parent = node;
		ResetPathData();
	}

	internal void Populate()
	{
		if (tree != null)
		{
			populating = true;
			tree.NotifyPopulateRequired(this);
			populating = false;
			Populated = true;
		}
	}

	/// <summary>Collapses the current tree node.</summary>
	public void Collapse()
	{
		Expanded = false;
	}

	/// <summary>Collapses the current node and all its child nodes.</summary>
	public void CollapseAll()
	{
		SetExpandedRec(expanded: false, -1);
	}

	/// <summary>Expands the current tree node.</summary>
	public void Expand()
	{
		Expanded = true;
	}

	internal void Expand(int depth)
	{
		SetExpandedRec(expanded: true, depth);
	}

	/// <summary>Expands the current node and all its child nodes.</summary>
	public void ExpandAll()
	{
		SetExpandedRec(expanded: true, -1);
	}

	private void SetExpandedRec(bool expanded, int depth)
	{
		Expanded = expanded;
		if (depth == 0)
		{
			return;
		}
		foreach (TreeNode childNode in ChildNodes)
		{
			childNode.SetExpandedRec(expanded, depth - 1);
		}
	}

	/// <summary>Selects the current node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	public void Select()
	{
		Selected = true;
	}

	/// <summary>Alternates between the expanded and collapsed state of the node.</summary>
	public void ToggleExpandState()
	{
		Expanded = !(Expanded ?? false);
	}

	/// <summary>Loads the node's previously saved view state.</summary>
	/// <param name="state">A <see cref="T:System.Object" /> that contains the saved view state values. </param>
	void IStateManager.LoadViewState(object state)
	{
		LoadViewState(state);
	}

	/// <summary>Loads the previously saved view state of the node. </summary>
	/// <param name="state">An <see cref="T:System.Object" /> that represents the state of the node.</param>
	protected virtual void LoadViewState(object state)
	{
		if (state != null)
		{
			object[] array = (object[])state;
			ViewState.LoadViewState(array[0]);
			if (tree != null && SelectedFlag)
			{
				tree.SetSelectedNode(this, loading: true);
			}
			if (!PopulateOnDemand || Populated)
			{
				((IStateManager)ChildNodes).LoadViewState(array[1]);
			}
		}
	}

	/// <summary>Saves the view state changes to a <see cref="T:System.Object" />.</summary>
	/// <returns>The <see cref="T:System.Object" /> that contains the view state changes.</returns>
	object IStateManager.SaveViewState()
	{
		return SaveViewState();
	}

	/// <summary>Saves the current view state of the node. </summary>
	/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the node. </returns>
	protected virtual object SaveViewState()
	{
		object[] array = new object[2]
		{
			ViewState.SaveViewState(),
			(nodes == null) ? null : ((IStateManager)nodes).SaveViewState()
		};
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null)
			{
				return array;
			}
		}
		return null;
	}

	/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to track changes to its view state.</summary>
	void IStateManager.TrackViewState()
	{
		TrackViewState();
	}

	/// <summary>Marks the starting point at which to begin tracking and saving view state changes to the node. </summary>
	protected void TrackViewState()
	{
		if (!marked)
		{
			marked = true;
			ViewState.TrackViewState();
			if (nodes != null)
			{
				((IStateManager)nodes).TrackViewState();
			}
		}
	}

	internal void SetDirty()
	{
		ViewState.SetDirty(dirty: true);
		if (nodes != null)
		{
			nodes.SetDirty();
		}
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class with the properties of the current <see cref="T:System.Web.UI.WebControls.TreeNode" /> instance.</summary>
	/// <returns>A new instance of <see cref="T:System.Web.UI.WebControls.TreeNode" /> with the properties of the current <see cref="T:System.Web.UI.WebControls.TreeNode" /> instance.</returns>
	public virtual object Clone()
	{
		TreeNode treeNode = ((tree != null) ? tree.CreateNode() : new TreeNode());
		foreach (DictionaryEntry item in ViewState)
		{
			treeNode.ViewState[(string)item.Key] = ((StateItem)item.Value).Value;
		}
		foreach (TreeNode childNode in ChildNodes)
		{
			treeNode.ChildNodes.Add((TreeNode)childNode.Clone());
		}
		return treeNode;
	}

	/// <summary>Creates a copy of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents a copy of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object.</returns>
	object ICloneable.Clone()
	{
		return Clone();
	}

	internal void Bind(IHierarchyData hierarchyData)
	{
		this.hierarchyData = hierarchyData;
		DataBound = true;
		DataPath = hierarchyData.Path;
		dataItem = hierarchyData.Item;
		TreeNodeBinding treeNodeBinding = GetBinding();
		if (treeNodeBinding != null)
		{
			if (treeNodeBinding.ImageToolTipField.Length > 0)
			{
				ImageToolTip = Convert.ToString(GetBoundPropertyValue(treeNodeBinding.ImageToolTipField));
				if (ImageToolTip.Length == 0)
				{
					ImageToolTip = treeNodeBinding.ImageToolTip;
				}
			}
			else if (treeNodeBinding.ImageToolTip.Length > 0)
			{
				ImageToolTip = treeNodeBinding.ImageToolTip;
			}
			if (treeNodeBinding.ImageUrlField.Length > 0)
			{
				ImageUrl = Convert.ToString(GetBoundPropertyValue(treeNodeBinding.ImageUrlField));
				if (ImageUrl.Length == 0)
				{
					ImageUrl = treeNodeBinding.ImageUrl;
				}
			}
			else if (treeNodeBinding.ImageUrl.Length > 0)
			{
				ImageUrl = treeNodeBinding.ImageUrl;
			}
			if (treeNodeBinding.NavigateUrlField.Length > 0)
			{
				NavigateUrl = Convert.ToString(GetBoundPropertyValue(treeNodeBinding.NavigateUrlField));
				if (NavigateUrl.Length == 0)
				{
					NavigateUrl = treeNodeBinding.NavigateUrl;
				}
			}
			else if (treeNodeBinding.NavigateUrl.Length > 0)
			{
				NavigateUrl = treeNodeBinding.NavigateUrl;
			}
			if (treeNodeBinding.HasPropertyValue("PopulateOnDemand"))
			{
				PopulateOnDemand = treeNodeBinding.PopulateOnDemand;
			}
			if (treeNodeBinding.HasPropertyValue("SelectAction"))
			{
				SelectAction = treeNodeBinding.SelectAction;
			}
			if (treeNodeBinding.HasPropertyValue("ShowCheckBox"))
			{
				ShowCheckBox = treeNodeBinding.ShowCheckBox;
			}
			if (treeNodeBinding.TargetField.Length > 0)
			{
				Target = Convert.ToString(GetBoundPropertyValue(treeNodeBinding.TargetField));
				if (Target.Length == 0)
				{
					Target = treeNodeBinding.Target;
				}
			}
			else if (treeNodeBinding.Target.Length > 0)
			{
				Target = treeNodeBinding.Target;
			}
			string text = null;
			if (treeNodeBinding.TextField.Length > 0)
			{
				text = Convert.ToString(GetBoundPropertyValue(treeNodeBinding.TextField));
				if (treeNodeBinding.FormatString.Length > 0)
				{
					text = string.Format(treeNodeBinding.FormatString, text);
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				if (treeNodeBinding.Text.Length > 0)
				{
					text = treeNodeBinding.Text;
				}
				else if (treeNodeBinding.Value.Length > 0)
				{
					text = treeNodeBinding.Value;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				Text = text;
			}
			if (treeNodeBinding.ToolTipField.Length > 0)
			{
				ToolTip = Convert.ToString(GetBoundPropertyValue(treeNodeBinding.ToolTipField));
				if (ToolTip.Length == 0)
				{
					ToolTip = treeNodeBinding.ToolTip;
				}
			}
			else if (treeNodeBinding.ToolTip.Length > 0)
			{
				ToolTip = treeNodeBinding.ToolTip;
			}
			string value = null;
			if (treeNodeBinding.ValueField.Length > 0)
			{
				value = Convert.ToString(GetBoundPropertyValue(treeNodeBinding.ValueField));
			}
			if (string.IsNullOrEmpty(value))
			{
				if (treeNodeBinding.Value.Length > 0)
				{
					value = treeNodeBinding.Value;
				}
				else if (treeNodeBinding.Text.Length > 0)
				{
					value = treeNodeBinding.Text;
				}
			}
			if (!string.IsNullOrEmpty(value))
			{
				Value = value;
			}
		}
		else
		{
			string text2 = (Value = GetDefaultBoundText());
			Text = text2;
		}
		if (hierarchyData is INavigateUIData navigateUIData)
		{
			SelectAction = TreeNodeSelectAction.None;
			Text = navigateUIData.ToString();
			NavigateUrl = navigateUIData.NavigateUrl;
			ToolTip = navigateUIData.Description;
		}
	}

	internal void SetDataItem(object item)
	{
		dataItem = item;
	}

	internal void SetDataPath(string path)
	{
		DataPath = path;
	}

	internal void SetDataBound(bool bound)
	{
		DataBound = bound;
	}

	private string GetDefaultBoundText()
	{
		if (hierarchyData != null)
		{
			return hierarchyData.ToString();
		}
		if (dataItem != null)
		{
			return dataItem.ToString();
		}
		return string.Empty;
	}

	private string GetDataItemType()
	{
		if (hierarchyData != null)
		{
			return hierarchyData.Type;
		}
		if (dataItem != null)
		{
			return dataItem.GetType().ToString();
		}
		return string.Empty;
	}

	private TreeNodeBinding GetBinding()
	{
		if (tree == null)
		{
			return null;
		}
		if (gotBinding)
		{
			return binding;
		}
		binding = tree.FindBindingForNode(GetDataItemType(), Depth);
		gotBinding = true;
		return binding;
	}

	private object GetBoundPropertyValue(string name)
	{
		if (boundProperties == null)
		{
			if (hierarchyData != null)
			{
				boundProperties = TypeDescriptor.GetProperties(hierarchyData);
			}
			else
			{
				boundProperties = TypeDescriptor.GetProperties(dataItem);
			}
		}
		PropertyDescriptor propertyDescriptor = boundProperties.Find(name, ignoreCase: true);
		if (propertyDescriptor == null)
		{
			throw new InvalidOperationException("Property '" + name + "' not found in data bound item");
		}
		if (hierarchyData != null)
		{
			return propertyDescriptor.GetValue(hierarchyData);
		}
		return propertyDescriptor.GetValue(dataItem);
	}

	internal void BeginRenderText(HtmlTextWriter writer)
	{
		RenderPreText(writer);
	}

	internal void EndRenderText(HtmlTextWriter writer)
	{
		RenderPostText(writer);
	}

	/// <summary>Allows control developers to add additional rendering to the node.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream used to write content to a Web page.</param>
	protected virtual void RenderPreText(HtmlTextWriter writer)
	{
	}

	/// <summary>Allows control developers to add additional rendering to the node.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream used to write content to a Web page. </param>
	protected virtual void RenderPostText(HtmlTextWriter writer)
	{
	}
}
