using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a collection that contains the key field of each record in a data source. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class DataKeyCollection : ICollection, IEnumerable
{
	private ArrayList list;

	/// <summary>Gets the number of items in the collection.</summary>
	/// <returns>The number of items in the collection.</returns>
	public int Count => list.Count;

	/// <summary>Gets a value indicating whether items in the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" /> can be modified.</summary>
	/// <returns>
	///     <see langword="false" /> for all cases.</returns>
	public bool IsReadOnly => false;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" /> is synchronized (thread-safe).</summary>
	/// <returns>
	///     <see langword="false" /> for all cases.</returns>
	public bool IsSynchronized => false;

	/// <summary>Gets the key field at the specified index in the collection.</summary>
	/// <param name="index">The zero-based index of the key field to retrieve from the collection. </param>
	/// <returns>The key field at the specified index in the collection.</returns>
	public object this[int index] => list[index];

	/// <summary>Gets the object used to synchronize access to the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" />.</summary>
	/// <returns>A <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
	public object SyncRoot => this;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" /> class.</summary>
	/// <param name="keys">A <see cref="T:System.Collections.ArrayList" /> that contains key fields from the data source. </param>
	public DataKeyCollection(ArrayList keys)
	{
		list = keys;
	}

	/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" /> to the specified <see cref="T:System.Array" /> object, starting at the specified index in the <see cref="T:System.Array" /> object.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> object that receives the copied items from the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" />. </param>
	/// <param name="index">The first position in the specified <see cref="T:System.Array" /> object to receive the copied contents. </param>
	public void CopyTo(Array array, int index)
	{
		list.CopyTo(array, index);
	}

	/// <summary>Creates a <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all key fields in the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" />.</summary>
	/// <returns>A <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all key fields in the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" />.</returns>
	public IEnumerator GetEnumerator()
	{
		return list.GetEnumerator();
	}
}
