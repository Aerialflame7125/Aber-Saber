using System;
using Zenject.Internal;

namespace Zenject;

[Serializable]
public class MemoryPoolSettings
{
	public int InitialSize;

	public int MaxSize;

	public PoolExpandMethods ExpandMethod;

	public static readonly MemoryPoolSettings Default = new MemoryPoolSettings();

	public MemoryPoolSettings()
	{
		InitialSize = 0;
		MaxSize = int.MaxValue;
		ExpandMethod = PoolExpandMethods.OneAtATime;
	}

	public MemoryPoolSettings(int initialSize, int maxSize, PoolExpandMethods expandMethod)
	{
		InitialSize = initialSize;
		MaxSize = maxSize;
		ExpandMethod = expandMethod;
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MemoryPoolSettings();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MemoryPoolSettings), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
