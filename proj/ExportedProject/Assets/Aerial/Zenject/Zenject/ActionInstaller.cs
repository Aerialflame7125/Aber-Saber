using System;
using Zenject.Internal;

namespace Zenject;

public class ActionInstaller : Installer<ActionInstaller>
{
	private readonly Action<DiContainer> _installMethod;

	public ActionInstaller(Action<DiContainer> installMethod)
	{
		_installMethod = installMethod;
	}

	public override void InstallBindings()
	{
		_installMethod(base.Container);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new ActionInstaller((Action<DiContainer>)P_0[0]);
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(ActionInstaller), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[1]
		{
			new InjectableInfo(optional: false, null, "installMethod", typeof(Action<DiContainer>), null, InjectSources.Any)
		}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
