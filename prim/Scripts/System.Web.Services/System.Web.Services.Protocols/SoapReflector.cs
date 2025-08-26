using System.Collections;
using System.Reflection;
using System.Threading;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols;

internal static class SoapReflector
{
	private class SoapParameterInfo
	{
		internal ParameterInfo parameterInfo;

		internal XmlAttributes xmlAttributes;

		internal SoapAttributes soapAttributes;
	}

	private class MethodAttribute
	{
		internal string action;

		internal string binding;

		internal string requestName;

		internal string requestNs;

		internal string responseName;

		internal string responseNs;
	}

	internal static bool ServiceDefaultIsEncoded(Type type)
	{
		return ServiceDefaultIsEncoded(GetSoapServiceAttribute(type));
	}

	internal static bool ServiceDefaultIsEncoded(object soapServiceAttribute)
	{
		if (soapServiceAttribute == null)
		{
			return false;
		}
		if (soapServiceAttribute is SoapDocumentServiceAttribute)
		{
			return ((SoapDocumentServiceAttribute)soapServiceAttribute).Use == SoapBindingUse.Encoded;
		}
		if (soapServiceAttribute is SoapRpcServiceAttribute)
		{
			return ((SoapRpcServiceAttribute)soapServiceAttribute).Use == SoapBindingUse.Encoded;
		}
		return false;
	}

	internal static string GetEncodedNamespace(string ns, bool serviceDefaultIsEncoded)
	{
		if (serviceDefaultIsEncoded)
		{
			return ns;
		}
		if (ns.EndsWith("/", StringComparison.Ordinal))
		{
			return ns + "encodedTypes";
		}
		return ns + "/encodedTypes";
	}

	internal static string GetLiteralNamespace(string ns, bool serviceDefaultIsEncoded)
	{
		if (!serviceDefaultIsEncoded)
		{
			return ns;
		}
		if (ns.EndsWith("/", StringComparison.Ordinal))
		{
			return ns + "literalTypes";
		}
		return ns + "/literalTypes";
	}

	internal static SoapReflectionImporter CreateSoapImporter(string defaultNs, bool serviceDefaultIsEncoded)
	{
		return new SoapReflectionImporter(GetEncodedNamespace(defaultNs, serviceDefaultIsEncoded));
	}

	internal static XmlReflectionImporter CreateXmlImporter(string defaultNs, bool serviceDefaultIsEncoded)
	{
		return new XmlReflectionImporter(GetLiteralNamespace(defaultNs, serviceDefaultIsEncoded));
	}

	internal static void IncludeTypes(LogicalMethodInfo[] methods, SoapReflectionImporter importer)
	{
		for (int i = 0; i < methods.Length; i++)
		{
			IncludeTypes(methods[i], importer);
		}
	}

	internal static void IncludeTypes(LogicalMethodInfo method, SoapReflectionImporter importer)
	{
		if (method.Declaration != null)
		{
			importer.IncludeTypes(method.Declaration.DeclaringType);
			importer.IncludeTypes(method.Declaration);
		}
		importer.IncludeTypes(method.DeclaringType);
		importer.IncludeTypes(method.CustomAttributeProvider);
	}

	internal static object GetSoapMethodAttribute(LogicalMethodInfo methodInfo)
	{
		object[] customAttributes = methodInfo.GetCustomAttributes(typeof(SoapRpcMethodAttribute));
		object[] customAttributes2 = methodInfo.GetCustomAttributes(typeof(SoapDocumentMethodAttribute));
		if (customAttributes.Length != 0)
		{
			if (customAttributes2.Length != 0)
			{
				throw new ArgumentException(Res.GetString("WebBothMethodAttrs"), "methodInfo");
			}
			return customAttributes[0];
		}
		if (customAttributes2.Length != 0)
		{
			return customAttributes2[0];
		}
		return null;
	}

