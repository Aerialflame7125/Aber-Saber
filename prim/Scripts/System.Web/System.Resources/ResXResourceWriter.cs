using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;

namespace System.Resources;

internal class ResXResourceWriter : IResourceWriter, IDisposable
{
	private string filename;

	private Stream stream;

	private TextWriter textwriter;

	private XmlTextWriter writer;

	private bool written;

	private string base_path;

	public static readonly string BinSerializedObjectMimeType = "application/x-microsoft.net.object.binary.base64";

	public static readonly string ByteArraySerializedObjectMimeType = "application/x-microsoft.net.object.bytearray.base64";

	public static readonly string DefaultSerializedObjectMimeType = BinSerializedObjectMimeType;

	public static readonly string ResMimeType = "text/microsoft-resx";

	public static readonly string ResourceSchema = schema;

	public static readonly string SoapSerializedObjectMimeType = "application/x-microsoft.net.object.soap.base64";

	public static readonly string Version = "2.0";

	private static string schema = "\n  <xsd:schema id='root' xmlns='' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:msdata='urn:schemas-microsoft-com:xml-msdata'>\n    <xsd:element name='root' msdata:IsDataSet='true'>\n      <xsd:complexType>\n        <xsd:choice maxOccurs='unbounded'>\n          <xsd:element name='data'>\n            <xsd:complexType>\n              <xsd:sequence>\n                <xsd:element name='value' type='xsd:string' minOccurs='0' msdata:Ordinal='1' />\n                <xsd:element name='comment' type='xsd:string' minOccurs='0' msdata:Ordinal='2' />\n              </xsd:sequence>\n              <xsd:attribute name='name' type='xsd:string' msdata:Ordinal='1' />\n              <xsd:attribute name='type' type='xsd:string' msdata:Ordinal='3' />\n              <xsd:attribute name='mimetype' type='xsd:string' msdata:Ordinal='4' />\n            </xsd:complexType>\n          </xsd:element>\n          <xsd:element name='resheader'>\n            <xsd:complexType>\n              <xsd:sequence>\n                <xsd:element name='value' type='xsd:string' minOccurs='0' msdata:Ordinal='1' />\n              </xsd:sequence>\n              <xsd:attribute name='name' type='xsd:string' use='required' />\n            </xsd:complexType>\n          </xsd:element>\n        </xsd:choice>\n      </xsd:complexType>\n    </xsd:element>\n  </xsd:schema>\n".Replace("'", "\"");

	public string BasePath
	{
		get
		{
			return base_path;
		}
		set
		{
			base_path = value;
		}
	}

	public ResXResourceWriter(Stream stream)
	{
		if (stream == null)
		{
			throw new ArgumentNullException("stream");
		}
		if (!stream.CanWrite)
		{
			throw new ArgumentException("stream is not writable.", "stream");
		}
		this.stream = stream;
	}

	public ResXResourceWriter(TextWriter textWriter)
	{
		if (textWriter == null)
		{
			throw new ArgumentNullException("textWriter");
		}
		textwriter = textWriter;
	}

	public ResXResourceWriter(string fileName)
	{
		if (fileName == null)
		{
			throw new ArgumentNullException("fileName");
		}
		filename = fileName;
	}

	~ResXResourceWriter()
	{
		Dispose(disposing: false);
	}

	private void InitWriter()
	{
		if (filename != null)
		{
			stream = File.Open(filename, FileMode.Create);
		}
		if (textwriter == null)
		{
			textwriter = new StreamWriter(stream, Encoding.UTF8);
		}
		writer = new XmlTextWriter(textwriter);
		writer.Formatting = Formatting.Indented;
		writer.WriteStartDocument();
		writer.WriteStartElement("root");
		writer.WriteRaw(schema);
		WriteHeader("resmimetype", "text/microsoft-resx");
		WriteHeader("version", "1.3");
		WriteHeader("reader", typeof(ResXResourceReader).AssemblyQualifiedName);
		WriteHeader("writer", typeof(ResXResourceWriter).AssemblyQualifiedName);
	}

	private void WriteHeader(string name, string value)
	{
		writer.WriteStartElement("resheader");
		writer.WriteAttributeString("name", name);
		writer.WriteStartElement("value");
		writer.WriteString(value);
		writer.WriteEndElement();
		writer.WriteEndElement();
	}

	private void WriteNiceBase64(byte[] value, int offset, int length)
	{
		string text = Convert.ToBase64String(value, offset, length);
		StringBuilder stringBuilder = new StringBuilder(text, text.Length + (text.Length + 160) / 80 * 3);
		int i = 0;
		int num = 80 + Environment.NewLine.Length + 1;
		string value2 = Environment.NewLine + "\t";
		for (; i < stringBuilder.Length; i += num)
		{
			stringBuilder.Insert(i, value2);
		}
		stringBuilder.Insert(stringBuilder.Length, Environment.NewLine);
		writer.WriteString(stringBuilder.ToString());
	}

