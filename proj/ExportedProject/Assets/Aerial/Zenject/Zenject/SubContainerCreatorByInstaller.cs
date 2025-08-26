using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using Zenject.Internal;

namespace Zenject;

[NoReflectionBaking]
public class SubContainerCreatorByInstaller : ISubContainerCreator
{
	private readonly Type _installerType;

	private readonly DiContainer _container;

	private readonly List<TypeValuePair> _extraArgs;

	private readonly SubContainerCreatorBindInfo _containerBindInfo;

	public SubContainerCreatorByInstaller(DiContainer container, SubContainerCreatorBindInfo containerBindInfo, Type installerType, IEnumerable<TypeValuePair> extraArgs)
	{
		_installerType = installerType;
		_container = container;
		_extraArgs = extraArgs.ToList();
		_containerBindInfo = containerBindInfo;
		Assert.That(TypeExtensions.DerivesFrom<InstallerBase>(installerType), "Invalid installer type given during bind command.  Expected type '{0}' to derive from 'Installer<>'", installerType);
	}

	public SubContainerCreatorByInstaller(DiContainer container, SubContainerCreatorBindInfo containerBindInfo, Type installerType)
		: this(container, containerBindInfo, installerType, new List<TypeValuePair>())
	{
	}

	public DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext context)
	{
		DiContainer diContainer = _container.CreateSubContainer();
		SubContainerCreatorUtil.ApplyBindSettings(_containerBindInfo, diContainer);
		List<TypeValuePair> list = ZenPools.SpawnList<TypeValuePair>();
		MiscExtensions.AllocFreeAddRange(list, _extraArgs);
		MiscExtensions.AllocFreeAddRange(list, args);
		InstallerBase installerBase = (InstallerBase)diContainer.InstantiateExplicit(_installerType, list);
		ZenPools.DespawnList(list);
		installerBase.InstallBindings();
		diContainer.ResolveRoots();
		return diContainer;
	}
}
