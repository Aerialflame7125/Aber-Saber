using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.WebControls;

/// <summary>Creates a multi selection check box group that can be dynamically created by binding the control to a data source.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class CheckBoxList : ListControl, IRepeatInfoUser, INamingContainer, IPostBackDataHandler
{
	private CheckBox check_box;

	/// <summary>Gets or sets the distance (in pixels) between the border and contents of the cell.</summary>
	/// <returns>The distance (in pixels) between the border and contents of the cell. The default is -1, which indicates this property is not set.</returns>
	[DefaultValue(-1)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual int CellPadding
	{
		get
		{
			return TableStyle.CellPadding;
		}
		set
		{
			TableStyle.CellPadding = value;
		}
	}

	/// <summary>Gets or sets the distance (in pixels) between cells.</summary>
	/// <returns>The distance (in pixels) between cells. The default is <see langword="-1" />, which indicates that this property is not set.</returns>
	[DefaultValue(-1)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual int CellSpacing
	{
		get
		{
			return TableStyle.CellSpacing;
		}
		set
		{
			TableStyle.CellSpacing = value;
		}
	}

	/// <summary>Gets or sets the number of columns to display in the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control.</summary>
	/// <returns>The number of columns to display in the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control. The default is <see langword="0" />, which indicates this property is not set.</returns>
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
				throw new ArgumentOutOfRangeException("value");
			}
			ViewState["RepeatColumns"] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the control displays vertically or horizontally.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> values. The default is <see langword="Vertical" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified display direction of the list is not one of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> values. </exception>
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
			if (value < RepeatDirection.Horizontal || value > RepeatDirection.Vertical)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			ViewState["RepeatDirection"] = value;
		}
	}

	/// <summary>Gets or sets a value that specifies whether the list will be rendered by using a <see langword="table" /> element, a <see langword="ul" /> element, an <see langword="ol" /> element, or a <see langword="span" /> element.</summary>
	/// <returns>A value that specifies whether the list will be rendered by using a <see langword="table" /> element, a <see langword="ul" /> element, an <see langword="ol" /> element, or a <see langword="span" /> element. The default is <see cref="F:System.Web.UI.WebControls.RepeatLayout.Table" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified layout is not one of the <see cref="T:System.Web.UI.WebControls.RepeatLayout" /> values. </exception>
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
				throw new ArgumentOutOfRangeException("value");
			}
			ViewState["RepeatLayout"] = value;
		}
	}

	/// <summary>Gets or sets the text alignment for the check boxes within the group.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TextAlign" /> values. The default value is <see langword="Right" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified label text alignment is not one of the <see cref="T:System.Web.UI.WebControls.TextAlign" /> values. </exception>
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
			if (value < TextAlign.Left || value > TextAlign.Right)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			ViewState["TextAlign"] = value;
		}
	}

	private TableStyle TableStyle => (TableStyle)base.ControlStyle;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control contains a footer section.</summary>
	/// <returns>
	///     <see langword="false" />, indicating that a <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> does not contain a footer section.</returns>
	protected virtual bool HasFooter => false;

	/// <summary>Gets a value that indicates whether the list control contains a footer section.</summary>
	/// <returns>
	///     <see langword="true" /> if the list control contains a footer section; otherwise, <see langword="false" />.</returns>
	bool IRepeatInfoUser.HasFooter => HasFooter;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control contains a heading section. </summary>
	/// <returns>
	///     <see langword="false" />, indicating that a <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> does not contain a heading section.</returns>
	protected virtual bool HasHeader => false;

	/// <summary>Gets a value that indicates whether the list control contains a heading section.</summary>
	/// <returns>
	///     <see langword="true" /> if the list control contains a heading section; otherwise, <see langword="false" />.</returns>
	bool IRepeatInfoUser.HasHeader => HasHeader;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control contains a separator between items in the list. </summary>
	/// <returns>
	///     <see langword="false" />, indicating that a <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> does not contain separators.</returns>
	protected virtual bool HasSeparators => false;

	/// <summary>Gets a value that indicates whether the list control contains a separator between items in the list.</summary>
	/// <returns>
	///     <see langword="true" /> if the list control contains a separator; otherwise, <see langword="false" />.</returns>
	bool IRepeatInfoUser.HasSeparators => HasSeparators;

	/// <summary>Gets the number of list items in the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control. </summary>
	/// <returns>The number of items in the <see cref="T:System.Web.UI.WebControls.CheckBoxList" />.</returns>
	protected virtual int RepeatedItemCount => Items.Count;

	/// <summary>Gets the number of items in the list control.</summary>
	/// <returns>The number of items in the list.</returns>
	int IRepeatInfoUser.RepeatedItemCount => RepeatedItemCount;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> class.</summary>
	public CheckBoxList()
	{
		check_box = new CheckBox();
		Controls.Add(check_box);
	}

	/// <summary>Creates a style object that is used internally by the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control to implement all style related properties.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains the style properties of the control.</returns>
	protected override Style CreateControlStyle()
	{
		return new TableStyle(ViewState);
	}

	/// <summary>Searches the current naming container for a server control with the specified ID and path offset. The <see cref="M:System.Web.UI.WebControls.CheckBoxList.FindControl(System.String,System.Int32)" /> method always returns the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> object. </summary>
	/// <param name="id">The identifier for the control to find.</param>
	/// <param name="pathOffset">The number of controls up the page control hierarchy needed to reach a naming container. </param>
	/// <returns>The current <see cref="T:System.Web.UI.WebControls.CheckBoxList" />.</returns>
	protected override Control FindControl(string id, int pathOffset)
	{
		return this;
	}

	/// <summary>Configures the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control prior to rendering on the client.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
		Page page = Page;
		for (int i = 0; i < Items.Count; i++)
		{
			if (Items[i].Selected)
			{
				check_box.ID = i.ToString(Helpers.InvariantCulture);
				page?.RegisterRequiresPostBack(check_box);
			}
		}
	}

	/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> on the client.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream for rendering on the client. </param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		if (Items.Count != 0)
		{
			RepeatInfo obj = new RepeatInfo
			{
				RepeatColumns = RepeatColumns,
				RepeatDirection = RepeatDirection,
				RepeatLayout = RepeatLayout
			};
			short num = 0;
			if (TabIndex != 0)
			{
				check_box.TabIndex = TabIndex;
				num = TabIndex;
				TabIndex = 0;
			}
			string accessKey = AccessKey;
			check_box.AccessKey = accessKey;
			AccessKey = null;
			obj.RenderRepeater(writer, this, TableStyle, this);
			if (num != 0)
			{
				TabIndex = num;
			}
			AccessKey = accessKey;
		}
	}

	/// <summary>Processes the posted data for the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control.</summary>
	/// <param name="postDataKey">The key identifier for the control, used to index the <see cref="T:System.Collections.Specialized.NameValueCollection" /> specified in the <paramref name="postCollection" /> parameter.</param>
	/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains value information indexed by control identifiers. </param>
	/// <returns>
	///     <see langword="true" /> if the state of the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> is different from the last posting; otherwise, <see langword="false" />.</returns>
	protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		if (!base.IsEnabled)
		{
			return false;
		}
		EnsureDataBound();
		int num = -1;
		try
		{
			string text = postDataKey.Substring(ClientID.Length + 1);
			if (char.IsDigit(text[0]))
			{
				num = int.Parse(text, Helpers.InvariantCulture);
			}
		}
		catch
		{
			return false;
		}
		if (num == -1)
		{
			return false;
		}
		bool flag = postCollection[postDataKey] == "on";
		ListItem listItem = Items[num];
		if (listItem.Enabled)
		{
			if (flag && !listItem.Selected)
			{
				listItem.Selected = true;
				return true;
			}
			if (!flag && listItem.Selected)
			{
				listItem.Selected = false;
				return true;
			}
		}
		return false;
	}

	/// <summary>Notifies the ASP.NET application that the state of the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control has changed.</summary>
	protected virtual void RaisePostDataChangedEvent()
	{
		if (CausesValidation)
		{
			Page?.Validate(ValidationGroup);
		}
		OnSelectedIndexChanged(EventArgs.Empty);
	}

	/// <summary>Processes posted data for the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control.</summary>
	/// <param name="postDataKey">The key identifier for the control, used to index the <paramref name="postCollection" />.</param>
	/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that contains value information indexed by control identifiers. </param>
	/// <returns>
	///     <see langword="true" /> if the server control's state changes as a result of the postback; otherwise, <see langword="false" />.</returns>
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
	/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
	/// <returns>
	///     <see langword="null" />, indicating that style attributes are not set on individual list items in a <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control.</returns>
	protected virtual Style GetItemStyle(ListItemType itemType, int repeatIndex)
	{
		return null;
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.WebControls.IRepeatInfoUser.GetItemStyle(System.Web.UI.WebControls.ListItemType,System.Int32)" />. </summary>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
	/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object that represents the style of the specified item type at the specified index in the list control.</returns>
	Style IRepeatInfoUser.GetItemStyle(ListItemType itemType, int repeatIndex)
	{
		return GetItemStyle(itemType, repeatIndex);
	}

	/// <summary>Renders a list item in the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control.</summary>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
	/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
	/// <param name="repeatInfo">A <see cref="T:System.Web.UI.WebControls.RepeatInfo" /> object that represents the information used to render the item in the list. </param>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream to render HTML content on the client. </param>
	protected virtual void RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
	{
		ListItem listItem = Items[repeatIndex];
		if (!string.IsNullOrEmpty(check_box.CssClass))
		{
			check_box.CssClass = string.Empty;
		}
		check_box.ID = repeatIndex.ToString(Helpers.InvariantCulture);
		check_box.Text = listItem.Text;
		check_box.AutoPostBack = AutoPostBack;
		check_box.Checked = listItem.Selected;
		check_box.TextAlign = TextAlign;
		if (!base.IsEnabled)
		{
			check_box.Enabled = false;
		}
		else
		{
			check_box.Enabled = listItem.Enabled;
		}
		check_box.ValidationGroup = ValidationGroup;
		check_box.CausesValidation = CausesValidation;
		if (check_box.HasAttributes)
		{
			check_box.Attributes.Clear();
		}
		if (listItem.HasAttributes)
		{
			check_box.Attributes.CopyFrom(listItem.Attributes);
		}
		if (!base.RenderingCompatibilityLessThan40)
		{
			AttributeCollection inputAttributes = check_box.InputAttributes;
			inputAttributes.Clear();
			inputAttributes.Add("value", listItem.Value);
		}
		check_box.RenderControl(writer);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.WebControls.IRepeatInfoUser.RenderItem(System.Web.UI.WebControls.ListItemType,System.Int32,System.Web.UI.WebControls.RepeatInfo,System.Web.UI.HtmlTextWriter)" />. </summary>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
	/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
	/// <param name="repeatInfo">A <see cref="T:System.Web.UI.WebControls.RepeatInfo" /> object that represents the information used to render the item in the list. </param>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream to render HTML content on the client. </param>
	void IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
	{
		RenderItem(itemType, repeatIndex, repeatInfo, writer);
	}

	internal override bool MultiSelectOk()
	{
		return true;
	}
}
