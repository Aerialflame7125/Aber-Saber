using System.Security.Permissions;

namespace System.Web.Caching;

/// <summary>Represents part of an output-cache entry that is stored in memory.</summary>
[Serializable]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Unrestricted)]
public class MemoryResponseElement : ResponseElement
{
	/// <summary>Gets an array that contains all or part of an output-cache response.</summary>
	/// <returns>An array of byte objects.</returns>
	public byte[] Buffer { get; private set; }

	/// <summary>Gets the size of the array that is referenced by the <see cref="P:System.Web.Caching.MemoryResponseElement.Buffer" /> property.</summary>
	/// <returns>The size of the array.</returns>
	public long Length { get; private set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.MemoryResponseElement" /> class.</summary>
	/// <param name="buffer">An array of bytes that contains a part of an output-cache response. </param>
	/// <param name="length">The size of the array in <paramref name="buffer" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="buffer" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="length" /> is less than zero or greater than the size of <paramref name="buffer" />.</exception>
	public MemoryResponseElement(byte[] buffer, long length)
	{
		if (buffer == null)
		{
			throw new ArgumentNullException("buffer");
		}
		if (length < 0 || length > buffer.Length)
		{
			throw new ArgumentOutOfRangeException("length", "is less than zero or greater than the size of buffer.");
		}
		Buffer = buffer;
		Length = length;
	}
}
