using System.Collections.Specialized;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

internal class SchemaCompiler
{
	private static StringCollection warnings;

	internal static StringCollection Warnings
	{
		get
		{
			if (warnings == null)
			{
				warnings = new StringCollection();
			}
			return warnings;
		}
	}

	internal static StringCollection Compile(XmlSchemas schemas)
	{
		AddImports(schemas);
		Warnings.Clear();
		schemas.Compile(ValidationCallbackWithErrorCode, fullCompile: true);
		return Warnings;
	}

	private static void AddImport(XmlSchema schema, string ns)
	{
		if (schema.TargetNamespace == ns)
		{
			return;
		}
		foreach (XmlSchemaExternal include in schema.Includes)
		{
			if (include is XmlSchemaImport xmlSchemaImport && xmlSchemaImport.Namespace == ns)
			{
				return;
			}
		}
		XmlSchemaImport xmlSchemaImport2 = new XmlSchemaImport();
		xmlSchemaImport2.Namespace = ns;
		schema.Includes.Add(xmlSchemaImport2);
	}

	private static void AddImports(XmlSchemas schemas)
	{
		foreach (XmlSchema schema in schemas)
		{
			AddImport(schema, "http://schemas.xmlsoap.org/soap/encoding/");
			AddImport(schema, "http://schemas.xmlsoap.org/wsdl/");
		}
	}

	internal static string WarningDetails(XmlSchemaException exception, string message)
	{
		XmlSchemaObject xmlSchemaObject = exception.SourceSchemaObject;
		if (exception.LineNumber == 0 && exception.LinePosition == 0)
		{
			return GetSchemaItem(xmlSchemaObject, null, message);
		}
		string text = null;
		if (xmlSchemaObject != null)
		{
			while (xmlSchemaObject.Parent != null)
			{
				xmlSchemaObject = xmlSchemaObject.Parent;
			}
			if (xmlSchemaObject is XmlSchema)
			{
				text = ((XmlSchema)xmlSchemaObject).TargetNamespace;
			}
		}
		return Res.GetString("SchemaSyntaxErrorDetails", text, message, exception.LineNumber, exception.LinePosition);
	}

	private static string GetSchemaItem(XmlSchemaObject o, string ns, string details)
	{
		if (o == null)
		{
			return null;
		}
		while (o.Parent != null && !(o.Parent is XmlSchema))
		{
			o = o.Parent;
		}
		if (ns == null || ns.Length == 0)
		{
			XmlSchemaObject xmlSchemaObject = o;
			while (xmlSchemaObject.Parent != null)
			{
				xmlSchemaObject = xmlSchemaObject.Parent;
			}
			if (xmlSchemaObject is XmlSchema)
			{
				ns = ((XmlSchema)xmlSchemaObject).TargetNamespace;
			}
		}
		string text = null;
		if (o is XmlSchemaNotation)
		{
			return Res.GetString("XmlSchemaNamedItem", ns, "notation", ((XmlSchemaNotation)o).Name, details);
		}
		if (o is XmlSchemaGroup)
		{
			return Res.GetString("XmlSchemaNamedItem", ns, "group", ((XmlSchemaGroup)o).Name, details);
		}
		if (o is XmlSchemaElement)
		{
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)o;
			if (xmlSchemaElement.Name == null || xmlSchemaElement.Name.Length == 0)
			{
				XmlQualifiedName parentName = GetParentName(o);
				return Res.GetString("XmlSchemaElementReference", xmlSchemaElement.RefName.ToString(), parentName.Name, parentName.Namespace);
			}
			return Res.GetString("XmlSchemaNamedItem", ns, "element", xmlSchemaElement.Name, details);
		}
		if (o is XmlSchemaType)
		{
			return Res.GetString("XmlSchemaNamedItem", ns, (o.GetType() == typeof(XmlSchemaSimpleType)) ? "simpleType" : "complexType", ((XmlSchemaType)o).Name, details);
		}
		if (o is XmlSchemaAttributeGroup)
		{
			return Res.GetString("XmlSchemaNamedItem", ns, "attributeGroup", ((XmlSchemaAttributeGroup)o).Name, details);
		}
		if (o is XmlSchemaAttribute)
		{
			XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)o;
			if (xmlSchemaAttribute.Name == null || xmlSchemaAttribute.Name.Length == 0)
			{
				XmlQualifiedName parentName2 = GetParentName(o);
				return Res.GetString("XmlSchemaAttributeReference", xmlSchemaAttribute.RefName.ToString(), parentName2.Name, parentName2.Namespace);
			}
			return Res.GetString("XmlSchemaNamedItem", ns, "attribute", xmlSchemaAttribute.Name, details);
		}
		if (o is XmlSchemaContent)
		{
			XmlQualifiedName parentName3 = GetParentName(o);
			return Res.GetString("XmlSchemaContentDef", parentName3.Name, parentName3.Namespace, details);
		}
		if (o is XmlSchemaExternal)
		{
			string text2 = ((o is XmlSchemaImport) ? "import" : ((o is XmlSchemaInclude) ? "include" : ((o is XmlSchemaRedefine) ? "redefine" : o.GetType().Name)));
			return Res.GetString("XmlSchemaItem", ns, text2, details);
		}
		if (o is XmlSchema)
		{
			return Res.GetString("XmlSchema", ns, details);
		}
		return Res.GetString("XmlSchemaNamedItem", ns, o.GetType().Name, null, details);
	}

	internal static XmlQualifiedName GetParentName(XmlSchemaObject item)
	{
		while (item.Parent != null)
		{
			if (item.Parent is XmlSchemaType)
			{
				XmlSchemaType xmlSchemaType = (XmlSchemaType)item.Parent;
				if (xmlSchemaType.Name != null && xmlSchemaType.Name.Length != 0)
				{
					return xmlSchemaType.QualifiedName;
				}
			}
			item = item.Parent;
		}
		return XmlQualifiedName.Empty;
	}

	private static void ValidationCallbackWithErrorCode(object sender, ValidationEventArgs args)
	{
		Warnings.Add(Res.GetString((args.Severity == XmlSeverityType.Error) ? "SchemaValidationError" : "SchemaValidationWarning", WarningDetails(args.Exception, args.Message)));
	}
}
