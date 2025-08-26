using System.Xml.Serialization;

namespace System.Web.Services.Description;

internal class GlobalSoapHeader
{
	internal string fieldName;

	internal XmlTypeMapping mapping;

	internal bool isEncoded;
}
