using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

namespace System.Resources;

internal class SerializedFromResXHandler : ResXDataNodeHandler, IWritableHandler
{
	private sealed class CustomBinder : SerializationBinder
	{
		private ITypeResolutionService typeResolver;

		public CustomBinder(ITypeResolutionService _typeResolver)
		{
			typeResolver = _typeResolver;
		}

		public override Type BindToType(string assemblyName, string typeName)
		{
			Type type = null;
			string text = $"{typeName}, {assemblyName}";
			if (typeResolver != null)
			{
				type = typeResolver.GetType(text);
			}
			if (type == null)
			{
				type = Type.GetType(text);
			}
			return type;
		}
	}

	private string dataString;

	private string mime_type;

	private CustomBinder binder;

	public string DataString => dataString;

	public SerializedFromResXHandler(string data, string _mime_type)
	{
		dataString = data;
		mime_type = _mime_type;
	}

	public override object GetValue(ITypeResolutionService typeResolver)
	{
		return DeserializeObject(typeResolver);
	}

	public override object GetValue(AssemblyName[] assemblyNames)
	{
		return DeserializeObject(new AssemblyNamesTypeResolutionService(assemblyNames));
	}

	public override string GetValueTypeName(ITypeResolutionService typeResolver)
	{
		return InternalGetValueType(typeResolver);
	}

	public override string GetValueTypeName(AssemblyName[] assemblyNames)
	{
		return InternalGetValueType(null);
	}

	private string InternalGetValueType(ITypeResolutionService typeResolver)
	{
		object obj;
		try
		{
			obj = DeserializeObject(typeResolver);
		}
		catch
		{
			return typeof(object).AssemblyQualifiedName;
		}
		return obj?.GetType().AssemblyQualifiedName;
	}

	private object DeserializeObject(ITypeResolutionService typeResolver)
	{
		try
		{
			if (mime_type == ResXResourceWriter.SoapSerializedObjectMimeType)
			{
				SoapFormatter soapFormatter = new SoapFormatter();
				if (binder == null)
				{
					binder = new CustomBinder(typeResolver);
				}
				soapFormatter.Binder = binder;
				using MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(dataString));
				return soapFormatter.Deserialize(serializationStream);
			}
			if (mime_type == ResXResourceWriter.BinSerializedObjectMimeType)
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				if (binder == null)
				{
					binder = new CustomBinder(typeResolver);
				}
				binaryFormatter.Binder = binder;
				using MemoryStream serializationStream2 = new MemoryStream(Convert.FromBase64String(dataString));
				return binaryFormatter.Deserialize(serializationStream2);
			}
			return null;
		}
		catch (SerializationException ex)
		{
			if (ex.Message.StartsWith("Couldn't find assembly"))
			{
				throw new ArgumentException(ex.Message);
			}
			throw ex;
		}
	}
}
