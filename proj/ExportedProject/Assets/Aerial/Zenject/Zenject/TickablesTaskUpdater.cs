using Zenject.Internal;

namespace Zenject;

public class TickablesTaskUpdater : TaskUpdater<ITickable>
{
	protected override void UpdateItem(ITickable task)
	{
		task.Tick();
	}

	private static object __zenCreate(object[] P_0)
	{
		return new TickablesTaskUpdater();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(TickablesTaskUpdater), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
