using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Profile;

namespace System.Web.Compilation;

internal class AppCodeCompiler
{
	private static bool _alreadyCompiled;

	internal static string DefaultAppCodeAssemblyName;

	private List<AppCodeAssembly> assemblies;

	private string providerTypeName;

	public AppCodeCompiler()
	{
		assemblies = new List<AppCodeAssembly>();
	}

	private bool ProcessAppCodeDir(string appCode, AppCodeAssembly defasm)
	{
		CompilationSection compilationSection = (CompilationSection)WebConfigurationManager.GetWebApplicationSection("system.web/compilation");
		if (compilationSection != null)
		{
			for (int i = 0; i < compilationSection.CodeSubDirectories.Count; i++)
			{
				string name = "App_SubCode_" + compilationSection.CodeSubDirectories[i].DirectoryName;
				assemblies.Add(new AppCodeAssembly(name, Path.Combine(appCode, compilationSection.CodeSubDirectories[i].DirectoryName)));
			}
		}
		return CollectFiles(appCode, defasm);
	}

	private CodeTypeReference GetProfilePropertyType(string type)
	{
		if (string.IsNullOrEmpty(type))
		{
			throw new ArgumentException("String size cannot be 0", "type");
		}
		return new CodeTypeReference(type);
	}

	private string FindProviderTypeName(ProfileSection ps, string providerName)
	{
		if (ps.Providers == null || ps.Providers.Count == 0)
		{
			return null;
		}
		return ps.Providers[providerName]?.Type;
	}

	private void GetProfileProviderAttribute(ProfileSection ps, CodeAttributeDeclarationCollection collection, string providerName)
	{
		if (string.IsNullOrEmpty(providerName))
		{
			providerTypeName = FindProviderTypeName(ps, ps.DefaultProvider);
		}
		else
		{
			providerTypeName = FindProviderTypeName(ps, providerName);
		}
		if (providerTypeName == null)
		{
			throw new HttpException($"Profile provider type not defined: {providerName}");
		}
		collection.Add(new CodeAttributeDeclaration("ProfileProvider", new CodeAttributeArgument(new CodePrimitiveExpression(providerTypeName))));
	}

	private void GetProfileSettingsSerializeAsAttribute(ProfileSection ps, CodeAttributeDeclarationCollection collection, SerializationMode mode)
	{
		string value = "SettingsSerializeAs." + mode;
		collection.Add(new CodeAttributeDeclaration("SettingsSerializeAs", new CodeAttributeArgument(new CodeSnippetExpression(value))));
	}

