using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Windows.Forms;

/// <summary>Provides methods to place data on and retrieve data from the system Clipboard. This class cannot be inherited.</summary>
/// <filterpriority>1</filterpriority>
public sealed class Clipboard
{
	private Clipboard()
	{
	}

	private static bool ConvertToClipboardData(ref int type, object obj, out byte[] data)
	{
		data = null;
		return false;
	}

	private static bool ConvertFromClipboardData(int type, IntPtr data, out object obj)
	{
		obj = null;
		if (data == IntPtr.Zero)
		{
			return false;
		}
		return false;
	}

	/// <summary>Removes all data from the Clipboard.</summary>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <filterpriority>1</filterpriority>
	public static void Clear()
	{
		IntPtr handle = XplatUI.ClipboardOpen(primary_selection: false);
		XplatUI.ClipboardStore(handle, null, 0, null);
	}

	/// <summary>Indicates whether there is data on the Clipboard in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format.</summary>
	/// <returns>true if there is audio data on the Clipboard; otherwise, false.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool ContainsAudio()
	{
		return ClipboardContainsFormat(DataFormats.WaveAudio);
	}

	/// <summary>Indicates whether there is data on the Clipboard that is in the specified format or can be converted to that format. </summary>
	/// <returns>true if there is data on the Clipboard that is in the specified <paramref name="format" /> or can be converted to that format; otherwise, false.</returns>
	/// <param name="format">The format of the data to look for. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats.</param>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	public static bool ContainsData(string format)
	{
		return ClipboardContainsFormat(format);
	}

	/// <summary>Indicates whether there is data on the Clipboard that is in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop" /> format or can be converted to that format.</summary>
	/// <returns>true if there is a file drop list on the Clipboard; otherwise, false.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool ContainsFileDropList()
	{
		return ClipboardContainsFormat(DataFormats.FileDrop);
	}

	/// <summary>Indicates whether there is data on the Clipboard that is in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap" /> format or can be converted to that format.</summary>
	/// <returns>true if there is image data on the Clipboard; otherwise, false.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool ContainsImage()
	{
		return ClipboardContainsFormat(DataFormats.Bitmap);
	}

	/// <summary>Indicates whether there is data on the Clipboard in the <see cref="F:System.Windows.Forms.TextDataFormat.Text" /> or <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format, depending on the operating system.</summary>
	/// <returns>true if there is text data on the Clipboard; otherwise, false.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool ContainsText()
	{
		return ClipboardContainsFormat(DataFormats.Text, DataFormats.UnicodeText);
	}

	/// <summary>Indicates whether there is text data on the Clipboard in the format indicated by the specified <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</summary>
	/// <returns>true if there is text data on the Clipboard in the value specified for <paramref name="format" />; otherwise, false.</returns>
	/// <param name="format">One of the <see cref="T:System.Windows.Forms.TextDataFormat" /> values.</param>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="format" /> is not a valid <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool ContainsText(TextDataFormat format)
	{
		return format switch
		{
			TextDataFormat.Text => ClipboardContainsFormat(DataFormats.Text), 
			TextDataFormat.UnicodeText => ClipboardContainsFormat(DataFormats.UnicodeText), 
			TextDataFormat.Rtf => ClipboardContainsFormat(DataFormats.Rtf), 
			TextDataFormat.Html => ClipboardContainsFormat(DataFormats.Html), 
			TextDataFormat.CommaSeparatedValue => ClipboardContainsFormat(DataFormats.CommaSeparatedValue), 
			_ => false, 
		};
	}

	/// <summary>Retrieves an audio stream from the Clipboard.</summary>
	/// <returns>A <see cref="T:System.IO.Stream" /> containing audio data or null if the Clipboard does not contain any data in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Stream GetAudioStream()
	{
		IDataObject dataObject = GetDataObject();
		if (dataObject == null)
		{
			return null;
		}
		return (Stream)dataObject.GetData(DataFormats.WaveAudio, autoConvert: true);
	}

	/// <summary>Retrieves data from the Clipboard in the specified format.</summary>
	/// <returns>An <see cref="T:System.Object" /> representing the Clipboard data or null if the Clipboard does not contain any data that is in the specified <paramref name="format" /> or can be converted to that format.</returns>
	/// <param name="format">The format of the data to retrieve. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats.</param>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	public static object GetData(string format)
	{
		return GetDataObject()?.GetData(format, autoConvert: true);
	}

