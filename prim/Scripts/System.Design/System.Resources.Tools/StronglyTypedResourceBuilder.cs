using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace System.Resources.Tools;

/// <summary>Provides support for strongly typed resources. This class cannot be inherited.</summary>
public static class StronglyTypedResourceBuilder
{
	private class ResourceItem
	{
		public string VerifiedKey { get; set; }

		public object Resource { get; set; }

		public bool isUnmatchable { get; set; }

		public bool toIgnore { get; set; }

		public ResourceItem(object value)
		{
			Resource = value;
		}
	}

	private static char[] specialChars = new char[30]
	{
		' ', '\u00a0', '.', ',', ';', '|', '~', '@', '#', '%',
		'^', '&', '*', '+', '-', '/', '\\', '<', '>', '?',
		'[', ']', '(', ')', '{', '}', '"', '\'', ':', '!'
	};

	private static char[] specialCharsNameSpace = new char[28]
	{
		' ', '\u00a0', ',', ';', '|', '~', '@', '#', '%', '^',
		'&', '*', '+', '-', '/', '\\', '<', '>', '?', '[',
		']', '(', ')', '{', '}', '"', '\'', '!'
	};

	/// <summary>Generates a class file that contains strongly typed properties that match the resources in the specified .resx file.</summary>
	/// <param name="resxFile">The name of a .resx file used as input.</param>
	/// <param name="baseName">The name of the class to be generated.</param>
	/// <param name="generatedCodeNamespace">The namespace of the class to be generated.</param>
	/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> class that provides the language in which the class will be generated.</param>
	/// <param name="internalClass">
	///   <see langword="true" /> to generate an internal class; <see langword="false" /> to generate a public class.</param>
	/// <param name="unmatchable">A <see cref="T:System.String" /> array that contains each resource name for which a property cannot be generated. Typically, a property cannot be generated because the resource name is not a valid identifier.</param>
	/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> container.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="basename" /> or <paramref name="codeProvider" /> is <see langword="null" />.</exception>
	public static CodeCompileUnit Create(string resxFile, string baseName, string generatedCodeNamespace, CodeDomProvider codeProvider, bool internalClass, out string[] unmatchable)
	{
		return Create(resxFile, baseName, generatedCodeNamespace, null, codeProvider, internalClass, out unmatchable);
	}

	/// <summary>Generates a class file that contains strongly typed properties that match the resources in the specified .resx file.</summary>
	/// <param name="resxFile">The name of a .resx file used as input.</param>
	/// <param name="baseName">The name of the class to be generated.</param>
	/// <param name="generatedCodeNamespace">The namespace of the class to be generated.</param>
	/// <param name="resourcesNamespace">The namespace of the resource to be generated.</param>
	/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> class that provides the language in which the class will be generated.</param>
	/// <param name="internalClass">
	///   <see langword="true" /> to generate an internal class; <see langword="false" /> to generate a public class.</param>
	/// <param name="unmatchable">A <see cref="T:System.String" /> array that contains each resource name for which a property cannot be generated. Typically, a property cannot be generated because the resource name is not a valid identifier.</param>
	/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> container.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="basename" /> or <paramref name="codeProvider" /> is <see langword="null" />.</exception>
	public static CodeCompileUnit Create(string resxFile, string baseName, string generatedCodeNamespace, string resourcesNamespace, CodeDomProvider codeProvider, bool internalClass, out string[] unmatchable)
	{
		if (resxFile == null)
		{
			throw new ArgumentNullException("Parameter resxFile must not be null");
		}
		List<char> list = new List<char>(Path.GetInvalidPathChars());
		char[] array = resxFile.ToCharArray();
		foreach (char item in array)
		{
			if (list.Contains(item))
			{
				throw new ArgumentException("Invalid character in resxFileName");
			}
		}
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		using (ResXResourceReader resXResourceReader = new ResXResourceReader(resxFile))
		{
			foreach (DictionaryEntry item2 in resXResourceReader)
			{
				dictionary.Add((string)item2.Key, item2.Value);
			}
		}
		return Create(dictionary, baseName, generatedCodeNamespace, resourcesNamespace, codeProvider, internalClass, out unmatchable);
	}

