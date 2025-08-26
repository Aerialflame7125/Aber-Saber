using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;
using UnityEngine;
using Zenject.Internal;

namespace Zenject;

[NoReflectionBaking]
public class DiContainer : IInstantiator
{
	private class ProviderInfo
	{
		public readonly DiContainer Container;

		public readonly bool NonLazy;

		public readonly IProvider Provider;

		public readonly BindingCondition Condition;

		public ProviderInfo(IProvider provider, BindingCondition condition, bool nonLazy, DiContainer container)
		{
			Provider = provider;
			Condition = condition;
			NonLazy = nonLazy;
			Container = container;
		}

		private static object __zenCreate(object[] P_0)
		{
			return new ProviderInfo((IProvider)P_0[0], (BindingCondition)P_0[1], (bool)P_0[2], (DiContainer)P_0[3]);
		}

		[Zenject.Internal.Preserve]
		private static InjectTypeInfo __zenCreateInjectTypeInfo()
		{
			return new InjectTypeInfo(typeof(ProviderInfo), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[4]
			{
				new InjectableInfo(optional: false, null, "provider", typeof(IProvider), null, InjectSources.Any),
				new InjectableInfo(optional: false, null, "condition", typeof(BindingCondition), null, InjectSources.Any),
				new InjectableInfo(optional: false, null, "nonLazy", typeof(bool), null, InjectSources.Any),
				new InjectableInfo(optional: false, null, "container", typeof(DiContainer), null, InjectSources.Any)
			}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
		}
	}

	private readonly Dictionary<Type, IDecoratorProvider> _decorators = new Dictionary<Type, IDecoratorProvider>();

	private readonly Dictionary<BindingId, List<ProviderInfo>> _providers = new Dictionary<BindingId, List<ProviderInfo>>();

	private readonly DiContainer[][] _containerLookups = new DiContainer[4][];

	private readonly HashSet<LookupId> _resolvesInProgress = new HashSet<LookupId>();

	private readonly HashSet<LookupId> _resolvesTwiceInProgress = new HashSet<LookupId>();

	private readonly LazyInstanceInjector _lazyInjector;

	private readonly SingletonMarkRegistry _singletonMarkRegistry = new SingletonMarkRegistry();

	private readonly Queue<BindStatement> _currentBindings = new Queue<BindStatement>();

	private readonly List<BindStatement> _childBindings = new List<BindStatement>();

	private readonly HashSet<Type> _validatedTypes = new HashSet<Type>();

	private readonly List<IValidatable> _validationQueue = new List<IValidatable>();

	private Transform _contextTransform;

	private bool _hasLookedUpContextTransform;

	private Transform _inheritedDefaultParent;

	private Transform _explicitDefaultParent;

	private bool _hasExplicitDefaultParent;

	private ZenjectSettings _settings;

	private bool _hasResolvedRoots;

	private bool _isFinalizingBinding;

	private bool _isValidating;

	private bool _isInstalling;

	public ZenjectSettings Settings
	{
		get
		{
			return _settings;
		}
		set
		{
			_settings = value;
			Rebind<ZenjectSettings>().FromInstance(value);
		}
	}

	internal SingletonMarkRegistry SingletonMarkRegistry => _singletonMarkRegistry;

	public IEnumerable<IProvider> AllProviders => (from x in _providers.Values.SelectMany((List<ProviderInfo> x) => x)
		select x.Provider).Distinct();

	private Transform ContextTransform
	{
		get
		{
			if (!_hasLookedUpContextTransform)
			{
				_hasLookedUpContextTransform = true;
				Context context = TryResolve<Context>();
				if (context != null)
				{
					_contextTransform = context.transform;
				}
			}
			return _contextTransform;
		}
	}

	public bool AssertOnNewGameObjects { get; set; }

	public Transform InheritedDefaultParent => _inheritedDefaultParent;

	public Transform DefaultParent
	{
		get
		{
			return _explicitDefaultParent;
		}
		set
		{
			_explicitDefaultParent = value;
			_hasExplicitDefaultParent = true;
		}
	}

	public DiContainer[] ParentContainers => _containerLookups[2];

	public DiContainer[] AncestorContainers => _containerLookups[3];

	public bool ChecksForCircularDependencies => true;

	public bool IsValidating => _isValidating;

	public bool IsInstalling
	{
		get
		{
			return _isInstalling;
		}
		set
		{
			_isInstalling = value;
		}
	}

	public IEnumerable<BindingId> AllContracts
	{
		get
		{
			FlushBindings();
			return _providers.Keys;
		}
	}

