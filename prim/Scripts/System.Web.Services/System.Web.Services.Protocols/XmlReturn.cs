using System.Collections;
using System.Security.Permissions;
using System.Security.Policy;
using System.Web.Services.Diagnostics;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols;

internal class XmlReturn
{
	private XmlReturn()
	{
	}

	internal static object[] GetInitializers(LogicalMethodInfo[] methodInfos)
	{
		if (methodInfos.Length == 0)
		{
			return new object[0];
		}
		WebServiceAttribute attribute = WebServiceReflector.GetAttribute(methodInfos);
		XmlReflectionImporter xmlReflectionImporter = SoapReflector.CreateXmlImporter(serviceDefaultIsEncoded: SoapReflector.ServiceDefaultIsEncoded(WebServiceReflector.GetMostDerivedType(methodInfos)), defaultNs: attribute.Namespace);
		WebMethodReflector.IncludeTypes(methodInfos, xmlReflectionImporter);
		ArrayList arrayList = new ArrayList();
		bool[] array = new bool[methodInfos.Length];
		for (int i = 0; i < methodInfos.Length; i++)
		{
			LogicalMethodInfo logicalMethodInfo = methodInfos[i];
			Type returnType = logicalMethodInfo.ReturnType;
			if (IsSupported(returnType) && HttpServerProtocol.AreUrlParametersSupported(logicalMethodInfo))
			{
				XmlAttributes xmlAttributes = new XmlAttributes(logicalMethodInfo.ReturnTypeCustomAttributeProvider);
				XmlTypeMapping xmlTypeMapping = xmlReflectionImporter.ImportTypeMapping(returnType, xmlAttributes.XmlRoot);
				xmlTypeMapping.SetKey(logicalMethodInfo.GetKey() + ":Return");
				arrayList.Add(xmlTypeMapping);
				array[i] = true;
			}
		}
		if (arrayList.Count == 0)
		{
			return new object[0];
		}
		XmlMapping[] array2 = (XmlMapping[])arrayList.ToArray(typeof(XmlMapping));
		Evidence evidenceForType = GetEvidenceForType(methodInfos[0].DeclaringType);
		TraceMethod caller = (Tracing.On ? new TraceMethod(typeof(XmlReturn), "GetInitializers", methodInfos) : null);
		if (Tracing.On)
		{
			Tracing.Enter(Tracing.TraceId("TraceCreateSerializer"), caller, new TraceMethod(typeof(XmlSerializer), "FromMappings", array2, evidenceForType));
		}
		XmlSerializer[] array3 = null;
		array3 = ((!AppDomain.CurrentDomain.IsHomogenous) ? XmlSerializer.FromMappings(array2, evidenceForType) : XmlSerializer.FromMappings(array2));
		if (Tracing.On)
		{
			Tracing.Exit(Tracing.TraceId("TraceCreateSerializer"), caller);
		}
		object[] array4 = new object[methodInfos.Length];
		int num = 0;
		for (int j = 0; j < array4.Length; j++)
		{
			if (array[j])
			{
				array4[j] = array3[num++];
			}
		}
		return array4;
	}

	private static bool IsSupported(Type returnType)
	{
		return returnType != typeof(void);
	}

	internal static object GetInitializer(LogicalMethodInfo methodInfo)
	{
		return GetInitializers(new LogicalMethodInfo[1] { methodInfo });
	}

	[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
	private static Evidence GetEvidenceForType(Type type)
	{
		return type.Assembly.Evidence;
	}
}
