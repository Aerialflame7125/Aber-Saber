using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.ComponentModel.Design.Serialization;

/// <summary>Serializes a set of components into a serialization store.</summary>
public sealed class CodeDomComponentSerializationService : ComponentSerializationService
{
	[Serializable]
	private class CodeDomSerializationStore : SerializationStore, ISerializable
	{
		[Serializable]
		private class Entry
		{
			private bool _isSerialized;

			private object _serialized;

			private bool _absolute;

			private string _name;

			public bool IsSerialized
			{
				get
				{
					return _isSerialized;
				}
				set
				{
					_isSerialized = value;
				}
			}

			public object Serialized
			{
				get
				{
					return _serialized;
				}
				set
				{
					_serialized = value;
					_isSerialized = true;
				}
			}

			public bool Absolute
			{
				get
				{
					return _absolute;
				}
				set
				{
					_absolute = value;
				}
			}

			public string Name
			{
				get
				{
					return _name;
				}
				set
				{
					_name = value;
				}
			}

			protected Entry()
			{
			}

			public Entry(string name)
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				_name = name;
				_isSerialized = false;
				_absolute = false;
			}
		}

		[Serializable]
		private class MemberEntry : Entry
		{
			private MemberDescriptor _descriptor;

			public MemberDescriptor Descriptor
			{
				get
				{
					return _descriptor;
				}
				set
				{
					_descriptor = value;
				}
			}

			protected MemberEntry()
			{
			}

			public MemberEntry(MemberDescriptor descriptor)
			{
				if (descriptor == null)
				{
					throw new ArgumentNullException("descriptor");
				}
				_descriptor = descriptor;
				base.Name = descriptor.Name;
			}
		}

		[Serializable]
		private class ObjectEntry : Entry
		{
			private Type _type;

			[NonSerialized]
			private object _instance;

			private Dictionary<string, MemberEntry> _members;

			private bool _entireObject;

			public Type Type => _type;

			public object Instance
			{
				get
				{
					return _instance;
				}
				set
				{
					_instance = value;
					if (value != null)
					{
						_type = value.GetType();
					}
				}
			}

			public Dictionary<string, MemberEntry> Members
			{
				get
				{
					if (_members == null)
					{
						_members = new Dictionary<string, MemberEntry>();
					}
					return _members;
				}
				set
				{
					_members = value;
				}
			}

			public bool IsEntireObject
			{
				get
				{
					return _entireObject;
				}
				set
				{
					_entireObject = value;
				}
			}

			protected ObjectEntry()
			{
			}

			public ObjectEntry(object instance, string name)
				: base(name)
			{
				if (instance == null)
				{
					throw new ArgumentNullException("instance");
				}
				_instance = instance;
				_type = instance.GetType();
				_entireObject = false;
			}
		}

		private class InstanceRedirectorDesignerSerializationManager : IDesignerSerializationManager, IServiceProvider
		{
			private DesignerSerializationManager _manager;

			private Dictionary<string, string> _nameMap;

			public IList Errors => _manager.Errors;

			ContextStack IDesignerSerializationManager.Context => ((IDesignerSerializationManager)_manager).Context;

			PropertyDescriptorCollection IDesignerSerializationManager.Properties => ((IDesignerSerializationManager)_manager).Properties;

			event EventHandler IDesignerSerializationManager.SerializationComplete
			{
				add
				{
					((IDesignerSerializationManager)_manager).SerializationComplete += value;
				}
				remove
				{
					((IDesignerSerializationManager)_manager).SerializationComplete -= value;
				}
			}

			event ResolveNameEventHandler IDesignerSerializationManager.ResolveName
			{
				add
				{
					((IDesignerSerializationManager)_manager).ResolveName += value;
				}
				remove
				{
					((IDesignerSerializationManager)_manager).ResolveName -= value;
				}
			}

			public InstanceRedirectorDesignerSerializationManager(IServiceProvider provider, IContainer container, bool validateRecycledTypes)
			{
				if (provider == null)
				{
					throw new ArgumentNullException("provider");
				}
				DesignerSerializationManager designerSerializationManager = new DesignerSerializationManager(provider)
				{
					PreserveNames = false
				};
				if (container != null)
				{
					designerSerializationManager.Container = container;
				}
				designerSerializationManager.ValidateRecycledTypes = validateRecycledTypes;
				_manager = designerSerializationManager;
			}

			public IDisposable CreateSession()
			{
				return _manager.CreateSession();
			}

			object IServiceProvider.GetService(Type service)
			{
				return ((IServiceProvider)_manager).GetService(service);
			}

			void IDesignerSerializationManager.AddSerializationProvider(IDesignerSerializationProvider provider)
			{
				((IDesignerSerializationManager)_manager).AddSerializationProvider(provider);
			}