	public DiContainer(IEnumerable<DiContainer> parentContainersEnumerable, bool isValidating)
	{
		_isValidating = isValidating;
		_lazyInjector = new LazyInstanceInjector(this);
		InstallDefaultBindings();
		FlushBindings();
		Assert.That(_currentBindings.Count == 0);
		_settings = ZenjectSettings.Default;
		DiContainer[] array = new DiContainer[1] { this };
		_containerLookups[1] = array;
		DiContainer[] array2 = parentContainersEnumerable.ToArray();
		_containerLookups[2] = array2;
		DiContainer[] array3 = FlattenInheritanceChain().ToArray();
		_containerLookups[3] = array3;
		_containerLookups[0] = array.Concat(array3).ToArray();
		if (!LinqExtensions.IsEmpty(array2))
		{
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].FlushBindings();
			}
			_inheritedDefaultParent = array2.First().DefaultParent;
			foreach (DiContainer item in array3.Distinct())
			{
				foreach (BindStatement childBinding in item._childBindings)
				{
					if (ShouldInheritBinding(childBinding, item))
					{
						FinalizeBinding(childBinding);
					}
				}
			}
			Assert.That(_currentBindings.Count == 0);
			Assert.That(_childBindings.Count == 0);
		}
		ZenjectSettings zenjectSettings = TryResolve<ZenjectSettings>();
		if (zenjectSettings != null)
		{
			_settings = zenjectSettings;
		}
	}

	public DiContainer(bool isValidating)
		: this(Enumerable.Empty<DiContainer>(), isValidating)
	{
	}

	public DiContainer()
		: this(Enumerable.Empty<DiContainer>(), isValidating: false)
	{
	}

	public DiContainer(DiContainer parentContainer, bool isValidating)
		: this(new DiContainer[1] { parentContainer }, isValidating)
	{
	}

	public DiContainer(DiContainer parentContainer)
		: this(new DiContainer[1] { parentContainer }, isValidating: false)
	{
	}

	public DiContainer(IEnumerable<DiContainer> parentContainers)
		: this(parentContainers, isValidating: false)
	{
	}

	private void InstallDefaultBindings()
	{
		Bind(typeof(DiContainer), typeof(IInstantiator)).FromInstance(this);
		Bind(typeof(LazyInject<>)).FromMethodUntyped(CreateLazyBinding).Lazy();
	}

	private object CreateLazyBinding(InjectContext context)
	{
		InjectContext injectContext = context.Clone();
		injectContext.MemberType = TypeExtensions.GenericArguments(context.MemberType).Single();
		object obj = Activator.CreateInstance(typeof(LazyInject<>).MakeGenericType(injectContext.MemberType), this, injectContext);
		if (_isValidating)
		{
			QueueForValidate((IValidatable)obj);
		}
		return obj;
	}

	public void QueueForValidate(IValidatable validatable)
	{
		if (!_hasResolvedRoots)
		{
			Type type = validatable.GetType();
			if (!_validatedTypes.Contains(type))
			{
				_validatedTypes.Add(type);
				_validationQueue.Add(validatable);
			}
		}
	}

	private bool ShouldInheritBinding(BindStatement binding, DiContainer ancestorContainer)
	{
		if (binding.BindingInheritanceMethod == BindingInheritanceMethods.CopyIntoAll || binding.BindingInheritanceMethod == BindingInheritanceMethods.MoveIntoAll)
		{
			return true;
		}
		if ((binding.BindingInheritanceMethod == BindingInheritanceMethods.CopyDirectOnly || binding.BindingInheritanceMethod == BindingInheritanceMethods.MoveDirectOnly) && ParentContainers.Contains(ancestorContainer))
		{
			return true;
		}
		return false;
	}

	public void ResolveRoots()
	{
		Assert.That(!_hasResolvedRoots);
		FlushBindings();
		ResolveDependencyRoots();
		_lazyInjector.LazyInjectAll();
		if (IsValidating)
		{
			FlushValidationQueue();
		}
		Assert.That(!_hasResolvedRoots);
		_hasResolvedRoots = true;
	}

	private void ResolveDependencyRoots()
	{
		List<BindingId> list = new List<BindingId>();
		List<ProviderInfo> list2 = new List<ProviderInfo>();
		foreach (KeyValuePair<BindingId, List<ProviderInfo>> provider in _providers)
		{
			foreach (ProviderInfo item in provider.Value)
			{
				if (item.NonLazy)
				{
					list.Add(provider.Key);
					list2.Add(item);
				}
			}
		}
		Assert.IsEqual(list2.Count, list.Count);
		List<object> list3 = ZenPools.SpawnList<object>();
		try
		{
			for (int i = 0; i < list2.Count; i++)
			{
				BindingId bindingId = list[i];
				ProviderInfo providerInfo = list2[i];
				using InjectContext injectContext = ZenPools.SpawnInjectContext(this, bindingId.Type);
				injectContext.Identifier = bindingId.Identifier;
				injectContext.SourceType = InjectSources.Local;
				injectContext.Optional = false;
				list3.Clear();
				SafeGetInstances(providerInfo, injectContext, list3);
			}
		}
		finally
		{
			ZenPools.DespawnList(list3);
		}
	}

	private void ValidateFullResolve()
	{
		Assert.That(!_hasResolvedRoots);
		Assert.That(IsValidating);
		foreach (BindingId item in _providers.Keys.ToList())
		{
			if (!TypeExtensions.IsOpenGenericType(item.Type))
			{
				using InjectContext injectContext = ZenPools.SpawnInjectContext(this, item.Type);
				injectContext.Identifier = item.Identifier;
				injectContext.SourceType = InjectSources.Local;
				injectContext.Optional = true;
				ResolveAll(injectContext);
			}
		}
	}

	private void FlushValidationQueue()
	{
		Assert.That(!_hasResolvedRoots);
		Assert.That(IsValidating);
		Assert.That(Application.isEditor);
		List<IValidatable> list = new List<IValidatable>();
		while (_validationQueue.Any())
		{
			list.Clear();
			MiscExtensions.AllocFreeAddRange(list, _validationQueue);
			_validationQueue.Clear();
			for (int i = 0; i < list.Count; i++)
			{
				list[i].Validate();
			}
		}
	}

	public DiContainer CreateSubContainer()
	{
		return CreateSubContainer(_isValidating);
	}

	public void QueueForInject(object instance)
	{
		_lazyInjector.AddInstance(instance);
	}

	public T LazyInject<T>(T instance)
	{
		_lazyInjector.LazyInject(instance);
		return instance;
	}

	private DiContainer CreateSubContainer(bool isValidating)
	{
		return new DiContainer(new DiContainer[1] { this }, isValidating);
	}

	public void RegisterProvider(BindingId bindingId, BindingCondition condition, IProvider provider, bool nonLazy)
	{
		ProviderInfo item = new ProviderInfo(provider, condition, nonLazy, this);
		if (!_providers.TryGetValue(bindingId, out var value))
		{
			value = new List<ProviderInfo>();
			_providers.Add(bindingId, value);
		}
		value.Add(item);
	}

	private void GetProviderMatches(InjectContext context, List<ProviderInfo> buffer)
	{
		Assert.IsNotNull(context);
		Assert.That(buffer.Count == 0);
		List<ProviderInfo> list = ZenPools.SpawnList<ProviderInfo>();
		try
		{
			GetProvidersForContract(context.BindingId, context.SourceType, list);
			for (int i = 0; i < list.Count; i++)
			{
				ProviderInfo providerInfo = list[i];
				if (providerInfo.Condition == null || providerInfo.Condition(context))
				{
					buffer.Add(providerInfo);
				}
			}
		}
		finally
		{
			ZenPools.DespawnList(list);
		}
	}

	private ProviderInfo TryGetUniqueProvider(InjectContext context)
	{
		Assert.IsNotNull(context);
		BindingId bindingId = context.BindingId;
		InjectSources sourceType = context.SourceType;
		DiContainer[] array = _containerLookups[(int)sourceType];
		for (int i = 0; i < array.Length; i++)
		{
			array[i].FlushBindings();
		}
		List<ProviderInfo> list = ZenPools.SpawnList<ProviderInfo>();
		try
		{
			ProviderInfo providerInfo = null;
			int num = int.MaxValue;
			bool flag = false;
			bool flag2 = false;
			foreach (DiContainer diContainer in array)
			{
				int containerHeirarchyDistance = GetContainerHeirarchyDistance(diContainer);
				if (containerHeirarchyDistance > num)
				{
					continue;
				}
				list.Clear();
				diContainer.GetLocalProviders(bindingId, list);
				for (int k = 0; k < list.Count; k++)
				{
					ProviderInfo providerInfo2 = list[k];
					bool flag3 = providerInfo2.Condition != null;
					if (flag3 && !providerInfo2.Condition(context))
					{
						continue;
					}
					Assert.That(providerInfo == null || num == containerHeirarchyDistance);
					if (flag3)
					{
						flag2 = (flag ? true : false);
					}
					else
					{
						if (flag)
						{
							continue;
						}
						if (providerInfo != null)
						{
							flag2 = true;
						}
					}
					if (!flag2)
					{
						num = containerHeirarchyDistance;
						flag = flag3;
						providerInfo = providerInfo2;
					}
				}
			}
			if (flag2)
			{
				throw Assert.CreateException("Found multiple matches when only one was expected for type '{0}'{1}. Object graph:\n {2}", context.MemberType, (!(context.ObjectType == null)) ? MiscExtensions.Fmt(" while building object with type '{0}'", context.ObjectType) : string.Empty, context.GetObjectGraphString());
			}
			return providerInfo;
		}
		finally
		{
			ZenPools.DespawnList(list);
		}
	}

	private List<DiContainer> FlattenInheritanceChain()
	{
		List<DiContainer> list = new List<DiContainer>();
		Queue<DiContainer> queue = new Queue<DiContainer>();
		queue.Enqueue(this);
		while (queue.Count > 0)
		{
			DiContainer diContainer = queue.Dequeue();
			DiContainer[] parentContainers = diContainer.ParentContainers;
			foreach (DiContainer item in parentContainers)
			{
				if (!list.Contains(item))
				{
					list.Add(item);
					queue.Enqueue(item);
				}
			}
		}
		return list;
	}

	private void GetLocalProviders(BindingId bindingId, List<ProviderInfo> buffer)
	{
		if (_providers.TryGetValue(bindingId, out var value))
		{
			MiscExtensions.AllocFreeAddRange(buffer, value);
		}
		else if (TypeExtensions.IsGenericType(bindingId.Type) && _providers.TryGetValue(new BindingId(bindingId.Type.GetGenericTypeDefinition(), bindingId.Identifier), out value))
		{
			MiscExtensions.AllocFreeAddRange(buffer, value);
		}
	}

	private void GetProvidersForContract(BindingId bindingId, InjectSources sourceType, List<ProviderInfo> buffer)
	{
		DiContainer[] array = _containerLookups[(int)sourceType];
		for (int i = 0; i < array.Length; i++)
		{
			array[i].FlushBindings();
		}
		for (int j = 0; j < array.Length; j++)
		{
			array[j].GetLocalProviders(bindingId, buffer);
		}
	}

	public void Install<TInstaller>() where TInstaller : Installer
	{
		TInstaller val = Instantiate<TInstaller>();
		val.InstallBindings();
	}

	public void Install<TInstaller>(object[] extraArgs) where TInstaller : Installer
	{
		TInstaller val = Instantiate<TInstaller>(extraArgs);
		val.InstallBindings();
	}

	public IList ResolveAll(InjectContext context)
	{
		List<object> list = ZenPools.SpawnList<object>();
		try
		{
			ResolveAll(context, list);
			return ReflectionUtil.CreateGenericList(context.MemberType, list);
		}
		finally
		{
			ZenPools.DespawnList(list);
		}
	}

	public void ResolveAll(InjectContext context, List<object> buffer)
	{
		Assert.IsNotNull(context);
		FlushBindings();
		CheckForInstallWarning(context);
		List<ProviderInfo> list = ZenPools.SpawnList<ProviderInfo>();
		try
		{
			GetProviderMatches(context, list);
			if (list.Count == 0)
			{
				if (!context.Optional)
				{
					throw Assert.CreateException("Could not find required dependency with type '{0}' Object graph:\n {1}", context.MemberType, context.GetObjectGraphString());
				}
				return;
			}
			List<object> list2 = ZenPools.SpawnList<object>();
			List<object> list3 = ZenPools.SpawnList<object>();
			try
			{
				for (int i = 0; i < list.Count; i++)
				{
					ProviderInfo providerInfo = list[i];
					list2.Clear();
					SafeGetInstances(providerInfo, context, list2);
					for (int j = 0; j < list2.Count; j++)
					{
						list3.Add(list2[j]);
					}
				}
				if (list3.Count == 0 && !context.Optional)
				{
					throw Assert.CreateException("Could not find required dependency with type '{0}'.  Found providers but they returned zero results!", context.MemberType);
				}
				if (IsValidating)
				{
					for (int k = 0; k < list3.Count; k++)
					{
						object obj = list3[k];
						if (obj is ValidationMarker)
						{
							list3[k] = TypeExtensions.GetDefaultValue(context.MemberType);
						}
					}
				}
				MiscExtensions.AllocFreeAddRange(buffer, list3);
			}
			finally
			{
				ZenPools.DespawnList(list2);
				ZenPools.DespawnList(list3);
			}
		}
		finally
		{
			ZenPools.DespawnList(list);
		}
	}

	private void CheckForInstallWarning(InjectContext context)
	{
		if (_settings.DisplayWarningWhenResolvingDuringInstall)
		{
			Assert.IsNotNull(context);
		}
	}

	public Type ResolveType<T>()
	{
		return ResolveType(typeof(T));
	}

	public Type ResolveType(Type type)
	{
		using InjectContext context = ZenPools.SpawnInjectContext(this, type);
		return ResolveType(context);
	}

	public Type ResolveType(InjectContext context)
	{
		Assert.IsNotNull(context);
		FlushBindings();
		ProviderInfo providerInfo = TryGetUniqueProvider(context);
		if (providerInfo == null)
		{
			throw Assert.CreateException("Unable to resolve {0}{1}. Object graph:\n{2}", context.BindingId, (!(context.ObjectType == null)) ? MiscExtensions.Fmt(" while building object with type '{0}'", context.ObjectType) : string.Empty, context.GetObjectGraphString());
		}
		return providerInfo.Provider.GetInstanceType(context);
	}

	public List<Type> ResolveTypeAll(Type type)
	{
		return ResolveTypeAll(type, null);
	}

	public List<Type> ResolveTypeAll(Type type, object identifier)
	{
		using InjectContext injectContext = ZenPools.SpawnInjectContext(this, type);
		injectContext.Identifier = identifier;
		return ResolveTypeAll(injectContext);
	}

	public List<Type> ResolveTypeAll(InjectContext context)
	{
		Assert.IsNotNull(context);
		FlushBindings();
		List<ProviderInfo> list = ZenPools.SpawnList<ProviderInfo>();
		try
		{
			GetProviderMatches(context, list);
			if (list.Count > 0)
			{
				return (from x in list
					select x.Provider.GetInstanceType(context) into x
					where x != null
					select x).ToList();
			}
			return new List<Type>();
		}
		finally
		{
			ZenPools.DespawnList(list);
		}
	}

	public object Resolve(BindingId id)
	{
		using InjectContext injectContext = ZenPools.SpawnInjectContext(this, id.Type);
		injectContext.Identifier = id.Identifier;
		return Resolve(injectContext);
	}

	public object Resolve(InjectContext context)
	{
		Assert.IsNotNull(context);
		Type memberType = context.MemberType;
		FlushBindings();
		CheckForInstallWarning(context);
		InjectContext injectContext = context;
		if (TypeExtensions.IsGenericType(memberType) && memberType.GetGenericTypeDefinition() == typeof(LazyInject<>))
		{
			injectContext = context.Clone();
			injectContext.Identifier = null;
			injectContext.SourceType = InjectSources.Local;
			injectContext.Optional = false;
		}
		ProviderInfo providerInfo = TryGetUniqueProvider(injectContext);
		if (providerInfo == null)
		{
			if (memberType.IsArray && memberType.GetArrayRank() == 1)
			{
				Type elementType = memberType.GetElementType();
				InjectContext injectContext2 = context.Clone();
				injectContext2.MemberType = elementType;
				injectContext2.Optional = true;
				List<object> list = ZenPools.SpawnList<object>();
				try
				{
					ResolveAll(injectContext2, list);
					return ReflectionUtil.CreateArray(injectContext2.MemberType, list);
				}
				finally
				{
					ZenPools.DespawnList(list);
				}
			}
			if (TypeExtensions.IsGenericType(memberType) && (memberType.GetGenericTypeDefinition() == typeof(List<>) || memberType.GetGenericTypeDefinition() == typeof(IList<>) || memberType.GetGenericTypeDefinition() == typeof(IReadOnlyList<>) || memberType.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
			{
				Type memberType2 = TypeExtensions.GenericArguments(memberType).Single();
				InjectContext injectContext3 = context.Clone();
				injectContext3.MemberType = memberType2;
				injectContext3.Optional = true;
				return ResolveAll(injectContext3);
			}
			if (context.Optional)
			{
				return context.FallBackValue;
			}
			throw Assert.CreateException("Unable to resolve '{0}'{1}. Object graph:\n{2}", context.BindingId, (!(context.ObjectType == null)) ? MiscExtensions.Fmt(" while building object with type '{0}'", context.ObjectType) : string.Empty, context.GetObjectGraphString());
		}
		List<object> list2 = ZenPools.SpawnList<object>();
		try
		{
			SafeGetInstances(providerInfo, context, list2);
			if (list2.Count == 0)
			{
				if (context.Optional)
				{
					return context.FallBackValue;
				}
				throw Assert.CreateException("Unable to resolve '{0}'{1}. Object graph:\n{2}", context.BindingId, (!(context.ObjectType == null)) ? MiscExtensions.Fmt(" while building object with type '{0}'", context.ObjectType) : string.Empty, context.GetObjectGraphString());
			}
			if (list2.Count() > 1)
			{
				throw Assert.CreateException("Provider returned multiple instances when only one was expected!  While resolving '{0}'{1}. Object graph:\n{2}", context.BindingId, (!(context.ObjectType == null)) ? MiscExtensions.Fmt(" while building object with type '{0}'", context.ObjectType) : string.Empty, context.GetObjectGraphString());
			}
			return list2.First();
		}
		finally
		{
			ZenPools.DespawnList(list2);
		}
	}

	private void SafeGetInstances(ProviderInfo providerInfo, InjectContext context, List<object> instances)
	{
		Assert.IsNotNull(context);
		IProvider provider = providerInfo.Provider;
		if (ChecksForCircularDependencies)
		{
			LookupId lookupId = ZenPools.SpawnLookupId(provider, context.BindingId);
			try
			{
				DiContainer container = providerInfo.Container;
				if (container._resolvesTwiceInProgress.Contains(lookupId))
				{
					throw Assert.CreateException("Circular dependency detected! Object graph:\n {0}", context.GetObjectGraphString());
				}
				bool flag = false;
				if (!container._resolvesInProgress.Add(lookupId))
				{
					bool condition = container._resolvesTwiceInProgress.Add(lookupId);
					Assert.That(condition);
					flag = true;
				}
				try
				{
					GetDecoratedInstances(provider, context, instances);
					return;
				}
				finally
				{
					if (flag)
					{
						bool condition2 = container._resolvesTwiceInProgress.Remove(lookupId);
						Assert.That(condition2);
					}
					else
					{
						bool condition3 = container._resolvesInProgress.Remove(lookupId);
						Assert.That(condition3);
					}
				}
			}
			finally
			{
				ZenPools.DespawnLookupId(lookupId);
			}
		}
		GetDecoratedInstances(provider, context, instances);
	}

	public DecoratorToChoiceFromBinder<TContract> Decorate<TContract>()
	{
		BindStatement bindStatement = StartBinding();
		BindInfo bindInfo = bindStatement.SpawnBindInfo();
		bindInfo.ContractTypes.Add(typeof(IFactory<TContract, TContract>));
		FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(PlaceholderFactory<TContract, TContract>));
		bindStatement.SetFinalizer(new PlaceholderFactoryBindingFinalizer<TContract>(bindInfo, factoryBindInfo));
		Guid guid = Guid.NewGuid();
		bindInfo.Identifier = guid;
		if (!_decorators.TryGetValue(typeof(TContract), out var value))
		{
			value = new DecoratorProvider<TContract>(this);
			_decorators.Add(typeof(TContract), value);
		}
		((DecoratorProvider<TContract>)value).AddFactoryId(guid);
		return new DecoratorToChoiceFromBinder<TContract>(this, bindInfo, factoryBindInfo);
	}

	private void GetDecoratedInstances(IProvider provider, InjectContext context, List<object> buffer)
	{
		IDecoratorProvider decoratorProvider = TryGetDecoratorProvider(context.BindingId.Type);
		if (decoratorProvider != null)
		{
			decoratorProvider.GetAllInstances(provider, context, buffer);
		}
		else
		{
			IProviderExtensions.GetAllInstances(provider, context, buffer);
		}
	}

	private IDecoratorProvider TryGetDecoratorProvider(Type contractType)
	{
		if (_decorators.TryGetValue(contractType, out var value))
		{
			return value;
		}
		DiContainer[] ancestorContainers = AncestorContainers;
		for (int i = 0; i < ancestorContainers.Length; i++)
		{
			if (ancestorContainers[i]._decorators.TryGetValue(contractType, out value))
			{
				return value;
			}
		}
		return null;
	}

	private int GetContainerHeirarchyDistance(DiContainer container)
	{
		return GetContainerHeirarchyDistance(container, 0).Value;
	}

	private int? GetContainerHeirarchyDistance(DiContainer container, int depth)
	{
		if (container == this)
		{
			return depth;
		}
		int? result = null;
		DiContainer[] parentContainers = ParentContainers;
		foreach (DiContainer diContainer in parentContainers)
		{
			int? containerHeirarchyDistance = diContainer.GetContainerHeirarchyDistance(container, depth + 1);
			if (containerHeirarchyDistance.HasValue && (!result.HasValue || containerHeirarchyDistance.Value < result.Value))
			{
				result = containerHeirarchyDistance;
			}
		}
		return result;
	}

	public IEnumerable<Type> GetDependencyContracts<TContract>()
	{
		return GetDependencyContracts(typeof(TContract));
	}

	public IEnumerable<Type> GetDependencyContracts(Type contract)
	{
		FlushBindings();
		InjectTypeInfo info = TypeAnalyzer.TryGetInfo(contract);
		if (info == null)
		{
			yield break;
		}
		foreach (InjectableInfo injectMember in info.AllInjectables)
		{
			yield return injectMember.MemberType;
		}
	}

	private object InstantiateInternal(Type concreteType, bool autoInject, List<TypeValuePair> extraArgs, InjectContext context, object concreteIdentifier)
	{
		Assert.That(!TypeExtensions.DerivesFrom<Component>(concreteType), "Error occurred while instantiating object of type '{0}'. Instantiator should not be used to create new mono behaviours.  Must use InstantiatePrefabForComponent, InstantiatePrefab, or InstantiateComponent.", concreteType);
		Assert.That(!TypeExtensions.IsAbstract(concreteType), "Expected type '{0}' to be non-abstract", concreteType);
		FlushBindings();
		CheckForInstallWarning(context);
		InjectTypeInfo injectTypeInfo = TypeAnalyzer.TryGetInfo(concreteType);
		Assert.IsNotNull(injectTypeInfo, "Tried to create type '{0}' but could not find type information", concreteType);
		bool flag = TypeAnalyzer.ShouldAllowDuringValidation(concreteType);
		object obj;
		if (TypeExtensions.DerivesFrom<ScriptableObject>(concreteType))
		{
			Assert.That(injectTypeInfo.InjectConstructor.Parameters.Length == 0, "Found constructor parameters on ScriptableObject type '{0}'.  This is not allowed.  Use an [Inject] method or fields instead.");
			obj = ((IsValidating && !flag) ? ((object)new ValidationMarker(concreteType)) : ((object)ScriptableObject.CreateInstance(concreteType)));
		}
		else
		{
			Assert.IsNotNull(injectTypeInfo.InjectConstructor.Factory, "More than one (or zero) constructors found for type '{0}' when creating dependencies.  Use one [Inject] attribute to specify which to use.", concreteType);
			object[] array = ZenPools.SpawnArray<object>(injectTypeInfo.InjectConstructor.Parameters.Length);
			try
			{
				for (int i = 0; i < injectTypeInfo.InjectConstructor.Parameters.Length; i++)
				{
					InjectableInfo injectableInfo = injectTypeInfo.InjectConstructor.Parameters[i];
					if (!InjectUtil.PopValueWithType(extraArgs, injectableInfo.MemberType, out var value))
					{
						using InjectContext context2 = ZenPools.SpawnInjectContext(this, injectableInfo, context, null, concreteType, concreteIdentifier);
						value = Resolve(context2);
					}
					if (value == null || value is ValidationMarker)
					{
						array[i] = TypeExtensions.GetDefaultValue(injectableInfo.MemberType);
					}
					else
					{
						array[i] = value;
					}
				}
				if (!IsValidating || flag)
				{
					try
					{
						obj = injectTypeInfo.InjectConstructor.Factory(array);
					}
					catch (Exception innerException)
					{
						throw Assert.CreateException(innerException, "Error occurred while instantiating object with type '{0}'", concreteType);
					}
				}
				else
				{
					obj = new ValidationMarker(concreteType);
				}
			}
			finally
			{
				ZenPools.DespawnArray(array);
			}
		}
		if (autoInject)
		{
			InjectExplicit(obj, concreteType, extraArgs, context, concreteIdentifier);
			if (extraArgs.Count > 0)
			{
				throw Assert.CreateException("Passed unnecessary parameters when injecting into type '{0}'. \nExtra Parameters: {1}\nObject graph:\n{2}", obj.GetType(), string.Join(",", extraArgs.Select((TypeValuePair x) => TypeStringFormatter.PrettyName(x.Type)).ToArray()), context.GetObjectGraphString());
			}
		}
		return obj;
	}

	public void InjectExplicit(object injectable, List<TypeValuePair> extraArgs)
	{
		Type type = ((!(injectable is ValidationMarker)) ? injectable.GetType() : ((ValidationMarker)injectable).MarkedType);
		InjectExplicit(injectable, type, extraArgs, new InjectContext(this, type, null), null);
	}

	public void InjectExplicit(object injectable, Type injectableType, List<TypeValuePair> extraArgs, InjectContext context, object concreteIdentifier)
	{
		if (IsValidating)
		{
			if (injectable is ValidationMarker { InstantiateFailed: not false })
			{
				return;
			}
			if (_settings.ValidationErrorResponse != ValidationErrorResponses.Throw)
			{
				try
				{
					InjectExplicitInternal(injectable, injectableType, extraArgs, context, concreteIdentifier);
					return;
				}
				catch (Exception e)
				{
					Log.ErrorException(e);
					return;
				}
			}
			InjectExplicitInternal(injectable, injectableType, extraArgs, context, concreteIdentifier);
		}
		else
		{
			InjectExplicitInternal(injectable, injectableType, extraArgs, context, concreteIdentifier);
		}
	}

	private void CallInjectMethodsTopDown(object injectable, Type injectableType, InjectTypeInfo typeInfo, List<TypeValuePair> extraArgs, InjectContext context, object concreteIdentifier, bool isDryRun)
	{
		if (typeInfo.BaseTypeInfo != null)
		{
			CallInjectMethodsTopDown(injectable, injectableType, typeInfo.BaseTypeInfo, extraArgs, context, concreteIdentifier, isDryRun);
		}
		for (int i = 0; i < typeInfo.InjectMethods.Length; i++)
		{
			InjectTypeInfo.InjectMethodInfo injectMethodInfo = typeInfo.InjectMethods[i];
			object[] array = ZenPools.SpawnArray<object>(injectMethodInfo.Parameters.Length);
			try
			{
				for (int j = 0; j < injectMethodInfo.Parameters.Length; j++)
				{
					InjectableInfo injectableInfo = injectMethodInfo.Parameters[j];
					if (!InjectUtil.PopValueWithType(extraArgs, injectableInfo.MemberType, out var value))
					{
						using InjectContext context2 = ZenPools.SpawnInjectContext(this, injectableInfo, context, injectable, injectableType, concreteIdentifier);
						value = Resolve(context2);
					}
					if (value is ValidationMarker)
					{
						Assert.That(IsValidating);
						array[j] = TypeExtensions.GetDefaultValue(injectableInfo.MemberType);
					}
					else
					{
						array[j] = value;
					}
				}
				if (!isDryRun)
				{
					injectMethodInfo.Action(injectable, array);
				}
			}
			finally
			{
				ZenPools.DespawnArray(array);
			}
		}
	}

	private void InjectMembersTopDown(object injectable, Type injectableType, InjectTypeInfo typeInfo, List<TypeValuePair> extraArgs, InjectContext context, object concreteIdentifier, bool isDryRun)
	{
		if (typeInfo.BaseTypeInfo != null)
		{
			InjectMembersTopDown(injectable, injectableType, typeInfo.BaseTypeInfo, extraArgs, context, concreteIdentifier, isDryRun);
		}
		for (int i = 0; i < typeInfo.InjectMembers.Length; i++)
		{
			InjectableInfo info = typeInfo.InjectMembers[i].Info;
			ZenMemberSetterMethod setter = typeInfo.InjectMembers[i].Setter;
			if (InjectUtil.PopValueWithType(extraArgs, info.MemberType, out var value))
			{
				if (!isDryRun)
				{
					if (value is ValidationMarker)
					{
						Assert.That(IsValidating);
					}
					else
					{
						setter(injectable, value);
					}
				}
				continue;
			}
			using (InjectContext context2 = ZenPools.SpawnInjectContext(this, info, context, injectable, injectableType, concreteIdentifier))
			{
				value = Resolve(context2);
			}
			if ((!info.Optional || value != null) && !isDryRun)
			{
				if (value is ValidationMarker)
				{
					Assert.That(IsValidating);
				}
				else
				{
					setter(injectable, value);
				}
			}
		}
	}

	private void InjectExplicitInternal(object injectable, Type injectableType, List<TypeValuePair> extraArgs, InjectContext context, object concreteIdentifier)
	{
		Assert.That(injectable != null);
		InjectTypeInfo injectTypeInfo = TypeAnalyzer.TryGetInfo(injectableType);
		if (injectTypeInfo == null)
		{
			Assert.That(LinqExtensions.IsEmpty(extraArgs));
			return;
		}
		bool flag = TypeAnalyzer.ShouldAllowDuringValidation(injectableType);
		bool flag2 = IsValidating && !flag;
		if (!flag2)
		{
			Assert.IsEqual(injectable.GetType(), injectableType);
		}
		if (injectableType == typeof(GameObject))
		{
			Assert.CreateException("Use InjectGameObject to Inject game objects instead of Inject method. Object graph: {0}", context.GetObjectGraphString());
		}
		FlushBindings();
		CheckForInstallWarning(context);
		InjectMembersTopDown(injectable, injectableType, injectTypeInfo, extraArgs, context, concreteIdentifier, flag2);
		CallInjectMethodsTopDown(injectable, injectableType, injectTypeInfo, extraArgs, context, concreteIdentifier, flag2);
		if (extraArgs.Count <= 0)
		{
			return;
		}
		throw Assert.CreateException("Passed unnecessary parameters when injecting into type '{0}'. \nExtra Parameters: {1}\nObject graph:\n{2}", injectableType, string.Join(",", extraArgs.Select((TypeValuePair x) => TypeStringFormatter.PrettyName(x.Type)).ToArray()), context.GetObjectGraphString());
	}

	internal GameObject CreateAndParentPrefabResource(string resourcePath, GameObjectCreationParameters gameObjectBindInfo, InjectContext context, out bool shouldMakeActive)
	{
		GameObject gameObject = (GameObject)Resources.Load(resourcePath);
		Assert.IsNotNull(gameObject, MiscExtensions.Fmt("Could not find prefab at resource location '{0}'", resourcePath));
		return CreateAndParentPrefab(gameObject, gameObjectBindInfo, context, out shouldMakeActive);
	}

	private GameObject GetPrefabAsGameObject(UnityEngine.Object prefab)
	{
		if (prefab is GameObject)
		{
			return (GameObject)prefab;
		}
		Assert.That(prefab is Component, "Invalid type given for prefab. Given object name: '{0}'", prefab.name);
		return ((Component)prefab).gameObject;
	}

	internal GameObject CreateAndParentPrefab(UnityEngine.Object prefab, GameObjectCreationParameters gameObjectBindInfo, InjectContext context, out bool shouldMakeActive)
	{
		Assert.That(prefab != null, "Null prefab found when instantiating game object");
		Assert.That(!AssertOnNewGameObjects, "Given DiContainer does not support creating new game objects");
		FlushBindings();
		GameObject prefabAsGameObject = GetPrefabAsGameObject(prefab);
		bool flag = (shouldMakeActive = prefabAsGameObject.activeSelf);
		Transform transformGroup = GetTransformGroup(gameObjectBindInfo, context);
		if (flag)
		{
			prefabAsGameObject.SetActive(value: false);
		}
		Transform parent = ((!(transformGroup != null)) ? ContextTransform : transformGroup);
		GameObject gameObject;
		bool worldPositionStays;
		if (gameObjectBindInfo.Position.HasValue && gameObjectBindInfo.Rotation.HasValue)
		{
			gameObject = UnityEngine.Object.Instantiate(prefabAsGameObject, gameObjectBindInfo.Position.Value, gameObjectBindInfo.Rotation.Value, parent);
			worldPositionStays = true;
		}
		else if (gameObjectBindInfo.Position.HasValue)
		{
			gameObject = UnityEngine.Object.Instantiate(prefabAsGameObject, gameObjectBindInfo.Position.Value, prefabAsGameObject.transform.rotation, parent);
			worldPositionStays = true;
		}
		else if (gameObjectBindInfo.Rotation.HasValue)
		{
			gameObject = UnityEngine.Object.Instantiate(prefabAsGameObject, prefabAsGameObject.transform.position, gameObjectBindInfo.Rotation.Value, parent);
			worldPositionStays = true;
		}
		else
		{
			gameObject = UnityEngine.Object.Instantiate(prefabAsGameObject, parent);
			worldPositionStays = false;
		}
		if (flag)
		{
			prefabAsGameObject.SetActive(value: true);
		}
		if (gameObject.transform.parent != transformGroup)
		{
			gameObject.transform.SetParent(transformGroup, worldPositionStays);
		}
		if (gameObjectBindInfo.Name != null)
		{
			gameObject.name = gameObjectBindInfo.Name;
		}
		return gameObject;
	}

	public GameObject CreateEmptyGameObject(string name)
	{
		return CreateEmptyGameObject(new GameObjectCreationParameters
		{
			Name = name
		}, null);
	}

	public GameObject CreateEmptyGameObject(GameObjectCreationParameters gameObjectBindInfo, InjectContext context)
	{
		Assert.That(!AssertOnNewGameObjects, "Given DiContainer does not support creating new game objects");
		FlushBindings();
		GameObject gameObject = new GameObject(gameObjectBindInfo.Name ?? "GameObject");
		Transform transformGroup = GetTransformGroup(gameObjectBindInfo, context);
		if (transformGroup == null)
		{
			gameObject.transform.SetParent(ContextTransform, worldPositionStays: false);
			gameObject.transform.SetParent(null, worldPositionStays: false);
		}
		else
		{
			gameObject.transform.SetParent(transformGroup, worldPositionStays: false);
		}
		return gameObject;
	}

	private Transform GetTransformGroup(GameObjectCreationParameters gameObjectBindInfo, InjectContext context)
	{
		Assert.That(!AssertOnNewGameObjects, "Given DiContainer does not support creating new game objects");
		if (gameObjectBindInfo.ParentTransform != null)
		{
			Assert.IsNull(gameObjectBindInfo.GroupName);
			Assert.IsNull(gameObjectBindInfo.ParentTransformGetter);
			return gameObjectBindInfo.ParentTransform;
		}
		if (gameObjectBindInfo.ParentTransformGetter != null && !IsValidating)
		{
			Assert.IsNull(gameObjectBindInfo.GroupName);
			if (context == null)
			{
				InjectContext injectContext = new InjectContext();
				injectContext.Container = this;
				context = injectContext;
			}
			return gameObjectBindInfo.ParentTransformGetter(context);
		}
		string groupName = gameObjectBindInfo.GroupName;
		Transform transform = ((!_hasExplicitDefaultParent) ? _inheritedDefaultParent : _explicitDefaultParent);
		if (transform == null)
		{
			if (groupName == null)
			{
				return null;
			}
			return (GameObject.Find("/" + groupName) ?? CreateTransformGroup(groupName)).transform;
		}
		if (groupName == null)
		{
			return transform;
		}
		foreach (Transform item in transform)
		{
			if (item.name == groupName)
			{
				return item;
			}
		}
		Transform transform3 = new GameObject(groupName).transform;
		transform3.SetParent(transform, worldPositionStays: false);
		return transform3;
	}

	private GameObject CreateTransformGroup(string groupName)
	{
		GameObject gameObject = new GameObject(groupName);
		gameObject.transform.SetParent(ContextTransform, worldPositionStays: false);
		gameObject.transform.SetParent(null, worldPositionStays: false);
		return gameObject;
	}

	public T Instantiate<T>()
	{
		return Instantiate<T>(new object[0]);
	}

	public T Instantiate<T>(IEnumerable<object> extraArgs)
	{
		object obj = Instantiate(typeof(T), extraArgs);
		if (IsValidating && !(obj is T))
		{
			Assert.That(obj is ValidationMarker);
			return default(T);
		}
		return (T)obj;
	}

	public object Instantiate(Type concreteType)
	{
		return Instantiate(concreteType, new object[0]);
	}

	public object Instantiate(Type concreteType, IEnumerable<object> extraArgs)
	{
		Assert.That(!LinqExtensions.ContainsItem(extraArgs, null), "Null value given to factory constructor arguments when instantiating object with type '{0}'. In order to use null use InstantiateExplicit", concreteType);
		return InstantiateExplicit(concreteType, InjectUtil.CreateArgList(extraArgs));
	}

	public TContract InstantiateComponent<TContract>(GameObject gameObject) where TContract : Component
	{
		return InstantiateComponent<TContract>(gameObject, new object[0]);
	}

	public TContract InstantiateComponent<TContract>(GameObject gameObject, IEnumerable<object> extraArgs) where TContract : Component
	{
		return (TContract)InstantiateComponent(typeof(TContract), gameObject, extraArgs);
	}

	public Component InstantiateComponent(Type componentType, GameObject gameObject)
	{
		return InstantiateComponent(componentType, gameObject, new object[0]);
	}

	public Component InstantiateComponent(Type componentType, GameObject gameObject, IEnumerable<object> extraArgs)
	{
		return InstantiateComponentExplicit(componentType, gameObject, InjectUtil.CreateArgList(extraArgs));
	}

	public T InstantiateComponentOnNewGameObject<T>() where T : Component
	{
		return InstantiateComponentOnNewGameObject<T>(typeof(T).Name);
	}

	public T InstantiateComponentOnNewGameObject<T>(IEnumerable<object> extraArgs) where T : Component
	{
		return InstantiateComponentOnNewGameObject<T>(typeof(T).Name, extraArgs);
	}

	public T InstantiateComponentOnNewGameObject<T>(string gameObjectName) where T : Component
	{
		return InstantiateComponentOnNewGameObject<T>(gameObjectName, new object[0]);
	}

	public T InstantiateComponentOnNewGameObject<T>(string gameObjectName, IEnumerable<object> extraArgs) where T : Component
	{
		return InstantiateComponent<T>(CreateEmptyGameObject(gameObjectName), extraArgs);
	}

	public GameObject InstantiatePrefab(UnityEngine.Object prefab)
	{
		return InstantiatePrefab(prefab, GameObjectCreationParameters.Default);
	}

	public GameObject InstantiatePrefab(UnityEngine.Object prefab, Transform parentTransform)
	{
		return InstantiatePrefab(prefab, new GameObjectCreationParameters
		{
			ParentTransform = parentTransform
		});
	}

	public GameObject InstantiatePrefab(UnityEngine.Object prefab, Vector3 position, Quaternion rotation, Transform parentTransform)
	{
		return InstantiatePrefab(prefab, new GameObjectCreationParameters
		{
			ParentTransform = parentTransform,
			Position = position,
			Rotation = rotation
		});
	}

	public GameObject InstantiatePrefab(UnityEngine.Object prefab, GameObjectCreationParameters gameObjectBindInfo)
	{
		FlushBindings();
		bool shouldMakeActive;
		GameObject gameObject = CreateAndParentPrefab(prefab, gameObjectBindInfo, null, out shouldMakeActive);
		InjectGameObject(gameObject);
		if (shouldMakeActive && !IsValidating)
		{
			gameObject.SetActive(value: true);
		}
		return gameObject;
	}

	public GameObject InstantiatePrefabResource(string resourcePath)
	{
		return InstantiatePrefabResource(resourcePath, GameObjectCreationParameters.Default);
	}

	public GameObject InstantiatePrefabResource(string resourcePath, Transform parentTransform)
	{
		return InstantiatePrefabResource(resourcePath, new GameObjectCreationParameters
		{
			ParentTransform = parentTransform
		});
	}

	public GameObject InstantiatePrefabResource(string resourcePath, Vector3 position, Quaternion rotation, Transform parentTransform)
	{
		return InstantiatePrefabResource(resourcePath, new GameObjectCreationParameters
		{
			ParentTransform = parentTransform,
			Position = position,
			Rotation = rotation
		});
	}

	public GameObject InstantiatePrefabResource(string resourcePath, GameObjectCreationParameters creationInfo)
	{
		GameObject gameObject = (GameObject)Resources.Load(resourcePath);
		Assert.IsNotNull(gameObject, MiscExtensions.Fmt("Could not find prefab at resource location '{0}'", resourcePath));
		return InstantiatePrefab(gameObject, creationInfo);
	}

	public T InstantiatePrefabForComponent<T>(UnityEngine.Object prefab)
	{
		return (T)InstantiatePrefabForComponent(typeof(T), prefab, null, new object[0]);
	}

	public T InstantiatePrefabForComponent<T>(UnityEngine.Object prefab, IEnumerable<object> extraArgs)
	{
		return (T)InstantiatePrefabForComponent(typeof(T), prefab, null, extraArgs);
	}

	public T InstantiatePrefabForComponent<T>(UnityEngine.Object prefab, Transform parentTransform)
	{
		return (T)InstantiatePrefabForComponent(typeof(T), prefab, parentTransform, new object[0]);
	}

	public T InstantiatePrefabForComponent<T>(UnityEngine.Object prefab, Transform parentTransform, IEnumerable<object> extraArgs)
	{
		return (T)InstantiatePrefabForComponent(typeof(T), prefab, parentTransform, extraArgs);
	}

	public T InstantiatePrefabForComponent<T>(UnityEngine.Object prefab, Vector3 position, Quaternion rotation, Transform parentTransform)
	{
		return (T)InstantiatePrefabForComponent(typeof(T), prefab, new object[0], new GameObjectCreationParameters
		{
			ParentTransform = parentTransform,
			Position = position,
			Rotation = rotation
		});
	}

	public T InstantiatePrefabForComponent<T>(UnityEngine.Object prefab, Vector3 position, Quaternion rotation, Transform parentTransform, IEnumerable<object> extraArgs)
	{
		return (T)InstantiatePrefabForComponent(typeof(T), prefab, extraArgs, new GameObjectCreationParameters
		{
			ParentTransform = parentTransform,
			Position = position,
			Rotation = rotation
		});
	}

	public object InstantiatePrefabForComponent(Type concreteType, UnityEngine.Object prefab, Transform parentTransform, IEnumerable<object> extraArgs)
	{
		return InstantiatePrefabForComponent(concreteType, prefab, extraArgs, new GameObjectCreationParameters
		{
			ParentTransform = parentTransform
		});
	}

	public object InstantiatePrefabForComponent(Type concreteType, UnityEngine.Object prefab, IEnumerable<object> extraArgs, GameObjectCreationParameters creationInfo)
	{
		return InstantiatePrefabForComponentExplicit(concreteType, prefab, InjectUtil.CreateArgList(extraArgs), creationInfo);
	}

	public T InstantiatePrefabResourceForComponent<T>(string resourcePath)
	{
		return (T)InstantiatePrefabResourceForComponent(typeof(T), resourcePath, null, new object[0]);
	}

	public T InstantiatePrefabResourceForComponent<T>(string resourcePath, IEnumerable<object> extraArgs)
	{
		return (T)InstantiatePrefabResourceForComponent(typeof(T), resourcePath, null, extraArgs);
	}

	public T InstantiatePrefabResourceForComponent<T>(string resourcePath, Transform parentTransform)
	{
		return (T)InstantiatePrefabResourceForComponent(typeof(T), resourcePath, parentTransform, new object[0]);
	}

	public T InstantiatePrefabResourceForComponent<T>(string resourcePath, Transform parentTransform, IEnumerable<object> extraArgs)
	{
		return (T)InstantiatePrefabResourceForComponent(typeof(T), resourcePath, parentTransform, extraArgs);
	}

	public T InstantiatePrefabResourceForComponent<T>(string resourcePath, Vector3 position, Quaternion rotation, Transform parentTransform)
	{
		return InstantiatePrefabResourceForComponent<T>(resourcePath, position, rotation, parentTransform, new object[0]);
	}

	public T InstantiatePrefabResourceForComponent<T>(string resourcePath, Vector3 position, Quaternion rotation, Transform parentTransform, IEnumerable<object> extraArgs)
	{
		List<TypeValuePair> extraArgs2 = InjectUtil.CreateArgList(extraArgs);
		GameObjectCreationParameters gameObjectCreationParameters = new GameObjectCreationParameters();
		gameObjectCreationParameters.ParentTransform = parentTransform;
		gameObjectCreationParameters.Position = position;
		gameObjectCreationParameters.Rotation = rotation;
		GameObjectCreationParameters creationInfo = gameObjectCreationParameters;
		return (T)InstantiatePrefabResourceForComponentExplicit(typeof(T), resourcePath, extraArgs2, creationInfo);
	}

	public object InstantiatePrefabResourceForComponent(Type concreteType, string resourcePath, Transform parentTransform, IEnumerable<object> extraArgs)
	{
		Assert.That(!LinqExtensions.ContainsItem(extraArgs, null), "Null value given to factory constructor arguments when instantiating object with type '{0}'. In order to use null use InstantiatePrefabForComponentExplicit", concreteType);
		return InstantiatePrefabResourceForComponentExplicit(concreteType, resourcePath, InjectUtil.CreateArgList(extraArgs), new GameObjectCreationParameters
		{
			ParentTransform = parentTransform
		});
	}

	public T InstantiateScriptableObjectResource<T>(string resourcePath) where T : ScriptableObject
	{
		return InstantiateScriptableObjectResource<T>(resourcePath, new object[0]);
	}

	public T InstantiateScriptableObjectResource<T>(string resourcePath, IEnumerable<object> extraArgs) where T : ScriptableObject
	{
		return (T)InstantiateScriptableObjectResource(typeof(T), resourcePath, extraArgs);
	}

	public object InstantiateScriptableObjectResource(Type scriptableObjectType, string resourcePath)
	{
		return InstantiateScriptableObjectResource(scriptableObjectType, resourcePath, new object[0]);
	}

	public object InstantiateScriptableObjectResource(Type scriptableObjectType, string resourcePath, IEnumerable<object> extraArgs)
	{
		Assert.DerivesFromOrEqual<ScriptableObject>(scriptableObjectType);
		return InstantiateScriptableObjectResourceExplicit(scriptableObjectType, resourcePath, InjectUtil.CreateArgList(extraArgs));
	}

	public void InjectGameObject(GameObject gameObject)
	{
		FlushBindings();
		ZenUtilInternal.AddStateMachineBehaviourAutoInjectersUnderGameObject(gameObject);
		List<MonoBehaviour> list = ZenPools.SpawnList<MonoBehaviour>();
		try
		{
			ZenUtilInternal.GetInjectableMonoBehavioursUnderGameObject(gameObject, list);
			for (int i = 0; i < list.Count; i++)
			{
				Inject(list[i]);
			}
		}
		finally
		{
			ZenPools.DespawnList(list);
		}
	}

	public T InjectGameObjectForComponent<T>(GameObject gameObject) where T : Component
	{
		return InjectGameObjectForComponent<T>(gameObject, new object[0]);
	}

	public T InjectGameObjectForComponent<T>(GameObject gameObject, IEnumerable<object> extraArgs) where T : Component
	{
		return (T)InjectGameObjectForComponent(gameObject, typeof(T), extraArgs);
	}

	public object InjectGameObjectForComponent(GameObject gameObject, Type componentType, IEnumerable<object> extraArgs)
	{
		return InjectGameObjectForComponentExplicit(gameObject, componentType, InjectUtil.CreateArgList(extraArgs), new InjectContext(this, componentType, null), null);
	}

	public Component InjectGameObjectForComponentExplicit(GameObject gameObject, Type componentType, List<TypeValuePair> extraArgs, InjectContext context, object concreteIdentifier)
	{
		if (!TypeExtensions.DerivesFrom<MonoBehaviour>(componentType) && extraArgs.Count > 0)
		{
			throw Assert.CreateException("Cannot inject into non-monobehaviours!  Argument list must be zero length");
		}
		ZenUtilInternal.AddStateMachineBehaviourAutoInjectersUnderGameObject(gameObject);
		List<MonoBehaviour> list = ZenPools.SpawnList<MonoBehaviour>();
		try
		{
			ZenUtilInternal.GetInjectableMonoBehavioursUnderGameObject(gameObject, list);
			for (int i = 0; i < list.Count; i++)
			{
				MonoBehaviour monoBehaviour = list[i];
				if (TypeExtensions.DerivesFromOrEqual(monoBehaviour.GetType(), componentType))
				{
					InjectExplicit(monoBehaviour, monoBehaviour.GetType(), extraArgs, context, concreteIdentifier);
				}
				else
				{
					Inject(monoBehaviour);
				}
			}
		}
		finally
		{
			ZenPools.DespawnList(list);
		}
		Component[] componentsInChildren = gameObject.GetComponentsInChildren(componentType, includeInactive: true);
		Assert.That(componentsInChildren.Length > 0, "Expected to find component with type '{0}' when injecting into game object '{1}'", componentType, gameObject.name);
		Assert.That(componentsInChildren.Length == 1, "Found multiple component with type '{0}' when injecting into game object '{1}'", componentType, gameObject.name);
		return componentsInChildren[0];
	}

	public void Inject(object injectable)
	{
		Inject(injectable, new object[0]);
	}

	public void Inject(object injectable, IEnumerable<object> extraArgs)
	{
		InjectExplicit(injectable, InjectUtil.CreateArgList(extraArgs));
	}

	public TContract Resolve<TContract>()
	{
		return (TContract)Resolve(typeof(TContract));
	}

	public object Resolve(Type contractType)
	{
		return ResolveId(contractType, null);
	}

	public TContract ResolveId<TContract>(object identifier)
	{
		return (TContract)ResolveId(typeof(TContract), identifier);
	}

	public object ResolveId(Type contractType, object identifier)
	{
		using InjectContext injectContext = ZenPools.SpawnInjectContext(this, contractType);
		injectContext.Identifier = identifier;
		return Resolve(injectContext);
	}

	public TContract TryResolve<TContract>() where TContract : class
	{
		return (TContract)TryResolve(typeof(TContract));
	}

	public object TryResolve(Type contractType)
	{
		return TryResolveId(contractType, null);
	}

	public TContract TryResolveId<TContract>(object identifier) where TContract : class
	{
		return (TContract)TryResolveId(typeof(TContract), identifier);
	}

	public object TryResolveId(Type contractType, object identifier)
	{
		using InjectContext injectContext = ZenPools.SpawnInjectContext(this, contractType);
		injectContext.Identifier = identifier;
		injectContext.Optional = true;
		return Resolve(injectContext);
	}

	public List<TContract> ResolveAll<TContract>()
	{
		return (List<TContract>)ResolveAll(typeof(TContract));
	}

	public IList ResolveAll(Type contractType)
	{
		return ResolveIdAll(contractType, null);
	}

	public List<TContract> ResolveIdAll<TContract>(object identifier)
	{
		return (List<TContract>)ResolveIdAll(typeof(TContract), identifier);
	}

	public IList ResolveIdAll(Type contractType, object identifier)
	{
		using InjectContext injectContext = ZenPools.SpawnInjectContext(this, contractType);
		injectContext.Identifier = identifier;
		injectContext.Optional = true;
		return ResolveAll(injectContext);
	}

	public void UnbindAll()
	{
		FlushBindings();
		_providers.Clear();
	}

	public bool Unbind<TContract>()
	{
		return Unbind(typeof(TContract));
	}

	public bool Unbind(Type contractType)
	{
		return UnbindId(contractType, null);
	}

	public bool UnbindId<TContract>(object identifier)
	{
		return UnbindId(typeof(TContract), identifier);
	}

	public bool UnbindId(Type contractType, object identifier)
	{
		FlushBindings();
		BindingId key = new BindingId(contractType, identifier);
		return _providers.Remove(key);
	}

	public void UnbindInterfacesTo<TConcrete>()
	{
		UnbindInterfacesTo(typeof(TConcrete));
	}

	public void UnbindInterfacesTo(Type concreteType)
	{
		Type[] array = TypeExtensions.Interfaces(concreteType);
		foreach (Type contractType in array)
		{
			Unbind(contractType, concreteType);
		}
	}

	public bool Unbind<TContract, TConcrete>()
	{
		return Unbind(typeof(TContract), typeof(TConcrete));
	}

	public bool Unbind(Type contractType, Type concreteType)
	{
		return UnbindId(contractType, concreteType, null);
	}

	public bool UnbindId<TContract, TConcrete>(object identifier)
	{
		return UnbindId(typeof(TContract), typeof(TConcrete), identifier);
	}

	public bool UnbindId(Type contractType, Type concreteType, object identifier)
	{
		FlushBindings();
		BindingId key = new BindingId(contractType, identifier);
		if (!_providers.TryGetValue(key, out var value))
		{
			return false;
		}
		List<ProviderInfo> list = value.Where((ProviderInfo x) => TypeExtensions.DerivesFromOrEqual(x.Provider.GetInstanceType(new InjectContext(this, contractType, identifier)), concreteType)).ToList();
		if (list.Count == 0)
		{
			return false;
		}
		foreach (ProviderInfo item in list)
		{
			bool condition = value.Remove(item);
			Assert.That(condition);
		}
		return true;
	}

	public bool HasBinding<TContract>()
	{
		return HasBinding(typeof(TContract));
	}

	public bool HasBinding(Type contractType)
	{
		return HasBindingId(contractType, null);
	}

	public bool HasBindingId<TContract>(object identifier)
	{
		return HasBindingId(typeof(TContract), identifier);
	}

	public bool HasBindingId(Type contractType, object identifier)
	{
		return HasBindingId(contractType, identifier, InjectSources.Any);
	}

	public bool HasBindingId(Type contractType, object identifier, InjectSources sourceType)
	{
		using InjectContext injectContext = ZenPools.SpawnInjectContext(this, contractType);
		injectContext.Identifier = identifier;
		injectContext.SourceType = sourceType;
		return HasBinding(injectContext);
	}

	public bool HasBinding(InjectContext context)
	{
		Assert.IsNotNull(context);
		FlushBindings();
		List<ProviderInfo> list = ZenPools.SpawnList<ProviderInfo>();
		try
		{
			GetProviderMatches(context, list);
			return list.Count > 0;
		}
		finally
		{
			ZenPools.DespawnList(list);
		}
	}

	public void FlushBindings()
	{
		while (_currentBindings.Count > 0)
		{
			BindStatement bindStatement = _currentBindings.Dequeue();
			if (bindStatement.BindingInheritanceMethod != BindingInheritanceMethods.MoveDirectOnly && bindStatement.BindingInheritanceMethod != BindingInheritanceMethods.MoveIntoAll)
			{
				FinalizeBinding(bindStatement);
			}
			if (bindStatement.BindingInheritanceMethod != BindingInheritanceMethods.None)
			{
				_childBindings.Add(bindStatement);
			}
			else
			{
				bindStatement.Dispose();
			}
		}
	}

	private void FinalizeBinding(BindStatement binding)
	{
		_isFinalizingBinding = true;
		try
		{
			binding.FinalizeBinding(this);
		}
		finally
		{
			_isFinalizingBinding = false;
		}
	}

	public BindStatement StartBinding(bool flush = true)
	{
		Assert.That(!_isFinalizingBinding, "Attempted to start a binding during a binding finalizer.  This is not allowed, since binding finalizers should directly use AddProvider instead, to allow for bindings to be inherited properly without duplicates");
		if (flush)
		{
			FlushBindings();
		}
		BindStatement bindStatement = ZenPools.SpawnStatement();
		_currentBindings.Enqueue(bindStatement);
		return bindStatement;
	}

	public ConcreteBinderGeneric<TContract> Rebind<TContract>()
	{
		return RebindId<TContract>(null);
	}

	public ConcreteBinderGeneric<TContract> RebindId<TContract>(object identifier)
	{
		UnbindId<TContract>(identifier);
		return Bind<TContract>().WithId(identifier);
	}

	public ConcreteBinderNonGeneric Rebind(Type contractType)
	{
		return RebindId(contractType, null);
	}

	public ConcreteBinderNonGeneric RebindId(Type contractType, object identifier)
	{
		UnbindId(contractType, identifier);
		return Bind(contractType).WithId(identifier);
	}

	public ConcreteIdBinderGeneric<TContract> Bind<TContract>()
	{
		return Bind<TContract>(StartBinding());
	}

	public ConcreteIdBinderGeneric<TContract> BindNoFlush<TContract>()
	{
		return Bind<TContract>(StartBinding(flush: false));
	}

	private ConcreteIdBinderGeneric<TContract> Bind<TContract>(BindStatement bindStatement)
	{
		BindInfo bindInfo = bindStatement.SpawnBindInfo();
		Assert.That(!TypeExtensions.DerivesFrom<IPlaceholderFactory>(typeof(TContract)), "You should not use Container.Bind for factory classes.  Use Container.BindFactory instead.");
		Assert.That(!bindInfo.ContractTypes.Contains(typeof(TContract)));
		bindInfo.ContractTypes.Add(typeof(TContract));
		return new ConcreteIdBinderGeneric<TContract>(this, bindInfo, bindStatement);
	}

	public ConcreteIdBinderNonGeneric Bind(params Type[] contractTypes)
	{
		BindStatement bindStatement = StartBinding();
		BindInfo bindInfo = bindStatement.SpawnBindInfo();
		MiscExtensions.AllocFreeAddRange(bindInfo.ContractTypes, contractTypes);
		return BindInternal(bindInfo, bindStatement);
	}

	public ConcreteIdBinderNonGeneric Bind(IEnumerable<Type> contractTypes)
	{
		BindStatement bindStatement = StartBinding();
		BindInfo bindInfo = bindStatement.SpawnBindInfo();
		bindInfo.ContractTypes.AddRange(contractTypes);
		return BindInternal(bindInfo, bindStatement);
	}

	private ConcreteIdBinderNonGeneric BindInternal(BindInfo bindInfo, BindStatement bindingFinalizer)
	{
		Assert.That(bindInfo.ContractTypes.All((Type x) => !TypeExtensions.DerivesFrom<IPlaceholderFactory>(x)), "You should not use Container.Bind for factory classes.  Use Container.BindFactory instead.");
		return new ConcreteIdBinderNonGeneric(this, bindInfo, bindingFinalizer);
	}

	public ConcreteIdBinderNonGeneric Bind(Action<ConventionSelectTypesBinder> generator)
	{
		ConventionBindInfo conventionBindInfo = new ConventionBindInfo();
		generator(new ConventionSelectTypesBinder(conventionBindInfo));
		List<Type> list = conventionBindInfo.ResolveTypes();
		Assert.That(list.All((Type x) => !TypeExtensions.DerivesFrom<IPlaceholderFactory>(x)), "You should not use Container.Bind for factory classes.  Use Container.BindFactory instead.");
		BindStatement bindStatement = StartBinding();
		BindInfo bindInfo = bindStatement.SpawnBindInfo();
		MiscExtensions.AllocFreeAddRange(bindInfo.ContractTypes, list);
		bindInfo.InvalidBindResponse = InvalidBindResponses.Skip;
		return new ConcreteIdBinderNonGeneric(this, bindInfo, bindStatement);
	}

	public FromBinderNonGeneric BindInterfacesTo<T>()
	{
		return BindInterfacesTo(typeof(T));
	}

	public FromBinderNonGeneric BindInterfacesTo(Type type)
	{
		BindStatement bindStatement = StartBinding();
		BindInfo bindInfo = bindStatement.SpawnBindInfo();
		Type[] array = TypeExtensions.Interfaces(type);
		if (array.Length == 0)
		{
			Log.Warn("Called BindInterfacesTo for type {0} but no interfaces were found", type);
		}
		MiscExtensions.AllocFreeAddRange(bindInfo.ContractTypes, array);
		bindInfo.RequireExplicitScope = true;
		return BindInternal(bindInfo, bindStatement).To(type);
	}

	public FromBinderNonGeneric BindInterfacesAndSelfTo<T>()
	{
		return BindInterfacesAndSelfTo(typeof(T));
	}

	public FromBinderNonGeneric BindInterfacesAndSelfTo(Type type)
	{
		BindStatement bindStatement = StartBinding();
		BindInfo bindInfo = bindStatement.SpawnBindInfo();
		MiscExtensions.AllocFreeAddRange(bindInfo.ContractTypes, TypeExtensions.Interfaces(type));
		bindInfo.ContractTypes.Add(type);
		bindInfo.RequireExplicitScope = true;
		return BindInternal(bindInfo, bindStatement).To(type);
	}

	public IdScopeConcreteIdArgConditionCopyNonLazyBinder BindInstance<TContract>(TContract instance)
	{
		BindStatement bindStatement = StartBinding();
		BindInfo bindInfo = bindStatement.SpawnBindInfo();
		bindInfo.ContractTypes.Add(typeof(TContract));
		bindStatement.SetFinalizer(new ScopableBindingFinalizer(bindInfo, (DiContainer container, Type type) => new InstanceProvider(type, instance, container)));
		return new IdScopeConcreteIdArgConditionCopyNonLazyBinder(bindInfo);
	}

	public void BindInstances(params object[] instances)
	{
		foreach (object obj in instances)
		{
			Assert.That(!ZenUtilInternal.IsNull(obj), "Found null instance provided to BindInstances method");
			Bind(obj.GetType()).FromInstance(obj);
		}
	}

	private FactoryToChoiceIdBinder<TContract> BindFactoryInternal<TContract, TFactoryContract, TFactoryConcrete>() where TFactoryContract : IFactory where TFactoryConcrete : IFactory, TFactoryContract
	{
		BindStatement bindStatement = StartBinding();
		BindInfo bindInfo = bindStatement.SpawnBindInfo();
		bindInfo.ContractTypes.Add(typeof(TFactoryContract));
		FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(TFactoryConcrete));
		bindStatement.SetFinalizer(new PlaceholderFactoryBindingFinalizer<TContract>(bindInfo, factoryBindInfo));
		return new FactoryToChoiceIdBinder<TContract>(this, bindInfo, factoryBindInfo);
	}

	public FactoryToChoiceIdBinder<TContract> BindIFactory<TContract>()
	{
		return BindFactoryInternal<TContract, IFactory<TContract>, PlaceholderFactory<TContract>>();
	}

	public FactoryToChoiceIdBinder<TContract> BindFactory<TContract, TFactory>() where TFactory : PlaceholderFactory<TContract>
	{
		return BindFactoryInternal<TContract, TFactory, TFactory>();
	}

	public FactoryToChoiceIdBinder<TContract> BindFactoryCustomInterface<TContract, TFactoryConcrete, TFactoryContract>() where TFactoryConcrete : PlaceholderFactory<TContract>, TFactoryContract where TFactoryContract : IFactory
	{
		return BindFactoryInternal<TContract, TFactoryContract, TFactoryConcrete>();
	}

	public MemoryPoolIdInitialSizeMaxSizeBinder<TItemContract> BindMemoryPool<TItemContract>()
	{
		return BindMemoryPool<TItemContract, MemoryPool<TItemContract>>();
	}

	public MemoryPoolIdInitialSizeMaxSizeBinder<TItemContract> BindMemoryPool<TItemContract, TPool>() where TPool : IMemoryPool
	{
		return BindMemoryPoolCustomInterface<TItemContract, TPool, TPool>();
	}

	public MemoryPoolIdInitialSizeMaxSizeBinder<TItemContract> BindMemoryPoolCustomInterface<TItemContract, TPoolConcrete, TPoolContract>(bool includeConcreteType = false) where TPoolConcrete : IMemoryPool, TPoolContract where TPoolContract : IMemoryPool
	{
		return BindMemoryPoolCustomInterfaceInternal<TItemContract, TPoolConcrete, TPoolContract>(includeConcreteType, StartBinding());
	}

	internal MemoryPoolIdInitialSizeMaxSizeBinder<TItemContract> BindMemoryPoolCustomInterfaceNoFlush<TItemContract, TPoolConcrete, TPoolContract>(bool includeConcreteType = false) where TPoolConcrete : IMemoryPool, TPoolContract where TPoolContract : IMemoryPool
	{
		return BindMemoryPoolCustomInterfaceInternal<TItemContract, TPoolConcrete, TPoolContract>(includeConcreteType, StartBinding(flush: false));
	}

	private MemoryPoolIdInitialSizeMaxSizeBinder<TItemContract> BindMemoryPoolCustomInterfaceInternal<TItemContract, TPoolConcrete, TPoolContract>(bool includeConcreteType, BindStatement statement) where TPoolConcrete : IMemoryPool, TPoolContract where TPoolContract : IMemoryPool
	{
		List<Type> list = new List<Type>();
		list.Add(typeof(IDisposable));
		list.Add(typeof(TPoolContract));
		List<Type> list2 = list;
		if (includeConcreteType)
		{
			list2.Add(typeof(TPoolConcrete));
		}
		BindInfo bindInfo = statement.SpawnBindInfo();
		MiscExtensions.AllocFreeAddRange(bindInfo.ContractTypes, list2);
		bindInfo.ContractTypes.Add(typeof(IMemoryPool));
		FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(TPoolConcrete));
		MemoryPoolBindInfo poolBindInfo = new MemoryPoolBindInfo();
		statement.SetFinalizer(new MemoryPoolBindingFinalizer<TItemContract>(bindInfo, factoryBindInfo, poolBindInfo));
		return new MemoryPoolIdInitialSizeMaxSizeBinder<TItemContract>(this, bindInfo, factoryBindInfo, poolBindInfo);
	}

	private FactoryToChoiceIdBinder<TParam1, TContract> BindFactoryInternal<TParam1, TContract, TFactoryContract, TFactoryConcrete>() where TFactoryContract : IFactory where TFactoryConcrete : IFactory, TFactoryContract
	{
		BindStatement bindStatement = StartBinding();
		BindInfo bindInfo = bindStatement.SpawnBindInfo();
		bindInfo.ContractTypes.Add(typeof(TFactoryContract));
		FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(TFactoryConcrete));
		bindStatement.SetFinalizer(new PlaceholderFactoryBindingFinalizer<TContract>(bindInfo, factoryBindInfo));
		return new FactoryToChoiceIdBinder<TParam1, TContract>(this, bindInfo, factoryBindInfo);
	}

	public FactoryToChoiceIdBinder<TParam1, TContract> BindIFactory<TParam1, TContract>()
	{
		return BindFactoryInternal<TParam1, TContract, IFactory<TParam1, TContract>, PlaceholderFactory<TParam1, TContract>>();
	}

	public FactoryToChoiceIdBinder<TParam1, TContract> BindFactory<TParam1, TContract, TFactory>() where TFactory : PlaceholderFactory<TParam1, TContract>
	{
		return BindFactoryInternal<TParam1, TContract, TFactory, TFactory>();
	}

	public FactoryToChoiceIdBinder<TParam1, TContract> BindFactoryCustomInterface<TParam1, TContract, TFactoryConcrete, TFactoryContract>() where TFactoryConcrete : PlaceholderFactory<TParam1, TContract>, TFactoryContract where TFactoryContract : IFactory
	{
		return BindFactoryInternal<TParam1, TContract, TFactoryContract, TFactoryConcrete>();
	}

	private FactoryToChoiceIdBinder<TParam1, TParam2, TContract> BindFactoryInternal<TParam1, TParam2, TContract, TFactoryContract, TFactoryConcrete>() where TFactoryContract : IFactory where TFactoryConcrete : IFactory, TFactoryContract
	{
		BindStatement bindStatement = StartBinding();
		BindInfo bindInfo = bindStatement.SpawnBindInfo();
		bindInfo.ContractTypes.Add(typeof(TFactoryContract));
		FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(TFactoryConcrete));
		bindStatement.SetFinalizer(new PlaceholderFactoryBindingFinalizer<TContract>(bindInfo, factoryBindInfo));
		return new FactoryToChoiceIdBinder<TParam1, TParam2, TContract>(this, bindInfo, factoryBindInfo);
	}

	public FactoryToChoiceIdBinder<TParam1, TParam2, TContract> BindIFactory<TParam1, TParam2, TContract>()
	{
		return BindFactoryInternal<TParam1, TParam2, TContract, IFactory<TParam1, TParam2, TContract>, PlaceholderFactory<TParam1, TParam2, TContract>>();
	}

	public FactoryToChoiceIdBinder<TParam1, TParam2, TContract> BindFactory<TParam1, TParam2, TContract, TFactory>() where TFactory : PlaceholderFactory<TParam1, TParam2, TContract>
	{
		return BindFactoryInternal<TParam1, TParam2, TContract, TFactory, TFactory>();
	}

	public FactoryToChoiceIdBinder<TParam1, TParam2, TContract> BindFactoryCustomInterface<TParam1, TParam2, TContract, TFactoryConcrete, TFactoryContract>() where TFactoryConcrete : PlaceholderFactory<TParam1, TParam2, TContract>, TFactoryContract where TFactoryContract : IFactory
	{
		return BindFactoryInternal<TParam1, TParam2, TContract, TFactoryContract, TFactoryConcrete>();
	}

	private FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TContract> BindFactoryInternal<TParam1, TParam2, TParam3, TContract, TFactoryContract, TFactoryConcrete>() where TFactoryContract : IFactory where TFactoryConcrete : IFactory, TFactoryContract
	{
		BindStatement bindStatement = StartBinding();
		BindInfo bindInfo = bindStatement.SpawnBindInfo();
		bindInfo.ContractTypes.Add(typeof(TFactoryContract));
		FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(TFactoryConcrete));
		bindStatement.SetFinalizer(new PlaceholderFactoryBindingFinalizer<TContract>(bindInfo, factoryBindInfo));
		return new FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TContract>(this, bindInfo, factoryBindInfo);
	}

	public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TContract> BindIFactory<TParam1, TParam2, TParam3, TContract>()
	{
		return BindFactoryInternal<TParam1, TParam2, TParam3, TContract, IFactory<TParam1, TParam2, TParam3, TContract>, PlaceholderFactory<TParam1, TParam2, TParam3, TContract>>();
	}

	public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TContract> BindFactory<TParam1, TParam2, TParam3, TContract, TFactory>() where TFactory : PlaceholderFactory<TParam1, TParam2, TParam3, TContract>
	{
		return BindFactoryInternal<TParam1, TParam2, TParam3, TContract, TFactory, TFactory>();
	}

	public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TContract> BindFactoryCustomInterface<TParam1, TParam2, TParam3, TContract, TFactoryConcrete, TFactoryContract>() where TFactoryConcrete : PlaceholderFactory<TParam1, TParam2, TParam3, TContract>, TFactoryContract where TFactoryContract : IFactory
	{
		return BindFactoryInternal<TParam1, TParam2, TParam3, TContract, TFactoryContract, TFactoryConcrete>();
	}

	private FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TContract> BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TContract, TFactoryContract, TFactoryConcrete>() where TFactoryContract : IFactory where TFactoryConcrete : IFactory, TFactoryContract
	{
		BindStatement bindStatement = StartBinding();
		BindInfo bindInfo = bindStatement.SpawnBindInfo();
		bindInfo.ContractTypes.Add(typeof(TFactoryContract));
		FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(TFactoryConcrete));
		bindStatement.SetFinalizer(new PlaceholderFactoryBindingFinalizer<TContract>(bindInfo, factoryBindInfo));
		return new FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TContract>(this, bindInfo, factoryBindInfo);
	}

	public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TContract> BindIFactory<TParam1, TParam2, TParam3, TParam4, TContract>()
	{
		return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TContract, IFactory<TParam1, TParam2, TParam3, TParam4, TContract>, PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TContract>>();
	}

	public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TContract> BindFactory<TParam1, TParam2, TParam3, TParam4, TContract, TFactory>() where TFactory : PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TContract>
	{
		return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TContract, TFactory, TFactory>();
	}

	public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TContract> BindFactoryCustomInterface<TParam1, TParam2, TParam3, TParam4, TContract, TFactoryConcrete, TFactoryContract>() where TFactoryConcrete : PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TContract>, TFactoryContract where TFactoryContract : IFactory
	{
		return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TContract, TFactoryContract, TFactoryConcrete>();
	}

	private FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TContract, TFactoryContract, TFactoryConcrete>() where TFactoryContract : IFactory where TFactoryConcrete : IFactory, TFactoryContract
	{
		BindStatement bindStatement = StartBinding();
		BindInfo bindInfo = bindStatement.SpawnBindInfo();
		bindInfo.ContractTypes.Add(typeof(TFactoryContract));
		FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(TFactoryConcrete));
		bindStatement.SetFinalizer(new PlaceholderFactoryBindingFinalizer<TContract>(bindInfo, factoryBindInfo));
		return new FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>(this, bindInfo, factoryBindInfo);
	}

	public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> BindIFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>()
	{
		return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TContract, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>, PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>>();
	}

	public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> BindFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TContract, TFactory>() where TFactory : PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>
	{
		return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TContract, TFactory, TFactory>();
	}

	public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> BindFactoryCustomInterface<TParam1, TParam2, TParam3, TParam4, TParam5, TContract, TFactoryConcrete, TFactoryContract>() where TFactoryConcrete : PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>, TFactoryContract where TFactoryContract : IFactory
	{
		return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TContract, TFactoryContract, TFactoryConcrete>();
	}

	private FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract, TFactoryContract, TFactoryConcrete>() where TFactoryContract : IFactory where TFactoryConcrete : IFactory, TFactoryContract
	{
		BindStatement bindStatement = StartBinding();
		BindInfo bindInfo = bindStatement.SpawnBindInfo();
		bindInfo.ContractTypes.Add(typeof(TFactoryContract));
		FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(TFactoryConcrete));
		bindStatement.SetFinalizer(new PlaceholderFactoryBindingFinalizer<TContract>(bindInfo, factoryBindInfo));
		return new FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>(this, bindInfo, factoryBindInfo);
	}

	public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> BindIFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>()
	{
		return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>, PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>>();
	}

	public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> BindFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract, TFactory>() where TFactory : PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>
	{
		return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract, TFactory, TFactory>();
	}

	public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> BindFactoryCustomInterface<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract, TFactoryConcrete, TFactoryContract>() where TFactoryConcrete : PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>, TFactoryContract where TFactoryContract : IFactory
	{
		return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract, TFactoryContract, TFactoryConcrete>();
	}

	private FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract, TFactoryContract, TFactoryConcrete>() where TFactoryContract : IFactory where TFactoryConcrete : IFactory, TFactoryContract
	{
		BindStatement bindStatement = StartBinding();
		BindInfo bindInfo = bindStatement.SpawnBindInfo();
		bindInfo.ContractTypes.Add(typeof(TFactoryContract));
		FactoryBindInfo factoryBindInfo = new FactoryBindInfo(typeof(TFactoryConcrete));
		bindStatement.SetFinalizer(new PlaceholderFactoryBindingFinalizer<TContract>(bindInfo, factoryBindInfo));
		return new FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>(this, bindInfo, factoryBindInfo);
	}

	public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> BindIFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>()
	{
		return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>, PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>>();
	}

	public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> BindFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract, TFactory>() where TFactory : PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>
	{
		return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract, TFactory, TFactory>();
	}

	public FactoryToChoiceIdBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> BindFactoryCustomInterface<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract, TFactoryConcrete, TFactoryContract>() where TFactoryConcrete : PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>, TFactoryContract where TFactoryContract : IFactory
	{
		return BindFactoryInternal<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract, TFactoryContract, TFactoryConcrete>();
	}

	public T InstantiateExplicit<T>(List<TypeValuePair> extraArgs)
	{
		return (T)InstantiateExplicit(typeof(T), extraArgs);
	}

	public Lazy<T> InstantiateLazy<T>()
	{
		return InstantiateLazy<T>(typeof(T));
	}

	public Lazy<T> InstantiateLazy<T>(Type concreteType)
	{
		Assert.That(TypeExtensions.DerivesFromOrEqual<T>(concreteType));
		return new Lazy<T>(() => (T)Instantiate(concreteType));
	}

	public Lazy<T> ResolveLazy<T>()
	{
		return ResolveLazy<T>(typeof(T));
	}

	public Lazy<T> ResolveLazy<T>(Type concreteType)
	{
		Assert.That(TypeExtensions.DerivesFromOrEqual<T>(concreteType));
		return new Lazy<T>(() => (T)Resolve(concreteType));
	}

	public object InstantiateExplicit(Type concreteType, List<TypeValuePair> extraArgs)
	{
		bool autoInject = true;
		return InstantiateExplicit(concreteType, autoInject, extraArgs, new InjectContext(this, concreteType, null), null);
	}

	public object InstantiateExplicit(Type concreteType, bool autoInject, List<TypeValuePair> extraArgs, InjectContext context, object concreteIdentifier)
	{
		if (IsValidating)
		{
			if (_settings.ValidationErrorResponse == ValidationErrorResponses.Throw)
			{
				return InstantiateInternal(concreteType, autoInject, extraArgs, context, concreteIdentifier);
			}
			try
			{
				return InstantiateInternal(concreteType, autoInject, extraArgs, context, concreteIdentifier);
			}
			catch (Exception e)
			{
				Log.ErrorException(e);
				return new ValidationMarker(concreteType, instantiateFailed: true);
			}
		}
		return InstantiateInternal(concreteType, autoInject, extraArgs, context, concreteIdentifier);
	}

	public Component InstantiateComponentExplicit(Type componentType, GameObject gameObject, List<TypeValuePair> extraArgs)
	{
		Assert.That(TypeExtensions.DerivesFrom<Component>(componentType));
		FlushBindings();
		Component component = gameObject.AddComponent(componentType);
		InjectExplicit(component, extraArgs);
		return component;
	}

	public object InstantiateScriptableObjectResourceExplicit(Type scriptableObjectType, string resourcePath, List<TypeValuePair> extraArgs)
	{
		UnityEngine.Object[] array = Resources.LoadAll(resourcePath, scriptableObjectType);
		Assert.That(array.Length > 0, "Could not find resource at path '{0}' with type '{1}'", resourcePath, scriptableObjectType);
		Assert.That(array.Length == 1, "Found multiple scriptable objects at path '{0}' when only 1 was expected with type '{1}'", resourcePath, scriptableObjectType);
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(array.Single());
		InjectExplicit(obj, extraArgs);
		return obj;
	}

	public object InstantiatePrefabResourceForComponentExplicit(Type componentType, string resourcePath, List<TypeValuePair> extraArgs, GameObjectCreationParameters creationInfo)
	{
		return InstantiatePrefabResourceForComponentExplicit(componentType, resourcePath, extraArgs, new InjectContext(this, componentType, null), null, creationInfo);
	}

	public object InstantiatePrefabResourceForComponentExplicit(Type componentType, string resourcePath, List<TypeValuePair> extraArgs, InjectContext context, object concreteIdentifier, GameObjectCreationParameters creationInfo)
	{
		GameObject gameObject = (GameObject)Resources.Load(resourcePath);
		Assert.IsNotNull(gameObject, MiscExtensions.Fmt("Could not find prefab at resource location '{0}'", resourcePath));
		return InstantiatePrefabForComponentExplicit(componentType, gameObject, extraArgs, context, concreteIdentifier, creationInfo);
	}

	public object InstantiatePrefabForComponentExplicit(Type componentType, UnityEngine.Object prefab, List<TypeValuePair> extraArgs)
	{
		return InstantiatePrefabForComponentExplicit(componentType, prefab, extraArgs, GameObjectCreationParameters.Default);
	}

	public object InstantiatePrefabForComponentExplicit(Type componentType, UnityEngine.Object prefab, List<TypeValuePair> extraArgs, GameObjectCreationParameters gameObjectBindInfo)
	{
		return InstantiatePrefabForComponentExplicit(componentType, prefab, extraArgs, new InjectContext(this, componentType, null), null, gameObjectBindInfo);
	}

	public object InstantiatePrefabForComponentExplicit(Type componentType, UnityEngine.Object prefab, List<TypeValuePair> extraArgs, InjectContext context, object concreteIdentifier, GameObjectCreationParameters gameObjectBindInfo)
	{
		Assert.That(!AssertOnNewGameObjects, "Given DiContainer does not support creating new game objects");
		FlushBindings();
		Assert.That(TypeExtensions.IsInterface(componentType) || TypeExtensions.DerivesFrom<Component>(componentType), "Expected type '{0}' to derive from UnityEngine.Component", componentType);
		bool shouldMakeActive;
		GameObject gameObject = CreateAndParentPrefab(prefab, gameObjectBindInfo, context, out shouldMakeActive);
		Component result = InjectGameObjectForComponentExplicit(gameObject, componentType, extraArgs, context, concreteIdentifier);
		if (shouldMakeActive && !IsValidating)
		{
			gameObject.SetActive(value: true);
		}
		return result;
	}

	public void BindExecutionOrder<T>(int order)
	{
		BindExecutionOrder(typeof(T), order);
	}

	public void BindExecutionOrder(Type type, int order)
	{
		Assert.That(TypeExtensions.DerivesFrom<ITickable>(type) || TypeExtensions.DerivesFrom<IInitializable>(type) || TypeExtensions.DerivesFrom<IDisposable>(type) || TypeExtensions.DerivesFrom<ILateDisposable>(type) || TypeExtensions.DerivesFrom<IFixedTickable>(type) || TypeExtensions.DerivesFrom<ILateTickable>(type) || TypeExtensions.DerivesFrom<IPoolable>(type), "Expected type '{0}' to derive from one or more of the following interfaces: ITickable, IInitializable, ILateTickable, IFixedTickable, IDisposable, ILateDisposable", type);
		if (TypeExtensions.DerivesFrom<ITickable>(type))
		{
			BindTickableExecutionOrder(type, order);
		}
		if (TypeExtensions.DerivesFrom<IInitializable>(type))
		{
			BindInitializableExecutionOrder(type, order);
		}
		if (TypeExtensions.DerivesFrom<IDisposable>(type))
		{
			BindDisposableExecutionOrder(type, order);
		}
		if (TypeExtensions.DerivesFrom<ILateDisposable>(type))
		{
			BindLateDisposableExecutionOrder(type, order);
		}
		if (TypeExtensions.DerivesFrom<IFixedTickable>(type))
		{
			BindFixedTickableExecutionOrder(type, order);
		}
		if (TypeExtensions.DerivesFrom<ILateTickable>(type))
		{
			BindLateTickableExecutionOrder(type, order);
		}
		if (TypeExtensions.DerivesFrom<IPoolable>(type))
		{
			BindPoolableExecutionOrder(type, order);
		}
	}

	public CopyNonLazyBinder BindTickableExecutionOrder<T>(int order) where T : ITickable
	{
		return BindTickableExecutionOrder(typeof(T), order);
	}

	public CopyNonLazyBinder BindTickableExecutionOrder(Type type, int order)
	{
		Assert.That(TypeExtensions.DerivesFrom<ITickable>(type), "Expected type '{0}' to derive from ITickable", type);
		return BindInstance(ValuePair.New(type, order)).WhenInjectedInto<TickableManager>();
	}

	public CopyNonLazyBinder BindInitializableExecutionOrder<T>(int order) where T : IInitializable
	{
		return BindInitializableExecutionOrder(typeof(T), order);
	}

	public CopyNonLazyBinder BindInitializableExecutionOrder(Type type, int order)
	{
		Assert.That(TypeExtensions.DerivesFrom<IInitializable>(type), "Expected type '{0}' to derive from IInitializable", type);
		return BindInstance(ValuePair.New(type, order)).WhenInjectedInto<InitializableManager>();
	}

	public CopyNonLazyBinder BindDisposableExecutionOrder<T>(int order) where T : IDisposable
	{
		return BindDisposableExecutionOrder(typeof(T), order);
	}

	public CopyNonLazyBinder BindLateDisposableExecutionOrder<T>(int order) where T : ILateDisposable
	{
		return BindLateDisposableExecutionOrder(typeof(T), order);
	}

	public CopyNonLazyBinder BindDisposableExecutionOrder(Type type, int order)
	{
		Assert.That(TypeExtensions.DerivesFrom<IDisposable>(type), "Expected type '{0}' to derive from IDisposable", type);
		return BindInstance(ValuePair.New(type, order)).WhenInjectedInto<DisposableManager>();
	}

	public CopyNonLazyBinder BindLateDisposableExecutionOrder(Type type, int order)
	{
		Assert.That(TypeExtensions.DerivesFrom<ILateDisposable>(type), "Expected type '{0}' to derive from ILateDisposable", type);
		return BindInstance(ValuePair.New(type, order)).WithId("Late").WhenInjectedInto<DisposableManager>();
	}

	public CopyNonLazyBinder BindFixedTickableExecutionOrder<T>(int order) where T : IFixedTickable
	{
		return BindFixedTickableExecutionOrder(typeof(T), order);
	}

	public CopyNonLazyBinder BindFixedTickableExecutionOrder(Type type, int order)
	{
		Assert.That(TypeExtensions.DerivesFrom<IFixedTickable>(type), "Expected type '{0}' to derive from IFixedTickable", type);
		return Bind<ValuePair<Type, int>>().WithId("Fixed").FromInstance(ValuePair.New(type, order)).WhenInjectedInto<TickableManager>();
	}

	public CopyNonLazyBinder BindLateTickableExecutionOrder<T>(int order) where T : ILateTickable
	{
		return BindLateTickableExecutionOrder(typeof(T), order);
	}

	public CopyNonLazyBinder BindLateTickableExecutionOrder(Type type, int order)
	{
		Assert.That(TypeExtensions.DerivesFrom<ILateTickable>(type), "Expected type '{0}' to derive from ILateTickable", type);
		return Bind<ValuePair<Type, int>>().WithId("Late").FromInstance(ValuePair.New(type, order)).WhenInjectedInto<TickableManager>();
	}

	public CopyNonLazyBinder BindPoolableExecutionOrder<T>(int order) where T : IPoolable
	{
		return BindPoolableExecutionOrder(typeof(T), order);
	}

	public CopyNonLazyBinder BindPoolableExecutionOrder(Type type, int order)
	{
		Assert.That(TypeExtensions.DerivesFrom<IPoolable>(type), "Expected type '{0}' to derive from IPoolable", type);
		return Bind<ValuePair<Type, int>>().FromInstance(ValuePair.New(type, order)).WhenInjectedInto<PoolableManager>();
	}
}
