using Zenject.Internal;

namespace Zenject;

public class ZenjectManagersInstaller : Installer<ZenjectManagersInstaller>
{
	public override void InstallBindings()
	{
		base.Container.Bind(typeof(TickableManager), typeof(InitializableManager), typeof(DisposableManager)).ToSelf().AsSingle()
			.CopyIntoAllSubContainers();
	}

	private static object __zenCreate(object[] P_0)
	{
		return new ZenjectManagersInstaller();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(ZenjectManagersInstaller), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
