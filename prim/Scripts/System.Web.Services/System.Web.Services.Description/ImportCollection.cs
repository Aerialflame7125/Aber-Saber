namespace System.Web.Services.Description;

/// <summary>Provides a collection of instances of the <see cref="T:System.Web.Services.Description.Import" /> class representing documents to be imported into the XML Web service. This class cannot be inherited.</summary>
public sealed class ImportCollection : ServiceDescriptionBaseCollection
{
	/// <summary>Gets or sets the value of an <see cref="T:System.Web.Services.Description.Import" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.Import" /> whose value is modified or returned. </param>
	/// <returns>An <see langword="Import" />.</returns>
	public Import this[int index]
	{
		get
		{
			return (Import)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	internal ImportCollection(ServiceDescription serviceDescription)
		: base(serviceDescription)
	{
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Import" /> to the end of the <see cref="T:System.Web.Services.Description.ImportCollection" />.</summary>
	/// <param name="import">The <see cref="T:System.Web.Services.Description.Import" /> to add to the collection. </param>
	/// <returns>The zero-based index where the <paramref name="import" /> parameter has been added.</returns>
	public int Add(Import import)
	{
		return base.List.Add(import);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Import" /> instance to the <see cref="T:System.Web.Services.Description.ImportCollection" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index at which to insert the <paramref name="import" /> parameter. </param>
	/// <param name="import">The <see cref="T:System.Web.Services.Description.Import" /> to add to the collection. </param>
	/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
	public void Insert(int index, Import import)
	{
		base.List.Insert(index, import);
	}

	/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.Import" /> and returns the zero-based index of the first occurrence within the collection.</summary>
	/// <param name="import">The <see cref="T:System.Web.Services.Description.Import" /> for which to search in the collection. </param>
	/// <returns>A 32-bit signed integer.</returns>
	public int IndexOf(Import import)
	{
		return base.List.IndexOf(import);
	}

	/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.Import" /> is a member of the <see cref="T:System.Web.Services.Description.ImportCollection" />.</summary>
	/// <param name="import">The <see cref="T:System.Web.Services.Description.Import" /> for which to check collection membership. </param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="import" /> parameter is a member of the <see cref="T:System.Web.Services.Description.ImportCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(Import import)
	{
		return base.List.Contains(import);
	}

	/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.Import" /> from the <see cref="T:System.Web.Services.Description.ImportCollection" />.</summary>
	/// <param name="import">The <see cref="T:System.Web.Services.Description.Import" /> to remove from the collection. </param>
	public void Remove(Import import)
	{
		base.List.Remove(import);
	}

	/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.ImportCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.Import" />, starting at the specified zero-based index of the target array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.Import" /> serving as the destination of the copy action. </param>
	/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
	public void CopyTo(Import[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	protected override void SetParent(object value, object parent)
	{
		((Import)value).SetParent((ServiceDescription)parent);
	}
}