	/// <summary>Retrieves the data that is currently on the system Clipboard.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.IDataObject" /> that represents the data currently on the Clipboard, or null if there is no data on the Clipboard.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">Data could not be retrieved from the Clipboard. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode and the <see cref="P:System.Windows.Forms.Application.MessageLoop" /> property value is true. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static IDataObject GetDataObject()
	{
		return GetDataObject(primary_selection: false);
	}

	/// <summary>Retrieves a collection of file names from the Clipboard. </summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> containing file names or null if the Clipboard does not contain any data that is in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop" /> format or can be converted to that format.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static StringCollection GetFileDropList()
	{
		IDataObject dataObject = GetDataObject();
		if (dataObject == null)
		{
			return null;
		}
		return (StringCollection)dataObject.GetData(DataFormats.FileDrop, autoConvert: true);
	}

	/// <summary>Retrieves an image from the Clipboard.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" /> representing the Clipboard image data or null if the Clipboard does not contain any data that is in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap" /> format or can be converted to that format.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Image GetImage()
	{
		IDataObject dataObject = GetDataObject();
		if (dataObject == null)
		{
			return null;
		}
		return (Image)dataObject.GetData(DataFormats.Bitmap, autoConvert: true);
	}

	/// <summary>Retrieves text data from the Clipboard in the <see cref="F:System.Windows.Forms.TextDataFormat.Text" /> or <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format, depending on the operating system.</summary>
	/// <returns>The Clipboard text data or <see cref="F:System.String.Empty" /> if the Clipboard does not contain data in the <see cref="F:System.Windows.Forms.TextDataFormat.Text" /> or <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format, depending on the operating system.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static string GetText()
	{
		return GetText(TextDataFormat.UnicodeText);
	}

	/// <summary>Retrieves text data from the Clipboard in the format indicated by the specified <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</summary>
	/// <returns>The Clipboard text data or <see cref="F:System.String.Empty" /> if the Clipboard does not contain data in the specified format.</returns>
	/// <param name="format">One of the <see cref="T:System.Windows.Forms.TextDataFormat" /> values.</param>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="format" /> is not a valid <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static string GetText(TextDataFormat format)
	{
		if (!Enum.IsDefined(typeof(TextDataFormat), format))
		{
			throw new InvalidEnumArgumentException($"Enum argument value '{format}' is not valid for TextDataFormat");
		}
		IDataObject dataObject = GetDataObject();
		if (dataObject == null)
		{
			return string.Empty;
		}
		string text = format switch
		{
			TextDataFormat.UnicodeText => (string)dataObject.GetData(DataFormats.UnicodeText, autoConvert: true), 
			TextDataFormat.Rtf => (string)dataObject.GetData(DataFormats.Rtf, autoConvert: true), 
			TextDataFormat.Html => (string)dataObject.GetData(DataFormats.Html, autoConvert: true), 
			TextDataFormat.CommaSeparatedValue => (string)dataObject.GetData(DataFormats.CommaSeparatedValue, autoConvert: true), 
			_ => (string)dataObject.GetData(DataFormats.Text, autoConvert: true), 
		};
		return (text != null) ? text : string.Empty;
	}

	/// <summary>Clears the Clipboard and then adds a <see cref="T:System.Byte" /> array in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format after converting it to a <see cref="T:System.IO.Stream" />.</summary>
	/// <param name="audioBytes">A <see cref="T:System.Byte" /> array containing the audio data.</param>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="audioBytes" /> is null.</exception>
	/// <filterpriority>1</filterpriority>
	public static void SetAudio(byte[] audioBytes)
	{
		if (audioBytes == null)
		{
			throw new ArgumentNullException("audioBytes");
		}
		MemoryStream audio = new MemoryStream(audioBytes);
		SetAudio(audio);
	}

	/// <summary>Clears the Clipboard and then adds a <see cref="T:System.IO.Stream" /> in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format.</summary>
	/// <param name="audioStream">A <see cref="T:System.IO.Stream" /> containing the audio data.</param>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="audioStream" /> is null.</exception>
	/// <filterpriority>1</filterpriority>
	public static void SetAudio(Stream audioStream)
	{
		if (audioStream == null)
		{
			throw new ArgumentNullException("audioStream");
		}
		SetData(DataFormats.WaveAudio, audioStream);
	}

