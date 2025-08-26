namespace System.Web.Services.Description;

/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.OperationFault" /> class. This class cannot be inherited.</summary>
public sealed class OperationFaultCollection : ServiceDescriptionBaseCollection
{
	/// <summary>Gets or sets the value of an <see cref="T:System.Web.Services.Description.OperationFault" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.OperationFault" /> whose value is modified or returned. </param>
	/// <returns>An <see langword="OperationFault" />.</returns>
	public OperationFault this[int index]
	{
		get
		{
			return (OperationFault)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	/// <summary>Gets an <see cref="T:System.Web.Services.Description.OperationFault" /> by its name.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Services.Description.OperationFault" /> returned. </param>
	/// <returns>An <see langword="OperationFault" />.</returns>
	public OperationFault this[string name] => (OperationFault)Table[name];

	internal OperationFaultCollection(Operation operation)
		: base(operation)
	{
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.OperationFault" /> to the end of the <see cref="T:System.Web.Services.Description.OperationFaultCollection" />.</summary>
	/// <param name="operationFaultMessage">The <see cref="T:System.Web.Services.Description.OperationFault" /> to add to the collection. </param>
	/// <returns>The zero-based index where the <paramref name="operationFaultMessage" /> parameter has been added.</returns>
	public int Add(OperationFault operationFaultMessage)
	{
		return base.List.Add(operationFaultMessage);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.OperationFault" /> to the <see cref="T:System.Web.Services.Description.OperationFaultCollection" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index at which to insert the <paramref name="operationFaultMessage" /> parameter. </param>
	/// <param name="operationFaultMessage">The <see cref="T:System.Web.Services.Description.OperationFault" /> to add to the collection. </param>
	public void Insert(int index, OperationFault operationFaultMessage)
	{
		base.List.Insert(index, operationFaultMessage);
	}

	/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.OperationFault" /> and returns the zero-based index of the first occurrence within the collection.</summary>
	/// <param name="operationFaultMessage">The <see cref="T:System.Web.Services.Description.OperationFault" /> for which to search in the collection. </param>
	/// <returns>A 32-bit signed integer.</returns>
	public int IndexOf(OperationFault operationFaultMessage)
	{
		return base.List.IndexOf(operationFaultMessage);
	}

	/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.OperationFault" /> is a member of the <see cref="T:System.Web.Services.Description.OperationFaultCollection" />.</summary>
	/// <param name="operationFaultMessage">The <see cref="T:System.Web.Services.Description.OperationFault" /> for which to check collection membership. </param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="operationFaultMessage" /> parameter is a member of the <see cref="T:System.Web.Services.Description.OperationFaultCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(OperationFault operationFaultMessage)
	{
		return base.List.Contains(operationFaultMessage);
	}

	/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.OperationFault" /> from the <see cref="T:System.Web.Services.Description.OperationFaultCollection" />.</summary>
	/// <param name="operationFaultMessage">The <see cref="T:System.Web.Services.Description.OperationFault" /> to remove from the collection. </param>
	public void Remove(OperationFault operationFaultMessage)
	{
		base.List.Remove(operationFaultMessage);
	}

	/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.OperationFaultCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.OperationFault" />, starting at the specified zero-based index of the target array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.OperationFault" /> serving as the destination of the copy action. </param>
	/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
	public void CopyTo(OperationFault[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	protected override string GetKey(object value)
	{
		return ((OperationFault)value).Name;
	}

	protected override void SetParent(object value, object parent)
	{
		((OperationFault)value).SetParent((Operation)parent);
	}
}
