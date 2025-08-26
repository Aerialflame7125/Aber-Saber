using System.Collections;
using System.Collections.Specialized;

namespace System.Web.Configuration;

/// <summary>Contains a collection of <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> objects. This class cannot be inherited.</summary>
[Serializable]
public sealed class VirtualDirectoryMappingCollection : NameObjectCollectionBase
{
	/// <summary>Returns a string array that contains all the keys in the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> instance.</summary>
	/// <returns>A string array that contains all the keys in the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> instance.</returns>
	public ICollection AllKeys => BaseGetAllKeys();

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object at the specified index location.</summary>
	/// <param name="index">An integer value that specifies a particular <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object within the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />.</param>
	/// <returns>A <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</returns>
	public VirtualDirectoryMapping this[int index] => (VirtualDirectoryMapping)BaseGet(index);

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object based on the specified virtual directory name.</summary>
	/// <param name="virtualDirectory">A string that contains the name of the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</param>
	/// <returns>A <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</returns>
	public VirtualDirectoryMapping this[string virtualDirectory] => (VirtualDirectoryMapping)BaseGet(virtualDirectory);

	/// <summary>Adds a <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object to the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> instance.</summary>
	/// <param name="virtualDirectory">A <see cref="T:System.String" /> that contains the virtual directory path.</param>
	/// <param name="mapping">A <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="mapping" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="virtualDirectory" /> already exists in the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> is read-only.</exception>
	public void Add(string virtualDirectory, VirtualDirectoryMapping mapping)
	{
		mapping.SetVirtualDirectory(virtualDirectory);
		BaseAdd(virtualDirectory, mapping);
	}

	/// <summary>Clears all <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> objects from the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> instance.</summary>
	public void Clear()
	{
		BaseClear();
	}

	/// <summary>Copies the entire <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
	/// <param name="array">A one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="array" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Array.Length" /> property of <paramref name="array" /> is less than the value of <see cref="P:System.Collections.Specialized.NameObjectCollectionBase.Count" /> plus <paramref name="index" />.</exception>
	public void CopyTo(VirtualDirectoryMapping[] array, int index)
	{
		((ICollection)this).CopyTo((Array)array, index);
	}

	/// <summary>Gets the specified <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> collection element at the specified index.</summary>
	/// <param name="index">An integer value that specifies a particular <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object within the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> element at the specified index.</returns>
	public VirtualDirectoryMapping Get(int index)
	{
		return (VirtualDirectoryMapping)BaseGet(index);
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> collection element based on the specified virtual-directory name.</summary>
	/// <param name="virtualDirectory">A string that contains the name of the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> element based on the specified virtual-directory name.</returns>
	public VirtualDirectoryMapping Get(string virtualDirectory)
	{
		return (VirtualDirectoryMapping)BaseGet(virtualDirectory);
	}

	/// <summary>Gets the key of the entry at the specified index of the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> instance.</summary>
	/// <param name="index">An integer value that specifies a particular <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object within the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />.</param>
	/// <returns>A string that contains the name of the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</returns>
	public string GetKey(int index)
	{
		return BaseGetKey(index);
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object from the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> instance.</summary>
	/// <param name="virtualDirectory">A string that contains the name of the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</param>
	public void Remove(string virtualDirectory)
	{
		BaseRemove(virtualDirectory);
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object at the specified index from the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />.</summary>
	/// <param name="index">An integer value that specifies a particular <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object within the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />.</param>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> class.</summary>
	public VirtualDirectoryMappingCollection()
	{
	}
}
