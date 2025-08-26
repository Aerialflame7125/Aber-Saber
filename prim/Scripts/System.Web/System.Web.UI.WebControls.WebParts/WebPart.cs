namespace System.Web.UI.WebControls.WebParts;

/// <summary>Serves as the base class for custom ASP.NET Web Parts controls, adding to the base <see cref="T:System.Web.UI.WebControls.WebParts.Part" /> class features some additional user interface (UI) properties, the ability to create connections, and personalization behavior. </summary>
public abstract class WebPart : Part, IWebPart, IWebActionable
{
	[Flags]
	private enum Allow
	{
		Close = 1,
		Connect = 2,
		Edit = 4,
		Hide = 8,
		Minimize = 0x10,
		ZoneChange = 0x20
	}

	private WebPartVerbCollection verbs = new WebPartVerbCollection();

	private Allow allow;

	private string auth_filter;

	private string catalog_icon_url;

	private WebPartExportMode exportMode;

	private string titleIconImageUrl;

	private string titleUrl;

	private string helpUrl;

	private bool isStatic;

	private bool hidden;

	private bool isClosed;

	private bool hasSharedData;

	private bool hasUserData;

	private WebPartHelpMode helpMode = WebPartHelpMode.Navigate;

	private int zoneIndex;

	/// <summary>Gets or sets a value indicating whether an end user can close a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control on a Web page.</summary>
	/// <returns>
	///     <see langword="true" /> if the control can be closed on a Web page; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool AllowClose
	{
		get
		{
			return (allow & Allow.Close) != 0;
		}
		set
		{
			if (value)
			{
				allow |= Allow.Close;
			}
			else
			{
				allow &= ~Allow.Close;
			}
		}
	}

