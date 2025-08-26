using System.Reflection;

namespace System.Web.Services;

internal class WebMethod
{
	internal MethodInfo declaration;

	internal WebServiceBindingAttribute binding;

	internal WebMethodAttribute attribute;

	internal WebMethod(MethodInfo declaration, WebServiceBindingAttribute binding, WebMethodAttribute attribute)
	{
		this.declaration = declaration;
		this.binding = binding;
		this.attribute = attribute;
	}
}