	private void WriteBytes(string name, Type type, byte[] value, int offset, int length)
	{
		WriteBytes(name, type, value, offset, length, string.Empty);
	}

	private void WriteBytes(string name, Type type, byte[] value, int offset, int length, string comment)
	{
		writer.WriteStartElement("data");
		writer.WriteAttributeString("name", name);
		if (type != null)
		{
			writer.WriteAttributeString("type", type.AssemblyQualifiedName);
			if (type != typeof(byte[]))
			{
				writer.WriteAttributeString("mimetype", ByteArraySerializedObjectMimeType);
			}
			writer.WriteStartElement("value");
			WriteNiceBase64(value, offset, length);
		}
		else
		{
			writer.WriteAttributeString("mimetype", BinSerializedObjectMimeType);
			writer.WriteStartElement("value");
			writer.WriteBase64(value, offset, length);
		}
		writer.WriteEndElement();
		if (comment != null && !comment.Equals(string.Empty))
		{
			writer.WriteStartElement("comment");
			writer.WriteString(comment);
			writer.WriteEndElement();
		}
		writer.WriteEndElement();
	}

	private void WriteBytes(string name, Type type, byte[] value, string comment)
	{
		WriteBytes(name, type, value, 0, value.Length, comment);
	}

	private void WriteString(string name, string value)
	{
		WriteString(name, value, null);
	}

	private void WriteString(string name, string value, Type type)
	{
		WriteString(name, value, type, string.Empty);
	}

	private void WriteString(string name, string value, Type type, string comment)
	{
		writer.WriteStartElement("data");
		writer.WriteAttributeString("name", name);
		if (type != null)
		{
			writer.WriteAttributeString("type", type.AssemblyQualifiedName);
		}
		writer.WriteStartElement("value");
		writer.WriteString(value);
		writer.WriteEndElement();
		if (comment != null && !comment.Equals(string.Empty))
		{
			writer.WriteStartElement("comment");
			writer.WriteString(comment);
			writer.WriteEndElement();
		}
		writer.WriteEndElement();
		writer.WriteWhitespace("\n  ");
	}

