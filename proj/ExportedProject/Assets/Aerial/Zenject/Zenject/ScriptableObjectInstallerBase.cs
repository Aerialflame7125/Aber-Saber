using System;
using UnityEngine;
using Zenject.Internal;

namespace Zenject;

public class ScriptableObjectInstallerBase : ScriptableObject, IInstaller
{
	[Inject]
	private DiContainer _container;

	bool IInstaller.IsEnabled => true;

	protected DiContainer Container => _container;

	public virtual void InstallBindings()
	{
		throw new NotImplementedException();
	}

	private static object __zenCreate(object[] P_0)
	{
		return new ScriptableObjectInstallerBase();
	}

	private static void __zenFieldSetter0(object P_0, object P_1)
	{
		((ScriptableObjectInstallerBase)P_0)._container = (DiContainer)P_1;
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(ScriptableObjectInstallerBase), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[1]
		{
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter0, new InjectableInfo(optional: false, null, "_container", typeof(DiContainer), null, InjectSources.Any))
		});
	}
}
