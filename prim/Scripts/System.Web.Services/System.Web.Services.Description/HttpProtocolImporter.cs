using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Web.Services.Configuration;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

internal abstract class HttpProtocolImporter : ProtocolImporter
{
	private MimeImporter[] importers;

	private ArrayList[] importedParameters;

	private ArrayList[] importedReturns;

	private bool hasInputPayload;

	private ArrayList codeClasses = new ArrayList();

	internal abstract Type BaseClass { get; }

	protected HttpProtocolImporter(bool hasInputPayload)
	{
		Type[] mimeImporterTypes = WebServicesSection.Current.MimeImporterTypes;
		importers = new MimeImporter[mimeImporterTypes.Length];
		importedParameters = new ArrayList[mimeImporterTypes.Length];
		importedReturns = new ArrayList[mimeImporterTypes.Length];
		for (int i = 0; i < importers.Length; i++)
		{
			MimeImporter mimeImporter = (MimeImporter)Activator.CreateInstance(mimeImporterTypes[i]);
			mimeImporter.ImportContext = this;
			importedParameters[i] = new ArrayList();
			importedReturns[i] = new ArrayList();
			importers[i] = mimeImporter;
		}
		this.hasInputPayload = hasInputPayload;
	}

	private MimeParameterCollection ImportMimeParameters()
	{
		for (int i = 0; i < importers.Length; i++)
		{
			MimeParameterCollection mimeParameterCollection = importers[i].ImportParameters();
			if (mimeParameterCollection != null)
			{
				importedParameters[i].Add(mimeParameterCollection);
				return mimeParameterCollection;
			}
		}
		return null;
	}

	private MimeReturn ImportMimeReturn()
	{
		if (base.OperationBinding.Output.Extensions.Count == 0)
		{
			MimeReturn mimeReturn = new MimeReturn();
			mimeReturn.TypeName = typeof(void).FullName;
			return mimeReturn;
		}
		for (int i = 0; i < importers.Length; i++)
		{
			MimeReturn mimeReturn = importers[i].ImportReturn();
			if (mimeReturn != null)
			{
				importedReturns[i].Add(mimeReturn);
				return mimeReturn;
			}
		}
		return null;
	}

	private MimeParameterCollection ImportUrlParameters()
	{
		if ((HttpUrlEncodedBinding)base.OperationBinding.Input.Extensions.Find(typeof(HttpUrlEncodedBinding)) == null)
		{
			return new MimeParameterCollection();
		}
		return ImportStringParametersMessage();
	}

	internal MimeParameterCollection ImportStringParametersMessage()
	{
		MimeParameterCollection mimeParameterCollection = new MimeParameterCollection();
		foreach (MessagePart part in base.InputMessage.Parts)
		{
			MimeParameter mimeParameter = ImportUrlParameter(part);
			if (mimeParameter == null)
			{
				return null;
			}
			mimeParameterCollection.Add(mimeParameter);
		}
		return mimeParameterCollection;
	}

	private MimeParameter ImportUrlParameter(MessagePart part)
	{
		return new MimeParameter
		{
			Name = CodeIdentifier.MakeValid(XmlConvert.DecodeName(part.Name)),
			TypeName = (IsRepeatingParameter(part) ? typeof(string[]).FullName : typeof(string).FullName)
		};
	}

