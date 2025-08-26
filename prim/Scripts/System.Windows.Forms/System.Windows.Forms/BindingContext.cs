using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Manages the collection of <see cref="T:System.Windows.Forms.BindingManagerBase" /> objects for any object that inherits from the <see cref="T:System.Windows.Forms.Control" /> class.</summary>
/// <filterpriority>2</filterpriority>
[DefaultEvent("CollectionChanged")]
public class BindingContext : ICollection, IEnumerable
{
	private class HashKey
	{
		public object source;

		public string member;

		public HashKey(object source, string member)
		{
			this.source = source;
			this.member = member;
		}

		public override int GetHashCode()
		{
			return source.GetHashCode() ^ member.GetHashCode();
		}

		public override bool Equals(object o)
		{
			if (!(o is HashKey hashKey))
			{
				return false;
			}
			return hashKey.source == source && hashKey.member == member;
		}
	}

	private Hashtable managers;

	private EventHandler onCollectionChangedHandler;

	/// <summary>Gets the total number of <see cref="T:System.Windows.Forms.CurrencyManager" /> objects managed by the <see cref="T:System.Windows.Forms.BindingContext" />.</summary>
	/// <returns>The number of data sources managed by the <see cref="T:System.Windows.Forms.BindingContext" />.</returns>
	int ICollection.Count => managers.Count;

	/// <summary>Gets a value indicating whether the collection is synchronized.</summary>
	/// <returns>true if the collection is thread safe; otherwise, false.</returns>
	bool ICollection.IsSynchronized => false;

	/// <summary>Gets an object to use for synchronization (thread safety).</summary>
	/// <returns>This property is derived from <see cref="T:System.Collections.ICollection" />, and is overridden to always return null.</returns>
	object ICollection.SyncRoot => null;

	/// <summary>Gets a value indicating whether the collection is read-only.</summary>
	/// <returns>true if the collection is read-only; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool IsReadOnly => false;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.BindingManagerBase" /> that is associated with the specified data source.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.BindingManagerBase" /> for the specified data source.</returns>
	/// <param name="dataSource">The data source associated with a particular <see cref="T:System.Windows.Forms.BindingManagerBase" />. </param>
	/// <filterpriority>1</filterpriority>
	public BindingManagerBase this[object dataSource] => this[dataSource, string.Empty];