	/// <summary>Generates a class file that contains strongly typed properties that match the resources referenced in the specified collection.</summary>
	/// <param name="resourceList">An <see cref="T:System.Collections.IDictionary" /> collection where each dictionary entry key/value pair is the name of a resource and the value of the resource.</param>
	/// <param name="baseName">The name of the class to be generated.</param>
	/// <param name="generatedCodeNamespace">The namespace of the class to be generated.</param>
	/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> class that provides the language in which the class will be generated.</param>
	/// <param name="internalClass">
	///   <see langword="true" /> to generate an internal class; <see langword="false" /> to generate a public class.</param>
	/// <param name="unmatchable">An array that contains each resource name for which a property cannot be generated. Typically, a property cannot be generated because the resource name is not a valid identifier.</param>
	/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> container.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="resourceList" />, <paramref name="basename" />, or <paramref name="codeProvider" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">A resource node name does not match its key in <paramref name="resourceList" />.</exception>
	public static CodeCompileUnit Create(IDictionary resourceList, string baseName, string generatedCodeNamespace, CodeDomProvider codeProvider, bool internalClass, out string[] unmatchable)
	{
		return Create(resourceList, baseName, generatedCodeNamespace, null, codeProvider, internalClass, out unmatchable);
	}

	/// <summary>Generates a class file that contains strongly typed properties that match the resources referenced in the specified collection.</summary>
	/// <param name="resourceList">An <see cref="T:System.Collections.IDictionary" /> collection where each dictionary entry key/value pair is the name of a resource and the value of the resource.</param>
	/// <param name="baseName">The name of the class to be generated.</param>
	/// <param name="generatedCodeNamespace">The namespace of the class to be generated.</param>
	/// <param name="resourcesNamespace">The namespace of the resource to be generated.</param>
	/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> object that provides the language in which the class will be generated.</param>
	/// <param name="internalClass">
	///   <see langword="true" /> to generate an internal class; <see langword="false" /> to generate a public class.</param>
	/// <param name="unmatchable">A <see cref="T:System.String" /> array that contains each resource name for which a property cannot be generated. Typically, a property cannot be generated because the resource name is not a valid identifier.</param>
	/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> container.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="resourceList" />, <paramref name="basename" />, or <paramref name="codeProvider" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">A resource node name does not match its key in <paramref name="resourceList" />.</exception>
	public static CodeCompileUnit Create(IDictionary resourceList, string baseName, string generatedCodeNamespace, string resourcesNamespace, CodeDomProvider codeProvider, bool internalClass, out string[] unmatchable)
	{
		if (resourceList == null)
		{
			throw new ArgumentNullException("Parameter resourceList must not be null");
		}
		if (codeProvider == null)
		{
			throw new ArgumentNullException("Parameter: codeProvider must not be null");
		}
		if (baseName == null)
		{
			throw new ArgumentNullException("Parameter: baseName must not be null");
		}
		string text = VerifyResourceName(baseName, codeProvider);
		if (text == null)
		{
			throw new ArgumentException("Parameter: baseName is invalid");
		}
		string text2;
		if (generatedCodeNamespace == null)
		{
			text2 = "";
		}
		else
		{
			text2 = CleanNamespaceChars(generatedCodeNamespace);
			text2 = codeProvider.CreateValidIdentifier(text2);
		}
		string resourcesToUse = ((resourcesNamespace == null) ? (text2 + "." + text) : ((!(resourcesNamespace == string.Empty)) ? (resourcesNamespace + "." + text) : text));
		Dictionary<string, ResourceItem> dictionary = new Dictionary<string, ResourceItem>(StringComparer.OrdinalIgnoreCase);
		foreach (DictionaryEntry resource in resourceList)
		{
			dictionary.Add((string)resource.Key, new ResourceItem(resource.Value));
		}
		ProcessResourceList(dictionary, codeProvider);
		CodeCompileUnit codeCompileUnit = GenerateCodeDOMBase(text, text2, resourcesToUse, internalClass);
		unmatchable = ResourcePropertyGeneration(codeCompileUnit.Namespaces[0].Types[0], dictionary, internalClass);
		return codeCompileUnit;
	}

