using Zenject.Internal;

namespace Zenject;

public class MonoInstaller : MonoInstallerBase
{
	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoInstaller), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoInstaller<TDerived> : MonoInstaller where TDerived : MonoInstaller<TDerived>
{
	public static TDerived InstallFromResource(DiContainer container)
	{
		return InstallFromResource(MonoInstallerUtil.GetDefaultResourcePath<TDerived>(), container);
	}

	public static TDerived InstallFromResource(string resourcePath, DiContainer container)
	{
		return InstallFromResource(resourcePath, container, new object[0]);
	}

	public static TDerived InstallFromResource(DiContainer container, object[] extraArgs)
	{
		return InstallFromResource(MonoInstallerUtil.GetDefaultResourcePath<TDerived>(), container, extraArgs);
	}

	public static TDerived InstallFromResource(string resourcePath, DiContainer container, object[] extraArgs)
	{
		TDerived val = MonoInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
		container.Inject(val, extraArgs);
		val.InstallBindings();
		return val;
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoInstaller<TDerived>), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoInstaller<TParam1, TDerived> : MonoInstallerBase where TDerived : MonoInstaller<TParam1, TDerived>
{
	public static TDerived InstallFromResource(DiContainer container, TParam1 p1)
	{
		return InstallFromResource(MonoInstallerUtil.GetDefaultResourcePath<TDerived>(), container, p1);
	}

	public static TDerived InstallFromResource(string resourcePath, DiContainer container, TParam1 p1)
	{
		TDerived val = MonoInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
		container.InjectExplicit(val, InjectUtil.CreateArgListExplicit(p1));
		val.InstallBindings();
		return val;
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoInstaller<TParam1, TDerived>), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoInstaller<TParam1, TParam2, TDerived> : MonoInstallerBase where TDerived : MonoInstaller<TParam1, TParam2, TDerived>
{
	public static TDerived InstallFromResource(DiContainer container, TParam1 p1, TParam2 p2)
	{
		return InstallFromResource(MonoInstallerUtil.GetDefaultResourcePath<TDerived>(), container, p1, p2);
	}

	public static TDerived InstallFromResource(string resourcePath, DiContainer container, TParam1 p1, TParam2 p2)
	{
		TDerived val = MonoInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
		container.InjectExplicit(val, InjectUtil.CreateArgListExplicit(p1, p2));
		val.InstallBindings();
		return val;
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoInstaller<TParam1, TParam2, TDerived>), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoInstaller<TParam1, TParam2, TParam3, TDerived> : MonoInstallerBase where TDerived : MonoInstaller<TParam1, TParam2, TParam3, TDerived>
{
	public static TDerived InstallFromResource(DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3)
	{
		return InstallFromResource(MonoInstallerUtil.GetDefaultResourcePath<TDerived>(), container, p1, p2, p3);
	}

	public static TDerived InstallFromResource(string resourcePath, DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3)
	{
		TDerived val = MonoInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
		container.InjectExplicit(val, InjectUtil.CreateArgListExplicit(p1, p2, p3));
		val.InstallBindings();
		return val;
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoInstaller<TParam1, TParam2, TParam3, TDerived>), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoInstaller<TParam1, TParam2, TParam3, TParam4, TDerived> : MonoInstallerBase where TDerived : MonoInstaller<TParam1, TParam2, TParam3, TParam4, TDerived>
{
	public static TDerived InstallFromResource(DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4)
	{
		return InstallFromResource(MonoInstallerUtil.GetDefaultResourcePath<TDerived>(), container, p1, p2, p3, p4);
	}

	public static TDerived InstallFromResource(string resourcePath, DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4)
	{
		TDerived val = MonoInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
		container.InjectExplicit(val, InjectUtil.CreateArgListExplicit(p1, p2, p3, p4));
		val.InstallBindings();
		return val;
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoInstaller<TParam1, TParam2, TParam3, TParam4, TDerived>), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoInstaller<TParam1, TParam2, TParam3, TParam4, TParam5, TDerived> : MonoInstallerBase where TDerived : MonoInstaller<TParam1, TParam2, TParam3, TParam4, TParam5, TDerived>
{
	public static TDerived InstallFromResource(DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5)
	{
		return InstallFromResource(MonoInstallerUtil.GetDefaultResourcePath<TDerived>(), container, p1, p2, p3, p4, p5);
	}

	public static TDerived InstallFromResource(string resourcePath, DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5)
	{
		TDerived val = MonoInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
		container.InjectExplicit(val, InjectUtil.CreateArgListExplicit(p1, p2, p3, p4, p5));
		val.InstallBindings();
		return val;
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoInstaller<TParam1, TParam2, TParam3, TParam4, TParam5, TDerived>), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
