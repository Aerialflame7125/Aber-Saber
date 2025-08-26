using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Web.Services.Configuration;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.CSharp;

namespace System.Web.Services.Description;

/// <summary>Exposes a means of generating client proxy classes for XML Web services.</summary>
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
public class ServiceDescriptionImporter
{
	private ServiceDescriptionImportStyle style;

	private ServiceDescriptionCollection serviceDescriptions = new ServiceDescriptionCollection();

	private XmlSchemas schemas = new XmlSchemas();

	private XmlSchemas allSchemas = new XmlSchemas();

	private string protocolName;

	private CodeGenerationOptions options = CodeGenerationOptions.GenerateOldAsync;

	private CodeCompileUnit codeCompileUnit;

	private CodeDomProvider codeProvider;

	private ProtocolImporter[] importers;

	private XmlSchemas abstractSchemas = new XmlSchemas();

	private XmlSchemas concreteSchemas = new XmlSchemas();

	private List<Type> extensions;

	/// <summary>Gets the collection of <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances to be imported.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> instance that contains the <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances slated to be imported by the <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance.</returns>
	public ServiceDescriptionCollection ServiceDescriptions => serviceDescriptions;

	/// <summary>Gets the <see cref="T:System.Xml.Serialization.XmlSchemas" /> used by the <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.ServiceDescriptions" /> property.</summary>
	/// <returns>An <see cref="T:System.Xml.Serialization.XmlSchemas" /> object that contains the XML schemas used by the <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances in the <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.ServiceDescriptions" /> collection.</returns>
	public XmlSchemas Schemas => schemas;

	/// <summary>Gets or sets a value that determines the style of code (client or server) that is generated when the <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.ServiceDescriptions" /> values are imported.</summary>
	/// <returns>One of the <see cref="T:System.Web.Services.Description.ServiceDescriptionImportStyle" /> values. The default is <see cref="F:System.Web.Services.Description.ServiceDescriptionImportStyle.Client" />.</returns>
	public ServiceDescriptionImportStyle Style
	{
		get
		{
			return style;
		}
		set
		{
			style = value;
		}
	}

	/// <summary>Gets or sets various options for code generation.</summary>
	/// <returns>A member or combination of members of the <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> enumeration.</returns>
	[ComVisible(false)]
	public CodeGenerationOptions CodeGenerationOptions
	{
		get
		{
			return options;
		}
		set
		{
			options = value;
		}
	}

	internal CodeCompileUnit CodeCompileUnit => codeCompileUnit;

	/// <summary>Gets or sets the code generator used by the service description importer.</summary>
	/// <returns>The <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> interface used by the service description importer to generate proxy code.</returns>
	[ComVisible(false)]
	public CodeDomProvider CodeGenerator
	{
		get
		{
			if (codeProvider == null)
			{
				codeProvider = new CSharpCodeProvider();
			}
			return codeProvider;
		}
		set
		{
			codeProvider = value;
		}
	}

	internal List<Type> Extensions
	{
		get
		{
			if (extensions == null)
			{
				extensions = new List<Type>();
			}
			return extensions;
		}
	}

	/// <summary>Gets or sets the protocol used to access the described XML Web services.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains the case-insensitive name of the protocol to be imported.</returns>
	public string ProtocolName
	{
		get
		{
			if (protocolName != null)
			{
				return protocolName;
			}
			return string.Empty;
		}
		set
		{
			protocolName = value;
		}
	}

	internal XmlSchemas AllSchemas => allSchemas;

	internal XmlSchemas AbstractSchemas => abstractSchemas;

	internal XmlSchemas ConcreteSchemas => concreteSchemas;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> class.</summary>
	public ServiceDescriptionImporter()
	{
		Type[] protocolImporterTypes = WebServicesSection.Current.ProtocolImporterTypes;
		importers = new ProtocolImporter[protocolImporterTypes.Length];
		for (int i = 0; i < importers.Length; i++)
		{
			importers[i] = (ProtocolImporter)Activator.CreateInstance(protocolImporterTypes[i]);
			importers[i].Initialize(this);
		}
	}

	internal ServiceDescriptionImporter(CodeCompileUnit codeCompileUnit)
		: this()
	{
		this.codeCompileUnit = codeCompileUnit;
	}

