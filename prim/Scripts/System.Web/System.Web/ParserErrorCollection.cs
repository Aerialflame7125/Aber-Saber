using System.Collections;

namespace System.Web;

/// <summary>Manages a set of parser errors detected during parsing. This class cannot be inherited.</summary>
[Serializable]
public sealed class ParserErrorCollection : CollectionBase
{
	/// <summary>Gets or sets the <see cref="T:System.Web.ParserError" /> object at the specified index within the collection.</summary>
	/// <param name="index">The index within the collection of the <see cref="T:System.Web.ParserError" /> object to get or set.</param>
	/// <returns>The <see cref="T:System.Web.ParserError" /> at the specified index within the collection.</returns>
	public ParserError this[int index]
	{
		get
		{
			return (ParserError)base.InnerList[index];
		}
		set
		{
			base.InnerList[index] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ParserErrorCollection" /> class.</summary>
	public ParserErrorCollection()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ParserErrorCollection" /> class.</summary>
	/// <param name="value">An array of type <see cref="T:System.Web.ParserError" /> that specifies the errors to add to the collection.</param>
	public ParserErrorCollection(ParserError[] value)
	{
		base.InnerList.AddRange(value);
	}

	/// <summary>Adds a value to the collection.</summary>
	/// <param name="value">The <see cref="T:System.Web.ParserError" /> value to add to the collection.</param>
	/// <returns>The index of the value within the collection; otherwise, -1 if the value is already in the collection.</returns>
	public int Add(ParserError value)
	{
		return base.List.Add(value);
	}

	/// <summary>Adds the objects in an existing <see cref="T:System.Web.ParserErrorCollection" /> to the collection. </summary>
	/// <param name="value">A <see cref="T:System.Web.ParserErrorCollection" /> containing the <see cref="T:System.Web.ParserError" /> objects to add to the collection. </param>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.ParserError" /> value is <see langword="null" />.</exception>
	public void AddRange(ParserErrorCollection value)
	{
		base.InnerList.AddRange(value);
	}

	/// <summary>Adds an array of <see cref="T:System.Web.ParserError" /> objects to the collection.</summary>
	/// <param name="value">An array of type <see cref="T:System.Web.ParserError" /> that specifies the values to add to the collection. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="value" /> is <see langword="null" />.</exception>
	public void AddRange(ParserError[] value)
	{
		base.InnerList.AddRange(value);
	}

	/// <summary>Determines whether the <see cref="T:System.Web.ParserError" /> object is located in the collection.</summary>
	/// <param name="value">The <see cref="T:System.Web.ParserError" /> to locate in the collection.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.ParserError" /> is in the collection; otherwise, <see langword="false" />.</returns>
	public bool Contains(ParserError value)
	{
		return base.InnerList.Contains(value);
	}

	/// <summary>Copies the <see cref="T:System.Web.ParserError" /> objects in the collection to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.ParserError" /> to which the parser errors in the collection are copied.</param>
	/// <param name="index">The first index within the array to which the <see cref="T:System.Web.ParserError" /> is copied.</param>
	public void CopyTo(ParserError[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	/// <summary>Gets the index of the specified <see cref="T:System.Web.ParserError" /> object in the collection.</summary>
	/// <param name="value">The <see cref="T:System.Web.ParserError" /> to locate in the collection.</param>
	/// <returns>The zero-based index of the <see cref="T:System.Web.ParserError" /> objects within the collection; otherwise, 1 if the <see cref="T:System.Web.ParserError" /> is not in the collection.</returns>
	public int IndexOf(ParserError value)
	{
		return base.InnerList.IndexOf(value);
	}

	/// <summary>Inserts the specified <see cref="T:System.Web.ParserError" /> object into the collection at the specified index.</summary>
	/// <param name="index">The index within the collection at which to insert the <see cref="T:System.Web.ParserError" />.</param>
	/// <param name="value">The <see cref="T:System.Web.ParserError" /> object to insert into the collection.</param>
	public void Insert(int index, ParserError value)
	{
		base.InnerList.Insert(index, value);
	}

	/// <summary>Removes the specified <see cref="T:System.Web.ParserError" /> object from the collection.</summary>
	/// <param name="value">The <see cref="T:System.Web.ParserError" /> to remove from the collection.</param>
	public void Remove(ParserError value)
	{
		base.InnerList.Remove(value);
	}
}
