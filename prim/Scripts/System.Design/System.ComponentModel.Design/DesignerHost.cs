using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design;

internal sealed class DesignerHost : Container, IDesignerLoaderHost, IDesignerHost, IServiceContainer, IServiceProvider, IComponentChangeService
{
	private enum TransactionAction
	{
		Commit,
		Cancel
	}

	private sealed class DesignerHostTransaction : DesignerTransaction
	{
		private DesignerHost _designerHost;

		public DesignerHostTransaction(DesignerHost host, string description)
			: base(description)
		{
			_designerHost = host;
		}

		protected override void OnCancel()
		{
			_designerHost.OnTransactionClosing(this, TransactionAction.Cancel);
			_designerHost.OnTransactionClosed(this, TransactionAction.Cancel);
		}

		protected override void OnCommit()
		{
			_designerHost.OnTransactionClosing(this, TransactionAction.Commit);
			_designerHost.OnTransactionClosed(this, TransactionAction.Commit);
		}
	}

	private IServiceProvider _serviceProvider;

	private Hashtable _designers;

	private Stack _transactions;

	private IServiceContainer _serviceContainer;

	private bool _loading;

	private bool _unloading;

	private IComponent _rootComponent;

	public IContainer Container => this;

	public bool InTransaction
	{
		get
		{
			if (_transactions != null && _transactions.Count > 0)
			{
				return true;
			}
			return false;
		}
	}

	public bool Loading => _loading;

	public IComponent RootComponent => _rootComponent;

	public string RootComponentClassName
	{
		get
		{
			if (_rootComponent != null)
			{
				return _rootComponent.GetType().AssemblyQualifiedName;
			}
			return null;
		}
	}

	public string TransactionDescription
	{
		get
		{
			if (_transactions != null && _transactions.Count > 0)
			{
				return ((DesignerHostTransaction)_transactions.Peek()).Description;
			}
			return null;
		}
	}

	public event EventHandler Activated;

	public event EventHandler Deactivated;

	public event EventHandler LoadComplete;

	public event DesignerTransactionCloseEventHandler TransactionClosed;

	public event DesignerTransactionCloseEventHandler TransactionClosing;

	public event EventHandler TransactionOpened;

	public event EventHandler TransactionOpening;

	internal event LoadedEventHandler DesignerLoaderHostLoaded;

	internal event EventHandler DesignerLoaderHostLoading;

	internal event EventHandler DesignerLoaderHostUnloading;

	internal event EventHandler DesignerLoaderHostUnloaded;

	public event ComponentEventHandler ComponentAdded;

	public event ComponentEventHandler ComponentAdding;

	public event ComponentChangedEventHandler ComponentChanged;

	public event ComponentChangingEventHandler ComponentChanging;

	public event ComponentEventHandler ComponentRemoved;

	public event ComponentEventHandler ComponentRemoving;

	public event ComponentRenameEventHandler ComponentRename;

	public DesignerHost(IServiceProvider serviceProvider)
	{
		if (serviceProvider == null)
		{
			throw new ArgumentNullException("serviceProvider");
		}
		_serviceProvider = serviceProvider;
		_serviceContainer = serviceProvider.GetService(typeof(IServiceContainer)) as IServiceContainer;
		_designers = new Hashtable();
		_transactions = new Stack();
		_loading = true;
	}

	public override void Add(IComponent component, string name)
	{
		AddPreProcess(component, name);
		base.Add(component, name);
		AddPostProcess(component, name);
	}

	internal void AddPreProcess(IComponent component, string name)
	{
		if (this.ComponentAdding != null)
		{
			this.ComponentAdding(this, new ComponentEventArgs(component));
		}
	}

	internal void AddPostProcess(IComponent component, string name)
	{
		IDesigner designer;
		if (_rootComponent == null)
		{
			_rootComponent = component;
			designer = CreateDesigner(component, rootDesigner: true);
		}
		else
		{
			designer = CreateDesigner(component, rootDesigner: false);
		}
		if (designer != null)
		{
			_designers[component] = designer;
			designer.Initialize(component);
		}
		else
		{
			if (GetService(typeof(IUIService)) is IUIService iUIService)
			{
				iUIService.ShowError("Unable to load a designer for component type '" + component.GetType().Name + "'");
			}
			DestroyComponent(component);
		}
		if (component == _rootComponent)
		{
			Activate();
		}
		if (component is IExtenderProvider && GetService(typeof(IExtenderProviderService)) is IExtenderProviderService extenderProviderService)
		{
			extenderProviderService.AddExtenderProvider((IExtenderProvider)component);
		}
		if (this.ComponentAdded != null)
		{
			this.ComponentAdded(this, new ComponentEventArgs(component));
		}
	}

