namespace System.Web.Services.Description;

/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.Port" /> class. This class cannot be inherited.</summary>
public sealed class PortCollection : ServiceDescriptionBaseCollection
{
	/// <summary>Gets or sets the value of a <see cref="T:System.Web.Services.Description.Port" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.Port" /> whose value is modified or returned. </param>
	/// <returns>The value of a port at the specified index.</returns>
	public Port this[int index]
	{
		get
		{
			return (Port)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.Services.Description.Port" /> specified by its name.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Services.Description.Port" /> returned. </param>
	/// <returns>A port specified by its name.</returns>
	public Port this[string name] => (Port)Table[name];

	internal PortCollection(Service service)
		: base(service)
	{
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Port" /> to the end of the <see cref="T:System.Web.Services.Description.PortCollection" />.</summary>
	/// <param name="port">The <see cref="T:System.Web.Services.Description.Port" /> to add to the collection. </param>
	/// <returns>The zero-based index where the <paramref name="port" /> parameter has been added.</returns>
	public int Add(Port port)
	{
		return base.List.Add(port);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Port" /> instance to the <see cref="T:System.Web.Services.Description.PortCollection" /> at the specified index.</summary>
	/// <param name="index">The zero-based index at which to insert the <paramref name="port" /> parameter.</param>
	/// <param name="port">The <see cref="T:System.Web.Services.Description.Port" /> to add to the collection. </param>
	/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
	public void Insert(int index, Port port)
	{
		base.List.Insert(index, port);
	}

	/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.Port" /> and returns the zero-based index of the first occurrence within the collection.</summary>
	/// <param name="port">The <see cref="T:System.Web.Services.Description.Port" /> for which to search in the collection.</param>
	/// <returns>A 32-bit signed integer.</returns>
	public int IndexOf(Port port)
	{
		return base.List.IndexOf(port);
	}

	/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.Port" /> is a member of the <see cref="T:System.Web.Services.Description.PortCollection" />.</summary>
	/// <param name="port">The <see cref="T:System.Web.Services.Description.Port" /> for which to check collection membership.</param>
	/// <returns>
	///     <see langword="true" /> if the specified <see cref="T:System.Web.Services.Description.Port" /> is a member of the <see cref="T:System.Web.Services.Description.PortCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(Port port)
	{
		return base.List.Contains(port);
	}

	/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.Port" /> from the <see cref="T:System.Web.Services.Description.PortCollection" />.</summary>
	/// <param name="port">The <see cref="T:System.Web.Services.Description.Port" /> to remove from the collection. </param>
	public void Remove(Port port)
	{
		base.List.Remove(port);
	}

	/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.PortCollection" /> to a one-dimensional array of type <see cref="T:System.Web.Services.Description.Port" />, starting at the specified zero-based index of the target array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.Port" /> serving as the destination for the copy action. </param>
	/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
	public void CopyTo(Port[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	protected override string GetKey(object value)
	{
		return ((Port)value).Name;
	}

	protected override void SetParent(object value, object parent)
	{
		((Port)value).SetParent((Service)parent);
	}
}
