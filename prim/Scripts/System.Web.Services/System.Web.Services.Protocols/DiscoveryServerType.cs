using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Web.Services.Description;
using System.Web.Services.Discovery;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols;

internal class DiscoveryServerType : ServerType
{
	private ServiceDescription description;

	private LogicalMethodInfo methodInfo;

	private Hashtable schemaTable = new Hashtable();

	private Hashtable wsdlTable = new Hashtable();

	private DiscoveryDocument discoDoc;

	public List<Action<Uri>> UriFixups { get; private set; }

	internal ServiceDescription Description => description;

	internal LogicalMethodInfo MethodInfo => methodInfo;

	internal DiscoveryDocument Disco => discoDoc;

	private void AddUriFixup(Action<Uri> fixup)
	{
		if (UriFixups != null)
		{
			UriFixups.Add(fixup);
		}
	}

	internal DiscoveryServerType(Type type, string uri, bool excludeSchemeHostPortFromCachingKey)
		: base(typeof(DiscoveryServerProtocol))
	{
		if (excludeSchemeHostPortFromCachingKey)
		{
			UriFixups = new List<Action<Uri>>();
		}
		uri = new Uri(uri, dontEscape: true).GetLeftPart(UriPartial.Path);
		methodInfo = new LogicalMethodInfo(typeof(DiscoveryServerProtocol).GetMethod("Discover", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
		ServiceDescriptionReflector serviceDescriptionReflector = new ServiceDescriptionReflector(UriFixups);
		serviceDescriptionReflector.Reflect(type, uri);
		XmlSchemas schemas = serviceDescriptionReflector.Schemas;
		description = serviceDescriptionReflector.ServiceDescription;
		_ = ServiceDescription.Serializer;
		AddSchemaImports(schemas, uri, serviceDescriptionReflector.ServiceDescriptions);
		for (int i = 1; i < serviceDescriptionReflector.ServiceDescriptions.Count; i++)
		{
			ServiceDescription serviceDescription = serviceDescriptionReflector.ServiceDescriptions[i];
			Import import = new Import();
			import.Namespace = serviceDescription.TargetNamespace;
			string text = "wsdl" + i.ToString(CultureInfo.InvariantCulture);
			import.Location = uri + "?wsdl=" + text;
			AddUriFixup(delegate(Uri current)
			{
				import.Location = CombineUris(current, import.Location);
			});
			serviceDescriptionReflector.ServiceDescription.Imports.Add(import);
			wsdlTable.Add(text, serviceDescription);
		}
		discoDoc = new DiscoveryDocument();
		ContractReference contractReference = new ContractReference(uri + "?wsdl", uri);
		AddUriFixup(delegate(Uri current)
		{
			contractReference.Ref = CombineUris(current, contractReference.Ref);
			contractReference.DocRef = CombineUris(current, contractReference.DocRef);
		});
		discoDoc.References.Add(contractReference);
		foreach (Service service in serviceDescriptionReflector.ServiceDescription.Services)
		{
			foreach (Port port in service.Ports)
			{
				SoapAddressBinding soapAddressBinding = (SoapAddressBinding)port.Extensions.Find(typeof(SoapAddressBinding));
				if (soapAddressBinding != null)
				{
					System.Web.Services.Discovery.SoapBinding binding = new System.Web.Services.Discovery.SoapBinding();
					binding.Binding = port.Binding;
					binding.Address = soapAddressBinding.Location;
					AddUriFixup(delegate(Uri current)
					{
						binding.Address = CombineUris(current, binding.Address);
					});
					discoDoc.References.Add(binding);
				}
			}
		}
	}

	internal void AddExternal(XmlSchema schema, string ns, string location)
	{
		if (schema == null)
		{
			return;
		}
		if (schema.TargetNamespace == ns)
		{
			XmlSchemaInclude include = new XmlSchemaInclude();
			include.SchemaLocation = location;
			AddUriFixup(delegate(Uri current)
			{
				include.SchemaLocation = CombineUris(current, include.SchemaLocation);
			});
			schema.Includes.Add(include);
			return;
		}
		XmlSchemaImport import = new XmlSchemaImport();
		import.SchemaLocation = location;
		AddUriFixup(delegate(Uri current)
		{
			import.SchemaLocation = CombineUris(current, import.SchemaLocation);
		});
		import.Namespace = ns;
		schema.Includes.Add(import);
	}

	private void AddSchemaImports(XmlSchemas schemas, string uri, ServiceDescriptionCollection descriptions)
	{
		int num = 0;
		foreach (XmlSchema schema in schemas)
		{
			if (schema == null)
			{
				continue;
			}
			if (schema.Id == null || schema.Id.Length == 0)
			{
				int num2 = num + 1;
				num = num2;
				schema.Id = "schema" + num2.ToString(CultureInfo.InvariantCulture);
			}
			string location = uri + "?schema=" + schema.Id;
			foreach (ServiceDescription description in descriptions)
			{
				if (description.Types.Schemas.Count == 0)
				{
					XmlSchema xmlSchema2 = new XmlSchema();
					xmlSchema2.TargetNamespace = description.TargetNamespace;
					schema.ElementFormDefault = XmlSchemaForm.Qualified;
					AddExternal(xmlSchema2, schema.TargetNamespace, location);
					description.Types.Schemas.Add(xmlSchema2);
				}
				else
				{
					AddExternal(description.Types.Schemas[0], schema.TargetNamespace, location);
				}
			}
			schemaTable.Add(schema.Id, schema);
		}
	}

	internal XmlSchema GetSchema(string id)
	{
		return (XmlSchema)schemaTable[id];
	}

	internal ServiceDescription GetServiceDescription(string id)
	{
		return (ServiceDescription)wsdlTable[id];
	}

	internal static string CombineUris(Uri schemeHostPort, string absolutePathAndQuery)
	{
		return string.Format(CultureInfo.InvariantCulture, "{0}://{1}{2}", schemeHostPort.Scheme, schemeHostPort.Authority, new Uri(absolutePathAndQuery).PathAndQuery);
	}
}
