using System.Collections;

namespace System.Web.UI.WebControls;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> objects in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control. This class cannot be inherited.</summary>
public sealed class RepeaterItemCollection : ICollection, IEnumerable
{
	private ArrayList l;

	/// <summary>Gets the number of <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> objects in the collection.</summary>
	/// <returns>The number of <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects in the collection.</returns>
	public int Count => l.Count;

	/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> objects in the <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" /> can be modified.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public bool IsReadOnly => false;

	/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" /> is synchronized (thread-safe).</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public bool IsSynchronized => false;

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> object at the specified index in the collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> to retrieve in the collection.</param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> object at the specified index in the collection.</returns>
	public RepeaterItem this[int index] => (RepeaterItem)l[index];

	/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" /> collection.</summary>
	/// <returns>A <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
	public object SyncRoot => l.SyncRoot;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" /> class.</summary>
	/// <param name="items">A <see cref="T:System.Collections.ArrayList" /> that contains the items with which to initialize the collection.</param>
	public RepeaterItemCollection(ArrayList items)
	{
		l = items;
	}

	/// <summary>Copies all the items from this <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" /> to the specified <see cref="T:System.Array" /> object, starting at the specified index in the <see cref="T:System.Array" /> object.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" />. </param>
	/// <param name="index">The first position in the specified <see cref="T:System.Array" /> to receive the copied contents. </param>
	public void CopyTo(Array array, int index)
	{
		l.CopyTo(array, index);
	}

	/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> interface that can iterate through all the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> objects in the <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" />.</summary>
	/// <returns>A <see cref="T:System.Collections.IEnumerator" /> interface that contains all <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> objects in the <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" />.</returns>
	public IEnumerator GetEnumerator()
	{
		return l.GetEnumerator();
	}
}
