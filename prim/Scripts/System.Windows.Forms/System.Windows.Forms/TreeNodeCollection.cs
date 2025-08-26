using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace System.Windows.Forms;

/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.TreeNode" /> objects. </summary>
/// <filterpriority>2</filterpriority>
[Editor("System.Windows.Forms.Design.TreeNodeCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
public class TreeNodeCollection : ICollection, IEnumerable, IList
{
	internal class TreeNodeEnumerator : IEnumerator
	{
		private TreeNodeCollection collection;

		private int index = -1;

		public object Current
		{
			get
			{
				if (index == -1)
				{
					return null;
				}
				return collection[index];
			}
		}

		public TreeNodeEnumerator(TreeNodeCollection collection)
		{
			this.collection = collection;
		}

		public bool MoveNext()
		{
			if (index + 1 >= collection.Count)
			{
				return false;
			}
			index++;
			return true;
		}

		public void Reset()
		{
			index = -1;
		}
	}

	private class TreeNodeComparer : IComparer
	{
		private CompareInfo compare;

		public TreeNodeComparer(CompareInfo compare)
		{
			this.compare = compare;
		}

		public int Compare(object x, object y)
		{
			TreeNode treeNode = (TreeNode)x;
			TreeNode treeNode2 = (TreeNode)y;
			int num = compare.Compare(treeNode.Text, treeNode2.Text);
			return (num != 0) ? num : (treeNode.Index - treeNode2.Index);
		}
	}

	private static readonly int OrigSize = 50;

	private TreeNode owner;

	private int count;

	private TreeNode[] nodes;

	/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
	/// <returns>false in all cases.</returns>
	bool ICollection.IsSynchronized => false;

	/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
	/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.TreeNodeCollection" />.</returns>
	object ICollection.SyncRoot => this;

	/// <summary>Gets a value indicating whether the tree node collection has a fixed size.</summary>
	/// <returns>false in all cases.</returns>
	bool IList.IsFixedSize => false;

	/// <summary>Gets or sets the tree node at the specified index in the collection.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> at the specified index in the <see cref="T:System.Windows.Forms.TreeNodeCollection" />.</returns>
	/// <exception cref="T:System.ArgumentException">The value set is not a <see cref="T:System.Windows.Forms.TreeNode" />.</exception>
	object IList.this[int index]
	{
		get
		{
			return this[index];
		}
		set
		{
			if (!(value is TreeNode))
			{
				throw new ArgumentException("Parameter must be of type TreeNode.", "value");
			}
			this[index] = (TreeNode)value;
		}
	}

	/// <summary>Gets the total number of <see cref="T:System.Windows.Forms.TreeNode" /> objects in the collection.</summary>
	/// <returns>The total number of <see cref="T:System.Windows.Forms.TreeNode" /> objects in the collection.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public int Count => count;

	/// <summary>Gets a value indicating whether the collection is read-only.</summary>
	/// <returns>true if the collection is read-only; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool IsReadOnly => false;

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.TreeNode" /> at the specified indexed location in the collection.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> at the specified indexed location in the collection.</returns>
	/// <param name="index">The indexed location of the <see cref="T:System.Windows.Forms.TreeNode" /> in the collection. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> value is less than 0 or is greater than the number of tree nodes in the collection. </exception>
	/// <filterpriority>1</filterpriority>
	public virtual TreeNode this[int index]
	{
		get
		{
			if (index < 0 || index >= Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return nodes[index];
		}
		set
		{
			if (index < 0 || index >= Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			SetupNode(value);
			nodes[index] = value;
		}
	}

	/// <summary>Gets the tree node with the specified key from the collection. </summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> with the specified key.</returns>
	/// <param name="key">The name of the <see cref="T:System.Windows.Forms.TreeNode" /> to retrieve from the collection.</param>
	/// <filterpriority>1</filterpriority>
	public virtual TreeNode this[string key]
	{
		get
		{
			for (int i = 0; i < count; i++)
			{
				if (string.Compare(key, nodes[i].Name, ignoreCase: true) == 0)
				{
					return nodes[i];
				}
			}
			return null;
		}
	}

	private TreeNodeCollection()
	{
	}

	internal TreeNodeCollection(TreeNode owner)
	{
		this.owner = owner;
		nodes = new TreeNode[OrigSize];
	}

	/// <summary>Adds an object to the end of the tree node collection.</summary>
	/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.TreeNode" /> that was added to the tree node collection.</returns>
	/// <param name="node">The object to add to the tree node collection.</param>
	/// <exception cref="T:System.Exception">
	///   <paramref name="node" /> is currently assigned to another <see cref="T:System.Windows.Forms.TreeView" /> control.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="node" /> is null.</exception>
	int IList.Add(object node)
	{
		return Add((TreeNode)node);
	}

	/// <summary>Determines whether the specified tree node is a member of the collection.</summary>
	/// <returns>true if <paramref name="node" /> is a member of the collection; otherwise, false.</returns>
	/// <param name="node">The object to find in the collection.</param>
	bool IList.Contains(object node)
	{
		return Contains((TreeNode)node);
	}

	/// <summary>Returns the index of the specified tree node in the collection.</summary>
	/// <returns>The zero-based index of the item found in the tree node collection; otherwise, -1.</returns>
	/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to locate in the collection.</param>
	int IList.IndexOf(object node)
	{
		return IndexOf((TreeNode)node);
	}

	/// <summary>Inserts an existing tree node in the tree node collection at the specified location.</summary>
	/// <param name="index">The indexed location within the collection to insert the tree node. </param>
	/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to insert into the collection.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="node" /> is currently assigned to another <see cref="T:System.Windows.Forms.TreeView" />.-or-<paramref name="node" /> is not a <see cref="T:System.Windows.Forms.TreeNode" />.</exception>
	void IList.Insert(int index, object node)
	{
		Insert(index, (TreeNode)node);
	}

	/// <summary>Removes the specified tree node from the tree node collection.</summary>
	/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to remove from the collection.</param>
	void IList.Remove(object node)
	{
		Remove((TreeNode)node);
	}

	/// <summary>Adds a new tree node with the specified label text to the end of the current tree node collection.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the tree node being added to the collection.</returns>
	/// <param name="text">The label text displayed by the <see cref="T:System.Windows.Forms.TreeNode" />. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual TreeNode Add(string text)
	{
		TreeNode treeNode = new TreeNode(text);
		Add(treeNode);
		return treeNode;
	}

	/// <summary>Adds a previously created tree node to the end of the tree node collection.</summary>
	/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.TreeNode" /> added to the tree node collection.</returns>
	/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to add to the collection. </param>
	/// <exception cref="T:System.ArgumentException">The <paramref name="node" /> is currently assigned to another <see cref="T:System.Windows.Forms.TreeView" />. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual int Add(TreeNode node)
	{
		if (node == null)
		{
			throw new ArgumentNullException("node");
		}
		TreeView treeView = null;
		if (owner != null)
		{
			treeView = owner.TreeView;
		}
		int result;
		if (treeView != null && treeView.Sorted)
		{
			result = AddSorted(node);
		}
		else
		{
			if (count >= nodes.Length)
			{
				Grow();
			}
			nodes[count] = node;
			result = count;
			count++;
		}
		SetupNode(node);
		treeView?.OnUIACollectionChanged(owner, new CollectionChangeEventArgs(CollectionChangeAction.Add, node));
		return result;
	}

	/// <summary>Creates a new tree node with the specified key and text, and adds it to the collection.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was added to the collection.</returns>
	/// <param name="key">The name of the tree node.</param>
	/// <param name="text">The text to display in the tree node.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual TreeNode Add(string key, string text)
	{
		TreeNode treeNode = new TreeNode(text);
		treeNode.Name = key;
		Add(treeNode);
		return treeNode;
	}

	/// <summary>Creates a tree node with the specified key, text, and image, and adds it to the collection.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was added to the collection.</returns>
	/// <param name="key">The name of the tree node.</param>
	/// <param name="text">The text to display in the tree node.</param>
	/// <param name="imageIndex">The index of the image to display in the tree node.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual TreeNode Add(string key, string text, int imageIndex)
	{
		TreeNode treeNode = Add(key, text);
		treeNode.ImageIndex = imageIndex;
		return treeNode;
	}

	/// <summary>Creates a tree node with the specified key, text, and image, and adds it to the collection.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was added to the collection.</returns>
	/// <param name="key">The name of the tree node.</param>
	/// <param name="text">The text to display in the tree node.</param>
	/// <param name="imageKey">The image to display in the tree node.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual TreeNode Add(string key, string text, string imageKey)
	{
		TreeNode treeNode = Add(key, text);
		treeNode.ImageKey = imageKey;
		return treeNode;
	}

	/// <summary>Creates a tree node with the specified key, text, and images, and adds it to the collection.</summary>
	/// <returns>The tree node that was added to the collection.</returns>
	/// <param name="key">The name of the tree node.</param>
	/// <param name="text">The text to display in the tree node.</param>
	/// <param name="imageIndex">The index of the image to display in the tree node.</param>
	/// <param name="selectedImageIndex">The index of the image to be displayed in the tree node when it is in a selected state.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual TreeNode Add(string key, string text, int imageIndex, int selectedImageIndex)
	{
		TreeNode treeNode = Add(key, text);
		treeNode.ImageIndex = imageIndex;
		treeNode.SelectedImageIndex = selectedImageIndex;
		return treeNode;
	}

	/// <summary>Creates a tree node with the specified key, text, and images, and adds it to the collection.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was added to the collection.</returns>
	/// <param name="key">The name of the tree node.</param>
	/// <param name="text">The text to display in the tree node.</param>
	/// <param name="imageKey">The key of the image to display in the tree node.</param>
	/// <param name="selectedImageKey">The key of the image to display when the node is in a selected state.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual TreeNode Add(string key, string text, string imageKey, string selectedImageKey)
	{
		TreeNode treeNode = Add(key, text);
		treeNode.ImageKey = imageKey;
		treeNode.SelectedImageKey = selectedImageKey;
		return treeNode;
	}

	/// <summary>Adds an array of previously created tree nodes to the collection.</summary>
	/// <param name="nodes">An array of <see cref="T:System.Windows.Forms.TreeNode" /> objects representing the tree nodes to add to the collection. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="nodes" /> is null.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="nodes" /> is the child of another <see cref="T:System.Windows.Forms.TreeView" />.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void AddRange(TreeNode[] nodes)
	{
		if (nodes == null)
		{
			throw new ArgumentNullException("nodes");
		}
		for (int i = 0; i < nodes.Length; i++)
		{
			Add(nodes[i]);
		}
	}

	/// <summary>Removes all tree nodes from the collection.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void Clear()
	{
		while (count > 0)
		{
			RemoveAt(0, update: false);
		}
		Array.Clear(nodes, 0, count);
		count = 0;
		TreeView treeView = null;
		if (owner != null)
		{
			treeView = owner.TreeView;
			if (treeView != null)
			{
				treeView.UpdateBelow(owner);
				treeView.RecalculateVisibleOrder(owner);
				treeView.UpdateScrollBars(force: false);
			}
		}
	}

	/// <summary>Determines whether the specified tree node is a member of the collection.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.TreeNode" /> is a member of the collection; otherwise, false.</returns>
	/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to locate in the collection. </param>
	/// <filterpriority>1</filterpriority>
	public bool Contains(TreeNode node)
	{
		return Array.IndexOf(nodes, node, 0, count) != -1;
	}

	/// <summary>Determines whether the collection contains a tree node with the specified key.</summary>
	/// <returns>true to indicate the collection contains a <see cref="T:System.Windows.Forms.TreeNode" /> with the specified key; otherwise, false. </returns>
	/// <param name="key">The name of the <see cref="T:System.Windows.Forms.TreeNode" /> to search for.</param>
	/// <filterpriority>1</filterpriority>
	public virtual bool ContainsKey(string key)
	{
		for (int i = 0; i < count; i++)
		{
			if (string.Compare(nodes[i].Name, key, ignoreCase: true, CultureInfo.InvariantCulture) == 0)
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
	/// <param name="dest">The destination array. </param>
	/// <param name="index">The index in the destination array at which storing begins. </param>
	/// <filterpriority>1</filterpriority>
	public void CopyTo(Array dest, int index)
	{
		Array.Copy(nodes, index, dest, index, count);
	}

	/// <summary>Returns an enumerator that can be used to iterate through the tree node collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the tree node collection.</returns>
	/// <filterpriority>1</filterpriority>
	public IEnumerator GetEnumerator()
	{
		return new TreeNodeEnumerator(this);
	}

	/// <summary>Returns the index of the specified tree node in the collection.</summary>
	/// <returns>The zero-based index of the item found in the tree node collection; otherwise, -1.</returns>
	/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to locate in the collection. </param>
	/// <filterpriority>1</filterpriority>
	public int IndexOf(TreeNode node)
	{
		return Array.IndexOf(nodes, node);
	}

	/// <summary>Returns the index of the first occurrence of a tree node with the specified key.</summary>
	/// <returns>The zero-based index of the first occurrence of a tree node with the specified key, if found; otherwise, -1.</returns>
	/// <param name="key">The name of the tree node to search for.</param>
	/// <filterpriority>1</filterpriority>
	public virtual int IndexOfKey(string key)
	{
		for (int i = 0; i < count; i++)
		{
			if (string.Compare(nodes[i].Name, key, ignoreCase: true, CultureInfo.InvariantCulture) == 0)
			{
				return i;
			}
		}
		return -1;
	}

	/// <summary>Creates a tree node with the specified text and inserts it at the specified index.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was inserted in the collection.</returns>
	/// <param name="index">The location within the collection to insert the node.</param>
	/// <param name="text">The text to display in the tree node.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual TreeNode Insert(int index, string text)
	{
		TreeNode treeNode = new TreeNode(text);
		Insert(index, treeNode);
		return treeNode;
	}

	/// <summary>Inserts an existing tree node into the tree node collection at the specified location.</summary>
	/// <param name="index">The indexed location within the collection to insert the tree node. </param>
	/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to insert into the collection. </param>
	/// <exception cref="T:System.ArgumentException">The <paramref name="node" /> is currently assigned to another <see cref="T:System.Windows.Forms.TreeView" />. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void Insert(int index, TreeNode node)
	{
		if (count >= nodes.Length)
		{
			Grow();
		}
		Array.Copy(nodes, index, nodes, index + 1, count - index);
		nodes[index] = node;
		count++;
		SetupNode(node);
	}

	/// <summary>Creates a tree node with the specified text and key, and inserts it into the collection.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was inserted in the collection.</returns>
	/// <param name="index">The location within the collection to insert the node.</param>
	/// <param name="key">The name of the tree node.</param>
	/// <param name="text">The text to display in the tree node.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual TreeNode Insert(int index, string key, string text)
	{
		TreeNode treeNode = new TreeNode(text);
		treeNode.Name = key;
		Insert(index, treeNode);
		return treeNode;
	}

	/// <summary>Creates a tree node with the specified key, text, and image, and inserts it into the collection at the specified index.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was inserted in the collection.</returns>
	/// <param name="index">The location within the collection to insert the node.</param>
	/// <param name="key">The name of the tree node.</param>
	/// <param name="text">The text to display in the tree node.</param>
	/// <param name="imageIndex">The index of the image to display in the tree node.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual TreeNode Insert(int index, string key, string text, int imageIndex)
	{
		TreeNode treeNode = new TreeNode(text);
		treeNode.Name = key;
		treeNode.ImageIndex = imageIndex;
		Insert(index, treeNode);
		return treeNode;
	}

	/// <summary>Creates a tree node with the specified key, text, and image, and inserts it into the collection at the specified index.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was inserted in the collection.</returns>
	/// <param name="index">The location within the collection to insert the node.</param>
	/// <param name="key">The name of the tree node.</param>
	/// <param name="text">The text to display in the tree node.</param>
	/// <param name="imageKey">The key of the image to display in the tree node.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual TreeNode Insert(int index, string key, string text, string imageKey)
	{
		TreeNode treeNode = new TreeNode(text);
		treeNode.Name = key;
		treeNode.ImageKey = imageKey;
		Insert(index, treeNode);
		return treeNode;
	}

	/// <summary>Creates a tree node with the specified key, text, and images, and inserts it into the collection at the specified index.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was inserted in the collection.</returns>
	/// <param name="index">The location within the collection to insert the node.</param>
	/// <param name="key">The name of the tree node.</param>
	/// <param name="text">The text to display in the tree node.</param>
	/// <param name="imageIndex">The index of the image to display in the tree node.</param>
	/// <param name="selectedImageIndex">The index of the image to display in the tree node when it is in a selected state.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual TreeNode Insert(int index, string key, string text, int imageIndex, int selectedImageIndex)
	{
		TreeNode treeNode = new TreeNode(text, imageIndex, selectedImageIndex);
		treeNode.Name = key;
		Insert(index, treeNode);
		return treeNode;
	}

	/// <summary>Creates a tree node with the specified key, text, and images, and inserts it into the collection at the specified index.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was inserted in the collection.</returns>
	/// <param name="index">The location within the collection to insert the node.</param>
	/// <param name="key">The name of the tree node.</param>
	/// <param name="text">The text to display in the tree node.</param>
	/// <param name="imageKey">The key of the image to display in the tree node.</param>
	/// <param name="selectedImageKey">The key of the image to display in the tree node when it is in a selected state.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual TreeNode Insert(int index, string key, string text, string imageKey, string selectedImageKey)
	{
		TreeNode treeNode = new TreeNode(text);
		treeNode.Name = key;
		treeNode.ImageKey = imageKey;
		treeNode.SelectedImageKey = selectedImageKey;
		Insert(index, treeNode);
		return treeNode;
	}

	/// <summary>Removes the specified tree node from the tree node collection.</summary>
	/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to remove. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Remove(TreeNode node)
	{
		if (node == null)
		{
			throw new NullReferenceException();
		}
		int num = IndexOf(node);
		if (num != -1)
		{
			RemoveAt(num);
		}
	}

	/// <summary>Removes a tree node from the tree node collection at a specified index.</summary>
	/// <param name="index">The index of the <see cref="T:System.Windows.Forms.TreeNode" /> to remove. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void RemoveAt(int index)
	{
		RemoveAt(index, update: true);
	}

	private void RemoveAt(int index, bool update)
	{
		TreeNode treeNode = nodes[index];
		TreeNode prevNode = GetPrevNode(treeNode);
		TreeNode selectedNode = null;
		bool flag = false;
		bool isVisible = treeNode.IsVisible;
		TreeView treeView = null;
		if (owner != null)
		{
			treeView = owner.TreeView;
		}
		if (treeView != null)
		{
			treeView.RecalculateVisibleOrder(prevNode);
			if (treeNode == treeView.SelectedNode)
			{
				flag = true;
				OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(treeNode);
				if (openTreeNodeEnumerator.MoveNext() && openTreeNodeEnumerator.MoveNext())
				{
					selectedNode = openTreeNodeEnumerator.CurrentNode;
				}
				else
				{
					openTreeNodeEnumerator = new OpenTreeNodeEnumerator(treeNode);
					openTreeNodeEnumerator.MovePrevious();
					selectedNode = ((openTreeNodeEnumerator.CurrentNode != treeNode) ? openTreeNodeEnumerator.CurrentNode : null);
				}
			}
		}
		Array.Copy(nodes, index + 1, nodes, index, count - index - 1);
		count--;
		nodes[count] = null;
		if (nodes.Length > OrigSize && nodes.Length > count * 2)
		{
			Shrink();
		}
		if (treeView != null && flag)
		{
			treeView.SelectedNode = selectedNode;
		}
		TreeNode parent = treeNode.parent;
		treeNode.parent = null;
		if (update && treeView != null && isVisible)
		{
			treeView.RecalculateVisibleOrder(prevNode);
			treeView.UpdateScrollBars(force: false);
			treeView.UpdateBelow(parent);
		}
		treeView?.OnUIACollectionChanged(owner, new CollectionChangeEventArgs(CollectionChangeAction.Remove, treeNode));
	}

	/// <summary>Removes the tree node with the specified key from the collection.</summary>
	/// <param name="key">The name of the tree node to remove from the collection.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void RemoveByKey(string key)
	{
		TreeNode treeNode = this[key];
		if (treeNode != null)
		{
			Remove(treeNode);
		}
	}

	private TreeNode GetPrevNode(TreeNode node)
	{
		OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(node);
		if (openTreeNodeEnumerator.MovePrevious() && openTreeNodeEnumerator.MovePrevious())
		{
			return openTreeNodeEnumerator.CurrentNode;
		}
		return null;
	}

	private void SetupNode(TreeNode node)
	{
		node.Remove();
		node.parent = owner;
		TreeView treeView = null;
		if (owner != null)
		{
			treeView = owner.TreeView;
		}
		if (treeView != null)
		{
			TreeNode prevNode = GetPrevNode(node);
			if (treeView.IsHandleCreated && node.ArePreviousNodesExpanded)
			{
				treeView.RecalculateVisibleOrder(prevNode);
			}
			if (owner == treeView.root_node || (node.Parent.IsVisible && node.Parent.IsExpanded))
			{
				treeView.UpdateScrollBars(force: false);
			}
		}
		if (owner != null && treeView != null && (owner.IsExpanded || owner.IsRoot))
		{
			treeView.UpdateBelow(owner);
		}
		else if (owner != null)
		{
			treeView?.UpdateBelow(owner);
		}
	}

	private int AddSorted(TreeNode node)
	{
		if (count >= nodes.Length)
		{
			Grow();
		}
		CompareInfo compareInfo = Application.CurrentCulture.CompareInfo;
		int num = 0;
		bool flag = false;
		for (int i = 0; i < count; i++)
		{
			num = i;
			int num2 = compareInfo.Compare(node.Text, nodes[i].Text);
			if (num2 < 0)
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			num = count;
		}
		for (int num3 = count - 1; num3 >= num; num3--)
		{
			nodes[num3 + 1] = nodes[num3];
		}
		count++;
		nodes[num] = node;
		return count;
	}

	internal void Sort(IComparer sorter)
	{
		TreeNode[] array = nodes;
		int length = count;
		IComparer comparer2;
		if (sorter == null)
		{
			IComparer comparer = new TreeNodeComparer(Application.CurrentCulture.CompareInfo);
			comparer2 = comparer;
		}
		else
		{
			comparer2 = sorter;
		}
		Array.Sort(array, 0, length, comparer2);
		for (int i = 0; i < count; i++)
		{
			nodes[i].Nodes.Sort(sorter);
		}
	}

	private void Grow()
	{
		TreeNode[] destinationArray = new TreeNode[nodes.Length + 50];
		Array.Copy(nodes, destinationArray, nodes.Length);
		nodes = destinationArray;
	}

	private void Shrink()
	{
		int num = ((count + 1 <= OrigSize) ? OrigSize : (count + 1));
		TreeNode[] destinationArray = new TreeNode[num];
		Array.Copy(nodes, destinationArray, count);
		nodes = destinationArray;
	}

	/// <summary>Finds the tree nodes with specified key, optionally searching subnodes.</summary>
	/// <returns>An array of <see cref="T:System.Windows.Forms.TreeNode" /> objects whose <see cref="P:System.Windows.Forms.TreeNode.Name" /> property matches the specified key.</returns>
	/// <param name="key">The name of the tree node to search for.</param>
	/// <param name="searchAllChildren">true to search child nodes of tree nodes; otherwise, false. </param>
	/// <filterpriority>1</filterpriority>
	public TreeNode[] Find(string key, bool searchAllChildren)
	{
		List<TreeNode> list = new List<TreeNode>(0);
		Find(key, searchAllChildren, this, list);
		return list.ToArray();
	}

	private static void Find(string key, bool searchAllChildren, TreeNodeCollection nodes, List<TreeNode> results)
	{
		for (int i = 0; i < nodes.Count; i++)
		{
			TreeNode treeNode = nodes[i];
			if (string.Compare(treeNode.Name, key, ignoreCase: true, CultureInfo.InvariantCulture) == 0)
			{
				results.Add(treeNode);
			}
		}
		if (!searchAllChildren)
		{
			return;
		}
		for (int j = 0; j < nodes.Count; j++)
		{
			TreeNodeCollection treeNodeCollection = nodes[j].Nodes;
			if (treeNodeCollection.Count > 0)
			{
				Find(key, searchAllChildren, treeNodeCollection, results);
			}
		}
	}
}
