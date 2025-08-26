using System;
using Zenject.Internal;

namespace Zenject;

public class SceneContextRegistryAdderAndRemover : IInitializable, IDisposable
{
	private readonly SceneContextRegistry _registry;

	private readonly SceneContext _sceneContext;

	public SceneContextRegistryAdderAndRemover(SceneContext sceneContext, SceneContextRegistry registry)
	{
		_registry = registry;
		_sceneContext = sceneContext;
	}

	public void Initialize()
	{
		_registry.Add(_sceneContext);
	}

	public void Dispose()
	{
		_registry.Remove(_sceneContext);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new SceneContextRegistryAdderAndRemover((SceneContext)P_0[0], (SceneContextRegistry)P_0[1]);
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(SceneContextRegistryAdderAndRemover), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[2]
		{
			new InjectableInfo(optional: false, null, "sceneContext", typeof(SceneContext), null, InjectSources.Any),
			new InjectableInfo(optional: false, null, "registry", typeof(SceneContextRegistry), null, InjectSources.Any)
		}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
