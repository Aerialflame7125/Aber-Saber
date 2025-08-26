using System.Collections;
using System.Collections.Generic;

namespace System.Web.UI;

/// <summary>Provides a base class for all strongly typed collections that manage <see cref="T:System.Web.UI.IStateManager" /> objects.</summary>
public abstract class StateManagedCollection : IList, ICollection, IEnumerable, IStateManager
{
	private ArrayList items = new ArrayList();

	private bool saveEverything;

	private bool isTrackingViewState;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.StateManagedCollection" /> collection is saving changes to its view state.</summary>
	/// <returns>
	///     <see langword="true" /> if the collection is marked to save its own state and the state of all the <see cref="T:System.Web.UI.IStateManager" /> items it contains; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => isTrackingViewState;

	/// <summary>Gets the number of elements contained in the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
	/// <returns>The number of elements contained in the <see cref="T:System.Web.UI.StateManagedCollection" />.</returns>
	public int Count => items.Count;

	/// <summary>Gets the number of elements contained in the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
	/// <returns>The number of elements in the <see cref="T:System.Web.UI.StateManagedCollection" />.</returns>
	int ICollection.Count => items.Count;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.StateManagedCollection" /> collection is synchronized (thread safe). This method returns <see langword="false" /> in all cases.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	bool ICollection.IsSynchronized => false;

	/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Web.UI.StateManagedCollection" /> collection. This method returns <see langword="null" /> in all cases.</summary>
	/// <returns>
	///     <see langword="null" /> in all cases.</returns>
	object ICollection.SyncRoot => this;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.StateManagedCollection" /> collection has a fixed size. This method returns <see langword="false" /> in all cases.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	bool IList.IsFixedSize => false;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.StateManagedCollection" /> collection is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.StateManagedCollection" /> is read-only; otherwise, <see langword="false" />.</returns>
	bool IList.IsReadOnly => false;

	/// <summary>Gets the <see cref="T:System.Web.UI.IStateManager" /> element at the specified index.</summary>
	/// <param name="index">The zero-based index of the element to get.</param>
	/// <returns>The element at the specified index.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="index" /> is out of range of the collection.</exception>
	object IList.this[int index]
	{
		get
		{
			return items[index];
		}
		set
		{
			if (index < 0 || index >= Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			OnValidate(value);
			if (isTrackingViewState)
			{
				((IStateManager)value).TrackViewState();
				SetDirty();
			}
			items[index] = value;
		}
	}

	/// <summary>When overridden in a derived class, creates an instance of a class that implements <see cref="T:System.Web.UI.IStateManager" />. The type of object created is based on the specified member of the collection returned by the <see cref="M:System.Web.UI.StateManagedCollection.GetKnownTypes" /> method.</summary>
	/// <param name="index">The index, from the ordered list of types returned by <see cref="M:System.Web.UI.StateManagedCollection.GetKnownTypes" />, of the type of <see cref="T:System.Web.UI.IStateManager" /> to create.</param>
	/// <returns>An instance of a class derived from <see cref="T:System.Web.UI.IStateManager" />, according to the <paramref name="index" /> provided.</returns>
	/// <exception cref="T:System.InvalidOperationException">In all cases when not overridden in a derived class.</exception>
	protected virtual object CreateKnownType(int index)
	{
		return null;
	}

	/// <summary>Forces the entire <see cref="T:System.Web.UI.StateManagedCollection" /> collection to be serialized into view state. </summary>
	public void SetDirty()
	{
		saveEverything = true;
		for (int i = 0; i < items.Count; i++)
		{
			SetDirtyObject(items[i]);
		}
	}

	/// <summary>When overridden in a derived class, instructs an <see langword="object" /> contained by the collection to record its entire state to view state, rather than recording only change information.</summary>
	/// <param name="o">The <see cref="T:System.Web.UI.IStateManager" /> that should serialize itself completely.</param>
	protected abstract void SetDirtyObject(object o);

	/// <summary>When overridden in a derived class, gets an array of <see cref="T:System.Web.UI.IStateManager" /> types that the <see cref="T:System.Web.UI.StateManagedCollection" /> collection can contain.</summary>
	/// <returns>An ordered array of <see cref="T:System.Type" /> objects that identify the types of <see cref="T:System.Web.UI.IStateManager" /> objects the collection can contain. The default implementation returns <see langword="null" />.</returns>
	protected virtual Type[] GetKnownTypes()
	{
		return null;
	}