	/// <summary>Clears the Clipboard and then adds data in the specified format.</summary>
	/// <param name="format">The format of the data to set. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats.</param>
	/// <param name="data">An <see cref="T:System.Object" /> representing the data to add.</param>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="data" /> is null.</exception>
	public static void SetData(string format, object data)
	{
		if (data == null)
		{
			throw new ArgumentNullException("data");
		}
		DataObject dataObject = new DataObject(format, data);
		SetDataObject(dataObject);
	}

	/// <summary>Clears the Clipboard and then places nonpersistent data on it.</summary>
	/// <param name="data">The data to place on the Clipboard. </param>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">Data could not be placed on the Clipboard. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="data" /> is null. </exception>
	/// <filterpriority>1</filterpriority>
	public static void SetDataObject(object data)
	{
		SetDataObject(data, copy: false);
	}

	/// <summary>Clears the Clipboard and then places data on it and specifies whether the data should remain after the application exits.</summary>
	/// <param name="data">The data to place on the Clipboard. </param>
	/// <param name="copy">true if you want data to remain on the Clipboard after this application exits; otherwise, false. </param>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">Data could not be placed on the Clipboard. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="data" /> is null. </exception>
	/// <filterpriority>1</filterpriority>
	public static void SetDataObject(object data, bool copy)
	{
		SetDataObject(data, copy, 10, 100);
	}

	internal static void SetDataObjectImpl(object data, bool copy)
	{
		XplatUI.ObjectToClipboard converter = ConvertToClipboardData;
		IntPtr handle = XplatUI.ClipboardOpen(primary_selection: false);
		XplatUI.ClipboardStore(handle, null, 0, null);
		int type = -1;
		if (data is IDataObject)
		{
			IDataObject dataObject = data as IDataObject;
			string[] formats = dataObject.GetFormats();
			for (int i = 0; i < formats.Length; i++)
			{
				DataFormats.Format format = DataFormats.GetFormat(formats[i]);
				if (format != null && format.Name != DataFormats.StringFormat)
				{
					type = format.Id;
				}
				object data2 = dataObject.GetData(formats[i]);
				if (IsDataSerializable(data2))
				{
					format.is_serializable = true;
				}
				XplatUI.ClipboardStore(handle, data2, type, converter);
			}
		}
		else
		{
			DataFormats.Format format = DataFormats.Format.Find(data.GetType().FullName);
			if (format != null && format.Name != DataFormats.StringFormat)
			{
				type = format.Id;
			}
			XplatUI.ClipboardStore(handle, data, type, converter);
		}
		XplatUI.ClipboardClose(handle);
	}

	private static bool IsDataSerializable(object obj)
	{
		if (obj is ISerializable)
		{
			return true;
		}
		AttributeCollection attributes = TypeDescriptor.GetAttributes(obj);
		return attributes[typeof(SerializableAttribute)] != null;
	}

	/// <summary>Clears the Clipboard and then attempts to place data on it the specified number of times and with the specified delay between attempts, optionally leaving the data on the Clipboard after the application exits.</summary>
	/// <param name="data">The data to place on the Clipboard.</param>
	/// <param name="copy">true if you want data to remain on the Clipboard after this application exits; otherwise, false.</param>
	/// <param name="retryTimes">The number of times to attempt placing the data on the Clipboard.</param>
	/// <param name="retryDelay">The number of milliseconds to pause between attempts. </param>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method. </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="data" /> is null.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="retryTimes" /> is less than zero.-or-<paramref name="retryDelay" /> is less than zero.</exception>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">Data could not be placed on the Clipboard. This typically occurs when the Clipboard is being used by another process.</exception>
	public static void SetDataObject(object data, bool copy, int retryTimes, int retryDelay)
	{
		if (data == null)
		{
			throw new ArgumentNullException("data");
		}
		if (retryTimes < 0)
		{
			throw new ArgumentOutOfRangeException("retryTimes");
		}
		if (retryDelay < 0)
		{
			throw new ArgumentOutOfRangeException("retryDelay");
		}
		bool flag = true;
		do
		{
			flag = false;
			retryTimes--;
			try
			{
				SetDataObjectImpl(data, copy);
			}
			catch (ExternalException)
			{
				if (retryTimes <= 0)
				{
					throw;
				}
				flag = true;
				Thread.Sleep(retryDelay);
			}
		}
		while (flag && retryTimes > 0);
	}