	public override void Remove(IComponent component)
	{
		DesignerTransaction designerTransaction = CreateTransaction("Remove " + component.Site.Name);
		RemovePreProcess(component);
		base.Remove(component);
		RemovePostProcess(component);
		designerTransaction.Commit();
	}

	internal void RemovePreProcess(IComponent component)
	{
		if (!_unloading && this.ComponentRemoving != null)
		{
			this.ComponentRemoving(this, new ComponentEventArgs(component));
		}
		if (_designers[component] is IDesigner designer)
		{
			designer.Dispose();
		}
		_designers.Remove(component);
		if (component == _rootComponent)
		{
			_rootComponent = null;
		}
		if (component is IExtenderProvider && GetService(typeof(IExtenderProviderService)) is IExtenderProviderService extenderProviderService)
		{
			extenderProviderService.RemoveExtenderProvider((IExtenderProvider)component);
		}
	}

	internal void RemovePostProcess(IComponent component)
	{
		if (!_unloading && this.ComponentRemoved != null)
		{
			this.ComponentRemoved(this, new ComponentEventArgs(component));
		}
	}

	protected override ISite CreateSite(IComponent component, string name)
	{
		if (name == null && GetService(typeof(INameCreationService)) is INameCreationService nameCreationService)
		{
			name = nameCreationService.CreateName(this, component.GetType());
		}
		return new DesignModeSite(component, name, this, this);
	}

	public void Activate()
	{
		if (GetService(typeof(ISelectionService)) is ISelectionService selectionService)
		{
			selectionService.SetSelectedComponents(new IComponent[1] { _rootComponent });
		}
		if (this.Activated != null)
		{
			this.Activated(this, EventArgs.Empty);
		}
	}

	public IComponent CreateComponent(Type componentClass)
	{
		return CreateComponent(componentClass, null);
	}

	public IComponent CreateComponent(Type componentClass, string name)
	{
		if (componentClass == null)
		{
			throw new ArgumentNullException("componentClass");
		}
		if (!typeof(IComponent).IsAssignableFrom(componentClass))
		{
			throw new ArgumentException("componentClass");
		}
		IComponent component = CreateInstance(componentClass) as IComponent;
		Add(component, name);
		return component;
	}

