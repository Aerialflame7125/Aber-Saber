using System.Collections;

namespace System.ComponentModel.Design;

/// <summary>Extends the design mode behavior of a component.</summary>
public class ComponentDesigner : ITreeDesigner, IDesigner, IDisposable, IDesignerFilter, IComponentInitializer
{
	/// <summary>Represents a collection of shadow properties that should override inherited default or assigned values for specific properties. This class cannot be inherited.</summary>
	protected sealed class ShadowPropertyCollection
	{
		private Hashtable _properties;

		private IComponent _component;

		/// <summary>Gets or sets the object at the specified index.</summary>
		/// <param name="propertyName">The name of the property to access in the collection.</param>
		/// <returns>The value of the specified property, if it exists in the collection. Otherwise, the value is retrieved from the current value of the nonshadowed property.</returns>
		public object this[string propertyName]
		{
			get
			{
				if (propertyName == null)
				{
					throw new ArgumentNullException("propertyName");
				}
				if (_properties != null && _properties.ContainsKey(propertyName))
				{
					return _properties[propertyName];
				}
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(_component.GetType())[propertyName];
				if (propertyDescriptor != null)
				{
					return propertyDescriptor.GetValue(_component);
				}
				throw new Exception("Propery not found!");
			}
			set
			{
				if (_properties == null)
				{
					_properties = new Hashtable();
				}
				_properties[propertyName] = value;
			}
		}

		internal ShadowPropertyCollection(IComponent component)
		{
			_component = component;
		}

		/// <summary>Indicates whether a property matching the specified name exists in the collection.</summary>
		/// <param name="propertyName">The name of the property to check for in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the property exists in the collection; otherwise, <see langword="false" />.</returns>
		public bool Contains(string propertyName)
		{
			if (_properties != null)
			{
				return _properties.ContainsKey(propertyName);
			}
			return false;
		}
	}

	private IComponent _component;

	private DesignerVerbCollection _verbs;

	private ShadowPropertyCollection _shadowPropertyCollection;

	private DesignerActionListCollection _designerActionList;

	/// <summary>Gets the collection of components associated with the component managed by the designer.</summary>
	/// <returns>The components that are associated with the component managed by the designer.</returns>
	public virtual ICollection AssociatedComponents => new IComponent[0];

	/// <summary>Gets the component this designer is designing.</summary>
	/// <returns>The component managed by the designer.</returns>
	public IComponent Component => _component;

	/// <summary>Gets the design-time verbs supported by the component that is associated with the designer.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects, or <see langword="null" /> if no designer verbs are available. This default implementation always returns <see langword="null" />.</returns>
	public virtual DesignerVerbCollection Verbs
	{
		get
		{
			if (_verbs == null)
			{
				_verbs = new DesignerVerbCollection();
			}
			return _verbs;
		}
	}

	/// <summary>Gets an attribute that indicates the type of inheritance of the associated component.</summary>
	/// <returns>The <see cref="T:System.ComponentModel.InheritanceAttribute" /> for the associated component.</returns>
	protected virtual InheritanceAttribute InheritanceAttribute
	{
		get
		{
			IInheritanceService inheritanceService = (IInheritanceService)GetService(typeof(IInheritanceService));
			if (inheritanceService != null)
			{
				return inheritanceService.GetInheritanceAttribute(_component);
			}
			return InheritanceAttribute.Default;
		}
	}

	/// <summary>Gets a value indicating whether this component is inherited.</summary>
	/// <returns>
	///   <see langword="true" /> if the component is inherited; otherwise, <see langword="false" />.</returns>
	protected bool Inherited => !InheritanceAttribute.Equals(InheritanceAttribute.NotInherited);

	/// <summary>Gets a collection of property values that override user settings.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.ComponentDesigner.ShadowPropertyCollection" /> that indicates the shadow properties of the design document.</returns>
	protected ShadowPropertyCollection ShadowProperties
	{
		get
		{
			if (_shadowPropertyCollection == null)
			{
				_shadowPropertyCollection = new ShadowPropertyCollection(_component);
			}
			return _shadowPropertyCollection;
		}
	}

