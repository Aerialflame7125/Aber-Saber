using System.Collections;

namespace System.Web.Services.Description;

/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.MimePart" /> class. This class cannot be inherited.</summary>
public sealed class MimePartCollection : CollectionBase
{
	/// <summary>Gets or sets the value of a <see cref="T:System.Web.Services.Description.MimePart" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.MimePart" /> whose value is modified or returned. </param>
	/// <returns>A <see langword="MimePart" />.</returns>
	public MimePart this[int index]
	{
		get
		{
			return (MimePart)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.MimePart" /> to the end of the <see cref="T:System.Web.Services.Description.MimePartCollection" />.</summary>
	/// <param name="mimePart">The <see cref="T:System.Web.Services.Description.MimePart" /> to add to the collection. </param>
	/// <returns>The zero-based index where the <paramref name="mimePart" /> parameter has been added.</returns>
	public int Add(MimePart mimePart)
	{
		return base.List.Add(mimePart);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.MimePart" /> to the <see cref="T:System.Web.Services.Description.MimePartCollection" /> at the specified index.</summary>
	/// <param name="index">The zero-based index at which to insert the <paramref name="mimePart" /> parameter. </param>
	/// <param name="mimePart">The <see cref="T:System.Web.Services.Description.MimePart" /> to add to the collection. </param>
	/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
	public void Insert(int index, MimePart mimePart)
	{
		base.List.Insert(index, mimePart);
	}

	/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.MimePart" /> and returns the zero-based index of the first occurrence within the collection.</summary>
	/// <param name="mimePart">The <see cref="T:System.Web.Services.Description.MimePart" /> for which to search the <see cref="T:System.Web.Services.Description.MimePartCollection" />. </param>
	/// <returns>A 32-bit signed integer.</returns>
	public int IndexOf(MimePart mimePart)
	{
		return base.List.IndexOf(mimePart);
	}

	/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.MimePart" /> is a member of the <see cref="T:System.Web.Services.Description.MimePartCollection" />.</summary>
	/// <param name="mimePart">The <see cref="T:System.Web.Services.Description.MimePart" /> to check for collection membership. </param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="mimePart" /> parameter is a member of the <see langword="MimePartCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(MimePart mimePart)
	{
		return base.List.Contains(mimePart);
	}

	/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.MimePart" /> from the <see cref="T:System.Web.Services.Description.MimePartCollection" />.</summary>
	/// <param name="mimePart">The <see cref="T:System.Web.Services.Description.MimePart" /> to remove from the collection. </param>
	public void Remove(MimePart mimePart)
	{
		base.List.Remove(mimePart);
	}

	/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.MimePartCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.MimePart" />, starting at the specified zero-based index of the target array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.MimePart" /> serving as the destination for the copy action. </param>
	/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
	public void CopyTo(MimePart[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.MimePartCollection" /> class. </summary>
	public MimePartCollection()
	{
	}
}
