using System.Collections;

namespace System.Web.UI.WebControls;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.GridViewRow" /> objects in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
public class GridViewRowCollection : ICollection, IEnumerable
{
	private ArrayList rows = new ArrayList();

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object at the specified index.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object to retrieve from the collection.</param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object at the specified index in the collection.</returns>
	public GridViewRow this[int index] => (GridViewRow)rows[index];

	/// <summary>Gets the number of items in the <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" /> object.</summary>
	/// <returns>The number of items in the <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" /> object.</returns>
	public int Count => rows.Count;

	/// <summary>Gets a value indicating whether the rows in the <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" /> object can be modified.</summary>
	/// <returns>Always returns <see langword="false" />.</returns>
	public bool IsReadOnly => false;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" /> object is synchronized (thread-safe). </summary>
	/// <returns>Always returns <see langword="false" />.</returns>
	public bool IsSynchronized => false;

	/// <summary>Gets the object used to synchronize access to the collection.</summary>
	/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
	public object SyncRoot => this;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" /> class using the specified <see cref="T:System.Collections.ArrayList" /> object.</summary>
	/// <param name="rows">An <see cref="T:System.Collections.ArrayList" /> object that contains the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> objects with which to initialize the collection.</param>
	public GridViewRowCollection(ArrayList rows)
	{
		this.rows = rows;
	}

	/// <summary>Copies all the items from this <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" /> to the specified <see cref="T:System.Array" /> object, starting at the specified index in the <see cref="T:System.Array" /> object.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> object that receives the copied items from the <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" /> object.</param>
	/// <param name="index">The first index in the specified <see cref="T:System.Array" /> object to receive the copied contents.</param>
	public void CopyTo(GridViewRow[] array, int index)
	{
		rows.CopyTo(array, index);
	}

	/// <summary>Returns an enumerator that contains all <see cref="T:System.Web.UI.WebControls.GridViewRow" /> objects in the <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" />.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all <see cref="T:System.Web.UI.WebControls.GridViewRow" /> objects in the <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" />.</returns>
	public IEnumerator GetEnumerator()
	{
		return rows.GetEnumerator();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" /> interface. The array must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in a <see cref="T:System.Array" /> object at which copying begins.</param>
	void ICollection.CopyTo(Array array, int index)
	{
		rows.CopyTo(array, index);
	}
}
