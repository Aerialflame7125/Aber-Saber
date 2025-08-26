using System.Collections;

namespace System.ComponentModel.Design;

/// <summary>Represents a collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects.</summary>
public class DesignerActionItemCollection : CollectionBase
{
	/// <summary>Gets or sets the element at the specified index.</summary>
	/// <param name="index">The zero-based index of the element.</param>
	/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> at the specified index.</returns>
	public DesignerActionItem this[int index]
	{
		get
		{
			return (DesignerActionItem)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionItemCollection" /> class.</summary>
	public DesignerActionItemCollection()
	{
	}

	/// <summary>Adds the supplied <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> to the current collection.</summary>
	/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> to add.</param>
	/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionItemCollection" /> index at which the value has been added.</returns>
	public int Add(DesignerActionItem value)
	{
		return base.List.Add(value);
	}

	/// <summary>Determines whether the <see cref="T:System.ComponentModel.Design.DesignerActionItemCollection" /> contains a specific element.</summary>
	/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> to locate in the <see cref="T:System.ComponentModel.Design.DesignerActionItemCollection" />.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.ComponentModel.Design.DesignerActionItemCollection" /> contains the specified value; otherwise, <see langword="false" />.</returns>
	public bool Contains(DesignerActionItem value)
	{
		return base.List.Contains(value);
	}

	/// <summary>Copies the elements of the current collection into the supplied array, starting at the specified array index.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that is the destination of the elements copied from the current collection. The array must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	public void CopyTo(DesignerActionItem[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	/// <summary>Determines the index of a specific item in the collection.</summary>
	/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> to locate in the collection.</param>
	/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.ComponentModel.Design.DesignerActionItemCollection" />, if found; otherwise, -1.</returns>
	public int IndexOf(DesignerActionItem value)
	{
		return base.List.IndexOf(value);
	}

	/// <summary>Inserts an element into the <see cref="T:System.ComponentModel.Design.DesignerActionItemCollection" /> at the specified index.</summary>
	/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
	/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> to insert.</param>
	public void Insert(int index, DesignerActionItem value)
	{
		base.List.Insert(index, value);
	}

	/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.ComponentModel.Design.DesignerActionItemCollection" />.</summary>
	/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> to remove from the <see cref="T:System.ComponentModel.Design.DesignerActionItemCollection" />.</param>
	public void Remove(DesignerActionItem value)
	{
		base.List.Remove(value);
	}
}
