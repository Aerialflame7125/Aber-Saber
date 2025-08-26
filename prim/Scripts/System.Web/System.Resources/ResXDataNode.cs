using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace System.Resources;

[Serializable]
internal sealed class ResXDataNode : ISerializable
{
	private string name;

	private ResXFileRef fileRef;

	private string comment;

	private Point pos;

	internal ResXDataNodeHandler handler;

	public string Comment
	{
		get
		{
			return comment ?? string.Empty;
		}
		set
		{
			comment = value;
		}
	}

	public ResXFileRef FileRef => fileRef;

	public string Name
	{
		get
		{
			return name ?? string.Empty;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == string.Empty)
			{
				throw new ArgumentException("name");
			}
			name = value;
		}
	}

	internal bool IsWritable => handler is IWritableHandler;

	internal string MimeType { get; set; }

	internal string Type { get; set; }

	internal string DataString
	{
		get
		{
			if (IsWritable)
			{
				return ((IWritableHandler)handler).DataString;
			}
			throw new NotSupportedException("Node Not Writable");
		}
	}

	public ResXDataNode(string name, object value)
		: this(name, value, Point.Empty)
	{
	}

	public ResXDataNode(string name, ResXFileRef fileRef)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		if (fileRef == null)
		{
			throw new ArgumentNullException("fileRef");
		}
		if (name.Length == 0)
		{
			throw new ArgumentException("name");
		}
		this.name = name;
		this.fileRef = fileRef;
		pos = Point.Empty;
		handler = new FileRefHandler(fileRef);
	}

	internal ResXDataNode(string name, object value, Point position)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		if (name.Length == 0)
		{
			throw new ArgumentException("name");
		}
		Type type = ((value == null) ? typeof(object) : value.GetType());
		if (value != null && !type.IsSerializable)
		{
			throw new InvalidOperationException($"'{name}' of type '{type}' cannot be added because it is not serializable");
		}
		this.name = name;
		pos = position;
		handler = new InMemoryHandler(value);
	}

	internal ResXDataNode(string nameAtt, string mimeTypeAtt, string typeAtt, string dataString, string commentString, Point position, string basePath)
	{
		name = nameAtt;
		comment = commentString;
		pos = position;
		MimeType = mimeTypeAtt;
		Type = typeAtt;
		if (!string.IsNullOrEmpty(mimeTypeAtt))
		{
			if (!string.IsNullOrEmpty(typeAtt))
			{
				handler = new TypeConverterFromResXHandler(dataString, mimeTypeAtt, typeAtt);
			}
			else
			{
				handler = new SerializedFromResXHandler(dataString, mimeTypeAtt);
			}
		}
		else if (!string.IsNullOrEmpty(typeAtt))
		{
			if (typeAtt.StartsWith("System.Resources.ResXNullRef, System.Windows.Forms"))
			{
				handler = new NullRefHandler(typeAtt);
			}
			else if (typeAtt.StartsWith("System.Byte[], mscorlib"))
			{
				handler = new ByteArrayFromResXHandler(dataString);
			}
			else if (typeAtt.StartsWith("System.Resources.ResXFileRef, System.Windows.Forms"))
			{
				ResXFileRef resXFileRef = BuildFileRef(dataString, basePath);
				handler = new FileRefHandler(resXFileRef);
				fileRef = resXFileRef;
			}
			else
			{
				handler = new TypeConverterFromResXHandler(dataString, mimeTypeAtt, typeAtt);
			}
		}
		else
		{
			handler = new InMemoryHandler(dataString);
		}
		if (handler == null)
		{
			throw new Exception("handler is null");
		}
	}

	public Point GetNodePosition()
	{
		return pos;
	}

	public string GetValueTypeName(AssemblyName[] names)
	{
		return handler.GetValueTypeName(names);
	}

	public string GetValueTypeName(ITypeResolutionService typeResolver)
	{
		return handler.GetValueTypeName(typeResolver);
	}

	public object GetValue(AssemblyName[] names)
	{
		return handler.GetValue(names);
	}

	public object GetValue(ITypeResolutionService typeResolver)
	{
		return handler.GetValue(typeResolver);
	}

	private ResXFileRef BuildFileRef(string dataString, string basePath)
	{
		string[] array = ResXFileRef.Parse(dataString);
		if (array.Length < 2)
		{
			throw new ArgumentException("ResXFileRef cannot be generated");
		}
		string fileName = array[0];
		if (basePath != null)
		{
			fileName = Path.Combine(basePath, array[0]);
		}
		string typeName = array[1];
		if (array.Length == 3)
		{
			Encoding encoding = Encoding.GetEncoding(array[2]);
			return new ResXFileRef(fileName, typeName, encoding);
		}
		return new ResXFileRef(fileName, typeName);
	}

	void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
	{
		si.AddValue("Name", Name);
		si.AddValue("Comment", Comment);
	}
}