	/// <summary>Gets a <see cref="T:System.Windows.Forms.BindingManagerBase" /> that is associated with the specified data source and data member.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.BindingManagerBase" /> for the specified data source and data member.</returns>
	/// <param name="dataSource">The data source associated with a particular <see cref="T:System.Windows.Forms.BindingManagerBase" />. </param>
	/// <param name="dataMember">A navigation path containing the information that resolves to a specific <see cref="T:System.Windows.Forms.BindingManagerBase" />. </param>
	/// <exception cref="T:System.Exception">The specified <paramref name="dataMember" /> does not exist within the data source. </exception>
	/// <filterpriority>1</filterpriority>
	public BindingManagerBase this[object dataSource, string dataMember]
	{
		get
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			if (dataMember == null)
			{
				dataMember = string.Empty;
			}
			if (dataSource is ICurrencyManagerProvider currencyManagerProvider)
			{
				if (dataMember.Length == 0)
				{
					return currencyManagerProvider.CurrencyManager;
				}
				return currencyManagerProvider.GetRelatedCurrencyManager(dataMember);
			}
			HashKey key = new HashKey(dataSource, dataMember);
			if (managers[key] is BindingManagerBase result)
			{
				return result;
			}
			BindingManagerBase bindingManagerBase = CreateBindingManager(dataSource, dataMember);
			if (bindingManagerBase == null)
			{
				return null;
			}
			managers[key] = bindingManagerBase;
			return bindingManagerBase;
		}
	}

	/// <summary>Always raises a <see cref="T:System.NotImplementedException" /> when handled.</summary>
	/// <exception cref="T:System.NotImplementedException">Occurs in all cases.</exception>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public event CollectionChangeEventHandler CollectionChanged
	{
		add
		{
			throw new NotImplementedException();
		}
		remove
		{
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingContext" /> class.</summary>
	public BindingContext()
	{
		managers = new Hashtable();
		onCollectionChangedHandler = null;
	}

	/// <summary>Copies the elements of the collection into a specified array, starting at the collection index.</summary>
	/// <param name="ar">An <see cref="T:System.Array" /> to copy into. </param>
	/// <param name="index">The collection index to begin copying from. </param>
	void ICollection.CopyTo(Array ar, int index)
	{
		managers.CopyTo(ar, index);
	}

	/// <summary>Gets an enumerator for the collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the collection.</returns>
	[System.MonoInternalNote("our enumerator is slightly different.  in MS's implementation the Values are WeakReferences to the managers.")]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return managers.GetEnumerator();
	}

	private BindingManagerBase CreateBindingManager(object data_source, string data_member)
	{
		if (data_member == string.Empty)
		{
			if (IsListType(data_source.GetType()))
			{
				return new CurrencyManager(data_source);
			}
			return new PropertyManager(data_source);
		}
		BindingMemberInfo bindingMemberInfo = new BindingMemberInfo(data_member);
		BindingManagerBase bindingManagerBase = this[data_source, bindingMemberInfo.BindingPath];
		PropertyDescriptor propertyDescriptor = bindingManagerBase?.GetItemProperties().Find(bindingMemberInfo.BindingField, ignoreCase: true);
		if (propertyDescriptor == null)
		{
			throw new ArgumentException($"Cannot create a child list for field {bindingMemberInfo.BindingField}.");
		}
		if (IsListType(propertyDescriptor.PropertyType))
		{
			return new RelatedCurrencyManager(bindingManagerBase, propertyDescriptor);
		}
		return new RelatedPropertyManager(bindingManagerBase, bindingMemberInfo.BindingField);
	}

	private bool IsListType(Type t)
	{
		return typeof(IList).IsAssignableFrom(t) || typeof(IListSource).IsAssignableFrom(t);
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.BindingContext" /> contains the <see cref="T:System.Windows.Forms.BindingManagerBase" /> associated with the specified data source.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.BindingContext" /> contains the specified <see cref="T:System.Windows.Forms.BindingManagerBase" />; otherwise, false.</returns>
	/// <param name="dataSource">An <see cref="T:System.Object" /> that represents the data source. </param>
	/// <filterpriority>1</filterpriority>
	public bool Contains(object dataSource)
	{
		return Contains(dataSource, string.Empty);
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.BindingContext" /> contains the <see cref="T:System.Windows.Forms.BindingManagerBase" /> associated with the specified data source and data member.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.BindingContext" /> contains the specified <see cref="T:System.Windows.Forms.BindingManagerBase" />; otherwise, false.</returns>
	/// <param name="dataSource">An <see cref="T:System.Object" /> that represents the data source. </param>
	/// <param name="dataMember">The information needed to resolve to a specific <see cref="T:System.Windows.Forms.BindingManagerBase" />. </param>
	/// <filterpriority>1</filterpriority>
	public bool Contains(object dataSource, string dataMember)
	{
		if (dataSource == null)
		{
			throw new ArgumentNullException("dataSource");
		}
		if (dataMember == null)
		{
			dataMember = string.Empty;
		}
		HashKey key = new HashKey(dataSource, dataMember);
		return managers[key] != null;
	}

	/// <summary>Adds the <see cref="T:System.Windows.Forms.BindingManagerBase" /> associated with a specific data source to the collection.</summary>
	/// <param name="dataSource">The <see cref="T:System.Object" /> associated with the <see cref="T:System.Windows.Forms.BindingManagerBase" />. </param>
	/// <param name="listManager">The <see cref="T:System.Windows.Forms.BindingManagerBase" /> to add. </param>
	protected internal void Add(object dataSource, BindingManagerBase listManager)
	{
		AddCore(dataSource, listManager);
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataSource));
	}

	/// <summary>Adds the <see cref="T:System.Windows.Forms.BindingManagerBase" /> associated with a specific data source to the collection.</summary>
	/// <param name="dataSource">The object associated with the <see cref="T:System.Windows.Forms.BindingManagerBase" />. </param>
	/// <param name="listManager">The <see cref="T:System.Windows.Forms.BindingManagerBase" /> to add.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataSource" /> is null.-or-<paramref name="listManager" /> is null.</exception>
	protected virtual void AddCore(object dataSource, BindingManagerBase listManager)
	{
		if (dataSource == null)
		{
			throw new ArgumentNullException("dataSource");
		}
		if (listManager == null)
		{
			throw new ArgumentNullException("listManager");
		}
		HashKey key = new HashKey(dataSource, string.Empty);
		managers[key] = listManager;
	}

	/// <summary>Clears the collection of any <see cref="T:System.Windows.Forms.BindingManagerBase" /> objects.</summary>
	protected internal void Clear()
	{
		ClearCore();
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
	}

	/// <summary>Clears the collection.</summary>
	protected virtual void ClearCore()
	{
		managers.Clear();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingContext.CollectionChanged" /> event.</summary>
	/// <param name="ccevent">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data.</param>
	protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
	{
		if (onCollectionChangedHandler != null)
		{
			onCollectionChangedHandler(this, ccevent);
		}
	}

	/// <summary>Deletes the <see cref="T:System.Windows.Forms.BindingManagerBase" /> associated with the specified data source.</summary>
	/// <param name="dataSource">The data source associated with the <see cref="T:System.Windows.Forms.BindingManagerBase" /> to remove. </param>
	protected internal void Remove(object dataSource)
	{
		if (dataSource == null)
		{
			throw new ArgumentNullException("dataSource");
		}
		RemoveCore(dataSource);
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataSource));
	}

	/// <summary>Removes the <see cref="T:System.Windows.Forms.BindingManagerBase" /> associated with the specified data source.</summary>
	/// <param name="dataSource">The data source associated with the <see cref="T:System.Windows.Forms.BindingManagerBase" /> to remove.</param>
	protected virtual void RemoveCore(object dataSource)
	{
		HashKey[] array = new HashKey[managers.Keys.Count];
		managers.Keys.CopyTo(array, 0);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].source == dataSource)
			{
				managers.Remove(array[i]);
			}
		}
	}

	/// <summary>Associates a <see cref="T:System.Windows.Forms.Binding" /> with a new <see cref="T:System.Windows.Forms.BindingContext" />.</summary>
	/// <param name="newBindingContext">The new <see cref="T:System.Windows.Forms.BindingContext" /> to associate with the <see cref="T:System.Windows.Forms.Binding" />.</param>
	/// <param name="binding">The <see cref="T:System.Windows.Forms.Binding" /> to associate with the new <see cref="T:System.Windows.Forms.BindingContext" />.</param>
	/// <filterpriority>1</filterpriority>
	[System.MonoTODO("Stub, does nothing")]
	public static void UpdateBinding(BindingContext newBindingContext, Binding binding)
	{
	}
}
