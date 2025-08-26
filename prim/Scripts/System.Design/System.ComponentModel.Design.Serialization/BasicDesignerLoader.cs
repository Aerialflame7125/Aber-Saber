using System.Collections;
using System.Windows.Forms;

namespace System.ComponentModel.Design.Serialization;

/// <summary>Provides an implementation of the <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderService" /> interface.</summary>
public abstract class BasicDesignerLoader : DesignerLoader, IDesignerLoaderService
{
	/// <summary>Defines the behavior of the <see cref="M:System.ComponentModel.Design.Serialization.BasicDesignerLoader.Reload(System.ComponentModel.Design.Serialization.BasicDesignerLoader.ReloadOptions)" /> method. These flags can be combined using the bitwise <see langword="OR" /> operator.</summary>
	[Flags]
	protected enum ReloadOptions
	{
		/// <summary>The designer loader flushes changes before reloading, but it does not force a reload, and it also does not set the <see cref="P:System.ComponentModel.Design.Serialization.BasicDesignerLoader.Modified" /> property to <see langword="true" /> if load errors occur.</summary>
		Default = 0,
		/// <summary>The designer loader forces the reload to occur. Normally, a reload occurs only if the <see cref="M:System.ComponentModel.Design.Serialization.BasicDesignerLoader.IsReloadNeeded" /> method returns <see langword="true" />. This flag bypasses calling this method and always performs the reload.</summary>
		Force = 1,
		/// <summary>The designer loader will set the <see cref="P:System.ComponentModel.Design.Serialization.BasicDesignerLoader.Modified" /> property to <see langword="true" /> if load errors occur. This flag is useful if you want a flush of the loader to overwrite persistent state that had errors.</summary>
		ModifyOnError = 2,
		/// <summary>The designer loader abandons any changes before reloading.</summary>
		NoFlush = 3
	}

	private bool _loaded;

	private bool _loading;

	private IDesignerLoaderHost _host;

	private int _dependenciesCount;

	private bool _notificationsEnabled;

	private bool _modified;

	private string _baseComponentClassName;

	private DesignerSerializationManager _serializationMananger;

	private bool _flushing;

	private bool _reloadScheduled;

	private ReloadOptions _reloadOptions;

	/// <summary>Gets a value indicating whether the designer loader is loading the design surface.</summary>
	/// <returns>
	///   <see langword="true" /> if the designer loader is currently loading the design surface; otherwise, <see langword="false" />.</returns>
	public override bool Loading => _loading;

	/// <summary>Gets the loader host.</summary>
	/// <returns>The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> that was passed to the <see cref="M:System.ComponentModel.Design.Serialization.BasicDesignerLoader.BeginLoad(System.ComponentModel.Design.Serialization.IDesignerLoaderHost)" /> method.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has not been initialized.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has been disposed.</exception>
	protected IDesignerLoaderHost LoaderHost => _host;

	/// <summary>Gets or sets a value indicating whether the designer has been modified.</summary>
	/// <returns>
	///   <see langword="true" /> if the designer has been modified; otherwise, <see langword="false" />,</returns>
	protected virtual bool Modified
	{
		get
		{
			return _modified;
		}
		set
		{
			_modified = value;
		}
	}

	/// <summary>Gets or sets the property provider for the serialization manager being used by the loader.</summary>
	/// <returns>An object whose properties are to be provided to the serialization manager.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has not been initialized.</exception>
	protected object PropertyProvider
	{
		get
		{
			if (!_loaded)
			{
				throw new InvalidOperationException("host not initialized");
			}
			return _serializationMananger.PropertyProvider;
		}
		set
		{
			if (!_loaded)
			{
				throw new InvalidOperationException("host not initialized");
			}
			_serializationMananger.PropertyProvider = value;
		}
	}

	/// <summary>Gets a value indicating whether a reload has been queued.</summary>
	/// <returns>
	///   <see langword="true" />, if a call to <see cref="M:System.ComponentModel.Design.Serialization.BasicDesignerLoader.Reload(System.ComponentModel.Design.Serialization.BasicDesignerLoader.ReloadOptions)" /> has queued a reload request; otherwise, <see langword="false" />.</returns>
	protected bool ReloadPending => _reloadScheduled;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.BasicDesignerLoader" /> class.</summary>
	protected BasicDesignerLoader()
	{
		_loading = (_loaded = (_flushing = (_reloadScheduled = false)));
		_host = null;
		_notificationsEnabled = false;
		_modified = false;
		_dependenciesCount = 0;
	}

