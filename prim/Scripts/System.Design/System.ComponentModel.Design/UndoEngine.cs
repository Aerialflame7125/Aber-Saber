using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;

namespace System.ComponentModel.Design;

/// <summary>Specifies generic undo/redo functionality at design time.</summary>
public abstract class UndoEngine : IDisposable
{
	/// <summary>Encapsulates a unit of work that a user can undo.</summary>
	protected class UndoUnit
	{
		private class Action
		{
			public virtual void Undo(UndoEngine engine)
			{
			}
		}

		private class ComponentRenameAction : Action
		{
			private string _oldName;

			private string _currentName;

			public ComponentRenameAction(string currentName, string oldName)
			{
				_currentName = currentName;
				_oldName = oldName;
			}

			public override void Undo(UndoEngine engine)
			{
				(engine.GetRequiredService(typeof(IDesignerHost)) as IDesignerHost).Container.Components[_currentName].Site.Name = _oldName;
				string currentName = _currentName;
				_currentName = _oldName;
				_oldName = currentName;
			}
		}

		private class ComponentAddRemoveAction : Action
		{
			private string _componentName;

			private SerializationStore _serializedComponent;

			private bool _added;

			public ComponentAddRemoveAction(UndoEngine engine, IComponent component, bool added)
			{
				if (component == null)
				{
					throw new ArgumentNullException("component");
				}
				ComponentSerializationService componentSerializationService = engine.GetRequiredService(typeof(ComponentSerializationService)) as ComponentSerializationService;
				_serializedComponent = componentSerializationService.CreateStore();
				componentSerializationService.Serialize(_serializedComponent, component);
				_serializedComponent.Close();
				_added = added;
				_componentName = component.Site.Name;
			}

			public override void Undo(UndoEngine engine)
			{
				IDesignerHost designerHost = engine.GetRequiredService(typeof(IDesignerHost)) as IDesignerHost;
				if (_added)
				{
					IComponent component = designerHost.Container.Components[_componentName];
					if (component != null)
					{
						designerHost.DestroyComponent(component);
					}
					_added = false;
				}
				else
				{
					(engine.GetRequiredService(typeof(ComponentSerializationService)) as ComponentSerializationService).DeserializeTo(_serializedComponent, designerHost.Container);
					_added = true;
				}
			}
		}

		private class ComponentChangeAction : Action
		{
			private string _componentName;

			private MemberDescriptor _member;

			private IComponent _component;

			private SerializationStore _afterChange;

			private SerializationStore _beforeChange;

			public bool IsComplete
			{
				get
				{
					if (_beforeChange != null)
					{
						return _afterChange != null;
					}
					return false;
				}
			}

			public string ComponentName => _componentName;

			public IComponent Component => _component;

			public MemberDescriptor Member => _member;

			public void SetOriginalState(UndoEngine engine, IComponent component, MemberDescriptor member)
			{
				_member = member;
				_component = component;
				_componentName = ((component.Site != null) ? component.Site.Name : null);
				ComponentSerializationService componentSerializationService = engine.GetRequiredService(typeof(ComponentSerializationService)) as ComponentSerializationService;
				_beforeChange = componentSerializationService.CreateStore();
				componentSerializationService.SerializeMemberAbsolute(_beforeChange, component, member);
				_beforeChange.Close();
			}

			public void SetModifiedState(UndoEngine engine, IComponent component, MemberDescriptor member)
			{
				ComponentSerializationService componentSerializationService = engine.GetRequiredService(typeof(ComponentSerializationService)) as ComponentSerializationService;
				_afterChange = componentSerializationService.CreateStore();
				componentSerializationService.SerializeMemberAbsolute(_afterChange, component, member);
				_afterChange.Close();
			}

