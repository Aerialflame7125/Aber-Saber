using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject;

[NoReflectionBaking]
public class SubContainerCreatorByNewPrefabMethod : SubContainerCreatorByNewPrefabDynamicContext
{
	private readonly Action<DiContainer> _installerMethod;

	public SubContainerCreatorByNewPrefabMethod(DiContainer container, IPrefabProvider prefabProvider, GameObjectCreationParameters gameObjectBindInfo, Action<DiContainer> installerMethod)
		: base(container, prefabProvider, gameObjectBindInfo)
	{
		_installerMethod = installerMethod;
	}

	protected override void AddInstallers(List<TypeValuePair> args, GameObjectContext context)
	{
		Assert.That(LinqExtensions.IsEmpty(args));
		context.AddNormalInstaller(new ActionInstaller(_installerMethod));
	}
}
[NoReflectionBaking]
public class SubContainerCreatorByNewPrefabMethod<TParam1> : SubContainerCreatorByNewPrefabDynamicContext
{
	private readonly Action<DiContainer, TParam1> _installerMethod;

	public SubContainerCreatorByNewPrefabMethod(DiContainer container, IPrefabProvider prefabProvider, GameObjectCreationParameters gameObjectBindInfo, Action<DiContainer, TParam1> installerMethod)
		: base(container, prefabProvider, gameObjectBindInfo)
	{
		_installerMethod = installerMethod;
	}

	protected override void AddInstallers(List<TypeValuePair> args, GameObjectContext context)
	{
		Assert.IsEqual(args.Count, 1);
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		context.AddNormalInstaller(new ActionInstaller(delegate(DiContainer subContainer)
		{
			_installerMethod(subContainer, (TParam1)args[0].Value);
		}));
	}
}
[NoReflectionBaking]
public class SubContainerCreatorByNewPrefabMethod<TParam1, TParam2> : SubContainerCreatorByNewPrefabDynamicContext
{
	private readonly Action<DiContainer, TParam1, TParam2> _installerMethod;

	public SubContainerCreatorByNewPrefabMethod(DiContainer container, IPrefabProvider prefabProvider, GameObjectCreationParameters gameObjectBindInfo, Action<DiContainer, TParam1, TParam2> installerMethod)
		: base(container, prefabProvider, gameObjectBindInfo)
	{
		_installerMethod = installerMethod;
	}

	protected override void AddInstallers(List<TypeValuePair> args, GameObjectContext context)
	{
		Assert.IsEqual(args.Count, 2);
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		context.AddNormalInstaller(new ActionInstaller(delegate(DiContainer subContainer)
		{
			_installerMethod(subContainer, (TParam1)args[0].Value, (TParam2)args[1].Value);
		}));
	}
}
[NoReflectionBaking]
public class SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3> : SubContainerCreatorByNewPrefabDynamicContext
{
	private readonly Action<DiContainer, TParam1, TParam2, TParam3> _installerMethod;

	public SubContainerCreatorByNewPrefabMethod(DiContainer container, IPrefabProvider prefabProvider, GameObjectCreationParameters gameObjectBindInfo, Action<DiContainer, TParam1, TParam2, TParam3> installerMethod)
		: base(container, prefabProvider, gameObjectBindInfo)
	{
		_installerMethod = installerMethod;
	}

	protected override void AddInstallers(List<TypeValuePair> args, GameObjectContext context)
	{
		Assert.IsEqual(args.Count, 3);
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam3>(args[2].Type));
		context.AddNormalInstaller(new ActionInstaller(delegate(DiContainer subContainer)
		{
			_installerMethod(subContainer, (TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value);
		}));
	}
}
[NoReflectionBaking]
public class SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4> : SubContainerCreatorByNewPrefabDynamicContext
{
	private readonly Action<DiContainer, TParam1, TParam2, TParam3, TParam4> _installerMethod;

	public SubContainerCreatorByNewPrefabMethod(DiContainer container, IPrefabProvider prefabProvider, GameObjectCreationParameters gameObjectBindInfo, Action<DiContainer, TParam1, TParam2, TParam3, TParam4> installerMethod)
		: base(container, prefabProvider, gameObjectBindInfo)
	{
		_installerMethod = installerMethod;
	}

	protected override void AddInstallers(List<TypeValuePair> args, GameObjectContext context)
	{
		Assert.IsEqual(args.Count, 4);
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam3>(args[2].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam4>(args[3].Type));
		context.AddNormalInstaller(new ActionInstaller(delegate(DiContainer subContainer)
		{
			_installerMethod(subContainer, (TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value);
		}));
	}
}
[NoReflectionBaking]
public class SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4, TParam5> : SubContainerCreatorByNewPrefabDynamicContext
{
	private readonly Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5> _installerMethod;

	public SubContainerCreatorByNewPrefabMethod(DiContainer container, IPrefabProvider prefabProvider, GameObjectCreationParameters gameObjectBindInfo, Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5> installerMethod)
		: base(container, prefabProvider, gameObjectBindInfo)
	{
		_installerMethod = installerMethod;
	}

	protected override void AddInstallers(List<TypeValuePair> args, GameObjectContext context)
	{
		Assert.IsEqual(args.Count, 5);
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam3>(args[2].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam4>(args[3].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam5>(args[4].Type));
		context.AddNormalInstaller(new ActionInstaller(delegate(DiContainer subContainer)
		{
			_installerMethod(subContainer, (TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value, (TParam5)args[4].Value);
		}));
	}
}
[NoReflectionBaking]
public class SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> : SubContainerCreatorByNewPrefabDynamicContext
{
	private readonly Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> _installerMethod;

	public SubContainerCreatorByNewPrefabMethod(DiContainer container, IPrefabProvider prefabProvider, GameObjectCreationParameters gameObjectBindInfo, Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> installerMethod)
		: base(container, prefabProvider, gameObjectBindInfo)
	{
		_installerMethod = installerMethod;
	}

	protected override void AddInstallers(List<TypeValuePair> args, GameObjectContext context)
	{
		Assert.IsEqual(args.Count, 5);
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam3>(args[2].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam4>(args[3].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam5>(args[4].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam6>(args[5].Type));
		context.AddNormalInstaller(new ActionInstaller(delegate(DiContainer subContainer)
		{
			_installerMethod(subContainer, (TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value, (TParam5)args[4].Value, (TParam6)args[5].Value);
		}));
	}
}
[NoReflectionBaking]
public class SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10> : SubContainerCreatorByNewPrefabDynamicContext
{
	private readonly Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10> _installerMethod;

	public SubContainerCreatorByNewPrefabMethod(DiContainer container, IPrefabProvider prefabProvider, GameObjectCreationParameters gameObjectBindInfo, Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10> installerMethod)
		: base(container, prefabProvider, gameObjectBindInfo)
	{
		_installerMethod = installerMethod;
	}

	protected override void AddInstallers(List<TypeValuePair> args, GameObjectContext context)
	{
		Assert.IsEqual(args.Count, 10);
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam3>(args[2].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam4>(args[3].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam5>(args[4].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam6>(args[5].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam7>(args[6].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam8>(args[7].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam9>(args[8].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam10>(args[9].Type));
		context.AddNormalInstaller(new ActionInstaller(delegate(DiContainer subContainer)
		{
			_installerMethod(subContainer, (TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value, (TParam5)args[4].Value, (TParam6)args[5].Value, (TParam7)args[6].Value, (TParam8)args[7].Value, (TParam9)args[8].Value, (TParam10)args[9].Value);
		}));
	}
}
