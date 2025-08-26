using System.CodeDom;
using System.CodeDom.Compiler;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Serialization.Advanced;

namespace System.Data.Design;

/// <summary>Generates internal mappings to .NET Framework types for XML schema element declarations, including literal XSD message parts in a WSDL document.</summary>
public class TypedDataSetSchemaImporterExtension : SchemaImporterExtension
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Data.Design.TypedDataSetSchemaImporterExtension" /> class.</summary>
	public TypedDataSetSchemaImporterExtension()
		: this(TypedDataSetGenerator.GenerateOption.None)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Data.Design.TypedDataSetSchemaImporterExtension" /> class</summary>
	/// <param name="dataSetGenerateOptions">The <see cref="T:System.Data.Design.TypedDataSetGenerator.GenerateOption" /> value for generating typed datasets that enable LINQ to DataSet and Hierarchical Update.</param>
	protected TypedDataSetSchemaImporterExtension(TypedDataSetGenerator.GenerateOption dataSetGenerateOptions)
	{
	}

	/// <summary>Generates internal type mapping information for an element defined in an XML schema document.</summary>
	/// <param name="type">XMLSchemaType</param>
	/// <param name="context">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> that represents the root class for the Xml schema object model hierarchy and serves as a base class for classes such as the XmlSchema class.</param>
	/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchema" /> class that represents a collection of XML schemas.</param>
	/// <param name="importer">The base <see cref="T:System.Xml.Serialization.XmlSchemaImporter" /> that generates internal mappings to .NET Framework types for XML schema element declarations, including literal XSD message parts in a WSDL document.</param>
	/// <param name="compileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> to contain the generated code.</param>
	/// <param name="mainNamespace">CodeNamespace</param>
	/// <param name="options">The <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> that specifies various options to use when generating .NET Framework types for use with an XML Web service.</param>
	/// <param name="codeProvider">The language specific <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> to use to generate the dataset.</param>
	/// <returns>A string representing the name of the typed dataset class.</returns>
	[System.MonoTODO]
	public override string ImportSchemaType(XmlSchemaType type, XmlSchemaObject context, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
	{
		if (type == null)
		{
			return null;
		}
		_ = context is XmlSchemaElement;
		return null;
	}

	/// <summary>Generates internal type mapping information for an element defined in an XML schema document.</summary>
	/// <param name="name">A <see cref="T:System.String" /> representing the name of the schema to import.</param>
	/// <param name="namespaceName">A <see cref="T:System.String" /> representing the namespace of the XML schema.</param>
	/// <param name="context">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> that represents the root class for the XML schema object model hierarchy and serves as a base class for classes such as the XmlSchema class.</param>
	/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchema" /> class that represents a collection of XML schemas.</param>
	/// <param name="importer">The base <see cref="T:System.Xml.Serialization.XmlSchemaImporter" /> that generates internal mappings to .NET Framework types for XML schema element declarations, including literal XSD message parts in a WSDL document.</param>
	/// <param name="compileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> to contain the generated code.</param>
	/// <param name="mainNamespace">The <see cref="T:System.CodeDom.CodeNamespace" /> that contains the generated dataset.</param>
	/// <param name="options">The <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> that specifies various options to use when generating .NET Framework types for use with an XML Web service.</param>
	/// <param name="codeProvider">The language specific <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> to use to generate the dataset.</param>
	/// <returns>A <see cref="T:System.String" /> representing the name of the typed dataset class.</returns>
	[System.MonoTODO]
	public override string ImportSchemaType(string name, string namespaceName, XmlSchemaObject context, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
	{
		return null;
	}
}
