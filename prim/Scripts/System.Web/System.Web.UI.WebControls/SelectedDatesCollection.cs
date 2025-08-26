using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Encapsulates a collection of <see cref="T:System.DateTime" /> objects that represent the selected dates in a <see cref="T:System.Web.UI.WebControls.Calendar" /> control. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class SelectedDatesCollection : ICollection, IEnumerable
{
	private ArrayList l;

	/// <summary>Gets the number of <see cref="T:System.DateTime" /> objects in the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection.</summary>
	/// <returns>The number of <see cref="T:System.DateTime" /> objects in the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />.</returns>
	public int Count => l.Count;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection is read-only.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public bool IsReadOnly => l.IsReadOnly;

	/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection is synchronized (thread safe).</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public bool IsSynchronized => l.IsSynchronized;

	/// <summary>Gets a <see cref="T:System.DateTime" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection.</summary>
	/// <param name="index">An ordinal index value that specifies which <see cref="T:System.DateTime" /> to return. </param>
	/// <returns>A <see cref="T:System.DateTime" /> that represents an element in the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />.</returns>
	public DateTime this[int index] => (DateTime)l[index];

	/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection.</summary>
	/// <returns>An object that can be used to synchronize access to the collection.</returns>
	public object SyncRoot => this;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> class with the specified date list.</summary>
	/// <param name="dateList">An <see cref="T:System.Collections.ArrayList" /> that represents a collection of dates. </param>
	public SelectedDatesCollection(ArrayList dateList)
	{
		l = dateList;
	}

	/// <summary>Appends the specified <see cref="T:System.DateTime" /> object to the end of the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection.</summary>
	/// <param name="date">The <see cref="T:System.DateTime" /> to add to the collection. </param>
	public void Add(DateTime date)
	{
		date = date.Date;
		if (!l.Contains(date))
		{
			l.Add(date);
		}
	}

	/// <summary>Removes all <see cref="T:System.DateTime" /> objects from the collection.</summary>
	public void Clear()
	{
		l.Clear();
	}

	/// <summary>Returns a value indicating whether the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection contains the specified <see cref="T:System.DateTime" /> object.</summary>
	/// <param name="date">The <see cref="T:System.DateTime" /> to search for in the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />. </param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> contains the specified <see cref="T:System.DateTime" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(DateTime date)
	{
		return l.Contains(date.Date);
	}

	/// <summary>Copies the items from the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection to the specified <see cref="T:System.Array" />, starting with the specified index.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />. </param>
	/// <param name="index">The first index in the specified <see cref="T:System.Array" /> to receive the items. </param>
	public void CopyTo(Array array, int index)
	{
		l.CopyTo(array, index);
	}

	/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.DateTime" /> objects within the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.DateTime" /> objects within the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />.</returns>
	public IEnumerator GetEnumerator()
	{
		return l.GetEnumerator();
	}

	/// <summary>Removes the specified <see cref="T:System.DateTime" /> object from the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection.</summary>
	/// <param name="date">The <see cref="T:System.DateTime" /> to remove from the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />. </param>
	public void Remove(DateTime date)
	{
		l.Remove(date.Date);
	}

	/// <summary>Adds the specified range of dates to the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection.</summary>
	/// <param name="fromDate">A <see cref="T:System.DateTime" /> that specifies the initial date to add to the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />. </param>
	/// <param name="toDate">A <see cref="T:System.DateTime" /> that specifies the end date to add to the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />. </param>
	public void SelectRange(DateTime fromDate, DateTime toDate)
	{
		fromDate = fromDate.Date;
		toDate = toDate.Date;
		l.Clear();
		DateTime dateTime = fromDate;
		while (dateTime <= toDate)
		{
			Add(dateTime);
			dateTime = dateTime.AddDays(1.0);
		}
	}
}
