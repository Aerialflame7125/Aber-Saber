using System.Collections.Generic;
using System.Security.Permissions;
using System.Security.Policy;
using System.Web.Services.Description;
using System.Web.Services.Diagnostics;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols;

/// <summary>Represents the attributes and metadata for an XML Web service method. This class cannot be inherited.</summary>
[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
public sealed class SoapServerMethod
{
	internal LogicalMethodInfo methodInfo;

	internal XmlSerializer returnSerializer;

	internal XmlSerializer parameterSerializer;

	internal XmlSerializer inHeaderSerializer;

	internal XmlSerializer outHeaderSerializer;

	internal SoapHeaderMapping[] inHeaderMappings;

	internal SoapHeaderMapping[] outHeaderMappings;

	internal SoapReflectedExtension[] extensions;

	internal object[] extensionInitializers;

	internal string action;

	internal bool oneWay;

	internal bool rpc;

	internal SoapBindingUse use;

	internal SoapParameterStyle paramStyle;

	internal WsiProfiles wsiClaims;

	/// <summary>Gets the <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> associated with this XML Web service method.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> associated with this XML Web service method.</returns>
	public LogicalMethodInfo MethodInfo => methodInfo;

	/// <summary>Gets the <see cref="T:System.Xml.Serialization.XmlSerializer" /> used with return values from this Web service method.</summary>
	/// <returns>The <see cref="T:System.Xml.Serialization.XmlSerializer" /> used with return values from this Web service method.</returns>
	public XmlSerializer ReturnSerializer => returnSerializer;

	/// <summary>Gets the <see cref="T:System.Xml.Serialization.XmlSerializer" /> used with parameters that are passed to this Web service method.</summary>
	/// <returns>The <see cref="T:System.Xml.Serialization.XmlSerializer" /> used with parameters that are passed to this Web service method.</returns>
	public XmlSerializer ParameterSerializer => parameterSerializer;

	/// <summary>Gets the <see cref="T:System.Xml.Serialization.XmlSerializer" /> used with SOAP requests to this Web service method.</summary>
	/// <returns>The <see cref="T:System.Xml.Serialization.XmlSerializer" /> used with SOAP requests to this Web service method.</returns>
	public XmlSerializer InHeaderSerializer => inHeaderSerializer;

	/// <summary>Gets the <see cref="T:System.Xml.Serialization.XmlSerializer" /> used with SOAP responses from this Web service method.</summary>
	/// <returns>The <see cref="T:System.Xml.Serialization.XmlSerializer" /> used with SOAP responses from this Web service method.</returns>
	public XmlSerializer OutHeaderSerializer => outHeaderSerializer;

	/// <summary>Gets the <see cref="T:System.Xml.Serialization.XmlMembersMapping" /> used with SOAP requests to this Web service method.</summary>
	/// <returns>The <see cref="T:System.Xml.Serialization.XmlMembersMapping" /> used with SOAP requests to this Web service method.</returns>
	public SoapHeaderMapping[] InHeaderMappings => inHeaderMappings;

	/// <summary>Gets the <see cref="T:System.Xml.Serialization.XmlMembersMapping" /> used with SOAP responses from this Web service method.</summary>
	/// <returns>The <see cref="T:System.Xml.Serialization.XmlMembersMapping" /> used with SOAP responses from this Web service method.</returns>
	public SoapHeaderMapping[] OutHeaderMappings => outHeaderMappings;

	/// <summary>Gets the <see langword="SOAPAction" /> HTTP header field of SOAP requests that are sent to this XML Web service method.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains the <see langword="SOAPAction" /> HTTP header field of SOAP requests that are sent to this XML Web service method.</returns>
	public string Action => action;

	/// <summary>Gets a <see cref="T:System.Boolean" /> that indicates whether an XML Web service client waits for the Web server to finish processing this XML Web service method.</summary>
	/// <returns>
	///     <see langword="true" /> if the XML Web service client does not wait for the Web server to completely process this XML Web service method; otherwise, <see langword="false" />.</returns>
	public bool OneWay => oneWay;

	/// <summary>Gets a <see cref="T:System.Boolean" /> that indicates whether SOAP messages sent to and from this XML Web service method use RPC formatting.</summary>
	/// <returns>
	///     <see langword="true" /> if SOAP messages sent to and from this XML Web service method use RPC formatting; otherwise, <see langword="false" />.</returns>
	public bool Rpc => rpc;

	/// <summary>Gets a <see cref="T:System.Web.Services.Description.SoapBindingUse" /> value that specifies whether the parts of SOAP messages sent to this XML Web service method are encoded as abstract type definitions or concrete schema definitions.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.SoapBindingUse" /> value that specifies whether the parts of SOAP messages sent to this XML Web service method are encoded as abstract type definitions or concrete schema definitions.</returns>
	public SoapBindingUse BindingUse => use;

	/// <summary>Gets a <see cref="T:System.Web.Services.Protocols.SoapParameterStyle" /> object that specifies how parameters are formatted in SOAP messages sent to this XML Web service method.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Protocols.SoapParameterStyle" /> that specifies how parameters are formatted in SOAP messages sent to this XML Web service method.</returns>
	public SoapParameterStyle ParameterStyle => paramStyle;

	/// <summary>Gets a <see cref="T:System.Web.Services.WsiProfiles" /> value that indicates the Web Services Interoperability (WSI) specification to which this Web service claims to conform.</summary>
	/// <returns>A <see cref="T:System.Web.Services.WsiProfiles" /> value that indicates the Web Services Interoperability (WSI) specification to which this Web service claims to conform.</returns>
	public WsiProfiles WsiClaims => wsiClaims;

	/// <summary>Creates a new <see cref="T:System.Web.Services.Protocols.SoapServerMethod" />.</summary>
	public SoapServerMethod()
	{
	}

	/// <summary>Creates a new <see cref="T:System.Web.Services.Protocols.SoapServerMethod" />.</summary>
	/// <param name="serverType">The <see cref="T:System.Type" /> to which this method belongs.</param>
	/// <param name="methodInfo">The <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> with which to initialize this <see cref="T:System.Web.Services.Protocols.SoapServerMethod" />.</param>
	public SoapServerMethod(Type serverType, LogicalMethodInfo methodInfo)
	{
		this.methodInfo = methodInfo;
		string @namespace = WebServiceReflector.GetAttribute(serverType).Namespace;
		bool serviceDefaultIsEncoded = SoapReflector.ServiceDefaultIsEncoded(serverType);
		SoapReflectionImporter soapReflectionImporter = SoapReflector.CreateSoapImporter(@namespace, serviceDefaultIsEncoded);
		XmlReflectionImporter xmlReflectionImporter = SoapReflector.CreateXmlImporter(@namespace, serviceDefaultIsEncoded);
		SoapReflector.IncludeTypes(methodInfo, soapReflectionImporter);
		WebMethodReflector.IncludeTypes(methodInfo, xmlReflectionImporter);
		SoapReflectedMethod soapMethod = SoapReflector.ReflectMethod(methodInfo, client: false, xmlReflectionImporter, soapReflectionImporter, @namespace);
		ImportReflectedMethod(soapMethod);
		ImportSerializers(soapMethod, GetServerTypeEvidence(serverType));
		ImportHeaderSerializers(soapMethod);
	}

	[SecurityPermission(SecurityAction.Assert, ControlEvidence = true)]
	private Evidence GetServerTypeEvidence(Type type)
	{
		return type.Assembly.Evidence;
	}

	private List<XmlMapping> GetXmlMappingsForMethod(SoapReflectedMethod soapMethod)
	{
		List<XmlMapping> list = new List<XmlMapping>();
		list.Add(soapMethod.requestMappings);
		if (soapMethod.responseMappings != null)
		{
			list.Add(soapMethod.responseMappings);
		}
		list.Add(soapMethod.inHeaderMappings);
		if (soapMethod.outHeaderMappings != null)
		{
			list.Add(soapMethod.outHeaderMappings);
		}
		return list;
	}

	private void ImportReflectedMethod(SoapReflectedMethod soapMethod)
	{
		action = soapMethod.action;
		extensions = soapMethod.extensions;
		extensionInitializers = SoapReflectedExtension.GetInitializers(methodInfo, soapMethod.extensions);
		oneWay = soapMethod.oneWay;
		rpc = soapMethod.rpc;
		use = soapMethod.use;
		paramStyle = soapMethod.paramStyle;
		wsiClaims = ((soapMethod.binding != null) ? soapMethod.binding.ConformsTo : WsiProfiles.None);
	}

	private void ImportHeaderSerializers(SoapReflectedMethod soapMethod)
	{
		List<SoapHeaderMapping> list = new List<SoapHeaderMapping>();
		List<SoapHeaderMapping> list2 = new List<SoapHeaderMapping>();
		for (int i = 0; i < soapMethod.headers.Length; i++)
		{
			SoapHeaderMapping soapHeaderMapping = new SoapHeaderMapping();
			SoapReflectedHeader soapReflectedHeader = soapMethod.headers[i];
			soapHeaderMapping.memberInfo = soapReflectedHeader.memberInfo;
			soapHeaderMapping.repeats = soapReflectedHeader.repeats;
			soapHeaderMapping.custom = soapReflectedHeader.custom;
			soapHeaderMapping.direction = soapReflectedHeader.direction;
			soapHeaderMapping.headerType = soapReflectedHeader.headerType;
			if (soapHeaderMapping.direction == SoapHeaderDirection.In)
			{
				list.Add(soapHeaderMapping);
				continue;
			}
			if (soapHeaderMapping.direction == SoapHeaderDirection.Out)
			{
				list2.Add(soapHeaderMapping);
				continue;
			}
			list.Add(soapHeaderMapping);
			list2.Add(soapHeaderMapping);
		}
		inHeaderMappings = list.ToArray();
		if (outHeaderSerializer != null)
		{
			outHeaderMappings = list2.ToArray();
		}
	}

	private void ImportSerializers(SoapReflectedMethod soapMethod, Evidence serverEvidence)
	{
		XmlMapping[] array = GetXmlMappingsForMethod(soapMethod).ToArray();
		TraceMethod caller = (Tracing.On ? new TraceMethod(this, "ImportSerializers") : null);
		if (Tracing.On)
		{
			Tracing.Enter(Tracing.TraceId("TraceCreateSerializer"), caller, new TraceMethod(typeof(XmlSerializer), "FromMappings", array, serverEvidence));
		}
		XmlSerializer[] array2 = null;
		array2 = ((!AppDomain.CurrentDomain.IsHomogenous) ? XmlSerializer.FromMappings(array, serverEvidence) : XmlSerializer.FromMappings(array));
		if (Tracing.On)
		{
			Tracing.Exit(Tracing.TraceId("TraceCreateSerializer"), caller);
		}
		int num = 0;
		parameterSerializer = array2[num++];
		if (soapMethod.responseMappings != null)
		{
			returnSerializer = array2[num++];
		}
		inHeaderSerializer = array2[num++];
		if (soapMethod.outHeaderMappings != null)
		{
			outHeaderSerializer = array2[num++];
		}
	}
}