	private void AddProfileClassGetProfileMethod(CodeTypeDeclaration profileClass)
	{
		CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(ProfileBase)), "Create"), new CodeVariableReferenceExpression("username"));
		CodeCastExpression codeCastExpression = new CodeCastExpression();
		codeCastExpression.TargetType = new CodeTypeReference("ProfileCommon");
		codeCastExpression.Expression = expression;
		CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
		codeMethodReturnStatement.Expression = codeCastExpression;
		CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
		codeMemberMethod.Name = "GetProfile";
		codeMemberMethod.ReturnType = new CodeTypeReference("ProfileCommon");
		codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression("System.String", "username"));
		codeMemberMethod.Statements.Add(codeMethodReturnStatement);
		codeMemberMethod.Attributes = MemberAttributes.Public;
		profileClass.Members.Add(codeMemberMethod);
	}

	private void AddProfileClassProperty(ProfileSection ps, CodeTypeDeclaration profileClass, ProfilePropertySettings pset)
	{
		string name = pset.Name;
		if (string.IsNullOrEmpty(name))
		{
			throw new HttpException("Profile property 'Name' attribute cannot be null.");
		}
		CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
		string text = pset.Type;
		if (text == "string")
		{
			text = "System.String";
		}
		codeMemberProperty.Name = name;
		codeMemberProperty.Type = GetProfilePropertyType(text);
		codeMemberProperty.Attributes = MemberAttributes.Public;
		CodeAttributeDeclarationCollection codeAttributeDeclarationCollection = new CodeAttributeDeclarationCollection();
		GetProfileProviderAttribute(ps, codeAttributeDeclarationCollection, pset.Provider);
		GetProfileSettingsSerializeAsAttribute(ps, codeAttributeDeclarationCollection, pset.SerializeAs);
		codeMemberProperty.CustomAttributes = codeAttributeDeclarationCollection;
		CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
		CodeCastExpression codeCastExpression = (CodeCastExpression)(codeMethodReturnStatement.Expression = new CodeCastExpression());
		CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "GetPropertyValue"), new CodePrimitiveExpression(name));
		codeCastExpression.TargetType = new CodeTypeReference(text);
		codeCastExpression.Expression = expression;
		codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
		if (!pset.ReadOnly)
		{
			expression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "SetPropertyValue"), new CodePrimitiveExpression(name), new CodeSnippetExpression("value"));
			codeMemberProperty.SetStatements.Add(expression);
		}
		profileClass.Members.Add(codeMemberProperty);
	}

	private void AddProfileClassGroupProperty(string groupName, string memberName, CodeTypeDeclaration profileClass)
	{
		CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
		codeMemberProperty.Name = memberName;
		codeMemberProperty.Type = new CodeTypeReference(groupName);
		codeMemberProperty.Attributes = MemberAttributes.Public;
		CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
		CodeCastExpression codeCastExpression = (CodeCastExpression)(codeMethodReturnStatement.Expression = new CodeCastExpression());
		CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "GetProfileGroup"), new CodePrimitiveExpression(memberName));
		codeCastExpression.TargetType = new CodeTypeReference(groupName);
		codeCastExpression.Expression = expression;
		codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
		profileClass.Members.Add(codeMemberProperty);
	}

	private void BuildProfileClass(ProfileSection ps, string className, ProfilePropertySettingsCollection psc, CodeNamespace ns, string baseClass, bool baseIsGlobal, SortedList<string, string> groupProperties)
	{
		CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(className);
		CodeTypeReference codeTypeReference = new CodeTypeReference(baseClass);
		if (baseIsGlobal)
		{
			codeTypeReference.Options |= CodeTypeReferenceOptions.GlobalReference;
		}
		codeTypeDeclaration.BaseTypes.Add(codeTypeReference);
		codeTypeDeclaration.TypeAttributes = TypeAttributes.Public;
		ns.Types.Add(codeTypeDeclaration);
		foreach (ProfilePropertySettings item in psc)
		{
			AddProfileClassProperty(ps, codeTypeDeclaration, item);
		}
		if (groupProperties != null && groupProperties.Count > 0)
		{
			foreach (KeyValuePair<string, string> groupProperty in groupProperties)
			{
				AddProfileClassGroupProperty(groupProperty.Key, groupProperty.Value, codeTypeDeclaration);
			}
		}
		AddProfileClassGetProfileMethod(codeTypeDeclaration);
	}

	private string MakeGroupName(string name)
	{
		return "ProfileGroup" + name;
	}

	private bool ProcessCustomProfile(ProfileSection ps, AppCodeAssembly defasm)
	{
		CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
		CodeNamespace codeNamespace = new CodeNamespace(null);
		codeCompileUnit.Namespaces.Add(codeNamespace);
		defasm.AddUnit(codeCompileUnit);
		codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
		codeNamespace.Imports.Add(new CodeNamespaceImport("System.Configuration"));
		codeNamespace.Imports.Add(new CodeNamespaceImport("System.Web"));
		codeNamespace.Imports.Add(new CodeNamespaceImport("System.Web.Profile"));
		RootProfilePropertySettingsCollection propertySettings = ps.PropertySettings;
		if (propertySettings == null)
		{
			return true;
		}
		SortedList<string, string> sortedList = new SortedList<string, string>();
		foreach (ProfileGroupSettings groupSetting in propertySettings.GroupSettings)
		{
			string text = MakeGroupName(groupSetting.Name);
			sortedList.Add(text, groupSetting.Name);
			BuildProfileClass(ps, text, groupSetting.PropertySettings, codeNamespace, "System.Web.Profile.ProfileGroupBase", baseIsGlobal: true, null);
		}
		string text2 = ps.Inherits;
		if (string.IsNullOrEmpty(text2))
		{
			text2 = "System.Web.Profile.ProfileBase";
		}
		else
		{
			string[] array = text2.Split(',');
			if (array.Length > 1)
			{
				text2 = array[0].Trim();
			}
		}
		bool baseIsGlobal = text2.IndexOf('.') != -1;
		BuildProfileClass(ps, "ProfileCommon", propertySettings, codeNamespace, text2, baseIsGlobal, sortedList);
		return true;
	}

	public static bool HaveCustomProfile(ProfileSection ps)
	{
		if (ps == null || !ps.Enabled)
		{
			return false;
		}
		RootProfilePropertySettingsCollection propertySettings = ps.PropertySettings;
		ProfileGroupSettingsCollection profileGroupSettingsCollection = propertySettings?.GroupSettings;
		if (!string.IsNullOrEmpty(ps.Inherits) || (propertySettings != null && propertySettings.Count > 0) || (profileGroupSettingsCollection != null && profileGroupSettingsCollection.Count > 0))
		{
			return true;
		}
		return false;
	}

	public void Compile()
	{
		if (_alreadyCompiled)
		{
			return;
		}
		string text = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Code");
		ProfileSection ps = WebConfigurationManager.GetWebApplicationSection("system.web/profile") as ProfileSection;
		bool flag = Directory.Exists(text);
		bool flag2 = HaveCustomProfile(ps);
		if (!flag && !flag2)
		{
			return;
		}
		AppCodeAssembly appCodeAssembly = new AppCodeAssembly("App_Code", text);
		assemblies.Add(appCodeAssembly);
		bool flag3 = false;
		if (flag)
		{
			flag3 = ProcessAppCodeDir(text, appCodeAssembly);
		}
		if (flag2 && ProcessCustomProfile(ps, appCodeAssembly))
		{
			flag3 = true;
		}
		if (!flag3)
		{
			return;
		}
		HttpRuntime.EnableAssemblyMapping(enable: true);
		string[] binDirectoryAssemblies = HttpApplication.BinDirectoryAssemblies;
		foreach (AppCodeAssembly assembly2 in assemblies)
		{
			assembly2.Build(binDirectoryAssemblies);
		}
		_alreadyCompiled = true;
		DefaultAppCodeAssemblyName = Path.GetFileNameWithoutExtension(appCodeAssembly.OutputAssemblyName);
		RunAppInitialize();
		if (!flag2 || providerTypeName == null || !(Type.GetType(providerTypeName, throwOnError: false) == null))
		{
			return;
		}
		foreach (Assembly topLevelAssembly in BuildManager.TopLevelAssemblies)
		{
			if (!(topLevelAssembly == null) && topLevelAssembly.GetType(providerTypeName, throwOnError: false) != null)
			{
				return;
			}
		}
		Exception innerException = null;
		Type type = null;
		try
		{
			type = HttpApplication.LoadTypeFromBin(providerTypeName);
		}
		catch (Exception ex)
		{
			innerException = ex;
		}
		if (!(type == null))
		{
			return;
		}
		throw new HttpException($"Profile provider type not found: {providerTypeName}", innerException);
	}

	private void RunAppInitialize()
	{
		MethodInfo methodInfo = null;
		foreach (Assembly codeAssembly in BuildManager.CodeAssemblies)
		{
			Type[] exportedTypes = codeAssembly.GetExportedTypes();
			if (exportedTypes == null || exportedTypes.Length == 0)
			{
				continue;
			}
			Type[] array = exportedTypes;
			for (int i = 0; i < array.Length; i++)
			{
				MethodInfo method = array[i].GetMethod("AppInitialize", BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public, null, Type.EmptyTypes, null);
				if (!(method == null))
				{
					if (methodInfo != null)
					{
						throw new HttpException("The static AppInitialize method found in more than one type in the App_Code directory.");
					}
					methodInfo = method;
				}
			}
		}
		if (!(methodInfo == null))
		{
			methodInfo.Invoke(null, null);
		}
	}

	private bool CollectFiles(string dir, AppCodeAssembly aca)
	{
		bool result = false;
		AppCodeAssembly aca2 = aca;
		string[] files = Directory.GetFiles(dir);
		foreach (string path in files)
		{
			aca.AddFile(path);
			result = true;
		}
		files = Directory.GetDirectories(dir);
		foreach (string text in files)
		{
			foreach (AppCodeAssembly assembly in assemblies)
			{
				if (assembly.SourcePath == text)
				{
					aca2 = assembly;
					break;
				}
			}
			if (CollectFiles(text, aca2))
			{
				result = true;
			}
			aca2 = aca;
		}
		return result;
	}
}