			public override void Undo(UndoEngine engine)
			{
				if (_beforeChange != null)
				{
					IDesignerHost designerHost = (IDesignerHost)engine.GetRequiredService(typeof(IDesignerHost));
					_component = designerHost.Container.Components[_componentName];
					(engine.GetRequiredService(typeof(ComponentSerializationService)) as ComponentSerializationService).DeserializeTo(_beforeChange, designerHost.Container);
					SerializationStore beforeChange = _beforeChange;
					_beforeChange = _afterChange;
					_afterChange = beforeChange;
				}
			}
		}

		private UndoEngine _engine;

		private string _name;

		private bool _closed;

		private List<Action> _actions;

		/// <summary>Gets the parent <see cref="P:System.ComponentModel.Design.UndoEngine.UndoUnit.UndoEngine" />.</summary>
		/// <returns>The <see cref="P:System.ComponentModel.Design.UndoEngine.UndoUnit.UndoEngine" /> to which this <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" /> is attached.</returns>
		protected UndoEngine UndoEngine => _engine;

		/// <summary>Gets a value indicating whether the <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" /> contains no events.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" /> contains no events; otherwise, <see langword="false" />.</returns>
		public virtual bool IsEmpty => _actions.Count == 0;

		/// <summary>Gets the name of the <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" />.</summary>
		/// <returns>The name of the <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" />.</returns>
		public virtual string Name => _name;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" /> class.</summary>
		/// <param name="engine">The undo engine that owns this undo unit.</param>
		/// <param name="name">The name for this undo unit.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="engine" /> is <see langword="null" />.</exception>
		public UndoUnit(UndoEngine engine, string name)
		{
			if (engine == null)
			{
				throw new ArgumentNullException("engine");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			_engine = engine;
			_name = name;
			_actions = new List<Action>();
		}

		/// <summary>Performs an undo or redo action.</summary>
		public void Undo()
		{
			_engine.OnUndoing(EventArgs.Empty);
			UndoCore();
			_engine.OnUndone(EventArgs.Empty);
		}

		/// <summary>Called by <see cref="M:System.ComponentModel.Design.UndoEngine.UndoUnit.Undo" /> to perform an undo action.</summary>
		protected virtual void UndoCore()
		{
			for (int num = _actions.Count - 1; num >= 0; num--)
			{
				_actions[num].Undo(_engine);
			}
			_actions.Reverse();
		}

		/// <summary>Receives a call from the undo engine to close this unit.</summary>
		public virtual void Close()
		{
			_closed = true;
		}

		/// <summary>Receives a call from the <see cref="T:System.ComponentModel.Design.UndoEngine" /> in response to a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdded" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentEventArgs" /> that contains the event data.</param>
		public virtual void ComponentAdded(ComponentEventArgs e)
		{
			if (!_closed)
			{
				_actions.Add(new ComponentAddRemoveAction(_engine, e.Component, added: true));
			}
		}

		/// <summary>Receives a call from the <see cref="T:System.ComponentModel.Design.UndoEngine" /> in response to a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdding" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentEventArgs" /> that contains the event data.</param>
		public virtual void ComponentAdding(ComponentEventArgs e)
		{
		}

		/// <summary>Receives a call from the <see cref="T:System.ComponentModel.Design.UndoEngine" /> in response to a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentChangedEventArgs" /> that contains the event data.</param>
		public virtual void ComponentChanged(ComponentChangedEventArgs e)
		{
			if (_closed)
			{
				return;
			}
			ComponentChangeAction componentChangeAction = null;
			for (int i = 0; i < _actions.Count; i++)
			{
				if (_actions[i] is ComponentChangeAction { IsComplete: false } componentChangeAction2 && componentChangeAction2.Component == e.Component && componentChangeAction2.Member.Equals(e.Member))
				{
					componentChangeAction2.SetModifiedState(_engine, (IComponent)e.Component, e.Member);
					break;
				}
			}
		}

		/// <summary>Receives a call from the <see cref="T:System.ComponentModel.Design.UndoEngine" /> in response to a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentChangedEventArgs" /> that contains the event data.</param>
		public virtual void ComponentChanging(ComponentChangingEventArgs e)
		{
			if (!_closed)
			{
				ComponentChangeAction componentChangeAction = new ComponentChangeAction();
				componentChangeAction.SetOriginalState(_engine, (IComponent)e.Component, e.Member);
				_actions.Add(componentChangeAction);
			}
		}

		/// <summary>Receives a call from the <see cref="T:System.ComponentModel.Design.UndoEngine" /> in response to a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoved" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentEventArgs" /> that contains the event data.</param>
		public virtual void ComponentRemoved(ComponentEventArgs e)
		{
		}

		/// <summary>Receives a call from the <see cref="T:System.ComponentModel.Design.UndoEngine" /> in response to a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoving" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentEventArgs" /> that contains the event data.</param>
		public virtual void ComponentRemoving(ComponentEventArgs e)
		{
			if (!_closed)
			{
				_actions.Add(new ComponentAddRemoveAction(_engine, e.Component, added: false));
			}
		}

		/// <summary>Receives a call from the <see cref="T:System.ComponentModel.Design.UndoEngine" /> in response to a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRename" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentRenameEventArgs" /> that contains the event data.</param>
		public virtual void ComponentRename(ComponentRenameEventArgs e)
		{
			if (!_closed)
			{
				_actions.Add(new ComponentRenameAction(e.NewName, e.OldName));
			}
		}

		/// <summary>Gets an instance of the requested service.</summary>
		/// <param name="serviceType">The type of service to retrieve.</param>
		/// <returns>An instance of the given service, or <see langword="null" /> if the service cannot be resolved.</returns>
		protected object GetService(Type serviceType)
		{
			return _engine.GetService(serviceType);
		}

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current name of the unit.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current name of the unit.</returns>
		public override string ToString()
		{
			return _name;
		}
	}

