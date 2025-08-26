using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

namespace System.Resources;

/// <summary>Enumerates XML resource (.resx) files and streams, and reads the sequential resource name and value pairs.</summary>
public class ResXResourceReader : IDisposable, IResourceReader, IEnumerable
{
	private class ResXHeader
	{
		private string resMimeType;

		private string reader;

		private string version;

		private string writer;

		public string ResMimeType
		{
			get
			{
				return resMimeType;
			}
			set
			{
				resMimeType = value;
			}
		}

		public string Reader
		{
			get
			{
				return reader;
			}
			set
			{
				reader = value;
			}
		}

		public string Version
		{
			get
			{
				return version;
			}
			set
			{
				version = value;
			}
		}

		public string Writer
		{
			get
			{
				return writer;
			}
			set
			{
				writer = value;
			}
		}

		public bool IsValid
		{
			get
			{
				if (string.Compare(ResMimeType, ResXResourceWriter.ResMimeType) != 0)
				{
					return false;
				}
				if (Reader == null || Writer == null)
				{
					return false;
				}
				string text = Reader.Split(',')[0].Trim();
				if (text != typeof(ResXResourceReader).FullName)
				{
					return false;
				}
				string text2 = Writer.Split(',')[0].Trim();
				if (text2 != typeof(ResXResourceWriter).FullName)
				{
					return false;
				}
				return true;
			}
		}

		public void Verify()
		{
			if (!IsValid)
			{
				throw new ArgumentException("Invalid ResX input.  Could not find valid \"resheader\" tags for the ResX reader & writer type names.");
			}
		}
	}

	private string fileName;

	private Stream stream;

	private TextReader reader;

	private Hashtable hasht;

	private ITypeResolutionService typeresolver;

	private XmlTextReader xmlReader;

	private string basepath;

	private bool useResXDataNodes;

	private AssemblyName[] assemblyNames;

	private Hashtable hashtm;