	/// <summary>When overridden in a derived class, performs additional work before the <see cref="M:System.Web.UI.StateManagedCollection.Clear" /> method removes all items from the collection.</summary>
	protected virtual void OnClear()
	{
	}

	/// <summary>When overridden in a derived class, performs additional work after the <see cref="M:System.Web.UI.StateManagedCollection.Clear" /> method finishes removing all items from the collection.</summary>
	protected virtual void OnClearComplete()
	{
	}

	/// <summary>When overridden in a derived class, performs additional work before the <see cref="M:System.Web.UI.StateManagedCollection.System#Collections#IList#Insert(System.Int32,System.Object)" /> or <see cref="M:System.Web.UI.StateManagedCollection.System#Collections#IList#Add(System.Object)" /> method adds an item to the collection.</summary>
	/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted by the <see cref="M:System.Web.UI.StateManagedCollection.System#Collections#IList#Insert(System.Int32,System.Object)" /> method.</param>
	/// <param name="value">The object to insert into the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
	protected virtual void OnInsert(int index, object value)
	{
	}

	/// <summary>When overridden in a derived class, performs additional work after the <see cref="M:System.Web.UI.StateManagedCollection.System#Collections#IList#Insert(System.Int32,System.Object)" /> or <see cref="M:System.Web.UI.StateManagedCollection.System#Collections#IList#Add(System.Object)" /> method adds an item to the collection.</summary>
	/// <param name="index">The zero-based index at which <paramref name="value" /> is inserted by the <see cref="M:System.Web.UI.StateManagedCollection.System#Collections#IList#Insert(System.Int32,System.Object)" /> method.</param>
	/// <param name="value">The object inserted into the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
	protected virtual void OnInsertComplete(int index, object value)
	{
	}

	/// <summary>When overridden in a derived class, performs additional work before the <see cref="M:System.Web.UI.StateManagedCollection.System#Collections#IList#Remove(System.Object)" /> or <see cref="M:System.Web.UI.StateManagedCollection.System#Collections#IList#RemoveAt(System.Int32)" /> method removes the specified item from the collection.</summary>
	/// <param name="index">The zero-based index of the item to remove, which is used when <see cref="M:System.Web.UI.StateManagedCollection.System#Collections#IList#RemoveAt(System.Int32)" /> is called.</param>
	/// <param name="value">The object to remove from the <see cref="T:System.Web.UI.StateManagedCollection" />, which is used when <see cref="M:System.Web.UI.StateManagedCollection.System#Collections#IList#Remove(System.Object)" /> is called.</param>
	protected virtual void OnRemove(int index, object value)
	{
	}

	/// <summary>When overridden in a derived class, performs additional work after the <see cref="M:System.Web.UI.StateManagedCollection.System#Collections#IList#Remove(System.Object)" /> or <see cref="M:System.Web.UI.StateManagedCollection.System#Collections#IList#RemoveAt(System.Int32)" /> method removes the specified item from the collection.</summary>
	/// <param name="index">The zero-based index of the item to remove, which is used when <see cref="M:System.Web.UI.StateManagedCollection.System#Collections#IList#RemoveAt(System.Int32)" /> is called.</param>
	/// <param name="value">The object removed from the <see cref="T:System.Web.UI.StateManagedCollection" />, which is used when <see cref="M:System.Web.UI.StateManagedCollection.System#Collections#IList#Remove(System.Object)" /> is called.</param>
	protected virtual void OnRemoveComplete(int index, object value)
	{
	}

	/// <summary>When overridden in a derived class, validates an element of the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
	/// <param name="value">The <see cref="T:System.Web.UI.IStateManager" /> to validate.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="value" /> is <see langword="null" />.</exception>
	protected virtual void OnValidate(object value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
	}

