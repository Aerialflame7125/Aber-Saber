using System.Reflection;
using System.Web.Services.Configuration;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;

namespace System.Web.Services.Description;

internal abstract class HttpProtocolReflector : ProtocolReflector
{
	private MimeReflector[] reflectors;

	internal string MethodUrl
	{
		get
		{
			string text = base.Method.MethodAttribute.MessageName;
			if (text.Length == 0)
			{
				text = base.Method.Name;
			}
			return "/" + text;
		}
	}

	protected HttpProtocolReflector()
	{
		Type[] mimeReflectorTypes = WebServicesSection.Current.MimeReflectorTypes;
		reflectors = new MimeReflector[mimeReflectorTypes.Length];
		for (int i = 0; i < reflectors.Length; i++)
		{
			MimeReflector mimeReflector = (MimeReflector)Activator.CreateInstance(mimeReflectorTypes[i]);
			mimeReflector.ReflectionContext = this;
			reflectors[i] = mimeReflector;
		}
	}

	protected bool ReflectMimeParameters()
	{
		bool result = false;
		for (int i = 0; i < reflectors.Length; i++)
		{
			if (reflectors[i].ReflectParameters())
			{
				result = true;
			}
		}
		return result;
	}

	protected bool ReflectMimeReturn()
	{
		if (base.Method.ReturnType == typeof(void))
		{
			_ = base.OutputMessage;
			return true;
		}
		bool result = false;
		for (int i = 0; i < reflectors.Length; i++)
		{
			if (reflectors[i].ReflectReturn())
			{
				result = true;
				break;
			}
		}
		return result;
	}

	protected bool ReflectUrlParameters()
	{
		if (!HttpServerProtocol.AreUrlParametersSupported(base.Method))
		{
			return false;
		}
		ReflectStringParametersMessage();
		base.OperationBinding.Input.Extensions.Add(new HttpUrlEncodedBinding());
		return true;
	}

	internal void ReflectStringParametersMessage()
	{
		Message message = base.InputMessage;
		ParameterInfo[] inParameters = base.Method.InParameters;
		foreach (ParameterInfo parameterInfo in inParameters)
		{
			MessagePart messagePart = new MessagePart();
			messagePart.Name = XmlConvert.EncodeLocalName(parameterInfo.Name);
			if (parameterInfo.ParameterType.IsArray)
			{
				string defaultNamespace = base.DefaultNamespace;
				defaultNamespace = ((!defaultNamespace.EndsWith("/", StringComparison.Ordinal)) ? (defaultNamespace + "/AbstractTypes") : (defaultNamespace + "AbstractTypes"));
				string name = "StringArray";
				if (!base.ServiceDescription.Types.Schemas.Contains(defaultNamespace))
				{
					XmlSchema xmlSchema = new XmlSchema();
					xmlSchema.TargetNamespace = defaultNamespace;
					base.ServiceDescription.Types.Schemas.Add(xmlSchema);
					XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
					xmlSchemaElement.Name = "String";
					xmlSchemaElement.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
					xmlSchemaElement.MinOccurs = 0m;
					xmlSchemaElement.MaxOccurs = decimal.MaxValue;
					XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
					xmlSchemaSequence.Items.Add(xmlSchemaElement);
					XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = new XmlSchemaComplexContentRestriction();
					xmlSchemaComplexContentRestriction.BaseTypeName = new XmlQualifiedName("Array", "http://schemas.xmlsoap.org/soap/encoding/");
					xmlSchemaComplexContentRestriction.Particle = xmlSchemaSequence;
					XmlSchemaImport xmlSchemaImport = new XmlSchemaImport();
					xmlSchemaImport.Namespace = xmlSchemaComplexContentRestriction.BaseTypeName.Namespace;
					XmlSchemaComplexContent xmlSchemaComplexContent = new XmlSchemaComplexContent();
					xmlSchemaComplexContent.Content = xmlSchemaComplexContentRestriction;
					XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
					xmlSchemaComplexType.Name = name;
					xmlSchemaComplexType.ContentModel = xmlSchemaComplexContent;
					xmlSchema.Items.Add(xmlSchemaComplexType);
					xmlSchema.Includes.Add(xmlSchemaImport);
				}
				messagePart.Type = new XmlQualifiedName(name, defaultNamespace);
			}
			else
			{
				messagePart.Type = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
			}
			message.Parts.Add(messagePart);
		}
	}
}
