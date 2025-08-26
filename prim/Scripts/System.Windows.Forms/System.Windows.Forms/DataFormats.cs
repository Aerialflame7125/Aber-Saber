namespace System.Windows.Forms;

/// <summary>Provides static, predefined <see cref="T:System.Windows.Forms.Clipboard" /> format names. Use them to identify the format of data that you store in an <see cref="T:System.Windows.Forms.IDataObject" />.</summary>
/// <filterpriority>2</filterpriority>
public class DataFormats
{
	/// <summary>Represents a Clipboard format type.</summary>
	public class Format
	{
		private static readonly object lockobj = new object();

		private static Format formats;

		private string name;

		private int id;

		private Format next;

		internal bool is_serializable;

		/// <summary>Gets the ID number for this format.</summary>
		/// <returns>The ID number for this format.</returns>
		public int Id => id;

		/// <summary>Gets the name of this format.</summary>
		/// <returns>The name of this format.</returns>
		public string Name => name;

		internal Format Next => next;

		internal static Format List => formats;

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataFormats.Format" /> class with a Boolean that indicates whether a Win32 handle is expected.</summary>
		/// <param name="name">The name of this format. </param>
		/// <param name="id">The ID number for this format. </param>
		public Format(string name, int id)
		{
			this.name = name;
			this.id = id;
			lock (lockobj)
			{
				if (formats == null)
				{
					formats = this;
					return;
				}
				Format format = formats;
				while (format.next != null)
				{
					format = format.next;
				}
				format.next = this;
			}
		}

		internal static Format Add(string name)
		{
			Format format = Find(name);
			if (format == null)
			{
				IntPtr handle = XplatUI.ClipboardOpen(primary_selection: false);
				format = new Format(name, XplatUI.ClipboardGetID(handle, name));
				XplatUI.ClipboardClose(handle);
			}
			return format;
		}

		internal static Format Add(int id)
		{
			Format format = Find(id);
			if (format == null)
			{
				format = new Format("Format" + id, id);
			}
			return format;
		}

		internal static Format Find(int id)
		{
			Format format = formats;
			while (format != null && format.Id != id)
			{
				format = format.next;
			}
			return format;
		}

		internal static Format Find(string name)
		{
			Format format = formats;
			while (format != null && !format.Name.Equals(name))
			{
				format = format.next;
			}
			return format;
		}
	}

	/// <summary>Specifies a Windows bitmap format. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string Bitmap = "Bitmap";

	/// <summary>Specifies a comma-separated value (CSV) format, which is a common interchange format used by spreadsheets. This format is not used directly by Windows Forms. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string CommaSeparatedValue = "Csv";

	/// <summary>Specifies the Windows device-independent bitmap (DIB) format. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string Dib = "DeviceIndependentBitmap";

	/// <summary>Specifies the Windows Data Interchange Format (DIF), which Windows Forms does not directly use. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string Dif = "DataInterchangeFormat";

	/// <summary>Specifies the Windows enhanced metafile format. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string EnhancedMetafile = "EnhancedMetafile";

	/// <summary>Specifies the Windows file drop format, which Windows Forms does not directly use. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string FileDrop = "FileDrop";

	/// <summary>Specifies text in the HTML Clipboard format. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string Html = "HTML Format";

	/// <summary>Specifies the Windows culture format, which Windows Forms does not directly use. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string Locale = "Locale";

	/// <summary>Specifies the Windows metafile format, which Windows Forms does not directly use. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string MetafilePict = "MetaFilePict";

	/// <summary>Specifies the standard Windows original equipment manufacturer (OEM) text format. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string OemText = "OEMText";

	/// <summary>Specifies the Windows palette format. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string Palette = "Palette";

	/// <summary>Specifies the Windows pen data format, which consists of pen strokes for handwriting software; Windows Forms does not use this format. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string PenData = "PenData";

	/// <summary>Specifies the Resource Interchange File Format (RIFF) audio format, which Windows Forms does not directly use. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string Riff = "RiffAudio";

	/// <summary>Specifies text consisting of Rich Text Format (RTF) data. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string Rtf = "Rich Text Format";

	/// <summary>Specifies a format that encapsulates any type of Windows Forms object. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string Serializable = "WindowsForms10PersistentObject";