	public void AddResource(string name, byte[] value)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (written)
		{
			throw new InvalidOperationException("The resource is already generated.");
		}
		if (writer == null)
		{
			InitWriter();
		}
		WriteBytes(name, value.GetType(), value, null);
	}

	public void AddResource(string name, object value)
	{
		AddResource(name, value, string.Empty);
	}

	private void AddResource(string name, object value, string comment)
	{
		if (value is string)
		{
			AddResource(name, (string)value, comment);
			return;
		}
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		if (value != null && !value.GetType().IsSerializable)
		{
			throw new InvalidOperationException($"The element '{name}' of type '{value.GetType().Name}' is not serializable.");
		}
		if (written)
		{
			throw new InvalidOperationException("The resource is already generated.");
		}
		if (writer == null)
		{
			InitWriter();
		}
		if (value is byte[])
		{
			WriteBytes(name, value.GetType(), (byte[])value, comment);
			return;
		}
		if (value == null)
		{
			WriteString(name, "", typeof(ResXNullRef), comment);
			return;
		}
		TypeConverter converter = TypeDescriptor.GetConverter(value);
		if (value is ResXFileRef)
		{
			ResXFileRef value2 = ProcessFileRefBasePath((ResXFileRef)value);
			string value3 = converter.ConvertToInvariantString(value2);
			WriteString(name, value3, value.GetType(), comment);
			return;
		}
		if (converter != null && converter.CanConvertTo(typeof(string)) && converter.CanConvertFrom(typeof(string)))
		{
			string value4 = converter.ConvertToInvariantString(value);
			WriteString(name, value4, value.GetType(), comment);
			return;
		}
		if (converter != null && converter.CanConvertTo(typeof(byte[])) && converter.CanConvertFrom(typeof(byte[])))
		{
			byte[] value5 = (byte[])converter.ConvertTo(value, typeof(byte[]));
			WriteBytes(name, value.GetType(), value5, comment);
			return;
		}
		MemoryStream memoryStream = new MemoryStream();
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		try
		{
			binaryFormatter.Serialize(memoryStream, value);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException(string.Concat("Cannot add a ", value.GetType(), "because it cannot be serialized: ", ex.Message));
		}
		WriteBytes(name, null, memoryStream.GetBuffer(), 0, (int)memoryStream.Length, comment);
		memoryStream.Close();
	}

	public void AddResource(string name, string value)
	{
		AddResource(name, value, string.Empty);
	}

	private void AddResource(string name, string value, string comment)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (written)
		{
			throw new InvalidOperationException("The resource is already generated.");
		}
		if (writer == null)
		{
			InitWriter();
		}
		WriteString(name, value, null, comment);
	}

	[MonoTODO("Stub, not implemented")]
	public virtual void AddAlias(string aliasName, AssemblyName assemblyName)
	{
	}

	public void AddResource(ResXDataNode node)
	{
		if (node == null)
		{
			throw new ArgumentNullException("node");
		}
		if (writer == null)
		{
			InitWriter();
		}
		if (node.IsWritable)
		{
			WriteWritableNode(node);
		}
		else if (node.FileRef != null)
		{
			AddResource(node.Name, node.FileRef, node.Comment);
		}
		else
		{
			AddResource(node.Name, node.GetValue((AssemblyName[])null), node.Comment);
		}
	}

	private ResXFileRef ProcessFileRefBasePath(ResXFileRef fileRef)
	{
		if (string.IsNullOrEmpty(BasePath))
		{
			return fileRef;
		}
		return new ResXFileRef(AbsoluteToRelativePath(BasePath, fileRef.FileName), fileRef.TypeName, fileRef.TextFileEncoding);
	}

	private static bool IsSeparator(char ch)
	{
		if (ch != Path.DirectorySeparatorChar && ch != Path.AltDirectorySeparatorChar)
		{
			return ch == Path.VolumeSeparatorChar;
		}
		return true;
	}

	private unsafe static string AbsoluteToRelativePath(string baseDirectoryPath, string absPath)
	{
		if (string.IsNullOrEmpty(baseDirectoryPath))
		{
			return absPath;
		}
		baseDirectoryPath = baseDirectoryPath.TrimEnd(Path.DirectorySeparatorChar);
		fixed (char* ptr = baseDirectoryPath)
		{
			fixed (char* ptr3 = absPath)
			{
				char* ptr2 = ptr + baseDirectoryPath.Length;
				char* ptr4 = ptr3 + absPath.Length;
				char* ptr5 = ptr4;
				char* ptr6 = ptr2;
				int num = 0;
				char* ptr7 = ptr3;
				char* ptr8 = ptr;
				while (ptr7 < ptr4 && *ptr7 == *ptr8)
				{
					if (IsSeparator(*ptr7))
					{
						num++;
						ptr5 = ptr7 + 1;
						ptr6 = ptr8;
					}
					ptr7++;
					ptr8++;
					if (ptr8 >= ptr2)
					{
						if (ptr7 >= ptr4 || IsSeparator(*ptr7))
						{
							num++;
							ptr5 = ptr7 + 1;
							ptr6 = ptr8;
						}
						break;
					}
				}
				if (num == 0)
				{
					return absPath;
				}
				if (ptr5 >= ptr4)
				{
					return ".";
				}
				if (ptr7 >= ptr4 && IsSeparator(*ptr8))
				{
					ptr5 = ptr7 + 1;
					ptr6 = ptr8;
				}
				int num2 = 0;
				for (; ptr6 < ptr2; ptr6++)
				{
					if (IsSeparator(*ptr6))
					{
						num2++;
					}
				}
				char[] array = new char[num2 * 2 + num2 + ptr4 - ptr5];
				fixed (char* ptr9 = array)
				{
					char* ptr10 = ptr9;
					for (int i = 0; i < num2; i++)
					{
						*(ptr10++) = '.';
						*(ptr10++) = '.';
						*(ptr10++) = Path.DirectorySeparatorChar;
					}
					while (ptr5 < ptr4)
					{
						*(ptr10++) = *(ptr5++);
					}
				}
				return new string(array);
			}
		}
	}

	private void WriteWritableNode(ResXDataNode node)
	{
		writer.WriteStartElement("data");
		writer.WriteAttributeString("name", node.Name);
		if (node.Type != null && !node.Type.Equals(string.Empty))
		{
			writer.WriteAttributeString("type", node.Type);
		}
		if (node.MimeType != null && !node.MimeType.Equals(string.Empty))
		{
			writer.WriteAttributeString("mimetype", node.MimeType);
		}
		writer.WriteStartElement("value");
		writer.WriteString(node.DataString);
		writer.WriteEndElement();
		if (node.Comment != null && !node.Comment.Equals(string.Empty))
		{
			writer.WriteStartElement("comment");
			writer.WriteString(node.Comment);
			writer.WriteEndElement();
		}
		writer.WriteEndElement();
		writer.WriteWhitespace("\n  ");
	}

	public void AddMetadata(string name, string value)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (written)
		{
			throw new InvalidOperationException("The resource is already generated.");
		}
		if (writer == null)
		{
			InitWriter();
		}
		writer.WriteStartElement("metadata");
		writer.WriteAttributeString("name", name);
		writer.WriteAttributeString("xml:space", "preserve");
		writer.WriteElementString("value", value);
		writer.WriteEndElement();
	}

	public void AddMetadata(string name, byte[] value)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (written)
		{
			throw new InvalidOperationException("The resource is already generated.");
		}
		if (writer == null)
		{
			InitWriter();
		}
		writer.WriteStartElement("metadata");
		writer.WriteAttributeString("name", name);
		writer.WriteAttributeString("type", value.GetType().AssemblyQualifiedName);
		writer.WriteStartElement("value");
		WriteNiceBase64(value, 0, value.Length);
		writer.WriteEndElement();
		writer.WriteEndElement();
	}

	public void AddMetadata(string name, object value)
	{
		if (value is string)
		{
			AddMetadata(name, (string)value);
			return;
		}
		if (value is byte[])
		{
			AddMetadata(name, (byte[])value);
			return;
		}
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (!value.GetType().IsSerializable)
		{
			throw new InvalidOperationException($"The element '{name}' of type '{value.GetType().Name}' is not serializable.");
		}
		if (written)
		{
			throw new InvalidOperationException("The resource is already generated.");
		}
		if (writer == null)
		{
			InitWriter();
		}
		Type type = value.GetType();
		TypeConverter converter = TypeDescriptor.GetConverter(value);
		if (converter != null && converter.CanConvertTo(typeof(string)) && converter.CanConvertFrom(typeof(string)))
		{
			string text = converter.ConvertToInvariantString(value);
			writer.WriteStartElement("metadata");
			writer.WriteAttributeString("name", name);
			if (type != null)
			{
				writer.WriteAttributeString("type", type.AssemblyQualifiedName);
			}
			writer.WriteStartElement("value");
			writer.WriteString(text);
			writer.WriteEndElement();
			writer.WriteEndElement();
			writer.WriteWhitespace("\n  ");
			return;
		}
		if (converter != null && converter.CanConvertTo(typeof(byte[])) && converter.CanConvertFrom(typeof(byte[])))
		{
			byte[] array = (byte[])converter.ConvertTo(value, typeof(byte[]));
			writer.WriteStartElement("metadata");
			writer.WriteAttributeString("name", name);
			if (type != null)
			{
				writer.WriteAttributeString("type", type.AssemblyQualifiedName);
				writer.WriteAttributeString("mimetype", ByteArraySerializedObjectMimeType);
				writer.WriteStartElement("value");
				WriteNiceBase64(array, 0, array.Length);
			}
			else
			{
				writer.WriteAttributeString("mimetype", BinSerializedObjectMimeType);
				writer.WriteStartElement("value");
				writer.WriteBase64(array, 0, array.Length);
			}
			writer.WriteEndElement();
			writer.WriteEndElement();
			return;
		}
		MemoryStream memoryStream = new MemoryStream();
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		try
		{
			binaryFormatter.Serialize(memoryStream, value);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException(string.Concat("Cannot add a ", value.GetType(), "because it cannot be serialized: ", ex.Message));
		}
		writer.WriteStartElement("metadata");
		writer.WriteAttributeString("name", name);
		if (type != null)
		{
			writer.WriteAttributeString("type", type.AssemblyQualifiedName);
			writer.WriteAttributeString("mimetype", ByteArraySerializedObjectMimeType);
			writer.WriteStartElement("value");
			WriteNiceBase64(memoryStream.GetBuffer(), 0, memoryStream.GetBuffer().Length);
		}
		else
		{
			writer.WriteAttributeString("mimetype", BinSerializedObjectMimeType);
			writer.WriteStartElement("value");
			writer.WriteBase64(memoryStream.GetBuffer(), 0, memoryStream.GetBuffer().Length);
		}
		writer.WriteEndElement();
		writer.WriteEndElement();
		memoryStream.Close();
	}

	public void Close()
	{
		if (!written)
		{
			Generate();
		}
		if (writer != null)
		{
			writer.Close();
			stream = null;
			filename = null;
			textwriter = null;
		}
	}

	public virtual void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	public void Generate()
	{
		if (written)
		{
			throw new InvalidOperationException("The resource is already generated.");
		}
		written = true;
		writer.WriteEndElement();
		writer.Flush();
	}

	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
		{
			Close();
		}
	}
}
