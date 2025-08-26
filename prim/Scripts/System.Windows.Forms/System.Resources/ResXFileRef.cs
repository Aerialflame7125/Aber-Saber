using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace System.Resources;

/// <summary>Represents a link to an external resource.</summary>
[Serializable]
[TypeConverter(typeof(Converter))]
public class ResXFileRef
{
	/// <summary>Provides a type converter to convert data for a <see cref="T:System.Resources.ResXFileRef" /> to and from a string.</summary>
	public class Converter : TypeConverter
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Resources.ResXFileRef.Converter" /> class. </summary>
		public Converter()
		{
		}

		/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from. </param>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return (object)sourceType == typeof(string);
		}

		/// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to. </param>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return (object)destinationType == typeof(string);
		}

		/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
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
			Type type = Type.GetType(array[1]);
			if ((object)type == typeof(string))
			{
				using (TextReader textReader = new StreamReader(encoding: (array.Length <= 2) ? Encoding.Default : Encoding.GetEncoding(array[2]), path: array[0]))
				{
					return textReader.ReadToEnd();
				}
			}
			byte[] array2;
			using (FileStream fileStream = new FileStream(array[0], FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				array2 = new byte[fileStream.Length];
				fileStream.Read(array2, 0, (int)fileStream.Length);
			}
			if ((object)type == typeof(byte[]))
			{
				return array2;
			}
			if ((object)type == typeof(Bitmap) && Path.GetExtension(array[0]) == ".ico")
			{
				MemoryStream stream = new MemoryStream(array2);
				return new Icon(stream).ToBitmap();
			}
			if ((object)type == typeof(MemoryStream))
			{
				return new MemoryStream(array2);
			}
			return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, new object[1]
			{
				new MemoryStream(array2)
			}, culture);
		}

		/// <summary>Provides a type converter to convert data for an resource reference to and from a string.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if ((object)destinationType != typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			return ((ResXFileRef)value).ToString();
		}
	}

	private string filename;

	private string typename;

	private Encoding textFileEncoding;

	/// <summary>Gets the file name specified in the current <see cref="Overload:System.Resources.ResXFileRef.#ctor" /> constructor.</summary>
	/// <returns>The name of the referenced file.</returns>
	public string FileName => filename;

	/// <summary>Gets the encoding specified in the current <see cref="Overload:System.Resources.ResXFileRef.#ctor" /> constructor.</summary>
	/// <returns>The encoding used in the referenced file.</returns>
	public Encoding TextFileEncoding => textFileEncoding;

	/// <summary>Gets the type name specified in the current <see cref="Overload:System.Resources.ResXFileRef.#ctor" /> constructor. </summary>
	/// <returns>The type name of the resource that is referenced. </returns>
	public string TypeName => typename;

	/// <summary>Creates a new instance of the <see cref="T:System.Resources.ResXFileRef" /> class that references the specified file.</summary>
	/// <param name="fileName">The file to reference. </param>
	/// <param name="typeName">The type of the resource that is referenced. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="fileName" /> or <paramref name="typeName " />is null.</exception>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXFileRef" /> class that references the specified file. </summary>
	/// <param name="fileName">The file to reference. </param>
	/// <param name="typeName">The type name of the resource that is referenced. </param>
	/// <param name="textFileEncoding">The encoding used in the referenced file.</param>
	public ResXFileRef(string fileName, string typeName, Encoding textFileEncoding)
		: this(fileName, typeName)
	{
		this.textFileEncoding = textFileEncoding;
	}

	/// <summary>Gets the text representation of the current <see cref="T:System.Resources.ResXFileRef" /> object.</summary>
	/// <returns>A string that consists of the concatenated text representations of the parameters specified in the current <see cref="Overload:System.Resources.ResXFileRef.#ctor" /> constructor.</returns>
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
