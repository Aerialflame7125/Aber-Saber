using System;
using System.Collections.Generic;
using Zenject.Internal;

namespace Zenject;

public class ExecutionOrderInstaller : Installer<List<Type>, ExecutionOrderInstaller>
{
	private List<Type> _typeOrder;

	public ExecutionOrderInstaller(List<Type> typeOrder)
	{
		_typeOrder = typeOrder;
	}

	public override void InstallBindings()
	{
		int num = -1 * _typeOrder.Count;
		foreach (Type item in _typeOrder)
		{
			base.Container.BindExecutionOrder(item, num);
			num++;
		}
	}

	private static object __zenCreate(object[] P_0)
	{
		return new ExecutionOrderInstaller((List<Type>)P_0[0]);
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(ExecutionOrderInstaller), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[1]
		{
			new InjectableInfo(optional: false, null, "typeOrder", typeof(List<Type>), null, InjectSources.Any)
		}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
