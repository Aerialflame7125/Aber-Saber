using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;
using Zenject.Internal;

namespace Zenject;

public class GuiRenderableManager
{
	private class RenderableInfo
	{
		public IGuiRenderable Renderable;

		public int Priority;

		public RenderableInfo(IGuiRenderable renderable, int priority)
		{
			Renderable = renderable;
			Priority = priority;
		}

		private static object __zenCreate(object[] P_0)
		{
			return new RenderableInfo((IGuiRenderable)P_0[0], (int)P_0[1]);
		}

		[Zenject.Internal.Preserve]
		private static InjectTypeInfo __zenCreateInjectTypeInfo()
		{
			return new InjectTypeInfo(typeof(RenderableInfo), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[2]
			{
				new InjectableInfo(optional: false, null, "renderable", typeof(IGuiRenderable), null, InjectSources.Any),
				new InjectableInfo(optional: false, null, "priority", typeof(int), null, InjectSources.Any)
			}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
		}
	}

	private List<RenderableInfo> _renderables;

	public GuiRenderableManager([Inject(Optional = true, Source = InjectSources.Local)] List<IGuiRenderable> renderables, [Inject(Optional = true, Source = InjectSources.Local)] List<ValuePair<Type, int>> priorities)
	{
		_renderables = new List<RenderableInfo>();
		foreach (IGuiRenderable renderable in renderables)
		{
			List<int> list = (from x in priorities
				where TypeExtensions.DerivesFromOrEqual(renderable.GetType(), x.First)
				select x.Second).ToList();
			int priority = ((!LinqExtensions.IsEmpty(list)) ? list.Distinct().Single() : 0);
			_renderables.Add(new RenderableInfo(renderable, priority));
		}
		_renderables = _renderables.OrderBy((RenderableInfo x) => x.Priority).ToList();
	}

	public void OnGui()
	{
		foreach (RenderableInfo renderable in _renderables)
		{
			try
			{
				renderable.Renderable.GuiRender();
			}
			catch (Exception innerException)
			{
				throw Assert.CreateException(innerException, "Error occurred while calling {0}.GuiRender", renderable.Renderable.GetType());
			}
		}
	}

	private static object __zenCreate(object[] P_0)
	{
		return new GuiRenderableManager((List<IGuiRenderable>)P_0[0], (List<ValuePair<Type, int>>)P_0[1]);
	}

	[Zenject.Internal.Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(GuiRenderableManager), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[2]
		{
			new InjectableInfo(optional: true, null, "renderables", typeof(List<IGuiRenderable>), null, InjectSources.Local),
			new InjectableInfo(optional: true, null, "priorities", typeof(List<ValuePair<Type, int>>), null, InjectSources.Local)
		}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
