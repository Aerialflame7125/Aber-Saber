using Zenject.Internal;

namespace Zenject;

public class ScriptableObjectInstaller : ScriptableObjectInstallerBase
{
	private static object __zenCreate(object[] P_0)
	{
		return new ScriptableObjectInstaller();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(ScriptableObjectInstaller), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class ScriptableObjectInstaller<TDerived> : ScriptableObjectInstaller where TDerived : ScriptableObjectInstaller<TDerived>
{
	public static TDerived InstallFromResource(DiContainer container)
	{
		return InstallFromResource(ScriptableObjectInstallerUtil.GetDefaultResourcePath<TDerived>(), container);
	}

	public static TDerived InstallFromResource(string resourcePath, DiContainer container)
	{
		TDerived val = ScriptableObjectInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
		container.Inject(val);
		val.InstallBindings();
		return val;
	}

	private static object __zenCreate(object[] P_0)
	{
		return new ScriptableObjectInstaller<TDerived>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(ScriptableObjectInstaller<TDerived>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class ScriptableObjectInstaller<TParam1, TDerived> : ScriptableObjectInstallerBase where TDerived : ScriptableObjectInstaller<TParam1, TDerived>
{
	public static TDerived InstallFromResource(DiContainer container, TParam1 p1)
	{
		return InstallFromResource(ScriptableObjectInstallerUtil.GetDefaultResourcePath<TDerived>(), container, p1);
	}

	public static TDerived InstallFromResource(string resourcePath, DiContainer container, TParam1 p1)
	{
		TDerived val = ScriptableObjectInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
		container.InjectExplicit(val, InjectUtil.CreateArgListExplicit(p1));
		val.InstallBindings();
		return val;
	}

	private static object __zenCreate(object[] P_0)
	{
		return new ScriptableObjectInstaller<TParam1, TDerived>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(ScriptableObjectInstaller<TParam1, TDerived>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class ScriptableObjectInstaller<TParam1, TParam2, TDerived> : ScriptableObjectInstallerBase where TDerived : ScriptableObjectInstaller<TParam1, TParam2, TDerived>
{
	public static TDerived InstallFromResource(DiContainer container, TParam1 p1, TParam2 p2)
	{
		return InstallFromResource(ScriptableObjectInstallerUtil.GetDefaultResourcePath<TDerived>(), container, p1, p2);
	}

	public static TDerived InstallFromResource(string resourcePath, DiContainer container, TParam1 p1, TParam2 p2)
	{
		TDerived val = ScriptableObjectInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
		container.InjectExplicit(val, InjectUtil.CreateArgListExplicit(p1, p2));
		val.InstallBindings();
		return val;
	}

	private static object __zenCreate(object[] P_0)
	{
		return new ScriptableObjectInstaller<TParam1, TParam2, TDerived>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(ScriptableObjectInstaller<TParam1, TParam2, TDerived>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class ScriptableObjectInstaller<TParam1, TParam2, TParam3, TDerived> : ScriptableObjectInstallerBase where TDerived : ScriptableObjectInstaller<TParam1, TParam2, TParam3, TDerived>
{
	public static TDerived InstallFromResource(DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3)
	{
		return InstallFromResource(ScriptableObjectInstallerUtil.GetDefaultResourcePath<TDerived>(), container, p1, p2, p3);
	}

	public static TDerived InstallFromResource(string resourcePath, DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3)
	{
		TDerived val = ScriptableObjectInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
		container.InjectExplicit(val, InjectUtil.CreateArgListExplicit(p1, p2, p3));
		val.InstallBindings();
		return val;
	}

	private static object __zenCreate(object[] P_0)
	{
		return new ScriptableObjectInstaller<TParam1, TParam2, TParam3, TDerived>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(ScriptableObjectInstaller<TParam1, TParam2, TParam3, TDerived>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class ScriptableObjectInstaller<TParam1, TParam2, TParam3, TParam4, TDerived> : ScriptableObjectInstallerBase where TDerived : ScriptableObjectInstaller<TParam1, TParam2, TParam3, TParam4, TDerived>
{
	public static TDerived InstallFromResource(DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4)
	{
		return InstallFromResource(ScriptableObjectInstallerUtil.GetDefaultResourcePath<TDerived>(), container, p1, p2, p3, p4);
	}

	public static TDerived InstallFromResource(string resourcePath, DiContainer container, TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4)
	{
		TDerived val = ScriptableObjectInstallerUtil.CreateInstaller<TDerived>(resourcePath, container);
		container.InjectExplicit(val, InjectUtil.CreateArgListExplicit(p1, p2, p3, p4));
		val.InstallBindings();
		return val;
	}

	private static object __zenCreate(object[] P_0)
	{
		return new ScriptableObjectInstaller<TParam1, TParam2, TParam3, TParam4, TDerived>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(ScriptableObjectInstaller<TParam1, TParam2, TParam3, TParam4, TDerived>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
