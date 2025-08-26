using System.Xml.Serialization;

namespace System.Web.Services.Description;

internal class MimeXmlReturn : MimeReturn
{
	private XmlTypeMapping mapping;

	internal XmlTypeMapping TypeMapping
	{
		get
		{
			return mapping;
		}
		set
		{
			mapping = value;
		}
	}
}