	/// <summary>Clears the Clipboard and then adds a collection of file names in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop" /> format.</summary>
	/// <param name="filePaths">A <see cref="T:System.Collections.Specialized.StringCollection" /> containing the file names.</param>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="filePaths" /> is null.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="filePaths" /> does not contain any strings.-or-At least one of the strings in <paramref name="filePaths" /> is <see cref="F:System.String.Empty" />, contains only white space, contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />, is null, contains a colon (:), or exceeds the system-defined maximum length.See the <see cref="P:System.Exception.InnerException" /> property of the <see cref="T:System.ArgumentException" /> for more information.</exception>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Needs additional checks for valid paths, see MSDN")]
	public static void SetFileDropList(StringCollection filePaths)
	{
		if (filePaths == null)
		{
			throw new ArgumentNullException("filePaths");
		}
		SetData(DataFormats.FileDrop, filePaths);
	}

	/// <summary>Clears the Clipboard and then adds an <see cref="T:System.Drawing.Image" /> in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap" /> format.</summary>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to add to the Clipboard.</param>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="image" /> is null.</exception>
	/// <filterpriority>1</filterpriority>
	public static void SetImage(Image image)
	{
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		SetData(DataFormats.Bitmap, image);
	}

	/// <summary>Clears the Clipboard and then adds text data in the <see cref="F:System.Windows.Forms.TextDataFormat.Text" /> or <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format, depending on the operating system.</summary>
	/// <param name="text">The text to add to the Clipboard.</param>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="text" /> is null or <see cref="F:System.String.Empty" />.</exception>
	/// <filterpriority>1</filterpriority>
	public static void SetText(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			throw new ArgumentNullException("text");
		}
		SetData(DataFormats.UnicodeText, text);
	}

	/// <summary>Clears the Clipboard and then adds text data in the format indicated by the specified <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</summary>
	/// <param name="text">The text to add to the Clipboard.</param>
	/// <param name="format">One of the <see cref="T:System.Windows.Forms.TextDataFormat" /> values.</param>
	/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
	/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="text" /> is null or <see cref="F:System.String.Empty" />.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="format" /> is not a valid <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</exception>
	/// <filterpriority>1</filterpriority>
	public static void SetText(string text, TextDataFormat format)
	{
		if (string.IsNullOrEmpty(text))
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
			SetData(DataFormats.Text, text);
			break;
		case TextDataFormat.UnicodeText:
			SetData(DataFormats.UnicodeText, text);
			break;
		case TextDataFormat.Rtf:
			SetData(DataFormats.Rtf, text);
			break;
		case TextDataFormat.Html:
			SetData(DataFormats.Html, text);
			break;
		case TextDataFormat.CommaSeparatedValue:
			SetData(DataFormats.CommaSeparatedValue, text);
			break;
		}
	}

	internal static IDataObject GetDataObject(bool primary_selection)
	{
		XplatUI.ClipboardToObject converter = ConvertFromClipboardData;
		IntPtr handle = XplatUI.ClipboardOpen(primary_selection);
		int[] array = XplatUI.ClipboardAvailableFormats(handle);
		if (array == null)
		{
			return null;
		}
		DataObject dataObject = new DataObject();
		for (int i = 0; i < array.Length; i++)
		{
			DataFormats.Format format = DataFormats.GetFormat(array[i]);
			if (format == null)
			{
				continue;
			}
			object obj = XplatUI.ClipboardRetrieve(handle, array[i], converter);
			if (obj != null)
			{
				dataObject.SetData(format.Name, obj);
				if (format.Name == DataFormats.Dib)
				{
					dataObject.SetData(DataFormats.Bitmap, obj);
				}
			}
		}
		XplatUI.ClipboardClose(handle);
		return dataObject;
	}

	internal static bool ClipboardContainsFormat(params string[] formats)
	{
		IntPtr handle = XplatUI.ClipboardOpen(primary_selection: false);
		int[] array = XplatUI.ClipboardAvailableFormats(handle);
		if (array == null)
		{
			return false;
		}
		int[] array2 = array;
		foreach (int id in array2)
		{
			DataFormats.Format format = DataFormats.GetFormat(id);
			if (format != null && ((IList)formats).Contains((object)format.Name))
			{
				return true;
			}
		}
		return false;
	}
}
