using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using Microsoft.Win32;

namespace System.IO;

/// <summary>Provides the base class for both <see cref="T:System.IO.FileInfo" /> and <see cref="T:System.IO.DirectoryInfo" /> objects.</summary>
[Serializable]
[ComVisible(true)]
public abstract class FileSystemInfo : MarshalByRefObject, ISerializable
{
	internal MonoIOStat _data;

	internal int _dataInitialised = -1;

	private const int ERROR_INVALID_PARAMETER = 87;

	internal const int ERROR_ACCESS_DENIED = 5;

	/// <summary>Represents the fully qualified path of the directory or file.</summary>
	/// <exception cref="T:System.IO.PathTooLongException">The fully qualified path exceeds the system-defined maximum length.</exception>
	protected string FullPath;

	/// <summary>The path originally specified by the user, whether relative or absolute.</summary>
	protected string OriginalPath;

	private string _displayPath = "";

	/// <summary>Gets the full path of the directory or file.</summary>
	/// <returns>A string containing the full path.</returns>
	/// <exception cref="T:System.IO.PathTooLongException">The fully qualified path and file name exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	public virtual string FullName
	{
		[SecuritySafeCritical]
		get
		{
			return FullPath;
		}
	}

	internal virtual string UnsafeGetFullName
	{
		[SecurityCritical]
		get
		{
			return FullPath;
		}
	}

	/// <summary>Gets the string representing the extension part of the file.</summary>
	/// <returns>A string containing the <see cref="T:System.IO.FileSystemInfo" /> extension.</returns>
	public string Extension
	{
		get
		{
			int length = FullPath.Length;
			int num = length;
			while (--num >= 0)
			{
				char c = FullPath[num];
				if (c == '.')
				{
					return FullPath.Substring(num, length - num);
				}
				if (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar || c == Path.VolumeSeparatorChar)
				{
					break;
				}
			}
			return string.Empty;
		}
	}

	/// <summary>For files, gets the name of the file. For directories, gets the name of the last directory in the hierarchy if a hierarchy exists. Otherwise, the <see langword="Name" /> property gets the name of the directory.</summary>
	/// <returns>A string that is the name of the parent directory, the name of the last directory in the hierarchy, or the name of a file, including the file name extension.</returns>
	public abstract string Name { get; }

	/// <summary>Gets a value indicating whether the file or directory exists.</summary>
	/// <returns>
	///   <see langword="true" /> if the file or directory exists; otherwise, <see langword="false" />.</returns>
	public abstract bool Exists { get; }

	/// <summary>Gets or sets the creation time of the current file or directory.</summary>
	/// <returns>The creation date and time of the current <see cref="T:System.IO.FileSystemInfo" /> object.</returns>
	/// <exception cref="T:System.IO.IOException">
	///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid creation time.</exception>
	public DateTime CreationTime
	{
		get
		{
			return CreationTimeUtc.ToLocalTime();
		}
		set
		{
			CreationTimeUtc = value.ToUniversalTime();
		}
	}

	/// <summary>Gets or sets the creation time, in coordinated universal time (UTC), of the current file or directory.</summary>
	/// <returns>The creation date and time in UTC format of the current <see cref="T:System.IO.FileSystemInfo" /> object.</returns>
	/// <exception cref="T:System.IO.IOException">
	///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
	[ComVisible(false)]
	public DateTime CreationTimeUtc
	{
		[SecuritySafeCritical]
		get
		{
			if (_dataInitialised == -1)
			{
				Refresh();
			}
			if (_dataInitialised != 0)
			{
				__Error.WinIOError(_dataInitialised, DisplayPath);
			}
			return DateTime.FromFileTimeUtc(_data.CreationTime);
		}
		set
		{
			if (this is DirectoryInfo)
			{
				Directory.SetCreationTimeUtc(FullPath, value);
			}
			else
			{
				File.SetCreationTimeUtc(FullPath, value);
			}
			_dataInitialised = -1;
		}
	}

	/// <summary>Gets or sets the time the current file or directory was last accessed.</summary>
	/// <returns>The time that the current file or directory was last accessed.</returns>
	/// <exception cref="T:System.IO.IOException">
	///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid access time</exception>
	public DateTime LastAccessTime
	{
		get
		{
			return LastAccessTimeUtc.ToLocalTime();
		}
		set
		{
			LastAccessTimeUtc = value.ToUniversalTime();
		}
	}

	/// <summary>Gets or sets the time, in coordinated universal time (UTC), that the current file or directory was last accessed.</summary>
	/// <returns>The UTC time that the current file or directory was last accessed.</returns>
	/// <exception cref="T:System.IO.IOException">
	///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
	[ComVisible(false)]
	public DateTime LastAccessTimeUtc
	{
		[SecuritySafeCritical]
		get
		{
			if (_dataInitialised == -1)
			{
				Refresh();
			}
			if (_dataInitialised != 0)
			{
				__Error.WinIOError(_dataInitialised, DisplayPath);
			}
			return DateTime.FromFileTimeUtc(_data.LastAccessTime);
		}
		set
		{
			if (this is DirectoryInfo)
			{
				Directory.SetLastAccessTimeUtc(FullPath, value);
			}
			else
			{
				File.SetLastAccessTimeUtc(FullPath, value);
			}
			_dataInitialised = -1;
		}
	}