	private ProtocolImporter FindImporterByName(string protocolName)
	{
		for (int i = 0; i < importers.Length; i++)
		{
			ProtocolImporter protocolImporter = importers[i];
			if (string.Compare(ProtocolName, protocolImporter.ProtocolName, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return protocolImporter;
			}
		}
		throw new ArgumentException(Res.GetString("ProtocolWithNameIsNotRecognized1", protocolName), "protocolName");
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.ServiceDescription" /> to the collection of <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.ServiceDescriptions" /> values to be imported.</summary>
	/// <param name="serviceDescription">The <see cref="T:System.Web.Services.Description.ServiceDescription" /> instance to add to the collection </param>
	/// <param name="appSettingUrlKey">Sets the initial value of the <see langword="Url" /> property of the proxy class to be generated from the instance represented by the <paramref name="serviceDescription" /> parameter. Specifies that it should be generated from the web.config file's <see langword="&lt;appsetting&gt;" /> section. </param>
	/// <param name="appSettingBaseUrl">Sets the initial value of the <see langword="Url" /> property of the proxy class to be generated from the instance represented by the <paramref name="serviceDescription" /> parameter. Specifies that it should be constructed from a combination of the value of this parameter and the URL specified by the <see langword="location" /> attribute in the WSDL document. </param>
	public void AddServiceDescription(ServiceDescription serviceDescription, string appSettingUrlKey, string appSettingBaseUrl)
	{
		if (serviceDescription == null)
		{
			throw new ArgumentNullException("serviceDescription");
		}
		serviceDescription.AppSettingUrlKey = appSettingUrlKey;
		serviceDescription.AppSettingBaseUrl = appSettingBaseUrl;
		ServiceDescriptions.Add(serviceDescription);
	}

	/// <summary>Imports the specified <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.ServiceDescriptions" /> values, that generates code as specified by the <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.Style" /> property.</summary>
	/// <param name="codeNamespace">The namespace into which the <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.ServiceDescriptions" /> values are imported. </param>
	/// <param name="codeCompileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> instance in which the code that represents the <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.ServiceDescriptions" /> value is generated. </param>
	/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescriptionImportWarnings" /> value that describes any error that occurred; or 0 if no error occurred.</returns>
	public ServiceDescriptionImportWarnings Import(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit)
	{
		if (codeCompileUnit != null)
		{
			codeCompileUnit.ReferencedAssemblies.Add("System.dll");
			codeCompileUnit.ReferencedAssemblies.Add("System.Xml.dll");
			codeCompileUnit.ReferencedAssemblies.Add("System.Web.Services.dll");
			codeCompileUnit.ReferencedAssemblies.Add("System.EnterpriseServices.dll");
		}
		return Import(codeNamespace, new ImportContext(new CodeIdentifiers(), shareTypes: false), new Hashtable(), new StringCollection());
	}

	/// <summary>Compiles a collection of Web references to produce a client proxy or a server stub.</summary>
	/// <param name="webReferences">A <see cref="T:System.Web.Services.Description.WebReferenceCollection" /> of Web references to compile.</param>
	/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> that specifies the code provider.</param>
	/// <param name="codeCompileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> that specifies the unit into which code is compiled.</param>
	/// <param name="options">A <see cref="T:System.Web.Services.Description.WebReferenceOptions" /> that specifies code generation options.</param>
	/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> of compiler warnings.</returns>
	public static StringCollection GenerateWebReferences(WebReferenceCollection webReferences, CodeDomProvider codeProvider, CodeCompileUnit codeCompileUnit, WebReferenceOptions options)
	{
		if (codeCompileUnit != null)
		{
			codeCompileUnit.ReferencedAssemblies.Add("System.dll");
			codeCompileUnit.ReferencedAssemblies.Add("System.Xml.dll");
			codeCompileUnit.ReferencedAssemblies.Add("System.Web.Services.dll");
			codeCompileUnit.ReferencedAssemblies.Add("System.EnterpriseServices.dll");
		}
		Hashtable hashtable = new Hashtable();
		Hashtable exportContext = new Hashtable();
		foreach (WebReference webReference in webReferences)
		{
			ServiceDescriptionImporter serviceDescriptionImporter = new ServiceDescriptionImporter(codeCompileUnit);
			XmlSchemas xmlSchemas = new XmlSchemas();
			ServiceDescriptionCollection serviceDescriptionCollection = new ServiceDescriptionCollection();
			foreach (DictionaryEntry document in webReference.Documents)
			{
				AddDocument((string)document.Key, document.Value, xmlSchemas, serviceDescriptionCollection, webReference.ValidationWarnings);
			}
			serviceDescriptionImporter.Schemas.Add(xmlSchemas);
			foreach (ServiceDescription item in serviceDescriptionCollection)
			{
				serviceDescriptionImporter.AddServiceDescription(item, webReference.AppSettingUrlKey, webReference.AppSettingBaseUrl);
			}
			serviceDescriptionImporter.CodeGenerator = codeProvider;
			serviceDescriptionImporter.ProtocolName = webReference.ProtocolName;
			serviceDescriptionImporter.Style = options.Style;
			serviceDescriptionImporter.CodeGenerationOptions = options.CodeGenerationOptions;
			StringEnumerator enumerator3 = options.SchemaImporterExtensions.GetEnumerator();
			try
			{
				while (enumerator3.MoveNext())
				{
					string current = enumerator3.Current;
					serviceDescriptionImporter.Extensions.Add(Type.GetType(current, throwOnError: true));
				}
			}
			finally
			{
				if (enumerator3 is IDisposable disposable)
				{
					disposable.Dispose();
				}
			}
			ImportContext importContext = Context(webReference.ProxyCode, hashtable, options.Verbose);
			webReference.Warnings = serviceDescriptionImporter.Import(webReference.ProxyCode, importContext, exportContext, webReference.ValidationWarnings);
			if (webReference.ValidationWarnings.Count != 0)
			{
				webReference.Warnings |= ServiceDescriptionImportWarnings.SchemaValidation;
			}
		}
		StringCollection stringCollection = new StringCollection();
		if (options.Verbose)
		{
			foreach (ImportContext value in hashtable.Values)
			{
				StringEnumerator enumerator3 = value.Warnings.GetEnumerator();
				try
				{
					while (enumerator3.MoveNext())
					{
						string current2 = enumerator3.Current;
						stringCollection.Add(current2);
					}
				}
				finally
				{
					if (enumerator3 is IDisposable disposable2)
					{
						disposable2.Dispose();
					}
				}
			}
		}
		return stringCollection;
	}

	internal static ImportContext Context(CodeNamespace ns, Hashtable namespaces, bool verbose)
	{
		if (namespaces[ns.Name] == null)
		{
			namespaces[ns.Name] = new ImportContext(new CodeIdentifiers(), shareTypes: true);
		}
		return (ImportContext)namespaces[ns.Name];
	}

	internal static void AddDocument(string path, object document, XmlSchemas schemas, ServiceDescriptionCollection descriptions, StringCollection warnings)
	{
		if (document is ServiceDescription serviceDescription)
		{
			descriptions.Add(serviceDescription);
		}
		else if (document is XmlSchema schema)
		{
			schemas.Add(schema);
		}
	}

	private void FindUse(MessagePart part, out bool isEncoded, out bool isLiteral)
	{
		isEncoded = false;
		isLiteral = false;
		string name = part.Message.Name;
		Operation operation = null;
		ServiceDescription serviceDescription = part.Message.ServiceDescription;
		foreach (PortType portType in serviceDescription.PortTypes)
		{
			foreach (Operation operation2 in portType.Operations)
			{
				foreach (OperationMessage message in operation2.Messages)
				{
					if (message.Message.Equals(new XmlQualifiedName(part.Message.Name, serviceDescription.TargetNamespace)))
					{
						operation = operation2;
						FindUse(operation, serviceDescription, name, ref isEncoded, ref isLiteral);
					}
				}
			}
		}
		if (operation == null)
		{
			FindUse(null, serviceDescription, name, ref isEncoded, ref isLiteral);
		}
	}

	private void FindUse(Operation operation, ServiceDescription description, string messageName, ref bool isEncoded, ref bool isLiteral)
	{
		string targetNamespace = description.TargetNamespace;
		foreach (Binding binding in description.Bindings)
		{
			if (operation != null && !new XmlQualifiedName(operation.PortType.Name, targetNamespace).Equals(binding.Type))
			{
				continue;
			}
			foreach (OperationBinding operation2 in binding.Operations)
			{
				if (operation2.Input != null)
				{
					foreach (object extension in operation2.Input.Extensions)
					{
						if (operation != null)
						{
							if (extension is SoapBodyBinding soapBodyBinding && operation.IsBoundBy(operation2))
							{
								if (soapBodyBinding.Use == SoapBindingUse.Encoded)
								{
									isEncoded = true;
								}
								else if (soapBodyBinding.Use == SoapBindingUse.Literal)
								{
									isLiteral = true;
								}
							}
						}
						else if (extension is SoapHeaderBinding soapHeaderBinding && soapHeaderBinding.Message.Name == messageName)
						{
							if (soapHeaderBinding.Use == SoapBindingUse.Encoded)
							{
								isEncoded = true;
							}
							else if (soapHeaderBinding.Use == SoapBindingUse.Literal)
							{
								isLiteral = true;
							}
						}
					}
				}
				if (operation2.Output == null)
				{
					continue;
				}
				foreach (object extension2 in operation2.Output.Extensions)
				{
					if (operation != null)
					{
						if (!operation.IsBoundBy(operation2))
						{
							continue;
						}
						if (extension2 is SoapBodyBinding soapBodyBinding2)
						{
							if (soapBodyBinding2.Use == SoapBindingUse.Encoded)
							{
								isEncoded = true;
							}
							else if (soapBodyBinding2.Use == SoapBindingUse.Literal)
							{
								isLiteral = true;
							}
						}
						else if (extension2 is MimeXmlBinding)
						{
							isLiteral = true;
						}
					}
					else if (extension2 is SoapHeaderBinding soapHeaderBinding2 && soapHeaderBinding2.Message.Name == messageName)
					{
						if (soapHeaderBinding2.Use == SoapBindingUse.Encoded)
						{
							isEncoded = true;
						}
						else if (soapHeaderBinding2.Use == SoapBindingUse.Literal)
						{
							isLiteral = true;
						}
					}
				}
			}
		}
	}

	private void AddImport(XmlSchema schema, Hashtable imports)
	{
		if (schema == null || imports[schema] != null)
		{
			return;
		}
		imports.Add(schema, schema);
		foreach (XmlSchemaExternal include in schema.Includes)
		{
			if (!(include is XmlSchemaImport))
			{
				continue;
			}
			XmlSchemaImport xmlSchemaImport = (XmlSchemaImport)include;
			foreach (XmlSchema schema2 in allSchemas.GetSchemas(xmlSchemaImport.Namespace))
			{
				AddImport(schema2, imports);
			}
		}
	}

	private ServiceDescriptionImportWarnings Import(CodeNamespace codeNamespace, ImportContext importContext, Hashtable exportContext, StringCollection warnings)
	{
		allSchemas = new XmlSchemas();
		foreach (XmlSchema schema7 in schemas)
		{
			allSchemas.Add(schema7);
		}
		foreach (ServiceDescription serviceDescription in serviceDescriptions)
		{
			foreach (XmlSchema schema8 in serviceDescription.Types.Schemas)
			{
				allSchemas.Add(schema8);
			}
		}
		Hashtable hashtable = new Hashtable();
		if (!allSchemas.Contains("http://schemas.xmlsoap.org/wsdl/"))
		{
			allSchemas.AddReference(ServiceDescription.Schema);
			hashtable[ServiceDescription.Schema] = ServiceDescription.Schema;
		}
		if (!allSchemas.Contains("http://schemas.xmlsoap.org/soap/encoding/"))
		{
			allSchemas.AddReference(ServiceDescription.SoapEncodingSchema);
			hashtable[ServiceDescription.SoapEncodingSchema] = ServiceDescription.SoapEncodingSchema;
		}
		allSchemas.Compile(null, fullCompile: false);
		foreach (ServiceDescription serviceDescription2 in serviceDescriptions)
		{
			foreach (Message message in serviceDescription2.Messages)
			{
				foreach (MessagePart part in message.Parts)
				{
					FindUse(part, out var isEncoded, out var isLiteral);
					if (part.Element != null && !part.Element.IsEmpty)
					{
						if (isEncoded)
						{
							throw new InvalidOperationException(Res.GetString("CanTSpecifyElementOnEncodedMessagePartsPart", part.Name, message.Name));
						}
						XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)allSchemas.Find(part.Element, typeof(XmlSchemaElement));
						if (xmlSchemaElement != null)
						{
							AddSchema(xmlSchemaElement.Parent as XmlSchema, isEncoded, isLiteral, abstractSchemas, concreteSchemas, hashtable);
							if (xmlSchemaElement.SchemaTypeName != null && !xmlSchemaElement.SchemaTypeName.IsEmpty)
							{
								XmlSchemaType xmlSchemaType = (XmlSchemaType)allSchemas.Find(xmlSchemaElement.SchemaTypeName, typeof(XmlSchemaType));
								if (xmlSchemaType != null)
								{
									AddSchema(xmlSchemaType.Parent as XmlSchema, isEncoded, isLiteral, abstractSchemas, concreteSchemas, hashtable);
								}
							}
						}
					}
					if (part.Type != null && !part.Type.IsEmpty)
					{
						XmlSchemaType xmlSchemaType2 = (XmlSchemaType)allSchemas.Find(part.Type, typeof(XmlSchemaType));
						if (xmlSchemaType2 != null)
						{
							AddSchema(xmlSchemaType2.Parent as XmlSchema, isEncoded, isLiteral, abstractSchemas, concreteSchemas, hashtable);
						}
					}
				}
			}
		}
		XmlSchemas[] array = new XmlSchemas[2] { abstractSchemas, concreteSchemas };
		Hashtable hashtable2;
		foreach (XmlSchemas xmlSchemas in array)
		{
			hashtable2 = new Hashtable();
			foreach (XmlSchema item in xmlSchemas)
			{
				AddImport(item, hashtable2);
			}
			foreach (XmlSchema key in hashtable2.Keys)
			{
				if (hashtable[key] == null && !xmlSchemas.Contains(key))
				{
					xmlSchemas.Add(key);
				}
			}
		}
		hashtable2 = new Hashtable();
		foreach (XmlSchema allSchema in allSchemas)
		{
			if (!abstractSchemas.Contains(allSchema) && !concreteSchemas.Contains(allSchema))
			{
				AddImport(allSchema, hashtable2);
			}
		}
		foreach (XmlSchema key2 in hashtable2.Keys)
		{
			if (hashtable[key2] == null)
			{
				if (!abstractSchemas.Contains(key2))
				{
					abstractSchemas.Add(key2);
				}
				if (!concreteSchemas.Contains(key2))
				{
					concreteSchemas.Add(key2);
				}
			}
		}
		if (abstractSchemas.Count > 0)
		{
			foreach (XmlSchema value in hashtable.Values)
			{
				abstractSchemas.AddReference(value);
			}
			StringEnumerator enumerator4 = SchemaCompiler.Compile(abstractSchemas).GetEnumerator();
			try
			{
				while (enumerator4.MoveNext())
				{
					string current = enumerator4.Current;
					warnings.Add(current);
				}
			}
			finally
			{
				if (enumerator4 is IDisposable disposable)
				{
					disposable.Dispose();
				}
			}
		}
		if (concreteSchemas.Count > 0)
		{
			foreach (XmlSchema value2 in hashtable.Values)
			{
				concreteSchemas.AddReference(value2);
			}
			StringEnumerator enumerator4 = SchemaCompiler.Compile(concreteSchemas).GetEnumerator();
			try
			{
				while (enumerator4.MoveNext())
				{
					string current2 = enumerator4.Current;
					warnings.Add(current2);
				}
			}
			finally
			{
				if (enumerator4 is IDisposable disposable2)
				{
					disposable2.Dispose();
				}
			}
		}
		if (ProtocolName.Length > 0)
		{
			ProtocolImporter protocolImporter = FindImporterByName(ProtocolName);
			if (protocolImporter.GenerateCode(codeNamespace, importContext, exportContext))
			{
				return protocolImporter.Warnings;
			}
		}
		else
		{
			for (int j = 0; j < importers.Length; j++)
			{
				ProtocolImporter protocolImporter2 = importers[j];
				if (protocolImporter2.GenerateCode(codeNamespace, importContext, exportContext))
				{
					return protocolImporter2.Warnings;
				}
			}
		}
		return ServiceDescriptionImportWarnings.NoCodeGenerated;
	}

	private static void AddSchema(XmlSchema schema, bool isEncoded, bool isLiteral, XmlSchemas abstractSchemas, XmlSchemas concreteSchemas, Hashtable references)
	{
		if (schema == null)
		{
			return;
		}
		if (isEncoded && !abstractSchemas.Contains(schema))
		{
			if (references.Contains(schema))
			{
				abstractSchemas.AddReference(schema);
			}
			else
			{
				abstractSchemas.Add(schema);
			}
		}
		if (isLiteral && !concreteSchemas.Contains(schema))
		{
			if (references.Contains(schema))
			{
				concreteSchemas.AddReference(schema);
			}
			else
			{
				concreteSchemas.Add(schema);
			}
		}
	}
}
