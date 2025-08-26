using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Represents the collection of groups within a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
/// <filterpriority>2</filterpriority>
[ListBindable(false)]
public class ListViewGroupCollection : ICollection, IEnumerable, IList
{
	private List<ListViewGroup> list;

	private ListView list_view_owner;

	private ListViewGroup default_group;

	/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
	/// <returns>true in all cases.</returns>
	bool ICollection.IsSynchronized => true;

	/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
	/// <returns>The object used to synchronize the collection.</returns>
	object ICollection.SyncRoot => this;

	/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
	/// <returns>false in all cases.</returns>
	bool IList.IsFixedSize => false;

	/// <summary>Gets a value indicating whether the collection is read-only.</summary>
	/// <returns>false in all cases.</returns>
	bool IList.IsReadOnly => false;

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ListViewGroup" /> at the specified index within the collection.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ListViewGroup" /> that represents the item located at the specified index within the collection.</returns>
	/// <param name="index">The zero-based index of the element to get or set.</param>
	object IList.this[int index]
	{
		get
		{
			return this[index];
		}
		set
		{
			if (value is ListViewGroup)
			{
				this[index] = (ListViewGroup)value;
			}
		}
	}

	internal ListView ListViewOwner
	{
		get
		{
			return list_view_owner;
		}
		set
		{
			list_view_owner = value;
		}
	}

