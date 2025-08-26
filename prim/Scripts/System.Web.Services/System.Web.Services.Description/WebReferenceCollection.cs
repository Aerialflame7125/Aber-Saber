using System.Collections;

namespace System.Web.Services.Description;

/// <summary>Describes a collection of <see cref="T:System.Web.Services.Description.WebReference" /> objects.</summary>
public sealed class WebReferenceCollection : CollectionBase
{
	/// <summary>Gets or sets the <see cref="T:System.Web.Services.Description.WebReference" /> instance at the specified index.</summary>
	/// <param name="index">The index of the Web reference.</param>
	/// <returns>The <see cref="T:System.Web.Services.Description.WebReference" /> instance at the specified index.</returns>
	public WebReference this[int index]
	{
		get
		{
			return (WebReference)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	/// <summary>Appends a <see cref="T:System.Web.Services.Description.WebReference" /> instance to the collection.</summary>
	/// <param name="webReference">The Web reference to append.</param>
	/// <returns>The index of the appended Web reference.</returns>
	public int Add(WebReference webReference)
	{
		return base.List.Add(webReference);
	}

	/// <summary>Inserts the specified <see cref="T:System.Web.Services.Description.WebReference" /> instance at the specified index.</summary>
	/// <param name="index">The index at which to insert the specified Web reference.</param>
	/// <param name="webReference">The Web reference to insert.</param>
	public void Insert(int index, WebReference webReference)
	{
		base.List.Insert(index, webReference);
	}

	/// <summary>Determines the index of the specified <see cref="T:System.Web.Services.Description.WebReference" /> instance.</summary>
	/// <param name="webReference">The Web reference to search for.</param>
	/// <returns>The index of the specified Web reference, or -1 if the collection does not contain the specified Web reference.</returns>
	public int IndexOf(WebReference webReference)
	{
		return base.List.IndexOf(webReference);
	}

	/// <summary>Determines whether the collection contains a given <see cref="T:System.Web.Services.Description.WebReference" /> instance.</summary>
	/// <param name="webReference">The Web reference to search for.</param>
	/// <returns>
	///     <see langword="true" /> if the collections contains the given Web reference instance; otherwise, <see langword="false" />.</returns>
	public bool Contains(WebReference webReference)
	{
		return base.List.Contains(webReference);
	}

	/// <summary>Removes the specified <see cref="T:System.Web.Services.Description.WebReference" /> instance from the collection.</summary>
	/// <param name="webReference">The Web reference to remove.</param>
	public void Remove(WebReference webReference)
	{
		base.List.Remove(webReference);
	}

	/// <summary>Copies members of the collection to a specified array, starting at the specified array index.</summary>
	/// <param name="array">An array of Web references into which the collection members are copied.</param>
	/// <param name="index">The array index at which to begin copying.</param>
	public void CopyTo(WebReference[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.WebReferenceCollection" /> class.</summary>
	public WebReferenceCollection()
	{
	}
}
