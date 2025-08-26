using System.Collections.Generic;
using System.Web.Configuration;
using System.Xml;

namespace System.Web.Compilation;

internal class PreservationFile
{
	private string _filePath;

	private string _assembly;

	private int _fileHash;

	private int _flags;

	private int _hash;

	private BuildResultTypeCode _resultType;

	private string _virtualPath;

	private List<string> _filedeps;

	public string Assembly
	{
		get
		{
			return _assembly;
		}
		set
		{
			_assembly = value;
		}
	}

	public string FilePath
	{
		get
		{
			return _filePath;
		}
		set
		{
			_filePath = value;
		}
	}

	public int FileHash
	{
		get
		{
			return _fileHash;
		}
		set
		{
			_fileHash = value;
		}
	}

	public int Flags
	{
		get
		{
			return _flags;
		}
		set
		{
			_flags = value;
		}
	}

	public int Hash
	{
		get
		{
			return _hash;
		}
		set
		{
			_hash = value;
		}
	}

	public BuildResultTypeCode ResultType
	{
		get
		{
			return _resultType;
		}
		set
		{
			_resultType = value;
		}
	}

	public string VirtualPath
	{
		get
		{
			return _virtualPath;
		}
		set
		{
			_virtualPath = value;
		}
	}

	public List<string> FileDeps
	{
		get
		{
			return _filedeps;
		}
		set
		{
			_filedeps = value;
		}
	}

	public PreservationFile()
	{
	}

	public PreservationFile(string filePath)
	{
		_filePath = filePath;
		Parse(filePath);
	}

	public void Parse()
	{
		if (_filePath == null)
		{
			throw new InvalidOperationException("File path is not defined");
		}
		Parse(_filePath);
	}

	public void Parse(string filePath)
	{
		if (filePath == null)
		{
			throw new ArgumentNullException("File path is required", "filePath");
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(filePath);
		XmlNode documentElement = xmlDocument.DocumentElement;
		if (documentElement.Name != "preserve")
		{
			throw new InvalidOperationException("Invalid assembly mapping file format");
		}
		ParseRecursively(documentElement);
	}

	private void ParseRecursively(XmlNode root)
	{
		_assembly = GetNonEmptyRequiredAttribute(root, "assembly");
		try
		{
			_virtualPath = GetNonEmptyOptionalAttribute(root, "virtualPath");
			_fileHash = GetNonEmptyOptionalAttributeInt32(root, "filehash");
			_hash = GetNonEmptyOptionalAttributeInt32(root, "hash");
			_flags = GetNonEmptyOptionalAttributeInt32(root, "flags");
			_resultType = (BuildResultTypeCode)GetNonEmptyOptionalAttributeInt32(root, "resultType");
			foreach (XmlNode childNode in root.ChildNodes)
			{
				if (childNode.NodeType == XmlNodeType.Element && !(childNode.Name != "filedeps"))
				{
					ReadFileDeps(childNode);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	private void ReadFileDeps(XmlNode node)
	{
		if (_filedeps == null)
		{
			_filedeps = new List<string>();
		}
		foreach (XmlNode childNode in node.ChildNodes)
		{
			if (childNode.NodeType == XmlNodeType.Element && !(childNode.Name != "filedep"))
			{
				string nonEmptyRequiredAttribute = GetNonEmptyRequiredAttribute(childNode, "name");
				_filedeps.Add(nonEmptyRequiredAttribute);
			}
		}
	}

	public void Save()
	{
		if (_filePath == null)
		{
			throw new InvalidOperationException("File path is not defined");
		}
		Save(_filePath);
	}

	public void Save(string filePath)
	{
		if (filePath == null)
		{
			throw new ArgumentNullException("File path is required", "filePath");
		}
		XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
		xmlWriterSettings.Indent = false;
		xmlWriterSettings.OmitXmlDeclaration = false;
		xmlWriterSettings.NewLineOnAttributes = false;
		using XmlWriter xmlWriter = XmlWriter.Create(filePath, xmlWriterSettings);
		xmlWriter.WriteStartElement("preserve");
		xmlWriter.WriteAttributeString("assembly", _assembly);
		if (!string.IsNullOrEmpty(_virtualPath))
		{
			xmlWriter.WriteAttributeString("virtualPath", _virtualPath);
		}
		if (_fileHash != 0)
		{
			xmlWriter.WriteAttributeString("filehash", _fileHash.ToString());
		}
		if (_flags != 0)
		{
			xmlWriter.WriteAttributeString("flags", _flags.ToString());
		}
		if (_hash != 0)
		{
			xmlWriter.WriteAttributeString("hash", _hash.ToString());
		}
		if (_resultType != 0)
		{
			int resultType = (int)_resultType;
			xmlWriter.WriteAttributeString("resultType", resultType.ToString());
		}
		if (_filedeps != null && _filedeps.Count > 0)
		{
			xmlWriter.WriteStartElement("filedeps");
			foreach (string filedep in _filedeps)
			{
				xmlWriter.WriteStartElement("filedep");
				xmlWriter.WriteAttributeString("name", filedep);
				xmlWriter.WriteEndElement();
			}
			xmlWriter.WriteEndElement();
		}
		xmlWriter.WriteEndElement();
	}

	private string GetNonEmptyOptionalAttribute(XmlNode n, string name)
	{
		return HandlersUtil.ExtractAttributeValue(name, n, optional: true);
	}

	private int GetNonEmptyOptionalAttributeInt32(XmlNode n, string name)
	{
		string nonEmptyOptionalAttribute = GetNonEmptyOptionalAttribute(n, name);
		if (nonEmptyOptionalAttribute != null)
		{
			return int.Parse(nonEmptyOptionalAttribute);
		}
		return 0;
	}

	private string GetNonEmptyRequiredAttribute(XmlNode n, string name)
	{
		return HandlersUtil.ExtractAttributeValue(name, n, optional: false, allowEmpty: false);
	}
}
