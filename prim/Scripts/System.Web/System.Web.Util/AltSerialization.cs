using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.Web.Util;

internal sealed class AltSerialization
{
	private AltSerialization()
	{
	}

	internal static void Serialize(BinaryWriter w, object value)
	{
		TypeCode typeCode = ((value != null) ? Type.GetTypeCode(value.GetType()) : TypeCode.Empty);
		w.Write((byte)typeCode);
		switch (typeCode)
		{
		case TypeCode.Boolean:
			w.Write((bool)value);
			break;
		case TypeCode.Byte:
			w.Write((byte)value);
			break;
		case TypeCode.Char:
			w.Write((char)value);
			break;
		case TypeCode.DateTime:
			w.Write(((DateTime)value).Ticks);
			break;
		case TypeCode.Decimal:
			w.Write((decimal)value);
			break;
		case TypeCode.Double:
			w.Write((double)value);
			break;
		case TypeCode.Int16:
			w.Write((short)value);
			break;
		case TypeCode.Int32:
			w.Write((int)value);
			break;
		case TypeCode.Int64:
			w.Write((long)value);
			break;
		case TypeCode.Object:
			new BinaryFormatter().Serialize(w.BaseStream, value);
			break;
		case TypeCode.SByte:
			w.Write((sbyte)value);
			break;
		case TypeCode.Single:
			w.Write((float)value);
			break;
		case TypeCode.String:
			w.Write((string)value);
			break;
		case TypeCode.UInt16:
			w.Write((ushort)value);
			break;
		case TypeCode.UInt32:
			w.Write((uint)value);
			break;
		case TypeCode.UInt64:
			w.Write((ulong)value);
			break;
		case TypeCode.Empty:
		case TypeCode.DBNull:
		case (TypeCode)17:
			break;
		}
	}

	internal static object Deserialize(BinaryReader r)
	{
		TypeCode typeCode = (TypeCode)r.ReadByte();
		return typeCode switch
		{
			TypeCode.Boolean => r.ReadBoolean(), 
			TypeCode.Byte => r.ReadByte(), 
			TypeCode.Char => r.ReadChar(), 
			TypeCode.DateTime => new DateTime(r.ReadInt64()), 
			TypeCode.DBNull => DBNull.Value, 
			TypeCode.Decimal => r.ReadDecimal(), 
			TypeCode.Double => r.ReadDouble(), 
			TypeCode.Empty => null, 
			TypeCode.Int16 => r.ReadInt16(), 
			TypeCode.Int32 => r.ReadInt32(), 
			TypeCode.Int64 => r.ReadInt64(), 
			TypeCode.Object => new BinaryFormatter().Deserialize(r.BaseStream), 
			TypeCode.SByte => r.ReadSByte(), 
			TypeCode.Single => r.ReadSingle(), 
			TypeCode.String => r.ReadString(), 
			TypeCode.UInt16 => r.ReadUInt16(), 
			TypeCode.UInt32 => r.ReadUInt32(), 
			TypeCode.UInt64 => r.ReadUInt64(), 
			_ => throw new ArgumentOutOfRangeException("TypeCode:" + typeCode), 
		};
	}
}
