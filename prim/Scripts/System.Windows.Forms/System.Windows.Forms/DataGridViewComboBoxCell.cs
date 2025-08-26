using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Displays a combo box in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewComboBoxCell : DataGridViewCell
{
	/// <summary>Represents the collection of selection choices in a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</summary>
	[ListBindable(false)]
	public class ObjectCollection : ICollection, IEnumerable, IList
	{
		private ArrayList list;

		private DataGridViewComboBoxCell owner;

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		bool IList.IsFixedSize => list.IsFixedSize;

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		bool ICollection.IsSynchronized => list.IsSynchronized;

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" />.</returns>
		object ICollection.SyncRoot => list.SyncRoot;

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		public int Count => list.Count;

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		public bool IsReadOnly => list.IsReadOnly;

		/// <summary>Gets or sets the item at the current index location. In C#, this property is the indexer for the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> class.</summary>
		/// <returns>The <see cref="T:System.Object" /> stored at the given index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than the number of items in the collection minus one. </exception>
		/// <exception cref="T:System.ArgumentNullException">The specified value when setting this property is null.</exception>
		/// <exception cref="T:System.ArgumentException">When setting this property, the cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not null.</exception>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, the cell is in a shared row.</exception>
		public virtual object this[int index]
		{
			get
			{
				return list[index];
			}
			set
			{
				ThrowIfOwnerIsDataBound();
				list[index] = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> that owns the collection.</param>
		public ObjectCollection(DataGridViewComboBoxCell owner)
		{
			this.owner = owner;
			list = new ArrayList();
		}

		/// <summary>Copies the elements of the collection to the specified array, starting at the specified index.</summary>
		/// <param name="destination">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or equal to or greater than the length of <paramref name="destination" />.-or-The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="destination" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destination" /> is multidimensional.</exception>
		void ICollection.CopyTo(Array destination, int index)
		{
			CopyTo((object[])destination, index);
		}

		/// <summary>Adds an object to the collection.</summary>
		/// <returns>The position in which to insert the new element.</returns>
		/// <param name="item">The object to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="item" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
		int IList.Add(object item)
		{
			return Add(item);
		}

		/// <summary>Adds an item to the list of items for a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="item">An object representing the item to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="item" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
		public int Add(object item)
		{
			ThrowIfOwnerIsDataBound();
			int result = AddInternal(item);
			SyncOwnerItems();
			return result;
		}

		internal int AddInternal(object item)
		{
			return list.Add(item);
		}

		internal void AddRangeInternal(ICollection items)
		{
			list.AddRange(items);
		}

		/// <summary>Adds the items of an existing <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> to the list of items in a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> to load into this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more of the items in the <paramref name="value" /> collection is null.</exception>
		/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
		public void AddRange(ObjectCollection value)
		{
			ThrowIfOwnerIsDataBound();
			AddRangeInternal(value);
			SyncOwnerItems();
		}

		private void SyncOwnerItems()
		{
			ThrowIfOwnerIsDataBound();
			if (owner != null)
			{
				owner.SyncItems();
			}
		}

		public void ThrowIfOwnerIsDataBound()
		{
			if (owner != null && owner.DataGridView != null && owner.DataSource != null)
			{
				throw new ArgumentException("Cannot modify collection if the cell is data bound.");
			}
		}

		/// <summary>Adds one or more items to the list of items for a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</summary>
		/// <param name="items">One or more objects that represent items for the drop-down list.-or-An <see cref="T:System.Array" /> of <see cref="T:System.Object" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="items" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more of the items in the <paramref name="items" /> array is null.</exception>
		/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
		public void AddRange(params object[] items)
		{
			ThrowIfOwnerIsDataBound();
			AddRangeInternal(items);
			SyncOwnerItems();
		}

		/// <summary>Clears all items from the collection.</summary>
		/// <exception cref="T:System.ArgumentException">The collection contains at least one item and the cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The collection contains at least one item and the cell is in a shared row.</exception>
		public void Clear()
		{
			ThrowIfOwnerIsDataBound();
			ClearInternal();
			SyncOwnerItems();
		}

		internal void ClearInternal()
		{
			list.Clear();
		}

		/// <summary>Determines whether the specified item is contained in the collection.</summary>
		/// <returns>true if the <paramref name="item" /> is in the collection; otherwise, false.</returns>
		/// <param name="value">An object representing the item to locate in the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		public bool Contains(object value)
		{
			return list.Contains(value);
		}

		/// <summary>Copies the entire collection into an existing array of objects at a specified location within the array.</summary>
		/// <param name="destination">The destination array to which the contents will be copied.</param>
		/// <param name="arrayIndex">The index of the element in <paramref name="dest" /> at which to start copying.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than 0 or equal to or greater than the length of <paramref name="destination" />.-or-The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of <paramref name="destination" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destination" /> is multidimensional.</exception>
		public void CopyTo(object[] destination, int arrayIndex)
		{
			list.CopyTo(destination, arrayIndex);
		}

		/// <summary>Returns an enumerator that can iterate through a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" />.</summary>
		/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
		public IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
		}

		/// <summary>Returns the index of the specified item in the collection.</summary>
		/// <returns>The zero-based index of the <paramref name="value" /> parameter if it is found in the collection; otherwise, -1.</returns>
		/// <param name="value">An object representing the item to locate in the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		public int IndexOf(object value)
		{
			return list.IndexOf(value);
		}

		/// <summary>Inserts an item into the collection at the specified index. </summary>
		/// <param name="index">The zero-based index at which to place <paramref name="item" /> within an unsorted <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</param>
		/// <param name="item">An object representing the item to insert.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="item" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than the number of items in the collection. </exception>
		/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
		public void Insert(int index, object item)
		{
			ThrowIfOwnerIsDataBound();
			InsertInternal(index, item);
			SyncOwnerItems();
		}

		internal void InsertInternal(int index, object item)
		{
			list.Insert(index, item);
		}

		/// <summary>Removes the specified object from the collection.</summary>
		/// <param name="value">An object representing the item to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
		public void Remove(object value)
		{
			ThrowIfOwnerIsDataBound();
			RemoveInternal(value);
			SyncOwnerItems();
		}

		internal void RemoveInternal(object value)
		{
			list.Remove(value);
		}

		/// <summary>Removes the object at the specified index.</summary>
		/// <param name="index">The zero-based index of the object to be removed.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than the number of items in the collection minus one. </exception>
		/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
		public void RemoveAt(int index)
		{
			ThrowIfOwnerIsDataBound();
			RemoveAtInternal(index);
			SyncOwnerItems();
		}

		internal void RemoveAtInternal(int index)
		{
			list.RemoveAt(index);
		}
	}

	private bool autoComplete;

	private object dataSource;

	private string displayMember;

	private DataGridViewComboBoxDisplayStyle displayStyle;

	private bool displayStyleForCurrentCellOnly;

	private int dropDownWidth;

	private FlatStyle flatStyle;

	private ObjectCollection items;

	private int maxDropDownItems;

	private bool sorted;

	private string valueMember;

	private DataGridViewComboBoxColumn owningColumnTemlate;

	/// <summary>Gets or sets a value indicating whether the cell will match the characters being entered in the cell with a selection from the drop-down list. </summary>
	/// <returns>true if automatic completion is activated; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	public virtual bool AutoComplete
	{
		get
		{
			return autoComplete;
		}
		set
		{
			autoComplete = value;
		}
	}

	/// <summary>Gets or sets the data source whose data contains the possible selections shown in the drop-down list.</summary>
	/// <returns>An <see cref="T:System.Collections.IList" /> or <see cref="T:System.ComponentModel.IListSource" /> that contains a collection of values used to supply data to the drop-down list. The default value is null.</returns>
	/// <exception cref="T:System.ArgumentException">The specified value when setting this property is not null and is not of type <see cref="T:System.Collections.IList" /> nor <see cref="T:System.ComponentModel.IListSource" />.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual object DataSource
	{
		get
		{
			return dataSource;
		}
		set
		{
			if (value is IList || value is IListSource || value == null)
			{
				dataSource = value;
				return;
			}
			throw new Exception("Value is no IList, IListSource or null.");
		}
	}

	/// <summary>Gets or sets a string that specifies where to gather selections to display in the drop-down list.</summary>
	/// <returns>A string specifying the name of a property or column in the data source specified in the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property. The default value is <see cref="F:System.String.Empty" />, which indicates that the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DisplayMember" /> property will not be used.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property is not null and the specified value when setting this property is not null or <see cref="F:System.String.Empty" /> and does not name a valid property or column in the data source.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue("")]
	public virtual string DisplayMember
	{
		get
		{
			return displayMember;
		}
		set
		{
			displayMember = value;
		}
	}

	/// <summary>Gets or sets a value that determines how the combo box is displayed when it is not in edit mode.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewComboBoxDisplayStyle" /> values. The default is <see cref="F:System.Windows.Forms.DataGridViewComboBoxDisplayStyle.DropDownButton" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewComboBoxDisplayStyle" /> value.</exception>
	[DefaultValue(DataGridViewComboBoxDisplayStyle.DropDownButton)]
	public DataGridViewComboBoxDisplayStyle DisplayStyle
	{
		get
		{
			return displayStyle;
		}
		set
		{
			displayStyle = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DisplayStyle" /> property value applies to the cell only when it is the current cell in the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	/// <returns>true if the display style applies to the cell only when it is the current cell; otherwise false. The default is false.</returns>
	[DefaultValue(false)]
	public bool DisplayStyleForCurrentCellOnly
	{
		get
		{
			return displayStyleForCurrentCellOnly;
		}
		set
		{
			displayStyleForCurrentCellOnly = value;
		}
	}

	/// <summary>Gets or sets the width of the of the drop-down list portion of a combo box.</summary>
	/// <returns>The width, in pixels, of the drop-down list. The default is 1.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than one.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(1)]
	public virtual int DropDownWidth
	{
		get
		{
			return dropDownWidth;
		}
		set
		{
			if (value < 1)
			{
				throw new ArgumentOutOfRangeException("Value is less than 1.");
			}
			dropDownWidth = value;
		}
	}

	/// <summary>Gets the type of the cell's hosted editing control.</summary>
	/// <returns>The <see cref="T:System.Type" /> of the underlying editing control. This property always returns <see cref="T:System.Windows.Forms.DataGridViewComboBoxEditingControl" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override Type EditType => typeof(DataGridViewComboBoxEditingControl);

	/// <summary>Gets or sets the flat style appearance of the cell.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. The default value is <see cref="F:System.Windows.Forms.FlatStyle.Standard" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not a valid <see cref="T:System.Windows.Forms.FlatStyle" /> value.</exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(FlatStyle.Standard)]
	public FlatStyle FlatStyle
	{
		get
		{
			return flatStyle;
		}
		set
		{
			if (!Enum.IsDefined(typeof(FlatStyle), value))
			{
				throw new InvalidEnumArgumentException("Value is not valid FlatStyle.");
			}
			flatStyle = value;
		}
	}

	/// <summary>Gets the class type of the formatted value associated with the cell.</summary>
	/// <returns>The type of the cell's formatted value. This property always returns <see cref="T:System.String" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override Type FormattedValueType => typeof(string);

	/// <summary>Gets the objects that represent the selection displayed in the drop-down list. </summary>
	/// <returns>An <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> containing the selection. </returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public virtual ObjectCollection Items
	{
		get
		{
			if (base.DataGridView != null && base.DataGridView.BindingContext != null && DataSource != null && !string.IsNullOrEmpty(ValueMember))
			{
				items.ClearInternal();
				CurrencyManager currencyManager = (CurrencyManager)base.DataGridView.BindingContext[DataSource];
				if (currencyManager != null && currencyManager.Count > 0)
				{
					foreach (object item in currencyManager.List)
					{
						items.AddInternal(item);
					}
				}
			}
			return items;
		}
	}

	/// <summary>Gets or sets the maximum number of items shown in the drop-down list.</summary>
	/// <returns>The number of drop-down list items to allow. The minimum is 1 and the maximum is 100; the default is 8.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 1 or greater than 100 when setting this property.</exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(8)]
	public virtual int MaxDropDownItems
	{
		get
		{
			return maxDropDownItems;
		}
		set
		{
			if (value < 1 || value > 100)
			{
				throw new ArgumentOutOfRangeException("Value is less than 1 or greater than 100.");
			}
			maxDropDownItems = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the items in the combo box are automatically sorted.</summary>
	/// <returns>true if the combo box is sorted; otherwise, false. The default is false.</returns>
	/// <exception cref="T:System.ArgumentException">An attempt was made to sort a cell that is attached to a data source.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public virtual bool Sorted
	{
		get
		{
			return sorted;
		}
		set
		{
			sorted = value;
		}
	}

	/// <summary>Gets or sets a string that specifies where to gather the underlying values used in the drop-down list.</summary>
	/// <returns>A string specifying the name of a property or column. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is ignored.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property is not null and the specified value when setting this property is not null or <see cref="F:System.String.Empty" /> and does not name a valid property or column in the data source.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue("")]
	public virtual string ValueMember
	{
		get
		{
			return valueMember;
		}
		set
		{
			valueMember = value;
		}
	}

	/// <returns>A <see cref="T:System.Type" /> representing the data type of the value in the cell.</returns>
	/// <filterpriority>1</filterpriority>
	public override Type ValueType => typeof(string);

	internal DataGridViewComboBoxColumn OwningColumnTemplate
	{
		get
		{
			return owningColumnTemlate;
		}
		set
		{
			owningColumnTemlate = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> class.</summary>
	public DataGridViewComboBoxCell()
	{
		autoComplete = true;
		dataSource = null;
		displayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
		displayStyleForCurrentCellOnly = false;
		dropDownWidth = 1;
		flatStyle = FlatStyle.Standard;
		items = new ObjectCollection(this);
		maxDropDownItems = 8;
		sorted = false;
		owningColumnTemlate = null;
	}

	/// <summary>Creates an exact copy of this cell.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override object Clone()
	{
		DataGridViewComboBoxCell dataGridViewComboBoxCell = (DataGridViewComboBoxCell)base.Clone();
		dataGridViewComboBoxCell.autoComplete = autoComplete;
		dataGridViewComboBoxCell.dataSource = dataSource;
		dataGridViewComboBoxCell.displayStyle = displayStyle;
		dataGridViewComboBoxCell.displayMember = displayMember;
		dataGridViewComboBoxCell.valueMember = valueMember;
		dataGridViewComboBoxCell.displayStyleForCurrentCellOnly = displayStyleForCurrentCellOnly;
		dataGridViewComboBoxCell.dropDownWidth = dropDownWidth;
		dataGridViewComboBoxCell.flatStyle = flatStyle;
		dataGridViewComboBoxCell.items.AddRangeInternal(items);
		dataGridViewComboBoxCell.maxDropDownItems = maxDropDownItems;
		dataGridViewComboBoxCell.sorted = sorted;
		return dataGridViewComboBoxCell;
	}

	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override void DetachEditingControl()
	{
		base.DataGridView.EditingControlInternal = null;
	}

	/// <summary>Attaches and initializes the hosted editing control.</summary>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	/// <param name="initialFormattedValue">The initial value to be displayed in the control.</param>
	/// <param name="dataGridViewCellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that determines the appearance of the hosted control.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
	{
		base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
		ComboBox comboBox = base.DataGridView.EditingControl as ComboBox;
		comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		comboBox.Sorted = Sorted;
		comboBox.DataSource = null;
		comboBox.ValueMember = null;
		comboBox.DisplayMember = null;
		comboBox.Items.Clear();
		comboBox.SelectedIndex = -1;
		if (DataSource != null)
		{
			comboBox.DataSource = DataSource;
			comboBox.ValueMember = ValueMember;
			comboBox.DisplayMember = DisplayMember;
			return;
		}
		comboBox.Items.AddRange(Items);
		if (base.FormattedValue != null && comboBox.Items.IndexOf(base.FormattedValue) != -1)
		{
			comboBox.SelectedItem = base.FormattedValue;
		}
	}

	internal void SyncItems()
	{
		if (DataSource != null || OwningColumnTemplate == null)
		{
			return;
		}
		if (OwningColumnTemplate.DataGridView != null && OwningColumnTemplate.DataGridView.EditingControl is DataGridViewComboBoxEditingControl { SelectedItem: var selectedItem } dataGridViewComboBoxEditingControl)
		{
			dataGridViewComboBoxEditingControl.Items.Clear();
			dataGridViewComboBoxEditingControl.Items.AddRange(items);
			if (dataGridViewComboBoxEditingControl.Items.IndexOf(selectedItem) != -1)
			{
				dataGridViewComboBoxEditingControl.SelectedItem = selectedItem;
			}
		}
		OwningColumnTemplate.SyncItems(Items);
	}

	/// <summary>Determines if edit mode should be started based on the given key.</summary>
	/// <returns>true if edit mode should be started; otherwise, false. </returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that represents the key that was pressed.</param>
	/// <filterpriority>1</filterpriority>
	public override bool KeyEntersEditMode(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Space)
		{
			return true;
		}
		if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.Z)
		{
			return true;
		}
		if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.Divide)
		{
			return true;
		}
		if (e.KeyCode == Keys.BrowserSearch || e.KeyCode == Keys.SelectMedia)
		{
			return true;
		}
		if (e.KeyCode >= Keys.OemSemicolon && e.KeyCode <= Keys.ProcessKey)
		{
			return true;
		}
		if (e.KeyCode == Keys.Attn || e.KeyCode == Keys.Packet)
		{
			return true;
		}
		if (e.KeyCode >= Keys.Exsel && e.KeyCode <= Keys.OemClear)
		{
			return true;
		}
		if (e.KeyCode == Keys.F4)
		{
			return true;
		}
		if (e.Modifiers == Keys.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
		{
			return true;
		}
		return false;
	}

	/// <returns>The cell value.</returns>
	/// <param name="formattedValue">The display value of the cell.</param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
	/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> for the display value type, or null to use the default converter.</param>
	/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> for the cell value type, or null to use the default converter.</param>
	public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
	{
		return base.ParseFormattedValue(formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter);
	}

	/// <returns>A string that represents the current object.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return $"DataGridViewComboBoxCell {{ ColumnIndex={base.ColumnIndex}, RowIndex={base.RowIndex} }}";
	}

	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
	/// <param name="graphics">The graphics context for the cell.</param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
	{
		if (base.DataGridView == null)
		{
			return Rectangle.Empty;
		}
		object formattedValue = base.FormattedValue;
		Size size = Size.Empty;
		if (formattedValue != null)
		{
			size = DataGridViewCell.MeasureTextSize(graphics, formattedValue.ToString(), cellStyle.Font, TextFormatFlags.Left);
		}
		return new Rectangle(1, (base.OwningRow.Height - size.Height) / 2, size.Width - 3, size.Height);
	}

	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
	/// <param name="graphics">The graphics context for the cell.</param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
	{
		if (base.DataGridView == null || string.IsNullOrEmpty(base.ErrorText))
		{
			return Rectangle.Empty;
		}
		Size size = new Size(12, 11);
		return new Rectangle(new Point(base.Size.Width - size.Width - 23, (base.Size.Height - size.Height) / 2), size);
	}

	/// <summary>Gets the formatted value of the cell's data. </summary>
	/// <returns>The value of the cell's data after formatting has been applied or null if the cell is not part of a <see cref="T:System.Windows.Forms.DataGridView" /> control.</returns>
	/// <param name="value">The value to be formatted. </param>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
	/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the value type that provides custom conversion to the formatted value type, or null if no such custom conversion is needed.</param>
	/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the formatted value type that provides custom conversion from the value type, or null if no such custom conversion is needed.</param>
	/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values describing the context in which the formatted value is needed.</param>
	/// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event of the <see cref="T:System.Windows.Forms.DataGridView" /> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" /> for type conversion errors or to type <see cref="T:System.ArgumentException" /> if <paramref name="value" /> cannot be found in the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> or the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.Items" /> collection. </exception>
	protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
	{
		return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
	}

	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
	/// <param name="rowIndex">The zero-based row index of the cell.</param>
	/// <param name="constraintSize">The cell's maximum allowable size.</param>
	protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
	{
		object formattedValue = base.FormattedValue;
		if (formattedValue != null)
		{
			Size result = DataGridViewCell.MeasureTextSize(graphics, formattedValue.ToString(), cellStyle.Font, TextFormatFlags.Left);
			result.Height = Math.Max(result.Height, 22);
			result.Width += 25;
			return result;
		}
		return new Size(39, 22);
	}

	/// <summary>Called when the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell changes.</summary>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property is not null and the value of either the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DisplayMember" /> property or the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.ValueMember" /> property is not null or <see cref="F:System.String.Empty" /> and does not name a valid property or column in the data source.</exception>
	protected override void OnDataGridViewChanged()
	{
		base.OnDataGridViewChanged();
	}

	/// <param name="rowIndex">The index of the cell's parent row. </param>
	/// <param name="throughMouseClick">true if a user action moved focus to the cell; false if a programmatic operation moved focus to the cell.</param>
	protected override void OnEnter(int rowIndex, bool throughMouseClick)
	{
		base.OnEnter(rowIndex, throughMouseClick);
	}

	/// <param name="rowIndex">The index of the cell's parent row. </param>
	/// <param name="throughMouseClick">true if a user action moved focus from the cell; false if a programmatic operation moved focus from the cell.</param>
	protected override void OnLeave(int rowIndex, bool throughMouseClick)
	{
		base.OnLeave(rowIndex, throughMouseClick);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
	{
		base.OnMouseClick(e);
	}

	/// <param name="rowIndex">The index of the cell's parent row. </param>
	protected override void OnMouseEnter(int rowIndex)
	{
		base.OnMouseEnter(rowIndex);
	}

	/// <param name="rowIndex">The index of the cell's parent row. </param>
	protected override void OnMouseLeave(int rowIndex)
	{
		base.OnMouseLeave(rowIndex);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
	{
		base.OnMouseMove(e);
	}

	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</param>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
	/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="rowIndex">The row index of the cell that is being painted.</param>
	/// <param name="elementState"></param>
	/// <param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="errorText">An error message that is associated with the cell.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
	/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
	/// <param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values that specifies which parts of the cell need to be painted.</param>
	protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
	}

	internal override void PaintPartContent(Graphics graphics, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle, object formattedValue)
	{
		Color foreColor = ((!Selected) ? cellStyle.ForeColor : cellStyle.SelectionForeColor);
		TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.TextBoxControl | TextFormatFlags.EndEllipsis;
		Rectangle contentBounds = base.ContentBounds;
		contentBounds.X += cellBounds.X;
		contentBounds.Y += cellBounds.Y;
		Rectangle rectangle = CalculateButtonArea(cellBounds);
		graphics.FillRectangle(SystemBrushes.Control, rectangle);
		ThemeEngine.Current.CPDrawComboButton(graphics, rectangle, ButtonState.Normal);
		if (formattedValue != null)
		{
			TextRenderer.DrawText(graphics, formattedValue.ToString(), cellStyle.Font, contentBounds, foreColor, flags);
		}
	}

	private Rectangle CalculateButtonArea(Rectangle cellBounds)
	{
		int width = ThemeEngine.Current.Border3DSize.Width;
		Rectangle rectangle = cellBounds;
		Rectangle result = cellBounds;
		result.X = rectangle.Right - 16 - width;
		result.Y = rectangle.Y + width;
		result.Width = 16;
		result.Height = rectangle.Height - 2 * width;
		return result;
	}
}
