namespace System.Web.Services.Description;

/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.Message" /> class. This class cannot be inherited.</summary>
public sealed class MessageCollection : ServiceDescriptionBaseCollection
{
	/// <summary>Gets or sets the value of a <see cref="T:System.Web.Services.Description.Message" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.Message" /> whose value is modified or returned. </param>
	/// <returns>A <see langword="Message" />.</returns>
	public Message this[int index]
	{
		get
		{
			return (Message)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.Services.Description.Message" /> specified by its name.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Services.Description.Message" /> returned. </param>
	/// <returns>A <see langword="Message" />.</returns>
	public Message this[string name] => (Message)Table[name];

	internal MessageCollection(ServiceDescription serviceDescription)
		: base(serviceDescription)
	{
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Message" /> to the end of the <see cref="T:System.Web.Services.Description.MessageCollection" />.</summary>
	/// <param name="message">The <see cref="T:System.Web.Services.Description.Message" /> to add to the collection. </param>
	/// <returns>The zero-based index where the <paramref name="message" /> parameter has been added.</returns>
	public int Add(Message message)
	{
		return base.List.Add(message);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Message" /> to the <see cref="T:System.Web.Services.Description.MessageCollection" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index at which to insert the <paramref name="message" /> parameter. </param>
	/// <param name="message">The <see cref="T:System.Web.Services.Description.Message" /> to add to the collection. </param>
	/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
	public void Insert(int index, Message message)
	{
		base.List.Insert(index, message);
	}

	/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.Message" /> and returns the zero-based index of the first occurrence within the collection.</summary>
	/// <param name="message">The <see cref="T:System.Web.Services.Description.Message" /> for which to search in the collection. </param>
	/// <returns>A 32-bit signed integer.</returns>
	public int IndexOf(Message message)
	{
		return base.List.IndexOf(message);
	}

	/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.Message" /> is a member of the <see cref="T:System.Web.Services.Description.MessageCollection" />.</summary>
	/// <param name="message">The <see cref="T:System.Web.Services.Description.Message" /> for which to check collection membership. </param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="message" /> parameter is a member of the <see cref="T:System.Web.Services.Description.MessageCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(Message message)
	{
		return base.List.Contains(message);
	}

	/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.Message" /> from the <see cref="T:System.Web.Services.Description.MessageCollection" />.</summary>
	/// <param name="message">The <see cref="T:System.Web.Services.Description.Message" /> to remove from the collection. </param>
	public void Remove(Message message)
	{
		base.List.Remove(message);
	}

	/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.MessageCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.Message" />, starting at the specified zero-based index of the target array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.Message" /> serving as the destination for the copy action. </param>
	/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
	public void CopyTo(Message[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	protected override string GetKey(object value)
	{
		return ((Message)value).Name;
	}

	protected override void SetParent(object value, object parent)
	{
		((Message)value).SetParent((ServiceDescription)parent);
	}
}
