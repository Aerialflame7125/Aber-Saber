using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

namespace System.Resources;

internal class ResXResourceReader : IResourceReader, IEnumerable, IDisposable
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
				if (Reader.Split(',')[0].Trim() != typeof(ResXResourceReader).FullName)
				{
					return false;
				}
				if (Writer.Split(',')[0].Trim() != typeof(ResXResourceWriter).FullName)
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

	private OrderedDictionary hasht;

	private ITypeResolutionService typeresolver;

	private XmlTextReader xmlReader;

	private string basepath;

	private bool useResXDataNodes;

	private AssemblyName[] assemblyNames;

	private OrderedDictionary hashtm;

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

	public ResXResourceReader(Stream stream, ITypeResolutionService typeResolver)
		: this(stream)
	{
		typeresolver = typeResolver;
	}

	public ResXResourceReader(string fileName)
	{
		this.fileName = fileName;
	}

	public ResXResourceReader(string fileName, ITypeResolutionService typeResolver)
		: this(fileName)
	{
		typeresolver = typeResolver;
	}

	public ResXResourceReader(TextReader reader)
	{
		this.reader = reader;
	}

	public ResXResourceReader(TextReader reader, ITypeResolutionService typeResolver)
		: this(reader)
	{
		typeresolver = typeResolver;
	}

	public ResXResourceReader(Stream stream, AssemblyName[] assemblyNames)
		: this(stream)
	{
		this.assemblyNames = assemblyNames;
	}

	public ResXResourceReader(string fileName, AssemblyName[] assemblyNames)
		: this(fileName)
	{
		this.assemblyNames = assemblyNames;
	}

	public ResXResourceReader(TextReader reader, AssemblyName[] assemblyNames)
		: this(reader)
	{
		this.assemblyNames = assemblyNames;
	}

	~ResXResourceReader()
	{
		Dispose(disposing: false);
	}

	private void LoadData()
	{
		hasht = new OrderedDictionary();
		hashtm = new OrderedDictionary();
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
			catch (SerializationException ex)
			{
				throw ex;
			}
			catch (TargetInvocationException ex2)
			{
				throw ex2;
			}
			catch (Exception ex3)
			{
				XmlException innerException2 = new XmlException(ex3.Message, ex3, xmlReader.LineNumber, xmlReader.LinePosition);
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
		while (xmlReader.Read() && (xmlReader.NodeType != XmlNodeType.EndElement || !(xmlReader.LocalName == (meta ? "metadata" : "data"))))
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
					if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.LocalName == (meta ? "metadata" : "data"))
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
		OrderedDictionary orderedDictionary = ((meta && !useResXDataNodes) ? hashtm : hasht);
		Point position = new Point(xmlReader.LineNumber, xmlReader.LinePosition);
		string attribute = GetAttribute("name");
		string attribute2 = GetAttribute("type");
		string attribute3 = GetAttribute("mimetype");
		string comment = null;
		string dataValue = GetDataValue(meta, out comment);
		ResXDataNode resXDataNode = new ResXDataNode(attribute, attribute3, attribute2, dataValue, comment, position, BasePath);
		if (useResXDataNodes)
		{
			orderedDictionary[attribute] = resXDataNode;
			return;
		}
		if (attribute == null)
		{
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Could not find a name for a resource. The resource value was '{0}'.", resXDataNode.GetValue((AssemblyName[])null).ToString()));
		}
		if (assemblyNames != null)
		{
			try
			{
				orderedDictionary[attribute] = resXDataNode.GetValue(assemblyNames);
				return;
			}
			catch (TypeLoadException ex)
			{
				if (resXDataNode.handler is TypeConverterFromResXHandler)
				{
					orderedDictionary[attribute] = null;
					return;
				}
				throw ex;
			}
		}
		try
		{
			orderedDictionary[attribute] = resXDataNode.GetValue(typeresolver);
		}
		catch (TypeLoadException ex2)
		{
			if (resXDataNode.handler is TypeConverterFromResXHandler)
			{
				orderedDictionary[attribute] = null;
				return;
			}
			throw ex2;
		}
	}

	public void Close()
	{
		if (reader != null)
		{
			reader.Close();
			reader = null;
		}
	}

	public IDictionaryEnumerator GetEnumerator()
	{
		if (hasht == null)
		{
			LoadData();
		}
		return hasht.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IResourceReader)this).GetEnumerator();
	}

	void IDisposable.Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
		{
			Close();
		}
	}

	public static ResXResourceReader FromFileContents(string fileContents)
	{
		return new ResXResourceReader(new StringReader(fileContents));
	}

	public static ResXResourceReader FromFileContents(string fileContents, ITypeResolutionService typeResolver)
	{
		return new ResXResourceReader(new StringReader(fileContents), typeResolver);
	}

	public static ResXResourceReader FromFileContents(string fileContents, AssemblyName[] assemblyNames)
	{
		return new ResXResourceReader(new StringReader(fileContents), assemblyNames);
	}

	public IDictionaryEnumerator GetMetadataEnumerator()
	{
		if (hashtm == null)
		{
			LoadData();
		}
		return hashtm.GetEnumerator();
	}
}
