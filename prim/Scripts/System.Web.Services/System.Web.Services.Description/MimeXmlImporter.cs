using System.CodeDom;
using System.Data;
using System.Data.Design;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

internal class MimeXmlImporter : MimeImporter
{
	private XmlSchemaImporter importer;

	private XmlCodeExporter exporter;

	private XmlSchemaImporter Importer
	{
		get
		{
			if (importer == null)
			{
				importer = new XmlSchemaImporter(base.ImportContext.ConcreteSchemas, base.ImportContext.ServiceImporter.CodeGenerationOptions, base.ImportContext.ServiceImporter.CodeGenerator, base.ImportContext.ImportContext);
				foreach (Type extension in base.ImportContext.ServiceImporter.Extensions)
				{
					importer.Extensions.Add(extension.FullName, extension);
				}
				importer.Extensions.Add(new TypedDataSetSchemaImporterExtension());
				importer.Extensions.Add(new DataSetSchemaImporterExtension());
			}
			return importer;
		}
	}

	private XmlCodeExporter Exporter
	{
		get
		{
			if (exporter == null)
			{
				exporter = new XmlCodeExporter(base.ImportContext.CodeNamespace, base.ImportContext.ServiceImporter.CodeCompileUnit, base.ImportContext.ServiceImporter.CodeGenerator, base.ImportContext.ServiceImporter.CodeGenerationOptions, base.ImportContext.ExportContext);
			}
			return exporter;
		}
	}

	internal override MimeParameterCollection ImportParameters()
	{
		return null;
	}

	internal override MimeReturn ImportReturn()
	{
		MimeContentBinding mimeContentBinding = (MimeContentBinding)base.ImportContext.OperationBinding.Output.Extensions.Find(typeof(MimeContentBinding));
		if (mimeContentBinding != null)
		{
			if (!ContentType.MatchesBase(mimeContentBinding.Type, "text/xml"))
			{
				return null;
			}
			return new MimeReturn
			{
				TypeName = typeof(XmlElement).FullName,
				ReaderType = typeof(XmlReturnReader)
			};
		}
		MimeXmlBinding mimeXmlBinding = (MimeXmlBinding)base.ImportContext.OperationBinding.Output.Extensions.Find(typeof(MimeXmlBinding));
		if (mimeXmlBinding != null)
		{
			MimeXmlReturn mimeXmlReturn = new MimeXmlReturn();
			MessagePart messagePart = base.ImportContext.OutputMessage.Parts.Count switch
			{
				0 => throw new InvalidOperationException(Res.GetString("MessageHasNoParts1", base.ImportContext.InputMessage.Name)), 
				1 => (mimeXmlBinding.Part != null && mimeXmlBinding.Part.Length != 0) ? base.ImportContext.OutputMessage.FindPartByName(mimeXmlBinding.Part) : base.ImportContext.OutputMessage.Parts[0], 
				_ => base.ImportContext.OutputMessage.FindPartByName(mimeXmlBinding.Part), 
			};
			mimeXmlReturn.TypeMapping = Importer.ImportTypeMapping(messagePart.Element);
			mimeXmlReturn.TypeName = mimeXmlReturn.TypeMapping.TypeFullName;
			mimeXmlReturn.ReaderType = typeof(XmlReturnReader);
			Exporter.AddMappingMetadata(mimeXmlReturn.Attributes, mimeXmlReturn.TypeMapping, string.Empty);
			return mimeXmlReturn;
		}
		return null;
	}

	internal override void GenerateCode(MimeReturn[] importedReturns, MimeParameterCollection[] importedParameters)
	{
		for (int i = 0; i < importedReturns.Length; i++)
		{
			if (importedReturns[i] is MimeXmlReturn)
			{
				GenerateCode((MimeXmlReturn)importedReturns[i]);
			}
		}
	}

	private void GenerateCode(MimeXmlReturn importedReturn)
	{
		Exporter.ExportTypeMapping(importedReturn.TypeMapping);
	}

	internal override void AddClassMetadata(CodeTypeDeclaration codeClass)
	{
		foreach (CodeAttributeDeclaration includeMetadatum in Exporter.IncludeMetadata)
		{
			codeClass.CustomAttributes.Add(includeMetadatum);
		}
	}
}
