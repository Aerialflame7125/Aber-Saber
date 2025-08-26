using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.Design;

/// <summary>Provides a type converter for a property representing a Boolean field in a data source schema.</summary>
[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
public class DataSourceBooleanViewSchemaConverter : DataSourceViewSchemaConverter
{
	/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.DataSourceBooleanViewSchemaConverter" /> class.</summary>
	public DataSourceBooleanViewSchemaConverter()
	{
	}

	/// <summary>Returns a list of available Boolean values that can be assigned to the associated field.</summary>
	/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <returns>A collection of Boolean values.</returns>
	[System.MonoTODO]
	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		throw new NotImplementedException();
	}
}
