using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModestTree;
using Zenject.Internal;

namespace Zenject;

[NoReflectionBaking]
public class InjectContext : IDisposable
{
	private BindingId _bindingId;

	private Type _objectType;

	private InjectContext _parentContext;

	private object _objectInstance;

	private string _memberName;

	private bool _optional;

	private InjectSources _sourceType;

	private object _fallBackValue;

	private object _concreteIdentifier;

	private DiContainer _container;

	public BindingId BindingId => _bindingId;

	public Type ObjectType
	{
		get
		{
			return _objectType;
		}
		set
		{
			_objectType = value;
		}
	}

	public InjectContext ParentContext
	{
		get
		{
			return _parentContext;
		}
		set
		{
			_parentContext = value;
		}
	}

	public object ObjectInstance
	{
		get
		{
			return _objectInstance;
		}
		set
		{
			_objectInstance = value;
		}
	}

	public object Identifier
	{
		get
		{
			return _bindingId.Identifier;
		}
		set
		{
			_bindingId.Identifier = value;
		}
	}

	public string MemberName
	{
		get
		{
			return _memberName;
		}
		set
		{
			_memberName = value;
		}
	}

	public Type MemberType
	{
		get
		{
			return _bindingId.Type;
		}
		set
		{
			_bindingId.Type = value;
		}
	}

	public bool Optional
	{
		get
		{
			return _optional;
		}
		set
		{
			_optional = value;
		}
	}

	public InjectSources SourceType
	{
		get
		{
			return _sourceType;
		}
		set
		{
			_sourceType = value;
		}
	}

	public object ConcreteIdentifier
	{
		get
		{
			return _concreteIdentifier;
		}
		set
		{
			_concreteIdentifier = value;
		}
	}

	public object FallBackValue
	{
		get
		{
			return _fallBackValue;
		}
		set
		{
			_fallBackValue = value;
		}
	}

	public DiContainer Container
	{
		get
		{
			return _container;
		}
		set
		{
			_container = value;
		}
	}

	public IEnumerable<InjectContext> ParentContexts
	{
		get
		{
			if (ParentContext == null)
			{
				yield break;
			}
			yield return ParentContext;
			foreach (InjectContext parentContext in ParentContext.ParentContexts)
			{
				yield return parentContext;
			}
		}
	}

	public IEnumerable<InjectContext> ParentContextsAndSelf
	{
		get
		{
			yield return this;
			foreach (InjectContext parentContext in ParentContexts)
			{
				yield return parentContext;
			}
		}
	}

	public IEnumerable<Type> AllObjectTypes
	{
		get
		{
			foreach (InjectContext context in ParentContextsAndSelf)
			{
				if (context.ObjectType != null)
				{
					yield return context.ObjectType;
				}
			}
		}
	}

	public InjectContext()
	{
		_bindingId = default(BindingId);
		Reset();
	}

	public InjectContext(DiContainer container, Type memberType)
		: this()
	{
		Container = container;
		MemberType = memberType;
	}

	public InjectContext(DiContainer container, Type memberType, object identifier)
		: this(container, memberType)
	{
		Identifier = identifier;
	}

	public InjectContext(DiContainer container, Type memberType, object identifier, bool optional)
		: this(container, memberType, identifier)
	{
		Optional = optional;
	}

	public void Dispose()
	{
		ZenPools.DespawnInjectContext(this);
	}

	public void Reset()
	{
		_objectType = null;
		_parentContext = null;
		_objectInstance = null;
		_memberName = string.Empty;
		_optional = false;
		_sourceType = InjectSources.Any;
		_fallBackValue = null;
		_container = null;
		_bindingId.Type = null;
		_bindingId.Identifier = null;
	}

	public InjectContext CreateSubContext(Type memberType)
	{
		return CreateSubContext(memberType, null);
	}

	public InjectContext CreateSubContext(Type memberType, object identifier)
	{
		InjectContext injectContext = new InjectContext();
		injectContext.ParentContext = this;
		injectContext.Identifier = identifier;
		injectContext.MemberType = memberType;
		injectContext.ConcreteIdentifier = null;
		injectContext.MemberName = string.Empty;
		injectContext.FallBackValue = null;
		injectContext.ObjectType = ObjectType;
		injectContext.ObjectInstance = ObjectInstance;
		injectContext.Optional = Optional;
		injectContext.SourceType = SourceType;
		injectContext.Container = Container;
		return injectContext;
	}

	public InjectContext Clone()
	{
		InjectContext injectContext = new InjectContext();
		injectContext.ObjectType = ObjectType;
		injectContext.ParentContext = ParentContext;
		injectContext.ConcreteIdentifier = ConcreteIdentifier;
		injectContext.ObjectInstance = ObjectInstance;
		injectContext.Identifier = Identifier;
		injectContext.MemberType = MemberType;
		injectContext.MemberName = MemberName;
		injectContext.Optional = Optional;
		injectContext.SourceType = SourceType;
		injectContext.FallBackValue = FallBackValue;
		injectContext.Container = Container;
		return injectContext;
	}

	public string GetObjectGraphString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (InjectContext item in ParentContextsAndSelf.Reverse())
		{
			if (!(item.ObjectType == null))
			{
				stringBuilder.AppendLine(TypeStringFormatter.PrettyName(item.ObjectType));
			}
		}
		return stringBuilder.ToString();
	}
}