	internal static object GetSoapServiceAttribute(Type type)
	{
		object[] customAttributes = type.GetCustomAttributes(typeof(SoapRpcServiceAttribute), inherit: false);
		object[] customAttributes2 = type.GetCustomAttributes(typeof(SoapDocumentServiceAttribute), inherit: false);
		if (customAttributes.Length != 0)
		{
			if (customAttributes2.Length != 0)
			{
				throw new ArgumentException(Res.GetString("WebBothServiceAttrs"), "methodInfo");
			}
			return customAttributes[0];
		}
		if (customAttributes2.Length != 0)
		{
			return customAttributes2[0];
		}
		return null;
	}

	internal static SoapServiceRoutingStyle GetSoapServiceRoutingStyle(object soapServiceAttribute)
	{
		if (soapServiceAttribute is SoapRpcServiceAttribute)
		{
			return ((SoapRpcServiceAttribute)soapServiceAttribute).RoutingStyle;
		}
		if (soapServiceAttribute is SoapDocumentServiceAttribute)
		{
			return ((SoapDocumentServiceAttribute)soapServiceAttribute).RoutingStyle;
		}
		return SoapServiceRoutingStyle.SoapAction;
	}

	internal static string GetSoapMethodBinding(LogicalMethodInfo method)
	{
		object[] customAttributes = method.GetCustomAttributes(typeof(SoapDocumentMethodAttribute));
		string text;
		if (customAttributes.Length == 0)
		{
			customAttributes = method.GetCustomAttributes(typeof(SoapRpcMethodAttribute));
			text = ((customAttributes.Length != 0) ? ((SoapRpcMethodAttribute)customAttributes[0]).Binding : string.Empty);
		}
		else
		{
			text = ((SoapDocumentMethodAttribute)customAttributes[0]).Binding;
		}
		if (method.Binding != null)
		{
			if (text.Length > 0 && text != method.Binding.Name)
			{
				throw new InvalidOperationException(Res.GetString("WebInvalidBindingName", text, method.Binding.Name));
			}
			return method.Binding.Name;
		}
		return text;
	}

