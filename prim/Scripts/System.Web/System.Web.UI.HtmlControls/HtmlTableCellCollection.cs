using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>A collection of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> objects that represent the cells in a single row of an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HtmlTableCellCollection : ICollection, IEnumerable
{
	private ControlCollection cc;

	/// <summary>Gets the number of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection.</summary>
	/// <returns>The number of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" />. The default value is <see langword="0" />.</returns>
	public int Count => cc.Count;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection is read-only.</summary>
	/// <returns>
	///     <see langword="false" /> for all cases.</returns>
	public bool IsReadOnly => false;

	/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection is synchronized (thread safe).</summary>
	/// <returns>
	///     <see langword="false" /> for all cases, which indicates that access to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> is not synchronized (not thread safe).</returns>
	public bool IsSynchronized => false;

	/// <summary>Gets the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> object at the specified index from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection.</summary>
	/// <param name="index">An ordinal index value that specifies the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> to return. </param>
	/// <returns>An <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> that represents a cell contained in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" />.</returns>
	public HtmlTableCell this[int index] => (HtmlTableCell)cc[index];

	/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection.</summary>
	/// <returns>An object that can be used to synchronize access to the collection.</returns>
	public object SyncRoot => this;

	internal HtmlTableCellCollection(HtmlTableRow tr)
	{
		cc = tr.Controls;
	}

	/// <summary>Appends the specified <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> object to the end of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection.</summary>
	/// <param name="cell">The <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> to add to the collection. </param>
	public void Add(HtmlTableCell cell)
	{
		cc.Add(cell);
	}

	/// <summary>Removes all <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> objects from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection.</summary>
	public void Clear()
	{
		cc.Clear();
	}

	/// <summary>Copies the items from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection to the specified <see cref="T:System.Array" />, beginning with the specified index in the <see cref="T:System.Array" />.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" />. </param>
	/// <param name="index">The first index in the specified <see cref="T:System.Array" /> to receive the items. </param>
	public void CopyTo(Array array, int index)
	{
		cc.CopyTo(array, index);
	}

	/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection.</summary>
	/// <returns>A <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" />.</returns>
	public IEnumerator GetEnumerator()
	{
		return cc.GetEnumerator();
	}

	/// <summary>Adds the specified <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> object at the specified index location of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection.</summary>
	/// <param name="index">The location in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> at which to add the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />. </param>
	/// <param name="cell">The <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> to add to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" />. </param>
	public void Insert(int index, HtmlTableCell cell)
	{
		cc.AddAt(index, cell);
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> object from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection.</summary>
	/// <param name="cell">The <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> to remove from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" />. </param>
	public void Remove(HtmlTableCell cell)
	{
		cc.Remove(cell);
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> object at the specified index from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> to remove from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" />. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is outside the range of index values in the collection. </exception>
	public void RemoveAt(int index)
	{
		cc.RemoveAt(index);
	}
}
