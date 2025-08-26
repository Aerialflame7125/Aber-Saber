using System.Collections;

namespace System.Web.UI.WebControls;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> objects in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
public class DetailsViewRowCollection : ICollection, IEnumerable
{
	private ArrayList rows = new ArrayList();

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> object from the collection at the specified index.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> to retrieve from the collection.</param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> at the specified index in the collection.</returns>
	public DetailsViewRow this[int index] => (DetailsViewRow)rows[index];

	/// <summary>Gets the number of items in the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> object.</summary>
	/// <returns>The number of items in the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" />.</returns>
	public int Count => rows.Count;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> object is synchronized (thread safe).</summary>
	/// <returns>Always returns <see langword="false" />.</returns>
	public bool IsSynchronized => false;

	/// <summary>Gets the object used to synchronize access to the collection.</summary>
	/// <returns>A <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
	public object SyncRoot => this;

	/// <summary>Gets a value indicating whether the rows in the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> object can be modified.</summary>
	/// <returns>Always returns <see langword="false" />.</returns>
	public bool IsReadOnly => false;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> class using the specified <see cref="T:System.Collections.ArrayList" /> object.</summary>
	/// <param name="rows">An <see cref="T:System.Collections.ArrayList" /> that contains the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> objects with which to initialize the collection.</param>
	public DetailsViewRowCollection(ArrayList rows)
	{
		this.rows = rows;
	}

	/// <summary>Copies all the items from this <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> object to the specified <see cref="T:System.Array" /> object, starting at the specified index in the <see cref="T:System.Array" />.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" />.</param>
	/// <param name="index">The first index in the specified <see cref="T:System.Array" /> to receive the copied contents.</param>
	public void CopyTo(DetailsViewRow[] array, int index)
	{
		rows.CopyTo(array, index);
	}

	/// <summary>Returns an enumerator that contains all <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> objects in the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> object.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> objects in the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" />.</returns>
	public IEnumerator GetEnumerator()
	{
		return rows.GetEnumerator();
	}

	/// <summary>Copies all the items from this <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> object to the specified <see cref="T:System.Array" /> object, starting at the specified index in the <see cref="T:System.Array" />.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" />.</param>
	/// <param name="index">The first index in the specified <see cref="T:System.Array" /> to receive the copied contents.</param>
	void ICollection.CopyTo(Array array, int index)
	{
		rows.CopyTo(array, index);
	}
}
