namespace System.Web.Services.Description;

/// <summary>Represents a collection of <see cref="T:System.Web.Services.Description.OperationInput" /> and <see cref="T:System.Web.Services.Description.OperationOutput" /> messages related to an XML Web service. This class cannot be inherited.</summary>
public sealed class OperationMessageCollection : ServiceDescriptionBaseCollection
{
	/// <summary>Gets or sets the value of an <see cref="T:System.Web.Services.Description.OperationMessage" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.OperationMessage" /> whose value is modified or returned.</param>
	/// <returns>An <see langword="OperationMessage" /> at the specified zero-based index.</returns>
	public OperationMessage this[int index]
	{
		get
		{
			return (OperationMessage)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	/// <summary>Gets the first occurrence of an <see cref="T:System.Web.Services.Description.OperationInput" /> within the collection.</summary>
	/// <returns>An <see cref="T:System.Web.Services.Description.OperationInput" /> within the collection.</returns>
	public OperationInput Input
	{
		get
		{
			for (int i = 0; i < base.List.Count; i++)
			{
				if (base.List[i] is OperationInput result)
				{
					return result;
				}
			}
			return null;
		}
	}

	/// <summary>Gets the first occurrence of an <see cref="T:System.Web.Services.Description.OperationOutput" /> within the collection.</summary>
	/// <returns>An <see cref="T:System.Web.Services.Description.OperationOutput" /> within the collection.</returns>
	public OperationOutput Output
	{
		get
		{
			for (int i = 0; i < base.List.Count; i++)
			{
				if (base.List[i] is OperationOutput result)
				{
					return result;
				}
			}
			return null;
		}
	}

	/// <summary>Gets the type of transmission supported by the <see cref="T:System.Web.Services.Description.OperationMessageCollection" />.</summary>
	/// <returns>One of the <see cref="T:System.Web.Services.Description.OperationFlow" /> values. The default is <see langword="SolicitResponse" />.</returns>
	public OperationFlow Flow
	{
		get
		{
			if (base.List.Count == 0)
			{
				return OperationFlow.None;
			}
			if (base.List.Count == 1)
			{
				if (base.List[0] is OperationInput)
				{
					return OperationFlow.OneWay;
				}
				return OperationFlow.Notification;
			}
			if (base.List[0] is OperationInput)
			{
				return OperationFlow.RequestResponse;
			}
			return OperationFlow.SolicitResponse;
		}
	}

	internal OperationMessageCollection(Operation operation)
		: base(operation)
	{
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.OperationMessage" /> to the end of the <see cref="T:System.Web.Services.Description.OperationMessageCollection" />.</summary>
	/// <param name="operationMessage">The <see cref="T:System.Web.Services.Description.OperationMessage" /> to add to the collection.</param>
	/// <returns>The zero-based index where the <paramref name="operationMessage" /> parameter has been added.</returns>
	public int Add(OperationMessage operationMessage)
	{
		return base.List.Add(operationMessage);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.OperationMessage" /> to the <see cref="T:System.Web.Services.Description.OperationMessageCollection" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index at which to insert the <paramref name="operationMessage" /> parameter.</param>
	/// <param name="operationMessage">The <see cref="T:System.Web.Services.Description.OperationMessage" /> to add to the collection.</param>
	public void Insert(int index, OperationMessage operationMessage)
	{
		base.List.Insert(index, operationMessage);
	}

	/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.OperationMessage" /> and returns the zero-based index of the first occurrence within the collection.</summary>
	/// <param name="operationMessage">The <see cref="T:System.Web.Services.Description.OperationMessage" /> for which to search in the collection.</param>
	/// <returns>The zero-based index of the specified operation message, or -1 if the element was not found in the collection.</returns>
	public int IndexOf(OperationMessage operationMessage)
	{
		return base.List.IndexOf(operationMessage);
	}

	/// <summary>Determines whether the specified <see cref="T:System.Web.Services.Description.OperationMessage" /> is a member of the <see cref="T:System.Web.Services.Description.OperationMessageCollection" />.</summary>
	/// <param name="operationMessage">The <see cref="T:System.Web.Services.Description.OperationMessage" /> for which to check collection membership.</param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="operationMessage" /> parameter is a member of the <see cref="T:System.Web.Services.Description.OperationMessageCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(OperationMessage operationMessage)
	{
		return base.List.Contains(operationMessage);
	}

	/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.OperationMessage" /> from the <see cref="T:System.Web.Services.Description.OperationMessageCollection" />.</summary>
	/// <param name="operationMessage">The <see cref="T:System.Web.Services.Description.OperationMessage" /> to remove from the collection.</param>
	public void Remove(OperationMessage operationMessage)
	{
		base.List.Remove(operationMessage);
	}

	/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.OperationMessageCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.OperationMessage" />, starting at the specified zero-based index of the target array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.OperationMessage" /> serving as the destination for the copy action.</param>
	/// <param name="index">The zero-based index at which to start placing the copied collection.</param>
	public void CopyTo(OperationMessage[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	protected override void SetParent(object value, object parent)
	{
		((OperationMessage)value).SetParent((Operation)parent);
	}

	protected override void OnInsert(int index, object value)
	{
		if (base.Count > 1 || (base.Count == 1 && value.GetType() == base.List[0].GetType()))
		{
			throw new InvalidOperationException(Res.GetString("WebDescriptionTooManyMessages"));
		}
		base.OnInsert(index, value);
	}

	protected override void OnSet(int index, object oldValue, object newValue)
	{
		if (oldValue.GetType() != newValue.GetType())
		{
			throw new InvalidOperationException(Res.GetString("WebDescriptionTooManyMessages"));
		}
		base.OnSet(index, oldValue, newValue);
	}

	protected override void OnValidate(object value)
	{
		if (!(value is OperationInput) && !(value is OperationOutput))
		{
			throw new ArgumentException(Res.GetString("OnlyOperationInputOrOperationOutputTypes"), "value");
		}
		base.OnValidate(value);
	}
}
