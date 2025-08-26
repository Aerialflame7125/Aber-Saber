using System.Security.Permissions;

namespace System.Web.Caching;

/// <summary>Represents part of an output-cache entry, stored as a file.</summary>
[Serializable]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Unrestricted)]
public class FileResponseElement : ResponseElement
{
	/// <summary>Gets the size of the data, starting at the offset that contains the data for a <see cref="T:System.Web.Caching.FileResponseElement" /> instance.</summary>
	/// <returns>The size of the data.</returns>
	public long Length { get; private set; }

	/// <summary>Gets the position in the file where the data from a <see cref="T:System.Web.Caching.FileResponseElement" /> instance starts. </summary>
	/// <returns>The starting point of the data in the file.</returns>
	public long Offset { get; private set; }

	/// <summary>Gets the location of the file that contains data from a <see cref="T:System.Web.Caching.FileResponseElement" /> instance.</summary>
	/// <returns>The fully qualified path of the file.</returns>
	public string Path { get; private set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.FileResponseElement" /> class. </summary>
	/// <param name="path">The fully qualified path for the file.</param>
	/// <param name="offset">The position in the file where the string starts.</param>
	/// <param name="length">The length of the data, starting at the offset that represents the output-cache data in the file defined by <paramref name="path" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="offset" /> is less than zero.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="length" /> is less than zero.</exception>
	public FileResponseElement(string path, long offset, long length)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (offset < 0)
		{
			throw new ArgumentOutOfRangeException("offset", "is less than zero.");
		}
		if (length < 0)
		{
			throw new ArgumentOutOfRangeException("length", "is less than zero.");
		}
		Length = length;
		Offset = offset;
		Path = path;
	}
}