	/// <summary>Initializes services.</summary>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has not been initialized.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has been disposed.</exception>
	protected virtual void Initialize()
	{
		_serializationMananger = new DesignerSerializationManager(_host);
		if (_host.GetService(typeof(IServiceContainer)) is DesignSurfaceServiceContainer designSurfaceServiceContainer)
		{
			designSurfaceServiceContainer.AddService(typeof(IDesignerLoaderService), this);
			designSurfaceServiceContainer.AddNonReplaceableService(typeof(IDesignerSerializationManager), _serializationMananger);
		}
	}

	/// <summary>Starts the loading process.</summary>
	/// <param name="host">The designer loader host to load.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="host" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The designer is already loaded, or <see cref="M:System.ComponentModel.Design.Serialization.BasicDesignerLoader.BeginLoad(System.ComponentModel.Design.Serialization.IDesignerLoaderHost)" /> has been called with a different designer loader host.</exception>
	/// <exception cref="T:System.ObjectDisposedException">
	///   <paramref name="host" /> has been disposed.</exception>
	public override void BeginLoad(IDesignerLoaderHost host)
	{
		if (host == null)
		{
			throw new ArgumentNullException("host");
		}
		if (_loaded)
		{
			throw new InvalidOperationException("Already loaded.");
		}
		if (_host != null && _host != host)
		{
			throw new InvalidOperationException("Trying to load with a different host");
		}
		if (_host == null)
		{
			_host = host;
			Initialize();
		}
		IDisposable disposable = _serializationMananger.CreateSession();
		IDesignerLoaderService designerLoaderService = _host.GetService(typeof(IDesignerLoaderService)) as IDesignerLoaderService;
		if (designerLoaderService != null)
		{
			_dependenciesCount = -1;
			designerLoaderService.AddLoadDependency();
		}
		else
		{
			OnBeginLoad();
		}
		bool successful = true;
		try
		{
			PerformLoad(_serializationMananger);
		}
		catch (Exception value)
		{
			successful = false;
			_serializationMananger.Errors.Add(value);
		}
		if (designerLoaderService != null)
		{
			designerLoaderService.DependentLoadComplete(successful, _serializationMananger.Errors);
		}
		else
		{
			OnEndLoad(successful, _serializationMananger.Errors);
		}
		disposable.Dispose();
	}

	/// <summary>Loads a designer from persistence.</summary>
	/// <param name="serializationManager">An <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for loading state for the designers.</param>
	protected abstract void PerformLoad(IDesignerSerializationManager serializationManager);

	/// <summary>Notifies the designer loader that loading is about to begin.</summary>
	protected virtual void OnBeginLoad()
	{
		_loading = true;
	}

	/// <summary>Notifies the designer loader that loading is complete.</summary>
	/// <param name="successful">
	///   <see langword="true" /> if the load completed successfully; otherwise, <see langword="false" />.</param>
	/// <param name="errors">An <see cref="T:System.Collections.ICollection" /> containing objects (usually exceptions) that were reported as errors.</param>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has not been initialized.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has been disposed.</exception>
	protected virtual void OnEndLoad(bool successful, ICollection errors)
	{
		_host.EndLoad(_baseComponentClassName, successful, errors);
		if (successful)
		{
			_loaded = true;
			EnableComponentNotification(enable: true);
		}
		else if (_reloadScheduled && (_reloadOptions & ReloadOptions.ModifyOnError) == ReloadOptions.ModifyOnError)
		{
			OnModifying();
			Modified = true;
		}
		_loading = false;
	}