	internal static SoapReflectedMethod ReflectMethod(LogicalMethodInfo methodInfo, bool client, XmlReflectionImporter xmlImporter, SoapReflectionImporter soapImporter, string defaultNs)
	{
		try
		{
			string key = methodInfo.GetKey();
			SoapReflectedMethod soapReflectedMethod = new SoapReflectedMethod();
			MethodAttribute methodAttribute = new MethodAttribute();
			object soapServiceAttribute = GetSoapServiceAttribute(methodInfo.DeclaringType);
			bool serviceDefaultIsEncoded = ServiceDefaultIsEncoded(soapServiceAttribute);
			object obj = GetSoapMethodAttribute(methodInfo);
			if (obj == null)
			{
				if (client)
				{
					return null;
				}
				obj = ((soapServiceAttribute is SoapRpcServiceAttribute) ? ((Attribute)new SoapRpcMethodAttribute
				{
					Use = ((SoapRpcServiceAttribute)soapServiceAttribute).Use
				}) : ((Attribute)((!(soapServiceAttribute is SoapDocumentServiceAttribute)) ? new SoapDocumentMethodAttribute() : new SoapDocumentMethodAttribute
				{
					Use = ((SoapDocumentServiceAttribute)soapServiceAttribute).Use
				})));
			}
			if (obj is SoapRpcMethodAttribute)
			{
				SoapRpcMethodAttribute soapRpcMethodAttribute = (SoapRpcMethodAttribute)obj;
				soapReflectedMethod.rpc = true;
				soapReflectedMethod.use = soapRpcMethodAttribute.Use;
				soapReflectedMethod.oneWay = soapRpcMethodAttribute.OneWay;
				methodAttribute.action = soapRpcMethodAttribute.Action;
				methodAttribute.binding = soapRpcMethodAttribute.Binding;
				methodAttribute.requestName = soapRpcMethodAttribute.RequestElementName;
				methodAttribute.requestNs = soapRpcMethodAttribute.RequestNamespace;
				methodAttribute.responseName = soapRpcMethodAttribute.ResponseElementName;
				methodAttribute.responseNs = soapRpcMethodAttribute.ResponseNamespace;
			}
			else
			{
				SoapDocumentMethodAttribute soapDocumentMethodAttribute = (SoapDocumentMethodAttribute)obj;
				soapReflectedMethod.rpc = false;
				soapReflectedMethod.use = soapDocumentMethodAttribute.Use;
				soapReflectedMethod.paramStyle = soapDocumentMethodAttribute.ParameterStyle;
				soapReflectedMethod.oneWay = soapDocumentMethodAttribute.OneWay;
				methodAttribute.action = soapDocumentMethodAttribute.Action;
				methodAttribute.binding = soapDocumentMethodAttribute.Binding;
				methodAttribute.requestName = soapDocumentMethodAttribute.RequestElementName;
				methodAttribute.requestNs = soapDocumentMethodAttribute.RequestNamespace;
				methodAttribute.responseName = soapDocumentMethodAttribute.ResponseElementName;
				methodAttribute.responseNs = soapDocumentMethodAttribute.ResponseNamespace;
				if (soapReflectedMethod.use == SoapBindingUse.Default)
				{
					if (soapServiceAttribute is SoapDocumentServiceAttribute)
					{
						soapReflectedMethod.use = ((SoapDocumentServiceAttribute)soapServiceAttribute).Use;
					}
					if (soapReflectedMethod.use == SoapBindingUse.Default)
					{
						soapReflectedMethod.use = SoapBindingUse.Literal;
					}
				}
				if (soapReflectedMethod.paramStyle == SoapParameterStyle.Default)
				{
					if (soapServiceAttribute is SoapDocumentServiceAttribute)
					{
						soapReflectedMethod.paramStyle = ((SoapDocumentServiceAttribute)soapServiceAttribute).ParameterStyle;
					}
					if (soapReflectedMethod.paramStyle == SoapParameterStyle.Default)
					{
						soapReflectedMethod.paramStyle = SoapParameterStyle.Wrapped;
					}
				}
			}
			if (methodAttribute.binding.Length > 0)
			{
				if (client)
				{
					throw new InvalidOperationException(Res.GetString("WebInvalidBindingPlacement", obj.GetType().Name));
				}
				soapReflectedMethod.binding = WebServiceBindingReflector.GetAttribute(methodInfo, methodAttribute.binding);
			}
			WebMethodAttribute methodAttribute2 = methodInfo.MethodAttribute;
			soapReflectedMethod.name = methodAttribute2.MessageName;
			if (soapReflectedMethod.name.Length == 0)
			{
				soapReflectedMethod.name = methodInfo.Name;
			}
			string text = ((!soapReflectedMethod.rpc) ? ((methodAttribute.requestName.Length == 0) ? soapReflectedMethod.name : methodAttribute.requestName) : ((methodAttribute.requestName.Length == 0 || !client) ? methodInfo.Name : methodAttribute.requestName));
			string text2 = methodAttribute.requestNs;
			if (text2 == null)
			{
				text2 = ((soapReflectedMethod.binding == null || soapReflectedMethod.binding.Namespace == null || soapReflectedMethod.binding.Namespace.Length == 0) ? defaultNs : soapReflectedMethod.binding.Namespace);
			}
			string text3 = ((!soapReflectedMethod.rpc || soapReflectedMethod.use == SoapBindingUse.Encoded) ? ((methodAttribute.responseName.Length == 0) ? (soapReflectedMethod.name + "Response") : methodAttribute.responseName) : (methodInfo.Name + "Response"));
			string text4 = methodAttribute.responseNs;
			if (text4 == null)
			{
				text4 = ((soapReflectedMethod.binding == null || soapReflectedMethod.binding.Namespace == null || soapReflectedMethod.binding.Namespace.Length == 0) ? defaultNs : soapReflectedMethod.binding.Namespace);
			}
			SoapParameterInfo[] array = ReflectParameters(methodInfo.InParameters, text2);
			SoapParameterInfo[] array2 = ReflectParameters(methodInfo.OutParameters, text4);
			soapReflectedMethod.action = methodAttribute.action;
			if (soapReflectedMethod.action == null)
			{
				soapReflectedMethod.action = GetDefaultAction(defaultNs, methodInfo);
			}
			soapReflectedMethod.methodInfo = methodInfo;
			if (soapReflectedMethod.oneWay)
			{
				if (array2.Length != 0)
				{
					throw new ArgumentException(Res.GetString("WebOneWayOutParameters"), "methodInfo");
				}
				if (methodInfo.ReturnType != typeof(void))
				{
					throw new ArgumentException(Res.GetString("WebOneWayReturnValue"), "methodInfo");
				}
			}
			XmlReflectionMember[] array3 = new XmlReflectionMember[array.Length];
			for (int i = 0; i < array3.Length; i++)
			{
				SoapParameterInfo soapParameterInfo = array[i];
				XmlReflectionMember xmlReflectionMember = new XmlReflectionMember();
				xmlReflectionMember.MemberName = soapParameterInfo.parameterInfo.Name;
				xmlReflectionMember.MemberType = soapParameterInfo.parameterInfo.ParameterType;
				if (xmlReflectionMember.MemberType.IsByRef)
				{
					xmlReflectionMember.MemberType = xmlReflectionMember.MemberType.GetElementType();
				}
				xmlReflectionMember.XmlAttributes = soapParameterInfo.xmlAttributes;
				xmlReflectionMember.SoapAttributes = soapParameterInfo.soapAttributes;
				array3[i] = xmlReflectionMember;
			}
			soapReflectedMethod.requestMappings = ImportMembersMapping(xmlImporter, soapImporter, serviceDefaultIsEncoded, soapReflectedMethod.rpc, soapReflectedMethod.use, soapReflectedMethod.paramStyle, text, text2, methodAttribute.requestNs == null, array3, validate: true, openModel: false, key, client);
			if (GetSoapServiceRoutingStyle(soapServiceAttribute) == SoapServiceRoutingStyle.RequestElement && soapReflectedMethod.paramStyle == SoapParameterStyle.Bare && soapReflectedMethod.requestMappings.Count != 1)
			{
				throw new ArgumentException(Res.GetString("WhenUsingAMessageStyleOfParametersAsDocument0"), "methodInfo");
			}
			string name = "";
			string ns = "";
			if (soapReflectedMethod.paramStyle == SoapParameterStyle.Bare)
			{
				if (soapReflectedMethod.requestMappings.Count == 1)
				{
					name = soapReflectedMethod.requestMappings[0].XsdElementName;
					ns = soapReflectedMethod.requestMappings[0].Namespace;
				}
			}
			else
			{
				name = soapReflectedMethod.requestMappings.XsdElementName;
				ns = soapReflectedMethod.requestMappings.Namespace;
			}
			soapReflectedMethod.requestElementName = new XmlQualifiedName(name, ns);
			if (!soapReflectedMethod.oneWay)
			{
				int num = array2.Length;
				int num2 = 0;
				CodeIdentifiers codeIdentifiers = null;
				if (methodInfo.ReturnType != typeof(void))
				{
					num++;
					num2 = 1;
					codeIdentifiers = new CodeIdentifiers();
				}
				array3 = new XmlReflectionMember[num];
				foreach (SoapParameterInfo soapParameterInfo2 in array2)
				{
					XmlReflectionMember xmlReflectionMember2 = new XmlReflectionMember();
					xmlReflectionMember2.MemberName = soapParameterInfo2.parameterInfo.Name;
					xmlReflectionMember2.MemberType = soapParameterInfo2.parameterInfo.ParameterType;
					if (xmlReflectionMember2.MemberType.IsByRef)
					{
						xmlReflectionMember2.MemberType = xmlReflectionMember2.MemberType.GetElementType();
					}
					xmlReflectionMember2.XmlAttributes = soapParameterInfo2.xmlAttributes;
					xmlReflectionMember2.SoapAttributes = soapParameterInfo2.soapAttributes;
					array3[num2++] = xmlReflectionMember2;
					codeIdentifiers?.Add(xmlReflectionMember2.MemberName, null);
				}
				if (methodInfo.ReturnType != typeof(void))
				{
					XmlReflectionMember xmlReflectionMember3 = new XmlReflectionMember();
					xmlReflectionMember3.MemberName = codeIdentifiers.MakeUnique(soapReflectedMethod.name + "Result");
					xmlReflectionMember3.MemberType = methodInfo.ReturnType;
					xmlReflectionMember3.IsReturnValue = true;
					xmlReflectionMember3.XmlAttributes = new XmlAttributes(methodInfo.ReturnTypeCustomAttributeProvider);
					xmlReflectionMember3.XmlAttributes.XmlRoot = null;
					xmlReflectionMember3.SoapAttributes = new SoapAttributes(methodInfo.ReturnTypeCustomAttributeProvider);
					array3[0] = xmlReflectionMember3;
				}
				soapReflectedMethod.responseMappings = ImportMembersMapping(xmlImporter, soapImporter, serviceDefaultIsEncoded, soapReflectedMethod.rpc, soapReflectedMethod.use, soapReflectedMethod.paramStyle, text3, text4, methodAttribute.responseNs == null, array3, validate: false, openModel: false, key + ":Response", !client);
			}
			SoapExtensionAttribute[] array4 = (SoapExtensionAttribute[])methodInfo.GetCustomAttributes(typeof(SoapExtensionAttribute));
			soapReflectedMethod.extensions = new SoapReflectedExtension[array4.Length];
			for (int k = 0; k < array4.Length; k++)
			{
				soapReflectedMethod.extensions[k] = new SoapReflectedExtension(array4[k].ExtensionType, array4[k]);
			}
			Array.Sort(soapReflectedMethod.extensions);
			SoapHeaderAttribute[] array5 = (SoapHeaderAttribute[])methodInfo.GetCustomAttributes(typeof(SoapHeaderAttribute));
			Array.Sort(array5, new SoapHeaderAttributeComparer());
			Hashtable hashtable = new Hashtable();
			soapReflectedMethod.headers = new SoapReflectedHeader[array5.Length];
			int num3 = 0;
			int num4 = soapReflectedMethod.headers.Length;
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			for (int l = 0; l < soapReflectedMethod.headers.Length; l++)
			{
				SoapHeaderAttribute soapHeaderAttribute = array5[l];
				SoapReflectedHeader soapReflectedHeader = new SoapReflectedHeader();
				Type declaringType = methodInfo.DeclaringType;
				if ((soapReflectedHeader.memberInfo = declaringType.GetField(soapHeaderAttribute.MemberName)) != null)
				{
					soapReflectedHeader.headerType = ((FieldInfo)soapReflectedHeader.memberInfo).FieldType;
				}
				else
				{
					if (!((soapReflectedHeader.memberInfo = declaringType.GetProperty(soapHeaderAttribute.MemberName)) != null))
					{
						throw HeaderException(soapHeaderAttribute.MemberName, methodInfo.DeclaringType, "WebHeaderMissing");
					}
					soapReflectedHeader.headerType = ((PropertyInfo)soapReflectedHeader.memberInfo).PropertyType;
				}
				if (soapReflectedHeader.headerType.IsArray)
				{
					soapReflectedHeader.headerType = soapReflectedHeader.headerType.GetElementType();
					soapReflectedHeader.repeats = true;
					if (soapReflectedHeader.headerType != typeof(SoapUnknownHeader) && soapReflectedHeader.headerType != typeof(SoapHeader))
					{
						throw HeaderException(soapHeaderAttribute.MemberName, methodInfo.DeclaringType, "WebHeaderType");
					}
				}
				if (MemberHelper.IsStatic(soapReflectedHeader.memberInfo))
				{
					throw HeaderException(soapHeaderAttribute.MemberName, methodInfo.DeclaringType, "WebHeaderStatic");
				}
				if (!MemberHelper.CanRead(soapReflectedHeader.memberInfo))
				{
					throw HeaderException(soapHeaderAttribute.MemberName, methodInfo.DeclaringType, "WebHeaderRead");
				}
				if (!MemberHelper.CanWrite(soapReflectedHeader.memberInfo))
				{
					throw HeaderException(soapHeaderAttribute.MemberName, methodInfo.DeclaringType, "WebHeaderWrite");
				}
				if (!typeof(SoapHeader).IsAssignableFrom(soapReflectedHeader.headerType))
				{
					throw HeaderException(soapHeaderAttribute.MemberName, methodInfo.DeclaringType, "WebHeaderType");
				}
				SoapHeaderDirection direction = soapHeaderAttribute.Direction;
				if (soapReflectedMethod.oneWay && (direction & (SoapHeaderDirection.Out | SoapHeaderDirection.Fault)) != 0)
				{
					throw HeaderException(soapHeaderAttribute.MemberName, methodInfo.DeclaringType, "WebHeaderOneWayOut");
				}
				if (hashtable.Contains(soapReflectedHeader.headerType))
				{
					SoapHeaderDirection soapHeaderDirection = (SoapHeaderDirection)hashtable[soapReflectedHeader.headerType];
					if ((soapHeaderDirection & direction) != 0)
					{
						throw HeaderException(soapHeaderAttribute.MemberName, methodInfo.DeclaringType, "WebMultiplyDeclaredHeaderTypes");
					}
					hashtable[soapReflectedHeader.headerType] = direction | soapHeaderDirection;
				}
				else
				{
					hashtable[soapReflectedHeader.headerType] = direction;
				}
				if (soapReflectedHeader.headerType != typeof(SoapHeader) && soapReflectedHeader.headerType != typeof(SoapUnknownHeader))
				{
					XmlReflectionMember xmlReflectionMember4 = new XmlReflectionMember();
					xmlReflectionMember4.MemberName = soapReflectedHeader.headerType.Name;
					xmlReflectionMember4.MemberType = soapReflectedHeader.headerType;
					XmlAttributes xmlAttributes = new XmlAttributes(soapReflectedHeader.headerType);
					if (xmlAttributes.XmlRoot != null)
					{
						xmlReflectionMember4.XmlAttributes = new XmlAttributes();
						XmlElementAttribute xmlElementAttribute = new XmlElementAttribute();
						xmlElementAttribute.ElementName = xmlAttributes.XmlRoot.ElementName;
						xmlElementAttribute.Namespace = xmlAttributes.XmlRoot.Namespace;
						xmlReflectionMember4.XmlAttributes.XmlElements.Add(xmlElementAttribute);
					}
					xmlReflectionMember4.OverrideIsNullable = true;
					if ((direction & SoapHeaderDirection.In) != 0)
					{
						arrayList.Add(xmlReflectionMember4);
					}
					if ((direction & (SoapHeaderDirection.Out | SoapHeaderDirection.Fault)) != 0)
					{
						arrayList2.Add(xmlReflectionMember4);
					}
					soapReflectedHeader.custom = true;
				}
				soapReflectedHeader.direction = direction;
				if (!soapReflectedHeader.custom)
				{
					soapReflectedMethod.headers[--num4] = soapReflectedHeader;
				}
				else
				{
					soapReflectedMethod.headers[num3++] = soapReflectedHeader;
				}
			}
			soapReflectedMethod.inHeaderMappings = ImportMembersMapping(xmlImporter, soapImporter, serviceDefaultIsEncoded, rpc: false, soapReflectedMethod.use, SoapParameterStyle.Bare, text + "InHeaders", defaultNs, nsIsDefault: true, (XmlReflectionMember[])arrayList.ToArray(typeof(XmlReflectionMember)), validate: false, openModel: true, key + ":InHeaders", client);
			if (!soapReflectedMethod.oneWay)
			{
				soapReflectedMethod.outHeaderMappings = ImportMembersMapping(xmlImporter, soapImporter, serviceDefaultIsEncoded, rpc: false, soapReflectedMethod.use, SoapParameterStyle.Bare, text3 + "OutHeaders", defaultNs, nsIsDefault: true, (XmlReflectionMember[])arrayList2.ToArray(typeof(XmlReflectionMember)), validate: false, openModel: true, key + ":OutHeaders", !client);
			}
			return soapReflectedMethod;
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			throw new InvalidOperationException(Res.GetString("WebReflectionErrorMethod", methodInfo.DeclaringType.Name, methodInfo.Name), ex);
		}
	}

