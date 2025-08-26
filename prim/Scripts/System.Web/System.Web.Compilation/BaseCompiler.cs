using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Web.Configuration;
using System.Web.UI;

namespace System.Web.Compilation;

internal abstract class BaseCompiler
{
	private const string DEFAULT_NAMESPACE = "ASP";

	internal static Guid HashMD5 = new Guid(1080993376, 25807, 19586, 182, 240, 66, 212, 129, 114, 167, 153);

	private static BindingFlags replaceableFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

	private TemplateParser parser;

	private CodeDomProvider provider;

	private ICodeCompiler compiler;

	private CodeCompileUnit unit;

	private CodeNamespace mainNS;

	private CompilerParameters compilerParameters;

	private bool isRebuilding;

	protected Hashtable partialNameOverride = new Hashtable();

	protected CodeTypeDeclaration partialClass;

	protected CodeTypeReferenceExpression partialClassExpr;

	protected CodeTypeDeclaration mainClass;

	protected CodeTypeReferenceExpression mainClassExpr;

	protected static CodeThisReferenceExpression thisRef = new CodeThisReferenceExpression();

	private VirtualPath inputVirtualPath;

	public VirtualPath InputVirtualPath
	{
		get
		{
			if (inputVirtualPath == null)
			{
				inputVirtualPath = new VirtualPath(VirtualPathUtility.Combine(parser.BaseVirtualDir, Path.GetFileName(parser.InputFile)));
			}
			return inputVirtualPath;
		}
	}

	internal string MainClassType
	{
		get
		{
			if (mainClassExpr == null)
			{
				return null;
			}
			return mainClassExpr.Type.BaseType;
		}
	}

	internal bool IsRebuildingPartial => isRebuilding;

	internal CodeDomProvider Provider
	{
		get
		{
			return provider;
		}
		set
		{
			provider = value;
		}
	}

	internal ICodeCompiler Compiler
	{
		get
		{
			return compiler;
		}
		set
		{
			compiler = value;
		}
	}

	internal CompilerParameters CompilerParameters
	{
		get
		{
			if (compilerParameters == null)
			{
				compilerParameters = new CompilerParameters();
			}
			return compilerParameters;
		}
		set
		{
			compilerParameters = value;
		}
	}

	internal CodeCompileUnit CompileUnit => unit;

	internal CodeTypeDeclaration DerivedType => mainClass;

	internal CodeTypeDeclaration BaseType
	{
		get
		{
			if (partialClass == null)
			{
				return DerivedType;
			}
			return partialClass;
		}
	}

	internal TemplateParser Parser => parser;

	protected BaseCompiler(TemplateParser parser)
	{
		this.parser = parser;
	}

	protected void AddReferencedAssembly(Assembly asm)
	{
		if (unit != null && !(asm == null))
		{
			StringCollection referencedAssemblies = unit.ReferencedAssemblies;
			string location = asm.Location;
			if (!referencedAssemblies.Contains(location))
			{
				referencedAssemblies.Add(location);
			}
		}
	}

	internal CodeStatement AddLinePragma(CodeExpression expression, ControlBuilder builder)
	{
		return AddLinePragma(new CodeExpressionStatement(expression), builder);
	}

	internal CodeStatement AddLinePragma(CodeStatement statement, ControlBuilder builder)
	{
		if (builder == null || statement == null)
		{
			return statement;
		}
		ILocation location = null;
		if (!(builder is CodeRenderBuilder))
		{
			location = builder.Location;
		}
		if (location != null)
		{
			return AddLinePragma(statement, location);
		}
		return AddLinePragma(statement, builder.Line, builder.FileName);
	}

	internal CodeStatement AddLinePragma(CodeStatement statement, ILocation location)
	{
		if (location == null || statement == null)
		{
			return statement;
		}
		return AddLinePragma(statement, location.BeginLine, location.Filename);
	}

	private bool IgnoreFile(string fileName)
	{
		if (parser != null && !parser.LinePragmasOn)
		{
			return true;
		}
		return string.Compare(fileName, "@@inner_string@@", StringComparison.OrdinalIgnoreCase) == 0;
	}

