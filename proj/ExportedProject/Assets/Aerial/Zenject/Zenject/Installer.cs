using Zenject.Internal;

namespace Zenject;

public abstract class Installer : InstallerBase
{
	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(Installer), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public abstract class Installer<TDerived> : InstallerBase where TDerived : Installer<TDerived>
{
	public static void Install(DiContainer container)
	{
		TDerived val = container.Instantiate<TDerived>();
		val.InstallBindings();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(Installer<TDerived>), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public abstract class Installer<TParam1, TDerived> : InstallerBase where TDerived : Installer<TParam1, TDerived>
{
	public static void Install(DiContainer container, TParam1 p1)
	{
		TDerived val = container.InstantiateExplicit<TDerived>(InjectUtil.CreateArgListExplicit(p1));
		val.InstallBindings();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(Installer<TParam1, TDerived>), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public abstract class Installer<TParam1, TParam2, TDerived> : InstallerBase where TDerived : Installer<TParam1, TParam2, TDerived>
{
	public static void Install(DiContainer container, TParam1 p1, TParam2 p2)
	{
		TDerived val = container.InstantiateExplicit<TDerived>(InjectUtil.CreateArgListExplicit(p1, p2));
		val.InstallBindings();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(Installer<TParam1, TParam2, TDerived>), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public abstract class Installer<TParam1, TParam2, TParam3, TDerived> : InstallerBase where TDerived : Installer<TParam1, TParam2, TParam3, TDerived>
{
	public static void Install(DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3)
	{
		TDerived val = container.InstantiateExplicit<TDerived>(InjectUtil.CreateArgListExplicit(p1, p2, p3));
		val.InstallBindings();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(Installer<TParam1, TParam2, TParam3, TDerived>), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public abstract class Installer<TParam1, TParam2, TParam3, TParam4, TDerived> : InstallerBase where TDerived : Installer<TParam1, TParam2, TParam3, TParam4, TDerived>
{
	public static void Install(DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4)
	{
		TDerived val = container.InstantiateExplicit<TDerived>(InjectUtil.CreateArgListExplicit(p1, p2, p3, p4));
		val.InstallBindings();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(Installer<TParam1, TParam2, TParam3, TParam4, TDerived>), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public abstract class Installer<TParam1, TParam2, TParam3, TParam4, TParam5, TDerived> : InstallerBase where TDerived : Installer<TParam1, TParam2, TParam3, TParam4, TParam5, TDerived>
{
	public static void Install(DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5)
	{
		TDerived val = container.InstantiateExplicit<TDerived>(InjectUtil.CreateArgListExplicit(p1, p2, p3, p4, p5));
		val.InstallBindings();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(Installer<TParam1, TParam2, TParam3, TParam4, TParam5, TDerived>), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
