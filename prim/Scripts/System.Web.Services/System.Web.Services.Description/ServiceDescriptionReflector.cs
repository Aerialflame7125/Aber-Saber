using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Web.Services.Configuration;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Provides a managed way of dynamically viewing, creating or invoking types supported by an XML Web service.</summary>
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
public class ServiceDescriptionReflector
{
	private ProtocolReflector[] reflectors;

	private ProtocolReflector[] reflectorsWithPost;

	private ServiceDescriptionCollection descriptions = new ServiceDescriptionCollection();

	private XmlSchemas schemas = new XmlSchemas();

	private ServiceDescriptionCollection descriptionsWithPost;

	private XmlSchemas schemasWithPost;

	private WebServiceAttribute serviceAttr;

	private ServiceDescription description;

	private Service service;

	private LogicalMethodInfo[] methods;

	private XmlSchemaExporter exporter;

	private XmlReflectionImporter importer;

	private Type serviceType;

	private string serviceUrl;

	private Hashtable reflectionContext;

	private List<Action<Uri>> uriFixups;

	internal List<Action<Uri>> UriFixups => uriFixups;

	/// <summary>Gets a reference to the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> associated with the XML Web service.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> associated with the XML Web service.</returns>
	public ServiceDescriptionCollection ServiceDescriptions => descriptions;

	/// <summary>Gets a reference to the <see cref="T:System.Xml.Serialization.XmlSchemas" /> associated with the XML Web service.</summary>
	/// <returns>An <see cref="T:System.Xml.Serialization.XmlSchemas" /> collection.</returns>
	public XmlSchemas Schemas => schemas;

	internal ServiceDescriptionCollection ServiceDescriptionsWithPost => descriptionsWithPost;

	internal XmlSchemas SchemasWithPost => schemasWithPost;

	internal ServiceDescription ServiceDescription => description;

	internal Service Service => service;

	internal Type ServiceType => serviceType;

	internal LogicalMethodInfo[] Methods => methods;

	internal string ServiceUrl => serviceUrl;

	internal XmlSchemaExporter SchemaExporter => exporter;

	internal XmlReflectionImporter ReflectionImporter => importer;

	internal WebServiceAttribute ServiceAttribute => serviceAttr;

