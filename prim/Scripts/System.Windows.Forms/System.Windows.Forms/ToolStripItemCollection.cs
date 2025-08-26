using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms;

/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.ToolStripItem" /> objects.</summary>
/// <filterpriority>2</filterpriority>
[ListBindable(false)]
[Editor("System.Windows.Forms.Design.ToolStripCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
public class ToolStripItemCollection : ArrangedElementCollection, ICollection, IEnumerable, IList
{
	private ToolStrip owner;

	private bool internal_created;

	/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
	/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
	bool IList.IsFixedSize => base.IsFixedSize;

	/// <summary>Retrieves the element at the specified index.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> at the specified index.</returns>
	/// <param name="index">The zero-based index of the item to get.</param>
	object IList.this[int index]
	{
		get
		{
			return this[index];
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only; otherwise, false.</returns>
	public override bool IsReadOnly => base.IsReadOnly;

	/// <summary>Gets the item at the specified index.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> located at the specified position in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />.</returns>
	/// <param name="index">The zero-based index of the item to get.</param>
	/// <filterpriority>1</filterpriority>
	public new virtual ToolStripItem this[int index] => (ToolStripItem)base[index];

	/// <summary>Gets the item with the specified name.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> with the specified name.</returns>
	/// <param name="key">The name of the item to get.</param>
	/// <filterpriority>1</filterpriority>
	public virtual ToolStripItem this[string key]
	{
		get
		{
			IEnumerator enumerator = GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ToolStripItem toolStripItem = (ToolStripItem)enumerator.Current;
					if (toolStripItem.Name == key)
					{
						return toolStripItem;
					}
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return null;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> class with the specified container <see cref="T:System.Windows.Forms.ToolStrip" /> and the specified array of <see cref="T:System.Windows.Forms.ToolStripItem" /> controls.</summary>
	/// <param name="owner">The <see cref="T:System.Windows.Forms.ToolStrip" /> to which this <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> belongs. </param>
	/// <param name="value">An array of type <see cref="T:System.Windows.Forms.ToolStripItem" /> containing the initial controls for this <see cref="T:System.Windows.Forms.ToolStripItemCollection" />. </param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="owner" /> parameter is null.</exception>
	public ToolStripItemCollection(ToolStrip owner, ToolStripItem[] value)
	{
		if (owner == null)
		{
			throw new ArgumentNullException("owner");
		}
		if (value == null)
		{
			throw new ArgumentNullException("toolStripItems");
		}
		this.owner = owner;
		foreach (ToolStripItem value2 in value)
		{
			AddNoOwnerOrLayout(value2);
		}
	}

	internal ToolStripItemCollection(ToolStrip owner, ToolStripItem[] value, bool internalcreated)
	{
		if (owner == null)
		{
			throw new ArgumentNullException("owner");
		}
		internal_created = internalcreated;
		this.owner = owner;
		if (value != null)
		{
			foreach (ToolStripItem value2 in value)
			{
				AddNoOwnerOrLayout(value2);
			}
		}
	}

	/// <summary>Adds an item to the collection.</summary>
	/// <returns>The location at which <paramref name="value" /> was inserted.</returns>
	/// <param name="value">The item to add to the collection.</param>
	int IList.Add(object value)
	{
		return Add((ToolStripItem)value);
	}

	/// <summary>Removes all items from the collection.</summary>
	void IList.Clear()
	{
		Clear();
	}

	/// <summary>Determines if the collection contains a specified item.</summary>
	/// <returns>true if <paramref name="value" /> is contained in the collection; otherwise, false.</returns>
	/// <param name="value">The item to locate in the collection.</param>
	bool IList.Contains(object value)
	{
		return Contains((ToolStripItem)value);
	}

	/// <summary>Determines the location of a specified item in the collection.</summary>
	/// <returns>The index of the item in the collection, if found; otherwise, -1.</returns>
	/// <param name="value">The item to locate in the collection.</param>
	int IList.IndexOf(object value)
	{
		return IndexOf((ToolStripItem)value);
	}

	/// <summary>Inserts an item into the collection at a specified index.</summary>
	/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
	/// <param name="value">The item to insert into the collection.</param>
	void IList.Insert(int index, object value)
	{
		Insert(index, (ToolStripItem)value);
	}

	/// <summary>Removes the first occurrence of a specified item from the collection.</summary>
	/// <param name="value">The item to remove from the collection.</param>
	void IList.Remove(object value)
	{
		Remove((ToolStripItem)value);
	}

	/// <summary>Removes an item from the collection at a specified index.</summary>
	/// <param name="index">The zero-based index of the item to remove.</param>
	void IList.RemoveAt(int index)
	{
		RemoveAt(index);
	}

	/// <summary>Adds a <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the specified image to the collection.</summary>
	/// <returns>The new <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
	public ToolStripItem Add(Image image)
	{
		ToolStripItem toolStripItem = owner.CreateDefaultItem(string.Empty, image, null);
		Add(toolStripItem);
		return toolStripItem;
	}

	/// <summary>Adds a <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the specified text to the collection.</summary>
	/// <returns>The new <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
	/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
	public ToolStripItem Add(string text)
	{
		ToolStripItem toolStripItem = owner.CreateDefaultItem(text, null, null);
		Add(toolStripItem);
		return toolStripItem;
	}

	/// <summary>Adds the specified item to the end of the collection.</summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the zero-based index of the new item in the collection.</returns>
	/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to add to the end of the collection. </param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int Add(ToolStripItem value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		value.InternalOwner = owner;
		if (value is ToolStripMenuItem && (value as ToolStripMenuItem).ShortcutKeys != 0)
		{
			ToolStripManager.AddToolStripMenuItem((ToolStripMenuItem)value);
		}
		int result = Add((object)value);
		if (internal_created)
		{
			owner.OnItemAdded(new ToolStripItemEventArgs(value));
		}
		return result;
	}

	/// <summary>Adds a <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the specified image and text to the collection.</summary>
	/// <returns>The new <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
	/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
	public ToolStripItem Add(string text, Image image)
	{
		ToolStripItem toolStripItem = owner.CreateDefaultItem(text, image, null);
		Add(toolStripItem);
		return toolStripItem;
	}

	/// <summary>Adds a <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the specified image and text to the collection and that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
	/// <returns>The new <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
	/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
	/// <param name="onClick">Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</param>
	public ToolStripItem Add(string text, Image image, EventHandler onClick)
	{
		ToolStripItem toolStripItem = owner.CreateDefaultItem(text, image, onClick);
		Add(toolStripItem);
		return toolStripItem;
	}

	/// <summary>Adds an array of <see cref="T:System.Windows.Forms.ToolStripItem" /> controls to the collection.</summary>
	/// <param name="toolStripItems">An array of <see cref="T:System.Windows.Forms.ToolStripItem" /> controls. </param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="toolStripItems" /> parameter is null. </exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void AddRange(ToolStripItem[] toolStripItems)
	{
		if (toolStripItems == null)
		{
			throw new ArgumentNullException("toolStripItems");
		}
		if (IsReadOnly)
		{
			throw new NotSupportedException("This collection is read-only");
		}
		owner.SuspendLayout();
		foreach (ToolStripItem value in toolStripItems)
		{
			Add(value);
		}
		owner.ResumeLayout();
	}

	/// <summary>Adds a <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> to the current collection.</summary>
	/// <param name="toolStripItems">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> to be added to the current collection. </param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="toolStripItems" /> parameter is null. </exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void AddRange(ToolStripItemCollection toolStripItems)
	{
		if (toolStripItems == null)
		{
			throw new ArgumentNullException("toolStripItems");
		}
		if (IsReadOnly)
		{
			throw new NotSupportedException("This collection is read-only");
		}
		owner.SuspendLayout();
		foreach (ToolStripItem toolStripItem in toolStripItems)
		{
			Add(toolStripItem);
		}
		owner.ResumeLayout();
	}

	/// <summary>Removes all items from the collection.</summary>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public new virtual void Clear()
	{
		if (IsReadOnly)
		{
			throw new NotSupportedException("This collection is read-only");
		}
		base.Clear();
		owner.PerformLayout();
	}

	/// <summary>Determines whether the specified item is a member of the collection.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is a member of the current <see cref="T:System.Windows.Forms.ToolStripItemCollection" />; otherwise, false.</returns>
	/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to search for in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />. </param>
	/// <filterpriority>1</filterpriority>
	public bool Contains(ToolStripItem value)
	{
		return Contains((object)value);
	}

	/// <summary>Determines whether the collection contains an item with the specified key.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> contains a <see cref="T:System.Windows.Forms.ToolStripItem" /> with the specified key; otherwise, false.</returns>
	/// <param name="key">The key to locate in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />. </param>
	/// <filterpriority>1</filterpriority>
	public virtual bool ContainsKey(string key)
	{
		return this[key] != null;
	}

	/// <summary>Copies the collection into the specified position of the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> array.</summary>
	/// <param name="array">The array of type <see cref="T:System.Windows.Forms.ToolStripItem" /> to which to copy the collection. </param>
	/// <param name="index">The position in the <see cref="T:System.Windows.Forms.ToolStripItem" /> array at which to paste the collection. </param>
	/// <filterpriority>1</filterpriority>
	public void CopyTo(ToolStripItem[] array, int index)
	{
		CopyTo((Array)array, index);
	}

	/// <summary>Searches for items by their name and returns an array of all matching controls.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> array of the search results.</returns>
	/// <param name="key">The item name to search the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> for.</param>
	/// <param name="searchAllChildren">true to search child items of the <see cref="T:System.Windows.Forms.ToolStripItem" /> specified by the <paramref name="key" /> parameter; otherwise, false. </param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null or empty.</exception>
	[System.MonoTODO("searchAllChildren parameter isn't used")]
	public ToolStripItem[] Find(string key, bool searchAllChildren)
	{
		if (key == null || key.Length == 0)
		{
			throw new ArgumentNullException("key");
		}
		List<ToolStripItem> list = new List<ToolStripItem>();
		IEnumerator enumerator = GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				ToolStripItem toolStripItem = (ToolStripItem)enumerator.Current;
				if (string.Compare(toolStripItem.Name, key, ignoreCase: true) == 0)
				{
					list.Add(toolStripItem);
					if (!searchAllChildren)
					{
					}
				}
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		return list.ToArray();
	}

	/// <summary>Retrieves the index of the specified item in the collection.</summary>
	/// <returns>A zero-based index value that represents the position of the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />, if found; otherwise, -1.</returns>
	/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to locate in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />. </param>
	/// <filterpriority>1</filterpriority>
	public int IndexOf(ToolStripItem value)
	{
		return IndexOf((object)value);
	}

	/// <summary>Retrieves the index of the first occurrence of the specified item within the collection.</summary>
	/// <returns>A zero-based index value that represents the position of the first occurrence of the <see cref="T:System.Windows.Forms.ToolStripItem" /> specified by the <paramref name="key" /> parameter, if found; otherwise, -1.</returns>
	/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ToolStripItem" /> to search for. </param>
	/// <filterpriority>1</filterpriority>
	public virtual int IndexOfKey(string key)
	{
		ToolStripItem toolStripItem = this[key];
		if (toolStripItem == null)
		{
			return -1;
		}
		return IndexOf(toolStripItem);
	}

	/// <summary>Inserts the specified item into the collection at the specified index.</summary>
	/// <param name="index">The location in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> at which to insert the <see cref="T:System.Windows.Forms.ToolStripItem" />. </param>
	/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to insert. </param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Insert(int index, ToolStripItem value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (value is ToolStripMenuItem && (value as ToolStripMenuItem).ShortcutKeys != 0)
		{
			ToolStripManager.AddToolStripMenuItem((ToolStripMenuItem)value);
		}
		if (value.Owner != null)
		{
			value.Owner.Items.Remove(value);
		}
		Insert(index, (object)value);
		if (internal_created)
		{
			value.InternalOwner = owner;
			owner.OnItemAdded(new ToolStripItemEventArgs(value));
		}
		if (owner.Created)
		{
			owner.PerformLayout();
		}
	}

	/// <summary>Removes the specified item from the collection.</summary>
	/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to remove from the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />. </param>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Remove(ToolStripItem value)
	{
		if (IsReadOnly)
		{
			throw new NotSupportedException("This collection is read-only");
		}
		Remove((object)value);
		if (value != null && internal_created)
		{
			value.InternalOwner = null;
			value.Parent = null;
		}
		if (internal_created)
		{
			owner.OnItemRemoved(new ToolStripItemEventArgs(value));
		}
		if (owner.Created)
		{
			owner.PerformLayout();
		}
	}

	/// <summary>Removes an item from the specified index in the collection.</summary>
	/// <param name="index">The index value of the <see cref="T:System.Windows.Forms.ToolStripItem" /> to remove. </param>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void RemoveAt(int index)
	{
		if (IsReadOnly)
		{
			throw new NotSupportedException("This collection is read-only");
		}
		ToolStripItem value = (ToolStripItem)base[index];
		Remove(value);
	}

	/// <summary>Removes the item that has the specified key.</summary>
	/// <param name="key">The key of the <see cref="T:System.Windows.Forms.ToolStripItem" /> to remove. </param>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void RemoveByKey(string key)
	{
		if (IsReadOnly)
		{
			throw new NotSupportedException("This collection is read-only");
		}
		ToolStripItem toolStripItem = this[key];
		if (toolStripItem != null)
		{
			Remove(toolStripItem);
		}
	}

	internal int AddNoOwnerOrLayout(ToolStripItem value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		return Add((object)value);
	}

	internal void InsertNoOwnerOrLayout(int index, ToolStripItem value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (index > Count)
		{
			Add((object)value);
		}
		else
		{
			Insert(index, (object)value);
		}
	}

	internal void RemoveNoOwnerOrLayout(ToolStripItem value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		Remove((object)value);
	}
}
