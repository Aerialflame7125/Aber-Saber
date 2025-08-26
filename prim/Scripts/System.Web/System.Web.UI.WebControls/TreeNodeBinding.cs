using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls;

/// <summary>Defines the relationship between a data item and the node it is binding to in a <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
[DefaultProperty("TextField")]
public sealed class TreeNodeBinding : IStateManager, ICloneable, IDataSourceViewSchemaAccessor
{
	private StateBag ViewState = new StateBag();

	/// <summary>Gets or sets the value to match against a <see cref="P:System.Web.UI.IHierarchyData.Type" /> property for a data item to determine whether to apply the tree node binding.</summary>
	/// <returns>The value to match against a data item's <see cref="P:System.Web.UI.IHierarchyData.Type" /> property to determine whether to apply the tree node binding. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.DataMember" /> property is not set.</returns>
	[DefaultValue("")]
	public string DataMember
	{
		get
		{
			object obj = ViewState["DataMember"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["DataMember"] = value;
		}
	}

	/// <summary>Gets or sets the node depth at which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
	/// <returns>The node depth at which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is -1, indicating that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.Depth" /> property is not set.</returns>
	[DefaultValue(-1)]
	public int Depth
	{
		get
		{
			object obj = ViewState["Depth"];
			if (obj != null)
			{
				return (int)obj;
			}
			return -1;
		}
		set
		{
			ViewState["Depth"] = value;
		}
	}

	/// <summary>Gets or sets the string that specifies the display format for the text of a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
	/// <returns>A formatting string that specifies the display format for the text of a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.FormatString" /> property is not set.</returns>
	[Localizable(true)]
	[DefaultValue("")]
	public string FormatString
	{
		get
		{
			object obj = ViewState["FormatString"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["FormatString"] = value;
		}
	}

	/// <summary>Gets or sets the ToolTip text for the image that is displayed next to a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
	/// <returns>The ToolTip text for the image that is displayed next to a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the P:System.Web.UI.WebControls.TreeNodeBinding.ImageToolTip property is not set.</returns>
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
			return "";
		}
		set
		{
			ViewState["ImageToolTip"] = value;
		}
	}

