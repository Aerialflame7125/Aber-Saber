using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Represents an ordered set of <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> objects.</summary>
[Editor("System.Web.UI.Design.EmbeddedMailObjectCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
public sealed class EmbeddedMailObjectsCollection : CollectionBase
{
	/// <summary>Returns a specific element of a <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" />, identified by its position.</summary>
	/// <param name="index">The zero-based index of the element to get.</param>
	/// <returns>Returns the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> at the location specified by the <paramref name="index" /> parameter.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index " />is less than zero.-or- 
	///         <paramref name="index " />is equal to or greater than the number of items in the collection. </exception>
	[MonoTODO("Not implemented")]
	public EmbeddedMailObject this[int index]
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Adds an <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> to the end of the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" /> collection.</summary>
	/// <param name="value">The <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> to add to the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" />. </param>
	/// <returns>Returns the index of the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> in the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The specified<paramref name=" value" /> is <see langword="null" />.</exception>
	[MonoTODO("Not implemented")]
	public int Add(EmbeddedMailObject value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Determines whether the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" /> contains a specific <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" />. </summary>
	/// <param name="value">The <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> to locate in the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" /> object.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" /> contains <paramref name="obj" />; otherwise, <see langword="false" />.</returns>
	[MonoTODO("Not implemented")]
	public bool Contains(EmbeddedMailObject value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance beginning at the specified index in the array.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Array" /> is the destination of the elements copied from the current <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" />. The array must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array " />at which copying begins.  </param>
	/// <exception cref="T:System.ArgumentNullException">The specified<paramref name=" array" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="index" /> is out of range of the collection.</exception>
	[MonoTODO("Not implemented")]
	public void CopyTo(EmbeddedMailObject[] array, int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>Determines the index of a specific <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> in the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" />.</summary>
	/// <param name="value">An <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> to locate in the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" />.</param>
	/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> if it exists in the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" />; otherwise, -1.</returns>
	[MonoTODO("Not implemented")]
	public int IndexOf(EmbeddedMailObject value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Inserts an <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> into the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" /> object at the specified index position.</summary>
	/// <param name="index">An integer value that indicates the index position at which to insert the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> in the collection.</param>
	/// <param name="value">An <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> object to insert into the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="index" /> is out of range of the collection.</exception>
	/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="value" /> is <see langword="null" />.</exception>
	[MonoTODO("Not implemented")]
	public void Insert(int index, EmbeddedMailObject value)
	{
		throw new NotImplementedException();
	}

	[MonoTODO("Not implemented")]
	protected override void OnValidate(object value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> from the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" />.</summary>
	/// <param name="value">The <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> to remove from the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" />.</param>
	[MonoTODO("Not implemented")]
	public void Remove(EmbeddedMailObject value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" /> class.</summary>
	public EmbeddedMailObjectsCollection()
	{
	}
}
