using System.Collections.Generic;

namespace System.ComponentModel.Design;

internal class ReferenceService : IReferenceService, IDisposable
{
	private List<IComponent> _references;

	internal ReferenceService(IServiceProvider provider)
	{
		if (provider == null)
		{
			throw new ArgumentNullException("provider");
		}
		_references = new List<IComponent>();
		if (provider.GetService(typeof(IComponentChangeService)) is IComponentChangeService componentChangeService)
		{
			componentChangeService.ComponentAdded += OnComponentAdded;
			componentChangeService.ComponentRemoved += OnComponentRemoved;
		}
	}

	private void OnComponentAdded(object sender, ComponentEventArgs args)
	{
		_references.Add(args.Component);
	}

	private void OnComponentRemoved(object sender, ComponentEventArgs args)
	{
		_references.Remove(args.Component);
	}

	public IComponent GetComponent(object reference)
	{
		return reference as IComponent;
	}

	public string GetName(object reference)
	{
		if (reference is IComponent { Site: not null } component)
		{
			return component.Site.Name;
		}
		return null;
	}

	public object GetReference(string name)
	{
		foreach (IComponent reference in _references)
		{
			if (reference.Site != null && reference.Site.Name == name)
			{
				return reference;
			}
		}
		return null;
	}

	public object[] GetReferences()
	{
		IComponent[] array = new IComponent[_references.Count];
		_references.CopyTo(array);
		return array;
	}

	public object[] GetReferences(Type baseType)
	{
		List<IComponent> list = new List<IComponent>();
		foreach (IComponent reference in _references)
		{
			if (baseType.IsAssignableFrom(reference.GetType()))
			{
				list.Add(reference);
			}
		}
		IComponent[] array = new IComponent[list.Count];
		list.CopyTo(array);
		return array;
	}

	public void Dispose()
	{
		_references.Clear();
	}
}