	private bool IsRepeatingParameter(MessagePart part)
	{
		XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)base.Schemas.Find(part.Type, typeof(XmlSchemaComplexType));
		if (xmlSchemaComplexType == null)
		{
			return false;
		}
		if (xmlSchemaComplexType.ContentModel == null)
		{
			return false;
		}
		if (xmlSchemaComplexType.ContentModel.Content == null)
		{
			throw new ArgumentException(Res.GetString("Missing2", xmlSchemaComplexType.Name, xmlSchemaComplexType.ContentModel.GetType().Name), "part");
		}
		if (xmlSchemaComplexType.ContentModel.Content is XmlSchemaComplexContentExtension)
		{
			return ((XmlSchemaComplexContentExtension)xmlSchemaComplexType.ContentModel.Content).BaseTypeName == new XmlQualifiedName("Array", "http://schemas.xmlsoap.org/soap/encoding/");
		}
		if (xmlSchemaComplexType.ContentModel.Content is XmlSchemaComplexContentRestriction)
		{
			return ((XmlSchemaComplexContentRestriction)xmlSchemaComplexType.ContentModel.Content).BaseTypeName == new XmlQualifiedName("Array", "http://schemas.xmlsoap.org/soap/encoding/");
		}
		return false;
	}

	private static void AppendMetadata(CodeAttributeDeclarationCollection from, CodeAttributeDeclarationCollection to)
	{
		foreach (CodeAttributeDeclaration item in from)
		{
			to.Add(item);
		}
	}

	private CodeMemberMethod GenerateMethod(HttpMethodInfo method)
	{
		MimeParameterCollection mimeParameterCollection = ((method.MimeParameters != null) ? method.MimeParameters : method.UrlParameters);
		string[] array = new string[mimeParameterCollection.Count];
		string[] array2 = new string[mimeParameterCollection.Count];
		for (int i = 0; i < mimeParameterCollection.Count; i++)
		{
			MimeParameter mimeParameter = mimeParameterCollection[i];
			array2[i] = mimeParameter.Name;
			array[i] = mimeParameter.TypeName;
		}
		CodeAttributeDeclarationCollection metadata = new CodeAttributeDeclarationCollection();
		CodeExpression[] array3 = new CodeExpression[2];
		if (method.MimeReturn.ReaderType == null)
		{
			array3[0] = new CodeTypeOfExpression(typeof(NopReturnReader).FullName);
		}
		else
		{
			array3[0] = new CodeTypeOfExpression(method.MimeReturn.ReaderType.FullName);
		}
		if (method.MimeParameters != null)
		{
			array3[1] = new CodeTypeOfExpression(method.MimeParameters.WriterType.FullName);
		}
		else
		{
			array3[1] = new CodeTypeOfExpression(typeof(UrlParameterWriter).FullName);
		}
		WebCodeGenerator.AddCustomAttribute(metadata, typeof(HttpMethodAttribute), array3, new string[0], new CodeExpression[0]);
		CodeMemberMethod codeMemberMethod = WebCodeGenerator.AddMethod(base.CodeTypeDeclaration, method.Name, new CodeFlags[array.Length], array, array2, method.MimeReturn.TypeName, metadata, (CodeFlags)(1 | ((base.Style != 0) ? 2 : 0)));
		AppendMetadata(method.MimeReturn.Attributes, codeMemberMethod.ReturnTypeCustomAttributes);
		codeMemberMethod.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), docComment: true));
		for (int j = 0; j < mimeParameterCollection.Count; j++)
		{
			AppendMetadata(mimeParameterCollection[j].Attributes, codeMemberMethod.Parameters[j].CustomAttributes);
		}
		if (base.Style == ServiceDescriptionImportStyle.Client)
		{
			bool num = (base.ServiceImporter.CodeGenerationOptions & CodeGenerationOptions.GenerateOldAsync) != 0;
			bool flag = (base.ServiceImporter.CodeGenerationOptions & CodeGenerationOptions.GenerateNewAsync) != 0 && base.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.DeclareEvents) && base.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.DeclareDelegates);
			CodeExpression[] array4 = new CodeExpression[3];
			CreateInvokeParams(array4, method, array2);
			CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "Invoke", array4);
			if (method.MimeReturn.ReaderType != null)
			{
				codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(new CodeCastExpression(method.MimeReturn.TypeName, expression)));
			}
			else
			{
				codeMemberMethod.Statements.Add(new CodeExpressionStatement(expression));
			}
			metadata = new CodeAttributeDeclarationCollection();
			string[] array5 = new string[array.Length + 2];
			array.CopyTo(array5, 0);
			array5[array.Length] = typeof(AsyncCallback).FullName;
			array5[array.Length + 1] = typeof(object).FullName;
			string[] array6 = new string[array2.Length + 2];
			array2.CopyTo(array6, 0);
			array6[array2.Length] = "callback";
			array6[array2.Length + 1] = "asyncState";
			if (num)
			{
				CodeMemberMethod codeMemberMethod2 = WebCodeGenerator.AddMethod(base.CodeTypeDeclaration, "Begin" + method.Name, new CodeFlags[array5.Length], array5, array6, typeof(IAsyncResult).FullName, metadata, CodeFlags.IsPublic);
				codeMemberMethod2.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), docComment: true));
				array4 = new CodeExpression[5];
				CreateInvokeParams(array4, method, array2);
				array4[3] = new CodeArgumentReferenceExpression("callback");
				array4[4] = new CodeArgumentReferenceExpression("asyncState");
				expression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "BeginInvoke", array4);
				codeMemberMethod2.Statements.Add(new CodeMethodReturnStatement(expression));
				CodeMemberMethod codeMemberMethod3 = WebCodeGenerator.AddMethod(base.CodeTypeDeclaration, "End" + method.Name, new CodeFlags[1], new string[1] { typeof(IAsyncResult).FullName }, new string[1] { "asyncResult" }, method.MimeReturn.TypeName, metadata, CodeFlags.IsPublic);
				codeMemberMethod3.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), docComment: true));
				CodeExpression codeExpression = new CodeArgumentReferenceExpression("asyncResult");
				expression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "EndInvoke", codeExpression);
				if (method.MimeReturn.ReaderType != null)
				{
					codeMemberMethod3.Statements.Add(new CodeMethodReturnStatement(new CodeCastExpression(method.MimeReturn.TypeName, expression)));
				}
				else
				{
					codeMemberMethod3.Statements.Add(new CodeExpressionStatement(expression));
				}
			}
			if (flag)
			{
				metadata = new CodeAttributeDeclarationCollection();
				string name = method.Name;
				string key = ProtocolImporter.MethodSignature(name, method.MimeReturn.TypeName, new CodeFlags[array.Length], array);
				DelegateInfo delegateInfo = (DelegateInfo)base.ExportContext[key];
				if (delegateInfo == null)
				{
					string handlerType = base.ClassNames.AddUnique(name + "CompletedEventHandler", name);
					string handlerArgs = base.ClassNames.AddUnique(name + "CompletedEventArgs", name);
					delegateInfo = new DelegateInfo(handlerType, handlerArgs);
				}
				string handlerName = base.MethodNames.AddUnique(name + "Completed", name);
				string methodName = base.MethodNames.AddUnique(name + "Async", name);
				string text = base.MethodNames.AddUnique(name + "OperationCompleted", name);
				string callbackName = base.MethodNames.AddUnique("On" + name + "OperationCompleted", name);
				WebCodeGenerator.AddEvent(base.CodeTypeDeclaration.Members, delegateInfo.handlerType, handlerName);
				WebCodeGenerator.AddCallbackDeclaration(base.CodeTypeDeclaration.Members, text);
				string text2 = ProtocolImporter.UniqueName("userState", array2);
				CodeMemberMethod codeMemberMethod4 = WebCodeGenerator.AddAsyncMethod(base.CodeTypeDeclaration, methodName, array, array2, text, callbackName, text2);
				array4 = new CodeExpression[5];
				CreateInvokeParams(array4, method, array2);
				array4[3] = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), text);
				array4[4] = new CodeArgumentReferenceExpression(text2);
				expression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "InvokeAsync", array4);
				codeMemberMethod4.Statements.Add(expression);
				bool flag2 = method.MimeReturn.ReaderType != null;
				WebCodeGenerator.AddCallbackImplementation(base.CodeTypeDeclaration, callbackName, handlerName, delegateInfo.handlerArgs, flag2);
				if (base.ExportContext[key] == null)
				{
					WebCodeGenerator.AddDelegate(base.ExtraCodeClasses, delegateInfo.handlerType, flag2 ? delegateInfo.handlerArgs : typeof(AsyncCompletedEventArgs).FullName);
					if (flag2)
					{
						base.ExtraCodeClasses.Add(WebCodeGenerator.CreateArgsClass(delegateInfo.handlerArgs, new string[1] { method.MimeReturn.TypeName }, new string[1] { "Result" }, base.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.PartialTypes)));
					}
					base.ExportContext[key] = delegateInfo;
				}
			}
		}
		return codeMemberMethod;
	}

	private void CreateInvokeParams(CodeExpression[] invokeParams, HttpMethodInfo method, string[] parameterNames)
	{
		invokeParams[0] = new CodePrimitiveExpression(method.Name);
		CodeExpression left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Url");
		CodeExpression right = new CodePrimitiveExpression(method.Href);
		invokeParams[1] = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Add, right);
		CodeExpression[] array = new CodeExpression[parameterNames.Length];
		for (int i = 0; i < parameterNames.Length; i++)
		{
			array[i] = new CodeArgumentReferenceExpression(parameterNames[i]);
		}
		invokeParams[2] = new CodeArrayCreateExpression(typeof(object).FullName, array);
	}

	protected override bool IsOperationFlowSupported(OperationFlow flow)
	{
		return flow == OperationFlow.RequestResponse;
	}

	protected override CodeMemberMethod GenerateMethod()
	{
		HttpOperationBinding httpOperationBinding = (HttpOperationBinding)base.OperationBinding.Extensions.Find(typeof(HttpOperationBinding));
		if (httpOperationBinding == null)
		{
			throw OperationBindingSyntaxException(Res.GetString("MissingHttpOperationElement0"));
		}
		HttpMethodInfo httpMethodInfo = new HttpMethodInfo();
		if (hasInputPayload)
		{
			httpMethodInfo.MimeParameters = ImportMimeParameters();
			if (httpMethodInfo.MimeParameters == null)
			{
				UnsupportedOperationWarning(Res.GetString("NoInputMIMEFormatsWereRecognized0"));
				return null;
			}
		}
		else
		{
			httpMethodInfo.UrlParameters = ImportUrlParameters();
			if (httpMethodInfo.UrlParameters == null)
			{
				UnsupportedOperationWarning(Res.GetString("NoInputHTTPFormatsWereRecognized0"));
				return null;
			}
		}
		httpMethodInfo.MimeReturn = ImportMimeReturn();
		if (httpMethodInfo.MimeReturn == null)
		{
			UnsupportedOperationWarning(Res.GetString("NoOutputMIMEFormatsWereRecognized0"));
			return null;
		}
		httpMethodInfo.Name = base.MethodNames.AddUnique(base.MethodName, httpMethodInfo);
		httpMethodInfo.Href = httpOperationBinding.Location;
		return GenerateMethod(httpMethodInfo);
	}

	protected override CodeTypeDeclaration BeginClass()
	{
		base.MethodNames.Clear();
		base.ExtraCodeClasses.Clear();
		CodeAttributeDeclarationCollection metadata = new CodeAttributeDeclarationCollection();
		if (base.Style == ServiceDescriptionImportStyle.Client)
		{
			WebCodeGenerator.AddCustomAttribute(metadata, typeof(DebuggerStepThroughAttribute), new CodeExpression[0]);
			WebCodeGenerator.AddCustomAttribute(metadata, typeof(DesignerCategoryAttribute), new CodeExpression[1]
			{
				new CodePrimitiveExpression("code")
			});
		}
		Type[] types = new Type[7]
		{
			typeof(SoapDocumentMethodAttribute),
			typeof(XmlAttributeAttribute),
			typeof(WebService),
			typeof(object),
			typeof(DebuggerStepThroughAttribute),
			typeof(DesignerCategoryAttribute),
			typeof(TransactionOption)
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
		CodeTypeDeclaration codeTypeDeclaration = WebCodeGenerator.CreateClass(base.ClassName, BaseClass.FullName, new string[0], metadata, CodeFlags.IsPublic | codeFlags, base.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.PartialTypes));
		codeTypeDeclaration.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), docComment: true));
		CodeConstructor codeConstructor = WebCodeGenerator.AddConstructor(codeTypeDeclaration, new string[0], new string[0], null, CodeFlags.IsPublic);
		codeConstructor.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), docComment: true));
		string url = ((base.Port == null) ? null : ((HttpAddressBinding)base.Port.Extensions.Find(typeof(HttpAddressBinding))))?.Location;
		ServiceDescription serviceDescription = base.Binding.ServiceDescription;
		ProtocolImporterUtil.GenerateConstructorStatements(codeConstructor, url, serviceDescription.AppSettingUrlKey, serviceDescription.AppSettingBaseUrl, soap11: false);
		codeClasses.Add(codeTypeDeclaration);
		return codeTypeDeclaration;
	}

	protected override void EndNamespace()
	{
		for (int i = 0; i < importers.Length; i++)
		{
			importers[i].GenerateCode((MimeReturn[])importedReturns[i].ToArray(typeof(MimeReturn)), (MimeParameterCollection[])importedParameters[i].ToArray(typeof(MimeParameterCollection)));
		}
		foreach (CodeTypeDeclaration codeClass in codeClasses)
		{
			if (codeClass.CustomAttributes == null)
			{
				codeClass.CustomAttributes = new CodeAttributeDeclarationCollection();
			}
			for (int j = 0; j < importers.Length; j++)
			{
				importers[j].AddClassMetadata(codeClass);
			}
		}
		foreach (CodeTypeDeclaration extraCodeClass in base.ExtraCodeClasses)
		{
			base.CodeNamespace.Types.Add(extraCodeClass);
		}
		CodeGenerator.ValidateIdentifiers(base.CodeNamespace);
	}
}
