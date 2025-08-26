using System.Collections.Specialized;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

internal class WebReferenceOptionsSerializationWriter : XmlSerializationWriter
{
	private string Write1_CodeGenerationOptions(CodeGenerationOptions v)
	{
		string text = null;
		return v switch
		{
			CodeGenerationOptions.GenerateProperties => "properties", 
			CodeGenerationOptions.GenerateNewAsync => "newAsync", 
			CodeGenerationOptions.GenerateOldAsync => "oldAsync", 
			CodeGenerationOptions.GenerateOrder => "order", 
			CodeGenerationOptions.EnableDataBinding => "enableDataBinding", 
			_ => XmlSerializationWriter.FromEnum((long)v, new string[5] { "properties", "newAsync", "oldAsync", "order", "enableDataBinding" }, new long[5] { 1L, 2L, 4L, 8L, 16L }, "System.Xml.Serialization.CodeGenerationOptions"), 
		};
	}

	private string Write2_ServiceDescriptionImportStyle(ServiceDescriptionImportStyle v)
	{
		string text = null;
		return v switch
		{
			ServiceDescriptionImportStyle.Client => "client", 
			ServiceDescriptionImportStyle.Server => "server", 
			ServiceDescriptionImportStyle.ServerInterface => "serverInterface", 
			_ => throw CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Web.Services.Description.ServiceDescriptionImportStyle"), 
		};
	}

	private void Write4_WebReferenceOptions(string n, string ns, WebReferenceOptions o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(WebReferenceOptions)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o);
		if (needType)
		{
			WriteXsiType("webReferenceOptions", "http://microsoft.com/webReference/");
		}
		if (o.CodeGenerationOptions != CodeGenerationOptions.GenerateOldAsync)
		{
			WriteElementString("codeGenerationOptions", "http://microsoft.com/webReference/", Write1_CodeGenerationOptions(o.CodeGenerationOptions));
		}
		StringCollection schemaImporterExtensions = o.SchemaImporterExtensions;
		if (schemaImporterExtensions != null)
		{
			WriteStartElement("schemaImporterExtensions", "http://microsoft.com/webReference/");
			for (int i = 0; i < schemaImporterExtensions.Count; i++)
			{
				WriteNullableStringLiteral("type", "http://microsoft.com/webReference/", schemaImporterExtensions[i]);
			}
			WriteEndElement();
		}
		if (o.Style != 0)
		{
			WriteElementString("style", "http://microsoft.com/webReference/", Write2_ServiceDescriptionImportStyle(o.Style));
		}
		WriteElementStringRaw("verbose", "http://microsoft.com/webReference/", XmlConvert.ToString(o.Verbose));
		WriteEndElement(o);
	}

	protected override void InitCallbacks()
	{
	}

	internal void Write5_webReferenceOptions(object o)
	{
		WriteStartDocument();
		if (o == null)
		{
			WriteNullTagLiteral("webReferenceOptions", "http://microsoft.com/webReference/");
			return;
		}
		TopLevelElement();
		Write4_WebReferenceOptions("webReferenceOptions", "http://microsoft.com/webReference/", (WebReferenceOptions)o, isNullable: true, needType: false);
	}
}
