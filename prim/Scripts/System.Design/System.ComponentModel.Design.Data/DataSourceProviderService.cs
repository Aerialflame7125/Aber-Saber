using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace System.ComponentModel.Design.Data;

/// <summary>Implements the basic functionality required of a service for providing access to a data source at the <see langword="EnvDTE.Project" /> level.</summary>
[Guid("ABE5C1F0-C96E-40c4-A22D-4A5CEC899BDC")]
public abstract class DataSourceProviderService
{
	/// <summary>When overridden in a derived class, gets the value indicating whether the service supports adding a new data source using <see cref="M:System.ComponentModel.Design.Data.DataSourceProviderService.InvokeAddNewDataSource(System.Windows.Forms.IWin32Window,System.Windows.Forms.FormStartPosition)" />.</summary>
	/// <returns>
	///   <see langword="true" /> if the service supports adding a new data source using <see cref="M:System.ComponentModel.Design.Data.DataSourceProviderService.InvokeAddNewDataSource(System.Windows.Forms.IWin32Window,System.Windows.Forms.FormStartPosition)" />; otherwise, <see langword="false" />.</returns>
	public abstract bool SupportsAddNewDataSource { get; }

	/// <summary>When overridden in a derived class, gets the value indicating whether the service supports configuring data sources using <see cref="M:System.ComponentModel.Design.Data.DataSourceProviderService.InvokeConfigureDataSource(System.Windows.Forms.IWin32Window,System.Windows.Forms.FormStartPosition,System.ComponentModel.Design.Data.DataSourceDescriptor)" />.</summary>
	/// <returns>
	///   <see langword="true" /> if the service supports configuring a data source using <see cref="M:System.ComponentModel.Design.Data.DataSourceProviderService.InvokeConfigureDataSource(System.Windows.Forms.IWin32Window,System.Windows.Forms.FormStartPosition,System.ComponentModel.Design.Data.DataSourceDescriptor)" />; otherwise, <see langword="false" />.</returns>
	public abstract bool SupportsConfigureDataSource { get; }

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DataSourceProviderService" /> class.</summary>
	protected DataSourceProviderService()
	{
	}

	/// <summary>When overridden in a derived class, creates and returns an instance of the given data source, and adds it to the design surface.</summary>
	/// <param name="host">The designer host.</param>
	/// <param name="dataSourceDescriptor">The data source.</param>
	/// <returns>An <see cref="T:System.Object" /> representing an instance of the added data source.</returns>
	/// <exception cref="T:System.ArgumentException">The type name cannot be created or resolved.</exception>
	public abstract object AddDataSourceInstance(IDesignerHost host, DataSourceDescriptor dataSourceDescriptor);

	/// <summary>When overridden in a derived class, retrieves the collection of data sources at the <see langword="EnvDTE.Project" /> level.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.Data.DataSourceGroupCollection" />, or <see langword="null" /> if there are no data sources at the <see langword="EnvDTE.Project" /> level.</returns>
	public abstract DataSourceGroupCollection GetDataSources();

	/// <summary>When overridden in a derived class, invokes the Add New Data Source Wizard.</summary>
	/// <param name="parentWindow">The parent window.</param>
	/// <param name="startPosition">The initial position of a form.</param>
	/// <returns>A <see cref="T:System.ComponentModel.Design.Data.DataSourceGroup" /> collection of newly added data sources, or <see langword="null" /> if no data sources are added.</returns>
	public abstract DataSourceGroup InvokeAddNewDataSource(IWin32Window parentWindow, FormStartPosition startPosition);

	/// <summary>When overridden in a derived class, invokes the Configure Data Source dialog box on the specified data source.</summary>
	/// <param name="parentWindow">The parent window.</param>
	/// <param name="startPosition">The initial position of a form.</param>
	/// <param name="dataSourceDescriptor">The data source.</param>
	/// <returns>
	///   <see langword="true" /> if any changes were made to that data source; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentException">The specified data source is invalid or <see langword="null" />.</exception>
	public abstract bool InvokeConfigureDataSource(IWin32Window parentWindow, FormStartPosition startPosition, DataSourceDescriptor dataSourceDescriptor);

	/// <summary>When overridden in a derived class, notifies the service that a component representing a data source was added to the design surface.</summary>
	/// <param name="dsc">The data source component.</param>
	public abstract void NotifyDataSourceComponentAdded(object dsc);
}