			void IDesignerSerializationManager.RemoveSerializationProvider(IDesignerSerializationProvider provider)
			{
				((IDesignerSerializationManager)_manager).RemoveSerializationProvider(provider);
			}

			object IDesignerSerializationManager.CreateInstance(Type type, ICollection arguments, string name, bool addToContainer)
			{
				object obj = ((IDesignerSerializationManager)_manager).CreateInstance(type, arguments, name, addToContainer);
				string name2 = ((IDesignerSerializationManager)_manager).GetName(obj);
				if (name2 != name)
				{
					if (_nameMap == null)
					{
						_nameMap = new Dictionary<string, string>();
					}
					_nameMap[name] = name2;
				}
				return obj;
			}

			object IDesignerSerializationManager.GetInstance(string name)
			{
				if (_nameMap != null && _nameMap.ContainsKey(name))
				{
					return ((IDesignerSerializationManager)_manager).GetInstance(_nameMap[name]);
				}
				return ((IDesignerSerializationManager)_manager).GetInstance(name);
			}

			Type IDesignerSerializationManager.GetType(string name)
			{
				return ((IDesignerSerializationManager)_manager).GetType(name);
			}

			object IDesignerSerializationManager.GetSerializer(Type type, Type serializerType)
			{
				return ((IDesignerSerializationManager)_manager).GetSerializer(type, serializerType);
			}

			string IDesignerSerializationManager.GetName(object instance)
			{
				return ((IDesignerSerializationManager)_manager).GetName(instance);
			}

			void IDesignerSerializationManager.SetName(object instance, string name)
			{
				((IDesignerSerializationManager)_manager).SetName(instance, name);
			}

			void IDesignerSerializationManager.ReportError(object error)
			{
				((IDesignerSerializationManager)_manager).ReportError(error);
			}
		}

		private bool _closed;

		private Dictionary<string, ObjectEntry> _objects;

		private IServiceProvider _provider;

		private ICollection _errors;

		public override ICollection Errors
		{
			get
			{
				if (_errors == null)
				{
					_errors = new object[0];
				}
				return _errors;
			}
		}

		internal CodeDomSerializationStore()
			: this(null)
		{
		}

		internal CodeDomSerializationStore(IServiceProvider provider)
		{
			_provider = provider;
		}

