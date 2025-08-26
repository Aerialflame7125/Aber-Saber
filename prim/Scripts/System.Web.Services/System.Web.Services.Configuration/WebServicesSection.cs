using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading;
using System.Web.Services.Description;
using System.Web.Services.Discovery;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace System.Web.Services.Configuration;

/// <summary>Represents the <see langword="webServices" /> element in the configuration file. This element controls the settings of XML Web services.</summary>
public sealed class WebServicesSection : ConfigurationSection
{
	private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

	private static object classSyncObject;

	private const string SectionName = "system.web/webServices";

	private readonly ConfigurationProperty conformanceWarnings = new ConfigurationProperty("conformanceWarnings", typeof(WsiProfilesElementCollection), null, ConfigurationPropertyOptions.None);

	private readonly ConfigurationProperty protocols = new ConfigurationProperty("protocols", typeof(ProtocolElementCollection), null, ConfigurationPropertyOptions.None);

	private readonly ConfigurationProperty serviceDescriptionFormatExtensionTypes = new ConfigurationProperty("serviceDescriptionFormatExtensionTypes", typeof(TypeElementCollection), null, ConfigurationPropertyOptions.None);

	private readonly ConfigurationProperty soapEnvelopeProcessing = new ConfigurationProperty("soapEnvelopeProcessing", typeof(SoapEnvelopeProcessingElement), null, ConfigurationPropertyOptions.None);

	private readonly ConfigurationProperty soapExtensionImporterTypes = new ConfigurationProperty("soapExtensionImporterTypes", typeof(TypeElementCollection), null, ConfigurationPropertyOptions.None);

	private readonly ConfigurationProperty soapExtensionReflectorTypes = new ConfigurationProperty("soapExtensionReflectorTypes", typeof(TypeElementCollection), null, ConfigurationPropertyOptions.None);

	private readonly ConfigurationProperty soapExtensionTypes = new ConfigurationProperty("soapExtensionTypes", typeof(SoapExtensionTypeElementCollection), null, ConfigurationPropertyOptions.None);

	private readonly ConfigurationProperty soapTransportImporterTypes = new ConfigurationProperty("soapTransportImporterTypes", typeof(TypeElementCollection), null, ConfigurationPropertyOptions.None);

	private readonly ConfigurationProperty wsdlHelpGenerator = new ConfigurationProperty("wsdlHelpGenerator", typeof(WsdlHelpGeneratorElement), null, ConfigurationPropertyOptions.None);

	private readonly ConfigurationProperty soapServerProtocolFactoryType = new ConfigurationProperty("soapServerProtocolFactory", typeof(TypeElement), null, ConfigurationPropertyOptions.None);

	private readonly ConfigurationProperty diagnostics = new ConfigurationProperty("diagnostics", typeof(DiagnosticsElement), null, ConfigurationPropertyOptions.None);

	private Type[] defaultFormatTypes = new Type[21]
	{
		typeof(HttpAddressBinding),
		typeof(HttpBinding),
		typeof(HttpOperationBinding),
		typeof(HttpUrlEncodedBinding),
		typeof(HttpUrlReplacementBinding),
		typeof(MimeContentBinding),
		typeof(MimeXmlBinding),
		typeof(MimeMultipartRelatedBinding),
		typeof(MimeTextBinding),
		typeof(System.Web.Services.Description.SoapBinding),
		typeof(SoapOperationBinding),
		typeof(SoapBodyBinding),
		typeof(SoapFaultBinding),
		typeof(SoapHeaderBinding),
		typeof(SoapAddressBinding),
		typeof(Soap12Binding),
		typeof(Soap12OperationBinding),
		typeof(Soap12BodyBinding),
		typeof(Soap12FaultBinding),
		typeof(Soap12HeaderBinding),
		typeof(Soap12AddressBinding)
	};

