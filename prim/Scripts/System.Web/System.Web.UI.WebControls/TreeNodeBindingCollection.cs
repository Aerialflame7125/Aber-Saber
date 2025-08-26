using System.Collections;

namespace System.Web.UI.WebControls;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> objects in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control. This class cannot be inherited.</summary>
public sealed class TreeNodeBindingCollection : StateManagedCollection
{
	private static Type[] types = new Type[1] { typeof(TreeNodeBinding) };

	/// <summary>Gets or sets the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> object.</summary>
	/// <param name="i">The zero-based index of the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to retrieve. </param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> at the specified index in the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" />.</returns>
	public TreeNodeBinding this[int i]
	{
		get
		{
			return (TreeNodeBinding)((IList)this)[i];
		}
		set
		{
			((IList)this)[i] = value;
		}
	}

	internal TreeNodeBindingCollection()
	{
	}

	/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object to the end of the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> object.</summary>
	/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to append. </param>
	/// <returns>The zero-based index of the location of the added <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> in the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" />.</returns>
	public int Add(TreeNodeBinding binding)
	{
		return ((IList)this).Add((object)binding);
	}

	/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is in the collection.</summary>
	/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to find.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> is in the collection; otherwise, <see langword="false" />.</returns>
	public bool Contains(TreeNodeBinding binding)
	{
		return ((IList)this).Contains((object)binding);
	}

	/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> object to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> objects, starting at the specified index in the target array.</summary>
	/// <param name="bindingArray">A zero-based array of <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> objects that receives the copied items from the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" />.</param>
	/// <param name="index">The position in <paramref name="bindingArray" /> at which to start receiving the copied content.</param>
	public void CopyTo(TreeNodeBinding[] bindingArray, int index)
	{
		((ICollection)this).CopyTo((Array)bindingArray, index);
	}

	protected override object CreateKnownType(int index)
	{
		return new TreeNodeBinding();
	}

	protected override Type[] GetKnownTypes()
	{
		return types;
	}

	/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object in the collection.</summary>
	/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to locate.</param>
	/// <returns>The zero-based index of the first occurrence of <paramref name="binding" /> within the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" />, if found; otherwise, -1.</returns>
	public int IndexOf(TreeNodeBinding binding)
	{
		return ((IList)this).IndexOf((object)binding);
	}

	/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object into the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> object at the specified index location.</summary>
	/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" />. </param>
	/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to insert. </param>
	public void Insert(int index, TreeNodeBinding binding)
	{
		((IList)this).Insert(index, (object)binding);
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object from the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> object.</summary>
	/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to remove. </param>
	public void Remove(TreeNodeBinding binding)
	{
		((IList)this).Remove((object)binding);
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object at the specified index location from the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> object.</summary>
	/// <param name="index">The zero-based index location of the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to remove. </param>
	public void RemoveAt(int index)
	{
		((IList)this).RemoveAt(index);
	}

	protected override void SetDirtyObject(object o)
	{
		((TreeNodeBinding)o).SetDirty();
	}

	protected override void OnClear()
	{
		base.OnClear();
	}

	protected override void OnRemoveComplete(int index, object value)
	{
		base.OnRemoveComplete(index, value);
	}

	protected override void OnValidate(object value)
	{
		base.OnValidate(value);
	}
}
