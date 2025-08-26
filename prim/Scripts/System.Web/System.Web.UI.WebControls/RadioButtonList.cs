using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a list control that encapsulates a group of radio button controls.</summary>
[ValidationProperty("SelectedItem")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class RadioButtonList : ListControl, IRepeatInfoUser, INamingContainer, IPostBackDataHandler
{
	private short tabIndex;

	/// <summary>Gets or sets the distance (in pixels) between the border and the contents of the table cell.</summary>
	/// <returns>The distance (in pixels) between the border and the contents of the table cell. The default is -1, which indicates that this property is not set.</returns>
	[DefaultValue(-1)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual int CellPadding
	{
		get
		{
			if (!base.ControlStyleCreated)
			{
				return -1;
			}
			return ((TableStyle)base.ControlStyle).CellPadding;
		}
		set
		{
			((TableStyle)base.ControlStyle).CellPadding = value;
		}
	}

	/// <summary>Gets or sets the distance (in pixels) between adjacent table cells.</summary>
	/// <returns>The distance (in pixels) between adjacent table cells. The default is -1, which indicates that this property is not set.</returns>
	[DefaultValue(-1)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual int CellSpacing
	{
		get
		{
			if (!base.ControlStyleCreated)
			{
				return -1;
			}
			return ((TableStyle)base.ControlStyle).CellSpacing;
		}
		set
		{
			((TableStyle)base.ControlStyle).CellSpacing = value;
		}
	}

	/// <summary>Gets or sets the number of columns to display in the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control.</summary>
	/// <returns>The number of columns to display in the <see cref="T:System.Web.UI.WebControls.RadioButtonList" />. The default is 0, which indicates that this property is not set.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The number of columns is set to a negative value. </exception>
	[DefaultValue(0)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual int RepeatColumns
	{
		get
		{
			return ViewState.GetInt("RepeatColumns", 0);
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("The number of columns is set to a negative value.");
			}
			ViewState["RepeatColumns"] = value;
		}
	}

	/// <summary>Gets or sets the direction in which the radio buttons within the group are displayed.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> values. The default is <see langword="Vertical" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The display direction of the list is not one of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> values. </exception>
	[DefaultValue(RepeatDirection.Vertical)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual RepeatDirection RepeatDirection
	{
		get
		{
			return (RepeatDirection)ViewState.GetInt("RepeatDirection", 1);
		}
		set
		{
			if (value != 0 && value != RepeatDirection.Vertical)
			{
				throw new ArgumentOutOfRangeException("he display direction of the list is not one of the RepeatDirection values.");
			}
			ViewState["RepeatDirection"] = value;
		}
	}

	/// <summary>Gets or sets a value that specifies whether the list will be rendered by using a <see langword="table" /> element, a <see langword="ul" /> element, an <see langword="ol" /> element, or a <see langword="span" /> element.</summary>
	/// <returns>A value that specifies whether the list will be rendered by using a <see langword="table" /> element, a <see langword="ul" /> element, an <see langword="ol" /> element, or a <see langword="span" /> element. The default is <see cref="F:System.Web.UI.WebControls.RepeatLayout.Table" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The radio button layout is not one of the <see cref="T:System.Web.UI.WebControls.RepeatLayout" /> values. </exception>
	[DefaultValue(RepeatLayout.Table)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual RepeatLayout RepeatLayout
	{
		get
		{
			return (RepeatLayout)ViewState.GetInt("RepeatLayout", 0);
		}
		set
		{
			if (value < RepeatLayout.Table || value > RepeatLayout.OrderedList)
			{
				throw new ArgumentOutOfRangeException("The radio buttons layout is not one of the RepeatLayout values.");
			}
			ViewState["RepeatLayout"] = value;
		}
	}

	/// <summary>Gets or sets the text alignment for the radio buttons within the group.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TextAlign" /> values. The default is <see langword="Right" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The label text alignment associated with the radio buttons is not one of the <see cref="T:System.Web.UI.WebControls.TextAlign" /> values. </exception>
	[DefaultValue(TextAlign.Right)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual TextAlign TextAlign
	{
		get
		{
			return (TextAlign)ViewState.GetInt("TextAlign", 2);
		}
		set
		{
			if (value != TextAlign.Left && value != TextAlign.Right)
			{
				throw new ArgumentOutOfRangeException("The label text alignment associated with the radio buttons is not one of the TextAlign values.");
			}
			ViewState["TextAlign"] = value;
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control contains a footer section.</summary>
	/// <returns>
	///     <see langword="false" />, indicating that the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> does not contain a footer section.</returns>
	protected virtual bool HasFooter => false;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control contains a heading section.</summary>
	/// <returns>
	///     <see langword="false" />, indicating that a <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control does not contain a heading section.</returns>
	protected virtual bool HasHeader => false;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control contains separators between items in the list.</summary>
	/// <returns>
	///     <see langword="false" />, indicating that a <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control does not contain separators.</returns>
	protected virtual bool HasSeparators => false;

	/// <summary>Gets the number of list items in the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control.</summary>
	/// <returns>The number of items in the list control.</returns>
	protected virtual int RepeatedItemCount => Items.Count;

	/// <summary>Gets a value that indicates whether the list control contains a footer section.</summary>
	/// <returns>
	///     <see langword="true" /> if the list control contains a footer section; otherwise, <see langword="false" />. </returns>
	bool IRepeatInfoUser.HasFooter => HasFooter;

	/// <summary>Gets a value that indicates whether the list control contains a heading section.</summary>
	/// <returns>
	///     <see langword="true" /> if the list control contains a header section; otherwise, <see langword="false" />. </returns>
	bool IRepeatInfoUser.HasHeader => HasHeader;

	/// <summary>Gets a value that indicates whether the list control contains a separator between items in the list.</summary>
	/// <returns>
	///     <see langword="true" /> if the list control contains has separators; otherwise, <see langword="false" />. </returns>
	bool IRepeatInfoUser.HasSeparators => HasSeparators;

	/// <summary>Gets the number of items in the list control.</summary>
	/// <returns>The number of items in the control.</returns>
	int IRepeatInfoUser.RepeatedItemCount => RepeatedItemCount;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> class.</summary>
	public RadioButtonList()
	{
	}

	/// <summary>Creates a style object that is used internally by the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control to implement all style-related properties.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains the style properties of the control.</returns>
	protected override Style CreateControlStyle()
	{
		return new TableStyle(ViewState);
	}

	/// <summary>Searches the current naming container for a server control with the specified ID and path offset. The <see cref="M:System.Web.UI.WebControls.RadioButtonList.FindControl(System.String,System.Int32)" /> method always returns the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> object.</summary>
	/// <param name="id">The identifier for the control to find.</param>
	/// <param name="pathOffset">The number of controls up the page control hierarchy needed to reach a naming container. </param>
	/// <returns>The current <see cref="T:System.Web.UI.WebControls.RadioButtonList" />.</returns>
	protected override Control FindControl(string id, int pathOffset)
	{
		return this;
	}

	/// <summary>Retrieves the style of the specified item type at the specified index in the list control.</summary>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
	/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
	/// <returns>
	///     <see langword="null" />, indicating that style attributes are not set on individual list items in a <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control.</returns>
	protected virtual Style GetItemStyle(ListItemType itemType, int repeatIndex)
	{
		return null;
	}

	/// <summary>Renders a list item in the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control.</summary>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
	/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
	/// <param name="repeatInfo">A <see cref="T:System.Web.UI.WebControls.RepeatInfo" /> that represents the information used to render the item in the list. </param>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
	protected virtual void RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
	{
		ListItem listItem = Items[repeatIndex];
		RadioButton radioButton = new RadioButton();
		radioButton.Text = listItem.Text;
		radioButton.ID = ClientID + "_" + repeatIndex;
		radioButton.TextAlign = TextAlign;
		radioButton.GroupName = UniqueID;
		radioButton.Page = Page;
		radioButton.Checked = listItem.Selected;
		radioButton.ValueAttribute = listItem.Value;
		radioButton.AutoPostBack = AutoPostBack;
		radioButton.Enabled = base.IsEnabled;
		radioButton.TabIndex = tabIndex;
		radioButton.ValidationGroup = ValidationGroup;
		radioButton.CausesValidation = CausesValidation;
		if (radioButton.HasAttributes)
		{
			radioButton.Attributes.Clear();
		}
		if (listItem.HasAttributes)
		{
			radioButton.Attributes.CopyFrom(listItem.Attributes);
		}
		radioButton.RenderControl(writer);
	}

	/// <summary>Processes the posted data for the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control.</summary>
	/// <param name="postDataKey">The key identifier for the control, used to index the <paramref name="postCollection" />.</param>
	/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains value information indexed by control identifiers. </param>
	/// <returns>
	///     <see langword="true" /> if the state of the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> is different from the last posting; otherwise, <see langword="false" />.</returns>
	protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		EnsureDataBound();
		string text = postCollection[postDataKey];
		ListItemCollection listItemCollection = Items;
		int count = listItemCollection.Count;
		int selectedIndex = SelectedIndex;
		for (int i = 0; i < count; i++)
		{
			ListItem listItem = listItemCollection[i];
			if (listItem != null && !(text != listItem.Value) && i != selectedIndex)
			{
				SelectedIndex = i;
				return true;
			}
		}
		return false;
	}

	/// <summary>Notifies the ASP.NET application that the state of the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control has changed.</summary>
	protected virtual void RaisePostDataChangedEvent()
	{
		ValidateEvent(UniqueID, string.Empty);
		Page page = Page;
		if (CausesValidation)
		{
			page?.Validate(ValidationGroup);
		}
		OnSelectedIndexChanged(EventArgs.Empty);
	}

	/// <summary>Processes posted data for the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control.</summary>
	/// <param name="postDataKey">The key identifier for the control, used to index the <paramref name="postCollection" />. </param>
	/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains value information indexed by control identifiers.</param>
	/// <returns>
	///     <see langword="true" /> if the server control's state changed as a result of the postback; otherwise, <see langword="false" />.</returns>
	bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostData(postDataKey, postCollection);
	}

	/// <summary>Raised when posted data for a control has changed.</summary>
	void IPostBackDataHandler.RaisePostDataChangedEvent()
	{
		RaisePostDataChangedEvent();
	}

	/// <summary>Retrieves the style of the specified item type at the specified index in the list control.</summary>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
	/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style of the specified item type at the specified index in the list control.</returns>
	Style IRepeatInfoUser.GetItemStyle(ListItemType itemType, int repeatIndex)
	{
		return GetItemStyle(itemType, repeatIndex);
	}

	/// <summary>Renders an item in the list with the specified information.</summary>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
	/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list. </param>
	/// <param name="repeatInfo">A <see cref="T:System.Web.UI.WebControls.RepeatInfo" /> that represents the information used to render the item in the list. </param>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
	void IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
	{
		RenderItem(itemType, repeatIndex, repeatInfo, writer);
	}

	/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.RadioButtonList" /> control on the client.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream for rendering on the client. </param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		Page?.ClientScript.RegisterForEventValidation(UniqueID);
		if (Items.Count != 0)
		{
			RepeatInfo obj = new RepeatInfo
			{
				RepeatColumns = RepeatColumns,
				RepeatDirection = RepeatDirection,
				RepeatLayout = RepeatLayout
			};
			tabIndex = TabIndex;
			TabIndex = 0;
			obj.RenderRepeater(writer, this, base.ControlStyle, this);
			TabIndex = tabIndex;
		}
	}
}
