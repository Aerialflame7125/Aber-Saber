using System.Collections;

namespace System.Web.UI.WebControls.WebParts;

/// <summary>Represents a collection of custom Web Parts verbs. This class cannot be inherited. </summary>
public sealed class WebPartVerbCollection : ReadOnlyCollectionBase
{
	/// <summary>Specifies an empty collection that you can use instead of creating a new one. This static field is read-only.</summary>
	public static readonly WebPartVerbCollection Empty = new WebPartVerbCollection();

	/// <summary>Gets a Web Parts verb from the collection at the specified index.</summary>
	/// <param name="index">The index value of the Web Parts verb to be retrieved.</param>
	/// <returns>A Web Parts verb from the collection.</returns>
	public WebPartVerb this[int index] => (WebPartVerb)base.InnerList[index];

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" /> class.</summary>
	public WebPartVerbCollection()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" /> class using the specified collection.</summary>
	/// <param name="verbs">An object derived from <see cref="T:System.Collections.ICollection" /> that contains a set of Web Parts verbs.</param>
	public WebPartVerbCollection(ICollection verbs)
	{
		base.InnerList.AddRange(verbs);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" /> class using the specified collections.</summary>
	/// <param name="existingVerbs">An existing <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" />.</param>
	/// <param name="verbs">An object derived from <see cref="T:System.Collections.ICollection" /> that contains a set of Web Parts verbs.</param>
	public WebPartVerbCollection(WebPartVerbCollection existingVerbs, ICollection verbs)
	{
		base.InnerList.AddRange(existingVerbs.InnerList);
		base.InnerList.AddRange(verbs);
	}

	/// <summary>Searches the Web Parts verb collection for the specified <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object.</summary>
	/// <param name="value">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> to be found.</param>
	/// <returns>
	///     <see langword="true" /> if the collection contains the Web Parts verb; otherwise, <see langword="false" />.</returns>
	public bool Contains(WebPartVerb value)
	{
		return base.InnerList.Contains(value);
	}

	/// <summary>Copies elements of the collection to the specified array, starting at the specified index.</summary>
	/// <param name="array">The array that elements are to be copied to.</param>
	/// <param name="index">The index where copying should begin.</param>
	public void CopyTo(WebPartVerb[] array, int index)
	{
		base.InnerList.CopyTo(0, array, index, Count);
	}

	/// <summary>Searches for the specified Web Parts verb and returns the zero-based index of the first occurrence within the entire collection.</summary>
	/// <param name="value">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> to be located.</param>
	/// <returns>The index of the Web Parts verb.</returns>
	public int IndexOf(WebPartVerb value)
	{
		return base.InnerList.IndexOf(value);
	}
}
