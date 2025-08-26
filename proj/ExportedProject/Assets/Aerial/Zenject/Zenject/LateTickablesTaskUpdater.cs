using Zenject.Internal;

namespace Zenject;

public class LateTickablesTaskUpdater : TaskUpdater<ILateTickable>
{
	protected override void UpdateItem(ILateTickable task)
	{
		task.LateTick();
	}

	private static object __zenCreate(object[] P_0)
	{
		return new LateTickablesTaskUpdater();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(LateTickablesTaskUpdater), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