	/// <summary>Gets the design-time action lists supported by the component associated with the designer.</summary>
	/// <returns>The design-time action lists supported by the component associated with the designer.</returns>
	public virtual DesignerActionListCollection ActionLists
	{
		get
		{
			if (_designerActionList == null)
			{
				_designerActionList = new DesignerActionListCollection();
			}
			return _designerActionList;
		}
	}

	/// <summary>Gets the parent component for this designer.</summary>
	/// <returns>The parent component for this designer, or <see langword="null" /> if this designer is the root component.</returns>
	protected virtual IComponent ParentComponent
	{
		get
		{
			if (GetService(typeof(IDesignerHost)) is IDesignerHost { RootComponent: var rootComponent } && rootComponent != _component)
			{
				return rootComponent;
			}
			return null;
		}
	}

	/// <summary>For a description of this member, see the <see cref="P:System.ComponentModel.Design.ITreeDesigner.Children" /> property.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the collection of <see cref="T:System.ComponentModel.Design.IDesigner" /> designers contained in the current parent designer.</returns>
	ICollection ITreeDesigner.Children
	{
		get
		{
			ICollection associatedComponents = AssociatedComponents;
			if (GetService(typeof(IDesignerHost)) is IDesignerHost designerHost)
			{
				ArrayList arrayList = new ArrayList();
				foreach (IComponent item in associatedComponents)
				{
					IDesigner designer = designerHost.GetDesigner(item);
					if (designer != null)
					{
						arrayList.Add(designer);
					}
				}
				IDesigner[] array = new IDesigner[arrayList.Count];
				arrayList.CopyTo(array);
				return array;
			}
			return new IDesigner[0];
		}
	}

