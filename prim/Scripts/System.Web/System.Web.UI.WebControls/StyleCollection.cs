using System.Collections;

namespace System.Web.UI.WebControls;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.Style" /> objects.</summary>
public class StyleCollection : StateManagedCollection
{
	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.Style" /> object at the specified index location in the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> object.</summary>
	/// <param name="i">The zero-based index location of the <see cref="T:System.Web.UI.WebControls.Style" /> object in the <see cref="T:System.Web.UI.WebControls.StyleCollection" />. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.StyleCollection" />.</returns>
	public Style this[int i]
	{
		get
		{
			return (Style)((IList)this)[i];
		}
		set
		{
			((IList)this)[i] = value;
		}
	}

	internal StyleCollection()
	{
	}

	/// <summary>Appends a specified <see cref="T:System.Web.UI.WebControls.Style" /> object to the end of the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> object.</summary>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.Style" /> object to add to the collection.</param>
	/// <returns>The index at which the style was added to the collection.</returns>
	public int Add(Style style)
	{
		return ((IList)this).Add((object)style);
	}

	/// <summary>Determines whether the specified style is contained within the collection.</summary>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.Style" /> to locate within the collection.</param>
	/// <returns>
	///     <see langword="true" /> if the style is in the collection; otherwise, <see langword="false" />.</returns>
	public bool Contains(Style style)
	{
		return ((IList)this).Contains((object)style);
	}

	/// <summary>Copies the elements of the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> to a one-dimensional <see cref="T:System.Web.UI.WebControls.Style" /> array, starting at the specified index of the target array.</summary>
	/// <param name="styleArray">The <see cref="T:System.Array" /> that is the destination of the copied styles. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="styleArray" /> at which copying begins.</param>
	public void CopyTo(Style[] styleArray, int index)
	{
		((ICollection)this).CopyTo((Array)styleArray, index);
	}

	/// <summary>Creates an instance of the <see cref="T:System.Web.UI.WebControls.Style" /> class, based on the single element collection returned by the <see cref="M:System.Web.UI.WebControls.StyleCollection.GetKnownTypes" /> method.</summary>
	/// <param name="index">The index, from the ordered list of types returned by <see cref="M:System.Web.UI.WebControls.StyleCollection.GetKnownTypes" />, of the type of the <see cref="T:System.Web.UI.IStateManager" /> object to create. Because the <see cref="M:System.Web.UI.WebControls.StyleCollection.GetKnownTypes" /> method of <see cref="T:System.Web.UI.WebControls.StyleCollection" /> returns a list with only one type, the input <paramref name="index" /> is ignored.</param>
	/// <returns>An instance of the <see cref="T:System.Web.UI.WebControls.Style" /> class.</returns>
	protected override object CreateKnownType(int index)
	{
		return new Style();
	}

	/// <summary>Gets an array of the <see cref="T:System.Web.UI.IStateManager" /> types that the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> can contain.</summary>
	/// <returns>An array containing one <see cref="T:System.Type" /> object for the <see cref="T:System.Web.UI.WebControls.Style" /> class, which indicates that the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> can contain <see cref="T:System.Web.UI.WebControls.Style" /> objects.</returns>
	protected override Type[] GetKnownTypes()
	{
		return new Type[1] { typeof(Style) };
	}

	/// <summary>Returns the index of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object within the collection.</summary>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.Style" /> to locate within the collection.</param>
	/// <returns>The zero-based index of the first occurrence of <paramref name="style" /> within the collection; otherwise, -1 if the style is not in the collection.</returns>
	public int IndexOf(Style style)
	{
		return ((IList)this).IndexOf((object)style);
	}

	/// <summary>Inserts a specified <see cref="T:System.Web.UI.WebControls.Style" /> object into the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> at the specified index location.</summary>
	/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.Style" /> object. </param>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.Style" /> object to insert into the collection. </param>
	public void Insert(int index, Style style)
	{
		((IList)this).Insert(index, (object)style);
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.Style" /> object from the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> object.</summary>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.Style" /> object to remove from the collection. </param>
	public void Remove(Style style)
	{
		((IList)this).Remove((object)style);
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.Style" /> object at the specified index location from the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> object.</summary>
	/// <param name="index">The zero-based index location of the <see cref="T:System.Web.UI.WebControls.Style" /> object to remove. </param>
	public void RemoveAt(int index)
	{
		((IList)this).RemoveAt(index);
	}

	/// <summary>Instructs the input <see cref="T:System.Web.UI.WebControls.Style" /> object contained in the collection to record its entire state to view state, rather than recording only change information.</summary>
	/// <param name="o">The <see cref="T:System.Web.UI.WebControls.Style" /> object that should serialize itself completely.</param>
	protected override void SetDirtyObject(object o)
	{
		if (o is Style style)
		{
			style.SetDirty();
		}
	}
}
