namespace System.Web.Services.Description;

/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.Service" /> class. This class cannot be inherited.</summary>
public sealed class ServiceCollection : ServiceDescriptionBaseCollection
{
	/// <summary>Gets or sets the value of a <see cref="T:System.Web.Services.Description.Service" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.Service" /> to be modified or returned. </param>
	/// <returns>A <see langword="Service" />.</returns>
	public Service this[int index]
	{
		get
		{
			return (Service)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.Services.Description.Service" /> specified by its name.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Services.Description.Service" /> returned. </param>
	/// <returns>A <see langword="Service" />.</returns>
	public Service this[string name] => (Service)Table[name];

	internal ServiceCollection(ServiceDescription serviceDescription)
		: base(serviceDescription)
	{
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Service" /> to the end of the <see cref="T:System.Web.Services.Description.ServiceCollection" />.</summary>
	/// <param name="service">The <see cref="T:System.Web.Services.Description.Service" /> instance to add to the collection. </param>
	/// <returns>The zero-based index where the <paramref name="service" /> parameter has been added.</returns>
	public int Add(Service service)
	{
		return base.List.Add(service);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Service" /> instance to the <see cref="T:System.Web.Services.Description.ServiceCollection" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index at which to insert the <paramref name="service" /> parameter. </param>
	/// <param name="service">The <see cref="T:System.Web.Services.Description.Service" /> to add to the collection. </param>
	/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
	public void Insert(int index, Service service)
	{
		base.List.Insert(index, service);
	}

	/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.Service" /> and returns the zero-based index of the first occurrence within the <see langword="ServiceCollection" />.</summary>
	/// <param name="service">The <see cref="T:System.Web.Services.Description.Service" /> for which to search in the collection. </param>
	/// <returns>A 32-bit signed integer.</returns>
	public int IndexOf(Service service)
	{
		return base.List.IndexOf(service);
	}

	/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.Service" /> instance is a member of the <see cref="T:System.Web.Services.Description.ServiceCollection" />.</summary>
	/// <param name="service">The <see cref="T:System.Web.Services.Description.Service" /> for which to check collection membership. </param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="service" /> parameter is a member of the <see cref="T:System.Web.Services.Description.ServiceCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(Service service)
	{
		return base.List.Contains(service);
	}

	/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.Service" /> from the <see cref="T:System.Web.Services.Description.ServiceCollection" />.</summary>
	/// <param name="service">The <see cref="T:System.Web.Services.Description.Service" /> to remove from the collection. </param>
	public void Remove(Service service)
	{
		base.List.Remove(service);
	}

	/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.ServiceCollection" /> to a one-dimensional array of type <see cref="T:System.Web.Services.Description.Service" />, starting at the specified zero-based index of the target array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.Service" /> serving as the destination for the copy action. </param>
	/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
	public void CopyTo(Service[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	protected override string GetKey(object value)
	{
		return ((Service)value).Name;
	}

	protected override void SetParent(object value, object parent)
	{
		((Service)value).SetParent((ServiceDescription)parent);
	}
}
