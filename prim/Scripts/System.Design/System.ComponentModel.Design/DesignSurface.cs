using System.Collections;
using System.ComponentModel.Design.Serialization;

namespace System.ComponentModel.Design;

/// <summary>Presents a user interface for designing components.</summary>
public class DesignSurface : IServiceProvider, IDisposable
{
	internal class DefaultDesignerLoader : DesignerLoader
	{
		private Type _componentType;

		private bool _loading;

		public override bool Loading => _loading;

		public DefaultDesignerLoader(Type componentType)
		{
			if (componentType == null)
			{
				throw new ArgumentNullException("componentType");
			}
			_componentType = componentType;
		}

		public override void BeginLoad(IDesignerLoaderHost loaderHost)
		{
			_loading = true;
			loaderHost.CreateComponent(_componentType);
			loaderHost.EndLoad(_componentType.FullName, successful: true, null);
			_loading = false;
		}

		public override void Dispose()
		{
			_componentType = null;
		}
	}

	private DesignerHost _designerHost;

	private DesignSurfaceServiceContainer _serviceContainer;

	private ICollection _loadErrors;

	private bool _isLoaded;

	private DesignerLoader _designerLoader;

	/// <summary>Gets the service container.</summary>
	/// <returns>The service container that provides all services to designers contained within the design surface.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" /> has been disposed.</exception>
	protected ServiceContainer ServiceContainer
	{
		get
		{
			if (_designerHost == null)
			{
				throw new ObjectDisposedException("DesignSurface");
			}
			return _serviceContainer;
		}
	}

	/// <summary>Gets the <see cref="T:System.ComponentModel.IContainer" /> implementation within the design surface.</summary>
	/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> implementation within the design surface.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" /> has been disposed.</exception>
	public IContainer ComponentContainer
	{
		get
		{
			if (_designerHost == null)
			{
				throw new ObjectDisposedException("DesignSurface");
			}
			return _designerHost.Container;
		}
	}

	/// <summary>Gets a value indicating whether the design surface is currently loaded.</summary>
	/// <returns>
	///   <see langword="true" /> if the design surface is currently loaded; otherwise, <see langword="false" />.</returns>
	public bool IsLoaded => _isLoaded;

	/// <summary>Returns a collection of loading errors or a void collection.</summary>
	/// <returns>A <see cref="T:System.Collections.ICollection" /> of loading errors.</returns>
	public ICollection LoadErrors
	{
		get
		{
			if (_loadErrors == null)
			{
				_loadErrors = new object[0];
			}
			return _loadErrors;
		}
	}

	/// <summary>Gets the view for the root designer.</summary>
	/// <returns>The view for the root designer.</returns>
	/// <exception cref="T:System.InvalidOperationException">The design surface is not loading, the designer loader has not yet created a root designer, or the design surface finished the load, but failed. More information may available in the <see cref="P:System.Exception.InnerException" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The designer loaded, but it does not offer a view compatible with this design surface.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" /> has been disposed.</exception>
	public object View
	{
		get
		{
			if (_designerHost == null)
			{
				throw new ObjectDisposedException("DesignSurface");
			}
			if (_designerHost.RootComponent == null || LoadErrors.Count > 0)
			{
				throw new InvalidOperationException("The DesignSurface isn't loaded.");
			}
			if (!(_designerHost.GetDesigner(_designerHost.RootComponent) is IRootDesigner { SupportedTechnologies: var supportedTechnologies } rootDesigner))
			{
				throw new InvalidOperationException("The DesignSurface isn't loaded.");
			}
			for (int i = 0; i < supportedTechnologies.Length; i++)
			{
				try
				{
					return rootDesigner.GetView(supportedTechnologies[i]);
				}
				catch
				{
				}
			}
			throw new NotSupportedException("No supported View Technology found.");
		}
	}

	/// <summary>Occurs when the design surface is disposed.</summary>
	public event EventHandler Disposed;

	/// <summary>Occurs when a call is made to the <see cref="M:System.ComponentModel.Design.DesignSurface.Flush" /> method of <see cref="T:System.ComponentModel.Design.DesignSurface" />.</summary>
	public event EventHandler Flushed;

	/// <summary>Occurs when the designer load has completed.</summary>
	public event LoadedEventHandler Loaded;

	/// <summary>Occurs when the designer is about to be loaded.</summary>
	public event EventHandler Loading;

	/// <summary>Occurs when a designer has finished unloading.</summary>
	public event EventHandler Unloaded;

	/// <summary>Occurs when a designer is about to unload.</summary>
	public event EventHandler Unloading;

