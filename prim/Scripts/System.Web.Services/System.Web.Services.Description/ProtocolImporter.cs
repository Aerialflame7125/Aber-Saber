using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Services.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Provides common functionality across communication protocols for generating classes for Web services. </summary>
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
public abstract class ProtocolImporter
{
	private ServiceDescriptionImporter importer;

	private CodeNamespace codeNamespace;

	private CodeIdentifiers methodNames;

	private CodeTypeDeclaration codeClass;

	private CodeTypeDeclarationCollection classes;

	private ServiceDescriptionImportWarnings warnings;

	private Port port;

	private PortType portType;

	private Binding binding;

	private Operation operation;

	private OperationBinding operationBinding;

	private bool encodedBinding;

	private ImportContext importContext;

	private Hashtable exportContext;

	private Service service;

	private Message inputMessage;

	private Message outputMessage;

	private string className;

	private int bindingCount;

	private bool anyPorts;

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> objects that belong to the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance that are searched for bindings from which to generate binding classes.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> objects that belong to the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance that are searched for bindings from which to generate binding classes.</returns>
	public ServiceDescriptionCollection ServiceDescriptions => importer.ServiceDescriptions;

	/// <summary>Gets all the XML schemas, both abstract and concrete, used by the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance.</summary>
	/// <returns>The XML schemas, both abstract and concrete, used by the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance.</returns>
	public XmlSchemas Schemas => importer.AllSchemas;

	/// <summary>Gets the abstract XML schemas used by the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance.</summary>
	/// <returns>The abstract XML schemas used by the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance</returns>
	public XmlSchemas AbstractSchemas => importer.AbstractSchemas;

	/// <summary>Gets the concrete XML schemas used by the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance.</summary>
	/// <returns>The concrete XML schemas used by the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance.</returns>
	public XmlSchemas ConcreteSchemas => importer.ConcreteSchemas;

	/// <summary>Gets a representation of the .NET Framework namespace of the binding classes that are being generated.</summary>
	/// <returns>A representation of the .NET Framework namespace of the binding classes that are being generated.</returns>
	public CodeNamespace CodeNamespace => codeNamespace;

	/// <summary>Gets a representation of the binding class that is currently being generated.</summary>
	/// <returns>A representation of the binding class that is currently being generated.</returns>
	public CodeTypeDeclaration CodeTypeDeclaration => codeClass;

	internal CodeTypeDeclarationCollection ExtraCodeClasses
	{
		get
		{
			if (classes == null)
			{
				classes = new CodeTypeDeclarationCollection();
			}
			return classes;
		}
	}

	/// <summary>Gets an enumeration value that indicates whether a client proxy class or an abstract server class is being generated. The values are Client and Server. The value is that of the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance's <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.Style" /> property.</summary>
	/// <returns>An enumeration value that indicates whether a client proxy class or an abstract server class is being generated. The values are Client and Server. The value is that of the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance's <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.Style" /> property.</returns>
	public ServiceDescriptionImportStyle Style => importer.Style;