	/// <summary>For a description of this member, see the <see cref="P:System.ComponentModel.Design.ITreeDesigner.Parent" /> property.</summary>
	/// <returns>An <see cref="T:System.ComponentModel.Design.IDesigner" /> representing the parent designer, or <see langword="null" /> if there is no parent.</returns>
	IDesigner ITreeDesigner.Parent
	{
		get
		{
			if (GetService(typeof(IDesignerHost)) is IDesignerHost designerHost && ParentComponent != null)
			{
				return designerHost.GetDesigner(ParentComponent);
			}
			return null;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentDesigner" /> class.</summary>
	public ComponentDesigner()
	{
	}

	/// <summary>Initializes a newly created component.</summary>
	/// <param name="defaultValues">A name/value dictionary of default values to apply to properties. May be <see langword="null" /> if no default values are specified.</param>
	public virtual void InitializeNewComponent(IDictionary defaultValues)
	{
		OnSetComponentDefaults();
	}

	/// <summary>Reinitializes an existing component.</summary>
	/// <param name="defaultValues">A name/value dictionary of default values to apply to properties. May be <see langword="null" /> if no default values are specified.</param>
	public virtual void InitializeExistingComponent(IDictionary defaultValues)
	{
		InitializeNonDefault();
	}

	/// <summary>Prepares the designer to view, edit, and design the specified component.</summary>
	/// <param name="component">The component for this designer.</param>
	public virtual void Initialize(IComponent component)
	{
		if (component == null)
		{
			throw new ArgumentNullException("component");
		}
		_component = component;
	}

	/// <summary>Initializes the settings for an imported component that is already initialized to settings other than the defaults.</summary>
	[Obsolete("This method has been deprecated. Use InitializeExistingComponent instead.")]
	public virtual void InitializeNonDefault()
	{
	}

	/// <summary>Creates a method signature in the source code file for the default event on the component and navigates the user's cursor to that location.</summary>
	/// <exception cref="T:System.ComponentModel.Design.CheckoutException">An attempt to check out a file that is checked into a source code management program failed.</exception>
	public virtual void DoDefaultAction()
	{
		IDesignerHost designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
		DesignerTransaction designerTransaction = null;
		if (designerHost != null)
		{
			designerTransaction = designerHost.CreateTransaction("ComponentDesigner_AddEvent");
		}
		IEventBindingService eventBindingService = GetService(typeof(IEventBindingService)) as IEventBindingService;
		EventDescriptor eventDescriptor = null;
		if (eventBindingService == null)
		{
			return;
		}
		ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
		try
		{
			if (selectionService != null)
			{
				foreach (IComponent selectedComponent in selectionService.GetSelectedComponents())
				{
					EventDescriptor defaultEvent = TypeDescriptor.GetDefaultEvent(selectedComponent);
					if (defaultEvent == null)
					{
						continue;
					}
					PropertyDescriptor eventProperty = eventBindingService.GetEventProperty(defaultEvent);
					if (eventProperty == null || eventProperty.IsReadOnly)
					{
						continue;
					}
					string text = eventProperty.GetValue(selectedComponent) as string;
					bool flag = true;
					if (text != null || text != string.Empty)
					{
						foreach (string compatibleMethod in eventBindingService.GetCompatibleMethods(defaultEvent))
						{
							if (compatibleMethod == text)
							{
								flag = false;
								break;
							}
						}
					}
					if (flag)
					{
						if (text == null)
						{
							text = eventBindingService.CreateUniqueMethodName(selectedComponent, defaultEvent);
						}
						eventProperty.SetValue(selectedComponent, text);
					}
					if (selectedComponent == _component)
					{
						eventDescriptor = defaultEvent;
					}
				}
			}
		}
		catch
		{
			if (designerTransaction != null)
			{
				designerTransaction.Cancel();
				designerTransaction = null;
			}
		}
		finally
		{
			designerTransaction?.Commit();
		}
		if (eventDescriptor != null)
		{
			eventBindingService.ShowCode(_component, eventDescriptor);
		}
	}

	/// <summary>Sets the default properties for the component.</summary>
	[Obsolete("This method has been deprecated. Use InitializeNewComponent instead.")]
	public virtual void OnSetComponentDefaults()
	{
		if (_component == null || _component.Site == null)
		{
			return;
		}
		PropertyDescriptor defaultProperty = TypeDescriptor.GetDefaultProperty(_component);
		if (defaultProperty != null && defaultProperty.PropertyType.Equals(typeof(string)))
		{
			string text = (string)defaultProperty.GetValue(_component);
			if (text != null && text.Length != 0)
			{
				defaultProperty.SetValue(_component, _component.Site.Name);
			}
		}
	}

	/// <summary>Gets the <see cref="T:System.ComponentModel.InheritanceAttribute" /> of the specified <see cref="T:System.ComponentModel.Design.ComponentDesigner" />.</summary>
	/// <param name="toInvoke">The <see cref="T:System.ComponentModel.Design.ComponentDesigner" /> whose inheritance attribute to retrieve.</param>
	/// <returns>The <see cref="T:System.ComponentModel.InheritanceAttribute" /> of the specified designer.</returns>
	protected InheritanceAttribute InvokeGetInheritanceAttribute(ComponentDesigner toInvoke)
	{
		return toInvoke.InheritanceAttribute;
	}

	/// <summary>Allows a designer to change or remove items from the set of attributes that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	/// <param name="attributes">The attributes for the class of the component.</param>
	protected virtual void PostFilterAttributes(IDictionary attributes)
	{
	}

	/// <summary>Allows a designer to change or remove items from the set of events that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	/// <param name="events">The events for the class of the component.</param>
	protected virtual void PostFilterEvents(IDictionary events)
	{
	}

	/// <summary>Allows a designer to change or remove items from the set of properties that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	/// <param name="properties">The properties for the class of the component.</param>
	protected virtual void PostFilterProperties(IDictionary properties)
	{
	}

	/// <summary>Allows a designer to add to the set of attributes that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	/// <param name="attributes">The attributes for the class of the component.</param>
	protected virtual void PreFilterAttributes(IDictionary attributes)
	{
	}

	/// <summary>Allows a designer to add to the set of events that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	/// <param name="events">The events for the class of the component.</param>
	protected virtual void PreFilterEvents(IDictionary events)
	{
	}

	/// <summary>Allows a designer to add to the set of properties that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	/// <param name="properties">The properties for the class of the component.</param>
	protected virtual void PreFilterProperties(IDictionary properties)
	{
	}

	/// <summary>Notifies the <see cref="T:System.ComponentModel.Design.IComponentChangeService" /> that this component has been changed.</summary>
	/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that indicates the member that has been changed.</param>
	/// <param name="oldValue">The old value of the member.</param>
	/// <param name="newValue">The new value of the member.</param>
	protected void RaiseComponentChanged(MemberDescriptor member, object oldValue, object newValue)
	{
		if (GetService(typeof(IComponentChangeService)) is IComponentChangeService componentChangeService)
		{
			componentChangeService.OnComponentChanged(_component, member, oldValue, newValue);
		}
	}

	/// <summary>Notifies the <see cref="T:System.ComponentModel.Design.IComponentChangeService" /> that this component is about to be changed.</summary>
	/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that indicates the member that is about to be changed.</param>
	protected void RaiseComponentChanging(MemberDescriptor member)
	{
		if (GetService(typeof(IComponentChangeService)) is IComponentChangeService componentChangeService)
		{
			componentChangeService.OnComponentChanging(_component, member);
		}
	}

	/// <summary>For a description of this member, see the <see cref="M:System.ComponentModel.Design.IDesignerFilter.PostFilterAttributes(System.Collections.IDictionary)" /> method.</summary>
	/// <param name="attributes">The <see cref="T:System.Attribute" /> objects for the class of the component. The keys in the dictionary of attributes are the <see cref="P:System.Attribute.TypeId" /> values of the attributes.</param>
	void IDesignerFilter.PostFilterAttributes(IDictionary attributes)
	{
		PostFilterAttributes(attributes);
	}

	/// <summary>For a description of this member, see the <see cref="M:System.ComponentModel.Design.IDesignerFilter.PostFilterEvents(System.Collections.IDictionary)" /> method.</summary>
	/// <param name="events">The <see cref="T:System.ComponentModel.EventDescriptor" /> objects that represent the events of the class of the component. The keys in the dictionary of events are event names.</param>
	void IDesignerFilter.PostFilterEvents(IDictionary events)
	{
		PostFilterEvents(events);
	}

	/// <summary>For a description of this member, see the <see cref="M:System.ComponentModel.Design.IDesignerFilter.PostFilterProperties(System.Collections.IDictionary)" /> method.</summary>
	/// <param name="properties">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that represent the properties of the class of the component. The keys in the dictionary of properties are property names.</param>
	void IDesignerFilter.PostFilterProperties(IDictionary properties)
	{
		PostFilterProperties(properties);
	}

	/// <summary>For a description of this member, see the <see cref="M:System.ComponentModel.Design.IDesignerFilter.PreFilterAttributes(System.Collections.IDictionary)" /> method.</summary>
	/// <param name="attributes">The <see cref="T:System.Attribute" /> objects for the class of the component. The keys in the dictionary of attributes are the <see cref="P:System.Attribute.TypeId" /> values of the attributes.</param>
	void IDesignerFilter.PreFilterAttributes(IDictionary attributes)
	{
		PreFilterAttributes(attributes);
	}

	/// <summary>For a description of this member, see the <see cref="M:System.ComponentModel.Design.IDesignerFilter.PreFilterEvents(System.Collections.IDictionary)" /> method.</summary>
	/// <param name="events">The <see cref="T:System.ComponentModel.EventDescriptor" /> objects that represent the events of the class of the component. The keys in the dictionary of events are event names.</param>
	void IDesignerFilter.PreFilterEvents(IDictionary events)
	{
		PreFilterEvents(events);
	}

	/// <summary>For a description of this member, see the <see cref="M:System.ComponentModel.Design.IDesignerFilter.PreFilterProperties(System.Collections.IDictionary)" /> method.</summary>
	/// <param name="properties">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that represent the properties of the class of the component. The keys in the dictionary of properties are property names.</param>
	void IDesignerFilter.PreFilterProperties(IDictionary properties)
	{
		PreFilterProperties(properties);
	}

	/// <summary>Attempts to retrieve the specified type of service from the design mode site of the designer's component.</summary>
	/// <param name="serviceType">The type of service to request.</param>
	/// <returns>An object implementing the requested service, or <see langword="null" /> if the service cannot be resolved.</returns>
	protected virtual object GetService(Type serviceType)
	{
		if (_component != null && _component.Site != null)
		{
			return _component.Site.GetService(serviceType);
		}
		return null;
	}

	/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.ComponentDesigner" />.</summary>
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.ComponentDesigner" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
		{
			_component = null;
		}
	}

	/// <summary>Attempts to free resources by calling <see langword="Dispose(false)" /> before the object is reclaimed by garbage collection.</summary>
	~ComponentDesigner()
	{
		Dispose(disposing: false);
	}
}