	/// <summary>Occurs when the <see cref="M:System.ComponentModel.Design.IDesignerHost.Activate" /> method has been called on <see cref="T:System.ComponentModel.Design.IDesignerHost" />.</summary>
	public event EventHandler ViewActivated;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignSurface" /> class.</summary>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" /> has been disposed.</exception>
	public DesignSurface()
		: this((IServiceProvider)null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignSurface" /> class.</summary>
	/// <param name="rootComponentType">The type of root component to create.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="rootComponent" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" /> has been disposed.</exception>
	public DesignSurface(Type rootComponentType)
		: this(null, rootComponentType)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignSurface" /> class.</summary>
	/// <param name="parentProvider">The parent service provider, or <see langword="null" /> if there is no parent used to resolve services.</param>
	/// <param name="rootComponentType">The type of root component to create.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="rootComponent" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" /> has been disposed.</exception>
	public DesignSurface(IServiceProvider parentProvider, Type rootComponentType)
		: this(parentProvider)
	{
		if (rootComponentType == null)
		{
			throw new ArgumentNullException("rootComponentType");
		}
		BeginLoad(rootComponentType);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignSurface" /> class.</summary>
	/// <param name="parentProvider">The parent service provider, or <see langword="null" /> if there is no parent used to resolve services.</param>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" /> has been disposed.</exception>
	public DesignSurface(IServiceProvider parentProvider)
	{
		_serviceContainer = new DesignSurfaceServiceContainer(parentProvider);
		_serviceContainer.AddNonReplaceableService(typeof(IServiceContainer), _serviceContainer);
		_designerHost = new DesignerHost(_serviceContainer);
		_designerHost.DesignerLoaderHostLoaded += OnDesignerHost_Loaded;
		_designerHost.DesignerLoaderHostLoading += OnDesignerHost_Loading;
		_designerHost.DesignerLoaderHostUnloading += OnDesignerHost_Unloading;
		_designerHost.DesignerLoaderHostUnloaded += OnDesignerHost_Unloaded;
		_designerHost.Activated += OnDesignerHost_Activated;
		_serviceContainer.AddNonReplaceableService(typeof(IComponentChangeService), _designerHost);
		_serviceContainer.AddNonReplaceableService(typeof(IDesignerHost), _designerHost);
		_serviceContainer.AddNonReplaceableService(typeof(IContainer), _designerHost);
		_serviceContainer.AddService(typeof(ITypeDescriptorFilterService), new TypeDescriptorFilterService(_serviceContainer));
		ExtenderService serviceInstance = new ExtenderService();
		_serviceContainer.AddService(typeof(IExtenderProviderService), serviceInstance);
		_serviceContainer.AddService(typeof(IExtenderListService), serviceInstance);
		_serviceContainer.AddService(typeof(DesignSurface), this);
		SelectionService serviceInstance2 = new SelectionService(_serviceContainer);
		_serviceContainer.AddService(typeof(ISelectionService), serviceInstance2);
	}

	/// <summary>Begins the loading process.</summary>
	/// <param name="rootComponentType">The type of component to create in design mode.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="rootComponentType" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" /> has been disposed.</exception>
	public void BeginLoad(Type rootComponentType)
	{
		if (rootComponentType == null)
		{
			throw new ArgumentNullException("rootComponentType");
		}
		if (_designerHost == null)
		{
			throw new ObjectDisposedException("DesignSurface");
		}
		BeginLoad(new DefaultDesignerLoader(rootComponentType));
	}

	/// <summary>Begins the loading process with the given designer loader.</summary>
	/// <param name="loader">The designer loader to use for loading the designer.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="loader" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" /> has been disposed.</exception>
	public void BeginLoad(DesignerLoader loader)
	{
		if (loader == null)
		{
			throw new ArgumentNullException("loader");
		}
		if (_designerHost == null)
		{
			throw new ObjectDisposedException("DesignSurface");
		}
		if (!_isLoaded)
		{
			_loadErrors = null;
			_designerLoader = loader;
			OnLoading(EventArgs.Empty);
			_designerLoader.BeginLoad(_designerHost);
		}
	}

	/// <summary>Releases the resources used by the <see cref="T:System.ComponentModel.Design.DesignSurface" />.</summary>
	public void Dispose()
	{
		Dispose(disposing: true);
	}

	/// <summary>Releases the resources used by the <see cref="T:System.ComponentModel.Design.DesignSurface" />.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected virtual void Dispose(bool disposing)
	{
		if (_designerLoader != null)
		{
			_designerLoader.Dispose();
			_designerLoader = null;
		}
		if (_designerHost != null)
		{
			_designerHost.Dispose();
			_designerHost.DesignerLoaderHostLoaded -= OnDesignerHost_Loaded;
			_designerHost.DesignerLoaderHostLoading -= OnDesignerHost_Loading;
			_designerHost.DesignerLoaderHostUnloading -= OnDesignerHost_Unloading;
			_designerHost.DesignerLoaderHostUnloaded -= OnDesignerHost_Unloaded;
			_designerHost.Activated -= OnDesignerHost_Activated;
			_designerHost = null;
		}
		if (_serviceContainer != null)
		{
			_serviceContainer.Dispose();
			_serviceContainer = null;
		}
		if (this.Disposed != null)
		{
			this.Disposed(this, EventArgs.Empty);
		}
	}

	/// <summary>Serializes changes to the design surface.</summary>
	public void Flush()
	{
		if (_designerLoader != null)
		{
			_designerLoader.Flush();
		}
		if (this.Flushed != null)
		{
			this.Flushed(this, EventArgs.Empty);
		}
	}

	private void OnDesignerHost_Loaded(object sender, LoadedEventArgs e)
	{
		OnLoaded(e);
	}

	private void OnDesignerHost_Loading(object sender, EventArgs e)
	{
		OnLoading(EventArgs.Empty);
	}

	private void OnDesignerHost_Unloading(object sender, EventArgs e)
	{
		OnUnloading(EventArgs.Empty);
	}

	private void OnDesignerHost_Unloaded(object sender, EventArgs e)
	{
		OnUnloaded(EventArgs.Empty);
	}

	/// <summary>Raises the <see cref="E:System.ComponentModel.Design.DesignSurface.Loaded" /> event.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.Design.LoadedEventArgs" /> that contains the event data.</param>
	protected virtual void OnLoaded(LoadedEventArgs e)
	{
		_loadErrors = e.Errors;
		_isLoaded = e.HasSucceeded;
		if (this.Loaded != null)
		{
			this.Loaded(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.ComponentModel.Design.DesignSurface.Loading" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnLoading(EventArgs e)
	{
		if (this.Loading != null)
		{
			this.Loading(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.ComponentModel.Design.DesignSurface.Unloaded" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnUnloaded(EventArgs e)
	{
		if (this.Unloaded != null)
		{
			this.Unloaded(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.ComponentModel.Design.DesignSurface.Unloading" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnUnloading(EventArgs e)
	{
		if (this.Unloading != null)
		{
			this.Unloading(this, e);
		}
	}

	internal void OnDesignerHost_Activated(object sender, EventArgs args)
	{
		OnViewActivate(EventArgs.Empty);
	}

	/// <summary>Raises the <see cref="E:System.ComponentModel.Design.DesignSurface.ViewActivated" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnViewActivate(EventArgs e)
	{
		if (this.ViewActivated != null)
		{
			this.ViewActivated(this, e);
		}
	}

	/// <summary>Creates an instance of a component.</summary>
	/// <param name="componentType">The type of component to create.</param>
	/// <returns>The newly created component.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="componentType" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" /> has been disposed.</exception>
	[Obsolete("CreateComponent has been replaced by CreateInstance")]
	protected internal virtual IComponent CreateComponent(Type componentType)
	{
		return CreateInstance(componentType) as IComponent;
	}

	/// <summary>Creates an instance of the given type.</summary>
	/// <param name="type">The type to create.</param>
	/// <returns>The newly created object.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="type" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" /> has been disposed.</exception>
	protected internal virtual object CreateInstance(Type type)
	{
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		return _designerHost.CreateComponent(type);
	}

	/// <summary>Creates a designer when a component is added to the container.</summary>
	/// <param name="component">The component for which the designer should be created.</param>
	/// <param name="rootDesigner">
	///   <see langword="true" /> to create a root designer; <see langword="false" /> to create a normal designer.</param>
	/// <returns>An instance of the requested designer, or <see langword="null" /> if no matching designer could be found.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="component" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" /> has been disposed.</exception>
	protected internal virtual IDesigner CreateDesigner(IComponent component, bool rootDesigner)
	{
		if (component == null)
		{
			throw new ArgumentNullException("component");
		}
		if (_designerHost == null)
		{
			throw new ObjectDisposedException("DesignerSurface");
		}
		return _designerHost.CreateDesigner(component, rootDesigner);
	}

	/// <summary>Creates a container suitable for nesting controls or components.</summary>
	/// <param name="owningComponent">The component that manages the nested container.</param>
	/// <returns>The nested container.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="owningComponent" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" /> has been disposed.</exception>
	public INestedContainer CreateNestedContainer(IComponent owningComponent)
	{
		return CreateNestedContainer(owningComponent, null);
	}

	/// <summary>Creates a container suitable for nesting controls or components.</summary>
	/// <param name="owningComponent">The component that manages the nested container.</param>
	/// <param name="containerName">An additional name for the nested container.</param>
	/// <returns>The nested container.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="owningComponent" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" /> has been disposed.</exception>
	public INestedContainer CreateNestedContainer(IComponent owningComponent, string containerName)
	{
		if (_designerHost == null)
		{
			throw new ObjectDisposedException("DesignSurface");
		}
		return new DesignModeNestedContainer(owningComponent, containerName);
	}

	/// <summary>Gets a service from the service container.</summary>
	/// <param name="serviceType">The type of service to retrieve.</param>
	/// <returns>An object that implements, or is a derived class of, <paramref name="serviceType" />, or <see langword="null" /> if the service does not exist in the service container.</returns>
	public object GetService(Type serviceType)
	{
		if (typeof(IServiceContainer) == serviceType)
		{
			return _serviceContainer;
		}
		return _serviceContainer.GetService(serviceType);
	}
}