	internal CodeStatement AddLinePragma(CodeStatement statement, int line, string fileName)
	{
		if (statement == null || IgnoreFile(fileName))
		{
			return statement;
		}
		statement.LinePragma = new CodeLinePragma(fileName, line);
		return statement;
	}

	internal CodeTypeMember AddLinePragma(CodeTypeMember member, ControlBuilder builder)
	{
		if (builder == null || member == null)
		{
			return member;
		}
		ILocation location = builder.Location;
		if (location != null)
		{
			return AddLinePragma(member, location);
		}
		return AddLinePragma(member, builder.Line, builder.FileName);
	}

	internal CodeTypeMember AddLinePragma(CodeTypeMember member, ILocation location)
	{
		if (location == null || member == null)
		{
			return member;
		}
		return AddLinePragma(member, location.BeginLine, location.Filename);
	}

	internal CodeTypeMember AddLinePragma(CodeTypeMember member, int line, string fileName)
	{
		if (member == null || IgnoreFile(fileName))
		{
			return member;
		}
		member.LinePragma = new CodeLinePragma(fileName, line);
		return member;
	}

	internal void ConstructType()
	{
		unit = new CodeCompileUnit();
		byte[] mD5Checksum = parser.MD5Checksum;
		if (mD5Checksum != null)
		{
			CodeChecksumPragma codeChecksumPragma = new CodeChecksumPragma();
			codeChecksumPragma.FileName = parser.InputFile;
			codeChecksumPragma.ChecksumAlgorithmId = HashMD5;
			codeChecksumPragma.ChecksumData = mD5Checksum;
			unit.StartDirectives.Add(codeChecksumPragma);
		}
		if (parser.IsPartial)
		{
			string name = null;
			string text = parser.PartialClassName;
			int num = text.LastIndexOf('.');
			if (num != -1)
			{
				name = text.Substring(0, num);
				text = text.Substring(num + 1);
			}
			CodeNamespace codeNamespace = new CodeNamespace(name);
			partialClass = new CodeTypeDeclaration(text);
			partialClass.IsPartial = true;
			partialClassExpr = new CodeTypeReferenceExpression(parser.PartialClassName);
			unit.Namespaces.Add(codeNamespace);
			partialClass.TypeAttributes = TypeAttributes.Public;
			codeNamespace.Types.Add(partialClass);
		}
		string text2 = parser.ClassName;
		string text3 = "ASP";
		int num2 = text2.LastIndexOf('.');
		if (num2 != -1)
		{
			text3 = text2.Substring(0, num2);
			text2 = text2.Substring(num2 + 1);
		}
		mainNS = new CodeNamespace(text3);
		mainClass = new CodeTypeDeclaration(text2);
		CodeTypeReference codeTypeReference;
		if (partialClass != null)
		{
			codeTypeReference = new CodeTypeReference(parser.PartialClassName);
			codeTypeReference.Options |= CodeTypeReferenceOptions.GlobalReference;
		}
		else
		{
			codeTypeReference = new CodeTypeReference(parser.BaseType.FullName);
			if (parser.BaseTypeIsGlobal)
			{
				codeTypeReference.Options |= CodeTypeReferenceOptions.GlobalReference;
			}
		}
		mainClass.BaseTypes.Add(codeTypeReference);
		mainClassExpr = new CodeTypeReferenceExpression(text3 + "." + text2);
		unit.Namespaces.Add(mainNS);
		mainClass.TypeAttributes = TypeAttributes.Public;
		mainNS.Types.Add(mainClass);
		foreach (string key in parser.Imports.Keys)
		{
			if (key is string)
			{
				mainNS.Imports.Add(new CodeNamespaceImport(key));
			}
		}
		StringCollection referencedAssemblies = unit.ReferencedAssemblies;
		if (parser.Assemblies != null)
		{
			foreach (string assembly2 in parser.Assemblies)
			{
				if (assembly2 is string value && !referencedAssemblies.Contains(value))
				{
					referencedAssemblies.Add(value);
				}
			}
		}
		ArrayList extraAssemblies = WebConfigurationManager.ExtraAssemblies;
		if (extraAssemblies != null && extraAssemblies.Count > 0)
		{
			foreach (object item in extraAssemblies)
			{
				if (item is string value2 && !referencedAssemblies.Contains(value2))
				{
					referencedAssemblies.Add(value2);
				}
			}
		}
		IList codeAssemblies = BuildManager.CodeAssemblies;
		if (codeAssemblies != null && codeAssemblies.Count > 0)
		{
			foreach (object item2 in codeAssemblies)
			{
				Assembly assembly = item2 as Assembly;
				if (item2 != null)
				{
					string location = assembly.Location;
					if (location != null && !referencedAssemblies.Contains(location))
					{
						referencedAssemblies.Add(location);
					}
				}
			}
		}
		unit.UserData["RequireVariableDeclaration"] = parser.ExplicitOn;
		unit.UserData["AllowLateBound"] = !parser.StrictOn;
		InitializeType();
		AddInterfaces();
		AddClassAttributes();
		CreateStaticFields();
		AddApplicationAndSessionObjects();
		AddScripts();
		CreateMethods();
		CreateConstructor(null, null);
	}