	/// <summary>Restores the previously saved view state of the <see cref="T:System.Web.UI.StateManagedCollection" /> collection and the <see cref="T:System.Web.UI.IStateManager" /> items it contains.</summary>
	/// <param name="savedState">An object that represents the collection and collection elements' state to restore.</param>
	void IStateManager.LoadViewState(object savedState)
	{
		if (savedState == null)
		{
			foreach (IStateManager item in items)
			{
				item.LoadViewState(null);
			}
			return;
		}
		Triplet obj = (savedState as Triplet) ?? throw new InvalidOperationException("Internal error.");
		List<int> list = obj.First as List<int>;
		List<object> list2 = obj.Second as List<object>;
		List<object> list3 = obj.Third as List<object>;
		saveEverything = list == null;
		if (saveEverything)
		{
			Clear();
			for (int i = 0; i < list2.Count; i++)
			{
				object obj2 = list3[i];
				IStateManager stateManager;
				if (obj2 is Type)
				{
					stateManager = (IStateManager)Activator.CreateInstance((Type)obj2);
				}
				else
				{
					if (!(obj2 is int))
					{
						continue;
					}
					stateManager = (IStateManager)CreateKnownType((int)obj2);
				}
				stateManager.TrackViewState();
				stateManager.LoadViewState(list2[i]);
				((IList)this).Add((object)stateManager);
			}
			return;
		}
		for (int j = 0; j < list.Count; j++)
		{
			int num = list[j];
			IStateManager stateManager;
			if (num < Count)
			{
				stateManager = ((IList)this)[num] as IStateManager;
				stateManager.TrackViewState();
				stateManager.LoadViewState(list2[j]);
				continue;
			}
			object obj2 = list3[j];
			if (obj2 is Type)
			{
				stateManager = (IStateManager)Activator.CreateInstance((Type)obj2);
			}
			else
			{
				if (!(obj2 is int))
				{
					continue;
				}
				stateManager = (IStateManager)CreateKnownType((int)obj2);
			}
			stateManager.TrackViewState();
			stateManager.LoadViewState(list2[j]);
			((IList)this).Add((object)stateManager);
		}
	}

	private void AddListItem<T>(ref List<T> list, T item)
	{
		if (list == null)
		{
			list = new List<T>();
		}
		list.Add(item);
	}

	/// <summary>Saves the changes to the <see cref="T:System.Web.UI.StateManagedCollection" /> collection and each <see cref="T:System.Web.UI.IStateManager" /> object it contains since the time the page was posted back to the server.</summary>
	/// <returns>The object that contains the changes to the view state of the <see cref="T:System.Web.UI.StateManagedCollection" /> and the items it contains. If no view state is associated with the collection and its elements, this method returns <see langword="null" />.</returns>
	object IStateManager.SaveViewState()
	{
		Type[] knownTypes = GetKnownTypes();
		bool flag = false;
		bool flag2 = knownTypes != null && knownTypes.Length != 0;
		int count = items.Count;
		List<int> list = null;
		List<object> list2 = null;
		List<object> list3 = null;
		for (int i = 0; i < count; i++)
		{
			if (!(items[i] is IStateManager stateManager))
			{
				continue;
			}
			stateManager.TrackViewState();
			object obj = stateManager.SaveViewState();
			if (saveEverything || obj != null)
			{
				flag = true;
				Type type = stateManager.GetType();
				int num = (flag2 ? Array.IndexOf(knownTypes, type) : (-1));
				if (!saveEverything)
				{
					AddListItem(ref list, i);
				}
				AddListItem(ref list2, obj);
				if (num == -1)
				{
					AddListItem(ref list3, type);
				}
				else
				{
					AddListItem(ref list3, num);
				}
			}
		}
		if (!flag)
		{
			return null;
		}
		return new Triplet(list, list2, list3);
	}

	/// <summary>Causes the <see cref="T:System.Web.UI.StateManagedCollection" /> collection and each of the <see cref="T:System.Web.UI.IStateManager" /> objects it contains to track changes to their view state so they can be persisted across requests for the same page.</summary>
	void IStateManager.TrackViewState()
	{
		isTrackingViewState = true;
		if (items == null || items.Count <= 0)
		{
			return;
		}
		foreach (object item in items)
		{
			if (item is IStateManager stateManager)
			{
				stateManager.TrackViewState();
			}
		}
	}