	internal Hashtable ReflectionContext
	{
		get
		{
			if (reflectionContext == null)
			{
				reflectionContext = new Hashtable();
			}
			return reflectionContext;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.ServiceDescriptionReflector" /> class.</summary>
	public ServiceDescriptionReflector()
	{
		Type[] protocolReflectorTypes = WebServicesSection.Current.ProtocolReflectorTypes;
		reflectors = new ProtocolReflector[protocolReflectorTypes.Length];
		for (int i = 0; i < reflectors.Length; i++)
		{
			ProtocolReflector protocolReflector = (ProtocolReflector)Activator.CreateInstance(protocolReflectorTypes[i]);
			protocolReflector.Initialize(this);
			reflectors[i] = protocolReflector;
		}
		WebServiceProtocols enabledProtocols = WebServicesSection.Current.EnabledProtocols;
		if ((enabledProtocols & WebServiceProtocols.HttpPost) == 0 && (enabledProtocols & WebServiceProtocols.HttpPostLocalhost) != 0)
		{
			reflectorsWithPost = new ProtocolReflector[reflectors.Length + 1];
			for (int j = 0; j < reflectorsWithPost.Length - 1; j++)
			{
				ProtocolReflector protocolReflector2 = (ProtocolReflector)Activator.CreateInstance(protocolReflectorTypes[j]);
				protocolReflector2.Initialize(this);
				reflectorsWithPost[j] = protocolReflector2;
			}
			ProtocolReflector protocolReflector3 = new HttpPostProtocolReflector();
			protocolReflector3.Initialize(this);
			reflectorsWithPost[reflectorsWithPost.Length - 1] = protocolReflector3;
		}
	}

	internal ServiceDescriptionReflector(List<Action<Uri>> uriFixups)
		: this()
	{
		this.uriFixups = uriFixups;
	}

	private void ReflectInternal(ProtocolReflector[] reflectors)
	{
		description = new ServiceDescription();
		description.TargetNamespace = serviceAttr.Namespace;
		ServiceDescriptions.Add(description);
		service = new Service();
		string name = serviceAttr.Name;
		if (name == null || name.Length == 0)
		{
			name = serviceType.Name;
		}
		service.Name = XmlConvert.EncodeLocalName(name);
		if (serviceAttr.Description != null && serviceAttr.Description.Length > 0)
		{
			service.Documentation = serviceAttr.Description;
		}
		description.Services.Add(service);
		reflectionContext = new Hashtable();
		exporter = new XmlSchemaExporter(description.Types.Schemas);
		importer = SoapReflector.CreateXmlImporter(serviceAttr.Namespace, SoapReflector.ServiceDefaultIsEncoded(serviceType));
		WebMethodReflector.IncludeTypes(methods, importer);
		for (int i = 0; i < reflectors.Length; i++)
		{
			reflectors[i].Reflect();
		}
	}

	/// <summary>Creates a <see cref="T:System.Web.Services.Description.ServiceDescription" /> including the specified <see cref="T:System.Type" /> for the XML Web service at the specified URL.</summary>
	/// <param name="type">The <see cref="T:System.Type" /> of the class or interface to reflect. </param>
	/// <param name="url">The address (URL) of the XML Web service. </param>
	public void Reflect(Type type, string url)
	{
		serviceType = type;
		serviceUrl = url;
		serviceAttr = WebServiceReflector.GetAttribute(type);
		methods = WebMethodReflector.GetMethods(type);
		CheckForDuplicateMethods(methods);
		descriptionsWithPost = descriptions;
		schemasWithPost = schemas;
		if (reflectorsWithPost != null)
		{
			ReflectInternal(reflectorsWithPost);
			descriptions = new ServiceDescriptionCollection();
			schemas = new XmlSchemas();
		}
		ReflectInternal(reflectors);
		if (serviceAttr.Description != null && serviceAttr.Description.Length > 0)
		{
			ServiceDescription.Documentation = serviceAttr.Description;
		}
		ServiceDescription.Types.Schemas.Compile(null, fullCompile: false);
		if (ServiceDescriptions.Count > 1)
		{
			Schemas.Add(ServiceDescription.Types.Schemas);
			ServiceDescription.Types.Schemas.Clear();
		}
		else
		{
			if (ServiceDescription.Types.Schemas.Count <= 0)
			{
				return;
			}
			XmlSchema[] array = new XmlSchema[ServiceDescription.Types.Schemas.Count];
			ServiceDescription.Types.Schemas.CopyTo(array, 0);
			XmlSchema[] array2 = array;
			foreach (XmlSchema schema in array2)
			{
				if (XmlSchemas.IsDataSet(schema))
				{
					ServiceDescription.Types.Schemas.Remove(schema);
					Schemas.Add(schema);
				}
			}
		}
	}

	private void CheckForDuplicateMethods(LogicalMethodInfo[] methods)
	{
		Hashtable hashtable = new Hashtable();
		foreach (LogicalMethodInfo logicalMethodInfo in methods)
		{
			string text = logicalMethodInfo.MethodAttribute.MessageName;
			if (text.Length == 0)
			{
				text = logicalMethodInfo.Name;
			}
			string key = ((logicalMethodInfo.Binding == null) ? text : (logicalMethodInfo.Binding.Name + "." + text));
			LogicalMethodInfo logicalMethodInfo2 = (LogicalMethodInfo)hashtable[key];
			if (logicalMethodInfo2 != null)
			{
				throw new InvalidOperationException(Res.GetString("BothAndUseTheMessageNameUseTheMessageName3", logicalMethodInfo, logicalMethodInfo2, XmlConvert.EncodeLocalName(text)));
			}
			hashtable.Add(key, logicalMethodInfo);
		}
	}
}
