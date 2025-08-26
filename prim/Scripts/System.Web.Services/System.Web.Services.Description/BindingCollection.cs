namespace System.Web.Services.Description;

/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.Binding" /> class supported by the XML Web service. This class cannot be inherited.</summary>
public sealed class BindingCollection : ServiceDescriptionBaseCollection
{
	/// <summary>Gets or sets the value of a <see cref="T:System.Web.Services.Description.Binding" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.Binding" /> whose value is modified or returned. </param>
	/// <returns>A <see langword="Binding" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
	public Binding this[int index]
	{
		get
		{
			return (Binding)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.Services.Description.Binding" /> specified by its name.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Services.Description.Binding" /> returned. </param>
	/// <returns>A <see langword="Binding" />.</returns>
	public Binding this[string name] => (Binding)Table[name];

	internal BindingCollection(ServiceDescription serviceDescription)
		: base(serviceDescription)
	{
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Binding" /> to the end of the <see cref="T:System.Web.Services.Description.BindingCollection" />.</summary>
	/// <param name="binding">The <see cref="T:System.Web.Services.Description.Binding" /> to add to the collection. </param>
	/// <returns>The zero-based index where the <paramref name="binding" /> parameter has been added.</returns>
	public int Add(Binding binding)
	{
		return base.List.Add(binding);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Binding" /> to the <see cref="T:System.Web.Services.Description.BindingCollection" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index at which to insert the <paramref name="binding" /> parameter. </param>
	/// <param name="binding">The <see cref="T:System.Web.Services.Description.Binding" /> to be added to the collection. </param>
	public void Insert(int index, Binding binding)
	{
		base.List.Insert(index, binding);
	}

	/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.Binding" /> and returns the zero-based index of the first occurrence within the collection.</summary>
	/// <param name="binding">The <see cref="T:System.Web.Services.Description.Binding" /> for which to search in the collection. </param>
	/// <returns>A 32-bit signed integer.</returns>
	public int IndexOf(Binding binding)
	{
		return base.List.IndexOf(binding);
	}

	/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.Binding" /> is a member of the <see cref="T:System.Web.Services.Description.BindingCollection" />.</summary>
	/// <param name="binding">A <see cref="T:System.Web.Services.Description.Binding" /> for which to check collection membership. </param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="binding" /> parameter is a member of the <see cref="T:System.Web.Services.Description.BindingCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(Binding binding)
	{
		return base.List.Contains(binding);
	}

	/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.Binding" /> from the <see cref="T:System.Web.Services.Description.BindingCollection" />.</summary>
	/// <param name="binding">The <see cref="T:System.Web.Services.Description.Binding" /> to remove from the collection. </param>
	public void Remove(Binding binding)
	{
		base.List.Remove(binding);
	}

	/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.BindingCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.Binding" />, starting at the specified zero-based index of the target array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.Binding" /> serving as the destination for the copy action. </param>
	/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
	public void CopyTo(Binding[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	protected override string GetKey(object value)
	{
		return ((Binding)value).Name;
	}

	protected override void SetParent(object value, object parent)
	{
		((Binding)value).SetParent((ServiceDescription)parent);
	}
}
