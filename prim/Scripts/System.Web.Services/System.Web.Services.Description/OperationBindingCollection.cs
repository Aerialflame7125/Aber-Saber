namespace System.Web.Services.Description;

/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.OperationBinding" /> class. This class cannot be inherited.</summary>
public sealed class OperationBindingCollection : ServiceDescriptionBaseCollection
{
	/// <summary>Gets or sets the value of an <see cref="T:System.Web.Services.Description.OperationBinding" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.OperationBinding" /> whose value is modified or returned. </param>
	/// <returns>An <see langword="OperationBinding" />.</returns>
	public OperationBinding this[int index]
	{
		get
		{
			return (OperationBinding)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	internal OperationBindingCollection(Binding binding)
		: base(binding)
	{
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.OperationBinding" /> to the end of the <see cref="T:System.Web.Services.Description.OperationBindingCollection" />.</summary>
	/// <param name="bindingOperation">The <see cref="T:System.Web.Services.Description.OperationBinding" /> to add to the collection. </param>
	/// <returns>The zero-based index where the <paramref name="bindingOperation" /> parameter has been added.</returns>
	public int Add(OperationBinding bindingOperation)
	{
		return base.List.Add(bindingOperation);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.OperationBinding" /> instance to the <see cref="T:System.Web.Services.Description.OperationBindingCollection" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index at which to insert the <paramref name="bindingOperation" /> parameter. </param>
	/// <param name="bindingOperation">The <see cref="T:System.Web.Services.Description.OperationBinding" /> to add to the collection. </param>
	public void Insert(int index, OperationBinding bindingOperation)
	{
		base.List.Insert(index, bindingOperation);
	}

	/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.OperationBinding" /> and returns the zero-based index of the first occurrence within the collection.</summary>
	/// <param name="bindingOperation">The <see cref="T:System.Web.Services.Description.OperationBinding" /> for which to search in the collection. </param>
	/// <returns>A 32-bit signed integer.</returns>
	public int IndexOf(OperationBinding bindingOperation)
	{
		return base.List.IndexOf(bindingOperation);
	}

	/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.OperationBinding" /> is a member of the <see cref="T:System.Web.Services.Description.OperationBindingCollection" />.</summary>
	/// <param name="bindingOperation">The <see cref="T:System.Web.Services.Description.OperationBinding" /> for which to check collection membership. </param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="bindingOperation" /> parameter is a member of the <see cref="T:System.Web.Services.Description.OperationBindingCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(OperationBinding bindingOperation)
	{
		return base.List.Contains(bindingOperation);
	}

	/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.OperationBinding" /> from the <see cref="T:System.Web.Services.Description.OperationBindingCollection" />.</summary>
	/// <param name="bindingOperation">The <see cref="T:System.Web.Services.Description.OperationBinding" /> to remove from the collection. </param>
	public void Remove(OperationBinding bindingOperation)
	{
		base.List.Remove(bindingOperation);
	}

	/// <summary>Copies the entire <see langword="OperationBindingCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.OperationBinding" />, starting at the specified zero-based index of the target array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.OperationBinding" /> serving as the destination for the copy action. </param>
	/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
	public void CopyTo(OperationBinding[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	protected override void SetParent(object value, object parent)
	{
		((OperationBinding)value).SetParent((Binding)parent);
	}
}
