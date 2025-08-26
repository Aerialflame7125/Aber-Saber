using System.Web.Services.Protocols;

namespace System.Web.Services;

internal class WebServiceBindingReflector
{
	private WebServiceBindingReflector()
	{
	}

	internal static WebServiceBindingAttribute GetAttribute(Type type)
	{
		while (type != null)
		{
			object[] customAttributes = type.GetCustomAttributes(typeof(WebServiceBindingAttribute), inherit: false);
			if (customAttributes.Length != 0)
			{
				if (customAttributes.Length > 1)
				{
					throw new ArgumentException(Res.GetString("OnlyOneWebServiceBindingAttributeMayBeSpecified1", type.FullName), "type");
				}
				return (WebServiceBindingAttribute)customAttributes[0];
			}
			type = type.BaseType;
		}
		return null;
	}

	internal static WebServiceBindingAttribute GetAttribute(LogicalMethodInfo methodInfo, string binding)
	{
		if (methodInfo.Binding != null)
		{
			if (binding.Length > 0 && methodInfo.Binding.Name != binding)
			{
				throw new InvalidOperationException(Res.GetString("WebInvalidBindingName", binding, methodInfo.Binding.Name));
			}
			return methodInfo.Binding;
		}
		Type declaringType = methodInfo.DeclaringType;
		object[] customAttributes = declaringType.GetCustomAttributes(typeof(WebServiceBindingAttribute), inherit: false);
		WebServiceBindingAttribute webServiceBindingAttribute = null;
		object[] array = customAttributes;
		for (int i = 0; i < array.Length; i++)
		{
			WebServiceBindingAttribute webServiceBindingAttribute2 = (WebServiceBindingAttribute)array[i];
			if (webServiceBindingAttribute2.Name == binding)
			{
				if (webServiceBindingAttribute != null)
				{
					throw new ArgumentException(Res.GetString("MultipleBindingsWithSameName2", declaringType.FullName, binding, "methodInfo"));
				}
				webServiceBindingAttribute = webServiceBindingAttribute2;
			}
		}
		if (webServiceBindingAttribute == null && binding != null && binding.Length > 0)
		{
			throw new ArgumentException(Res.GetString("TypeIsMissingWebServiceBindingAttributeThat2", declaringType.FullName, binding), "methodInfo");
		}
		return webServiceBindingAttribute;
	}
}