	private static string[] ResourcePropertyGeneration(CodeTypeDeclaration resType, Dictionary<string, ResourceItem> resourceItemDict, bool internalClass)
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, ResourceItem> item in resourceItemDict)
		{
			if (item.Value.isUnmatchable)
			{
				list.Add(item.Key);
			}
			else if (!item.Value.toIgnore)
			{
				if (item.Value.Resource is Stream)
				{
					resType.Members.Add(GenerateStreamResourceProp(item.Value.VerifiedKey, item.Key, internalClass));
				}
				else if (item.Value.Resource is string)
				{
					resType.Members.Add(GenerateStringResourceProp(item.Value.VerifiedKey, item.Key, internalClass));
				}
				else
				{
					resType.Members.Add(GenerateStandardResourceProp(item.Value.VerifiedKey, item.Key, item.Value.Resource.GetType(), internalClass));
				}
			}
		}
		return list.ToArray();
	}

	private static CodeCompileUnit GenerateCodeDOMBase(string baseNameToUse, string generatedCodeNamespaceToUse, string resourcesToUse, bool internalClass)
	{
		CodeCompileUnit obj = new CodeCompileUnit
		{
			ReferencedAssemblies = { "System.dll" }
		};
		CodeNamespace codeNamespace = new CodeNamespace(generatedCodeNamespaceToUse);
		obj.Namespaces.Add(codeNamespace);
		codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
		CodeTypeDeclaration codeTypeDeclaration = GenerateBaseType(baseNameToUse, internalClass);
		codeNamespace.Types.Add(codeTypeDeclaration);
		GenerateFields(codeTypeDeclaration);
		codeTypeDeclaration.Members.Add(GenerateConstructor());
		codeTypeDeclaration.Members.Add(GenerateResourceManagerProp(baseNameToUse, resourcesToUse, internalClass));
		codeTypeDeclaration.Members.Add(GenerateCultureProp(internalClass));
		return obj;
	}

	private static void ProcessResourceList(Dictionary<string, ResourceItem> resourceItemDict, CodeDomProvider codeProvider)
	{
		foreach (KeyValuePair<string, ResourceItem> item in resourceItemDict)
		{
			if (item.Key.StartsWith(">>") || item.Key.StartsWith("$"))
			{
				item.Value.toIgnore = true;
				continue;
			}
			if (item.Key == "ResourceManager" || item.Key == "Culture")
			{
				item.Value.isUnmatchable = true;
				continue;
			}
			item.Value.VerifiedKey = VerifyResourceName(item.Key, codeProvider);
			if (item.Value.VerifiedKey == null)
			{
				item.Value.isUnmatchable = true;
				continue;
			}
			foreach (KeyValuePair<string, ResourceItem> item2 in resourceItemDict)
			{
				if (item2.Value != item.Value && item2.Value.VerifiedKey != null && string.Equals(item2.Value.VerifiedKey, item.Value.VerifiedKey, StringComparison.OrdinalIgnoreCase))
				{
					item2.Value.isUnmatchable = true;
					item.Value.isUnmatchable = true;
				}
			}
		}
	}

	private static CodeTypeDeclaration GenerateBaseType(string baseNameToUse, bool internalClass)
	{
		CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(baseNameToUse);
		codeTypeDeclaration.IsClass = true;
		if (internalClass)
		{
			codeTypeDeclaration.TypeAttributes = TypeAttributes.NotPublic;
		}
		else
		{
			codeTypeDeclaration.TypeAttributes = TypeAttributes.Public;
		}
		codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration("System.CodeDom.Compiler.GeneratedCodeAttribute", new CodeAttributeArgument(new CodePrimitiveExpression("System.Resources.Tools.StronglyTypedResourceBuilder")), new CodeAttributeArgument(new CodePrimitiveExpression("4.0.0.0"))));
		codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration("System.Diagnostics.DebuggerNonUserCodeAttribute"));
		codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration("System.Runtime.CompilerServices.CompilerGeneratedAttribute"));
		return codeTypeDeclaration;
	}

	private static void GenerateFields(CodeTypeDeclaration resType)
	{
		CodeMemberField codeMemberField = new CodeMemberField();
		codeMemberField.Attributes = (MemberAttributes)20483;
		codeMemberField.Name = "resourceMan";
		codeMemberField.Type = new CodeTypeReference(typeof(ResourceManager));
		resType.Members.Add(codeMemberField);
		CodeMemberField codeMemberField2 = new CodeMemberField();
		codeMemberField2.Attributes = (MemberAttributes)20483;
		codeMemberField2.Name = "resourceCulture";
		codeMemberField2.Type = new CodeTypeReference(typeof(CultureInfo));
		resType.Members.Add(codeMemberField2);
	}

	private static CodeConstructor GenerateConstructor()
	{
		CodeConstructor codeConstructor = new CodeConstructor();
		codeConstructor.Attributes = MemberAttributes.FamilyAndAssembly;
		codeConstructor.CustomAttributes.Add(new CodeAttributeDeclaration("System.Diagnostics.CodeAnalysis.SuppressMessageAttribute", new CodeAttributeArgument(new CodePrimitiveExpression("Microsoft.Performance")), new CodeAttributeArgument(new CodePrimitiveExpression("CA1811:AvoidUncalledPrivateCode"))));
		return codeConstructor;
	}

	private static CodeAttributeDeclaration DefaultPropertyAttribute()
	{
		return new CodeAttributeDeclaration("System.ComponentModel.EditorBrowsableAttribute", new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("System.ComponentModel.EditorBrowsableState"), "Advanced")));
	}

	private static CodeMemberProperty GenerateCultureProp(bool internalClass)
	{
		CodeMemberProperty codeMemberProperty = GeneratePropertyBase("Culture", typeof(CultureInfo), internalClass, hasGet: true, hasSet: true);
		codeMemberProperty.CustomAttributes.Add(DefaultPropertyAttribute());
		codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(null, "resourceCulture")));
		codeMemberProperty.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(null, "resourceCulture"), new CodePropertySetValueReferenceExpression()));
		return codeMemberProperty;
	}

	private static CodeMemberProperty GenerateResourceManagerProp(string baseNameToUse, string resourcesToUse, bool internalClass)
	{
		CodeMemberProperty codeMemberProperty = GeneratePropertyBase("ResourceManager", typeof(ResourceManager), internalClass, hasGet: true, hasSet: false);
		codeMemberProperty.CustomAttributes.Add(DefaultPropertyAttribute());
		CodeStatement[] trueStatements = new CodeStatement[2]
		{
			new CodeVariableDeclarationStatement(new CodeTypeReference("System.Resources.ResourceManager"), "temp", new CodeObjectCreateExpression(new CodeTypeReference("System.Resources.ResourceManager"), new CodePrimitiveExpression(resourcesToUse), new CodePropertyReferenceExpression(new CodeTypeOfExpression(baseNameToUse), "Assembly"))),
			new CodeAssignStatement(new CodeFieldReferenceExpression(null, "resourceMan"), new CodeVariableReferenceExpression("temp"))
		};
		codeMemberProperty.GetStatements.Add(new CodeConditionStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("System.Object"), "Equals"), new CodePrimitiveExpression(null), new CodeFieldReferenceExpression(null, "resourceMan")), trueStatements));
		codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(null, "resourceMan")));
		return codeMemberProperty;
	}

	private static CodeMemberProperty GenerateStandardResourceProp(string propName, string resName, Type propertyType, bool isInternal)
	{
		CodeMemberProperty codeMemberProperty = GeneratePropertyBase(propName, propertyType, isInternal, hasGet: true, hasSet: false);
		codeMemberProperty.GetStatements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference("System.Object"), "obj", new CodeMethodInvokeExpression(new CodePropertyReferenceExpression(null, "ResourceManager"), "GetObject", new CodePrimitiveExpression(resName), new CodeFieldReferenceExpression(null, "resourceCulture"))));
		codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeCastExpression(new CodeTypeReference(propertyType), new CodeVariableReferenceExpression("obj"))));
		return codeMemberProperty;
	}

	private static CodeMemberProperty GenerateStringResourceProp(string propName, string resName, bool isInternal)
	{
		CodeMemberProperty codeMemberProperty = GeneratePropertyBase(propName, typeof(string), isInternal, hasGet: true, hasSet: false);
		codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(null, "ResourceManager"), "GetString"), new CodePrimitiveExpression(resName), new CodeFieldReferenceExpression(null, "resourceCulture"))));
		return codeMemberProperty;
	}

	private static CodeMemberProperty GenerateStreamResourceProp(string propName, string resName, bool isInternal)
	{
		CodeMemberProperty codeMemberProperty = GeneratePropertyBase(propName, typeof(UnmanagedMemoryStream), isInternal, hasGet: true, hasSet: false);
		codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(null, "ResourceManager"), "GetStream"), new CodePrimitiveExpression(resName), new CodeFieldReferenceExpression(null, "resourceCulture"))));
		return codeMemberProperty;
	}

	private static CodeMemberProperty GeneratePropertyBase(string name, Type propertyType, bool isInternal, bool hasGet, bool hasSet)
	{
		CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
		codeMemberProperty.Name = name;
		codeMemberProperty.Type = new CodeTypeReference(propertyType);
		if (isInternal)
		{
			codeMemberProperty.Attributes = (MemberAttributes)4099;
		}
		else
		{
			codeMemberProperty.Attributes = (MemberAttributes)24579;
		}
		codeMemberProperty.HasGet = hasGet;
		codeMemberProperty.HasSet = hasSet;
		return codeMemberProperty;
	}

	/// <summary>Generates a valid resource string based on the specified input string and code provider.</summary>
	/// <param name="key">The string to verify and, if necessary, convert to a valid resource name.</param>
	/// <param name="provider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> object that specifies the target language to use.</param>
	/// <returns>A valid resource name derived from the <paramref name="key" /> parameter. Any invalid tokens are replaced with the underscore (_) character, or <see langword="null" /> if the derived string still contains invalid characters according to the language specified by the <paramref name="provider" /> parameter.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="key" /> or <paramref name="provider" /> is <see langword="null" />.</exception>
	public static string VerifyResourceName(string key, CodeDomProvider provider)
	{
		if (key == null)
		{
			throw new ArgumentNullException("Parameter: key must not be null");
		}
		if (provider == null)
		{
			throw new ArgumentNullException("Parameter: provider must not be null");
		}
		string value;
		if (key == string.Empty)
		{
			value = "_";
		}
		else
		{
			char[] array = key.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = VerifySpecialChar(array[i]);
			}
			value = new string(array);
		}
		value = provider.CreateValidIdentifier(value);
		if (provider.IsValidIdentifier(value))
		{
			return value;
		}
		return null;
	}

	private static char VerifySpecialChar(char ch)
	{
		for (int i = 0; i < specialChars.Length; i++)
		{
			if (specialChars[i] == ch)
			{
				return '_';
			}
		}
		return ch;
	}

	private static string CleanNamespaceChars(string name)
	{
		char[] array = name.ToCharArray();
		for (int i = 0; i < array.Length; i++)
		{
			char[] array2 = specialCharsNameSpace;
			foreach (char c in array2)
			{
				if (array[i] == c)
				{
					array[i] = '_';
				}
			}
		}
		return new string(array);
	}
}
