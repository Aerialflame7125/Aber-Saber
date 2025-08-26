namespace System.ComponentModel.Design;

/// <summary>Establishes a design-time service that manages the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects for components.</summary>
public class DesignerActionService : IDisposable
{
	/// <summary>Occurs when a <see cref="T:System.ComponentModel.Design.DesignerActionList" /> is removed or added for any component.</summary>
	public event DesignerActionListsChangedEventHandler DesignerActionListsChanged;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionService" /> class.</summary>
	/// <param name="serviceProvider">The service provider for the current design-time environment.</param>
	[System.MonoTODO]
	public DesignerActionService(IServiceProvider serviceProvider)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds a <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to the current collection of managed smart tags.</summary>
	/// <param name="comp">The <see cref="T:System.ComponentModel.IComponent" /> to associate the smart tags with.</param>
	/// <param name="actionList">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> that contains the new smart tag items to be added.</param>
	/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are <see langword="null" />.</exception>
	[System.MonoTODO]
	public void Add(IComponent comp, DesignerActionList actionList)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds a <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> to the current collection of managed smart tags.</summary>
	/// <param name="comp">The <see cref="T:System.ComponentModel.IComponent" /> to associate the smart tags with.</param>
	/// <param name="designerActionListCollection">The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> that contains the new smart tag items to be added.</param>
	/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are <see langword="null" />.</exception>
	[System.MonoTODO]
	public void Add(IComponent comp, DesignerActionListCollection designerActionListCollection)
	{
		throw new NotImplementedException();
	}

	/// <summary>Releases all components from management and clears all push-model smart tag lists.</summary>
	[System.MonoTODO]
	public void Clear()
	{
		throw new NotImplementedException();
	}

	/// <summary>Determines whether the current smart tag service manages the action lists for the specified component.</summary>
	/// <param name="comp">The <see cref="T:System.ComponentModel.IComponent" /> to search for.</param>
	/// <returns>
	///   <see langword="true" /> if the component is managed by the current service; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="comp" /> is <see langword="null" />.</exception>
	[System.MonoTODO]
	public bool Contains(IComponent comp)
	{
		throw new NotImplementedException();
	}

	/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.DesignerActionService" /> class.</summary>
	public void Dispose()
	{
		Dispose(disposing: true);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.DesignerActionService" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	[System.MonoTODO]
	protected virtual void Dispose(bool disposing)
	{
	}

	/// <summary>Returns the collection of smart tag item lists associated with a component.</summary>
	/// <param name="component">The component that the smart tags are associated with.</param>
	/// <returns>The collection of smart tags for the specified component.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="comp" /> is <see langword="null" />.</exception>
	[System.MonoTODO]
	public DesignerActionListCollection GetComponentActions(IComponent component)
	{
		return GetComponentActions(component, ComponentActionsType.All);
	}

	/// <summary>Returns the collection of smart tag item lists of the specified type associated with a component.</summary>
	/// <param name="component">The component that the smart tags are associated with.</param>
	/// <param name="type">The <see cref="T:System.ComponentModel.Design.ComponentActionsType" /> to filter the associated smart tags with.</param>
	/// <returns>The collection of smart tags of the specified type for the specified component.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="comp" /> is <see langword="null" />.</exception>
	[System.MonoTODO]
	public virtual DesignerActionListCollection GetComponentActions(IComponent component, ComponentActionsType type)
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves the pull-model smart tags associated with a component.</summary>
	/// <param name="component">The component that the smart tags are associated with.</param>
	/// <param name="actionLists">The collection to add the associated smart tags to.</param>
	/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are <see langword="null" />.</exception>
	[System.MonoTODO]
	protected virtual void GetComponentDesignerActions(IComponent component, DesignerActionListCollection actionLists)
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves the push-model smart tags associated with a component.</summary>
	/// <param name="component">The component that the smart tags are associated with.</param>
	/// <param name="actionLists">The collection to add the associated smart tags to.</param>
	/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are <see langword="null" />.</exception>
	[System.MonoTODO]
	protected virtual void GetComponentServiceActions(IComponent component, DesignerActionListCollection actionLists)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes the specified smart tag list from all components managed by the current service.</summary>
	/// <param name="actionList">The list of smart tags to be removed.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="actionList" /> is <see langword="null" />.</exception>
	[System.MonoTODO]
	public void Remove(DesignerActionList actionList)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes all the smart tag lists associated with the specified component.</summary>
	/// <param name="comp">The component to disassociate the smart tags from.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="comp" /> is <see langword="null" />.</exception>
	[System.MonoTODO]
	public void Remove(IComponent comp)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes the specified smart tag list from the specified component.</summary>
	/// <param name="comp">The component to disassociate the smart tags from.</param>
	/// <param name="actionList">The smart tag list to remove.</param>
	/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are <see langword="null" />.</exception>
	[System.MonoTODO]
	public void Remove(IComponent comp, DesignerActionList actionList)
	{
		throw new NotImplementedException();
	}
}
