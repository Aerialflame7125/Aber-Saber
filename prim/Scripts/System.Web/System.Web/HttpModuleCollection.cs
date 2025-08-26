using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Web;

/// <summary>Provides a way to index and retrieve a collection of <see cref="T:System.Web.IHttpModule" /> objects.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpModuleCollection : NameObjectCollectionBase
{
	/// <summary>Gets the <see cref="T:System.Web.IHttpModule" /> object with the specified name from the <see cref="T:System.Web.HttpModuleCollection" />.</summary>
	/// <param name="name">The key of the item to be retrieved. </param>
	/// <returns>The <see cref="T:System.Web.IHttpModule" /> object module specified by the <paramref name="name" /> parameter.</returns>
	public IHttpModule this[string name] => Get(name);

	/// <summary>Gets the <see cref="T:System.Web.IHttpModule" /> object with the specified numerical index from the <see cref="T:System.Web.HttpModuleCollection" />.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.IHttpModule" /> object to retrieve from the collection. </param>
	/// <returns>The <see cref="T:System.Web.IHttpModule" /> object module specified by the <paramref name="index" /> parameter.</returns>
	public IHttpModule this[int index] => Get(index);

	/// <summary>Gets a string array containing all the keys (module names) in the <see cref="T:System.Web.HttpModuleCollection" />.</summary>
	/// <returns>An array of module names.</returns>
	public string[] AllKeys => BaseGetAllKeys();

	internal HttpModuleCollection()
	{
	}

	internal void AddModule(string key, IHttpModule m)
	{
		BaseAdd(key, m);
	}

	/// <summary>Copies members of the module collection to an <see cref="T:System.Array" />, beginning at the specified index of the array.</summary>
	/// <param name="dest">The destination <see cref="T:System.Array" />. </param>
	/// <param name="index">The index of the destination <see cref="T:System.Array" /> where copying starts. </param>
	public void CopyTo(Array dest, int index)
	{
		BaseGetAllValues().CopyTo(dest, index);
	}

	/// <summary>Returns the key (name) of the <see cref="T:System.Web.IHttpModule" /> object at the specified numerical index.</summary>
	/// <param name="index">Index of the key to retrieve from the collection. </param>
	/// <returns>The name of the <see cref="T:System.Web.IHttpModule" /> member specified by the <paramref name="index" /> parameter.</returns>
	public string GetKey(int index)
	{
		return BaseGetKey(index);
	}

	/// <summary>Returns the <see cref="T:System.Web.IHttpModule" /> object with the specified index from the <see cref="T:System.Web.HttpModuleCollection" />.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.IHttpModule" /> object to return from the collection. </param>
	/// <returns>The <see cref="T:System.Web.IHttpModule" /> member specified by the <paramref name="index" /> parameter.</returns>
	public IHttpModule Get(int index)
	{
		return (IHttpModule)BaseGet(index);
	}

	/// <summary>Returns the <see cref="T:System.Web.IHttpModule" /> object with the specified name from the <see cref="T:System.Web.HttpModuleCollection" />.</summary>
	/// <param name="name">The key of the item to be retrieved. </param>
	/// <returns>The <see cref="T:System.Web.IHttpModule" /> member specified by the <paramref name="name" /> parameter.</returns>
	public IHttpModule Get(string name)
	{
		return (IHttpModule)BaseGet(name);
	}
}
