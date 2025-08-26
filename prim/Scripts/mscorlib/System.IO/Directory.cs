using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;

namespace System.IO;

/// <summary>Exposes static methods for creating, moving, and enumerating through directories and subdirectories. This class cannot be inherited.</summary>
[ComVisible(true)]
public static class Directory
{
	internal sealed class SearchData
	{
		public readonly string fullPath;

		public readonly string userPath;

		public readonly SearchOption searchOption;

		public SearchData(string fullPath, string userPath, SearchOption searchOption)
		{
			this.fullPath = fullPath;
			this.userPath = userPath;
			this.searchOption = searchOption;
		}
	}

	/// <summary>Returns the names of files (including their paths) in the specified directory.</summary>
	/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
	/// <returns>An array of the full names (including paths) for the files in the specified directory, or an empty array if no files are found.</returns>
	/// <exception cref="T:System.IO.IOException">
	///   <paramref name="path" /> is a file name.  
	/// -or-  
	/// A network error has occurred.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is not found or is invalid (for example, it is on an unmapped drive).</exception>
	public static string[] GetFiles(string path)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		return InternalGetFiles(path, "*", SearchOption.TopDirectoryOnly);
	}

	/// <summary>Returns the names of files (including their paths) that match the specified search pattern in the specified directory.</summary>
	/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
	/// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search pattern, or an empty array if no files are found.</returns>
	/// <exception cref="T:System.IO.IOException">
	///   <paramref name="path" /> is a file name.  
	/// -or-  
	/// A network error has occurred.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using <see cref="M:System.IO.Path.GetInvalidPathChars" />.  
	/// -or-  
	/// <paramref name="searchPattern" /> doesn't contain a valid pattern.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> or <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is not found or is invalid (for example, it is on an unmapped drive).</exception>
	public static string[] GetFiles(string path, string searchPattern)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (searchPattern == null)
		{
			throw new ArgumentNullException("searchPattern");
		}
		return InternalGetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
	}

	/// <summary>Returns the names of files (including their paths) that match the specified search pattern in the specified directory, using a value to determine whether to search subdirectories.</summary>
	/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
	/// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include all subdirectories or only the current directory.</param>
	/// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search pattern and option, or an empty array if no files are found.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
	/// -or-  
	/// <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> or <paramref name="searchpattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is not found or is invalid (for example, it is on an unmapped drive).</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.IO.IOException">
	///   <paramref name="path" /> is a file name.  
	/// -or-  
	/// A network error has occurred.</exception>
	public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (searchPattern == null)
		{
			throw new ArgumentNullException("searchPattern");
		}
		if (searchOption != 0 && searchOption != SearchOption.AllDirectories)
		{
			throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("Enum value was out of legal range."));
		}
		return InternalGetFiles(path, searchPattern, searchOption);
	}

	private static string[] InternalGetFiles(string path, string searchPattern, SearchOption searchOption)
	{
		return InternalGetFileDirectoryNames(path, path, searchPattern, includeFiles: true, includeDirs: false, searchOption, checkHost: true);
	}

	[SecurityCritical]
	internal static string[] UnsafeGetFiles(string path, string searchPattern, SearchOption searchOption)
	{
		return InternalGetFileDirectoryNames(path, path, searchPattern, includeFiles: true, includeDirs: false, searchOption, checkHost: false);
	}

	/// <summary>Returns the names of subdirectories (including their paths) in the specified directory.</summary>
	/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
	/// <returns>An array of the full names (including paths) of subdirectories in the specified path, or an empty array if no directories are found.</returns>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.IO.IOException">
	///   <paramref name="path" /> is a file name.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
	public static string[] GetDirectories(string path)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		return InternalGetDirectories(path, "*", SearchOption.TopDirectoryOnly);
	}

	/// <summary>Returns the names of subdirectories (including their paths) that match the specified search pattern in the specified directory.</summary>
	/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
	/// <param name="searchPattern">The search string to match against the names of subdirectories in <paramref name="path" />. This parameter can contain a combination of valid literal and wildcard characters, but it doesn't support regular expressions.</param>
	/// <returns>An array of the full names (including paths) of the subdirectories that match the search pattern in the specified directory, or an empty array if no directories are found.</returns>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using <see cref="M:System.IO.Path.GetInvalidPathChars" />.  
	/// -or-  
	/// <paramref name="searchPattern" /> doesn't contain a valid pattern.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> or <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.IO.IOException">
	///   <paramref name="path" /> is a file name.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
	public static string[] GetDirectories(string path, string searchPattern)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (searchPattern == null)
		{
			throw new ArgumentNullException("searchPattern");
		}
		return InternalGetDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);
	}

	/// <summary>Returns the names of the subdirectories (including their paths) that match the specified search pattern in the specified directory, and optionally searches subdirectories.</summary>
	/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
	/// <param name="searchPattern">The search string to match against the names of subdirectories in <paramref name="path" />. This parameter can contain a combination of valid literal and wildcard characters, but it doesn't support regular expressions.</param>
	/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include all subdirectories or only the current directory.</param>
	/// <returns>An array of the full names (including paths) of the subdirectories that match the specified criteria, or an empty array if no directories are found.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
	/// -or-  
	/// <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> or <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.IO.IOException">
	///   <paramref name="path" /> is a file name.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
	public static string[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (searchPattern == null)
		{
			throw new ArgumentNullException("searchPattern");
		}
		if (searchOption != 0 && searchOption != SearchOption.AllDirectories)
		{
			throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("Enum value was out of legal range."));
		}
		return InternalGetDirectories(path, searchPattern, searchOption);
	}

	private static string[] InternalGetDirectories(string path, string searchPattern, SearchOption searchOption)
	{
		return InternalGetFileDirectoryNames(path, path, searchPattern, includeFiles: false, includeDirs: true, searchOption, checkHost: true);
	}

	[SecurityCritical]
	internal static string[] UnsafeGetDirectories(string path, string searchPattern, SearchOption searchOption)
	{
		return InternalGetFileDirectoryNames(path, path, searchPattern, includeFiles: false, includeDirs: true, searchOption, checkHost: false);
	}

	/// <summary>Returns the names of all files and subdirectories in a specified path.</summary>
	/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
	/// <returns>An array of the names of files and subdirectories in the specified directory, or an empty array if no files or subdirectories are found.</returns>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.IO.IOException">
	///   <paramref name="path" /> is a file name.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
	public static string[] GetFileSystemEntries(string path)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		return InternalGetFileSystemEntries(path, "*", SearchOption.TopDirectoryOnly);
	}

	/// <summary>Returns an array of file names and directory names that match a search pattern in a specified path.</summary>
	/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
	/// <param name="searchPattern">The search string to match against the names of file and directories in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <returns>An array of file names and directory names that match the specified search criteria, or an empty array if no files or directories are found.</returns>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
	/// -or-  
	/// <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> or <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.IO.IOException">
	///   <paramref name="path" /> is a file name.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
	public static string[] GetFileSystemEntries(string path, string searchPattern)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (searchPattern == null)
		{
			throw new ArgumentNullException("searchPattern");
		}
		return InternalGetFileSystemEntries(path, searchPattern, SearchOption.TopDirectoryOnly);
	}

	/// <summary>Returns an array of all the file names and directory names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
	/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
	/// <param name="searchPattern">The search string to match against the names of files and directories in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.  
	///  The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
	/// <returns>An array of file the file names and directory names that match the specified search criteria, or an empty array if no files or directories are found.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
	/// -or-
	///  <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">
	///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
	/// <exception cref="T:System.IO.IOException">
	///   <paramref name="path" /> is a file name.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	public static string[] GetFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (searchPattern == null)
		{
			throw new ArgumentNullException("searchPattern");
		}
		if (searchOption != 0 && searchOption != SearchOption.AllDirectories)
		{
			throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("Enum value was out of legal range."));
		}
		return InternalGetFileSystemEntries(path, searchPattern, searchOption);
	}

	private static string[] InternalGetFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
	{
		return InternalGetFileDirectoryNames(path, path, searchPattern, includeFiles: true, includeDirs: true, searchOption, checkHost: true);
	}

	internal static string[] InternalGetFileDirectoryNames(string path, string userPathOriginal, string searchPattern, bool includeFiles, bool includeDirs, SearchOption searchOption, bool checkHost)
	{
		return new List<string>(FileSystemEnumerableFactory.CreateFileNameIterator(path, userPathOriginal, searchPattern, includeFiles, includeDirs, searchOption, checkHost)).ToArray();
	}

	/// <summary>Returns an enumerable collection of directory names in a specified path.</summary>
	/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
	/// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by <paramref name="path" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">
	///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
	/// <exception cref="T:System.IO.IOException">
	///   <paramref name="path" /> is a file name.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	public static IEnumerable<string> EnumerateDirectories(string path)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		return InternalEnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly);
	}

	/// <summary>Returns an enumerable collection of directory names that match a search pattern in a specified path.</summary>
	/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
	/// <param name="searchPattern">The search string to match against the names of directories in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by <paramref name="path" /> and that match the specified search pattern.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
	/// -or-
	///  <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">
	///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
	/// <exception cref="T:System.IO.IOException">
	///   <paramref name="path" /> is a file name.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (searchPattern == null)
		{
			throw new ArgumentNullException("searchPattern");
		}
		return InternalEnumerateDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);
	}

	/// <summary>Returns an enumerable collection of directory names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
	/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
	/// <param name="searchPattern">The search string to match against the names of directories in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.  
	///  The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
	/// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by <paramref name="path" /> and that match the specified search pattern and option.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
	/// -or-
	///  <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">
	///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
	/// <exception cref="T:System.IO.IOException">
	///   <paramref name="path" /> is a file name.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (searchPattern == null)
		{
			throw new ArgumentNullException("searchPattern");
		}
		if (searchOption != 0 && searchOption != SearchOption.AllDirectories)
		{
			throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("Enum value was out of legal range."));
		}
		return InternalEnumerateDirectories(path, searchPattern, searchOption);
	}

	private static IEnumerable<string> InternalEnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
	{
		return EnumerateFileSystemNames(path, searchPattern, searchOption, includeFiles: false, includeDirs: true);
	}

	/// <summary>Returns an enumerable collection of file names in a specified path.</summary>
	/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
	/// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">
	///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
	/// <exception cref="T:System.IO.IOException">
	///   <paramref name="path" /> is a file name.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	public static IEnumerable<string> EnumerateFiles(string path)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		return InternalEnumerateFiles(path, "*", SearchOption.TopDirectoryOnly);
	}

	/// <summary>Returns an enumerable collection of file names that match a search pattern in a specified path.</summary>
	/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
	/// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path" /> and that match the specified search pattern.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
	/// -or-
	///  <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">
	///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
	/// <exception cref="T:System.IO.IOException">
	///   <paramref name="path" /> is a file name.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	public static IEnumerable<string> EnumerateFiles(string path, string searchPattern)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (searchPattern == null)
		{
			throw new ArgumentNullException("searchPattern");
		}
		return InternalEnumerateFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
	}

	/// <summary>Returns an enumerable collection of file names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
	/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
	/// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.  
	///  The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
	/// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path" /> and that match the specified search pattern and option.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
	/// -or-
	///  <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">
	///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
	/// <exception cref="T:System.IO.IOException">
	///   <paramref name="path" /> is a file name.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (searchPattern == null)
		{
			throw new ArgumentNullException("searchPattern");
		}
		if (searchOption != 0 && searchOption != SearchOption.AllDirectories)
		{
			throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("Enum value was out of legal range."));
		}
		return InternalEnumerateFiles(path, searchPattern, searchOption);
	}

	private static IEnumerable<string> InternalEnumerateFiles(string path, string searchPattern, SearchOption searchOption)
	{
		return EnumerateFileSystemNames(path, searchPattern, searchOption, includeFiles: true, includeDirs: false);
	}

	/// <summary>Returns an enumerable collection of file names and directory names in a specified path.</summary>
	/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
	/// <returns>An enumerable collection of file-system entries in the directory specified by <paramref name="path" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">
	///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
	/// <exception cref="T:System.IO.IOException">
	///   <paramref name="path" /> is a file name.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	public static IEnumerable<string> EnumerateFileSystemEntries(string path)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		return InternalEnumerateFileSystemEntries(path, "*", SearchOption.TopDirectoryOnly);
	}

	/// <summary>Returns an enumerable collection of file names and directory names that  match a search pattern in a specified path.</summary>
	/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
	/// <param name="searchPattern">The search string to match against the names of file-system entries in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <returns>An enumerable collection of file-system entries in the directory specified by <paramref name="path" /> and that match the specified search pattern.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
	/// -or-
	///  <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">
	///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
	/// <exception cref="T:System.IO.IOException">
	///   <paramref name="path" /> is a file name.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (searchPattern == null)
		{
			throw new ArgumentNullException("searchPattern");
		}
		return InternalEnumerateFileSystemEntries(path, searchPattern, SearchOption.TopDirectoryOnly);
	}

	/// <summary>Returns an enumerable collection of file names and directory names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
	/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
	/// <param name="searchPattern">The search string to match against file-system entries in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
	/// <param name="searchOption">One of the enumeration values  that specifies whether the search operation should include only the current directory or should include all subdirectories.  
	///  The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
	/// <returns>An enumerable collection of file-system entries in the directory specified by <paramref name="path" /> and that match the specified search pattern and option.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
	/// -or-
	///  <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">
	///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
	/// <exception cref="T:System.IO.IOException">
	///   <paramref name="path" /> is a file name.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (searchPattern == null)
		{
			throw new ArgumentNullException("searchPattern");
		}
		if (searchOption != 0 && searchOption != SearchOption.AllDirectories)
		{
			throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("Enum value was out of legal range."));
		}
		return InternalEnumerateFileSystemEntries(path, searchPattern, searchOption);
	}

	private static IEnumerable<string> InternalEnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
	{
		return EnumerateFileSystemNames(path, searchPattern, searchOption, includeFiles: true, includeDirs: true);
	}

	private static IEnumerable<string> EnumerateFileSystemNames(string path, string searchPattern, SearchOption searchOption, bool includeFiles, bool includeDirs)
	{
		return FileSystemEnumerableFactory.CreateFileNameIterator(path, path, searchPattern, includeFiles, includeDirs, searchOption, checkHost: true);
	}

	/// <summary>Returns the volume information, root information, or both for the specified path.</summary>
	/// <param name="path">The path of a file or directory.</param>
	/// <returns>A string that contains the volume information, root information, or both for the specified path.</returns>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	public static string GetDirectoryRoot(string path)
	{
		Path.Validate(path);
		return new string(Path.DirectorySeparatorChar, 1);
	}

	/// <summary>Creates all directories and subdirectories in the specified path unless they already exist.</summary>
	/// <param name="path">The directory to create.</param>
	/// <returns>An object that represents the directory at the specified path. This object is returned regardless of whether a directory at the specified path already exists.</returns>
	/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is a file.  
	///  -or-  
	///  The network name is not known.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
	/// -or-  
	/// <paramref name="path" /> is prefixed with, or contains, only a colon character (:).</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
	/// <exception cref="T:System.NotSupportedException">
	///   <paramref name="path" /> contains a colon character (:) that is not part of a drive label ("C:\").</exception>
	public static DirectoryInfo CreateDirectory(string path)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (path.Length == 0)
		{
			throw new ArgumentException("Path is empty");
		}
		if (path.IndexOfAny(Path.InvalidPathChars) != -1)
		{
			throw new ArgumentException("Path contains invalid chars");
		}
		if (path.Trim().Length == 0)
		{
			throw new ArgumentException("Only blank characters in path");
		}
		if (File.Exists(path))
		{
			throw new IOException("Cannot create " + path + " because a file with the same name already exists.");
		}
		if (Environment.IsRunningOnWindows && path == ":")
		{
			throw new ArgumentException("Only ':' In path");
		}
		return CreateDirectoriesInternal(path);
	}

	/// <summary>Creates all the directories in the specified path, unless the already exist, applying the specified Windows security.</summary>
	/// <param name="path">The directory to create.</param>
	/// <param name="directorySecurity">The access control to apply to the directory.</param>
	/// <returns>An object that represents the directory at the specified path. This object is returned regardless of whether a directory at the specified path already exists.</returns>
	/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is a file.  
	///  -or-  
	///  The network name is not known.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
	/// -or-  
	/// <paramref name="path" /> is prefixed with, or contains, only a colon character (:).</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
	/// <exception cref="T:System.NotSupportedException">
	///   <paramref name="path" /> contains a colon character (:) that is not part of a drive label ("C:\").</exception>
	[MonoLimitation("DirectorySecurity not implemented")]
	public static DirectoryInfo CreateDirectory(string path, DirectorySecurity directorySecurity)
	{
		return CreateDirectory(path);
	}

	private static DirectoryInfo CreateDirectoriesInternal(string path)
	{
		if (SecurityManager.SecurityEnabled)
		{
			new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, path).Demand();
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(path, simpleOriginalPath: true);
		if (directoryInfo.Parent != null && !directoryInfo.Parent.Exists)
		{
			directoryInfo.Parent.Create();
		}
		if (!MonoIO.CreateDirectory(directoryInfo.FullName, out var error) && error != MonoIOError.ERROR_ALREADY_EXISTS && error != MonoIOError.ERROR_FILE_EXISTS)
		{
			throw MonoIO.GetException(path, error);
		}
		return directoryInfo;
	}

	/// <summary>Deletes an empty directory from a specified path.</summary>
	/// <param name="path">The name of the empty directory to remove. This directory must be writable and empty.</param>
	/// <exception cref="T:System.IO.IOException">A file with the same name and location specified by <paramref name="path" /> exists.  
	///  -or-  
	///  The directory is the application's current working directory.  
	///  -or-  
	///  The directory specified by <paramref name="path" /> is not empty.  
	///  -or-  
	///  The directory is read-only or contains a read-only file.  
	///  -or-  
	///  The directory is being used by another process.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">
	///   <paramref name="path" /> does not exist or could not be found.  
	/// -or-  
	/// The specified path is invalid (for example, it is on an unmapped drive).</exception>
	public static void Delete(string path)
	{
		Path.Validate(path);
		if (Environment.IsRunningOnWindows && path == ":")
		{
			throw new NotSupportedException("Only ':' In path");
		}
		if ((!MonoIO.ExistsSymlink(path, out var error)) ? MonoIO.RemoveDirectory(path, out error) : MonoIO.DeleteFile(path, out error))
		{
			return;
		}
		if (error == MonoIOError.ERROR_FILE_NOT_FOUND)
		{
			if (File.Exists(path))
			{
				throw new IOException("Directory does not exist, but a file of the same name exists.");
			}
			throw new DirectoryNotFoundException("Directory does not exist.");
		}
		throw MonoIO.GetException(path, error);
	}

	private static void RecursiveDelete(string path)
	{
		string[] directories = GetDirectories(path);
		foreach (string path2 in directories)
		{
			if (MonoIO.ExistsSymlink(path2, out var error))
			{
				MonoIO.DeleteFile(path2, out error);
			}
			else
			{
				RecursiveDelete(path2);
			}
		}
		directories = GetFiles(path);
		for (int i = 0; i < directories.Length; i++)
		{
			File.Delete(directories[i]);
		}
		Delete(path);
	}

	/// <summary>Deletes the specified directory and, if indicated, any subdirectories and files in the directory.</summary>
	/// <param name="path">The name of the directory to remove.</param>
	/// <param name="recursive">
	///   <see langword="true" /> to remove directories, subdirectories, and files in <paramref name="path" />; otherwise, <see langword="false" />.</param>
	/// <exception cref="T:System.IO.IOException">A file with the same name and location specified by <paramref name="path" /> exists.  
	///  -or-  
	///  The directory specified by <paramref name="path" /> is read-only, or <paramref name="recursive" /> is <see langword="false" /> and <paramref name="path" /> is not an empty directory.  
	///  -or-  
	///  The directory is the application's current working directory.  
	///  -or-  
	///  The directory contains a read-only file.  
	///  -or-  
	///  The directory is being used by another process.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">
	///   <paramref name="path" /> does not exist or could not be found.  
	/// -or-  
	/// The specified path is invalid (for example, it is on an unmapped drive).</exception>
	public static void Delete(string path, bool recursive)
	{
		Path.Validate(path);
		if (recursive)
		{
			RecursiveDelete(path);
		}
		else
		{
			Delete(path);
		}
	}

	/// <summary>Determines whether the given path refers to an existing directory on disk.</summary>
	/// <param name="path">The path to test.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="path" /> refers to an existing directory; <see langword="false" /> if the directory does not exist or an error occurs when trying to determine if the specified directory exists.</returns>
	public static bool Exists(string path)
	{
		if (path == null)
		{
			return false;
		}
		if (!SecurityManager.CheckElevatedPermissions())
		{
			return false;
		}
		string fullPath;
		try
		{
			fullPath = Path.GetFullPath(path);
		}
		catch
		{
			return false;
		}
		MonoIOError error;
		return MonoIO.ExistsDirectory(fullPath, out error);
	}

	/// <summary>Returns the date and time the specified file or directory was last accessed.</summary>
	/// <param name="path">The file or directory for which to obtain access date and time information.</param>
	/// <returns>A structure that is set to the date and time the specified file or directory was last accessed. This value is expressed in local time.</returns>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.NotSupportedException">The <paramref name="path" /> parameter is in an invalid format.</exception>
	public static DateTime GetLastAccessTime(string path)
	{
		return File.GetLastAccessTime(path);
	}

	/// <summary>Returns the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last accessed.</summary>
	/// <param name="path">The file or directory for which to obtain access date and time information.</param>
	/// <returns>A structure that is set to the date and time the specified file or directory was last accessed. This value is expressed in UTC time.</returns>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.NotSupportedException">The <paramref name="path" /> parameter is in an invalid format.</exception>
	public static DateTime GetLastAccessTimeUtc(string path)
	{
		return GetLastAccessTime(path).ToUniversalTime();
	}

	/// <summary>Returns the date and time the specified file or directory was last written to.</summary>
	/// <param name="path">The file or directory for which to obtain modification date and time information.</param>
	/// <returns>A structure that is set to the date and time the specified file or directory was last written to. This value is expressed in local time.</returns>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	public static DateTime GetLastWriteTime(string path)
	{
		return File.GetLastWriteTime(path);
	}

	/// <summary>Returns the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last written to.</summary>
	/// <param name="path">The file or directory for which to obtain modification date and time information.</param>
	/// <returns>A structure that is set to the date and time the specified file or directory was last written to. This value is expressed in UTC time.</returns>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	public static DateTime GetLastWriteTimeUtc(string path)
	{
		return GetLastWriteTime(path).ToUniversalTime();
	}

	/// <summary>Gets the creation date and time of a directory.</summary>
	/// <param name="path">The path of the directory.</param>
	/// <returns>A structure that is set to the creation date and time for the specified directory. This value is expressed in local time.</returns>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	public static DateTime GetCreationTime(string path)
	{
		return File.GetCreationTime(path);
	}

	/// <summary>Gets the creation date and time, in Coordinated Universal Time (UTC) format, of a directory.</summary>
	/// <param name="path">The path of the directory.</param>
	/// <returns>A structure that is set to the creation date and time for the specified directory. This value is expressed in UTC time.</returns>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	public static DateTime GetCreationTimeUtc(string path)
	{
		return GetCreationTime(path).ToUniversalTime();
	}

	/// <summary>Gets the current working directory of the application.</summary>
	/// <returns>A string that contains the absolute path of the current working directory, and does not end with a backslash (\).</returns>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.NotSupportedException">The operating system is Windows CE, which does not have current directory functionality.  
	///  This method is available in the .NET Compact Framework, but is not currently supported.</exception>
	public static string GetCurrentDirectory()
	{
		string text = InsecureGetCurrentDirectory();
		if (text != null && text.Length > 0 && SecurityManager.SecurityEnabled)
		{
			new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
		}
		return text;
	}

	internal static string InsecureGetCurrentDirectory()
	{
		MonoIOError error;
		string currentDirectory = MonoIO.GetCurrentDirectory(out error);
		if (error != 0)
		{
			throw MonoIO.GetException(error);
		}
		return currentDirectory;
	}

	/// <summary>Retrieves the names of the logical drives on this computer in the form "&lt;drive letter&gt;:\".</summary>
	/// <returns>The logical drives on this computer.</returns>
	/// <exception cref="T:System.IO.IOException">An I/O error occured (for example, a disk error).</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	public static string[] GetLogicalDrives()
	{
		return Environment.GetLogicalDrives();
	}

	private static bool IsRootDirectory(string path)
	{
		if (Path.DirectorySeparatorChar == '/' && path == "/")
		{
			return true;
		}
		if (Path.DirectorySeparatorChar == '\\' && path.Length == 3 && path.EndsWith(":\\"))
		{
			return true;
		}
		return false;
	}

	/// <summary>Retrieves the parent directory of the specified path, including both absolute and relative paths.</summary>
	/// <param name="path">The path for which to retrieve the parent directory.</param>
	/// <returns>The parent directory, or <see langword="null" /> if <paramref name="path" /> is the root directory, including the root of a UNC server or share name.</returns>
	/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is read-only.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For more information, see the <see cref="T:System.IO.PathTooLongException" /> topic.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path was not found.</exception>
	/// <exception cref="T:System.NotSupportedException">
	///   <paramref name="path" /> is in an invalid format.</exception>
	/// <exception cref="T:System.Security.SecurityException">.NET Framework only: The caller does not have the required permissions.</exception>
	public static DirectoryInfo GetParent(string path)
	{
		Path.Validate(path);
		if (IsRootDirectory(path))
		{
			return null;
		}
		string text = Path.GetDirectoryName(path);
		if (text.Length == 0)
		{
			text = GetCurrentDirectory();
		}
		return new DirectoryInfo(text);
	}

	/// <summary>Moves a file or a directory and its contents to a new location.</summary>
	/// <param name="sourceDirName">The path of the file or directory to move.</param>
	/// <param name="destDirName">The path to the new location for <paramref name="sourceDirName" />. If <paramref name="sourceDirName" /> is a file, then <paramref name="destDirName" /> must also be a file name.</param>
	/// <exception cref="T:System.IO.IOException">An attempt was made to move a directory to a different volume.  
	///  -or-  
	///  <paramref name="destDirName" /> already exists.  
	///  -or-  
	///  The <paramref name="sourceDirName" /> and <paramref name="destDirName" /> parameters refer to the same file or directory.  
	///  -or-  
	///  The directory or a file within it is being used by another process.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="sourceDirName" /> or <paramref name="destDirName" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="sourceDirName" /> or <paramref name="destDirName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The path specified by <paramref name="sourceDirName" /> is invalid (for example, it is on an unmapped drive).</exception>
	public static void Move(string sourceDirName, string destDirName)
	{
		if (sourceDirName == null)
		{
			throw new ArgumentNullException("sourceDirName");
		}
		if (destDirName == null)
		{
			throw new ArgumentNullException("destDirName");
		}
		if (sourceDirName.Trim().Length == 0 || sourceDirName.IndexOfAny(Path.InvalidPathChars) != -1)
		{
			throw new ArgumentException("Invalid source directory name: " + sourceDirName, "sourceDirName");
		}
		if (destDirName.Trim().Length == 0 || destDirName.IndexOfAny(Path.InvalidPathChars) != -1)
		{
			throw new ArgumentException("Invalid target directory name: " + destDirName, "destDirName");
		}
		if (sourceDirName == destDirName)
		{
			throw new IOException("Source and destination path must be different.");
		}
		if (Exists(destDirName))
		{
			throw new IOException(destDirName + " already exists.");
		}
		if (!Exists(sourceDirName) && !File.Exists(sourceDirName))
		{
			throw new DirectoryNotFoundException(sourceDirName + " does not exist");
		}
		if (!MonoIO.MoveFile(sourceDirName, destDirName, out var error))
		{
			throw MonoIO.GetException(error);
		}
	}

	/// <summary>Applies access control list (ACL) entries described by a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object to the specified directory.</summary>
	/// <param name="path">A directory to add or remove access control list (ACL) entries from.</param>
	/// <param name="directorySecurity">A <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that describes an ACL entry to apply to the directory described by the <paramref name="path" /> parameter.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="directorySecurity" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory could not be found.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> was invalid.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The current process does not have access to the directory specified by <paramref name="path" />.  
	///  -or-  
	///  The current process does not have sufficient privilege to set the ACL entry.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows 2000 or later.</exception>
	public static void SetAccessControl(string path, DirectorySecurity directorySecurity)
	{
		if (directorySecurity == null)
		{
			throw new ArgumentNullException("directorySecurity");
		}
		directorySecurity.PersistModifications(path);
	}

	/// <summary>Sets the creation date and time for the specified file or directory.</summary>
	/// <param name="path">The file or directory for which to set the creation date and time information.</param>
	/// <param name="creationTime">The date and time the file or directory was last written to. This value is expressed in local time.</param>
	/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="creationTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
	public static void SetCreationTime(string path, DateTime creationTime)
	{
		File.SetCreationTime(path, creationTime);
	}

	/// <summary>Sets the creation date and time, in Coordinated Universal Time (UTC) format, for the specified file or directory.</summary>
	/// <param name="path">The file or directory for which to set the creation date and time information.</param>
	/// <param name="creationTimeUtc">The date and time the directory or file was created. This value is expressed in local time.</param>
	/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="creationTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
	public static void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
	{
		SetCreationTime(path, creationTimeUtc.ToLocalTime());
	}

	/// <summary>Sets the application's current working directory to the specified directory.</summary>
	/// <param name="path">The path to which the current working directory is set.</param>
	/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to access unmanaged code.</exception>
	/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified directory was not found.</exception>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public static void SetCurrentDirectory(string path)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (path.Trim().Length == 0)
		{
			throw new ArgumentException("path string must not be an empty string or whitespace string");
		}
		if (!Exists(path))
		{
			throw new DirectoryNotFoundException("Directory \"" + path + "\" not found.");
		}
		MonoIO.SetCurrentDirectory(path, out var error);
		if (error != 0)
		{
			throw MonoIO.GetException(path, error);
		}
	}

	/// <summary>Sets the date and time the specified file or directory was last accessed.</summary>
	/// <param name="path">The file or directory for which to set the access date and time information.</param>
	/// <param name="lastAccessTime">An object that contains the value to set for the access date and time of <paramref name="path" />. This value is expressed in local time.</param>
	/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="lastAccessTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
	public static void SetLastAccessTime(string path, DateTime lastAccessTime)
	{
		File.SetLastAccessTime(path, lastAccessTime);
	}

	/// <summary>Sets the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last accessed.</summary>
	/// <param name="path">The file or directory for which to set the access date and time information.</param>
	/// <param name="lastAccessTimeUtc">An object that  contains the value to set for the access date and time of <paramref name="path" />. This value is expressed in UTC time.</param>
	/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="lastAccessTimeUtc" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
	public static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
	{
		SetLastAccessTime(path, lastAccessTimeUtc.ToLocalTime());
	}

	/// <summary>Sets the date and time a directory was last written to.</summary>
	/// <param name="path">The path of the directory.</param>
	/// <param name="lastWriteTime">The date and time the directory was last written to. This value is expressed in local time.</param>
	/// <exception cref="T:System.IO.FileNotFoundException">
	///   <paramref name="path" /> was not found (for example, the directory doesn't exist or it is on an unmapped drive).</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">
	///   <paramref name="path" /> was not found (for example, the directory doesn't exist or it is on an unmapped drive).</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="lastWriteTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
	public static void SetLastWriteTime(string path, DateTime lastWriteTime)
	{
		File.SetLastWriteTime(path, lastWriteTime);
	}

	/// <summary>Sets the date and time, in Coordinated Universal Time (UTC) format, that a directory was last written to.</summary>
	/// <param name="path">The path of the directory.</param>
	/// <param name="lastWriteTimeUtc">The date and time the directory was last written to. This value is expressed in UTC time.</param>
	/// <exception cref="T:System.IO.FileNotFoundException">
	///   <paramref name="path" /> was not found (for example, the directory doesn't exist or it is on an unmapped drive).</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">
	///   <paramref name="path" /> was not found (for example, the directory doesn't exist or it is on an unmapped drive).</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="lastWriteTimeUtc" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
	public static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
	{
		SetLastWriteTime(path, lastWriteTimeUtc.ToLocalTime());
	}

	/// <summary>Gets a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the specified type of access control list (ACL) entries for a specified directory.</summary>
	/// <param name="path">The path to a directory containing a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that describes the file's access control list (ACL) information.</param>
	/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> values that specifies the type of access control list (ACL) information to receive.</param>
	/// <returns>An object that encapsulates the access control rules for the file described by the <paramref name="path" /> parameter.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the directory.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows 2000 or later.</exception>
	/// <exception cref="T:System.SystemException">A system-level error occurred, such as the directory could not be found. The specific exception may be a subclass of <see cref="T:System.SystemException" />.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="path" /> parameter specified a directory that is read-only.  
	///  -or-  
	///  This operation is not supported on the current platform.  
	///  -or-  
	///  The caller does not have the required permission.</exception>
	public static DirectorySecurity GetAccessControl(string path, AccessControlSections includeSections)
	{
		return new DirectorySecurity(path, includeSections);
	}

	/// <summary>Gets a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the access control list (ACL) entries for a specified directory.</summary>
	/// <param name="path">The path to a directory containing a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that describes the file's access control list (ACL) information.</param>
	/// <returns>An object that encapsulates the access control rules for the file described by the <paramref name="path" /> parameter.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the directory.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows 2000 or later.</exception>
	/// <exception cref="T:System.SystemException">A system-level error occurred, such as the directory could not be found. The specific exception may be a subclass of <see cref="T:System.SystemException" />.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="path" /> parameter specified a directory that is read-only.  
	///  -or-  
	///  This operation is not supported on the current platform.  
	///  -or-  
	///  The caller does not have the required permission.</exception>
	public static DirectorySecurity GetAccessControl(string path)
	{
		return GetAccessControl(path, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
	}

	internal static string GetDemandDir(string fullPath, bool thisDirOnly)
	{
		if (thisDirOnly)
		{
			if (fullPath.EndsWith(Path.DirectorySeparatorChar) || fullPath.EndsWith(Path.AltDirectorySeparatorChar))
			{
				return fullPath + ".";
			}
			return fullPath + Path.DirectorySeparatorCharAsString + ".";
		}
		if (!fullPath.EndsWith(Path.DirectorySeparatorChar) && !fullPath.EndsWith(Path.AltDirectorySeparatorChar))
		{
			return fullPath + Path.DirectorySeparatorCharAsString;
		}
		return fullPath;
	}
}
