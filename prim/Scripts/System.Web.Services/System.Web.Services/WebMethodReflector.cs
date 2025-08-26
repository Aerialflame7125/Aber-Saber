using System.Collections;
using System.Reflection;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace System.Web.Services;

internal class WebMethodReflector
{
	private WebMethodReflector()
	{
	}

	internal static WebMethodAttribute GetAttribute(MethodInfo implementation, MethodInfo declaration)
	{
		WebMethodAttribute webMethodAttribute = null;
		WebMethodAttribute webMethodAttribute2 = null;
		object[] customAttributes;
		if (declaration != null)
		{
			customAttributes = declaration.GetCustomAttributes(typeof(WebMethodAttribute), inherit: false);
			if (customAttributes.Length != 0)
			{
				webMethodAttribute = (WebMethodAttribute)customAttributes[0];
			}
		}
		customAttributes = implementation.GetCustomAttributes(typeof(WebMethodAttribute), inherit: false);
		if (customAttributes.Length != 0)
		{
			webMethodAttribute2 = (WebMethodAttribute)customAttributes[0];
		}
		if (webMethodAttribute == null)
		{
			return webMethodAttribute2;
		}
		if (webMethodAttribute2 == null)
		{
			return webMethodAttribute;
		}
		if (webMethodAttribute2.MessageNameSpecified)
		{
			throw new InvalidOperationException(Res.GetString("ContractOverride", implementation.Name, implementation.DeclaringType.FullName, declaration.DeclaringType.FullName, declaration.ToString(), "WebMethod.MessageName"));
		}
		return new WebMethodAttribute(webMethodAttribute2.EnableSessionSpecified ? webMethodAttribute2.EnableSession : webMethodAttribute.EnableSession)
		{
			TransactionOption = (webMethodAttribute2.TransactionOptionSpecified ? webMethodAttribute2.TransactionOption : webMethodAttribute.TransactionOption),
			CacheDuration = (webMethodAttribute2.CacheDurationSpecified ? webMethodAttribute2.CacheDuration : webMethodAttribute.CacheDuration),
			BufferResponse = (webMethodAttribute2.BufferResponseSpecified ? webMethodAttribute2.BufferResponse : webMethodAttribute.BufferResponse),
			Description = (webMethodAttribute2.DescriptionSpecified ? webMethodAttribute2.Description : webMethodAttribute.Description)
		};
	}

	internal static MethodInfo FindInterfaceMethodInfo(Type type, string signature)
	{
		Type[] interfaces = type.GetInterfaces();
		foreach (Type interfaceType in interfaces)
		{
			InterfaceMapping interfaceMap = type.GetInterfaceMap(interfaceType);
			MethodInfo[] targetMethods = interfaceMap.TargetMethods;
			for (int j = 0; j < targetMethods.Length; j++)
			{
				if (targetMethods[j].ToString() == signature)
				{
					return interfaceMap.InterfaceMethods[j];
				}
			}
		}
		return null;
	}

	internal static LogicalMethodInfo[] GetMethods(Type type)
	{
		if (type.IsInterface)
		{
			throw new InvalidOperationException(Res.GetString("NeedConcreteType", type.FullName));
		}
		ArrayList arrayList = new ArrayList();
		MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		Hashtable hashtable = new Hashtable();
		Hashtable hashtable2 = new Hashtable();
		for (int i = 0; i < methods.Length; i++)
		{
			Type declaringType = methods[i].DeclaringType;
			if (declaringType == typeof(object) || declaringType == typeof(WebService))
			{
				continue;
			}
			string text = methods[i].ToString();
			MethodInfo methodInfo = FindInterfaceMethodInfo(declaringType, text);
			WebServiceBindingAttribute webServiceBindingAttribute = null;
			if (methodInfo != null)
			{
				object[] customAttributes = methodInfo.DeclaringType.GetCustomAttributes(typeof(WebServiceBindingAttribute), inherit: false);
				if (customAttributes.Length != 0)
				{
					if (customAttributes.Length > 1)
					{
						throw new ArgumentException(Res.GetString("OnlyOneWebServiceBindingAttributeMayBeSpecified1", methodInfo.DeclaringType.FullName), "type");
					}
					webServiceBindingAttribute = (WebServiceBindingAttribute)customAttributes[0];
					if (webServiceBindingAttribute.Name == null || webServiceBindingAttribute.Name.Length == 0)
					{
						webServiceBindingAttribute.Name = methodInfo.DeclaringType.Name;
					}
				}
				else
				{
					methodInfo = null;
				}
			}
			else if (!methods[i].IsPublic)
			{
				continue;
			}
			WebMethodAttribute attribute = GetAttribute(methods[i], methodInfo);
			if (attribute != null)
			{
				WebMethod value = new WebMethod(methodInfo, webServiceBindingAttribute, attribute);
				hashtable2.Add(methods[i], value);
				MethodInfo methodInfo2 = (MethodInfo)hashtable[text];
				if (methodInfo2 == null)
				{
					hashtable.Add(text, methods[i]);
					arrayList.Add(methods[i]);
				}
				else if (methodInfo2.DeclaringType.IsAssignableFrom(methods[i].DeclaringType))
				{
					hashtable[text] = methods[i];
					arrayList[arrayList.IndexOf(methodInfo2)] = methods[i];
				}
			}
		}
		return LogicalMethodInfo.Create((MethodInfo[])arrayList.ToArray(typeof(MethodInfo)), (LogicalMethodTypes)3, hashtable2);
	}

	internal static void IncludeTypes(LogicalMethodInfo[] methods, XmlReflectionImporter importer)
	{
		for (int i = 0; i < methods.Length; i++)
		{
			IncludeTypes(methods[i], importer);
		}
	}

	internal static void IncludeTypes(LogicalMethodInfo method, XmlReflectionImporter importer)
	{
		if (method.Declaration != null)
		{
			importer.IncludeTypes(method.Declaration.DeclaringType);
			importer.IncludeTypes(method.Declaration);
		}
		importer.IncludeTypes(method.DeclaringType);
		importer.IncludeTypes(method.CustomAttributeProvider);
	}
}
