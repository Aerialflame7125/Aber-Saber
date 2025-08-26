using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.Resources;

internal class TypeConverterFromResXHandler : ResXDataNodeHandler, IWritableHandler
{
	private string dataString;

	private string mime_type;

	private string typeString;

	public string DataString => dataString;

	public TypeConverterFromResXHandler(string data, string _mime_type, string _typeString)
	{
		dataString = data;
		mime_type = _mime_type;
		typeString = _typeString;
	}

	public override object GetValue(ITypeResolutionService typeResolver)
	{
		if (!string.IsNullOrEmpty(mime_type) && mime_type != ResXResourceWriter.ByteArraySerializedObjectMimeType)
		{
			return null;
		}
		Type type = ResolveType(typeString, typeResolver);
		if (type == null)
		{
			throw new TypeLoadException();
		}
		TypeConverter converter = TypeDescriptor.GetConverter(type);
		if (converter == null)
		{
			throw new TypeLoadException();
		}
		return ConvertData(converter);
	}

	public override object GetValue(AssemblyName[] assemblyNames)
	{
		if (!string.IsNullOrEmpty(mime_type) && mime_type != ResXResourceWriter.ByteArraySerializedObjectMimeType)
		{
			return null;
		}
		Type type = ResolveType(typeString, assemblyNames);
		if (type == null)
		{
			throw new TypeLoadException();
		}
		TypeConverter converter = TypeDescriptor.GetConverter(type);
		if (converter == null)
		{
			throw new TypeLoadException();
		}
		return ConvertData(converter);
	}

	public override string GetValueTypeName(ITypeResolutionService typeResolver)
	{
		Type type = ResolveType(typeString, typeResolver);
		if (type == null)
		{
			return typeString;
		}
		return type.AssemblyQualifiedName;
	}

	public override string GetValueTypeName(AssemblyName[] assemblyNames)
	{
		Type type = ResolveType(typeString, assemblyNames);
		if (type == null)
		{
			return typeString;
		}
		return type.AssemblyQualifiedName;
	}

	private object ConvertData(TypeConverter c)
	{
		if (mime_type == ResXResourceWriter.ByteArraySerializedObjectMimeType)
		{
			if (c.CanConvertFrom(typeof(byte[])))
			{
				return c.ConvertFrom(Convert.FromBase64String(dataString));
			}
		}
		else
		{
			if (!string.IsNullOrEmpty(mime_type))
			{
				throw new Exception("shouldnt get here, invalid mime type");
			}
			if (c.CanConvertFrom(typeof(string)))
			{
				return c.ConvertFromInvariantString(dataString);
			}
		}
		throw new TypeLoadException("No converter for this type found");
	}
}
