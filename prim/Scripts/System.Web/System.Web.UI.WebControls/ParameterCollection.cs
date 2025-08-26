using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.Parameter" /> and <see cref="T:System.Web.UI.WebControls.Parameter" />-derived objects that are used by data source controls in advanced data-binding scenarios.</summary>
[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
public class ParameterCollection : StateManagedCollection
{
	private static Type[] _knownTypes = new Type[6]
	{
		typeof(ControlParameter),
		typeof(CookieParameter),
		typeof(FormParameter),
		typeof(Parameter),
		typeof(QueryStringParameter),
		typeof(SessionParameter)
	};

	private EventHandler _parametersChanged;

	/// <summary>Gets or sets the <see cref="T:System.Web.UI.WebControls.Parameter" /> object at the specified index in the collection.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.Parameter" /> to retrieve from the collection. </param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.Parameter" /> at the specified index in the collection. </returns>
	public Parameter this[int index]
	{
		get
		{
			return (Parameter)((IList)this)[index];
		}
		set
		{
			((IList)this)[index] = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.UI.WebControls.Parameter" /> object with the specified name in the collection.</summary>
	/// <param name="name">The <see cref="P:System.Web.UI.WebControls.Parameter.Name" /> of the <see cref="T:System.Web.UI.WebControls.Parameter" /> to retrieve from the collection. </param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.Parameter" /> with the specified name in the collection. If the <see cref="T:System.Web.UI.WebControls.Parameter" /> is not found in the collection, the indexer returns <see langword="null" />.</returns>
	public Parameter this[string name]
	{
		get
		{
			int num = IndexOfString(name);
			if (num == -1)
			{
				return null;
			}
			return (Parameter)((IList)this)[num];
		}
		set
		{
			int num = IndexOfString(name);
			if (num == -1)
			{
				Add(value);
			}
			else
			{
				((IList)this)[num] = value;
			}
		}
	}

	/// <summary>Occurs when one or more <see cref="T:System.Web.UI.WebControls.Parameter" /> objects contained by the collection changes state.</summary>
	public event EventHandler ParametersChanged
	{
		add
		{
			_parametersChanged = (EventHandler)Delegate.Combine(_parametersChanged, value);
		}
		remove
		{
			_parametersChanged = (EventHandler)Delegate.Remove(_parametersChanged, value);
		}
	}

	/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.Parameter" /> object to the end of the collection.</summary>
	/// <param name="parameter">The <see cref="T:System.Web.UI.WebControls.Parameter" /> to append to the collection. </param>
	/// <returns>The index value of the added item.</returns>
	public int Add(Parameter parameter)
	{
		return ((IList)this).Add((object)parameter);
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.Parameter" /> object with the specified name and default value, and appends it to the end of the collection.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="value">A string that serves as a default value for the parameter. </param>
	/// <returns>The index value of the added item.</returns>
	public int Add(string name, string value)
	{
		return ((IList)this).Add((object)new Parameter(name, TypeCode.Object, value));
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.Parameter" /> object with the specified name, <see cref="T:System.TypeCode" />, and default value, and appends it to the end of the collection.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="type">The type of the parameter.</param>
	/// <param name="value">The default value for the parameter.</param>
	/// <returns>The index value of the added item.</returns>
	public int Add(string name, TypeCode type, string value)
	{
		return ((IList)this).Add((object)new Parameter(name, type, value));
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.Parameter" /> object with the specified name, database type, and default value, and adds it to the end of the collection.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="dbType">The database type of the parameter.</param>
	/// <param name="value">The default value for the parameter. </param>
	/// <returns>The index value of the added item.</returns>
	public int Add(string name, DbType dbType, string value)
	{
		return ((IList)this).Add((object)new Parameter(name, dbType, value));
	}

	/// <summary>Creates an instance of a default <see cref="T:System.Web.UI.WebControls.Parameter" /> object.</summary>
	/// <param name="index">The index of the type of <see cref="T:System.Web.UI.WebControls.Parameter" /> to create from the ordered list of types returned by <see cref="M:System.Web.UI.WebControls.ParameterCollection.GetKnownTypes" />. </param>
	/// <returns>A default instance of a <see cref="T:System.Web.UI.WebControls.Parameter" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is not within the recognized range. </exception>
	protected override object CreateKnownType(int index)
	{
		return index switch
		{
			0 => new ControlParameter(), 
			1 => new CookieParameter(), 
			2 => new FormParameter(), 
			3 => new Parameter(), 
			4 => new QueryStringParameter(), 
			5 => new SessionParameter(), 
			_ => throw new ArgumentOutOfRangeException("index"), 
		};
	}

	/// <summary>Gets an array of <see cref="T:System.Web.UI.WebControls.Parameter" /> types that the <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> collection can contain.</summary>
	/// <returns>An ordered array of <see cref="T:System.Type" /> objects that identify the types of <see cref="T:System.Web.UI.WebControls.Parameter" /> objects that the collection can contain.</returns>
	protected override Type[] GetKnownTypes()
	{
		return _knownTypes;
	}

	/// <summary>Gets an ordered collection of <see cref="T:System.Web.UI.WebControls.Parameter" /> object names and their corresponding values currently contained by the collection.</summary>
	/// <param name="context">The current <see cref="T:System.Web.HttpRequest" /> that the <see cref="T:System.Web.UI.WebControls.Parameter" /> binds to.</param>
	/// <param name="control">The <see cref="T:System.Web.UI.Control" /> instance that is passed to each parameter's <see cref="M:System.Web.UI.WebControls.ControlParameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> method. </param>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of name/value pairs.</returns>
	public IOrderedDictionary GetValues(HttpContext context, Control control)
	{
		OrderedDictionary orderedDictionary = new OrderedDictionary();
		IEnumerator enumerator = GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Parameter parameter = (Parameter)enumerator.Current;
				string key = parameter.Name;
				int num = 1;
				while (orderedDictionary.Contains(key))
				{
					key = parameter.Name + num;
					num++;
				}
				orderedDictionary.Add(key, parameter.GetValue(context, control));
			}
			return orderedDictionary;
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
	}

	/// <summary>Iterates through the <see cref="T:System.Web.UI.WebControls.Parameter" /> objects contained by the collection, and calls the <see langword="Evaluate" /> method on each one.</summary>
	/// <param name="context">The current <see cref="T:System.Web.HttpRequest" /> that the <see cref="T:System.Web.UI.WebControls.Parameter" /> binds to.</param>
	/// <param name="control">The <see cref="T:System.Web.UI.Control" /> instance that is passed to each parameter's <see cref="M:System.Web.UI.WebControls.ControlParameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> method. </param>
	public void UpdateValues(HttpContext context, Control control)
	{
		IEnumerator enumerator = GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				((Parameter)enumerator.Current).UpdateValue(context, control);
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
	}

	/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.Parameter" /> object into the <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> collection at the specified index.</summary>
	/// <param name="index">The zero-based index at which the <see cref="T:System.Web.UI.WebControls.Parameter" /> is inserted. </param>
	/// <param name="parameter">The <see cref="T:System.Web.UI.WebControls.Parameter" /> to insert. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is less than zero.-or- 
	///         <paramref name="index" /> is greater than <see langword="Count" />. </exception>
	public void Insert(int index, Parameter parameter)
	{
		((IList)this).Insert(index, (object)parameter);
	}

	/// <summary>Performs additional custom processes after clearing the contents of the collection.</summary>
	protected override void OnClearComplete()
	{
		base.OnClearComplete();
		OnParametersChanged(EventArgs.Empty);
	}

	/// <summary>Occurs before the <see cref="M:System.Web.UI.WebControls.ParameterCollection.Insert(System.Int32,System.Web.UI.WebControls.Parameter)" /> method is called.</summary>
	/// <param name="index">The index in the collection that the <see cref="T:System.Web.UI.WebControls.Parameter" /> is inserted at. </param>
	/// <param name="value">The <see cref="T:System.Web.UI.WebControls.Parameter" /> that is inserted into the <see cref="T:System.Web.UI.WebControls.ParameterCollection" />. </param>
	protected override void OnInsert(int index, object value)
	{
		base.OnInsert(index, value);
		((Parameter)value).SetOwnerCollection(this);
	}

	/// <summary>Occurs after the <see cref="M:System.Web.UI.WebControls.ParameterCollection.Insert(System.Int32,System.Web.UI.WebControls.Parameter)" /> method completes.</summary>
	/// <param name="index">The index in the collection that the <see cref="T:System.Web.UI.WebControls.Parameter" /> was inserted at. </param>
	/// <param name="value">The <see cref="T:System.Web.UI.WebControls.Parameter" /> that was inserted into the <see cref="T:System.Web.UI.WebControls.ParameterCollection" />. </param>
	protected override void OnInsertComplete(int index, object value)
	{
		base.OnInsertComplete(index, value);
		OnParametersChanged(EventArgs.Empty);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ParameterCollection.ParametersChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnParametersChanged(EventArgs e)
	{
		if (_parametersChanged != null)
		{
			_parametersChanged(this, e);
		}
	}

	/// <summary>Performs additional custom processes when validating a value.</summary>
	/// <param name="o">The <see langword="object" /> being validated. </param>
	/// <exception cref="T:System.ArgumentException">The object is not an instance of the <see cref="T:System.Web.UI.WebControls.Parameter" /> class or one of its derived classes. </exception>
	/// <exception cref="T:System.ArgumentNullException">The object is <see langword="null" />. </exception>
	protected override void OnValidate(object o)
	{
		base.OnValidate(o);
		if (!(o is Parameter))
		{
			throw new ArgumentException("o is not a Parameter");
		}
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.Parameter" /> object from the <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> collection.</summary>
	/// <param name="parameter">The <see cref="T:System.Web.UI.WebControls.Parameter" /> to remove from the <see cref="T:System.Web.UI.WebControls.ParameterCollection" />. </param>
	public void Remove(Parameter parameter)
	{
		((IList)this).Remove((object)parameter);
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.Parameter" /> object at the specified index from the <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> collection.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.Parameter" /> to remove. </param>
	public void RemoveAt(int index)
	{
		((IList)this).RemoveAt(index);
	}

	/// <summary>Marks the specified <see cref="T:System.Web.UI.WebControls.Parameter" /> object as having changed since the last load or save from view state.</summary>
	/// <param name="o">The <see cref="T:System.Web.UI.WebControls.Parameter" /> to mark as having changed since the last load or save from view state. </param>
	protected override void SetDirtyObject(object o)
	{
		((Parameter)o).SetDirty();
	}

	internal void CallOnParameterChanged()
	{
		OnParametersChanged(EventArgs.Empty);
	}

	private int IndexOfString(string name)
	{
		for (int i = 0; i < base.Count; i++)
		{
			if (string.Compare(((Parameter)((IList)this)[i]).Name, name, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return i;
			}
		}
		return -1;
	}

	/// <summary>Determines whether the <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> collection contains a specific value</summary>
	/// <param name="parameter">The <see cref="T:System.Web.UI.WebControls.Parameter" /> to locate in the <see cref="T:System.Web.UI.WebControls.ParameterCollection" />.</param>
	/// <returns>
	///     <see langword="true" /> if the object is found in the <see cref="T:System.Web.UI.WebControls.ParameterCollection" />; otherwise, <see langword="false" />. If <see langword="null" /> is passed for the <paramref name="value" /> parameter, <see langword="false" /> is returned.</returns>
	public bool Contains(Parameter parameter)
	{
		return ((IList)this).Contains((object)parameter);
	}

	/// <summary>Copies a specified index of a parameter array to the parameter collection.</summary>
	/// <param name="parameterArray">Parameter array from which the value at a specified index is to be copied from.</param>
	/// <param name="index">The <see langword="integer" /> index of the <paramref name="parameterArray" /> item that is to be copied. </param>
	public void CopyTo(Parameter[] parameterArray, int index)
	{
		((ICollection)this).CopyTo((Array)parameterArray, index);
	}

	/// <summary>Determines the index of a specified <see cref="T:System.Web.UI.WebControls.Parameter" /> object in the <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> collection.</summary>
	/// <param name="parameter">The <see cref="T:System.Web.UI.WebControls.Parameter" /> to locate in the <see cref="T:System.Web.UI.WebControls.ParameterCollection" />.</param>
	/// <returns>The index of <paramref name="parameter" />, if it is found in the collection; otherwise, -1.</returns>
	public int IndexOf(Parameter parameter)
	{
		return ((IList)this).IndexOf((object)parameter);
	}

	/// <summary>Occurs after the <see cref="M:System.Web.UI.WebControls.ParameterCollection.Remove(System.Web.UI.WebControls.Parameter)" /> method completes.</summary>
	/// <param name="index">The index in the collection that the <see cref="T:System.Web.UI.WebControls.Parameter" /> was removed from. </param>
	/// <param name="value">The <see cref="T:System.Web.UI.WebControls.Parameter" /> that was removed from the <see cref="T:System.Web.UI.WebControls.ParameterCollection" />. </param>
	protected override void OnRemoveComplete(int index, object value)
	{
		base.OnRemoveComplete(index, value);
		OnParametersChanged(EventArgs.Empty);
	}

	/// <summary>Initializes the class for use by an inherited class instance. This constructor can be called only by an inherited class.</summary>
	public ParameterCollection()
	{
	}
}