	/// <summary>Gets or sets a <see cref="T:System.Web.Services.Description.ServiceDescriptionImportWarnings" /> enumeration value that indicates the types of warnings, if any, issued by the protocol importer while generating binding classes.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescriptionImportWarnings" /> enumeration value that indicates the types of warnings, if any, issued by the protocol importer while generating binding classes.</returns>
	public ServiceDescriptionImportWarnings Warnings
	{
		get
		{
			return warnings;
		}
		set
		{
			warnings = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Xml.Serialization.CodeIdentifiers" /> object that generates a unique name for the binding class that is currently being generated.</summary>
	/// <returns>The <see cref="T:System.Xml.Serialization.CodeIdentifiers" /> object that generates a unique name for the binding class that is currently being generated.</returns>
	public CodeIdentifiers ClassNames => importContext.TypeIdentifiers;

	/// <summary>Gets the name of the binding class method which that the protocol importer is currently generating.</summary>
	/// <returns>The name of the binding class method which that the protocol importer is currently generating.</returns>
	public string MethodName => CodeIdentifier.MakeValid(XmlConvert.DecodeName(Operation.Name));

	/// <summary>Gets the name of the binding class that is currently being generated.</summary>
	/// <returns>The name of the binding class that is currently being generated.</returns>
	public string ClassName => className;

	/// <summary>Gets a Web Services Description Language (WSDL) port that contains a reference to the binding that the protocol importer is currently processing to generate a binding class. If more than one port refers to the current binding, the current port is the one in which the binding has most recently been found.</summary>
	/// <returns>The Web Services Description Language (WSDL) port that contains a reference to the binding that the protocol importer is currently processing to generate a binding class. If more than one port refers to the current binding, the current port is the one in which the binding has most recently been found.</returns>
	public Port Port => port;

	/// <summary>Gets the Web Services Description Language (WSDL) <see cref="P:System.Web.Services.Description.ProtocolImporter.PortType" /> that is implemented by the binding that the protocol importer is currently processing to generate a binding class.</summary>
	/// <returns>The Web Services Description Language (WSDL) <see cref="P:System.Web.Services.Description.ProtocolImporter.PortType" /> that is implemented by the binding that the protocol importer is currently processing to generate a binding class.</returns>
	public PortType PortType => portType;

	/// <summary>Gets the Web Services Description Language (WSDL) binding that the protocol importer is currently processing to generate a class.</summary>
	/// <returns>The Web Services Description Language (WSDL) binding that the protocol importer is currently processing to generate a class.</returns>
	public Binding Binding => binding;

	/// <summary>Gets the Web Services Description Language (WSDL) service that contains a reference to the binding that the protocol importer is currently processing to generate a binding class.</summary>
	/// <returns>The Web Services Description Language (WSDL) service that contains a reference to the binding that the protocol importer is currently processing to generate a binding class.</returns>
	public Service Service => service;

	internal ServiceDescriptionImporter ServiceImporter => importer;

	/// <summary>Gets the abstract Web Services Description Language (WSDL) operation that the protocol importer is currently processing to generate a method in a binding class.</summary>
	/// <returns>The abstract Web Services Description Language (WSDL) operation that the protocol importer is currently processing to generate a method in a binding class.</returns>
	public Operation Operation => operation;

	/// <summary>Gets the Web Services Description Language (WSDL) operation binding that the protocol importer is currently processing to generate a method in a binding class.</summary>
	/// <returns>The Web Services Description Language (WSDL) operation binding that the protocol importer is currently processing to generate a method in a binding class.</returns>
	public OperationBinding OperationBinding => operationBinding;

	/// <summary>Gets the Web Services Description Language (WSDL) input message for the abstract operation that the protocol importer is currently processing to generate a method in a binding class.</summary>
	/// <returns>The Web Services Description Language (WSDL) input message for the abstract operation that the protocol importer is currently processing to generate a method in a binding class.</returns>
	public Message InputMessage => inputMessage;

	/// <summary>Gets the Web Services Description Language (WSDL) output message for the abstract operation that the protocol importer is currently processing to generate a method in a binding class.</summary>
	/// <returns>The Web Services Description Language (WSDL) output message for the abstract operation that the protocol importer is currently processing to generate a method in a binding class.</returns>
	public Message OutputMessage => outputMessage;

	internal ImportContext ImportContext => importContext;

	internal bool IsEncodedBinding
	{
		get
		{
			return encodedBinding;
		}
		set
		{
			encodedBinding = value;
		}
	}

	internal Hashtable ExportContext
	{
		get
		{
			if (exportContext == null)
			{
				exportContext = new Hashtable();
			}
			return exportContext;
		}
	}

	internal CodeIdentifiers MethodNames
	{
		get
		{
			if (methodNames == null)
			{
				methodNames = new CodeIdentifiers();
			}
			return methodNames;
		}
	}

	/// <summary>Abstract property that concrete derived classes must implement to get the name of the protocol being used.</summary>
	/// <returns>The name of the protocol being used.</returns>
	public abstract string ProtocolName { get; }

	internal void Initialize(ServiceDescriptionImporter importer)
	{
		this.importer = importer;
	}

	internal bool GenerateCode(CodeNamespace codeNamespace, ImportContext importContext, Hashtable exportContext)
	{
		bindingCount = 0;
		anyPorts = false;
		this.codeNamespace = codeNamespace;
		Hashtable hashtable = new Hashtable();
		Hashtable hashtable2 = new Hashtable();
		foreach (ServiceDescription serviceDescription2 in ServiceDescriptions)
		{
			foreach (Service service3 in serviceDescription2.Services)
			{
				foreach (Port port3 in service3.Ports)
				{
					Binding binding = ServiceDescriptions.GetBinding(port3.Binding);
					if (!hashtable.Contains(binding))
					{
						PortType portType = ServiceDescriptions.GetPortType(binding.Type);
						MoveToBinding(service3, port3, binding, portType);
						if (IsBindingSupported())
						{
							bindingCount++;
							anyPorts = true;
							hashtable.Add(binding, binding);
						}
						else if (binding != null)
						{
							hashtable2[binding] = binding;
						}
					}
				}
			}
		}
		if (bindingCount == 0)
		{
			foreach (ServiceDescription serviceDescription3 in ServiceDescriptions)
			{
				foreach (Binding binding5 in serviceDescription3.Bindings)
				{
					if (!hashtable2.Contains(binding5))
					{
						PortType portType2 = ServiceDescriptions.GetPortType(binding5.Type);
						MoveToBinding(binding5, portType2);
						if (IsBindingSupported())
						{
							bindingCount++;
						}
					}
				}
			}
		}
		if (bindingCount == 0)
		{
			return codeNamespace.Comments.Count > 0;
		}
		this.importContext = importContext;
		this.exportContext = exportContext;
		BeginNamespace();
		hashtable.Clear();
		foreach (ServiceDescription serviceDescription4 in ServiceDescriptions)
		{
			if (anyPorts)
			{
				foreach (Service service4 in serviceDescription4.Services)
				{
					foreach (Port port4 in service4.Ports)
					{
						Binding binding3 = ServiceDescriptions.GetBinding(port4.Binding);
						PortType portType3 = ServiceDescriptions.GetPortType(binding3.Type);
						MoveToBinding(service4, port4, binding3, portType3);
						if (IsBindingSupported() && !hashtable.Contains(binding3))
						{
							GenerateClassForBinding();
							hashtable.Add(binding3, binding3);
						}
					}
				}
				continue;
			}
			foreach (Binding binding6 in serviceDescription4.Bindings)
			{
				PortType portType4 = ServiceDescriptions.GetPortType(binding6.Type);
				MoveToBinding(binding6, portType4);
				if (IsBindingSupported())
				{
					GenerateClassForBinding();
				}
			}
		}
		EndNamespace();
		return true;
	}

	private void MoveToBinding(Binding binding, PortType portType)
	{
		MoveToBinding(null, null, binding, portType);
	}

	private void MoveToBinding(Service service, Port port, Binding binding, PortType portType)
	{
		this.service = service;
		this.port = port;
		this.portType = portType;
		this.binding = binding;
		encodedBinding = false;
	}

	private void MoveToOperation(Operation operation)
	{
		this.operation = operation;
		this.operationBinding = null;
		foreach (OperationBinding operation2 in binding.Operations)
		{
			if (operation.IsBoundBy(operation2))
			{
				if (operationBinding != null)
				{
					throw OperationSyntaxException(Res.GetString("DuplicateInputOutputNames0"));
				}
				operationBinding = operation2;
			}
		}
		if (operationBinding == null)
		{
			throw OperationSyntaxException(Res.GetString("MissingBinding0"));
		}
		if (operation.Messages.Input != null && operationBinding.Input == null)
		{
			throw OperationSyntaxException(Res.GetString("MissingInputBinding0"));
		}
		if (operation.Messages.Output != null && operationBinding.Output == null)
		{
			throw OperationSyntaxException(Res.GetString("MissingOutputBinding0"));
		}
		inputMessage = ((operation.Messages.Input == null) ? null : ServiceDescriptions.GetMessage(operation.Messages.Input.Message));
		outputMessage = ((operation.Messages.Output == null) ? null : ServiceDescriptions.GetMessage(operation.Messages.Output.Message));
	}

	private void GenerateClassForBinding()
	{
		try
		{
			if (bindingCount == 1 && service != null && Style != ServiceDescriptionImportStyle.ServerInterface)
			{
				className = XmlConvert.DecodeName(service.Name);
			}
			else
			{
				className = binding.Name;
				if (Style == ServiceDescriptionImportStyle.ServerInterface)
				{
					className = "I" + CodeIdentifier.MakePascal(className);
				}
			}
			className = XmlConvert.DecodeName(className);
			className = ClassNames.AddUnique(CodeIdentifier.MakeValid(className), null);
			codeClass = BeginClass();
			int num = 0;
			for (int i = 0; i < portType.Operations.Count; i++)
			{
				MoveToOperation(portType.Operations[i]);
				if (!IsOperationFlowSupported(operation.Messages.Flow))
				{
					switch (operation.Messages.Flow)
					{
					case OperationFlow.SolicitResponse:
						UnsupportedOperationWarning(Res.GetString("SolicitResponseIsNotSupported0"));
						continue;
					case OperationFlow.RequestResponse:
						UnsupportedOperationWarning(Res.GetString("RequestResponseIsNotSupported0"));
						continue;
					case OperationFlow.OneWay:
						UnsupportedOperationWarning(Res.GetString("OneWayIsNotSupported0"));
						continue;
					case OperationFlow.Notification:
						UnsupportedOperationWarning(Res.GetString("NotificationIsNotSupported0"));
						continue;
					}
				}
				CodeMemberMethod codeMemberMethod;
				try
				{
					codeMemberMethod = GenerateMethod();
				}
				catch (Exception ex)
				{
					if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
					{
						throw;
					}
					throw new InvalidOperationException(Res.GetString("UnableToImportOperation1", operation.Name), ex);
				}
				if (codeMemberMethod != null)
				{
					AddExtensionWarningComments(codeClass.Comments, operationBinding.Extensions);
					if (operationBinding.Input != null)
					{
						AddExtensionWarningComments(codeClass.Comments, operationBinding.Input.Extensions);
					}
					if (operationBinding.Output != null)
					{
						AddExtensionWarningComments(codeClass.Comments, operationBinding.Output.Extensions);
					}
					num++;
				}
			}
			if ((ServiceImporter.CodeGenerationOptions & CodeGenerationOptions.GenerateNewAsync) != 0 && ServiceImporter.CodeGenerator.Supports(GeneratorSupport.DeclareEvents) && ServiceImporter.CodeGenerator.Supports(GeneratorSupport.DeclareDelegates) && num > 0 && Style == ServiceDescriptionImportStyle.Client)
			{
				CodeAttributeDeclarationCollection metadata = new CodeAttributeDeclarationCollection();
				string text = "CancelAsync";
				string text2 = MethodNames.AddUnique(text, text);
				CodeMemberMethod codeMemberMethod2 = WebCodeGenerator.AddMethod(CodeTypeDeclaration, text2, new CodeFlags[1], new string[1] { typeof(object).FullName }, new string[1] { "userState" }, typeof(void).FullName, metadata, (CodeFlags)(1 | ((!(text != text2)) ? 8 : 0)));
				codeMemberMethod2.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), docComment: true));
				CodeMethodInvokeExpression value = new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(), text)
				{
					Parameters = { (CodeExpression)new CodeArgumentReferenceExpression("userState") }
				};
				codeMemberMethod2.Statements.Add(value);
			}
			EndClass();
			if (portType.Operations.Count == 0)
			{
				NoMethodsGeneratedWarning();
			}
			AddExtensionWarningComments(codeClass.Comments, binding.Extensions);
			if (port != null)
			{
				AddExtensionWarningComments(codeClass.Comments, port.Extensions);
			}
			codeNamespace.Types.Add(codeClass);
		}
		catch (Exception ex2)
		{
			if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
			{
				throw;
			}
			throw new InvalidOperationException(Res.GetString("UnableToImportBindingFromNamespace2", binding.Name, binding.ServiceDescription.TargetNamespace), ex2);
		}
	}

	/// <summary>For each unhandled extension or XML element in the input extensions collection, turns on a <see cref="F:System.Web.Services.Description.ServiceDescriptionImportWarnings.RequiredExtensionsIgnored" /> or <see cref="F:System.Web.Services.Description.ServiceDescriptionImportWarnings.OptionalExtensionsIgnored" /> warning for each unhandled extension or XML element in the input extensions collection. </summary>
	/// <param name="comments">A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> that specifies the collection of code comments to which each warning message is added.</param>
	/// <param name="extensions">A <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> that specifies the extensions or XML elements for which warnings should be generated if they are not handled.</param>
	public void AddExtensionWarningComments(CodeCommentStatementCollection comments, ServiceDescriptionFormatExtensionCollection extensions)
	{
		foreach (object extension in extensions)
		{
			if (extensions.IsHandled(extension))
			{
				continue;
			}
			string text = null;
			string text2 = null;
			if (extension is XmlElement)
			{
				XmlElement obj = (XmlElement)extension;
				text = obj.LocalName;
				text2 = obj.NamespaceURI;
			}
			else if (extension is ServiceDescriptionFormatExtension)
			{
				XmlFormatExtensionAttribute[] array = (XmlFormatExtensionAttribute[])extension.GetType().GetCustomAttributes(typeof(XmlFormatExtensionAttribute), inherit: false);
				if (array.Length != 0)
				{
					text = array[0].ElementName;
					text2 = array[0].Namespace;
				}
			}
			if (text != null)
			{
				if (extensions.IsRequired(extension))
				{
					warnings |= ServiceDescriptionImportWarnings.RequiredExtensionsIgnored;
					AddWarningComment(comments, Res.GetString("WebServiceDescriptionIgnoredRequired", text, text2));
				}
				else
				{
					warnings |= ServiceDescriptionImportWarnings.OptionalExtensionsIgnored;
					AddWarningComment(comments, Res.GetString("WebServiceDescriptionIgnoredOptional", text, text2));
				}
			}
		}
	}

	/// <summary>Turns on an <see cref="F:System.Web.Services.Description.ServiceDescriptionImportWarnings.UnsupportedBindingsIgnored" /> warning in the <see cref="T:System.Web.Services.Description.ServiceDescriptionImportWarnings" /> enumeration obtained through the <see cref="P:System.Web.Services.Description.ProtocolImporter.Warnings" /> property. This method also adds a warning message to the comments for the class that is being generated.</summary>
	/// <param name="text">Annotation to be added to the warning message, which already indicates that the binding has been ignored.</param>
	public void UnsupportedBindingWarning(string text)
	{
		AddWarningComment((codeClass == null) ? codeNamespace.Comments : codeClass.Comments, Res.GetString("TheBinding0FromNamespace1WasIgnored2", Binding.Name, Binding.ServiceDescription.TargetNamespace, text));
		warnings |= ServiceDescriptionImportWarnings.UnsupportedBindingsIgnored;
	}

	/// <summary>Turns on an <see cref="F:System.Web.Services.Description.ServiceDescriptionImportWarnings.UnsupportedOperationsIgnored" /> warning in the <see cref="T:System.Web.Services.Description.ServiceDescriptionImportWarnings" /> enumeration obtained through the <see cref="P:System.Web.Services.Description.ProtocolImporter.Warnings" /> property. This method also adds a warning message to the comments for the class that is being generated.</summary>
	/// <param name="text">Annotation to be added to the warning message, which already indicates that the operation binding has been ignored.</param>
	public void UnsupportedOperationWarning(string text)
	{
		AddWarningComment((codeClass == null) ? codeNamespace.Comments : codeClass.Comments, Res.GetString("TheOperation0FromNamespace1WasIgnored2", operation.Name, operation.PortType.ServiceDescription.TargetNamespace, text));
		warnings |= ServiceDescriptionImportWarnings.UnsupportedOperationsIgnored;
	}

	/// <summary>Turns on an <see cref="F:System.Web.Services.Description.ServiceDescriptionImportWarnings.UnsupportedOperationsIgnored" /> warning in the <see cref="T:System.Web.Services.Description.ServiceDescriptionImportWarnings" /> enumeration obtained through the <see cref="P:System.Web.Services.Description.ProtocolImporter.Warnings" /> property. This method also adds a warning message to the comments for the class that is being generated.</summary>
	/// <param name="text">Annotation to be added to the warning message, which already indicates that the operation binding has been ignored.</param>
	public void UnsupportedOperationBindingWarning(string text)
	{
		AddWarningComment((codeClass == null) ? codeNamespace.Comments : codeClass.Comments, Res.GetString("TheOperationBinding0FromNamespace1WasIgnored", operationBinding.Name, operationBinding.Binding.ServiceDescription.TargetNamespace, text));
		warnings |= ServiceDescriptionImportWarnings.UnsupportedOperationsIgnored;
	}

	private void NoMethodsGeneratedWarning()
	{
		AddWarningComment(codeClass.Comments, Res.GetString("NoMethodsWereFoundInTheWSDLForThisProtocol"));
		warnings |= ServiceDescriptionImportWarnings.NoMethodsGenerated;
	}

	internal static void AddWarningComment(CodeCommentStatementCollection comments, string text)
	{
		comments.Add(new CodeCommentStatement(Res.GetString("CodegenWarningDetails", text)));
	}

	/// <summary>Produces an Exception indicating that the current <see cref="P:System.Web.Services.Description.ProtocolImporter.Operation" /> instance for which a binding class is being generated is invalid within the target namespace.</summary>
	/// <param name="text">Annotation to be added to the exception message, which already indicates that the operation syntax is invalid.</param>
	/// <returns>An Exception indicating that the current <see cref="P:System.Web.Services.Description.ProtocolImporter.Operation" /> instance for which a binding class is being generated is invalid within the target namespace.</returns>
	public Exception OperationSyntaxException(string text)
	{
		return new Exception(Res.GetString("TheOperationFromNamespaceHadInvalidSyntax3", operation.Name, operation.PortType.Name, operation.PortType.ServiceDescription.TargetNamespace, text));
	}

	/// <summary>Produces an Exception indicating that the current <see cref="P:System.Web.Services.Description.ProtocolImporter.OperationBinding" /> instance for which a binding class is being generated is invalid within the target namespace.</summary>
	/// <param name="text">Annotation to be added to the exception message, which already indicates that the operation binding syntax is invalid.</param>
	/// <returns>An Exception indicating that the current <see cref="P:System.Web.Services.Description.ProtocolImporter.OperationBinding" /> instance for which a binding class is being generated is invalid within the target namespace.</returns>
	public Exception OperationBindingSyntaxException(string text)
	{
		return new Exception(Res.GetString("TheOperationBindingFromNamespaceHadInvalid3", operationBinding.Name, operationBinding.Binding.ServiceDescription.TargetNamespace, text));
	}

	/// <summary>When overridden in a derived class, performs namespace-wide initialization during code generation.</summary>
	protected virtual void BeginNamespace()
	{
		MethodNames.Clear();
	}

	/// <summary>When overridden in a derived class, determines whether a class can be generated for the current binding.</summary>
	/// <returns>True if the binding is supported; otherwise false.</returns>
	protected abstract bool IsBindingSupported();

	/// <summary>When overridden in a derived class, determines whether the current operation's operation flow is supported.</summary>
	/// <param name="flow">An <see cref="T:System.Web.Services.Description.OperationFlow" />  enumeration value that represents a transmission pattern.</param>
	/// <returns>True if the current operation's operation flow is supported.</returns>
	protected abstract bool IsOperationFlowSupported(OperationFlow flow);

	/// <summary>When overridden in a derived class, initializes the generation of a binding class.</summary>
	/// <returns>The generated class.</returns>
	protected abstract CodeTypeDeclaration BeginClass();

	/// <summary>When overridden in a derived class, generates method code for binding classes.</summary>
	/// <returns>The generated method.</returns>
	protected abstract CodeMemberMethod GenerateMethod();

	/// <summary>When overridden in a derived class, processes a binding class.</summary>
	protected virtual void EndClass()
	{
	}

	/// <summary>When overridden in a derived class, performs processing for an entire namespace.</summary>
	protected virtual void EndNamespace()
	{
		if (classes != null)
		{
			foreach (CodeTypeDeclaration @class in classes)
			{
				codeNamespace.Types.Add(@class);
			}
		}
		CodeGenerator.ValidateIdentifiers(codeNamespace);
	}

	internal static string UniqueName(string baseName, string[] scope)
	{
		CodeIdentifiers codeIdentifiers = new CodeIdentifiers();
		for (int i = 0; i < scope.Length; i++)
		{
			codeIdentifiers.AddUnique(scope[i], scope[i]);
		}
		return codeIdentifiers.AddUnique(baseName, baseName);
	}

	internal static string MethodSignature(string methodName, string returnType, CodeFlags[] parameterFlags, string[] parameterTypes)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(returnType);
		stringBuilder.Append(" ");
		stringBuilder.Append(methodName);
		stringBuilder.Append(" (");
		for (int i = 0; i < parameterTypes.Length; i++)
		{
			if ((parameterFlags[i] & CodeFlags.IsByRef) != 0)
			{
				stringBuilder.Append("ref ");
			}
			else if ((parameterFlags[i] & CodeFlags.IsOut) != 0)
			{
				stringBuilder.Append("out ");
			}
			stringBuilder.Append(parameterTypes[i]);
			if (i > 0)
			{
				stringBuilder.Append(",");
			}
		}
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.ProtocolImporter" /> class. </summary>
	protected ProtocolImporter()
	{
	}
}
