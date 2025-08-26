using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Windows.Forms;

/// <summary>Implements a basic data transfer mechanism.</summary>
/// <filterpriority>2</filterpriority>
[ClassInterface(ClassInterfaceType.None)]
public class DataObject : System.Runtime.InteropServices.ComTypes.IDataObject, IDataObject
{
	private class Entry
	{
		private string type;

		private object data;

		private bool autoconvert;

		internal Entry next;

		public object Data
		{
			get
			{
				return data;
			}
			set
			{
				data = value;
			}
		}

		public bool AutoConvert
		{
			get
			{
				return autoconvert;
			}
			set
			{
				autoconvert = value;
			}
		}

		internal Entry(string type, object data, bool autoconvert)
		{
			this.type = type;
			this.data = data;
			this.autoconvert = autoconvert;
		}

		public static int Count(Entry entries)
		{
			int num = 0;
			while (entries != null)
			{
				num++;
				entries = entries.next;
			}
			return num;
		}

		public static Entry Find(Entry entries, string type)
		{
			return Find(entries, type, only_convertible: false);
		}

		public static Entry Find(Entry entries, string type, bool only_convertible)
		{
			while (entries != null)
			{
				bool flag = true;
				if (only_convertible && !entries.autoconvert)
				{
					flag = false;
				}
				if (flag && string.Compare(entries.type, type, ignoreCase: true) == 0)
				{
					return entries;
				}
				entries = entries.next;
			}
			return null;
		}

		public static Entry FindConvertible(Entry entries, string type)
		{
			Entry entry = Find(entries, type);
			if (entry != null)
			{
				return entry;
			}
			if (type == DataFormats.StringFormat || type == DataFormats.Text || type == DataFormats.UnicodeText)
			{
				for (entry = entries; entry != null; entry = entry.next)
				{
					if (entry.type == DataFormats.StringFormat || entry.type == DataFormats.Text || entry.type == DataFormats.UnicodeText)
					{
						return entry;
					}
				}
			}
			return null;
		}

		public static string[] Entries(Entry entries, bool convertible)
		{
			ArrayList arrayList = new ArrayList(Count(entries));
			Entry entry = entries;
			if (convertible)
			{
				Entry entry2 = Find(entries, DataFormats.Text);
				Entry entry3 = Find(entries, DataFormats.UnicodeText);
				Entry entry4 = Find(entries, DataFormats.StringFormat);
				bool flag = entry2?.AutoConvert ?? false;
				bool flag2 = entry3?.AutoConvert ?? false;
				bool flag3 = entry4?.AutoConvert ?? false;
				if (flag || flag2 || flag3)
				{
					arrayList.Add(DataFormats.StringFormat);
					arrayList.Add(DataFormats.UnicodeText);
					arrayList.Add(DataFormats.Text);
				}
			}
			while (entry != null)
			{
				if (!arrayList.Contains(entry.type))
				{
					arrayList.Add(entry.type);
				}
				entry = entry.next;
			}
			string[] array = new string[arrayList.Count];
			for (int i = 0; i < arrayList.Count; i++)
			{
				array[i] = (string)arrayList[i];
			}
			return array;
		}
	}

