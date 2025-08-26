using Zenject.Internal;

namespace Zenject;

public class FixedTickablesTaskUpdater : TaskUpdater<IFixedTickable>
{
	protected override void UpdateItem(IFixedTickable task)
	{
		task.FixedTick();
	}

	private static object __zenCreate(object[] P_0)
	{
		return new FixedTickablesTaskUpdater();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(FixedTickablesTaskUpdater), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
