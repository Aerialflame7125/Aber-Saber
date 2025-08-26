using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Serves as the base class for the different column types of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class DataGridColumn : IStateManager
{
	internal class ForeColorLinkButton : LinkButton
	{
		private Color GetForeColor(WebControl control)
		{
			if (control == null)
			{
				return Color.Empty;
			}
			if (control is Table)
			{
				return control.ControlStyle.ForeColor;
			}
			Color foreColor = control.ControlStyle.ForeColor;
			if (foreColor != Color.Empty)
			{
				return foreColor;
			}
			return GetForeColor((WebControl)control.Parent);
		}

		protected internal override void Render(HtmlTextWriter writer)
		{
			Color foreColor = GetForeColor(this);
			if (foreColor != Color.Empty)
			{
				ForeColor = foreColor;
			}
			base.Render(writer);
		}
	}

	private DataGrid owner;

	private StateBag viewstate;

	private bool tracking_viewstate;

	private bool design;

	private TableItemStyle footer_style;

	private TableItemStyle header_style;

	private TableItemStyle item_style;

	/// <summary>Gets the style properties for the footer section of the column.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the footer section of the column. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public virtual TableItemStyle FooterStyle
	{
		get
		{
			if (footer_style == null)
			{
				footer_style = new TableItemStyle();
				if (tracking_viewstate)
				{
					footer_style.TrackViewState();
				}
			}
			return footer_style;
		}
	}

	/// <summary>Gets or sets the text displayed in the footer section of the column.</summary>
	/// <returns>The text displayed in the footer section of the column. The default value is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public virtual string FooterText
	{
		get
		{
			return viewstate.GetString("FooterText", string.Empty);
		}
		set
		{
			viewstate["FooterText"] = value;
		}
	}

	/// <summary>Gets or sets the location of an image to display in the header section of the column.</summary>
	/// <returns>The location of an image to display in the header section of the column. The default value is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	[UrlProperty]
	public virtual string HeaderImageUrl
	{
		get
		{
			return viewstate.GetString("HeaderImageUrl", string.Empty);
		}
		set
		{
			viewstate["HeaderImageUrl"] = value;
		}
	}

	/// <summary>Gets the style properties for the header section of the column.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the header section of the column. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public virtual TableItemStyle HeaderStyle
	{
		get
		{
			if (header_style == null)
			{
				header_style = new TableItemStyle();
				if (tracking_viewstate)
				{
					header_style.TrackViewState();
				}
			}
			return header_style;
		}
	}

	/// <summary>Gets or sets the text displayed in the header section of the column.</summary>
	/// <returns>The text displayed in the header section of the column. The default value is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public virtual string HeaderText
	{
		get
		{
			return viewstate.GetString("HeaderText", string.Empty);
		}
		set
		{
			viewstate["HeaderText"] = value;
		}
	}

	/// <summary>Gets the style properties for the item cells of the column.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the item cells of the column. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public virtual TableItemStyle ItemStyle
	{
		get
		{
			if (item_style == null)
			{
				item_style = new TableItemStyle();
				if (tracking_viewstate)
				{
					item_style.TrackViewState();
				}
			}
			return item_style;
		}
	}

	/// <summary>Gets or sets the name of the field or expression to pass to the <see cref="M:System.Web.UI.WebControls.DataGrid.OnSortCommand(System.Web.UI.WebControls.DataGridSortCommandEventArgs)" /> method when a column is selected for sorting.</summary>
	/// <returns>The name of the field to pass to <see cref="M:System.Web.UI.WebControls.DataGrid.OnSortCommand(System.Web.UI.WebControls.DataGridSortCommandEventArgs)" /> when a column is selected for sorting. The default value is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public virtual string SortExpression
	{
		get
		{
			return viewstate.GetString("SortExpression", string.Empty);
		}
		set
		{
			viewstate["SortExpression"] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the column is visible in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>
	///     <see langword="true" /> if the column is visible in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public bool Visible
	{
		get
		{
			return viewstate.GetBool("Visible", def: true);
		}
		set
		{
			viewstate["Visible"] = value;
		}
	}

	/// <summary>Gets a value that indicates whether the column is in design mode.</summary>
	/// <returns>
	///     <see langword="true" /> if the column is in design mode; otherwise, <see langword="false" />.</returns>
	protected bool DesignMode => design;

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that the column is a member of.</summary>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that the column is a member of.</returns>
	protected DataGrid Owner => owner;

	/// <summary>Gets the <see cref="T:System.Web.UI.StateBag" /> object that allows a column derived from the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> class to store its properties.</summary>
	/// <returns>The <see cref="T:System.Web.UI.StateBag" /> for the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />.</returns>
	protected StateBag ViewState => viewstate;

	/// <summary>Gets a value that indicates whether the column is tracking view state changes.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> object is tracking its view-state changes; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => IsTrackingViewState;

	/// <summary>Gets a value that determines whether the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> object is marked to save its state.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> is marked; otherwise, <see langword="false" />.</returns>
	protected bool IsTrackingViewState => tracking_viewstate;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> class.</summary>
	protected DataGridColumn()
	{
		viewstate = new StateBag();
	}

	/// <summary>Provides the base implementation to reset a column derived from the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> class to its initial state.</summary>
	public virtual void Initialize()
	{
		if (owner != null && owner.Site != null)
		{
			design = owner.Site.DesignMode;
		}
	}

	/// <summary>Provides the base implementation to reset the specified cell from a column derived from the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> class to its initial state.</summary>
	/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> that represents the cell to reset. </param>
	/// <param name="columnIndex">The column number where the cell is located. </param>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
	public virtual void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
	{
		switch (itemType)
		{
		case ListItemType.Header:
		{
			bool flag = false;
			string sortExpression = SortExpression;
			if (owner != null && sortExpression.Length > 0)
			{
				flag = owner.AllowSorting;
			}
			string headerImageUrl = HeaderImageUrl;
			if (headerImageUrl.Length > 0)
			{
				if (flag)
				{
					ImageButton imageButton = new ImageButton();
					imageButton.ImageUrl = headerImageUrl;
					imageButton.CommandName = "Sort";
					imageButton.CommandArgument = sortExpression;
					cell.Controls.Add(imageButton);
				}
				else
				{
					Image image = new Image();
					image.ImageUrl = headerImageUrl;
					cell.Controls.Add(image);
				}
			}
			else if (flag)
			{
				LinkButton linkButton = new ForeColorLinkButton();
				linkButton.Text = HeaderText;
				linkButton.CommandName = "Sort";
				linkButton.CommandArgument = sortExpression;
				cell.Controls.Add(linkButton);
			}
			else
			{
				string headerText = HeaderText;
				if (headerText.Length > 0)
				{
					cell.Text = headerText;
				}
				else
				{
					cell.Text = "&nbsp;";
				}
			}
			break;
		}
		case ListItemType.Footer:
		{
			string footerText = FooterText;
			if (footerText.Length > 0)
			{
				cell.Text = footerText;
			}
			else
			{
				cell.Text = "&nbsp;";
			}
			break;
		}
		}
	}

	/// <summary>Returns the string representation of the column.</summary>
	/// <returns>Returns <see cref="F:System.String.Empty" />.</returns>
	public override string ToString()
	{
		return string.Empty;
	}

	internal TableItemStyle GetStyle(ListItemType type)
	{
		return type switch
		{
			ListItemType.Header => header_style, 
			ListItemType.Footer => footer_style, 
			_ => item_style, 
		};
	}

	internal void Set_Owner(DataGrid value)
	{
		owner = value;
	}

	/// <summary>Calls the <see cref="M:System.Web.UI.Design.WebControls.DataGridDesigner.OnColumnsChanged" /> method.</summary>
	protected virtual void OnColumnChanged()
	{
	}

	/// <summary>Loads previously saved state.</summary>
	/// <param name="state">An <see cref="T:System.Object" /> that represents the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> object to restore.</param>
	void IStateManager.LoadViewState(object savedState)
	{
		LoadViewState(savedState);
	}

	/// <summary>Returns an object containing state changes.</summary>
	/// <returns>The <see cref="T:System.Object" /> that contains the view state changes.</returns>
	object IStateManager.SaveViewState()
	{
		return SaveViewState();
	}

	/// <summary>Starts tracking state changes.</summary>
	void IStateManager.TrackViewState()
	{
		TrackViewState();
	}

	/// <summary>Loads the state of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> object.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />. </param>
	protected virtual void LoadViewState(object savedState)
	{
		if (savedState is object[] array)
		{
			if (array[0] != null)
			{
				viewstate.LoadViewState(array[0]);
			}
			if (array[1] != null)
			{
				FooterStyle.LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				HeaderStyle.LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				ItemStyle.LoadViewState(array[3]);
			}
		}
	}

	/// <summary>Saves the current state of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> object.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />.</returns>
	protected virtual object SaveViewState()
	{
		object[] array = new object[4]
		{
			viewstate.SaveViewState(),
			null,
			null,
			null
		};
		if (footer_style != null)
		{
			array[1] = footer_style.SaveViewState();
		}
		if (header_style != null)
		{
			array[2] = header_style.SaveViewState();
		}
		if (item_style != null)
		{
			array[3] = item_style.SaveViewState();
		}
		return array;
	}

	/// <summary>Causes tracking of view-state changes to the server control so they can be stored in the server control's <see cref="T:System.Web.UI.StateBag" /> object.</summary>
	protected virtual void TrackViewState()
	{
		tracking_viewstate = true;
		viewstate.TrackViewState();
		if (footer_style != null)
		{
			footer_style.TrackViewState();
		}
		if (header_style != null)
		{
			header_style.TrackViewState();
		}
		if (item_style != null)
		{
			item_style.TrackViewState();
		}
	}
}