		private CodeDomSerializationStore(SerializationInfo info, StreamingContext context)
		{
			_objects = (Dictionary<string, ObjectEntry>)info.GetValue("objects", typeof(Dictionary<string, ObjectEntry>));
			_closed = (bool)info.GetValue("closed", typeof(bool));
		}

		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("objects", _objects);
			info.AddValue("closed", _closed);
		}

		public override void Close()
		{
			if (!_closed)
			{
				Serialize(_provider);
				_closed = true;
			}
		}

		internal static CodeDomSerializationStore Load(Stream stream)
		{
			return new BinaryFormatter().Deserialize(stream) as CodeDomSerializationStore;
		}

		public override void Save(Stream stream)
		{
			Close();
			new BinaryFormatter().Serialize(stream, this);
		}

		private void Serialize(IServiceProvider provider)
		{
			if (provider == null || _objects == null)
			{
				return;
			}
			InstanceRedirectorDesignerSerializationManager instanceRedirectorDesignerSerializationManager = new InstanceRedirectorDesignerSerializationManager(provider, null, validateRecycledTypes: false);
			((IDesignerSerializationManager)instanceRedirectorDesignerSerializationManager).AddSerializationProvider((IDesignerSerializationProvider)CodeDomSerializationProvider.Instance);
			IDisposable disposable = instanceRedirectorDesignerSerializationManager.CreateSession();
			foreach (ObjectEntry value in _objects.Values)
			{
				if (value.IsEntireObject)
				{
					CodeDomSerializer codeDomSerializer = (CodeDomSerializer)((IDesignerSerializationManager)instanceRedirectorDesignerSerializationManager).GetSerializer(value.Type, typeof(CodeDomSerializer));
					if (codeDomSerializer != null)
					{
						object obj = null;
						obj = ((!value.Absolute) ? codeDomSerializer.Serialize(instanceRedirectorDesignerSerializationManager, value.Instance) : codeDomSerializer.SerializeAbsolute(instanceRedirectorDesignerSerializationManager, value.Instance));
						value.Serialized = obj;
					}
					continue;
				}
				foreach (MemberEntry value2 in value.Members.Values)
				{
					CodeDomSerializer codeDomSerializer2 = (CodeDomSerializer)((IDesignerSerializationManager)instanceRedirectorDesignerSerializationManager).GetSerializer(value.Type, typeof(CodeDomSerializer));
					if (codeDomSerializer2 != null)
					{
						object obj2 = null;
						obj2 = ((!value2.Absolute) ? codeDomSerializer2.SerializeMember(instanceRedirectorDesignerSerializationManager, value.Instance, value2.Descriptor) : codeDomSerializer2.SerializeMemberAbsolute(instanceRedirectorDesignerSerializationManager, value.Instance, value2.Descriptor));
						value2.Serialized = obj2;
					}
				}
			}
			_errors = instanceRedirectorDesignerSerializationManager.Errors;
			disposable.Dispose();
		}

		internal void AddObject(object instance, bool absolute)
		{
			if (_closed)
			{
				throw new InvalidOperationException("store is closed");
			}
			if (_objects == null)
			{
				_objects = new Dictionary<string, ObjectEntry>();
			}
			string name = GetName(instance);
			if (!_objects.ContainsKey(name))
			{
				ObjectEntry objectEntry = new ObjectEntry(instance, name);
				objectEntry.Absolute = absolute;
				objectEntry.IsEntireObject = true;
				_objects[name] = objectEntry;
			}
		}

		internal void AddMember(object owner, MemberDescriptor member, bool absolute)
		{
			if (_closed)
			{
				throw new InvalidOperationException("store is closed");
			}
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			if (_objects == null)
			{
				_objects = new Dictionary<string, ObjectEntry>();
			}
			string name = GetName(owner);
			if (!_objects.ContainsKey(name))
			{
				_objects.Add(name, new ObjectEntry(owner, name));
			}
			MemberEntry memberEntry = new MemberEntry(member);
			memberEntry.Absolute = absolute;
			_objects[name].Members[member.Name] = memberEntry;
		}

		private string GetName(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			string text = null;
			if (value is IComponent { Site: not null } component)
			{
				if (component.Site is INestedSite)
				{
					return ((INestedSite)component.Site).FullName;
				}
				return (component.Site != null) ? component.Site.Name : null;
			}
			if (value is MemberDescriptor)
			{
				return ((MemberDescriptor)value).Name;
			}
			return value.GetHashCode().ToString();
		}

		internal ICollection Deserialize(IServiceProvider provider, IContainer container, bool validateRecycledTypes, bool applyDefaults)
		{
			List<object> list = new List<object>();
			if (provider == null || _objects == null)
			{
				return list;
			}
			_provider = provider;
			InstanceRedirectorDesignerSerializationManager instanceRedirectorDesignerSerializationManager = new InstanceRedirectorDesignerSerializationManager(provider, container, validateRecycledTypes);
			((IDesignerSerializationManager)instanceRedirectorDesignerSerializationManager).AddSerializationProvider((IDesignerSerializationProvider)CodeDomSerializationProvider.Instance);
			IDisposable disposable = instanceRedirectorDesignerSerializationManager.CreateSession();
			foreach (ObjectEntry value in _objects.Values)
			{
				list.Add(DeserializeEntry(instanceRedirectorDesignerSerializationManager, value));
			}
			_errors = instanceRedirectorDesignerSerializationManager.Errors;
			disposable.Dispose();
			return list;
		}

		private object DeserializeEntry(IDesignerSerializationManager manager, ObjectEntry objectEntry)
		{
			object obj = null;
			if (objectEntry.IsEntireObject)
			{
				CodeDomSerializer codeDomSerializer = (CodeDomSerializer)manager.GetSerializer(objectEntry.Type, typeof(CodeDomSerializer));
				if (codeDomSerializer != null)
				{
					obj = codeDomSerializer.Deserialize(manager, objectEntry.Serialized);
					string name = manager.GetName(obj);
					if (name != objectEntry.Name)
					{
						objectEntry.Name = name;
					}
				}
			}
			else
			{
				foreach (MemberEntry value in objectEntry.Members.Values)
				{
					((CodeDomSerializer)manager.GetSerializer(objectEntry.Type, typeof(CodeDomSerializer)))?.Deserialize(manager, value.Serialized);
				}
			}
			return obj;
		}
	}

	private IServiceProvider _provider;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService" /> class.</summary>
	public CodeDomComponentSerializationService()
		: this(null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService" /> class using the given service provider to resolve services.</summary>
	/// <param name="provider">An <see cref="T:System.IServiceProvider" /> to use for resolving services.</param>
	public CodeDomComponentSerializationService(IServiceProvider provider)
	{
		_provider = provider;
	}

	/// <summary>Creates a new <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</summary>
	/// <returns>A new serialization store.</returns>
	public override SerializationStore CreateStore()
	{
		return new CodeDomSerializationStore(_provider);
	}

	/// <summary>Loads a <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> from the given stream.</summary>
	/// <param name="stream">The stream from which to load the <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</param>
	/// <returns>The loaded <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="stream" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Runtime.Serialization.SerializationException">
	///   <paramref name="stream" /> supports seeking, but its length is 0.</exception>
	public override SerializationStore LoadStore(Stream stream)
	{
		return CodeDomSerializationStore.Load(stream);
	}

	/// <summary>Deserializes the given store to produce a collection of objects.</summary>
	/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> from which objects will be deserialized.</param>
	/// <returns>A collection of deserialized components.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="store" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">
	///   <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
	public override ICollection Deserialize(SerializationStore store)
	{
		return Deserialize(store, null);
	}

	/// <summary>Deserializes the given store and populates the given <see cref="T:System.ComponentModel.IContainer" /> with deserialized <see cref="T:System.ComponentModel.IComponent" /> objects.</summary>
	/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> from which objects will be deserialized.</param>
	/// <param name="container">A container to which <see cref="T:System.ComponentModel.IComponent" /> objects will be added.</param>
	/// <returns>A collection of deserialized components.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="store" /> or <paramref name="container" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">
	///   <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
	public override ICollection Deserialize(SerializationStore store, IContainer container)
	{
		return DeserializeCore(store, container, validateRecycledTypes: true, applyDefaults: true);
	}

	/// <summary>Deserializes the given <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to the given container, optionally applying default property values.</summary>
	/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> from which the objects will be deserialized.</param>
	/// <param name="container">A container of objects to which data will be applied.</param>
	/// <param name="validateRecycledTypes">
	///   <see langword="true" /> to validate the recycled type; otherwise, <see langword="false" />.</param>
	/// <param name="applyDefaults">
	///   <see langword="true" /> to apply default property values; otherwise, <see langword="false" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="store" /> or <paramref name="container" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">
	///   <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
	public override void DeserializeTo(SerializationStore store, IContainer container, bool validateRecycledTypes, bool applyDefaults)
	{
		DeserializeCore(store, container, validateRecycledTypes, applyDefaults);
	}

	private ICollection DeserializeCore(SerializationStore store, IContainer container, bool validateRecycledTypes, bool applyDefaults)
	{
		return ((store as CodeDomSerializationStore) ?? throw new InvalidOperationException("store type unsupported")).Deserialize(_provider, container, validateRecycledTypes, applyDefaults);
	}

	/// <summary>Serializes the given object to the given <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</summary>
	/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to which <paramref name="value" /> will be serialized.</param>
	/// <param name="value">The object to serialize.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="store" /> or <paramref name="value" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">
	///   <paramref name="store" /> is closed, or <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
	public override void Serialize(SerializationStore store, object value)
	{
		SerializeCore(store, value, absolute: false);
	}

	/// <summary>Serializes the given object, accounting for default property values.</summary>
	/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to which <paramref name="value" /> will be serialized.</param>
	/// <param name="value">The object to serialize.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="store" /> or <paramref name="value" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">
	///   <paramref name="store" /> is closed, or <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
	public override void SerializeAbsolute(SerializationStore store, object value)
	{
		SerializeCore(store, value, absolute: true);
	}

	/// <summary>Serializes the given member on the given object.</summary>
	/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to which <paramref name="member" /> will be serialized.</param>
	/// <param name="owningObject">The object that owns the <paramref name="member" />.</param>
	/// <param name="member">The given member.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="store" />, <paramref name="owningObject" />, or <paramref name="member" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">
	///   <paramref name="store" /> is closed, or <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
	public override void SerializeMember(SerializationStore store, object owningObject, MemberDescriptor member)
	{
		SerializeMemberCore(store, owningObject, member, absolute: false);
	}

	/// <summary>Serializes the given member on the given object, but also serializes the member if it contains the default property value.</summary>
	/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to which <paramref name="member" /> will be serialized.</param>
	/// <param name="owningObject">The object that owns the <paramref name="member" />.</param>
	/// <param name="member">The given member.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="store" />, <paramref name="owningObject" />, or <paramref name="member" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">
	///   <paramref name="store" /> is closed, or <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
	public override void SerializeMemberAbsolute(SerializationStore store, object owningObject, MemberDescriptor member)
	{
		SerializeMemberCore(store, owningObject, member, absolute: true);
	}

	private void SerializeCore(SerializationStore store, object value, bool absolute)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (store == null)
		{
			throw new ArgumentNullException("store");
		}
		CodeDomSerializationStore obj = store as CodeDomSerializationStore;
		if (store == null)
		{
			throw new InvalidOperationException("store type unsupported");
		}
		obj.AddObject(value, absolute);
	}

	private void SerializeMemberCore(SerializationStore store, object owningObject, MemberDescriptor member, bool absolute)
	{
		if (member == null)
		{
			throw new ArgumentNullException("member");
		}
		if (owningObject == null)
		{
			throw new ArgumentNullException("owningObject");
		}
		if (store == null)
		{
			throw new ArgumentNullException("store");
		}
		((store as CodeDomSerializationStore) ?? throw new InvalidOperationException("store type unsupported")).AddMember(owningObject, member, absolute);
	}
}
