namespace System.Web.UI.Design;

/// <summary>Provides a base class for designers for data views based on hierarchical data. This class must be inherited.</summary>
public abstract class DesignerHierarchicalDataSourceView
{
	/// <summary>Gets the associated designer.</summary>
	/// <returns>The parent <see cref="T:System.Web.UI.Design.IHierarchicalDataSourceDesigner" />.</returns>
	[System.MonoTODO]
	public IHierarchicalDataSourceDesigner DataSourceDesigner
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the path to the block of data that is presented in the view.</summary>
	/// <returns>The path provided when creating the <see cref="T:System.Web.UI.Design.DesignerHierarchicalDataSourceView" />.</returns>
	[System.MonoTODO]
	public string Path
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a schema that describes the data source view for the associated control.</summary>
	/// <returns>This implementation always returns <see langword="null" />.</returns>
	[System.MonoTODO]
	public virtual IDataSourceSchema Schema
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initiates a new instance of the <see cref="T:System.Web.UI.Design.DesignerHierarchicalDataSourceView" /> class.</summary>
	/// <param name="owner">The <see cref="T:System.Web.UI.Design.IHierarchicalDataSourceDesigner" /> that is the designer for the associated control.</param>
	/// <param name="viewPath">A unique path to the block of data to use for the view.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="owner" /> is <see langword="null" />  
	/// -or-  
	/// <paramref name="viewPath" /> is <see langword="null" />.</exception>
	[System.MonoTODO]
	protected DesignerHierarchicalDataSourceView(IHierarchicalDataSourceDesigner owner, string viewPath)
	{
		throw new NotImplementedException();
	}

	/// <summary>Generates design-time data that matches the schema of the associated data source control and returns a value indicating whether the data is sample or real data.</summary>
	/// <param name="isSampleData">
	///   <see langword="true" /> to indicate the returned data is sample data; <see langword="false" /> to indicate the returned data is live data.</param>
	/// <returns>This implementation always returns <see langword="null" />.</returns>
	[System.MonoTODO]
	public virtual IHierarchicalEnumerable GetDesignTimeData(out bool isSampleData)
	{
		throw new NotImplementedException();
	}
}
