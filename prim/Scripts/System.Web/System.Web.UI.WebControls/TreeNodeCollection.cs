using System.Collections;

namespace System.Web.UI.WebControls;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.TreeNode" /> objects in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control. This class cannot be inherited.</summary>
public sealed class TreeNodeCollection : ICollection, IEnumerable, IStateManager
{
	private ArrayList items = new ArrayList();

	private TreeView tree;

	private TreeNode parent;

	private bool marked;

	private bool dirty;

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to retrieve. </param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />.</returns>
	public TreeNode this[int index] => (TreeNode)items[index];

	/// <summary>Gets the number of items in the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
	/// <returns>The number of items in the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />.</returns>
	public int Count => items.Count;

	/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> is synchronized (thread safe).</summary>
	/// <returns>
	///     <see langword="false" />.</returns>
	public bool IsSynchronized => false;

	/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
	/// <returns>A <see cref="T:System.Object" /> that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />.</returns>
	public object SyncRoot => items;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object is saving changes to its view state.</summary>
	/// <returns>
	///     <see langword="true" /> if the control is marked to save its state; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => marked;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> class using the default values.</summary>
	public TreeNodeCollection()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> class using the specified parent node (or owner).</summary>
	/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object that represents the parent node of the collection. </param>
	public TreeNodeCollection(TreeNode owner)
	{
		parent = owner;
		tree = owner.Tree;
	}

	internal TreeNodeCollection(TreeView tree)
	{
		this.tree = tree;
	}

	internal void SetTree(TreeView tree)
	{
		this.tree = tree;
		foreach (TreeNode item in items)
		{
			item.Tree = tree;
		}
	}

	/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to the end of the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
	/// <param name="child">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to append. </param>
	public void Add(TreeNode child)
	{
		Add(child, updateParent: true);
	}

	internal void Add(TreeNode child, bool updateParent)
	{
		int index = items.Add(child);
		if (parent != null)
		{
			parent.HadChildrenBeforePopulating = true;
		}
		if (updateParent)
		{
			child.Index = index;
			child.SetParent(parent);
			child.Tree = tree;
			if (marked)
			{
				((IStateManager)child).TrackViewState();
				SetDirty();
			}
		}
	}

	/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object in a <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object at the specified index location.</summary>
	/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object. </param>
	/// <param name="child">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to add. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="child" /> is <see langword="null" />.</exception>
	public void AddAt(int index, TreeNode child)
	{
		items.Insert(index, child);
		child.Index = index;
		child.SetParent(parent);
		child.Tree = tree;
		for (int i = index + 1; i < items.Count; i++)
		{
			((TreeNode)items[i]).Index = i;
		}
		if (marked)
		{
			((IStateManager)child).TrackViewState();
			SetDirty();
		}
	}

	internal void SetDirty()
	{
		for (int i = 0; i < Count; i++)
		{
			this[i].SetDirty();
		}
		dirty = true;
	}

	/// <summary>Empties the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
	public void Clear()
	{
		if (tree != null || parent != null)
		{
			foreach (TreeNode item in items)
			{
				item.Tree = null;
				item.SetParent(null);
			}
		}
		items.Clear();
		if (marked)
		{
			dirty = true;
		}
	}

	/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object is in the collection.</summary>
	/// <param name="c">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to find. </param>
	/// <returns>
	///     <see langword="true" /> if the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object is contained in the collection; otherwise, <see langword="false" />.</returns>
	public bool Contains(TreeNode c)
	{
		return items.Contains(c);
	}

	/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.TreeNode" /> objects, starting at the specified index in the target array.</summary>
	/// <param name="nodeArray">A zero-based array of <see cref="T:System.Web.UI.WebControls.TreeNode" /> objects that receives the copied items from the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />.</param>
	/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
	public void CopyTo(TreeNode[] nodeArray, int index)
	{
		items.CopyTo(nodeArray, index);
	}

	/// <summary>Returns an enumerator that can be used to iterate through a <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
	/// <returns>An enumerator that can be used to iterate through the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />.</returns>
	public IEnumerator GetEnumerator()
	{
		return items.GetEnumerator();
	}

