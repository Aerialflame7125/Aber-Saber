using System.Collections;
using System.ComponentModel;

namespace System.Web.UI;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.ExpressionBinding" /> objects. This class cannot be inherited.</summary>
public sealed class ExpressionBindingCollection : ICollection, IEnumerable
{
	private static readonly object changedEvent = new object();

	private Hashtable list;

	private ArrayList removed;

	private EventHandlerList events = new EventHandlerList();

	/// <summary>Gets the number of <see cref="T:System.Web.UI.ExpressionBinding" /> objects in the <see cref="T:System.Web.UI.ExpressionBindingCollection" /> collection.</summary>
	/// <returns>The number of <see cref="T:System.Web.UI.ExpressionBinding" /> objects in the <see cref="T:System.Web.UI.ExpressionBindingCollection" />.</returns>
	public int Count => list.Count;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.ExpressionBinding" /> objects in the collection can be modified.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases. </returns>
	public bool IsReadOnly => list.IsReadOnly;

	/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
	/// <returns>
	///     <see langword="false" /> in all cases. </returns>
	public bool IsSynchronized => list.IsSynchronized;

	/// <summary>Gets an <see cref="T:System.Web.UI.ExpressionBinding" /> object from the collection with the specified <see cref="P:System.Web.UI.ExpressionBinding.PropertyName" /> value.</summary>
	/// <param name="propertyName">The <see cref="P:System.Web.UI.ExpressionBinding.PropertyName" /> of the <see cref="T:System.Web.UI.ExpressionBinding" /> to retrieve.</param>
	/// <returns>An <see cref="T:System.Web.UI.ExpressionBinding" /> in the <see cref="T:System.Web.UI.ExpressionBindingCollection" /> with the specified <see cref="P:System.Web.UI.ExpressionBinding.PropertyName" />.</returns>
	public ExpressionBinding this[string propertyName] => list[propertyName] as ExpressionBinding;

	/// <summary>Gets a collection of strings representing the names of bindings that have been removed.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the names of bindings that have been removed.</returns>
	public ICollection RemovedBindings => removed;

	/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
	/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the <see cref="T:System.Web.UI.ExpressionBindingCollection" />.</returns>
	public object SyncRoot => list.SyncRoot;

	/// <summary>Occurs when the collection of <see cref="T:System.Web.UI.ExpressionBinding" /> objects is changed.</summary>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ExpressionBindingCollection" /> class.</summary>
	public ExpressionBindingCollection()
	{
		list = new Hashtable();
		removed = new ArrayList();
	}

	/// <summary>Appends the specified <see cref="T:System.Web.UI.ExpressionBinding" /> object to the end of the collection.</summary>
	/// <param name="binding">The <see cref="T:System.Web.UI.ExpressionBinding" /> to append to the <see cref="T:System.Web.UI.ExpressionBindingCollection" />.</param>
	public void Add(ExpressionBinding binding)
	{
		list.Add(binding.PropertyName, binding);
		OnChanged(new EventArgs());
	}

	/// <summary>Removes all the <see cref="T:System.Web.UI.ExpressionBinding" /> objects from the collection.</summary>
	public void Clear()
	{
		list.Clear();
		removed.Clear();
		OnChanged(new EventArgs());
	}

	/// <summary>Determines whether the <see cref="T:System.Web.UI.ExpressionBindingCollection" /> collection contains a specific <see cref="T:System.Web.UI.ExpressionBinding" /> object.</summary>
	/// <param name="propName">The <see cref="P:System.Web.UI.ExpressionBinding.PropertyName" /> of the <see cref="T:System.Web.UI.ExpressionBinding" /> to locate in the collection.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.ExpressionBinding" /> is found in the <see cref="T:System.Web.UI.ExpressionBindingCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(string propName)
	{
		return list.Contains(propName);
	}

	/// <summary>Copies all the <see cref="T:System.Web.UI.ExpressionBinding" /> objects from the <see cref="T:System.Web.UI.ExpressionBindingCollection" /> collection to a one-dimensional array, starting at the specified index in the target array.</summary>
	/// <param name="array">The zero-based array that receives the <see cref="T:System.Web.UI.ExpressionBinding" /> objects copied from the collection.</param>
	/// <param name="index">The position in the target array at which the array starts receiving the copied items.</param>
	public void CopyTo(Array array, int index)
	{
		list.CopyTo(array, index);
	}

	/// <summary>Copies all the <see cref="T:System.Web.UI.ExpressionBinding" /> objects from the <see cref="T:System.Web.UI.ExpressionBindingCollection" /> collection to a one-dimensional array of <see cref="T:System.Web.UI.ExpressionBinding" /> objects, starting at the specified index in the target array.</summary>
	/// <param name="array">The zero-based array of <see cref="T:System.Web.UI.ExpressionBinding" /> objects that receives the <see cref="T:System.Web.UI.ExpressionBinding" /> objects copied from the collection.</param>
	/// <param name="index">The position in the target array at which the array starts receiving the copied items.</param>
	public void CopyTo(ExpressionBinding[] array, int index)
	{
		if (index < 0)
		{
			throw new ArgumentNullException("Index cannot be negative");
		}
		if (index >= array.Length)
		{
			throw new ArgumentException("Index cannot be greater than or equal to length of array passed");
		}
		if (list.Count > array.Length - index + 1)
		{
			throw new ArgumentException("Number of elements in source is greater than available space from index to end of destination");
		}
		foreach (string key in list.Keys)
		{
			array[index++] = (ExpressionBinding)list[key];
		}
	}

	/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" />-implemented object that can be used to iterate through the <see cref="T:System.Web.UI.ExpressionBinding" /> objects in the collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all the <see cref="T:System.Web.UI.ExpressionBinding" /> objects in the <see cref="T:System.Web.UI.ExpressionBindingCollection" />.</returns>
	public IEnumerator GetEnumerator()
	{
		return list.GetEnumerator();
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.ExpressionBinding" /> object from the collection.</summary>
	/// <param name="binding">The <see cref="T:System.Web.UI.ExpressionBinding" /> to remove from the collection.</param>
	public void Remove(ExpressionBinding binding)
	{
		Remove(binding.PropertyName, addToRemovedList: true);
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.ExpressionBinding" /> object from the collection.</summary>
	/// <param name="propertyName">The <see cref="P:System.Web.UI.ExpressionBinding.PropertyName" /> of the <see cref="T:System.Web.UI.ExpressionBinding" /> to remove from the collection.</param>
	public void Remove(string propertyName)
	{
		Remove(propertyName, addToRemovedList: true);
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.ExpressionBinding" /> object from the collection.</summary>
	/// <param name="propertyName">The <see cref="P:System.Web.UI.ExpressionBinding.PropertyName" /> of the <see cref="T:System.Web.UI.ExpressionBinding" /> to remove from the collection.</param>
	/// <param name="addToRemovedList">
	///       <see langword="true" /> to add the <see cref="T:System.Web.UI.ExpressionBinding" /> to the <see cref="P:System.Web.UI.ExpressionBindingCollection.RemovedBindings" /> collection; otherwise, <see langword="false" />.</param>
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
		OnChanged(new EventArgs());
	}

	private void OnChanged(EventArgs e)
	{
		if (events[changedEvent] is EventHandler eventHandler)
		{
			eventHandler(this, e);
		}
	}
}
