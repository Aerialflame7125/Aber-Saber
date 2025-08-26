using System;
using System.Diagnostics;
using UnityEngine;
using Zenject.Internal;

namespace Zenject;

[DebuggerStepThrough]
public class MonoInstallerBase : MonoBehaviour, IInstaller
{
	[Inject]
	protected DiContainer Container { get; set; }

	public virtual bool IsEnabled => base.enabled;

	public virtual void Start()
	{
	}

	public virtual void InstallBindings()
	{
		throw new NotImplementedException();
	}

	private static void __zenPropertySetter0(object P_0, object P_1)
	{
		((MonoInstallerBase)P_0).Container = (DiContainer)P_1;
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoInstallerBase), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[1]
		{
			new InjectTypeInfo.InjectMemberInfo(__zenPropertySetter0, new InjectableInfo(optional: false, null, "Container", typeof(DiContainer), null, InjectSources.Any))
		});
	}
}