	/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object.</summary>
	/// <param name="value">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to locate. </param>
	/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />, if found; otherwise, -1.</returns>
	public int IndexOf(TreeNode value)
	{
		return items.IndexOf(value);
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object from the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
	/// <param name="value">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to remove. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="value" /> is <see langword="null" />.</exception>
	public void Remove(TreeNode value)
	{
		int num = IndexOf(value);
		if (num != -1)
		{
			items.RemoveAt(num);
			if (tree != null)
			{
				value.Tree = null;
			}
			if (marked)
			{
				SetDirty();
			}
		}
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object at the specified index location from the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
	/// <param name="index">The zero-based index location of the node to remove. </param>
	public void RemoveAt(int index)
	{
		TreeNode treeNode = (TreeNode)items[index];
		items.RemoveAt(index);
		if (tree != null)
		{
			treeNode.Tree = null;
		}
		if (marked)
		{
			SetDirty();
		}
	}

	/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index in the target array.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> object that receives the copied items from the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />. </param>
	/// <param name="index">The position in the target array at which to start receiving the copied content. </param>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="array" /> is not an array of <see cref="T:System.Web.UI.WebControls.TreeNode" /> objects.</exception>
	void ICollection.CopyTo(Array array, int index)
	{
		items.CopyTo(array, index);
	}

	/// <summary>Loads the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object's previously saved view state.</summary>
	/// <param name="state">A <see cref="T:System.Object" /> that contains the saved view state values. </param>
	void IStateManager.LoadViewState(object state)
	{
		if (state == null)
		{
			return;
		}
		object[] array = (object[])state;
		dirty = (bool)array[0];
		if (dirty)
		{
			items.Clear();
			for (int i = 1; i < array.Length; i++)
			{
				if (!(array[i] is Pair pair))
				{
					throw new InvalidOperationException("Broken view state (item " + i + ")");
				}
				TreeNode treeNode = ((!(pair.First as Type == null)) ? (Activator.CreateInstance(pair.First as Type) as TreeNode) : new TreeNode());
				Add(treeNode);
				object second = pair.Second;
				if (second != null)
				{
					((IStateManager)treeNode).LoadViewState(second);
				}
			}
			return;
		}
		for (int j = 1; j < array.Length; j++)
		{
			if (!(array[j] is Pair pair2))
			{
				throw new InvalidOperationException("Broken view state " + j + ")");
			}
			int index = (int)pair2.First;
			((IStateManager)(TreeNode)items[index]).LoadViewState(pair2.Second);
		}
	}

	/// <summary>Saves the changes to view state to a <see cref="T:System.Object" />.</summary>
	/// <returns>The <see cref="T:System.Object" /> that contains the view state changes.</returns>
	object IStateManager.SaveViewState()
	{
		object[] array = null;
		bool flag = false;
		if (dirty)
		{
			if (items.Count > 0)
			{
				flag = true;
				array = new object[items.Count + 1];
				array[0] = true;
				for (int i = 0; i < items.Count; i++)
				{
					TreeNode obj = items[i] as TreeNode;
					object y = ((IStateManager)obj).SaveViewState();
					Type type = obj.GetType();
					array[i + 1] = new Pair((type == typeof(TreeNode)) ? null : type, y);
				}
			}
		}
		else
		{
			ArrayList arrayList = new ArrayList();
			for (int j = 0; j < items.Count; j++)
			{
				object obj2 = ((IStateManager)(items[j] as TreeNode)).SaveViewState();
				if (obj2 != null)
				{
					flag = true;
					arrayList.Add(new Pair(j, obj2));
				}
			}
			if (flag)
			{
				arrayList.Insert(0, false);
				array = arrayList.ToArray();
			}
		}
		if (flag)
		{
			return array;
		}
		return null;
	}

	/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> to track changes to its view state.</summary>
	void IStateManager.TrackViewState()
	{
		marked = true;
		for (int i = 0; i < items.Count; i++)
		{
			((IStateManager)items[i]).TrackViewState();
		}
	}
}
