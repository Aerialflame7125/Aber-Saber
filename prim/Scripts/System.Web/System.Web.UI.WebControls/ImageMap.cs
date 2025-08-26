using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Creates a control that displays an image on a page. When a hot spot region defined within the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control is clicked, the control either generates a postback to the server or navigates to a specified URL.</summary>
[ParseChildren(true, "HotSpots")]
[DefaultProperty("HotSpots")]
[DefaultEvent("Click")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class ImageMap : Image, IPostBackEventHandler
{
	private HotSpotCollection spots;

	private static readonly object ClickEvent;

	/// <summary>Gets or sets a value indicating whether the control can respond to user interaction.</summary>
	/// <returns>
	///     <see langword="true" /> if the control is to respond to user clicks; otherwise, <see langword="false" />.</returns>
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public override bool Enabled
	{
		get
		{
			return base.Enabled;
		}
		set
		{
			base.Enabled = value;
		}
	}

	/// <summary>Gets or sets the default behavior for the <see cref="T:System.Web.UI.WebControls.HotSpot" /> objects of an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control when the <see cref="T:System.Web.UI.WebControls.HotSpot" /> objects are clicked.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HotSpotMode" /> enumeration values. The default is <see langword="NotSet" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.HotSpotMode" /> enumeration values. </exception>
	[DefaultValue(HotSpotMode.NotSet)]
	public virtual HotSpotMode HotSpotMode
	{
		get
		{
			object obj = ViewState["HotSpotMode"];
			if (obj == null)
			{
				return HotSpotMode.NotSet;
			}
			return (HotSpotMode)obj;
		}
		set
		{
			ViewState["HotSpotMode"] = value;
		}
	}

	/// <summary>Gets or sets the target window or frame that displays the Web page content linked to when the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control is clicked.</summary>
	/// <returns>The target window or frame that displays the specified Web page when the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control is clicked. Values must begin with a letter in the range of A through Z (case-insensitive), except for the following special values, which begin with an underscore: 
	///             <see langword="_blank " />
	///           Renders the content in a new window without frames. 
	///             <see langword="_parent " />
	///           Renders the content in the immediate frameset parent. 
	///             <see langword="_search" />
	///           Renders the content in the search pane.
	///             <see langword="_self " />
	///           Renders the content in the frame with focus. 
	///             <see langword="_top " />
	///           Renders the content in the full window without frames. Check your browser documentation to determine if the <see langword="_search" /> value is supported.  For example, Microsoft Internet Explorer 5.0 and later support the <see langword="_search" /> target value.The default value is an empty string ("").</returns>
	[DefaultValue("")]
	public virtual string Target
	{
		get
		{
			object obj = ViewState["Target"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			ViewState["Target"] = value;
		}
	}

	/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.HotSpot" /> objects that represents the defined hot spot regions in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.HotSpotCollection" /> object that represents the defined hot spot regions in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control.</returns>
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public HotSpotCollection HotSpots
	{
		get
		{
			if (spots == null)
			{
				spots = new HotSpotCollection();
				if (base.IsTrackingViewState)
				{
					((IStateManager)spots).TrackViewState();
				}
			}
			return spots;
		}
	}

	/// <summary>Occurs when a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control is clicked.</summary>
	[Category("Action")]
	public event ImageMapEventHandler Click
	{
		add
		{
			base.Events.AddHandler(ClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ClickEvent, value);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ImageMap.Click" /> event for the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control.</summary>
	/// <param name="e">An argument of type <see cref="T:System.Web.UI.WebControls.ImageMapEventArgs" /> that contains the event data. </param>
	protected virtual void OnClick(ImageMapEventArgs e)
	{
		if (base.Events != null)
		{
			((ImageMapEventHandler)base.Events[Click])?.Invoke(this, e);
		}
	}

	/// <summary>Tracks view-state changes to the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control so they can be stored in the control's <see cref="T:System.Web.UI.StateBag" /> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
	protected override void TrackViewState()
	{
		base.TrackViewState();
		if (spots != null)
		{
			((IStateManager)spots).TrackViewState();
		}
	}

	/// <summary>Saves any changes to an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control's view-state that have occurred since the time the page was posted back to the server.</summary>
	/// <returns>Returns the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control's current view state. If there is no view state associated with the control, this method returns <see langword="null" />.</returns>
	protected override object SaveViewState()
	{
		object obj = base.SaveViewState();
		object obj2 = ((spots != null) ? ((IStateManager)spots).SaveViewState() : null);
		if (obj != null || obj2 != null)
		{
			return new Pair(obj, obj2);
		}
		return null;
	}

	/// <summary>Restores view-state information for the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control from a previous page request that was saved by the <see cref="M:System.Web.UI.WebControls.ImageMap.SaveViewState" /> method.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control to restore. </param>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="savedState" /> is not a valid <see cref="P:System.Web.UI.Control.ViewState" />.</exception>
	protected override void LoadViewState(object savedState)
	{
		if (savedState == null)
		{
			base.LoadViewState((object)null);
			return;
		}
		Pair pair = (Pair)savedState;
		base.LoadViewState(pair.First);
		((IStateManager)HotSpots).LoadViewState(pair.Second);
	}

	/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control when a form is posted back to the server.</summary>
	/// <param name="eventArgument">The argument for the event.</param>
	protected virtual void RaisePostBackEvent(string eventArgument)
	{
		ValidateEvent(UniqueID, eventArgument);
		HotSpot hotSpot = HotSpots[int.Parse(eventArgument)];
		OnClick(new ImageMapEventArgs(hotSpot.PostBackValue));
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" />. </summary>
	/// <param name="eventArgument">The argument for the event.</param>
	void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
	{
		RaisePostBackEvent(eventArgument);
	}

	/// <summary>Adds the HTML attributes and styles of an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" />.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		base.AddAttributesToRender(writer);
		if (spots != null && spots.Count > 0)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Usemap, "#ImageMap" + ClientID);
		}
	}

	/// <summary>Sends the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to render on the client.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control content. </param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		base.Render(writer);
		if (spots == null || spots.Count <= 0)
		{
			return;
		}
		bool flag = Enabled;
		writer.AddAttribute(HtmlTextWriterAttribute.Id, "ImageMap" + ClientID);
		writer.AddAttribute(HtmlTextWriterAttribute.Name, "ImageMap" + ClientID);
		writer.RenderBeginTag(HtmlTextWriterTag.Map);
		for (int i = 0; i < spots.Count; i++)
		{
			HotSpot hotSpot = spots[i];
			writer.AddAttribute(HtmlTextWriterAttribute.Shape, hotSpot.MarkupName);
			writer.AddAttribute(HtmlTextWriterAttribute.Coords, hotSpot.GetCoordinates());
			writer.AddAttribute(HtmlTextWriterAttribute.Title, hotSpot.AlternateText);
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, hotSpot.AlternateText);
			if (hotSpot.AccessKey.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, hotSpot.AccessKey);
			}
			if (hotSpot.TabIndex != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, hotSpot.TabIndex.ToString());
			}
			switch ((hotSpot.HotSpotMode != 0) ? hotSpot.HotSpotMode : HotSpotMode)
			{
			case HotSpotMode.Inactive:
				writer.AddAttribute("nohref", "true", fEndode: false);
				break;
			case HotSpotMode.Navigate:
			{
				string value = ((hotSpot.Target.Length > 0) ? hotSpot.Target : Target);
				if (!string.IsNullOrEmpty(value))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Target, value);
				}
				if (flag)
				{
					string value2 = ResolveClientUrl(hotSpot.NavigateUrl);
					writer.AddAttribute(HtmlTextWriterAttribute.Href, value2);
				}
				break;
			}
			case HotSpotMode.PostBack:
				writer.AddAttribute(HtmlTextWriterAttribute.Href, Page.ClientScript.GetPostBackClientHyperlink(this, i.ToString(), registerForEventValidation: true));
				break;
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Area);
			writer.RenderEndTag();
		}
		writer.RenderEndTag();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ImageMap" /> class. </summary>
	public ImageMap()
	{
	}

	static ImageMap()
	{
		Click = new object();
	}
}
