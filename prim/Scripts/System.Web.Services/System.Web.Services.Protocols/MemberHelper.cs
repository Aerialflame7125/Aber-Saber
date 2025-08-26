using System.Reflection;

namespace System.Web.Services.Protocols;

internal class MemberHelper
{
	private static object[] emptyObjectArray = new object[0];

	private MemberHelper()
	{
	}

	internal static void SetValue(MemberInfo memberInfo, object target, object value)
	{
		if (memberInfo is FieldInfo)
		{
			((FieldInfo)memberInfo).SetValue(target, value);
		}
		else
		{
			((PropertyInfo)memberInfo).SetValue(target, value, emptyObjectArray);
		}
	}

	internal static object GetValue(MemberInfo memberInfo, object target)
	{
		if (memberInfo is FieldInfo)
		{
			return ((FieldInfo)memberInfo).GetValue(target);
		}
		return ((PropertyInfo)memberInfo).GetValue(target, emptyObjectArray);
	}

	internal static bool IsStatic(MemberInfo memberInfo)
	{
		if (memberInfo is FieldInfo)
		{
			return ((FieldInfo)memberInfo).IsStatic;
		}
		return false;
	}

	internal static bool CanRead(MemberInfo memberInfo)
	{
		if (memberInfo is FieldInfo)
		{
			return true;
		}
		return ((PropertyInfo)memberInfo).CanRead;
	}

	internal static bool CanWrite(MemberInfo memberInfo)
	{
		if (memberInfo is FieldInfo)
		{
			return true;
		}
		return ((PropertyInfo)memberInfo).CanWrite;
	}
}
