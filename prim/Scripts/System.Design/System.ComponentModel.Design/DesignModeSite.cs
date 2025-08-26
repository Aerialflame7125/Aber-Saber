using System.Collections;
using System.ComponentModel.Design.Serialization;

namespace System.ComponentModel.Design;

internal class DesignModeSite : ISite, IServiceProvider, IDictionaryService, IServiceContainer
{
	private IServiceProvider _serviceProvider;

	private IComponent _component;

	private IContainer _container;

	private string _componentName;

	private NestedContainer _nestedContainer;

	private ServiceContainer _siteSpecificServices;

	private Hashtable _dictionary;

	public IComponent Component => _component;

	public IContainer Container => _container;

	public bool DesignMode => true;

	public string Name
	{
		get
		{
			return _componentName;
		}
		set
		{
			if (value != _componentName && value != null && value.Trim().Length > 0)
			{
				INameCreationService nameCreationService = GetService(typeof(INameCreationService)) as INameCreationService;
				if (_container.Components[value] == null && (nameCreationService == null || (nameCreationService != null && nameCreationService.IsValidName(value))))
				{
					string componentName = _componentName;
					_componentName = value;
					((DesignerHost)GetService(typeof(IDesignerHost))).OnComponentRename(_component, componentName, _componentName);
				}
			}
		}
	}

	private ServiceContainer SiteSpecificServices
	{
		get
		{
			if (_siteSpecificServices == null)
			{
				_siteSpecificServices = new ServiceContainer(null);
			}
			return _siteSpecificServices;
		}
	}

	public DesignModeSite(IComponent component, string name, IContainer container, IServiceProvider serviceProvider)
	{
		_component = component;
		_container = container;
		_componentName = name;
		_serviceProvider = serviceProvider;
	}

	void IServiceContainer.AddService(Type serviceType, object serviceInstance)
	{
		SiteSpecificServices.AddService(serviceType, serviceInstance);
	}

	void IServiceContainer.AddService(Type serviceType, object serviceInstance, bool promote)
	{
		SiteSpecificServices.AddService(serviceType, serviceInstance, promote);
	}

	void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback)
	{
		SiteSpecificServices.AddService(serviceType, callback);
	}

	void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
	{
		SiteSpecificServices.AddService(serviceType, callback, promote);
	}

	void IServiceContainer.RemoveService(Type serviceType)
	{
		SiteSpecificServices.RemoveService(serviceType);
	}

	void IServiceContainer.RemoveService(Type serviceType, bool promote)
	{
		SiteSpecificServices.RemoveService(serviceType, promote);
	}

	object IDictionaryService.GetKey(object value)
	{
		if (_dictionary != null)
		{
			foreach (DictionaryEntry item in _dictionary)
			{
				if (value != null && value.Equals(item.Value))
				{
					return item.Key;
				}
			}
		}
		return null;
	}

	object IDictionaryService.GetValue(object key)
	{
		if (_dictionary != null)
		{
			return _dictionary[key];
		}
		return null;
	}

	void IDictionaryService.SetValue(object key, object value)
	{
		if (_dictionary == null)
		{
			_dictionary = new Hashtable();
		}
		if (value == null)
		{
			_dictionary.Remove(key);
		}
		_dictionary[key] = value;
	}

	public virtual object GetService(Type service)
	{
		object obj = null;
		if (typeof(IDictionaryService) == service)
		{
			obj = this;
		}
		if (typeof(INestedContainer) == service)
		{
			if (_nestedContainer == null)
			{
				_nestedContainer = new DesignModeNestedContainer(_component, null);
			}
			obj = _nestedContainer;
		}
		if (obj == null && service != typeof(IServiceContainer) && _siteSpecificServices != null)
		{
			obj = _siteSpecificServices.GetService(service);
		}
		if (obj == null)
		{
			obj = _serviceProvider.GetService(service);
		}
		return obj;
	}
}
