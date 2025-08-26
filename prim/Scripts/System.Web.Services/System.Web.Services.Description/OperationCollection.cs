namespace System.Web.Services.Description;

/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.Operation" /> class. This class cannot be inherited.</summary>
public sealed class OperationCollection : ServiceDescriptionBaseCollection
{
	/// <summary>Gets or sets the value of an <see cref="T:System.Web.Services.Description.Operation" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.Operation" /> whose value is modified or returned. </param>
	/// <returns>An <see langword="Operation" />.</returns>
	public Operation this[int index]
	{
		get
		{
			return (Operation)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	internal OperationCollection(PortType portType)
		: base(portType)
	{
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Operation" /> to the end of the <see cref="T:System.Web.Services.Description.OperationCollection" />.</summary>
	/// <param name="operation">The <see cref="T:System.Web.Services.Description.Operation" /> to add to the collection. </param>
	/// <returns>The zero-based index where the <paramref name="operation" /> parameter has been added.</returns>
	public int Add(Operation operation)
	{
		return base.List.Add(operation);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Operation" /> to the <see cref="T:System.Web.Services.Description.OperationCollection" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index at which to insert the <paramref name="operation" /> parameter. </param>
	/// <param name="operation">The <see cref="T:System.Web.Services.Description.Operation" /> to add to the collection. </param>
	public void Insert(int index, Operation operation)
	{
		base.List.Insert(index, operation);
	}

	/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.Operation" /> and returns the zero-based index of the first occurrence within the collection.</summary>
	/// <param name="operation">The <see cref="T:System.Web.Services.Description.Operation" /> for which to search in the collection. </param>
	/// <returns>A 32-bit signed integer.</returns>
	public int IndexOf(Operation operation)
	{
		return base.List.IndexOf(operation);
	}

	/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.Operation" /> is a member of the <see cref="T:System.Web.Services.Description.OperationCollection" />.</summary>
	/// <param name="operation">The <see cref="T:System.Web.Services.Description.Operation" /> for which to check collection membership. </param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="operation" /> is a member of the <see cref="T:System.Web.Services.Description.OperationCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(Operation operation)
	{
		return base.List.Contains(operation);
	}

	/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.Operation" /> from the <see cref="T:System.Web.Services.Description.OperationCollection" />.</summary>
	/// <param name="operation">The <see cref="T:System.Web.Services.Description.Operation" /> to remove from the collection. </param>
	public void Remove(Operation operation)
	{
		base.List.Remove(operation);
	}

	/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.OperationCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.Operation" />, starting at the specified zero-based index of the target array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.Operation" /> serving as the destination for the copy action. </param>
	/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
	public void CopyTo(Operation[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	protected override void SetParent(object value, object parent)
	{
		((Operation)value).SetParent((PortType)parent);
	}
}
