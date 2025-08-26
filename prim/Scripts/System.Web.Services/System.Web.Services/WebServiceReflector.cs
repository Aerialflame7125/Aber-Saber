using System.Web.Services.Protocols;

namespace System.Web.Services;

internal class WebServiceReflector
{
	private WebServiceReflector()
	{
	}

	internal static WebServiceAttribute GetAttribute(Type type)
	{
		object[] customAttributes = type.GetCustomAttributes(typeof(WebServiceAttribute), inherit: false);
		if (customAttributes.Length == 0)
		{
			return new WebServiceAttribute();
		}
		return (WebServiceAttribute)customAttributes[0];
	}

	internal static WebServiceAttribute GetAttribute(LogicalMethodInfo[] methodInfos)
	{
		if (methodInfos.Length == 0)
		{
			return new WebServiceAttribute();
		}
		return GetAttribute(GetMostDerivedType(methodInfos));
	}

	internal static Type GetMostDerivedType(LogicalMethodInfo[] methodInfos)
	{
		if (methodInfos.Length == 0)
		{
			return null;
		}
		Type type = methodInfos[0].DeclaringType;
		for (int i = 1; i < methodInfos.Length; i++)
		{
			Type declaringType = methodInfos[i].DeclaringType;
			if (declaringType.IsSubclassOf(type))
			{
				type = declaringType;
			}
		}
		return type;
	}
}
