using System.Collections;

namespace System.DirectoryServices;

/// <summary>The <see cref="T:System.DirectoryServices.SearchResultCollection" /> class contains the <see cref="T:System.DirectoryServices.SearchResult" /> instances that the Active Directory hierarchy returned during a <see cref="T:System.DirectoryServices.DirectorySearcher" /> query.</summary>
public class SearchResultCollection : MarshalByRefObject, ICollection, IEnumerable, IDisposable
{
	private ArrayList sValues = new ArrayList();

	/// <summary>Gets the number of <see cref="T:System.DirectoryServices.SearchResult" /> objects in this collection.</summary>
	/// <returns>The number of <see cref="T:System.DirectoryServices.SearchResult" /> objects in this collection.</returns>
	public int Count => sValues.Count;

	/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
	/// <returns>
	///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
	bool ICollection.IsSynchronized => sValues.IsSynchronized;

	/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
	/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
	object ICollection.SyncRoot => sValues.SyncRoot;

	/// <summary>Gets the <see cref="T:System.DirectoryServices.SearchResult" /> object that is located at a specified index in this collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.SearchResult" /> object to retrieve.</param>
	/// <returns>The <see cref="T:System.DirectoryServices.SearchResult" /> object that is located at the specified index.</returns>
	public SearchResult this[int index] => (SearchResult)sValues[index];

	/// <summary>Gets the <see cref="T:System.DirectoryServices.DirectorySearcher" /> properties that were specified before the search was executed.</summary>
	/// <returns>An array of type <see cref="T:System.String" /> that contains the properties that were specified in the <see cref="P:System.DirectoryServices.DirectorySearcher.PropertiesToLoad" /> property collection before the search was executed.</returns>
	public string[] PropertiesLoaded
	{
		[System.MonoTODO]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the handle that is returned by the <c>IDirectorySearch::ExecuteSearch</c> method that performs the actual search. For more information, see the IDirectorySearch::ExecuteSearch article.</summary>
	/// <returns>The ADS_SEARCH_HANDLE value that this collection uses.</returns>
	public IntPtr Handle
	{
		[System.MonoTODO]
		get
		{
			throw new NotImplementedException();
		}
	}

	internal SearchResultCollection()
	{
	}

	/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="array" /> is multidimensional.  
	/// -or-  
	/// The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
	/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
	void ICollection.CopyTo(Array oArray, int iArrayIndex)
	{
		sValues.CopyTo(oArray, iArrayIndex);
	}

	/// <summary>Copies all <see cref="T:System.DirectoryServices.SearchResult" /> objects in this collection to the specific array, starting at the specified index in the target array.</summary>
	/// <param name="results">The array of <see cref="T:System.DirectoryServices.SearchResult" /> objects that receives the elements of this collection.</param>
	/// <param name="index">The zero-based index in <paramref name="results" /> where this method starts copying this collection.</param>
	public void CopyTo(SearchResult[] results, int index)
	{
		((ICollection)this).CopyTo((Array)results, index);
	}

	internal void Add(object oValue)
	{
		sValues.Add(oValue);
	}

	private bool Contains(object oValues)
	{
		return sValues.Contains(oValues);
	}

	/// <summary>Determines if a specified <see cref="T:System.DirectoryServices.SearchResult" /> object is in this collection.</summary>
	/// <param name="result">The <see cref="T:System.DirectoryServices.SearchResult" /> object to find.</param>
	/// <returns>
	///   <see langword="true" /> if the specified property belongs to this collection; otherwise, <see langword="false" />.</returns>
	public bool Contains(SearchResult result)
	{
		return sValues.Contains(result);
	}

	/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.SearchResult" /> object in this collection.</summary>
	/// <param name="result">The <see cref="T:System.DirectoryServices.SearchResult" /> object to search for in this collection.</param>
	/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.SearchResult" /> object.</returns>
	public int IndexOf(SearchResult result)
	{
		return sValues.IndexOf(result);
	}

	/// <summary>Returns an enumerator that you can use to iterate through this collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that you can use to iterate through this collection.</returns>
	public IEnumerator GetEnumerator()
	{
		return sValues.GetEnumerator();
	}

	/// <summary>Releases all resources that are used by the <see cref="T:System.DirectoryServices.SearchResultCollection" /> object.</summary>
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	/// <summary>Releases the unmanaged resources that are used by the <see cref="T:System.DirectoryServices.SearchResultCollection" /> object and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	[System.MonoTODO]
	protected virtual void Dispose(bool disposing)
	{
	}

	/// <summary>Overrides the <see cref="M:System.Object.Finalize" /> method.</summary>
	~SearchResultCollection()
	{
		Dispose(disposing: false);
	}
}