	/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control allows other controls to form connections with it.</summary>
	/// <returns>A Boolean value that indicates whether connections can be formed with the control. The default is <see langword="true" />.</returns>
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool AllowConnect
	{
		get
		{
			return (allow & Allow.Connect) != 0;
		}
		set
		{
			if (value)
			{
				allow |= Allow.Connect;
			}
			else
			{
				allow &= ~Allow.Connect;
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether an end user can modify a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control through the user interface (UI) provided by one or more <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control can be modified; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool AllowEdit
	{
		get
		{
			return (allow & Allow.Edit) != 0;
		}
		set
		{
			if (value)
			{
				allow |= Allow.Edit;
			}
			else
			{
				allow &= ~Allow.Edit;
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether end users are allowed to hide a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control can be hidden; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool AllowHide
	{
		get
		{
			return (allow & Allow.Hide) != 0;
		}
		set
		{
			if (value)
			{
				allow |= Allow.Hide;
			}
			else
			{
				allow &= ~Allow.Hide;
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether end users can minimize a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control can be minimized; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool AllowMinimize
	{
		get
		{
			return (allow & Allow.Minimize) != 0;
		}
		set
		{
			if (value)
			{
				allow |= Allow.Minimize;
			}
			else
			{
				allow &= ~Allow.Minimize;
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether a user can move a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control between <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zones.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control can move between zones; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool AllowZoneChange
	{
		get
		{
			return (allow & Allow.ZoneChange) != 0;
		}
		set
		{
			if (value)
			{
				allow |= Allow.ZoneChange;
			}
			else
			{
				allow &= ~Allow.ZoneChange;
			}
		}
	}

	/// <summary>Gets or sets an arbitrary string to determine whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is authorized to be added to a page. </summary>
	/// <returns>A string that authorizes a control to be added to a Web page. The default value is an empty string ("").</returns>
	public virtual string AuthorizationFilter
	{
		get
		{
			return auth_filter;
		}
		set
		{
			auth_filter = value;
		}
	}

	/// <summary>Gets or sets the URL to an image that represents a Web Parts control in a catalog of controls. </summary>
	/// <returns>A string that represents the URL to an image used to represent the control in a catalog. The default value is an empty string ("").</returns>
	/// <exception cref="T:System.ArgumentException">The internal validation system has determined that the URL might contain script attacks.</exception>
	public virtual string CatalogIconImageUrl
	{
		get
		{
			return catalog_icon_url;
		}
		set
		{
			catalog_icon_url = value;
		}
	}

	/// <summary>Gets or sets whether a part control is in a minimized or normal state.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeState" /> values. The default is <see cref="F:System.Web.UI.WebControls.WebParts.PartChromeState.Normal" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeState" /> values. </exception>
	public override PartChromeState ChromeState
	{
		get
		{
			return base.ChromeState;
		}
		set
		{
			base.ChromeState = value;
		}
	}

	/// <summary>Gets or sets the type of border that frames a Web Parts control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> values. The default is <see cref="F:System.Web.UI.WebControls.WebParts.PartChromeType.Default" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> values. </exception>
	public override PartChromeType ChromeType
	{
		get
		{
			return base.ChromeType;
		}
		set
		{
			base.ChromeType = value;
		}
	}

	/// <summary>Gets an error message to display to users if errors occur during the connection process.</summary>
	/// <returns>A string that contains the error message.</returns>
	[MonoTODO("Not implemented")]
	public string ConnectErrorMessage => "";

	/// <summary>Gets or sets a brief phrase that summarizes what the part control does, for use in ToolTips and catalogs of part controls.</summary>
	/// <returns>A string that briefly summarizes the part control's functionality. The default value is an empty string ("").</returns>
	public override string Description
	{
		get
		{
			return base.Description;
		}
		set
		{
			base.Description = value;
		}
	}

	/// <summary>Gets or sets the horizontal direction that content flows within the control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ContentDirection" /> that indicates the horizontal direction content will flow.</returns>
	[MonoTODO("Not implemented")]
	public override ContentDirection Direction
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

	/// <summary>Gets a string that contains the full title text actually displayed in the title bar of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control instance.</summary>
	/// <returns>A string that represents the complete, displayed title of the control. The default value is an empty string ("").</returns>
	public string DisplayTitle => "Untitled";

	/// <summary>Gets or sets whether all, some, or none of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control's properties can be exported. </summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartExportMode" /> values. The default is <see cref="F:System.Web.UI.WebControls.WebParts.WebPartExportMode.None" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartExportMode" /> values.</exception>
	/// <exception cref="T:System.InvalidOperationException">The control is already loaded and the personalization scope of the control is set to the <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> scope.</exception>
	public virtual WebPartExportMode ExportMode
	{
		get
		{
			return exportMode;
		}
		set
		{
			exportMode = value;
		}
	}

	/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control has any shared personalization data associated with it.</summary>
	/// <returns>A Boolean value that indicates whether the control has shared personalization data.</returns>
	public bool HasSharedData => hasSharedData;

	/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control has any user personalization data associated with it.</summary>
	/// <returns>A Boolean value that indicates whether the control has any user personalization data.</returns>
	public bool HasUserData => hasUserData;

	/// <summary>Gets or sets the height of a zone.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> object that indicates the height of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZone" />. The default type of a <see cref="T:System.Web.UI.WebControls.Unit" /> is pixels, as indicated by the <see cref="P:System.Web.UI.WebControls.Unit.Type" /> property.</returns>
	public override Unit Height
	{
		get
		{
			return base.Height;
		}
		set
		{
			base.Height = value;
		}
	}

	/// <summary>Gets or sets the type of user interface (UI) used to display Help content for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartHelpMode" /> values. The default is <see cref="F:System.Web.UI.WebControls.WebParts.WebPartHelpMode.Modal" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartHelpMode" /> values.</exception>
	public virtual WebPartHelpMode HelpMode
	{
		get
		{
			return helpMode;
		}
		set
		{
			helpMode = value;
		}
	}

	/// <summary>Gets or sets the URL to a Help file for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
	/// <returns>A string that represents the URL to a Help file. The default value is an empty string ("").</returns>
	/// <exception cref="T:System.ArgumentException">The internal validation system has determined that the URL might contain script attacks.</exception>
	public virtual string HelpUrl
	{
		get
		{
			return helpUrl;
		}
		set
		{
			helpUrl = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is displayed on a Web page.</summary>
	/// <returns>
	///     <see langword="false" /> if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is displayed on a Web page; otherwise, <see langword="true" />. The default value is <see langword="false" />.</returns>
	public virtual bool Hidden
	{
		get
		{
			return hidden;
		}
		set
		{
			hidden = value;
		}
	}

	/// <summary>Gets or sets an error message that is used if errors occur when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is imported.</summary>
	/// <returns>A string that contains the error message. The default value is a standard error message supplied by the Web Parts control set. </returns>
	public virtual string ImportErrorMessage
	{
		get
		{
			return ViewState.GetString("ImportErrorMessage", "Cannot import this Web Part.");
		}
		set
		{
			ViewState["ImportErrorMessage"] = value;
		}
	}

	/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is currently closed on a Web Parts page.</summary>
	/// <returns>A Boolean value that indicates whether the control is closed.</returns>
	public bool IsClosed => isClosed;

	/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is shared, meaning that it is visible to all users of a Web Parts page.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control has shared user visibility on a Web page; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	public bool IsShared => false;

	/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is standalone, meaning that it is not contained within a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is not contained in a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone; otherwise, <see langword="false" />. </returns>
	public bool IsStandalone => true;

	/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is a static control, which means the control is declared in the markup of a Web Parts page and not added to the page programmatically.</summary>
	/// <returns>A Boolean value that indicates whether the control is static.</returns>
	public bool IsStatic => isStatic;

	/// <summary>Gets a string that is concatenated with the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.Title" /> property value to form a complete title for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control. </summary>
	/// <returns>A string that serves as a subtitle for the control. The default value is an empty string ("").</returns>
	public virtual string Subtitle => string.Empty;

	/// <summary>Gets or sets the title of a part control.</summary>
	/// <returns>A string that represents the title of the part control. The default value is an empty string ("").</returns>
	public override string Title
	{
		get
		{
			return base.Title;
		}
		set
		{
			base.Title = value;
		}
	}

	/// <summary>Gets or sets the URL to an image used to represent a Web Parts control in the control's title bar.</summary>
	/// <returns>A string that represents the URL to an image used to represent the control in its title bar. The default value is an empty string ("").</returns>
	/// <exception cref="T:System.ArgumentException">The internal validation system has determined that the URL might contain script attacks.</exception>
	public virtual string TitleIconImageUrl
	{
		get
		{
			return titleIconImageUrl;
		}
		set
		{
			titleIconImageUrl = value;
		}
	}

	/// <summary>Gets or sets a URL to supplemental information about a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control. </summary>
	/// <returns>A string that represents a URL to more information about the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control. The default value is an empty string ("").</returns>
	/// <exception cref="T:System.ArgumentException">The internal validation system has determined that the URL might contain script attacks.</exception>
	public virtual string TitleUrl
	{
		get
		{
			return titleUrl;
		}
		set
		{
			titleUrl = value;
		}
	}

	/// <summary>Gets a collection of custom verbs associated with a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" /> that contains custom <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> objects associated with a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control. The default value is <see cref="F:System.Web.UI.WebControls.WebParts.WebPartVerbCollection.Empty" />. </returns>
	public virtual WebPartVerbCollection Verbs => verbs;

	/// <summary>Gets or sets the width of the Web server control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the width of the control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />.</returns>
	public override Unit Width
	{
		get
		{
			return base.Width;
		}
		set
		{
			base.Width = value;
		}
	}

	/// <summary>Gets the index position of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control within its zone.</summary>
	/// <returns>The numerical order of a control within its zone. The first control in a zone has an index value of zero.</returns>
	public int ZoneIndex => zoneIndex;

	/// <summary>Initializes the class for use by an inherited class instance. This constructor can only be called by an inherited class.</summary>
	protected WebPart()
	{
		verbs = new WebPartVerbCollection();
		allow = Allow.Close | Allow.Connect | Allow.Edit | Allow.Hide | Allow.Minimize | Allow.ZoneChange;
		auth_filter = "";
		catalog_icon_url = "";
		titleIconImageUrl = string.Empty;
		titleUrl = string.Empty;
		helpUrl = string.Empty;
		isStatic = false;
		hasUserData = false;
		hasSharedData = false;
		hidden = false;
		isClosed = false;
	}

	/// <summary>Sets a flag indicating that personalization data has changed for the current <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control instance. </summary>
	/// <exception cref="T:System.InvalidOperationException">
	///         <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.WebPartManager" /> is <see langword="null" />.</exception>
	[MonoTODO("Not implemented")]
	protected void SetPersonalizationDirty()
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets a flag indicating that personalization data has changed for the specified server control that resides in a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone.</summary>
	/// <param name="control">The <see cref="T:System.Web.UI.Control" /> for which the personalization data has changed.</param>
	/// <exception cref="T:System.ArgumentNullException">The object in the <paramref name="control" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The control is not associated with a page.</exception>
	/// <exception cref="T:System.InvalidOperationException">The page associated with the control does not have a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" />.</exception>
	/// <exception cref="T:System.ArgumentException">The control derives from <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />. Controls that derive from <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> should use the protected <see cref="M:System.Web.UI.WebControls.WebParts.WebPart.SetPersonalizationDirty" /> method instead. </exception>
	[MonoTODO("Not implemented")]
	public static void SetPersonalizationDirty(Control control)
	{
		throw new NotImplementedException();
	}

	/// <summary>Causes the control to track changes to its view state so they can be stored in the object's <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
	protected override void TrackViewState()
	{
		base.TrackViewState();
		foreach (IStateManager verb in verbs)
		{
			verb.TrackViewState();
		}
	}

	internal void SetZoneIndex(int index)
	{
		zoneIndex = index;
	}

	/// <summary>Enables derived classes to provide custom handling when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is closed on a Web Parts page.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected internal virtual void OnClosing(EventArgs e)
	{
	}

	/// <summary>Enables derived classes to provide custom handling when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is beginning or ending the process of connecting to other controls.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal virtual void OnConnectModeChanged(EventArgs e)
	{
	}

	/// <summary>Enables derived classes to provide custom handling when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is permanently removed from a Web Parts page.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal virtual void OnDeleting(EventArgs e)
	{
	}

	/// <summary>Enables derived classes to provide custom handling when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is entering or leaving edit mode.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal virtual void OnEditModeChanged(EventArgs e)
	{
	}
}
