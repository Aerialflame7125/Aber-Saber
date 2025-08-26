using System.Collections;

namespace System.Web.Services.Description;

/// <summary>Provides a collection of instances of the <see cref="T:System.Web.Services.Description.MimeTextMatch" /> class. This class cannot be inherited.</summary>
public sealed class MimeTextMatchCollection : CollectionBase
{
	/// <summary>Gets or sets the value of the member of the <see cref="T:System.Web.Services.Description.MimeTextMatchCollection" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.MimeTextMatch" /> whose value is returned or modified. </param>
	/// <returns>A <see langword="MimeTextMatch" />.</returns>
	public MimeTextMatch this[int index]
	{
		get
		{
			return (MimeTextMatch)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.MimeTextMatch" /> to the end of the <see cref="T:System.Web.Services.Description.MimeTextMatchCollection" />.</summary>
	/// <param name="match">The <see cref="T:System.Web.Services.Description.MimeTextMatch" /> to add to the collection. </param>
	/// <returns>The zero-based index where the <paramref name="match" /> parameter has been added.</returns>
	public int Add(MimeTextMatch match)
	{
		return base.List.Add(match);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.MimeTextMatch" /> to the <see cref="T:System.Web.Services.Description.MimeTextMatchCollection" /> at the specified index.</summary>
	/// <param name="index">The zero-based index at which to insert the <paramref name="match" /> parameter. </param>
	/// <param name="match">The <see cref="T:System.Web.Services.Description.MimeTextMatch" /> to add to the collection. </param>
	/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
	public void Insert(int index, MimeTextMatch match)
	{
		base.List.Insert(index, match);
	}

	/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.MimeTextMatch" /> and returns the zero-based index of the first occurrence within the collection.</summary>
	/// <param name="match">The <see cref="T:System.Web.Services.Description.MimeTextMatch" /> for which to search in the collection. </param>
	/// <returns>A 32-bit signed integer.</returns>
	public int IndexOf(MimeTextMatch match)
	{
		return base.List.IndexOf(match);
	}

	/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.MimeTextMatch" /> is a member of the <see cref="T:System.Web.Services.Description.MimeTextMatchCollection" />.</summary>
	/// <param name="match">The <see cref="T:System.Web.Services.Description.MimeTextMatch" /> for which to check collection membership. </param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="match" /> parameter is a member of the <see langword="MimeTextMatchCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(MimeTextMatch match)
	{
		return base.List.Contains(match);
	}

	/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.MimeTextMatch" /> from the <see cref="T:System.Web.Services.Description.MimeTextMatchCollection" />.</summary>
	/// <param name="match">The <see cref="T:System.Web.Services.Description.MimeTextMatch" /> to remove from the collection. </param>
	public void Remove(MimeTextMatch match)
	{
		base.List.Remove(match);
	}

	/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.MimeTextMatchCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.MimeTextMatch" />, starting at the specified zero-based index of the target array.</summary>
	/// <param name="array">The array of type <see cref="T:System.Web.Services.Description.MimeTextMatch" /> serving as the destination for the copy action. </param>
	/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
	public void CopyTo(MimeTextMatch[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.MimeTextMatchCollection" /> class. </summary>
	public MimeTextMatchCollection()
	{
	}
}
