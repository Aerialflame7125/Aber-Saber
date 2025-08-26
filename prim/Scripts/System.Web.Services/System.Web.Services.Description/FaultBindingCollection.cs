namespace System.Web.Services.Description;

/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.FaultBinding" /> class. This class cannot be inherited.</summary>
public sealed class FaultBindingCollection : ServiceDescriptionBaseCollection
{
	/// <summary>Gets or sets the value of a <see cref="T:System.Web.Services.Description.FaultBinding" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.FaultBinding" /> whose value is modified or returned. </param>
	/// <returns>A <see langword="FaultBinding" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
	public FaultBinding this[int index]
	{
		get
		{
			return (FaultBinding)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.Services.Description.FaultBinding" /> specified by its name.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Services.Description.FaultBinding" /> returned. </param>
	/// <returns>A <see langword="FaultBinding" />.</returns>
	public FaultBinding this[string name] => (FaultBinding)Table[name];

	internal FaultBindingCollection(OperationBinding operationBinding)
		: base(operationBinding)
	{
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.FaultBinding" /> to the end of the <see cref="T:System.Web.Services.Description.FaultBindingCollection" />.</summary>
	/// <param name="bindingOperationFault">The <see cref="T:System.Web.Services.Description.FaultBinding" /> to add to the collection. </param>
	/// <returns>The zero-based index where the <paramref name="bindingOperationFault" /> parameter has been added.</returns>
	public int Add(FaultBinding bindingOperationFault)
	{
		return base.List.Add(bindingOperationFault);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.FaultBinding" /> to the <see langword="FaultBindingCollection" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index at which to insert the <paramref name="bindingOperationFault" /> parameter. </param>
	/// <param name="bindingOperationFault">The <see cref="T:System.Web.Services.Description.FaultBinding" /> to add to the collection. </param>
	public void Insert(int index, FaultBinding bindingOperationFault)
	{
		base.List.Insert(index, bindingOperationFault);
	}

	/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.FaultBinding" /> and returns the zero-based index of the first occurrence within the collection.</summary>
	/// <param name="bindingOperationFault">The <see cref="T:System.Web.Services.Description.FaultBinding" /> for which to search in the collection. </param>
	/// <returns>A 32-bit signed integer.</returns>
	public int IndexOf(FaultBinding bindingOperationFault)
	{
		return base.List.IndexOf(bindingOperationFault);
	}

	/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.FaultBinding" /> is a member of the <see cref="T:System.Web.Services.Description.FaultBindingCollection" />.</summary>
	/// <param name="bindingOperationFault">The <see cref="T:System.Web.Services.Description.FaultBinding" /> for which to check collection membership. </param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="bindingOperationFault" /> parameter is a member of the <see langword="FaultBindingCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(FaultBinding bindingOperationFault)
	{
		return base.List.Contains(bindingOperationFault);
	}

	/// <summary>Removes the first occurrence the specified <see cref="T:System.Web.Services.Description.FaultBinding" /> from the <see cref="T:System.Web.Services.Description.FaultBindingCollection" />.</summary>
	/// <param name="bindingOperationFault">The <see cref="T:System.Web.Services.Description.FaultBinding" /> to remove from the collection. </param>
	public void Remove(FaultBinding bindingOperationFault)
	{
		base.List.Remove(bindingOperationFault);
	}

	/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.FaultBindingCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.FaultBinding" />, starting at the specified zero-based index of the target array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.FaultBinding" /> serving as the destination for the copy action. </param>
	/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
	public void CopyTo(FaultBinding[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	protected override string GetKey(object value)
	{
		return ((FaultBinding)value).Name;
	}

	protected override void SetParent(object value, object parent)
	{
		((FaultBinding)value).SetParent((OperationBinding)parent);
	}
}
