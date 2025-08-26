using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace System.Web.Util;

internal class SerializationHelper
{
	internal string SerializeToBase64(object value)
	{
		return Convert.ToBase64String(SerializeToBinary(value));
	}

	internal object DeserializeFromBase64(string value)
	{
		return DeserializeFromBinary(Convert.FromBase64String(value));
	}

	internal string SerializeToXml(object value)
	{
		using MemoryStream memoryStream = new MemoryStream();
		new XmlSerializer(typeof(object), "http://www.nauck-it.de/PostgreSQLProvider").Serialize(memoryStream, value);
		return Convert.ToBase64String(memoryStream.ToArray());
	}

	internal object DeserializeFromXml(string value)
	{
		using MemoryStream stream = new MemoryStream(Convert.FromBase64String(value));
		return new XmlSerializer(typeof(object), "http://www.nauck-it.de/PostgreSQLProvider").Deserialize(stream);
	}

	internal byte[] SerializeToBinary(object value)
	{
		using MemoryStream memoryStream = new MemoryStream();
		new BinaryFormatter().Serialize(memoryStream, value);
		return memoryStream.ToArray();
	}

	internal object DeserializeFromBinary(byte[] value)
	{
		using MemoryStream serializationStream = new MemoryStream(value);
		return new BinaryFormatter().Deserialize(serializationStream);
	}
}