	internal object CreateInstance(Type type)
	{
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null);
	}

	internal IDesigner CreateDesigner(IComponent component, bool rootDesigner)
	{
		if (component == null)
		{
			throw new ArgumentNullException("component");
		}
		if (rootDesigner)
		{
			return CreateDesigner(component, typeof(IRootDesigner));
		}
		return CreateDesigner(component, typeof(IDesigner));
	}

	private IDesigner CreateDesigner(IComponent component, Type designerBaseType)
	{
		IDesigner designer = null;
		foreach (Attribute attribute in TypeDescriptor.GetAttributes(component))
		{
			if (attribute is DesignerAttribute designerAttribute && (designerBaseType.FullName == designerAttribute.DesignerBaseTypeName || designerBaseType.AssemblyQualifiedName == designerAttribute.DesignerBaseTypeName))
			{
				Type type = Type.GetType(designerAttribute.DesignerTypeName);
				if (type == null && designerBaseType == typeof(IRootDesigner))
				{
					type = typeof(DocumentDesigner);
				}
				if (type != null)
				{
					designer = (IDesigner)Activator.CreateInstance(type);
				}
				break;
			}
		}
		if (designer == null)
		{
			Type baseType = component.GetType().BaseType;
			do
			{
				foreach (Attribute attribute2 in TypeDescriptor.GetAttributes(baseType))
				{
					if (attribute2 is DesignerAttribute designerAttribute2 && (designerBaseType.FullName == designerAttribute2.DesignerBaseTypeName || designerBaseType.AssemblyQualifiedName == designerAttribute2.DesignerBaseTypeName))
					{
						Type type2 = Type.GetType(designerAttribute2.DesignerTypeName);
						if (type2 != null)
						{
							designer = (IDesigner)Activator.CreateInstance(type2);
						}
						break;
					}
				}
				baseType = baseType.BaseType;
			}
			while (designer == null && baseType != null);
		}
		return designer;
	}

	public void DestroyComponent(IComponent component)
	{
		if (component.Site != null && component.Site.Container == this)
		{
			Remove(component);
			component.Dispose();
		}
	}

	public IDesigner GetDesigner(IComponent component)
	{
		if (component == null)
		{
			throw new ArgumentNullException("component");
		}
		return _designers[component] as IDesigner;
	}

	public DesignerTransaction CreateTransaction()
	{
		return CreateTransaction(null);
	}

	public DesignerTransaction CreateTransaction(string description)
	{
		if (this.TransactionOpening != null)
		{
			this.TransactionOpening(this, EventArgs.Empty);
		}
		DesignerHostTransaction designerHostTransaction = new DesignerHostTransaction(this, description);
		_transactions.Push(designerHostTransaction);
		if (this.TransactionOpened != null)
		{
			this.TransactionOpened(this, EventArgs.Empty);
		}
		return designerHostTransaction;
	}

	public Type GetType(string typeName)
	{
		if (GetService(typeof(ITypeResolutionService)) is ITypeResolutionService typeResolutionService)
		{
			return typeResolutionService.GetType(typeName);
		}
		return Type.GetType(typeName);
	}

	protected override void Dispose(bool disposing)
	{
		Unload();
		base.Dispose(disposing);
	}

	private void OnTransactionClosing(DesignerHostTransaction raiser, TransactionAction action)
	{
		bool commit = false;
		bool lastTransaction = false;
		if (_transactions.Peek() != raiser)
		{
			throw new InvalidOperationException("Current transaction differs from the one a commit was requested for.");
		}
		if (_transactions.Count == 1)
		{
			lastTransaction = true;
		}
		if (action == TransactionAction.Commit)
		{
			commit = true;
		}
		if (this.TransactionClosing != null)
		{
			this.TransactionClosing(this, new DesignerTransactionCloseEventArgs(commit, lastTransaction));
		}
	}

	private void OnTransactionClosed(DesignerHostTransaction raiser, TransactionAction action)
	{
		bool commit = false;
		bool lastTransaction = false;
		if (_transactions.Peek() != raiser)
		{
			throw new InvalidOperationException("Current transaction differs from the one a commit was requested for.");
		}
		if (_transactions.Count == 1)
		{
			lastTransaction = true;
		}
		if (action == TransactionAction.Commit)
		{
			commit = true;
		}
		_transactions.Pop();
		if (this.TransactionClosed != null)
		{
			this.TransactionClosed(this, new DesignerTransactionCloseEventArgs(commit, lastTransaction));
		}
	}

	public void EndLoad(string rootClassName, bool successful, ICollection errorCollection)
	{
		if (this.DesignerLoaderHostLoaded != null)
		{
			this.DesignerLoaderHostLoaded(this, new LoadedEventArgs(successful, errorCollection));
		}
		if (this.LoadComplete != null)
		{
			this.LoadComplete(this, EventArgs.Empty);
		}
		_loading = false;
	}

	public void Reload()
	{
		_loading = true;
		Unload();
		if (this.DesignerLoaderHostLoading != null)
		{
			this.DesignerLoaderHostLoading(this, EventArgs.Empty);
		}
	}

	private void Unload()
	{
		_unloading = true;
		if (this.DesignerLoaderHostUnloading != null)
		{
			this.DesignerLoaderHostUnloading(this, EventArgs.Empty);
		}
		IComponent[] array = new IComponent[Components.Count];
		Components.CopyTo(array, 0);
		IComponent[] array2 = array;
		foreach (IComponent component in array2)
		{
			Remove(component);
		}
		_transactions.Clear();
		if (this.DesignerLoaderHostUnloaded != null)
		{
			this.DesignerLoaderHostUnloaded(this, EventArgs.Empty);
		}
		_unloading = false;
	}

	public void OnComponentChanged(object component, MemberDescriptor member, object oldValue, object newValue)
	{
		if (this.ComponentChanged != null)
		{
			this.ComponentChanged(this, new ComponentChangedEventArgs(component, member, oldValue, newValue));
		}
	}

	public void OnComponentChanging(object component, MemberDescriptor member)
	{
		if (this.ComponentChanging != null)
		{
			this.ComponentChanging(this, new ComponentChangingEventArgs(component, member));
		}
	}

	internal void OnComponentRename(object component, string oldName, string newName)
	{
		if (this.ComponentRename != null)
		{
			this.ComponentRename(this, new ComponentRenameEventArgs(component, oldName, newName));
		}
	}

	public void AddService(Type serviceType, object serviceInstance)
	{
		_serviceContainer.AddService(serviceType, serviceInstance);
	}

	public void AddService(Type serviceType, object serviceInstance, bool promote)
	{
		_serviceContainer.AddService(serviceType, serviceInstance, promote);
	}

	public void AddService(Type serviceType, ServiceCreatorCallback callback)
	{
		_serviceContainer.AddService(serviceType, callback);
	}

	public void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
	{
		_serviceContainer.AddService(serviceType, callback, promote);
	}

	public void RemoveService(Type serviceType)
	{
		_serviceContainer.RemoveService(serviceType);
	}

	public void RemoveService(Type serviceType, bool promote)
	{
		_serviceContainer.RemoveService(serviceType, promote);
	}

	public new object GetService(Type serviceType)
	{
		if (_serviceProvider != null)
		{
			return _serviceProvider.GetService(serviceType);
		}
		return null;
	}
}
