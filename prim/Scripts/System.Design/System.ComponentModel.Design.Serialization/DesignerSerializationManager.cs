using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace System.ComponentModel.Design.Serialization;

/// <summary>Provides an implementation of the <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> interface.</summary>
public class DesignerSerializationManager : IDesignerSerializationManager, IServiceProvider
{
	private class Session : IDisposable
	{
		private DesignerSerializationManager _manager;

		public Session(DesignerSerializationManager manager)
		{
			_manager = manager;
		}

		public void Dispose()
		{
			_manager.OnSessionDisposed(EventArgs.Empty);
		}
	}

	private IServiceProvider _serviceProvider;

	private bool _preserveNames;

	private bool _validateRecycledTypes;

	private bool _recycleInstances;

	private IContainer _designerContainer;

	private object _propertyProvider;

	private Session _session;

	private ArrayList _errors;

	private List<IDesignerSerializationProvider> _serializationProviders;

	private Dictionary<Type, object> _serializersCache;

	private Dictionary<string, object> _instancesByNameCache;

	private Dictionary<object, string> _instancesByValueCache;

	private ContextStack _contextStack;

	private EventHandler _serializationCompleteHandler;

	private ResolveNameEventHandler _resolveNameHandler;

	/// <summary>Gets or sets a value that indicates whether <see cref="M:System.ComponentModel.Design.Serialization.DesignerSerializationManager.CreateInstance(System.Type,System.Collections.ICollection,System.String,System.Boolean)" /> will always create a new instance of a type.</summary>
	/// <returns>
	///   <see langword="true" /> if <see cref="M:System.ComponentModel.Design.Serialization.DesignerSerializationManager.CreateInstance(System.Type,System.Collections.ICollection,System.String,System.Boolean)" /> will return the existing instance; <see langword="false" /> if <see cref="M:System.ComponentModel.Design.Serialization.DesignerSerializationManager.CreateInstance(System.Type,System.Collections.ICollection,System.String,System.Boolean)" /> will create a new instance of a type. The default is <see langword="false" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The serialization manager has an active serialization session.</exception>
	public bool RecycleInstances
	{
		get
		{
			return _recycleInstances;
		}
		set
		{
			VerifyNotInSession();
			_recycleInstances = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="M:System.ComponentModel.Design.Serialization.DesignerSerializationManager.CreateInstance(System.Type,System.Collections.ICollection,System.String,System.Boolean)" /> method should check for the presence of the given name in the container.</summary>
	/// <returns>
	///   <see langword="true" /> if <see cref="M:System.ComponentModel.Design.Serialization.DesignerSerializationManager.CreateInstance(System.Type,System.Collections.ICollection,System.String,System.Boolean)" /> will pass the given component name; <see langword="false" /> if <see cref="M:System.ComponentModel.Design.Serialization.DesignerSerializationManager.CreateInstance(System.Type,System.Collections.ICollection,System.String,System.Boolean)" /> will check for the presence of the given name in the container. The default is <see langword="true" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">This property was changed from within a serialization session.</exception>
	public bool PreserveNames
	{
		get
		{
			return _preserveNames;
		}
		set
		{
			VerifyNotInSession();
			_preserveNames = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the <see cref="M:System.ComponentModel.Design.Serialization.DesignerSerializationManager.CreateInstance(System.Type,System.Collections.ICollection,System.String,System.Boolean)" /> method will verify that matching names refer to the same type.</summary>
	/// <returns>
	///   <see langword="true" /> if <see cref="M:System.ComponentModel.Design.Serialization.DesignerSerializationManager.CreateInstance(System.Type,System.Collections.ICollection,System.String,System.Boolean)" /> verifies types; otherwise, <see langword="false" /> if it does not. The default is <see langword="true" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The serialization manager has an active serialization session.</exception>
	public bool ValidateRecycledTypes
	{
		get
		{
			return _validateRecycledTypes;
		}
		set
		{
			VerifyNotInSession();
			_validateRecycledTypes = value;
		}
	}

	/// <summary>Gets or sets to the container for this serialization manager.</summary>
	/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> to which the serialization manager will add components.</returns>
	/// <exception cref="T:System.InvalidOperationException">The serialization manager has an active serialization session.</exception>
	public IContainer Container
	{
		get
		{
			if (_designerContainer == null)
			{
				_designerContainer = (GetService(typeof(IDesignerHost)) as IDesignerHost).Container;
			}
			return _designerContainer;
		}
		set
		{
			VerifyNotInSession();
			_designerContainer = value;
		}
	}

	/// <summary>Gets the object that should be used to provide properties to the serialization manager's <see cref="P:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.Properties" /> property.</summary>
	/// <returns>The object that should be used to provide properties to the serialization manager's <see cref="P:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.Properties" /> property.</returns>
	public object PropertyProvider
	{
		get
		{
			return _propertyProvider;
		}
		set
		{
			_propertyProvider = value;
		}
	}

	/// <summary>Gets the list of errors that occurred during serialization or deserialization.</summary>
	/// <returns>The list of errors that occurred during serialization or deserialization.</returns>
	/// <exception cref="T:System.InvalidOperationException">This property was accessed outside of a serialization session.</exception>
	public IList Errors => _errors;

	/// <summary>Gets the context stack for this serialization session.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.Serialization.ContextStack" /> that stores data.</returns>
	/// <exception cref="T:System.InvalidOperationException">This property was accessed outside of a serialization session.</exception>
	ContextStack IDesignerSerializationManager.Context => _contextStack;

	/// <summary>Implements the <see cref="P:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.Properties" /> property.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the properties to be serialized.</returns>
	PropertyDescriptorCollection IDesignerSerializationManager.Properties
	{
		get
		{
			PropertyDescriptorCollection result = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
			object propertyProvider = PropertyProvider;
			if (propertyProvider != null)
			{
				result = TypeDescriptor.GetProperties(propertyProvider);
			}
			return result;
		}
	}

	/// <summary>Occurs when a session is disposed.</summary>
	public event EventHandler SessionDisposed;

	/// <summary>Occurs when a session is created.</summary>
	public event EventHandler SessionCreated;

	/// <summary>Occurs when serialization is complete.</summary>
	/// <exception cref="T:System.InvalidOperationException">The serialization manager does not have an active serialization session.</exception>
	event EventHandler IDesignerSerializationManager.SerializationComplete
	{
		add
		{
			VerifyInSession();
			_serializationCompleteHandler = (EventHandler)Delegate.Combine(_serializationCompleteHandler, value);
		}
		remove
		{
			_serializationCompleteHandler = (EventHandler)Delegate.Remove(_serializationCompleteHandler, value);
		}
	}

	/// <summary>Occurs when <see cref="M:System.ComponentModel.Design.Serialization.DesignerSerializationManager.System#ComponentModel#Design#Serialization#IDesignerSerializationManager#GetName(System.Object)" /> cannot locate the specified name in the serialization manager's name table.</summary>
	/// <exception cref="T:System.InvalidOperationException">The serialization manager does not have an active serialization session.</exception>
	event ResolveNameEventHandler IDesignerSerializationManager.ResolveName
	{
		add
		{
			VerifyInSession();
			_resolveNameHandler = (ResolveNameEventHandler)Delegate.Combine(_resolveNameHandler, value);
		}
		remove
		{
			_resolveNameHandler = (ResolveNameEventHandler)Delegate.Remove(_resolveNameHandler, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializationManager" /> class.</summary>
	public DesignerSerializationManager()
		: this(null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializationManager" /> class with the given service provider.</summary>
	/// <param name="provider">An <see cref="T:System.IServiceProvider" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="provider" /> is <see langword="null" />.</exception>
	public DesignerSerializationManager(IServiceProvider provider)
	{
		_serviceProvider = provider;
		_preserveNames = true;
		_validateRecycledTypes = true;
	}

	/// <summary>Raises the <see cref="E:System.ComponentModel.Design.Serialization.DesignerSerializationManager.SessionCreated" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnSessionCreated(EventArgs e)
	{
		if (this.SessionCreated != null)
		{
			this.SessionCreated(this, e);
		}
	}

	/// <summary>Creates an instance of a type.</summary>
	/// <param name="type">The type to create an instance of.</param>
	/// <param name="arguments">The parameters of the type's constructor. This can be <see langword="null" /> or an empty collection to invoke the default constructor.</param>
	/// <param name="name">A name to give the object. If <see langword="null" />, the object will not be given a name, unless the object is added to a container and the container gives the object a name.</param>
	/// <param name="addToContainer">
	///   <see langword="true" /> to add the object to the container if the object implements <see cref="T:System.ComponentModel.IComponent" />; otherwise, <see langword="false" />.</param>
	/// <returns>A new instance of the type specified by <paramref name="type" />.</returns>
	/// <exception cref="T:System.Runtime.Serialization.SerializationException">
	///   <paramref name="type" /> does not have a constructor that takes parameters contained in <paramref name="arguments" />.</exception>
	protected virtual object CreateInstance(Type type, ICollection arguments, string name, bool addToContainer)
	{
		VerifyInSession();
		object value = null;
		if (name != null && _recycleInstances)
		{
			_instancesByNameCache.TryGetValue(name, out value);
			if (value != null && _validateRecycledTypes && value.GetType() != type)
			{
				value = null;
			}
		}
		if (value == null || !_recycleInstances)
		{
			value = CreateInstance(type, arguments);
		}
		if (addToContainer && value != null && Container != null && typeof(IComponent).IsAssignableFrom(type))
		{
			if (_preserveNames)
			{
				Container.Add((IComponent)value, name);
			}
			else if (name != null && Container.Components[name] != null)
			{
				Container.Add((IComponent)value);
			}
			else
			{
				Container.Add((IComponent)value, name);
			}
			ISite site = ((IComponent)value).Site;
			if (site != null)
			{
				name = site.Name;
			}
		}
		if (value != null && name != null)
		{
			_instancesByNameCache[name] = value;
			_instancesByValueCache[value] = name;
		}
		return value;
	}

	private object CreateInstance(Type type, ICollection argsCollection)
	{
		object result = null;
		object[] array = null;
		Type[] array2 = new Type[0];
		if (argsCollection != null)
		{
			array = new object[argsCollection.Count];
			array2 = new Type[argsCollection.Count];
			argsCollection.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					array2[i] = null;
				}
				else
				{
					array2[i] = array[i].GetType();
				}
			}
		}
		ConstructorInfo constructor = type.GetConstructor(array2);
		if (constructor != null)
		{
			result = constructor.Invoke(array);
		}
		return result;
	}

	/// <summary>Gets the serializer for the given object type.</summary>
	/// <param name="objectType">The type of object for which to retrieve the serializer.</param>
	/// <param name="serializerType">The type of serializer to retrieve.</param>
	/// <returns>The serializer for <paramref name="objectType" />, or <see langword="null" />, if not found.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectType" /> or <paramref name="serializerType" /> is <see langword="null" />.</exception>
	public object GetSerializer(Type objectType, Type serializerType)
	{
		VerifyInSession();
		if (serializerType == null)
		{
			throw new ArgumentNullException("serializerType");
		}
		object value = null;
		if (objectType != null)
		{
			_serializersCache.TryGetValue(objectType, out value);
			if (value != null && !serializerType.IsAssignableFrom(value.GetType()))
			{
				value = null;
			}
			if (TypeDescriptor.GetAttributes(objectType)[typeof(DefaultSerializationProviderAttribute)] is DefaultSerializationProviderAttribute defaultSerializationProviderAttribute && GetType(defaultSerializationProviderAttribute.ProviderTypeName) == serializerType)
			{
				object obj = Activator.CreateInstance(GetType(defaultSerializationProviderAttribute.ProviderTypeName), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
				((IDesignerSerializationManager)this).AddSerializationProvider((IDesignerSerializationProvider)obj);
			}
		}
		if (value == null && objectType != null)
		{
			if (TypeDescriptor.GetAttributes(objectType)[typeof(DesignerSerializerAttribute)] is DesignerSerializerAttribute designerSerializerAttribute && GetType(designerSerializerAttribute.SerializerBaseTypeName) == serializerType)
			{
				try
				{
					value = Activator.CreateInstance(GetType(designerSerializerAttribute.SerializerTypeName), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
				}
				catch
				{
				}
			}
			if (value != null)
			{
				_serializersCache[objectType] = value;
			}
		}
		if (value == null && _serializationProviders != null)
		{
			foreach (IDesignerSerializationProvider serializationProvider in _serializationProviders)
			{
				value = serializationProvider.GetSerializer(this, null, objectType, serializerType);
				if (value != null)
				{
					break;
				}
			}
		}
		return value;
	}

	private void VerifyInSession()
	{
		if (_session == null)
		{
			throw new InvalidOperationException("Not in session.");
		}
	}

	private void VerifyNotInSession()
	{
		if (_session != null)
		{
			throw new InvalidOperationException("In session.");
		}
	}

	/// <summary>Creates a new serialization session.</summary>
	/// <returns>An <see cref="T:System.IDisposable" /> that represents a new serialization session.</returns>
	/// <exception cref="T:System.InvalidOperationException">The serialization manager is already within a session. This version of <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializationManager" /> does not support simultaneous sessions.</exception>
	public IDisposable CreateSession()
	{
		VerifyNotInSession();
		_errors = new ArrayList();
		_session = new Session(this);
		_serializersCache = new Dictionary<Type, object>();
		_instancesByNameCache = new Dictionary<string, object>();
		_instancesByValueCache = new Dictionary<object, string>();
		_contextStack = new ContextStack();
		OnSessionCreated(EventArgs.Empty);
		return _session;
	}

	/// <summary>Raises the <see cref="E:System.ComponentModel.Design.Serialization.DesignerSerializationManager.SessionDisposed" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnSessionDisposed(EventArgs e)
	{
		_errors.Clear();
		_errors = null;
		_serializersCache.Clear();
		_serializersCache = null;
		_instancesByNameCache.Clear();
		_instancesByNameCache = null;
		_instancesByValueCache.Clear();
		_instancesByValueCache = null;
		_session = null;
		_contextStack = null;
		_resolveNameHandler = null;
		_serializationCompleteHandler = null;
		if (this.SessionDisposed != null)
		{
			this.SessionDisposed(this, e);
		}
		if (_serializationCompleteHandler != null)
		{
			_serializationCompleteHandler(this, EventArgs.Empty);
		}
	}

	/// <summary>Gets the requested type.</summary>
	/// <param name="typeName">The name of the type to retrieve.</param>
	/// <returns>The requested type, or <see langword="null" /> if the type cannot be resolved.</returns>
	protected virtual Type GetType(string typeName)
	{
		if (typeName == null)
		{
			throw new ArgumentNullException("typeName");
		}
		VerifyInSession();
		Type type = null;
		if (GetService(typeof(ITypeResolutionService)) is ITypeResolutionService typeResolutionService)
		{
			type = typeResolutionService.GetType(typeName);
		}
		if (type == null)
		{
			type = Type.GetType(typeName);
		}
		return type;
	}

	/// <summary>Raises the <see cref="E:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.ResolveName" /> event.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.Design.Serialization.ResolveNameEventArgs" /> that contains the event data.</param>
	protected virtual void OnResolveName(ResolveNameEventArgs e)
	{
		if (_resolveNameHandler != null)
		{
			_resolveNameHandler(this, e);
		}
	}

	/// <summary>Adds a custom serialization provider to the serialization manager.</summary>
	/// <param name="provider">The serialization provider to add.</param>
	void IDesignerSerializationManager.AddSerializationProvider(IDesignerSerializationProvider provider)
	{
		if (_serializationProviders == null)
		{
			_serializationProviders = new List<IDesignerSerializationProvider>();
		}
		if (!_serializationProviders.Contains(provider))
		{
			_serializationProviders.Add(provider);
		}
	}

	/// <summary>Removes a previously added serialization provider.</summary>
	/// <param name="provider">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationProvider" /> to remove.</param>
	void IDesignerSerializationManager.RemoveSerializationProvider(IDesignerSerializationProvider provider)
	{
		if (_serializationProviders != null)
		{
			_serializationProviders.Remove(provider);
		}
	}

	/// <summary>Implements the <see cref="M:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.CreateInstance(System.Type,System.Collections.ICollection,System.String,System.Boolean)" /> method.</summary>
	/// <param name="type">The data type to create.</param>
	/// <param name="arguments">The arguments to pass to the constructor for this type.</param>
	/// <param name="name">The name of the object. This name can be used to access the object later through <see cref="M:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.GetInstance(System.String)" />. If <see langword="null" /> is passed, the object is still created but cannot be accessed by name.</param>
	/// <param name="addToContainer">
	///   <see langword="true" /> to add this object to the design container. The object must implement <see cref="T:System.ComponentModel.IComponent" /> for this to have any effect.</param>
	/// <returns>The newly created object instance.</returns>
	object IDesignerSerializationManager.CreateInstance(Type type, ICollection arguments, string name, bool addToContainer)
	{
		return CreateInstance(type, arguments, name, addToContainer);
	}

	/// <summary>Retrieves an instance of a created object of the specified name.</summary>
	/// <param name="name">The name of the object to retrieve.</param>
	/// <returns>An instance of the object with the given name, or <see langword="null" /> if no object by that name can be found.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="name" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">This property was accessed outside of a serialization session.</exception>
	object IDesignerSerializationManager.GetInstance(string name)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		VerifyInSession();
		object value = null;
		_instancesByNameCache.TryGetValue(name, out value);
		if (value == null && Container != null)
		{
			value = Container.Components[name];
		}
		if (value == null)
		{
			value = RequestInstance(name);
		}
		return value;
	}

	private object RequestInstance(string name)
	{
		ResolveNameEventArgs resolveNameEventArgs = new ResolveNameEventArgs(name);
		OnResolveName(resolveNameEventArgs);
		return resolveNameEventArgs.Value;
	}

	/// <summary>Gets a type of the specified name.</summary>
	/// <param name="typeName">The fully qualified name of the type to load.</param>
	/// <returns>An instance of the type, or <see langword="null" /> if the type cannot be loaded.</returns>
	/// <exception cref="T:System.InvalidOperationException">This property was accessed outside of a serialization session.</exception>
	Type IDesignerSerializationManager.GetType(string name)
	{
		return GetType(name);
	}

	/// <summary>Gets a serializer of the requested type for the specified object type.</summary>
	/// <param name="objectType">The type of the object to get the serializer for.</param>
	/// <param name="serializerType">The type of the serializer to retrieve.</param>
	/// <returns>An instance of the requested serializer, or <see langword="null" /> if no appropriate serializer can be located.</returns>
	object IDesignerSerializationManager.GetSerializer(Type type, Type serializerType)
	{
		return GetSerializer(type, serializerType);
	}

	/// <summary>Retrieves a name for the specified object.</summary>
	/// <param name="value">The object for which to retrieve the name.</param>
	/// <returns>The name of the object, or <see langword="null" /> if the object is unnamed.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">This property was accessed outside of a serialization session.</exception>
	string IDesignerSerializationManager.GetName(object instance)
	{
		if (instance == null)
		{
			throw new ArgumentNullException("instance");
		}
		VerifyInSession();
		string value = null;
		if (instance is IComponent)
		{
			ISite site = ((IComponent)instance).Site;
			if (site != null && site is INestedSite)
			{
				value = ((INestedSite)site).FullName;
			}
			else if (site != null)
			{
				value = site.Name;
			}
		}
		if (value == null)
		{
			_instancesByValueCache.TryGetValue(instance, out value);
		}
		return value;
	}

	/// <summary>Sets the name for the specified object.</summary>
	/// <param name="instance">The object to set the name.</param>
	/// <param name="name">A <see cref="T:System.String" /> used as the name of the object.</param>
	/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The object specified by instance already has a name, or <paramref name="name" /> is already used by another named object.</exception>
	/// <exception cref="T:System.InvalidOperationException">This property was accessed outside of a serialization session.</exception>
	void IDesignerSerializationManager.SetName(object instance, string name)
	{
		if (instance == null)
		{
			throw new ArgumentNullException("instance");
		}
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		if (_instancesByNameCache.ContainsKey(name))
		{
			throw new ArgumentException("The object specified by instance already has a name, or name is already used by another named object.");
		}
		_instancesByNameCache.Add(name, instance);
		_instancesByValueCache.Add(instance, name);
	}

	/// <summary>Used to report a recoverable error in serialization.</summary>
	/// <param name="errorInformation">An object containing the error information, usually of type <see cref="T:System.String" /> or <see cref="T:System.Exception" />.</param>
	/// <exception cref="T:System.InvalidOperationException">This property was accessed outside of a serialization session.</exception>
	void IDesignerSerializationManager.ReportError(object error)
	{
		VerifyInSession();
		_errors.Add(error);
	}

	/// <summary>For a description of this member, see the <see cref="M:System.IServiceProvider.GetService(System.Type)" /> method.</summary>
	/// <param name="serviceType">An object that specifies the type of service object to get.</param>
	/// <returns>A service object of type <paramref name="serviceType" />.  
	///  -or-  
	///  <see langword="null" /> if there is no service object of type <paramref name="serviceType" />.</returns>
	object IServiceProvider.GetService(Type serviceType)
	{
		return GetService(serviceType);
	}

	/// <summary>Gets the requested service.</summary>
	/// <param name="serviceType">The type of service to retrieve.</param>
	/// <returns>The requested service, or <see langword="null" /> if the service cannot be resolved.</returns>
	protected virtual object GetService(Type serviceType)
	{
		object result = null;
		if (_serviceProvider != null)
		{
			result = _serviceProvider.GetService(serviceType);
		}
		return result;
	}
}
