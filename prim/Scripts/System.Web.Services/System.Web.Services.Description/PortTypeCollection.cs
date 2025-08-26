namespace System.Web.Services.Description;

/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.PortType" /> class; that is, a collection of sets of operations supported by the XML Web service. This class cannot be inherited.</summary>
public sealed class PortTypeCollection : ServiceDescriptionBaseCollection
{
	/// <summary>Gets or sets the value of a <see cref="T:System.Web.Services.Description.PortType" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.PortType" /> whose value is modified or returned. </param>
	/// <returns>A <see langword="PortType" />.</returns>
	public PortType this[int index]
	{
		get
		{
			return (PortType)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.PortType" /> specified by its name.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Services.Description.PortType" /> returned. </param>
	/// <returns>The name of the <paramref name="value" /> parameter.</returns>
	/// <exception cref="T:System.InvalidCastException">The <paramref name="value" /> parameter cannot be explicitly cast to type <see cref="T:System.Web.Services.Description.PortType" />. </exception>
	public PortType this[string name] => (PortType)Table[name];

	internal PortTypeCollection(ServiceDescription serviceDescription)
		: base(serviceDescription)
	{
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.PortType" /> to the end of the <see cref="T:System.Web.Services.Description.PortTypeCollection" />.</summary>
	/// <param name="portType">The <see cref="T:System.Web.Services.Description.PortType" /> to add to the collection. </param>
	/// <returns>The zero-based index where the <paramref name="portType" /> parameter has been added.</returns>
	public int Add(PortType portType)
	{
		return base.List.Add(portType);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.PortType" /> to the <see cref="T:System.Web.Services.Description.PortTypeCollection" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index at which to insert the <paramref name="portType" /> parameter. </param>
	/// <param name="portType">The <see cref="T:System.Web.Services.Description.PortType" /> to add to the collection. </param>
	/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
	public void Insert(int index, PortType portType)
	{
		base.List.Insert(index, portType);
	}

	/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.PortType" /> and returns the zero-based index of the first occurrence within the collection.</summary>
	/// <param name="portType">The <see cref="T:System.Web.Services.Description.PortType" /> for which to search in the collection. </param>
	/// <returns>A 32-bit signed integer.</returns>
	public int IndexOf(PortType portType)
	{
		return base.List.IndexOf(portType);
	}

	/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.PortType" /> is a member of the <see cref="T:System.Web.Services.Description.PortTypeCollection" />.</summary>
	/// <param name="portType">The <see cref="T:System.Web.Services.Description.PortType" /> for which to check for collection membership. </param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="portType" /> parameter is a member of the <see cref="T:System.Web.Services.Description.PortTypeCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(PortType portType)
	{
		return base.List.Contains(portType);
	}

	/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.PortType" /> from the <see cref="T:System.Web.Services.Description.PortTypeCollection" />.</summary>
	/// <param name="portType">The <see cref="T:System.Web.Services.Description.PortType" /> to remove from the collection. </param>
	public void Remove(PortType portType)
	{
		base.List.Remove(portType);
	}

	/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.PortTypeCollection" /> to a one-dimensional array of type <see cref="T:System.Web.Services.Description.PortType" />, starting at the specified zero-based index of the target array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.PortType" /> serving as the destination for the copy action. </param>
	/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
	public void CopyTo(PortType[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	protected override string GetKey(object value)
	{
		return ((PortType)value).Name;
	}

	protected override void SetParent(object value, object parent)
	{
		((PortType)value).SetParent((ServiceDescription)parent);
	}
}