	/// <summary>Gets or sets the time when the current file or directory was last written to.</summary>
	/// <returns>The time the current file was last written.</returns>
	/// <exception cref="T:System.IO.IOException">
	///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid write time.</exception>
	public DateTime LastWriteTime
	{
		get
		{
			return LastWriteTimeUtc.ToLocalTime();
		}
		set
		{
			LastWriteTimeUtc = value.ToUniversalTime();
		}
	}

	/// <summary>Gets or sets the time, in coordinated universal time (UTC), when the current file or directory was last written to.</summary>
	/// <returns>The UTC time when the current file was last written to.</returns>
	/// <exception cref="T:System.IO.IOException">
	///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid write time.</exception>
	[ComVisible(false)]
	public DateTime LastWriteTimeUtc
	{
		[SecuritySafeCritical]
		get
		{
			if (_dataInitialised == -1)
			{
				Refresh();
			}
			if (_dataInitialised != 0)
			{
				__Error.WinIOError(_dataInitialised, DisplayPath);
			}
			return DateTime.FromFileTimeUtc(_data.LastWriteTime);
		}
		set
		{
			if (this is DirectoryInfo)
			{
				Directory.SetLastWriteTimeUtc(FullPath, value);
			}
			else
			{
				File.SetLastWriteTimeUtc(FullPath, value);
			}
			_dataInitialised = -1;
		}
	}

	/// <summary>Gets or sets the attributes for the current file or directory.</summary>
	/// <returns>
	///   <see cref="T:System.IO.FileAttributes" /> of the current <see cref="T:System.IO.FileSystemInfo" />.</returns>
	/// <exception cref="T:System.IO.FileNotFoundException">The specified file doesn't exist. Only thrown when setting the property value.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid. For example, it's on an unmapped drive. Only thrown when setting the property value.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller doesn't have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">The caller attempts to set an invalid file attribute.  
	///  -or-  
	///  The user attempts to set an attribute value but doesn't have write permission.</exception>
	/// <exception cref="T:System.IO.IOException">
	///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data.</exception>
	public FileAttributes Attributes
	{
		[SecuritySafeCritical]
		get
		{
			if (_dataInitialised == -1)
			{
				Refresh();
			}
			if (_dataInitialised != 0)
			{
				__Error.WinIOError(_dataInitialised, DisplayPath);
			}
			return _data.fileAttributes;
		}
		[SecuritySafeCritical]
		set
		{
			if (!MonoIO.SetFileAttributes(FullPath, value, out var error))
			{
				MonoIOError num = error;
				switch (num)
				{
				case MonoIOError.ERROR_INVALID_PARAMETER:
					throw new ArgumentException(Environment.GetResourceString("Invalid File or Directory attributes value."));
				case MonoIOError.ERROR_ACCESS_DENIED:
					throw new ArgumentException(Environment.GetResourceString("Access to the path is denied."));
				}
				__Error.WinIOError((int)num, DisplayPath);
			}
			_dataInitialised = -1;
		}
	}

	internal string DisplayPath
	{
		get
		{
			return _displayPath;
		}
		set
		{
			_displayPath = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemInfo" /> class.</summary>
	protected FileSystemInfo()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemInfo" /> class with serialized data.</summary>
	/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
	/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
	/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> is null.</exception>
	protected FileSystemInfo(SerializationInfo info, StreamingContext context)
	{
		if (info == null)
		{
			throw new ArgumentNullException("info");
		}
		FullPath = Path.GetFullPathInternal(info.GetString("FullPath"));
		OriginalPath = info.GetString("OriginalPath");
		_dataInitialised = -1;
	}

	[SecurityCritical]
	internal void InitializeFrom(Win32Native.WIN32_FIND_DATA findData)
	{
		throw new NotImplementedException();
	}

	/// <summary>Deletes a file or directory.</summary>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
	/// <exception cref="T:System.IO.IOException">There is an open handle on the file or directory, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
	public abstract void Delete();

	/// <summary>Refreshes the state of the object.</summary>
	/// <exception cref="T:System.IO.IOException">A device such as a disk drive is not ready.</exception>
	[SecuritySafeCritical]
	public void Refresh()
	{
		_dataInitialised = File.FillAttributeInfo(FullPath, ref _data, tryagain: false, returnErrorOnNotFound: false);
	}

	/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name and additional exception information.</summary>
	/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
	/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
	[SecurityCritical]
	[ComVisible(false)]
	public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("OriginalPath", OriginalPath, typeof(string));
		info.AddValue("FullPath", FullPath, typeof(string));
	}
}