	/// <summary>Removes all items from the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
	public void Clear()
	{
		OnClear();
		items.Clear();
		OnClearComplete();
		if (isTrackingViewState)
		{
			SetDirty();
		}
	}

	/// <summary>Returns an iterator that iterates through the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the <see cref="T:System.Web.UI.StateManagedCollection" />.</returns>
	public IEnumerator GetEnumerator()
	{
		return items.GetEnumerator();
	}

	/// <summary>Copies the elements of the <see cref="T:System.Web.UI.StateManagedCollection" /> collection to an array, starting at a particular array index.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Web.UI.StateManagedCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="array" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is less than zero.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="array" /> is multidimensional.- or -
	///         <paramref name="index" /> is greater than or equal to the length of <paramref name="array" />.- or -The number of elements in the source <see cref="T:System.Web.UI.StateManagedCollection" /> is greater than the available space from the <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
	public void CopyTo(Array array, int index)
	{
		items.CopyTo(array, index);
	}

	/// <summary>Returns an iterator that iterates through the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the <see cref="T:System.Web.UI.StateManagedCollection" />.</returns>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	/// <summary>Adds an item to the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
	/// <param name="value">The<see langword=" object " />to add to the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
	/// <returns>The position at which the new element was inserted.</returns>
	/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="value" /> is <see langword="null" />.</exception>
	int IList.Add(object value)
	{
		OnValidate(value);
		if (isTrackingViewState)
		{
			((IStateManager)value).TrackViewState();
			SetDirtyObject(value);
		}
		OnInsert(-1, value);
		items.Add(value);
		OnInsertComplete(-1, value);
		return Count - 1;
	}

	/// <summary>Inserts an item into the <see cref="T:System.Web.UI.StateManagedCollection" /> collection at the specified index.</summary>
	/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
	/// <param name="value">The object to insert into the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="index" /> is out of range of the collection.</exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.UI.StateManagedCollection" /> is read-only.</exception>
	/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="value" /> is <see langword="null" />.</exception>
	void IList.Insert(int index, object value)
	{
		OnValidate(value);
		if (isTrackingViewState)
		{
			((IStateManager)value).TrackViewState();
			SetDirty();
		}
		OnInsert(index, value);
		items.Insert(index, value);
		OnInsertComplete(index, value);
	}

	/// <summary>Removes the first occurrence of the specified object from the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
	/// <param name="value">The object to remove from the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.UI.StateManagedCollection" /> is read-only.</exception>
	void IList.Remove(object value)
	{
		if (value != null)
		{
			OnValidate(value);
			int num = ((IList)this).IndexOf(value);
			if (num >= 0)
			{
				((IList)this).RemoveAt(num);
			}
		}
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.IStateManager" /> element at the specified index.</summary>
	/// <param name="index">The zero-based index of the item to remove.</param>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.UI.StateManagedCollection" /> is read-only.</exception>
	void IList.RemoveAt(int index)
	{
		object value = items[index];
		OnRemove(index, value);
		items.RemoveAt(index);
		OnRemoveComplete(index, value);
		if (isTrackingViewState)
		{
			SetDirty();
		}
	}

	/// <summary>Removes all items from the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
	void IList.Clear()
	{
		Clear();
	}

	/// <summary>Determines whether the <see cref="T:System.Web.UI.StateManagedCollection" /> collection contains a specific value.</summary>
	/// <param name="value">The <see langword="object" /> to locate in the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
	/// <returns>
	///     <see langword="true" /> if the object is found in the <see cref="T:System.Web.UI.StateManagedCollection" />; otherwise, <see langword="false" />. If <see langword="null" /> is passed for the value parameter, <see langword="false" /> is returned.</returns>
	bool IList.Contains(object value)
	{
		if (value == null)
		{
			return false;
		}
		OnValidate(value);
		return items.Contains(value);
	}

	/// <summary>Determines the index of a specified item in the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
	/// <param name="value">The object to locate in the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
	/// <returns>The index of <paramref name="value" />, if it is found in the list; otherwise, -1.</returns>
	int IList.IndexOf(object value)
	{
		if (value == null)
		{
			return -1;
		}
		OnValidate(value);
		return items.IndexOf(value);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.StateManagedCollection" /> class. </summary>
	protected StateManagedCollection()
	{
	}
}
