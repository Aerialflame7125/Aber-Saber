using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject;

[NoReflectionBaking]
public class SubContainerCreatorByMethod : SubContainerCreatorByMethodBase
{
	private readonly Action<DiContainer> _installMethod;

	public SubContainerCreatorByMethod(DiContainer container, SubContainerCreatorBindInfo containerBindInfo, Action<DiContainer> installMethod)
		: base(container, containerBindInfo)
	{
		_installMethod = installMethod;
	}

	public override DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext context)
	{
		Assert.IsEmpty(args);
		DiContainer diContainer = CreateEmptySubContainer();
		_installMethod(diContainer);
		diContainer.ResolveRoots();
		return diContainer;
	}
}
[NoReflectionBaking]
public class SubContainerCreatorByMethod<TParam1> : SubContainerCreatorByMethodBase
{
	private readonly Action<DiContainer, TParam1> _installMethod;

	public SubContainerCreatorByMethod(DiContainer container, SubContainerCreatorBindInfo containerBindInfo, Action<DiContainer, TParam1> installMethod)
		: base(container, containerBindInfo)
	{
		_installMethod = installMethod;
	}

	public override DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext context)
	{
		Assert.IsEqual(args.Count, 1);
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		DiContainer diContainer = CreateEmptySubContainer();
		_installMethod(diContainer, (TParam1)args[0].Value);
		diContainer.ResolveRoots();
		return diContainer;
	}
}
[NoReflectionBaking]
public class SubContainerCreatorByMethod<TParam1, TParam2> : SubContainerCreatorByMethodBase
{
	private readonly Action<DiContainer, TParam1, TParam2> _installMethod;

	public SubContainerCreatorByMethod(DiContainer container, SubContainerCreatorBindInfo containerBindInfo, Action<DiContainer, TParam1, TParam2> installMethod)
		: base(container, containerBindInfo)
	{
		_installMethod = installMethod;
	}

	public override DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext context)
	{
		Assert.IsEqual(args.Count, 2);
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		DiContainer diContainer = CreateEmptySubContainer();
		_installMethod(diContainer, (TParam1)args[0].Value, (TParam2)args[1].Value);
		diContainer.ResolveRoots();
		return diContainer;
	}
}
[NoReflectionBaking]
public class SubContainerCreatorByMethod<TParam1, TParam2, TParam3> : SubContainerCreatorByMethodBase
{
	private readonly Action<DiContainer, TParam1, TParam2, TParam3> _installMethod;

	public SubContainerCreatorByMethod(DiContainer container, SubContainerCreatorBindInfo containerBindInfo, Action<DiContainer, TParam1, TParam2, TParam3> installMethod)
		: base(container, containerBindInfo)
	{
		_installMethod = installMethod;
	}

	public override DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext context)
	{
		Assert.IsEqual(args.Count, 3);
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam3>(args[2].Type));
		DiContainer diContainer = CreateEmptySubContainer();
		_installMethod(diContainer, (TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value);
		diContainer.ResolveRoots();
		return diContainer;
	}
}
[NoReflectionBaking]
public class SubContainerCreatorByMethod<TParam1, TParam2, TParam3, TParam4> : SubContainerCreatorByMethodBase
{
	private readonly Action<DiContainer, TParam1, TParam2, TParam3, TParam4> _installMethod;

	public SubContainerCreatorByMethod(DiContainer container, SubContainerCreatorBindInfo containerBindInfo, Action<DiContainer, TParam1, TParam2, TParam3, TParam4> installMethod)
		: base(container, containerBindInfo)
	{
		_installMethod = installMethod;
	}

	public override DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext context)
	{
		Assert.IsEqual(args.Count, 4);
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam3>(args[2].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam4>(args[3].Type));
		DiContainer diContainer = CreateEmptySubContainer();
		_installMethod(diContainer, (TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value);
		diContainer.ResolveRoots();
		return diContainer;
	}
}
[NoReflectionBaking]
public class SubContainerCreatorByMethod<TParam1, TParam2, TParam3, TParam4, TParam5> : SubContainerCreatorByMethodBase
{
	private readonly Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5> _installMethod;

	public SubContainerCreatorByMethod(DiContainer container, SubContainerCreatorBindInfo containerBindInfo, Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5> installMethod)
		: base(container, containerBindInfo)
	{
		_installMethod = installMethod;
	}

	public override DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext context)
	{
		Assert.IsEqual(args.Count, 5);
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam3>(args[2].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam4>(args[3].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam5>(args[4].Type));
		DiContainer diContainer = CreateEmptySubContainer();
		_installMethod(diContainer, (TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value, (TParam5)args[4].Value);
		diContainer.ResolveRoots();
		return diContainer;
	}
}
[NoReflectionBaking]
public class SubContainerCreatorByMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> : SubContainerCreatorByMethodBase
{
	private readonly Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> _installMethod;

	public SubContainerCreatorByMethod(DiContainer container, SubContainerCreatorBindInfo containerBindInfo, Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> installMethod)
		: base(container, containerBindInfo)
	{
		_installMethod = installMethod;
	}

	public override DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext context)
	{
		Assert.IsEqual(args.Count, 5);
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam3>(args[2].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam4>(args[3].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam5>(args[4].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam6>(args[5].Type));
		DiContainer diContainer = CreateEmptySubContainer();
		_installMethod(diContainer, (TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value, (TParam5)args[4].Value, (TParam6)args[5].Value);
		diContainer.ResolveRoots();
		return diContainer;
	}
}
[NoReflectionBaking]
public class SubContainerCreatorByMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10> : SubContainerCreatorByMethodBase
{
	private readonly Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10> _installMethod;

	public SubContainerCreatorByMethod(DiContainer container, SubContainerCreatorBindInfo containerBindInfo, Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10> installMethod)
		: base(container, containerBindInfo)
	{
		_installMethod = installMethod;
	}

	public override DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext context)
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
		DiContainer diContainer = CreateEmptySubContainer();
		_installMethod(diContainer, (TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value, (TParam5)args[4].Value, (TParam6)args[5].Value, (TParam7)args[6].Value, (TParam8)args[7].Value, (TParam9)args[8].Value, (TParam10)args[9].Value);
		diContainer.ResolveRoots();
		return diContainer;
	}
}
