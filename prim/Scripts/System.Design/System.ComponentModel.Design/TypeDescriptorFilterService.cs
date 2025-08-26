using System.Collections;

namespace System.ComponentModel.Design;

internal sealed class TypeDescriptorFilterService : ITypeDescriptorFilterService, IDisposable
{
	private IServiceProvider _serviceProvider;

	public TypeDescriptorFilterService(IServiceProvider serviceProvider)
	{
		if (serviceProvider == null)
		{
			throw new ArgumentNullException("serviceProvider");
		}
		_serviceProvider = serviceProvider;
	}

	public bool FilterAttributes(IComponent component, IDictionary attributes)
	{
		if (_serviceProvider == null)
		{
			throw new ObjectDisposedException("TypeDescriptorFilterService");
		}
		if (component == null)
		{
			throw new ArgumentNullException("component");
		}
		if (_serviceProvider.GetService(typeof(IDesignerHost)) is IDesignerHost designerHost)
		{
			IDesigner designer = designerHost.GetDesigner(component);
			if (designer is IDesignerFilter)
			{
				((IDesignerFilter)designer).PreFilterAttributes(attributes);
				((IDesignerFilter)designer).PostFilterAttributes(attributes);
			}
		}
		return true;
	}

	public bool FilterEvents(IComponent component, IDictionary events)
	{
		if (_serviceProvider == null)
		{
			throw new ObjectDisposedException("TypeDescriptorFilterService");
		}
		if (component == null)
		{
			throw new ArgumentNullException("component");
		}
		if (_serviceProvider.GetService(typeof(IDesignerHost)) is IDesignerHost designerHost)
		{
			IDesigner designer = designerHost.GetDesigner(component);
			if (designer is IDesignerFilter)
			{
				((IDesignerFilter)designer).PreFilterEvents(events);
				((IDesignerFilter)designer).PostFilterEvents(events);
			}
		}
		return true;
	}

	public bool FilterProperties(IComponent component, IDictionary properties)
	{
		if (_serviceProvider == null)
		{
			throw new ObjectDisposedException("TypeDescriptorFilterService");
		}
		if (component == null)
		{
			throw new ArgumentNullException("component");
		}
		if (_serviceProvider.GetService(typeof(IDesignerHost)) is IDesignerHost designerHost)
		{
			IDesigner designer = designerHost.GetDesigner(component);
			if (designer is IDesignerFilter)
			{
				((IDesignerFilter)designer).PreFilterProperties(properties);
				((IDesignerFilter)designer).PostFilterProperties(properties);
			}
		}
		return true;
	}

	public void Dispose()
	{
		_serviceProvider = null;
	}
}
