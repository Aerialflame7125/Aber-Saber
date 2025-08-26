using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text.RegularExpressions;

namespace System.Windows.Forms;

/// <summary>Encapsulates the data source for a form.</summary>
[ComplexBindingProperties("DataSource", "DataMember")]
[Designer("System.Windows.Forms.Design.BindingSourceDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[DefaultProperty("DataSource")]
[DefaultEvent("CurrentChanged")]
public class BindingSource : Component, IDisposable, ICollection, IEnumerable, IList, IComponent, IBindingList, IBindingListView, ISupportInitializeNotification, ISupportInitialize, ICancelAddNew, ITypedList, ICurrencyManagerProvider
{
	private bool is_initialized = true;

	private IList list;

	private CurrencyManager currency_manager;

	private Dictionary<string, CurrencyManager> related_currency_managers = new Dictionary<string, CurrencyManager>();

	internal Type item_type;

	private bool item_has_default_ctor;

	private bool list_is_ibinding;

	private object datasource;

	private string datamember;

	private bool raise_list_changed_events;

	private bool allow_new_set;

	private bool allow_new;

	private bool add_pending;

	private int pending_add_index;

	private string filter;

	private string sort;

	private static object AddingNewEvent;

	private static object BindingCompleteEvent;

	private static object CurrentChangedEvent;

	private static object CurrentItemChangedEvent;

	private static object DataErrorEvent;

	private static object DataMemberChangedEvent;

	private static object DataSourceChangedEvent;

	private static object ListChangedEvent;

	private static object PositionChangedEvent;

	private static object InitializedEvent;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.BindingSource" /> is initialized.</summary>
	/// <returns>true to indicate the <see cref="T:System.Windows.Forms.BindingSource" /> is initialized; otherwise, false.</returns>
	bool ISupportInitializeNotification.IsInitialized => is_initialized;

	/// <summary>Gets a value indicating whether items in the underlying list can be edited.</summary>
	/// <returns>true to indicate list items can be edited; otherwise, false.</returns>
	[Browsable(false)]
	public virtual bool AllowEdit
	{
		get
		{
			if (list == null)
			{
				return false;
			}
			if (list.IsReadOnly)
			{
				return false;
			}
			if (list is IBindingList)
			{
				return ((IBindingList)list).AllowEdit;
			}
			return true;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="M:System.Windows.Forms.BindingSource.AddNew" /> method can be used to add items to the list.</summary>
	/// <returns>true if <see cref="M:System.Windows.Forms.BindingSource.AddNew" /> can be used to add items to the list; otherwise, false.</returns>
	/// <exception cref="T:System.InvalidOperationException">This property is set to true when the underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property has a fixed size or is read-only.</exception>
	/// <exception cref="T:System.MissingMethodException">The property is set to true and the <see cref="E:System.Windows.Forms.BindingSource.AddingNew" /> event is not handled when the underlying list type does not have a default constructor.</exception>
	public virtual bool AllowNew
	{
		get
		{
			if (allow_new_set)
			{
				return allow_new;
			}
			if (list is IBindingList)
			{
				return ((IBindingList)list).AllowNew;
			}
			if (list.IsFixedSize || list.IsReadOnly || !item_has_default_ctor)
			{
				return false;
			}
			return true;
		}
		set
		{
			if (value != allow_new || !allow_new_set)
			{
				if (value && (list.IsReadOnly || list.IsFixedSize))
				{
					throw new InvalidOperationException();
				}
				allow_new_set = true;
				allow_new = value;
				if (raise_list_changed_events)
				{
					OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
				}
			}
		}
	}

	private bool IsAddingNewHandled => (object)base.Events[AddingNew] != null;

	/// <summary>Gets a value indicating whether items can be removed from the underlying list.</summary>
	/// <returns>true to indicate list items can be removed from the list; otherwise, false.</returns>
	[Browsable(false)]
	public virtual bool AllowRemove
	{
		get
		{
			if (list == null)
			{
				return false;
			}
			if (list.IsFixedSize || list.IsReadOnly)
			{
				return false;
			}
			if (list is IBindingList)
			{
				return ((IBindingList)list).AllowRemove;
			}
			return true;
		}
	}

	/// <summary>Gets the total number of items in the underlying list, taking the current <see cref="P:System.Windows.Forms.BindingSource.Filter" /> value into consideration.</summary>
	/// <returns>The total number of filtered items in the underlying list.</returns>
	[Browsable(false)]
	public virtual int Count => list.Count;

	/// <summary>Gets the currency manager associated with this <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with this <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
	[Browsable(false)]
	public virtual CurrencyManager CurrencyManager => currency_manager;

	/// <summary>Gets the current item in the list.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the current item in the underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property, or null if the list has no items.</returns>
	[Browsable(false)]
	public object Current
	{
		get
		{
			if (currency_manager.Count > 0)
			{
				return currency_manager.Current;
			}
			return null;
		}
	}

	/// <summary>Gets or sets the specific list in the data source to which the connector currently binds to.</summary>
	/// <returns>The name of a list (or row) in the <see cref="P:System.Windows.Forms.BindingSource.DataSource" />. The default is an empty string ("").</returns>
	[Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue("")]
	public string DataMember
	{
		get
		{
			return datamember;
		}
		set
		{
			if (value == null)
			{
				value = string.Empty;
			}
			if (datamember != value)
			{
				datamember = value;
				ResetList();
				OnDataMemberChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the data source that the connector binds to.</summary>
	/// <returns>An <see cref="T:System.Object" /> that acts as a data source. The default is null.</returns>
	[DefaultValue(null)]
	[AttributeProvider(typeof(IListSource))]
	[RefreshProperties(RefreshProperties.Repaint)]
	public object DataSource
	{
		get
		{
			return datasource;
		}
		set
		{
			if (datasource != value)
			{
				if (datasource == null)
				{
					datamember = string.Empty;
				}
				DisconnectDataSourceEvents(datasource);
				datasource = value;
				ConnectDataSourceEvents(datasource);
				ResetList();
				OnDataSourceChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the expression used to filter which rows are viewed.</summary>
	/// <returns>A string that specifies how rows are to be filtered. The default is null.</returns>
	[DefaultValue(null)]
	public virtual string Filter
	{
		get
		{
			return filter;
		}
		set
		{
			if (SupportsFiltering)
			{
				((IBindingListView)list).Filter = value;
			}
			filter = value;
		}
	}

	/// <summary>Gets a value indicating whether the list binding is suspended.</summary>
	/// <returns>true to indicate the binding is suspended; otherwise, false. </returns>
	[Browsable(false)]
	public bool IsBindingSuspended => currency_manager.IsBindingSuspended;

	/// <summary>Gets a value indicating whether the underlying list has a fixed size.</summary>
	/// <returns>true if the underlying list has a fixed size; otherwise, false.</returns>
	[Browsable(false)]
	public virtual bool IsFixedSize => list.IsFixedSize;

	/// <summary>Gets a value indicating whether the underlying list is read-only.</summary>
	/// <returns>true if the list is read-only; otherwise, false.</returns>
	[Browsable(false)]
	public virtual bool IsReadOnly => list.IsReadOnly;

	/// <summary>Gets a value indicating whether the items in the underlying list are sorted. </summary>
	/// <returns>true if the list is an <see cref="T:System.ComponentModel.IBindingList" /> and is sorted; otherwise, false. </returns>
	[Browsable(false)]
	public virtual bool IsSorted => list is IBindingList && ((IBindingList)list).IsSorted;

	/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
	/// <returns>true to indicate the list is synchronized; otherwise, false.</returns>
	[Browsable(false)]
	public virtual bool IsSynchronized => list.IsSynchronized;

	/// <summary>Gets or sets the list element at the specified index.</summary>
	/// <returns>The element at the specified index.</returns>
	/// <param name="index">The zero-based index of the element to retrieve.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero or is equal to or greater than <see cref="P:System.Windows.Forms.BindingSource.Count" />.</exception>
	[Browsable(false)]
	public virtual object this[int index]
	{
		get
		{
			return list[index];
		}
		set
		{
			list[index] = value;
		}
	}

	/// <summary>Gets the list that the connector is bound to.</summary>
	/// <returns>An <see cref="T:System.Collections.IList" /> that represents the list, or null if there is no underlying list associated with this <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
	[Browsable(false)]
	public IList List => list;

	/// <summary>Gets or sets the index of the current item in the underlying list.</summary>
	/// <returns>A zero-based index that specifies the position of the current item in the underlying list.</returns>
	[Browsable(false)]
	[DefaultValue(-1)]
	public int Position
	{
		get
		{
			return currency_manager.Position;
		}
		set
		{
			if (value >= Count)
			{
				value = Count - 1;
			}
			if (value < 0)
			{
				value = 0;
			}
			currency_manager.Position = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether <see cref="E:System.Windows.Forms.BindingSource.ListChanged" /> events should be raised.</summary>
	/// <returns>true if <see cref="E:System.Windows.Forms.BindingSource.ListChanged" /> events should be raised; otherwise, false. The default is true.</returns>
	[DefaultValue(true)]
	[Browsable(false)]
	public bool RaiseListChangedEvents
	{
		get
		{
			return raise_list_changed_events;
		}
		set
		{
			raise_list_changed_events = value;
		}
	}

	/// <summary>Gets or sets the column names used for sorting, and the sort order for viewing the rows in the data source.</summary>
	/// <returns>A case-sensitive string containing the column name followed by "ASC" (for ascending) or "DESC" (for descending). The default is null.</returns>
	[DefaultValue(null)]
	public string Sort
	{
		get
		{
			return sort;
		}
		set
		{
			if (value == null || value.Length == 0)
			{
				if (list_is_ibinding && SupportsSorting)
				{
					RemoveSort();
				}
				sort = value;
				return;
			}
			if (!list_is_ibinding || !SupportsSorting)
			{
				throw new ArgumentException("value");
			}
			ProcessSortString(value);
			sort = value;
		}
	}

	/// <summary>Gets the collection of sort descriptions applied to the data source.</summary>
	/// <returns>If the data source is an <see cref="T:System.ComponentModel.IBindingListView" />, a <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> that contains the sort descriptions applied to the list; otherwise, null.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public virtual ListSortDescriptionCollection SortDescriptions
	{
		get
		{
			if (list is IBindingListView)
			{
				return ((IBindingListView)list).SortDescriptions;
			}
			return null;
		}
	}

	/// <summary>Gets the direction the items in the list are sorted.</summary>
	/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values indicating the direction the list is sorted.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual ListSortDirection SortDirection
	{
		get
		{
			if (list is IBindingList)
			{
				return ((IBindingList)list).SortDirection;
			}
			return ListSortDirection.Ascending;
		}
	}

	/// <summary>Gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting the list.</summary>
	/// <returns>If the list is an <see cref="T:System.ComponentModel.IBindingList" />, the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting; otherwise, null.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public virtual PropertyDescriptor SortProperty
	{
		get
		{
			if (list is IBindingList)
			{
				return ((IBindingList)list).SortProperty;
			}
			return null;
		}
	}

	/// <summary>Gets a value indicating whether the data source supports multi-column sorting.</summary>
	/// <returns>true if the list is an <see cref="T:System.ComponentModel.IBindingListView" /> and supports multi-column sorting; otherwise, false. </returns>
	[Browsable(false)]
	public virtual bool SupportsAdvancedSorting => list is IBindingListView && ((IBindingListView)list).SupportsAdvancedSorting;

	/// <summary>Gets a value indicating whether the data source supports change notification.</summary>
	/// <returns>true in all cases.</returns>
	[Browsable(false)]
	public virtual bool SupportsChangeNotification => true;

	/// <summary>Gets a value indicating whether the data source supports filtering.</summary>
	/// <returns>true if the list is an <see cref="T:System.ComponentModel.IBindingListView" /> and supports filtering; otherwise, false.</returns>
	[Browsable(false)]
	public virtual bool SupportsFiltering => list is IBindingListView && ((IBindingListView)list).SupportsFiltering;

	/// <summary>Gets a value indicating whether the data source supports searching with the <see cref="M:System.Windows.Forms.BindingSource.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method.</summary>
	/// <returns>true if the list is a <see cref="T:System.ComponentModel.IBindingList" /> and supports the searching with the <see cref="Overload:System.Windows.Forms.BindingSource.Find" /> method; otherwise, false.</returns>
	[Browsable(false)]
	public virtual bool SupportsSearching => list is IBindingList && ((IBindingList)list).SupportsSearching;

	/// <summary>Gets a value indicating whether the data source supports sorting.</summary>
	/// <returns>true if the data source is an <see cref="T:System.ComponentModel.IBindingList" /> and supports sorting; otherwise, false.</returns>
	[Browsable(false)]
	public virtual bool SupportsSorting => list is IBindingList && ((IBindingList)list).SupportsSorting;

	/// <summary>Gets an object that can be used to synchronize access to the underlying list.</summary>
	/// <returns>An object that can be used to synchronize access to the underlying list.</returns>
	[Browsable(false)]
	public virtual object SyncRoot => list.SyncRoot;

	/// <summary>Occurs before an item is added to the underlying list.</summary>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.ComponentModel.AddingNewEventArgs.NewObject" /> is not the same type as the type contained in the list.</exception>
	public event AddingNewEventHandler AddingNew
	{
		add
		{
			base.Events.AddHandler(AddingNewEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(AddingNewEvent, value);
		}
	}

	/// <summary>Occurs when all the clients have been bound to this <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
	public event BindingCompleteEventHandler BindingComplete
	{
		add
		{
			base.Events.AddHandler(BindingCompleteEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(BindingCompleteEvent, value);
		}
	}

	/// <summary>Occurs when the currently bound item changes.</summary>
	public event EventHandler CurrentChanged
	{
		add
		{
			base.Events.AddHandler(CurrentChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(CurrentChangedEvent, value);
		}
	}

	/// <summary>Occurs when a property value of the <see cref="P:System.Windows.Forms.BindingSource.Current" /> property has changed.</summary>
	public event EventHandler CurrentItemChanged
	{
		add
		{
			base.Events.AddHandler(CurrentItemChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(CurrentItemChangedEvent, value);
		}
	}

	/// <summary>Occurs when a currency-related exception is silently handled by the <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
	public event BindingManagerDataErrorEventHandler DataError
	{
		add
		{
			base.Events.AddHandler(DataErrorEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DataErrorEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.BindingSource.DataMember" /> property value has changed.</summary>
	public event EventHandler DataMemberChanged
	{
		add
		{
			base.Events.AddHandler(DataMemberChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DataMemberChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.BindingSource.DataSource" /> property value has changed.</summary>
	public event EventHandler DataSourceChanged
	{
		add
		{
			base.Events.AddHandler(DataSourceChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DataSourceChangedEvent, value);
		}
	}

	/// <summary>Occurs when the underlying list changes or an item in the list changes.</summary>
	public event ListChangedEventHandler ListChanged
	{
		add
		{
			base.Events.AddHandler(ListChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ListChangedEvent, value);
		}
	}

	/// <summary>Occurs after the value of the <see cref="P:System.Windows.Forms.BindingSource.Position" /> property has changed.</summary>
	public event EventHandler PositionChanged
	{
		add
		{
			base.Events.AddHandler(PositionChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(PositionChangedEvent, value);
		}
	}

	event EventHandler ISupportInitializeNotification.Initialized
	{
		add
		{
			base.Events.AddHandler(InitializedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(InitializedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingSource" /> class and adds the <see cref="T:System.Windows.Forms.BindingSource" /> to the specified container.</summary>
	/// <param name="container">The <see cref="T:System.ComponentModel.IContainer" /> to add the current <see cref="T:System.Windows.Forms.BindingSource" /> to.</param>
	public BindingSource(IContainer container)
		: this()
	{
		container.Add(this);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingSource" /> class with the specified data source and data member.</summary>
	/// <param name="dataSource">The data source for the <see cref="T:System.Windows.Forms.BindingSource" />.</param>
	/// <param name="dataMember">The specific column or list name within the data source to bind to.</param>
	public BindingSource(object dataSource, string dataMember)
	{
		datasource = dataSource;
		datamember = dataMember;
		raise_list_changed_events = true;
		ResetList();
		ConnectCurrencyManager();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingSource" /> class to the default property values.</summary>
	public BindingSource()
		: this(null, string.Empty)
	{
	}

	static BindingSource()
	{
		AddingNew = new object();
		BindingComplete = new object();
		CurrentChanged = new object();
		CurrentItemChanged = new object();
		DataError = new object();
		DataMemberChanged = new object();
		DataSourceChanged = new object();
		ListChanged = new object();
		PositionChanged = new object();
		InitializedEvent = new object();
	}

	/// <summary>Discards a pending new item from the collection.</summary>
	/// <param name="position">The index of the item that was added to the collection. </param>
	void ICancelAddNew.CancelNew(int position)
	{
		if (add_pending && position == pending_add_index)
		{
			add_pending = false;
			list.RemoveAt(position);
			if (raise_list_changed_events && !list_is_ibinding)
			{
				OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, position));
			}
		}
	}

	/// <summary>Commits a pending new item to the collection.</summary>
	/// <param name="position">The index of the item that was added to the collection. </param>
	void ICancelAddNew.EndNew(int position)
	{
		if (add_pending && position == pending_add_index)
		{
			add_pending = false;
		}
	}

	/// <summary>Signals the <see cref="T:System.Windows.Forms.BindingSource" /> that initialization is starting.</summary>
	void ISupportInitialize.BeginInit()
	{
		is_initialized = false;
	}

	/// <summary>Signals the <see cref="T:System.Windows.Forms.BindingSource" /> that initialization is complete. </summary>
	void ISupportInitialize.EndInit()
	{
		if (datasource != null && datasource is ISupportInitializeNotification)
		{
			ISupportInitializeNotification supportInitializeNotification = (ISupportInitializeNotification)datasource;
			if (!supportInitializeNotification.IsInitialized)
			{
				supportInitializeNotification.Initialized += DataSourceEndInitHandler;
				return;
			}
		}
		is_initialized = true;
		ResetList();
		((EventHandler)base.Events[InitializedEvent])?.Invoke(this, EventArgs.Empty);
	}

	/// <summary>Adds the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the indexes used for searching.</summary>
	/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the indexes used for searching. </param>
	/// <exception cref="T:System.NotSupportedException">The underlying list is not an <see cref="T:System.ComponentModel.IBindingList" />.</exception>
	void IBindingList.AddIndex(PropertyDescriptor property)
	{
		if (!(list is IBindingList))
		{
			throw new NotSupportedException();
		}
		((IBindingList)list).AddIndex(property);
	}

	/// <summary>Removes the <see cref="T:System.ComponentModel.PropertyDescriptor" /> from the indexes used for searching.</summary>
	/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the indexes used for searching.  </param>
	void IBindingList.RemoveIndex(PropertyDescriptor prop)
	{
		if (!(list is IBindingList))
		{
			throw new NotSupportedException();
		}
		((IBindingList)list).RemoveIndex(prop);
	}

	private IList GetListFromEnumerable(IEnumerable enumerable)
	{
		IList list = null;
		IEnumerator enumerator = enumerable.GetEnumerator();
		if (enumerable is string)
		{
			list = new BindingList<char>();
		}
		else
		{
			object obj = null;
			if (enumerator.MoveNext())
			{
				obj = enumerator.Current;
			}
			if (obj == null)
			{
				return null;
			}
			Type type = typeof(BindingList<>).MakeGenericType(obj.GetType());
			list = (IList)Activator.CreateInstance(type);
		}
		enumerator.Reset();
		while (enumerator.MoveNext())
		{
			list.Add(enumerator.Current);
		}
		return list;
	}

	private void ConnectCurrencyManager()
	{
		currency_manager = new CurrencyManager(this);
		currency_manager.PositionChanged += delegate(object o, EventArgs args)
		{
			OnPositionChanged(args);
		};
		currency_manager.CurrentChanged += delegate(object o, EventArgs args)
		{
			OnCurrentChanged(args);
		};
		currency_manager.BindingComplete += delegate(object o, BindingCompleteEventArgs args)
		{
			OnBindingComplete(args);
		};
		currency_manager.DataError += delegate(object o, BindingManagerDataErrorEventArgs args)
		{
			OnDataError(args);
		};
		currency_manager.CurrentChanged += delegate(object o, EventArgs args)
		{
			OnCurrentChanged(args);
		};
		currency_manager.CurrentItemChanged += delegate(object o, EventArgs args)
		{
			OnCurrentItemChanged(args);
		};
	}

	private void ResetList()
	{
		if (!is_initialized)
		{
			return;
		}
		object obj = ListBindingHelper.GetList(datasource, datamember);
		IList list;
		if (datasource == null)
		{
			list = new BindingList<object>();
		}
		else if (obj == null)
		{
			Type propertyType = ListBindingHelper.GetListItemProperties(datasource)[datamember].PropertyType;
			Type type = typeof(BindingList<>).MakeGenericType(propertyType);
			list = (IList)Activator.CreateInstance(type);
		}
		else if (obj is IList)
		{
			list = (IList)obj;
		}
		else if (obj is IEnumerable)
		{
			IList listFromEnumerable = GetListFromEnumerable((IEnumerable)obj);
			IList obj2;
			if (listFromEnumerable == null)
			{
				IList list2 = this.list;
				obj2 = list2;
			}
			else
			{
				obj2 = listFromEnumerable;
			}
			list = obj2;
		}
		else if (obj is Type)
		{
			Type type2 = typeof(BindingList<>).MakeGenericType((Type)obj);
			list = (IList)Activator.CreateInstance(type2);
		}
		else
		{
			Type type3 = typeof(BindingList<>).MakeGenericType(obj.GetType());
			list = (IList)Activator.CreateInstance(type3);
			list.Add(obj);
		}
		SetList(list);
	}

	private void SetList(IList l)
	{
		if (list is IBindingList)
		{
			((IBindingList)list).ListChanged -= IBindingListChangedHandler;
		}
		list = l;
		item_type = ListBindingHelper.GetListItemType(list);
		item_has_default_ctor = (object)item_type.GetConstructor(Type.EmptyTypes) != null;
		list_is_ibinding = list is IBindingList;
		if (list_is_ibinding)
		{
			((IBindingList)list).ListChanged += IBindingListChangedHandler;
			if (list is IBindingListView)
			{
				((IBindingListView)list).Filter = filter;
			}
		}
		ResetBindings(metadataChanged: true);
	}

	private void ConnectDataSourceEvents(object dataSource)
	{
		if (dataSource != null && dataSource is ICurrencyManagerProvider { CurrencyManager: not null } currencyManagerProvider)
		{
			currencyManagerProvider.CurrencyManager.CurrentItemChanged += OnParentCurrencyManagerChanged;
			currencyManagerProvider.CurrencyManager.MetaDataChanged += OnParentCurrencyManagerChanged;
		}
	}

	private void OnParentCurrencyManagerChanged(object sender, EventArgs args)
	{
		ResetList();
	}

	private void DisconnectDataSourceEvents(object dataSource)
	{
		if (dataSource != null && dataSource is ICurrencyManagerProvider { CurrencyManager: not null } currencyManagerProvider)
		{
			currencyManagerProvider.CurrencyManager.CurrentItemChanged -= OnParentCurrencyManagerChanged;
			currencyManagerProvider.CurrencyManager.MetaDataChanged -= OnParentCurrencyManagerChanged;
		}
	}

	private void IBindingListChangedHandler(object o, ListChangedEventArgs args)
	{
		if (raise_list_changed_events)
		{
			OnListChanged(args);
		}
	}

	private void ProcessSortString(string sort)
	{
		sort = Regex.Replace(sort, "( )+", " ");
		string[] array = sort.Split(',');
		PropertyDescriptorCollection itemProperties = GetItemProperties(null);
		if (array.Length == 1)
		{
			ListSortDescription listSortDescription = GetListSortDescription(itemProperties, array[0]);
			ApplySort(listSortDescription.PropertyDescriptor, listSortDescription.SortDirection);
			return;
		}
		if (!SupportsAdvancedSorting)
		{
			throw new ArgumentException("value");
		}
		ListSortDescription[] array2 = new ListSortDescription[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = GetListSortDescription(itemProperties, array[i]);
		}
		ApplySort(new ListSortDescriptionCollection(array2));
	}

	private ListSortDescription GetListSortDescription(PropertyDescriptorCollection prop_descs, string property)
	{
		property = property.Trim();
		string[] array = property.Split(new char[1] { ' ' }, 2);
		string name = array[0];
		PropertyDescriptor propertyDescriptor = prop_descs[name];
		if (propertyDescriptor == null)
		{
			throw new ArgumentException("value");
		}
		ListSortDirection direction = ListSortDirection.Ascending;
		if (array.Length > 1)
		{
			string strA = array[1];
			if (string.Compare(strA, "ASC", ignoreCase: true) == 0)
			{
				direction = ListSortDirection.Ascending;
			}
			else
			{
				if (string.Compare(strA, "DESC", ignoreCase: true) != 0)
				{
					throw new ArgumentException("value");
				}
				direction = ListSortDirection.Descending;
			}
		}
		return new ListSortDescription(propertyDescriptor, direction);
	}

	/// <summary>Adds an existing item to the internal list.</summary>
	/// <returns>The zero-based index at which <paramref name="value" /> was added to the underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property. </returns>
	/// <param name="value">An <see cref="T:System.Object" /> to be added to the internal list.</param>
	/// <exception cref="T:System.InvalidOperationException">
	///   <paramref name="value" /> differs in type from the existing items in the underlying list.</exception>
	public virtual int Add(object value)
	{
		if (datasource == null && this.list.Count == 0 && value != null)
		{
			Type type = typeof(BindingList<>).MakeGenericType(value.GetType());
			IList list = (IList)Activator.CreateInstance(type);
			SetList(list);
		}
		if (value != null && !item_type.IsAssignableFrom(value.GetType()))
		{
			throw new InvalidOperationException("Objects added to the list must all be of the same type.");
		}
		if (this.list.IsReadOnly)
		{
			throw new NotSupportedException("Collection is read-only.");
		}
		if (this.list.IsFixedSize)
		{
			throw new NotSupportedException("Collection has a fixed size.");
		}
		int num = this.list.Add(value);
		if (raise_list_changed_events && !list_is_ibinding)
		{
			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, num));
		}
		return num;
	}

	/// <summary>Adds a new item to the underlying list.</summary>
	/// <returns>The <see cref="T:System.Object" /> that was created and added to the list.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.BindingSource.AllowNew" /> property is set to false. -or-A public default constructor could not be found for the current item type.</exception>
	public virtual object AddNew()
	{
		if (!AllowEdit)
		{
			throw new InvalidOperationException("Item cannot be added to a read-only or fixed-size list.");
		}
		if (!AllowNew)
		{
			throw new InvalidOperationException("AddNew is set to false.");
		}
		EndEdit();
		AddingNewEventArgs addingNewEventArgs = new AddingNewEventArgs();
		OnAddingNew(addingNewEventArgs);
		object obj = addingNewEventArgs.NewObject;
		if (obj != null)
		{
			if (!item_type.IsAssignableFrom(obj.GetType()))
			{
				throw new InvalidOperationException("Objects added to the list must all be of the same type.");
			}
		}
		else
		{
			if (list is IBindingList)
			{
				object obj2 = ((IBindingList)list).AddNew();
				add_pending = true;
				pending_add_index = list.IndexOf(obj2);
				return obj2;
			}
			if (!item_has_default_ctor)
			{
				throw new InvalidOperationException("AddNew cannot be called on '" + item_type.Name + ", since it does not have a public default ctor. Set AllowNew to true , handling AddingNew and creating the appropriate object.");
			}
			obj = Activator.CreateInstance(item_type);
		}
		int newIndex = list.Add(obj);
		if (raise_list_changed_events && !list_is_ibinding)
		{
			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, newIndex));
		}
		add_pending = true;
		pending_add_index = newIndex;
		return obj;
	}

	/// <summary>Sorts the data source using the specified property descriptor and sort direction.</summary>
	/// <param name="property">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the property by which to sort the data source.</param>
	/// <param name="sort">A <see cref="T:System.ComponentModel.ListSortDirection" /> indicating how the list should be sorted.</param>
	/// <exception cref="T:System.NotSupportedException">The data source is not an <see cref="T:System.ComponentModel.IBindingList" />.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ApplySort(PropertyDescriptor property, ListSortDirection sort)
	{
		if (!list_is_ibinding)
		{
			throw new NotSupportedException("This operation requires an IBindingList.");
		}
		IBindingList bindingList = (IBindingList)list;
		bindingList.ApplySort(property, sort);
	}

	/// <summary>Sorts the data source with the specified sort descriptions.</summary>
	/// <param name="sorts">A <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> containing the sort descriptions to apply to the data source.</param>
	/// <exception cref="T:System.NotSupportedException">The data source is not an <see cref="T:System.ComponentModel.IBindingListView" />.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ApplySort(ListSortDescriptionCollection sorts)
	{
		if (!(list is IBindingListView))
		{
			throw new NotSupportedException("This operation requires an IBindingListView.");
		}
		IBindingListView bindingListView = (IBindingListView)list;
		bindingListView.ApplySort(sorts);
	}

	/// <summary>Cancels the current edit operation.</summary>
	public void CancelEdit()
	{
		currency_manager.CancelCurrentEdit();
	}

	/// <summary>Removes all elements from the list.</summary>
	public virtual void Clear()
	{
		if (list.IsReadOnly)
		{
			throw new NotSupportedException("Collection is read-only.");
		}
		list.Clear();
		if (raise_list_changed_events && !list_is_ibinding)
		{
			OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
		}
	}

	/// <summary>Determines whether an object is an item in the list.</summary>
	/// <returns>true if the <paramref name="value" /> parameter is found in the <see cref="P:System.Windows.Forms.BindingSource.List" />; otherwise, false.</returns>
	/// <param name="value">The <see cref="T:System.Object" /> to locate in the underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property. The value can be null. </param>
	public virtual bool Contains(object value)
	{
		return list.Contains(value);
	}

	/// <summary>Copies the contents of the <see cref="P:System.Windows.Forms.BindingSource.List" /> to the specified array, starting at the specified index value.</summary>
	/// <param name="arr">The destination array.</param>
	/// <param name="index">The index in the destination array at which to start the copy operation.</param>
	public virtual void CopyTo(Array arr, int index)
	{
		list.CopyTo(arr, index);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.BindingSource" /> and optionally releases the managed resources. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	/// <summary>Applies pending changes to the underlying data source.</summary>
	public void EndEdit()
	{
		currency_manager.EndCurrentEdit();
	}

	/// <summary>Returns the index of the item in the list with the specified property name and value.</summary>
	/// <returns>The zero-based index of the item with the specified property name and value. </returns>
	/// <param name="propertyName">The name of the property to search for.</param>
	/// <param name="key">The value of the item with the specified <paramref name="propertyName" /> to find.</param>
	/// <exception cref="T:System.InvalidOperationException">The underlying list is not a <see cref="T:System.ComponentModel.IBindingList" /> with searching functionality implemented.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="propertyName" /> does not match a property in the list.</exception>
	public int Find(string propertyName, object key)
	{
		PropertyDescriptor propertyDescriptor = GetItemProperties(null).Find(propertyName, ignoreCase: true);
		if (propertyDescriptor == null)
		{
			throw new ArgumentException("propertyName");
		}
		return Find(propertyDescriptor, key);
	}

	/// <summary>Searches for the index of the item that has the given property descriptor.</summary>
	/// <returns>The zero-based index of the item that has the given value for <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
	/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search for. </param>
	/// <param name="key">The value of <paramref name="prop" /> to match. </param>
	/// <exception cref="T:System.NotSupportedException">The underlying list is not of type <see cref="T:System.ComponentModel.IBindingList" />.</exception>
	public virtual int Find(PropertyDescriptor prop, object key)
	{
		if (!list_is_ibinding)
		{
			throw new NotSupportedException();
		}
		return ((IBindingList)list).Find(prop, key);
	}

	/// <summary>Retrieves an enumerator for the <see cref="P:System.Windows.Forms.BindingSource.List" />.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="P:System.Windows.Forms.BindingSource.List" />. </returns>
	public virtual IEnumerator GetEnumerator()
	{
		return List.GetEnumerator();
	}

	/// <summary>Retrieves an array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects representing the bindable properties of the data source list type.</summary>
	/// <returns>An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that represents the properties on this list type used to bind data.</returns>
	/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects to find in the list as bindable.</param>
	public virtual PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
	{
		return ListBindingHelper.GetListItemProperties(list, listAccessors);
	}

	/// <summary>Gets the name of the list supplying data for the binding.</summary>
	/// <returns>The name of the list supplying the data for binding.</returns>
	/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects to find in the list as bindable.</param>
	public virtual string GetListName(PropertyDescriptor[] listAccessors)
	{
		return ListBindingHelper.GetListName(list, listAccessors);
	}

	/// <summary>Gets the related currency manager for the specified data member.</summary>
	/// <returns>The related <see cref="T:System.Windows.Forms.CurrencyManager" /> for the specified data member.</returns>
	/// <param name="dataMember">The name of column or list, within the data source to retrieve the currency manager for.</param>
	public virtual CurrencyManager GetRelatedCurrencyManager(string dataMember)
	{
		if (dataMember == null || dataMember.Length == 0)
		{
			return currency_manager;
		}
		if (related_currency_managers.ContainsKey(dataMember))
		{
			return related_currency_managers[dataMember];
		}
		if (dataMember.IndexOf('.') != -1)
		{
			return null;
		}
		BindingSource bindingSource = new BindingSource(this, dataMember);
		related_currency_managers[dataMember] = bindingSource.CurrencyManager;
		return bindingSource.CurrencyManager;
	}

	/// <summary>Searches for the specified object and returns the index of the first occurrence within the entire list.</summary>
	/// <returns>The zero-based index of the first occurrence of the <paramref name="value" /> parameter; otherwise, -1 if <paramref name="value" /> is not in the list.</returns>
	/// <param name="value">The <see cref="T:System.Object" /> to locate in the underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property. The value can be null. </param>
	public virtual int IndexOf(object value)
	{
		return list.IndexOf(value);
	}

	/// <summary>Inserts an item into the list at the specified index.</summary>
	/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted. </param>
	/// <param name="value">The <see cref="T:System.Object" /> to insert. The value can be null. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero or greater than <see cref="P:System.Windows.Forms.BindingSource.Count" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The list is read-only or has a fixed size.</exception>
	public virtual void Insert(int index, object value)
	{
		if (index < 0 || index > list.Count)
		{
			throw new ArgumentOutOfRangeException("index");
		}
		if (list.IsReadOnly || list.IsFixedSize)
		{
			throw new NotSupportedException();
		}
		if (!item_type.IsAssignableFrom(value.GetType()))
		{
			throw new ArgumentException("value");
		}
		list.Insert(index, value);
		if (raise_list_changed_events && !list_is_ibinding)
		{
			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
		}
	}

	/// <summary>Moves to the first item in the list.</summary>
	public void MoveFirst()
	{
		Position = 0;
	}

	/// <summary>Moves to the last item in the list.</summary>
	public void MoveLast()
	{
		Position = Count - 1;
	}

	/// <summary>Moves to the next item in the list.</summary>
	public void MoveNext()
	{
		Position++;
	}

	/// <summary>Moves to the previous item in the list.</summary>
	public void MovePrevious()
	{
		Position--;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.AddingNew" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnAddingNew(AddingNewEventArgs e)
	{
		((AddingNewEventHandler)base.Events[AddingNew])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.BindingComplete" /> event. </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" />  that contains the event data. </param>
	protected virtual void OnBindingComplete(BindingCompleteEventArgs e)
	{
		((BindingCompleteEventHandler)base.Events[BindingComplete])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.CurrentChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnCurrentChanged(EventArgs e)
	{
		((EventHandler)base.Events[CurrentChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.CurrentItemChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnCurrentItemChanged(EventArgs e)
	{
		((EventHandler)base.Events[CurrentItemChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.DataError" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.BindingManagerDataErrorEventArgs" /> that contains the event data. </param>
	protected virtual void OnDataError(BindingManagerDataErrorEventArgs e)
	{
		((BindingManagerDataErrorEventHandler)base.Events[DataError])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.DataMemberChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnDataMemberChanged(EventArgs e)
	{
		((EventHandler)base.Events[DataMemberChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.DataSourceChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnDataSourceChanged(EventArgs e)
	{
		((EventHandler)base.Events[DataSourceChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.ListChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnListChanged(ListChangedEventArgs e)
	{
		((ListChangedEventHandler)base.Events[ListChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.PositionChanged" /> event.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data.</param>
	protected virtual void OnPositionChanged(EventArgs e)
	{
		((EventHandler)base.Events[PositionChanged])?.Invoke(this, e);
	}

	/// <summary>Removes the specified item from the list.</summary>
	/// <param name="value">The item to remove from the underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property.</param>
	/// <exception cref="T:System.NotSupportedException">The underlying list has a fixed size or is read-only. </exception>
	public virtual void Remove(object value)
	{
		if (list.IsReadOnly)
		{
			throw new NotSupportedException("Collection is read-only.");
		}
		if (list.IsFixedSize)
		{
			throw new NotSupportedException("Collection has a fixed size.");
		}
		int num = ((!list_is_ibinding) ? list.IndexOf(value) : (-1));
		list.Remove(value);
		if (num != -1 && raise_list_changed_events)
		{
			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, num));
		}
	}

	/// <summary>Removes the item at the specified index in the list.</summary>
	/// <param name="index">The zero-based index of the item to remove. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero or greater than the value of the <see cref="P:System.Windows.Forms.BindingSource.Count" /> property.</exception>
	/// <exception cref="T:System.NotSupportedException">The underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property is read-only or has a fixed size.</exception>
	public virtual void RemoveAt(int index)
	{
		if (index < 0 || index > list.Count)
		{
			throw new ArgumentOutOfRangeException("index");
		}
		if (list.IsReadOnly || list.IsFixedSize)
		{
			throw new InvalidOperationException();
		}
		list.RemoveAt(index);
		if (raise_list_changed_events && !list_is_ibinding)
		{
			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
		}
	}

	/// <summary>Removes the current item from the list.</summary>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.BindingSource.AllowRemove" /> property is false.-or-<see cref="P:System.Windows.Forms.BindingSource.Position" /> is less than zero or greater than <see cref="P:System.Windows.Forms.BindingSource.Count" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property is read-only or has a fixed size.</exception>
	public void RemoveCurrent()
	{
		if (Position < 0)
		{
			throw new InvalidOperationException("Cannot remove item because there is no current item.");
		}
		if (!AllowRemove)
		{
			throw new InvalidOperationException("Cannot remove item because list does not allow removal of items.");
		}
		RemoveAt(Position);
	}

	/// <summary>Removes the filter associated with the <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
	/// <exception cref="T:System.NotSupportedException">The underlying list does not support filtering.</exception>
	public virtual void RemoveFilter()
	{
		Filter = null;
	}

	/// <summary>Removes the sort associated with the <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
	/// <exception cref="T:System.NotSupportedException">The underlying list does not support sorting.</exception>
	public virtual void RemoveSort()
	{
		if (list_is_ibinding)
		{
			sort = null;
			((IBindingList)list).RemoveSort();
		}
	}

	/// <summary>Reinitializes the <see cref="P:System.Windows.Forms.BindingSource.AllowNew" /> property.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual void ResetAllowNew()
	{
		allow_new_set = false;
	}

	/// <summary>Causes a control bound to the <see cref="T:System.Windows.Forms.BindingSource" /> to reread all the items in the list and refresh their displayed values. </summary>
	/// <param name="metadataChanged">true if the data schema has changed; false if only values have changed.</param>
	public void ResetBindings(bool metadataChanged)
	{
		if (metadataChanged)
		{
			OnListChanged(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
		}
		OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1, -1));
	}

	/// <summary>Causes a control bound to the <see cref="T:System.Windows.Forms.BindingSource" /> to reread the currently selected item and refresh its displayed value.</summary>
	public void ResetCurrentItem()
	{
		OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, Position, -1));
	}

	/// <summary>Causes a control bound to the <see cref="T:System.Windows.Forms.BindingSource" /> to reread the item at the specified index, and refresh its displayed value. </summary>
	/// <param name="itemIndex">The zero-based index of the item that has changed.</param>
	public void ResetItem(int itemIndex)
	{
		OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, itemIndex, -1));
	}

	/// <summary>Resumes data binding.</summary>
	public void ResumeBinding()
	{
		currency_manager.ResumeBinding();
	}

	/// <summary>Suspends data binding to prevent changes from updating the bound data source.</summary>
	public void SuspendBinding()
	{
		currency_manager.SuspendBinding();
	}

	private void DataSourceEndInitHandler(object o, EventArgs args)
	{
		((ISupportInitializeNotification)datasource).Initialized -= DataSourceEndInitHandler;
		((ISupportInitialize)this).EndInit();
	}
}
