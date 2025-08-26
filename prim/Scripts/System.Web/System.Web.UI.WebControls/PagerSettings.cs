using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents the properties of the paging controls in a control that supports pagination. This class cannot be inherited.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class PagerSettings : IStateManager
{
	private static readonly object propertyChangedEvent = new object();

	private StateBag ViewState = new StateBag();

	private Control ctrl;

	private EventHandlerList events = new EventHandlerList();

	/// <summary>Gets or sets the URL to an image to display for the first-page button.</summary>
	/// <returns>The URL to an image to display for the first-page button. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.PagerSettings.FirstPageImageUrl" /> is not set.</returns>
	[WebCategory("Appearance")]
	[NotifyParentProperty(true)]
	[UrlProperty]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string FirstPageImageUrl
	{
		get
		{
			object obj = ViewState["FirstPageImageUrl"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			ViewState["FirstPageImageUrl"] = value;
			RaisePropertyChanged();
		}
	}

	/// <summary>Gets or sets the text to display for the first-page button.</summary>
	/// <returns>The text to display for the first-page button. The default is "&amp;lt;&amp;lt;", which renders as "&lt;&lt;".</returns>
	[WebCategory("Appearance")]
	[DefaultValue("&lt;&lt;")]
	[NotifyParentProperty(true)]
	public string FirstPageText
	{
		get
		{
			object obj = ViewState["FirstPageText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "&lt;&lt;";
		}
		set
		{
			ViewState["FirstPageText"] = value;
			RaisePropertyChanged();
		}
	}

	/// <summary>Gets or sets the URL to an image to display for the last-page button.</summary>
	/// <returns>The URL to an image to display for the last-page button. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.PagerSettings.LastPageImageUrl" /> is not set.</returns>
	[WebCategory("Appearance")]
	[NotifyParentProperty(true)]
	[UrlProperty]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string LastPageImageUrl
	{
		get
		{
			object obj = ViewState["LastPageImageUrl"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			ViewState["LastPageImageUrl"] = value;
			RaisePropertyChanged();
		}
	}

	/// <summary>Gets or sets the text to display for the last-page button.</summary>
	/// <returns>The text to display for the last-page button. The default is "&amp;gt;&amp;gt;", which renders as "&gt;&gt;".</returns>
	[NotifyParentProperty(true)]
	[WebCategory("Appearance")]
	[DefaultValue("&gt;&gt;")]
	public string LastPageText
	{
		get
		{
			object obj = ViewState["LastPageText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "&gt;&gt;";
		}
		set
		{
			ViewState["LastPageText"] = value;
			RaisePropertyChanged();
		}
	}

	/// <summary>Gets or sets the mode in which to display the pager controls in a control that supports pagination.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.PagerButtons" /> values. The default is <see langword="PagerButtons.Numeric" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.PagerSettings.Mode" /> is set to a value that is not one of the <see cref="T:System.Web.UI.WebControls.PagerButtons" /> values.</exception>
	[NotifyParentProperty(true)]
	[WebCategory("Appearance")]
	[DefaultValue(PagerButtons.Numeric)]
	public PagerButtons Mode
	{
		get
		{
			object obj = ViewState["Mode"];
			if (obj != null)
			{
				return (PagerButtons)obj;
			}
			return PagerButtons.Numeric;
		}
		set
		{
			ViewState["Mode"] = value;
			RaisePropertyChanged();
		}
	}

	/// <summary>Gets or sets the URL to an image to display for the next-page button.</summary>
	/// <returns>The URL to an image to display for the next-page button. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.PagerSettings.NextPageImageUrl" /> is not set.</returns>
	[WebCategory("Appearance")]
	[NotifyParentProperty(true)]
	[UrlProperty]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string NextPageImageUrl
	{
		get
		{
			object obj = ViewState["NextPageImageUrl"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			ViewState["NextPageImageUrl"] = value;
			RaisePropertyChanged();
		}
	}

	/// <summary>Gets or sets the text to display for the next-page button.</summary>
	/// <returns>The text to display for the next-page button. The default is "&amp;gt;", which renders as "&gt;".</returns>
	[WebCategory("Appearance")]
	[NotifyParentProperty(true)]
	[DefaultValue("&gt;")]
	public string NextPageText
	{
		get
		{
			object obj = ViewState["NextPageText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "&gt;";
		}
		set
		{
			ViewState["NextPageText"] = value;
			RaisePropertyChanged();
		}
	}

	/// <summary>Gets or sets the number of page buttons to display in the pager when the <see cref="P:System.Web.UI.WebControls.PagerSettings.Mode" /> property is set to the <see cref="F:System.Web.UI.WebControls.PagerButtons.Numeric" /> or <see cref="F:System.Web.UI.WebControls.PagerButtons.NumericFirstLast" /> value.</summary>
	/// <returns>The number of page buttons to display in the pager. The default is 10.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.PagerSettings.PageButtonCount" /> is set to a value that is less than 1.</exception>
	[WebCategory("Behavior")]
	[NotifyParentProperty(true)]
	[DefaultValue(10)]
	public int PageButtonCount
	{
		get
		{
			object obj = ViewState["PageButtonCount"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 10;
		}
		set
		{
			ViewState["PageButtonCount"] = value;
			RaisePropertyChanged();
		}
	}

	/// <summary>Gets or sets a value that specifies the location where the pager is displayed.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.PagerPosition" /> values. The default is <see cref="F:System.Web.UI.WebControls.PagerPosition.Bottom" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.PagerSettings.Position" /> is set to a value that is not one of the <see cref="T:System.Web.UI.WebControls.PagerPosition" /> values.</exception>
	[WebCategory("Layout")]
	[DefaultValue(PagerPosition.Bottom)]
	[NotifyParentProperty(true)]
	public PagerPosition Position
	{
		get
		{
			object obj = ViewState["Position"];
			if (obj != null)
			{
				return (PagerPosition)obj;
			}
			return PagerPosition.Bottom;
		}
		set
		{
			ViewState["Position"] = value;
		}
	}

	/// <summary>Gets or sets the URL to an image to display for the previous-page button.</summary>
	/// <returns>The URL to an image to display for the previous-page button. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.PagerSettings.PreviousPageImageUrl" /> is not set.</returns>
	[WebCategory("Appearance")]
	[NotifyParentProperty(true)]
	[UrlProperty]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string PreviousPageImageUrl
	{
		get
		{
			object obj = ViewState["PreviousPageImageUrl"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			ViewState["PreviousPageImageUrl"] = value;
			RaisePropertyChanged();
		}
	}

	/// <summary>Gets or sets the text to display for the previous page button.</summary>
	/// <returns>The text to display for the previous page button. The default is "&amp;lt;", which renders as "&lt;".</returns>
	[WebCategory("Appearance")]
	[DefaultValue("&lt;")]
	[NotifyParentProperty(true)]
	public string PreviousPageText
	{
		get
		{
			object obj = ViewState["PreviousPageText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "&lt;";
		}
		set
		{
			ViewState["PreviousPageText"] = value;
			RaisePropertyChanged();
		}
	}

	/// <summary>Gets or sets a value indicating whether the paging controls are displayed in a control that supports pagination.</summary>
	/// <returns>
	///     <see langword="true" /> to display the pager; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[WebCategory("Appearance")]
	[DefaultValue(true)]
	[NotifyParentProperty(true)]
	public bool Visible
	{
		get
		{
			object obj = ViewState["Visible"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return true;
		}
		set
		{
			ViewState["Visible"] = value;
		}
	}

	/// <summary>Gets a value that indicates whether the server control is tracking its view state changes.</summary>
	/// <returns>
	///     <see langword="true" /> if the data source view is marked to save its state; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => ViewState.IsTrackingViewState;

	/// <summary>Occurs when a property of a <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object changes values.</summary>
	[Browsable(false)]
	public event EventHandler PropertyChanged
	{
		add
		{
			events.AddHandler(propertyChangedEvent, value);
		}
		remove
		{
			events.RemoveHandler(propertyChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> class.</summary>
	public PagerSettings()
	{
	}

	internal PagerSettings(Control ctrl)
	{
		this.ctrl = ctrl;
	}

	private void RaisePropertyChanged()
	{
		if (events[propertyChangedEvent] is EventHandler eventHandler)
		{
			eventHandler(this, EventArgs.Empty);
		}
	}

	/// <summary>Retrieves the string representation of a <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object.</summary>
	/// <returns>An empty string ("").</returns>
	public override string ToString()
	{
		return string.Empty;
	}

	/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object.</summary>
	/// <param name="state">An object that represents the state of the <see cref="T:System.Web.UI.WebControls.PagerSettings" />.</param>
	void IStateManager.LoadViewState(object savedState)
	{
		ViewState.LoadViewState(savedState);
	}

	/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object.</summary>
	/// <returns>An object that contains the saved state of the <see cref="T:System.Web.UI.WebControls.PagerSettings" />.</returns>
	object IStateManager.SaveViewState()
	{
		return ViewState.SaveViewState();
	}

	/// <summary>Marks the starting point at which to begin tracking and saving view state changes to the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object.</summary>
	void IStateManager.TrackViewState()
	{
		ViewState.TrackViewState();
	}

	internal Table CreatePagerControl(int currentPage, int pageCount)
	{
		Table table = new Table();
		TableRow tableRow = new TableRow();
		table.Rows.Add(tableRow);
		int num = ((Mode != PagerButtons.Numeric && Mode != PagerButtons.NumericFirstLast) ? 1 : PageButtonCount);
		int num2 = num * (currentPage / num);
		int num3 = num2 + num;
		if (num3 > pageCount)
		{
			num3 = pageCount;
			if (num3 - num2 < num)
			{
				num2 = num3 - num;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
		}
		if ((Mode == PagerButtons.NumericFirstLast || Mode == PagerButtons.NextPreviousFirstLast) && num2 > 0)
		{
			tableRow.Cells.Add(CreateCell(FirstPageText, FirstPageImageUrl, "Page", "First"));
		}
		if ((Mode == PagerButtons.NextPrevious || Mode == PagerButtons.NextPreviousFirstLast) && num2 > 0)
		{
			tableRow.Cells.Add(CreateCell(PreviousPageText, PreviousPageImageUrl, "Page", "Prev"));
		}
		if (Mode == PagerButtons.Numeric || Mode == PagerButtons.NumericFirstLast)
		{
			if (num2 > 0)
			{
				tableRow.Cells.Add(CreateCell("...", string.Empty, "Page", num2.ToString()));
			}
			for (int i = num2; i < num3; i++)
			{
				tableRow.Cells.Add(CreateCell((i + 1).ToString(), string.Empty, (i != currentPage) ? "Page" : "", (i != currentPage) ? (i + 1).ToString() : ""));
			}
			if (num3 < pageCount)
			{
				tableRow.Cells.Add(CreateCell("...", string.Empty, "Page", (num3 + 1).ToString()));
			}
		}
		if ((Mode == PagerButtons.NextPrevious || Mode == PagerButtons.NextPreviousFirstLast) && num3 < pageCount)
		{
			tableRow.Cells.Add(CreateCell(NextPageText, NextPageImageUrl, "Page", "Next"));
		}
		if ((Mode == PagerButtons.NumericFirstLast || Mode == PagerButtons.NextPreviousFirstLast) && num3 < pageCount)
		{
			tableRow.Cells.Add(CreateCell(LastPageText, LastPageImageUrl, "Page", "Last"));
		}
		return table;
	}

	private TableCell CreateCell(string text, string image, string command, string argument)
	{
		TableCell tableCell = new TableCell();
		Control child = ((!string.IsNullOrEmpty(command)) ? ((Control)DataControlButton.CreateButton((!string.IsNullOrEmpty(image)) ? ButtonType.Image : ButtonType.Link, ctrl, text, image, command, argument, allowCallback: true)) : new Label
		{
			Text = text
		});
		tableCell.Controls.Add(child);
		return tableCell;
	}
}