	/// <summary>Enables or disables component notification with the <see cref="T:System.ComponentModel.Design.Serialization.DesignerLoader" />.</summary>
	/// <param name="enable">
	///   <see langword="true" /> to enable component notification by the <see cref="T:System.ComponentModel.Design.Serialization.DesignerLoader" />; <see langword="false" /> to disable component notification by the <see cref="T:System.ComponentModel.Design.Serialization.DesignerLoader" />.</param>
	/// <returns>
	///   <see langword="true" /> if the component notification was enabled prior to this call; otherwise, <see langword="false" />.</returns>
	protected virtual bool EnableComponentNotification(bool enable)
	{
		if (!_loaded)
		{
			throw new InvalidOperationException("host not initialized");
		}
		if (_host.GetService(typeof(IComponentChangeService)) is IComponentChangeService componentChangeService && _notificationsEnabled != enable)
		{
			if (enable)
			{
				componentChangeService.ComponentAdding += OnComponentAdding;
				componentChangeService.ComponentAdded += OnComponentAdded;
				componentChangeService.ComponentRemoving += OnComponentRemoving;
				componentChangeService.ComponentRemoved += OnComponentRemoved;
				componentChangeService.ComponentChanging += OnComponentChanging;
				componentChangeService.ComponentChanged += OnComponentChanged;
				componentChangeService.ComponentRename += OnComponentRename;
			}
			else
			{
				componentChangeService.ComponentAdding -= OnComponentAdding;
				componentChangeService.ComponentAdded -= OnComponentAdded;
				componentChangeService.ComponentRemoving -= OnComponentRemoving;
				componentChangeService.ComponentRemoved -= OnComponentRemoved;
				componentChangeService.ComponentChanging -= OnComponentChanging;
				componentChangeService.ComponentChanged -= OnComponentChanged;
				componentChangeService.ComponentRename -= OnComponentRename;
			}
		}
		if (!_notificationsEnabled)
		{
			return false;
		}
		return true;
	}

	private void OnComponentAdded(object sender, ComponentEventArgs args)
	{
		if (!_loading && _loaded)
		{
			Modified = true;
		}
	}

	private void OnComponentRemoved(object sender, ComponentEventArgs args)
	{
		if (!_loading && _loaded)
		{
			Modified = true;
		}
	}

	private void OnComponentAdding(object sender, ComponentEventArgs args)
	{
		if (!_loading && _loaded)
		{
			OnModifying();
		}
	}

	private void OnComponentRemoving(object sender, ComponentEventArgs args)
	{
		if (!_loading && _loaded)
		{
			OnModifying();
		}
	}

	private void OnComponentChanged(object sender, ComponentChangedEventArgs args)
	{
		if (!_loading && _loaded)
		{
			Modified = true;
		}
	}

	private void OnComponentChanging(object sender, ComponentChangingEventArgs args)
	{
		if (!_loading && _loaded)
		{
			OnModifying();
		}
	}

	private void OnComponentRename(object sender, ComponentRenameEventArgs args)
	{
		if (!_loading && _loaded)
		{
			OnModifying();
			Modified = true;
		}
	}

	/// <summary>Flushes pending changes to the designer loader.</summary>
	public override void Flush()
	{
		if (!_loaded)
		{
			throw new InvalidOperationException("host not initialized");
		}
		if (_flushing || !Modified)
		{
			return;
		}
		_flushing = true;
		using (_serializationMananger.CreateSession())
		{
			try
			{
				PerformFlush(_serializationMananger);
			}
			catch (Exception value)
			{
				_serializationMananger.Errors.Add(value);
				ReportFlushErrors(_serializationMananger.Errors);
			}
		}
		_flushing = false;
	}

	/// <summary>Flushes all changes to the designer.</summary>
	/// <param name="serializationManager">An <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for persisting the state of loaded designers.</param>
	protected abstract void PerformFlush(IDesignerSerializationManager serializationManager);

	/// <summary>Indicates whether the designer should be reloaded.</summary>
	/// <returns>
	///   <see langword="true" /> if the designer should be reloaded; otherwise, <see langword="false" />. The default implementation always returns <see langword="true" />.</returns>
	protected virtual bool IsReloadNeeded()
	{
		return true;
	}

	/// <summary>Notifies the designer loader that unloading is about to begin.</summary>
	protected virtual void OnBeginUnload()
	{
	}

	/// <summary>Notifies the designer loader that the state of the document is about to be modified.</summary>
	protected virtual void OnModifying()
	{
	}