	private bool _undoing;

	private UndoUnit _currentUnit;

	private IServiceProvider _provider;

	private bool _enabled;

	/// <summary>Enables or disables the <see cref="T:System.ComponentModel.Design.UndoEngine" />.</summary>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.ComponentModel.Design.UndoEngine" /> is enabled; otherwise, <see langword="false" />.</returns>
	public bool Enabled
	{
		get
		{
			return _enabled;
		}
		set
		{
			if (value)
			{
				Enable();
			}
			else
			{
				Disable();
			}
		}
	}

	/// <summary>Indicates if an undo action is in progress.</summary>
	/// <returns>
	///   <see langword="true" /> if an undo action is in progress; otherwise, <see langword="false" />.</returns>
	public bool UndoInProgress => _undoing;

	/// <summary>Occurs immediately before an undo action is performed.</summary>
	public event EventHandler Undoing;

	/// <summary>Occurs immediately after an undo action is performed.</summary>
	public event EventHandler Undone;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.UndoEngine" /> class.</summary>
	/// <param name="provider">A parenting service provider.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="provider" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.NotSupportedException">A required service cannot be found. See <see cref="T:System.ComponentModel.Design.UndoEngine" /> for required services. If you have removed this service, ensure that you provide a replacement.</exception>
	protected UndoEngine(IServiceProvider provider)
	{
		if (provider == null)
		{
			throw new ArgumentNullException("provider");
		}
		_provider = provider;
		_currentUnit = null;
		Enable();
	}

	private void Enable()
	{
		if (!_enabled)
		{
			IComponentChangeService obj = GetRequiredService(typeof(IComponentChangeService)) as IComponentChangeService;
			obj.ComponentAdding += OnComponentAdding;
			obj.ComponentAdded += OnComponentAdded;
			obj.ComponentRemoving += OnComponentRemoving;
			obj.ComponentRemoved += OnComponentRemoved;
			obj.ComponentChanging += OnComponentChanging;
			obj.ComponentChanged += OnComponentChanged;
			obj.ComponentRename += OnComponentRename;
			IDesignerHost obj2 = GetRequiredService(typeof(IDesignerHost)) as IDesignerHost;
			obj2.TransactionClosed += OnTransactionClosed;
			obj2.TransactionOpened += OnTransactionOpened;
			_enabled = true;
		}
	}