	private static XmlMembersMapping ImportMembersMapping(XmlReflectionImporter xmlImporter, SoapReflectionImporter soapImporter, bool serviceDefaultIsEncoded, bool rpc, SoapBindingUse use, SoapParameterStyle paramStyle, string elementName, string elementNamespace, bool nsIsDefault, XmlReflectionMember[] members, bool validate, bool openModel, string key, bool writeAccess)
	{
		XmlMembersMapping xmlMembersMapping = null;
		if (use == SoapBindingUse.Encoded)
		{
			string ns = ((!rpc && paramStyle != SoapParameterStyle.Bare && nsIsDefault) ? GetEncodedNamespace(elementNamespace, serviceDefaultIsEncoded) : elementNamespace);
			xmlMembersMapping = soapImporter.ImportMembersMapping(elementName, ns, members, rpc || paramStyle != SoapParameterStyle.Bare, rpc, validate, (!writeAccess) ? XmlMappingAccess.Read : XmlMappingAccess.Write);
		}
		else
		{
			string ns2 = (nsIsDefault ? GetLiteralNamespace(elementNamespace, serviceDefaultIsEncoded) : elementNamespace);
			xmlMembersMapping = xmlImporter.ImportMembersMapping(elementName, ns2, members, paramStyle != SoapParameterStyle.Bare, rpc, openModel, (!writeAccess) ? XmlMappingAccess.Read : XmlMappingAccess.Write);
		}
		xmlMembersMapping?.SetKey(key);
		return xmlMembersMapping;
	}

