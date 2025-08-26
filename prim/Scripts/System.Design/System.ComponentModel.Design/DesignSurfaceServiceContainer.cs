using System.Collections;

namespace System.ComponentModel.Design;

internal sealed class DesignSurfaceServiceContainer : ServiceContainer
{
	private Hashtable _nonRemoveableServices;

	public DesignSurfaceServiceContainer()
		: this(null)
	{
	}

	public DesignSurfaceServiceContainer(IServiceProvider parentProvider)
		: base(parentProvider)
	{
	}

	internal void AddNonReplaceableService(Type serviceType, object instance)
	{
		if (_nonRemoveableServices == null)
		{
			_nonRemoveableServices = new Hashtable();
		}
		_nonRemoveableServices[serviceType] = serviceType;
		AddService(serviceType, instance);
	}

	internal void RemoveNonReplaceableService(Type serviceType, object instance)
	{
		if (_nonRemoveableServices != null)
		{
			_nonRemoveableServices.Remove(serviceType);
		}
		RemoveService(serviceType);
	}

	public override void RemoveService(Type serviceType, bool promote)
	{
		if (serviceType != null && _nonRemoveableServices != null && _nonRemoveableServices.ContainsKey(serviceType))
		{
			throw new InvalidOperationException("Cannot remove non-replaceable service: " + serviceType.AssemblyQualifiedName);
		}
		base.RemoveService(serviceType, promote);
	}
}
