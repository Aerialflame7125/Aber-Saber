using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using Microsoft.Win32.SafeHandles;

namespace System.IO;

/// <summary>Exposes instance methods for creating, moving, and enumerating through directories and subdirectories. This class cannot be inherited.</summary>
[Serializable]
[ComVisible(true)]
public sealed class DirectoryInfo : FileSystemInfo
{
	private string current;

	private string parent;

	/// <summary>Gets a value indicating whether the directory exists.</summary>
	/// <returns>
	///   <see langword="true" /> if the directory exists; otherwise, <see langword="false" />.</returns>
	public override bool Exists
	{
		get
		{
			if (_dataInitialised == -1)
			{
				Refresh();
			}
			if (_data.fileAttributes == (FileAttributes)(-1))
			{
				return false;
			}
			if ((_data.fileAttributes & FileAttributes.Directory) == 0)
			{
				return false;
			}
			return true;
		}
	}

	/// <summary>Gets the name of this <see cref="T:System.IO.DirectoryInfo" /> instance.</summary>
	/// <returns>The directory name.</returns>
	public override string Name => current;

	/// <summary>Gets the parent directory of a specified subdirectory.</summary>
	/// <returns>The parent directory, or <see langword="null" /> if the path is null or if the file path denotes a root (such as "\", "C:", or * "\\server\share").</returns>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	public DirectoryInfo Parent
	{
		get
		{
			if (parent == null || parent.Length == 0)
			{
				return null;
			}
			return new DirectoryInfo(parent);
		}
	}

