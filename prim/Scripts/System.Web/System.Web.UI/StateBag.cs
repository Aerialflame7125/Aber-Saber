using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Manages the view state of ASP.NET server controls, including pages. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class StateBag : IDictionary, ICollection, IEnumerable, IStateManager
{
	private HybridDictionary ht;

	private bool track;

	/// <summary>Gets a value indicating whether state changes are being tracked.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.StateBag" /> is marked to save changes to its state; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => track;

	internal bool IsTrackingViewState => track;

	/// <summary>Gets the number of <see cref="T:System.Web.UI.StateItem" /> objects in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
	/// <returns>The number of items in the <see cref="T:System.Web.UI.StateBag" />.</returns>
	public int Count => ht.Count;

	/// <summary>Gets or sets the value of an item stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
	/// <param name="key">The key for the item. </param>
	/// <returns>The specified item in the <see cref="T:System.Web.UI.StateBag" /> object.</returns>
	public object this[string key]
	{
		get
		{
			if (ht[key] is StateItem stateItem)
			{
				return stateItem.Value;
			}
			return null;
		}
		set
		{
			if (value == null && !IsTrackingViewState)
			{
				Remove(key);
			}
			else
			{
				Add(key, value);
			}
		}
	}

	/// <summary>Gets a collection of keys representing the items in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
	/// <returns>The collection of keys.</returns>
	public ICollection Keys => ht.Keys;

	/// <summary>Gets a collection of the view-state values stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
	/// <returns>The collection of view-state values.</returns>
	public ICollection Values => ht.Values;

	/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
	/// <returns>
	///     <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
	bool ICollection.IsSynchronized => false;

	/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
	/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
	object ICollection.SyncRoot => ht;

	/// <summary>For a description of this member, see <see cref="P:System.Collections.IDictionary.Item(System.Object)" />.</summary>
	/// <param name="key">The key of the element to get.</param>
	/// <returns>The element with the specified <paramref name="key" />.</returns>
	object IDictionary.this[object key]
	{
		get
		{
			return this[(string)key];
		}
		set
		{
			this[(string)key] = value;
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Collections.IDictionary.IsFixedSize" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> object has a fixed size; otherwise, <see langword="false" />.</returns>
	bool IDictionary.IsFixedSize => false;

	/// <summary>For a description of this member, see <see cref="P:System.Collections.IDictionary.IsReadOnly" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> object is read-only; otherwise, <see langword="false" />.</returns>
	bool IDictionary.IsReadOnly => false;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.StateBag" /> class that allows stored state values to be case-insensitive.</summary>
	/// <param name="ignoreCase">
	///       <see langword="true" /> to ignore case; otherwise, <see langword="false" />. </param>
	public StateBag(bool ignoreCase)
	{
		ht = new HybridDictionary(ignoreCase);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.StateBag" /> class. This is the default constructor for this class.</summary>
	public StateBag()
		: this(ignoreCase: false)
	{
	}

	/// <summary>Restores the previously saved view state of the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
	/// <param name="state">An object that represents the <see cref="T:System.Web.UI.StateBag" /> state to restore. </param>
	void IStateManager.LoadViewState(object savedState)
	{
		LoadViewState(savedState);
	}

	/// <summary>Saves the changes to the <see cref="T:System.Web.UI.StateBag" /> object since the time the page was posted back to the server.</summary>
	/// <returns>The object that contains the changes to the view state of the <see cref="T:System.Web.UI.StateBag" />. If there are no changes, or there are no <see cref="T:System.Web.UI.StateItem" /> elements in the <see cref="T:System.Web.UI.StateBag" />, this method returns <see langword="null" />.</returns>
	object IStateManager.SaveViewState()
	{
		return SaveViewState();
	}

	/// <summary>Causes the <see cref="T:System.Web.UI.StateBag" /> object to track changes to its state so that it can be persisted across requests.</summary>
	void IStateManager.TrackViewState()
	{
		TrackViewState();
	}

	internal void LoadViewState(object savedState)
	{
		if (savedState == null)
		{
			return;
		}
		foreach (DictionaryEntry item in (Hashtable)savedState)
		{
			Add((string)item.Key, item.Value);
		}
	}

	internal object SaveViewState()
	{
		Hashtable hashtable = null;
		foreach (DictionaryEntry item in ht)
		{
			StateItem stateItem = (StateItem)item.Value;
			if (stateItem.IsDirty)
			{
				if (hashtable == null)
				{
					hashtable = new Hashtable();
				}
				hashtable.Add(item.Key, stateItem.Value);
			}
		}
		return hashtable;
	}

	internal void TrackViewState()
	{
		track = true;
	}

	/// <summary>Adds a new <see cref="T:System.Web.UI.StateItem" /> object to the <see cref="T:System.Web.UI.StateBag" /> object. If the item already exists in the <see cref="T:System.Web.UI.StateBag" /> object, this method updates the value of the item.</summary>
	/// <param name="key">The attribute name for the <see cref="T:System.Web.UI.StateItem" />. </param>
	/// <param name="value">The value of the item to add to the <see cref="T:System.Web.UI.StateBag" />. </param>
	/// <returns>Returns a <see cref="T:System.Web.UI.StateItem" /> that represents the object added to view state.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="key" /> is <see langword="null" />.- or -The number of characters in <paramref name="key" /> is 0. </exception>
	public StateItem Add(string key, object value)
	{
		StateItem stateItem = ht[key] as StateItem;
		if (stateItem == null)
		{
			stateItem = (StateItem)(ht[key] = new StateItem(value));
		}
		stateItem.Value = value;
		stateItem.IsDirty |= track;
		return stateItem;
	}

	internal string GetString(string key, string def)
	{
		string text = (string)this[key];
		if (text != null)
		{
			return text;
		}
		return def;
	}

	internal bool GetBool(string key, bool def)
	{
		object obj = this[key];
		if (obj != null)
		{
			return (bool)obj;
		}
		return def;
	}

	internal char GetChar(string key, char def)
	{
		object obj = this[key];
		if (obj != null)
		{
			return (char)obj;
		}
		return def;
	}

	internal int GetInt(string key, int def)
	{
		object obj = this[key];
		if (obj != null)
		{
			return (int)obj;
		}
		return def;
	}

	internal short GetShort(string key, short def)
	{
		object obj = this[key];
		if (obj != null)
		{
			return (short)obj;
		}
		return def;
	}

	/// <summary>Removes all items from the current <see cref="T:System.Web.UI.StateBag" /> object.</summary>
	public void Clear()
	{
		ht.Clear();
	}

	/// <summary>Returns an enumerator that iterates over all the key/value pairs of the <see cref="T:System.Web.UI.StateItem" /> objects stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
	/// <returns>The enumerator to iterate through the state bag.</returns>
	public IDictionaryEnumerator GetEnumerator()
	{
		return ht.GetEnumerator();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	/// <summary>Checks a <see cref="T:System.Web.UI.StateItem" /> object stored in the <see cref="T:System.Web.UI.StateBag" /> object to evaluate whether it has been modified since the call to <see cref="M:System.Web.UI.Control.TrackViewState" />.</summary>
	/// <param name="key">The key of the item to check. </param>
	/// <returns>
	///     <see langword="true" /> if the item has been modified; otherwise, <see langword="false" />.</returns>
	public bool IsItemDirty(string key)
	{
		if (ht[key] is StateItem stateItem)
		{
			return stateItem.IsDirty;
		}
		return false;
	}

	/// <summary>Removes the specified key/value pair from the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
	/// <param name="key">The item to remove. </param>
	public void Remove(string key)
	{
		ht.Remove(key);
	}

	/// <summary>Sets the <see cref="P:System.Web.SessionState.ISessionStateItemCollection.Dirty" /> property for the specified <see cref="T:System.Web.UI.StateItem" /> object in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
	/// <param name="key">The key that identifies which <see cref="T:System.Web.UI.StateItem" /> in the <see cref="T:System.Web.UI.StateBag" /> to set. </param>
	/// <param name="dirty">
	///       <see langword="true" /> to mark the state of the item as modified; otherwise, <see langword="false" />.</param>
	public void SetItemDirty(string key, bool dirty)
	{
		StateItem stateItem = (StateItem)ht[key];
		if (stateItem != null)
		{
			stateItem.IsDirty = dirty;
		}
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IDictionary.Add(System.Object,System.Object)" />.</summary>
	/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
	/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add. </param>
	void IDictionary.Add(object key, object value)
	{
		Add((string)key, value);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IDictionary.Remove(System.Object)" />.</summary>
	/// <param name="key">The key of the element to remove. </param>
	void IDictionary.Remove(object key)
	{
		Remove((string)key);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	void ICollection.CopyTo(Array array, int index)
	{
		ht.CopyTo(array, index);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IDictionary.Contains(System.Object)" />.</summary>
	/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" /> object.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, <see langword="false" />.</returns>
	bool IDictionary.Contains(object key)
	{
		return ht.Contains(key);
	}

	/// <summary>Sets the state of the <see cref="T:System.Web.UI.StateBag" /> object as well as the <see cref="P:System.Web.SessionState.ISessionStateItemCollection.Dirty" /> property of each of the <see cref="T:System.Web.UI.StateItem" /> objects contained by it.</summary>
	/// <param name="dirty">
	///       <see langword="true" /> to mark the state of the collection and its items as modified; otherwise, <see langword="false" />.</param>
	public void SetDirty(bool dirty)
	{
		foreach (DictionaryEntry item in ht)
		{
			((StateItem)item.Value).IsDirty = dirty;
		}
	}
}
