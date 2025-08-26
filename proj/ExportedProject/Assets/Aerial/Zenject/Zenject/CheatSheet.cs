using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject.Internal;

namespace Zenject;

public class CheatSheet : Installer<CheatSheet>
{
	public class Norf
	{
		[Inject(Id = "FooA")]
		public string Foo;

		private static object __zenCreate(object[] P_0)
		{
			return new Norf();
		}

		private static void __zenFieldSetter0(object P_0, object P_1)
		{
			((Norf)P_0).Foo = (string)P_1;
		}

		[Preserve]
		private static InjectTypeInfo __zenCreateInjectTypeInfo()
		{
			return new InjectTypeInfo(typeof(Norf), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[1]
			{
				new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter0, new InjectableInfo(optional: false, "FooA", "Foo", typeof(string), null, InjectSources.Any))
			});
		}
	}

	public class Qux
	{
		[Inject(Id = "FooB")]
		public string Foo;

		private static object __zenCreate(object[] P_0)
		{
			return new Qux();
		}

		private static void __zenFieldSetter0(object P_0, object P_1)
		{
			((Qux)P_0).Foo = (string)P_1;
		}

		[Preserve]
		private static InjectTypeInfo __zenCreateInjectTypeInfo()
		{
			return new InjectTypeInfo(typeof(Qux), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[1]
			{
				new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter0, new InjectableInfo(optional: false, "FooB", "Foo", typeof(string), null, InjectSources.Any))
			});
		}
	}

	public class Norf2
	{
		[Inject]
		public Foo Foo;

		private static object __zenCreate(object[] P_0)
		{
			return new Norf2();
		}

		private static void __zenFieldSetter0(object P_0, object P_1)
		{
			((Norf2)P_0).Foo = (Foo)P_1;
		}

		[Preserve]
		private static InjectTypeInfo __zenCreateInjectTypeInfo()
		{
			return new InjectTypeInfo(typeof(Norf2), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[1]
			{
				new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter0, new InjectableInfo(optional: false, null, "Foo", typeof(Foo), null, InjectSources.Any))
			});
		}
	}

	public class Qux2
	{
		[Inject]
		public Foo Foo;

		[Inject(Id = "FooA")]
		public Foo Foo2;

		private static object __zenCreate(object[] P_0)
		{
			return new Qux2();
		}

		private static void __zenFieldSetter0(object P_0, object P_1)
		{
			((Qux2)P_0).Foo = (Foo)P_1;
		}

		private static void __zenFieldSetter1(object P_0, object P_1)
		{
			((Qux2)P_0).Foo2 = (Foo)P_1;
		}

		[Preserve]
		private static InjectTypeInfo __zenCreateInjectTypeInfo()
		{
			return new InjectTypeInfo(typeof(Qux2), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[2]
			{
				new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter0, new InjectableInfo(optional: false, null, "Foo", typeof(Foo), null, InjectSources.Any)),
				new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter1, new InjectableInfo(optional: false, "FooA", "Foo2", typeof(Foo), null, InjectSources.Any))
			});
		}
	}

	public class FooInstaller : Installer<FooInstaller>
	{
		public FooInstaller(string foo)
		{
		}

		public override void InstallBindings()
		{
		}

		private static object __zenCreate(object[] P_0)
		{
			return new FooInstaller((string)P_0[0]);
		}

		[Preserve]
		private static InjectTypeInfo __zenCreateInjectTypeInfo()
		{
			return new InjectTypeInfo(typeof(FooInstaller), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[1]
			{
				new InjectableInfo(optional: false, null, "foo", typeof(string), null, InjectSources.Any)
			}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
		}
	}

	public class FooInstallerWithArgs : Installer<string, FooInstallerWithArgs>
	{
		public FooInstallerWithArgs(string foo)
		{
		}

		public override void InstallBindings()
		{
		}

		private static object __zenCreate(object[] P_0)
		{
			return new FooInstallerWithArgs((string)P_0[0]);
		}

		[Preserve]
		private static InjectTypeInfo __zenCreateInjectTypeInfo()
		{
			return new InjectTypeInfo(typeof(FooInstallerWithArgs), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[1]
			{
				new InjectableInfo(optional: false, null, "foo", typeof(string), null, InjectSources.Any)
			}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
		}
	}

	public interface IFoo2
	{
	}

	public interface IFoo
	{
	}

	public interface IBar : IFoo
	{
	}

	public class Foo : MonoBehaviour, IFoo, IFoo2, IBar
	{
		public Bar GetBar()
		{
			return new Bar();
		}

		public string GetTitle()
		{
			return "title";
		}

		[Preserve]
		private static InjectTypeInfo __zenCreateInjectTypeInfo()
		{
			return new InjectTypeInfo(typeof(Foo), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
		}
	}

	public class Foo1 : IFoo
	{
		private static object __zenCreate(object[] P_0)
		{
			return new Foo1();
		}

		[Preserve]
		private static InjectTypeInfo __zenCreateInjectTypeInfo()
		{
			return new InjectTypeInfo(typeof(Foo1), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
		}
	}

	public class Foo2 : IFoo
	{
		private static object __zenCreate(object[] P_0)
		{
			return new Foo2();
		}

		[Preserve]
		private static InjectTypeInfo __zenCreateInjectTypeInfo()
		{
			return new InjectTypeInfo(typeof(Foo2), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
		}
	}

	public class Foo3 : IFoo
	{
		private static object __zenCreate(object[] P_0)
		{
			return new Foo3();
		}

		[Preserve]
		private static InjectTypeInfo __zenCreateInjectTypeInfo()
		{
			return new InjectTypeInfo(typeof(Foo3), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
		}
	}

	public class Baz
	{
		private static object __zenCreate(object[] P_0)
		{
			return new Baz();
		}

		[Preserve]
		private static InjectTypeInfo __zenCreateInjectTypeInfo()
		{
			return new InjectTypeInfo(typeof(Baz), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
		}
	}

	public class Gui
	{
		private static object __zenCreate(object[] P_0)
		{
			return new Gui();
		}

		[Preserve]
		private static InjectTypeInfo __zenCreateInjectTypeInfo()
		{
			return new InjectTypeInfo(typeof(Gui), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
		}
	}

	public class Bar : IBar, IFoo
	{
		public Foo Foo => null;

		private static object __zenCreate(object[] P_0)
		{
			return new Bar();
		}

		[Preserve]
		private static InjectTypeInfo __zenCreateInjectTypeInfo()
		{
			return new InjectTypeInfo(typeof(Bar), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
		}
	}

	public override void InstallBindings()
	{
		base.Container.Bind<Foo>().AsTransient();
		base.Container.Bind<IFoo>().To<Foo>().AsTransient();
		base.Container.Bind(typeof(IFoo)).To(typeof(Foo)).AsTransient();
		base.Container.Bind<Foo>().AsSingle();
		base.Container.Bind<IFoo>().To<Foo>().AsSingle();
		base.Container.Bind(typeof(Foo), typeof(IFoo), typeof(IFoo2)).To<Foo>().AsSingle();
		base.Container.BindInterfacesAndSelfTo<Foo>().AsSingle();
		base.Container.BindInterfacesTo<Foo>().AsSingle();
		base.Container.Bind<Foo>().FromInstance(new Foo());
		base.Container.BindInstance(new Foo());
		base.Container.BindInstances(new Foo(), new Bar());
		base.Container.Bind<int>().FromInstance(10);
		base.Container.Bind<bool>().FromInstance(instance: false);
		base.Container.BindInstance(10);
		base.Container.BindInstance(instance: false);
		base.Container.BindInstance(10).WhenInjectedInto<Foo>();
		base.Container.Bind<Foo>().FromMethod(GetFoo);
		base.Container.Bind<IFoo>().FromMethod(GetRandomFoo);
		base.Container.Bind<Foo>().FromMethod((InjectContext ctx) => new Foo());
		base.Container.Bind<Foo>().FromMethod((InjectContext ctx) => ctx.Container.Instantiate<Foo>());
		InstallMore();
	}

	private Foo GetFoo(InjectContext ctx)
	{
		return new Foo();
	}

	private IFoo GetRandomFoo(InjectContext ctx)
	{
		return Random.Range(0, 3) switch
		{
			0 => ctx.Container.Instantiate<Foo1>(), 
			1 => ctx.Container.Instantiate<Foo2>(), 
			_ => ctx.Container.Instantiate<Foo3>(), 
		};
	}

	private void InstallMore()
	{
		base.Container.Bind<Foo>().AsSingle();
		base.Container.Bind<Bar>().FromResolveGetter((Foo foo) => foo.GetBar());
		base.Container.Bind<string>().FromResolveGetter((Foo foo) => foo.GetTitle());
		base.Container.Bind<Foo>().FromNewComponentOnNewGameObject().AsSingle();
		base.Container.Bind<Foo>().FromNewComponentOnNewGameObject().WithGameObjectName("Foo1")
			.AsSingle();
		base.Container.Bind<IFoo>().To<Foo>().FromNewComponentOnNewGameObject()
			.AsSingle();
		GameObject prefab = null;
		base.Container.Bind<Foo>().FromComponentInNewPrefab(prefab).AsSingle();
		base.Container.Bind<IFoo>().To<Foo>().FromComponentInNewPrefab(prefab)
			.AsSingle();
		base.Container.Bind(typeof(Foo), typeof(Bar)).FromComponentInNewPrefab(prefab).AsSingle();
		base.Container.Bind<Foo>().FromComponentInNewPrefab(prefab).AsTransient();
		base.Container.Bind<IFoo>().To<Foo>().FromComponentInNewPrefab(prefab);
		base.Container.Bind<string>().WithId("PlayerName").FromInstance("name of the player");
		base.Container.BindInstance("name of the player").WithId("PlayerName");
		base.Container.BindInstance("foo").WithId("FooA");
		base.Container.BindInstance("asdf").WithId("FooB");
		InstallMore2();
	}

	public void InstallMore2()
	{
		base.Container.Bind<Foo>().AsCached();
		base.Container.Bind<Foo>().WithId("FooA").AsCached();
		base.Container.Bind<Foo>().WithId("FooA").AsCached();
		InstallMore3();
	}

	public void InstallMore3()
	{
		base.Container.Bind<Foo>().AsSingle().WhenInjectedInto<Bar>();
		base.Container.Bind<IFoo>().To<Foo1>().AsSingle()
			.WhenInjectedInto<Bar>();
		base.Container.Bind<IFoo>().To<Foo2>().AsSingle()
			.WhenInjectedInto<Qux>();
		base.Container.Bind<IFoo>().To<Foo1>().AsSingle();
		base.Container.Bind<IFoo>().To<Foo2>().AsSingle()
			.WhenInjectedInto<Qux>();
		base.Container.Bind<Foo>().AsSingle().WhenInjectedInto(typeof(Bar), typeof(Qux), typeof(Baz));
		base.Container.BindInstance("my game").WithId("Title").WhenInjectedInto<Gui>();
		base.Container.BindInstance(5).WhenInjectedInto<Gui>();
		base.Container.BindInstance(5f).When((InjectContext ctx) => ctx.ObjectType == typeof(Gui) && ctx.MemberName == "width");
		base.Container.Bind<IFoo>().To<Foo>().AsTransient()
			.When((InjectContext ctx) => ctx.AllObjectTypes.Contains(typeof(Bar)));
		Foo foo = new Foo();
		Foo foo2 = new Foo();
		base.Container.Bind<Bar>().WithId("Bar1").AsCached();
		base.Container.Bind<Bar>().WithId("Bar2").AsCached();
		base.Container.BindInstance(foo).When((InjectContext c) => c.ParentContexts.Where((InjectContext x) => x.MemberType == typeof(Bar) && object.Equals(x.Identifier, "Bar1")).Any());
		base.Container.BindInstance(foo2).When((InjectContext c) => c.ParentContexts.Where((InjectContext x) => x.MemberType == typeof(Bar) && object.Equals(x.Identifier, "Bar2")).Any());
		Assert.That(base.Container.ResolveId<Bar>("Bar1").Foo == foo);
		Assert.That(base.Container.ResolveId<Bar>("Bar2").Foo == foo2);
		GameObject prefab = null;
		base.Container.Bind<Foo>().FromComponentInNewPrefab(prefab).AsSingle();
		base.Container.Bind<IBar>().To<Foo>().FromResolve();
		base.Container.Bind<IFoo>().To<IBar>().FromResolve();
		base.Container.Bind(typeof(Foo), typeof(IBar), typeof(IFoo)).To<Foo>().FromComponentInNewPrefab(prefab)
			.AsSingle();
		InstallMore4();
	}

	private void InstallMore4()
	{
		Installer<FooInstaller>.Install(base.Container);
		base.Container.BindInstance("foo").WhenInjectedInto<FooInstaller>();
		Installer<FooInstaller>.Install(base.Container);
		Installer<string, FooInstallerWithArgs>.Install(base.Container, "foo");
		Foo injectable = new Foo();
		base.Container.Inject(injectable);
		base.Container.Resolve<IFoo>();
		base.Container.TryResolve<IFoo>();
		base.Container.BindInstance(new Foo());
		base.Container.BindInstance(new Foo());
		List<IFoo> list = base.Container.ResolveAll<IFoo>();
		base.Container.Instantiate<Foo>();
		GameObject prefab = null;
		GameObject prefab2 = null;
		GameObject gameObject = base.Container.InstantiatePrefab(prefab);
		Foo foo = base.Container.InstantiatePrefabForComponent<Foo>(prefab2);
		Foo foo2 = base.Container.InstantiateComponent<Foo>(gameObject);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new CheatSheet();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(CheatSheet), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
