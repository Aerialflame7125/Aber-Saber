using System.Reflection;

namespace System.ComponentModel.Design;

/// <summary>Provides a set of methods for identifying inherited components.</summary>
public class InheritanceService : IInheritanceService, IDisposable
{
	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.InheritanceService" /> class.</summary>
	[System.MonoTODO]
	public InheritanceService()
	{
	}

	/// <summary>Adds the components inherited by the specified component to the <see cref="T:System.ComponentModel.Design.InheritanceService" />.</summary>
	/// <param name="component">The component to search for inherited components to add to the specified container.</param>
	/// <param name="container">The container to add the inherited components to.</param>
	[System.MonoTODO]
	public void AddInheritedComponents(IComponent component, IContainer container)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds the components of the specified type that are inherited by the specified component to the <see cref="T:System.ComponentModel.Design.InheritanceService" />.</summary>
	/// <param name="type">The base type to search for.</param>
	/// <param name="component">The component to search for inherited components to add to the specified container.</param>
	/// <param name="container">The container to add the inherited components to.</param>
	[System.MonoTODO]
	protected virtual void AddInheritedComponents(Type type, IComponent component, IContainer container)
	{
		throw new NotImplementedException();
	}

	/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.InheritanceService" />.</summary>
	[System.MonoTODO]
	public void Dispose()
	{
		throw new NotImplementedException();
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.InheritanceService" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	[System.MonoTODO]
	protected virtual void Dispose(bool disposing)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the inheritance attribute of the specified component.</summary>
	/// <param name="component">The component to retrieve the inheritance attribute for.</param>
	/// <returns>An <see cref="T:System.ComponentModel.InheritanceAttribute" /> that describes the level of inheritance that this component comes from.</returns>
	[System.MonoTODO]
	public InheritanceAttribute GetInheritanceAttribute(IComponent component)
	{
		throw new NotImplementedException();
	}

	/// <summary>Indicates whether to ignore the specified member.</summary>
	/// <param name="member">The member to check. This member is either a <see cref="T:System.Reflection.FieldInfo" /> or a <see cref="T:System.Reflection.MethodInfo" />.</param>
	/// <param name="component">The component instance this member is bound to.</param>
	/// <returns>
	///   <see langword="true" /> if the specified member should be included in the set of inherited components; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	protected virtual bool IgnoreInheritedMember(MemberInfo member, IComponent component)
	{
		throw new NotImplementedException();
	}
}