	private void Disable()
	{
		if (_enabled)
		{
			IComponentChangeService obj = GetRequiredService(typeof(IComponentChangeService)) as IComponentChangeService;
			obj.ComponentAdding -= OnComponentAdding;
			obj.ComponentAdded -= OnComponentAdded;
			obj.ComponentRemoving -= OnComponentRemoving;
			obj.ComponentRemoved -= OnComponentRemoved;
			obj.ComponentChanging -= OnComponentChanging;
			obj.ComponentChanged -= OnComponentChanged;
			obj.ComponentRename -= OnComponentRename;
			IDesignerHost obj2 = GetRequiredService(typeof(IDesignerHost)) as IDesignerHost;
			obj2.TransactionClosed -= OnTransactionClosed;
			obj2.TransactionOpened -= OnTransactionOpened;
			_enabled = false;
		}
	}

	private void OnTransactionOpened(object sender, EventArgs args)
	{
		if (_currentUnit == null)
		{
			IDesignerHost designerHost = GetRequiredService(typeof(IDesignerHost)) as IDesignerHost;
			_currentUnit = CreateUndoUnit(designerHost.TransactionDescription, primary: true);
		}
	}

	private void OnTransactionClosed(object sender, DesignerTransactionCloseEventArgs args)
	{
		if (!(GetRequiredService(typeof(IDesignerHost)) as IDesignerHost).InTransaction)
		{
			_currentUnit.Close();
			if (args.TransactionCommitted)
			{
				AddUndoUnit(_currentUnit);
			}
			else
			{
				_currentUnit.Undo();
				DiscardUndoUnit(_currentUnit);
			}
			_currentUnit = null;
		}
	}

	private void OnComponentAdding(object sender, ComponentEventArgs args)
	{
		if (_currentUnit == null)
		{
			_currentUnit = CreateUndoUnit("Add " + args.Component.GetType().Name, primary: true);
		}
		_currentUnit.ComponentAdding(args);
	}

	private void OnComponentAdded(object sender, ComponentEventArgs args)
	{
		if (_currentUnit == null)
		{
			_currentUnit = CreateUndoUnit("Add " + args.Component.Site.Name, primary: true);
		}
		_currentUnit.ComponentAdded(args);
		if (!(GetRequiredService(typeof(IDesignerHost)) as IDesignerHost).InTransaction)
		{
			_currentUnit.Close();
			AddUndoUnit(_currentUnit);
			_currentUnit = null;
		}
	}

	private void OnComponentRemoving(object sender, ComponentEventArgs args)
	{
		if (_currentUnit == null)
		{
			_currentUnit = CreateUndoUnit("Remove " + args.Component.Site.Name, primary: true);
		}
		_currentUnit.ComponentRemoving(args);
	}

	private void OnComponentRemoved(object sender, ComponentEventArgs args)
	{
		if (_currentUnit == null)
		{
			_currentUnit = CreateUndoUnit("Remove " + args.Component.GetType().Name, primary: true);
		}
		_currentUnit.ComponentRemoved(args);
		if (!(GetRequiredService(typeof(IDesignerHost)) as IDesignerHost).InTransaction)
		{
			_currentUnit.Close();
			AddUndoUnit(_currentUnit);
			_currentUnit = null;
		}
	}

	private void OnComponentChanging(object sender, ComponentChangingEventArgs args)
	{
		if (_currentUnit == null)
		{
			_currentUnit = CreateUndoUnit("Modify " + ((IComponent)args.Component).Site.Name + ((args.Member != null) ? ("." + args.Member.Name) : ""), primary: true);
		}
		_currentUnit.ComponentChanging(args);
	}

