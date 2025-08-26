namespace System.ComponentModel.Design;

/// <summary>Manages a collection of <see cref="T:System.ComponentModel.Design.DesignSurface" /> objects.</summary>
public class DesignSurfaceManager : IServiceProvider, IDisposable
{
	private class MergedServiceProvider : IServiceProvider
	{
		private IServiceProvider _primaryProvider;

		private IServiceProvider _secondaryProvider;

		public MergedServiceProvider(IServiceProvider primary, IServiceProvider secondary)
		{
			if (primary == null)
			{
				throw new ArgumentNullException("primary");
			}
			if (secondary == null)
			{
				throw new ArgumentNullException("secondary");
			}
			_primaryProvider = primary;
			_secondaryProvider = secondary;
		}

		public object GetService(Type service)
		{
			object service2 = _primaryProvider.GetService(service);
			if (service2 == null)
			{
				service2 = _secondaryProvider.GetService(service);
			}
			return service2;
		}
	}

	private IServiceProvider _parentProvider;

	private ServiceContainer _serviceContainer;

	/// <summary>Gets or sets the active designer.</summary>
	/// <returns>The active designer.</returns>
	public virtual DesignSurface ActiveDesignSurface
	{
		get
		{
			if (GetService(typeof(IDesignerEventService)) is DesignerEventService { ActiveDesigner: { } activeDesigner })
			{
				return activeDesigner.GetService(typeof(DesignSurface)) as DesignSurface;
			}
			return null;
		}
		set
		{
			if (value == null)
			{
				return;
			}
			DesignSurface designSurface = null;
			DesignerEventService designerEventService = GetService(typeof(IDesignerEventService)) as DesignerEventService;
			if (designerEventService != null)
			{
				IDesignerHost activeDesigner = designerEventService.ActiveDesigner;
				if (activeDesigner != null)
				{
					designSurface = activeDesigner.GetService(typeof(DesignSurface)) as DesignSurface;
				}
			}
			ISelectionService selectionService = null;
			if (designSurface != value)
			{
				if (designSurface != null && designSurface.GetService(typeof(ISelectionService)) is ISelectionService selectionService2)
				{
					selectionService2.SelectionChanged -= OnSelectionChanged;
				}
				if (value.GetService(typeof(ISelectionService)) is ISelectionService selectionService3)
				{
					selectionService3.SelectionChanged += OnSelectionChanged;
				}
				designerEventService.ActiveDesigner = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (this.ActiveDesignSurfaceChanged != null)
				{
					this.ActiveDesignSurfaceChanged(this, new ActiveDesignSurfaceChangedEventArgs(designSurface, value));
				}
			}
		}
	}

	/// <summary>Gets a collection of design surfaces.</summary>
	/// <returns>A collection of design surfaces that are currently hosted by the design surface manager.</returns>
	public DesignSurfaceCollection DesignSurfaces
	{
		get
		{
			if (GetService(typeof(IDesignerEventService)) is DesignerEventService designerEventService)
			{
				return new DesignSurfaceCollection(designerEventService.Designers);
			}
			return new DesignSurfaceCollection(null);
		}
	}

	/// <summary>Gets the design surface manager's <see cref="P:System.ComponentModel.Design.DesignSurfaceManager.ServiceContainer" />.</summary>
	/// <returns>The design surface manager's <see cref="P:System.ComponentModel.Design.DesignSurfaceManager.ServiceContainer" />.</returns>
	protected ServiceContainer ServiceContainer
	{
		get
		{
			if (_serviceContainer == null)
			{
				_serviceContainer = new ServiceContainer(_parentProvider);
			}
			return _serviceContainer;
		}
	}

	/// <summary>Occurs when the global selection changes.</summary>
	public event EventHandler SelectionChanged;

	/// <summary>Occurs when a designer is disposed.</summary>
	public event DesignSurfaceEventHandler DesignSurfaceDisposed;

	/// <summary>Occurs when a designer is created.</summary>
	public event DesignSurfaceEventHandler DesignSurfaceCreated;

