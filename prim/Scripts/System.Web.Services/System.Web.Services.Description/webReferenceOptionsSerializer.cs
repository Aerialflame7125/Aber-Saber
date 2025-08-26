using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

internal sealed class webReferenceOptionsSerializer : XmlSerializer
{
	protected override XmlSerializationReader CreateReader()
	{
		return new WebReferenceOptionsSerializationReader();
	}

	protected override XmlSerializationWriter CreateWriter()
	{
		return new WebReferenceOptionsSerializationWriter();
	}

	public override bool CanDeserialize(XmlReader xmlReader)
	{
		return true;
	}

	protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
	{
		((WebReferenceOptionsSerializationWriter)writer).Write5_webReferenceOptions(objectToSerialize);
	}

	protected override object Deserialize(XmlSerializationReader reader)
	{
		return ((WebReferenceOptionsSerializationReader)reader).Read5_webReferenceOptions();
	}
}