	/// <summary>Specifies the Windows Forms string class format, which Windows Forms uses to store string objects. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string StringFormat = "System.String";

	/// <summary>Specifies the Windows symbolic link format, which Windows Forms does not directly use. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string SymbolicLink = "SymbolicLink";

	/// <summary>Specifies the standard ANSI text format. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string Text = "Text";

	/// <summary>Specifies the Tagged Image File Format (TIFF), which Windows Forms does not directly use. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string Tiff = "Tiff";

	/// <summary>Specifies the standard Windows Unicode text format. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string UnicodeText = "UnicodeText";

	/// <summary>Specifies the wave audio format, which Windows Forms does not directly use. This static field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly string WaveAudio = "WaveAudio";

	private static object lock_object = new object();

	private static bool initialized;

	private DataFormats()
	{
	}

	internal static bool ContainsFormat(int id)
	{
		lock (lock_object)
		{
			if (!initialized)
			{
				Init();
			}
			return Format.Find(id) != null;
		}
	}

	/// <summary>Returns a <see cref="T:System.Windows.Forms.DataFormats.Format" /> with the Windows Clipboard numeric ID and name for the specified ID.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataFormats.Format" /> that has the Windows Clipboard numeric ID and the name of the format.</returns>
	/// <param name="id">The format ID. </param>
	/// <filterpriority>1</filterpriority>
	public static Format GetFormat(int id)
	{
		lock (lock_object)
		{
			if (!initialized)
			{
				Init();
			}
			return Format.Find(id);
		}
	}

	/// <summary>Returns a <see cref="T:System.Windows.Forms.DataFormats.Format" /> with the Windows Clipboard numeric ID and name for the specified format.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataFormats.Format" /> that has the Windows Clipboard numeric ID and the name of the format.</returns>
	/// <param name="format">The format name. </param>
	/// <exception cref="T:System.ComponentModel.Win32Exception">Registering a new <see cref="T:System.Windows.Forms.Clipboard" /> format failed. </exception>
	/// <filterpriority>1</filterpriority>
	public static Format GetFormat(string format)
	{
		lock (lock_object)
		{
			if (!initialized)
			{
				Init();
			}
			return Format.Add(format);
		}
	}

	private static void Init()
	{
		if (!initialized)
		{
			IntPtr handle = XplatUI.ClipboardOpen(primary_selection: false);
			new Format(Text, XplatUI.ClipboardGetID(handle, Text));
			new Format(Bitmap, XplatUI.ClipboardGetID(handle, Bitmap));
			new Format(MetafilePict, XplatUI.ClipboardGetID(handle, MetafilePict));
			new Format(SymbolicLink, XplatUI.ClipboardGetID(handle, SymbolicLink));
			new Format(Dif, XplatUI.ClipboardGetID(handle, Dif));
			new Format(Tiff, XplatUI.ClipboardGetID(handle, Tiff));
			new Format(OemText, XplatUI.ClipboardGetID(handle, OemText));
			new Format(Dib, XplatUI.ClipboardGetID(handle, Dib));
			new Format(Palette, XplatUI.ClipboardGetID(handle, Palette));
			new Format(PenData, XplatUI.ClipboardGetID(handle, PenData));
			new Format(Riff, XplatUI.ClipboardGetID(handle, Riff));
			new Format(WaveAudio, XplatUI.ClipboardGetID(handle, WaveAudio));
			new Format(UnicodeText, XplatUI.ClipboardGetID(handle, UnicodeText));
			new Format(EnhancedMetafile, XplatUI.ClipboardGetID(handle, EnhancedMetafile));
			new Format(FileDrop, XplatUI.ClipboardGetID(handle, FileDrop));
			new Format(Locale, XplatUI.ClipboardGetID(handle, Locale));
			new Format(CommaSeparatedValue, XplatUI.ClipboardGetID(handle, CommaSeparatedValue));
			new Format(Html, XplatUI.ClipboardGetID(handle, Html));
			new Format(Rtf, XplatUI.ClipboardGetID(handle, Rtf));
			new Format(Serializable, XplatUI.ClipboardGetID(handle, Serializable));
			new Format(StringFormat, XplatUI.ClipboardGetID(handle, StringFormat));
			XplatUI.ClipboardClose(handle);
			initialized = true;
		}
	}
}
