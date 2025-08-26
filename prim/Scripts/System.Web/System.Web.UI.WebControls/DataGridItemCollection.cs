using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.DataGridItem" /> objects in a <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class DataGridItemCollection : ICollection, IEnumerable
{
	private ArrayList array;

	/// <summary>Gets the number of <see cref="T:System.Web.UI.WebControls.DataGridItem" /> objects in the collection.</summary>
	/// <returns>The number of <see cref="T:System.Web.UI.WebControls.DataGridItem" /> objects in the collection.</returns>
	public int Count => array.Count;

	/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> objects in the <see cref="T:System.Web.UI.WebControls.DataGridItemCollection" /> collection can be modified.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public bool IsReadOnly => array.IsReadOnly;

	/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.DataGridItemCollection" /> collection is synchronized (thread-safe).</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public bool IsSynchronized => array.IsSynchronized;

	/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.DataGridItemCollection" /> collection.</summary>
	/// <returns>A <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
	public object SyncRoot => array.SyncRoot;

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object at the specified index in the collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> to retrieve from the collection. </param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.DataGridItem" /> at the specified index in the collection.</returns>
	public DataGridItem this[int index] => (DataGridItem)array[index];

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridItemCollection" /> class.</summary>
	/// <param name="items">A <see cref="T:System.Collections.ArrayList" /> that contains the items with which to initialize the collection. </param>
	public DataGridItemCollection(ArrayList items)
	{
		array = items;
	}

	/// <summary>Copies all the items from this <see cref="T:System.Web.UI.WebControls.DataGridItemCollection" /> collection to the specified <see cref="T:System.Array" />, starting at the specified index in the <see cref="T:System.Array" />.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.DataGridItemCollection" />. </param>
	/// <param name="index">The first position in the specified <see cref="T:System.Array" /> to receive the copied contents. </param>
	public void CopyTo(Array array, int index)
	{
		if (!(array is DataGridItem[]))
		{
			throw new InvalidCastException("Target array must be DataGridItem[]");
		}
		if (index + this.array.Count > array.Length)
		{
			throw new IndexOutOfRangeException("Target array not large enough to hold copied array.");
		}
		this.array.CopyTo(array, index);
	}

	/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> objects in the <see cref="T:System.Web.UI.WebControls.DataGridItemCollection" /> collection.</summary>
	/// <returns>A <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.Web.UI.WebControls.DataGridItem" /> objects in the <see cref="T:System.Web.UI.WebControls.DataGridItemCollection" />.</returns>
	public IEnumerator GetEnumerator()
	{
		return array.GetEnumerator();
	}
}
