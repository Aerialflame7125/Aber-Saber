using System.Collections;
using System.Reflection;
using System.Web.Services.Configuration;
using System.Web.Services.Diagnostics;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols;

internal class SoapClientType
{
	private Hashtable methods = new Hashtable();

	private WebServiceBindingAttribute binding;

	internal SoapReflectedExtension[] HighPriExtensions;

	internal SoapReflectedExtension[] LowPriExtensions;

	internal object[] HighPriExtensionInitializers;

	internal object[] LowPriExtensionInitializers;

	internal string serviceNamespace;

	internal bool serviceDefaultIsEncoded;

	internal WebServiceBindingAttribute Binding => binding;

	internal SoapClientType(Type type)
	{
		binding = WebServiceBindingReflector.GetAttribute(type);
		if (binding == null)
		{
			throw new InvalidOperationException(Res.GetString("WebClientBindingAttributeRequired"));
		}
		serviceNamespace = binding.Namespace;
		serviceDefaultIsEncoded = SoapReflector.ServiceDefaultIsEncoded(type);
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		GenerateXmlMappings(type, arrayList, serviceNamespace, serviceDefaultIsEncoded, arrayList2);
		XmlMapping[] array = (XmlMapping[])arrayList2.ToArray(typeof(XmlMapping));
		TraceMethod caller = (Tracing.On ? new TraceMethod(this, ".ctor", type) : null);
		if (Tracing.On)
		{
			Tracing.Enter(Tracing.TraceId("TraceCreateSerializer"), caller, new TraceMethod(typeof(XmlSerializer), "FromMappings", array, type));
		}
		XmlSerializer[] array2 = XmlSerializer.FromMappings(array, type);
		if (Tracing.On)
		{
			Tracing.Exit(Tracing.TraceId("TraceCreateSerializer"), caller);
		}
		SoapExtensionTypeElementCollection soapExtensionTypes = WebServicesSection.Current.SoapExtensionTypes;
		ArrayList arrayList3 = new ArrayList();
		ArrayList arrayList4 = new ArrayList();
		for (int i = 0; i < soapExtensionTypes.Count; i++)
		{
			_ = soapExtensionTypes[i];
			SoapReflectedExtension value = new SoapReflectedExtension(soapExtensionTypes[i].Type, null, soapExtensionTypes[i].Priority);
			if (soapExtensionTypes[i].Group == PriorityGroup.High)
			{
				arrayList3.Add(value);
			}
			else
			{
				arrayList4.Add(value);
			}
		}
		HighPriExtensions = (SoapReflectedExtension[])arrayList3.ToArray(typeof(SoapReflectedExtension));
		LowPriExtensions = (SoapReflectedExtension[])arrayList4.ToArray(typeof(SoapReflectedExtension));
		Array.Sort(HighPriExtensions);
		Array.Sort(LowPriExtensions);
		HighPriExtensionInitializers = SoapReflectedExtension.GetInitializers(type, HighPriExtensions);
		LowPriExtensionInitializers = SoapReflectedExtension.GetInitializers(type, LowPriExtensions);
		int num = 0;
		for (int j = 0; j < arrayList.Count; j++)
		{
			SoapReflectedMethod soapReflectedMethod = (SoapReflectedMethod)arrayList[j];
			SoapClientMethod soapClientMethod = new SoapClientMethod
			{
				parameterSerializer = array2[num++]
			};
			if (soapReflectedMethod.responseMappings != null)
			{
				soapClientMethod.returnSerializer = array2[num++];
			}
			soapClientMethod.inHeaderSerializer = array2[num++];
			if (soapReflectedMethod.outHeaderMappings != null)
			{
				soapClientMethod.outHeaderSerializer = array2[num++];
			}
			soapClientMethod.action = soapReflectedMethod.action;
			soapClientMethod.oneWay = soapReflectedMethod.oneWay;
			soapClientMethod.rpc = soapReflectedMethod.rpc;
			soapClientMethod.use = soapReflectedMethod.use;
			soapClientMethod.paramStyle = soapReflectedMethod.paramStyle;
			soapClientMethod.methodInfo = soapReflectedMethod.methodInfo;
			soapClientMethod.extensions = soapReflectedMethod.extensions;
			soapClientMethod.extensionInitializers = SoapReflectedExtension.GetInitializers(soapClientMethod.methodInfo, soapReflectedMethod.extensions);
			ArrayList arrayList5 = new ArrayList();
			ArrayList arrayList6 = new ArrayList();
			for (int k = 0; k < soapReflectedMethod.headers.Length; k++)
			{
				SoapHeaderMapping soapHeaderMapping = new SoapHeaderMapping();
				SoapReflectedHeader soapReflectedHeader = soapReflectedMethod.headers[k];
				soapHeaderMapping.memberInfo = soapReflectedHeader.memberInfo;
				soapHeaderMapping.repeats = soapReflectedHeader.repeats;
				soapHeaderMapping.custom = soapReflectedHeader.custom;
				soapHeaderMapping.direction = soapReflectedHeader.direction;
				soapHeaderMapping.headerType = soapReflectedHeader.headerType;
				if ((soapHeaderMapping.direction & SoapHeaderDirection.In) != 0)
				{
					arrayList5.Add(soapHeaderMapping);
				}
				if ((soapHeaderMapping.direction & (SoapHeaderDirection.Out | SoapHeaderDirection.Fault)) != 0)
				{
					arrayList6.Add(soapHeaderMapping);
				}
			}
			soapClientMethod.inHeaderMappings = (SoapHeaderMapping[])arrayList5.ToArray(typeof(SoapHeaderMapping));
			if (soapClientMethod.outHeaderSerializer != null)
			{
				soapClientMethod.outHeaderMappings = (SoapHeaderMapping[])arrayList6.ToArray(typeof(SoapHeaderMapping));
			}
			methods.Add(soapReflectedMethod.name, soapClientMethod);
		}
	}

	internal static void GenerateXmlMappings(Type type, ArrayList soapMethodList, string serviceNamespace, bool serviceDefaultIsEncoded, ArrayList mappings)
	{
		LogicalMethodInfo[] array = LogicalMethodInfo.Create(type.GetMethods(BindingFlags.Instance | BindingFlags.Public), LogicalMethodTypes.Sync);
		SoapReflectionImporter soapReflectionImporter = SoapReflector.CreateSoapImporter(serviceNamespace, serviceDefaultIsEncoded);
		XmlReflectionImporter xmlReflectionImporter = SoapReflector.CreateXmlImporter(serviceNamespace, serviceDefaultIsEncoded);
		WebMethodReflector.IncludeTypes(array, xmlReflectionImporter);
		SoapReflector.IncludeTypes(array, soapReflectionImporter);
		for (int i = 0; i < array.Length; i++)
		{
			SoapReflectedMethod soapReflectedMethod = SoapReflector.ReflectMethod(array[i], client: true, xmlReflectionImporter, soapReflectionImporter, serviceNamespace);
			if (soapReflectedMethod != null)
			{
				soapMethodList.Add(soapReflectedMethod);
				mappings.Add(soapReflectedMethod.requestMappings);
				if (soapReflectedMethod.responseMappings != null)
				{
					mappings.Add(soapReflectedMethod.responseMappings);
				}
				mappings.Add(soapReflectedMethod.inHeaderMappings);
				if (soapReflectedMethod.outHeaderMappings != null)
				{
					mappings.Add(soapReflectedMethod.outHeaderMappings);
				}
			}
		}
	}

	internal SoapClientMethod GetMethod(string name)
	{
		return (SoapClientMethod)methods[name];
	}
}
