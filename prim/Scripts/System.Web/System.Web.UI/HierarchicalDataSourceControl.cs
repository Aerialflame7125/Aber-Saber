using System.ComponentModel;

namespace System.Web.UI;

/// <summary>Provides a base class for data source controls that represent hierarchical data.</summary>
[NonVisualControl]
[Designer("System.Web.UI.Design.HierarchicalDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ControlBuilder(typeof(DataSourceControlBuilder))]
[Bindable(false)]
public abstract class HierarchicalDataSourceControl : Control, IHierarchicalDataSource
{
	private static object dataSourceChanged = new object();

	/// <summary>Gets a value indicating whether this control supports themes.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt was made to set the value of the <see cref="P:System.Web.UI.HierarchicalDataSourceControl.EnableTheming" /> property. </exception>
	[Browsable(false)]
	[DefaultValue(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override bool EnableTheming
	{
		get
		{
			return false;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Gets or sets the skin to apply to the <see cref="T:System.Web.UI.HierarchicalDataSourceControl" /> control.</summary>
	/// <returns>
	///     <see cref="F:System.String.Empty" /> in all cases.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt was made to set the value of the <see cref="P:System.Web.UI.HierarchicalDataSourceControl.SkinID" /> property. </exception>
	[Browsable(false)]
	[DefaultValue("")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string SkinID
	{
		get
		{
			return string.Empty;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Gets or sets a value indicating whether the control is visually displayed.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt was made to set the value of the <see cref="P:System.Web.UI.HierarchicalDataSourceControl.Visible" /> property. </exception>
	[Browsable(false)]
	[DefaultValue(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override bool Visible
	{
		get
		{
			return false;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Web.UI.HierarchicalDataSourceControl" /> has changed in some way that affects data-bound controls.</summary>
	event EventHandler IHierarchicalDataSource.DataSourceChanged
	{
		add
		{
			base.Events.AddHandler(dataSourceChanged, value);
		}
		remove
		{
			base.Events.RemoveHandler(dataSourceChanged, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HierarchicalDataSourceControl" /> class. </summary>
	protected HierarchicalDataSourceControl()
	{
	}

	/// <summary>Gets the view helper object for the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> interface for the specified path.</summary>
	/// <param name="viewPath">The hierarchical path of the view to retrieve. </param>
	/// <returns>A <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> that represents a single view of the data at the hierarchical level identified by the <paramref name="viewPath" /> parameter.</returns>
	protected abstract HierarchicalDataSourceView GetHierarchicalView(string viewPath);

	/// <summary>Gets the view helper object for the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> interface for the specified path.</summary>
	/// <param name="viewPath">The hierarchical path of the view to retrieve. </param>
	/// <returns>Returns a <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> that represents a single view of the data at the hierarchical level identified by the <paramref name="viewPath" /> parameter.</returns>
	HierarchicalDataSourceView IHierarchicalDataSource.GetHierarchicalView(string viewPath)
	{
		return GetHierarchicalView(viewPath);
	}

	/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object to hold the child controls (both literal and server) of the server control.</summary>
	/// <returns>An <see cref="T:System.Web.UI.EmptyControlCollection" /> that prevents any child controls from being added.</returns>
	protected override ControlCollection CreateControlCollection()
	{
		return new EmptyControlCollection(this);
	}

	/// <summary>Searches the current naming container for a server control with the specified <paramref name="id" /> parameter.</summary>
	/// <param name="id">The identifier for the control to be found.</param>
	/// <returns>The specified control, or <see langword="null" /> if the specified control does not exist.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override Control FindControl(string id)
	{
		if (id == ID)
		{
			return this;
		}
		return null;
	}

	/// <summary>Determines if the server control contains any child controls.</summary>
	/// <returns>
	///     <see langword="true" /> if the control contains other controls; otherwise, <see langword="false" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override bool HasControls()
	{
		return false;
	}

	/// <summary>Sets input focus to the control.</summary>
	/// <exception cref="T:System.NotSupportedException">An attempt was made to call the <see cref="M:System.Web.UI.HierarchicalDataSourceControl.Focus" /> method.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override void Focus()
	{
		throw new NotSupportedException();
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.IHierarchicalDataSource.DataSourceChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
	protected virtual void OnDataSourceChanged(EventArgs e)
	{
		if (base.Events[dataSourceChanged] is EventHandler eventHandler)
		{
			eventHandler(this, e);
		}
	}

	/// <summary>Outputs server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object and stores tracing information about the control if tracing is enabled.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content.</param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override void RenderControl(HtmlTextWriter writer)
	{
	}
}
