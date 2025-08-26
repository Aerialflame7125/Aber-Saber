using System.Collections;
using System.Security.Permissions;
using System.Web.Services.Configuration;
using System.Web.Services.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols;

/// <summary>The <see cref="T:System.Web.Services.Protocols.SoapServerType" /> class represents the type on which the XML Web service is based.</summary>
[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
public sealed class SoapServerType : ServerType
{
	private Hashtable methods = new Hashtable();

	private Hashtable duplicateMethods = new Hashtable();

	internal SoapReflectedExtension[] HighPriExtensions;

	internal SoapReflectedExtension[] LowPriExtensions;

	internal object[] HighPriExtensionInitializers;

	internal object[] LowPriExtensionInitializers;

	internal string serviceNamespace;

	internal bool serviceDefaultIsEncoded;

	internal bool routingOnSoapAction;

	internal WebServiceProtocols protocolsSupported;

	/// <summary>Gets a <see cref="T:System.String" /> that contains the namespace to which this XML Web service belongs.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains the namespace to which this XML Web service belongs.</returns>
	public string ServiceNamespace => serviceNamespace;

	/// <summary>Returns a <see cref="T:System.Boolean" /> that indicates whether SOAP data transmissions sent to and from this XML Web service are encoded by default.</summary>
	/// <returns>
	///     <see langword="true" /> if SOAP data transmissions sent to and from this XML Web service are encoded by default; otherwise, <see langword="false" />.</returns>
	public bool ServiceDefaultIsEncoded => serviceDefaultIsEncoded;

	/// <summary>Returns a <see cref="T:System.Boolean" /> that indicates whether SOAP messages that are routed to this XML Web service are routed based on the <see langword="SOAPAction" /> HTTP header.</summary>
	/// <returns>
	///     <see langword="true" /> if SOAP messages that are routed to this XML Web service are routed based on the <see langword="SOAPAction" /> HTTP header; otherwise, <see langword="false" />.</returns>
	public bool ServiceRoutingOnSoapAction => routingOnSoapAction;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapServerType" /> class.</summary>
	/// <param name="type">The <see cref="T:System.Type" /> on which this XML Web service is based.</param>
	/// <param name="protocolsSupported">A <see cref="T:System.Web.Services.Configuration.WebServiceProtocols" /> value that specifies the transmission protocols that are used to decrypt data sent in the XML Web service request.</param>
	public SoapServerType(Type type, WebServiceProtocols protocolsSupported)
		: base(type)
	{
		this.protocolsSupported = protocolsSupported;
		bool flag = (protocolsSupported & WebServiceProtocols.HttpSoap) != 0;
		LogicalMethodInfo[] array = WebMethodReflector.GetMethods(type);
		ArrayList arrayList = new ArrayList();
		WebServiceAttribute attribute = WebServiceReflector.GetAttribute(type);
		object soapServiceAttribute = SoapReflector.GetSoapServiceAttribute(type);
		routingOnSoapAction = SoapReflector.GetSoapServiceRoutingStyle(soapServiceAttribute) == SoapServiceRoutingStyle.SoapAction;
		serviceNamespace = attribute.Namespace;
		serviceDefaultIsEncoded = SoapReflector.ServiceDefaultIsEncoded(type);
		SoapReflectionImporter soapReflectionImporter = SoapReflector.CreateSoapImporter(serviceNamespace, serviceDefaultIsEncoded);
		XmlReflectionImporter xmlReflectionImporter = SoapReflector.CreateXmlImporter(serviceNamespace, serviceDefaultIsEncoded);
		SoapReflector.IncludeTypes(array, soapReflectionImporter);
		WebMethodReflector.IncludeTypes(array, xmlReflectionImporter);
		SoapReflectedMethod[] array2 = new SoapReflectedMethod[array.Length];
		SoapExtensionTypeElementCollection soapExtensionTypes = WebServicesSection.Current.SoapExtensionTypes;
		ArrayList arrayList2 = new ArrayList();
		ArrayList arrayList3 = new ArrayList();
		for (int i = 0; i < soapExtensionTypes.Count; i++)
		{
			SoapExtensionTypeElement soapExtensionTypeElement = soapExtensionTypes[i];
			if (soapExtensionTypeElement != null)
			{
				SoapReflectedExtension value = new SoapReflectedExtension(soapExtensionTypeElement.Type, null, soapExtensionTypeElement.Priority);
				if (soapExtensionTypeElement.Group == PriorityGroup.High)
				{
					arrayList2.Add(value);
				}
				else
				{
					arrayList3.Add(value);
				}
			}
		}
		HighPriExtensions = (SoapReflectedExtension[])arrayList2.ToArray(typeof(SoapReflectedExtension));
		LowPriExtensions = (SoapReflectedExtension[])arrayList3.ToArray(typeof(SoapReflectedExtension));
		Array.Sort(HighPriExtensions);
		Array.Sort(LowPriExtensions);
		HighPriExtensionInitializers = SoapReflectedExtension.GetInitializers(type, HighPriExtensions);
		LowPriExtensionInitializers = SoapReflectedExtension.GetInitializers(type, LowPriExtensions);
		for (int j = 0; j < array.Length; j++)
		{
			SoapReflectedMethod soapReflectedMethod = SoapReflector.ReflectMethod(array[j], client: false, xmlReflectionImporter, soapReflectionImporter, attribute.Namespace);
			arrayList.Add(soapReflectedMethod.requestMappings);
			if (soapReflectedMethod.responseMappings != null)
			{
				arrayList.Add(soapReflectedMethod.responseMappings);
			}
			arrayList.Add(soapReflectedMethod.inHeaderMappings);
			if (soapReflectedMethod.outHeaderMappings != null)
			{
				arrayList.Add(soapReflectedMethod.outHeaderMappings);
			}
			array2[j] = soapReflectedMethod;
		}
		XmlMapping[] array3 = (XmlMapping[])arrayList.ToArray(typeof(XmlMapping));
		TraceMethod caller = (Tracing.On ? new TraceMethod(this, ".ctor", type, protocolsSupported) : null);
		if (Tracing.On)
		{
			Tracing.Enter(Tracing.TraceId("TraceCreateSerializer"), caller, new TraceMethod(typeof(XmlSerializer), "FromMappings", array3, base.Evidence));
		}
		XmlSerializer[] array4 = null;
		array4 = ((!AppDomain.CurrentDomain.IsHomogenous) ? XmlSerializer.FromMappings(array3, base.Evidence) : XmlSerializer.FromMappings(array3));
		if (Tracing.On)
		{
			Tracing.Exit(Tracing.TraceId("TraceCreateSerializer"), caller);
		}
		int num = 0;
		for (int k = 0; k < array2.Length; k++)
		{
			SoapServerMethod soapServerMethod = new SoapServerMethod();
			SoapReflectedMethod soapReflectedMethod2 = array2[k];
			soapServerMethod.parameterSerializer = array4[num++];
			if (soapReflectedMethod2.responseMappings != null)
			{
				soapServerMethod.returnSerializer = array4[num++];
			}
			soapServerMethod.inHeaderSerializer = array4[num++];
			if (soapReflectedMethod2.outHeaderMappings != null)
			{
				soapServerMethod.outHeaderSerializer = array4[num++];
			}
			soapServerMethod.methodInfo = soapReflectedMethod2.methodInfo;
			soapServerMethod.action = soapReflectedMethod2.action;
			soapServerMethod.extensions = soapReflectedMethod2.extensions;
			soapServerMethod.extensionInitializers = SoapReflectedExtension.GetInitializers(soapServerMethod.methodInfo, soapReflectedMethod2.extensions);
			soapServerMethod.oneWay = soapReflectedMethod2.oneWay;
			soapServerMethod.rpc = soapReflectedMethod2.rpc;
			soapServerMethod.use = soapReflectedMethod2.use;
			soapServerMethod.paramStyle = soapReflectedMethod2.paramStyle;
			soapServerMethod.wsiClaims = ((soapReflectedMethod2.binding != null) ? soapReflectedMethod2.binding.ConformsTo : WsiProfiles.None);
			ArrayList arrayList4 = new ArrayList();
			ArrayList arrayList5 = new ArrayList();
			for (int l = 0; l < soapReflectedMethod2.headers.Length; l++)
			{
				SoapHeaderMapping soapHeaderMapping = new SoapHeaderMapping();
				SoapReflectedHeader soapReflectedHeader = soapReflectedMethod2.headers[l];
				soapHeaderMapping.memberInfo = soapReflectedHeader.memberInfo;
				soapHeaderMapping.repeats = soapReflectedHeader.repeats;
				soapHeaderMapping.custom = soapReflectedHeader.custom;
				soapHeaderMapping.direction = soapReflectedHeader.direction;
				soapHeaderMapping.headerType = soapReflectedHeader.headerType;
				if (soapHeaderMapping.direction == SoapHeaderDirection.In)
				{
					arrayList4.Add(soapHeaderMapping);
					continue;
				}
				if (soapHeaderMapping.direction == SoapHeaderDirection.Out)
				{
					arrayList5.Add(soapHeaderMapping);
					continue;
				}
				arrayList4.Add(soapHeaderMapping);
				arrayList5.Add(soapHeaderMapping);
			}
			soapServerMethod.inHeaderMappings = (SoapHeaderMapping[])arrayList4.ToArray(typeof(SoapHeaderMapping));
			if (soapServerMethod.outHeaderSerializer != null)
			{
				soapServerMethod.outHeaderMappings = (SoapHeaderMapping[])arrayList5.ToArray(typeof(SoapHeaderMapping));
			}
			if (flag && !routingOnSoapAction && soapReflectedMethod2.requestElementName.IsEmpty)
			{
				throw new SoapException(Res.GetString("TheMethodDoesNotHaveARequestElementEither1", soapServerMethod.methodInfo.Name), new XmlQualifiedName("Client", "http://schemas.xmlsoap.org/soap/envelope/"));
			}
			if (methods[soapReflectedMethod2.action] == null)
			{
				methods[soapReflectedMethod2.action] = soapServerMethod;
			}
			else
			{
				if (flag && routingOnSoapAction)
				{
					SoapServerMethod soapServerMethod2 = (SoapServerMethod)methods[soapReflectedMethod2.action];
					throw new SoapException(Res.GetString("TheMethodsAndUseTheSameSoapActionWhenTheService3", soapServerMethod.methodInfo.Name, soapServerMethod2.methodInfo.Name, soapReflectedMethod2.action), new XmlQualifiedName("Client", "http://schemas.xmlsoap.org/soap/envelope/"));
				}
				duplicateMethods[soapReflectedMethod2.action] = soapServerMethod;
			}
			if (methods[soapReflectedMethod2.requestElementName] == null)
			{
				methods[soapReflectedMethod2.requestElementName] = soapServerMethod;
				continue;
			}
			if (flag && !routingOnSoapAction)
			{
				SoapServerMethod soapServerMethod3 = (SoapServerMethod)methods[soapReflectedMethod2.requestElementName];
				throw new SoapException(Res.GetString("TheMethodsAndUseTheSameRequestElementXmlns4", soapServerMethod.methodInfo.Name, soapServerMethod3.methodInfo.Name, soapReflectedMethod2.requestElementName.Name, soapReflectedMethod2.requestElementName.Namespace), new XmlQualifiedName("Client", "http://schemas.xmlsoap.org/soap/envelope/"));
			}
			duplicateMethods[soapReflectedMethod2.requestElementName] = soapServerMethod;
		}
	}

	/// <summary>Returns the <see cref="T:System.Web.Services.Protocols.SoapServerMethod" /> associated with the specified key.</summary>
	/// <param name="key">The key associated with the desired <see cref="T:System.Web.Services.Protocols.SoapServerMethod" />.</param>
	/// <returns>The <see cref="T:System.Web.Services.Protocols.SoapServerMethod" /> associated with the specified key.</returns>
	public SoapServerMethod GetMethod(object key)
	{
		return (SoapServerMethod)methods[key];
	}

	/// <summary>Returns the duplicate <see cref="T:System.Web.Services.Protocols.SoapServerMethod" /> associated with the specified key.</summary>
	/// <param name="key">The key associated with the desired duplicate <see cref="T:System.Web.Services.Protocols.SoapServerMethod" />.</param>
	/// <returns>The duplicate <see cref="T:System.Web.Services.Protocols.SoapServerMethod" /> associated with the specified key.</returns>
	public SoapServerMethod GetDuplicateMethod(object key)
	{
		return (SoapServerMethod)duplicateMethods[key];
	}
}
