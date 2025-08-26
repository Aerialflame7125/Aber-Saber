using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents the collection of <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class DataListItemCollection : ICollection, IEnumerable
{
	private ArrayList list;

	/// <summary>Gets the number of <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects in the collection.</summary>
	/// <returns>The number of <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects in the collection.</returns>
	public int Count => list.Count;

	/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects in the <see cref="T:System.Web.UI.WebControls.DataListItemCollection" /> can be modified.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public bool IsReadOnly => false;

	/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.DataListItemCollection" /> is synchronized (thread-safe).</summary>
	/// <returns>
	///     <see langword="false" /> for all cases.</returns>
	public bool IsSynchronized => false;

	/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.DataListItem" /> object at the specified index in the collection.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.DataListItem" /> in the collection to retrieve. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.DataListItem" /> object in the collection at the specified index.</returns>
	public DataListItem this[int index] => (DataListItem)list[index];

	/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.DataListItemCollection" /> collection.</summary>
	/// <returns>A <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
	public object SyncRoot => this;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataListItemCollection" /> class.</summary>
	/// <param name="items">A <see cref="T:System.Collections.ArrayList" /> object that contains the items with which to initialize the collection. </param>
	public DataListItemCollection(ArrayList items)
	{
		list = items;
	}

	/// <summary>Copies all the items from this <see cref="T:System.Web.UI.WebControls.DataListItemCollection" /> collection to the specified <see cref="T:System.Array" /> object, starting at the specified index in the <see cref="T:System.Array" /> object.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> object that receives the copied items from the <see cref="T:System.Web.UI.WebControls.DataListItemCollection" /> collection. </param>
	/// <param name="index">The first position in the specified <see cref="T:System.Array" /> object to receive the copied contents. </param>
	public void CopyTo(Array array, int index)
	{
		list.CopyTo(array, index);
	}

	/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> interface that contains all <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects in the <see cref="T:System.Web.UI.WebControls.DataListItemCollection" />.</summary>
	/// <returns>A <see cref="T:System.Collections.IEnumerator" /> interface that contains all <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects in the <see cref="T:System.Web.UI.WebControls.DataListItemCollection" />.</returns>
	public IEnumerator GetEnumerator()
	{
		return list.GetEnumerator();
	}
}
