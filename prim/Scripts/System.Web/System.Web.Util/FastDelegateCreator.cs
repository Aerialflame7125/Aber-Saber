using System.Reflection;
using System.Reflection.Emit;
using System.Security.Permissions;

namespace System.Web.Util;

[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
internal static class FastDelegateCreator<TDelegate> where TDelegate : class
{
	private static readonly Func<object, IntPtr, TDelegate> _factory = GetFactory();

	internal static TDelegate BindTo(object obj, IntPtr method)
	{
		return _factory(obj, method);
	}

	internal static TDelegate BindTo(object obj, MethodInfo method)
	{
		return BindTo(obj, method.MethodHandle.GetFunctionPointer());
	}

	[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
	private static Func<object, IntPtr, TDelegate> GetFactory()
	{
		ConstructorInfo constructor = typeof(TDelegate).GetConstructor(new Type[2]
		{
			typeof(object),
			typeof(IntPtr)
		});
		DynamicMethod dynamicMethod = new DynamicMethod("FastCreateDelegate_" + typeof(TDelegate).Name, typeof(TDelegate), new Type[2]
		{
			typeof(object),
			typeof(IntPtr)
		}, typeof(FastDelegateCreator<TDelegate>), skipVisibility: true);
		ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
		iLGenerator.Emit(OpCodes.Ldarg_0);
		iLGenerator.Emit(OpCodes.Ldarg_1);
		iLGenerator.Emit(OpCodes.Newobj, constructor);
		iLGenerator.Emit(OpCodes.Ret);
		return (Func<object, IntPtr, TDelegate>)dynamicMethod.CreateDelegate(typeof(Func<object, IntPtr, TDelegate>));
	}
}
