using System.Collections;

namespace System.Web.UI.WebControls;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> objects.</summary>
public sealed class MenuItemBindingCollection : StateManagedCollection
{
	private static Type[] types = new Type[1] { typeof(MenuItemBinding) };

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object at the specified index from the collection.</summary>
	/// <param name="i">The zero-based index of the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> to retrieve.</param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> at the specified index in the collection.</returns>
	public MenuItemBinding this[int i]
	{
		get
		{
			return (MenuItemBinding)((IList)this)[i];
		}
		set
		{
			((IList)this)[i] = value;
		}
	}

	internal MenuItemBindingCollection()
	{
	}

	/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object to the end of the collection.</summary>
	/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> to append to the end of the collection.</param>
	/// <returns>The index at which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> was inserted in the collection.</returns>
	public int Add(MenuItemBinding binding)
	{
		return ((IList)this).Add((object)binding);
	}

	/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is in the collection.</summary>
	/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> to find.</param>
	/// <returns>
	///     <see langword="true" /> if the specified <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is contained in the collection; otherwise, <see langword="false" />.</returns>
	public bool Contains(MenuItemBinding binding)
	{
		return ((IList)this).Contains((object)binding);
	}

	/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.MenuItemBindingCollection" /> object to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> objects, starting at the specified index in the target array.</summary>
	/// <param name="array">A zero-based array of <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> objects that receives the copied items from the collection.</param>
	/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
	public void CopyTo(MenuItemBinding[] array, int index)
	{
		((ICollection)this).CopyTo((Array)array, index);
	}

	protected override object CreateKnownType(int index)
	{
		return new MenuItemBinding();
	}

	protected override Type[] GetKnownTypes()
	{
		return types;
	}

	/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object in the collection.</summary>
	/// <param name="value">The <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> to determine the index of.</param>
	/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the collection, if found; otherwise, -1.</returns>
	public int IndexOf(MenuItemBinding value)
	{
		return ((IList)this).IndexOf((object)value);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object to the collection at the specified index location.</summary>
	/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" />.</param>
	/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> to insert.</param>
	public void Insert(int index, MenuItemBinding binding)
	{
		((IList)this).Insert(index, (object)binding);
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object from the collection.</summary>
	/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> to remove from the collection.</param>
	public void Remove(MenuItemBinding binding)
	{
		((IList)this).Remove((object)binding);
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object at the specified index location from the collection.</summary>
	/// <param name="index">The zero-based index location of the menu item binding to remove.</param>
	public void RemoveAt(int index)
	{
		((IList)this).RemoveAt(index);
	}

	protected override void SetDirtyObject(object o)
	{
		((MenuItemBinding)o).SetDirty();
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