	internal CodeFieldReferenceExpression GetMainClassFieldReferenceExpression(string fieldName)
	{
		CodeTypeReference codeTypeReference = new CodeTypeReference(mainNS.Name + "." + mainClass.Name);
		codeTypeReference.Options |= CodeTypeReferenceOptions.GlobalReference;
		return new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(codeTypeReference), fieldName);
	}

	protected virtual void InitializeType()
	{
	}

	protected virtual void CreateStaticFields()
	{
		CodeMemberField codeMemberField = new CodeMemberField(typeof(bool), "__initialized");
		codeMemberField.Attributes = (MemberAttributes)20483;
		codeMemberField.InitExpression = new CodePrimitiveExpression(false);
		mainClass.Members.Add(codeMemberField);
	}

	private void AssignAppRelativeVirtualPath(CodeConstructor ctor)
	{
		if (string.IsNullOrEmpty(parser.InputFile))
		{
			return;
		}
		Type type = parser.CodeFileBaseClassType;
		if (type == null)
		{
			type = parser.BaseType;
		}
		if (!(type == null) && type.IsSubclassOf(typeof(TemplateControl)))
		{
			CodeTypeReference codeTypeReference = new CodeTypeReference(type.FullName);
			if (parser.BaseTypeIsGlobal)
			{
				codeTypeReference.Options |= CodeTypeReferenceOptions.GlobalReference;
			}
			CodePropertyReferenceExpression left = new CodePropertyReferenceExpression(new CodeCastExpression(codeTypeReference, new CodeThisReferenceExpression()), "AppRelativeVirtualPath");
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = left;
			codeAssignStatement.Right = new CodePrimitiveExpression(VirtualPathUtility.RemoveTrailingSlash(InputVirtualPath.AppRelative));
			ctor.Statements.Add(codeAssignStatement);
		}
	}

	protected virtual void CreateConstructor(CodeStatementCollection localVars, CodeStatementCollection trueStmt)
	{
		CodeConstructor codeConstructor = new CodeConstructor();
		codeConstructor.Attributes = MemberAttributes.Public;
		mainClass.Members.Add(codeConstructor);
		if (localVars != null)
		{
			codeConstructor.Statements.AddRange(localVars);
		}
		AssignAppRelativeVirtualPath(codeConstructor);
		CodeFieldReferenceExpression mainClassFieldReferenceExpression = GetMainClassFieldReferenceExpression("__initialized");
		CodeBinaryOperatorExpression condition = new CodeBinaryOperatorExpression(mainClassFieldReferenceExpression, CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(false));
		CodeAssignStatement value = new CodeAssignStatement(mainClassFieldReferenceExpression, new CodePrimitiveExpression(true));
		CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
		codeConditionStatement.Condition = condition;
		if (trueStmt != null)
		{
			codeConditionStatement.TrueStatements.AddRange(trueStmt);
		}
		codeConditionStatement.TrueStatements.Add(value);
		codeConstructor.Statements.Add(codeConditionStatement);
		AddStatementsToConstructor(codeConstructor);
	}

	protected virtual void AddStatementsToConstructor(CodeConstructor ctor)
	{
	}

	private void AddScripts()
	{
		if (parser.Scripts == null || parser.Scripts.Count == 0)
		{
			return;
		}
		foreach (ServerSideScript script in parser.Scripts)
		{
			if (script is ServerSideScript serverSideScript)
			{
				mainClass.Members.Add(AddLinePragma(new CodeSnippetTypeMember(serverSideScript.Script), serverSideScript.Location));
			}
		}
	}

	protected internal virtual void CreateMethods()
	{
	}

	private void InternalCreatePageProperty(string retType, string name, string contextProperty)
	{
		CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
		codeMemberProperty.Name = name;
		codeMemberProperty.Type = new CodeTypeReference(retType);
		codeMemberProperty.Attributes = (MemberAttributes)12290;
		CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
		CodeCastExpression codeCastExpression = (CodeCastExpression)(codeMethodReturnStatement.Expression = new CodeCastExpression());
		CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression();
		codePropertyReferenceExpression.TargetObject = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Context");
		codePropertyReferenceExpression.PropertyName = contextProperty;
		codeCastExpression.TargetType = new CodeTypeReference(retType);
		codeCastExpression.Expression = codePropertyReferenceExpression;
		codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
		if (partialClass == null)
		{
			mainClass.Members.Add(codeMemberProperty);
		}
		else
		{
			partialClass.Members.Add(codeMemberProperty);
		}
	}

	protected void CreateProfileProperty()
	{
		string retType = ((!AppCodeCompiler.HaveCustomProfile(WebConfigurationManager.GetWebApplicationSection("system.web/profile") as ProfileSection)) ? "System.Web.Profile.DefaultProfile" : "ProfileCommon");
		InternalCreatePageProperty(retType, "Profile", "Profile");
	}

	protected virtual void AddInterfaces()
	{
		if (parser.Interfaces == null)
		{
			return;
		}
		foreach (string @interface in parser.Interfaces)
		{
			if (@interface is string)
			{
				mainClass.BaseTypes.Add(new CodeTypeReference(@interface));
			}
		}
	}

	protected virtual void AddClassAttributes()
	{
	}

	protected virtual void AddApplicationAndSessionObjects()
	{
	}

	protected void CreateApplicationOrSessionPropertyForObject(Type type, string propName, bool isApplication, bool isPublic)
	{
		CodeExpression codeExpression = null;
		CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
		codeMemberProperty.Type = new CodeTypeReference(type);
		codeMemberProperty.Name = propName;
		if (isPublic)
		{
			codeMemberProperty.Attributes = (MemberAttributes)24578;
		}
		else
		{
			codeMemberProperty.Attributes = (MemberAttributes)20482;
		}
		CodePropertyReferenceExpression targetObject = ((!isApplication) ? new CodePropertyReferenceExpression(thisRef, "Session") : new CodePropertyReferenceExpression(thisRef, "Application"));
		CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(targetObject, "StaticObjects"), "GetObject"), new CodePrimitiveExpression(propName));
		CodeCastExpression codeCastExpression = new CodeCastExpression(codeMemberProperty.Type, expression);
		if (isApplication)
		{
			CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression(thisRef, "cached" + propName);
			CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
			codeConditionStatement.Condition = new CodeBinaryOperatorExpression(codeFieldReferenceExpression, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null));
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = codeFieldReferenceExpression;
			codeAssignStatement.Right = codeCastExpression;
			codeConditionStatement.TrueStatements.Add(codeAssignStatement);
			codeMemberProperty.GetStatements.Add(codeConditionStatement);
			codeExpression = codeFieldReferenceExpression;
		}
		else
		{
			codeExpression = codeCastExpression;
		}
		codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(codeExpression));
		mainClass.Members.Add(codeMemberProperty);
	}

	protected string CreateFieldForObject(Type type, string name)
	{
		string text = "cached" + name;
		CodeMemberField codeMemberField = new CodeMemberField(type, text);
		codeMemberField.Attributes = MemberAttributes.Private;
		mainClass.Members.Add(codeMemberField);
		return text;
	}

	protected void CreatePropertyForObject(Type type, string propName, string fieldName, bool isPublic)
	{
		CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression(thisRef, fieldName);
		CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
		codeMemberProperty.Type = new CodeTypeReference(type);
		codeMemberProperty.Name = propName;
		if (isPublic)
		{
			codeMemberProperty.Attributes = (MemberAttributes)24578;
		}
		else
		{
			codeMemberProperty.Attributes = (MemberAttributes)20482;
		}
		CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
		codeConditionStatement.Condition = new CodeBinaryOperatorExpression(codeFieldReferenceExpression, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null));
		CodeObjectCreateExpression right = new CodeObjectCreateExpression(codeMemberProperty.Type);
		codeConditionStatement.TrueStatements.Add(new CodeAssignStatement(codeFieldReferenceExpression, right));
		codeMemberProperty.GetStatements.Add(codeConditionStatement);
		codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(codeFieldReferenceExpression));
		mainClass.Members.Add(codeMemberProperty);
	}

	private void CheckCompilerErrors(CompilerResults results)
	{
		if (results.NativeCompilerReturnValue == 0)
		{
			return;
		}
		string fileText = null;
		CompilerErrorCollection errors = results.Errors;
		string text = ((errors != null && errors.Count > 0) ? errors[0] : null)?.FileName;
		if (text != null && File.Exists(text))
		{
			using StreamReader streamReader = File.OpenText(text);
			fileText = streamReader.ReadToEnd();
		}
		else
		{
			StringWriter stringWriter = new StringWriter();
			provider.CreateGenerator().GenerateCodeFromCompileUnit(unit, stringWriter, null);
			fileText = stringWriter.ToString();
		}
		throw new CompilationException(parser.InputFile, errors, fileText);
	}

	protected string DynamicDir()
	{
		return AppDomain.CurrentDomain.SetupInformation.DynamicBase;
	}

	internal static CodeDomProvider CreateProvider(string lang)
	{
		CompilerParameters par;
		string tempdir;
		return CreateProvider(HttpContext.Current, lang, out par, out tempdir);
	}

	internal static CodeDomProvider CreateProvider(string lang, out string compilerOptions, out int warningLevel, out string tempdir)
	{
		return CreateProvider(HttpContext.Current, lang, out compilerOptions, out warningLevel, out tempdir);
	}

	internal static CodeDomProvider CreateProvider(HttpContext context, string lang, out string compilerOptions, out int warningLevel, out string tempdir)
	{
		CompilerParameters par;
		CodeDomProvider result = CreateProvider(context, lang, out par, out tempdir);
		if (par != null)
		{
			warningLevel = par.WarningLevel;
			compilerOptions = par.CompilerOptions;
			return result;
		}
		warningLevel = 2;
		compilerOptions = string.Empty;
		return result;
	}

	internal static CodeDomProvider CreateProvider(HttpContext context, string lang, out CompilerParameters par, out string tempdir)
	{
		CodeDomProvider result = null;
		par = null;
		CompilationSection compilationSection = (CompilationSection)WebConfigurationManager.GetWebApplicationSection("system.web/compilation");
		Compiler compiler = compilationSection.Compilers[lang];
		if (compiler == null)
		{
			CompilerInfo compilerInfo = CodeDomProvider.GetCompilerInfo(lang);
			if (compilerInfo != null && compilerInfo.IsCodeDomProviderTypeValid)
			{
				result = compilerInfo.CreateProvider();
				par = compilerInfo.CreateDefaultCompilerParameters();
			}
		}
		else
		{
			result = Activator.CreateInstance(HttpApplication.LoadType(compiler.Type, throwOnMissing: true)) as CodeDomProvider;
			par = new CompilerParameters();
			par.CompilerOptions = compiler.CompilerOptions;
			par.WarningLevel = compiler.WarningLevel;
		}
		tempdir = compilationSection.TempDirectory;
		return result;
	}

	[MonoTODO("find out how to extract the warningLevel and compilerOptions in the <system.codedom> case")]
	public virtual Type GetCompiledType()
	{
		Type typeFromCache = CachingCompiler.GetTypeFromCache(parser.InputFile);
		if (typeFromCache != null)
		{
			return typeFromCache;
		}
		ConstructType();
		string language = parser.Language;
		Provider = CreateProvider(parser.Context, language, out var compilerOptions, out var warningLevel, out var tempdir);
		if (Provider == null)
		{
			throw new HttpException("Configuration error. Language not supported: " + language, 500);
		}
		CompilerParameters compilerParameters = CompilerParameters;
		compilerParameters.IncludeDebugInformation = parser.Debug;
		compilerParameters.CompilerOptions = compilerOptions + " " + parser.CompilerOptions;
		compilerParameters.WarningLevel = warningLevel;
		bool keepFiles = Environment.GetEnvironmentVariable("MONO_ASPNET_NODELETE") != null;
		if (tempdir == null || tempdir == "")
		{
			tempdir = DynamicDir();
		}
		TempFileCollection tempFileCollection2 = (compilerParameters.TempFiles = new TempFileCollection(tempdir, keepFiles));
		string fileName = Path.GetFileName(tempFileCollection2.AddExtension("dll", keepFile: true));
		compilerParameters.OutputAssembly = Path.Combine(DynamicDir(), fileName);
		CompilerResults compilerResults = CachingCompiler.Compile(this);
		CheckCompilerErrors(compilerResults);
		Assembly assembly = compilerResults.CompiledAssembly;
		if (assembly == null)
		{
			if (!File.Exists(compilerParameters.OutputAssembly))
			{
				compilerResults.TempFiles.Delete();
				throw new CompilationException(parser.InputFile, compilerResults.Errors, "No assembly returned after compilation!?");
			}
			assembly = Assembly.LoadFrom(compilerParameters.OutputAssembly);
		}
		compilerResults.TempFiles.Delete();
		Type type = assembly.GetType(MainClassType, throwOnError: true);
		if (parser.IsPartial && !isRebuilding && CheckPartialBaseType(type))
		{
			isRebuilding = true;
			parser.RootBuilder.ResetState();
			return GetCompiledType();
		}
		return type;
	}

	internal bool CheckPartialBaseType(Type type)
	{
		Type baseType = type.BaseType;
		if (baseType == null || baseType == typeof(Page))
		{
			return false;
		}
		bool result = false;
		if (CheckPartialBaseFields(type, baseType))
		{
			result = true;
		}
		if (CheckPartialBaseProperties(type, baseType))
		{
			result = true;
		}
		return result;
	}

	internal bool CheckPartialBaseFields(Type type, Type baseType)
	{
		bool result = false;
		FieldInfo[] fields = baseType.GetFields(replaceableFlags);
		foreach (FieldInfo fieldInfo in fields)
		{
			if (!fieldInfo.IsPrivate)
			{
				FieldInfo field = type.GetField(fieldInfo.Name, replaceableFlags);
				if (field != null && field.DeclaringType == type)
				{
					partialNameOverride[field.Name] = true;
					result = true;
				}
			}
		}
		return result;
	}

	internal bool CheckPartialBaseProperties(Type type, Type baseType)
	{
		bool result = false;
		PropertyInfo[] properties = baseType.GetProperties();
		foreach (PropertyInfo propertyInfo in properties)
		{
			PropertyInfo property = type.GetProperty(propertyInfo.Name);
			if (property != null && property.DeclaringType == type)
			{
				partialNameOverride[property.Name] = true;
				result = true;
			}
		}
		return result;
	}
}
