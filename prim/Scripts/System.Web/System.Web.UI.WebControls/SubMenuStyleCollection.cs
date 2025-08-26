using System.Collections;

namespace System.Web.UI.WebControls;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> objects in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
public class SubMenuStyleCollection : StateManagedCollection
{
	private static Type[] types = new Type[1] { typeof(SubMenuStyle) };

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection object.</summary>
	/// <param name="i">The location of the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object in the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection.</param>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than zero or greater than or equal to the <see cref="P:System.Web.UI.StateManagedCollection.Count" /> property value.</exception>
	public SubMenuStyle this[int i]
	{
		get
		{
			return (SubMenuStyle)((IList)this)[i];
		}
		set
		{
			((IList)this)[i] = value;
		}
	}

	internal SubMenuStyleCollection()
	{
	}

	/// <summary>Adds a submenu style to the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection.</summary>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> instance to add to the collection.</param>
	/// <returns>The position in the collection at which the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> instance was inserted.</returns>
	public int Add(SubMenuStyle style)
	{
		return ((IList)this).Add((object)style);
	}

	/// <summary>Determines whether a <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection contains a specific <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> instance.</summary>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> instance to locate in the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> instance is found in the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection; otherwise, <see langword="false" />. If <see langword="null" /> is passed as the <paramref name="style" /> parameter, <see langword="false" /> is returned.</returns>
	public bool Contains(SubMenuStyle style)
	{
		return ((IList)this).Contains((object)style);
	}

	/// <summary>Copies the contents of a <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection to an array, starting at a specified array index.</summary>
	/// <param name="styleArray">The one-dimensional array that is the destination of the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> objects copied from the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection. The <paramref name="array" /> must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="array" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///         <paramref name="index" /> is less than zero.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="array" /> is multidimensional- or -
	///         <paramref name="index" /> is greater than or equal to the length of <paramref name="array" />.- or -The number of <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> objects in the source <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection is greater than the available space from the <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
	public void CopyTo(SubMenuStyle[] styleArray, int index)
	{
		((ICollection)this).CopyTo((Array)styleArray, index);
	}

	/// <summary>Creates an <see cref="T:System.Object" /> of the data type that corresponds to the specified index.</summary>
	/// <param name="index">The index of the data type to create. This parameter is not used in this implementation of the method; therefore, you should always pass in <see langword="null" />.</param>
	/// <returns>Always returns an empty <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object.</returns>
	protected override object CreateKnownType(int index)
	{
		return new SubMenuStyle();
	}

	/// <summary>Creates an array of <see cref="T:System.Type" /> objects that contains the supported data types of the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> class.</summary>
	/// <returns>An array of <see cref="T:System.Type" /> objects that contains the data types supported by the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> class.</returns>
	protected override Type[] GetKnownTypes()
	{
		return types;
	}

	/// <summary>Determines the location of a specified <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object in the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection.</summary>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object to locate in the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection.</param>
	/// <returns>The index of the specified <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object if it is found in the list; otherwise, -1.</returns>
	public int IndexOf(SubMenuStyle style)
	{
		return ((IList)this).IndexOf((object)style);
	}

	/// <summary>Inserts a <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object into the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection at the specified index.</summary>
	/// <param name="index">The zero-based index at which the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object should be inserted.</param>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object to insert into the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="index" /> is outside the range of the collection.</exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection is read-only.</exception>
	/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="style" /> is <see langword="null" />.</exception>
	public void Insert(int index, SubMenuStyle style)
	{
		((IList)this).Insert(index, (object)style);
	}

	/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object from the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection.</summary>
	/// <param name="style">The <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object to remove from the collection.</param>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection is read-only.</exception>
	public void Remove(SubMenuStyle style)
	{
		((IList)this).Remove((object)style);
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object at the specified location.</summary>
	/// <param name="index">The zero-based index location of the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object to remove from the collection.</param>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection is read-only.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is less than zero.- or -
	///         <paramref name="index" /> is equal to or greater than <see cref="P:System.Web.UI.StateManagedCollection.Count" />.</exception>
	public void RemoveAt(int index)
	{
		((IList)this).RemoveAt(index);
	}

	/// <summary>Instructs a <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object contained by the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection to record its entire state to view state.</summary>
	/// <param name="o">The object that should serialize itself completely.</param>
	protected override void SetDirtyObject(object o)
	{
		((SubMenuStyle)o).SetDirty();
	}
}
