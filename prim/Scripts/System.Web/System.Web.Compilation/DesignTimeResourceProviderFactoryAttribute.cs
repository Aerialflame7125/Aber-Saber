namespace System.Web.Compilation;

/// <summary>Specifies the type of resource provider factory for design time. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class DesignTimeResourceProviderFactoryAttribute : Attribute
{
	private string _factoryTypeName;

	/// <summary>Gets the value of the factory type name.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the name of the factory type.</returns>
	public string FactoryTypeName => _factoryTypeName;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.DesignTimeResourceProviderFactoryAttribute" /> class with the attribute set to the qualified name of the specified factory type. </summary>
	/// <param name="factoryType">The type of the resource provider factory.</param>
	public DesignTimeResourceProviderFactoryAttribute(Type factoryType)
	{
		_factoryTypeName = factoryType.AssemblyQualifiedName;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.DesignTimeResourceProviderFactoryAttribute" /> class with the attribute set to the specified factory type name. </summary>
	/// <param name="factoryTypeName">The name of the resource provider factory type.</param>
	public DesignTimeResourceProviderFactoryAttribute(string factoryTypeName)
	{
		_factoryTypeName = factoryTypeName;
	}

	/// <summary>Determines whether the default provider is used.</summary>
	/// <returns>
	///     <see langword="true" /> if <see cref="P:System.Web.Compilation.DesignTimeResourceProviderFactoryAttribute.FactoryTypeName" /> equals <see langword="null" />; otherwise, <see langword="false" />.</returns>
	public override bool IsDefaultAttribute()
	{
		return _factoryTypeName == null;
	}
}
