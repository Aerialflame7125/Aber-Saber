namespace System.Web.Services.Description;

/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.MessagePart" /> class. This class cannot be inherited.</summary>
public sealed class MessagePartCollection : ServiceDescriptionBaseCollection
{
	/// <summary>Gets or sets the value of a <see cref="T:System.Web.Services.Description.MessagePart" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.MessagePart" /> whose value is modified or returned. </param>
	/// <returns>A <see langword="MessagePart" />.</returns>
	public MessagePart this[int index]
	{
		get
		{
			return (MessagePart)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.Services.Description.MessagePart" /> specified by its name.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Services.Description.MessagePart" /> returned. </param>
	/// <returns>A <see langword="MessagePart" />.</returns>
	public MessagePart this[string name] => (MessagePart)Table[name];

	internal MessagePartCollection(Message message)
		: base(message)
	{
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.MessagePart" /> to the end of the <see cref="T:System.Web.Services.Description.MessagePartCollection" />.</summary>
	/// <param name="messagePart">The <see cref="T:System.Web.Services.Description.MessagePart" /> to add to the collection. </param>
	/// <returns>The zero-based index where the <paramref name="messagePart" /> parameter has been added.</returns>
	public int Add(MessagePart messagePart)
	{
		return base.List.Add(messagePart);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.MessagePart" /> to the <see cref="T:System.Web.Services.Description.MessagePartCollection" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index at which to insert the <paramref name="messagePart" /> parameter. </param>
	/// <param name="messagePart">The <see cref="T:System.Web.Services.Description.MessagePart" /> to add to the collection. </param>
	public void Insert(int index, MessagePart messagePart)
	{
		base.List.Insert(index, messagePart);
	}

	/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.MessagePart" /> and returns the zero-based index of the first occurrence within the collection.</summary>
	/// <param name="messagePart">The <see cref="T:System.Web.Services.Description.MessagePart" /> for which to search in the collection. </param>
	/// <returns>A 32-bit signed integer.</returns>
	public int IndexOf(MessagePart messagePart)
	{
		return base.List.IndexOf(messagePart);
	}

	/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.MessagePart" /> is a member of the <see langword="MessagePartCollection" />.</summary>
	/// <param name="messagePart">The <see cref="T:System.Web.Services.Description.MessagePart" /> for which to check collection membership. </param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="messagePart" /> parameter is a member of the <see cref="T:System.Web.Services.Description.MessagePartCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(MessagePart messagePart)
	{
		return base.List.Contains(messagePart);
	}

	/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.MessagePart" /> from the <see cref="T:System.Web.Services.Description.MessagePartCollection" />.</summary>
	/// <param name="messagePart">The <see cref="T:System.Web.Services.Description.MessagePart" /> to remove from the collection. </param>
	public void Remove(MessagePart messagePart)
	{
		base.List.Remove(messagePart);
	}

	/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.MessagePartCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.MessagePart" />, starting at the specified zero-based index of the target array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.MessagePart" /> serving as the destination of the copy action. </param>
	/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
	public void CopyTo(MessagePart[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	protected override string GetKey(object value)
	{
		return ((MessagePart)value).Name;
	}

	protected override void SetParent(object value, object parent)
	{
		((MessagePart)value).SetParent((Message)parent);
	}
}
