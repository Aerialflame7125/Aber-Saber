using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.Design;

/// <summary>Provides a type converter that can retrieve a list of the hierarchical data sources that are accessible to the current component.</summary>
[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
public class HierarchicalDataSourceConverter : DataSourceConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.HierarchicalDataSourceConverter" /> class.</summary>
	public HierarchicalDataSourceConverter()
	{
	}

	/// <summary>Indicates whether the specified component is a valid data source for this converter.</summary>
	/// <param name="component">The component to check as a valid data source.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="component" /> implements <see cref="T:System.Web.UI.IHierarchicalEnumerable" />; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	protected override bool IsValidDataSource(IComponent component)
	{
		throw new NotImplementedException();
	}
}
