using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace System.Data.Design;

public sealed class TypedDataSetGenerator
{
	[Flags]
	public enum GenerateOption
	{
		None = 0,
		HierarchicalUpdate = 1,
		LinqOverTypedDatasets = 2
	}

	[MonoTODO]
	public static ICollection<Assembly> ReferencedAssemblies
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	private TypedDataSetGenerator()
	{
	}

	public static string Generate(DataSet dataSet, CodeNamespace codeNamespace, CodeDomProvider codeProvider)
	{
		System.Data.TypedDataSetGenerator.Generate(dataSet, codeNamespace, codeProvider.CreateGenerator());
		return null;
	}

	public static string Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider)
	{
		DataSet dataSet = new DataSet();
		dataSet.ReadXmlSchema(inputFileContent);
		System.Data.TypedDataSetGenerator.Generate(dataSet, mainNamespace, codeProvider.CreateGenerator());
		return null;
	}

	[MonoTODO]
	public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, Hashtable customDBProviders)
	{
		throw new NotImplementedException();
	}

	[MonoTODO]
	public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, DbProviderFactory specifiedFactory)
	{
		throw new NotImplementedException();
	}

	[MonoTODO]
	public static string Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, GenerateOption option)
	{
		throw new NotImplementedException();
	}

	[MonoTODO]
	public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, Hashtable customDBProviders, GenerateOption option)
	{
		throw new NotImplementedException();
	}

	[MonoTODO]
	public static string GetProviderName(string inputFileContent)
	{
		throw new NotImplementedException();
	}

	[MonoTODO]
	public static string GetProviderName(string inputFileContent, string tableName)
	{
		throw new NotImplementedException();
	}
}