	private Type[] discoveryReferenceTypes = new Type[4]
	{
		typeof(DiscoveryDocumentReference),
		typeof(ContractReference),
		typeof(SchemaReference),
		typeof(System.Web.Services.Discovery.SoapBinding)
	};

	private XmlSerializer discoveryDocumentSerializer;

	private WebServiceProtocols enabledProtocols;

	private Type[] mimeImporterTypes = new Type[3]
	{
		typeof(MimeXmlImporter),
		typeof(MimeFormImporter),
		typeof(MimeTextImporter)
	};

	private Type[] mimeReflectorTypes = new Type[2]
	{
		typeof(MimeXmlReflector),
		typeof(MimeFormReflector)
	};

	private Type[] parameterReaderTypes = new Type[2]
	{
		typeof(UrlParameterReader),
		typeof(HtmlFormParameterReader)
	};

	private Type[] protocolImporterTypes = new Type[0];

	private Type[] protocolReflectorTypes = new Type[0];

	private Type[] returnWriterTypes = new Type[1] { typeof(XmlReturnWriter) };

	private ServerProtocolFactory[] serverProtocolFactories;

	private Type soapServerProtocolFactory;

	private static object ClassSyncObject
	{
		get
		{
			if (classSyncObject == null)
			{
				object value = new object();
				Interlocked.CompareExchange(ref classSyncObject, value, null);
			}
			return classSyncObject;
		}
	}

	/// <summary>Gets the collection of conformance warnings for the Web Service. This property corresponds to the <see langword="configurationWarnings" /> element in the configuration file.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Configuration.WsiProfilesElementCollection" /> object that represents the collection of conformance warnings for the Web Service.</returns>
	[ConfigurationProperty("conformanceWarnings")]
	public WsiProfilesElementCollection ConformanceWarnings => (WsiProfilesElementCollection)base[conformanceWarnings];

