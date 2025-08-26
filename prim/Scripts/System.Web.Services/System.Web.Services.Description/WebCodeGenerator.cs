using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

internal class WebCodeGenerator
{
	private static CodeAttributeDeclaration generatedCodeAttribute;

	internal static CodeAttributeDeclaration GeneratedCodeAttribute
	{
		get
		{
			if (generatedCodeAttribute == null)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(GeneratedCodeAttribute).FullName);
				Assembly assembly = Assembly.GetEntryAssembly();
				if (assembly == null)
				{
					assembly = Assembly.GetExecutingAssembly();
					if (assembly == null)
					{
						assembly = typeof(WebCodeGenerator).Assembly;
					}
				}
				AssemblyName name = assembly.GetName();
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(name.Name)));
				string productVersion = GetProductVersion(assembly);
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression((productVersion == null) ? name.Version.ToString() : productVersion)));
				generatedCodeAttribute = codeAttributeDeclaration;
			}
			return generatedCodeAttribute;
		}
	}

	private WebCodeGenerator()
	{
	}

	private static string GetProductVersion(Assembly assembly)
	{
		object[] customAttributes = assembly.GetCustomAttributes(inherit: true);
		for (int i = 0; i < customAttributes.Length; i++)
		{
			if (customAttributes[i] is AssemblyInformationalVersionAttribute)
			{
				return ((AssemblyInformationalVersionAttribute)customAttributes[i]).InformationalVersion;
			}
		}
		return null;
	}

	internal static string[] GetNamespacesForTypes(Type[] types)
	{
		Hashtable hashtable = new Hashtable();
		for (int i = 0; i < types.Length; i++)
		{
			string fullName = types[i].FullName;
			int num = fullName.LastIndexOf('.');
			if (num > 0)
			{
				hashtable[fullName.Substring(0, num)] = types[i];
			}
		}
		string[] array = new string[hashtable.Keys.Count];
		hashtable.Keys.CopyTo(array, 0);
		return array;
	}

	internal static void AddImports(CodeNamespace codeNamespace, string[] namespaces)
	{
		foreach (string nameSpace in namespaces)
		{
			codeNamespace.Imports.Add(new CodeNamespaceImport(nameSpace));
		}
	}

	private static CodeMemberProperty CreatePropertyDeclaration(CodeMemberField field, string name, string typeName)
	{
		CodeMemberProperty obj = new CodeMemberProperty
		{
			Type = new CodeTypeReference(typeName),
			Name = name
		};
		CodeMethodReturnStatement value = new CodeMethodReturnStatement
		{
			Expression = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name)
		};
		obj.GetStatements.Add(value);
		CodeExpression left = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name);
		CodeExpression right = new CodeArgumentReferenceExpression("value");
		obj.SetStatements.Add(new CodeAssignStatement(left, right));
		return obj;
	}

	internal static CodeTypeMember AddMember(CodeTypeDeclaration codeClass, string typeName, string memberName, CodeExpression initializer, CodeAttributeDeclarationCollection metadata, CodeFlags flags, CodeGenerationOptions options)
	{
		bool num = (options & CodeGenerationOptions.GenerateProperties) != 0;
		string name = (num ? MakeFieldName(memberName) : memberName);
		CodeMemberField codeMemberField = new CodeMemberField(typeName, name)
		{
			InitExpression = initializer
		};
		CodeTypeMember codeTypeMember;
		if (num)
		{
			codeClass.Members.Add(codeMemberField);
			codeTypeMember = CreatePropertyDeclaration(codeMemberField, memberName, typeName);
		}
		else
		{
			codeTypeMember = codeMemberField;
		}
		codeTypeMember.CustomAttributes = metadata;
		if ((flags & CodeFlags.IsPublic) != 0)
		{
			codeTypeMember.Attributes = (codeMemberField.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public;
		}
		codeClass.Members.Add(codeTypeMember);
		return codeTypeMember;
	}

	internal static string FullTypeName(XmlMemberMapping mapping, CodeDomProvider codeProvider)
	{
		return mapping.GenerateTypeName(codeProvider);
	}

	private static string MakeFieldName(string name)
	{
		return CodeIdentifier.MakeCamel(name) + "Field";
	}

	internal static CodeConstructor AddConstructor(CodeTypeDeclaration codeClass, string[] parameterTypeNames, string[] parameterNames, CodeAttributeDeclarationCollection metadata, CodeFlags flags)
	{
		CodeConstructor codeConstructor = new CodeConstructor();
		if ((flags & CodeFlags.IsPublic) != 0)
		{
			codeConstructor.Attributes = (codeConstructor.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public;
		}
		if ((flags & CodeFlags.IsAbstract) != 0)
		{
			codeConstructor.Attributes |= MemberAttributes.Abstract;
		}
		codeConstructor.CustomAttributes = metadata;
		for (int i = 0; i < parameterTypeNames.Length; i++)
		{
			CodeParameterDeclarationExpression value = new CodeParameterDeclarationExpression(parameterTypeNames[i], parameterNames[i]);
			codeConstructor.Parameters.Add(value);
		}
		codeClass.Members.Add(codeConstructor);
		return codeConstructor;
	}

	internal static CodeMemberMethod AddMethod(CodeTypeDeclaration codeClass, string methodName, CodeFlags[] parameterFlags, string[] parameterTypeNames, string[] parameterNames, string returnTypeName, CodeAttributeDeclarationCollection metadata, CodeFlags flags)
	{
		return AddMethod(codeClass, methodName, parameterFlags, parameterTypeNames, parameterNames, new CodeAttributeDeclarationCollection[0], returnTypeName, metadata, flags);
	}

	internal static CodeMemberMethod AddMethod(CodeTypeDeclaration codeClass, string methodName, CodeFlags[] parameterFlags, string[] parameterTypeNames, string[] parameterNames, CodeAttributeDeclarationCollection[] parameterAttributes, string returnTypeName, CodeAttributeDeclarationCollection metadata, CodeFlags flags)
	{
		CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
		codeMemberMethod.Name = methodName;
		codeMemberMethod.ReturnType = new CodeTypeReference(returnTypeName);
		codeMemberMethod.CustomAttributes = metadata;
		if ((flags & CodeFlags.IsPublic) != 0)
		{
			codeMemberMethod.Attributes = (codeMemberMethod.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public;
		}
		if ((flags & CodeFlags.IsAbstract) != 0)
		{
			codeMemberMethod.Attributes = (codeMemberMethod.Attributes & (MemberAttributes)(-16)) | MemberAttributes.Abstract;
		}
		if ((flags & CodeFlags.IsNew) != 0)
		{
			codeMemberMethod.Attributes = (codeMemberMethod.Attributes & (MemberAttributes)(-241)) | MemberAttributes.New;
		}
		for (int i = 0; i < parameterNames.Length; i++)
		{
			CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression(parameterTypeNames[i], parameterNames[i]);
			if ((parameterFlags[i] & CodeFlags.IsByRef) != 0)
			{
				codeParameterDeclarationExpression.Direction = FieldDirection.Ref;
			}
			else if ((parameterFlags[i] & CodeFlags.IsOut) != 0)
			{
				codeParameterDeclarationExpression.Direction = FieldDirection.Out;
			}
			if (i < parameterAttributes.Length)
			{
				codeParameterDeclarationExpression.CustomAttributes = parameterAttributes[i];
			}
			codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
		}
		codeClass.Members.Add(codeMemberMethod);
		return codeMemberMethod;
	}

	internal static CodeTypeDeclaration AddClass(CodeNamespace codeNamespace, string className, string baseClassName, string[] implementedInterfaceNames, CodeAttributeDeclarationCollection metadata, CodeFlags flags, bool isPartial)
	{
		CodeTypeDeclaration codeTypeDeclaration = CreateClass(className, baseClassName, implementedInterfaceNames, metadata, flags, isPartial);
		codeNamespace.Types.Add(codeTypeDeclaration);
		return codeTypeDeclaration;
	}

	internal static CodeTypeDeclaration CreateClass(string className, string baseClassName, string[] implementedInterfaceNames, CodeAttributeDeclarationCollection metadata, CodeFlags flags, bool isPartial)
	{
		CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(className);
		if (baseClassName != null && baseClassName.Length > 0)
		{
			codeTypeDeclaration.BaseTypes.Add(baseClassName);
		}
		foreach (string value in implementedInterfaceNames)
		{
			codeTypeDeclaration.BaseTypes.Add(value);
		}
		codeTypeDeclaration.IsStruct = (flags & CodeFlags.IsStruct) != 0;
		if ((flags & CodeFlags.IsPublic) != 0)
		{
			codeTypeDeclaration.TypeAttributes |= TypeAttributes.Public;
		}
		else
		{
			codeTypeDeclaration.TypeAttributes &= ~TypeAttributes.Public;
		}
		if ((flags & CodeFlags.IsAbstract) != 0)
		{
			codeTypeDeclaration.TypeAttributes |= TypeAttributes.Abstract;
		}
		else
		{
			codeTypeDeclaration.TypeAttributes &= ~TypeAttributes.Abstract;
		}
		if ((flags & CodeFlags.IsInterface) != 0)
		{
			codeTypeDeclaration.IsInterface = true;
		}
		else
		{
			codeTypeDeclaration.IsPartial = isPartial;
		}
		codeTypeDeclaration.CustomAttributes = metadata;
		codeTypeDeclaration.CustomAttributes.Add(GeneratedCodeAttribute);
		return codeTypeDeclaration;
	}

	internal static CodeAttributeDeclarationCollection AddCustomAttribute(CodeAttributeDeclarationCollection metadata, Type type, CodeAttributeArgument[] arguments)
	{
		if (metadata == null)
		{
			metadata = new CodeAttributeDeclarationCollection();
		}
		CodeAttributeDeclaration value = new CodeAttributeDeclaration(type.FullName, arguments);
		metadata.Add(value);
		return metadata;
	}

	internal static CodeAttributeDeclarationCollection AddCustomAttribute(CodeAttributeDeclarationCollection metadata, Type type, CodeExpression[] arguments)
	{
		return AddCustomAttribute(metadata, type, arguments, new string[0], new CodeExpression[0]);
	}

	internal static CodeAttributeDeclarationCollection AddCustomAttribute(CodeAttributeDeclarationCollection metadata, Type type, CodeExpression[] parameters, string[] propNames, CodeExpression[] propValues)
	{
		CodeAttributeArgument[] array = new CodeAttributeArgument[((parameters != null) ? parameters.Length : 0) + ((propNames != null) ? propNames.Length : 0)];
		for (int i = 0; i < parameters.Length; i++)
		{
			array[i] = new CodeAttributeArgument(null, parameters[i]);
		}
		for (int j = 0; j < propNames.Length; j++)
		{
			array[parameters.Length + j] = new CodeAttributeArgument(propNames[j], propValues[j]);
		}
		return AddCustomAttribute(metadata, type, array);
	}

	internal static void AddEvent(CodeTypeMemberCollection members, string handlerType, string handlerName)
	{
		CodeMemberEvent codeMemberEvent = new CodeMemberEvent();
		codeMemberEvent.Type = new CodeTypeReference(handlerType);
		codeMemberEvent.Name = handlerName;
		codeMemberEvent.Attributes = (codeMemberEvent.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public;
		codeMemberEvent.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), docComment: true));
		members.Add(codeMemberEvent);
	}

	internal static void AddDelegate(CodeTypeDeclarationCollection codeClasses, string handlerType, string handlerArgs)
	{
		CodeTypeDelegate codeTypeDelegate = new CodeTypeDelegate(handlerType);
		codeTypeDelegate.CustomAttributes.Add(GeneratedCodeAttribute);
		codeTypeDelegate.Parameters.Add(new CodeParameterDeclarationExpression(typeof(object), "sender"));
		codeTypeDelegate.Parameters.Add(new CodeParameterDeclarationExpression(handlerArgs, "e"));
		codeTypeDelegate.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), docComment: true));
		codeClasses.Add(codeTypeDelegate);
	}

	internal static void AddCallbackDeclaration(CodeTypeMemberCollection members, string callbackMember)
	{
		CodeMemberField codeMemberField = new CodeMemberField();
		codeMemberField.Type = new CodeTypeReference(typeof(SendOrPostCallback));
		codeMemberField.Name = callbackMember;
		members.Add(codeMemberField);
	}

	internal static void AddCallbackImplementation(CodeTypeDeclaration codeClass, string callbackName, string handlerName, string handlerArgs, bool methodHasOutParameters)
	{
		CodeMemberMethod codeMemberMethod = AddMethod(codeClass, callbackName, new CodeFlags[1], new string[1] { typeof(object).FullName }, new string[1] { "arg" }, typeof(void).FullName, null, (CodeFlags)0);
		CodeBinaryOperatorExpression condition = new CodeBinaryOperatorExpression(new CodeEventReferenceExpression(new CodeThisReferenceExpression(), handlerName), CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null));
		CodeStatement[] array = new CodeStatement[2]
		{
			new CodeVariableDeclarationStatement(typeof(InvokeCompletedEventArgs), "invokeArgs", new CodeCastExpression(typeof(InvokeCompletedEventArgs), new CodeArgumentReferenceExpression("arg"))),
			null
		};
		CodeVariableReferenceExpression targetObject = new CodeVariableReferenceExpression("invokeArgs");
		CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression();
		if (methodHasOutParameters)
		{
			codeObjectCreateExpression.CreateType = new CodeTypeReference(handlerArgs);
			codeObjectCreateExpression.Parameters.Add(new CodePropertyReferenceExpression(targetObject, "Results"));
		}
		else
		{
			codeObjectCreateExpression.CreateType = new CodeTypeReference(typeof(AsyncCompletedEventArgs));
		}
		codeObjectCreateExpression.Parameters.Add(new CodePropertyReferenceExpression(targetObject, "Error"));
		codeObjectCreateExpression.Parameters.Add(new CodePropertyReferenceExpression(targetObject, "Cancelled"));
		codeObjectCreateExpression.Parameters.Add(new CodePropertyReferenceExpression(targetObject, "UserState"));
		array[1] = new CodeExpressionStatement(new CodeDelegateInvokeExpression(new CodeEventReferenceExpression(new CodeThisReferenceExpression(), handlerName), new CodeThisReferenceExpression(), codeObjectCreateExpression));
		codeMemberMethod.Statements.Add(new CodeConditionStatement(condition, array, new CodeStatement[0]));
	}

	internal static CodeMemberMethod AddAsyncMethod(CodeTypeDeclaration codeClass, string methodName, string[] parameterTypeNames, string[] parameterNames, string callbackMember, string callbackName, string userState)
	{
		CodeMemberMethod codeMemberMethod = AddMethod(codeClass, methodName, new CodeFlags[parameterNames.Length], parameterTypeNames, parameterNames, typeof(void).FullName, null, CodeFlags.IsPublic);
		codeMemberMethod.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), docComment: true));
		CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), methodName);
		for (int i = 0; i < parameterNames.Length; i++)
		{
			codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression(parameterNames[i]));
		}
		codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(null));
		codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
		codeMemberMethod = AddMethod(codeClass, methodName, new CodeFlags[parameterNames.Length], parameterTypeNames, parameterNames, typeof(void).FullName, null, CodeFlags.IsPublic);
		codeMemberMethod.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), docComment: true));
		codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(object), userState));
		CodeFieldReferenceExpression left = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), callbackMember);
		CodeBinaryOperatorExpression condition = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null));
		CodeDelegateCreateExpression codeDelegateCreateExpression = new CodeDelegateCreateExpression();
		codeDelegateCreateExpression.DelegateType = new CodeTypeReference(typeof(SendOrPostCallback));
		codeDelegateCreateExpression.TargetObject = new CodeThisReferenceExpression();
		codeDelegateCreateExpression.MethodName = callbackName;
		CodeStatement[] trueStatements = new CodeStatement[1]
		{
			new CodeAssignStatement(left, codeDelegateCreateExpression)
		};
		codeMemberMethod.Statements.Add(new CodeConditionStatement(condition, trueStatements, new CodeStatement[0]));
		return codeMemberMethod;
	}

	internal static CodeTypeDeclaration CreateArgsClass(string name, string[] paramTypes, string[] paramNames, bool isPartial)
	{
		CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(name);
		codeTypeDeclaration.CustomAttributes.Add(GeneratedCodeAttribute);
		codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(typeof(DebuggerStepThroughAttribute).FullName));
		codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(typeof(DesignerCategoryAttribute).FullName, new CodeAttributeArgument(new CodePrimitiveExpression("code"))));
		codeTypeDeclaration.IsPartial = isPartial;
		codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(typeof(AsyncCompletedEventArgs)));
		CodeIdentifiers codeIdentifiers = new CodeIdentifiers();
		codeIdentifiers.AddUnique("Error", "Error");
		codeIdentifiers.AddUnique("Cancelled", "Cancelled");
		codeIdentifiers.AddUnique("UserState", "UserState");
		for (int i = 0; i < paramNames.Length; i++)
		{
			if (paramNames[i] != null)
			{
				codeIdentifiers.AddUnique(paramNames[i], paramNames[i]);
			}
		}
		string text = codeIdentifiers.AddUnique("results", "results");
		CodeMemberField codeMemberField = new CodeMemberField(typeof(object[]), text);
		codeTypeDeclaration.Members.Add(codeMemberField);
		CodeConstructor codeConstructor = new CodeConstructor();
		codeConstructor.Attributes = (codeConstructor.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Assembly;
		CodeParameterDeclarationExpression value = new CodeParameterDeclarationExpression(typeof(object[]), text);
		codeConstructor.Parameters.Add(value);
		codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(Exception), "exception"));
		codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(bool), "cancelled"));
		codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(object), "userState"));
		codeConstructor.BaseConstructorArgs.Add(new CodeArgumentReferenceExpression("exception"));
		codeConstructor.BaseConstructorArgs.Add(new CodeArgumentReferenceExpression("cancelled"));
		codeConstructor.BaseConstructorArgs.Add(new CodeArgumentReferenceExpression("userState"));
		codeConstructor.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), codeMemberField.Name), new CodeArgumentReferenceExpression(text)));
		codeTypeDeclaration.Members.Add(codeConstructor);
		int num = 0;
		for (int j = 0; j < paramNames.Length; j++)
		{
			if (paramNames[j] != null)
			{
				codeTypeDeclaration.Members.Add(CreatePropertyDeclaration(codeMemberField, paramNames[j], paramTypes[j], num++));
			}
		}
		codeTypeDeclaration.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), docComment: true));
		return codeTypeDeclaration;
	}

	private static CodeMemberProperty CreatePropertyDeclaration(CodeMemberField field, string name, string typeName, int index)
	{
		CodeMemberProperty obj = new CodeMemberProperty
		{
			Type = new CodeTypeReference(typeName),
			Name = name
		};
		obj.Attributes = (obj.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public;
		obj.GetStatements.Add(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "RaiseExceptionIfNecessary"));
		CodeArrayIndexerExpression expression = new CodeArrayIndexerExpression
		{
			TargetObject = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name),
			Indices = { (CodeExpression)new CodePrimitiveExpression(index) }
		};
		CodeMethodReturnStatement value = new CodeMethodReturnStatement
		{
			Expression = new CodeCastExpression(typeName, expression)
		};
		obj.GetStatements.Add(value);
		obj.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), docComment: true));
		return obj;
	}
}