	private void OnComponentChanged(object sender, ComponentChangedEventArgs args)
	{
		if (_currentUnit == null)
		{
			_currentUnit = CreateUndoUnit("Modify " + ((IComponent)args.Component).Site.Name + "." + ((args.Member != null) ? ("." + args.Member.Name) : ""), primary: true);
		}
		_currentUnit.ComponentChanged(args);
		if (!(GetRequiredService(typeof(IDesignerHost)) as IDesignerHost).InTransaction)
		{
			_currentUnit.Close();
			AddUndoUnit(_currentUnit);
			_currentUnit = null;
		}
	}

	private void OnComponentRename(object sender, ComponentRenameEventArgs args)
	{
		if (_currentUnit == null)
		{
			_currentUnit = CreateUndoUnit("Rename " + ((IComponent)args.Component).Site.Name, primary: true);
		}
		_currentUnit.ComponentRename(args);
		if (!(GetRequiredService(typeof(IDesignerHost)) as IDesignerHost).InTransaction)
		{
			_currentUnit.Close();
			AddUndoUnit(_currentUnit);
			_currentUnit = null;
		}
	}

	/// <summary>Creates a new <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" />.</summary>
	/// <param name="name">The name of the unit to create.</param>
	/// <param name="primary">
	///   <see langword="true" /> to create the first of a series of nested units; <see langword="false" /> to create subsequent nested units.</param>
	/// <returns>A new <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" /> with a specified name.</returns>
	protected virtual UndoUnit CreateUndoUnit(string name, bool primary)
	{
		return new UndoUnit(this, name);
	}

	/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.UndoEngine" />.</summary>
	public void Dispose()
	{
		Dispose(disposing: true);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.UndoEngine" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected virtual void Dispose(bool disposing)
	{
		if (disposing && _currentUnit != null)
		{
			_currentUnit.Close();
			_currentUnit = null;
		}
	}

	/// <summary>Gets the requested service.</summary>
	/// <param name="serviceType">The type of service to retrieve.</param>
	/// <returns>The requested service, if found.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="serviceType" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.NotSupportedException">
	///   <paramref name="serviceType" /> is required but cannot be found. If you have removed this service, ensure that you provide a replacement.</exception>
	protected object GetRequiredService(Type serviceType)
	{
		return GetService(serviceType) ?? throw new NotSupportedException("Service '" + serviceType.Name + "' missing");
	}

	/// <summary>Gets the requested service.</summary>
	/// <param name="serviceType">The type of service to retrieve.</param>
	/// <returns>The requested service, or <see langword="null" /> if the requested service is not found.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="serviceType" /> is <see langword="null" />.</exception>
	protected object GetService(Type serviceType)
	{
		if (serviceType == null)
		{
			throw new ArgumentNullException("serviceType");
		}
		if (_provider != null)
		{
			return _provider.GetService(serviceType);
		}
		return null;
	}

	/// <summary>Raises the <see cref="E:System.ComponentModel.Design.UndoEngine.Undoing" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnUndoing(EventArgs e)
	{
		Disable();
		_undoing = true;
		if (this.Undoing != null)
		{
			this.Undoing(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.ComponentModel.Design.UndoEngine.Undone" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnUndone(EventArgs e)
	{
		Enable();
		_undoing = false;
		if (this.Undone != null)
		{
			this.Undone(this, e);
		}
	}

	/// <summary>Adds an <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" /> to the undo stack.</summary>
	/// <param name="unit">The undo unit to add</param>
	protected abstract void AddUndoUnit(UndoUnit unit);

	/// <summary>Discards an <see cref="T:System.ComponentModel.Design.UndoEngine.UndoUnit" />.</summary>
	/// <param name="unit">The unit to discard.</param>
	protected virtual void DiscardUndoUnit(UndoUnit unit)
	{
	}
}
