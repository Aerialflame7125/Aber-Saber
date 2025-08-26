using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;
using System.Web.SessionState;
using System.Web.UI;

namespace System.Web.Compilation;

internal class PageCompiler : TemplateControlCompiler
{
	private PageParser pageParser;

	private static CodeTypeReference intRef = new CodeTypeReference(typeof(int));

	public PageCompiler(PageParser pageParser)
		: base(pageParser)
	{
		this.pageParser = pageParser;
	}

	protected override void CreateStaticFields()
	{
		base.CreateStaticFields();
		CodeMemberField codeMemberField = new CodeMemberField(typeof(object), "__fileDependencies");
		codeMemberField.Attributes = (MemberAttributes)20483;
		codeMemberField.InitExpression = new CodePrimitiveExpression(null);
		mainClass.Members.Add(codeMemberField);
		if (pageParser.OutputCache)
		{
			codeMemberField = new CodeMemberField(typeof(OutputCacheParameters), "__outputCacheSettings");
			codeMemberField.Attributes = (MemberAttributes)20483;
			codeMemberField.InitExpression = new CodePrimitiveExpression(null);
			mainClass.Members.Add(codeMemberField);
		}
	}

	protected override void CreateConstructor(CodeStatementCollection localVars, CodeStatementCollection trueStmt)
	{
		MainDirectiveAttribute<string> masterPageFile = pageParser.MasterPageFile;
		if (masterPageFile != null && !masterPageFile.IsExpression)
		{
			BuildManager.GetCompiledType(masterPageFile.Value);
		}
		MainDirectiveAttribute<string> clientTarget = pageParser.ClientTarget;
		if (clientTarget != null)
		{
			CodeExpression left = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "ClientTarget");
			CodeExpression codeExpression = null;
			if (clientTarget.IsExpression)
			{
				PropertyInfo propertyInfo = TemplateControlCompiler.GetFieldOrProperty(typeof(Page), "ClientTarget") as PropertyInfo;
				if (propertyInfo != null)
				{
					codeExpression = CompileExpression(propertyInfo, propertyInfo.PropertyType, clientTarget.UnparsedValue, useSetAttribute: false);
				}
			}
			if (codeExpression == null)
			{
				codeExpression = new CodePrimitiveExpression(clientTarget.Value);
			}
			if (localVars == null)
			{
				localVars = new CodeStatementCollection();
			}
			localVars.Add(new CodeAssignStatement(left, codeExpression));
		}
		List<string> dependencies = pageParser.Dependencies;
		int num = dependencies?.Count ?? 0;
		if (num > 0)
		{
			if (localVars == null)
			{
				localVars = new CodeStatementCollection();
			}
			if (trueStmt == null)
			{
				trueStmt = new CodeStatementCollection();
			}
			localVars.Add(new CodeVariableDeclarationStatement(typeof(string[]), "dependencies"));
			CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("dependencies");
			trueStmt.Add(new CodeAssignStatement(codeVariableReferenceExpression, new CodeArrayCreateExpression(typeof(string), num)));
			CodeAssignStatement value2;
			for (int i = 0; i < num; i++)
			{
				object value = dependencies[i];
				value2 = new CodeAssignStatement(new CodeArrayIndexerExpression(codeVariableReferenceExpression, new CodePrimitiveExpression(i)), new CodePrimitiveExpression(value));
				trueStmt.Add(value2);
			}
			CodeMethodInvokeExpression right = new CodeMethodInvokeExpression(BaseCompiler.thisRef, "GetWrappedFileDependencies", codeVariableReferenceExpression);
			value2 = new CodeAssignStatement(GetMainClassFieldReferenceExpression("__fileDependencies"), right);
			trueStmt.Add(value2);
		}
		base.CreateConstructor(localVars, trueStmt);
	}

	protected override void AddInterfaces()
	{
		base.AddInterfaces();
		if (pageParser.EnableSessionState)
		{
			CodeTypeReference value = new CodeTypeReference(typeof(IRequiresSessionState));
			if (partialClass != null)
			{
				partialClass.BaseTypes.Add(value);
			}
			else
			{
				mainClass.BaseTypes.Add(value);
			}
		}
		if (pageParser.ReadOnlySessionState)
		{
			CodeTypeReference value = new CodeTypeReference(typeof(IReadOnlySessionState));
			if (partialClass != null)
			{
				partialClass.BaseTypes.Add(value);
			}
			else
			{
				mainClass.BaseTypes.Add(value);
			}
		}
		if (pageParser.Async)
		{
			mainClass.BaseTypes.Add(new CodeTypeReference(typeof(IHttpAsyncHandler)));
		}
		mainClass.BaseTypes.Add(new CodeTypeReference(typeof(IHttpHandler)));
	}

	private void CreateGetTypeHashCode()
	{
		CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
		codeMemberMethod.ReturnType = intRef;
		codeMemberMethod.Name = "GetTypeHashCode";
		codeMemberMethod.Attributes = (MemberAttributes)24580;
		Random random = new Random(pageParser.InputFile.GetHashCode());
		codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(random.Next())));
		mainClass.Members.Add(codeMemberMethod);
	}

	private static CodeExpression GetExpressionForValueAndType(object value, Type valueType)
	{
		if (valueType == typeof(TimeSpan))
		{
			return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(TimeSpan)), "Parse"), new CodePrimitiveExpression(((TimeSpan)value/*cast due to .constrained prefix*/).ToString()));
		}
		throw new HttpException($"Unable to create assign expression for type '{valueType}'.");
	}

	private static CodeAssignStatement CreatePropertyAssign(CodeExpression owner, string name, CodeExpression rhs)
	{
		return new CodeAssignStatement(new CodePropertyReferenceExpression(owner, name), rhs);
	}

	private static CodeAssignStatement CreatePropertyAssign(CodeExpression owner, string name, object value)
	{
		CodeExpression rhs;
		if (value == null || value is string)
		{
			rhs = new CodePrimitiveExpression(value);
		}
		else
		{
			Type type = value.GetType();
			rhs = ((!type.IsPrimitive) ? GetExpressionForValueAndType(value, type) : new CodePrimitiveExpression(value));
		}
		return CreatePropertyAssign(owner, name, rhs);
	}

	private static CodeAssignStatement CreatePropertyAssign(string name, object value)
	{
		return CreatePropertyAssign(BaseCompiler.thisRef, name, value);
	}

	private void AssignPropertyWithExpression<T>(CodeMemberMethod method, string name, MainDirectiveAttribute<T> value, ILocation location)
	{
		if (value == null)
		{
			return;
		}
		CodeExpression codeExpression = null;
		if (value.IsExpression)
		{
			PropertyInfo propertyInfo = TemplateControlCompiler.GetFieldOrProperty(typeof(Page), name) as PropertyInfo;
			if (propertyInfo != null)
			{
				codeExpression = CompileExpression(propertyInfo, propertyInfo.PropertyType, value.UnparsedValue, useSetAttribute: false);
			}
		}
		CodeAssignStatement statement = ((codeExpression == null) ? CreatePropertyAssign(name, value.Value) : CreatePropertyAssign(BaseCompiler.thisRef, name, codeExpression));
		method.Statements.Add(AddLinePragma(statement, location));
	}

	private void AddStatementsFromDirective(ControlBuilder builder, CodeMemberMethod method, ILocation location)
	{
		AssignPropertyWithExpression(method, "ResponseEncoding", pageParser.ResponseEncoding, location);
		AssignPropertyWithExpression(method, "CodePage", pageParser.CodePage, location);
		AssignPropertyWithExpression(method, "LCID", pageParser.LCID, location);
		string contentType = pageParser.ContentType;
		if (contentType != null)
		{
			method.Statements.Add(AddLinePragma(CreatePropertyAssign("ContentType", contentType), location));
		}
		string culture = pageParser.Culture;
		if (culture != null)
		{
			method.Statements.Add(AddLinePragma(CreatePropertyAssign("Culture", culture), location));
		}
		culture = pageParser.UICulture;
		if (culture != null)
		{
			method.Statements.Add(AddLinePragma(CreatePropertyAssign("UICulture", culture), location));
		}
		string errorPage = pageParser.ErrorPage;
		if (errorPage != null)
		{
			method.Statements.Add(AddLinePragma(CreatePropertyAssign("ErrorPage", errorPage), location));
		}
		if (pageParser.HaveTrace)
		{
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "TraceEnabled");
			codeAssignStatement.Right = new CodePrimitiveExpression(pageParser.Trace);
			method.Statements.Add(AddLinePragma(codeAssignStatement, location));
		}
		if (pageParser.TraceMode != TraceMode.Default)
		{
			CodeAssignStatement codeAssignStatement2 = new CodeAssignStatement();
			CodeTypeReferenceExpression targetObject = new CodeTypeReferenceExpression("System.Web.TraceMode");
			codeAssignStatement2.Left = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "TraceModeValue");
			codeAssignStatement2.Right = new CodeFieldReferenceExpression(targetObject, pageParser.TraceMode.ToString());
			method.Statements.Add(AddLinePragma(codeAssignStatement2, location));
		}
		if (pageParser.NotBuffer)
		{
			CodeAssignStatement codeAssignStatement3 = new CodeAssignStatement();
			codeAssignStatement3.Left = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "Buffer");
			codeAssignStatement3.Right = new CodePrimitiveExpression(false);
			method.Statements.Add(AddLinePragma(codeAssignStatement3, location));
		}
		if (!pageParser.EnableEventValidation)
		{
			CodeAssignStatement codeAssignStatement4 = new CodeAssignStatement();
			CodePropertyReferenceExpression left = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "EnableEventValidation");
			codeAssignStatement4.Left = left;
			codeAssignStatement4.Right = new CodePrimitiveExpression(pageParser.EnableEventValidation);
			method.Statements.Add(AddLinePragma(codeAssignStatement4, location));
		}
		if (pageParser.MaintainScrollPositionOnPostBack)
		{
			CodeAssignStatement codeAssignStatement5 = new CodeAssignStatement();
			CodePropertyReferenceExpression left2 = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "MaintainScrollPositionOnPostBack");
			codeAssignStatement5.Left = left2;
			codeAssignStatement5.Right = new CodePrimitiveExpression(pageParser.MaintainScrollPositionOnPostBack);
			method.Statements.Add(AddLinePragma(codeAssignStatement5, location));
		}
	}

	protected override void AddStatementsToConstructor(CodeConstructor ctor)
	{
		base.AddStatementsToConstructor(ctor);
		if (pageParser.OutputCache)
		{
			OutputCacheParamsBlock(ctor);
		}
	}

	protected override void AddStatementsToInitMethodTop(ControlBuilder builder, CodeMemberMethod method)
	{
		base.AddStatementsToInitMethodTop(builder, method);
		ILocation directiveLocation = pageParser.DirectiveLocation;
		AddStatementsFromDirective(builder, method, directiveLocation);
		CodeArgumentReferenceExpression owner = new CodeArgumentReferenceExpression("__ctrl");
		if (pageParser.EnableViewStateMacSet)
		{
			method.Statements.Add(AddLinePragma(CreatePropertyAssign(owner, "EnableViewStateMac", pageParser.EnableViewStateMacSet), directiveLocation));
		}
		AssignPropertyWithExpression(method, "Title", pageParser.Title, directiveLocation);
		AssignPropertyWithExpression(method, "MasterPageFile", pageParser.MasterPageFile, directiveLocation);
		AssignPropertyWithExpression(method, "Theme", pageParser.Theme, directiveLocation);
		if (pageParser.StyleSheetTheme != null)
		{
			method.Statements.Add(AddLinePragma(CreatePropertyAssign(owner, "StyleSheetTheme", pageParser.StyleSheetTheme), directiveLocation));
		}
		if (pageParser.Async)
		{
			method.Statements.Add(AddLinePragma(CreatePropertyAssign(owner, "AsyncMode", pageParser.Async), directiveLocation));
		}
		if (pageParser.AsyncTimeout != -1)
		{
			method.Statements.Add(AddLinePragma(CreatePropertyAssign(owner, "AsyncTimeout", TimeSpan.FromSeconds(pageParser.AsyncTimeout)), directiveLocation));
		}
		CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(BaseCompiler.thisRef, "InitializeCulture");
		method.Statements.Add(AddLinePragma(new CodeExpressionStatement(expression), directiveLocation));
	}

	protected override void AddStatementsToInitMethodBottom(ControlBuilder builder, CodeMemberMethod method)
	{
		ILocation directiveLocation = pageParser.DirectiveLocation;
		AssignPropertyWithExpression(method, "MetaDescription", pageParser.MetaDescription, directiveLocation);
		AssignPropertyWithExpression(method, "MetaKeywords", pageParser.MetaKeywords, directiveLocation);
	}

	protected override void PrependStatementsToFrameworkInitialize(CodeMemberMethod method)
	{
		base.PrependStatementsToFrameworkInitialize(method);
		if (pageParser.StyleSheetTheme != null)
		{
			method.Statements.Add(CreatePropertyAssign("StyleSheetTheme", pageParser.StyleSheetTheme));
		}
	}

	protected override void AppendStatementsToFrameworkInitialize(CodeMemberMethod method)
	{
		base.AppendStatementsToFrameworkInitialize(method);
		if ((pageParser.Dependencies?.Count ?? 0) > 0)
		{
			CodeFieldReferenceExpression mainClassFieldReferenceExpression = GetMainClassFieldReferenceExpression("__fileDependencies");
			method.Statements.Add(new CodeMethodInvokeExpression(BaseCompiler.thisRef, "AddWrappedFileDependencies", mainClassFieldReferenceExpression));
		}
		if (pageParser.OutputCache)
		{
			CodeMethodInvokeExpression value = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(BaseCompiler.thisRef, "InitOutputCache"), GetMainClassFieldReferenceExpression("__outputCacheSettings"));
			method.Statements.Add(value);
		}
		if (pageParser.ValidateRequest)
		{
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			CodePropertyReferenceExpression targetObject = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "Request");
			codeMethodInvokeExpression.Method = new CodeMethodReferenceExpression(targetObject, "ValidateInput");
			method.Statements.Add(codeMethodInvokeExpression);
		}
	}

	private CodeAssignStatement AssignOutputCacheParameter(CodeVariableReferenceExpression variable, string propName, object value)
	{
		CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
		codeAssignStatement.Left = new CodeFieldReferenceExpression(variable, propName);
		if (value is OutputCacheLocation)
		{
			codeAssignStatement.Right = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(new CodeTypeReference(typeof(OutputCacheLocation), CodeTypeReferenceOptions.GlobalReference)), value.ToString());
		}
		else
		{
			codeAssignStatement.Right = new CodePrimitiveExpression(value);
		}
		return codeAssignStatement;
	}

	private void OutputCacheParamsBlock(CodeMemberMethod method)
	{
		List<CodeStatement> list = new List<CodeStatement>();
		CodeVariableDeclarationStatement item = new CodeVariableDeclarationStatement(typeof(OutputCacheParameters), "outputCacheSettings");
		CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("outputCacheSettings");
		list.Add(item);
		list.Add(new CodeAssignStatement(codeVariableReferenceExpression, new CodeObjectCreateExpression(typeof(OutputCacheParameters))));
		TemplateParser.OutputCacheParsedParams outputCacheParsedParameters = pageParser.OutputCacheParsedParameters;
		if ((outputCacheParsedParameters & TemplateParser.OutputCacheParsedParams.CacheProfile) != 0)
		{
			list.Add(AssignOutputCacheParameter(codeVariableReferenceExpression, "CacheProfile", pageParser.OutputCacheCacheProfile));
		}
		list.Add(AssignOutputCacheParameter(codeVariableReferenceExpression, "Duration", pageParser.OutputCacheDuration));
		if ((outputCacheParsedParameters & TemplateParser.OutputCacheParsedParams.Location) != 0)
		{
			list.Add(AssignOutputCacheParameter(codeVariableReferenceExpression, "Location", pageParser.OutputCacheLocation));
		}
		if ((outputCacheParsedParameters & TemplateParser.OutputCacheParsedParams.NoStore) != 0)
		{
			list.Add(AssignOutputCacheParameter(codeVariableReferenceExpression, "NoStore", pageParser.OutputCacheNoStore));
		}
		if ((outputCacheParsedParameters & TemplateParser.OutputCacheParsedParams.SqlDependency) != 0)
		{
			list.Add(AssignOutputCacheParameter(codeVariableReferenceExpression, "SqlDependency", pageParser.OutputCacheSqlDependency));
		}
		if ((outputCacheParsedParameters & TemplateParser.OutputCacheParsedParams.VaryByContentEncodings) != 0)
		{
			list.Add(AssignOutputCacheParameter(codeVariableReferenceExpression, "VaryByContentEncoding", pageParser.OutputCacheVaryByContentEncodings));
		}
		if ((outputCacheParsedParameters & TemplateParser.OutputCacheParsedParams.VaryByControl) != 0)
		{
			list.Add(AssignOutputCacheParameter(codeVariableReferenceExpression, "VaryByControl", pageParser.OutputCacheVaryByControls));
		}
		if ((outputCacheParsedParameters & TemplateParser.OutputCacheParsedParams.VaryByCustom) != 0)
		{
			list.Add(AssignOutputCacheParameter(codeVariableReferenceExpression, "VaryByCustom", pageParser.OutputCacheVaryByCustom));
		}
		if ((outputCacheParsedParameters & TemplateParser.OutputCacheParsedParams.VaryByHeader) != 0)
		{
			list.Add(AssignOutputCacheParameter(codeVariableReferenceExpression, "VaryByHeader", pageParser.OutputCacheVaryByHeader));
		}
		list.Add(AssignOutputCacheParameter(codeVariableReferenceExpression, "VaryByParam", pageParser.OutputCacheVaryByParam));
		CodeFieldReferenceExpression mainClassFieldReferenceExpression = GetMainClassFieldReferenceExpression("__outputCacheSettings");
		list.Add(new CodeAssignStatement(mainClassFieldReferenceExpression, codeVariableReferenceExpression));
		CodeConditionStatement value = new CodeConditionStatement(new CodeBinaryOperatorExpression(mainClassFieldReferenceExpression, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null)), list.ToArray());
		method.Statements.Add(value);
	}

	private void CreateStronglyTypedProperty(Type type, string name)
	{
		if (!(type == null))
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Name = name;
			codeMemberProperty.Type = new CodeTypeReference(type);
			codeMemberProperty.Attributes = (MemberAttributes)24592;
			CodeExpression expression = new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), name);
			expression = new CodeCastExpression(type, expression);
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(expression));
			if (partialClass != null)
			{
				partialClass.Members.Add(codeMemberProperty);
			}
			else
			{
				mainClass.Members.Add(codeMemberProperty);
			}
			AddReferencedAssembly(type.Assembly);
		}
	}

	protected internal override void CreateMethods()
	{
		base.CreateMethods();
		CreateProfileProperty();
		CreateStronglyTypedProperty(pageParser.MasterType, "Master");
		CreateStronglyTypedProperty(pageParser.PreviousPageType, "PreviousPage");
		CreateGetTypeHashCode();
		if (pageParser.Async)
		{
			CreateAsyncMethods();
		}
	}

	private void CreateAsyncMethods()
	{
		CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
		codeMemberMethod.ReturnType = new CodeTypeReference(typeof(IAsyncResult));
		codeMemberMethod.Name = "BeginProcessRequest";
		codeMemberMethod.Attributes = MemberAttributes.Public;
		CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression();
		codeParameterDeclarationExpression.Type = new CodeTypeReference(typeof(HttpContext));
		codeParameterDeclarationExpression.Name = "context";
		codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
		codeParameterDeclarationExpression = new CodeParameterDeclarationExpression();
		codeParameterDeclarationExpression.Type = new CodeTypeReference(typeof(AsyncCallback));
		codeParameterDeclarationExpression.Name = "cb";
		codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
		codeParameterDeclarationExpression = new CodeParameterDeclarationExpression();
		codeParameterDeclarationExpression.Type = new CodeTypeReference(typeof(object));
		codeParameterDeclarationExpression.Name = "data";
		codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
		CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(BaseCompiler.thisRef, "AsyncPageBeginProcessRequest");
		codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("context"));
		codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("cb"));
		codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("data"));
		codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(codeMethodInvokeExpression));
		mainClass.Members.Add(codeMemberMethod);
		codeMemberMethod = new CodeMemberMethod();
		codeMemberMethod.ReturnType = new CodeTypeReference(typeof(void));
		codeMemberMethod.Name = "EndProcessRequest";
		codeMemberMethod.Attributes = MemberAttributes.Public;
		codeParameterDeclarationExpression = new CodeParameterDeclarationExpression();
		codeParameterDeclarationExpression.Type = new CodeTypeReference(typeof(IAsyncResult));
		codeParameterDeclarationExpression.Name = "ar";
		codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
		codeMethodInvokeExpression = new CodeMethodInvokeExpression(BaseCompiler.thisRef, "AsyncPageEndProcessRequest");
		codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("ar"));
		codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
		mainClass.Members.Add(codeMemberMethod);
		codeMemberMethod = new CodeMemberMethod();
		codeMemberMethod.ReturnType = new CodeTypeReference(typeof(void));
		codeMemberMethod.Name = "ProcessRequest";
		codeMemberMethod.Attributes = (MemberAttributes)24580;
		codeParameterDeclarationExpression = new CodeParameterDeclarationExpression();
		codeParameterDeclarationExpression.Type = new CodeTypeReference(typeof(HttpContext));
		codeParameterDeclarationExpression.Name = "context";
		codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
		codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(), "ProcessRequest");
		codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("context"));
		codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
		mainClass.Members.Add(codeMemberMethod);
	}

	public static Type CompilePageType(PageParser pageParser)
	{
		return new PageCompiler(pageParser).GetCompiledType();
	}
}
