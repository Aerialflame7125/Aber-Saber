using System;
using ModestTree;
using UnityEngine;
using Zenject.Internal;

namespace Zenject;

public static class SubContainerCreatorUtil
{
	private class DefaultParentObjectDestroyer : IDisposable
	{
		private readonly GameObject _gameObject;

		public DefaultParentObjectDestroyer(GameObject gameObject)
		{
			_gameObject = gameObject;
		}

		public void Dispose()
		{
			UnityEngine.Object.Destroy(_gameObject);
		}

		private static object __zenCreate(object[] P_0)
		{
			return new DefaultParentObjectDestroyer((GameObject)P_0[0]);
		}

		[Preserve]
		private static InjectTypeInfo __zenCreateInjectTypeInfo()
		{
			return new InjectTypeInfo(typeof(DefaultParentObjectDestroyer), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[1]
			{
				new InjectableInfo(optional: false, null, "gameObject", typeof(GameObject), null, InjectSources.Any)
			}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
		}
	}

	public static void ApplyBindSettings(SubContainerCreatorBindInfo subContainerBindInfo, DiContainer subContainer)
	{
		if (subContainerBindInfo.DefaultParentName != null)
		{
			GameObject gameObject = new GameObject(subContainerBindInfo.DefaultParentName);
			gameObject.transform.SetParent(subContainer.InheritedDefaultParent, worldPositionStays: false);
			subContainer.DefaultParent = gameObject.transform;
			subContainer.Bind<IDisposable>().To<DefaultParentObjectDestroyer>().AsCached()
				.WithArguments(gameObject);
			subContainer.BindDisposableExecutionOrder<DefaultParentObjectDestroyer>(int.MinValue);
		}
		if (subContainerBindInfo.CreateKernel)
		{
			DiContainer diContainer = LinqExtensions.OnlyOrDefault(subContainer.ParentContainers);
			Assert.IsNotNull(diContainer, "Could not find unique container when using WithKernel!");
			if (subContainerBindInfo.KernelType != null)
			{
				diContainer.Bind(TypeExtensions.Interfaces(typeof(Kernel))).To(subContainerBindInfo.KernelType).FromSubContainerResolve()
					.ByInstance(subContainer)
					.AsCached();
				subContainer.Bind(subContainerBindInfo.KernelType).AsCached();
			}
			else
			{
				diContainer.BindInterfacesTo<Kernel>().FromSubContainerResolve().ByInstance(subContainer)
					.AsCached();
				subContainer.Bind<Kernel>().AsCached();
			}
		}
	}
}
