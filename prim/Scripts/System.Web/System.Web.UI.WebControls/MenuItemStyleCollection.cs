using System.Collections;

namespace System.Web.UI.WebControls;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> objects in a <see cref="T:System.Web.UI.WebControls.Menu" /> control. This class cannot be inherited.</summary>
public sealed class MenuItemStyleCollection : StateManagedCollection
{
	private static Type[] types = new Type[1] { typeof(MenuItemStyle) };

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object at the specified index from the collection.</summary>
	/// <param name="i">The zero-based index of the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" />  to retrieve.</param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> at the specified index in the collection.</returns>
	public MenuItemStyle this[int i]
	{
		get
		{
			return (MenuItemStyle)((IList)this)[i];
		}
		set
		{
			((IList)this)[i] = value;
		}
	}

	internal MenuItemStyleCollection()
	{
	}

	/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object to the end of the current collection.</summary>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> to append to the end of the current collection.</param>
	/// <returns>The zero-based index of the added <see cref="T:System.Web.UI.WebControls.MenuItemStyle" />.</returns>
	public int Add(MenuItemStyle style)
	{
		return ((IList)this).Add((object)style);
	}

	/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object is in the collection.</summary>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.MenuItemStyle" />  to find.</param>
	/// <returns>
	///     <see langword="true" /> if the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> is contained in the collection; otherwise, <see langword="false" />.</returns>
	public bool Contains(MenuItemStyle style)
	{
		return ((IList)this).Contains((object)style);
	}

	/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.MenuItemStyleCollection" /> object to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> objects, starting at the specified index in the target array.</summary>
	/// <param name="styleArray">A zero-based array of <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> objects that received the copied items from the <see cref="T:System.Web.UI.WebControls.MenuItemStyleCollection" />.</param>
	/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
	public void CopyTo(MenuItemStyle[] styleArray, int index)
	{
		((ICollection)this).CopyTo((Array)styleArray, index);
	}

	protected override object CreateKnownType(int index)
	{
		return new MenuItemStyle();
	}

	protected override Type[] GetKnownTypes()
	{
		return types;
	}

	/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object in the collection.</summary>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> to locate.</param>
	/// <returns>The zero-based index of the first occurrence of the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" />, if found; otherwise, -1.</returns>
	public int IndexOf(MenuItemStyle style)
	{
		return ((IList)this).IndexOf((object)style);
	}

	/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object into the collection at the specified index location.</summary>
	/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" />.</param>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> to insert.</param>
	public void Insert(int index, MenuItemStyle style)
	{
		((IList)this).Insert(index, (object)style);
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object from the collection.</summary>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> to remove.</param>
	public void Remove(MenuItemStyle style)
	{
		((IList)this).Remove((object)style);
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object at the specified index location from the collection.</summary>
	/// <param name="index">The zero-based index location of the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> to remove.</param>
	public void RemoveAt(int index)
	{
		((IList)this).RemoveAt(index);
	}

	protected override void SetDirtyObject(object o)
	{
		((MenuItemStyle)o).SetDirty();
	}

	protected override void OnInsert(int index, object value)
	{
		base.OnInsert(index, value);
	}
}
