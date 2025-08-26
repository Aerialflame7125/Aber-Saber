using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>A collection of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects that represent the rows of an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HtmlTableRowCollection : ICollection, IEnumerable
{
	private ControlCollection cc;

	/// <summary>Gets the number of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
	/// <returns>The number of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />. The default value is <see langword="0" />.</returns>
	public int Count => cc.Count;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection is read-only.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public bool IsReadOnly => false;

	/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection is synchronized (thread safe).</summary>
	/// <returns>
	///     <see langword="false" /> in all cases, which indicates that access to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> is not synchronized (not thread safe).</returns>
	public bool IsSynchronized => false;

	/// <summary>Gets the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> object at the specified index from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
	/// <param name="index">An ordinal index value that specifies the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> to return. </param>
	/// <returns>An <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> that represents a row contained in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />.</returns>
	public HtmlTableRow this[int index] => (HtmlTableRow)cc[index];

	/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
	/// <returns>An object that can be used to synchronize access to the collection.</returns>
	public object SyncRoot => this;

	internal HtmlTableRowCollection(HtmlTable table)
	{
		cc = table.Controls;
	}

	/// <summary>Appends the specified <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> object to the end of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
	/// <param name="row">The <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> to add to the collection. </param>
	public void Add(HtmlTableRow row)
	{
		cc.Add(row);
	}

	/// <summary>Removes all <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
	public void Clear()
	{
		cc.Clear();
	}

	/// <summary>Copies the items from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection to the specified <see cref="T:System.Array" /> object, starting at the specified index in the array.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />. </param>
	/// <param name="index">The first index in the specified array to receive the items. </param>
	public void CopyTo(Array array, int index)
	{
		cc.CopyTo(array, index);
	}

	/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />.</returns>
	public IEnumerator GetEnumerator()
	{
		return cc.GetEnumerator();
	}

	/// <summary>Adds an <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> object to the specified location in the collection.</summary>
	/// <param name="index">The location in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> at which to add the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" />. </param>
	/// <param name="row">The <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> to add to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />. </param>
	public void Insert(int index, HtmlTableRow row)
	{
		cc.AddAt(index, row);
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> object from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
	/// <param name="row">The <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> to remove from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />. </param>
	public void Remove(HtmlTableRow row)
	{
		cc.Remove(row);
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> object at the specified index from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> to remove from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is outside the range of index values in the collection. </exception>
	public void RemoveAt(int index)
	{
		cc.RemoveAt(index);
	}
}