	/// <summary>Gets the number of groups in the collection.</summary>
	/// <returns>The number of groups in the collection.</returns>
	/// <filterpriority>1</filterpriority>
	public int Count => list.Count;

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ListViewGroup" /> at the specified index within the collection.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ListViewGroup" /> at the specified index within the collection.</returns>
	/// <param name="index">The index within the collection of the <see cref="T:System.Windows.Forms.ListViewGroup" /> to get or set. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than 0 or greater than or equal to <see cref="P:System.Windows.Forms.ListViewGroupCollection.Count" />.</exception>
	/// <filterpriority>1</filterpriority>
	public ListViewGroup this[int index]
	{
		get
		{
			if (list.Count <= index || index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return list[index];
		}
		set
		{
			if (list.Count <= index || index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (!Contains(value))
			{
				if (value != null)
				{
					CheckListViewItemsInGroup(value);
				}
				list[index] = value;
				if (list_view_owner != null)
				{
					list_view_owner.Redraw(recalculate: true);
				}
			}
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ListViewGroup" /> with the specified <see cref="P:System.Windows.Forms.ListViewGroup.Name" /> property value. </summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ListViewGroup" /> with the specified name, or null if no such <see cref="T:System.Windows.Forms.ListViewGroup" /> exists.</returns>
	/// <param name="key">The name of the group to get or set.</param>
	public ListViewGroup this[string key]
	{
		get
		{
			int num = IndexOfKey(key);
			if (num != -1)
			{
				return this[num];
			}
			return null;
		}
		set
		{
			int num = IndexOfKey(key);
			if (num != -1)
			{
				this[num] = value;
			}
		}
	}

	internal int InternalCount => list.Count + 1;

	internal ListViewGroup DefaultGroup => default_group;

	private ListViewGroupCollection()
	{
		list = new List<ListViewGroup>();
		default_group = new ListViewGroup("Default Group");
		default_group.IsDefault = true;
	}

	internal ListViewGroupCollection(ListView listViewOwner)
		: this()
	{
		list_view_owner = listViewOwner;
		default_group.ListViewOwner = listViewOwner;
	}

	/// <summary>Adds a new <see cref="T:System.Windows.Forms.ListViewGroup" /> to the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</summary>
	/// <returns>The index at which the <see cref="T:System.Windows.Forms.ListViewGroup" /> has been added.</returns>
	/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to add to the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.ListViewGroup" />.-or-<paramref name="value" /> contains at least one <see cref="T:System.Windows.Forms.ListViewItem" /> that belongs to a <see cref="T:System.Windows.Forms.ListView" /> control other than the one that owns this <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</exception>
	int IList.Add(object value)
	{
		if (!(value is ListViewGroup))
		{
			throw new ArgumentException("value");
		}
		return Add((ListViewGroup)value);
	}

	/// <summary>Determines whether the specified value is located in the collection.</summary>
	/// <returns>true if <paramref name="value" /> is a <see cref="T:System.Windows.Forms.ListViewGroup" /> contained in the collection; otherwise, false.</returns>
	/// <param name="value">An object that represents the <see cref="T:System.Windows.Forms.ListViewGroup" /> to locate in the collection.</param>
	bool IList.Contains(object value)
	{
		if (value is ListViewGroup)
		{
			return Contains((ListViewGroup)value);
		}
		return false;
	}

	/// <summary>Returns the index within the collection of the specified value.</summary>
	/// <returns>The zero-based index of <paramref name="value" /> if it is in the collection; otherwise, -1.</returns>
	/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to find in the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</param>
	int IList.IndexOf(object value)
	{
		if (value is ListViewGroup)
		{
			return IndexOf((ListViewGroup)value);
		}
		return -1;
	}

	/// <summary>Inserts a <see cref="T:System.Windows.Forms.ListViewGroup" /> into the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</summary>
	/// <param name="index">The position at which the <see cref="T:System.Windows.Forms.ListViewGroup" /> is added to the collection.</param>
	/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to add to the collection.</param>
	void IList.Insert(int index, object value)
	{
		if (value is ListViewGroup)
		{
			Insert(index, (ListViewGroup)value);
		}
	}

	/// <summary>Removes the <see cref="T:System.Windows.Forms.ListViewGroup" /> from the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</summary>
	/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to remove from the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</param>
	void IList.Remove(object value)
	{
		Remove((ListViewGroup)value);
	}

	/// <summary>Returns an enumerator used to iterate through the collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the collection.</returns>
	/// <filterpriority>1</filterpriority>
	public IEnumerator GetEnumerator()
	{
		return list.GetEnumerator();
	}

	/// <summary>Copies the groups in the collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
	/// <param name="array">The <see cref="T:System.Array" /> to which the groups are copied. </param>
	/// <param name="index">The first index within the array to which the groups are copied. </param>
	/// <filterpriority>1</filterpriority>
	public void CopyTo(Array array, int index)
	{
		((ICollection)list).CopyTo(array, index);
	}

	/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ListViewGroup" /> to the collection.</summary>
	/// <returns>The index of the group within the collection, or -1 if the group is already present in the collection.</returns>
	/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to add to the collection. </param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="group" /> contains at least one <see cref="T:System.Windows.Forms.ListViewItem" /> that belongs to a <see cref="T:System.Windows.Forms.ListView" /> control other than the one that owns this <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int Add(ListViewGroup group)
	{
		if (Contains(group))
		{
			return -1;
		}
		AddGroup(group);
		if (list_view_owner != null)
		{
			list_view_owner.Redraw(recalculate: true);
		}
		return list.Count - 1;
	}

	/// <summary>Adds a new <see cref="T:System.Windows.Forms.ListViewGroup" /> to the collection using the specified values to initialize the <see cref="P:System.Windows.Forms.ListViewGroup.Name" /> and <see cref="P:System.Windows.Forms.ListViewGroup.Header" /> properties </summary>
	/// <returns>The new <see cref="T:System.Windows.Forms.ListViewGroup" />.</returns>
	/// <param name="key">The initial value of the <see cref="P:System.Windows.Forms.ListViewGroup.Name" /> property for the new group.</param>
	/// <param name="headerText">The initial value of the <see cref="P:System.Windows.Forms.ListViewGroup.Header" /> property for the new group.</param>
	public ListViewGroup Add(string key, string headerText)
	{
		ListViewGroup listViewGroup = new ListViewGroup(key, headerText);
		Add(listViewGroup);
		return listViewGroup;
	}

	/// <summary>Removes all groups from the collection.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Clear()
	{
		foreach (ListViewGroup item in list)
		{
			item.ListViewOwner = null;
		}
		list.Clear();
		if (list_view_owner != null)
		{
			list_view_owner.Redraw(recalculate: true);
		}
	}

	/// <summary>Determines whether the specified group is located in the collection.</summary>
	/// <returns>true if the group is in the collection; otherwise, false.</returns>
	/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to locate in the collection. </param>
	/// <filterpriority>1</filterpriority>
	public bool Contains(ListViewGroup value)
	{
		return list.Contains(value);
	}

	/// <summary>Returns the index of the specified <see cref="T:System.Windows.Forms.ListViewGroup" /> within the collection.</summary>
	/// <returns>The zero-based index of the group within the collection, or -1 if the group is not in the collection.</returns>
	/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to locate in the collection. </param>
	/// <filterpriority>1</filterpriority>
	public int IndexOf(ListViewGroup value)
	{
		return list.IndexOf(value);
	}

	/// <summary>Inserts the specified <see cref="T:System.Windows.Forms.ListViewGroup" /> into the collection at the specified index.</summary>
	/// <param name="index">The index within the collection at which to insert the group. </param>
	/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to insert into the collection. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Insert(int index, ListViewGroup group)
	{
		if (!Contains(group))
		{
			CheckListViewItemsInGroup(group);
			group.ListViewOwner = list_view_owner;
			list.Insert(index, group);
			if (list_view_owner != null)
			{
				list_view_owner.Redraw(recalculate: true);
			}
		}
	}

	/// <summary>Removes the specified <see cref="T:System.Windows.Forms.ListViewGroup" /> from the collection.</summary>
	/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to remove from the collection. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Remove(ListViewGroup group)
	{
		int num = list.IndexOf(group);
		if (num != -1)
		{
			RemoveAt(num);
		}
	}

	/// <summary>Removes the <see cref="T:System.Windows.Forms.ListViewGroup" /> at the specified index within the collection.</summary>
	/// <param name="index">The index within the collection of the <see cref="T:System.Windows.Forms.ListViewGroup" /> to remove. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void RemoveAt(int index)
	{
		if (list.Count > index && index >= 0)
		{
			ListViewGroup listViewGroup = list[index];
			listViewGroup.ListViewOwner = null;
			list.RemoveAt(index);
			if (list_view_owner != null)
			{
				list_view_owner.Redraw(recalculate: true);
			}
		}
	}

	private int IndexOfKey(string key)
	{
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].Name == key)
			{
				return i;
			}
		}
		return -1;
	}

	/// <summary>Adds an array of groups to the collection.</summary>
	/// <param name="groups">An array of type <see cref="T:System.Windows.Forms.ListViewGroup" /> that specifies the groups to add to the collection. </param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="groups" /> contains at least one group with at least one <see cref="T:System.Windows.Forms.ListViewItem" /> that belongs to a <see cref="T:System.Windows.Forms.ListView" /> control other than the one that owns this <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void AddRange(ListViewGroup[] groups)
	{
		foreach (ListViewGroup group in groups)
		{
			AddGroup(group);
		}
		if (list_view_owner != null)
		{
			list_view_owner.Redraw(recalculate: true);
		}
	}

	/// <summary>Adds the groups in an existing <see cref="T:System.Windows.Forms.ListViewGroupCollection" /> to the collection.</summary>
	/// <param name="groups">A <see cref="T:System.Windows.Forms.ListViewGroupCollection" /> containing the groups to add to the collection. </param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="groups" /> contains at least one group with at least one <see cref="T:System.Windows.Forms.ListViewItem" /> that belongs to a <see cref="T:System.Windows.Forms.ListView" /> control other than the one that owns this <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void AddRange(ListViewGroupCollection groups)
	{
		foreach (ListViewGroup group in groups)
		{
			AddGroup(group);
		}
		if (list_view_owner != null)
		{
			list_view_owner.Redraw(recalculate: true);
		}
	}

	internal ListViewGroup GetInternalGroup(int index)
	{
		if (index == 0)
		{
			return default_group;
		}
		return list[index - 1];
	}

	private void AddGroup(ListViewGroup group)
	{
		if (!Contains(group))
		{
			CheckListViewItemsInGroup(group);
			group.ListViewOwner = list_view_owner;
			list.Add(group);
		}
	}

	private void CheckListViewItemsInGroup(ListViewGroup value)
	{
		foreach (ListViewItem item in value.Items)
		{
			if (item.ListView != null && item.ListView != list_view_owner)
			{
				throw new ArgumentException("ListViewItem belongs to a ListView control other than the one that owns this ListViewGroupCollection.", "ListViewGroup");
			}
		}
	}
}
