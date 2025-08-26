using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Design;
using System.Diagnostics;
using System.Security.Permissions;
using System.Threading;
using System.Web.Services.Configuration;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Generates classes for Web services that use the SOAP protocol.</summary>
[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
public class SoapProtocolImporter : ProtocolImporter
{
	private XmlSchemaImporter xmlImporter;

	private XmlCodeExporter xmlExporter;

	private SoapSchemaImporter soapImporter;

	private SoapCodeExporter soapExporter;

	private ArrayList xmlMembers = new ArrayList();

	private ArrayList soapMembers = new ArrayList();

	private Hashtable headers = new Hashtable();

	private Hashtable classHeaders = new Hashtable();

	private ArrayList propertyNames = new ArrayList();

	private ArrayList propertyValues = new ArrayList();

	private SoapExtensionImporter[] extensions;

	private SoapTransportImporter transport;

	private SoapBinding soapBinding;

	private ArrayList codeClasses = new ArrayList();

	private static TypedDataSetSchemaImporterExtension typedDataSetSchemaImporterExtension;

	/// <summary>Gets a value of "Soap".</summary>
	/// <returns>A <see cref="T:System.String" /> object that contains the string "Soap".</returns>
	public override string ProtocolName => "Soap";

	/// <summary>Gets a <see cref="T:System.Web.Services.Description.SoapBinding" /> instance obtained through the <see cref="P:System.Web.Services.Description.SoapProtocolImporter.SoapBinding" /> property's <see cref="P:System.Web.Services.Description.Binding.Extensions" /> property.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.SoapBinding" /> instance obtained through the <see cref="P:System.Web.Services.Description.SoapProtocolImporter.SoapBinding" /> property's <see cref="P:System.Web.Services.Description.Binding.Extensions" /> property.</returns>
	public SoapBinding SoapBinding => soapBinding;

	/// <summary>Gets the object of type <see cref="T:System.Xml.Serialization.SoapSchemaImporter" /> used internally by the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> class to generate mappings between SOAP-encoded XML schema content and .NET Framework types.</summary>
	/// <returns>An object of type <see cref="T:System.Xml.Serialization.SoapSchemaImporter" /> used internally by the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> class to generate mappings between SOAP-encoded XML schema content and .NET Framework types.</returns>
	public SoapSchemaImporter SoapImporter => soapImporter;

	/// <summary>Gets the object of type <see cref="T:System.Xml.Serialization.XmlSchemaImporter" /> used internally by the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> class to generate mappings between literal XML schema content and .NET Framework types.</summary>
	/// <returns>An object of type <see cref="T:System.Xml.Serialization.XmlSchemaImporter" /> used internally by the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> class to generate mappings between literal XML schema content and .NET Framework types.</returns>
	public XmlSchemaImporter XmlImporter => xmlImporter;

	/// <summary>Gets the object of type <see cref="T:System.Xml.Serialization.XmlCodeExporter" /> used internally by the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> class to generate code from mappings between literal XML schema content and .NET Framework types.</summary>
	/// <returns>An object of type <see cref="T:System.Xml.Serialization.XmlCodeExporter" /> used internally by the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> class to generate code from mappings between literal XML schema content and .NET Framework types.</returns>
	public XmlCodeExporter XmlExporter => xmlExporter;

	/// <summary>Gets the object of type <see cref="T:System.Xml.Serialization.SoapCodeExporter" /> used internally by the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> class to generate code from mappings between SOAP-encoded XML schema content and .NET Framework types.</summary>
	/// <returns>An object of type <see cref="T:System.Xml.Serialization.SoapCodeExporter" /> used internally by the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> class to generate code from mappings between SOAP-encoded XML schema content and .NET Framework types.</returns>
	public SoapCodeExporter SoapExporter => soapExporter;

	private static TypedDataSetSchemaImporterExtension TypedDataSetSchemaImporterExtension
	{
		get
		{
			if (typedDataSetSchemaImporterExtension == null)
			{
				typedDataSetSchemaImporterExtension = new TypedDataSetSchemaImporterExtension();
			}
			return typedDataSetSchemaImporterExtension;
		}
	}

	private bool MetadataPropertiesAdded => propertyNames.Count > 0;

	/// <summary>Performs initialization for an entire code namespace during code generation.</summary>
	protected override void BeginNamespace()
	{
		try
		{
			base.MethodNames.Clear();
			base.ExtraCodeClasses.Clear();
			soapImporter = new SoapSchemaImporter(base.AbstractSchemas, base.ServiceImporter.CodeGenerationOptions, base.ImportContext);
			xmlImporter = new XmlSchemaImporter(base.ConcreteSchemas, base.ServiceImporter.CodeGenerationOptions, base.ServiceImporter.CodeGenerator, base.ImportContext);
			foreach (Type extension in base.ServiceImporter.Extensions)
			{
				xmlImporter.Extensions.Add(extension.FullName, extension);
			}
			xmlImporter.Extensions.Add(TypedDataSetSchemaImporterExtension);
			xmlImporter.Extensions.Add(new DataSetSchemaImporterExtension());
			xmlExporter = new XmlCodeExporter(base.CodeNamespace, base.ServiceImporter.CodeCompileUnit, base.ServiceImporter.CodeGenerator, base.ServiceImporter.CodeGenerationOptions, base.ExportContext);
			soapExporter = new SoapCodeExporter(base.CodeNamespace, null, base.ServiceImporter.CodeGenerator, base.ServiceImporter.CodeGenerationOptions, base.ExportContext);
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			throw new InvalidOperationException(Res.GetString("InitFailed"), ex);
		}
	}

	/// <summary>Performs processing for an entire code namespace after binding class generation.</summary>
	protected override void EndNamespace()
	{
		base.ConcreteSchemas.Compile(null, fullCompile: false);
		foreach (GlobalSoapHeader value4 in headers.Values)
		{
			if (value4.isEncoded)
			{
				soapExporter.ExportTypeMapping(value4.mapping);
			}
			else
			{
				xmlExporter.ExportTypeMapping(value4.mapping);
			}
		}
		foreach (XmlMembersMapping xmlMember in xmlMembers)
		{
			xmlExporter.ExportMembersMapping(xmlMember);
		}
		foreach (XmlMembersMapping soapMember in soapMembers)
		{
			soapExporter.ExportMembersMapping(soapMember);
		}
		foreach (CodeTypeDeclaration codeClass in codeClasses)
		{
			foreach (CodeAttributeDeclaration includeMetadatum in xmlExporter.IncludeMetadata)
			{
				codeClass.CustomAttributes.Add(includeMetadatum);
			}
			foreach (CodeAttributeDeclaration includeMetadatum2 in soapExporter.IncludeMetadata)
			{
				codeClass.CustomAttributes.Add(includeMetadatum2);
			}
		}
		foreach (CodeTypeDeclaration extraCodeClass in base.ExtraCodeClasses)
		{
			base.CodeNamespace.Types.Add(extraCodeClass);
		}
		CodeGenerator.ValidateIdentifiers(base.CodeNamespace);
	}

	/// <summary>Determines whether a class can be generated for the current binding.</summary>
	/// <returns>
	///     <see langword="true" /> if the class can be generated for the current binding; otherwise <see langword="false" />.</returns>
	protected override bool IsBindingSupported()
	{
		SoapBinding soapBinding = (SoapBinding)base.Binding.Extensions.Find(typeof(SoapBinding));
		if (soapBinding == null || soapBinding.GetType() != typeof(SoapBinding))
		{
			return false;
		}
		if (GetTransport(soapBinding.Transport) == null)
		{
			UnsupportedBindingWarning(Res.GetString("ThereIsNoSoapTransportImporterThatUnderstands1", soapBinding.Transport));
			return false;
		}
		return true;
	}

	internal SoapTransportImporter GetTransport(string transport)
	{
		Type[] soapTransportImporters = WebServicesSection.Current.SoapTransportImporters;
		for (int i = 0; i < soapTransportImporters.Length; i++)
		{
			SoapTransportImporter soapTransportImporter = (SoapTransportImporter)Activator.CreateInstance(soapTransportImporters[i]);
			soapTransportImporter.ImportContext = this;
			if (soapTransportImporter.IsSupportedTransport(transport))
			{
				return soapTransportImporter;
			}
		}
		return null;
	}

	/// <summary>Initializes the generation of a binding class.</summary>
	/// <returns>A binding class.</returns>
	protected override CodeTypeDeclaration BeginClass()
	{
		base.MethodNames.Clear();
		soapBinding = (SoapBinding)base.Binding.Extensions.Find(typeof(SoapBinding));
		transport = GetTransport(soapBinding.Transport);
		Type[] types = new Type[6]
		{
			typeof(SoapDocumentMethodAttribute),
			typeof(XmlAttributeAttribute),
			typeof(WebService),
			typeof(object),
			typeof(DebuggerStepThroughAttribute),
			typeof(DesignerCategoryAttribute)
		};
		WebCodeGenerator.AddImports(base.CodeNamespace, WebCodeGenerator.GetNamespacesForTypes(types));
		CodeFlags codeFlags = (CodeFlags)0;
		if (base.Style == ServiceDescriptionImportStyle.Server)
		{
			codeFlags = CodeFlags.IsAbstract;
		}
		else if (base.Style == ServiceDescriptionImportStyle.ServerInterface)
		{
			codeFlags = CodeFlags.IsInterface;
		}
		CodeTypeDeclaration codeTypeDeclaration = WebCodeGenerator.CreateClass(base.ClassName, null, new string[0], null, CodeFlags.IsPublic | codeFlags, base.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.PartialTypes));
		codeTypeDeclaration.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), docComment: true));
		if (base.Style == ServiceDescriptionImportStyle.Client)
		{
			codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(typeof(DebuggerStepThroughAttribute).FullName));
			codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(typeof(DesignerCategoryAttribute).FullName, new CodeAttributeArgument(new CodePrimitiveExpression("code"))));
		}
		else if (base.Style == ServiceDescriptionImportStyle.Server)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(WebServiceAttribute).FullName);
			string value = ((base.Service != null) ? base.Service.ServiceDescription.TargetNamespace : base.Binding.ServiceDescription.TargetNamespace);
			codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(value)));
			codeTypeDeclaration.CustomAttributes.Add(codeAttributeDeclaration);
		}
		CodeAttributeDeclaration codeAttributeDeclaration2 = new CodeAttributeDeclaration(typeof(WebServiceBindingAttribute).FullName);
		codeAttributeDeclaration2.Arguments.Add(new CodeAttributeArgument("Name", new CodePrimitiveExpression(XmlConvert.DecodeName(base.Binding.Name))));
		codeAttributeDeclaration2.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(base.Binding.ServiceDescription.TargetNamespace)));
		codeTypeDeclaration.CustomAttributes.Add(codeAttributeDeclaration2);
		codeClasses.Add(codeTypeDeclaration);
		classHeaders.Clear();
		return codeTypeDeclaration;
	}

	/// <summary>Processes a binding class after the generation of methods.</summary>
	protected override void EndClass()
	{
		if (transport != null)
		{
			transport.ImportClass();
		}
		soapBinding = null;
	}

	/// <summary>Determines whether the current operation's operation flow is supported.</summary>
	/// <param name="flow">An <see cref="T:System.Web.Services.Description.OperationFlow" /> enumeration value that represents a transmission pattern.</param>
	/// <returns>
	///     <see langword="true" /> if the current operation's operation flow is supported; otherwise <see langword="false" />.</returns>
	protected override bool IsOperationFlowSupported(OperationFlow flow)
	{
		if (flow != OperationFlow.OneWay)
		{
			return flow == OperationFlow.RequestResponse;
		}
		return true;
	}

	private void BeginMetadata()
	{
		propertyNames.Clear();
		propertyValues.Clear();
	}

	private void AddMetadataProperty(string name, object value)
	{
		AddMetadataProperty(name, new CodePrimitiveExpression(value));
	}

	private void AddMetadataProperty(string name, CodeExpression expr)
	{
		propertyNames.Add(name);
		propertyValues.Add(expr);
	}

	private void EndMetadata(CodeAttributeDeclarationCollection metadata, Type attributeType, string parameter)
	{
		CodeExpression[] parameters = ((parameter != null) ? new CodeExpression[1]
		{
			new CodePrimitiveExpression(parameter)
		} : new CodeExpression[0]);
		WebCodeGenerator.AddCustomAttribute(metadata, attributeType, parameters, (string[])propertyNames.ToArray(typeof(string)), (CodeExpression[])propertyValues.ToArray(typeof(CodeExpression)));
	}

	private void GenerateExtensionMetadata(CodeAttributeDeclarationCollection metadata)
	{
		if (extensions == null)
		{
			TypeElementCollection soapExtensionImporterTypes = WebServicesSection.Current.SoapExtensionImporterTypes;
			extensions = new SoapExtensionImporter[soapExtensionImporterTypes.Count];
			for (int i = 0; i < extensions.Length; i++)
			{
				SoapExtensionImporter soapExtensionImporter = (SoapExtensionImporter)Activator.CreateInstance(soapExtensionImporterTypes[i].Type);
				soapExtensionImporter.ImportContext = this;
				extensions[i] = soapExtensionImporter;
			}
		}
		SoapExtensionImporter[] array = extensions;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].ImportMethod(metadata);
		}
	}

	private void PrepareHeaders(MessageBinding messageBinding)
	{
		SoapHeaderBinding[] array = (SoapHeaderBinding[])messageBinding.Extensions.FindAll(typeof(SoapHeaderBinding));
		for (int i = 0; i < array.Length; i++)
		{
			array[i].MapToProperty = true;
		}
	}

	private void GenerateHeaders(CodeAttributeDeclarationCollection metadata, SoapBindingUse use, bool rpc, MessageBinding requestMessage, MessageBinding responseMessage)
	{
		Hashtable hashtable = new Hashtable();
		for (int i = 0; i < 2; i++)
		{
			MessageBinding messageBinding;
			SoapHeaderDirection soapHeaderDirection;
			if (i == 0)
			{
				messageBinding = requestMessage;
				soapHeaderDirection = SoapHeaderDirection.In;
			}
			else
			{
				if (responseMessage == null)
				{
					continue;
				}
				messageBinding = responseMessage;
				soapHeaderDirection = SoapHeaderDirection.Out;
			}
			SoapHeaderBinding[] array = (SoapHeaderBinding[])messageBinding.Extensions.FindAll(typeof(SoapHeaderBinding));
			foreach (SoapHeaderBinding soapHeaderBinding in array)
			{
				if (!soapHeaderBinding.MapToProperty)
				{
					continue;
				}
				if (use != soapHeaderBinding.Use)
				{
					throw new InvalidOperationException(Res.GetString("WebDescriptionHeaderAndBodyUseMismatch"));
				}
				if (use == SoapBindingUse.Encoded && !IsSoapEncodingPresent(soapHeaderBinding.Encoding))
				{
					throw new InvalidOperationException(Res.GetString("WebUnknownEncodingStyle", soapHeaderBinding.Encoding));
				}
				Message message = base.ServiceDescriptions.GetMessage(soapHeaderBinding.Message);
				if (message == null)
				{
					throw new InvalidOperationException(Res.GetString("MissingMessage2", soapHeaderBinding.Message.Name, soapHeaderBinding.Message.Namespace));
				}
				MessagePart messagePart = message.FindPartByName(soapHeaderBinding.Part);
				if (messagePart == null)
				{
					throw new InvalidOperationException(Res.GetString("MissingMessagePartForMessageFromNamespace3", messagePart.Name, soapHeaderBinding.Message.Name, soapHeaderBinding.Message.Namespace));
				}
				XmlTypeMapping xmlTypeMapping;
				string key;
				if (use == SoapBindingUse.Encoded)
				{
					if (messagePart.Type.IsEmpty)
					{
						throw new InvalidOperationException(Res.GetString("WebDescriptionPartTypeRequired", messagePart.Name, soapHeaderBinding.Message.Name, soapHeaderBinding.Message.Namespace));
					}
					if (!messagePart.Element.IsEmpty)
					{
						UnsupportedOperationBindingWarning(Res.GetString("WebDescriptionPartElementWarning", messagePart.Name, soapHeaderBinding.Message.Name, soapHeaderBinding.Message.Namespace));
					}
					xmlTypeMapping = soapImporter.ImportDerivedTypeMapping(messagePart.Type, typeof(SoapHeader), baseTypeCanBeIndirect: true);
					key = "type=" + messagePart.Type.ToString();
				}
				else
				{
					if (messagePart.Element.IsEmpty)
					{
						throw new InvalidOperationException(Res.GetString("WebDescriptionPartElementRequired", messagePart.Name, soapHeaderBinding.Message.Name, soapHeaderBinding.Message.Namespace));
					}
					if (!messagePart.Type.IsEmpty)
					{
						UnsupportedOperationBindingWarning(Res.GetString("WebDescriptionPartTypeWarning", messagePart.Name, soapHeaderBinding.Message.Name, soapHeaderBinding.Message.Namespace));
					}
					xmlTypeMapping = xmlImporter.ImportDerivedTypeMapping(messagePart.Element, typeof(SoapHeader), baseTypeCanBeIndirect: true);
					key = "element=" + messagePart.Element.ToString();
				}
				LocalSoapHeader localSoapHeader = (LocalSoapHeader)hashtable[key];
				if (localSoapHeader == null)
				{
					GlobalSoapHeader globalSoapHeader = (GlobalSoapHeader)classHeaders[key];
					if (globalSoapHeader == null)
					{
						globalSoapHeader = new GlobalSoapHeader();
						globalSoapHeader.isEncoded = use == SoapBindingUse.Encoded;
						string text = CodeIdentifier.MakeValid(xmlTypeMapping.ElementName);
						if (text == xmlTypeMapping.TypeName)
						{
							text += "Value";
						}
						text = base.MethodNames.AddUnique(text, xmlTypeMapping);
						globalSoapHeader.fieldName = text;
						WebCodeGenerator.AddMember(base.CodeTypeDeclaration, xmlTypeMapping.TypeFullName, globalSoapHeader.fieldName, null, null, CodeFlags.IsPublic, base.ServiceImporter.CodeGenerationOptions);
						globalSoapHeader.mapping = xmlTypeMapping;
						classHeaders.Add(key, globalSoapHeader);
						if (headers[key] == null)
						{
							headers.Add(key, globalSoapHeader);
						}
					}
					localSoapHeader = new LocalSoapHeader();
					localSoapHeader.fieldName = globalSoapHeader.fieldName;
					localSoapHeader.direction = soapHeaderDirection;
					hashtable.Add(key, localSoapHeader);
				}
				else if (localSoapHeader.direction != soapHeaderDirection)
				{
					localSoapHeader.direction = SoapHeaderDirection.InOut;
				}
			}
		}
		foreach (LocalSoapHeader value in hashtable.Values)
		{
			BeginMetadata();
			if (value.direction == SoapHeaderDirection.Out)
			{
				AddMetadataProperty("Direction", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SoapHeaderDirection).FullName), SoapHeaderDirection.Out.ToString()));
			}
			else if (value.direction == SoapHeaderDirection.InOut)
			{
				AddMetadataProperty("Direction", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SoapHeaderDirection).FullName), SoapHeaderDirection.InOut.ToString()));
			}
			EndMetadata(metadata, typeof(SoapHeaderAttribute), value.fieldName);
		}
	}

	/// <summary>Generates method code for a binding class.</summary>
	/// <returns>The method code for a binding class.</returns>
	protected override CodeMemberMethod GenerateMethod()
	{
		SoapOperationBinding soapOperationBinding = (SoapOperationBinding)base.OperationBinding.Extensions.Find(typeof(SoapOperationBinding));
		if (soapOperationBinding == null)
		{
			throw OperationBindingSyntaxException(Res.GetString("MissingSoapOperationBinding0"));
		}
		SoapBindingStyle soapBindingStyle = soapOperationBinding.Style;
		if (soapBindingStyle == SoapBindingStyle.Default)
		{
			soapBindingStyle = SoapBinding.Style;
		}
		if (soapBindingStyle == SoapBindingStyle.Default)
		{
			soapBindingStyle = SoapBindingStyle.Document;
		}
		string[] parameterOrder = base.Operation.ParameterOrder;
		Message message = base.InputMessage;
		MessageBinding input = base.OperationBinding.Input;
		SoapBodyBinding soapBodyBinding = (SoapBodyBinding)base.OperationBinding.Input.Extensions.Find(typeof(SoapBodyBinding));
		if (soapBodyBinding == null)
		{
			UnsupportedOperationBindingWarning(Res.GetString("MissingSoapBodyInputBinding0"));
			return null;
		}
		Message message2;
		MessageBinding messageBinding;
		SoapBodyBinding soapBodyBinding2;
		if (base.Operation.Messages.Output != null)
		{
			message2 = base.OutputMessage;
			messageBinding = base.OperationBinding.Output;
			soapBodyBinding2 = (SoapBodyBinding)base.OperationBinding.Output.Extensions.Find(typeof(SoapBodyBinding));
			if (soapBodyBinding2 == null)
			{
				UnsupportedOperationBindingWarning(Res.GetString("MissingSoapBodyOutputBinding0"));
				return null;
			}
		}
		else
		{
			message2 = null;
			messageBinding = null;
			soapBodyBinding2 = null;
		}
		CodeAttributeDeclarationCollection metadata = new CodeAttributeDeclarationCollection();
		PrepareHeaders(input);
		if (messageBinding != null)
		{
			PrepareHeaders(messageBinding);
		}
		string messageName = null;
		string name = ((!string.IsNullOrEmpty(input.Name) && soapBindingStyle != SoapBindingStyle.Rpc) ? input.Name : base.Operation.Name);
		name = XmlConvert.DecodeName(name);
		if (messageBinding != null)
		{
			messageName = ((!string.IsNullOrEmpty(messageBinding.Name) && soapBindingStyle != SoapBindingStyle.Rpc) ? messageBinding.Name : (base.Operation.Name + "Response"));
			messageName = XmlConvert.DecodeName(messageName);
		}
		GenerateExtensionMetadata(metadata);
		GenerateHeaders(metadata, soapBodyBinding.Use, soapBindingStyle == SoapBindingStyle.Rpc, input, messageBinding);
		MessagePart[] messageParts = GetMessageParts(message, soapBodyBinding);
		if (!CheckMessageStyles(base.MethodName, messageParts, soapBodyBinding, soapBindingStyle, out var hasWrapper))
		{
			return null;
		}
		MessagePart[] parts = null;
		if (message2 != null)
		{
			parts = GetMessageParts(message2, soapBodyBinding2);
			if (!CheckMessageStyles(base.MethodName, parts, soapBodyBinding2, soapBindingStyle, out var hasWrapper2))
			{
				return null;
			}
			if (hasWrapper != hasWrapper2)
			{
				hasWrapper = false;
			}
		}
		bool flag = (soapBindingStyle != SoapBindingStyle.Rpc && hasWrapper) || (soapBodyBinding.Use == SoapBindingUse.Literal && soapBindingStyle == SoapBindingStyle.Rpc);
		XmlMembersMapping xmlMembersMapping = ImportMessage(name, messageParts, soapBodyBinding, soapBindingStyle, hasWrapper);
		if (xmlMembersMapping == null)
		{
			return null;
		}
		XmlMembersMapping xmlMembersMapping2 = null;
		if (message2 != null)
		{
			xmlMembersMapping2 = ImportMessage(messageName, parts, soapBodyBinding2, soapBindingStyle, hasWrapper);
			if (xmlMembersMapping2 == null)
			{
				return null;
			}
		}
		string text = CodeIdentifier.MakeValid(XmlConvert.DecodeName(base.Operation.Name));
		if (base.ClassName == text)
		{
			text = "Call" + text;
		}
		string text2 = base.MethodNames.AddUnique(CodeIdentifier.MakeValid(XmlConvert.DecodeName(text)), base.Operation);
		bool flag2 = text != text2;
		CodeIdentifiers codeIdentifiers = new CodeIdentifiers(caseSensitive: false);
		codeIdentifiers.AddReserved(text2);
		SoapParameters soapParameters = new SoapParameters(xmlMembersMapping, xmlMembersMapping2, parameterOrder, base.MethodNames);
		foreach (SoapParameter parameter in soapParameters.Parameters)
		{
			if ((parameter.IsOut || parameter.IsByRef) && !base.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.ReferenceParameters))
			{
				UnsupportedOperationWarning(Res.GetString("CodeGenSupportReferenceParameters", base.ServiceImporter.CodeGenerator.GetType().Name));
				return null;
			}
			parameter.name = codeIdentifiers.AddUnique(parameter.name, null);
			if (parameter.mapping.CheckSpecified)
			{
				parameter.specifiedName = codeIdentifiers.AddUnique(parameter.name + "Specified", null);
			}
		}
		if (base.Style != ServiceDescriptionImportStyle.Client || flag2)
		{
			BeginMetadata();
			if (flag2)
			{
				AddMetadataProperty("MessageName", text2);
			}
			EndMetadata(metadata, typeof(WebMethodAttribute), null);
		}
		BeginMetadata();
		if (flag && xmlMembersMapping.ElementName.Length > 0 && xmlMembersMapping.ElementName != text2)
		{
			AddMetadataProperty("RequestElementName", xmlMembersMapping.ElementName);
		}
		if (xmlMembersMapping.Namespace != null)
		{
			AddMetadataProperty("RequestNamespace", xmlMembersMapping.Namespace);
		}
		if (xmlMembersMapping2 == null)
		{
			AddMetadataProperty("OneWay", true);
		}
		else
		{
			if (flag && xmlMembersMapping2.ElementName.Length > 0 && xmlMembersMapping2.ElementName != text2 + "Response")
			{
				AddMetadataProperty("ResponseElementName", xmlMembersMapping2.ElementName);
			}
			if (xmlMembersMapping2.Namespace != null)
			{
				AddMetadataProperty("ResponseNamespace", xmlMembersMapping2.Namespace);
			}
		}
		if (soapBindingStyle == SoapBindingStyle.Rpc)
		{
			if (soapBodyBinding.Use != SoapBindingUse.Encoded)
			{
				AddMetadataProperty("Use", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SoapBindingUse).FullName), Enum.Format(typeof(SoapBindingUse), soapBodyBinding.Use, "G")));
			}
			EndMetadata(metadata, typeof(SoapRpcMethodAttribute), soapOperationBinding.SoapAction);
		}
		else
		{
			AddMetadataProperty("Use", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SoapBindingUse).FullName), Enum.Format(typeof(SoapBindingUse), soapBodyBinding.Use, "G")));
			AddMetadataProperty("ParameterStyle", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SoapParameterStyle).FullName), Enum.Format(typeof(SoapParameterStyle), (!hasWrapper) ? SoapParameterStyle.Bare : SoapParameterStyle.Wrapped, "G")));
			EndMetadata(metadata, typeof(SoapDocumentMethodAttribute), soapOperationBinding.SoapAction);
		}
		base.IsEncodedBinding = base.IsEncodedBinding || soapBodyBinding.Use == SoapBindingUse.Encoded;
		CodeAttributeDeclarationCollection[] array = new CodeAttributeDeclarationCollection[soapParameters.Parameters.Count + soapParameters.CheckSpecifiedCount];
		int num = 0;
		CodeAttributeDeclaration value = new CodeAttributeDeclaration(typeof(XmlIgnoreAttribute).FullName);
		foreach (SoapParameter parameter2 in soapParameters.Parameters)
		{
			array[num] = new CodeAttributeDeclarationCollection();
			if (soapBodyBinding.Use == SoapBindingUse.Encoded)
			{
				soapExporter.AddMappingMetadata(array[num], parameter2.mapping, parameter2.name != parameter2.mapping.MemberName);
			}
			else
			{
				string ns = ((soapBindingStyle == SoapBindingStyle.Rpc) ? parameter2.mapping.Namespace : (parameter2.IsOut ? xmlMembersMapping2.Namespace : xmlMembersMapping.Namespace));
				bool forceUseMemberName = parameter2.name != parameter2.mapping.MemberName;
				xmlExporter.AddMappingMetadata(array[num], parameter2.mapping, ns, forceUseMemberName);
				if (parameter2.mapping.CheckSpecified)
				{
					num++;
					array[num] = new CodeAttributeDeclarationCollection();
					xmlExporter.AddMappingMetadata(array[num], parameter2.mapping, ns, parameter2.specifiedName != parameter2.mapping.MemberName + "Specified");
					array[num].Add(value);
				}
			}
			if (array[num].Count > 0 && !base.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.ParameterAttributes))
			{
				UnsupportedOperationWarning(Res.GetString("CodeGenSupportParameterAttributes", base.ServiceImporter.CodeGenerator.GetType().Name));
				return null;
			}
			num++;
		}
		CodeFlags[] codeFlags = SoapParameter.GetCodeFlags(soapParameters.Parameters, soapParameters.CheckSpecifiedCount);
		string[] typeFullNames = SoapParameter.GetTypeFullNames(soapParameters.Parameters, soapParameters.CheckSpecifiedCount, base.ServiceImporter.CodeGenerator);
		string text3 = ((soapParameters.Return == null) ? typeof(void).FullName : WebCodeGenerator.FullTypeName(soapParameters.Return, base.ServiceImporter.CodeGenerator));
		CodeMemberMethod codeMemberMethod = WebCodeGenerator.AddMethod(base.CodeTypeDeclaration, text, codeFlags, typeFullNames, SoapParameter.GetNames(soapParameters.Parameters, soapParameters.CheckSpecifiedCount), array, text3, metadata, (CodeFlags)(1 | ((base.Style != 0) ? 2 : 0)));
		codeMemberMethod.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), docComment: true));
		if (soapParameters.Return != null)
		{
			if (soapBodyBinding.Use == SoapBindingUse.Encoded)
			{
				soapExporter.AddMappingMetadata(codeMemberMethod.ReturnTypeCustomAttributes, soapParameters.Return, soapParameters.Return.ElementName != text2 + "Result");
			}
			else
			{
				xmlExporter.AddMappingMetadata(codeMemberMethod.ReturnTypeCustomAttributes, soapParameters.Return, xmlMembersMapping2.Namespace, soapParameters.Return.ElementName != text2 + "Result");
			}
			if (codeMemberMethod.ReturnTypeCustomAttributes.Count != 0 && !base.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.ReturnTypeAttributes))
			{
				UnsupportedOperationWarning(Res.GetString("CodeGenSupportReturnTypeAttributes", base.ServiceImporter.CodeGenerator.GetType().Name));
				return null;
			}
		}
		string resultsName = codeIdentifiers.MakeUnique("results");
		if (base.Style == ServiceDescriptionImportStyle.Client)
		{
			bool num2 = (base.ServiceImporter.CodeGenerationOptions & CodeGenerationOptions.GenerateOldAsync) != 0;
			bool flag3 = (base.ServiceImporter.CodeGenerationOptions & CodeGenerationOptions.GenerateNewAsync) != 0 && base.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.DeclareEvents) && base.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.DeclareDelegates);
			CodeExpression[] array2 = new CodeExpression[2];
			CreateInvokeParams(array2, text2, soapParameters.InParameters, soapParameters.InCheckSpecifiedCount);
			CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "Invoke", array2);
			WriteReturnMappings(codeMemberMethod, invoke, soapParameters, resultsName);
			if (num2)
			{
				int num3 = soapParameters.InParameters.Count + soapParameters.InCheckSpecifiedCount;
				string[] array3 = new string[num3 + 2];
				SoapParameter.GetTypeFullNames(soapParameters.InParameters, array3, 0, soapParameters.InCheckSpecifiedCount, base.ServiceImporter.CodeGenerator);
				array3[num3] = typeof(AsyncCallback).FullName;
				array3[num3 + 1] = typeof(object).FullName;
				string[] array4 = new string[num3 + 2];
				SoapParameter.GetNames(soapParameters.InParameters, array4, 0, soapParameters.InCheckSpecifiedCount);
				array4[num3] = "callback";
				array4[num3 + 1] = "asyncState";
				CodeFlags[] parameterFlags = new CodeFlags[num3 + 2];
				CodeMemberMethod codeMemberMethod2 = WebCodeGenerator.AddMethod(base.CodeTypeDeclaration, "Begin" + text2, parameterFlags, array3, array4, typeof(IAsyncResult).FullName, null, CodeFlags.IsPublic);
				codeMemberMethod2.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), docComment: true));
				array2 = new CodeExpression[4];
				CreateInvokeParams(array2, text2, soapParameters.InParameters, soapParameters.InCheckSpecifiedCount);
				array2[2] = new CodeArgumentReferenceExpression("callback");
				array2[3] = new CodeArgumentReferenceExpression("asyncState");
				invoke = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "BeginInvoke", array2);
				codeMemberMethod2.Statements.Add(new CodeMethodReturnStatement(invoke));
				int num4 = soapParameters.OutParameters.Count + soapParameters.OutCheckSpecifiedCount;
				string[] array5 = new string[num4 + 1];
				SoapParameter.GetTypeFullNames(soapParameters.OutParameters, array5, 1, soapParameters.OutCheckSpecifiedCount, base.ServiceImporter.CodeGenerator);
				array5[0] = typeof(IAsyncResult).FullName;
				string[] array6 = new string[num4 + 1];
				SoapParameter.GetNames(soapParameters.OutParameters, array6, 1, soapParameters.OutCheckSpecifiedCount);
				array6[0] = "asyncResult";
				CodeFlags[] array7 = new CodeFlags[num4 + 1];
				for (int i = 0; i < num4; i++)
				{
					array7[i + 1] = CodeFlags.IsOut;
				}
				CodeMemberMethod codeMemberMethod3 = WebCodeGenerator.AddMethod(base.CodeTypeDeclaration, "End" + text2, array7, array5, array6, (soapParameters.Return == null) ? typeof(void).FullName : WebCodeGenerator.FullTypeName(soapParameters.Return, base.ServiceImporter.CodeGenerator), null, CodeFlags.IsPublic);
				codeMemberMethod3.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), docComment: true));
				CodeExpression codeExpression = new CodeArgumentReferenceExpression("asyncResult");
				invoke = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "EndInvoke", codeExpression);
				WriteReturnMappings(codeMemberMethod3, invoke, soapParameters, resultsName);
			}
			if (flag3)
			{
				string key = ProtocolImporter.MethodSignature(text2, text3, codeFlags, typeFullNames);
				DelegateInfo delegateInfo = (DelegateInfo)base.ExportContext[key];
				if (delegateInfo == null)
				{
					string handlerType = base.ClassNames.AddUnique(text2 + "CompletedEventHandler", text2);
					string handlerArgs = base.ClassNames.AddUnique(text2 + "CompletedEventArgs", text2);
					delegateInfo = new DelegateInfo(handlerType, handlerArgs);
				}
				string handlerName = base.MethodNames.AddUnique(text2 + "Completed", text2);
				string methodName = base.MethodNames.AddUnique(text2 + "Async", text2);
				string text4 = base.MethodNames.AddUnique(text2 + "OperationCompleted", text2);
				string callbackName = base.MethodNames.AddUnique("On" + text2 + "OperationCompleted", text2);
				WebCodeGenerator.AddEvent(base.CodeTypeDeclaration.Members, delegateInfo.handlerType, handlerName);
				WebCodeGenerator.AddCallbackDeclaration(base.CodeTypeDeclaration.Members, text4);
				string[] names = SoapParameter.GetNames(soapParameters.InParameters, soapParameters.InCheckSpecifiedCount);
				string text5 = ProtocolImporter.UniqueName("userState", names);
				CodeMemberMethod codeMemberMethod4 = WebCodeGenerator.AddAsyncMethod(base.CodeTypeDeclaration, methodName, SoapParameter.GetTypeFullNames(soapParameters.InParameters, soapParameters.InCheckSpecifiedCount, base.ServiceImporter.CodeGenerator), names, text4, callbackName, text5);
				array2 = new CodeExpression[4];
				CreateInvokeParams(array2, text2, soapParameters.InParameters, soapParameters.InCheckSpecifiedCount);
				array2[2] = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), text4);
				array2[3] = new CodeArgumentReferenceExpression(text5);
				invoke = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "InvokeAsync", array2);
				codeMemberMethod4.Statements.Add(invoke);
				bool flag4 = soapParameters.Return != null || soapParameters.OutParameters.Count > 0;
				WebCodeGenerator.AddCallbackImplementation(base.CodeTypeDeclaration, callbackName, handlerName, delegateInfo.handlerArgs, flag4);
				if (base.ExportContext[key] == null)
				{
					WebCodeGenerator.AddDelegate(base.ExtraCodeClasses, delegateInfo.handlerType, flag4 ? delegateInfo.handlerArgs : typeof(AsyncCompletedEventArgs).FullName);
					if (flag4)
					{
						int num5 = soapParameters.OutParameters.Count + soapParameters.OutCheckSpecifiedCount;
						string[] array8 = new string[num5 + 1];
						SoapParameter.GetTypeFullNames(soapParameters.OutParameters, array8, 1, soapParameters.OutCheckSpecifiedCount, base.ServiceImporter.CodeGenerator);
						array8[0] = ((soapParameters.Return == null) ? null : WebCodeGenerator.FullTypeName(soapParameters.Return, base.ServiceImporter.CodeGenerator));
						string[] array9 = new string[num5 + 1];
						SoapParameter.GetNames(soapParameters.OutParameters, array9, 1, soapParameters.OutCheckSpecifiedCount);
						array9[0] = ((soapParameters.Return == null) ? null : "Result");
						base.ExtraCodeClasses.Add(WebCodeGenerator.CreateArgsClass(delegateInfo.handlerArgs, array8, array9, base.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.PartialTypes)));
					}
					base.ExportContext[key] = delegateInfo;
				}
			}
		}
		return codeMemberMethod;
	}

	private void WriteReturnMappings(CodeMemberMethod codeMethod, CodeExpression invoke, SoapParameters parameters, string resultsName)
	{
		if (parameters.Return == null && parameters.OutParameters.Count == 0)
		{
			codeMethod.Statements.Add(new CodeExpressionStatement(invoke));
			return;
		}
		codeMethod.Statements.Add(new CodeVariableDeclarationStatement(typeof(object[]), resultsName, invoke));
		int num = ((parameters.Return != null) ? 1 : 0);
		for (int i = 0; i < parameters.OutParameters.Count; i++)
		{
			SoapParameter soapParameter = (SoapParameter)parameters.OutParameters[i];
			CodeExpression left = new CodeArgumentReferenceExpression(soapParameter.name);
			CodeExpression codeExpression = new CodeArrayIndexerExpression();
			((CodeArrayIndexerExpression)codeExpression).TargetObject = new CodeVariableReferenceExpression(resultsName);
			((CodeArrayIndexerExpression)codeExpression).Indices.Add(new CodePrimitiveExpression(num++));
			codeExpression = new CodeCastExpression(WebCodeGenerator.FullTypeName(soapParameter.mapping, base.ServiceImporter.CodeGenerator), codeExpression);
			codeMethod.Statements.Add(new CodeAssignStatement(left, codeExpression));
			if (soapParameter.mapping.CheckSpecified)
			{
				left = new CodeArgumentReferenceExpression(soapParameter.name + "Specified");
				codeExpression = new CodeArrayIndexerExpression();
				((CodeArrayIndexerExpression)codeExpression).TargetObject = new CodeVariableReferenceExpression(resultsName);
				((CodeArrayIndexerExpression)codeExpression).Indices.Add(new CodePrimitiveExpression(num++));
				codeExpression = new CodeCastExpression(typeof(bool).FullName, codeExpression);
				codeMethod.Statements.Add(new CodeAssignStatement(left, codeExpression));
			}
		}
		if (parameters.Return != null)
		{
			CodeExpression codeExpression2 = new CodeArrayIndexerExpression();
			((CodeArrayIndexerExpression)codeExpression2).TargetObject = new CodeVariableReferenceExpression(resultsName);
			((CodeArrayIndexerExpression)codeExpression2).Indices.Add(new CodePrimitiveExpression(0));
			codeExpression2 = new CodeCastExpression(WebCodeGenerator.FullTypeName(parameters.Return, base.ServiceImporter.CodeGenerator), codeExpression2);
			codeMethod.Statements.Add(new CodeMethodReturnStatement(codeExpression2));
		}
	}

	private void CreateInvokeParams(CodeExpression[] invokeParams, string methodName, IList parameters, int checkSpecifiedCount)
	{
		invokeParams[0] = new CodePrimitiveExpression(methodName);
		CodeExpression[] array = new CodeExpression[parameters.Count + checkSpecifiedCount];
		int num = 0;
		for (int i = 0; i < parameters.Count; i++)
		{
			SoapParameter soapParameter = (SoapParameter)parameters[i];
			array[num++] = new CodeArgumentReferenceExpression(soapParameter.name);
			if (soapParameter.mapping.CheckSpecified)
			{
				array[num++] = new CodeArgumentReferenceExpression(soapParameter.specifiedName);
			}
		}
		invokeParams[1] = new CodeArrayCreateExpression(typeof(object).FullName, array);
	}

	private bool CheckMessageStyles(string messageName, MessagePart[] parts, SoapBodyBinding soapBodyBinding, SoapBindingStyle soapBindingStyle, out bool hasWrapper)
	{
		hasWrapper = false;
		if (soapBodyBinding.Use == SoapBindingUse.Default)
		{
			soapBodyBinding.Use = SoapBindingUse.Literal;
		}
		if (soapBodyBinding.Use == SoapBindingUse.Literal)
		{
			MessagePart[] array;
			if (soapBindingStyle == SoapBindingStyle.Rpc)
			{
				array = parts;
				for (int i = 0; i < array.Length; i++)
				{
					if (!array[i].Element.IsEmpty)
					{
						UnsupportedOperationBindingWarning(Res.GetString("EachMessagePartInRpcUseLiteralMessageMustSpecify0"));
						return false;
					}
				}
				return true;
			}
			if (parts.Length == 1 && !parts[0].Type.IsEmpty)
			{
				if (!parts[0].Element.IsEmpty)
				{
					UnsupportedOperationBindingWarning(Res.GetString("SpecifyingATypeForUseLiteralMessagesIs0"));
					return false;
				}
				if (xmlImporter.ImportAnyType(parts[0].Type, parts[0].Name) == null)
				{
					UnsupportedOperationBindingWarning(Res.GetString("SpecifyingATypeForUseLiteralMessagesIsAny", parts[0].Type.Name, parts[0].Type.Namespace));
					return false;
				}
				return true;
			}
			array = parts;
			foreach (MessagePart messagePart in array)
			{
				if (!messagePart.Type.IsEmpty)
				{
					UnsupportedOperationBindingWarning(Res.GetString("SpecifyingATypeForUseLiteralMessagesIs0"));
					return false;
				}
				if (messagePart.Element.IsEmpty)
				{
					UnsupportedOperationBindingWarning(Res.GetString("EachMessagePartInAUseLiteralMessageMustSpecify0"));
					return false;
				}
			}
		}
		else if (soapBodyBinding.Use == SoapBindingUse.Encoded)
		{
			if (!IsSoapEncodingPresent(soapBodyBinding.Encoding))
			{
				UnsupportedOperationBindingWarning(Res.GetString("TheEncodingIsNotSupported1", soapBodyBinding.Encoding));
				return false;
			}
			MessagePart[] array = parts;
			foreach (MessagePart messagePart2 in array)
			{
				if (!messagePart2.Element.IsEmpty)
				{
					UnsupportedOperationBindingWarning(Res.GetString("SpecifyingAnElementForUseEncodedMessageParts0"));
					return false;
				}
				if (messagePart2.Type.IsEmpty)
				{
					UnsupportedOperationBindingWarning(Res.GetString("EachMessagePartInAnUseEncodedMessageMustSpecify0"));
					return false;
				}
			}
		}
		switch (soapBindingStyle)
		{
		case SoapBindingStyle.Rpc:
			return true;
		case SoapBindingStyle.Document:
			hasWrapper = parts.Length == 1 && string.Compare(parts[0].Name, "parameters", StringComparison.Ordinal) == 0;
			return true;
		default:
			return false;
		}
	}

	/// <summary>Checks for the presence of "http://schemas.xmlsoap.org/soap/encoding/" in a string that represents a list of Uniform Resource Indicators (URIs).</summary>
	/// <param name="uriList">A space-delimited list of URIs.</param>
	/// <returns>
	///     <see langword="true" /> it the string contains http://schemas.xmlsoap.org/soap/encoding/; otherwise <see langword="false" />.</returns>
	protected virtual bool IsSoapEncodingPresent(string uriList)
	{
		int num = 0;
		do
		{
			num = uriList.IndexOf("http://schemas.xmlsoap.org/soap/encoding/", num, StringComparison.Ordinal);
			if (num < 0)
			{
				return false;
			}
			int num2 = num + "http://schemas.xmlsoap.org/soap/encoding/".Length;
			if ((num == 0 || uriList[num - 1] == ' ') && (num2 == uriList.Length || uriList[num2] == ' '))
			{
				return true;
			}
			num = num2;
		}
		while (num < uriList.Length);
		return false;
	}

	private MessagePart[] GetMessageParts(Message message, SoapBodyBinding soapBodyBinding)
	{
		MessagePart[] array;
		if (soapBodyBinding.Parts == null)
		{
			array = new MessagePart[message.Parts.Count];
			message.Parts.CopyTo(array, 0);
		}
		else
		{
			array = message.FindPartsByName(soapBodyBinding.Parts);
		}
		return array;
	}

	private XmlMembersMapping ImportMessage(string messageName, MessagePart[] parts, SoapBodyBinding soapBodyBinding, SoapBindingStyle soapBindingStyle, bool wrapped)
	{
		if (soapBodyBinding.Use == SoapBindingUse.Encoded)
		{
			return ImportEncodedMessage(messageName, parts, soapBodyBinding, wrapped);
		}
		return ImportLiteralMessage(messageName, parts, soapBodyBinding, soapBindingStyle, wrapped);
	}

	private XmlMembersMapping ImportEncodedMessage(string messageName, MessagePart[] parts, SoapBodyBinding soapBodyBinding, bool wrapped)
	{
		XmlMembersMapping xmlMembersMapping;
		if (wrapped)
		{
			SoapSchemaMember soapSchemaMember = new SoapSchemaMember();
			soapSchemaMember.MemberName = parts[0].Name;
			soapSchemaMember.MemberType = parts[0].Type;
			xmlMembersMapping = soapImporter.ImportMembersMapping(messageName, soapBodyBinding.Namespace, soapSchemaMember);
		}
		else
		{
			SoapSchemaMember[] array = new SoapSchemaMember[parts.Length];
			for (int i = 0; i < array.Length; i++)
			{
				MessagePart messagePart = parts[i];
				SoapSchemaMember soapSchemaMember2 = new SoapSchemaMember();
				soapSchemaMember2.MemberName = messagePart.Name;
				soapSchemaMember2.MemberType = messagePart.Type;
				array[i] = soapSchemaMember2;
			}
			xmlMembersMapping = soapImporter.ImportMembersMapping(messageName, soapBodyBinding.Namespace, array);
		}
		soapMembers.Add(xmlMembersMapping);
		return xmlMembersMapping;
	}

	private XmlMembersMapping ImportLiteralMessage(string messageName, MessagePart[] parts, SoapBodyBinding soapBodyBinding, SoapBindingStyle soapBindingStyle, bool wrapped)
	{
		XmlMembersMapping xmlMembersMapping;
		if (soapBindingStyle == SoapBindingStyle.Rpc)
		{
			SoapSchemaMember[] array = new SoapSchemaMember[parts.Length];
			for (int i = 0; i < array.Length; i++)
			{
				MessagePart messagePart = parts[i];
				SoapSchemaMember soapSchemaMember = new SoapSchemaMember();
				soapSchemaMember.MemberName = messagePart.Name;
				soapSchemaMember.MemberType = messagePart.Type;
				array[i] = soapSchemaMember;
			}
			xmlMembersMapping = xmlImporter.ImportMembersMapping(messageName, soapBodyBinding.Namespace, array);
		}
		else if (wrapped)
		{
			xmlMembersMapping = xmlImporter.ImportMembersMapping(parts[0].Element);
		}
		else
		{
			if (parts.Length == 1 && !parts[0].Type.IsEmpty)
			{
				xmlMembersMapping = xmlImporter.ImportAnyType(parts[0].Type, parts[0].Name);
				xmlMembers.Add(xmlMembersMapping);
				return xmlMembersMapping;
			}
			XmlQualifiedName[] array2 = new XmlQualifiedName[parts.Length];
			for (int j = 0; j < parts.Length; j++)
			{
				array2[j] = parts[j].Element;
			}
			xmlMembersMapping = xmlImporter.ImportMembersMapping(array2);
		}
		xmlMembers.Add(xmlMembersMapping);
		return xmlMembersMapping;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> class. </summary>
	public SoapProtocolImporter()
	{
	}
}