	private static Exception HeaderException(string memberName, Type declaringType, string description)
	{
		return new Exception(Res.GetString(description, declaringType.Name, memberName));
	}

	private static SoapParameterInfo[] ReflectParameters(ParameterInfo[] paramInfos, string ns)
	{
		SoapParameterInfo[] array = new SoapParameterInfo[paramInfos.Length];
		for (int i = 0; i < paramInfos.Length; i++)
		{
			SoapParameterInfo soapParameterInfo = new SoapParameterInfo();
			ParameterInfo parameterInfo = paramInfos[i];
			if (parameterInfo.ParameterType.IsArray && parameterInfo.ParameterType.GetArrayRank() > 1)
			{
				throw new InvalidOperationException(Res.GetString("WebMultiDimArray"));
			}
			soapParameterInfo.xmlAttributes = new XmlAttributes(parameterInfo);
			soapParameterInfo.soapAttributes = new SoapAttributes(parameterInfo);
			soapParameterInfo.parameterInfo = parameterInfo;
			array[i] = soapParameterInfo;
		}
		return array;
	}

	private static string GetDefaultAction(string defaultNs, LogicalMethodInfo methodInfo)
	{
		string text = methodInfo.MethodAttribute.MessageName;
		if (text.Length == 0)
		{
			text = methodInfo.Name;
		}
		if (defaultNs.EndsWith("/", StringComparison.Ordinal))
		{
			return defaultNs + text;
		}
		return defaultNs + "/" + text;
	}
}
