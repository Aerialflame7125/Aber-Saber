using System.Collections;

namespace System.Web.UI.Design;

/// <summary>Serves as the base class for design-time data source view classes.</summary>
public abstract class DesignerDataSourceView
{
	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports the <see cref="M:System.Web.UI.DataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> method.</summary>
	/// <returns>
	///   <see langword="true" /> if the <see cref="M:System.Web.UI.DataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> method is supported; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public virtual bool CanDelete
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports the <see cref="M:System.Web.UI.DataSourceView.ExecuteInsert(System.Collections.IDictionary)" /> method.</summary>
	/// <returns>
	///   <see langword="true" /> if the <see cref="M:System.Web.UI.DataSourceView.ExecuteInsert(System.Collections.IDictionary)" /> method is supported; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public virtual bool CanInsert
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports paging through the data that is retrieved by the <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method.</summary>
	/// <returns>
	///   <see langword="true" /> if paging through the data retrieved by the <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method is supported; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public virtual bool CanPage
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports retrieving the total number of data rows instead of the data itself.</summary>
	/// <returns>
	///   <see langword="true" /> if retrieving the total number of data rows is supported; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public virtual bool CanRetrieveTotalRowCount
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports a sorted view on the underlying data source.</summary>
	/// <returns>
	///   <see langword="true" /> if a sorted view on the underlying data source is supported; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public virtual bool CanSort
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports the <see cref="M:System.Web.UI.DataSourceView.ExecuteUpdate(System.Collections.IDictionary,System.Collections.IDictionary,System.Collections.IDictionary)" /> method.</summary>
	/// <returns>
	///   <see langword="true" /> if the <see cref="M:System.Web.UI.DataSourceView.ExecuteUpdate(System.Collections.IDictionary,System.Collections.IDictionary,System.Collections.IDictionary)" /> method is supported; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public virtual bool CanUpdate
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a reference to the designer that created this <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> control.</summary>
	/// <returns>The <see cref="T:System.Web.UI.Design.IDataSourceDesigner" /> object provided when the current <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> instance was created.</returns>
	[System.MonoNotSupported("")]
	public IDataSourceDesigner DataSourceDesigner
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the name of the view as provided when this instance of the <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> class was created.</summary>
	/// <returns>The view name.</returns>
	[System.MonoNotSupported("")]
	public string Name
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a schema that describes the data source view that is represented by this view object.</summary>
	/// <returns>An <see cref="T:System.Web.UI.Design.IDataSourceViewSchema" /> object.</returns>
	[System.MonoNotSupported("")]
	public virtual IDataSourceViewSchema Schema
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> class using the specified data source designer and view name.</summary>
	/// <param name="owner">The parent data source designer.</param>
	/// <param name="viewName">The name of the view in the data source.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="owner" /> is <see langword="null" />  
	/// -or-  
	/// <paramref name="viewName" /> is <see langword="null" />.</exception>
	[System.MonoNotSupported("")]
	protected DesignerDataSourceView(IDataSourceDesigner owner, string viewName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Generates design-time data that matches the schema of the associated data source control using the specified number of rows, indicating whether it is returning sample data or real data.</summary>
	/// <param name="minimumRows">The minimum number of rows to return.</param>
	/// <param name="isSampleData">
	///   <see langword="true" /> to indicate that the returned data is sample data; <see langword="false" /> to indicate that the returned data is live data.</param>
	/// <returns>A <see cref="T:System.Web.UI.DataSourceView" /> object containing data to display at design time.</returns>
	[System.MonoNotSupported("")]
	public virtual IEnumerable GetDesignTimeData(int minimumRows, out bool isSampleData)
	{
		throw new NotImplementedException();
	}
}