	/// <summary>Queues a reload of the designer.</summary>
	/// <param name="flags">A bitwise combination of the <see cref="T:System.ComponentModel.Design.Serialization.BasicDesignerLoader.ReloadOptions" /> values.</param>
	protected void Reload(ReloadOptions flags)
	{
		if (!_reloadScheduled)
		{
			_reloadScheduled = true;
			_reloadOptions = flags;
			if ((flags & ReloadOptions.Force) == ReloadOptions.Force)
			{
				ReloadCore();
			}
			else
			{
				Application.Idle += OnIdle;
			}
		}
	}

	private void OnIdle(object sender, EventArgs args)
	{
		Application.Idle -= OnIdle;
		ReloadCore();
	}

	private void ReloadCore()
	{
		if ((_reloadOptions & ReloadOptions.NoFlush) != ReloadOptions.NoFlush)
		{
			Flush();
		}
		Unload();
		_host.Reload();
		BeginLoad(_host);
		_reloadScheduled = false;
	}

	private void Unload()
	{
		if (_loaded)
		{
			OnBeginUnload();
			EnableComponentNotification(enable: false);
			_loaded = false;
			_baseComponentClassName = null;
		}
	}

	/// <summary>Reports errors that occurred while flushing changes.</summary>
	/// <param name="errors">An <see cref="T:System.Collections.ICollection" /> containing error objects, usually exceptions.</param>
	/// <exception cref="T:System.InvalidOperationException">One or more errors occurred while flushing changes.</exception>
	protected virtual void ReportFlushErrors(ICollection errors)
	{
		object obj = null;
		foreach (object error in errors)
		{
			obj = error;
		}
		throw (Exception)obj;
	}

	/// <summary>Sets the full class name of the base component.</summary>
	/// <param name="name">A string representing the full name of the component to be designed.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="name" /> is <see langword="null" />.</exception>
	protected void SetBaseComponentClassName(string name)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		_baseComponentClassName = name;
	}

	/// <summary>Registers an external component as part of the load process managed by <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderService" />.</summary>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has not been initialized.</exception>
	void IDesignerLoaderService.AddLoadDependency()
	{
		_dependenciesCount++;
		if (_dependenciesCount == 0)
		{
			_dependenciesCount = 1;
			OnBeginLoad();
		}
	}

	/// <summary>Signals that a dependent load has finished.</summary>
	/// <param name="successful">
	///   <see langword="true" /> to load successfully; otherwise, <see langword="false" />.</param>
	/// <param name="errorCollection">An <see cref="T:System.Collections.ICollection" /> containing errors that occurred during the load.</param>
	/// <exception cref="T:System.InvalidOperationException">No load dependencies have been added by <see cref="M:System.ComponentModel.Design.Serialization.BasicDesignerLoader.System#ComponentModel#Design#Serialization#IDesignerLoaderService#AddLoadDependency" />, or the <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has not been initialized.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has been disposed.</exception>
	void IDesignerLoaderService.DependentLoadComplete(bool successful, ICollection errorCollection)
	{
		if (_dependenciesCount == 0)
		{
			throw new InvalidOperationException("dependencies == 0");
		}
		_dependenciesCount--;
		if (_dependenciesCount == 0)
		{
			OnEndLoad(successful, errorCollection);
		}
	}

	/// <summary>Reloads the design document.</summary>
	/// <returns>
	///   <see langword="true" /> if the reload request is accepted; <see langword="false" /> if the loader does not allow the reload.</returns>
	bool IDesignerLoaderService.Reload()
	{
		if (_dependenciesCount == 0)
		{
			Reload(ReloadOptions.Force);
			return true;
		}
		return false;
	}

	/// <summary>Gets the requested service.</summary>
	/// <param name="serviceType">The <see cref="T:System.Type" /> of the service.</param>
	/// <returns>The requested service, or <see langword="null" /> if the requested service cannot be found.</returns>
	protected object GetService(Type serviceType)
	{
		if (_host != null)
		{
			return _host.GetService(serviceType);
		}
		return null;
	}

	/// <summary>Releases the resources used by the <see cref="T:System.ComponentModel.Design.Serialization.BasicDesignerLoader" />.</summary>
	public override void Dispose()
	{
		LoaderHost.RemoveService(typeof(IDesignerLoaderService));
		Unload();
	}
}