	/// <summary>Occurs when the currently active designer changes.</summary>
	public event ActiveDesignSurfaceChangedEventHandler ActiveDesignSurfaceChanged;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignSurfaceManager" /> class.</summary>
	public DesignSurfaceManager()
		: this(null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignSurfaceManager" /> class.</summary>
	/// <param name="parentProvider">A parent service provider. Service requests are forwarded to this provider if they cannot be resolved by the design surface manager.</param>
	public DesignSurfaceManager(IServiceProvider parentProvider)
	{
		_parentProvider = parentProvider;
		ServiceContainer.AddService(typeof(IDesignerEventService), new DesignerEventService());
	}

	/// <summary>Implementation that creates the design surface.</summary>
	/// <param name="parentProvider">A service provider to pass to the design surface. This is either an instance of <see cref="T:System.ComponentModel.Design.DesignSurfaceManager" /> or an object that implements <see cref="T:System.IServiceProvider" />, and represents a merge between the service provider of the <see cref="T:System.ComponentModel.Design.DesignSurfaceManager" /> class and an externally passed provider.</param>
	/// <returns>A new design surface instance.</returns>
	protected virtual DesignSurface CreateDesignSurfaceCore(IServiceProvider parentProvider)
	{
		DesignSurface designSurface = new DesignSurface(parentProvider);
		OnDesignSurfaceCreated(designSurface);
		return designSurface;
	}

	/// <summary>Creates an instance of a design surface.</summary>
	/// <returns>A new design surface instance.</returns>
	public DesignSurface CreateDesignSurface()
	{
		return CreateDesignSurfaceCore(this);
	}

	/// <summary>Creates an instance of a design surface.</summary>
	/// <param name="parentProvider">A parent service provider. A new merged service provider will be created that will first ask this provider for a service, and then delegate any failures to the design surface manager object. This merged provider will be passed into the <see cref="M:System.ComponentModel.Design.DesignSurfaceManager.CreateDesignSurfaceCore(System.IServiceProvider)" /> method.</param>
	/// <returns>A new design surface instance.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="parentProvider" /> is <see langword="null" />.</exception>
	public DesignSurface CreateDesignSurface(IServiceProvider parentProvider)
	{
		if (parentProvider == null)
		{
			throw new ArgumentNullException("parentProvider");
		}
		return CreateDesignSurfaceCore(new MergedServiceProvider(parentProvider, this));
	}

	private void OnSelectionChanged(object sender, EventArgs args)
	{
		if (this.SelectionChanged != null)
		{
			this.SelectionChanged(this, EventArgs.Empty);
		}
		if (GetService(typeof(IDesignerEventService)) is DesignerEventService designerEventService)
		{
			designerEventService.RaiseSelectionChanged();
		}
	}

	private void OnDesignSurfaceCreated(DesignSurface surface)
	{
		if (this.DesignSurfaceCreated != null)
		{
			this.DesignSurfaceCreated(this, new DesignSurfaceEventArgs(surface));
		}
		surface.Disposed += OnDesignSurfaceDisposed;
		if (GetService(typeof(IDesignerEventService)) is DesignerEventService designerEventService)
		{
			designerEventService.RaiseDesignerCreated(surface.GetService(typeof(IDesignerHost)) as IDesignerHost);
		}
	}

	private void OnDesignSurfaceDisposed(object sender, EventArgs args)
	{
		DesignSurface designSurface = (DesignSurface)sender;
		designSurface.Disposed -= OnDesignSurfaceDisposed;
		if (this.DesignSurfaceDisposed != null)
		{
			this.DesignSurfaceDisposed(this, new DesignSurfaceEventArgs(designSurface));
		}
		if (GetService(typeof(IDesignerEventService)) is DesignerEventService designerEventService)
		{
			designerEventService.RaiseDesignerDisposed(designSurface.GetService(typeof(IDesignerHost)) as IDesignerHost);
		}
	}

	/// <summary>Gets a service in the design surface manager's service container.</summary>
	/// <param name="serviceType">The service type to retrieve.</param>
	/// <returns>An object that implements, or is a derived class of, the given service type; otherwise, <see langword="null" /> if the service does not exist in the service container.</returns>
	public object GetService(Type serviceType)
	{
		if (_serviceContainer != null)
		{
			return _serviceContainer.GetService(serviceType);
		}
		return null;
	}

	/// <summary>Releases the resources used by the <see cref="T:System.ComponentModel.Design.DesignSurfaceManager" />.</summary>
	public void Dispose()
	{
		Dispose(disposing: true);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.DesignSurfaceManager" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected virtual void Dispose(bool disposing)
	{
		if (disposing && _serviceContainer != null)
		{
			_serviceContainer.Dispose();
			_serviceContainer = null;
		}
	}
}
