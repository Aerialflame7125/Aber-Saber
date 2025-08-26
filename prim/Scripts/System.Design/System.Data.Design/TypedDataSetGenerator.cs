using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace System.Data.Design;

/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" /> class.</summary>
public sealed class TypedDataSetGenerator
{
	/// <summary>Provides the <see cref="T:System.Data.Design.TypedDataSetGenerator" /> with information for creating typed datasets that support LINQ to DataSet and hierarchical update.</summary>
	[Flags]
	public enum GenerateOption
	{
		/// <summary>Generates typed datasets that are compatible with typed datasets generated in versions of Visual Studio earlier than Visual Studio 2008.</summary>
		None = 0,
		/// <summary>Generates typed datasets that have a TableAdapterManager and associated methods for enabling hierarchical update.</summary>
		HierarchicalUpdate = 1,
		/// <summary>Generates typed datasets that have data tables that inherit from <see cref="T:System.Data.TypedTableBase`1" /> in order to enable the ability to perform LINQ queries on data tables.</summary>
		LinqOverTypedDatasets = 2
	}

	/// <summary>Gets or sets the collection of assemblies referenced in a typed dataset.</summary>
	/// <returns>A collection containing all referenced assemblies in the dataset.</returns>
	[System.MonoTODO]
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

	/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" /> based on an existing <see cref="T:System.Data.DataSet" />.</summary>
	/// <param name="dataSet">The source <see cref="T:System.Data.DataSet" /> that specifies the metadata for the typed <see cref="T:System.Data.DataSet" />.</param>
	/// <param name="codeNamespace">The namespace that provides the target namespace for the typed <see cref="T:System.Data.DataSet" />.</param>
	/// <param name="codeProvider">The language-specific <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> to use to generate the dataset.</param>
	/// <returns>A strongly typed <see cref="T:System.Data.DataSet" />.</returns>
	public static string Generate(DataSet dataSet, CodeNamespace codeNamespace, CodeDomProvider codeProvider)
	{
		System.Data.TypedDataSetGenerator.Generate(dataSet, codeNamespace, codeProvider.CreateGenerator());
		return null;
	}

	/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" /> based on the provided input file.</summary>
	/// <param name="inputFileContent">A string that represents the XML schema to base the <see cref="T:System.Data.DataSet" /> on.</param>
	/// <param name="compileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> to contain the generated code.</param>
	/// <param name="mainNamespace">The <see cref="T:System.CodeDom.CodeNamespace" /> that contains the generated dataset.</param>
	/// <param name="codeProvider">The language-specific <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> to use to generate the dataset.</param>
	/// <returns>A strongly typed <see cref="T:System.Data.DataSet" />.</returns>
	public static string Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider)
	{
		DataSet dataSet = new DataSet();
		dataSet.ReadXmlSchema(inputFileContent);
		System.Data.TypedDataSetGenerator.Generate(dataSet, mainNamespace, codeProvider.CreateGenerator());
		return null;
	}

	/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" /> based on the provided input file.</summary>
	/// <param name="inputFileContent">A string that represents the XML schema to base the <see cref="T:System.Data.DataSet" /> on.</param>
	/// <param name="compileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> to contain the generated code.</param>
	/// <param name="mainNamespace">The <see cref="T:System.CodeDom.CodeNamespace" /> that contains the generated dataset.</param>
	/// <param name="codeProvider">The language specific <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> to use to generate the dataset.</param>
	/// <param name="customDBProviders">A HashTable that maps connections to specific providers in the typed dataset.</param>
	[System.MonoTODO]
	public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, Hashtable customDBProviders)
	{
		throw new NotImplementedException();
	}

	/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" /> based on the provided input file.</summary>
	/// <param name="inputFileContent">A string that represents the XML schema to base the <see cref="T:System.Data.DataSet" /> on.</param>
	/// <param name="compileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> to contain the generated code.</param>
	/// <param name="mainNamespace">The <see cref="T:System.CodeDom.CodeNamespace" /> that contains the generated dataset.</param>
	/// <param name="codeProvider">The language-specific <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> to use to generate the dataset.</param>
	/// <param name="specifiedFactory">The <see cref="T:System.Data.Common.DbProviderFactory" /> to use to override the provider contained in the <paramref name="inputFileContent" />.</param>
	[System.MonoTODO]
	public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, DbProviderFactory specifiedFactory)
	{
		throw new NotImplementedException();
	}

	/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" /> based on the provided input file.</summary>
	/// <param name="inputFileContent">A string that represents the XML schema to base the <see cref="T:System.Data.DataSet" /> on.</param>
	/// <param name="compileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> to contain the generated code.</param>
	/// <param name="mainNamespace">The <see cref="T:System.CodeDom.CodeNamespace" /> that contains the generated dataset.</param>
	/// <param name="codeProvider">The language-specific <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> to use to generate the dataset.</param>
	/// <param name="option">The <see cref="T:System.Data.Design.TypedDataSetGenerator.GenerateOption" /> that determines what (if any) additional components and methods to create when generating a typed dataset.</param>
	/// <returns>A strongly typed <see cref="T:System.Data.DataSet" />.</returns>
	[System.MonoTODO]
	public static string Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, GenerateOption option)
	{
		throw new NotImplementedException();
	}

	/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" /> based on the provided input file.</summary>
	/// <param name="inputFileContent">A string that represents the XML schema to base the <see cref="T:System.Data.DataSet" /> on.</param>
	/// <param name="compileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> to contain the generated code.</param>
	/// <param name="mainNamespace">The <see cref="T:System.CodeDom.CodeNamespace" /> that contains the generated dataset.</param>
	/// <param name="codeProvider">The language-specific <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> to use to generate the dataset.</param>
	/// <param name="customDBProviders">A HashTable that maps connections to specific providers in the typed dataset.</param>
	/// <param name="option">The <see cref="T:System.Data.Design.TypedDataSetGenerator.GenerateOption" /> that determines what (if any) additional components and methods to create when generating a typed dataset.</param>
	[System.MonoTODO]
	public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, Hashtable customDBProviders, GenerateOption option)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the first provider name found in the provided input file.</summary>
	/// <param name="inputFileContent">A string that represents the XML schema to base the <see cref="T:System.Data.DataSet" /> on.</param>
	/// <returns>A string that represents the specific provider for this <see cref="T:System.Data.DataSet" />.</returns>
	[System.MonoTODO]
	public static string GetProviderName(string inputFileContent)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the provider name for the <paramref name="tableName" /> in the input file.</summary>
	/// <param name="inputFileContent">A string that represents the XML schema to base the <see cref="T:System.Data.DataSet" /> on.</param>
	/// <param name="tableName">A string that represents the name of the table to return the provider name from.</param>
	/// <returns>A string that represents the provider name for the specific table passed in to the <paramref name="tableName" /> parameter.</returns>
	[System.MonoTODO]
	public static string GetProviderName(string inputFileContent, string tableName)
	{
		throw new NotImplementedException();
	}
}
