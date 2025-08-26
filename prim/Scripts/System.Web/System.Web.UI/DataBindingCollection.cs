using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Provides a collection of <see cref="T:System.Web.UI.DataBinding" /> objects for an ASP.NET server control. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class DataBindingCollection : ICollection, IEnumerable
{
	private static readonly object changedEvent = new object();

	private Hashtable list;

	private ArrayList removed;

	private EventHandlerList events = new EventHandlerList();

	/// <summary>Gets the number of <see cref="T:System.Web.UI.DataBinding" /> objects in the <see cref="T:System.Web.UI.DataBindingCollection" /> object.</summary>
	/// <returns>The number of <see cref="T:System.Web.UI.DataBinding" /> objects in the <see cref="T:System.Web.UI.DataBindingCollection" />.</returns>
	public int Count => list.Count;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataBindingCollection" /> collection is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool IsReadOnly => list.IsReadOnly;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataBindingCollection" /> collection is synchronized (thread safe).</summary>
	/// <returns>Always <see langword="false" />.</returns>
	public bool IsSynchronized => list.IsSynchronized;

	/// <summary>Gets the <see cref="T:System.Web.UI.DataBinding" /> object with the specified property name.</summary>
	/// <param name="propertyName">The name of the property to be found. </param>
	/// <returns>The <see cref="T:System.Web.UI.DataBinding" /> with the specified property name. If no object with the specified name exists, this value is <see langword="null" />.</returns>
	public DataBinding this[string propertyName] => list[propertyName] as DataBinding;

	/// <summary>Gets an array of the names of the <see cref="T:System.Web.UI.DataBinding" /> objects removed from the collection.</summary>
	/// <returns>The array of names of the <see cref="T:System.Web.UI.DataBinding" /> objects removed from the collection. </returns>
	public string[] RemovedBindings => (string[])removed.ToArray(typeof(string));

	/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Web.UI.DataBindingCollection" /> collection.</summary>
	/// <returns>The <see cref="T:System.Object" /> to be used to synchronize access to the collection.</returns>
	public object SyncRoot => list.SyncRoot;

	/// <summary>Occurs when the collection of <see cref="T:System.Web.UI.DataBinding" /> objects is changed. </summary>
	public event EventHandler Changed
	{
		add
		{
			events.AddHandler(changedEvent, value);
		}
		remove
		{
			events.RemoveHandler(changedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBindingCollection" /> class.</summary>
	public DataBindingCollection()
	{
		list = new Hashtable();
		removed = new ArrayList();
	}

	/// <summary>Adds the specified <see cref="T:System.Web.UI.DataBinding" /> object to the <see cref="T:System.Web.UI.DataBindingCollection" /> collection.</summary>
	/// <param name="binding">The data-binding object to add to the collection. </param>
	public void Add(DataBinding binding)
	{
		list.Add(binding.PropertyName, binding);
		RaiseChanged();
	}

	/// <summary>Removes all <see cref="T:System.Web.UI.DataBinding" /> objects from the <see cref="T:System.Web.UI.DataBindingCollection" /> collection.</summary>
	public void Clear()
	{
		list.Clear();
	}

	/// <summary>Copies the <see cref="T:System.Web.UI.DataBindingCollection" /> values to a one-dimensional <see cref="T:System.Array" />, beginning at the <see cref="T:System.Array" /> object's specified index.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from <see cref="T:System.Web.UI.DataBindingCollection" />. </param>
	/// <param name="index">The index in the array, specified by the <paramref name="array" /> parameter, where copying begins. </param>
	public void CopyTo(Array array, int index)
	{
		list.Values.CopyTo(array, index);
	}

	/// <summary>Returns an enumerator to iterate through the <see cref="T:System.Web.UI.DataBindingCollection" /> object.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that contains the collection's members.</returns>
	public IEnumerator GetEnumerator()
	{
		return list.GetEnumerator();
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.DataBinding" /> object from the <see cref="T:System.Web.UI.DataBindingCollection" /> collection and adds it to the <see cref="P:System.Web.UI.DataBindingCollection.RemovedBindings" /> collection.</summary>
	/// <param name="binding">The <see cref="T:System.Web.UI.DataBinding" /> to be removed from the <see cref="T:System.Web.UI.DataBindingCollection" />. </param>
	public void Remove(DataBinding binding)
	{
		string propertyName = binding.PropertyName;
		Remove(propertyName);
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.DataBinding" /> object associated with the specified property name from the <see cref="T:System.Web.UI.DataBindingCollection" /> collection and adds it to the <see cref="P:System.Web.UI.DataBindingCollection.RemovedBindings" /> collection.</summary>
	/// <param name="propertyName">The property name associated with the <see cref="T:System.Web.UI.DataBinding" /> to be removed. </param>
	public void Remove(string propertyName)
	{
		removed.Add(propertyName);
		list.Remove(propertyName);
		RaiseChanged();
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.DataBinding" /> object, associated with the specified property name, from the <see cref="T:System.Web.UI.DataBindingCollection" /> collection and controls whether to add the binding to the <see cref="P:System.Web.UI.DataBindingCollection.RemovedBindings" /> list.</summary>
	/// <param name="propertyName">The property associated with the <see cref="T:System.Web.UI.DataBinding" /> to be removed. </param>
	/// <param name="addToRemovedList">A Boolean value that indicates whether to add the property name to the <see cref="P:System.Web.UI.DataBindingCollection.RemovedBindings" /> list. <see langword="true" /> indicates that the <paramref name="propertyName" /> parameter will be added to the <see cref="P:System.Web.UI.DataBindingCollection.RemovedBindings" /> property, and <see langword="false" /> indicates that <paramref name="propertyName" /> will not be added to the <see cref="P:System.Web.UI.DataBindingCollection.RemovedBindings" /> property. </param>
	public void Remove(string propertyName, bool addToRemovedList)
	{
		if (addToRemovedList)
		{
			removed.Add(string.Empty);
		}
		else
		{
			removed.Add(propertyName);
		}
		list.Remove(propertyName);
	}

	/// <summary>Determines whether the data-binding collection contains a specific <see cref="T:System.Web.UI.DataBinding" /> object.</summary>
	/// <param name="propertyName">The name of the object to locate in the collection.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.DataBindingCollection" /> contains an element with the specified name; otherwise, <see langword="false" />.</returns>
	public bool Contains(string propertyName)
	{
		return list.Contains(propertyName);
	}

	internal void RaiseChanged()
	{
		if (events[changedEvent] is EventHandler eventHandler)
		{
			eventHandler(this, EventArgs.Empty);
		}
	}
}