	private Entry entries;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataObject" /> class.</summary>
	public DataObject()
	{
		entries = null;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataObject" /> class and adds the specified object to it.</summary>
	/// <param name="data">The data to store. </param>
	public DataObject(object data)
	{
		SetData(data);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataObject" /> class and adds the specified object in the specified format.</summary>
	/// <param name="format">The format of the specified data. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats.</param>
	/// <param name="data">The data to store. </param>
	public DataObject(string format, object data)
	{
		SetData(format, data);
	}

	/// <summary>Creates a connection between a data object and an advisory sink. This method is called by an object that supports an advisory sink and enables the advisory sink to be notified of changes in the object's data.</summary>
	/// <returns>This method supports the standard return values E_INVALIDARG, E_UNEXPECTED, and E_OUTOFMEMORY, as well as the following: ValueDescriptionS_OKThe advisory connection was created.E_NOTIMPLThis method is not implemented on the data object.DV_E_LINDEXThere is an invalid value for <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.lindex" />; currently, only -1 is supported.DV_E_FORMATETCThere is an invalid value for the <paramref name="pFormatetc" /> parameter.OLE_E_ADVISENOTSUPPORTEDThe data object does not support change notification.</returns>
	/// <param name="pFormatetc"> A <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, target device, aspect, and medium that will be used for future notifications.</param>
	/// <param name="advf">One of the <see cref="T:System.Runtime.InteropServices.ComTypes.ADVF" /> values that specifies a group of flags for controlling the advisory connection.</param>
	/// <param name="pAdvSink">A pointer to the <see cref="T:System.Runtime.InteropServices.ComTypes.IAdviseSink" /> interface on the advisory sink that will receive the change notification.</param>
	/// <param name="pdwConnection">When this method returns, contains a pointer to a DWORD token that identifies this connection. You can use this token later to delete the advisory connection by passing it to <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.DUnadvise(System.Int32)" />. If this value is zero, the connection was not established. This parameter is passed uninitialized.</param>
	int System.Runtime.InteropServices.ComTypes.IDataObject.DAdvise(ref FORMATETC pFormatetc, ADVF advf, IAdviseSink adviseSink, out int connection)
	{
		throw new NotImplementedException();
	}

	/// <summary>Destroys a notification connection that had been previously established.</summary>
	/// <param name="dwConnection">A DWORD token that specifies the connection to remove. Use the value returned by <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.DAdvise(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.ADVF,System.Runtime.InteropServices.ComTypes.IAdviseSink,System.Int32@)" /> when the connection was originally established.</param>
	void System.Runtime.InteropServices.ComTypes.IDataObject.DUnadvise(int connection)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates an object that can be used to enumerate the current advisory connections.</summary>
	/// <returns>This method supports the standard return value E_OUTOFMEMORY, as well as the following:ValueDescriptionS_OKThe enumerator object is successfully instantiated or there are no connections.OLE_E_ADVISENOTSUPPORTEDThis object does not support advisory notifications.</returns>
	/// <param name="enumAdvise">When this method returns, contains an <see cref="T:System.Runtime.InteropServices.ComTypes.IEnumSTATDATA" /> that receives the interface pointer to the new enumerator object. If the implementation sets <paramref name="enumAdvise" /> to null, there are no connections to advisory sinks at this time. This parameter is passed uninitialized.</param>
	int System.Runtime.InteropServices.ComTypes.IDataObject.EnumDAdvise(out IEnumSTATDATA enumAdvise)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates an object for enumerating the <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structures for a data object. These structures are used in calls to <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> or <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.SetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@,System.Boolean)" />. </summary>
	/// <returns>This method supports the standard return values E_INVALIDARG and E_OUTOFMEMORY, as well as the following:ValueDescriptionS_OKThe enumerator object was successfully created.E_NOTIMPLThe direction specified by the <paramref name="direction" /> parameter is not supported.OLE_S_USEREGRequests that OLE enumerate the formats from the registry.</returns>
	/// <param name="dwDirection">One of the <see cref="T:System.Runtime.InteropServices.ComTypes.DATADIR" /> values that specifies the direction of the data.</param>
	IEnumFORMATETC System.Runtime.InteropServices.ComTypes.IDataObject.EnumFormatEtc(DATADIR direction)
	{
		throw new NotImplementedException();
	}

	/// <summary>Provides a standard <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure that is logically equivalent to a more complex structure. Use this method to determine whether two different <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structures would return the same data, removing the need for duplicate rendering.</summary>
	/// <returns>This method supports the standard return values E_INVALIDARG, E_UNEXPECTED, and E_OUTOFMEMORY, as well as the following: ValueDescriptionS_OKThe returned <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure is different from the one that was passed.DATA_S_SAMEFORMATETCThe <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structures are the same and null is returned in the <paramref name="formatOut" /> parameter.DV_E_LINDEXThere is an invalid value for <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.lindex" />; currently, only -1 is supported.DV_E_FORMATETCThere is an invalid value for the <paramref name="pFormatetc" /> parameter.OLE_E_NOTRUNNINGThe application is not running.</returns>
	/// <param name="pformatetcIn">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device that the caller would like to use to retrieve data in a subsequent call such as <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />. The <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> member is not significant in this case and should be ignored.</param>
	/// <param name="pformatetcOut">When this method returns, contains a pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure that contains the most general information possible for a specific rendering, making it canonically equivalent to <paramref name="formatetIn" />. The caller must allocate this structure and the <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetCanonicalFormatEtc(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.FORMATETC@)" /> method must fill in the data. To retrieve data in a subsequent call such as <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />, the caller uses the supplied value of <paramref name="formatOut" />, unless the value supplied is null. This value is null if the method returns DATA_S_SAMEFORMATETC. The <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> member is not significant in this case and should be ignored. This parameter is passed uninitialized.</param>
	int System.Runtime.InteropServices.ComTypes.IDataObject.GetCanonicalFormatEtc(ref FORMATETC formatIn, out FORMATETC formatOut)
	{
		throw new NotImplementedException();
	}

	/// <summary>Obtains data from a source data object. The <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> method, which is called by a data consumer, renders the data described in the specified <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure and transfers it through the specified <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure. The caller then assumes responsibility for releasing the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure.</summary>
	/// <param name="formatetc">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device to use when passing the data. It is possible to specify more than one medium by using the Boolean OR operator, allowing the method to choose the best medium among those specified.</param>
	/// <param name="medium">When this method returns, contains a pointer to the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure that indicates the storage medium containing the returned data through its <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.tymed" /> member, and the responsibility for releasing the medium through the value of its <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member. If <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> is null, the receiver of the medium is responsible for releasing it; otherwise, <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> points to the IUnknown interface on the appropriate object so its Release method can be called. The medium must be allocated and filled in by <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />. This parameter is passed uninitialized.</param>
	/// <exception cref="T:System.OutOfMemoryException">There is not enough memory to perform this operation.</exception>
	void System.Runtime.InteropServices.ComTypes.IDataObject.GetData(ref FORMATETC format, out STGMEDIUM medium)
	{
		throw new NotImplementedException();
	}

	/// <summary>Obtains data from a source data object. This method, which is called by a data consumer, differs from the <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> method in that the caller must allocate and free the specified storage medium.</summary>
	/// <param name="formatetc">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device to use when passing the data. Only one medium can be specified in <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" />, and only the following <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> values are valid: <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_ISTORAGE" />, <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_ISTREAM" />, <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_HGLOBAL" />, or <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_FILE" />.</param>
	/// <param name="medium">A <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" />, passed by reference, that defines the storage medium containing the data being transferred. The medium must be allocated by the caller and filled in by <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetDataHere(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />. The caller must also free the medium. The implementation of this method must always supply a value of null for the <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member of the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure that this parameter points to.</param>
	void System.Runtime.InteropServices.ComTypes.IDataObject.GetDataHere(ref FORMATETC format, ref STGMEDIUM medium)
	{
		throw new NotImplementedException();
	}

	/// <summary>Determines whether the data object is capable of rendering the data described in the <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure. Objects attempting a paste or drop operation can call this method before calling <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> to get an indication of whether the operation may be successful.</summary>
	/// <returns>This method supports the standard return values E_INVALIDARG, E_UNEXPECTED, and E_OUTOFMEMORY, as well as the following: ValueDescriptionS_OKA subsequent call to <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> would probably be successful.DV_E_LINDEXAn invalid value for <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.lindex" />; currently, only -1 is supported.DV_E_FORMATETCAn invalid value for the <paramref name="pFormatetc" /> parameter.DV_E_TYMEDAn invalid <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.tymed" /> value.DV_E_DVASPECTAn invalid <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.dwAspect" /> value.OLE_E_NOTRUNNINGThe application is not running.</returns>
	/// <param name="formatetc">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device to use for the query.</param>
	int System.Runtime.InteropServices.ComTypes.IDataObject.QueryGetData(ref FORMATETC format)
	{
		throw new NotImplementedException();
	}

	/// <summary>Transfers data to the object that implements this method. This method is called by an object that contains a data source.</summary>
	/// <param name="pFormatetcIn">A <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format used by the data object when interpreting the data contained in the storage medium.</param>
	/// <param name="pmedium">A <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure, passed by reference, that defines the storage medium in which the data is being passed.</param>
	/// <param name="fRelease">true to specify that the data object called, which implements <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.SetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@,System.Boolean)" />, owns the storage medium after the call returns. This means that the data object must free the medium after it has been used by calling the ReleaseStgMedium function. false to specify that the caller retains ownership of the storage medium, and the data object called uses the storage medium for the duration of the call only.</param>
	/// <exception cref="T:System.NotImplementedException">This method does not support the type of the underlying data object.</exception>
	void System.Runtime.InteropServices.ComTypes.IDataObject.SetData(ref FORMATETC formatIn, ref STGMEDIUM medium, bool release)
	{
		throw new NotImplementedException();
	}

	/// <summary>Indicates whether the data object contains data in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format.</summary>
	/// <returns>true if the data object contains audio data; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public virtual bool ContainsAudio()
	{
		return GetDataPresent(DataFormats.WaveAudio, autoConvert: true);
	}

	/// <summary>Indicates whether the data object contains data that is in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop" /> format or can be converted to that format.</summary>
	/// <returns>true if the data object contains a file drop list; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public virtual bool ContainsFileDropList()
	{
		return GetDataPresent(DataFormats.FileDrop, autoConvert: true);
	}

	/// <summary>Indicates whether the data object contains data that is in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap" /> format or can be converted to that format.</summary>
	/// <returns>true if the data object contains image data; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public virtual bool ContainsImage()
	{
		return GetDataPresent(DataFormats.Bitmap, autoConvert: true);
	}

	/// <summary>Indicates whether the data object contains data in the <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format.</summary>
	/// <returns>true if the data object contains text data; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public virtual bool ContainsText()
	{
		return GetDataPresent(DataFormats.UnicodeText, autoConvert: true);
	}

	/// <summary>Indicates whether the data object contains text data in the format indicated by the specified <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</summary>
	/// <returns>true if the data object contains text data in the specified format; otherwise, false.</returns>
	/// <param name="format">One of the <see cref="T:System.Windows.Forms.TextDataFormat" /> values.</param>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="format" /> is not a valid <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</exception>
	/// <filterpriority>1</filterpriority>
	public virtual bool ContainsText(TextDataFormat format)
	{
		if (!Enum.IsDefined(typeof(TextDataFormat), format))
		{
			throw new InvalidEnumArgumentException($"Enum argument value '{format}' is not valid for TextDataFormat");
		}
		return GetDataPresent(TextFormatToDataFormat(format), autoConvert: true);
	}

	/// <summary>Retrieves an audio stream from the data object.</summary>
	/// <returns>A <see cref="T:System.IO.Stream" /> containing audio data or null if the data object does not contain any data in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format.</returns>
	/// <filterpriority>1</filterpriority>
	public virtual Stream GetAudioStream()
	{
		return (Stream)GetData(DataFormats.WaveAudio, autoConvert: true);
	}

	/// <summary>Returns the data associated with the specified data format.</summary>
	/// <returns>The data associated with the specified format, or null.</returns>
	/// <param name="format">The format of the data to retrieve. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
	/// <filterpriority>1</filterpriority>
	public virtual object GetData(string format)
	{
		return GetData(format, autoConvert: true);
	}

	/// <summary>Returns the data associated with the specified data format, using an automated conversion parameter to determine whether to convert the data to the format.</summary>
	/// <returns>The data associated with the specified format, or null.</returns>
	/// <param name="format">The format of the data to retrieve. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
	/// <param name="autoConvert">true to the convert data to the specified format; otherwise, false. </param>
	/// <filterpriority>1</filterpriority>
	public virtual object GetData(string format, bool autoConvert)
	{
		return ((!autoConvert) ? Entry.Find(entries, format) : Entry.FindConvertible(entries, format))?.Data;
	}

	/// <summary>Returns the data associated with the specified class type format.</summary>
	/// <returns>The data associated with the specified format, or null.</returns>
	/// <param name="format">A <see cref="T:System.Type" /> representing the format of the data to retrieve. </param>
	/// <filterpriority>1</filterpriority>
	public virtual object GetData(Type format)
	{
		return GetData(format.FullName, autoConvert: true);
	}

	/// <summary>Determines whether data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with, or can be converted to, the specified format.</summary>
	/// <returns>true if data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with, or can be converted to, the specified format; otherwise, false.</returns>
	/// <param name="format">The format to check for. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
	/// <filterpriority>1</filterpriority>
	public virtual bool GetDataPresent(string format)
	{
		return GetDataPresent(format, autoConvert: true);
	}

	/// <summary>Determines whether this <see cref="T:System.Windows.Forms.DataObject" /> contains data in the specified format or, optionally, contains data that can be converted to the specified format.</summary>
	/// <returns>true if the data is in, or can be converted to, the specified format; otherwise, false.</returns>
	/// <param name="format">The format to check for. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
	/// <param name="autoConvert">true to determine whether data stored in this <see cref="T:System.Windows.Forms.DataObject" /> can be converted to the specified format; false to check whether the data is in the specified format. </param>
	/// <filterpriority>1</filterpriority>
	public virtual bool GetDataPresent(string format, bool autoConvert)
	{
		if (autoConvert)
		{
			return Entry.FindConvertible(entries, format) != null;
		}
		return Entry.Find(entries, format) != null;
	}

	/// <summary>Determines whether data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with, or can be converted to, the specified format.</summary>
	/// <returns>true if data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with, or can be converted to, the specified format; otherwise, false.</returns>
	/// <param name="format">A <see cref="T:System.Type" /> representing the format to check for. </param>
	/// <filterpriority>1</filterpriority>
	public virtual bool GetDataPresent(Type format)
	{
		return GetDataPresent(format.FullName, autoConvert: true);
	}

	/// <summary>Retrieves a collection of file names from the data object. </summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> containing file names or null if the data object does not contain any data that is in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop" /> format or can be converted to that format.</returns>
	/// <filterpriority>1</filterpriority>
	public virtual StringCollection GetFileDropList()
	{
		return (StringCollection)GetData(DataFormats.FileDrop, autoConvert: true);
	}

	/// <summary>Returns a list of all formats that data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with or can be converted to.</summary>
	/// <returns>An array of type <see cref="T:System.String" />, containing a list of all formats that are supported by the data stored in this object.</returns>
	/// <filterpriority>1</filterpriority>
	public virtual string[] GetFormats()
	{
		return GetFormats(autoConvert: true);
	}

	/// <summary>Returns a list of all formats that data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with or can be converted to, using an automatic conversion parameter to determine whether to retrieve only native data formats or all formats that the data can be converted to.</summary>
	/// <returns>An array of type <see cref="T:System.String" />, containing a list of all formats that are supported by the data stored in this object.</returns>
	/// <param name="autoConvert">true to retrieve all formats that data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with, or can be converted to; false to retrieve only native data formats. </param>
	/// <filterpriority>1</filterpriority>
	public virtual string[] GetFormats(bool autoConvert)
	{
		return Entry.Entries(entries, autoConvert);
	}

	/// <summary>Retrieves an image from the data object.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" /> representing the image data in the data object or null if the data object does not contain any data that is in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap" /> format or can be converted to that format.</returns>
	/// <filterpriority>1</filterpriority>
	public virtual Image GetImage()
	{
		return (Image)GetData(DataFormats.Bitmap, autoConvert: true);
	}

	/// <summary>Retrieves text data from the data object in the <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format.</summary>
	/// <returns>The text data in the data object or <see cref="F:System.String.Empty" /> if the data object does not contain data in the <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format.</returns>
	/// <filterpriority>1</filterpriority>
	public virtual string GetText()
	{
		return (string)GetData(DataFormats.UnicodeText, autoConvert: true);
	}

	/// <summary>Retrieves text data from the data object in the format indicated by the specified <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</summary>
	/// <returns>The text data in the data object or <see cref="F:System.String.Empty" /> if the data object does not contain data in the specified format.</returns>
	/// <param name="format">One of the <see cref="T:System.Windows.Forms.TextDataFormat" /> values.</param>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="format" /> is not a valid <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</exception>
	/// <filterpriority>1</filterpriority>
	public virtual string GetText(TextDataFormat format)
	{
		if (!Enum.IsDefined(typeof(TextDataFormat), format))
		{
			throw new InvalidEnumArgumentException($"Enum argument value '{format}' is not valid for TextDataFormat");
		}
		return (string)GetData(TextFormatToDataFormat(format), autoConvert: false);
	}

	/// <summary>Adds a <see cref="T:System.Byte" /> array to the data object in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format after converting it to a <see cref="T:System.IO.Stream" />.</summary>
	/// <param name="audioBytes">A <see cref="T:System.Byte" /> array containing the audio data.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="audioBytes" /> is null.</exception>
	/// <filterpriority>1</filterpriority>
	public virtual void SetAudio(byte[] audioBytes)
	{
		if (audioBytes == null)
		{
			throw new ArgumentNullException("audioBytes");
		}
		MemoryStream audio = new MemoryStream(audioBytes);
		SetAudio(audio);
	}

	/// <summary>Adds a <see cref="T:System.IO.Stream" /> to the data object in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format.</summary>
	/// <param name="audioStream">A <see cref="T:System.IO.Stream" /> containing the audio data.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="audioStream" /> is null.</exception>
	/// <filterpriority>1</filterpriority>
	public virtual void SetAudio(Stream audioStream)
	{
		if (audioStream == null)
		{
			throw new ArgumentNullException("audioStream");
		}
		SetData(DataFormats.WaveAudio, audioStream);
	}

	/// <summary>Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject" /> using the object type as the data format.</summary>
	/// <param name="data">The data to store. </param>
	/// <filterpriority>1</filterpriority>
	public virtual void SetData(object data)
	{
		SetData(data.GetType(), data);
	}

	/// <summary>Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject" /> using the specified format and indicating whether the data can be converted to another format.</summary>
	/// <param name="format">The format associated with the data. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
	/// <param name="autoConvert">true to allow the data to be converted to another format; otherwise, false. </param>
	/// <param name="data">The data to store. </param>
	/// <filterpriority>1</filterpriority>
	public virtual void SetData(string format, bool autoConvert, object data)
	{
		Entry entry = Entry.Find(entries, format);
		if (entry == null)
		{
			entry = new Entry(format, data, autoConvert);
			lock (this)
			{
				if (entries == null)
				{
					entries = entry;
					return;
				}
				Entry next = entries;
				while (next.next != null)
				{
					next = next.next;
				}
				next.next = entry;
				return;
			}
		}
		entry.Data = data;
	}

	/// <summary>Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject" /> using the specified format.</summary>
	/// <param name="format">The format associated with the data. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
	/// <param name="data">The data to store. </param>
	/// <filterpriority>1</filterpriority>
	public virtual void SetData(string format, object data)
	{
		SetData(format, autoConvert: true, data);
	}

	/// <summary>Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject" /> using the specified type as the format.</summary>
	/// <param name="format">A <see cref="T:System.Type" /> representing the format associated with the data. </param>
	/// <param name="data">The data to store. </param>
	/// <filterpriority>1</filterpriority>
	public virtual void SetData(Type format, object data)
	{
		SetData(EnsureFormat(format), autoConvert: true, data);
	}

	/// <summary>Adds a collection of file names to the data object in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop" /> format.</summary>
	/// <param name="filePaths">A <see cref="T:System.Collections.Specialized.StringCollection" /> containing the file names.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="filePaths" /> is null.</exception>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Needs additional checks for valid paths, see MSDN")]
	public virtual void SetFileDropList(StringCollection filePaths)
	{
		if (filePaths == null)
		{
			throw new ArgumentNullException("filePaths");
		}
		SetData(DataFormats.FileDrop, filePaths);
	}

	/// <summary>Adds an <see cref="T:System.Drawing.Image" /> to the data object in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap" /> format.</summary>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to add to the data object.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="image" /> is null.</exception>
	/// <filterpriority>1</filterpriority>
	public virtual void SetImage(Image image)
	{
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		SetData(DataFormats.Bitmap, image);
	}

	/// <summary>Adds text data to the data object in the <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format.</summary>
	/// <param name="textData">The text to add to the data object.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="textData" /> is null or <see cref="F:System.String.Empty" />.</exception>
	/// <filterpriority>1</filterpriority>
	public virtual void SetText(string textData)
	{
		if (string.IsNullOrEmpty(textData))
		{
			throw new ArgumentNullException("text");
		}
		SetData(DataFormats.UnicodeText, textData);
	}

	/// <summary>Adds text data to the data object in the format indicated by the specified <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</summary>
	/// <param name="textData">The text to add to the data object.</param>
	/// <param name="format">One of the <see cref="T:System.Windows.Forms.TextDataFormat" /> values.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="textData" /> is null or <see cref="F:System.String.Empty" />.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="format" /> is not a valid <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</exception>
	/// <filterpriority>1</filterpriority>
	public virtual void SetText(string textData, TextDataFormat format)
	{
		if (string.IsNullOrEmpty(textData))
		{
			throw new ArgumentNullException("text");
		}
		if (!Enum.IsDefined(typeof(TextDataFormat), format))
		{
			throw new InvalidEnumArgumentException($"Enum argument value '{format}' is not valid for TextDataFormat");
		}
		switch (format)
		{
		case TextDataFormat.Text:
			SetData(DataFormats.Text, textData);
			break;
		case TextDataFormat.UnicodeText:
			SetData(DataFormats.UnicodeText, textData);
			break;
		case TextDataFormat.Rtf:
			SetData(DataFormats.Rtf, textData);
			break;
		case TextDataFormat.Html:
			SetData(DataFormats.Html, textData);
			break;
		case TextDataFormat.CommaSeparatedValue:
			SetData(DataFormats.CommaSeparatedValue, textData);
			break;
		}
	}

	internal string EnsureFormat(string name)
	{
		DataFormats.Format format = DataFormats.Format.Find(name);
		if (format == null)
		{
			format = DataFormats.Format.Add(name);
		}
		return format.Name;
	}

	internal string EnsureFormat(Type type)
	{
		return EnsureFormat(type.FullName);
	}

	private string TextFormatToDataFormat(TextDataFormat format)
	{
		return format switch
		{
			TextDataFormat.UnicodeText => DataFormats.UnicodeText, 
			TextDataFormat.Rtf => DataFormats.Rtf, 
			TextDataFormat.Html => DataFormats.Html, 
			TextDataFormat.CommaSeparatedValue => DataFormats.CommaSeparatedValue, 
			_ => DataFormats.Text, 
		};
	}
}
