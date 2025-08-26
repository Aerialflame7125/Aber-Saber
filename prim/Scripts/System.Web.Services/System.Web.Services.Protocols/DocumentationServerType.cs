using System.Collections.Generic;
using System.Reflection;
using System.Web.Services.Description;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols;

internal class DocumentationServerType : ServerType
{
	private ServiceDescriptionCollection serviceDescriptions;

	private ServiceDescriptionCollection serviceDescriptionsWithPost;

	private XmlSchemas schemas;

	private XmlSchemas schemasWithPost;

	private LogicalMethodInfo methodInfo;

	public List<Action<Uri>> UriFixups { get; private set; }

	internal LogicalMethodInfo MethodInfo => methodInfo;

	internal XmlSchemas Schemas => schemas;

	internal ServiceDescriptionCollection ServiceDescriptions => serviceDescriptions;

	internal ServiceDescriptionCollection ServiceDescriptionsWithPost => serviceDescriptionsWithPost;

	internal XmlSchemas SchemasWithPost => schemasWithPost;

	private void AddUriFixup(Action<Uri> fixup)
	{
		if (UriFixups != null)
		{
			UriFixups.Add(fixup);
		}
	}

	internal DocumentationServerType(Type type, string uri, bool excludeSchemeHostPortFromCachingKey)
		: base(typeof(DocumentationServerProtocol))
	{
		if (excludeSchemeHostPortFromCachingKey)
		{
			UriFixups = new List<Action<Uri>>();
		}
		uri = new Uri(uri, dontEscape: true).GetLeftPart(UriPartial.Path);
		methodInfo = new LogicalMethodInfo(typeof(DocumentationServerProtocol).GetMethod("Documentation", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
		ServiceDescriptionReflector serviceDescriptionReflector = new ServiceDescriptionReflector(UriFixups);
		serviceDescriptionReflector.Reflect(type, uri);
		schemas = serviceDescriptionReflector.Schemas;
		serviceDescriptions = serviceDescriptionReflector.ServiceDescriptions;
		schemasWithPost = serviceDescriptionReflector.SchemasWithPost;
		serviceDescriptionsWithPost = serviceDescriptionReflector.ServiceDescriptionsWithPost;
	}
}
