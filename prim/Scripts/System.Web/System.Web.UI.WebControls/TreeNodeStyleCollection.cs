using System.Collections;

namespace System.Web.UI.WebControls;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> objects that is in a <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
public sealed class TreeNodeStyleCollection : StateManagedCollection
{
	private static Type[] types = new Type[1] { typeof(TreeNodeStyle) };

	/// <summary>Gets or sets the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" /> object.</summary>
	/// <param name="i">The zero-based index of the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to retrieve. </param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> at the specified index in the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" />.</returns>
	public TreeNodeStyle this[int i]
	{
		get
		{
			return (TreeNodeStyle)((IList)this)[i];
		}
		set
		{
			((IList)this)[i] = value;
		}
	}

	internal TreeNodeStyleCollection()
	{
	}

	/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object to the end of the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" /> object.</summary>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to append. </param>
	/// <returns>The position into which the new <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> was inserted.</returns>
	public int Add(TreeNodeStyle style)
	{
		style.Font.Underline = style.Font.Underline;
		return ((IList)this).Add((object)style);
	}

	/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object is in the collection.</summary>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to find. </param>
	/// <returns>
	///     <see langword="true" />, if the specified <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object is contained in the collection; otherwise, <see langword="false" />.</returns>
	public bool Contains(TreeNodeStyle style)
	{
		return ((IList)this).Contains((object)style);
	}

	/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" /> object to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> objects, starting at the specified index in the target array.</summary>
	/// <param name="styleArray">A zero-based array of <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> objects that receives the copied items from the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" />.</param>
	/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
	public void CopyTo(TreeNodeStyle[] styleArray, int index)
	{
		((ICollection)this).CopyTo((Array)styleArray, index);
	}

	protected override object CreateKnownType(int index)
	{
		return new TreeNodeStyle();
	}

	protected override Type[] GetKnownTypes()
	{
		return types;
	}

	/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object in the collection.</summary>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to locate.</param>
	/// <returns>The zero-based index of the first occurrence of <paramref name="style" /> within the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" />, if found; otherwise, -1.</returns>
	public int IndexOf(TreeNodeStyle style)
	{
		return ((IList)this).IndexOf((object)style);
	}

	/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object into the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" /> object at the specified index location.</summary>
	/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" />. </param>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to insert. </param>
	public void Insert(int index, TreeNodeStyle style)
	{
		((IList)this).Insert(index, (object)style);
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object from the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" /> object.</summary>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to remove. </param>
	public void Remove(TreeNodeStyle style)
	{
		((IList)this).Remove((object)style);
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object at the specified index location from the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" /> object.</summary>
	/// <param name="index">The zero-based index location of the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to remove. </param>
	public void RemoveAt(int index)
	{
		((IList)this).RemoveAt(index);
	}

	protected override void SetDirtyObject(object o)
	{
		((TreeNodeStyle)o).SetDirty();
	}

	protected override void OnInsert(int index, object value)
	{
		base.OnInsert(index, value);
	}
}
