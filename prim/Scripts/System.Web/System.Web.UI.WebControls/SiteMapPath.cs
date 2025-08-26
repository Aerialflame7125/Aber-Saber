using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Displays a set of text or image hyperlinks that enable users to more easily navigate a Web site, while taking a minimal amount of page space.</summary>
[Designer("System.Web.UI.Design.WebControls.SiteMapPathDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
public class SiteMapPath : CompositeControl
{
	private SiteMapProvider provider;

	private Style currentNodeStyle;

	private Style nodeStyle;

	private Style pathSeparatorStyle;

	private Style rootNodeStyle;

	private ITemplate currentNodeTemplate;

	private ITemplate nodeTemplate;

	private ITemplate pathSeparatorTemplate;

	private ITemplate rootNodeTemplate;

	private static readonly object ItemCreatedEvent;

	private static readonly object ItemDataBoundEvent;

	/// <summary>Gets the style used for the display text for the current node.</summary>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.Style" /> that contains the style settings for the display text for the current node of the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public Style CurrentNodeStyle
	{
		get
		{
			if (currentNodeStyle == null)
			{
				currentNodeStyle = new Style();
				if (base.IsTrackingViewState)
				{
					((IStateManager)currentNodeStyle).TrackViewState();
				}
			}
			return currentNodeStyle;
		}
	}

	/// <summary>Gets or sets a control template to use for the node of a site navigation path that represents the currently displayed page.</summary>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object that implements the <see cref="M:System.Web.UI.ITemplate.InstantiateIn(System.Web.UI.Control)" /> method, to render custom content for the navigation path node that represents the currently displayed page.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(SiteMapNodeItem), BindingDirection.OneWay)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public virtual ITemplate CurrentNodeTemplate
	{
		get
		{
			return currentNodeTemplate;
		}
		set
		{
			currentNodeTemplate = value;
			UpdateControls();
		}
	}

	/// <summary>Gets the style used for the display text for all nodes in the site navigation path.</summary>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.Style" /> that contains the style settings for the display text in the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public Style NodeStyle
	{
		get
		{
			if (nodeStyle == null)
			{
				nodeStyle = new Style();
				if (base.IsTrackingViewState)
				{
					((IStateManager)nodeStyle).TrackViewState();
				}
			}
			return nodeStyle;
		}
	}

	/// <summary>Gets or sets a control template to use for all functional nodes of a site navigation path.</summary>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object that implements the <see cref="M:System.Web.UI.ITemplate.InstantiateIn(System.Web.UI.Control)" /> method, to render custom content for each node of a navigation path.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(SiteMapNodeItem), BindingDirection.OneWay)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public virtual ITemplate NodeTemplate
	{
		get
		{
			return nodeTemplate;
		}
		set
		{
			nodeTemplate = value;
			UpdateControls();
		}
	}

	/// <summary>Gets or sets the number of levels of parent nodes the control displays, relative to the currently displayed node.</summary>
	/// <returns>An integer that specifies the number of levels of parent nodes displayed, relative to the current context node. The default value is -1, which indicates no restriction on the number of parent levels that the control displays.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value for <see cref="P:System.Web.UI.WebControls.SiteMapPath.ParentLevelsDisplayed" /> is less than -1.</exception>
	[DefaultValue(-1)]
	[Themeable(false)]
	public virtual int ParentLevelsDisplayed
	{
		get
		{
			return ViewState.GetInt("ParentLevelsDisplayed", -1);
		}
		set
		{
			if (value < -1)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			ViewState["ParentLevelsDisplayed"] = value;
			UpdateControls();
		}
	}

	/// <summary>Gets or sets the order that the navigation path nodes are rendered in.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.PathDirection" /> that indicates the hierarchical order that navigation nodes are rendered in. The default is <see cref="F:System.Web.UI.WebControls.PathDirection.RootToCurrent" />, which indicates that the nodes are rendered in hierarchical order from the top-most node to the current node, from left to right.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value for <see cref="P:System.Web.UI.WebControls.SiteMapPath.PathDirection" /> is not one of the base <see cref="T:System.Web.UI.WebControls.PathDirection" /> enumerations. </exception>
	[DefaultValue(PathDirection.RootToCurrent)]
	public virtual PathDirection PathDirection
	{
		get
		{
			return (PathDirection)ViewState.GetInt("PathDirection", 0);
		}
		set
		{
			if (value != 0 && value != PathDirection.CurrentToRoot)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			ViewState["PathDirection"] = value;
			UpdateControls();
		}
	}

	/// <summary>Gets or sets the string that delimits <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> nodes in the rendered navigation path.</summary>
	/// <returns>A string that represents the delimiter for the nodes in a navigation path. The default is " &gt; ", which is a character pointing from left to right, and corresponds to the default <see cref="T:System.Web.UI.WebControls.PathDirection" />, which is set to <see cref="F:System.Web.UI.WebControls.PathDirection.RootToCurrent" />.</returns>
	[DefaultValue(" > ")]
	[Localizable(true)]
	public virtual string PathSeparator
	{
		get
		{
			return ViewState.GetString("PathSeparator", " > ");
		}
		set
		{
			ViewState["PathSeparator"] = value;
			UpdateControls();
		}
	}

	/// <summary>Gets the style used for the <see cref="P:System.Web.UI.WebControls.SiteMapPath.PathSeparator" /> string.</summary>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.Style" /> that contains the style settings of the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control's <see cref="P:System.Web.UI.WebControls.SiteMapPath.PathSeparator" /> text.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public Style PathSeparatorStyle
	{
		get
		{
			if (pathSeparatorStyle == null)
			{
				pathSeparatorStyle = new Style();
				if (base.IsTrackingViewState)
				{
					((IStateManager)pathSeparatorStyle).TrackViewState();
				}
			}
			return pathSeparatorStyle;
		}
	}

	/// <summary>Gets or sets a control template to use for the path delimiter of a site navigation path.</summary>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object that implements the <see cref="M:System.Web.UI.ITemplate.InstantiateIn(System.Web.UI.Control)" /> method, to render custom content for the path delimiter of a navigation path.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(SiteMapNodeItem), BindingDirection.OneWay)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public virtual ITemplate PathSeparatorTemplate
	{
		get
		{
			return pathSeparatorTemplate;
		}
		set
		{
			pathSeparatorTemplate = value;
			UpdateControls();
		}
	}

	/// <summary>Gets or sets a <see cref="T:System.Web.SiteMapProvider" /> that is associated with the Web server control.</summary>
	/// <returns>A <see cref="T:System.Web.SiteMapProvider" /> instance that is associated with the control. If no provider is explicitly set, the default site map provider is used.</returns>
	/// <exception cref="T:System.Web.HttpException">The provider named by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.SiteMapProvider" /> property is not available.- or -There is no default provider configured for the site.</exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public SiteMapProvider Provider
	{
		get
		{
			if (provider == null)
			{
				if (SiteMapProvider.Length == 0)
				{
					provider = SiteMap.Provider;
					if (provider == null)
					{
						throw new HttpException("There is no default provider configured for the site.");
					}
				}
				else
				{
					provider = SiteMap.Providers[SiteMapProvider];
					if (provider == null)
					{
						throw new HttpException("SiteMap provider '" + SiteMapProvider + "' not found.");
					}
				}
			}
			return provider;
		}
		set
		{
			provider = value;
			UpdateControls();
		}
	}

	/// <summary>Indicates whether the site navigation node that represents the currently displayed page is rendered as a hyperlink.</summary>
	/// <returns>
	///     <see langword="true" /> if the node that represents the current page is rendered as a hyperlink; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	public virtual bool RenderCurrentNodeAsLink
	{
		get
		{
			return ViewState.GetBool("RenderCurrentNodeAsLink", def: false);
		}
		set
		{
			ViewState["RenderCurrentNodeAsLink"] = value;
			UpdateControls();
		}
	}

	/// <summary>Gets the style for the root node display text.</summary>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.Style" /> that contains the style settings for the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control root node display text.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public Style RootNodeStyle
	{
		get
		{
			if (rootNodeStyle == null)
			{
				rootNodeStyle = new Style();
				if (base.IsTrackingViewState)
				{
					((IStateManager)rootNodeStyle).TrackViewState();
				}
			}
			return rootNodeStyle;
		}
	}

	/// <summary>Gets or sets a control template to use for the root node of a site navigation path.</summary>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object that implements the <see cref="M:System.Web.UI.ITemplate.InstantiateIn(System.Web.UI.Control)" /> method, to render custom content for the root node of a navigation path.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(SiteMapNodeItem), BindingDirection.OneWay)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public virtual ITemplate RootNodeTemplate
	{
		get
		{
			return rootNodeTemplate;
		}
		set
		{
			rootNodeTemplate = value;
			UpdateControls();
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control writes an additional hyperlink attribute for hyperlinked navigation nodes. Depending on client support, when a mouse hovers over a hyperlink that has the additional attribute set, a ToolTip is displayed.</summary>
	/// <returns>
	///     <see langword="true" /> if alternate text should be written for hyperlinked navigation nodes; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[Themeable(false)]
	public virtual bool ShowToolTips
	{
		get
		{
			return ViewState.GetBool("ShowToolTips", def: true);
		}
		set
		{
			ViewState["ShowToolTips"] = value;
			UpdateControls();
		}
	}

	/// <summary>Gets or sets the name of the <see cref="T:System.Web.SiteMapProvider" /> used to render the site navigation control.</summary>
	/// <returns>The name of a <see cref="T:System.Web.SiteMapProvider" /> that defines the navigation structure for the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> to display. All available providers are contained in the <see cref="P:System.Web.SiteMap.Providers" /> collection, and can be enumerated and retrieved by name using the <see cref="P:System.Web.SiteMapProviderCollection.Item(System.String)" /> property.</returns>
	[DefaultValue("")]
	[Themeable(false)]
	public virtual string SiteMapProvider
	{
		get
		{
			return ViewState.GetString("SiteMapProvider", "");
		}
		set
		{
			ViewState["SiteMapProvider"] = value;
			UpdateControls();
		}
	}

	/// <summary>Gets or sets a value that is used to render alternate text for screen readers to skip the control's content.</summary>
	/// <returns>A string that the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control renders as alternate text with an invisible image, as a hint to screen readers. The default value is "Skip Navigation Links". </returns>
	[Localizable(true)]
	public virtual string SkipLinkText
	{
		get
		{
			return ViewState.GetString("SkipLinkText", "Skip Navigation Links");
		}
		set
		{
			ViewState["SkipLinkText"] = value;
		}
	}

	/// <summary>Occurs when a <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> is created by the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> and is associated with its corresponding <see cref="T:System.Web.SiteMapNode" />. This event is raised by the <see cref="M:System.Web.UI.WebControls.SiteMapPath.OnItemCreated(System.Web.UI.WebControls.SiteMapNodeItemEventArgs)" /> method.</summary>
	public event SiteMapNodeItemEventHandler ItemCreated
	{
		add
		{
			base.Events.AddHandler(ItemCreatedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ItemCreatedEvent, value);
		}
	}

	/// <summary>Occurs after a <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> has been bound to its underlying <see cref="T:System.Web.SiteMapNode" /> data by the <see cref="T:System.Web.UI.WebControls.SiteMapPath" />. This event is raised by the <see cref="M:System.Web.UI.WebControls.SiteMapPath.OnItemDataBound(System.Web.UI.WebControls.SiteMapNodeItemEventArgs)" /> method.</summary>
	public event SiteMapNodeItemEventHandler ItemDataBound
	{
		add
		{
			base.Events.AddHandler(ItemDataBoundEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ItemDataBoundEvent, value);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SiteMapPath.ItemCreated" /> event of the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemEventArgs" /> that contains event data. </param>
	protected virtual void OnItemCreated(SiteMapNodeItemEventArgs e)
	{
		if (base.Events != null)
		{
			((SiteMapNodeItemEventHandler)base.Events[ItemCreated])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SiteMapPath.ItemDataBound" /> event of the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemEventArgs" /> that contains event data. </param>
	protected virtual void OnItemDataBound(SiteMapNodeItemEventArgs e)
	{
		if (base.Events != null)
		{
			((SiteMapNodeItemEventHandler)base.Events[ItemDataBound])?.Invoke(this, e);
		}
	}

	private void UpdateControls()
	{
		base.ChildControlsCreated = false;
	}

	/// <summary>Binds a data source to the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control and its child controls.</summary>
	public override void DataBind()
	{
		base.DataBind();
		foreach (Control control in Controls)
		{
			if (control is SiteMapNodeItem)
			{
				SiteMapNodeItem item = (SiteMapNodeItem)control;
				OnItemDataBound(new SiteMapNodeItemEventArgs(item));
			}
		}
	}

	/// <summary>Clears the current child controls collection, and rebuilds it by calling the <see cref="M:System.Web.UI.WebControls.SiteMapPath.CreateControlHierarchy" /> method.</summary>
	protected internal override void CreateChildControls()
	{
		Controls.Clear();
		CreateControlHierarchy();
		DataBind();
	}

	/// <summary>Examines the site map structure provided by the <see cref="P:System.Web.UI.WebControls.SiteMapPath.SiteMapProvider" /> and builds a child controls collection based on the styles and templates defined for the functional nodes.</summary>
	/// <exception cref="T:System.Web.HttpException">No <see cref="P:System.Web.UI.WebControls.SiteMapPath.SiteMapProvider" /> is available to the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control. </exception>
	protected virtual void CreateControlHierarchy()
	{
		ArrayList arrayList = new ArrayList();
		SiteMapNode siteMapNode = Provider.CurrentNode;
		if (siteMapNode == null)
		{
			return;
		}
		int num = ((ParentLevelsDisplayed != -1) ? (ParentLevelsDisplayed + 1) : int.MaxValue);
		while (siteMapNode != null && num > 0)
		{
			if (arrayList.Count > 0)
			{
				SiteMapNodeItem siteMapNodeItem = new SiteMapNodeItem(arrayList.Count, SiteMapNodeItemType.PathSeparator);
				InitializeItem(siteMapNodeItem);
				SiteMapNodeItemEventArgs e = new SiteMapNodeItemEventArgs(siteMapNodeItem);
				OnItemCreated(e);
				arrayList.Add(siteMapNodeItem);
			}
			SiteMapNodeItem siteMapNodeItem2 = new SiteMapNodeItem(itemType: (arrayList.Count != 0) ? ((siteMapNode.ParentNode != null) ? SiteMapNodeItemType.Parent : SiteMapNodeItemType.Root) : SiteMapNodeItemType.Current, itemIndex: arrayList.Count);
			siteMapNodeItem2.SiteMapNode = siteMapNode;
			InitializeItem(siteMapNodeItem2);
			SiteMapNodeItemEventArgs e2 = new SiteMapNodeItemEventArgs(siteMapNodeItem2);
			OnItemCreated(e2);
			arrayList.Add(siteMapNodeItem2);
			siteMapNode = siteMapNode.ParentNode;
			num--;
		}
		if (PathDirection == PathDirection.RootToCurrent)
		{
			for (int num2 = arrayList.Count - 1; num2 >= 0; num2--)
			{
				Controls.Add((Control)arrayList[num2]);
			}
		}
		else
		{
			for (int i = 0; i < arrayList.Count; i++)
			{
				Controls.Add((Control)arrayList[i]);
			}
		}
	}

	/// <summary>Populates a <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" />, which is a Web server control that represents a <see cref="T:System.Web.SiteMapNode" />, with a set of child controls based on the node's function and the specified templates and styles for the node.</summary>
	/// <param name="item">The <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> to initialize. </param>
	protected virtual void InitializeItem(SiteMapNodeItem item)
	{
		switch (item.ItemType)
		{
		case SiteMapNodeItemType.Root:
			if (RootNodeTemplate != null)
			{
				item.ApplyStyle(NodeStyle);
				item.ApplyStyle(RootNodeStyle);
				RootNodeTemplate.InstantiateIn(item);
			}
			else if (NodeTemplate != null)
			{
				item.ApplyStyle(NodeStyle);
				item.ApplyStyle(RootNodeStyle);
				NodeTemplate.InstantiateIn(item);
			}
			else
			{
				WebControl webControl = CreateHyperLink(item);
				webControl.ApplyStyle(NodeStyle);
				webControl.ApplyStyle(RootNodeStyle);
				item.Controls.Add(webControl);
			}
			break;
		case SiteMapNodeItemType.Current:
			if (CurrentNodeTemplate != null)
			{
				item.ApplyStyle(NodeStyle);
				item.ApplyStyle(CurrentNodeStyle);
				CurrentNodeTemplate.InstantiateIn(item);
			}
			else if (NodeTemplate != null)
			{
				item.ApplyStyle(NodeStyle);
				item.ApplyStyle(CurrentNodeStyle);
				NodeTemplate.InstantiateIn(item);
			}
			else if (RenderCurrentNodeAsLink)
			{
				HyperLink hyperLink = CreateHyperLink(item);
				hyperLink.ApplyStyle(NodeStyle);
				hyperLink.ApplyStyle(CurrentNodeStyle);
				item.Controls.Add(hyperLink);
			}
			else
			{
				Literal child = CreateLiteral(item);
				item.ApplyStyle(NodeStyle);
				item.ApplyStyle(CurrentNodeStyle);
				item.Controls.Add(child);
			}
			break;
		case SiteMapNodeItemType.Parent:
			if (NodeTemplate != null)
			{
				item.ApplyStyle(NodeStyle);
				NodeTemplate.InstantiateIn(item);
			}
			else
			{
				WebControl webControl2 = CreateHyperLink(item);
				webControl2.ApplyStyle(NodeStyle);
				item.Controls.Add(webControl2);
			}
			break;
		case SiteMapNodeItemType.PathSeparator:
		{
			if (PathSeparatorTemplate != null)
			{
				item.ApplyStyle(PathSeparatorStyle);
				PathSeparatorTemplate.InstantiateIn(item);
				break;
			}
			Literal literal = new Literal();
			literal.Text = HttpUtility.HtmlEncode(PathSeparator);
			item.ApplyStyle(PathSeparatorStyle);
			item.Controls.Add(literal);
			break;
		}
		}
	}

	private HyperLink CreateHyperLink(SiteMapNodeItem item)
	{
		HyperLink hyperLink = new HyperLink();
		hyperLink.Text = item.SiteMapNode.Title;
		hyperLink.NavigateUrl = item.SiteMapNode.Url;
		if (ShowToolTips)
		{
			hyperLink.ToolTip = item.SiteMapNode.Description;
		}
		return hyperLink;
	}

	private Literal CreateLiteral(SiteMapNodeItem item)
	{
		return new Literal
		{
			Text = item.SiteMapNode.Title
		};
	}

	/// <summary>Restores view-state information from a previous request that was saved with the <see cref="M:System.Web.UI.WebControls.SiteMapPath.SaveViewState" /> method.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored. </param>
	protected override void LoadViewState(object savedState)
	{
		if (savedState == null)
		{
			base.LoadViewState((object)null);
			return;
		}
		object[] array = (object[])savedState;
		base.LoadViewState(array[0]);
		if (array[1] != null)
		{
			((IStateManager)CurrentNodeStyle).LoadViewState(array[1]);
		}
		if (array[2] != null)
		{
			((IStateManager)NodeStyle).LoadViewState(array[2]);
		}
		if (array[3] != null)
		{
			((IStateManager)PathSeparatorStyle).LoadViewState(array[3]);
		}
		if (array[4] != null)
		{
			((IStateManager)RootNodeStyle).LoadViewState(array[4]);
		}
	}

	/// <summary>Overrides the <see cref="M:System.Web.UI.Control.OnDataBinding(System.EventArgs)" /> method of the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> class and raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
	[MonoTODO("why override?")]
	protected override void OnDataBinding(EventArgs e)
	{
		base.OnDataBinding(e);
	}

	/// <summary>Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered output.</param>
	[MonoTODO("why override?")]
	protected internal override void Render(HtmlTextWriter writer)
	{
		base.Render(writer);
	}

	/// <summary>Renders the nodes in the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream used to write content to a Web page.</param>
	protected internal override void RenderContents(HtmlTextWriter writer)
	{
		string text = ClientID + "_SkipLink";
		string skipLinkText = SkipLinkText;
		bool num = !string.IsNullOrEmpty(skipLinkText);
		if (num)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "#" + text);
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, skipLinkText);
			writer.AddAttribute(HtmlTextWriterAttribute.Height, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Width, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Src, Page.ClientScript.GetWebResourceUrl(typeof(SiteMapPath), "transparent.gif"));
			writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}
		base.RenderContents(writer);
		if (num)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, text);
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.RenderEndTag();
		}
	}

	/// <summary>Saves changes to view state for the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control.</summary>
	/// <returns>Returns the server control's current view state. If there is no view state associated with the control, this method returns <see langword="null" />.</returns>
	protected override object SaveViewState()
	{
		object[] array = new object[5]
		{
			base.SaveViewState(),
			null,
			null,
			null,
			null
		};
		if (currentNodeStyle != null)
		{
			array[1] = ((IStateManager)currentNodeStyle).SaveViewState();
		}
		if (nodeStyle != null)
		{
			array[2] = ((IStateManager)nodeStyle).SaveViewState();
		}
		if (pathSeparatorStyle != null)
		{
			array[3] = ((IStateManager)pathSeparatorStyle).SaveViewState();
		}
		if (rootNodeStyle != null)
		{
			array[4] = ((IStateManager)rootNodeStyle).SaveViewState();
		}
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null)
			{
				return array;
			}
		}
		return null;
	}

	/// <summary>Tracks changes to the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control's view state.</summary>
	protected override void TrackViewState()
	{
		base.TrackViewState();
		if (currentNodeStyle != null)
		{
			((IStateManager)currentNodeStyle).TrackViewState();
		}
		if (nodeStyle != null)
		{
			((IStateManager)nodeStyle).TrackViewState();
		}
		if (pathSeparatorStyle != null)
		{
			((IStateManager)pathSeparatorStyle).TrackViewState();
		}
		if (rootNodeStyle != null)
		{
			((IStateManager)rootNodeStyle).TrackViewState();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> class.</summary>
	public SiteMapPath()
	{
	}

	static SiteMapPath()
	{
		ItemCreated = new object();
		ItemDataBound = new object();
	}
}
