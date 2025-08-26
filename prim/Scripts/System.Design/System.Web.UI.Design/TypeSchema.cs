using System.Security.Permissions;

namespace System.Web.UI.Design;

/// <summary>Represents the structure, or schema, of an object type.</summary>
[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
public sealed class TypeSchema : IDataSourceSchema
{
	/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.TypeSchema" /> class using the provided <see cref="T:System.Type" /> object.</summary>
	/// <param name="type">A <see cref="T:System.Type" /> that describes an object.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="type" /> is <see langword="null" />.</exception>
	[System.MonoTODO]
	public TypeSchema(Type type)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets an array of schema descriptors for views into a data source.</summary>
	/// <returns>An array of instances of <see cref="T:System.Web.UI.Design.IDataSourceViewSchema" />, or of instances of a class that implements the <see cref="T:System.Web.UI.Design.IDataSourceViewSchema" /> interface.</returns>
	[System.MonoTODO]
	public IDataSourceViewSchema[] GetViews()
	{
		throw new NotImplementedException();
	}
}
