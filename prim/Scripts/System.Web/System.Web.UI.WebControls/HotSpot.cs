using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Implements the basic functionality common to all hot spot shapes.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class HotSpot : IStateManager
{
	private StateBag viewState = new StateBag();

	/// <summary>Gets or sets the access key that allows you to quickly navigate to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region.</summary>
	/// <returns>The access key for quick navigation to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified access key is neither <see langword="null" />, an empty string (""), nor a single character string. </exception>
	[Localizable(true)]
	[DefaultValue("")]
	public virtual string AccessKey
	{
		get
		{
			object obj = viewState["AccessKey"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			if (value == null || value.Length < 2)
			{
				viewState["AccessKey"] = value;
				return;
			}
			throw new ArgumentOutOfRangeException("value", "AccessKey can only be null, empty or a single character");
		}
	}

	/// <summary>Gets or sets the alternate text to display for a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control when the image is unavailable or renders to a browser that does not support images.</summary>
	/// <returns>The text displayed in place of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> when the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control's image is unavailable. The default value is an empty string ("").</returns>
	[Localizable(true)]
	[NotifyParentProperty(true)]
	[WebCategory("Behavior")]
	[DefaultValue("")]
	[Bindable(true)]
	public virtual string AlternateText
	{
		get
		{
			object obj = viewState["AlternateText"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			viewState["AlternateText"] = value;
		}
	}

	/// <summary>Gets or sets the behavior of a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control when the <see cref="T:System.Web.UI.WebControls.HotSpot" /> is clicked.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HotSpotMode" /> enumeration values. The default is <see langword="Default" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.HotSpotMode" /> enumeration values. </exception>
	[WebCategory("Behavior")]
	[DefaultValue(HotSpotMode.NotSet)]
	[NotifyParentProperty(true)]
	public virtual HotSpotMode HotSpotMode
	{
		get
		{
			object obj = viewState["HotSpotMode"];
			if (obj == null)
			{
				return HotSpotMode.NotSet;
			}
			return (HotSpotMode)obj;
		}
		set
		{
			if (value < HotSpotMode.NotSet || value > HotSpotMode.Inactive)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			viewState["HotSpotMode"] = value;
		}
	}

	/// <summary>Gets or sets the URL to navigate to when a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object is clicked.</summary>
	/// <returns>The URL to navigate to when a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object is clicked. The default is an empty string ("").</returns>
	[DefaultValue("")]
	[Bindable(true)]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[NotifyParentProperty(true)]
	[UrlProperty]
	public string NavigateUrl
	{
		get
		{
			object obj = viewState["NavigateUrl"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			viewState["NavigateUrl"] = value;
		}
	}

	/// <summary>Gets or sets the name of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to pass in the event data when the <see cref="T:System.Web.UI.WebControls.HotSpot" /> is clicked.</summary>
	/// <returns>The name of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to pass in the event data when the <see cref="T:System.Web.UI.WebControls.HotSpot" /> is clicked. The default is an empty string ("").</returns>
	[Bindable(true)]
	[WebCategory("Behavior")]
	[DefaultValue("")]
	[NotifyParentProperty(true)]
	public string PostBackValue
	{
		get
		{
			object obj = viewState["PostBackValue"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			viewState["PostBackValue"] = value;
		}
	}

	/// <summary>Gets or sets the tab index of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region.</summary>
	/// <returns>The tab index of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region. The default is 0, which indicates that this property is not set.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified tab index is not between -32768 and 32767. </exception>
	[DefaultValue(0)]
	[WebCategory("Accessibility")]
	public virtual short TabIndex
	{
		get
		{
			object obj = viewState["TabIndex"];
			if (obj == null)
			{
				return 0;
			}
			return (short)obj;
		}
		set
		{
			viewState["TabIndex"] = value;
		}
	}

	/// <summary>Gets or sets the target window or frame in which to display the Web page content linked to when a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object that navigates to a URL is clicked.</summary>
	/// <returns>The target window or frame in which to load the Web page linked to when a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object that navigates to a URL is clicked. The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
	[WebCategory("Behavior")]
	[NotifyParentProperty(true)]
	[DefaultValue("")]
	[TypeConverter(typeof(TargetConverter))]
	public virtual string Target
	{
		get
		{
			object obj = viewState["Target"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			viewState["Target"] = value;
		}
	}

	/// <summary>Gets a dictionary of state information that allows you to save and restore the view state of a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object across multiple requests for the same page.</summary>
	/// <returns>An instance of the <see cref="T:System.Web.UI.StateBag" /> class that contains the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region's view-state information.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	protected StateBag ViewState => viewState;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object is tracking its view-state changes.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object is tracking its view-state changes; otherwise, <see langword="false" />.</returns>
	protected virtual bool IsTrackingViewState => viewState.IsTrackingViewState;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object is tracking its view-state changes.  </summary>
	/// <returns>
	///     <see langword="true" /> if a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object is tracking its view-state changes; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => IsTrackingViewState;

	/// <summary>When overridden in a derived class, gets the string representation for the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's shape.</summary>
	/// <returns>A string that represents the name of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's shape.</returns>
	protected internal abstract string MarkupName { get; }

	/// <summary>Restores the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's previously saved view state to the object.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to restore. </param>
	protected virtual void LoadViewState(object savedState)
	{
		viewState.LoadViewState(savedState);
	}

	/// <summary>Saves the changes to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's view state since the time the page was posted back to the server.</summary>
	/// <returns>The <see cref="T:System.Object" /> that contains the changes to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's view state. If no view state is associated with the object, this method returns <see langword="null" />.</returns>
	protected virtual object SaveViewState()
	{
		return viewState.SaveViewState();
	}

	/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to track changes to its view state so they can be stored in the object's <see cref="T:System.Web.UI.StateBag" /> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
	protected virtual void TrackViewState()
	{
		viewState.TrackViewState();
	}

	/// <summary>Restores the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's previously saved view state to the object.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that contains the saved view state values for the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to restore. </param>
	void IStateManager.LoadViewState(object savedState)
	{
		LoadViewState(savedState);
	}

	/// <summary>Saves the changes to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's view state since the last time the page was posted back to the server.</summary>
	/// <returns>The <see cref="T:System.Object" /> that contains the changes to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's view state.</returns>
	object IStateManager.SaveViewState()
	{
		return SaveViewState();
	}

	/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region to track changes to its view state.</summary>
	void IStateManager.TrackViewState()
	{
		TrackViewState();
	}

	/// <summary>Returns the <see cref="T:System.String" /> representation of this instance of a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object.</summary>
	/// <returns>A string that represents this <see cref="T:System.Web.UI.WebControls.HotSpot" /> object.</returns>
	public override string ToString()
	{
		return GetType().Name;
	}

	internal void SetDirty()
	{
		viewState.SetDirty(dirty: true);
	}

	/// <summary>When overridden in a derived class, returns a string that represents the coordinates of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region.</summary>
	/// <returns>A string that represents the coordinates of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region.</returns>
	public abstract string GetCoordinates();

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> class.</summary>
	protected HotSpot()
	{
	}
}