	/// <summary>Gets or sets the base path for the relative file path specified in a <see cref="T:System.Resources.ResXFileRef" /> object.</summary>
	/// <returns>A path that, if prepended to the relative file path specified in a <see cref="T:System.Resources.ResXFileRef" /> object, yields an absolute path to a resource file.</returns>
	/// <exception cref="T:System.InvalidOperationException">In a set operation, a value cannot be specified because the XML resource file has already been accessed and is in use.</exception>
	public string BasePath
	{
		get
		{
			return basepath;
		}
		set
		{
			basepath = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether <see cref="T:System.Resources.ResXDataNode" /> objects are returned when reading the current XML resource file or stream.</summary>
	/// <returns>true if resource data nodes are retrieved; false if resource data nodes are ignored.</returns>
	/// <exception cref="T:System.InvalidOperationException">In a set operation, the enumerator for the resource file or stream is already open.</exception>
	public bool UseResXDataNodes
	{
		get
		{
			return useResXDataNodes;
		}
		set
		{
			if (xmlReader != null)
			{
				throw new InvalidOperationException();
			}
			useResXDataNodes = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class for the specified stream.</summary>
	/// <param name="stream">An input stream that contains resources. </param>
	public ResXResourceReader(Stream stream)
	{
		if (stream == null)
		{
			throw new ArgumentNullException("stream");
		}
		if (!stream.CanRead)
		{
			throw new ArgumentException("Stream was not readable.");
		}
		this.stream = stream;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class using an input stream and a type resolution service.  </summary>
	/// <param name="stream">An input stream that contains resources. </param>
	/// <param name="typeResolver">An object that resolves type names specified in a resource.</param>
	public ResXResourceReader(Stream stream, ITypeResolutionService typeResolver)
		: this(stream)
	{
		typeresolver = typeResolver;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class for the specified resource file.</summary>
	/// <param name="fileName">The path of the resource file to read. </param>
	public ResXResourceReader(string fileName)
	{
		this.fileName = fileName;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class using a file name and a type resolution service. </summary>
	/// <param name="fileName">The name of an XML resource file that contains resources. </param>
	/// <param name="typeResolver">An object that resolves type names specified in a resource.</param>
	public ResXResourceReader(string fileName, ITypeResolutionService typeResolver)
		: this(fileName)
	{
		typeresolver = typeResolver;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class for the specified <see cref="T:System.IO.TextReader" />.</summary>
	/// <param name="reader">A text input stream that contains resources. </param>
	public ResXResourceReader(TextReader reader)
	{
		this.reader = reader;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class using a text stream reader and a type resolution service.  </summary>
	/// <param name="reader">A text stream reader that contains resources. </param>
	/// <param name="typeResolver">An object that resolves type names specified in a resource.</param>
	public ResXResourceReader(TextReader reader, ITypeResolutionService typeResolver)
		: this(reader)
	{
		typeresolver = typeResolver;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class using a stream and an array of assembly names. </summary>
	/// <param name="stream">An input stream that contains resources. </param>
	/// <param name="assemblyNames">An array of <see cref="T:System.Reflection.AssemblyName" /> objects that specifies one or more assemblies. The assemblies are used to resolve a type name in the resource to an actual type. </param>
	public ResXResourceReader(Stream stream, AssemblyName[] assemblyNames)
		: this(stream)
	{
		this.assemblyNames = assemblyNames;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class using an XML resource file name and an array of assembly names. </summary>
	/// <param name="fileName">The name of an XML resource file that contains resources. </param>
	/// <param name="assemblyNames">An array of <see cref="T:System.Reflection.AssemblyName" /> objects that specifies one or more assemblies. The assemblies are used to resolve a type name in the resource to an actual type. </param>
	public ResXResourceReader(string fileName, AssemblyName[] assemblyNames)
		: this(fileName)
	{
		this.assemblyNames = assemblyNames;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class using a <see cref="T:System.IO.TextReader" /> object and an array of assembly names.</summary>
	/// <param name="reader">An object used to read resources from a stream of text. </param>
	/// <param name="assemblyNames">An array of <see cref="T:System.Reflection.AssemblyName" /> objects that specifies one or more assemblies. The assemblies are used to resolve a type name in the resource to an actual type. </param>
	public ResXResourceReader(TextReader reader, AssemblyName[] assemblyNames)
		: this(reader)
	{
		this.assemblyNames = assemblyNames;
	}

	/// <summary>Returns an enumerator for the current <see cref="T:System.Resources.ResXResourceReader" /> object. For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />. </summary>
	/// <returns>An enumerator that can iterate through the name/value pairs in the XML resource (.resx) stream or string associated with the current <see cref="T:System.Resources.ResXResourceReader" /> object.</returns>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IResourceReader)this).GetEnumerator();
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Resources.ResXResourceReader" /> and optionally releases the managed resources. For a description of this member, see <see cref="M:System.IDisposable.Dispose" />. </summary>
	void IDisposable.Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	/// <summary>This member overrides the <see cref="M:System.Object.Finalize" /> method. </summary>
	~ResXResourceReader()
	{
		Dispose(disposing: false);
	}

	private void LoadData()
	{
		hasht = new Hashtable();
		hashtm = new Hashtable();
		if (fileName != null)
		{
			stream = File.OpenRead(fileName);
		}
		try
		{
			xmlReader = null;
			if (stream != null)
			{
				xmlReader = new XmlTextReader(stream);
			}
			else if (reader != null)
			{
				xmlReader = new XmlTextReader(reader);
			}
			if (xmlReader == null)
			{
				throw new InvalidOperationException("ResourceReader is closed.");
			}
			xmlReader.WhitespaceHandling = WhitespaceHandling.None;
			ResXHeader resXHeader = new ResXHeader();
			try
			{
				while (xmlReader.Read())
				{
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						switch (xmlReader.LocalName)
						{
						case "resheader":
							ParseHeaderNode(resXHeader);
							break;
						case "data":
							ParseDataNode(meta: false);
							break;
						case "metadata":
							ParseDataNode(meta: true);
							break;
						}
					}
				}
			}
			catch (XmlException innerException)
			{
				throw new ArgumentException("Invalid ResX input.", innerException);
			}
			catch (Exception ex)
			{
				XmlException innerException2 = new XmlException(ex.Message, ex, xmlReader.LineNumber, xmlReader.LinePosition);
				throw new ArgumentException("Invalid ResX input.", innerException2);
			}
			resXHeader.Verify();
		}
		finally
		{
			if (fileName != null)
			{
				stream.Close();
				stream = null;
			}
			xmlReader = null;
		}
	}

	private void ParseHeaderNode(ResXHeader header)
	{
		string attribute = GetAttribute("name");
		if (attribute != null)
		{
			if (string.Compare(attribute, "resmimetype", ignoreCase: true) == 0)
			{
				header.ResMimeType = GetHeaderValue();
			}
			else if (string.Compare(attribute, "reader", ignoreCase: true) == 0)
			{
				header.Reader = GetHeaderValue();
			}
			else if (string.Compare(attribute, "version", ignoreCase: true) == 0)
			{
				header.Version = GetHeaderValue();
			}
			else if (string.Compare(attribute, "writer", ignoreCase: true) == 0)
			{
				header.Writer = GetHeaderValue();
			}
		}
	}

	private string GetHeaderValue()
	{
		string text = null;
		xmlReader.ReadStartElement();
		if (xmlReader.NodeType == XmlNodeType.Element)
		{
			return xmlReader.ReadElementString();
		}
		return xmlReader.Value.Trim();
	}

	private string GetAttribute(string name)
	{
		if (!xmlReader.HasAttributes)
		{
			return null;
		}
		for (int i = 0; i < xmlReader.AttributeCount; i++)
		{
			xmlReader.MoveToAttribute(i);
			if (string.Compare(xmlReader.Name, name, ignoreCase: true) == 0)
			{
				string value = xmlReader.Value;
				xmlReader.MoveToElement();
				return value;
			}
		}
		xmlReader.MoveToElement();
		return null;
	}

	private string GetDataValue(bool meta, out string comment)
	{
		string result = null;
		comment = null;
		while (xmlReader.Read() && (xmlReader.NodeType != XmlNodeType.EndElement || !(xmlReader.LocalName == ((!meta) ? "data" : "metadata"))))
		{
			if (xmlReader.NodeType == XmlNodeType.Element)
			{
				if (xmlReader.Name.Equals("value"))
				{
					xmlReader.WhitespaceHandling = WhitespaceHandling.Significant;
					result = xmlReader.ReadString();
					xmlReader.WhitespaceHandling = WhitespaceHandling.None;
				}
				else if (xmlReader.Name.Equals("comment"))
				{
					xmlReader.WhitespaceHandling = WhitespaceHandling.Significant;
					comment = xmlReader.ReadString();
					xmlReader.WhitespaceHandling = WhitespaceHandling.None;
					if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.LocalName == ((!meta) ? "data" : "metadata"))
					{
						break;
					}
				}
			}
			else
			{
				result = xmlReader.Value.Trim();
			}
		}
		return result;
	}

	private void ParseDataNode(bool meta)
	{
		Hashtable hashtable = ((!meta || useResXDataNodes) ? hasht : hashtm);
		Point position = new Point(xmlReader.LineNumber, xmlReader.LinePosition);
		string attribute = GetAttribute("name");
		string attribute2 = GetAttribute("type");
		string attribute3 = GetAttribute("mimetype");
		Type type = ((attribute2 != null) ? ResolveType(attribute2) : null);
		if (attribute2 != null && (object)type == null)
		{
			throw new ArgumentException($"The type '{attribute2}' of the element '{attribute}' could not be resolved.");
		}
		if ((object)type == typeof(ResXNullRef))
		{
			if (useResXDataNodes)
			{
				hashtable[attribute] = new ResXDataNode(attribute, null, position);
			}
			else
			{
				hashtable[attribute] = null;
			}
			return;
		}
		string comment = null;
		string dataValue = GetDataValue(meta, out comment);
		object obj = null;
		if (attribute3 != null && attribute3.Length > 0)
		{
			if (attribute3 == ResXResourceWriter.BinSerializedObjectMimeType)
			{
				byte[] buffer = Convert.FromBase64String(dataValue);
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				using MemoryStream serializationStream = new MemoryStream(buffer);
				obj = binaryFormatter.Deserialize(serializationStream);
			}
			else if (attribute3 == ResXResourceWriter.ByteArraySerializedObjectMimeType && (object)type != null)
			{
				TypeConverter converter = TypeDescriptor.GetConverter(type);
				if (converter.CanConvertFrom(typeof(byte[])))
				{
					obj = converter.ConvertFrom(Convert.FromBase64String(dataValue));
				}
			}
		}
		else if ((object)type != null)
		{
			if ((object)type == typeof(byte[]))
			{
				obj = Convert.FromBase64String(dataValue);
			}
			else
			{
				TypeConverter converter2 = TypeDescriptor.GetConverter(type);
				if (converter2.CanConvertFrom(typeof(string)))
				{
					if (BasePath != null && (object)type == typeof(ResXFileRef))
					{
						string[] array = ResXFileRef.Parse(dataValue);
						array[0] = Path.Combine(BasePath, array[0]);
						obj = converter2.ConvertFromInvariantString(string.Join(";", array));
					}
					else
					{
						obj = converter2.ConvertFromInvariantString(dataValue);
					}
				}
			}
		}
		else
		{
			obj = dataValue;
		}
		if (attribute == null)
		{
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Could not find a name for a resource. The resource value was '{0}'.", new object[1] { obj }));
		}
		if (useResXDataNodes)
		{
			ResXDataNode resXDataNode = new ResXDataNode(attribute, obj, position);
			resXDataNode.Comment = comment;
			hashtable[attribute] = resXDataNode;
		}
		else
		{
			hashtable[attribute] = obj;
		}
	}

	private Type ResolveType(string type)
	{
		if (typeresolver != null)
		{
			return typeresolver.GetType(type);
		}
		if (assemblyNames != null)
		{
			AssemblyName[] array = assemblyNames;
			foreach (AssemblyName assemblyRef in array)
			{
				Assembly assembly = Assembly.Load(assemblyRef);
				Type type2 = assembly.GetType(type, throwOnError: false);
				if ((object)type2 != null)
				{
					return type2;
				}
			}
		}
		return Type.GetType(type);
	}

	/// <summary>Releases all resources used by the <see cref="T:System.Resources.ResXResourceReader" />.</summary>
	public void Close()
	{
		if (reader != null)
		{
			reader.Close();
			reader = null;
		}
	}

	/// <summary>Returns an enumerator for the current <see cref="T:System.Resources.ResXResourceReader" /> object.</summary>
	/// <returns>An enumerator for the current <see cref="T:System.Resources.ResourceReader" /> object.</returns>
	public IDictionaryEnumerator GetEnumerator()
	{
		if (hasht == null)
		{
			LoadData();
		}
		return hasht.GetEnumerator();
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Resources.ResXResourceReader" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
		{
			Close();
		}
	}

	/// <summary>Creates a new <see cref="T:System.Resources.ResXResourceReader" /> object and initializes it to read a string whose contents are in the form of an XML resource file.</summary>
	/// <returns>A <see cref="T:System.Resources.ResXResourceReader" /> object that reads resources from the <paramref name="fileContents" /> string.</returns>
	/// <param name="fileContents">A string containing XML resource-formatted information. </param>
	public static ResXResourceReader FromFileContents(string fileContents)
	{
		return new ResXResourceReader(new StringReader(fileContents));
	}

	/// <summary>Creates a new <see cref="T:System.Resources.ResXResourceReader" /> object and initializes it to read a string whose contents are in the form of an XML resource file, and to use an <see cref="T:System.ComponentModel.Design.ITypeResolutionService" /> object to resolve type names specified in a resource.</summary>
	/// <returns>An object that reads resources from the <paramref name="fileContents" /> string.</returns>
	/// <param name="fileContents">A string containing XML resource-formatted information. </param>
	/// <param name="typeResolver">An object that resolves type names specified in a resource.</param>
	public static ResXResourceReader FromFileContents(string fileContents, ITypeResolutionService typeResolver)
	{
		return new ResXResourceReader(new StringReader(fileContents), typeResolver);
	}

	/// <summary>Creates a new <see cref="T:System.Resources.ResXResourceReader" /> object and initializes it to read a string whose contents are in the form of an XML resource file, and to use an array of <see cref="T:System.Reflection.AssemblyName" /> objects to resolve type names specified in a resource. </summary>
	/// <returns>An object that reads resources from the <paramref name="fileContents" /> string.</returns>
	/// <param name="fileContents">A string whose contents are in the form of an XML resource file. </param>
	/// <param name="assemblyNames">An array of <see cref="T:System.Reflection.AssemblyName" /> objects that specifies one or more assemblies. The assemblies are used to resolve a type name in the resource to an actual type. </param>
	public static ResXResourceReader FromFileContents(string fileContents, AssemblyName[] assemblyNames)
	{
		return new ResXResourceReader(new StringReader(fileContents), assemblyNames);
	}

	/// <summary>Provides a dictionary enumerator that can retrieve the design-time properties from the current XML resource file or stream.</summary>
	/// <returns>An enumerator for the metadata in a resource.</returns>
	public IDictionaryEnumerator GetMetadataEnumerator()
	{
		if (hashtm == null)
		{
			LoadData();
		}
		return hashtm.GetEnumerator();
	}
}