	/// <summary>Gets the root portion of the directory.</summary>
	/// <returns>An object that represents the root of the directory.</returns>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	public DirectoryInfo Root
	{
		get
		{
			string pathRoot = Path.GetPathRoot(FullPath);
			if (pathRoot == null)
			{
				return null;
			}
			return new DirectoryInfo(pathRoot);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.IO.DirectoryInfo" /> class on the specified path.</summary>
	/// <param name="path">A string specifying the path on which to create the <see langword="DirectoryInfo" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> contains invalid characters such as ", &lt;, &gt;, or |.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	public DirectoryInfo(string path)
		: this(path, simpleOriginalPath: false)
	{
	}

	internal DirectoryInfo(string path, bool simpleOriginalPath)
	{
		CheckPath(path);
		FullPath = Path.GetFullPath(path);
		if (simpleOriginalPath)
		{
			OriginalPath = Path.GetFileName(FullPath);
		}
		else
		{
			OriginalPath = path;
		}
		Initialize();
	}

	private DirectoryInfo(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
		Initialize();
	}

	private void Initialize()
	{
		int num = FullPath.Length - 1;
		if (num > 1 && FullPath[num] == Path.DirectorySeparatorChar)
		{
			num--;
		}
		int num2 = FullPath.LastIndexOf(Path.DirectorySeparatorChar, num);
		if (num2 == -1 || (num2 == 0 && num == 0))
		{
			current = FullPath;
			parent = null;
			return;
		}
		current = FullPath.Substring(num2 + 1, num - num2);
		if (num2 == 0 && !Environment.IsRunningOnWindows)
		{
			parent = Path.DirectorySeparatorStr;
		}
		else
		{
			parent = FullPath.Substring(0, num2);
		}
		if (Environment.IsRunningOnWindows && parent.Length == 2 && parent[1] == ':' && char.IsLetter(parent[0]))
		{
			parent += Path.DirectorySeparatorChar;
		}
	}

	/// <summary>Creates a directory.</summary>
	/// <exception cref="T:System.IO.IOException">The directory cannot be created.</exception>
	public void Create()
	{
		Directory.CreateDirectory(FullPath);
	}

	/// <summary>Creates a subdirectory or subdirectories on the specified path. The specified path can be relative to this instance of the <see cref="T:System.IO.DirectoryInfo" /> class.</summary>
	/// <param name="path">The specified path. This cannot be a different disk volume or Universal Naming Convention (UNC) name.</param>
	/// <returns>The last directory specified in <paramref name="path" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> does not specify a valid file path or contains invalid <see langword="DirectoryInfo" /> characters.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
	/// <exception cref="T:System.IO.IOException">The subdirectory cannot be created.  
	///  -or-  
	///  A file or directory already has the name specified by <paramref name="path" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have code access permission to create the directory.  
	///  -or-  
	///  The caller does not have code access permission to read the directory described by the returned <see cref="T:System.IO.DirectoryInfo" /> object.  This can occur when the <paramref name="path" /> parameter describes an existing directory.</exception>
	/// <exception cref="T:System.NotSupportedException">
	///   <paramref name="path" /> contains a colon character (:) that is not part of a drive label ("C:\").</exception>
	public DirectoryInfo CreateSubdirectory(string path)
	{
		CheckPath(path);
		path = Path.Combine(FullPath, path);
		Directory.CreateDirectory(path);
		return new DirectoryInfo(path);
	}

	/// <summary>Returns a file list from the current directory.</summary>
	/// <returns>An array of type <see cref="T:System.IO.FileInfo" />.</returns>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid, such as being on an unmapped drive.</exception>
	public FileInfo[] GetFiles()
	{
		return GetFiles("*");
	}

	/// <summary>Returns a file list from the current directory matching the given search pattern.</summary>
	/// <param name="searchPattern">The search string to match against the names of files.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <returns>An array of type <see cref="T:System.IO.FileInfo" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid (for example, it is on an unmapped drive).</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	public FileInfo[] GetFiles(string searchPattern)
	{
		if (searchPattern == null)
		{
			throw new ArgumentNullException("searchPattern");
		}
		string[] files = Directory.GetFiles(FullPath, searchPattern);
		FileInfo[] array = new FileInfo[files.Length];
		int num = 0;
		string[] array2 = files;
		foreach (string fileName in array2)
		{
			array[num++] = new FileInfo(fileName);
		}
		return array;
	}

	/// <summary>Returns the subdirectories of the current directory.</summary>
	/// <returns>An array of <see cref="T:System.IO.DirectoryInfo" /> objects.</returns>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid, such as being on an unmapped drive.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	public DirectoryInfo[] GetDirectories()
	{
		return GetDirectories("*");
	}

	/// <summary>Returns an array of directories in the current <see cref="T:System.IO.DirectoryInfo" /> matching the given search criteria.</summary>
	/// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <returns>An array of type <see langword="DirectoryInfo" /> matching <paramref name="searchPattern" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see langword="DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	public DirectoryInfo[] GetDirectories(string searchPattern)
	{
		if (searchPattern == null)
		{
			throw new ArgumentNullException("searchPattern");
		}
		string[] directories = Directory.GetDirectories(FullPath, searchPattern);
		DirectoryInfo[] array = new DirectoryInfo[directories.Length];
		int num = 0;
		string[] array2 = directories;
		foreach (string path in array2)
		{
			array[num++] = new DirectoryInfo(path);
		}
		return array;
	}

	/// <summary>Returns an array of strongly typed <see cref="T:System.IO.FileSystemInfo" /> entries representing all the files and subdirectories in a directory.</summary>
	/// <returns>An array of strongly typed <see cref="T:System.IO.FileSystemInfo" /> entries.</returns>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid (for example, it is on an unmapped drive).</exception>
	public FileSystemInfo[] GetFileSystemInfos()
	{
		return GetFileSystemInfos("*");
	}

	/// <summary>Retrieves an array of strongly typed <see cref="T:System.IO.FileSystemInfo" /> objects representing the files and subdirectories that match the specified search criteria.</summary>
	/// <param name="searchPattern">The search string to match against the names of directories and files.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <returns>An array of strongly typed <see langword="FileSystemInfo" /> objects matching the search criteria.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	public FileSystemInfo[] GetFileSystemInfos(string searchPattern)
	{
		return GetFileSystemInfos(searchPattern, SearchOption.TopDirectoryOnly);
	}

	/// <summary>Retrieves an array of <see cref="T:System.IO.FileSystemInfo" /> objects that represent the files and subdirectories matching the specified search criteria.</summary>
	/// <param name="searchPattern">The search string to match against the names of directories and filesa.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories. The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
	/// <returns>An array of file system entries that match the search criteria.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	public FileSystemInfo[] GetFileSystemInfos(string searchPattern, SearchOption searchOption)
	{
		if (searchPattern == null)
		{
			throw new ArgumentNullException("searchPattern");
		}
		if (searchOption != 0 && searchOption != SearchOption.AllDirectories)
		{
			throw new ArgumentOutOfRangeException("searchOption", "Must be TopDirectoryOnly or AllDirectories");
		}
		if (!Directory.Exists(FullPath))
		{
			throw new IOException("Invalid directory");
		}
		List<FileSystemInfo> list = new List<FileSystemInfo>();
		InternalGetFileSystemInfos(searchPattern, searchOption, list);
		return list.ToArray();
	}

	private void InternalGetFileSystemInfos(string searchPattern, SearchOption searchOption, List<FileSystemInfo> infos)
	{
		string[] directories = Directory.GetDirectories(FullPath, searchPattern);
		string[] files = Directory.GetFiles(FullPath, searchPattern);
		Array.ForEach(directories, delegate(string dir)
		{
			infos.Add(new DirectoryInfo(dir));
		});
		Array.ForEach(files, delegate(string file)
		{
			infos.Add(new FileInfo(file));
		});
		if (directories.Length != 0 && searchOption != 0)
		{
			string[] array = directories;
			for (int i = 0; i < array.Length; i++)
			{
				new DirectoryInfo(array[i]).InternalGetFileSystemInfos(searchPattern, searchOption, infos);
			}
		}
	}

	/// <summary>Deletes this <see cref="T:System.IO.DirectoryInfo" /> if it is empty.</summary>
	/// <exception cref="T:System.UnauthorizedAccessException">The directory contains a read-only file.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory described by this <see cref="T:System.IO.DirectoryInfo" /> object does not exist or could not be found.</exception>
	/// <exception cref="T:System.IO.IOException">The directory is not empty.  
	///  -or-  
	///  The directory is the application's current working directory.  
	///  -or-  
	///  There is an open handle on the directory, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories. For more information, see How to: Enumerate Directories and Files.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	public override void Delete()
	{
		Delete(recursive: false);
	}

	/// <summary>Deletes this instance of a <see cref="T:System.IO.DirectoryInfo" />, specifying whether to delete subdirectories and files.</summary>
	/// <param name="recursive">
	///   <see langword="true" /> to delete this directory, its subdirectories, and all files; otherwise, <see langword="false" />.</param>
	/// <exception cref="T:System.UnauthorizedAccessException">The directory contains a read-only file.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory described by this <see cref="T:System.IO.DirectoryInfo" /> object does not exist or could not be found.</exception>
	/// <exception cref="T:System.IO.IOException">The directory is read-only.  
	///  -or-  
	///  The directory contains one or more files or subdirectories and <paramref name="recursive" /> is <see langword="false" />.  
	///  -or-  
	///  The directory is the application's current working directory.  
	///  -or-  
	///  There is an open handle on the directory or on one of its files, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	public void Delete(bool recursive)
	{
		Directory.Delete(FullPath, recursive);
	}

	/// <summary>Moves a <see cref="T:System.IO.DirectoryInfo" /> instance and its contents to a new path.</summary>
	/// <param name="destDirName">The name and path to which to move this directory. The destination cannot be another disk volume or a directory with the identical name. It can be an existing directory to which you want to add this directory as a subdirectory.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="destDirName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="destDirName" /> is an empty string (''").</exception>
	/// <exception cref="T:System.IO.IOException">An attempt was made to move a directory to a different volume.  
	///  -or-  
	///  <paramref name="destDirName" /> already exists.  
	///  -or-  
	///  You are not authorized to access this path.  
	///  -or-  
	///  The directory being moved and the destination directory have the same name.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The destination directory cannot be found.</exception>
	public void MoveTo(string destDirName)
	{
		if (destDirName == null)
		{
			throw new ArgumentNullException("destDirName");
		}
		if (destDirName.Length == 0)
		{
			throw new ArgumentException("An empty file name is not valid.", "destDirName");
		}
		Directory.Move(FullPath, Path.GetFullPath(destDirName));
		FullPath = (OriginalPath = destDirName);
		Initialize();
	}

	/// <summary>Returns the original path that was passed by the user.</summary>
	/// <returns>The original path that was passed by the user.</returns>
	public override string ToString()
	{
		return OriginalPath;
	}

	/// <summary>Returns an array of directories in the current <see cref="T:System.IO.DirectoryInfo" /> matching the given search criteria and using a value to determine whether to search subdirectories.</summary>
	/// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories.</param>
	/// <returns>An array of type <see langword="DirectoryInfo" /> matching <paramref name="searchPattern" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see langword="DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	public DirectoryInfo[] GetDirectories(string searchPattern, SearchOption searchOption)
	{
		string[] directories = Directory.GetDirectories(FullPath, searchPattern, searchOption);
		DirectoryInfo[] array = new DirectoryInfo[directories.Length];
		for (int i = 0; i < directories.Length; i++)
		{
			string path = directories[i];
			array[i] = new DirectoryInfo(path);
		}
		return array;
	}

	internal int GetFilesSubdirs(ArrayList l, string pattern)
	{
		FileInfo[] array = null;
		try
		{
			array = GetFiles(pattern);
		}
		catch (UnauthorizedAccessException)
		{
			return 0;
		}
		int num = array.Length;
		l.Add(array);
		DirectoryInfo[] directories = GetDirectories();
		foreach (DirectoryInfo directoryInfo in directories)
		{
			num += directoryInfo.GetFilesSubdirs(l, pattern);
		}
		return num;
	}

	/// <summary>Returns a file list from the current directory matching the given search pattern and using a value to determine whether to search subdirectories.</summary>
	/// <param name="searchPattern">The search string to match against the names of files.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories.</param>
	/// <returns>An array of type <see cref="T:System.IO.FileInfo" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid (for example, it is on an unmapped drive).</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	public FileInfo[] GetFiles(string searchPattern, SearchOption searchOption)
	{
		switch (searchOption)
		{
		case SearchOption.TopDirectoryOnly:
			return GetFiles(searchPattern);
		case SearchOption.AllDirectories:
		{
			ArrayList arrayList = new ArrayList();
			int filesSubdirs = GetFilesSubdirs(arrayList, searchPattern);
			int num = 0;
			FileInfo[] array = new FileInfo[filesSubdirs];
			{
				foreach (FileInfo[] item in arrayList)
				{
					item.CopyTo(array, num);
					num += item.Length;
				}
				return array;
			}
		}
		default:
		{
			string text = Locale.GetText("Invalid enum value '{0}' for '{1}'.", searchOption, "SearchOption");
			throw new ArgumentOutOfRangeException("searchOption", text);
		}
		}
	}

	/// <summary>Creates a directory using a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object.</summary>
	/// <param name="directorySecurity">The access control to apply to the directory.</param>
	/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is read-only or is not empty.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
	/// <exception cref="T:System.NotSupportedException">Creating a directory with only the colon (:) character was attempted.</exception>
	[MonoLimitation("DirectorySecurity isn't implemented")]
	public void Create(DirectorySecurity directorySecurity)
	{
		if (directorySecurity != null)
		{
			throw new UnauthorizedAccessException();
		}
		Create();
	}

	/// <summary>Creates a subdirectory or subdirectories on the specified path with the specified security. The specified path can be relative to this instance of the <see cref="T:System.IO.DirectoryInfo" /> class.</summary>
	/// <param name="path">The specified path. This cannot be a different disk volume or Universal Naming Convention (UNC) name.</param>
	/// <param name="directorySecurity">The security to apply.</param>
	/// <returns>The last directory specified in <paramref name="path" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> does not specify a valid file path or contains invalid <see langword="DirectoryInfo" /> characters.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
	/// <exception cref="T:System.IO.IOException">The subdirectory cannot be created.  
	///  -or-  
	///  A file or directory already has the name specified by <paramref name="path" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have code access permission to create the directory.  
	///  -or-  
	///  The caller does not have code access permission to read the directory described by the returned <see cref="T:System.IO.DirectoryInfo" /> object.  This can occur when the <paramref name="path" /> parameter describes an existing directory.</exception>
	/// <exception cref="T:System.NotSupportedException">
	///   <paramref name="path" /> contains a colon character (:) that is not part of a drive label ("C:\").</exception>
	[MonoLimitation("DirectorySecurity isn't implemented")]
	public DirectoryInfo CreateSubdirectory(string path, DirectorySecurity directorySecurity)
	{
		if (directorySecurity != null)
		{
			throw new UnauthorizedAccessException();
		}
		return CreateSubdirectory(path);
	}

	/// <summary>Gets a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the access control list (ACL) entries for the directory described by the current <see cref="T:System.IO.DirectoryInfo" /> object.</summary>
	/// <returns>A <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the access control rules for the directory.</returns>
	/// <exception cref="T:System.SystemException">The directory could not be found or modified.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The directory is read-only.  
	///  -or-  
	///  This operation is not supported on the current platform.  
	///  -or-  
	///  The caller does not have the required permission.</exception>
	/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the directory.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows 2000 or later.</exception>
	public DirectorySecurity GetAccessControl()
	{
		return Directory.GetAccessControl(FullPath);
	}

	/// <summary>Gets a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the specified type of access control list (ACL) entries for the directory described by the current <see cref="T:System.IO.DirectoryInfo" /> object.</summary>
	/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> values that specifies the type of access control list (ACL) information to receive.</param>
	/// <returns>A <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the access control rules for the file described by the <paramref name="path" /> parameter.  
	///  Exceptions  
	///   Exception type  
	///
	///   Condition  
	///
	///  <see cref="T:System.SystemException" /> The directory could not be found or modified.  
	///
	///  <see cref="T:System.UnauthorizedAccessException" /> The current process does not have access to open the directory.  
	///
	///  <see cref="T:System.IO.IOException" /> An I/O error occurred while opening the directory.  
	///
	///  <see cref="T:System.PlatformNotSupportedException" /> The current operating system is not Microsoft Windows 2000 or later.  
	///
	///  <see cref="T:System.UnauthorizedAccessException" /> The directory is read-only.  
	///
	///  -or-  
	///
	///  This operation is not supported on the current platform.  
	///
	///  -or-  
	///
	///  The caller does not have the required permission.</returns>
	public DirectorySecurity GetAccessControl(AccessControlSections includeSections)
	{
		return Directory.GetAccessControl(FullPath, includeSections);
	}

	/// <summary>Applies access control list (ACL) entries described by a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object to the directory described by the current <see cref="T:System.IO.DirectoryInfo" /> object.</summary>
	/// <param name="directorySecurity">An object that describes an ACL entry to apply to the directory described by the <paramref name="path" /> parameter.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="directorySecurity" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.SystemException">The file could not be found or modified.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The current process does not have access to open the file.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows 2000 or later.</exception>
	public void SetAccessControl(DirectorySecurity directorySecurity)
	{
		Directory.SetAccessControl(FullPath, directorySecurity);
	}

	/// <summary>Returns an enumerable collection of directory information in the current directory.</summary>
	/// <returns>An enumerable collection of directories in the current directory.</returns>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	public IEnumerable<DirectoryInfo> EnumerateDirectories()
	{
		return EnumerateDirectories("*", SearchOption.TopDirectoryOnly);
	}

	/// <summary>Returns an enumerable collection of directory information that matches a specified search pattern.</summary>
	/// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <returns>An enumerable collection of directories that matches <paramref name="searchPattern" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern)
	{
		return EnumerateDirectories(searchPattern, SearchOption.TopDirectoryOnly);
	}

	/// <summary>Returns an enumerable collection of directory information that matches a specified search pattern and search subdirectory option.</summary>
	/// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories. The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
	/// <returns>An enumerable collection of directories that matches <paramref name="searchPattern" /> and <paramref name="searchOption" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern, SearchOption searchOption)
	{
		if (searchPattern == null)
		{
			throw new ArgumentNullException("searchPattern");
		}
		return CreateEnumerateDirectoriesIterator(searchPattern, searchOption);
	}

	private IEnumerable<DirectoryInfo> CreateEnumerateDirectoriesIterator(string searchPattern, SearchOption searchOption)
	{
		foreach (string item in Directory.EnumerateDirectories(FullPath, searchPattern, searchOption))
		{
			yield return new DirectoryInfo(item);
		}
	}

	/// <summary>Returns an enumerable collection of file information in the current directory.</summary>
	/// <returns>An enumerable collection of the files in the current directory.</returns>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	public IEnumerable<FileInfo> EnumerateFiles()
	{
		return EnumerateFiles("*", SearchOption.TopDirectoryOnly);
	}

	/// <summary>Returns an enumerable collection of file information that matches a search pattern.</summary>
	/// <param name="searchPattern">The search string to match against the names of files.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <returns>An enumerable collection of files that matches <paramref name="searchPattern" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid, (for example, it is on an unmapped drive).</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	public IEnumerable<FileInfo> EnumerateFiles(string searchPattern)
	{
		return EnumerateFiles(searchPattern, SearchOption.TopDirectoryOnly);
	}

	/// <summary>Returns an enumerable collection of file information that matches a specified search pattern and search subdirectory option.</summary>
	/// <param name="searchPattern">The search string to match against the names of files.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories. The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
	/// <returns>An enumerable collection of files that matches <paramref name="searchPattern" /> and <paramref name="searchOption" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	public IEnumerable<FileInfo> EnumerateFiles(string searchPattern, SearchOption searchOption)
	{
		if (searchPattern == null)
		{
			throw new ArgumentNullException("searchPattern");
		}
		return CreateEnumerateFilesIterator(searchPattern, searchOption);
	}

	private IEnumerable<FileInfo> CreateEnumerateFilesIterator(string searchPattern, SearchOption searchOption)
	{
		foreach (string item in Directory.EnumerateFiles(FullPath, searchPattern, searchOption))
		{
			yield return new FileInfo(item);
		}
	}

	/// <summary>Returns an enumerable collection of file system information in the current directory.</summary>
	/// <returns>An enumerable collection of file system information in the current directory.</returns>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos()
	{
		return EnumerateFileSystemInfos("*", SearchOption.TopDirectoryOnly);
	}

	/// <summary>Returns an enumerable collection of file system information that matches a specified search pattern.</summary>
	/// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <returns>An enumerable collection of file system information objects that matches <paramref name="searchPattern" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern)
	{
		return EnumerateFileSystemInfos(searchPattern, SearchOption.TopDirectoryOnly);
	}

	/// <summary>Returns an enumerable collection of file system information that matches a specified search pattern and search subdirectory option.</summary>
	/// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories. The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
	/// <returns>An enumerable collection of file system information objects that matches <paramref name="searchPattern" /> and <paramref name="searchOption" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern, SearchOption searchOption)
	{
		if (searchPattern == null)
		{
			throw new ArgumentNullException("searchPattern");
		}
		if (searchOption != 0 && searchOption != SearchOption.AllDirectories)
		{
			throw new ArgumentOutOfRangeException("searchoption");
		}
		return EnumerateFileSystemInfos(FullPath, searchPattern, searchOption);
	}

	internal static IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string basePath, string searchPattern, SearchOption searchOption)
	{
		Path.Validate(basePath);
		SafeFindHandle findHandle = null;
		try
		{
			string text = Path.Combine(basePath, searchPattern);
			string fileName;
			int fileAttr;
			int error;
			try
			{
			}
			finally
			{
				findHandle = new SafeFindHandle(MonoIO.FindFirstFile(text, out fileName, out fileAttr, out error));
			}
			if (findHandle.IsInvalid)
			{
				MonoIOError monoIOError = (MonoIOError)error;
				if (monoIOError != MonoIOError.ERROR_FILE_NOT_FOUND)
				{
					throw MonoIO.GetException(Path.GetDirectoryName(text), monoIOError);
				}
				yield break;
			}
			while (fileName != null)
			{
				if (!(fileName == ".") && !(fileName == ".."))
				{
					FileAttributes attrs = (FileAttributes)fileAttr;
					string fullPath = Path.Combine(basePath, fileName);
					if ((attrs & FileAttributes.ReparsePoint) == 0)
					{
						if ((attrs & FileAttributes.Directory) != 0)
						{
							yield return new DirectoryInfo(fullPath);
						}
						else
						{
							yield return new FileInfo(fullPath);
						}
					}
					if ((attrs & FileAttributes.Directory) != 0 && searchOption == SearchOption.AllDirectories)
					{
						foreach (FileSystemInfo item in EnumerateFileSystemInfos(fullPath, searchPattern, searchOption))
						{
							yield return item;
						}
					}
				}
				if (!MonoIO.FindNextFile(findHandle.DangerousGetHandle(), out fileName, out fileAttr, out var _))
				{
					break;
				}
			}
		}
		finally
		{
			findHandle?.Dispose();
		}
	}

	internal void CheckPath(string path)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (path.Length == 0)
		{
			throw new ArgumentException("An empty file name is not valid.");
		}
		if (path.IndexOfAny(Path.InvalidPathChars) != -1)
		{
			throw new ArgumentException("Illegal characters in path.");
		}
		if (Environment.IsRunningOnWindows)
		{
			int num = path.IndexOf(':');
			if (num >= 0 && num != 1)
			{
				throw new ArgumentException("path");
			}
		}
	}
}
