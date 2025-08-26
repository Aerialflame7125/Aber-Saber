using Zenject.Internal;

namespace Zenject;

public abstract class InstallerBase : IInstaller
{
	[Inject]
	private DiContainer _container;

	protected DiContainer Container => _container;

	public virtual bool IsEnabled => true;

	public abstract void InstallBindings();

	private static void __zenFieldSetter0(object P_0, object P_1)
	{
		((InstallerBase)P_0)._container = (DiContainer)P_1;
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(InstallerBase), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[1]
		{
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter0, new InjectableInfo(optional: false, null, "_container", typeof(DiContainer), null, InjectSources.Any))
		});
	}
}
