using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace System.Resources;

[Serializable]
[TypeConverter(typeof(Converter))]
internal class ResXFileRef
{
	internal class Converter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return null;
			}
			string[] array = Parse((string)value);
			if (array.Length == 1)
			{
				throw new ArgumentException("value");
			}
			string text = array[0];
			if (Path.DirectorySeparatorChar == '/')
			{
				text = text.Replace("\\", "/");
			}
			Type type = Type.GetType(array[1]);
			if (type == typeof(string))
			{
				Encoding encoding = ((array.Length <= 2) ? Encoding.Default : Encoding.GetEncoding(array[2]));
				using TextReader textReader = new StreamReader(text, encoding);
				return textReader.ReadToEnd();
			}
			byte[] array2;
			using (FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				array2 = new byte[fileStream.Length];
				fileStream.Read(array2, 0, (int)fileStream.Length);
			}
			if (type == typeof(byte[]))
			{
				return array2;
			}
			if (type == typeof(Bitmap) && Path.GetExtension(text) == ".ico")
			{
				return new Icon(new MemoryStream(array2)).ToBitmap();
			}
			if (type == typeof(MemoryStream))
			{
				return new MemoryStream(array2);
			}
			return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, new object[1]
			{
				new MemoryStream(array2)
			}, culture);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			return ((ResXFileRef)value).ToString();
		}
	}

	private string filename;

	private string typename;

	private Encoding textFileEncoding;

	public string FileName => filename;

	public Encoding TextFileEncoding => textFileEncoding;

	public string TypeName => typename;

	public ResXFileRef(string fileName, string typeName)
	{
		if (fileName == null)
		{
			throw new ArgumentNullException("fileName");
		}
		if (typeName == null)
		{
			throw new ArgumentNullException("typeName");
		}
		filename = fileName;
		typename = typeName;
	}

	public ResXFileRef(string fileName, string typeName, Encoding textFileEncoding)
		: this(fileName, typeName)
	{
		this.textFileEncoding = textFileEncoding;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (filename != null)
		{
			stringBuilder.Append(filename);
		}
		stringBuilder.Append(';');
		if (typename != null)
		{
			stringBuilder.Append(typename);
		}
		if (textFileEncoding != null)
		{
			stringBuilder.Append(';');
			stringBuilder.Append(textFileEncoding.WebName);
		}
		return stringBuilder.ToString();
	}

	internal static string[] Parse(string fileRef)
	{
		if (fileRef == null)
		{
			throw new ArgumentNullException("fileRef");
		}
		return fileRef.Split(';');
	}
}