	internal WsiProfiles EnabledConformanceWarnings
	{
		get
		{
			WsiProfiles wsiProfiles = WsiProfiles.None;
			foreach (WsiProfilesElement conformanceWarning in ConformanceWarnings)
			{
				wsiProfiles |= conformanceWarning.Name;
			}
			return wsiProfiles;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.Services.Configuration.WebServicesSection" /> object that represents the current section.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Configuration.WebServicesSection" /> object.</returns>
	public static WebServicesSection Current
	{
		get
		{
			WebServicesSection webServicesSection = null;
			if (Thread.GetDomain().GetData(".appDomain") != null)
			{
				webServicesSection = GetConfigFromHttpContext();
			}
			if (webServicesSection == null)
			{
				webServicesSection = (WebServicesSection)System.Configuration.PrivilegedConfigurationManager.GetSection("system.web/webServices");
			}
			return webServicesSection;
		}
	}

	internal XmlSerializer DiscoveryDocumentSerializer
	{
		get
		{
			if (discoveryDocumentSerializer == null)
			{
				lock (ClassSyncObject)
				{
					if (discoveryDocumentSerializer == null)
					{
						XmlAttributeOverrides xmlAttributeOverrides = new XmlAttributeOverrides();
						XmlAttributes xmlAttributes = new XmlAttributes();
						Type[] array = DiscoveryReferenceTypes;
						foreach (Type type in array)
						{
							object[] customAttributes = type.GetCustomAttributes(typeof(XmlRootAttribute), inherit: false);
							if (customAttributes.Length == 0)
							{
								throw new InvalidOperationException(Res.GetString("WebMissingCustomAttribute", type.FullName, "XmlRoot"));
							}
							string elementName = ((XmlRootAttribute)customAttributes[0]).ElementName;
							string @namespace = ((XmlRootAttribute)customAttributes[0]).Namespace;
							XmlElementAttribute xmlElementAttribute = new XmlElementAttribute(elementName, type);
							xmlElementAttribute.Namespace = @namespace;
							xmlAttributes.XmlElements.Add(xmlElementAttribute);
						}
						xmlAttributeOverrides.Add(typeof(DiscoveryDocument), "References", xmlAttributes);
						discoveryDocumentSerializer = new DiscoveryDocumentSerializer();
					}
				}
			}
			return discoveryDocumentSerializer;
		}
	}

	internal Type[] DiscoveryReferenceTypes => discoveryReferenceTypes;

	/// <summary>Gets one of the <see cref="T:System.Web.Services.Configuration.WebServiceProtocols" /> values that indicates the Web service protocol.</summary>
	/// <returns>One of the <see cref="T:System.Web.Services.Configuration.WebServiceProtocols" /> values.</returns>
	public WebServiceProtocols EnabledProtocols
	{
		get
		{
			if (enabledProtocols == WebServiceProtocols.Unknown)
			{
				lock (ClassSyncObject)
				{
					if (enabledProtocols == WebServiceProtocols.Unknown)
					{
						WebServiceProtocols webServiceProtocols = WebServiceProtocols.Unknown;
						foreach (ProtocolElement protocol in Protocols)
						{
							webServiceProtocols |= protocol.Name;
						}
						enabledProtocols = webServiceProtocols;
					}
				}
			}
			return enabledProtocols;
		}
	}

	internal Type[] MimeImporterTypes => mimeImporterTypes;

	internal Type[] MimeReflectorTypes => mimeReflectorTypes;

	internal Type[] ParameterReaderTypes => parameterReaderTypes;

	protected override ConfigurationPropertyCollection Properties => properties;

	internal Type[] ProtocolImporterTypes
	{
		get
		{
			if (protocolImporterTypes.Length == 0)
			{
				lock (ClassSyncObject)
				{
					if (protocolImporterTypes.Length == 0)
					{
						WebServiceProtocols num = EnabledProtocols;
						List<Type> list = new List<Type>();
						if ((num & WebServiceProtocols.HttpSoap) != 0)
						{
							list.Add(typeof(SoapProtocolImporter));
						}
						if ((num & WebServiceProtocols.HttpSoap12) != 0)
						{
							list.Add(typeof(Soap12ProtocolImporter));
						}
						if ((num & WebServiceProtocols.HttpGet) != 0)
						{
							list.Add(typeof(HttpGetProtocolImporter));
						}
						if ((num & WebServiceProtocols.HttpPost) != 0)
						{
							list.Add(typeof(HttpPostProtocolImporter));
						}
						protocolImporterTypes = list.ToArray();
					}
				}
			}
			return protocolImporterTypes;
		}
		set
		{
			protocolImporterTypes = value;
		}
	}

	internal Type[] ProtocolReflectorTypes
	{
		get
		{
			if (protocolReflectorTypes.Length == 0)
			{
				lock (ClassSyncObject)
				{
					if (protocolReflectorTypes.Length == 0)
					{
						WebServiceProtocols num = EnabledProtocols;
						List<Type> list = new List<Type>();
						if ((num & WebServiceProtocols.HttpSoap) != 0)
						{
							list.Add(typeof(SoapProtocolReflector));
						}
						if ((num & WebServiceProtocols.HttpSoap12) != 0)
						{
							list.Add(typeof(Soap12ProtocolReflector));
						}
						if ((num & WebServiceProtocols.HttpGet) != 0)
						{
							list.Add(typeof(HttpGetProtocolReflector));
						}
						if ((num & WebServiceProtocols.HttpPost) != 0)
						{
							list.Add(typeof(HttpPostProtocolReflector));
						}
						protocolReflectorTypes = list.ToArray();
					}
				}
			}
			return protocolReflectorTypes;
		}
		set
		{
			protocolReflectorTypes = value;
		}
	}

	/// <summary>Gets the transmission protocol that is used to decrypt data sent from a client browser in an HTTP request.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Configuration.WebServiceProtocols" /> object that represents the transmission protocol that is used to decrypt data sent from a client browser in an HTTP request.</returns>
	[ConfigurationProperty("protocols")]
	public ProtocolElementCollection Protocols => (ProtocolElementCollection)base[protocols];

	/// <summary>Gets or sets the <see cref="T:System.Web.Services.Configuration.SoapEnvelopeProcessingElement" /> for the <see cref="T:System.Web.Services.Configuration.WebServicesSection" /> object.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Configuration.SoapEnvelopeProcessingElement" /> for the current configuration file.</returns>
	[ConfigurationProperty("soapEnvelopeProcessing")]
	public SoapEnvelopeProcessingElement SoapEnvelopeProcessing
	{
		get
		{
			return (SoapEnvelopeProcessingElement)base[soapEnvelopeProcessing];
		}
		set
		{
			base[soapEnvelopeProcessing] = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.Services.Configuration.DiagnosticsElement" /> for the <see cref="T:System.Web.Services.Configuration.WebServicesSection" /> object.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Configuration.DiagnosticsElement" /> for the current configuration file.</returns>
	public DiagnosticsElement Diagnostics
	{
		get
		{
			return (DiagnosticsElement)base[diagnostics];
		}
		set
		{
			base[diagnostics] = value;
		}
	}

	internal Type[] ReturnWriterTypes => returnWriterTypes;

	internal ServerProtocolFactory[] ServerProtocolFactories
	{
		get
		{
			if (serverProtocolFactories == null)
			{
				lock (ClassSyncObject)
				{
					if (serverProtocolFactories == null)
					{
						WebServiceProtocols num = EnabledProtocols;
						List<ServerProtocolFactory> list = new List<ServerProtocolFactory>();
						if ((num & WebServiceProtocols.AnyHttpSoap) != 0)
						{
							list.Add((ServerProtocolFactory)Activator.CreateInstance(SoapServerProtocolFactory));
						}
						if ((num & WebServiceProtocols.HttpPost) != 0)
						{
							list.Add(new HttpPostServerProtocolFactory());
						}
						if ((num & WebServiceProtocols.HttpPostLocalhost) != 0)
						{
							list.Add(new HttpPostLocalhostServerProtocolFactory());
						}
						if ((num & WebServiceProtocols.HttpGet) != 0)
						{
							list.Add(new HttpGetServerProtocolFactory());
						}
						if ((num & WebServiceProtocols.Documentation) != 0)
						{
							list.Add(new DiscoveryServerProtocolFactory());
							list.Add(new DocumentationServerProtocolFactory());
						}
						serverProtocolFactories = list.ToArray();
					}
				}
			}
			return serverProtocolFactories;
		}
	}

	internal bool ServiceDescriptionExtended => ServiceDescriptionFormatExtensionTypes.Count > 0;

	/// <summary>Gets the <see cref="T:System.Web.Services.Configuration.TypeElementCollection" /> that specifies the service description format extension to run within the scope of the configuration file.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Configuration.TypeElementCollection" /> that specifies the service description format extension to run within the scope of the configuration file.</returns>
	[ConfigurationProperty("serviceDescriptionFormatExtensionTypes")]
	public TypeElementCollection ServiceDescriptionFormatExtensionTypes => (TypeElementCollection)base[serviceDescriptionFormatExtensionTypes];

	/// <summary>Gets the <see cref="T:System.Web.Services.Configuration.TypeElementCollection" /> that specifies the SOAP extensions to run when a service description for an XML Web service within the scope of the configuration file is accessed.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Configuration.TypeElementCollection" /> that specifies the SOAP extensions to run when a service description for an XML Web service within the scope of the configuration file is accessed.</returns>
	[ConfigurationProperty("soapExtensionImporterTypes")]
	public TypeElementCollection SoapExtensionImporterTypes => (TypeElementCollection)base[soapExtensionImporterTypes];

	/// <summary>Gets the <see cref="T:System.Web.Services.Configuration.TypeElementCollection" /> that specifies the SOAP extensions to run when a service description is generated for all XML Web services within the scope of the configuration file.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Configuration.TypeElementCollection" /> that specifies the SOAP extensions to run when a service description is generated for all XML Web services within the scope of the configuration file.</returns>
	[ConfigurationProperty("soapExtensionReflectorTypes")]
	public TypeElementCollection SoapExtensionReflectorTypes => (TypeElementCollection)base[soapExtensionReflectorTypes];

	/// <summary>Gets the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElementCollection" /> that specifies the SOAP extensions to run with all XML Web services within the scope of the configuration file.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElementCollection" /> that specifies the SOAP extensions to run with all XML Web services within the scope of the configuration file.</returns>
	[ConfigurationProperty("soapExtensionTypes")]
	public SoapExtensionTypeElementCollection SoapExtensionTypes => (SoapExtensionTypeElementCollection)base[soapExtensionTypes];

	/// <summary>Gets a <see cref="T:System.Web.Services.Configuration.TypeElement" /> object that corresponds to the protocol used to call the Web service.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Configuration.TypeElement" /> object that corresponds to the protocol used to call the Web service.</returns>
	[ConfigurationProperty("soapServerProtocolFactory")]
	public TypeElement SoapServerProtocolFactoryType => (TypeElement)base[soapServerProtocolFactoryType];

	internal Type SoapServerProtocolFactory
	{
		get
		{
			if (soapServerProtocolFactory == null)
			{
				lock (ClassSyncObject)
				{
					if (soapServerProtocolFactory == null)
					{
						soapServerProtocolFactory = SoapServerProtocolFactoryType.Type;
					}
				}
			}
			return soapServerProtocolFactory;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.Services.Configuration.TypeElementCollection" /> object that represents the SoapTransportImporterTypes configuration element.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Configuration.TypeElementCollection" /> object that represents the <see langword="SoapTransportImporterTypes" /> configuration element.</returns>
	[ConfigurationProperty("soapTransportImporterTypes")]
	public TypeElementCollection SoapTransportImporterTypes => (TypeElementCollection)base[soapTransportImporterTypes];

	internal Type[] SoapTransportImporters
	{
		get
		{
			Type[] array = new Type[1 + SoapTransportImporterTypes.Count];
			array[0] = typeof(SoapHttpTransportImporter);
			for (int i = 0; i < SoapTransportImporterTypes.Count; i++)
			{
				array[i + 1] = SoapTransportImporterTypes[i].Type;
			}
			return array;
		}
	}

	/// <summary>Gets the Web service Help page (an .aspx file) that is displayed to a browser when the browser navigates directly to an ASMX page.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Configuration.WsdlHelpGeneratorElement" /> object that specifies the XML Web service Help page (an .aspx file) that is displayed to a browser when the browser navigates directly to an ASMX XML Web service page.</returns>
	[ConfigurationProperty("wsdlHelpGenerator")]
	public WsdlHelpGeneratorElement WsdlHelpGenerator => (WsdlHelpGeneratorElement)base[wsdlHelpGenerator];

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.WebServicesSection" /> class.</summary>
	public WebServicesSection()
	{
		properties.Add(conformanceWarnings);
		properties.Add(protocols);
		properties.Add(serviceDescriptionFormatExtensionTypes);
		properties.Add(soapEnvelopeProcessing);
		properties.Add(soapExtensionImporterTypes);
		properties.Add(soapExtensionReflectorTypes);
		properties.Add(soapExtensionTypes);
		properties.Add(soapTransportImporterTypes);
		properties.Add(wsdlHelpGenerator);
		properties.Add(soapServerProtocolFactoryType);
		properties.Add(diagnostics);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	private static WebServicesSection GetConfigFromHttpContext()
	{
		PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
		HttpContext current = HttpContext.Current;
		if (current != null)
		{
			return (WebServicesSection)current.GetSection("system.web/webServices");
		}
		return null;
	}

	internal Type[] GetAllFormatExtensionTypes()
	{
		if (ServiceDescriptionFormatExtensionTypes.Count == 0)
		{
			return defaultFormatTypes;
		}
		Type[] array = new Type[defaultFormatTypes.Length + ServiceDescriptionFormatExtensionTypes.Count];
		Array.Copy(defaultFormatTypes, array, defaultFormatTypes.Length);
		for (int i = 0; i < ServiceDescriptionFormatExtensionTypes.Count; i++)
		{
			array[i + defaultFormatTypes.Length] = ServiceDescriptionFormatExtensionTypes[i].Type;
		}
		return array;
	}

	private static XmlFormatExtensionPointAttribute GetExtensionPointAttribute(Type type)
	{
		object[] customAttributes = type.GetCustomAttributes(typeof(XmlFormatExtensionPointAttribute), inherit: false);
		if (customAttributes.Length == 0)
		{
			throw new ArgumentException(Res.GetString("TheSyntaxOfTypeMayNotBeExtended1", type.FullName), "type");
		}
		return (XmlFormatExtensionPointAttribute)customAttributes[0];
	}

	/// <summary>Retrieves the specified configuration section.</summary>
	/// <param name="config">A <see cref="T:System.Configuration.Configuration" /> object that represents the section to be retrieved.</param>
	/// <returns>A <see cref="T:System.Web.Services.Configuration.WebServicesSection" /> object that represents the section being retrieved.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="config" /> is <see langword="null" />.</exception>
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	public static WebServicesSection GetSection(System.Configuration.Configuration config)
	{
		if (config == null)
		{
			throw new ArgumentNullException("config");
		}
		return (WebServicesSection)config.GetSection("system.web/webServices");
	}

	protected override void InitializeDefault()
	{
		ConformanceWarnings.SetDefaults();
		Protocols.SetDefaults();
		if (Thread.GetDomain().GetData(".appDomain") != null)
		{
			WsdlHelpGenerator.SetDefaults();
		}
		SoapServerProtocolFactoryType.Type = typeof(SoapServerProtocolFactory);
	}

	internal static void LoadXmlFormatExtensions(Type[] extensionTypes, XmlAttributeOverrides overrides, XmlSerializerNamespaces namespaces)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add(typeof(ServiceDescription), new XmlAttributes());
		hashtable.Add(typeof(Import), new XmlAttributes());
		hashtable.Add(typeof(Port), new XmlAttributes());
		hashtable.Add(typeof(Service), new XmlAttributes());
		hashtable.Add(typeof(FaultBinding), new XmlAttributes());
		hashtable.Add(typeof(InputBinding), new XmlAttributes());
		hashtable.Add(typeof(OutputBinding), new XmlAttributes());
		hashtable.Add(typeof(OperationBinding), new XmlAttributes());
		hashtable.Add(typeof(Binding), new XmlAttributes());
		hashtable.Add(typeof(OperationFault), new XmlAttributes());
		hashtable.Add(typeof(OperationInput), new XmlAttributes());
		hashtable.Add(typeof(OperationOutput), new XmlAttributes());
		hashtable.Add(typeof(Operation), new XmlAttributes());
		hashtable.Add(typeof(PortType), new XmlAttributes());
		hashtable.Add(typeof(Message), new XmlAttributes());
		hashtable.Add(typeof(MessagePart), new XmlAttributes());
		hashtable.Add(typeof(Types), new XmlAttributes());
		Hashtable hashtable2 = new Hashtable();
		foreach (Type type in extensionTypes)
		{
			if (hashtable2[type] != null)
			{
				continue;
			}
			hashtable2.Add(type, type);
			object[] customAttributes = type.GetCustomAttributes(typeof(XmlFormatExtensionAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				throw new ArgumentException(Res.GetString("RequiredXmlFormatExtensionAttributeIsMissing1", type.FullName), "extensionTypes");
			}
			XmlFormatExtensionAttribute xmlFormatExtensionAttribute = (XmlFormatExtensionAttribute)customAttributes[0];
			Type[] extensionPoints = xmlFormatExtensionAttribute.ExtensionPoints;
			foreach (Type key in extensionPoints)
			{
				XmlAttributes xmlAttributes = (XmlAttributes)hashtable[key];
				if (xmlAttributes == null)
				{
					xmlAttributes = new XmlAttributes();
					hashtable.Add(key, xmlAttributes);
				}
				XmlElementAttribute xmlElementAttribute = new XmlElementAttribute(xmlFormatExtensionAttribute.ElementName, type);
				xmlElementAttribute.Namespace = xmlFormatExtensionAttribute.Namespace;
				xmlAttributes.XmlElements.Add(xmlElementAttribute);
			}
			customAttributes = type.GetCustomAttributes(typeof(XmlFormatExtensionPrefixAttribute), inherit: false);
			string[] array = new string[customAttributes.Length];
			Hashtable hashtable3 = new Hashtable();
			for (int k = 0; k < customAttributes.Length; k++)
			{
				XmlFormatExtensionPrefixAttribute xmlFormatExtensionPrefixAttribute = (XmlFormatExtensionPrefixAttribute)customAttributes[k];
				array[k] = xmlFormatExtensionPrefixAttribute.Prefix;
				hashtable3.Add(xmlFormatExtensionPrefixAttribute.Prefix, xmlFormatExtensionPrefixAttribute.Namespace);
			}
			Array.Sort(array, System.InvariantComparer.Default);
			for (int l = 0; l < array.Length; l++)
			{
				namespaces.Add(array[l], (string)hashtable3[array[l]]);
			}
		}
		foreach (Type key2 in hashtable.Keys)
		{
			XmlFormatExtensionPointAttribute extensionPointAttribute = GetExtensionPointAttribute(key2);
			XmlAttributes xmlAttributes2 = (XmlAttributes)hashtable[key2];
			if (extensionPointAttribute.AllowElements)
			{
				xmlAttributes2.XmlAnyElements.Add(new XmlAnyElementAttribute());
			}
			overrides.Add(key2, extensionPointAttribute.MemberName, xmlAttributes2);
		}
	}

	protected override void Reset(ConfigurationElement parentElement)
	{
		serverProtocolFactories = null;
		enabledProtocols = WebServiceProtocols.Unknown;
		if (parentElement != null)
		{
			WebServicesSection webServicesSection = (WebServicesSection)parentElement;
			discoveryDocumentSerializer = webServicesSection.discoveryDocumentSerializer;
		}
		base.Reset(parentElement);
	}

	private void TurnOnGetAndPost()
	{
		bool flag = (EnabledProtocols & WebServiceProtocols.HttpPost) == 0;
		bool flag2 = (EnabledProtocols & WebServiceProtocols.HttpGet) == 0;
		if (flag2 || flag)
		{
			ArrayList arrayList = new ArrayList(ProtocolImporterTypes);
			ArrayList arrayList2 = new ArrayList(ProtocolReflectorTypes);
			if (flag)
			{
				arrayList.Add(typeof(HttpPostProtocolImporter));
				arrayList2.Add(typeof(HttpPostProtocolReflector));
			}
			if (flag2)
			{
				arrayList.Add(typeof(HttpGetProtocolImporter));
				arrayList2.Add(typeof(HttpGetProtocolReflector));
			}
			ProtocolImporterTypes = (Type[])arrayList.ToArray(typeof(Type));
			ProtocolReflectorTypes = (Type[])arrayList2.ToArray(typeof(Type));
			enabledProtocols |= WebServiceProtocols.HttpGet | WebServiceProtocols.HttpPost;
		}
	}
}
