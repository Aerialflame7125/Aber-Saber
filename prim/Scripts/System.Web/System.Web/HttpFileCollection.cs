using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Web;

/// <summary>Provides access to and organizes files uploaded by a client.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpFileCollection : NameObjectCollectionBase
{
	/// <summary>Gets the object with the specified name from the file collection.</summary>
	/// <param name="name">Name of item to be returned. </param>
	/// <returns>The <see cref="T:System.Web.HttpPostedFile" /> specified by <paramref name="name" />.</returns>
	public HttpPostedFile this[string name] => Get(name);

	/// <summary>Gets the object with the specified numerical index from the <see cref="T:System.Web.HttpFileCollection" />.</summary>
	/// <param name="index">The index of the item to get from the file collection. </param>
	/// <returns>The <see cref="T:System.Web.HttpPostedFile" /> specified by <paramref name="index" />.</returns>
	public HttpPostedFile this[int index] => Get(index);

	/// <summary>Gets a string array containing the keys (names) of all members in the file collection.</summary>
	/// <returns>An array of file names.</returns>
	public string[] AllKeys => BaseGetAllKeys();

	internal HttpFileCollection()
	{
	}

	internal void AddFile(string name, HttpPostedFile file)
	{
		BaseAdd(name, file);
	}

	/// <summary>Copies members of the file collection to an <see cref="T:System.Array" /> beginning at the specified index of the array.</summary>
	/// <param name="dest">The destination <see cref="T:System.Array" />. </param>
	/// <param name="index">The index of the destination array where copying starts. </param>
	public void CopyTo(Array dest, int index)
	{
		BaseGetAllValues().CopyTo(dest, index);
	}

	/// <summary>Returns the name of the <see cref="T:System.Web.HttpFileCollection" /> member with the specified numerical index.</summary>
	/// <param name="index">The index of the object name to be returned. </param>
	/// <returns>The name of the <see cref="T:System.Web.HttpFileCollection" /> member specified by <paramref name="index" />.</returns>
	public string GetKey(int index)
	{
		return BaseGetKey(index);
	}

	/// <summary>Returns the <see cref="T:System.Web.HttpPostedFile" /> object with the specified numerical index from the file collection.</summary>
	/// <param name="index">The index of the object to be returned from the file collection. </param>
	/// <returns>An <see cref="T:System.Web.HttpPostedFile" /> object.</returns>
	public HttpPostedFile Get(int index)
	{
		return (HttpPostedFile)BaseGet(index);
	}

	/// <summary>Returns the <see cref="T:System.Web.HttpPostedFile" /> object with the specified name from the file collection.</summary>
	/// <param name="name">The name of the object to be returned from a file collection. </param>
	/// <returns>An <see cref="T:System.Web.HttpPostedFile" /> object. </returns>
	public HttpPostedFile Get(string name)
	{
		return (HttpPostedFile)BaseGet(name);
	}
}