	/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.ImageToolTip" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
	/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.ImageToolTip" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.ImageToolTipField" /> property is not set.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string ImageToolTipField
	{
		get
		{
			object obj = ViewState["ImageToolTipField"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["ImageToolTipField"] = value;
		}
	}

	/// <summary>Gets or sets the URL to an image that is displayed next to a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
	/// <returns>The URL to an image that is displayed next to a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The 
	///     <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.ImageUrl" /> property is not set.</returns>
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
			return "";
		}
		set
		{
			ViewState["ImageUrl"] = value;
		}
	}

	/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.ImageUrl" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
	/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.ImageUrl" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.ImageUrlField" /> property is not set.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string ImageUrlField
	{
		get
		{
			object obj = ViewState["ImageUrlField"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["ImageUrlField"] = value;
		}
	}

	/// <summary>Gets or sets the URL to link to when a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied is clicked.</summary>
	/// <returns>The URL to link to when a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied is clicked. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.NavigateUrl" /> property is not set.</returns>
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
			return "";
		}
		set
		{
			ViewState["NavigateUrl"] = value;
		}
	}

	/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.NavigateUrl" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
	/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.NavigateUrl" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.NavigateUrlField" /> property is not set.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string NavigateUrlField
	{
		get
		{
			object obj = ViewState["NavigateUrlField"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["NavigateUrlField"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied is populated dynamically.</summary>
	/// <returns>
	///     <see langword="true" /> to populate the node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied dynamically; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
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
		}
	}

	/// <summary>Gets or sets the event or events to raise when a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied is selected.</summary>
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

	/// <summary>Gets or sets a value indicating whether a check box is displayed next to a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
	/// <returns>
	///     <see langword="true" /> to display a check box next to a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
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

	/// <summary>Gets or sets the target window or frame in which to display the Web page content that is associated with a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
	/// <returns>The target window or frame in which to display the linked Web page content. Values must begin with a letter in the range of A through Z (case insensitive), except for certain special values that begin with an underscore, as shown in the following table.Target value Description 
	///             <see langword="_blank" />
	///           Renders the content in a new window without frames. 
	///             <see langword="_parent" />
	///           Renders the content in the immediate frameset parent. 
	///             <see langword="_search" />
	///           Renders the content in the search pane.
	///             <see langword="_self" />
	///           Renders the content in the frame with focus. 
	///             <see langword="_top" />
	///           Renders the content in the full window without frames. Check your browser documentation to determine if the <see langword="_search" /> value is supported.  For example, Microsoft Internet Explorer version 5.0 and later supports the <see langword="_search" /> target value.The default is an empty string (""), which refreshes the window or frame with focus.</returns>
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

	/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.Target" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
	/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.Target" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.TargetField" /> property is not set.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string TargetField
	{
		get
		{
			object obj = ViewState["TargetField"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["TargetField"] = value;
		}
	}

	/// <summary>Gets or sets the text that is displayed for the node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
	/// <returns>The text displayed for the node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string ("").</returns>
	[Localizable(true)]
	[DefaultValue("")]
	[WebSysDescription("The display text of the tree node.")]
	public string Text
	{
		get
		{
			object obj = ViewState["Text"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["Text"] = value;
		}
	}

	/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.Text" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
	/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.Text" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.TextField" /> property is not set.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string TextField
	{
		get
		{
			object obj = ViewState["TextField"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["TextField"] = value;
		}
	}

	/// <summary>Gets or sets the ToolTip text for a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
	/// <returns>The ToolTip text for a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.ToolTip" /> property is not set.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public string ToolTip
	{
		get
		{
			object obj = ViewState["ToolTip"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["ToolTip"] = value;
		}
	}

	/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.ToolTip" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
	/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.ToolTip" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.ToolTipField" /> property is not set.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string ToolTipField
	{
		get
		{
			object obj = ViewState["ToolTipField"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["ToolTipField"] = value;
		}
	}

	/// <summary>Gets or sets a displayed value that is not displayed but is used to store any additional data about a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied, such as data used for handling postback events.</summary>
	/// <returns>Supplemental data about a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied; this data is not displayed. The default is an empty string ("").</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public string Value
	{
		get
		{
			object obj = ViewState["Value"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["Value"] = value;
		}
	}

	/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.Value" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
	/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.Value" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.ValueField" /> property is not set.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string ValueField
	{
		get
		{
			object obj = ViewState["ValueField"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			ViewState["ValueField"] = value;
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" />.</summary>
	/// <returns>
	///     <see langword="true" />, if the control is marked to save its state; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => ViewState.IsTrackingViewState;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataSourceViewSchemaAccessor.DataSourceViewSchema" />.</summary>
	/// <returns>An object that represents the schema that is associated with the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object.</returns>
	[MonoTODO("Not implemented")]
	object IDataSourceViewSchemaAccessor.DataSourceViewSchema
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	internal bool HasPropertyValue(string propName)
	{
		return ViewState[propName] != null;
	}

	/// <summary>Loads the previously saved view state for the node.</summary>
	/// <param name="state">A <see cref="T:System.Object" /> that contains the saved view state values. </param>
	void IStateManager.LoadViewState(object savedState)
	{
		ViewState.LoadViewState(savedState);
	}

	/// <summary>Saves the view state changes to an object.</summary>
	/// <returns>The object that contains the view state changes.</returns>
	object IStateManager.SaveViewState()
	{
		return ViewState.SaveViewState();
	}

	/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to track changes to its view state.</summary>
	void IStateManager.TrackViewState()
	{
		ViewState.TrackViewState();
	}

	/// <summary>Creates a copy of the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object.</summary>
	/// <returns>An object that represents a copy of the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object.</returns>
	object ICloneable.Clone()
	{
		TreeNodeBinding treeNodeBinding = new TreeNodeBinding();
		foreach (DictionaryEntry item in ViewState)
		{
			treeNodeBinding.ViewState[(string)item.Key] = item.Value;
		}
		return treeNodeBinding;
	}

	internal void SetDirty()
	{
		foreach (string key in ViewState.Keys)
		{
			ViewState.SetItemDirty(key, dirty: true);
		}
	}

	/// <summary>Returns the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.DataMember" /> property.</summary>
	/// <returns>Returns the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.DataMember" /> property. If the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.DataMember" /> property is <see langword="null" /> or an empty string (""), (Empty) is returned.</returns>
	public override string ToString()
	{
		if (DataMember.Length <= 0)
		{
			return "(Empty)";
		}
		return DataMember;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> class.</summary>
	public TreeNodeBinding()
	{
	}
}
